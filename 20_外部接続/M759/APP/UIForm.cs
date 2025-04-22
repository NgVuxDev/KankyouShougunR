using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DenshiKeiyakuHimodzukeHojo.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;

namespace DenshiKeiyakuHimodzukeHojo.App
{
    /// <summary>
    /// UIForm
    /// </summary>
    public partial class UIForm : SuperPopupForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, List<string>> InOutSysId = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.NONE)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            bool catchErr = true;
            this.logic.WindowInit(out catchErr);
            if (!catchErr)
            {
                return;
            }

            // イベントバンディング
            var allControl = (new ControlUtility()).GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            this.KENSAKU_HOUHOU.Focus();
        }

        /// <summary>
        /// 自動紐付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            if (this.logic.Click_ButtonF9())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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
        protected void c_GotFocus(object sender, EventArgs e)
        {
            var activ = this.ActiveControl as SuperPopupForm;

            if (activ == null && this.ActiveControl != null)
            {
                this.lb_hint.Text = (string)this.ActiveControl.Tag;
            }
        }

        /// <summary>
        /// KENSAKU_HOUHOU_Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KENSAKU_HOUHOU_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.KENSAKU_HOUHOU.Text))
            {
                this.KENSAKU_HOUHOU.IsInputErrorOccured = true;
                e.Cancel = true;
                this.logic.errmessage.MessageBoxShow("E001", "検索方法");

            }
        }
    }
}
