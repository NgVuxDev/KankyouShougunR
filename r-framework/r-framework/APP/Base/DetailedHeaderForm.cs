using System;
using System.Drawing;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;

namespace r_framework.APP.Base
{
    /// <summary>
    /// 単票画面のヘッダークラス
    /// </summary>
    public partial class DetailedHeaderForm : Form
    {
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
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 646;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DetailedHeaderForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// FormのLoad処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailedHeaderForm_Load(object sender, System.EventArgs e)
        {
            ////全コントロールを取得
            this.allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }
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
        /// ラベルタイトルテキストチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }
    }
}