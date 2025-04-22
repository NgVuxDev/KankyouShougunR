// $Id: BookmarkHoshuForm.cs 18796 $
using System;
using BookmarkHoshu.Logic;
using GrapeCity.Win.MultiRow;
using MasterCommon.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using System.Windows.Forms;

namespace BookmarkHoshu.APP
{
    /// <summary>
    /// マイメニュー選択
    /// </summary>
    [Implementation]
    public partial class BookmarkHoshuForm : SuperForm
    {
        /// <summary>
        /// マイメニュー選択ロジック
        /// </summary>
        private BookmarkHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 処理中フラグ
        /// </summary>
        public static bool processingFlg = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BookmarkHoshuForm()
            : base(WINDOW_ID.M_BOOKMARK, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new BookmarkHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
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

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.logic.WindowInit())
            {
                return;
            }

            this.Search(null, e);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            }
        }

        #region - Function Event -

        /// <summary>
        /// F8 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            if (processingFlg)
            {
                return;
            }

            try
            {
                processingFlg = true;

                bool isCheck = (sender == null) ? false : true;

                if (this.logic.SearchCheck(isCheck))
                {
                    return;
                }

                int count = this.logic.Search();
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                    return;
                }
                else if (count == -1)
                {
                    return;
                }

                this.logic.SetIchiran();
            }
            finally
            {
                processingFlg = false;
            }
        }

        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                if (this.logic.RegistCheck())
                {
                    return;
                }

                if (this.logic.CreateEntity(false))
                {
                    return;
                }
                this.logic.Regist(base.RegistErrorFlag);
                if (base.RegistErrorFlag)
                {
                    return;
                }
                this.Search(sender, e);
            }
        }

        /// <summary>
        /// F11 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            this.logic.ClearCondition();
        }

        /// <summary>
        /// F12 Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;

            // 前回設定値保存
            if (this.logic.SetPrevData())
            {
                return;
            }

            this.Close();
            parentForm.Close();
        }

        #endregion

        #region - Control Event -

        /// <summary>
        /// 部署CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUSHO_CD_TextChanged(object sender, EventArgs e)
        {
            // 一覧の初期化
            if (this.logic.ClearIchiran())
            {
                return;
            }

            // 社員CDの初期化
            if (string.IsNullOrEmpty(this.BUSHO_CD.Text))
            {
                this.SHAIN_CD.Text = string.Empty;
            }

            // ファンクションボタンを非活性
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            this.BUSHO_NAME_RYAKU.Text = string.Empty;
            if (this.BUSHO_CD.Text.Length >= 3)
            {
                if (this.BUSHO_CD.Text.Equals("999"))
                {
                    return;
                }
                else
                {
                    //  部署名称の取得
                    var bushoName = string.Empty;
                    bool ret = true;
                    if (this.logic.GetBushoName(this.BUSHO_CD.Text, out bushoName,out ret))
                    {
                        this.BUSHO_NAME_RYAKU.Text = bushoName;
                    }
                }
            }
        }

        /// <summary>
        /// 部署CD検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BUSHO_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 社員CDの初期化
            if (!this.BUSHO_CD.Text.Equals(base.PreviousValue))
            {
                this.SHAIN_CD.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(this.BUSHO_CD.Text))
            {
                this.BUSHO_CD_HIDDEN.Text = string.Empty;
                return;
            }
            else
            {
                if (this.BUSHO_CD.Text.Equals("999"))
                {
                    e.Cancel = true;
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "部署");
                }
                else
                {
                    this.BUSHO_CD_HIDDEN.Text = this.BUSHO_CD.Text;
                }
            }

            //  部署名称の取得
            var bushoName = string.Empty;
            bool catchErr = false;
            if (this.logic.GetBushoName(this.BUSHO_CD.Text, out bushoName, out catchErr))
            {
                if (catchErr)
                {
                    return;
                }
                if (!this.BUSHO_CD.Text.Equals("999"))
                {
                    this.BUSHO_NAME_RYAKU.Text = bushoName;
                }
            }
            else
            {
                e.Cancel = true;
                if (catchErr)
                {
                    return;
                }
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E020", "部署");
            }
        }

        /// <summary>
        /// 社員CD変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_TextChanged(object sender, EventArgs e)
        {
            // 一覧の初期化
            this.logic.ClearIchiran();

            // ファンクションボタンを非活性
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.ParentForm, false);
            this.SHAIN_NAME_RYAKU.Text = string.Empty;
            if (this.SHAIN_CD.Text.Length >= 6)
            {
                //  社員名称の取得
                var strShainName = string.Empty;
                var strBushoCD = this.BUSHO_CD.Text;

                if (this.BUSHO_CD.Text.Equals("999"))
                {
                    strBushoCD = null;
                }
                bool catchErr = false;
                if (this.logic.GetShainName(this.SHAIN_CD.Text, ref strBushoCD, out strShainName, out catchErr))
                {
                    if (catchErr)
                    {
                        return;
                    }
                    this.BUSHO_CD_HIDDEN.Text = strBushoCD;
                    this.BUSHO_CD.Text = strBushoCD;
                    this.SHAIN_NAME_RYAKU.Text = strShainName;
                }
            }
        }

        /// <summary>
        /// 社員CD検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 空の場合、何もしない
            if (string.IsNullOrEmpty(this.SHAIN_CD.Text))
            {
                return;
            }

            //  社員名称の取得
            var strShainName = string.Empty;
            var strBushoCD = this.BUSHO_CD.Text;

            if (this.BUSHO_CD.Text.Equals("999"))
            {
                strBushoCD = null;
            }

            bool catchErr = false;
            if (this.logic.GetShainName(this.SHAIN_CD.Text, ref strBushoCD, out strShainName, out catchErr))
            {
                if (catchErr)
                {
                    return;
                }
                this.BUSHO_CD_HIDDEN.Text = strBushoCD;
                this.BUSHO_CD.Text = strBushoCD;
                this.SHAIN_NAME_RYAKU.Text = strShainName;
            }
            else
            {
                e.Cancel = true;
                if (catchErr)
                {
                    return;
                }
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E020", "社員");
            }
        }

        /// <summary>
        /// セル変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValueChanged(object sender, CellEventArgs e)
        {
            if (BookmarkHoshuForm.processingFlg)
            {
                return;
            }

            try
            {
                BookmarkHoshuForm.processingFlg = true;
                //this.logic.Ichiran_CellValueChanged(e);
            }
            finally
            {
                BookmarkHoshuForm.processingFlg = false;
            }
        }

        #endregion
    }
}
