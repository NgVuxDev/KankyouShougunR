// $Id: ZipCodeHoshuForm.cs 56987 2015-07-28 07:33:31Z wuq@oec-h.com $
using System;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using ZipCodeHoshu.Logic;
using System.Windows.Forms;

namespace ZipCodeHoshu.APP
{
    /// <summary>
    /// メニュー権限保守画面
    /// </summary>
    [Implementation]
    public partial class ZipCodeHoshuForm : SuperForm
    {
        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private ZipCodeHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ZipCodeHoshuForm()
            : base(WINDOW_ID.S_ZIP_CODE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ZipCodeHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            int count = this.logic.Search();
            if (count == 0)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("C001");
                return;
            }
            else if (count < 0)
            {
                return;
            }


            this.logic.SetIchiran();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            this.RegistErrorFlag = false;
            this.logic.Regist(base.RegistErrorFlag);
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void PhysicalDelete(object sender, EventArgs e)
        {
            this.RegistErrorFlag = false;
            this.logic.PhysicalDelete();
            if (base.RegistErrorFlag)
            {
                return;
            }
            this.Search(sender, e);
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.Cancel();
        }

        //プレビュ機能削除
        ///// <summary>
        ///// プレビュー
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void Preview(object sender, EventArgs e)
        //{
        //    this.logic.Preview();
        //}

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            this.logic.CancelCondition();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            Properties.Settings.Default.LIST_POST3_TEXT = this.POST3.Text;
            Properties.Settings.Default.LIST_POST7_TEXT = this.POST7.Text;
            Properties.Settings.Default.Save();

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// フォーム切替処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeForm(object sender, EventArgs e)
        {
            ZipCodeKobetsuHoshuForm form = new ZipCodeKobetsuHoshuForm();
            if (form.IsDisposed)
            {
                return;
            }

            this.FormClose(null, e);

            form.SetDefault(this.POST7_OLD.Text, this.SIKUCHOUSON_OLD.Text, this.POST7_NEW.Text, this.SIKUCHOUSON_NEW.Text);

            MasterBaseForm masterBaseForm = new MasterBaseForm(form, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, true);
            masterBaseForm.Show();
        }

        /// <summary>
        /// 郵便辞書読込処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void RegistFromCsvFile(object sender, EventArgs e)
        {
            bool ret = false;
            bool catchErr = false;
            ret = this.logic.RegistFromCsvFile(out catchErr);
            if (ret && !catchErr) this.Search(sender, e);
        }

        /// <summary>
        /// 郵便ホームページリンククリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.post.japanpost.jp/zipcode/download.html");
        }

        /// <summary>
        /// POST3入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void POST3_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.POST3.Text))
            {
                this.POST7.ReadOnly = false;
            }
            else
            {
                this.POST7.ReadOnly = true;
            }
        }

        /// <summary>
        /// POST7入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void POST7_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.POST7.Text))
            {
                this.POST3.ReadOnly = false;
            }
            else
            {
                this.POST3.ReadOnly = true;
            }
        }

        /// <summary>
        /// 郵便ホームページへのリンクラベルの表示を切り替えます
        /// </summary>
        /// <param name="value">Trueの場合、表示</param>
        internal void SetYubinSiteLinkVisible(bool value)
        {
            this.linkLabel1.Visible = value;
            this.label4.Visible = value;
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }
    }

}
