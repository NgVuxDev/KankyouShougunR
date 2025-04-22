using Shougun.Core.ExternalConnection.CommunicateLib.Const;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Shougun.Core.ExternalConnection.CommunicateLib.Helpers
{
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    public class RemoteAppHelper
    {
        [DllImport("User32.dll")]
        private static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        //For use with WM_COPYDATA and COPYDATASTRUCT

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        //For use with WM_COPYDATA and COPYDATASTRUCT

        [DllImport("User32.dll", EntryPoint = "PostMessage", CharSet = CharSet.Unicode)]
        public static extern int PostMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage", CharSet = CharSet.Unicode)]
        public static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(int hWnd);

        //Used for WM_COPYDATA for string messages
        
        public bool BringAppToFront(int hWnd)
        {
            return SetForegroundWindow(hWnd);
        }

        public int SendWindowsStringMessage(int hWnd, int wParam, string msg)
        {
            int result = 0;
            if (hWnd > 0)
            {
                byte[] sarr = Encoding.UTF8.GetBytes(msg);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = msg;
                cds.cbData = len + 1;
                result = SendMessage(hWnd, ConstCls.WM_COPYDATA, wParam, ref cds);
            }

            return result;
        }

        public int SendWindowsMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            int result = 0;
            if (hWnd > 0)
            {
                result = SendMessage(hWnd, Msg, wParam, lParam);
            }

            return result;
        }

        public int GetWindowId(string className, string windowName)
        {
            return FindWindow(className, windowName);
        }
    }
}