using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// 数値専用編集コントロールクラス
    /// </summary>
    public class DgvCustomAlphaNumTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomAlphaNumTextBoxEditingControl()
        {
            //IMEを無効にする
            base.ImeMode = ImeMode.Disable;
        }

        /// <summary>
        /// ペースト時のMsg区分
        /// </summary>
        private const int WM_PASTE = 0x302;

        /// <summary>
        /// プロセスイベントクラス
        /// </summary>
        /// <param name="m">処理メッセージ</param>
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

            base.WndProc(ref m);
        }

        /// <summary>
        /// テキスト変更処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        /// <summary>
        /// 入力データのチェックを行う
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

            object obj = this.GetCurrentCell();
            if (obj != null)
            {
                var column = obj as r_framework.CustomControl.DgvCustomTextBoxCell;
                if (column != null)
                {
                    column.TextBoxChanged = !handled;
                }
            }

            e.Handled = handled;
        }

        /// <summary>
        /// 入力データのチェックを行う
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        public bool CheckUseInputData(char chr)
        {
            bool handled = true;

            object obj = this.GetCurrentCell();
            if (obj != null)
            {
                var column = obj as DgvCustomTextBoxCell;
                if (column != null)
                {
                    var cell = column.OwningColumn as DgvCustomAlphaNumTextBoxColumn;
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
            }

            return handled;
        }

        /// <summary>
        /// アクティブセル取得
        /// </summary>
        /// <returns></returns>
        protected DataGridViewCell GetCurrentCell()
        {
            if (this.EditingControlDataGridView == null)
            {
                return null;
            }

            return this.EditingControlDataGridView.CurrentCell;
        }

        /// <summary>
        /// フォーカス処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            object obj = this.GetCurrentCell();
            if (obj != null)
            {
                var column = obj as r_framework.CustomControl.DgvCustomTextBoxCell;
                if (column != null)
                {
                    column.TextBoxChanged = false;
                }
            }
        }
    }
}