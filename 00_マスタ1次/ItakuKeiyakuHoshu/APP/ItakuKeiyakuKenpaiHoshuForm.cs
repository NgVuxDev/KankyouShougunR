// $Id: ItakuKeiyakuKenpaiHoshuForm.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using ItakuKeiyakuHoshu.Const;
using ItakuKeiyakuHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Configuration;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Function.ShougunCSCommon.Dto;

namespace ItakuKeiyakuHoshu.APP
{
    /// <summary>
    /// 委託契約登録画面
    /// </summary>
    [Implementation]
    public partial class ItakuKeiyakuKenpaiHoshuForm : SuperForm
    {
        /// <summary>
        /// システムID
        /// </summary>
        protected String systemId;

        /// <summary>
        /// errchk
        /// </summary>
        protected bool errchk;

        /// <summary>
        /// 委託契約登録画面ロジック
        /// </summary>
        protected ItakuKeiyakuKenpaiHoshuLogic logic;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        /// <summary>
        /// 前回値コード
        /// </summary>
        internal string PreviousCd;

        /// <summary>
        /// 前回値名前
        /// </summary>
        internal string PreviousName;

        /// <summary>
        /// 前回値社内経路コード
        /// </summary>
        internal string shanaiKeiroPreviousCd;

        private string sbnKyokaGyoushaBef { get; set; }
        private string sbnKyokaGyoushaAft { get; set; }
        private string unpanGyoushaBef { get; set; }
        private string unpanGyoushaAft { get; set; }

        //画面上の登録方法
        internal short dispTourokuHouhou;

		/// <summary>
        /// Request inxs subapp transaction id 
        /// </summary>
        internal string transactionId;

        /// <summary>
        /// コンストラクタ(新規用)
        /// </summary>
        public ItakuKeiyakuKenpaiHoshuForm(String keiyakuShurui,short tourokuHouhou)
            : base(WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI, WINDOW_TYPE.NEW_WINDOW_FLAG)
        {
            // SelectForm側で設定しているので不要
            //CommonShogunData.Create(SystemProperty.Shain.CD);
            InitializeComponent();

            //委託契約登録画面固有の処理
            this.systemId = string.Empty;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ItakuKeiyakuKenpaiHoshuLogic(this);
            this.logic.SystemId = string.Empty;
            this.logic.KeiyakuShurui = keiyakuShurui;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //登録方法ー基本マスタ。タブと一部コントロールの非表示
            if (tourokuHouhou.Equals(Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI))
            {
                this.dispTourokuHouhou = Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI;
            }
            else if (tourokuHouhou.Equals(Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON))
            {
                this.dispTourokuHouhou = Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON;

                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage2);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage3);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage1);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage4);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage10);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage5);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage11);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage8);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage9);

                this.label8.Visible = false;
                this.KEIYAKUSHO_SEND_DATE.Visible = false;
                this.label13.Visible = false;
                this.KEIYAKUSHO_RETURN_DATE.Visible = false;
                this.label11.Visible = false;
                this.KEIYAKUSHO_END_DATE.Visible = false;

                this.label27.Visible = false;
                this.panel1.Visible = false;
                this.JIZEN_KYOUGI.Visible = false;
                this.JIZEN_KYOUGI_1.Visible = false;
                this.JIZEN_KYOUGI_2.Visible = false;

            }

            // オプション未使用なら電子契約タブも削除
            if (!AppConfig.AppOptions.IsDenshiKeiyaku())
            {
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage12);
            }
        }

        /// <summary>
        /// コンストラクタ(修正削除複写参照用)
        /// </summary>
        public ItakuKeiyakuKenpaiHoshuForm(WINDOW_TYPE windowType, String systemId,short tourokuHouhou)
            : base(WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI, windowType)
        {
            CommonShogunData.Create(SystemProperty.Shain.CD);
            InitializeComponent();

            //委託契約登録画面固有の処理
            this.systemId = systemId;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new ItakuKeiyakuKenpaiHoshuLogic(this);
            this.logic.SystemId = this.systemId;
            this.logic.KeiyakuShurui = string.Empty;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //登録方法ー基本マスタ。タブと一部コントロールの非表示
            if (tourokuHouhou.Equals(Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI))
            {
                dispTourokuHouhou = Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI;
                
            }
            else if (tourokuHouhou.Equals(Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON))
            {
                dispTourokuHouhou = Const.ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_KIHON;

                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage2);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage3);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage1);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage4);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage10);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage5);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage11);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage8);
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage9);

                this.label20.Visible = false;
                this.ITAKU_KEIYAKU_STATUS_NAME.Visible = false;

                this.label8.Visible = false;
                this.KEIYAKUSHO_SEND_DATE.Visible = false;
                this.label13.Visible = false;
                this.KEIYAKUSHO_RETURN_DATE.Visible = false;
                this.label11.Visible = false;
                this.KEIYAKUSHO_END_DATE.Visible = false;

                this.label27.Visible = false;
                this.panel1.Visible = false;
                this.JIZEN_KYOUGI.Visible = false;
                this.JIZEN_KYOUGI_1.Visible = false;
                this.JIZEN_KYOUGI_2.Visible = false;
            }

            // オプション未使用なら電子契約タブも削除
            if (!AppConfig.AppOptions.IsDenshiKeiyaku())
            {
                this.tabItakuKeiyakuData.TabPages.Remove(this.tabPage12);
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			//Init inxs subapp transaction id
            this.transactionId = Guid.NewGuid().ToString();

            bool catchErr = this.HeaderTitleInit();
            if (catchErr)
            {
                return;
            }
            catchErr = this.logic.WindowInit(base.WindowType);
            if (catchErr)
            {
                return;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.listKihonHstGenba != null)
            {
                this.listKihonHstGenba.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listHinmei != null)
            {
                this.listHinmei.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listHoukokushoBunrui != null)
            {
                this.listHoukokushoBunrui.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listOboe != null)
            {
                this.listOboe.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listBetsu2 != null)
            {
                this.listBetsu2.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listBetsu3 != null)
            {
                this.listBetsu3.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listBetsu4 != null)
            {
                this.listBetsu4.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.listUpnKyokasho != null)
            {
                this.listUpnKyokasho.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (this.keiroIchiran != null)
            {
                //this.keiroIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                this.keiroIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            }
            if (this.keiroIchiran2 != null)
            {
                //this.keiroIchiran2.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                this.keiroIchiran2.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            }
            if (this.label38 != null)
            {
                this.label38.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            if (this.btnUpRow != null)
            {
                this.btnUpRow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            if (this.btnDownRow != null)
            {
                this.btnDownRow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            // Loadイベント終了からShownイベントまでの間
            // Validatingイベントでエラーが発生しないように制御
            this.logic.errorCancelFlg = false;
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

            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }
            base.OnShown(e);
        }

        /// <summary>
        /// 画面表示処理
        /// </summary>
        /// <param name="e"></param>
        internal void FormShown(object sender, EventArgs e)
        {
            // 修正・削除・複写時の表示時にMultiRowのValidationイベントが2重に実施されるため
            // 通常のEventInitではバインドせず、画面表示時にバインドするようにした
            //this.logic.RemoveDynamicEvent();
            //this.logic.SetDynamicEvent();

            this.logic.errorCancelFlg = true;
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.HAISHUTSU_JIGYOUSHA_CD.Focus();
            }
            else if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.ITAKU_KEIYAKU_NO.Focus();
            }
        }

        /// <summary>
        /// ヘッダータイトルの設定
        /// </summary>
        public bool HeaderTitleInit()
        {
            try
            {
                base.HeaderFormInit();
                ((DetailedHeaderForm)((BusinessBaseForm)this.ParentForm).headerForm).lb_title.Text =
                    WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_ITAKU_KEIYAKU_KENPAI);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderTitleInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 【新規】モード切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CreateMode(object sender, EventArgs e)
        {
            // 権限チェック
            if (!r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }

            // 処理モード変更
            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

            // 画面タイトル変更
            bool catchErr = this.HeaderTitleInit();
            if (catchErr)
            {
                return;
            }

            // 画面初期化
            this.logic.SystemId = string.Empty;
            catchErr = this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
            if (catchErr)
            {
                return;
            }
            this.PreviousValue = string.Empty;
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
            if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                // 画面タイトル変更
                bool catchErr = this.HeaderTitleInit();
                if (catchErr)
                {
                    return;
                }
                // 画面初期化
                catchErr = this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                if (catchErr)
                {
                    return;
                }
                this.PreviousValue = string.Empty;
            }
            else if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
            {
                // 処理モード変更
                base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                // 画面タイトル変更
                bool catchErr = this.HeaderTitleInit();
                if (catchErr)
                {
                    return;
                }
                // 画面初期化
                catchErr = this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                if (catchErr)
                {
                    return;
                }
                this.PreviousValue = string.Empty;
            }
            else
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E158", "修正");
            }
        }

        /// <summary>
        /// 取消ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            bool catchErr = this.logic.Cancel((BusinessBaseForm)this.Parent);
            if (catchErr)
            {
                return;
            }

            // 画面初期化
            this.PreviousValue = string.Empty;
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
                if (this.logic.HstCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
                if (this.logic.DateCheck())
                {
                    return;
                }
                /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end
                if (!this.ItakuKeiyakuHinmeiCheck())
                {
                    return;
                }

                if (!this.ItakuKeiyakuHoukokushoBunruiCheck())
                {
                    return;
                }

                if (!this.ItakuKeiyakuTsumikaeCheck())
                {
                    return;
                }

                if (!this.ItakuKeiyakuBetsu3Check())
                {
                    return;
                }

                // VUNGUYEN 20150525 #1294 START
                if (this.logic.CheckRegistData())
                {
                    return;
                }
                // VUNGUYEN 20150525 #1294 END

                // 電子契約オプション = ONの場合、電子契約タブのチェックを行う。
                if (AppConfig.AppOptions.IsDenshiKeiyaku())
                {
                    // 電子契約データのチェックを行う。
                    if (this.logic.CheckDenshiKeiyakuData())
                    {
                        return;
                    }
                }

                bool catchErr = false;
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                if (base.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                {
                    // ファイルアップロードボタン押下の場合、印刷処理は行わない。
                    // if (!sender.ToString().Contains("ﾌｧｲﾙｱｯﾌﾟﾛｰﾄﾞ"))
                    if (!sender.ToString().Contains("ﾌｧｲﾙｱｯﾌﾟﾛｰﾄﾞ") && (this.dispTourokuHouhou.Equals(ItakuKeiyakuHoshuConstans.ITAKU_KEIYAKU_TOUROKU_HOUHOU_SYOUSAI)))
                    {
                        //157928 S
                        //if (msgLogic.MessageBoxShowConfirm("印刷しますか？", MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        //{
                        //    // 印刷出力
                        //    catchErr = this.logic.Print();
                        //    if (catchErr)
                        //    {
                        //        return;
                        //    }
                        //}
                        //157928 E
                    }

                    if (base.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                    {
                        if (msgLogic.MessageBoxShowConfirm("更新しますか？") == System.Windows.Forms.DialogResult.No)
                        {
                            // 処理を行わない
                            return;
                        }
                    }
                    else
                    {
                        if (msgLogic.MessageBoxShowConfirm("登録しますか？") == System.Windows.Forms.DialogResult.No)
                        {
                            // 処理を行わない
                            return;
                        }
                    }
                }
                else
                {
                    if (!this.logic.ExistFileLinkItakuKeiyakuKihon())
                    {
                        if (msgLogic.MessageBoxShow("C026") == DialogResult.No)
                        {
                            // 処理を行わない
                            return;
                        }
                    }
                    else
                    {
                        if (this.errmessage.MessageBoxShowConfirm("ファイルアップロードを行った委託契約書の削除を行いますか？", MessageBoxDefaultButton.Button1)
                            == System.Windows.Forms.DialogResult.No)
                        {
                            // 処理を行わない
                            return;
                        }
                        // ファイルアップロード用DB接続を確立
                        if (!this.logic.uploadLogic.CanConnectDB())
                        {
                            this.errmessage.MessageBoxShowError("ファイルアップロード用DBに接続できませんでした。\n接続情報を確認してください。");

                            // 処理を行わない
                            return;
                        }
                    }
                }

                // 新規の場合のみ、システムIDを採番する
                // 登録データ作成前に行う必要あり
                if (base.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                {
                    // 新規採番
                    catchErr = this.logic.Saiban();
                    if (catchErr)
                    {
                        return;
                    }
                }

                // 登録用データの作成
                catchErr = this.logic.CreateEntity(false);
                if (catchErr)
                {
                    return;
                }

                switch (base.WindowType)
                {
                    // 新規追加
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 重複チェック
                        bool result = this.DupliUpdateViewCheck(e, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }
                        if (result)
                        {
                            // 重複していなければ登録を行う
                            this.logic.Regist(base.RegistErrorFlag);
                            if (base.RegistErrorFlag)
                            {
                                return;
                            }
                        }

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))//157928
                        {
                            // DB登録後、新規モードで再表示
                            base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;//157928
                            catchErr = this.HeaderTitleInit();
                            if (catchErr)
                            {
                                return;
                            }
                            this.logic.SystemId = this.logic.dto.ItakuKeiyakuKihon.SYSTEM_ID;//157928
                            this.logic.isRegist = false;
                            catchErr = this.logic.WindowInitReference((BusinessBaseForm)this.Parent);//157928
                            if (catchErr)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // 新規権限がない場合は画面Close
                            this.FormClose(sender, e);
                        }
                        break;

                    // 更新
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        this.logic.Update(base.RegistErrorFlag);
                        if (base.RegistErrorFlag)
                        {
                            return;
                        }

                        // DB更新後、処理モード解除処理を実施
                        //this.logic.Cancel((BusinessBaseForm)this.Parent);

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))//157928
                        {
                            // DB登録後、新規モードで再表示
                            base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;//157928
                            catchErr = this.HeaderTitleInit();
                            if (catchErr)
                            {
                                return;
                            }
                            this.logic.SystemId = this.logic.dto.ItakuKeiyakuKihon.SYSTEM_ID;//157928
                            this.logic.isRegist = false;
                            catchErr = this.logic.WindowInitReference((BusinessBaseForm)this.Parent);//157928
                            if (catchErr)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // 新規権限がない場合は画面Close
                            this.FormClose(sender, e);
                        }

                        // 画面初期化
                        this.PreviousValue = string.Empty;

                        break;

                    // 論理削除
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.logic.LogicalDelete();
                        if (base.RegistErrorFlag)
                        {
                            return;
                        }

                        // DB更新後、委託契約一覧画面に遷移する
                        //this.logic.ShowIchiran();

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // DB登録後、新規モードで再表示
                            base.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            catchErr = this.HeaderTitleInit();
                            if (catchErr)
                            {
                                return;
                            }
                            this.logic.SystemId = string.Empty;
                            this.logic.isRegist = false;
                            catchErr = this.logic.WindowInitNewMode((BusinessBaseForm)this.Parent);
                            if (catchErr)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // 新規権限がない場合は画面Close
                            this.FormClose(sender, e);
                        }

                        // 画面初期化
                        this.PreviousValue = string.Empty;

                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 入金先CD重複チェック and 修正モード起動要否チェック
        /// </summary>
        /// <param name="e">イベント</param>
        internal bool DupliUpdateViewCheck(EventArgs e, out bool catchErr)
        {
            try
            {
                bool result = false;
                catchErr = false;

                // システムIDの入力値をゼロパディング
                string zeroPadCd = this.logic.ZeroPadding(this.SYSTEM_ID.Text);

                // 重複チェック
                ItakuKeiyakuHoshuConstans.SystemIdLeaveResult isUpdate = this.logic.DupliCheckSystemId(zeroPadCd);

                if (isUpdate == ItakuKeiyakuHoshuConstans.SystemIdLeaveResult.FALSE_ON)
                {
                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正モードで表示する
                        this.logic.SystemId = zeroPadCd;
                        base.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        // 画面タイトル変更
                        catchErr = this.HeaderTitleInit();
                        if (catchErr)
                        {
                            return true;
                        }
                        this.HAISHUTSU_JIGYOUSHA_CD.Focus();
                        // 修正モードで画面初期化
                        this.logic.WindowInitUpdate((BusinessBaseForm)this.Parent);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority("M001", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        // 参照モードで表示する
                        this.logic.SystemId = zeroPadCd;
                        base.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        // 画面タイトル変更
                        catchErr = this.HeaderTitleInit();
                        if (catchErr)
                        {
                            return true;
                        }
                        this.HAISHUTSU_JIGYOUSHA_CD.Focus();
                        // 参照モードで画面初期化
                        this.logic.WindowInitReference((BusinessBaseForm)this.Parent);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }

                    result = true;
                }
                else if (isUpdate != ItakuKeiyakuHoshuConstans.SystemIdLeaveResult.TURE_NONE)
                {
                    // 入力した入金先CDが重複した かつ 修正モード未起動の場合
                    this.SYSTEM_ID.Text = string.Empty;
                    this.SYSTEM_ID.Focus();
                }
                else
                {
                    // 重複しなければINSERT処理を行うフラグON
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("DupliUpdateViewCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
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
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// フォーカスを次へ移動させる
        /// </summary>
        public virtual void MoveFocus()
        {
            this.ProcessTabKey(true);
        }

        /// <summary>
        /// 画面タイプ変更処理
        /// </summary>
        /// <param name="type"></param>
        public virtual void SetWindowType(WINDOW_TYPE type)
        {
            base.WindowType = type;
            bool catchErr = this.HeaderTitleInit();
            if (catchErr)
            {
                return;
            }

            // 画面初期化
            this.PreviousValue = string.Empty;
        }

        /// <summary>
        /// 排出事業者ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterHaishutsuJigyousyaCD()
        {
            if (!this.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(string.Empty))
            {
                // コードから略称、住所をセットする
                bool catchErr = this.logic.SetJigyoushaData(this.HAISHUTSU_JIGYOUSHA_CD.Text, this.GYOUSHA_NAME_RYAKU, this.GYOUSHA_ADDRESS1, this.GYOUSHA_ADDRESS2, this.GYOUSHA_TODOUFUKEN_NAME);
                if (catchErr)
                {
                    return;
                }
                // 排出事業者CD Validated時処理
                this.logic.CheckHaishutsuJigyoushaCD();
            }
        }

        /// <summary>
        /// 排出事業者ポップアップ選択前処理
        /// </summary>
        public void PopupBeforeHaishutsuJigyousyaCD()
        {
            this.PreviousValue = this.HAISHUTSU_JIGYOUSHA_CD.Text;
        }

        /// <summary>
        /// 排出事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterHaishutsuJigyoujouCD()
        {
            if (!this.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(string.Empty))
            {
                // 前回値に排出事業場CDを保存する。
                this.PreviousCd = this.HAISHUTSU_JIGYOUJOU_CD.Text;

                // コードから略称、住所をセットする
                bool catchErr = this.logic.SetJigyoushaData(this.HAISHUTSU_JIGYOUSHA_CD.Text, this.GYOUSHA_NAME_RYAKU, this.GYOUSHA_ADDRESS1, this.GYOUSHA_ADDRESS2, this.GYOUSHA_TODOUFUKEN_NAME);
                if (catchErr)
                {
                    return;
                }

                if (!this.HAISHUTSU_JIGYOUJOU_CD.Text.Equals(string.Empty))
                {
                    // 排出事業場の設定処理・関連処理を行う
                    this.logic.CheckHaishutsuJigyoujouCD(new System.ComponentModel.CancelEventArgs());
                }
            }
        }

        /// <summary>
        /// 基本情報一覧内の排出事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterHaishutsuJigyoujouCDHstGenba()
        {
            // カレントセルを取得する
            Cell cell = this.listKihonHstGenba.CurrentCell;

            if (!this.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(string.Empty))
            {
                this.PreviousValue = string.Empty;

                // コードから略称、住所をセットする
                bool catchErr = this.logic.SetJigyoushaData(this.HAISHUTSU_JIGYOUSHA_CD.Text, this.GYOUSHA_NAME_RYAKU, this.GYOUSHA_ADDRESS1, this.GYOUSHA_ADDRESS2, this.GYOUSHA_TODOUFUKEN_NAME);
                if (catchErr)
                {
                    return;
                }

                if (!this.listKihonHstGenba[cell.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString().Equals(string.Empty))
                {
                    // コードから略称、住所をセットする
                    this.logic.SetJigyoujouData(this.HAISHUTSU_JIGYOUSHA_CD.Text, this.listKihonHstGenba[cell.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString(), this.listKihonHstGenba[cell.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_NAME"], this.listKihonHstGenba[cell.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1"], this.listKihonHstGenba[cell.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2"], this.listKihonHstGenba[cell.RowIndex, "HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME"], out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    this.PreviousValue = this.listKihonHstGenba[cell.RowIndex, "GENBA_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 別表1(予定)一覧内の排出事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterHaishutsuJigyoujouCDBetsu1Yotei()
        {
            // カレントセルを取得する
            Cell cell = this.listKihonHstGenba.CurrentCell;

            if (!this.HAISHUTSU_JIGYOUSHA_CD.Text.Equals(string.Empty) && !this.listHoukokushoBunrui[cell.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString().Equals(string.Empty))
            {
                // コードから略称、住所をセットする
                bool catchErr = false;
                this.logic.SetJigyoujouData(this.HAISHUTSU_JIGYOUSHA_CD.Text, this.listHoukokushoBunrui[cell.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_CD"].Value.ToString(), this.listHoukokushoBunrui[cell.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_NAME"], this.listHoukokushoBunrui[cell.RowIndex, "YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS1"], "", "", out catchErr);
            }
        }

        /// <summary>
        /// 別表3処分一覧内の処分事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterShobunJigyoujouCDBetsu3()
        {
            // カレントセルを取得する
            Cell cell = this.listBetsu3.CurrentCell;
            bool catchErr = false;
            if (!this.logic.CheckBetsu3Duplicate(this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value, this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value, listBetsu3[cell.RowIndex, "SHOBUN_HOUHOU_CD"].Value, cell.RowIndex, out catchErr))
            {
                this.logic.isError = true;

                if (this.listBetsu3.EditingControl != null)
                {
                    var textBox = this.listBetsu3.EditingControl as TextBox;
                    if (textBox != null)
                    {
                        textBox.SelectAll();
                    }
                }
                return;
            }

            if (!this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty))
            {
                // コードから略称、住所をセットする
                catchErr = this.logic.SetJigyoushaData(this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString(), this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_NAME"], this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_ADDRESS1"], this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_ADDRESS2"], this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_TODOUFUKEN_NAME"]);
                if (catchErr)
                {
                    return;
                }

                if (!this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(string.Empty) && !this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(this.PreviousValue))
                {
                    // コードから略称、住所をセットする
                    this.logic.SetJigyoujouData(this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value.ToString(), this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value.ToString(), this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_NAME"], this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS1"], this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_ADDRESS2"], this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"], out catchErr);
                }
            }
        }

        public virtual void PopupAfterShobunHouhou()
        {
            Cell cell = this.listBetsu3.CurrentCell;
            bool catchErr = false;
            if (!this.logic.CheckBetsu3Duplicate(this.listBetsu3[cell.RowIndex, "SHOBUN_GYOUSHA_CD"].Value, this.listBetsu3[cell.RowIndex, "SHOBUN_JIGYOUJOU_CD"].Value, listBetsu3[cell.RowIndex, "SHOBUN_HOUHOU_CD"].Value, cell.RowIndex, out catchErr))
            {
                this.logic.isError = true;

                if (this.listBetsu3.EditingControl != null)
                {
                    var textBox = this.listBetsu3.EditingControl as TextBox;
                    if (textBox != null)
                    {
                        textBox.SelectAll();
                    }
                }
                this.listBetsu3[cell.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = string.Empty;
                return;
            }
        }

        /// <summary>
        /// 別表4最終一覧内の処分事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterShobunJigyoujouCDBetsu4()
        {
            // カレントセルを取得する
            Cell cell = this.listBetsu4.CurrentCell;
            bool catchErr = false;
            if (!this.logic.CheckBetsu4Duplicate(this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value, this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value, this.listBetsu4[cell.RowIndex, "SHOBUN_HOUHOU_CD"].Value, cell.RowIndex, out catchErr))
            {
                this.logic.isError = true;

                if (this.listBetsu4.EditingControl != null)
                {
                    var textBox = this.listBetsu4.EditingControl as TextBox;
                    if (textBox != null)
                    {
                        textBox.SelectAll();
                    }
                }
                return;
            }

            if (!this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty))
            {
                // コードから略称、住所をセットする
                catchErr = this.logic.SetJigyoushaData(this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString(), this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_NAME"], this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS1"], this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_ADDRESS2"], this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_TODOUFUKEN_NAME"]);
                if (catchErr)
                {
                    return;
                }

                if (!this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(string.Empty) && !this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(this.PreviousValue))
                {
                    // コードから略称、住所をセットする
                    this.logic.SetJigyoujouData(this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value.ToString(), this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value.ToString(), this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_NAME"], this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS1"], this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_ADDRESS2"], this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GENBA_TODOUFUKEN_NAME"], out catchErr);
                }
            }
        }

        public virtual void PopupAfterLastShobunHouhou()
        {
            Cell cell = this.listBetsu4.CurrentCell;
            bool catchErr = false;
            if (!this.logic.CheckBetsu4Duplicate(this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_GYOUSHA_CD"].Value, this.listBetsu4[cell.RowIndex, "LAST_SHOBUN_JIGYOUJOU_CD"].Value, this.listBetsu4[cell.RowIndex, "SHOBUN_HOUHOU_CD"].Value, cell.RowIndex, out catchErr))
            {
                this.logic.isError = true;

                if (this.listBetsu4.EditingControl != null)
                {
                    var textBox = this.listBetsu4.EditingControl as TextBox;
                    if (textBox != null)
                    {
                        textBox.SelectAll();
                    }
                }
                this.listBetsu4[cell.RowIndex, "SHOBUN_HOUHOU_NAME_RYAKU"].Value = string.Empty;
                return;
            }
        }

        /// <summary>
        /// 処分許可証　現場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterSbnKyokashoGenbaCD()
        {
            bool catchErr = false;
            // コードから略称、住所をセットする
            this.logic.SetJigyoujouData(this.SBNKYOKA_GYOUSHA_CD.Text, this.SBNKYOKA_GENBA_CD.Text, this.SBNKYOKA_GENBA_NAME_RYAKU, null, null, null, out catchErr);
        }

        /// <summary>
        /// 日付コントロールフォーカスイン共通処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void DateControlEnter(object sender, EventArgs e)
        {
            // VUNGUYEN 20150525 #1294 START
            if (((r_framework.CustomControl.CustomDateTimePicker)sender).Value == null)
            {
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //((r_framework.CustomControl.CustomDateTimePicker)sender).Value = DateTime.Today;
                ((r_framework.CustomControl.CustomDateTimePicker)sender).Value = this.logic.parentForm.sysDate.Date;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
            }
            // VUNGUYEN 20150525 #1294 END
        }

        /// <summary>
        /// 排出事業者CD Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void HaishutsuJigyoushaCDValidated(object sender, EventArgs e)
        {
            this.logic.CheckHaishutsuJigyoushaCD();
        }

        /// <summary>
        /// 排出事業場CD Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void HaishutsuJigyoujouCDValidated(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.isError && this.HAISHUTSU_JIGYOUJOU_CD.Text.Equals(this.PreviousCd))
            {
                return;
            }
            this.logic.isError = false;

            this.logic.CheckHaishutsuJigyoujouCD(e);
        }

        /// <summary>
        /// ヘッダ部分(タブ外)－排出事業場CD エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void HaishutsuJigyoujouCDEnter(object sender, EventArgs e)
        {
            // 前回値保存
            this.PreviousCd = HAISHUTSU_JIGYOUJOU_CD.Text;
            this.PreviousName = GENBA_NAME_RYAKU.Text;
        }

        /// <summary>
        /// 委託契約書参照ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FileRefClick(object sender, EventArgs e)
        {
            this.logic.FileRefClick();
        }

        /// <summary>
        /// 委託契約書閲覧ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void BrowseClick(object sender, EventArgs e)
        {
            this.logic.BrowseClick();
        }

        /// <summary>
        /// タブ切り替えイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void TabSelectIndexChanged(object sender, EventArgs e)
        {
            if ("tabPage7" == this.tabItakuKeiyakuData.SelectedTab.Name)
            {
                this.logic.IsYoteiTabSelect = true;
            }

            this.logic.TabSelect();
        }

        /// <summary>
        /// システムID Leaveイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SystemIdValidated(object sender, EventArgs e)
        {
            this.logic.CheckSystemId();
        }

        /// <summary>
        /// 日付コントロール共通値変更チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void DateControlValidating(object sender, CancelEventArgs e)
        {
            // 日付相関チェック
            if (!this.logic.CheckDateCorrelation(((System.Windows.Forms.Control)sender).Name))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 日付コントロール共通値変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void DateControlValueChanged(object sender, EventArgs e)
        {
            // 契約日付チェック
            bool catchErr = this.logic.CheckKeiyakuDate();
            if (catchErr)
            {
                return;
            }

            // ステータスチェック
            this.logic.CheckStatus();
        }

        /// <summary>
        /// 更新種別テキスト変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void KoushinShubetsuTextChanged(object sender, EventArgs e)
        {
            this.logic.KoushinShubetsuTextChanged();
        }

        /// <summary>
        /// 委託契約基本 排出現場一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListKihonHstGenbaCellValidating(object sender, CellValidatingEventArgs e)
        {
            if (!this.logic.isSeted)
            {
                this.logic.ListKihonHstGenbaCellValidating(e);
            }
        }

        /// <summary>
        /// 委託契約基本 明細のセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListKihonHstGenbaCellEnter(object sender, CellEventArgs e)
        {
            this.logic.ListKihonHstGenbaCellEnter(sender, e);
        }

        /// <summary>
        /// 委託契約 別表1排出一覧のセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu1HstCellEnter(object sender, CellEventArgs e)
        {
            this.logic.ListBetsu1HstCellEnter(e);
            this.logic.ListBetsu1HstCellSwitchCdName(e, ItakuKeiyakuHoshuConstans.FocusSwitch.IN);
        }

        /// <summary>
        /// 委託契約 別表3排出一覧のセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu3HstCellEnter(object sender, CellEventArgs e)
        {
            this.logic.ListBetsu3HstCellEnter(sender, e);
        }

        /// <summary>
        /// 委託契約 別表4排出一覧のセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu4HstCellEnter(object sender, CellEventArgs e)
        {
            this.logic.ListBetsu4HstCellEnter(sender, e);
        }

        /// <summary>
        /// 委託契約 別表1排出一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu1HstCellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListBetsu1HstCellValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu1HstCellValidated(object sender, CellEventArgs e)
        {
            this.logic.ListBetsu1HstCellSwitchCdName(e, ItakuKeiyakuHoshuConstans.FocusSwitch.OUT);
        }

        /// <summary>
        /// セルフォーマット設定イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu1HstCellFormatting(object sender, CellFormattingEventArgs e)
        {
            bool catchErr = false;
            if (e.CellName.Equals("UNPAN_YOTEI_SUU") && e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = this.logic.FormatSystemSuuryouHinmei(Convert.ToDecimal(e.Value), out catchErr);
                if (catchErr)
                {
                    return;
                }
            }
            if (e.CellName.Equals("UNPAN_ITAKU_TANKA") && e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = this.logic.FormatSystemTanka(Convert.ToDecimal(e.Value), out catchErr);
                if (catchErr)
                {
                    return;
                }
            }
            if (e.CellName.Equals("SHOBUN_YOTEI_SUU") && e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = this.logic.FormatSystemSuuryouHinmei(Convert.ToDecimal(e.Value), out catchErr);
                if (catchErr)
                {
                    return;
                }
            }
            if (e.CellName.Equals("SHOBUN_ITAKU_TANKA") && e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = this.logic.FormatSystemTanka(Convert.ToDecimal(e.Value), out catchErr);
            }
        }

        /// <summary>
        /// 委託契約 別表1予定一覧のデータエラーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu1HstDataError(object sender, DataErrorEventArgs e)
        {
            // 例外を無視する
            if (e.CellIndex < 0)
                return;

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if ((e.Context & DataErrorContexts.CurrentCellChange) != DataErrorContexts.CurrentCellChange)
            {
                if (e.CellName.Equals("UNPAN_YOTEI_SUU"))
                {
                    msgLogic.MessageBoxShow("E084", ((TextBox)this.listHinmei.EditingControl).Text);
                    e.Cancel = true;
                    ((TextBox)this.listHinmei.EditingControl).SelectAll();
                    this.listHinmei.Focus();
                }
                if (e.CellName.Equals("UNPAN_ITAKU_TANKA"))
                {
                    msgLogic.MessageBoxShow("E084", ((TextBox)this.listHinmei.EditingControl).Text);
                    e.Cancel = true;
                    ((TextBox)this.listHinmei.EditingControl).SelectAll();
                    this.listHinmei.Focus();
                }
                if (e.CellName.Equals("SHOBUN_YOTEI_SUU"))
                {
                    msgLogic.MessageBoxShow("E084", ((TextBox)this.listHinmei.EditingControl).Text);
                    e.Cancel = true;
                    ((TextBox)this.listHinmei.EditingControl).SelectAll();
                    this.listHinmei.Focus();
                }
                if (e.CellName.Equals("SHOBUN_ITAKU_TANKA"))
                {
                    msgLogic.MessageBoxShow("E084", ((TextBox)this.listHinmei.EditingControl).Text);
                    e.Cancel = true;
                    ((TextBox)this.listHinmei.EditingControl).SelectAll();
                    this.listHinmei.Focus();
                }
            }
        }

        /// <summary>
        /// 委託契約 別表1予定一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu1YoteiCellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListBetsu1YoteiCellValidating(e);
        }

        /// <summary>
        /// 委託契約 別表2一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu2CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListBetsu2CellValidating(e);
        }

        /// <summary>
        /// 委託契約 別表3一覧のセル編集開始イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu3CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            this.logic.ListBetsu3CellBeginEdit(e);
        }

        /// <summary>
        /// 委託契約 別表3処分一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu3CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListBetsu3CellValidating(e);
        }

        /// <summary>
        /// 委託契約 別表4一覧のセル編集開始イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu4CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            this.logic.ListBetsu4CellBeginEdit(e);
        }

        /// <summary>
        /// 委託契約 別表4一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListBetsu4CellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListBetsu4CellValidating(e);
        }

        /// <summary>
        /// 委託契約 再生品目のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListSaiseiCellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListSaiseiCellValidating(e);
        }

        /// <summary>
        /// 委託契約 覚書のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListOboeCellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListOboeCellValidating(e);
        }

        /// <summary>
        /// 運搬紐付業者選択ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UpnKyokaSearch(object sender, EventArgs e)
        {
            /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
            if (this.logic.UPNDateCheck())
            {
                return;
            }
            /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end
            this.logic.UpnKyokaSearch();
        }

        /// <summary>
        /// 運搬紐付業者ゴミ箱ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void UpnKyokaDust(object sender, EventArgs e)
        {
            this.logic.UpnKyokaDust();
        }

        ///// <summary>
        ///// 処分許可証　業者CD Validatingイベント
        ///// </summary>
        ///// <param name="e"></param>
        //internal virtual void SbnKyokaGyoushaValidating(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    this.logic.SbnKyokaGyoushaValidating(e);
        //}

        /// <summary>
        /// 処分許可証　現場CD Validatingイベント
        /// </summary>
        /// <param name="e"></param>
        internal virtual void SbnKyokaGenbaValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.SbnKyokaGenbaValidating(e);
        }

        /// <summary>
        /// 処分紐付業者選択ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SbnKyokaSearch(object sender, EventArgs e)
        {
            /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
            if (this.logic.SBNDateCheck())
            {
                return;
            }
            /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end
            this.logic.SbnKyokaSearch();
        }

        /// <summary>
        /// 処分紐付業者ゴミ箱ボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void SbnKyokaDust(object sender, EventArgs e)
        {
            this.logic.SbnKyokaDust();
        }

        /// <summary>
        /// 行政許可区分表示変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListKyokashoCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellName.Equals("UPNKYOKA_KYOKA_KBN") || e.CellName.Equals("SBNKYOKA_KYOKA_KBN"))
            {
                this.logic.ListKyokashoCellFormatting(e);
            }
        }

        /// <summary>
        /// 中間処分場パターン呼出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void BtnGetSbnPtn(object sender, EventArgs e)
        {
            this.logic.GetSbnPtn();
        }

        /// <summary>
        /// 中間処分場パターン登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void BtnSetSbnPtn(object sender, EventArgs e)
        {
            this.logic.SetSbnPtn();
        }

        /// <summary>
        /// 最終処分場パターン呼出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void BtnGetLastSbnPtn(object sender, EventArgs e)
        {
            this.logic.GetLastSbnPtn();
        }

        /// <summary>
        /// 最終処分場パターン登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void BtnSetLastSbnPtn(object sender, EventArgs e)
        {
            this.logic.SetLastSbnPtn();
        }

        /// <summary>
        /// 印刷
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Print(object sender, EventArgs e)
        {
            if (this.logic.HstCheck())
            {
                return;
            }
            /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　start
            if (this.logic.DateCheck())
            {
                return;
            }
            /// 20141217 Houkakou 「委託契約書入力」の日付チェックを追加する　end
            // 印刷出力
            this.logic.Print();
        }

        /// <summary>
        /// 別表4最終編集ボックス表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ListBetsu4EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.ListBetsu4EditingControlKeyDown;
            e.Control.KeyDown += this.ListBetsu4EditingControlKeyDown;
        }

        /// <summary>
        /// 別表4最終編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBetsu4EditingControlKeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckListBetsu4Popup(e);
        }

        /// <summary>
        /// 別表4最終一覧内の処分事業者ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterShobunGyoushaCDBetsu4()
        {
            this.logic.CheckLastShobunGyoushaCD();
        }

        /// <summary>
        /// 個別指定ボックスチェックチェンジ表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>>
        private void KOBETSU_SHITEI_CHECK_CheckedChanged(object sender, EventArgs e)
        {
            bool check_flag = this.KOBETSU_SHITEI_CHECK.Checked;

            this.logic.KOBETSU_SHITEI_CHECK_CheckedChanged(check_flag);
        }

        /// <summary>
        /// 現場一覧内の処分事業者ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterShobunGyoushaCDGenba()
        {
            this.logic.CheckShobunGyoushaCDGenba();
        }

        /// <summary>
        /// 別表3一覧内の処分事業者ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterShobunGyoushaCDBetsu3()
        {
            this.logic.CheckShobunGyoushaCD();
        }

        /// <summary>
        /// 現場一覧内の処分事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterShobunJigyoujouCDGenba()
        {
            // カレントセルを取得する
            Cell cell = this.listHinmei.CurrentCell;
            bool catchErr = false;

            if (!this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty))
            {
                // コードから略称、住所をセットする
                catchErr = this.logic.SetJigyoushaData(this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_GYOUSHA_CD"].Value.ToString(), this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_GYOUSHA_NAME"], null, null, null);
                if (catchErr)
                {
                    return;
                }

                if (!this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(string.Empty) && !this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_JIGYOUJOU_CD"].Value.ToString().Equals(this.PreviousValue))
                {
                    // コードから略称、住所をセットする
                    this.logic.SetJigyoujouData(this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_GYOUSHA_CD"].Value.ToString(), this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_JIGYOUJOU_CD"].Value.ToString(), this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_JIGYOUJOU_NAME"], this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_JIGYOUJOU_ADDRESS1"], this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_JIGYOUJOU_ADDRESS2"], this.listHinmei[cell.RowIndex, "HINMEI_SHOBUN_TODOUFUKEN"], out catchErr);
                }
            }
        }

        /// <summary>
        /// 運搬事業場ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterUnpanGyousha()
        {
            // カレントセルを取得する
            Cell cell = this.listBetsu2.CurrentCell;

            if (!this.listBetsu2[cell.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty) && errchk && !this.listBetsu2[cell.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString().Equals(this.PreviousValue))
            {
                this.logic.SetJigyoushaData(this.listBetsu2[cell.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString(), this.listBetsu2[cell.RowIndex, "UNPAN_GYOUSHA_NAME"], this.listBetsu2[cell.RowIndex, "UNPAN_GYOUSHA_ADDRESS1"], this.listBetsu2[cell.RowIndex, "UNPAN_GYOUSHA_ADDRESS2"], this.listBetsu2[cell.RowIndex, "UNPAN_TODOUFUKEN_NAME"]);
            }
        }

        /// <summary>
        /// セルフォーマット設定イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListTsumikaeCellFormatting(object sender, CellFormattingEventArgs e)
        {
            bool catchErr = false;
            if (e.CellName.Equals("HOKAN_JOGEN") && e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
            {
                e.Value = this.logic.FormatSystemSuuryou2syousuu(Convert.ToDecimal(e.Value), out catchErr);
                if (catchErr)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 委託契約 積替一覧のセルエンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListTsumikaeCellEnter(object sender, CellEventArgs e)
        {
            this.logic.ListTsumikaeCellSwitchCdName(e, ItakuKeiyakuHoshuConstans.FocusSwitch.IN);
        }

        /// <summary>
        /// 積替編集ボックス表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ListTsumikaeEditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.ListTsumikaeEditingControlKeyDown;
            e.Control.KeyDown += this.ListTsumikaeEditingControlKeyDown;
        }

        /// <summary>
        /// 積替編集ボックスキーダウン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListTsumikaeEditingControlKeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckListTsumikaePopup(e);
        }

        /// <summary>
        /// 委託契約 積替一覧のセル編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListTsumikaeCellValidating(object sender, CellValidatingEventArgs e)
        {
            this.logic.ListTsumikaeCellValidating(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void ListTsumikaeCellValidated(object sender, CellEventArgs e)
        {
            this.logic.ListTsumikaeCellSwitchCdName(e, ItakuKeiyakuHoshuConstans.FocusSwitch.OUT);
        }

        /// <summary>
        /// 積替保管場所ポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterTsumikae()
        {
            // カレントセルを取得する
            Cell cell = this.listTsumikae.CurrentCell;

            if (!this.listTsumikae[cell.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString().Equals(string.Empty))
            {
                bool catchErr = false;
                // コードから略称、住所をセットする
                catchErr = this.logic.SetJigyoushaData(this.listTsumikae[cell.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString(), this.listTsumikae[cell.RowIndex, "UNPAN_GYOUSHA_NAME"], null, null, null);
                if (catchErr)
                {
                    return;
                }

                if (!this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString().Equals(string.Empty) && !this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString().Equals(this.PreviousValue))
                {
                    // コードから略称、住所をセットする
                    this.logic.SetJigyoujouData(this.listTsumikae[cell.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString(), this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_CD"].Value.ToString(), this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_NAME"], this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS1"], this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_ADDRESS2"], this.listTsumikae[cell.RowIndex, "TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"], out catchErr);
                }
            }
        }

        private bool iserr = false;

        /// <summary>
        /// 運搬タブの明細行追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBetsu2RowsAdding(object sender, RowsAddingEventArgs e)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.errchk = true;
            if (this.listBetsu2[e.RowIndex - 1, "UNPAN_GYOUSHA_CD"].Value != null)
            {
                // 契約種類が「収集運搬」、「収集運搬処分」の場合は、運搬タブでの２行以上の入力を不可にする
                if (this.listBetsu2.Rows.Count > 1 && (this.ITAKU_KEIYAKU_SHURUI.Text == "1" || this.ITAKU_KEIYAKU_SHURUI.Text == "3") && e.RowIndex > 1)
                {
                    if (!iserr)
                    {
                        msgLogic.MessageBoxShow("E216");
                        iserr = true;
                        ((TextBox)this.listBetsu2.EditingControl).Clear();
                        iserr = false;
                        this.errchk = false;
                    }
                    e.Cancel = true;
                }
                // '2'（処分契約）の場合,5行まで入力可能、6行以上の入力は不可とする
                if (this.listBetsu2.Rows.Count > 5 && this.ITAKU_KEIYAKU_SHURUI.Text == "2" && e.RowIndex > 5)
                {
                    if (!iserr)
                    {
                        msgLogic.MessageBoxShow("E217");
                        iserr = true;
                        ((TextBox)this.listBetsu2.EditingControl).Clear();
                        iserr = false;
                        this.errchk = false;
                    }
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 委託契約品名タブにの品名CDの必須入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool ItakuKeiyakuHinmeiCheck()
        {
            try
            {
                foreach (Row dr in this.listHinmei.Rows)
                {
                    if (dr.IsNewRow ||
                        ((dr["HINMEI_CD"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_CD"].Value.ToString())) &&
                         (dr["HINMEI_NAME"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_NAME"].Value.ToString())) &&
                         (dr["TSUMIKAE"].Value == null || string.IsNullOrWhiteSpace(dr["TSUMIKAE"].Value.ToString()) || !bool.Parse(dr["TSUMIKAE"].Value.ToString())) &&
                         (dr["UNPAN_YOTEI_SUU"].Value == null || string.IsNullOrWhiteSpace(dr["UNPAN_YOTEI_SUU"].Value.ToString())) &&
                         (dr["UNPAN_YOTEI_SUU_UNIT_CD"].Value == null || string.IsNullOrWhiteSpace(dr["UNPAN_YOTEI_SUU_UNIT_CD"].Value.ToString())) &&
                         (dr["UNPAN_ITAKU_TANKA"].Value == null || string.IsNullOrWhiteSpace(dr["UNPAN_ITAKU_TANKA"].Value.ToString())) &&
                         (dr["SHOBUN_YOTEI_SUU"].Value == null || string.IsNullOrWhiteSpace(dr["SHOBUN_YOTEI_SUU"].Value.ToString())) &&
                         (dr["SHOBUN_YOTEI_SUU_UNIT_CD"].Value == null || string.IsNullOrWhiteSpace(dr["SHOBUN_YOTEI_SUU_UNIT_CD"].Value.ToString())) &&
                         (dr["SHOBUN_ITAKU_TANKA"].Value == null || string.IsNullOrWhiteSpace(dr["SHOBUN_ITAKU_TANKA"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_HOUHOU_CD"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_HOUHOU_CD"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_HOUHOU_NAME_RYAKU"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_HOUHOU_NAME_RYAKU"].Value.ToString())) &&
                         (dr["SHISETSU_CAPACITY"].Value == null || string.IsNullOrWhiteSpace(dr["SHISETSU_CAPACITY"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_GYOUSHA_CD"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_GYOUSHA_NAME"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_GYOUSHA_NAME"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_JIGYOUJOU_CD"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_JIGYOUJOU_CD"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_JIGYOUJOU_NAME"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_JIGYOUJOU_NAME"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_TODOUFUKEN"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_TODOUFUKEN"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_JIGYOUJOU_ADDRESS1"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_JIGYOUJOU_ADDRESS1"].Value.ToString())) &&
                         (dr["HINMEI_SHOBUN_JIGYOUJOU_ADDRESS2"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_SHOBUN_JIGYOUJOU_ADDRESS2"].Value.ToString()))
                       ))
                    {
                        continue;
                    }

                    if (dr["HINMEI_CD"].Value == null || string.IsNullOrWhiteSpace(dr["HINMEI_CD"].Value.ToString()))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "品名CD");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ItakuKeiyakuHinmeiCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 委託契約報告書分類タブにの報告書分類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        internal bool ItakuKeiyakuHoukokushoBunruiCheck()
        {
            try
            {
                Dictionary<string, string> houkokushoBunruiCdList = new Dictionary<string, string>();

                foreach (Row dr in this.listHoukokushoBunrui.Rows)
                {
                    if (dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value == null || string.IsNullOrWhiteSpace(dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value.ToString()))
                    {
                        continue;
                    }

                    string key = dr.Cells["HOUKOKUSHO_BUNRUI_CD"].Value.ToString();
                    if (houkokushoBunruiCdList.ContainsKey(key))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E031", "報告書分類CD");
                        return false;
                    }
                    else
                    {
                        houkokushoBunruiCdList.Add(key, key);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ItakuKeiyakuHoukokushoBunruiCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 委託契約処分タブにの処分受託者CDと処分事業場CDの入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool ItakuKeiyakuBetsu3Check()
        {
            try
            {
                foreach (Row dr in this.listBetsu3.Rows)
                {
                    if (dr.IsNewRow ||
                        (this.ITAKU_KEIYAKU_SHURUI.Text == "1" &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_GYOUSHA_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_GYOUSHA_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_GYOUSHA_TODOUFUKEN_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_GYOUSHA_ADDRESS1"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_GYOUSHA_ADDRESS2"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS1"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS2"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_HOUHOU_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_HOUHOU_NAME_RYAKU"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHISETSU_CAPACITY"].Value))) ||
                        ((this.ITAKU_KEIYAKU_SHURUI.Text == "2" || this.ITAKU_KEIYAKU_SHURUI.Text == "3") &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_TODOUFUKEN_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS1"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_ADDRESS2"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_HOUHOU_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_HOUHOU_NAME_RYAKU"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHISETSU_CAPACITY"].Value))))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_GYOUSHA_CD"].Value)))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "処分受託者CD");
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(Convert.ToString(dr["SHOBUN_JIGYOUJOU_CD"].Value)))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "処分事業場CD");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ItakuKeiyakuBetsu3Check", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 委託契約積替タブにの運搬業者CDと積替保管場所CDの入力チェックと重複チェック
        /// </summary>
        /// <returns></returns>
        internal bool ItakuKeiyakuTsumikaeCheck()
        {
            try
            {
                string gyoshaCD = string.Empty;
                string hokabaCd = string.Empty;
                string gyoshaCDTemp = string.Empty;
                string hokabaCdTemp = string.Empty;
                foreach (Row dr in this.listTsumikae.Rows)
                {
                    if (dr.IsNewRow ||
                        (string.IsNullOrWhiteSpace(Convert.ToString(dr["UNPAN_GYOUSHA_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["UNPAN_GYOUSHA_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["HOKAN_JOGEN"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["HOKAN_JOGEN_UNIT_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["KONGOU"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["KONGOU_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHUSENBETU"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["SHUSENBETU_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["UNPAN_FROM"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["UNPAN_FROM_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["UNPAN_TO"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["UNPAN_TO_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["TSUMIKAE_HOKANBA_CD"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["TSUMIKAE_HOKANBA_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["TSUMIKAE_HOKANBA_ADDRESS1"].Value)) &&
                         string.IsNullOrWhiteSpace(Convert.ToString(dr["TSUMIKAE_HOKANBA_ADDRESS2"].Value))
                       ))
                    {
                        continue;
                    }
                    gyoshaCD = Convert.ToString(dr["UNPAN_GYOUSHA_CD"].Value);
                    hokabaCd = Convert.ToString(dr["TSUMIKAE_HOKANBA_CD"].Value);
                    if (string.IsNullOrWhiteSpace(gyoshaCD))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "運搬業者CD");
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(hokabaCd))
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E048", "積替保管場所CD");
                        return false;
                    }

                    //for (int i = 0; i < dr.Index; i++)
                    //{
                    //    Row row = this.listTSUMIKAE.Rows[i];
                    //    gyoshaCDTemp = Convert.ToString(row["UNPAN_GYOUSHA_CD"].Value);
                    //    hokabaCdTemp = Convert.ToString(row["TSUMIKAE_HOKANBA_CD"].Value);
                    //    if (gyoshaCD == gyoshaCDTemp && hokabaCd == hokabaCdTemp)
                    //    {
                    //        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //        msgLogic.MessageBoxShow("E031", "運搬業者CD、積替保管場CD");
                    //        return false;
                    //    }
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ItakuKeiyakuTsumikaeCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        // 委託契約書
        private void ITAKU_KEIYAKU_FILE_PATH_Validating(object sender, CancelEventArgs e)
        {
            if (!this.logic.errorCancelFlg)
            {
                return;
            }

            string path = this.ITAKU_KEIYAKU_FILE_PATH.GetResultText();
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    return;
                }
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E024", this.ITAKU_KEIYAKU_FILE_PATH.DisplayItemName);
                var textBox = this.ITAKU_KEIYAKU_FILE_PATH as TextBox;
                if (textBox != null)
                {
                    textBox.SelectAll();
                }
                e.Cancel = true;
            }
        }

        private void UPNKYOKA_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.UPNKYOKA_GYOUSHA_CD_Validating(sender, e);
        }

        private void UPNKYOKA_CHIIKI_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.UPNKYOKA_CHIIKI_CD_Validating(sender, e);
        }

        private void SBNKYOKA_GYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.SBNKYOKA_GYOUSHA_CD_Validating(sender, e);
        }

        private void SBNKYOKA_CHIIKI_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.SBNKYOKA_CHIIKI_CD_Validating(sender, e);
        }

        /// <summary>
        /// 品名一覧内の品名CDポップアップ選択後処理
        /// </summary>
        public virtual void PopupAfterHinmei()
        {
            this.logic.PopupAfterHinmei();
        }

        // VUNGUYEN 20150525 #1294 START
        private void UPNKYOKA_BEGIN_Leave(object sender, EventArgs e)
        {
            if (this.logic.UPNKYOKA_CheckDateRelation())
            {
                UPNKYOKA_BEGIN.Focus();
            }
        }

        private void UPNKYOKA_END_Leave(object sender, EventArgs e)
        {
            if (this.logic.UPNKYOKA_CheckDateRelation())
            {
                UPNKYOKA_END.Focus();
            }
        }

        private void SBNKYOKA_BEGIN_Leave(object sender, EventArgs e)
        {
            if (this.logic.SBNKYOKA_CheckDateRelation())
            {
                SBNKYOKA_BEGIN.Focus();
            }
        }

        private void SBNKYOKA_END_Leave(object sender, EventArgs e)
        {
            if (this.logic.SBNKYOKA_CheckDateRelation())
            {
                SBNKYOKA_END.Focus();
            }
        }

        private void KEIYAKUSHO_CREATE_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                ITAKU_KEIYAKU_NO.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                KOUSHIN_SHUBETSU.Focus();
            }
        }

        private void KEIYAKUSHO_SEND_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                BIKOU1.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                BIKOU2.Focus();
            }
        }

        private void KEIYAKUSHO_RETURN_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                KOBETSU_SHITEI_CHECK.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                HAISHUTSU_JIGYOUSHA_CD.Focus();
            }
        }

        private void KEIYAKUSHO_END_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                KOBETSU_SHITEI_CHECK.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                HAISHUTSU_JIGYOUSHA_CD.Focus();
            }
        }

        private void KEIYAKUSHO_KEIYAKU_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                KOUSHIN_SHUBETSU.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                YUUKOU_BEGIN.Focus();
            }
        }

        private void YUUKOU_BEGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                KEIYAKUSHO_KEIYAKU_DATE.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                YUUKOU_END.Focus();
            }
        }

        private void YUUKOU_END_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                YUUKOU_BEGIN.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                BIKOU1.Focus();
            }
        }

        private void KOUSHIN_END_DATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                YUUKOU_END.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                BIKOU1.Focus();
            }
        }

        private void UPNKYOKA_BEGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                UPNKYOKA_KBN.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                UPNKYOKA_END.Focus();
            }
        }

        private void UPNKYOKA_END_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                UPNKYOKA_BEGIN.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                UPNKYOKA_NO.Focus();
            }
        }

        private void SBNKYOKA_BEGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                SBNKYOKA_KBN.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                SBNKYOKA_END.Focus();
            }
        }

        private void SBNKYOKA_END_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && e.Shift)
            {
                SBNKYOKA_BEGIN.Focus();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                SBNKYOKA_NO.Focus();
            }
        }

        // VUNGUYEN 20150525 #1294 END

        /// <summary>
        /// 処分許可業者POPUP_BEFイベント
        /// </summary>
        public void SBNKYOKA_GYOUSHA_POPUP_BEF()
        {
            sbnKyokaGyoushaBef = this.SBNKYOKA_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 処分許可業者POPUP_AFTイベント
        /// </summary>
        public void SBNKYOKA_GYOUSHA_POPUP_AFT()
        {
            sbnKyokaGyoushaAft = this.SBNKYOKA_GYOUSHA_CD.Text;
            if (sbnKyokaGyoushaBef != sbnKyokaGyoushaAft)
            {
                this.SBNKYOKA_GENBA_CD.Text = string.Empty;
                this.SBNKYOKA_GENBA_NAME_RYAKU.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分許可業者POPUP_BEFイベント
        /// </summary>
        public void UNPAN_GYOUSHA_POPUP_BEF()
        {
            if (this.listTsumikae.CurrentRow == null) { return; }
            unpanGyoushaBef = string.Empty;
            if (this.listTsumikae.CurrentCell.Name == "UNPAN_GYOUSHA_CD")
            {
                unpanGyoushaBef = Convert.ToString(this.listTsumikae.CurrentCell.EditedFormattedValue);
            }
        }

        /// <summary>
        /// 処分許可業者POPUP_AFTイベント
        /// </summary>
        public void UNPAN_GYOUSHA_POPUP_AFT()
        {
            if (this.listTsumikae.CurrentRow == null) { return; }
            if (this.listTsumikae.CurrentCell.Name == "UNPAN_GYOUSHA_CD")
            {
                unpanGyoushaAft = Convert.ToString(this.listTsumikae.CurrentCell.EditedFormattedValue);
                if (unpanGyoushaBef != unpanGyoushaAft)
                {
                    this.listTsumikae.CurrentRow.Cells["TSUMIKAE_HOKANBA_CD"].Value = string.Empty;
                    this.listTsumikae.CurrentRow.Cells["TSUMIKAE_HOKANBA_NAME"].Value = string.Empty;
                    this.listTsumikae.CurrentRow.Cells["TSUMIKAE_HOKANBA_TODOUFUKEN_NAME"].Value = string.Empty;
                    this.listTsumikae.CurrentRow.Cells["TSUMIKAE_HOKANBA_ADDRESS1"].Value = string.Empty;
                    this.listTsumikae.CurrentRow.Cells["TSUMIKAE_HOKANBA_ADDRESS2"].Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// 社内経路名CD Enterイベント
        /// </summary>
        /// <param name="e"></param>
        internal void ShanaiKeiroCDTextEnter(object sender, EventArgs e)
        {
            // 前回値として社内経路名CDを保持する。
            this.shanaiKeiroPreviousCd = this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text;
        }

        /// <summary>
        /// 社内経路名CD Validatedイベント
        /// </summary>
        /// <param name="e"></param>
        internal void ShanaiKeiroCDTextValidated(object sender, EventArgs e)
        {
            var msgLogic = new r_framework.Logic.MessageBoxShowLogic();

            // 前回値と比較する。
            if (!this.shanaiKeiroPreviousCd.Equals(this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Text))
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.DialogResult.Yes;

                if (this.keiroIchiran.Rows.Count > 1)
                {
                    result = msgLogic.MessageBoxShowConfirm("既に承認経路がセットされています。\nクリアを行い、承認経路をセットしますか？");
                }
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.logic.ShanaiKeiroCDValidated();
                }
            }
        }

        /// <summary>
        /// 一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.CellEnter(e.ColumnIndex);
        }

        /// <summary>
        /// 一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            this.logic.ShainCDValidating(e.ColumnIndex);
        }

        /// <summary>
        /// 契約先一覧CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void IchiranKeiyakusaki_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            this.logic.CellValidatingKeiyakusaki(e.ColumnIndex);
        }
        /// <summary>
        /// 社員ポップアップ選択後処理
        /// </summary>
        internal void PopupAfterShainCD()
        {
            this.logic.SetShainInfo();
        }

        /// <summary>
        /// 契約先一覧CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void IchiranKeiyakusaki_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.CellEnterKeiyakusaki(e.ColumnIndex);
        }

        /// <summary>
        /// ファイルアップロード一覧クリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.previewClick(sender,e);
        }
    }
}