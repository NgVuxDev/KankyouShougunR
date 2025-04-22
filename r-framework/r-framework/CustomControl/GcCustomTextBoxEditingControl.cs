using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using System.Runtime.InteropServices;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 入力用コントロール。MultiRow.TextBoxEditingControlからの派生。
    /// </summary>
    public class GcCustomTextBoxEditingControl : TextBoxEditingControl
    {
        const Int32 WM_USER = 0x400;
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (!this.ReadOnly)
            {
                // アクティブなテキストボックスのIME変換モードを設定する。
                // 勝手に変換モードが無変換になってしまうことがある現象の対策。
                r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this);

                // フォーカス取得で既存テキストを全選択状態にする
                PostMessage(this.Handle, WM_USER + 1, IntPtr.Zero, IntPtr.Zero);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (WM_USER+1))
            {
                if (!this.IsDisposed && this.Focused)
                {
                    this.SelectAll();
                }
                return;
            }

            base.WndProc(ref m);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // アクティブなテキストボックスのIME変換モードを設定する。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this);
        }
    }
}
