using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Text;

namespace Shougun.Printing.Common.UI
{

    public class DebugMessageWindow
    {
        private privateDebugMessageWindow window = null;

        private DebugMessageWindow()
        {
        }

        public DebugMessageWindow(string title, bool forceCreateMode)
        {
#if DEBUG
            forceCreateMode = true;
#endif
            if (forceCreateMode)
            {
                this.window = new privateDebugMessageWindow();
                this.window.Text = title;
                this.window.Show();
            }
        }

        public void Close()
        {
            if (this.window != null)
            {
                this.window.Close();
                this.window.Dispose();
                this.window = null;
            }
        }

        public void WriteLine()
        {
            if (this.window != null)
                this.window.WriteLine("");
        }

        public void WriteLine(string format, params object[] args)
        {
            if (this.window != null)
                this.window.WriteLine(string.Format(format, args));
        }

        public void WriteLine(string message)
        {
            if (this.window != null)
                this.window.WriteLine(message);
        }


        class privateDebugMessageWindow : Form
        {
            private IContainer components = null;
            private TextBox textBox1 = null;

            public privateDebugMessageWindow()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                this.components = new Container();
                this.textBox1 = new TextBox();
                this.SuspendLayout();
                this.textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                this.textBox1.BackColor = Color.White;
                this.textBox1.Location = new Point(0, 0);
                this.textBox1.Multiline = true;
                this.textBox1.Name = "textBox1";
                this.textBox1.ReadOnly = true;
                this.textBox1.ScrollBars = ScrollBars.Both;
                this.textBox1.Size = new Size(733, 416);
                this.textBox1.TabIndex = 0;

                this.AutoScaleDimensions = new SizeF(6F, 12F);
                this.AutoScaleMode = AutoScaleMode.Font;
                this.ClientSize = new Size(300, 300);
                this.Controls.Add(this.textBox1);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "DebugMessageWindow";
                this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                //this.TopMost = true;
                this.ResumeLayout(false);
                this.PerformLayout();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            private delegate void writeLineDelegate(string message);
            public void WriteLine(string message)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new writeLineDelegate(WriteLine), new object[] { message });
                    return;
                }
                var buf = new StringBuilder(this.textBox1.Text);
                buf.AppendFormat("\r\n{0} {1}", DateTime.Now.ToString("HH:mm:ss"), message);
                this.textBox1.Text = buf.ToString();
                this.textBox1.Select(this.textBox1.Text.Length, 0);
                this.textBox1.ScrollToCaret();
            }
        }
    }
}
