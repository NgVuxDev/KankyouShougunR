using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using Shougun.Printing.Common;

namespace Shougun.Printing.Common.UI
{
    /// <summary>
    /// 環境将軍R印刷管理/帳票印刷設定ダイアログ(モーダル)
    /// </summary>
    public partial class ServerPrintSettingsDialog : Form
    {
        /// <summary>
        /// エラーメッセージの表示
        /// </summary>
        /// <param name="message"></param>
        private void showErrorMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 帳票設定ダイアログコンストラクタ
        /// </summary>
        public ServerPrintSettingsDialog()
        {
            InitializeComponent();
            this.BackColor = UI.FormStyle.FormBackColor;
            this.label1.BackColor = UI.FormStyle.LabelBackColor;
            this.label1.ForeColor = UI.FormStyle.LabelForeColor;
            this.label2.BackColor = UI.FormStyle.LabelBackColor;
            this.label2.ForeColor = UI.FormStyle.LabelForeColor;
            this.label3.BackColor = UI.FormStyle.LabelBackColor;
            this.label3.ForeColor = UI.FormStyle.LabelForeColor;
            this.clientNameLabel.BackColor = UI.FormStyle.ReadOnlyBackColor;
        }


        /// <summary>
        /// OnLoad
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var clientName = ServerSettings.GetClientComputerName();
            if (string.IsNullOrEmpty(clientName))
            {
                UI.ErrorMessageBox.ShowLastError();
                this.Close();
                return;
            }

            this.clientNameLabel.Text = clientName;

            string directory = ServerSettings.GetClientSidePrintingDirectory();
            if (string.IsNullOrEmpty(directory))
            {
                if (LastError.HasError)
                {
                    //UI.ErrorMessageBox.ShowLastError();
                }
            }
            else
            {
                printingDirectoryTextBox.Text = directory;
            }
            this.storeButton.Enabled = false;
        }


        /// <summary>
        /// F9保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storeButton_Click(object sender, EventArgs e)
        {
            if (this.storeButton.Enabled)
            {
                var result = MessageBox.Show("変更を保存しますか？", "印刷設定", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }

                var directory = printingDirectoryTextBox.Text;
                if (!Directory.Exists(directory) && directory != string.Empty)
                {
                    UI.ErrorMessageBox.Show("指定されたディレクトリは存在しません");
                    return;
                }

                if (directory == string.Empty)
                {
                    var blankResult = MessageBox.Show("印刷処理が行えなくなりますが、よろしいでしょうか？", "印刷設定",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (blankResult != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (!ServerSettings.SetPrintingDirectory(directory))
                {
                    UI.ErrorMessageBox.ShowLastError();
                }

                this.storeButton.Enabled = false;
            }
        }

        /// <summary>
        /// ユーザー終了確認
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.storeButton.Enabled)
            {
                var result = MessageBox.Show("変更を破棄して終了してもよろしいですか？", "印刷設定", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

                int serverRev, clientRev;
                if (Shougun.Printing.Verup.Launcher.ExistsVerupServerSide(out serverRev, out clientRev))
                {
                    Shougun.Printing.Verup.Launcher.LaunchVerupServerSide();
                }
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// キー押下イベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyData)
            {
                case Keys.F8:
                    testButton_Click(testButton, EventArgs.Empty);
                    break;
                case Keys.F9:
                    storeButton_Click(storeButton, EventArgs.Empty);
                    break;
                case Keys.F12:
                    this.Close();
                    break;
                default:
                    return;
            }
            e.Handled = true;
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            printingDirectoryTextBox.Text = Clipboard.GetText();
            this.storeButton.Enabled = true;
        }

        private void printingDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            this.storeButton.Enabled = true;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            var directory = printingDirectoryTextBox.Text;
            if (Directory.Exists(directory))
            {
                var source = @"Setting\ServerToClientPrintingTest.xps";
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_0_R000_A2_DS_C1.xps";
                var destination = Path.Combine(directory, fileName);
                System.IO.File.Copy(source, destination, false);
            }
            else
            {
                UI.ErrorMessageBox.Show("指定されたディレクトリは存在しません");
            }
        }
    }
}
