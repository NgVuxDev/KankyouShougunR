using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.FormManager;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.Scale.Keiryou.Const;
using Shougun.Core.Scale.Keiryou;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Core.Scale.Keiryou.Dto;

namespace Shougun.Core.Scale.Keiryou
{
    /// <summary>
    /// 計量入力
    /// </summary>
    public partial class UIForm : SuperForm
    {

        #region マニフェスト連携用変数
        public short RenkeiDenshuKbnCd { get; private set; }
        public long RenkeiSystemId { get; private set; }
        public long RenkeiMeisaiSystemId { get; private set; }
        #endregion

        #region フィールド
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 計量入力のSYSTE_ID
        /// </summary>
        public long KeiryouSysId = -1;

        /// <summary>
        /// 計量入力のSEQ
        /// </summary>
        public int KeiryouSEQ = -1;

        /// <summary>
        /// 計量明細のSYSTE_ID
        /// </summary>
        public long MeisaiSysId = -1;

        /// <summary>
        /// 計量番号
        /// </summary>
        public long KeiryouNumber = -1;

        /// <summary>
        /// SEQ
        /// このパラメータが０以外だとDeleteFlgを無視して表示する
        /// </summary>
        public string SEQ = "0";

        /// <summary>
        /// 継続計量フラグ
        /// </summary>
        internal bool KeizokuKeiryouFlg = false;

        /// <summary>
        /// 受付番号
        /// </summary>
        public long UketukeNumber = -1;

        /// <summary>
        /// 前回値チェック用変数(header用)
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        public Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();


        /// <summary>
        /// 前回計量番号
        /// </summary>
        public string beforKeiryouNumber = string.Empty;

        /// <summary>
        /// 前回受付番号
        /// </summary>
        public string beforUketukeNumber = string.Empty;

        /// <summary>
        /// 前回取引先コード
        /// </summary>
        public string beforTorihikisakiCD = string.Empty;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyousaCD = string.Empty;

        /// <summary>
        /// 前回現場コード
        /// </summary>
        public string beforeGenbaCD = string.Empty;

        /// <summary>
        /// 前回運搬業者コード
        /// </summary>
        public string beforUnpanGyoushaCD = string.Empty;

        /// <summary>
        /// 前回荷積業者コード
        /// </summary>
        public string beforNizumiGyoushaCD = string.Empty;

        /// <summary>
        /// 前回荷積現場コード
        /// </summary>
        public string beforNizumiGenbaCD = string.Empty;

        /// <summary>
        /// 前回荷降現場コード
        /// </summary>
        public string beforNioroshiGyoushaCD = string.Empty;

        /// <summary>
        /// 前回荷降現場コード
        /// </summary>
        public string beforNioroshiGenbaCD = string.Empty;

        /// <summary>
        /// Close処理の後に実行するメソッド
        /// 制約：戻り値なし、引数なし、Publicなメソッド
        /// </summary>
        public delegate void LastRunMethod();

        /// <summary>
        /// Close処理の後に実行するメソッド
        /// </summary>
        public LastRunMethod closeMethod;

        /// <summary>
        /// 複写ボタン処理かどうかのフラグ
        /// </summary>
        private bool blnCopy = false;

        /// <summary>
        /// 複写処理
        /// </summary>
        public bool blnCopyProgress = false;

        /// <summary>
        /// 受付番号処理
        /// </summary>
        public bool blnUketsukeProgress = false;

        /// <summary>
        /// 手入力切替
        /// 手入力：true;自動入力:false
        /// /// </summary>
        private bool blnTenyuuryoku = false;

        /// <summary>
        /// 現在選択されているセル名称
        /// /// </summary>
        public String selectedCellName = string.Empty;

        /// <summary>
        /// MultiRowが編集中かどうかのフラグ
        /// </summary>
        private bool editingMultiRowFlag = false;

        /// <summary>
        /// 車輌選択ポップアップ選択中フラグ
        /// </summary>
        internal bool isSelectingSharyouCd = false;

        /// <summary>
        /// 現在画面ロード中かどうかフラグ
        /// FormのLeaveイベント実行中にMultiRowのデータを変更すると
        /// もう一回Leaveイベントが発生してしまう問題の回避策
        /// </summary>
        public bool nowLoding = false;

        /// <summary>
        /// 受付番号検索中
        /// FormのLeaveイベント実行中にMultiRowのデータを変更すると
        /// もう一回Leaveイベントが発生してしまう問題の回避策
        /// </summary>
        public bool uketsukeNumberNowLoding = false;


        /// <summary>
        /// 明細のValueChangedイベントが複数回発行されないための判定変数
        /// </summary>
        private Dictionary<string, bool> controledListForDetailValueChanged = new Dictionary<string, bool>();

        /// <summary>
        /// 画面遷移が発生するコントロール名一覧
        /// </summary>
        private string[] controlNamesForChangeScreenEvents = new string[] { "bt_func7", "bt_func12" };

        /// <summary>
        /// 伝票発行ポップアップ用DTO
        /// </summary>
        internal ParameterDTOClass denpyouHakouPopUpDTO = new ParameterDTOClass();

        // No.2334-->
        /// <summary>
        /// 滞留新規フラグ
        /// </summary>
        internal bool TairyuuNewFlg = false;
        // No.2334<--

        /// <summary>
        /// データ移動モード Flg
        /// True:データ移動モード
        /// </summary>
        internal bool moveData_flg = false;
        /// <summary>
        /// データ移動用 取引先
        /// </summary>
        internal string moveData_torihikisakiCd;
        /// <summary>
        /// データ移動用 業者
        /// </summary>
        internal string moveData_gyousyaCd;
        /// <summary>
        /// データ移動用 現場
        /// </summary>
        internal string moveData_genbaCd;
        /// <summary>
        /// データ移動用 入出区分
        /// </summary>
        internal string moveData_inOutKbn;

        #endregion

        /// <summary>
        /// CellValueChanged実行フラグ
        /// </summary>
        private bool bExecuteCalc = false;

        /// <summary>
        /// 現在イベント処理中かどうかフラグ
        /// FormのCellValueChangedイベント実行中にデータを変更すると
        /// もう一回CellValueChangedイベントが発生してしまう問題の回避策
        /// </summary>
        private bool cellValueChanging = false;

        #region 初期化処理
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="windowType">モード</param>
        /// <param name="keiryouNumber">計量番号 keiryouNumber</param>
        /// <param name="uketsukeNumber">受付番号 uketsukeNumber</param>
        /// <param name="lastRunMethod">計量入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, long keiryouNumber = -1, long uketsukeNumber = -1, bool keizokuKeiryouFlg = false,
            bool newChangeFlg = false, LastRunMethod lastRunMethod = null, string SEQ = "0")
            : base(WINDOW_ID.T_KEIRYO, windowType)
        {
            LogUtility.DebugMethodStart(windowId, windowType, keiryouNumber, uketsukeNumber, keizokuKeiryouFlg, newChangeFlg, lastRunMethod, SEQ);
            try
            {
                CommonShogunData.Create(SystemProperty.Shain.CD);

                TairyuuNewFlg = newChangeFlg;   // No.2334

                this.InitializeComponent();
                this.WindowId = windowId;
                this.WindowType = windowType;
                this.KeiryouNumber = keiryouNumber;
                this.UketukeNumber = uketsukeNumber;
                this.closeMethod = lastRunMethod;
                this.KeizokuKeiryouFlg = keizokuKeiryouFlg;
                if (string.IsNullOrEmpty(SEQ))
                {
                    SEQ = "0";
                }
                this.SEQ = SEQ;

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);

                // マニフェスト連携用変数の初期化
                RenkeiDenshuKbnCd = (short)DENSHU_KBN.KEIRYOU;
                RenkeiSystemId = -1;
                RenkeiMeisaiSystemId = -1;
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, string torihikisakiCd, string gyousyaCd, string genbaCd, string inOutKbn)
            : this(windowId, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowId, windowType, torihikisakiCd, gyousyaCd, genbaCd, inOutKbn);

                //データ移動用
                this.moveData_flg = true;
                this.moveData_torihikisakiCd = torihikisakiCd;
                this.moveData_gyousyaCd = gyousyaCd;
                this.moveData_genbaCd = genbaCd;
                this.moveData_inOutKbn = inOutKbn;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowId, windowType, torihikisakiCd, gyousyaCd, genbaCd);
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {

                // 複写処理判定
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && this.KeiryouNumber >= 0)
                {
                    blnCopyProgress = true;

                }
                else
                {
                    blnCopyProgress = false;
                }

                //受付番号と計量番号判定
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && this.UketukeNumber >= 0)
                {
                    blnUketsukeProgress = true;
                }
                else
                {
                    blnUketsukeProgress = false;
                }

                base.OnLoad(e);
                bool isOpenFormError = this.logic.GetAllEntityData();
                if (!isOpenFormError)
                {
                    return;
                }
                ParentBaseForm = (BusinessBaseForm)this.Parent;

                //重量コントロール起動
                truckScaleWeight1.ProcessWeight();

                if (!this.logic.WindowInit())
                {
                    return;
                }
                if (!this.logic.ButtonInit())
                {
                    return;
                }
                if (!this.logic.EventInit())
                {
                    return;
                }

                if (!isOpenFormError)
                {
                    this.FormClose(null, e);
                }


                //受付番号と計量番号判定
                if (this.KeiryouNumber != -1)
                {
                    this.KEIRYOU_NUMBER.Text = this.KeiryouNumber.ToString();
                    base.OnLoad(e);
                    if (!this.logic.GetAllEntityData())
                    {
                        return;
                    }

                    if (!this.logic.WindowInit())
                    {
                        return;
                    }

                    // 複写の場合
                    if (blnCopyProgress)
                    {
                        if (!TairyuuNewFlg) // No.2334
                        {
                            // 計量番号、  日連番/年連番、 受付番号をクリアする
                            this.KEIRYOU_NUMBER.Text = string.Empty;
                            this.RENBAN.Text = string.Empty;
                            this.UKETSUKE_NUMBER.Text = string.Empty;
                        }

                        // 入力担当者
                        this.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                        this.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();

                        // 登録者情報
                        this.logic.headerForm.CreateUser.Text = string.Empty;
                        this.logic.headerForm.CreateDate.Text = string.Empty;

                        // 更新者情報
                        this.logic.headerForm.LastUpdateUser.Text = string.Empty;
                        this.logic.headerForm.LastUpdateDate.Text = string.Empty;

                        // 日付系初期値設定
                        this.DENPYOU_DATE.Value = DateTime.Now;


                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        if (!this.logic.setHeaderInfo(this.WindowType))
                        {
                            return;
                        }
                    }


                    if (!this.logic.ButtonInit())
                    {
                        return;
                    }
                    this.DENPYOU_DATE.Focus();  // 初期フォーカス位置
                }
                else if (this.UketukeNumber != -1)
                {

                    if (!this.uketsukeNumberNowLoding)
                    {
                        this.uketsukeNumberNowLoding = true;

                        base.OnLoad(e);
                        if (!this.logic.GetAllEntityData())
                        {
                            this.uketsukeNumberNowLoding = false;
                            return;
                        }
                        if (!this.logic.setUketsukeEntity(this.UketukeNumber))
                        {
                            this.uketsukeNumberNowLoding = false;
                            this.UKETSUKE_NUMBER.Focus();
                            return;
                        }

                        if (!this.logic.WindowInit())
                        {
                            return;
                        }
                        if (!this.logic.ButtonInit())
                        {
                            return;
                        }
                        // 初期フォーカス位置  
                        this.UKETSUKE_NUMBER.Text = this.UketukeNumber.ToString();
                        this.beforUketukeNumber = this.UketukeNumber.ToString();
                        this.UKETSUKE_NUMBER.Focus();

                        this.uketsukeNumberNowLoding = false;
                    }

                }

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.gcMultiRow1 != null)
                {
                    this.gcMultiRow1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodStart(e);
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
            this.logic.SetTopControlFocus();   // No.3822
        }
        #endregion

        #region イベント

        /// <summary>
        /// 重量取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Weight_Click(object sender, EventArgs e)
        {
            //this.logic.EventRun(false);
            this.logic.SetJyuuryou();
            //this.logic.EventRun(true);

        }

        /// <summary>
        /// 重量値フォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NumberFormat(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.ToAmountValue(sender);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 計量番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KEIRYOU_NUMBER_OnLeave(object sender, EventArgs e)
        {

            // 番号が変更されていない場合、処理しない
            if (this.beforKeiryouNumber == this.KEIRYOU_NUMBER.Text)
            {
                return;
            }


            try
            {
                if (!nowLoding)
                {
                    nowLoding = true;
                    // 計量番号検索処理
                    this.KEIRYOU_NUMBER_Search(sender, e);
                    // 前回検索計量番号を設定
                    this.beforKeiryouNumber = this.KEIRYOU_NUMBER.Text;
                    nowLoding = false;
                }

            }
            catch
            {
                nowLoding = false;
                throw;
            }
        }
        /// <summary>
        /// 計量番号検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KEIRYOU_NUMBER_Search(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                {
                    return;
                }

                // 初期化
                this.blnUketsukeProgress = false;

                long keiryouNumber = 0;
                if (!string.IsNullOrEmpty(this.KEIRYOU_NUMBER.Text)
                    && long.TryParse(this.KEIRYOU_NUMBER.Text.ToString(), out keiryouNumber))
                {
                    this.KeiryouNumber = keiryouNumber;

                    //base.OnLoad(e);
                    if (!this.logic.GetAllEntityData())
                    {
                        // エラー発生時には値をクリアする
                        this.KEIRYOU_NUMBER.IsInputErrorOccured = true;

                        nowLoding = false;
                        this.KEIRYOU_NUMBER.Text = this.beforKeiryouNumber;
                        this.KEIRYOU_NUMBER.Focus();

                        return;
                    }
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    base.HeaderFormInit();
                    this.KEIRYOU_NUMBER.IsInputErrorOccured = false;
                    if (!this.logic.WindowInit())
                    {
                        return;
                    }
                    if (!this.logic.ButtonInit())
                    {
                        return;
                    }
                    this.DENPYOU_DATE.Focus();  // 初期フォーカス位置

                }

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 受付番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UKETSUKE_NUMBER_OnLeave(object sender, EventArgs e)
        {

            // 読取専用の場合、処理しない
            if (UKETSUKE_NUMBER.ReadOnly)
            {
                return;
            }

            // 空白の場合処理しない
            if (string.IsNullOrEmpty(this.UKETSUKE_NUMBER.Text))
            {
                // 入出力区分
                this.KIHON_KEIRYOU.ReadOnly = false;
                //this.KIHON_KEIRYOU.TabStop = true;
                this.KIHON_KEIRYOU.TabStop = this.logic.GetTabStop("KIHON_KEIRYOU");    // No.3822
                this.radbtnHannyu.Enabled = true;
                this.radbtnHanshutu.Enabled = true;

                return;
            }

            // 新規モード以外処理しない
            if (!this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                return;
            }


            // 番号が変更されていない場合、処理しない
            if (this.beforUketukeNumber == this.UKETSUKE_NUMBER.Text)
            {
                return;
            }

            this.UKETSUKE_NUMBER.Text = this.UKETSUKE_NUMBER.Text;
            try
            {
                if (!this.uketsukeNumberNowLoding)
                {
                    this.uketsukeNumberNowLoding = true;

                    //base.OnLoad(e);
                    if (!this.logic.GetAllEntityData())
                    {
                        // エラー発生時には値をクリアする
                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.KeiryouNumber = -1;
                        base.HeaderFormInit();

                        this.uketsukeNumberNowLoding = false;

                        this.UKETSUKE_NUMBER.IsInputErrorOccured = true;
                        this.UKETSUKE_NUMBER.Text = this.beforUketukeNumber;
                        this.UKETSUKE_NUMBER.Focus();
                        return;
                    }
                    if (!this.logic.setUketsukeEntity(long.Parse(this.UKETSUKE_NUMBER.Text)))
                    {
                        // エラー発生時には値をクリアする
                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.KeiryouNumber = -1;
                        base.HeaderFormInit();

                        this.uketsukeNumberNowLoding = false;

                        this.UKETSUKE_NUMBER.IsInputErrorOccured = true;
                        this.UKETSUKE_NUMBER.Text = this.beforUketukeNumber;
                        this.UKETSUKE_NUMBER.Focus();
                        return;
                    }
                    this.UKETSUKE_NUMBER.IsInputErrorOccured = false;

                    if (!this.logic.WindowInit())
                    {
                        return;
                    }
                    if (!this.logic.ButtonInit())
                    {
                        return;
                    }
                    this.DENPYOU_DATE.Focus();  // 初期フォーカス位置      

                    // 前回検索受付番号を設定
                    this.beforUketukeNumber = this.UKETSUKE_NUMBER.Text;


                    this.uketsukeNumberNowLoding = false;
                    /// 20141017 Houkakou 「計量入力画面」の休動Checkを追加する　start
                    this.logic.UketukeBangoCheck();
                    /// 20141017 Houkakou 「計量入力画面」の休動Checkを追加する　end
                }

            }
            catch
            {

                this.uketsukeNumberNowLoding = false;
                throw;

            }

        }

        /// <summary>
        /// ヘッダーの拠点更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KYOTEN_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.logic.CheckKyotenCd();
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 計量番号前ボタンクリック処理
        /// 現在入力されている番号の前の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void previousButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                String previousNumber;
                String tableName = "T_KEIRYOU_ENTRY";
                String fieldName = "KEIRYOU_NUMBER";
                String keiryouNumber = this.KEIRYOU_NUMBER.Text;
                // 前の計量番号を取得
                bool catchErr = true;
                previousNumber = this.logic.GetPreviousNumber(tableName, fieldName, keiryouNumber, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                // 計量番号を設定
                this.KEIRYOU_NUMBER.Text = previousNumber;
                // イベント削除
                this.KEIRYOU_NUMBER.Enter -= this.Control_Enter;
                // 計量番号更新後処理
                // No.2586-->
                if (!nowLoding)
                {
                    nowLoding = true;
                    this.KEIRYOU_NUMBER_Search(sender, e);
                    nowLoding = false;
                }
                // No.2586<--

                // 計量番号にフォーカスを設定
                this.KEIRYOU_NUMBER.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 計量番号後ボタンクリック処理
        /// 現在入力されている番号の後の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void nextButton_OnClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                String nextNumber;
                String tableName = "T_KEIRYOU_ENTRY";
                String fieldName = "KEIRYOU_NUMBER";
                String keiryouNumber = this.KEIRYOU_NUMBER.Text;
                // 次の計量番号を取得
                bool catchErr = true;
                nextNumber = this.logic.GetNextNumber(tableName, fieldName, keiryouNumber, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                // 計量番号を設定
                this.KEIRYOU_NUMBER.Text = nextNumber;
                // イベント削除
                this.KEIRYOU_NUMBER.Enter -= this.Control_Enter;
                // 計量番号更新後処理
                // No.2586-->
                if (!nowLoding)
                {
                    nowLoding = true;
                    this.KEIRYOU_NUMBER_Search(sender, e);
                    nowLoding = false;
                }
                // No.2586<--

                // 計量番号にフォーカスを設定
                this.KEIRYOU_NUMBER.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 入力区分入力処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KIHON_KEIRYOU_TextChanged(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if ("1".Equals(this.KIHON_KEIRYOU.Text))
                {
                    if (!this.logic.nizumi_nioroshi(true))
                    {
                        return;
                    }
                    this.GYOUSHA_CD.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_UKEIRE";
                    this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_UKEIRE";
                    this.GENBA_CD.PopupSearchSendParams[1].KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
                    this.GENBA_SEARCH_BUTTON.PopupSearchSendParams[1].KeyName = "M_GYOUSHA.GYOUSHAKBN_UKEIRE";
                    this.UNPAN_GYOUSHA_CD.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_UKEIRE";
                    this.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_UKEIRE";
                }
                if ("2".Equals(this.KIHON_KEIRYOU.Text))
                {
                    if (!this.logic.nizumi_nioroshi(false))
                    {
                        return;
                    }
                    this.GYOUSHA_CD.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_SHUKKA";
                    this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_SHUKKA";
                    this.GENBA_CD.PopupSearchSendParams[1].KeyName = "M_GYOUSHA.GYOUSHAKBN_SHUKKA";
                    this.GENBA_SEARCH_BUTTON.PopupSearchSendParams[1].KeyName = "M_GYOUSHA.GYOUSHAKBN_SHUKKA";
                    this.UNPAN_GYOUSHA_CD.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_SHUKKA";
                    this.UNPAN_GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams[0].KeyName = "GYOUSHAKBN_SHUKKA";
                }
                this.logic.CheckUnpanGyoushaCd();
            }
            catch
            {
                throw;
            }


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);


            // 番号が削除された場合
            if (string.IsNullOrEmpty(NIZUMI_GYOUSHA_CD.Text))
            {
                this.beforNizumiGyoushaCD = string.Empty;
                this.beforNizumiGenbaCD = string.Empty;
                this.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
                this.NIZUMI_GYOUSHA_NAME.ReadOnly = true;
                this.NIZUMI_GYOUSHA_NAME.TabStop = false;
                this.NIZUMI_GENBA_CD.Text = string.Empty;
                this.NIZUMI_GENBA_NAME.Text = string.Empty;
                this.NIZUMI_GENBA_NAME.ReadOnly = true;
                this.NIZUMI_GENBA_NAME.TabStop = false;

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.logic.MoveToNextControlForShokuchikbnCheck(this.NIZUMI_GYOUSHA_CD);

                return;
            }


            // 番号が変更されていない場合、処理しない
            if (this.beforNizumiGyoushaCD == this.NIZUMI_GYOUSHA_CD.Text)
            {
                return;
            }

            try
            {

                // 前回検索荷降業者コードを設定
                this.beforNizumiGyoushaCD = this.NIZUMI_GYOUSHA_CD.Text.PadLeft(6, '0');
                if (!this.logic.CheckNizumiGyoushaCd())
                {
                    this.beforNizumiGyoushaCD = string.Empty;
                    return;
                }

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が変更されていない場合、処理しない
            if (this.beforNizumiGenbaCD == this.NIZUMI_GENBA_CD.Text)
            {
                return;
            }

            try
            {

                // 前回検索荷積現場コードを設定
                this.beforNizumiGenbaCD = this.NIZUMI_GENBA_CD.Text.PadLeft(6, '0');

                if (!this.logic.ChechNizumiGenbaCd())
                {
                    this.beforNizumiGenbaCD = string.Empty;

                    return;
                }

                // 前回検索荷降業者コードを設定
                this.beforNioroshiGyoushaCD = this.NIZUMI_GYOUSHA_CD.Text.PadLeft(6, '0');
                this.NIZUMI_GYOUSHA_CD_OnValidated(sender, e);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷卸業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が削除された場合
            if (string.IsNullOrEmpty(NIOROSHI_GYOUSHA_CD.Text))
            {
                this.beforNioroshiGyoushaCD = string.Empty;
                this.beforNioroshiGenbaCD = string.Empty;
                this.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
                this.NIOROSHI_GYOUSHA_NAME.TabStop = false;
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.ReadOnly = true;
                this.NIOROSHI_GENBA_NAME.TabStop = false;

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.logic.MoveToNextControlForShokuchikbnCheck(this.NIOROSHI_GYOUSHA_CD);

                return;
            }



            // 番号が変更されていない場合、処理しない
            if (this.beforNioroshiGyoushaCD == this.NIOROSHI_GYOUSHA_CD.Text)
            {
                return;
            }

            try
            {
                // 前回検索荷降業者コードを設定
                this.beforNioroshiGyoushaCD = this.NIOROSHI_GYOUSHA_CD.Text.PadLeft(6, '0');

                if (this.logic.CheckNioroshiGyoushaCd())
                {
                    this.beforNioroshiGyoushaCD = string.Empty;
                    return;
                }

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が変更されていない場合、処理しない
            if (this.beforNioroshiGenbaCD == this.NIOROSHI_GENBA_CD.Text)
            {
                return;
            }

            try
            {
                // 前回検索荷降現場コードを設定
                this.beforNioroshiGenbaCD = this.NIOROSHI_GENBA_CD.Text.PadLeft(6, '0');

                if (!this.logic.ChechNioroshiGenbaCd())
                {
                    this.beforNioroshiGenbaCD = string.Empty;
                    this.NIOROSHI_GENBA_CD.Focus();
                    return;
                }

                // 前回検索荷降業者コードを設定
                this.beforNioroshiGyoushaCD = this.NIOROSHI_GYOUSHA_CD.Text.PadLeft(6, '0');
                this.NIOROSHI_GYOUSHA_CD_OnValidated(sender, e);

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UNPAN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が変更されていない場合、処理しない
            if (this.beforUnpanGyoushaCD == this.UNPAN_GYOUSHA_CD.Text)
            {
                return;
            }

            try
            {
                // 前回検索運搬業者コードを設定
                this.beforUnpanGyoushaCD = this.UNPAN_GYOUSHA_CD.Text.PadLeft(6, '0');

                this.logic.CheckUnpanGyoushaCd();

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NYUURYOKU_TANTOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.CheckNyuuryokuTantousha();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void TORIHIKISAKI_CD_OnValidated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が削除された場合
            if (string.IsNullOrEmpty(TORIHIKISAKI_CD.Text))
            {

                this.beforTorihikisakiCD = string.Empty;
                //this.beforGyousaCD = string.Empty;
                //this.beforeGenbaCD = string.Empty;

                this.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
                this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;

                // No.2392-->
                //this.GYOUSHA_CD.Text = string.Empty;
                //this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                //this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                //this.GYOUSHA_NAME_RYAKU.TabStop = false;

                //this.GENBA_CD.Text = string.Empty;
                //this.GENBA_NAME_RYAKU.Text = string.Empty;
                //this.GENBA_NAME_RYAKU.ReadOnly = true;
                //this.GENBA_NAME_RYAKU.TabStop = false;
                // No.2392 <--

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.logic.MoveToNextControlForShokuchikbnCheck(this.TORIHIKISAKI_CD);

                return;
            }

            // 番号が変更されていない場合、処理しない
            if (this.beforTorihikisakiCD == this.TORIHIKISAKI_CD.Text)
            {
                return;
            }

            try
            {
                // 前回検索取引先コードを設定
                this.beforTorihikisakiCD = this.TORIHIKISAKI_CD.Text.PadLeft(6, '0');

                if (!this.logic.CheckTorihikisaki())
                {
                    this.beforTorihikisakiCD = string.Empty;
                    return;
                }

                // 営業担当者の設定
                this.logic.setEigyou_Tantousha(GENBA_CD.Text, GYOUSHA_CD.Text, TORIHIKISAKI_CD.Text);


            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が削除された場合
            if (string.IsNullOrEmpty(GYOUSHA_CD.Text))
            {
                this.beforGyousaCD = string.Empty;
                this.beforeGenbaCD = string.Empty;

                this.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
                this.GYOUSHA_NAME_RYAKU.TabStop = false;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_NAME_RYAKU.ReadOnly = true;
                this.GENBA_NAME_RYAKU.TabStop = false;

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                this.logic.MoveToNextControlForShokuchikbnCheck(this.GYOUSHA_CD);
                return;
            }

            // 番号が変更されていない場合、処理しない
            if (this.beforGyousaCD == this.GYOUSHA_CD.Text)
            {
                return;
            }

            try
            {
                // 現場の初期化
                this.beforeGenbaCD = string.Empty;
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_NAME_RYAKU.Text = string.Empty;
                this.GENBA_NAME_RYAKU.ReadOnly = true;
                this.GENBA_NAME_RYAKU.TabStop = false;

                // 前回検索業者コードを設定
                this.beforGyousaCD = this.GYOUSHA_CD.Text.PadLeft(6, '0');

                if (this.logic.CheckGyousha())
                {
                    this.beforGyousaCD = string.Empty;
                    return;
                }

                // 営業担当者の設定
                this.logic.setEigyou_Tantousha(GENBA_CD.Text, GYOUSHA_CD.Text, TORIHIKISAKI_CD.Text);

            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 番号が変更されていない場合、処理しない
            if (this.beforeGenbaCD == this.GENBA_CD.Text)
            {
                return;
            }

            try
            {
                // 前回検索業者コードを設定
                this.beforeGenbaCD = this.GENBA_CD.Text.PadLeft(6, '0');

                if (!this.logic.CheckGenba())
                {
                    this.beforeGenbaCD = string.Empty;
                    return;
                }

                // 営業担当者の設定
                this.logic.setEigyou_Tantousha(GENBA_CD.Text, GYOUSHA_CD.Text, TORIHIKISAKI_CD.Text);

                // 前回検索業者コードを設定
                this.beforGyousaCD = this.GYOUSHA_CD.Text.PadLeft(6, '0');
                //this.GYOUSHA_CD_Validated(sender, e);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.CheckEigyouTantousha();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ShayouCdSet();   // 比較用車輌CDをセット

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.CheckSharyou();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌名フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_NAME_RYAKU_Enter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                isSelectingSharyouCd = false;
                if (this.SHARYOU_NAME_RYAKU.ReadOnly)
                {
                    var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                    this.SelectNextControl(this.SHARYOU_NAME_RYAKU, !isPressShift, false, false, true);
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeNewWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.KeiryouNumber = -1;
                this.SEQ = "0";

                //base.OnLoad(e);
                if (!this.logic.GetAllEntityData())
                {
                    return;
                }

                if (!this.logic.WindowInit())
                {
                    return;
                }
                if (!this.logic.ButtonInit())
                {
                    return;
                }
                base.HeaderFormInit();
                this.logic.headerForm.KYOTEN_CD.Focus();
            }
            catch
            {
                throw;
            }
            LogUtility.DebugMethodEnd();
        }

        // No.2334-->
        /// <summary>
        /// 新規を指定受入番号に変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeWindow(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (!nowLoding)
            {
                nowLoding = true;
                if (this.KeiryouNumber > 0)
                {
                    this.TairyuuNewFlg = true;

                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.KEIRYOU_NUMBER.Text = this.KeiryouNumber.ToString();

                    //base.OnLoad(e);

                    if (!this.logic.GetAllEntityData())
                    {
                        return;
                    }

                    if (!this.logic.WindowInit())
                    {
                        return;
                    }
                    if (!this.logic.ButtonInit())
                    {
                        return;
                    }
                    base.HeaderFormInit();
                }
                nowLoding = false;
            }

            LogUtility.DebugMethodEnd();
        }
        // No.2334<--

        /// <summary>
        /// 修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeUpdateWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                // 修正モード
                this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;

                // 計量番号検索処理
                this.KEIRYOU_NUMBER_Search(sender, e);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// F4 受付伝票画面遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ForwordUketukeDenpyo(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                r_framework.FormManager.FormManager.OpenForm("G021", this.logic.headerForm.KYOTEN_CD.Text);
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 伝票一覧表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowDenpyouIchiran(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenForm("G055", DENSHU_KBN.KEIRYOU, CommonShogunData.LOGIN_USER_INFO.SHAIN_CD);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 滞留登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void TairyuuRegist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                /// 20141015 chinchisi 「計量入力画面」の休動Checkを追加する　start
                //作業日check
                if (!this.logic.SharyouDateCheck())
                {
                    this.SHARYOU_CD.Focus();
                    return;
                }
                else if (!this.logic.UntenshaDateCheck())
                {
                    this.UNTENSHA_CD.Focus();
                    return;
                }
                else if (!this.logic.HannyuusakiDateCheck())
                {
                    this.NIOROSHI_GENBA_CD.Focus();
                    return;
                }
                /// 20141015 chinchisi 「計量入力画面」の休動Checkを追加する　start
                
                // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                if (!this.logic.SetRequiredSetting(true))
                {
                    return;
                }
                var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                if (!this.logic.RequiredSettingInit())
                {
                    return;
                }
                // 登録処理
                if (!base.RegistErrorFlag)
                {
                    if (!this.logic.CreateEntityAndUpdateTables(true, base.RegistErrorFlag))
                    {
                        return;
                    }

                    // 完了メッセージ表示
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");

                    // 滞留一覧画面を更新
                    FormManager.UpdateForm("G303");

                    //画面を閉じる
                    this.FormClose(null, e);

                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 複写処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Copy(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 複写処理の場合：true
            blnCopy = true;

            // 計量番号、  日連番/年連番、 受付番号をクリアする
            this.KEIRYOU_NUMBER.Text = string.Empty;
            this.RENBAN.Text = string.Empty;
            this.UKETSUKE_NUMBER.Text = string.Empty;

            // 入力担当者
            this.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
            this.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();

            // 登録者情報
            this.logic.headerForm.CreateUser.Text = string.Empty;
            this.logic.headerForm.CreateDate.Text = string.Empty;

            // 更新者情報
            this.logic.headerForm.LastUpdateUser.Text = string.Empty;
            this.logic.headerForm.LastUpdateDate.Text = string.Empty;

            // 日付系初期値設定
            this.DENPYOU_DATE.Value = DateTime.Now;


            // 活性制御
            this.logic.ChangeEnabledForInputControl(false);

            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            if (!this.logic.setHeaderInfo(this.WindowType))
            {
                return;
            }

            // ボタンを初期化
            if (!this.logic.ButtonInit())
            {
                return;
            }

            // 複写処理以外場合：false
            blnCopy = false;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　start
                //作業日check
                if (!this.logic.SharyouDateCheck())
                {
                    this.SHARYOU_CD.Focus();
                    return;
                }
                else if (!this.logic.UntenshaDateCheck())
                {
                    this.UNTENSHA_CD.Focus();
                    return;
                }
                else if (!this.logic.HannyuusakiDateCheck())
                {
                    this.NIOROSHI_GENBA_CD.Focus();
                    return;
                }
                /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　start
                 
                // No.4101-->
                if (this.KIHON_KEIRYOU.Text.Equals(KeiryouConstans.KIHON_KEIRYOU_CD_UKEIRE_STR))
                {   // 入出区分が受入のとき
                    if (!string.IsNullOrEmpty(this.KUUSHA_JYURYO.Text))
                    {   //車輌の空車重量が入ってる場合
                        int last = this.gcMultiRow1.RowCount - 1;
                        if (last > 0)
                        {
                            Row targetRow = this.gcMultiRow1.Rows[last - 1];  // 一番最後は空行
                            if (targetRow != null && (targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value == null
                                || string.IsNullOrEmpty(targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value.ToString())))
                            {   // 最後の行の空車重量が空だった場合、車輌の空車重量を入れる
                                targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value = decimal.Parse(this.KUUSHA_JYURYO.Text);
                                // 重量値計算のためCurrentRowを変更
                                this.gcMultiRow1.ClearSelection();
                                this.gcMultiRow1.AddSelection(last - 1);
                                // 正味重量
                                if (!this.logic.CalcStackOrEmptyJyuuryou())
                                {
                                    return;
                                }
                                // 数量計算
                                this.logic.CalcSuuryou(targetRow);
                            }
                        }
                        else
                        {   // 空行のみの場合
                            Row targetRow = this.gcMultiRow1.Rows[0];
                            if (targetRow != null && targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value == null
                                || string.IsNullOrEmpty(targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))
                            {   // 最後の行の空車重量が空だった場合、車輌の空車重量を入れる
                                targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value = decimal.Parse(this.KUUSHA_JYURYO.Text);
                                // 重量値計算のためCurrentRowを変更
                                this.gcMultiRow1.ClearSelection();
                                this.gcMultiRow1.AddSelection(0);
                                // 正味重量
                                if (!this.logic.CalcStackOrEmptyJyuuryou())
                                {
                                    return;
                                }
                                // 数量計算
                                this.logic.CalcSuuryou(targetRow);
                            }
                        }
                    }
                }
                // No.4101<--

                // 登録前にもう一度計算する
                if (!this.logic.CalcDetail())
                {
                    return;
                }

                this.blnCopyProgress = false;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        //伝票発行ポップアップ表示
                        //if (this.ShowDenpyouHakouPopup())
                        //{
                        // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                        this.logic.SetRequiredSetting(false);
                        var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                        base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                        this.logic.RequiredSettingInit();
                        // Ditailの行数チェックはFWでできないので自前でチェック
                        if (!base.RegistErrorFlag
                            && !this.logic.CheckRequiredDataForDeital())
                        {
                            msgLogic.MessageBoxShow("E001", "明細行");
                            base.RegistErrorFlag = true;
                        }
                        /*　取り下げのため重量整合性チェックは行わないが残しておく */
                        //else
                        //{

                        //    //↑でエラーがない場合
                        //    if (!base.RegistErrorFlag)
                        //    {
                        //        //明細内の重量整合性チェック
                        //        if (!this.CheckJyuuryouSoukan())
                        //        {
                        //            base.RegistErrorFlag = true;
                        //        }
                        //    }
                        //}
                        break;

                    default:
                        break;
                }

                // 登録処理
                if (!base.RegistErrorFlag)
                {
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            if (!this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag))
                            {
                                break;
                            }

                            // 【追加】モード初期表示処理
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.KeiryouNumber = -1;

                            //// 帳票出力
                            //if (this.denpyouHakouPopUpDTO != null
                            //    && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                            //{
                            //    this.logic.Print();
                            //}

                            //伝票発行
                            if (!this.logic.KeiryouhyouHakkou())
                            {
                                return;
                            }

                            // 完了メッセージ表示
                            msgLogic.MessageBoxShow("I001", "登録");

                            // 滞留一覧画面を更新
                            FormManager.UpdateForm("G303");

                            // 画面初期化
                            if (!this.logic.WindowInit())
                            {
                                return;
                            }
                            // フォームロード
                            //this.OnLoad(e);
                            if (!this.PreOnLoad(e))
                            {
                                return;
                            }

                            // 初期フォーカス位置
                            //this.MoveToSharyouCd();

                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            if (!this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag))
                            {
                                return;
                            }

                            //// 帳票出力
                            //if (this.denpyouHakouPopUpDTO != null
                            //    && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                            //{
                            //    this.logic.Print();
                            //}

                            //伝票発行
                            if (!this.logic.KeiryouhyouHakkou())
                            {
                                return;
                            }

                            // 完了メッセージ表示
                            msgLogic.MessageBoxShow("I001", "更新");

                            // 滞留一覧画面を更新
                            FormManager.UpdateForm("G303");

                            // 【追加】モード初期表示処理
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.KeiryouNumber = -1;
                            this.UketukeNumber = -1;

                            // 初期化
                            if (!this.logic.WindowInit())
                            {
                                return;
                            }
                            // フォームロード
                            //this.OnLoad(e);
                            if (!this.PreOnLoad(e))
                            {
                                return;
                            }

                            // 初期フォーカス位置
                            //this.MoveToSharyouCd();

                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            var result = msgLogic.MessageBoxShow("C026");
                            if (result == DialogResult.Yes)
                            {
                                if (!this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag))
                                {
                                    return;
                                }

                                // 【追加】モード初期表示処理
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                this.KeiryouNumber = -1;

                                // 完了メッセージ表示
                                msgLogic.MessageBoxShow("I001", "削除");

                                // 滞留一覧画面を更新
                                FormManager.UpdateForm("G303");

                                // 【追加】モード初期表示処理
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                this.KeiryouNumber = -1;

                                // 初期化
                                if (!this.logic.WindowInit())
                                {
                                    return;
                                }
                                // フォームロード
                                //this.OnLoad(e);
                                if (!this.PreOnLoad(e))
                                {
                                    return;
                                }

                                // 初期フォーカス位置
                                //this.MoveToSharyouCd();
                            }

                            break;

                        default:
                            break;
                    }

                    this.logic.headerForm.windowTypeLabel.Text = "新規";
                    this.logic.headerForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                    this.logic.headerForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    this.logic.headerForm.KYOTEN_CD.Focus();
                }
                else
                {
                    // 必須チェックエラーフォーカス処理
                    this.logic.SetErrorFocus();
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodStart(sender, e);
        }



        /// <summary>
        /// 明細に行を追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AddRow(object sender, EventArgs e)
        {

            LogUtility.DebugMethodStart(sender, e);
            if (editingMultiRowFlag)
            {
                return;
            }
            editingMultiRowFlag = true;
            if ((this.logic.judgeWarihuri()))
            {
                // 割振行の場合はエラー
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E127");
            }
            else
            {
                // 割振行でなければ追加
                if (!this.logic.AddNewRow())
                {
                    return;
                }
                // 合計系計算
                this.logic.CalcTotalValues();
            }
            editingMultiRowFlag = false;
            LogUtility.DebugMethodStart(sender, e);

        }

        /// <summary>
        /// 明細の行を削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RemoveRow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                editingMultiRowFlag = true;
                if (!this.logic.RemoveSelectedRow())
                {
                    return;
                }
                // 合計系計算
                this.logic.CalcTotalValues();
                editingMultiRowFlag = false;
            }
            catch
            {
                throw;
            }
            if (editingMultiRowFlag)
            {
                return;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                this.Close();
                ParentBaseForm.Close();
                if (closeMethod != null)
                {
                    this.closeMethod();
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 計量票発行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KeiryouhyouHakkou(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.KeiryouhyouHakkou();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 手入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Tenyuuryoku(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);
            //try
            //{
            //    var titleControl = (Label)controlUtil.FindControl(ParentBaseForm.headerForm, "lb_title");

            //    // 手入力
            //    if (!blnTenyuuryoku)
            //    {
            //        // フォームタイトル名
            //        ParentBaseForm.Text = this.WindowId.ToTitleString() + KeiryouConstans.MANUAL;
            //        // フォームヘッダ名
            //        titleControl.Text = this.WindowId.ToTitleString() + KeiryouConstans.MANUAL;
            //        this.logic.Tenyuuryoku(false);

            //        blnTenyuuryoku = true;

            //    }
            //    else
            //    {
            //        // フォームタイトル名
            //        ParentBaseForm.Text = this.WindowId.ToTitleString();
            //        // フォームヘッダ名
            //        titleControl.Text = this.WindowId.ToTitleString();
            //        this.logic.Tenyuuryoku(true);
            //        blnTenyuuryoku = false;

            //    }

            //}
            //catch
            //{
            //    throw;
            //}

            //LogUtility.DebugMethodEnd();
        }

        // 明細のイベント

        /// <summary>
        /// 明細の行移動処理
        /// 明細の行が増減するたびに必ず実行してください
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_Leave(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if (editingMultiRowFlag)
                {
                    return;
                }
                editingMultiRowFlag = true;
                // ROW_NOを採番
                this.logic.NumberingRowNo();
                editingMultiRowFlag = false;
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各CELLの更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_OnValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 前回値と変更が無かったら処理中断
            if (beforeValuesForDetail.ContainsKey(e.CellName)
                && beforeValuesForDetail[e.CellName].Equals(
                    Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
            {
                return;
            }


            if (editingMultiRowFlag == false)
            {
                editingMultiRowFlag = true;
                bExecuteCalc = false;
                switch (e.CellName)
                {
                    case LogicClass.CELL_NAME_HINMEI_CD:
                        bool catchErr = true;
                        this.logic.SetDenpyouKbn(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (!this.logic.SearchAndCalcForUnit(true))
                        {
                            return;
                        }
                        bExecuteCalc = true;
                        break;

                    case LogicClass.CELL_NAME_STAK_JYUURYOU:

                        this.logic.EventRun(false);
                        if (!this.logic.ChangeWarihuriAndChouseiInputStatus())
                        {
                            return;
                        }
                        if (!this.logic.CalcYoukiJyuuryou())                         // 容器重量にも影響するので再計算
                        {
                            return;
                        }
                        //this.logic.CalcDetail();
                        this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow);    // 数量再計算
                        // 正味重量計算
                        if (!this.logic.CalcStackOrEmptyJyuuryou())
                        {
                            return;
                        }
                        this.logic.EventRun(true);

                        bExecuteCalc = true;


                        break;

                    case LogicClass.CELL_NAME_EMPTY_JYUURYOU:

                        this.logic.EventRun(false);
                        if (!this.logic.ChangeWarihuriAndChouseiInputStatus())
                        {
                            return;
                        }
                        if (!this.logic.CalcYoukiJyuuryou())                         // 容器重量にも影響するので再計算
                        {
                            return;
                        }
                        //this.logic.CalcDetail();
                        this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow);    // 数量再計算
                        // 正味重量計算
                        if (!this.logic.CalcStackOrEmptyJyuuryou())
                        {
                            return;
                        }

                        this.logic.EventRun(true);
                        bExecuteCalc = true;


                        break;

                    case LogicClass.CELL_NAME_CHOUSEI_JYUURYOU:
                        this.logic.EventRun(false);
                        if (!this.logic.CalcChouseiJyuuryou())
                        {
                            return;
                        }
                        if (!this.logic.ChangeInputStatusForChousei())
                        {
                            return;
                        }
                        this.logic.EventRun(true);

                        bExecuteCalc = true;
                        break;

                    case LogicClass.CELL_NAME_CHOUSEI_PERCENT:
                        this.logic.EventRun(false);
                        if (!this.logic.CalcChouseiPercent())
                        {
                            return;
                        }
                        if (!this.logic.ChangeInputStatusForChousei())
                        {
                            return;
                        }
                        this.logic.EventRun(true);

                        bExecuteCalc = true;
                        break;
                    case LogicClass.CELL_NAME_YOUKI_CD:
                        this.logic.EventRun(false);
                        if (!this.logic.CalcYoukiSuuryou())
                        {
                            return;
                        }
                        this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow);    // 数量再計算
                        this.logic.EventRun(true);
                        break;
                    case LogicClass.CELL_NAME_YOUKI_SUURYOU:
                        this.logic.EventRun(false);
                        if (!this.logic.CalcYoukiSuuryou())
                        {
                            return;
                        }
                        this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow);    // 数量再計算
                        this.logic.EventRun(true);
                        bExecuteCalc = true;
                        break;

                    case LogicClass.CELL_NAME_YOUKI_JYUURYOU:
                        this.logic.EventRun(false);
                        if (!this.logic.CalcYoukiJyuuryou())
                        {
                            return;
                        }
                        this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow);    // 数量再計算
                        if (this.gcMultiRow1.CurrentRow["YOUKI_JYUURYOU"].Value == null)
                        {
                            // 容器重量をクリアした場合は、容器数量も同時にクリアする
                            this.gcMultiRow1.CurrentRow["YOUKI_SUURYOU"].Value = string.Empty;
                        }
                        this.logic.EventRun(true);
                        bExecuteCalc = true;
                        break;

                    case LogicClass.CELL_NAME_WARIFURI_JYUURYOU:
                        this.logic.EventRun(false);
                        if (!this.logic.ExecuteWarifuri(true))
                        {
                            return;
                        }
                        this.logic.EventRun(true);
                        bExecuteCalc = true;
                        break;

                    case LogicClass.CELL_NAME_WARIFURI_PERCENT:
                        this.logic.EventRun(false);
                        if (!this.logic.ExecuteWarifuri(false))
                        {
                            return;
                        }
                        this.logic.EventRun(true);

                        bExecuteCalc = true;
                        break;


                    default:
                        break;
                }



                //再計算対象の項目だった場合、金額の再計算を行う。
                if (bExecuteCalc && !cellValueChanging)
                {
                    // 処理開始(true)
                    cellValueChanging = true;

                    // 高々十数件の明細行を計算するだけなので
                    // 毎回合計系計算を実行する
                    this.logic.CalcTotalValues();

                    // 処理終了(false)
                    cellValueChanging = false;
                }

                editingMultiRowFlag = false;
            }
            LogUtility.DebugMethodStart(sender, e);
        }
        /// <summary>
        /// 各CELLの値検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                switch (e.CellName)
                {
                    case LogicClass.CELL_NAME_STAK_JYUURYOU:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateJyuryouFormat(this.gcMultiRow1.CurrentRow, e.CellName))
                        {
                            e.Cancel = true;
                        }
                        else if (!this.logic.ValidateStackJyuuryou())
                        {
                            e.Cancel = true;
                        }
                        break;
                    case LogicClass.CELL_NAME_EMPTY_JYUURYOU:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateJyuryouFormat(this.gcMultiRow1.CurrentRow, e.CellName))
                        {
                            e.Cancel = true;
                        }
                        else if (!this.logic.ValidateEmpyJyuuryou())
                        {
                            e.Cancel = true;
                        }
                        break;
                    case LogicClass.CELL_NAME_CHOUSEI_JYUURYOU:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateChouseiJyuuryou())
                        {
                            e.Cancel = true;
                        }
                        break;
                    case LogicClass.CELL_NAME_CHOUSEI_PERCENT:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateChouseiPercent())
                        {
                            e.Cancel = true;
                        }
                        break;

                    case LogicClass.CELL_NAME_WARIFURI_JYUURYOU:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateWarifuriJyuuryou())
                        {
                            e.Cancel = true;
                        }
                        break;

                    case LogicClass.CELL_NAME_WARIFURI_PERCENT:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateWarifuriPercent())
                        {
                            e.Cancel = true;
                        }
                        break;
                    case LogicClass.CELL_NAME_HINMEI_CD:
                        // 前回値と変更が無かったら処理中断
                        if (beforeValuesForDetail.ContainsKey(e.CellName)
                            && beforeValuesForDetail[e.CellName].Equals(
                                Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
                        {
                            return;
                        }

                        bool catchErr = true;
                        bool bResult = this.logic.SetDenpyouKbn(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (!bResult)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                            if ((this.logic.CheckHinmeiCd(this.gcMultiRow1.CurrentRow)))    // 品名コードの存在チェック（伝種区分が受入、または共通）
                            {
                                if (!this.logic.SearchAndCalcForUnit(true))
                                {
                                    return;
                                }
                                this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow);    // 数量再計算
                            }
                            // 前回値チェック用データをセット
                            if (beforeValuesForDetail.ContainsKey(e.CellName))
                            {
                                beforeValuesForDetail[e.CellName] = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value);
                            }
                            else
                            {
                                beforeValuesForDetail.Add(e.CellName, Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value));
                            }
                        }
                        break;
                    case LogicClass.CELL_NAME_HINMEI_NAME:
                        if (e.Cancel)
                        {
                            return;
                        }
                        if (!this.logic.ValidateHinmeiName())
                        {
                            e.Cancel = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw;
            }


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_OnCellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Row row = this.gcMultiRow1.CurrentRow;

            if (row == null)
            {
                return;
            }


            //伝票ポップアップキャンセル時もHINMEI_CDに対してENTERイベントが発生していまうため、値が再保存されないように制御する。
            if (!bCancelDenpyoPopup)
            {
                // 前回値チェック用データをセット
                if (beforeValuesForDetail.ContainsKey(e.CellName))
                {
                    beforeValuesForDetail[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                }
                else
                {
                    beforeValuesForDetail.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                }
            }
            else
            {
                bCancelDenpyoPopup = false;
            }

            // 伝票ポップアップキャンセル後、再度伝票ポップアップを表示して入力すると、品名がセットされない事象に対応
            // 品名コードが存在し、かつ品名が空の場合は、再セット
            switch (e.CellName)
            {
                case LogicClass.CELL_NAME_MEISAI_BIKOU:
                    if ((this.logic.CheckHinmeiCd(row)))    // 品名コードの存在チェック（伝種区分が受入、または共通）
                    {
                        bool catchErr = true;
                        // 入力された品名コードが存在するとき
                        if (row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].EditedFormattedValue != null)
                        {
                            if (string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value.ToString()))
                            {
                                // 品名が空の場合再セット
                                row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = this.logic.SearchHinmei(row.Cells["HINMEI_CD"].Value.ToString(), out catchErr);
                                if (!catchErr)
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // 品名が空の場合再セット
                            row.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = this.logic.SearchHinmei(row.Cells["HINMEI_CD"].Value.ToString(), out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
                        }
                    }
                    break;

                // No.2595-->
                case LogicClass.CELL_NAME_HINMEI_CD:
                    GcCustomAlphaNumTextBoxCell target = (GcCustomAlphaNumTextBoxCell)this.gcMultiRow1[e.RowIndex, e.CellName];
                    if (string.IsNullOrWhiteSpace(this.UKETSUKE_NUMBER.Text))
                    {
                        // 伝種区分CD「9」は検索条件に含まれているが、除外の代用
                        target.popupWindowSetting[0].SearchCondition[2].Value = "9";
                    }
                    else
                    {
                        target.popupWindowSetting[0].SearchCondition[2].Value = "3";
                    }
                    break;
                // No.2595<--
            }


            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ROW選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_OnRowEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // マニフェスト連携用のデータを設定
            Row row = this.gcMultiRow1.CurrentRow;
            Cell systemId = row.Cells[LogicClass.CELL_NAME_SYSTEM_ID];
            Cell detailSystemId = row.Cells[LogicClass.CELL_NAME_DETAIL_SYSTEM_ID];
            long renkeisysId = -1;
            long renkeiMeisaiSysid = -1;

            if (systemId.Value != null
                && !string.IsNullOrEmpty(Convert.ToString(systemId.Value)))
            {
                if (long.TryParse(Convert.ToString(systemId.Value), out renkeisysId))
                {
                    this.RenkeiSystemId = renkeisysId;
                }
            }

            if (detailSystemId.Value != null
                && !string.IsNullOrEmpty(Convert.ToString(detailSystemId.Value)))
            {
                if (long.TryParse(Convert.ToString(detailSystemId.Value), out renkeiMeisaiSysid))
                {
                    this.RenkeiMeisaiSystemId = renkeiMeisaiSysid;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion


        /// <summary>
        /// 運転者CDロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUntensha();
        }


        /// <summary>
        /// 取引先CDへフォーカス移動する
        /// 取引先CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToTorihikisakiCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.TORIHIKISAKI_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.TORIHIKISAKI_CD.Focus();
        }

        /// <summary>
        /// 業者CDへフォーカス移動する
        /// 業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGyoushaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.GYOUSHA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// 現場CDへフォーカス移動する
        /// 現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGenbaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.GENBA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.GENBA_CD.Focus();
        }

        /// <summary>
        /// 荷降業者CDへフォーカス移動する
        /// 荷降業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNioroshiGyoushaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.NIOROSHI_GYOUSHA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.NIOROSHI_GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// 荷降現場CDへフォーカス移動する
        /// 荷降現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNioroshiGenbaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.NIOROSHI_GENBA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.NIOROSHI_GENBA_CD.Focus();
        }


        /// <summary>
        /// 荷積業者CDへフォーカス移動する
        /// 荷積業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNizumiGyoushaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.NIZUMI_GYOUSHA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.NIZUMI_GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// 荷積現場CDへフォーカス移動する
        /// 荷積現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNizumiGenbaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.NIZUMI_GENBA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.NIZUMI_GENBA_CD.Focus();
        }

        /// <summary>
        /// 営業担当者CDへフォーカス移動する
        /// 営業担当者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToEigyouTantoushaCd()
        {
            this.EIGYOU_TANTOUSHA_CD.Focus();
        }

        /// <summary>
        /// 入力担当者CDへフォーカス移動する
        /// 入力担当者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNyuuryokuTantoushaCd()
        {
            this.NYUURYOKU_TANTOUSHA_CD.Focus();
        }

        /// <summary>
        /// 車輌CDへフォーカス移動する
        /// 車輌CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToSharyouCd()
        {
            this.SHARYOU_CD.Focus();
        }

        /// <summary>
        /// 車種CDへフォーカス移動する
        /// 車種CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToShashuCd()
        {
            this.SHASHU_CD.Focus();
        }

        /// <summary>
        /// 運搬業者CDへフォーカス移動する
        /// 運搬業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToUnpanGyoushaCd()
        {
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.UNPAN_GYOUSHA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
            this.UNPAN_GYOUSHA_CD.Focus();
        }

        /// <summary>
        /// 運転者CDへフォーカス移動する
        /// 運転者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToUntenshaCd()
        {
            this.UNTENSHA_CD.Focus();
        }

        /// <summary>
        /// 形態区分CDへフォーカス移動する
        /// 形態区分CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToKeitaiKbnCd()
        {
            this.KEITAI_KBN_CD.Focus();
        }

        /// <summary>
        /// コンテナCDへフォーカス移動する
        /// コンテナCDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToContenaJoukyouCd()
        {
            this.CONTENA_SOUSA_CD.Focus();
        }

        /// <summary>
        /// マニ種類CDへフォーカス移動する
        /// マニ種類CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToManiShuruiCd()
        {
            this.MANIFEST_SHURUI_CD.Focus();
        }

        /// <summary>
        /// マニ手配CDへフォーカス移動する
        /// マニ手配CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToManiTehaiCd()
        {
            this.MANIFEST_TEHAI_CD.Focus();
        }
        #region ユーティリティ
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            try
            {
                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm.headerForm));
                allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm));

                return allControl.ToArray();
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// コンストラクタで渡された計量番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistKeiryouData()
        {
            try
            {
                return this.logic.IsExistKeiryouData(this.KeiryouNumber);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// コンストラクタで渡された受付番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistUketsukeData()
        {
            try
            {
                return this.logic.IsExistUketsukeData(this.UketukeNumber);
            }
            catch
            {
                throw;
            }

        }
        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                Type type = sender.GetType();
                if (type.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                    // イベント削除
                    ctrl.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomNumericTextBox2")
                {
                    CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }

                    // イベント削除
                    ctrl.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomPopupOpenButton")
                {
                    CustomPopupOpenButton ctrl = (CustomPopupOpenButton)sender;
                    // テキスト名を取得
                    String textName = this.logic.GetTextName(ctrl.Name);
                    Control control = controlUtil.FindControl(this, textName);

                    if (dicControl.ContainsKey(textName))
                    {
                        dicControl[textName] = control.Text;
                    }
                    else
                    {
                        dicControl.Add(textName, control.Text);
                    }

                    // イベント削除
                    control.Enter -= this.Control_Enter;
                }
                else if (type.Name == "CustomDateTimePicker")
                {
                    CustomDateTimePicker ctrl = (CustomDateTimePicker)sender;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        /// <summary>
        /// 画面遷移するイベントかどうかチェック
        /// 以下の操作の場合に画面遷移すると判断する。
        /// 　・一覧ボタンクリック(F7)
        /// 　・閉じるボタンクリック(F12)
        /// </summary>
        /// <returns></returns>
        private bool isChangeScreenEvent()
        {
            try
            {
                bool returnVal = false;
                if (this.ActiveControl == null
                    && this.ParentBaseForm.ActiveControl != null
                    && -1 < Array.IndexOf(controlNamesForChangeScreenEvents, this.ParentBaseForm.ActiveControl.Name))
                {
                    returnVal = true;
                }

                return returnVal;

            }
            catch
            {
                throw;
            }
        }


        #endregion






        ///// <summary>
        ///// 明細内の1行目空車重量ブランクで登録するかどうか確認する。No2374
        ///// true:OK, false:登録中止
        ///// </summary>
        ///// <returns></returns>
        //private bool CheckRegistContinue_EMPTY_JYUURYOU()
        //{
        //    foreach (Row row in gcMultiRow1.Rows)
        //    {
        //        //１行目は空車重量ブランクがありえる。
        //        if (row.Index == 0)
        //        {
        //            //総重量＝有り AND 空車重量＝無し
        //            if (row[KeiryouConstans.STACK_JYUURYOU].Value != null &&
        //                row[KeiryouConstans.EMPTY_JYUURYOU].Value == null )
        //            {
        //                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
        //                var result = msgLogic.MessageBoxShowConfirm("空車重量が入っていない明細行がありますが、登録を進めますか？");
        //                if (result == System.Windows.Forms.DialogResult.Yes)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    row[KeiryouConstans.EMPTY_JYUURYOU].Style.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}


        /// <summary>
        /// 明細内の重量整合性チェック
        /// 総重量と空重量の各行間の整合性をチェック
        /// true:OK, false:エラー
        /// </summary>
        /// <returns></returns>
        private bool CheckJyuuryouSoukan()
        {

            string before_stack = "";
            string before_empty = "";

            foreach (Row row in gcMultiRow1.Rows)
            {
                //一行目はチェックしない。
                if (row.Index > 0)
                {
                    //割り振り行,最後の行は無視。
                    if (row[KeiryouConstans.STACK_JYUURYOU].Value != null &&
                        row[KeiryouConstans.EMPTY_JYUURYOU].Value != null)
                    {
                        // 一行上の行の総重量もしくは空重量が空じゃない場合
                        if (!string.IsNullOrEmpty(before_stack) ||
                            !string.IsNullOrEmpty(before_empty))
                        {
                            if (this.KIHON_KEIRYOU.Text == "1")
                            {
                                //受入　（空重量と次の行の総重量が一致しないとエラー）
                                if (before_empty != row[KeiryouConstans.STACK_JYUURYOU].Value.ToString())
                                {
                                    //エラー
                                    row[KeiryouConstans.STACK_JYUURYOU].Style.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShowError("総重量と１つ前の空車重量が不整合です。");
                                    return false;
                                }
                            }
                            else
                            {
                                //出荷  (総重量と次の行の空重量が一致しないとエラー)
                                if (before_stack != row[KeiryouConstans.EMPTY_JYUURYOU].Value.ToString())
                                {
                                    //エラー
                                    row[KeiryouConstans.EMPTY_JYUURYOU].Style.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShowError("１つ前の総重量と空車重量が不整合です。");
                                    return false;
                                }
                            }
                        }
                    }
                }

                // 総重量：前行の値を格納
                // 割り振り行,最後の行は無視。
                if (row[KeiryouConstans.STACK_JYUURYOU].Value != null)
                {
                    before_stack = row[KeiryouConstans.STACK_JYUURYOU].Value.ToString();
                }
                else
                {
                    before_stack = string.Empty;
                }

                // 空車重量：前行の値を格納
                // 割り振り行,最後の行は無視。
                if (row[KeiryouConstans.EMPTY_JYUURYOU].Value != null)
                {
                    before_empty = row[KeiryouConstans.EMPTY_JYUURYOU].Value.ToString();
                }
                else
                {
                    before_empty = string.Empty;
                }
            }
            return true;
        }

        // No.4101-->
        private void KUUSHA_JYURYO_TextChanged(object sender, EventArgs e)
        {
            if (this.KIHON_KEIRYOU.Text.Equals(KeiryouConstans.KIHON_KEIRYOU_CD_SHUKKA_STR))
            {   // 入出区分が出荷のとき
                if (!string.IsNullOrEmpty(this.KUUSHA_JYURYO.Text))
                {   //車輌の空車重量が入ってる場合
                    Row targetRow = this.gcMultiRow1.Rows[0];
                    if (targetRow != null && (targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value == null
                        || string.IsNullOrEmpty(targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value.ToString())))
                    {   // 最後の行の空車重量が空だった場合、車輌の空車重量を入れる
                        targetRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value = decimal.Parse(this.KUUSHA_JYURYO.Text);
                        // 重量値計算のためCurrentRowを変更
                        this.gcMultiRow1.ClearSelection();
                        this.gcMultiRow1.AddSelection(0);
                        // 正味重量、金額計算
                        if (!this.logic.CalcStackOrEmptyJyuuryou())
                        {
                            return;
                        }
                        // 数量計算
                        this.logic.CalcSuuryou(targetRow);
                    }
                }
            }
        }
        // No.4101<--

        // No.2595-->
        private void KEITAI_KBN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.CheckKeitaiKbn();
        }
        // No.2595<--

        private void UIForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            this.logic.SetMoveData();
        }

        // No.3822-->
        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (ActiveControl != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;
                    if (ActiveControl.Name.Equals("GYOUSHA_CD") || ActiveControl.Name.Equals("TORIHIKISAKI_CD"))
                    {   // とりあえずヘッダーにShift+Enterでヘッダーに移動しないようにする(Shift+TABではここに来ない)
                        return;
                    }
                    else if (forward == false && ActiveControl.Name.Equals("NIZUMI_GYOUSHA_CD") || ActiveControl.Name.Equals("NIOROSHI_GYOUSHA_CD"))
                    {
                        return;
                    }
                    this.SelectNextControl(ActiveControl, !forward, true, true, true);  // Activeが変更になっているため前の位置に戻す
                    this.logic.GotoNextControl(forward);
                }
            }
        }
        // No.3822<--

        /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　start
        #region 荷降現場Validating
        /// <summary>
        /// 荷降現場Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.HannyuusakiDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 車輌更新Validating
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.SharyouDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 運転者Validating
        /// <summary>
        /// 運転者Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.logic.UntenshaDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion
        /// 20141013 chinchisi 「計量入力画面」の休動Checkを追加する　end

        private bool PreOnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            try
            {

                // 複写処理判定
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && this.KeiryouNumber >= 0)
                {
                    blnCopyProgress = true;

                }
                else
                {
                    blnCopyProgress = false;
                }

                //受付番号と計量番号判定
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && this.UketukeNumber >= 0)
                {
                    blnUketsukeProgress = true;
                }
                else
                {
                    blnUketsukeProgress = false;
                }

                //base.OnLoad(e);
                bool isOpenFormError = this.logic.GetAllEntityData();
                if (!isOpenFormError)
                {
                    LogUtility.DebugMethodStart(false);
                    return false;
                }
                ParentBaseForm = (BusinessBaseForm)this.Parent;

                //重量コントロール起動
                truckScaleWeight1.ProcessWeight();

                if (!this.logic.WindowInit())
                {
                    LogUtility.DebugMethodStart(false);
                    return false;
                }
                if (!this.logic.ButtonInit())
                {
                    LogUtility.DebugMethodStart(false);
                    return false;
                }
                if (!this.logic.EventInit())
                {
                    LogUtility.DebugMethodStart(false);
                    return false;
                }

                if (!isOpenFormError)
                {
                    this.FormClose(null, e);
                }


                //受付番号と計量番号判定
                if (this.KeiryouNumber != -1)
                {
                    this.KEIRYOU_NUMBER.Text = this.KeiryouNumber.ToString();
                    //base.OnLoad(e);
                    if (!this.logic.GetAllEntityData())
                    {
                        LogUtility.DebugMethodStart(false);
                        return false;
                    }

                    if (!this.logic.WindowInit())
                    {
                        LogUtility.DebugMethodStart(false);
                        return false;
                    }

                    // 複写の場合
                    if (blnCopyProgress)
                    {
                        if (!TairyuuNewFlg) // No.2334
                        {
                            // 計量番号、  日連番/年連番、 受付番号をクリアする
                            this.KEIRYOU_NUMBER.Text = string.Empty;
                            this.RENBAN.Text = string.Empty;
                            this.UKETSUKE_NUMBER.Text = string.Empty;
                        }

                        // 入力担当者
                        this.NYUURYOKU_TANTOUSHA_CD.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_CD.ToString();
                        this.NYUURYOKU_TANTOUSHA_NAME.Text = CommonShogunData.LOGIN_USER_INFO.SHAIN_NAME_RYAKU.ToString();

                        // 登録者情報
                        this.logic.headerForm.CreateUser.Text = string.Empty;
                        this.logic.headerForm.CreateDate.Text = string.Empty;

                        // 更新者情報
                        this.logic.headerForm.LastUpdateUser.Text = string.Empty;
                        this.logic.headerForm.LastUpdateDate.Text = string.Empty;

                        // 日付系初期値設定
                        this.DENPYOU_DATE.Value = DateTime.Now;


                        this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                        this.logic.setHeaderInfo(this.WindowType);
                    }


                    if (!this.logic.ButtonInit())
                    {
                        LogUtility.DebugMethodStart(false);
                        return false;
                    }
                    this.DENPYOU_DATE.Focus();  // 初期フォーカス位置
                }
                else if (this.UketukeNumber != -1)
                {

                    if (!this.uketsukeNumberNowLoding)
                    {
                        this.uketsukeNumberNowLoding = true;

                        //base.OnLoad(e);
                        if (!this.logic.GetAllEntityData())
                        {
                            this.uketsukeNumberNowLoding = false;
                            LogUtility.DebugMethodStart(false);
                            return false;
                        }
                        if (!this.logic.setUketsukeEntity(this.UketukeNumber))
                        {
                            this.uketsukeNumberNowLoding = false;
                            this.UKETSUKE_NUMBER.Focus();
                            LogUtility.DebugMethodStart(false);
                            return false;
                        }

                        if (!this.logic.WindowInit())
                        {
                            LogUtility.DebugMethodStart(false);
                            return false;
                        }
                        if (!this.logic.ButtonInit())
                        {
                            LogUtility.DebugMethodStart(false);
                            return false;
                        }
                        // 初期フォーカス位置  
                        this.UKETSUKE_NUMBER.Text = this.UketukeNumber.ToString();
                        this.beforUketukeNumber = this.UketukeNumber.ToString();
                        this.UKETSUKE_NUMBER.Focus();

                        this.uketsukeNumberNowLoding = false;
                    }

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("PreOnLoad", ex);
                this.logic.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodStart(false);
                return false;
            }
            LogUtility.DebugMethodStart(true);
            return true;

        }

        /// <summary>
        /// 荷済業者POPUP_BEF
        /// </summary>
        public void SetNizumiGyoushaBef()
        {
            this.beforNizumiGyoushaCD = this.NIZUMI_GYOUSHA_CD.Text;
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.NIZUMI_GYOUSHA_PopBefore();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
        }

        /// <summary>
        /// 荷済業者POPUP_AFT
        /// </summary>
        public void SetNizumiGyoushaAft()
        {
            if (this.beforNizumiGyoushaCD != this.NIZUMI_GYOUSHA_CD.Text)
            {
                this.NIZUMI_GENBA_CD.Text = string.Empty;
                this.NIZUMI_GENBA_NAME.Text = string.Empty;
            }
            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.NIZUMI_GYOUSHA_PopAfter();
            // 20151021 katen #13337 品名手入力に関する機能修正 end
        }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal string torihikisakiPopBefore;
        public void TORIHIKISAKI_PopBefore()
        {
            this.torihikisakiPopBefore = this.TORIHIKISAKI_CD.Text;
            this.TORIHIKISAKI_CD.Text = string.Empty;
        }
        public void TORIHIKISAKI_PopAfter()
        {
            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                this.TORIHIKISAKI_CD.Text = this.torihikisakiPopBefore;
                return;
            }
            this.logic.torihikisaki_Pop();
        }
        internal string gyoushaPopBefore;
        public void GYOUSHA_PopBefore()
        {
            this.gyoushaPopBefore = this.GYOUSHA_CD.Text;
            this.GYOUSHA_CD.Text = string.Empty;
        }
        public void GYOUSHA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_CD.Text = this.gyoushaPopBefore;
                return;
            }
            this.logic.gyousha_Pop();
        }
        internal string genbaPopBefore;
        public void GENBA_PopBefore()
        {
            this.genbaPopBefore = this.GENBA_CD.Text;
            this.GENBA_CD.Text = string.Empty;
        }
        public void GENBA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_CD.Text = this.genbaPopBefore;
                return;
            }
            this.logic.genba_Pop();
        }
        internal string upnGyoushaPopBefore;
        public void UNPAN_GYOUSHA_PopBefore()
        {
            this.upnGyoushaPopBefore = this.UNPAN_GYOUSHA_CD.Text;
            this.UNPAN_GYOUSHA_CD.Text = string.Empty;
        }
        public void UNPAN_GYOUSHA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                this.UNPAN_GYOUSHA_CD.Text = this.upnGyoushaPopBefore;
                return;
            }
            this.logic.upnGyousha_Pop();
        }
        internal string nioroshiGyoushaPopBefore;
        public void NIOROSHI_GYOUSHA_PopBefore()
        {
            this.nioroshiGyoushaPopBefore = this.NIOROSHI_GYOUSHA_CD.Text;
            this.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
        }
        public void NIOROSHI_GYOUSHA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                this.NIOROSHI_GYOUSHA_CD.Text = this.nioroshiGyoushaPopBefore;
                return;
            }
            this.logic.nioroshiGyousha_Pop();
        }
        internal string nioroshiGenbaPopBefore;
        public void NIOROSHI_GENBA_PopBefore()
        {
            this.nioroshiGenbaPopBefore = this.NIOROSHI_GENBA_CD.Text;
            this.NIOROSHI_GENBA_CD.Text = string.Empty;
        }
        public void NIOROSHI_GENBA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                this.NIOROSHI_GENBA_CD.Text = this.nioroshiGenbaPopBefore;
                return;
            }
            this.logic.nioroshiGenba_Pop();
        }
        internal string nizumiGyoushaPopBefore;
        public void NIZUMI_GYOUSHA_PopBefore()
        {
            this.nizumiGyoushaPopBefore = this.NIZUMI_GYOUSHA_CD.Text;
            this.NIZUMI_GYOUSHA_CD.Text = string.Empty;
        }
        public void NIZUMI_GYOUSHA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.NIZUMI_GYOUSHA_CD.Text))
            {
                this.NIZUMI_GYOUSHA_CD.Text = this.nizumiGyoushaPopBefore;
                return;
            }
            this.logic.nizumiGyousha_Pop();
        }
        internal string nizumiGenbaPopBefore;
        public void NIZUMI_GENBA_PopBefore()
        {
            this.nizumiGenbaPopBefore = this.NIZUMI_GENBA_CD.Text;
            this.NIZUMI_GENBA_CD.Text = string.Empty;
        }
        public void NIZUMI_GENBA_PopAfter()
        {
            if (string.IsNullOrEmpty(this.NIZUMI_GENBA_CD.Text))
            {
                this.NIZUMI_GENBA_CD.Text = this.nizumiGenbaPopBefore;
                return;
            }
            this.logic.nizumiGenba_Pop();
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end
    }
}

