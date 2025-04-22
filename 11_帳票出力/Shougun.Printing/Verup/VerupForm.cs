using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Permissions;


namespace Shougun.Printing.Verup
{
    public partial class VerupForm : Form
    {
        private static VerupForm instance = null;
        public static void WriteLine()
        {
            if (VerupForm.instance != null)
            {
                VerupForm.instance.writeLine("");
            }
        }

        public static void WriteLine(string format, params object[] args)
        {
            if (VerupForm.instance != null)
            {
                VerupForm.instance.writeLine(string.Format(format, args));
            }
        }

        public static void WriteLine(string message)
        {
            if (VerupForm.instance != null)
            {
                VerupForm.instance.writeLine(message);
            }
        }

        public static void Complete(bool success)
        {
            if (VerupForm.instance != null)
            {
                VerupForm.instance.complete(success);
            }
        }

        public string[] argv = null;
        private bool canClosable = false;

        public VerupForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ユーザーによるフォームを閉じる操作を無効化する。
        /// Task Barのサムネイルウインドウの×ボタン
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!this.canClosable)
            {
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }
        
        protected override void OnShown(EventArgs e)
        {
            this.Text = Application.ProductName;
            base.OnShown(e);
            VerupForm.instance = this;


            VerupForm.WriteLine("開始:{0}", DateTime.Now.ToString("yyyy/MM/dd(ddd)"));

            if (this.argv[0].Equals("CloudServerSideProcess"))
            {
                var proc = new VerupServerProc();
                var thread = new Thread(new ThreadStart(proc.Run));
                thread.Start();

            }
            else if (argv[0].Equals("CloudClientSideProcess"))
            {
                var proc = new VerupClientProc();
                proc.WaitClientProcessid = int.Parse(argv[1]);
                proc.ProgramDir = argv[2].Trim('\"');
                var thread = new Thread(new ThreadStart(proc.Run));
                thread.Start();
            }
            else
            {
                Application.Exit();
            }
        }

        private delegate void writeLineDelegate(string message);
        private void writeLine(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new writeLineDelegate(writeLine), new object[] { message });
                Thread.Sleep(200);
                return;
            }
            var buf = new StringBuilder(this.textBox1.Text);
            buf.AppendFormat("\r\n{0} {1}", DateTime.Now.ToString("HH:mm:ss"), message);
            this.textBox1.Text = buf.ToString();
            this.textBox1.Select(this.textBox1.Text.Length, 0);
            this.textBox1.ScrollToCaret();
        }

        private delegate void completeDelegate(bool success);
        private void complete(bool success)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new completeDelegate(complete), new object[] { success });
                return;
            }

            WriteLine("更新ログを保存");
            var logPath = VerupIO.GetVersionUpLogFilePasth();
            WriteLine("    {0}", logPath);
            VerupIO.WriteVerupLogFile(logPath, this.textBox1.Text);

            this.canClosable = true;
            if (success)
            {
                WriteLine("このウインドウは10秒後に自動的に閉じます");
                this.closeTimer.Interval = 10000;
                this.closeTimer.Enabled = true;
            }
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.closeTimer.Enabled = false;
            this.Close();
        }
    }
}
