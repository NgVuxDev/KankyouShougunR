using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;

namespace r_framework.CustomControl
{
    /// <summary>
    /// 英数専用編集コントロールクラス
    /// </summary>
    public class GcCustomAlphaNumTextBoxEditingControl : TextBoxEditingControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomAlphaNumTextBoxEditingControl()
        {
            //IMEを無効にする
            base.ImeMode = ImeMode.Disable;
        }

        private const Int32 WM_USER = 0x400;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// ペースト処理
        /// </summary>
        private const int WM_PASTE = 0x302;

        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                IDataObject iData = Clipboard.GetDataObject();
                //文字列がクリップボードにあるか
                if (iData != null && iData.GetDataPresent(DataFormats.Text))
                {
                    string clipStr = (string)iData.GetData(DataFormats.Text);
                    //クリップボードの文字列が数字のみか調べる
                    //Char 型の1次元配列に変換する
                    char[] clip = clipStr.ToCharArray();
                    bool check = false;
                    if (clip != null)
                    {
                        for (int i = 0; i < clip.Length; i++)
                        {
                            if (CheckUseInputData(clip[i]))
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check)
                        {
                            return;
                        }
                    }
                }
            }
            else if (m.Msg == (WM_USER + 1))
            {
                if (!this.IsDisposed && this.Focused)
                {
                    this.SelectAll();
                }
                return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            bool handled = true;

            if (Control.ModifierKeys == Keys.Control)
            {
                handled = false;
            }
            else
            {
                handled = CheckUseInputData(e.KeyChar);
            }

            var column = this.GcMultiRow as r_framework.CustomControl.GcCustomMultiRow;
            if (column != null)
            {
                GcCustomTextBoxCell cell = column.CurrentCell as r_framework.CustomControl.GcCustomTextBoxCell;
                if (cell != null)
                {
                    cell.TextBoxChanged = !handled;
                }
            }

            e.Handled = handled;
        }

        [Description("コントロールのIMEモードを取得")]
        public new ImeMode ImeMode
        {
            get { return base.ImeMode; }
            set { }
        }

        /// <summary>
        /// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        /// <para>とりあえず、CustomAlphaNumTextBoxだけ。</para>
        /// </summary>
        [Browsable(false)]
        public string[] PopupDataHeaderTitle { get; set; }

        /// <summary>
        /// 入力データのチェックを行う
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public bool CheckUseInputData(char chr)
        {
            bool handled = true;

            //cell プロパティ呼ぶ
            var column = this.GcMultiRow as r_framework.CustomControl.GcCustomMultiRow;
            if (column != null)
            {
                var cell = column.CurrentCell as r_framework.CustomControl.GcCustomAlphaNumTextBoxCell;
                if (cell != null)
                {
                    if (Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(chr))
                    {
                        return false;
                    }

                    if (('a' <= chr && chr <= 'z') ||
                       ('A' <= chr && chr <= 'Z'))
                    {
                        if (cell.AlphabetLimitFlag)
                        {
                            handled = false;
                        }
                    }
                    else if ('0' <= chr && chr <= '9')
                    {
                        if (cell.NumberLimitFlag)
                        {
                            handled = false;
                        }
                    }
                    else
                    {
                        string targetStr = cell.CharacterLimitList;
                        if (targetStr != null)
                        {
                            //Char 型の1次元配列に変換する
                            char[] symbol = targetStr.ToCharArray();
                            if (symbol != null)
                            {
                                for (int i = 0; i < symbol.Length; i++)
                                {
                                    if (symbol[i].Equals(chr))
                                    {
                                        handled = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return handled;
        }

        /// <summary>
        /// フォーカス処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            // 勝手にIMEモードが有効になってしまう現象の対策
            base.ImeMode = ImeMode.Disable;

            var column = this.GcMultiRow as r_framework.CustomControl.GcCustomMultiRow;
            if (column != null)
            {
                GcCustomTextBoxCell cell = column.CurrentCell as r_framework.CustomControl.GcCustomTextBoxCell;
                if (cell != null)
                {
                    cell.TextBoxChanged = false;
                }
            }

            // フォーカス取得で既存テキストを全選択状態にする
            PostMessage(this.Handle, WM_USER + 1, IntPtr.Zero, IntPtr.Zero);
        }

        protected override void OnImeModeChanged(EventArgs e)
        {
            base.OnImeModeChanged(e);

            // 勝手にIMEモードが有効になってしまう現象の対策
            if (base.ImeMode != ImeMode.Disable)
            {
                base.ImeMode = ImeMode.Disable;
            }
        }
    }
}