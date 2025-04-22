// $Id: DenManiJigyoujouHoshuForm.cs 53608 2015-06-26 00:09:57Z miya@e-mall.co.jp $
using System;
using DenManiJigyoujouHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace DenManiJigyoujouHoshu.APP
{
    /// <summary>
    /// 事業者入力画面
    /// </summary>
    [Implementation]
    public partial class DenManiJigyoujouHoshuForm : SuperForm
    {
        /// <summary>
        /// 事業者入力画面ロジック
        /// </summary>
        private DenManiJigyoujouHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public r_framework.Dto.SearchConditionsDto searchConditionsDto = new r_framework.Dto.SearchConditionsDto();

        private string beforeJigyoushaKbn = "";

        /// <summary>
        /// ポップアップ表示中に処理を実行したくない場合に使用
        /// </summary>
        private bool dispPopupFlag = false;

        private string preGyoushaCd = string.Empty;

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public DenManiJigyoujouHoshuForm()
            : base(WINDOW_ID.M_DENSHI_JIGYOUJOU, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenManiJigyoujouHoshuLogic(this);

            this.logic.kanyushaId = string.Empty;
            this.logic.jigyoujouCd = string.Empty;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ(【修正】【削除】【複写】【参照】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="kanyushaId">加入者番号</param>
        /// <param name="jigyoujouCd">事業場CD</param>
        public DenManiJigyoujouHoshuForm(WINDOW_TYPE windowType, string kanyushaId, string jigyoujouCd)
            : base(WINDOW_ID.M_DENSHI_JIGYOUJOU, windowType)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenManiJigyoujouHoshuLogic(this);

            this.logic.kanyushaId = kanyushaId;
            this.logic.jigyoujouCd = jigyoujouCd;

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
            this.logic.WindowInit(base.WindowType);
        }

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>

        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M312", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 画面再描画
            //base.OnLoad(e);
            //this.logic.WindowInit(base.WindowType);

            // 画面初期化
            this.logic.kanyushaId = string.Empty;
            this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
        }

        /// <summary>
        /// 【修正】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UpdateMode(object sender, EventArgs e)
        {
            // 権限チェック
            // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
            if (r_framework.Authority.Manager.CheckAuthority("M312", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                // 画面再描画
                //base.OnLoad(e);
                //this.logic.WindowInit(base.WindowType);
                // 画面初期化
                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M312", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                // 画面再描画
                //base.OnLoad(e);
                //this.logic.WindowInit(base.WindowType);
                // 画面初期化
                this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        /// <summary>
        /// 一覧画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            this.logic.ShowIchiran();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                if (!this.logic.TodoufukenSikuchousonCheck())
                {
                    return;;
                }

                bool catchErr = this.logic.CreateEntity(false);

                switch (base.WindowType)
                {
                    // 新規追加
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.logic.Regist(base.RegistErrorFlag);
                        break;

                    // 更新
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.logic.Update(base.RegistErrorFlag);
                        break;

                    // 論理削除
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.logic.LogicalDelete();
                        break;

                    default:
                        break;
                }

                if (this.logic.isRegist)
                {
                    // 権限チェック
                    if (r_framework.Authority.Manager.CheckAuthority("M312", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // DB更新後、新規モードで表示
                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        //base.OnLoad(e);
                        this.logic.kanyushaId = string.Empty;
                        this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                    }
                    else
                    {
                        // 新規権限がない場合は画面Close
                        this.FormClose(sender, e);
                    }
                }
            }
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
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            //var parentForm = (BusinessBaseForm)this.Parent;

            //this.Close();
            //parentForm.Close();
            base.CloseTopForm();
        }

        /// <summary>
        /// 加入者番号入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EDI_MEMBER_ID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.SearchDenshiJigyoushaData())
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 加入者番号ポップアップ後処理
        /// </summary>
        public void EDI_MEMBER_ID_AfterPopup()
        {
            this.dispPopupFlag = true;
            this.dispPopupFlag = false;
        }

        /// <summary>
        /// 事業場CD入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JIGYOUJOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.ChangeJigyoujouCD();
        }

        /// <summary>
        /// 業者CD入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_NAME.Text = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME.Text = string.Empty;
                this.preGyoushaCd = string.Empty;
            }
            else
            {
                if (!string.Equals(this.GYOUSHA_CD.Text, this.preGyoushaCd))
                {
                    this.GENBA_CD.Text = string.Empty;
                    this.GENBA_NAME.Text = string.Empty;
                    this.preGyoushaCd = this.GYOUSHA_CD.Text;
                }
            }
        }

        /// <summary>
        /// 加入者番号ポップアップ後処理
        /// </summary>
        public void GYOUSHA_CD_AfterPopup()
        {
            bool catchErr = false;
            this.GYOUSHA_NAME.Text = this.logic.getGyoushaName(this.GYOUSHA_CD.Text, out catchErr);
            if(catchErr)
            {
            	return;
            }
            if (!string.Equals(this.GYOUSHA_CD.Text, this.preGyoushaCd))
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME.Text = string.Empty;
                this.preGyoushaCd = this.GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 現場CD入力後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.GenbaValidated();
        }

        /// <summary>
        /// 加入者番号ポップアップ後処理
        /// </summary>
        public void GENBA_CD_AfterPopup()
        {
            bool catchErr = false;
            this.GYOUSHA_NAME.Text = this.logic.getGyoushaName(this.GYOUSHA_CD.Text, out catchErr);
            if (catchErr)
            {
                return;
            }
            this.GENBA_NAME.Text = this.logic.getGenbaName(this.GYOUSHA_CD.Text, this.GENBA_CD.Text, out catchErr);
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DenManiJigyoujouHoshuForm_UserRegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            this.logic.RegistCheck(sender, e);
        }

        private void JIGYOUSHA_KBN_TextChanged(object sender, EventArgs e)
        {
            if (this.dispPopupFlag)
            {
                // ポップアップの処理で事業者区分を変更するため、無駄にイベントが走らないように制御
                return;
            }

            if (this.beforeJigyoushaKbn != this.JIGYOUSHA_KBN.Text)
            {
                if (!string.IsNullOrWhiteSpace(this.JIGYOUSHA_KBN.Text))
                {
                    this.EDI_MEMBER_ID.Text = "";
                    this.JIGYOUSHA_NAME.Text = "";
                }
                this.beforeJigyoushaKbn = this.JIGYOUSHA_KBN.Text;
            }

            bool catchErr = false;
            this.EDI_MEMBER_ID.PopupDataSource = this.logic.GetPopupDispDataForEDI_MEMBER_ID(out catchErr);
        }


        /// <summary>
        /// 業者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.preGyoushaCd = this.GYOUSHA_CD.Text;
        }

        public void GYOUSHA_CD_BeforePopup()
        {
            this.preGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 存在する電子事業場かどうか検索する
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            this.logic.SetSearchString();
            return this.logic.Search();
        }

        /// <summary>
        /// 住所参照ボタン押下後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JIGYOUJOU_POST_SEACRH_BUTTON_Click(object sender, EventArgs e)
        {
            this.logic.JigyoujouPostCheck();
        }

        /// <summary>
        /// 虫眼鏡ボタン押下後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SIKUCHOUSON_SEARCH_BUTTON_Click(object sender, EventArgs e)
        {
            this.logic.TodoufukenSikuchousonLikeCheck();
        }
    }
}
