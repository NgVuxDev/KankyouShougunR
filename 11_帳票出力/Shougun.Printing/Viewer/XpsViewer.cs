using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;

namespace Shougun.Printing.Viewer
{
    public class XpsViewer
    {
        private ViewerWindow viewerWindow = null;
        public string XpsPath { get; private set; }

        public XpsViewer()
        {
            this.viewerWindow = new Shougun.Printing.Viewer.ViewerWindow();
            this.viewerWindow.CommandEventHandler += onCommandHandler;
        }

        public void ViewXpsFile(string path, string title)
        {
            try
            {
                this.viewerWindow.ViewXpsFile(path, title);
                this.XpsPath = path;
            }
            catch
            {
            }
        }

        public bool Show()
        {
            //すべてのプロセスを列挙する
            IntPtr targetHWnd = (IntPtr)0;
            Rectangle rect = Rectangle.Empty;
            foreach (System.Diagnostics.Process p
                in System.Diagnostics.Process.GetProcesses())
            {
                // 印刷PG(MonitorForm)の座標を取得
                if (p.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    RECT winRect = new RECT();
                    targetHWnd = p.MainWindowHandle;
                    GetWindowRect(p.MainWindowHandle, ref winRect);
                    rect = new Rectangle(winRect.left, winRect.top,
                                         winRect.right - winRect.left,
                                         winRect.bottom - winRect.top);
                    break;
                }
            }

            if (this.viewerWindow.WindowState == System.Windows.WindowState.Minimized)
            {
                this.viewerWindow.WindowState = System.Windows.WindowState.Normal;
            }

            bool adjustLayout = this.viewerWindow.RestoreBounds.IsEmpty;

            this.viewerWindow.Show();

            if (adjustLayout)
            {
                if (rect != Rectangle.Empty)
                {
                    // MonitorFormの中心に表示
                    this.viewerWindow.Left = rect.X - ((this.viewerWindow.Width - rect.Width) / 2);
                    this.viewerWindow.Top = rect.Y - ((this.viewerWindow.Height - rect.Height) / 2);
                }

                if (targetHWnd != (IntPtr)0)
                {
                    try
                    {
                        var screen = System.Windows.Forms.Screen.FromHandle(targetHWnd);
                        int overRight = (int)(this.viewerWindow.Left + this.viewerWindow.Width) - screen.Bounds.Right;
                        if (overRight > 0)
                        {
                            this.viewerWindow.Left -= overRight;
                        }
                        int overBottom = (int)(this.viewerWindow.Top + this.viewerWindow.Height) - screen.Bounds.Bottom;
                        if (overBottom > 0)
                        {
                            this.viewerWindow.Top -= overBottom;
                        }
                        if (this.viewerWindow.Top < screen.Bounds.Top)
                        {
                            this.viewerWindow.Top = screen.Bounds.Top;
                        }
                        if (this.viewerWindow.Left < screen.Bounds.Left)
                        {
                            this.viewerWindow.Left = screen.Bounds.Left;
                        }
                        if (this.viewerWindow.Width > screen.Bounds.Size.Width)
                        {
                            this.viewerWindow.Width = screen.Bounds.Size.Width;
                        }
                        if (this.viewerWindow.Height > screen.Bounds.Size.Height)
                        {
                            this.viewerWindow.Height = screen.Bounds.Size.Height;
                        }
                    }
                    catch (Exception) { }
                }
            }

            this.viewerWindow.Activate();
            return true;
        }


        public bool IsAcive
        {
            get
            {
                return this.viewerWindow.IsActive;
            }
        }

        public event CommandEventHandler CommandEventHandler;
        private void onCommandHandler(object sender, string command)
        {
            if (CommandEventHandler != null)
            {
                CommandEventHandler(this, command);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindWindow(
            string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        public void Block(bool block)
        {
            IntPtr hWnd = FindWindow(null, this.viewerWindow.Title);
            if (hWnd != IntPtr.Zero)
            {
                EnableWindow(hWnd, !block);
            }
        }

        public void Close()
        {
            if (this.viewerWindow != null)
            {
                this.viewerWindow.Close();
            }
        }

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
