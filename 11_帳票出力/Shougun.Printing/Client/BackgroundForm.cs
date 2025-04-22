using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shougun.Printing.Common;
using Shougun.Printing.Common.UI;

namespace Shougun.Printing.Client
{
    public partial class BackgroundForm : Form
    {
        private Form monitorWindow;
        private System.Windows.Forms.ToolStripMenuItem TerminateMenuItem;

        public BackgroundForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.notifyIcon.Visible = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Hide();
            this.monitorWindow = Shougun.Printing.Manager.MonitorForm.CreateFormAndStartMonitoring();
            if (Common.Initializer.ProcessMode == ProcessMode.CloudClientSideProcess)
            {
                string revFile = System.IO.Path.Combine(LocalDirectories.AppProgramDirectory, "Shougun.Printing.Revision.txt");
                int revision = 0;
                if (System.IO.File.Exists(revFile))
                {
                    string data = System.IO.File.ReadAllText(revFile, Encoding.UTF8);
                    int.TryParse(data, out revision);
                }
                this.programMenuItem.Text = string.Format("{0}\r\n     Revision:{1}", Application.ProductName, revision); 
                this.notifyIcon.Visible = true;
                this.notifyIcon.BalloonTipText = string.Format("{0}(Revision:{1})を起動しました", Application.ProductName, revision);
                this.notifyIcon.ShowBalloonTip(10000);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.monitorWindow != null)
            {
                Shougun.Printing.Manager.MonitorForm.TerminateMonitoring();
            }
            this.notifyIcon.Visible = false;
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Exit();
        }

        private void DestinationCopyMenuItem_Click(object sender, EventArgs e)
        {
            var printingDirectory = LocalDirectories.PrintingDirectory;
            if (string.IsNullOrEmpty(printingDirectory))
            {
                return;
            }

            var root = System.IO.Path.GetPathRoot(printingDirectory);
            if (root.Length != 3) // ex) "c:\"
            {
                return;
            }

            printingDirectory = string.Format(@"\\tsclient\{0}\{1}", root[0], printingDirectory.Substring(3));


            Clipboard.SetDataObject(printingDirectory, true);

            this.notifyIcon.BalloonTipText = "クリップボードにパス情報を貼り付けました。\r\n" + printingDirectory;
            this.notifyIcon.ShowBalloonTip(10000);
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.monitorWindow.Visible)
                {
                    this.monitorWindow.Activate();
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (this.monitorWindow.Visible)
            {
                this.monitorWindow.Activate();
            }
        }

        private void PrintSettingsMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new ReportSettingsDialog();
            dialog.ShowDialog();
            dialog.Dispose();
            dialog = null;
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            var message = "印刷プログラムを終了します。よろしいですか？\r\n印刷プログラムを終了後に環境将軍Rを使用する場合は、スタートメニューから印刷プログラムを再度実行してください。";
            var dialogResult = MessageBox.Show(message, Application.ProductName, 
                                    MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Question, 
                                    MessageBoxDefaultButton.Button2, 
                                    MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
