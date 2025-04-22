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
    public partial class ZipCodeKobetsuHoshuForm : SuperForm
    {
        /// <summary>
        /// メニュー権限保守画面ロジック
        /// </summary>
        private ZipCodeKobetsuHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ZipCodeKobetsuHoshuForm()
            : base(WINDOW_ID.S_ZIP_CODE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ZipCodeKobetsuHoshuLogic(this);

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
            base.RegistErrorFlag = false;
            this.logic.Regist(base.RegistErrorFlag);
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

        /// <summary>
        /// プレビュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Preview(object sender, EventArgs e)
        {
            this.logic.Preview();
        }

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

            Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT = this.POST7_OLD.Text;
            Properties.Settings.Default.KOBETSU_SIKUCHOUSON_OLD_TEXT = this.SIKUCHOUSON_OLD.Text;
            Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT = this.POST7_NEW.Text;
            Properties.Settings.Default.KOBETSU_SIKUCHOUSON_NEW_TEXT = this.SIKUCHOUSON_NEW.Text;
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
            ZipCodeHoshuForm form = new ZipCodeHoshuForm();
            if (form.IsDisposed)
            {
                return;
            }

            this.FormClose(null, e);

            MasterBaseForm masterBaseForm = new MasterBaseForm(form, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, true);
            masterBaseForm.Show();
        }

        /// <summary>
        /// 初期値セット処理
        /// </summary>
        /// <param name="oldPost"></param>
        /// <param name="oldAddr"></param>
        /// <param name="newPost"></param>
        /// <param name="newAddr"></param>
        public virtual void SetDefault(string oldPost, string oldAddr, string newPost, string newAddr)
        {
            this.POST7_OLD.Text = oldPost;
            this.SIKUCHOUSON_OLD.Text = oldAddr;
            this.POST7_NEW.Text = oldPost;
            this.SIKUCHOUSON_NEW.Text = oldAddr;

            Properties.Settings.Default.KOBETSU_POST7_OLD_TEXT = this.POST7_OLD.Text;
            Properties.Settings.Default.KOBETSU_SIKUCHOUSON_OLD_TEXT = this.SIKUCHOUSON_OLD.Text;
            Properties.Settings.Default.KOBETSU_POST7_NEW_TEXT = this.POST7_NEW.Text;
            Properties.Settings.Default.KOBETSU_SIKUCHOUSON_NEW_TEXT = this.SIKUCHOUSON_NEW.Text;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// ヘッダセルのチェックボックス処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.ColumnHeader && this.Ichiran.CurrentCell is CheckBoxCell)
            {
                //チェックボックス型セルの値を取得します
                this.logic.ChangeHeaderCheckBox();
                this.Ichiran.Refresh();
            }
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
