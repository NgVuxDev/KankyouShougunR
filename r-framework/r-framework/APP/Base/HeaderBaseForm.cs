using System.Windows.Forms;
using System.Drawing;
using System;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using r_framework.Logic;

namespace r_framework.APP.Base
{
    /// <summary>
    /// ヘッダーベースクラス
    /// </summary>
    public partial class HeaderBaseForm : Form
    {
        /// <summary>
        /// フォーカスアウトエラーフラグ
        /// </summary>
        public bool FocusOutErrorFlag = false;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// 画面に表示しているすべてのコントロールを格納するフィールド
        /// </summary>
        public Control[] allControl;

        /// <summary>
        /// フォーカスがあたっているコントロール
        /// </summary>
        public Control FoucusControl { get; set; }

        /// <summary>
        /// フォーカスがあたっているコントロール
        /// </summary>
        private int CurrentTabIndex { get; set; }

        /// <summary>
        /// フォーカスがヘッダあるがどうかのチェックフラグ
        /// </summary>
        public static bool HeaderFocusFlag { get; set; }

        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HeaderBaseForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            ////全コントロールを取得
            this.allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            // モードラベルが非表示の場合、タイトルラベルを左端に寄せる
            if (!this.windowTypeLabel.Visible)
            {
                this.lb_title.Location = new Point(0, this.lb_title.Location.Y);
            }
           HeaderFocusFlag = true;
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        void c_GotFocus(object sender, EventArgs e)
        {
            if (Parent == null)
            {
                return;
            }
            FoucusControl = ActiveControl;

            if (FoucusControl != null)
            {
                if (IsPanelorGroupBox(FoucusControl))
                {
                    return;
                }

                var parentForm = Parent;
                var hintLabel = (Label)controlUtil.FindControl(ControlUtility.GetTopControl(this), "lb_hint");

                hintLabel.Text = (string)FoucusControl.Tag;
                CurrentTabIndex = FoucusControl.TabIndex;
            }
        }

        /// <summary>
        /// パネルorグループボックスに属しているコントロールか判定
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsPanelorGroupBox(Control c)
        {
            if (c == null)
            {
                return false;
            }

            if (c != null
                && (typeof(Panel) == c.GetType()
                || typeof(GroupBox) == c.GetType()))
            {
                return true;
            }
            else
            {
                return IsPanelorGroupBox(c.Parent);
            }
        }

        /// <summary>
        /// タブインデックスが最初のものにフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderBaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            // タブインデックスが最初のものにフォーカス
            HeaderFocusFlag = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                string a = Convert.ToString(e.KeyData);
                if (e.KeyData.Equals(Keys.LButton | Keys.Back | Keys.Shift))
                {
                    this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                    e.Handled = true;
                }
                else
                {
                    // タブインデックスが最後のものにフォーカス
                    Control lastControl;
                    if (ControlUtility.TryHeaderFormGetLastTabIndexControl(controlUtil.GetAllControls(this), CurrentTabIndex, out lastControl))
                    {
                        if (this.Validate())
                        {
                            HeaderFocusFlag = true;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                        e.Handled = true;

                    }
                }
            }
        }


       
    }
}