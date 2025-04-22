// $Id: DenManiJigyoushaHoshuForm.cs 53608 2015-06-26 00:09:57Z miya@e-mall.co.jp $
using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using DenManiJigyoushaHoshu.Logic;

namespace DenManiJigyoushaHoshu.APP
{
    /// <summary>
    /// 事業者入力画面
    /// </summary>
    [Implementation]
    public partial class DenManiJigyoushaHoshuForm : SuperForm
    {
        /// <summary>
        /// 事業者入力画面ロジック
        /// </summary>
        private DenManiJigyoushaHoshuLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ(【新規】モード起動時)
        /// </summary>
        public DenManiJigyoushaHoshuForm()
            : base(WINDOW_ID.M_DENSHI_JIGYOUSHA, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenManiJigyoushaHoshuLogic(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// コンストラクタ(【修正】【削除】【複写】【参照】モード起動時)
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="kanyushaId">加入者番号</param>
        public DenManiJigyoushaHoshuForm(WINDOW_TYPE windowType, string kanyushaId)
            : base(WINDOW_ID.M_DENSHI_JIGYOUSHA, windowType)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new DenManiJigyoushaHoshuLogic(this);

            this.logic.kanyushaId = kanyushaId;

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
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M309", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
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
            if (r_framework.Authority.Manager.CheckAuthority("M309", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                this.logic.kanyushaId = this.EDI_MEMBER_ID.Text;
                // 画面再描画
                //base.OnLoad(e);
                //this.logic.WindowInit(base.WindowType);
                // 画面初期化
                this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M309", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                this.logic.kanyushaId = this.EDI_MEMBER_ID.Text;
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
                if (catchErr)
                {
                    return;
                }

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
                    if (r_framework.Authority.Manager.CheckAuthority("M309", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                    {
                        // DB更新後、新規モードで表示
                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.logic.kanyushaId = string.Empty;
                        //base.OnLoad(e);
                        //this.logic.WindowInit(base.WindowType);
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
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = this.logic.CreateEntity(true);
                if (catchErr)
                {
                    return;
                }
                
                this.logic.LogicalDelete();
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
        /// 加入者ID変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void EDI_MEMBER_ID_Validated(object sender, EventArgs e)
        {
            this.logic.ChangeKanyuushaID();
        }

        /// <summary>
        /// 事業者区分変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void JigyousyaKbnChanged(object sender, EventArgs e)
        {
           if (this.HST_KBN.Checked  == false
                && this.UPN_KBN.Checked == false 
                    && this.SBN_KBN.Checked == false)
           {
               this.DUMY_JIGYOUSYA_KBN.Text = string.Empty;
           }
           else
           {
               this.DUMY_JIGYOUSYA_KBN.Text = "1";
           }
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void DenManiJigyoushaHoshuForm_UserRegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            this.logic.RegistCheck(sender, e);
        }

        /// <summary>
        /// 存在する電子事業者かどうか検索する
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            return this.logic.Search();
        }

        /// <summary>
        /// 住所参照ボタン押下後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JIGYOUSHA_POST_SEACRH_BUTTON_Click(object sender, EventArgs e)
        {
            this.logic.JigyoushaPostCheck();
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
