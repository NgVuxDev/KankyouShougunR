using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Core.Scale.Keiryou.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;

namespace Shougun.Core.Scale.KeiryouNyuuryoku
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
        public long RenkeiJissekiSeq { get; private set; }
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
        /// Close処理の後に実行するメソッド
        /// 制約：戻り値なし、引数なし、Publicなメソッド
        /// </summary>
        public delegate void LastRunMethod();

        /// <summary>
        /// Close処理の後に実行するメソッド
        /// </summary>
        public LastRunMethod closeMethod;

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
        private bool nowLoding = false;

        /// <summary>
        /// 明細のValueChangedイベントが複数回発行されないための判定変数
        /// </summary>
        private Dictionary<string, bool> controledListForDetailValueChanged = new Dictionary<string, bool>();

        /// <summary>
        /// 画面遷移が発生するコントロール名一覧
        /// </summary>
        private string[] controlNamesForChangeScreenEvents = new string[] { "bt_func7", "bt_func12" };

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 前回値チェック用変数(Jisseki用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForJisseki = new Dictionary<string, string>();

        /// <summary>
        /// 伝票発行ポップアップ用DTO
        /// </summary>
        internal ParameterDTOClass denpyouHakouPopUpDTO = new ParameterDTOClass();

        /// <summary>
        /// この画面が呼び出された時、受付番号が引数に存在したかを示します
        /// /// </summary>
        internal bool isArgumentUketsukeNumber = false;

        /// <summary>
        /// 継続計量フラグ
        /// </summary>
        internal bool KeizokuKeiryouFlg = false;

        /// <summary>
        /// 滞留新規フラグ
        /// </summary>
        internal bool TairyuuNewFlg = false;

        /// <summary>
        /// 車輌CDが編集中かどうかのフラグ
        /// </summary>
        private bool editingSharyouCdFlag = false;

        /// <summary>
        /// KeyDownイベントで押されたキーを保存します
        /// </summary>
        internal KeyEventArgs keyEventArgs;

        /// <summary>
        /// クリックのフォーカス移動か判断するフラグ
        /// </summary>
        internal bool isNotMoveFocus;

        /// <summary>
        /// CDを保存するかどうかのフラグ
        /// </summary>
        private bool isSaveCd = false;

        /// <summary>
        /// 諸口区分の前回値を保持する
        /// </summary>
        internal bool oldShokuchiKbn;

        /// <summary>
        /// 諸口区分(車輌名用)の前回値を保持する
        /// </summary>
        internal bool oldSharyouShokuchiKbn = false;

        /// <summary>
        /// 検索ボタンから入力されたか判断するフラグ
        /// </summary>
        internal bool isFromSearchButton;

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
        /// セルの編集状態の設定やコミットをやるか判断するフラグ
        /// </summary>
        internal bool notEditingOperationFlg = false;

        /// <summary>
        /// 前回フォーカスのあったコントロール名を保持します
        /// </summary>
        internal string beforControlName = string.Empty;

        /// <summary>
        /// 前々回フォーカスのあったコントロール名を保持します
        /// </summary>
        internal string beforbeforControlName = string.Empty;

        /// <summary>
        /// 諸口区分によるフォーカス移動用
        /// 諸口区分設定によってフォーカスを設定した場合に入力項目設定によるフォーカス移動処理を行いたくない場合にTrueを設定
        /// 入力項目設定によるフォーカス移動処理時にTrueだった場合にFalseにし、処理を中断させている
        /// </summary>
        internal bool isSetShokuchiForcus = false;

        /// <summary>
        /// 品名を再読み込みしたかのフラグ
        /// Detailの金額計算で使用する。
        /// </summary>
        internal bool isHinmeiReLoad = false;

        /// <summary>
        /// 計量区分テーブル
        /// </summary>
        private DataTable keiryouKbnTable;

        /// <summary>
        /// 取引区分テーブル
        /// </summary>
        private DataTable torihikiKbnTable;

        /// <summary>
        /// 税計算区分（請求）テーブル
        /// </summary>
        private DataTable seikyuuZeiKeisanKbnTable;

        /// <summary>
        /// 税計算区分（支払）テーブル
        /// </summary>
        private DataTable shiharaiZeiKeisanKbnTable;

        /// <summary>
        /// 税区分（請求）テーブル
        /// </summary>
        private DataTable seikyuuZeiKbnTable;

        /// <summary>
        /// 税区分（支払）テーブル
        /// </summary>
        private DataTable shiharaiZeiKbnTable;

        /// <summary>
        /// マニフェスト区分テーブル
        /// </summary>
        private DataTable manifestKbnTable;

        /// <summary>
        /// マニ種類テーブル
        /// </summary>
        private DataTable manifesthaikiKbnTable;

        /// <summary>
        /// 伝種区分
        /// </summary>
        internal DENSHU_KBN selectDenshuKbnCd = DENSHU_KBN.UKEIRE;

        #endregion

        public bool IsDataLoading { get; set; }

        internal bool validateFlag = false;

        internal bool isInputError = false;

        internal bool isCopy = false;

        internal bool ismobile_mode = false;

        internal bool isfile_upload = false;

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
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="windowType">モード</param>
        /// <param name="keiryouNumber">計量入力 KEIRYOU_NUMBER</param>
        /// <param name="lastRunMethod">計量入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, long keiryouNumber = -1, LastRunMethod lastRunMethod = null, bool keizokuKeiryouFlg = false, bool newChangeFlg = false, string SEQ = "0")
            : base(WINDOW_ID.T_KEIRYOU_NYUURYOKU, windowType)
        {
            LogUtility.DebugMethodStart(windowId, windowType, keiryouNumber, lastRunMethod, keizokuKeiryouFlg, newChangeFlg, SEQ);

            CommonShogunData.Create(SystemProperty.Shain.CD);

            TairyuuNewFlg = newChangeFlg;

            this.InitializeComponent();

            // 時間コンボボックスのItemsをセット
            this.SAGYOU_HOUR.SetItems();
            this.SAGYOU_MINUTE.SetItems(1);

            this.WindowId = windowId;
            this.WindowType = windowType;
            this.KeiryouNumber = keiryouNumber;
            this.closeMethod = lastRunMethod;
            this.KeizokuKeiryouFlg = keizokuKeiryouFlg;
            if (string.IsNullOrEmpty(SEQ))
            {
                SEQ = "0";
            }
            this.SEQ = SEQ;
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG && this.KeiryouNumber != -1)
            {
                this.isCopy = true;
            }

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // マニフェスト連携用変数の初期化
            RenkeiDenshuKbnCd = (short)SalesPaymentConstans.DENSHU_KBN_CD_KEIRYOU;
            RenkeiSystemId = -1;
            RenkeiMeisaiSystemId = -1;
            RenkeiJissekiSeq = -1;

            LogUtility.DebugMethodEnd(windowType, windowType, keiryouNumber, lastRunMethod, keizokuKeiryouFlg, newChangeFlg, SEQ);
        }

        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(windowId, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowId, windowType, torihikisakiCd, gyousyaCd, genbaCd);

                //データ移動用
                this.moveData_flg = true;
                this.moveData_torihikisakiCd = torihikisakiCd;
                this.moveData_gyousyaCd = gyousyaCd;
                this.moveData_genbaCd = genbaCd;
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
            base.OnLoad(e);
            bool catchErr = true;
            bool retDate = this.logic.GetAllEntityData(out catchErr);
            if (!catchErr)
            {
                return;
            }
            bool isOpenFormError = retDate;
            ParentBaseForm = (BusinessBaseForm)this.Parent;

            //重量値表示プロセス起動
            truckScaleWeight1.ProcessWeight();

            // 計量区分テーブル設定
            catchErr = this.SetKeiryouKbnTable();
            if (catchErr)
            {
                return;
            }

            // 取引区分テーブル設定
            catchErr = this.SetTorihikiKbnTable();
            if (catchErr)
            {
                return;
            }

            // 税計算区分（請求）テーブル設定
            catchErr = this.SetSeikyuuZeiKeisanKbnTable();
            if (catchErr)
            {
                return;
            }

            // 税計算区分（支払）テーブル設定
            catchErr = this.SetShiharaiZeiKeisanKbnTable();
            if (catchErr)
            {
                return;
            }

            // 税区分（請求）テーブル設定
            catchErr = this.SetSeikyuuZeiKbnTable();
            if (catchErr)
            {
                return;
            }

            // 税区分（支払）テーブル設定
            catchErr = this.SetShiharaiZeiKbnTable();
            if (catchErr)
            {
                return;
            }

            // マニフェスト区分テーブル設定
            catchErr = this.SetManifestKbnTable();
            if (catchErr)
            {
                return;
            }

            // マニ種類テーブル設定
            catchErr = this.SetManifestHaikikbnTable();
            if (catchErr)
            {
                return;
            }

            long tempKeiryouNumber = this.KeiryouNumber;

            if (!this.logic.dto.sysInfoEntity.KEIRYOU_KIHON_KEIRYOU.IsNull)
            {
                if (this.logic.dto.sysInfoEntity.KEIRYOU_KIHON_KEIRYOU.Value == 1)
                {
                    this.selectDenshuKbnCd = DENSHU_KBN.UKEIRE;
                }
                else
                {
                    this.selectDenshuKbnCd = DENSHU_KBN.SHUKKA;
                }
            }

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

            // スクロールバーが下がる場合があるため、
            // 強制的にバーを先頭にする
            this.AutoScrollPosition = new Point(0, 0); 

            this.KeiryouNumber = tempKeiryouNumber;

            if (!this.logic.GetTairyuuData())
            {
                return;
            }

            // 継続入力の初期値を設定
            // システム設定値がない場合は、「2:しない」を初期値とする
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            var keizokuNyuuryoku = userProfile.Settings.DefaultValue.Where(v => v.Name == "継続入力").Select(v => v.Value).DefaultIfEmpty("2").FirstOrDefault();
            this.logic.headerForm.KEIZOKU_NYUURYOKU_VALUE.Text = keizokuNyuuryoku;

            if (!this.logic.dto.sysInfoEntity.KEIRYOU_HYOU_PRINT_KBN.IsNull
                && this.logic.dto.sysInfoEntity.KEIRYOU_HYOU_PRINT_KBN.Value == 1)
            {
                this.logic.headerForm.PRINT_KBN_VALUE.Text = "1";
            }
            else
            {
                this.logic.headerForm.PRINT_KBN_VALUE.Text = "2";
            }
            this.ParentBaseForm.Controls.Add(this.tairyuuIchiran);
            this.ParentBaseForm.inForm.SendToBack();

            if (!isOpenFormError)
            {
                base.CloseTopForm();
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.DETAIL_TAB != null)
            {
                this.DETAIL_TAB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.gcMultiRow1 != null)
            {
                this.gcMultiRow1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            //明細の横に項目を追加するので、右端の設定は切る
            if (this.gcMultiRow2 != null)
            {
                this.gcMultiRow2.Anchor = AnchorStyles.Left | AnchorStyles.None | AnchorStyles.Top | AnchorStyles.Bottom;
            }

            if (this.JISSEKI_TAB != null)
            {
                this.JISSEKI_TAB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.dgvTenpuFileDetail != null)
            {
                this.dgvTenpuFileDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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
            //this.logic.SetTopControlFocus();
            this.SHARYOU_CD.Focus();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }
        #endregion

        #region イベント
        /// <summary>
        /// 重量値、金額値用フォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NumberFormat(object sender, EventArgs e)
        {
            if (!this.logic.ToAmountValue(sender))
            {
                return;
            }
        }

        /// <summary>
        /// 計量番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ENTRY_NUMBER_Validated(object sender, EventArgs e)
        {
            if (this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                return;
            }

            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            if (!nowLoding)
            {
                nowLoding = true;
                long keiryouNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text)
                    && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out keiryouNumber))
                {
                    if (this.KeiryouNumber != keiryouNumber)
                    {
                        this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        this.KeiryouNumber = keiryouNumber;

                        bool catchErr = true;
                        bool retDate = this.logic.GetAllEntityData(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (!retDate)
                        {
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.KeiryouNumber = -1;

                            // 再描画を有効にして最新の状態に更新
                            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));

                            this.ENTRY_NUMBER.Focus();
                            nowLoding = false;
                            return;
                        }

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                // 修正権限は無いが参照権限がある場合は参照モードで起動
                                this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                HeaderFormInit();
                            }
                            else
                            {
                                // どちらも無い場合はアラートを表示して処理中断
                                MessageBoxShowLogic msg = new MessageBoxShowLogic();
                                msg.MessageBoxShow("E158", "修正");
                                this.WindowType = tmpType;
                                HeaderFormInit();
                                this.ENTRY_NUMBER.Focus();
                                nowLoding = false;
                                return;
                            }
                        }

                        if (!this.logic.WindowInit())
                        {
                            return;
                        }

                        if (!this.logic.ButtonInit())
                        {
                            return;
                        }

                        this.execEntryNumberEvent = true;

                        // スクロールバーが下がる場合があるため、
                        // 強制的にバーを先頭にする
                        this.AutoScrollPosition = new Point(0, 0);
                    }
                }
                nowLoding = false;
            }

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            // 初期フォーカス制御については、OnKeyPressEventとの関係で制御が難しいため、OnKeyPressEvent側で制御する。
        }

        /// <summary>
        /// 売上消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_SHOUHIZEI_RATE_VALUE_TextChanged(object sender, EventArgs e)
        {
            bool catchErr = true;
            string retString = this.logic.ToPercentForUriageShouhizeiRate(out catchErr);
            if (!catchErr)
            {
                return;
            }

            this.URIAGE_SHOUHIZEI_RATE_VALUE.Text = retString;
        }

        /// <summary>
        /// フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Enter(object sender, EventArgs e)
        {
            this.logic.beforeCd = ((Control)sender).Text;
        }

        /// <summary>
        /// 売上消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHOUHIZEI_RATE_VALUE_TextChanged(object sender, EventArgs e)
        {
            bool catchErr = true;
            string retString = this.logic.ToPercentForShiharaiShouhizeiRate(out catchErr);
            if (!catchErr)
            {
                return;
            }
            this.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = retString;
        }

        /// <summary>
        /// 排出事業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void HST_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.HST_GYOUSHA_CD_Validated();
        }
        public void HstGyoushaCdPopupBefore()
        {
            this.logic.popupCd = this.HST_GYOUSHA_CD.Text;
        }
        public void HstGyoushaCdPopupAfter()
        {
            if (this.logic.popupCd != this.HST_GYOUSHA_CD.Text)
            {
                this.logic.HST_GYOUSHA_CD_Validated();
            }
        }

        /// <summary>
        /// 排出事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void HST_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.HST_GENBA_CD_Validated();
        }
        public void HstGenbaCdPopupBefore()
        {
            this.logic.popupCd = this.HST_GENBA_CD.Text;
        }
        public void HstGenbaCdPopupAfter()
        {
            if (this.logic.popupCd != this.HST_GENBA_CD.Text)
            {
                this.logic.HST_GENBA_CD_Validated();
            }
        }

        /// <summary>
        /// 処分業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SBN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.SBN_GYOUSHA_CD_Validated();
        }
        public void SbnGyoushaCdPopupBefore()
        {
            this.logic.popupCd = this.SBN_GYOUSHA_CD.Text;
        }
        public void SbnGyoushaCdPopupAfter()
        {
            if (this.logic.popupCd != this.SBN_GYOUSHA_CD.Text)
            {
                this.logic.SBN_GYOUSHA_CD_Validated();
            }
        }

        /// <summary>
        /// 処分事業場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SBN_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.SBN_GENBA_CD_Validated();
        }
        public void SbnGenbaCdPopupBefore()
        {
            this.logic.popupCd = this.SBN_GENBA_CD.Text;
        }
        public void SbnGenbaCdPopupAfter()
        {
            if (this.logic.popupCd != this.SBN_GENBA_CD.Text)
            {
                this.logic.SBN_GENBA_CD_Validated();
            }
        }

        /// <summary>
        /// 最終処分業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LAST_SBN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.LAST_SBN_GYOUSHA_CD_Validated();
        }
        public void LastSbnGyoushaCdPopupBefore()
        {
            this.logic.popupCd = this.LAST_SBN_GYOUSHA_CD.Text;
        }
        public void LastSbnGyoushaCdPopupAfter()
        {
            if (this.logic.popupCd != this.LAST_SBN_GYOUSHA_CD.Text)
            {
                this.logic.LAST_SBN_GYOUSHA_CD_Validated();
            }
        }

        /// <summary>
        /// 最終処分場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LAST_SBN_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.LAST_SBN_GENBA_CD_Validated();
        }
        public void LastSbnGenbaCdPopupBefore()
        {
            this.logic.popupCd = this.LAST_SBN_GENBA_CD.Text;
        }
        public void LastSbnGenbaCdPopupAfter()
        {
            if (this.logic.popupCd != this.LAST_SBN_GENBA_CD.Text)
            {
                this.logic.LAST_SBN_GENBA_CD_Validated();
            }
        }

        /// <summary>
        /// 荷積業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 荷積業者を取得
            bool catchErr = true;
            var retData = this.logic.accessor.GetGyousha(this.NIZUMI_GYOUSHA_CD.Text, this.DENPYOU_DATE.Text, this.logic.footerForm.sysDate.Date, out catchErr);
            if (!catchErr)
            {
                return;
            }
            var nizumiGyousha = retData;
            if (nizumiGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nizumiGyousha.SHOKUCHI_KBN;
            }

            this.logic.NizumiGyoushaCdSet();  //比較用業者CDをセット
        }

        /// <summary>
        /// 荷積業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.logic.IsSuuryouKesannFlg = true;

            this.isNotMoveFocus = this.SetNizumiGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 荷積現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 荷積現場を取得
            bool catchErr = true;
            var retData = this.logic.accessor.GetGenba(this.NIZUMI_GYOUSHA_CD.Text, this.NIZUMI_GENBA_CD.Text, this.DENPYOU_DATE.Text, this.logic.footerForm.sysDate.Date, out catchErr);
            if (!catchErr)
            {
                return;
            }
            var nizumiGenba = retData;
            if (nizumiGenba != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nizumiGenba.SHOKUCHI_KBN;
            }

            this.logic.NizumiGenbaCdSet();   // 比較用現場CDをセット
        }

        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.logic.IsSuuryouKesannFlg = true;

            this.isNotMoveFocus = this.SetNizumiGenba();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }
        
        /// <summary>
        /// 荷降業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 荷降業者を取得
            bool catchErr = true;
            var retData = this.logic.accessor.GetGyousha(this.NIOROSHI_GYOUSHA_CD.Text, this.DENPYOU_DATE.Text, this.logic.footerForm.sysDate.Date, out catchErr);
            if (!catchErr)
            {
                return;
            }
            var nioroshiGyousha = retData;
            if (nioroshiGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGyousha.SHOKUCHI_KBN;
            }

            this.logic.NioroshiGyoushaCdSet();  //比較用業者CDをセット
        }

        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.logic.IsSuuryouKesannFlg = true;

            this.isNotMoveFocus = this.SetNioroshiGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 荷降現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 荷降現場を取得
            bool catchErr = true;
            var retData = this.logic.accessor.GetGenba(this.NIOROSHI_GYOUSHA_CD.Text, this.NIOROSHI_GENBA_CD.Text, this.DENPYOU_DATE.Text, this.logic.footerForm.sysDate.Date, out catchErr);
            if (!catchErr)
            {
                return;
            }
            var nioroshiGenba = retData;
            if (nioroshiGenba != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGenba.SHOKUCHI_KBN;
            }

            this.logic.NioroshiGenbaCdSet();   // 比較用現場CDをセット
        }

        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.logic.IsSuuryouKesannFlg = true;

            this.isNotMoveFocus = this.SetNioroshiGenba();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 運搬業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            // 運搬業者を取得
            bool catchErr = true;
            var retData = this.logic.accessor.GetGyousha(this.UNPAN_GYOUSHA_CD.Text, this.DENPYOU_DATE.Text, this.logic.footerForm.sysDate.Date, out catchErr);
            if (!catchErr)
            {
                return;
            }
            var unpanGyousha = retData;
            if (unpanGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)unpanGyousha.SHOKUCHI_KBN;
            }
        }

        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UNPAN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.logic.IsSuuryouKesannFlg = true;

            this.isNotMoveFocus = this.SetUnpanGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 入力担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NYUURYOKU_TANTOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            if (!this.logic.CheckNyuuryokuTantousha())
            {
                return;
            }
        }

        #region 取引先イベント
        /// <summary>
        /// 取引先フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isInputError)
            {
                this.logic.TorihikisakiCdSet(); // 比較用取引先CDをセット
            }
        }

        /// <summary>
        /// 取引先更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void TORIHIKISAKI_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.logic.IsSuuryouKesannFlg = true;

            this.isNotMoveFocus = this.SetTorihikisaki();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }
        #endregion 取引先イベント

        #region 業者イベント
        /// <summary>
        /// 業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 業者前回値保存
            this.gyoushaEnter();
        }

        /// <summary>
        /// 業者前回値保存
        /// </summary>
        public void gyoushaEnter()
        {
            if (!this.isInputError)
            {
                this.logic.EnterGyoushaCdSet();

                if (!this.isFromSearchButton)
                {
                    this.logic.GyoushaCdSet();  //比較用業者CDをセット
                }
            }
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.validateFlag = true;
            this.logic.IsSuuryouKesannFlg = true;
            this.isNotMoveFocus = this.SetGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.validateFlag = false;
            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }
        #endregion 業者イベント

        #region 現場イベント
        /// <summary>
        /// 現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isInputError)
            {
                this.logic.GenbaCdSet();   // 比較用現場CDをセット
            }

        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;
            this.validateFlag = true;
            this.logic.IsSuuryouKesannFlg = true;
            this.isNotMoveFocus = this.SetGenba();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.validateFlag = false;
            this.oldShokuchiKbn = false;
            this.logic.IsSuuryouKesannFlg = false;
        }
        #endregion 現場イベント

        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckEigyouTantousha())
            {
                return;
            }
        }

        /// <summary>
        /// 車輌フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            if (!this.editingSharyouCdFlag)
            {
                this.logic.ShayouCdSet();   // 比較用車輌CDをセット
            }
        }

        /// <summary>
        /// 車輌検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            this.editingSharyouCdFlag = true;

            if (!this.logic.CheckSharyou())
            {
                return;
            }

            if (string.IsNullOrEmpty(this.SHARYOU_EMPTY_JYUURYOU.Text) || this.selectDenshuKbnCd == DENSHU_KBN.SHUKKA)
            {
                this.logic.footerForm.bt_func8.Enabled = false;
            }
            else
            {
                this.logic.footerForm.bt_func8.Enabled = true;
            }

            if (this.SHARYOU_CD.AutoChangeBackColorEnabled)
            {
                this.SHARYOU_CD.UpdateBackColor(false);
            }
            this.editingSharyouCdFlag = false;
        }

        public void SharyouPopupBefore()
        {
            this.logic.popupCd = this.SHARYOU_CD.Text;
            this.logic.popupCd2 = this.UNPAN_GYOUSHA_CD.Text;
            this.logic.GyoushaCdSet();
            this.isSelectingSharyouCd = true;
        }

        public void SharyouPopupAfter()
        {
            if (this.logic.popupCd != this.SHARYOU_CD.Text || this.logic.popupCd2 != this.UNPAN_GYOUSHA_CD.Text)
            {
                this.SHARYOU_CD_Validated(null, null);
                this.isSelectingSharyouCd = false;
            }
            MoveToSharyouCd();
        }

        /// <summary>
        /// 車輌名フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_NAME_RYAKU_Enter(object sender, EventArgs e)
        {
            this.isSelectingSharyouCd = false;
        }

        /// <summary>
        /// 形態区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckKeitaiKbn())
            {
                return;
            }
        }

        /// <summary>
        /// 台貫区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAIKAN_KBN_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckDaikanKbn())
            {
                return;
            }
        }

        /// <summary>
        /// 伝票日付検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            this.logic.IsSuuryouKesannFlg = true;
            var inputDenpyouDate = this.DENPYOU_DATE.Text;
            if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.logic.tmpDenpyouDate.Equals(inputDenpyouDate))
            {
                if(!this.logic.CheckDenpyouDate())
                {
                    return;
                }

                if (!this.logic.tmpDenpyouDate.Equals(inputDenpyouDate))
                {
                    this.logic.SetUriageShouhizeiRate();
                    this.logic.SetShiharaiShouhizeiRate();
                }
            }

            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 計量番号前ボタンクリック処理
        /// 現在入力されている番号の前の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void previousButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            this.GetKeiryouDataForPreOrNextButton(true, sender, e);

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            this.SHARYOU_CD.Focus();
            //if (!this.logic.SetTopControlFocus())
            //{
            //    return;
            //}
        }

        /// <summary>
        /// 計量番号後ボタンクリック処理
        /// 現在入力されている番号の後の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void nextButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            this.GetKeiryouDataForPreOrNextButton(false, sender, e);

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            //this.logic.SetTopControlFocus();
            this.SHARYOU_CD.Focus();
        }

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_SETREDRAW = 0x000B;

        /// <summary>
        /// 新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeNewWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.isCopy = false;
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            // 追加権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                this.InitNumbers();
                this.SEQ = "0";

                this.STACK_JYUURYOU.Text = String.Empty;
                this.EMPTY_JYUURYOU.Text = String.Empty;
                this.SHARYOU_EMPTY_JYUURYOU.Text = String.Empty;

                //base.OnLoad(e);
                // Entity等初期化
                if (this.ismobile_mode)
                {
                    this.logic._tabPageManager.ChangeTabPageVisible(1, true);
                }
                this.logic = new LogicClass(this);
                bool catchErr = true;
                bool retDate = this.logic.GetAllEntityData(out catchErr);
                if (!catchErr)
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

                // スクロールバーが下がる場合があるため、
                // 強制的にバーを先頭にする
                this.AutoScrollPosition = new Point(0, 0);

                base.HeaderFormInit();
            }

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            // 初期フォーカス位置
            //this.logic.SetTopControlFocus();
            this.SHARYOU_CD.Focus();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeUpdateWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.isCopy = false;
            this.ENTRY_NUMBER_Validated(sender, e);
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

            FormManager.OpenFormWithAuth("G673", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.KEIRYOU, CommonShogunData.LOGIN_USER_INFO.SHAIN_CD);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌空車重量取込
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void SharyouKuushaJyuuryouTorikomi(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.DETAIL_TAB.SelectedIndex != 0)
            {
                this.logic.msgLogic.MessageBoxShow("E275", "計量明細タブ", "[F8]空車取込を実行");
                return;
            }

            // 重量取込処理中にグローバル変数が変わる可能性があるのでローカル変数に待避
            string beforCtrlName = this.beforControlName;
            string beforeRowIndex = string.Empty;
            string beforeCellIndex = string.Empty;
            if (beforCtrlName.Equals("gcMultiRow1"))
            {
                Cell cell = this.gcMultiRow1.SelectedCells[0];
                beforeRowIndex = cell.RowIndex.ToString();
                beforeCellIndex = cell.CellIndex.ToString();
            }
            bool catchErr = true;
            this.EMPTY_JYUURYOU.Text = this.SHARYOU_EMPTY_JYUURYOU.Text;
            if (string.IsNullOrEmpty(this.EMPTY_KEIRYOU_TIME.Text))
            {
                this.EMPTY_KEIRYOU_TIME.Text = this.logic.GetDate(out catchErr);
            }
            var lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();
            if (null != lastRow)
            {
                var isInput = false;

                var sharyouEmptyJyuuryou = this.SHARYOU_EMPTY_JYUURYOU.Text;
                var stackJyuuryou = lastRow.Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Value;
                var emptyJyuuryou = lastRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value;

                if (this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).Count() == 1)
                {
                    // 入力が1行のみの場合、
                    //          「総重量が入力されている」かつ
                    //          「空車重量がブランク」の場合に空車重量を入力する
                    if (null != stackJyuuryou && !String.IsNullOrEmpty(stackJyuuryou.ToString()) && (null == emptyJyuuryou || String.IsNullOrEmpty(emptyJyuuryou.ToString())))
                    {
                        isInput = true;
                    }

                    // 入力が1行のみの場合、
                    //         「総重量が入力されている」かつ
                    //         「空車重量が入力されている」かつ
                    //         「空車重量と車輌の空車重量が一致していない」の場合、新規行を追加し、空車重量をセットする
                    if (null != stackJyuuryou && !String.IsNullOrEmpty(stackJyuuryou.ToString()) &&
                        null != emptyJyuuryou && !String.IsNullOrEmpty(emptyJyuuryou.ToString()) &&
                        0 != Decimal.Parse(emptyJyuuryou.ToString()).CompareTo(Decimal.Parse(sharyouEmptyJyuuryou))
                    )
                    {
                        this.gcMultiRow1.Rows.Add();
                        lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();

                        isInput = true;

                        // このとき、1つ上の空車重量を最終行の総重量にコピーする
                        lastRow.Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Value = emptyJyuuryou;
                    }
                }
                else
                {
                    // 入力が2行以上あった場合、
                    // 最終行が
                    //         「総重量が入力されている」かつ
                    //         「空車重量がブランク」の場合、空車重量をセットする
                    if (null != stackJyuuryou && !String.IsNullOrEmpty(stackJyuuryou.ToString()) &&
                        (null == emptyJyuuryou || String.IsNullOrEmpty(emptyJyuuryou.ToString()))
                    )
                    {
                        isInput = true;
                    }

                    var prevRow = this.gcMultiRow1.Rows[lastRow.Index - 1];
                    var emptyJyuuryouRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false && (r.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value != null && !String.IsNullOrEmpty(r.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))).LastOrDefault();
                    if (null != prevRow)
                    {
                        var prevRowEmptyJyuuryou = prevRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value;
                        var targetEmptyJyuuryou = String.Empty;
                        if (null != emptyJyuuryouRow && null != emptyJyuuryouRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value)
                        {
                            targetEmptyJyuuryou = emptyJyuuryouRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value.ToString();
                        }

                        // 最終行が
                        //         「総重量がブランク」かつ
                        //         「空車重量がブランク」かつ
                        //         「1つ上の行の空車重量が入力されている」かつ
                        //         「1つ上の行の空車重量が車輌の空車重量と一致していない」の場合、空車重量をセットする
                        if ((null == stackJyuuryou || String.IsNullOrEmpty(stackJyuuryou.ToString())) &&
                            (null == emptyJyuuryou || String.IsNullOrEmpty(emptyJyuuryou.ToString())) &&
                            null != prevRowEmptyJyuuryou && !String.IsNullOrEmpty(prevRowEmptyJyuuryou.ToString()) &&
                            0 != Decimal.Parse(prevRowEmptyJyuuryou.ToString()).CompareTo(Decimal.Parse(sharyouEmptyJyuuryou))
                        )
                        {
                            isInput = true;

                            // このとき、1つ上の空車重量を最終行の総重量にコピーする
                            lastRow.Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Value = targetEmptyJyuuryou;
                        }

                        // 最終行が
                        //         「総重量が入力されている」かつ
                        //         「空車重量が入力されている」かつ
                        //         「空車重量と車輌の空車重量が一致していない」の場合、新規行を追加し、空車重量をセットする
                        if (null != stackJyuuryou && !String.IsNullOrEmpty(stackJyuuryou.ToString()) &&
                            null != emptyJyuuryou && !String.IsNullOrEmpty(emptyJyuuryou.ToString()) &&
                            0 != Decimal.Parse(emptyJyuuryou.ToString()).CompareTo(Decimal.Parse(sharyouEmptyJyuuryou))
                        )
                        {
                            this.gcMultiRow1.Rows.Add();
                            lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();

                            isInput = true;

                            // このとき、1つ上の空車重量を最終行の総重量にコピーする
                            lastRow.Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Value = targetEmptyJyuuryou;
                        }
                    }
                }

                if (isInput)
                {
                    lastRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value = sharyouEmptyJyuuryou;
                    lastRow.Cells[LogicClass.CELL_NAME_KEIRYOU_TIME].Value = this.logic.GetDate(out catchErr);

                    // 再計算
                    // 数量にフォーカスがある場合、数量計算に不都合があるので一度フォーカスを外す。
                    if (this.gcMultiRow1.CurrentCell != null && this.gcMultiRow1.CurrentCell.Name.Equals(LogicClass.CELL_NAME_SUURYOU))
                    {
                        this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(this.gcMultiRow1.CurrentRow.Index, LogicClass.CELL_NAME_ROW_NO);
                    }
                    this.gcMultiRow1.ClearSelection();
                    this.gcMultiRow1.AddSelection(lastRow.Index);
                    this.beforeValuesForDetail[LogicClass.CELL_NAME_EMPTY_JYUURYOU] = String.Empty;
                    this.gcMultiRow1_CellValidated(
                        lastRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU],
                        new CellEventArgs(lastRow.Index, lastRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].CellIndex, LogicClass.CELL_NAME_EMPTY_JYUURYOU)
                        );
                }
            }

            // 処理前のコントロールにフォーカスを戻す
            if (!string.IsNullOrEmpty(beforCtrlName))
            {
                if (beforCtrlName.Equals("KYOTEN_CD"))
                {
                    ((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Focus();
                }
                else
                {
                    this.Controls[beforCtrlName].Focus();
                }

                if (beforCtrlName.Equals("gcMultiRow1") && !string.IsNullOrEmpty(beforeRowIndex) && !string.IsNullOrEmpty(beforeCellIndex))
                {
                    int iRow = int.Parse(beforeRowIndex);
                    int iCell = int.Parse(beforeCellIndex);
                    this.gcMultiRow1.EndEdit();
                    this.gcMultiRow1[iRow, iCell].Selected = true;
                    this.gcMultiRow1.BeginEdit(true);
                }
            }

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

            // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
            if (!this.logic.SetRequiredSetting(true))
            {
                return;
            }
            var autoCheckLogic = new AutoRegistCheckLogic(this.GetControl_Tairyuu(), this.GetControl_Tairyuu());
            base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (!this.logic.RequiredSettingInit())
            {
                return;
            }

            bool ret = true;

            // 委託契約チェック
            if (!base.RegistErrorFlag)
            {
                bool catchErr = true;
                bool retCheck = this.logic.CheckItakukeiyaku(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                ret = retCheck;
                if (!ret)
                {
                    return;
                }
            }

            if (this.ismobile_mode)
            {
                //受入実績明細チェック
                if (!base.RegistErrorFlag)
                {
                    // 明細行項目の入力チェック
                    if (!this.CheckDetailColumn2())
                    {
                        base.RegistErrorFlag = true;
                    }
                }
            }

            // 登録処理
            if (!base.RegistErrorFlag)
            {

                bool catchErr = true;
                bool retCheck = this.logic.CreateEntityAndUpdateTables(true, base.RegistErrorFlag,out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (!retCheck)
                {
                    return;
                }

                // 完了メッセージ表示
                this.logic.msgLogic.MessageBoxShow("I001", "登録");

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:

                        if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(((UIHeaderForm)this.logic.footerForm.headerForm).KEIZOKU_NYUURYOKU_VALUE.Text))
                        {
                            // 新規モードに切り替え、再度入力可能状態とする
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.InitNumbers();

                            // Entity等の初期化
                            if (this.ismobile_mode)
                            {
                                this.logic._tabPageManager.ChangeTabPageVisible(1, true);
                            }
                            this.logic = new LogicClass(this);
                            catchErr = true;
                            bool retDate = this.logic.GetAllEntityData(out catchErr);
                            if (!catchErr)
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

                            if (!this.logic.GetTairyuuData())
                            {
                                return;
                            }

                            // スクロールバーが下がる場合があるため、
                            // 強制的にバーを先頭にする
                            this.AutoScrollPosition = new Point(0, 0);
                        }
                        else
                        {
                            //画面を閉じる
                            base.CloseTopForm();
                        }
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(((UIHeaderForm)this.logic.footerForm.headerForm).KEIZOKU_NYUURYOKU_VALUE.Text)
                            && r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 新規モードに切り替え、再度入力可能状態とする
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.KeiryouNumber = -1;
                            this.InitNumbers();

                            // Entity等の初期化
                            if (this.ismobile_mode)
                            {
                                this.logic._tabPageManager.ChangeTabPageVisible(1, true);
                            }
                            this.logic = new LogicClass(this);
                            catchErr = true;
                            bool retDate = this.logic.GetAllEntityData(out catchErr);
                            if (!catchErr)
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

                            if (!this.logic.GetTairyuuData())
                            {
                                return;
                            }

                            // スクロールバーが下がる場合があるため、
                            // 強制的にバーを先頭にする
                            this.AutoScrollPosition = new Point(0, 0);
                        }
                        else
                        {
                            //画面を閉じる
                            base.CloseTopForm();
                        }
                        break;
                    default:
                        break;
                }

            }

            if ((SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(((UIHeaderForm)this.logic.footerForm.headerForm).KEIZOKU_NYUURYOKU_VALUE.PrevText)
                && r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.NEW_WINDOW_FLAG, false)
                && this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                || base.RegistErrorFlag)
            {
                if (!base.RegistErrorFlag)
                {
                    this.SHARYOU_CD.Focus();
                }
            }

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
            
            //領収書チェック、登録番号チェック
            if (this.logic.Ryousyu_ShikiriCheck())
            {
                return;
            }

            this.RegistDataProcess(sender, e);

            if ((SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(((UIHeaderForm)this.logic.footerForm.headerForm).KEIZOKU_NYUURYOKU_VALUE.PrevText)
                && r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.NEW_WINDOW_FLAG, false)
                && this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                || base.RegistErrorFlag
                || !this.logic.isRegistered)
            {
                if (!base.RegistErrorFlag)
                {
                    this.SHARYOU_CD.Focus();
                }
                else
                {
                    SetControlFocus();
                }
            }

            LogUtility.DebugMethodStart(sender, e);
        }

        /// <summary>
        /// 登録メイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal bool RegistDataProcess(object sender, EventArgs e, bool isManiRegist = false)
        {
            bool ret = true;
            try
            {
                functionStatus currentFuncState = new functionStatus();
                functionStatus allFalseFuncState = new functionStatus();

                allFalseFuncState.f1_state = false;
                allFalseFuncState.f2_state = false;
                allFalseFuncState.f3_state = false;
                allFalseFuncState.f4_state = false;
                allFalseFuncState.f5_state = false;
                allFalseFuncState.f6_state = false;
                allFalseFuncState.f7_state = false;
                allFalseFuncState.f8_state = false;
                allFalseFuncState.f9_state = false;
                allFalseFuncState.f10_state = false;
                allFalseFuncState.f11_state = false;
                allFalseFuncState.f12_state = false;
                allFalseFuncState.sf1_state = false;
                allFalseFuncState.sf2_state = false;
                allFalseFuncState.sf3_state = false;
                allFalseFuncState.sf4_state = false;
                allFalseFuncState.sf5_state = false;

                this.getFunctionEnabled(ref currentFuncState);

                if (this.logic.footerForm.bt_func9.Enabled)
                {
                    this.setFunctionEnabled(allFalseFuncState);
                }
                else
                {
                    return ret;
                }

                if (!this.logic.SetJyuuryouForRegist())
                {
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }

                // 取引先と拠点コードの関連チェック
                if (!this.logic.CheckTorihikisakiAndKyotenCd(null, this.TORIHIKISAKI_CD.Text))
                {
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }

                #region 休動対応箇所
                // HACK 休動処理実装済みだが計量将軍では未使用
                bool catchErr = true;
                bool retCheck = this.logic.SharyouDateCheck(out catchErr);
                if (!catchErr)
                {
                    this.setFunctionEnabled(currentFuncState);
                    ret = false;
                    return ret;
                }

                bool retCheck2 = this.logic.UntenshaDateCheck(out catchErr);
                if (!catchErr)
                {
                    this.setFunctionEnabled(currentFuncState);
                    ret = false;
                    return ret;
                }

                bool retCheck3 = this.logic.HannyuusakiDateCheck(out catchErr);
                if (!catchErr)
                {
                    this.setFunctionEnabled(currentFuncState);
                    ret = false;
                    return ret;
                }

                // 車輛チェック
                if (!retCheck)
                {
                    this.SHARYOU_CD.Focus();
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }
                else if (!retCheck2)
                {
                    this.UNTENSHA_CD.Focus();
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }
                else if (!retCheck3)
                {
                    this.NIOROSHI_GENBA_CD.Focus();
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }
                #endregion

                bool chkFlg = false;
                if (this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG && !this.logic.JyuuryouCheck(out chkFlg))
                {
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }

                this.logic.IsRegist = true;

                // 登録前にもう一度計算する
                // CalcDetailを実行すると空行を削除する処理が実行される。
                // その時に削除する行がCurrentだった場合にエラーが出てしまうので、Currentを移動しておく。(#23898)
                this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, LogicClass.CELL_NAME_ROW_NO);
                if (!this.logic.CalcDetail())
                {
                    this.setFunctionEnabled(currentFuncState);
                    ret = false;
                    return ret;
                }

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                        if (!this.logic.SetRequiredSetting(false))
                        {
                            this.setFunctionEnabled(currentFuncState);
                            ret = false;
                            return ret;
                        }
                        var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                        base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                        if (!this.logic.RequiredSettingInit())
                        {
                            this.setFunctionEnabled(currentFuncState);
                            ret = false;
                            return ret;
                        }

                        catchErr = true;
                        retCheck = this.logic.CheckRequiredDataForDeital(out catchErr);
                        if (!catchErr)
                        {
                            this.setFunctionEnabled(currentFuncState);
                            ret = false;
                            return ret;
                        }
                        // Detailの行数チェックはFWでできないので自前でチェック
                        if (!base.RegistErrorFlag
                            && !retCheck)
                        {
                            this.logic.msgLogic.MessageBoxShow("E001", "明細行");
                            base.RegistErrorFlag = true;
                        }
                        else
                        {
                            if (!base.RegistErrorFlag)
                            {
                                // 明細行項目の入力チェック
                                if (!this.CheckDetailColumn())
                                {
                                    base.RegistErrorFlag = true;
                                }
                            }
                        }

                        if (this.ismobile_mode)
                        {
                            //受入実績明細チェック
                            if (!base.RegistErrorFlag)
                            {
                                // 明細行項目の入力チェック
                                if (!this.CheckDetailColumn2())
                                {
                                    base.RegistErrorFlag = true;
                                }
                            }
                        }
 
                        // 現金取引チェック
                        if (!base.RegistErrorFlag)
                        {
                            catchErr = true;
                            retCheck = this.logic.GenkinTorihikiCheck(out catchErr);
                            if (!catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                ret = false;
                                return ret;
                            }
                            if (!retCheck)
                            {
                                base.RegistErrorFlag = true;
                            }
                        }

                        // 金額チェック
                        if (!base.RegistErrorFlag)
                        {
                            if (!this.CheckDetailKingaku())
                            {
                                base.RegistErrorFlag = true;
                            }
                        }

                        if (!base.RegistErrorFlag && this.ismobile_mode)
                        {
                            // 受入実績明細の入力がない状態で、作業日時、作業者、作業時備考が入力されていた場合、確認メッセージを表示する。
                            int jissekiMeisaiCnt = 0;
                            foreach (Row dr in this.gcMultiRow2.Rows)
                            {
                                if (dr.IsNewRow || string.IsNullOrEmpty(Convert.ToString(dr.Cells["ROW_NO"].Value)))
                                {
                                    continue;
                                }
                                jissekiMeisaiCnt++;
                            }

                            if (jissekiMeisaiCnt == 0
                                && (!string.IsNullOrEmpty(this.SAGYOU_DATE.Text)
                                    || !string.IsNullOrEmpty(this.SAGYOU_HOUR.Text)
                                    || !string.IsNullOrEmpty(this.SAGYOU_MINUTE.Text)
                                    || !string.IsNullOrEmpty(this.SAGYOUSHA_CD.Text)
                                    || !string.IsNullOrEmpty(this.SAGYOU_BIKOU.Text)))
                            {
                                var dia = this.logic.msgLogic.MessageBoxShowConfirm("受入実績の明細が入力されていない為、\n受入実績の内容は破棄されます。\n\n登録を続行しますか？");
                                if (dia == System.Windows.Forms.DialogResult.Yes)
                                {
                                    base.RegistErrorFlag = false;
                                }
                                else
                                {
                                    base.RegistErrorFlag = true;
                                }
                            }
                        }

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:

                        break;

                    default:
                        break;
                }

                // 登録処理
                if (!base.RegistErrorFlag)
                {
                    var lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();
                    if (null != lastRow)
                    {
                        var kuushaJyuuryou = this.STACK_JYUURYOU.Text;
                        var stackJyuuryou = lastRow.Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Value;
                        var emptyJyuuryou = lastRow.Cells[LogicClass.CELL_NAME_EMPTY_JYUURYOU].Value;

                        // 「車輌の空車重量が入っている」かつ「最終行の総重量が入っている」かつ「最終行の空車重量が入っていない」場合は、アラートを表示
                        if (!String.IsNullOrEmpty(kuushaJyuuryou)
                            && null != stackJyuuryou && !String.IsNullOrEmpty(stackJyuuryou.ToString())
                            && (null == emptyJyuuryou || String.IsNullOrEmpty(emptyJyuuryou.ToString()))
                        )
                        {
                            var messageLogic = new MessageBoxShowLogic();
                            var dialogResult = messageLogic.MessageBoxShow("C064");
                            if (DialogResult.No == dialogResult)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                this.logic.IsRegist = false;

                                return ret;
                            }
                        }
                    }

                    DialogResult result = new DialogResult();

                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            catchErr = true;
                            retCheck = this.logic.CheckItakukeiyaku(out catchErr);
                            if (!catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                ret = false;
                                return ret;
                            }

                            ret = retCheck;
                            if (!ret)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                return ret;
                            }

                            catchErr = true;
                            retCheck = this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag, out catchErr);
                            if (!catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                ret = false;
                                return ret;
                            }

                            if (!retCheck)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                this.logic.IsRegist = false;

                                return ret;
                            }

                            if (this.logic.headerForm.PRINT_KBN_VALUE.Text == "1" && !isManiRegist)
                            {
                                // 計量票
                                this.logic.isSubFunctionCall = false;
                                if (!this.logic.PrintKeiryouhyou())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }
                                this.logic.isSubFunctionCall = true;
                            }

                            // 帳票出力
                            if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.RECEIPT_KBN_CD.Text) && this.RECEIPT_KBN_CD.Enabled && !isManiRegist)
                            {
                                this.logic.Print();
                            }

                            //領収証但し書き
                            if (RECEIPT_KBN_CD.Enabled && RECEIPT_KBN_CD.Text.Equals(rb_RECEIPT_KBN_1.Value))
                            {
                                this.logic.SetStatus_tadasigaki();   
                            }
                            

                            // 完了メッセージ表示
                            this.logic.msgLogic.MessageBoxShow("I001", "登録");

                            this.logic.isRegistered = true;

                            if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(((UIHeaderForm)this.logic.footerForm.headerForm).KEIZOKU_NYUURYOKU_VALUE.Text))
                            {
                                // 【追加】モード初期表示処理
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                this.InitNumbers();

                                // Entity等の初期化
                                if (this.ismobile_mode)
                                {
                                    this.logic._tabPageManager.ChangeTabPageVisible(1, true);
                                }
                                this.logic = new LogicClass(this);
                                catchErr = true;
                                bool retDate = this.logic.GetAllEntityData(out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }
                                if (!this.logic.WindowInit())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }

                                if (!this.logic.ButtonInit())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }

                                if (!this.logic.GetTairyuuData())
                                {
                                    ret = false;
                                    return ret;
                                }
                                // スクロールバーが下がる場合があるため、
                                // 強制的にバーを先頭にする
                                this.AutoScrollPosition = new Point(0, 0);
                            }
                            else
                            {
                                //画面を閉じる
                                base.CloseTopForm();
                            }

                            break;

                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                            if (!this.logic.dto.entryEntity.TAIRYUU_KBN)
                            {
                                if (!chkFlg)
                                {
                                    result = this.logic.msgLogic.MessageBoxShow("C038");
                                }
                                else
                                {
                                    result = DialogResult.Yes;
                                }
                            }
                            if (result == DialogResult.Yes || this.logic.dto.entryEntity.TAIRYUU_KBN)
                            {
                                catchErr = true;
                                retCheck = this.logic.CheckItakukeiyaku(out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }
                                ret = retCheck;
                                if (!ret)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return ret;
                                }

                                catchErr = true;
                                retCheck = this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag, out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }
                                if (!retCheck)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    this.logic.IsRegist = false;

                                    return ret;
                                }

                                // 計量票
                                if (this.logic.headerForm.PRINT_KBN_VALUE.Text == "1" && !isManiRegist)
                                {
                                    this.logic.isSubFunctionCall = false;
                                    if (!this.logic.PrintKeiryouhyou())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        ret = false;
                                        return ret;
                                    }
                                    this.logic.isSubFunctionCall = true;
                                }

                                // 帳票出力
                                if (ConstClass.RYOSYUSYO_KBN_1.Equals(this.RECEIPT_KBN_CD.Text) && this.RECEIPT_KBN_CD.Enabled && !isManiRegist)
                                {
                                    this.logic.Print();
                                }

                                //領収証但し書き
                                if (RECEIPT_KBN_CD.Enabled && RECEIPT_KBN_CD.Text.Equals(rb_RECEIPT_KBN_1.Value))
                                {
                                    this.logic.SetStatus_tadasigaki();   
                                }

                                // 完了メッセージ表示
                                this.logic.msgLogic.MessageBoxShow("I001", "更新");

                                this.logic.isRegistered = true;

                                if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(((UIHeaderForm)this.logic.footerForm.headerForm).KEIZOKU_NYUURYOKU_VALUE.Text)
                                    && r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    // 継続入力ON かつ 追加権限がある場合
                                    // 【追加】モード初期表示処理
                                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                    this.KeiryouNumber = -1;


                                    // Entity等の初期化
                                    if (this.ismobile_mode)
                                    {
                                        this.logic._tabPageManager.ChangeTabPageVisible(1, true);
                                    }
                                    this.logic = new LogicClass(this);
                                    catchErr = true;
                                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                                    if (!catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        ret = false;
                                        return ret;
                                    }
                                    if (!this.logic.WindowInit())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        ret = false;
                                        return ret;
                                    }

                                    if (!this.logic.ButtonInit())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        ret = false;
                                        return ret;
                                    }

                                    if (!this.logic.GetTairyuuData())
                                    {
                                        ret = false;
                                        return ret;
                                    }
                                    // スクロールバーが下がる場合があるため、
                                    // 強制的にバーを先頭にする
                                    this.AutoScrollPosition = new Point(0, 0);
                                    this.logic.isClose = false;
                                }
                                else if (isManiRegist)
                                {
                                    this.logic.isClose = true;
                                }
                                else
                                {
                                    //画面を閉じる
                                    base.CloseTopForm();
                                }
                            }

                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            result = this.logic.msgLogic.MessageBoxShow("C026");
                            if (result == DialogResult.Yes)
                            {
                                catchErr = true;
                                retCheck = this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag, out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }
                                if (!retCheck)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    this.logic.IsRegist = false;

                                    return ret;
                                }

                                // 完了メッセージ表示
                                this.logic.msgLogic.MessageBoxShow("I001", "削除");

                                this.logic.isRegistered = true;

                                //画面を閉じる
                                base.CloseTopForm();
                            }

                            break;

                        default:
                            break;
                    }
                }

                this.setFunctionEnabled(currentFuncState);
                this.logic.IsRegist = false;

                // フォーカス制御はRegistメソッドで制御する

                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistDataProcess", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
                return ret;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
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

            if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE && this.DETAIL_TAB.SelectedIndex == 1)
            {
                // 行を追加
                this.logic.AddNewRow2();
            }
            else
            {
                // 行を追加
                this.logic.AddNewRow();
                // 合計系計算
                if (!this.logic.CalcTotalValues())
                {
                    return;
                }
                if (this.DETAIL_TAB.SelectedIndex != 0)
                {
                    this.DETAIL_TAB.SelectedIndex = 0;
                }
            }
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
            if (editingMultiRowFlag)
            {
                return;
            }

            if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE && this.DETAIL_TAB.SelectedIndex == 1)
            {
                editingMultiRowFlag = true;
                //受入実績
                if (!this.logic.RemoveSelectedRow2())
                {
                    return;
                }
                editingMultiRowFlag = false;
            }
            else
            {
                editingMultiRowFlag = true;
                if (!this.logic.RemoveSelectedRow())
                {
                    return;
                }

                // 合計系計算
                if (!this.logic.CalcTotalValues())
                {
                    return;
                }

                if (this.DETAIL_TAB.SelectedIndex != 0)
                {
                    this.DETAIL_TAB.SelectedIndex = 0;
                }
                if (this.selectDenshuKbnCd == DENSHU_KBN.SHUKKA && this.gcMultiRow1.Rows.Count > 0
                    && this.gcMultiRow1.Rows[0].IsNewRow)
                {
                    this.EMPTY_JYUURYOU.Text = string.Empty;
                    this.EMPTY_KEIRYOU_TIME.Text = string.Empty;
                }
                editingMultiRowFlag = false;
            }
            LogUtility.DebugMethodStart(sender, e);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            base.CloseTopForm();
        }

        /// <summary>
        /// 閉じる処理後に実行したいメソッドがある場合、実行する。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (this.closeMethod != null)
            {
                this.closeMethod();
                this.closeMethod = null;
            }
            base.OnHandleDestroyed(e);
        }

        // 明細のイベント

        /// <summary>
        /// 明細の行移動処理
        /// 明細の行が増減するたびに必ず実行してください
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_RowLeave(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (editingMultiRowFlag)
            {
                return;
            }
            editingMultiRowFlag = true;
            // ROW_NOを採番
            this.notEditingOperationFlg = true;
            if (!this.logic.NumberingRowNo())
            {
                return;
            }

            this.notEditingOperationFlg = false;
            editingMultiRowFlag = false;
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

            this.isHinmeiReLoad = false;

            switch (e.CellName)
            {
                case LogicClass.CELL_NAME_CHOUSEI_JYUURYOU:

                    bool catchErr = true;
                    bool retChousei = this.logic.ValidateChouseiJyuuryou(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case LogicClass.CELL_NAME_CHOUSEI_PERCENT:
                    catchErr = true;
                    retChousei = this.logic.ValidateChouseiPercent(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case LogicClass.CELL_NAME_HINMEI_CD:
                    if (beforeValuesForDetail.ContainsKey(e.CellName)
                        && beforeValuesForDetail[e.CellName].Equals(
                            Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)) && !this.isInputError)
                    {
                    }
                    else
                    {
                        this.isInputError = false;
                        var value = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value);
                        if (string.IsNullOrEmpty(value))
                        {
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = "";
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_ZEI_KBN_CD].Value = "";
                        }
                        catchErr = true;
                        retChousei = this.logic.GetHinmei(this.gcMultiRow1.CurrentRow,out catchErr);
                        if (!catchErr)
                        {
                            this.isInputError = true;
                            e.Cancel = true;
                            return;
                        }
                        if (retChousei)
                        {
                            this.logic.msgLogic.MessageBoxShow("E020", "品名");
                            this.isInputError = true;
                            e.Cancel = true;
                            return;
                        }
                    }

                    // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                    catchErr = true;
                    retChousei = this.logic.CheckHinmeiCd(this.gcMultiRow1.CurrentRow, out catchErr);
                    if (!catchErr)
                    {
                        this.isInputError = true;
                        e.Cancel = true;
                        return;
                    }
                    if (!retChousei)
                    {
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_CD].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_TANKA].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_KINGAKU].Value = string.Empty;

                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;

                        return;
                    }

                    // 空だったら処理中断
                    this.gcMultiRow1.BeginEdit(false);
                    this.gcMultiRow1.EndEdit();
                    this.gcMultiRow1.NotifyCurrentCellDirty(false);
                    if (string.IsNullOrEmpty((string)this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value))
                    {
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                        return;
                    }

                    var targetRow = this.gcMultiRow1.CurrentRow;
                    if (targetRow != null)
                    {
                        GcCustomTextBoxCell control = (GcCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                        if (control.TextBoxChanged == true 
                            || (beforeValuesForDetail.ContainsKey(e.CellName) && !beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
                            )
                        {
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                            control.TextBoxChanged = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value)))
                    {
                        return;
                    }

                    bool bResult = this.logic.SetDenpyouKbn();
                    if (!bResult)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                        catchErr = true;
                        retChousei = this.logic.CheckHinmeiCd(this.gcMultiRow1.CurrentRow, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (retChousei)    // 品名コードの存在チェック（伝種区分が受入、または共通）
                        {
                            // 品名再読込フラグを立てる
                            this.isHinmeiReLoad = true;

                            if (!this.logic.SearchAndCalcForUnit(true, this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }

                            if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                            {
                                return;
                            }

                            if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }

                        }
                        else if (this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value == null)
                        {
                            // 品名CDに入力がなければ、単位コードとその略称もクリアする
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_CD].Value = string.Empty;
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_TANKA].Value = string.Empty;
                        }
                        // 合計系計算
                        if (!this.logic.CalcTotalValues())
                        {
                            return;
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

                        // 単位kgの品名数量設定(上記処理で前回値を更新してしまうため)
                        if (!this.logic.SetHinmeiSuuryou(e.CellName, this.gcMultiRow1.CurrentRow, false))
                        {
                            return;
                        }

                    }
                    break;

                case LogicClass.CELL_NAME_HINMEI_NAME:
                    catchErr = true;
                    retChousei = this.logic.ValidateHinmeiName(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case LogicClass.CELL_NAME_STAK_JYUURYOU:
                case LogicClass.CELL_NAME_EMPTY_JYUURYOU:
                    catchErr = true;
                    retChousei = this.logic.ValidateJyuryouFormat(this.gcMultiRow1.CurrentRow, e.CellName, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case LogicClass.CELL_NAME_KEIRYOU_TIME:
                    var cell = this.gcMultiRow1[e.RowIndex, e.CellIndex];
                    if (!this.logic.IsTimeChkOK(cell))
                    {
                        e.Cancel = true;
                    }
                    break;

                default:
                    break;
            }

            // 単価と金額の活性/非活性制御
            if (e.CellName.Equals(LogicClass.CELL_NAME_TANKA) &&
                beforeValuesForDetail.ContainsKey(e.CellName) &&
                !beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
            {
                // 単価の場合のみCellValidatedでReadOnly設定が変わる場合があるのでここで一旦計算を行う
                if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                {
                    return;
                }
                if (!this.logic.CalcTotalValues())
                {
                    return;
                }
            }
            SetIchranReadOnly(e.RowIndex);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各CELLの更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 品名CDは前回値との比較を行わない
            if (e.CellName != LogicClass.CELL_NAME_HINMEI_CD)
            {
                // 前回値と変更が無かったら処理中断
                if (beforeValuesForDetail.ContainsKey(e.CellName) &&
                    beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
                {
                    return;
                }
            }

            if (editingMultiRowFlag == false)
            {
                editingMultiRowFlag = true;

                switch (e.CellName)
                {
                    case LogicClass.CELL_NAME_UNIT_CD:
                        if (!this.logic.SearchAndCalcForUnit(false, this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_STAK_JYUURYOU:
                        if (!this.logic.ChangeChouseiInputStatus())
                        {
                            return;
                        }
                        if (!this.logic.CalcYoukiJyuuryou())                         // 容器重量にも影響するので再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetail())
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        if (!this.logic.SetKeiryouTime(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_EMPTY_JYUURYOU:
                        if (!this.logic.ChangeChouseiInputStatus())
                        {
                            return;
                        }
                        if (!this.logic.CalcYoukiJyuuryou())                         // 容器重量にも影響するので再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetail())
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        if (!this.logic.SetKeiryouTime(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_CHOUSEI_JYUURYOU:
                        if (!this.logic.CalcChouseiJyuuryou())
                        {
                            return;
                        }
                        if (!this.logic.ChangeInputStatusForChousei())
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_CHOUSEI_PERCENT:
                        if (!this.logic.CalcChouseiPercent())
                        {
                            return;
                        }
                        if (!this.logic.ChangeInputStatusForChousei())
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        };
                        break;

                    case LogicClass.CELL_NAME_YOUKI_CD:
                        if (!this.logic.InitYoukiItem())
                        {
                            return;
                        }
                        if (!this.logic.CalcYoukiSuuryou())
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_YOUKI_SUURYOU:
                        if (!this.logic.CalcYoukiSuuryou())
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_YOUKI_JYUURYOU:
                        if (!this.logic.CalcYoukiJyuuryou())                         // 容器重量にも影響するので再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        if (this.gcMultiRow1.CurrentRow["YOUKI_JYUURYOU"].Value == null
                            || string.IsNullOrEmpty(this.gcMultiRow1.CurrentRow["YOUKI_JYUURYOU"].Value.ToString()))
                        {
                            // 容器重量をクリアした場合は、容器数量も同時にクリアする
                            this.gcMultiRow1.CurrentRow["YOUKI_SUURYOU"].Value = string.Empty;
                        }
                        break;

                    case LogicClass.CELL_NAME_SUURYOU:
                    case LogicClass.CELL_NAME_TANKA:
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_NET_JYUURYOU:
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_HINMEI_CD:
                        // 品名をセット
                        if (!this.logic.SetHinmeiName(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    default:
                        break;
                }

                // 高々十数件の明細行を計算するだけなので
                // 毎回合計系計算を実行する
                if (!this.logic.CalcTotalValues())
                {
                    return;
                }
                editingMultiRowFlag = false;

                bool isKingakuNotCalc = false;
                if (e.CellName.Equals(LogicClass.CELL_NAME_HINMEI_CD) && !this.isHinmeiReLoad)
                {
                    /* 品名の場合、前回値が同じでも再度ここまで到達するため、*/
                    /* 品名再読込がされていなければ、金額計算をさせない。    */
                    isKingakuNotCalc = true;
                }

                // 単位kgの品名数量設定
                if (!this.logic.SetHinmeiSuuryou(e.CellName, this.gcMultiRow1.CurrentRow, isKingakuNotCalc))
                {
                    return;
                }

            }
            LogUtility.DebugMethodStart(sender, e);
        }

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Row row = this.gcMultiRow1.CurrentRow;

            if (row == null)
            {
                return;
            }

            // 品名でPopup表示後処理追加
            if (e.CellName == LogicClass.CELL_NAME_HINMEI_CD)
            {
                // PopupResult取得できるようにPopupAfterExecuteにデータ設定
                GcCustomTextBoxCell cell = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                cell.PopupAfterExecute = PopupAfter_HINMEI_CD;
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
                case LogicClass.CELL_NAME_HINMEI_NAME:
                    // 品名をセット
                    if (!this.logic.SetHinmeiName(row))
                    {
                        return;
                    }
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ROW選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_RowEnter(object sender, CellEventArgs e)
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

        /// <summary>
        /// 品名検索(F6)
        /// </summary>
        internal void SearchHinmei(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.SearchHinmei();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 切替(F6)
        /// </summary>
        internal void ChangeTabPage(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.ChangeTabPage();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 重量取込
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Weight_Click(object sender, EventArgs e)
        {
            if (this.logic.IsRegist || this.IsDisposed)
            {
                return;
            }

            if (this.DETAIL_TAB.SelectedIndex != 0)
            {
                this.logic.msgLogic.MessageBoxShow("E275", "計量明細タブ", "[F1]重量取込を実行");
                return;
            }

            if (this.logic.dto.sysInfoEntity != null && this.logic.dto.sysInfoEntity.KEIRYOU_GOODS_KBN == 1
                && this.gcMultiRow1.Rows.Count >= 1
                && !string.IsNullOrEmpty(Convert.ToString(this.gcMultiRow1[0, LogicClass.CELL_NAME_NET_JYUURYOU].Value)))
            {
                var dia = this.logic.msgLogic.MessageBoxShowConfirm("単品目計量票が印刷設定されています。\n計量票の数量に誤差が生じますがよろしいですか？");
                if (dia != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            functionStatus currentFuncState = new functionStatus();
            functionStatus allFalseFuncState = new functionStatus();

            allFalseFuncState.f1_state = false;
            allFalseFuncState.f2_state = false;
            allFalseFuncState.f3_state = false;
            allFalseFuncState.f4_state = false;
            allFalseFuncState.f5_state = false;
            allFalseFuncState.f6_state = false;
            allFalseFuncState.f7_state = false;
            allFalseFuncState.f8_state = false;
            allFalseFuncState.f9_state = false;
            allFalseFuncState.f10_state = false;
            allFalseFuncState.f11_state = false;
            allFalseFuncState.f12_state = false;
            allFalseFuncState.sf1_state = false;
            allFalseFuncState.sf2_state = false;
            allFalseFuncState.sf3_state = false;
            allFalseFuncState.sf4_state = false;
            allFalseFuncState.sf5_state = false;

            this.getFunctionEnabled(ref currentFuncState);
            this.setFunctionEnabled(allFalseFuncState);

            //重量値表示プロセス起動
            truckScaleWeight1.ProcessWeight();

            // 自動手動重量表示の値を読み込んでSetJyuuryouに渡す変数にセット
            bool WeightDisplaySwitch = truckScaleWeight1.WeightDisplaySwitch();

            if (!this.logic.SetJyuuryou(WeightDisplaySwitch))
            {
                this.setFunctionEnabled(currentFuncState);
                return;
            }
            if (!this.logic.CalcTotalValues())
            {
                this.setFunctionEnabled(currentFuncState);
                return;
            }

            this.setFunctionEnabled(currentFuncState);
        }

        /// <summary>
        /// 明細部の容器CDを変更したら容器重量を再計算する。
        /// </summary>
        public virtual void Detail_YOUKI_CD_PopupAfterExecute()
        {
            SendKeys.SendWait("{TAB}");    //フォーカスアウトイベントで再計算
            SendKeys.SendWait("+({TAB})"); //Shift + TABで元の位置へ戻す
        }

        #region プロセスボタン押下処理
        /// <summary>
        /// [1]計量票発行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, System.EventArgs e)
        {
            //if (!this.logic.PrintKeiryouhyou())
            //{
            //    return;
            //}
        }
        /// <summary>
        /// [2]車輌登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, System.EventArgs e)
        {
            if (!this.logic.ShowSharyou())
            {
                return;
            }
        }
        /// <summary>
        /// [3]マニフェスト登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process3_Click(object sender, System.EventArgs e)
        {
            this.logic.ShowManifest();
        }
        /// <summary>
        /// [4]滞留検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process4_Click(object sender, System.EventArgs e)
        {
            this.logic.GetTairyuuData();
        }
        /// <summary>
        /// [5]未実装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process5_Click(object sender, System.EventArgs e)
        {
        }
        #endregion プロセスボタン押下処理

        /// <summary>
        /// 行追加後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow1_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            // Dictionaryへの追加
            //this.logic.AddRowDic(e.RowIndex);
        }
        /// <summary>
        /// 行削除中イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow1_RowsRemoving(object sender, RowsRemovingEventArgs e)
        {
            // Dictionaryからの削除
            //this.logic.RemoveRowDic(e.RowIndex);
        }

        /// <summary>
        /// 運転者CDロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckUntensha())
            {
                return;
            }
        }
        #endregion

        #region ユーティリティ
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        public Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm.headerForm));
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm));

            return allControl.ToArray();
        }

        /// <summary>
        /// コンストラクタで渡された計量番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistKeiryouData()
        {
            bool catchErr = true;
            bool retExist = this.logic.IsExistKeiryouData(this.KeiryouNumber, out catchErr);
            if (!catchErr)
            {
                return false;
            }

            return retExist;
        }

        /// <summary>
        /// 計量番号、SEQのデータで滞留登録された計量伝票用の権限チェック
        /// </summary>
        /// <returns></returns>
        public bool HasAuthorityTairyuu()
        {
            bool catchErr = true;
            bool retExist = this.logic.HasAuthorityTairyuu(this.KeiryouNumber, this.SEQ, out catchErr);
            if (!catchErr)
            {
                return false;
            }
            return retExist;
        }

        /// <summary>
        /// 画面遷移するイベントかどうかチェック
        /// 以下の操作の場合に画面遷移すると判断する。
        /// 　・一覧ボタンクリック(F7)
        /// 　・閉じるボタンクリック(F12)
        /// </summary>
        /// <returns></returns>
        private bool isChangeScreenEvent()
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

        /// <summary>
        /// 取引先CDへフォーカス移動する
        /// 取引先CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToTorihikisakiCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.TORIHIKISAKI_CD.Text != this.logic.tmpTorihikisakiCd)
            {
                this.SetTorihikisaki();
                this.TORIHIKISAKI_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 業者CDへフォーカス移動する
        /// 業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGyoushaCd()
        {
            this.SetGyousha();
            this.gyoushaEnter();
        }

        /// <summary>
        /// 現場ポップアップ起動前に実行されます
        /// </summary>
        public virtual void GenbaPopupBefore()
        {
            this.logic.GenbaCdSet();
        }

        /// <summary>
        /// 現場CDへフォーカス移動する
        /// 現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.GYOUSHA_CD.Text != this.logic.tmpGyoushaCd || this.GENBA_CD.Text != this.logic.tmpGenbaCd)
            {
                this.logic.IsSuuryouKesannFlg = true;
                this.SetGenba();
                this.GENBA_CD.Focus();
                this.logic.IsSuuryouKesannFlg = false;
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 排出事業者CDへフォーカス移動する
        /// 排出事業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToHstGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.HST_GYOUSHA_CD.Text != this.logic.tmpHstGyoushaCd)
            {
                this.SetHstGyousha();
                this.HST_GYOUSHA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 排出事業場ポップアップ起動前に実行されます
        /// </summary>
        public virtual void HstGenbaPopupBefore()
        {
            this.logic.HstGenbaCdSet();
        }

        /// <summary>
        /// 排出事業場CDへフォーカス移動する
        /// 排出事業場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToHstGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.HST_GYOUSHA_CD.Text != this.logic.tmpHstGyoushaCd || this.HST_GENBA_CD.Text != this.logic.tmpHstGenbaCd)
            {
                this.SetHstGenba();
                this.HST_GENBA_CD.Focus();
            }
        }

        /// <summary>
        /// 処分事業者CDへフォーカス移動する
        /// 処分事業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToSbnGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.HST_GYOUSHA_CD.Text != this.logic.tmpSbnGyoushaCd)
            {
                this.SetSbnGyousha();
                this.HST_GYOUSHA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 処分事業場ポップアップ起動前に実行されます
        /// </summary>
        public virtual void SbnGenbaPopupBefore()
        {
            this.logic.SbnGenbaCdSet();
        }

        /// <summary>
        /// 処分事業場CDへフォーカス移動する
        /// 処分事業場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToSbnGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.SBN_GYOUSHA_CD.Text != this.logic.tmpSbnGyoushaCd || this.SBN_GENBA_CD.Text != this.logic.tmpSbnGenbaCd)
            {
                this.SetSbnGenba();
                this.SBN_GENBA_CD.Focus();
            }
        }

        /// <summary>
        /// 最終処分業者CDへフォーカス移動する
        /// 最終処分業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToLastSbnGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.LAST_SBN_GYOUSHA_CD.Text != this.logic.tmpLastSbnGyoushaCd)
            {
                this.SetLastSbnGyousha();
                this.LAST_SBN_GYOUSHA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 最終処分場ポップアップ起動前に実行されます
        /// </summary>
        public virtual void LastSbnGenbaPopupBefore()
        {
            this.logic.LastSbnGenbaCdSet();
        }

        /// <summary>
        /// 最終処分場CDへフォーカス移動する
        /// 最終処分場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToLastSbnGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.LAST_SBN_GYOUSHA_CD.Text != this.logic.tmpLastSbnGyoushaCd || this.LAST_SBN_GENBA_CD.Text != this.logic.tmpLastSbnGenbaCd)
            {
                this.SetLastSbnGenba();
                this.LAST_SBN_GENBA_CD.Focus();
            }
        }

        /// <summary>
        /// 荷降業者CDへフォーカス移動する
        /// 荷降業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNioroshiGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.NIOROSHI_GYOUSHA_CD.Text != this.logic.tmpNioroshiGyoushaCd)
            {
                this.SetNioroshiGyousha();
                this.NIOROSHI_GYOUSHA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷降現場ポップアップ起動前に実行されます
        /// </summary>
        public virtual void NioroshiGenbaPopupBefore()
        {
            this.logic.NioroshiGenbaCdSet();
        }

        /// <summary>
        /// 荷降現場CDへフォーカス移動する
        /// 荷降現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNioroshiGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.NIOROSHI_GYOUSHA_CD.Text != this.logic.tmpNioroshiGyoushaCd || this.NIOROSHI_GENBA_CD.Text != this.logic.tmpNioroshiGenbaCd)
            {
                this.SetNioroshiGenba();
                this.NIOROSHI_GENBA_CD.Focus();
            }
        }

        /// <summary>
        /// 荷積業者CDへフォーカス移動する
        /// 荷積業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNizumiGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.NIZUMI_GYOUSHA_CD.Text != this.logic.tmpNizumiGyoushaCd)
            {
                this.SetNizumiGyousha();
                this.NIZUMI_GYOUSHA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷積現場ポップアップ起動前に実行されます
        /// </summary>
        public virtual void NizumiGenbaPopupBefore()
        {
            this.logic.NizumiGenbaCdSet();
        }

        /// <summary>
        /// 荷積現場CDへフォーカス移動する
        /// 荷積現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNizumiGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.NIZUMI_GYOUSHA_CD.Text != this.logic.tmpNizumiGyoushaCd || this.NIZUMI_GENBA_CD.Text != this.logic.tmpNizumiGenbaCd)
            {
                this.SetNizumiGenba();
                this.NIZUMI_GENBA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
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
            this.logic.UnpanGyoushaCdSet();
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
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.UNPAN_GYOUSHA_CD.Text != this.logic.tmpUnpanGyoushaCd)
            {
                this.SetUnpanGyousha();
                this.UNPAN_GYOUSHA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 運転者CDへフォーカス移動する
        /// 運転者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToUntenshaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.UNTENSHA_CD.Focus();
            if (this.UNTENSHA_CD.Text != this.logic.tmpUntenshaCd)
            {
                this.logic.CheckUntensha();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 計量番号を初期化
        /// </summary>
        private void InitNumbers()
        {
            this.KeiryouNumber = -1;
        }

        /// <summary>
        /// 滞留登録チェック用のコントロール取得
        /// </summary>
        /// <returns></returns>
        private Control[] GetControl_Tairyuu()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm.headerForm));

            return allControl.ToArray();
        }
        #endregion

        #region 伝票データ取得メソッド
        /// <summary>
        /// 計量データ取得メソッド
        /// </summary>
        /// <param name="isPrevious">true:前伝票を取得する、false:次伝票を取得する</param>
        private void GetKeiryouDataForPreOrNextButton(bool isPrevious, object sender, EventArgs e)
        {
            if (this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                return;
            }

            if (!nowLoding)
            {
                nowLoding = true;
                long keiryouNumber = 0;
                long preOrNextKeiryouNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;

                if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                {
                    long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out keiryouNumber);
                }
                // 受入番号の入力がある場合
                if (isPrevious)
                {
                    preOrNextKeiryouNumber = this.logic.GetPreKeiryouNumber(keiryouNumber);
                }
                else
                {
                    preOrNextKeiryouNumber = this.logic.GetNextKeiryouNumber(keiryouNumber);
                }
                if (preOrNextKeiryouNumber <= 0)
                {
                    if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                    {
                        nowLoding = false;
                        return;
                    }
                }
                if (preOrNextKeiryouNumber > 0)
                {
                    // 入力されている受入番号の後の受入番号が取得できた場合
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.KeiryouNumber = preOrNextKeiryouNumber;
                    bool catchErr = true;
                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retDate)
                    {
                        // エラー発生時には値をクリアする
                        this.ChangeNewWindow(sender, e);
                        nowLoding = false;
                        return;
                    }

                    catchErr = true;
                    bool retExist = this.logic.HasAuthorityTairyuu(this.KeiryouNumber, this.SEQ, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 滞留登録された受入伝票用権限チェック
                    if (!retExist)
                    {
                        // 滞留登録された受入伝票かつ新規権限がない場合はアラートを表示して処理中断
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E158", "新規");
                        this.WindowType = tmpType;
                        HeaderFormInit();
                        nowLoding = false;
                        return;
                    }

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G672", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            // 修正権限は無いが参照権限がある場合は参照モードで起動
                            this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            HeaderFormInit();
                        }
                        else
                        {
                            // どちらも無い場合はアラートを表示して処理中断
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            msg.MessageBoxShow("E158", "修正");
                            this.WindowType = tmpType;
                            HeaderFormInit();
                            nowLoding = false;
                            return;
                        }
                    }

                    if (!this.logic.WindowInit())
                    {
                        return;
                    }

                    if (!this.logic.ButtonInit())
                    {
                        return;
                    }

                    if (!this.logic.GetTairyuuData())
                    {
                        return;
                    }

                    // スクロールバーが下がる場合があるため、
                    // 強制的にバーを先頭にする
                    this.AutoScrollPosition = new Point(0, 0);

                    // 初期フォーカス位置の設定は本メソッドの呼び出し元で制御する
                }
                else
                {
                    // 入力されている受入番号の後の受入番号が取得できなかった場合
                    this.logic.msgLogic.MessageBoxShow("E045");
                    nowLoding = false;
                    return;
                }
                nowLoding = false;
            }
        }
        #endregion

        #region 取引先CD・業者CD・現場CDの関連情報セット処理

        /// <summary>
        /// 取引先CDに関連する情報をセット
        /// </summary>
        public bool SetTorihikisaki()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            if (this.isInputError || this.TORIHIKISAKI_CD.Text != this.logic.tmpTorihikisakiCd)
            {
                this.logic.IsSuuryouKesannFlg = true;
                ret = this.logic.CheckTorihikisaki(out catchErr);
                this.logic.IsSuuryouKesannFlg = false;

                if (!ret || !catchErr)
                {
                    return false;
                }
            }
            if (!this.isInputError)
            {
                this.logic.TorihikisakiCdSet();
            }

            return ret;
        }

        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        public bool SetGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            if (this.isInputError || this.GYOUSHA_CD.Text != this.logic.tmpGyoushaCd)
            {
                this.logic.IsSuuryouKesannFlg = true;
                ret = this.logic.CheckGyousha(out catchErr);
                this.logic.IsSuuryouKesannFlg = false;

                if (!catchErr)
                {
                    return false;
                }
            }
            this.logic.hasShow = false;

            return ret;
        }

        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        public bool SetGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;
            if (this.isInputError || (String.IsNullOrEmpty(this.GYOUSHA_CD.Text) || !this.logic.tmpGyoushaCd.Equals(this.GYOUSHA_CD.Text) ||
                    (this.logic.tmpGyoushaCd.Equals(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_NAME_RYAKU.Text)))
                    || (String.IsNullOrEmpty(this.GENBA_CD.Text) || !this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text) ||
                       (this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text) && string.IsNullOrEmpty(this.GENBA_NAME_RYAKU.Text)))
                    || this.isFromSearchButton)
            {
                ret = this.logic.CheckGenba(out catchErr);
                if (!ret || !catchErr)
                {
                    return false;
                }
            }

            this.logic.hasShow = false;
            return ret;
        }

        /// <summary>
        /// 排出事業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetHstGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            ret = this.logic.CheckHstGyoushaCd(out catchErr);

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 排出事業場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetHstGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            ret = this.logic.CheckHstGenbaCd(out catchErr);

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 処分業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetSbnGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            ret = this.logic.CheckSbnGyoushaCd(out catchErr);

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 処分事業場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetSbnGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            ret = this.logic.CheckSbnGenbaCd(out catchErr);

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 最終処分業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetLastSbnGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            ret = this.logic.CheckLastSbnGyoushaCd(out catchErr);

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 最終処分場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetLastSbnGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            ret = this.logic.CheckLastSbnGenbaCd(out catchErr);

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }
        
        /// <summary>
        /// 荷降業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNioroshiGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckNioroshiGyoushaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (!catchErr)
            {
                return false;
            }
            
            return ret;
        }

        /// <summary>
        /// 荷降現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNioroshiGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckNioroshiGenbaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 荷積業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckNizumiGyoushaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 荷積現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = true;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckNizumiGenbaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (!catchErr)
            {
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 運搬業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetUnpanGyousha()
        {
            // 初期化
            bool ret = false;

            bool catchErr = true;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckUnpanGyoushaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (!catchErr)
            {
                return false;
            }

            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            // 車輌の空車重量が変わる可能性があるので[F8]車輌空車取込ボタンの活性制御をする
            if (string.IsNullOrEmpty(this.SHARYOU_EMPTY_JYUURYOU.Text) || this.selectDenshuKbnCd == DENSHU_KBN.SHUKKA)
            {
                this.logic.footerForm.bt_func8.Enabled = false;
            }
            else
            {
                this.logic.footerForm.bt_func8.Enabled = true;
            }

            return ret;
        }

        #endregion 取引先CD・業者CD・現場CDの関連情報セット処理

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
                            //受入　（空重量と次の行の総重量が一致しないとエラー）
                            if (before_empty != row[KeiryouConstans.STACK_JYUURYOU].Value.ToString())
                            {
                                //エラー
                                row[KeiryouConstans.STACK_JYUURYOU].Style.BackColor = System.Drawing.Color.FromArgb(255, 100, 100);
                                this.logic.msgLogic.MessageBoxShowError("総重量と１つ前の空車重量が不整合です。");
                                return false;
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

        #region KeyDownイベントを発生させます
        /// <summary>
        /// KeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.keyEventArgs = e;
            if (e != null)
            {
                base.OnKeyDown(e);
            }
        }
        #endregion KeyDownイベントを発生させます
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            if (!this.logic.SetMoveData())
            {
                return;
            }
        }

        /// <summary>
        /// 品名設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_HINMEI_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var targetRow = this.gcMultiRow1.CurrentRow;
                if (targetRow != null)
                {
                    targetRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                }
            }
        }

        private bool execEntryNumberEvent = false;

        /// <summary>
        /// キー押下処理（TAB移動制御）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (this.ActiveControl != null && (this.ActiveControl.Name == "gcMultiRow1" || (this.ActiveControl.Name == "DETAIL_TAB" && this.DETAIL_TAB.SelectedIndex == 0)))
                {
                    this.gcMultiRow1[0, "HINMEI_CD"].Selected = true;
                }
                if (this.ismobile_mode)
                {
                    if (this.ActiveControl != null && (this.ActiveControl.Name == "gcMultiRow2" || (this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE && this.ActiveControl.Name == "DETAIL_TAB" && this.DETAIL_TAB.SelectedIndex == 1)))
                    {
                        this.gcMultiRow2[0, "HINMEI_CD"].Selected = true;
                    }
                }
            }
            return;
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (ActiveControl != null)
                {
                    var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;

                    if ("ENTRY_NUMBER".Equals(this.beforbeforControlName) && this.execEntryNumberEvent)
                    {
                        if (!this.logic.SetTopControlFocus())
                        {
                            return;
                        }
                        this.execEntryNumberEvent = false;
                    }
                    else if ("TAIRYUU_BIKOU".Equals(this.beforbeforControlName) && (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
                    {
                    }
                    else if (this.isSetShokuchiForcus)
                    {
                        // 諸口区分によるフォーカス移動の場合、ここで判定用のフラグを戻す
                        this.isSetShokuchiForcus = false;

                        if (!forward)
                        {
                            // Shiftの場合は諸口のCD⇒CDの前の項目の移動なので入力項目設定に従ってフォーカス移動を行う

                            // ActiveControlをCD項目に戻す
                            string activeControlName = this.ActiveControl.Name;
                            if (activeControlName.Equals(GYOUSHA_NAME_RYAKU.Name))
                            {
                                // 業者名⇒業者CD
                                this.ActiveControl = this.GYOUSHA_CD;
                            }
                            else if (activeControlName.Equals(GENBA_NAME_RYAKU.Name))
                            {
                                // 現場名⇒現場CD
                                this.ActiveControl = this.GENBA_CD;
                            }
                            else
                            {
                                this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();
                            }

                            this.logic.GotoNextControl(forward);
                        }
                    }
                    else
                    {
                        this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();

                        this.logic.GotoNextControl(forward);
                    }
                }
            }
        }

        /// <summary>
        /// 車輌フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_Enter(object sender, EventArgs e)
        {
            // 比較用車種CDをセット
            this.logic.ShashuCdSet();
        }

        /// <summary>
        /// 運転者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.isFromSearchButton)
            {
                // 比較用運転者CDをセット
                this.logic.UntenshaCdSet();
            }
        }

        /// <summary>
        /// 形態区分フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_Enter(object sender, EventArgs e)
        {
            // 比較用形態区分CDをセット
            this.logic.KeitaiKbnCdSet();
        }

        /// <summary>
        /// 伝票日付フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENPYOU_DATE_Enter(object sender, EventArgs e)
        {
            // 比較用伝票日付をセット
            this.logic.DenpyouDateSet();
        }

        /// <summary>
        /// 明細行の項目入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckDetailColumn()
        {
            foreach (Row row in this.gcMultiRow1.Rows)
            {
                if (!row.IsNewRow)
                {
                    // 金額
                    if (row.Cells[LogicClass.CELL_NAME_KINGAKU].Visible && row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue != null
                        && string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue.ToString()))
                    {
                        this.logic.msgLogic.MessageBoxShow("E148", "金額");
                        var cellKingaku = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_KINGAKU];
                        cellKingaku.IsInputErrorOccured = true;
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 登録時に数量*単価が金額に一致するかの金額チェックを実行します
        /// </summary>
        /// <returns></returns>
        private bool CheckDetailKingaku()
        {
            /* ここで行っている金額チェックの計算方法は明細の金額計算と同様です。 */
            /* どちらかの変更を行った際にはもう一方も修正してください。           */

            foreach (Row row in this.gcMultiRow1.Rows)
            {
                if (row.IsNewRow) continue;

                // 数量*単価=金額のチェック
                // 金額の計算は数量*単価で行っているため基本ありえないが何か起きた場合のため
                if (row.Cells[LogicClass.CELL_NAME_SUURYOU].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_SUURYOU].FormattedValue.ToString()) &&
                    row.Cells[LogicClass.CELL_NAME_TANKA].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_TANKA].FormattedValue.ToString()))
                {
                    decimal suryou = decimal.Parse(row.Cells[LogicClass.CELL_NAME_SUURYOU].FormattedValue.ToString());
                    decimal tanka = decimal.Parse(row.Cells[LogicClass.CELL_NAME_TANKA].FormattedValue.ToString());
                    decimal kingaku = decimal.Parse(row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue.ToString());
                    short kingakuHasuuCd = 3;

                    // 金額端数取得
                    if (row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                    {
                        if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
                        {
                            short.TryParse(Convert.ToString(this.logic.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        else
                        {
                            short.TryParse(Convert.ToString(this.logic.dto.sysInfoEntity.SEIKYUU_KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                    }
                    else if (row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                    {
                        if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
                        {
                            short.TryParse(Convert.ToString(this.logic.dto.torihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                        else
                        {
                            short.TryParse(Convert.ToString(this.logic.dto.sysInfoEntity.SHIHARAI_KINGAKU_HASUU_CD), out kingakuHasuuCd);
                        }
                    }

                    decimal tmpKingaku = Shougun.Function.ShougunCSCommon.Utility.CommonCalc.FractionCalc(suryou * tanka, kingakuHasuuCd);

                    if (!tmpKingaku.Equals(kingaku) && row.Cells[LogicClass.CELL_NAME_KINGAKU].Visible)
                    {
                        new MessageBoxShowLogic().MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                        return false;
                    }

                }
            }
            return true;
        }

        #region 休動対応箇所
        // HACK 休動処理実装済みだが計量将軍では未使用
        #region 車輌更新Validating
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = true;
            bool retCheck = this.logic.SharyouDateCheck(out catchErr);
            if (!catchErr || !retCheck)
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
            bool catchErr = true;
            bool retCheck = this.logic.UntenshaDateCheck(out catchErr);
            if (!catchErr || !retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 荷降現場Validating
        /// <summary>
        /// 荷降現場Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = true;
            bool retCheck = this.logic.HannyuusakiDateCheck(out catchErr);
            if (!catchErr || !retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 荷積現場Validating
        /// <summary>
        /// 荷積現場Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = true;
            // TODO ↓荷積現場で搬入先休動チェックは不要。削除すること
            bool retCheck = this.logic.HannyuusakiDateCheck(out catchErr);
            if (!catchErr || !retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion
        #endregion

        public void SetControlFocus()
        {

            if (string.IsNullOrEmpty(((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Text))
            {
                ((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.NYUURYOKU_TANTOUSHA_CD.Text))
            {
                this.NYUURYOKU_TANTOUSHA_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.DENPYOU_DATE.Text))
            {
                this.DENPYOU_DATE.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_CD.Focus();
                return;
            }

            if (this.gcMultiRow1.Rows.Count > 1)
            {
                foreach (var row in this.gcMultiRow1.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    var hinmeicell = row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                    var suuryoucell = row.Cells[LogicClass.CELL_NAME_SUURYOU];
                    var unitcell = row.Cells[LogicClass.CELL_NAME_UNIT_CD];
                    var kingakucell = row.Cells[LogicClass.CELL_NAME_KINGAKU];
                    this.gcMultiRow1.Rows[0].Cells[LogicClass.CELL_NAME_HINMEI_CD].Selected = false;

                    if (string.IsNullOrEmpty(Convert.ToString(hinmeicell.Value)))
                    {
                        this.DETAIL_TAB.SelectedIndex = 0;
                        hinmeicell.Style.BackColor = Constans.ERROR_COLOR;
                        hinmeicell.GcMultiRow.Focus();
                        hinmeicell.Selected = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(suuryoucell.Value)))
                    {
                        this.DETAIL_TAB.SelectedIndex = 0;
                        suuryoucell.Style.BackColor = Constans.ERROR_COLOR;
                        suuryoucell.GcMultiRow.Focus();
                        suuryoucell.Selected = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(unitcell.Value)))
                    {
                        this.DETAIL_TAB.SelectedIndex = 0;
                        unitcell.Style.BackColor = Constans.ERROR_COLOR;
                        unitcell.GcMultiRow.Focus();
                        unitcell.Selected = true;
                        return;
                    }

                    if (kingakucell.Visible && string.IsNullOrEmpty(Convert.ToString(kingakucell.Value)))
                    {
                        this.DETAIL_TAB.SelectedIndex = 0;
                        kingakucell.Style.BackColor = Constans.ERROR_COLOR;
                        kingakucell.GcMultiRow.Focus();
                        kingakucell.Selected = true;
                        return;
                    }
                }
                this.gcMultiRow1.Rows[0].Cells[LogicClass.CELL_NAME_HINMEI_CD].Selected = false;
            }

            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                }
                this.TORIHIKISAKI_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.SEIKYUU_TORIHIKI_KBN_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                } 
                this.SEIKYUU_TORIHIKI_KBN_CD.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                } 
                this.SEIKYUU_ZEI_KEISAN_KBN_CD.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.SEIKYUU_ZEI_KBN_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                } 
                this.SEIKYUU_ZEI_KBN_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.SHIHARAI_TORIHIKI_KBN_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                }
                this.SHIHARAI_TORIHIKI_KBN_CD.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                }
                this.SHIHARAI_ZEI_KEISAN_KBN_CD.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.SHIHARAI_ZEI_KBN_CD.Text))
            {
                if (this.ismobile_mode && this.selectDenshuKbnCd == DENSHU_KBN.UKEIRE)
                {
                    this.DETAIL_TAB.SelectedIndex = 4;
                }
                else
                {
                    this.DETAIL_TAB.SelectedIndex = 3;
                }
                this.SHIHARAI_ZEI_KBN_CD.Focus();
                return;
            }

            if (this.gcMultiRow1.Rows.Count == 1)
            {
                this.DETAIL_TAB.SelectedIndex = 0;
                var hinmeicell = this.gcMultiRow1.Rows[0].Cells[LogicClass.CELL_NAME_HINMEI_CD];
                hinmeicell.GcMultiRow.Focus();
                hinmeicell.Selected = true;
            }
        }

        #region ファンクションコントロール/状態取得→復元用
        private struct functionStatus
        {
            public bool f1_state { get; set; }
            public bool f2_state { get; set; }
            public bool f3_state { get; set; }
            public bool f4_state { get; set; }
            public bool f5_state { get; set; }
            public bool f6_state { get; set; }
            public bool f7_state { get; set; }
            public bool f8_state { get; set; }
            public bool f9_state { get; set; }
            public bool f10_state { get; set; }
            public bool f11_state { get; set; }
            public bool f12_state { get; set; }
            public bool sf1_state { get; set; }
            public bool sf2_state { get; set; }
            public bool sf3_state { get; set; }
            public bool sf4_state { get; set; }
            public bool sf5_state { get; set; }
        }

        private void getFunctionEnabled(ref functionStatus fs)
        {
            fs.f1_state = this.logic.footerForm.bt_func1.Enabled;
            fs.f2_state = this.logic.footerForm.bt_func2.Enabled;
            fs.f3_state = this.logic.footerForm.bt_func3.Enabled;
            fs.f4_state = this.logic.footerForm.bt_func4.Enabled;
            fs.f5_state = this.logic.footerForm.bt_func5.Enabled;
            fs.f6_state = this.logic.footerForm.bt_func6.Enabled;
            fs.f7_state = this.logic.footerForm.bt_func7.Enabled;
            fs.f8_state = this.logic.footerForm.bt_func8.Enabled;
            fs.f9_state = this.logic.footerForm.bt_func9.Enabled;
            fs.f10_state = this.logic.footerForm.bt_func10.Enabled;
            fs.f11_state = this.logic.footerForm.bt_func11.Enabled;
            fs.f12_state = this.logic.footerForm.bt_func12.Enabled;
            fs.sf1_state = this.logic.footerForm.bt_process1.Enabled;
            fs.sf2_state = this.logic.footerForm.bt_process2.Enabled;
            fs.sf3_state = this.logic.footerForm.bt_process3.Enabled;
            fs.sf4_state = this.logic.footerForm.bt_process4.Enabled;
            fs.sf5_state = this.logic.footerForm.bt_process5.Enabled;
        }

        private void setFunctionEnabled(functionStatus fs)
        {
            this.logic.footerForm.bt_func1.Enabled = fs.f1_state;
            this.logic.footerForm.bt_func2.Enabled = fs.f2_state;
            this.logic.footerForm.bt_func3.Enabled = fs.f3_state;
            this.logic.footerForm.bt_func4.Enabled = fs.f4_state;
            this.logic.footerForm.bt_func5.Enabled = fs.f5_state;
            this.logic.footerForm.bt_func6.Enabled = fs.f6_state;
            this.logic.footerForm.bt_func7.Enabled = fs.f7_state;
            this.logic.footerForm.bt_func8.Enabled = fs.f8_state;
            this.logic.footerForm.bt_func9.Enabled = fs.f9_state;
            this.logic.footerForm.bt_func10.Enabled = fs.f10_state;
            this.logic.footerForm.bt_func11.Enabled = fs.f11_state;
            this.logic.footerForm.bt_func12.Enabled = fs.f12_state;
            this.logic.footerForm.bt_process1.Enabled = fs.sf1_state;
            this.logic.footerForm.bt_process2.Enabled = fs.sf2_state;
            this.logic.footerForm.bt_process3.Enabled = fs.sf3_state;
            this.logic.footerForm.bt_process4.Enabled = fs.sf4_state;
            this.logic.footerForm.bt_process5.Enabled = fs.sf5_state;
        }
        #endregion

        internal string hinmeiCd = "";
        internal string hinmeiName = "";
        public void HINMEI_CD_PopupBeforeExecuteMethod()
        {
            this.hinmeiCd = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            this.hinmeiName = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value);
        }

        public void HINMEI_CD_PopupAfterExecuteMethod()
        {
            if (this.hinmeiCd == Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value) && this.hinmeiName == Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value))
            {
                return;
            }
            this.logic.GetHinmeiForPop(this.gcMultiRow1.CurrentRow);
            if (beforeValuesForDetail.ContainsKey(LogicClass.CELL_NAME_HINMEI_CD))
            {
                beforeValuesForDetail[LogicClass.CELL_NAME_HINMEI_CD] = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            }
        }

        public void TorihikisakiPopupBefore()
        {
            this.logic.TorihikisakiCdSet();
        }

        public void UnpanGyoushaPopupBefore()
        {
            this.logic.UnpanGyoushaCdSet();
        }

        public void GyoushaPopupBefore()
        {
            this.logic.GyoushaCdSet();
        }

        public void NioroshiGyoushaPopupBefore()
        {
            this.logic.NioroshiGyoushaCdSet();
        }

        public void NizumiGyoushaPopupBefore()
        {
            this.logic.NizumiGyoushaCdSet();
        }

        public void HstGyoushaPopupBefore()
        {
            this.logic.HstGyoushaCdSet();
        }

        public void SbnGyoushaPopupBefore()
        {
            this.logic.SbnGyoushaCdSet();
        }

        public void LastSbnGyoushaPopupBefore()
        {
            this.logic.LastSbnGyoushaCdSet();
        }


        #region 単価と金額の活性/非活性制御
        /// <summary>
        /// 単価と金額の活性/非活性制御
        /// </summary>
        /// <param name="rowIndex"></param>
        internal void SetIchranReadOnly(int rowIndex)
        {
            LogUtility.DebugMethodStart(rowIndex);

            if (rowIndex < 0) return;
            var row = this.gcMultiRow1.Rows[rowIndex];

            if ((row.Cells[LogicClass.CELL_NAME_TANKA].Value == null || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_TANKA].Value.ToString())) &&
                (row.Cells[LogicClass.CELL_NAME_KINGAKU].Value == null || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].Value.ToString())))
            {
                // 「単価」、「金額」どちらも空の場合、両方操作可
                this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].ReadOnly = false;
                this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].ReadOnly = false;
            }
            else if (row.Cells[LogicClass.CELL_NAME_TANKA].Value != null && !string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_TANKA].Value.ToString()))
            {
                // 「単価」のみ入力済みの場合、「金額」操作不可
                this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].ReadOnly = false;
                this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].ReadOnly = true;
            }
            else if (row.Cells[LogicClass.CELL_NAME_KINGAKU].Value != null && !string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].Value.ToString()))
            {
                // 「金額」のみ入力済みの場合、「単価」操作不可
                this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].ReadOnly = true;
                this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].ReadOnly = false;
            }

            // 設定した背景色を反映
            this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].UpdateBackColor(false);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 計量区分テーブル設定
        /// </summary>
        private bool SetKeiryouKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "通常登録";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "仮登録";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "計上";
                dt.Rows.Add(row);

                this.keiryouKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKeiryouKbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 計量区分ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.keiryouKbnTable;
                form.PopupTitleLabel = "計量区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "計量区分CD", "計量区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.KEIRYOU_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.KEIRYOU_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 計量区分検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.KEIRYOU_KBN_NAME.Text = string.Empty;
            var rows = this.keiryouKbnTable.Select(string.Format("CD = '{0}'", KEIRYOU_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.KEIRYOU_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.KEIRYOU_KBN_CD.Text = string.Empty;
                this.KEIRYOU_KBN_NAME.Text = string.Empty;
            }
        }

        #region 取引区分
        /// <summary>
        /// 取引区分テーブル設定
        /// </summary>
        private bool SetTorihikiKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "現金";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "掛け";
                dt.Rows.Add(row);

                this.torihikiKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikiKbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 取引区分(請求)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_TORIHIKI_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.torihikiKbnTable;
                form.PopupTitleLabel = "取引区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "取引区分CD", "取引区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.SEIKYUU_TORIHIKI_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.SEIKYUU_TORIHIKI_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 取引区分(請求)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_TORIHIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_TORIHIKI_KBN_NAME.Text = string.Empty;
            var rows = this.torihikiKbnTable.Select(string.Format("CD = '{0}'", SEIKYUU_TORIHIKI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SEIKYUU_TORIHIKI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SEIKYUU_TORIHIKI_KBN_CD.Text = string.Empty;
                this.SEIKYUU_TORIHIKI_KBN_NAME.Text = string.Empty;
            }
            if (this.SEIKYUU_TORIHIKI_KBN_CD.Text == "1")
            {
                this.RECEIPT_KBN_CD.Enabled = true;
                this.rb_RECEIPT_KBN_1.Enabled = true;
                this.rb_RECEIPT_KBN_2.Enabled = true;
                if (this.RECEIPT_KBN_CD.Text == "1")
                {
                    this.RECEIPT_KEISHOU_1.Enabled = true;
                    this.RECEIPT_KEISHOU_2.Enabled = true;
                    this.RECEIPT_TADASHIGAKI.Enabled = true;

                    string tadasigaki = Properties.Settings.Default.tadasigaki;
                    if (!string.IsNullOrEmpty(tadasigaki))
                    {
                        if (string.IsNullOrEmpty(this.RECEIPT_TADASHIGAKI.Text))
                        {   // データが設定されていない場合のみ前回値入れる
                            this.RECEIPT_TADASHIGAKI.Text = tadasigaki;
                        }
                    }

                }
            }
            else
            {
                this.RECEIPT_KBN_CD.Enabled = false;
                this.rb_RECEIPT_KBN_1.Enabled = false;
                this.rb_RECEIPT_KBN_2.Enabled = false;
                this.RECEIPT_KEISHOU_1.Enabled = false;
                this.RECEIPT_KEISHOU_2.Enabled = false;
                this.RECEIPT_TADASHIGAKI.Enabled = false;
            }
            if ((this.SEIKYUU_TORIHIKI_KBN_CD.Text != "2" || this.SHIHARAI_TORIHIKI_KBN_CD.Text != "2")
                && this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.CASHEIR_RENDOU_KBN_CD.Enabled = true;
                this.rb_CASHEIR_RENDOU_KBN_1.Enabled = true;
                this.rb_CASHEIR_RENDOU_KBN_2.Enabled = true;
            }
            else
            {
                this.CASHEIR_RENDOU_KBN_CD.Text = "2";
                this.CASHEIR_RENDOU_KBN_CD.Enabled = false;
                this.rb_CASHEIR_RENDOU_KBN_1.Enabled = false;
                this.rb_CASHEIR_RENDOU_KBN_2.Enabled = false;
            }

            if ((this.SEIKYUU_TORIHIKI_KBN_CD.Text != "2" && this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text == "2"))
            {
                this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = "1";
            }

            this.SetSeikyuuZeiKeisanKbnTable();
        }
        #endregion

        #region
        /// <summary>
        /// 取引区分(支払)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_TORIHIKI_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.torihikiKbnTable;
                form.PopupTitleLabel = "取引区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "取引区分CD", "取引区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.SHIHARAI_TORIHIKI_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.SHIHARAI_TORIHIKI_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 取引区分(支払)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_TORIHIKI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_TORIHIKI_KBN_NAME.Text = string.Empty;
            var rows = this.torihikiKbnTable.Select(string.Format("CD = '{0}'", SHIHARAI_TORIHIKI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SHIHARAI_TORIHIKI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SHIHARAI_TORIHIKI_KBN_CD.Text = string.Empty;
                this.SHIHARAI_TORIHIKI_KBN_NAME.Text = string.Empty;
            }
            if ((this.SEIKYUU_TORIHIKI_KBN_CD.Text != "2" || this.SHIHARAI_TORIHIKI_KBN_CD.Text != "2")
                && this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.CASHEIR_RENDOU_KBN_CD.Enabled = true;
                this.rb_CASHEIR_RENDOU_KBN_1.Enabled = true;
                this.rb_CASHEIR_RENDOU_KBN_2.Enabled = true;
            }
            else
            {
                this.CASHEIR_RENDOU_KBN_CD.Text = "2";
                this.CASHEIR_RENDOU_KBN_CD.Enabled = false;
                this.rb_CASHEIR_RENDOU_KBN_1.Enabled = false;
                this.rb_CASHEIR_RENDOU_KBN_2.Enabled = false;
            }

            if ((this.SHIHARAI_TORIHIKI_KBN_CD.Text != "2" && this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text == "2"))
            {
                this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = "1";
            }

            this.SetShiharaiZeiKeisanKbnTable();
        }

        /// <summary>
        /// 税計算区分（請求）テーブル設定
        /// </summary>
        private bool SetSeikyuuZeiKeisanKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "伝票毎";
                dt.Rows.Add(row);
                if (this.SEIKYUU_TORIHIKI_KBN_CD.Text == "2")
                {
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "請求毎";
                    dt.Rows.Add(row);
                }
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "明細毎";
                dt.Rows.Add(row);

                this.seikyuuZeiKeisanKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSeikyuuZeiKeisanKbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 取引区分(請求)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_ZEI_KEISAN_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.seikyuuZeiKeisanKbnTable;
                form.PopupTitleLabel = "税計算区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "税計算区分CD", "税計算区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 税計算区分(請求)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = string.Empty;
            var rows = this.seikyuuZeiKeisanKbnTable.Select(string.Format("CD = '{0}'", SEIKYUU_ZEI_KEISAN_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text = string.Empty;
                this.SEIKYUU_ZEI_KEISAN_KBN_NAME.Text = string.Empty;
            }

            this.SetSeikyuuZeiKbnTable();
        }

        /// <summary>
        /// 税計算区分（支払）テーブル設定
        /// </summary>
        private bool SetShiharaiZeiKeisanKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "伝票毎";
                dt.Rows.Add(row);
                if (this.SHIHARAI_TORIHIKI_KBN_CD.Text == "2")
                {
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "精算毎";
                    dt.Rows.Add(row);
                }
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "明細毎";
                dt.Rows.Add(row);

                this.shiharaiZeiKeisanKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetShiharaiZeiKeisanKbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 税計算区分(支払)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KEISAN_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.shiharaiZeiKeisanKbnTable;
                form.PopupTitleLabel = "税計算区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "税計算区分CD", "税計算区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 税計算区分(支払)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KEISAN_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = string.Empty;
            var rows = this.shiharaiZeiKeisanKbnTable.Select(string.Format("CD = '{0}'", SHIHARAI_ZEI_KEISAN_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text = string.Empty;
                this.SHIHARAI_ZEI_KEISAN_KBN_NAME.Text = string.Empty;
            }

            this.SetShiharaiZeiKbnTable();
        }
        #endregion

        #region
        /// <summary>
        /// 税区分テーブル設定
        /// </summary>
        private bool SetSeikyuuZeiKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "外税";
                dt.Rows.Add(row);
                if (this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text != "1" && this.SEIKYUU_ZEI_KEISAN_KBN_CD.Text != "2")
                {
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "内税";
                    dt.Rows.Add(row);
                    
                }
                else if (this.SEIKYUU_ZEI_KBN_CD.Text == "2")
                {
                    this.SEIKYUU_ZEI_KBN_CD.Text = "1";
                }
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "非課税";
                dt.Rows.Add(row);

                this.seikyuuZeiKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZeiKbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 税区分(請求)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_ZEI_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.seikyuuZeiKbnTable;
                form.PopupTitleLabel = "税区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "税区分CD", "税区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.SEIKYUU_ZEI_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.SEIKYUU_ZEI_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 税区分テーブル設定
        /// </summary>
        private bool SetShiharaiZeiKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "外税";
                dt.Rows.Add(row);
                if (this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text != "1" && this.SHIHARAI_ZEI_KEISAN_KBN_CD.Text != "2")
                {
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "内税";
                    dt.Rows.Add(row);
                }
                else if (this.SHIHARAI_ZEI_KBN_CD.Text == "2")
                {
                    this.SHIHARAI_ZEI_KBN_CD.Text = "1";
                }
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "非課税";
                dt.Rows.Add(row);

                this.shiharaiZeiKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZeiKbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 税区分(請求)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEIKYUU_ZEI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_ZEI_KBN_NAME.Text = string.Empty;
            var rows = this.seikyuuZeiKbnTable.Select(string.Format("CD = '{0}'", SEIKYUU_ZEI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SEIKYUU_ZEI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SEIKYUU_ZEI_KBN_CD.Text = string.Empty;
                this.SEIKYUU_ZEI_KBN_NAME.Text = string.Empty;
            }
        }
        
        /// <summary>
        /// 税区分(支払)ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.shiharaiZeiKbnTable;
                form.PopupTitleLabel = "税区分検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "税区分CD", "税区分名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.SHIHARAI_ZEI_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.SHIHARAI_ZEI_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 税区分(支払)検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_ZEI_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SHIHARAI_ZEI_KBN_NAME.Text = string.Empty;
            var rows = this.shiharaiZeiKbnTable.Select(string.Format("CD = '{0}'", SHIHARAI_ZEI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SHIHARAI_ZEI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SHIHARAI_ZEI_KBN_CD.Text = string.Empty;
                this.SHIHARAI_ZEI_KBN_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 領収書検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RECEIPT_KBN_CD_TextChanged(object sender, EventArgs e)
        {
            this.SEIKYUU_TORIHIKI_KBN_NAME.Text = string.Empty;
            var rows = this.torihikiKbnTable.Select(string.Format("CD = '{0}'", SEIKYUU_TORIHIKI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.SEIKYUU_TORIHIKI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.SEIKYUU_TORIHIKI_KBN_CD.Text = string.Empty;
                this.SEIKYUU_TORIHIKI_KBN_NAME.Text = string.Empty;
            }
            if (this.RECEIPT_KBN_CD.Text == "1" && this.SEIKYUU_TORIHIKI_KBN_CD.Text == "1")
            {
                this.RECEIPT_KEISHOU_1.Enabled = true;
                this.RECEIPT_KEISHOU_2.Enabled = true;
                this.RECEIPT_TADASHIGAKI.Enabled = true;

                string tadasigaki = Properties.Settings.Default.tadasigaki;
                if (!string.IsNullOrEmpty(tadasigaki))
                {
                    if (string.IsNullOrEmpty(this.RECEIPT_TADASHIGAKI.Text))
                    {   // データが設定されていない場合のみ前回値入れる
                        this.RECEIPT_TADASHIGAKI.Text = tadasigaki;
                    }
                }

            }
            else
            {
                this.RECEIPT_KEISHOU_1.Enabled = false;
                this.RECEIPT_KEISHOU_2.Enabled = false;
                this.RECEIPT_TADASHIGAKI.Enabled = false;
            }

            this.SetSeikyuuZeiKeisanKbnTable();
        }
        #endregion

        #region
        /// <summary>
        /// マニフェスト区分テーブル設定
        /// </summary>
        private bool SetManifestKbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "一次";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "二次";
                dt.Rows.Add(row);

                this.manifestKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManifestbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニ種類テーブル設定
        /// </summary>
        private bool SetManifestHaikikbnTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "産廃";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "建廃";
                dt.Rows.Add(row);
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "積替";
                dt.Rows.Add(row);

                this.manifesthaikiKbnTable = dt;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetManifestHaikikbnTable", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// マニ種類ポップアップ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANIFEST_HAIKI_KBN_CD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                form.table = this.manifesthaikiKbnTable;
                form.PopupTitleLabel = "マニ種類検索";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "マニ種類CD", "マニ種類名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.MANIFEST_HAIKI_KBN_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.MANIFEST_HAIKI_KBN_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
            }
        }

        /// <summary>
        /// マニ種類検証時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MANIFEST_HAIKI_KBN_CD_Validated(object sender, EventArgs e)
        {
            this.MANIFEST_HAIKI_KBN_NAME.Text = string.Empty;
            var rows = this.manifesthaikiKbnTable.Select(string.Format("CD = '{0}'", MANIFEST_HAIKI_KBN_CD.Text));
            if (rows.Length > 0)
            {
                this.MANIFEST_HAIKI_KBN_NAME.Text = Convert.ToString(rows[0]["VALUE"]);
            }
            else
            {
                this.MANIFEST_HAIKI_KBN_CD.Text = string.Empty;
                this.MANIFEST_HAIKI_KBN_NAME.Text = string.Empty;
            }
        }

        private void STACK_KEIRYOU_TIME_Enter(object sender, EventArgs e)
        {
            this.STACK_KEIRYOU_TIME.Text = this.STACK_KEIRYOU_TIME.Text.Replace(":", "");
        }
        #endregion

        private void STACK_KEIRYOU_TIME_Validated(object sender, EventArgs e)
        {
            bool result = this.logic.IsTimeChkOK(this.STACK_KEIRYOU_TIME);
            if (!result)
            {
                this.STACK_KEIRYOU_TIME.Focus();
                return;
            }
            else
            {
                this.STACK_KEIRYOU_TIME.UpdateBackColor(false);
            }
        }

        private void EMPTY_KEIRYOU_TIME_Enter(object sender, EventArgs e)
        {
            this.EMPTY_KEIRYOU_TIME.Text = this.EMPTY_KEIRYOU_TIME.Text.Replace(":", "");
        }

        private void EMPTY_KEIRYOU_TIME_Validated(object sender, EventArgs e)
        {
            bool result = this.logic.IsTimeChkOK(this.EMPTY_KEIRYOU_TIME);
            if (!result)
            {
                this.EMPTY_KEIRYOU_TIME.Focus();
                return;
            }
            else
            {
                this.EMPTY_KEIRYOU_TIME.UpdateBackColor(false);
            }
        }

        private void tairyuuIchiran_DoubleClick(object sender, EventArgs e)
        {
            this.nowLoding = true;
            this.logic.ShowDenpyou();
            this.nowLoding = false;
        }

        private void STACK_JYUURYOU_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(STACK_JYUURYOU.Text) && string.IsNullOrEmpty(this.STACK_KEIRYOU_TIME.Text))
            {
                bool catchErr = true;
                this.STACK_KEIRYOU_TIME.Text = this.logic.GetDate(out catchErr);
            }
        }

        private void EMPTY_JYUURYOU_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EMPTY_JYUURYOU.Text) && string.IsNullOrEmpty(this.EMPTY_KEIRYOU_TIME.Text))
            {
                bool catchErr = true;
                this.EMPTY_KEIRYOU_TIME.Text = this.logic.GetDate(out catchErr);
            }
        }

        ///////////////////////////////////////////////////////////////
        //受入実績
        ///////////////////////////////////////////////////////////////
        private void gcMultiRow2_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            // Dictionaryへの追加
            //this.logic.AddRowDic(e.RowIndex);
        }
        private void gcMultiRow2_RowsRemoving(object sender, RowsRemovingEventArgs e)
        {
            // Dictionaryからの削除
            //this.logic.RemoveRowDic(e.RowIndex);
        }

        internal string hinmeiCd2 = "";
        internal string hinmeiName2 = "";
        public void HINMEI_CD_PopupBeforeExecuteMethod2()
        {
            this.hinmeiCd2 = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            this.hinmeiName2 = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value);
        }
        public void HINMEI_CD_PopupAfterExecuteMethod2()
        {
            if (this.hinmeiCd2 == Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value) && this.hinmeiName2 == Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value))
            {
                return;
            }
            this.logic.GetHinmeiForPop2(this.gcMultiRow2.CurrentRow);
            if (beforeValuesForJisseki.ContainsKey(LogicClass.CELL_NAME_HINMEI_CD))
            {
                beforeValuesForJisseki[LogicClass.CELL_NAME_HINMEI_CD] = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            }
        }

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Row row = this.gcMultiRow2.CurrentRow;

            if (row == null)
            {
                return;
            }

            // 前回値チェック用データをセット
            if (beforeValuesForJisseki.ContainsKey(e.CellName))
            {
                beforeValuesForJisseki[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
            }
            else
            {
                beforeValuesForJisseki.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各CELLの更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_CellValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 品名CDは前回値との比較を行わない
            if (e.CellName != LogicClass.CELL_NAME_HINMEI_CD)
            {
                // 前回値と変更が無かったら処理中断
                if (beforeValuesForJisseki.ContainsKey(e.CellName) &&
                    beforeValuesForJisseki[e.CellName].Equals(Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value)))
                {
                    return;
                }
            }

            if (editingMultiRowFlag == false)
            {
                editingMultiRowFlag = true;

                switch (e.CellName)
                {
                    case LogicClass.CELL_NAME_HINMEI_CD:
                        // 品名をセット
                        if (!this.logic.SetHinmeiName2(this.gcMultiRow2.CurrentRow))
                        {
                            return;
                        }
                        break;
                    default:
                        break;
                }

                // 毎回合計系計算を実行する
                if (!this.logic.CalcTotalValues2())
                {
                    return;
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
        private void gcMultiRow2_CellValidating(object sender, CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            switch (e.CellName)
            {
                case LogicClass.CELL_NAME_HINMEI_NAME:
                    bool catchErr = true;
                    bool retChousei = this.logic.ValidateHinmeiName2(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;
                case LogicClass.CELL_NAME_HINMEI_CD:
                    if (beforeValuesForJisseki.ContainsKey(e.CellName)
                        && beforeValuesForJisseki[e.CellName].Equals(
                            Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value)) && !this.isInputError)
                    {
                    }
                    else
                    {
                        this.isInputError = false;
                        var value = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value);
                        if (string.IsNullOrEmpty(value))
                        {
                            this.gcMultiRow2.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = "";
                        }
                        catchErr = true;
                        retChousei = this.logic.GetHinmei2(this.gcMultiRow2.CurrentRow, out catchErr);
                        if (!catchErr)
                        {
                            this.isInputError = true;
                            e.Cancel = true;
                            return;
                        }
                        if (retChousei)
                        {
                            this.logic.msgLogic.MessageBoxShow("E020", "品名");
                            this.isInputError = true;
                            e.Cancel = true;
                            return;
                        }
                    }

                    // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                    catchErr = true;
                    retChousei = this.logic.CheckHinmeiCd2(this.gcMultiRow2.CurrentRow, out catchErr);
                    if (!catchErr)
                    {
                        this.isInputError = true;
                        e.Cancel = true;
                        return;
                    }
                    if (!retChousei)
                    {
                        return;
                    }

                    // 空だったら処理中断
                    this.gcMultiRow2.BeginEdit(false);
                    this.gcMultiRow2.EndEdit();
                    this.gcMultiRow2.NotifyCurrentCellDirty(false);
                    if (string.IsNullOrEmpty((string)this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value))
                    {
                        return;
                    }

                    var targetRow = this.gcMultiRow2.CurrentRow;
                    if (targetRow != null)
                    {
                        GcCustomTextBoxCell control = (GcCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                        if (control.TextBoxChanged == true
                            || (beforeValuesForJisseki.ContainsKey(e.CellName) && !beforeValuesForJisseki[e.CellName].Equals(Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value)))
                            )
                        {
                            control.TextBoxChanged = false;
                        }
                    }

                    // 合計系計算
                    if (!this.logic.CalcTotalValues2())
                    {
                        return;
                    }
                    // 前回値チェック用データをセット
                    if (beforeValuesForJisseki.ContainsKey(e.CellName))
                    {
                        beforeValuesForJisseki[e.CellName] = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value);
                    }
                    else
                    {
                        beforeValuesForJisseki.Add(e.CellName, Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[e.CellName].Value));
                    }

                    break;

                 default:
                    break;
            }

            LogUtility.DebugMethodEnd();

        }
        /// <summary>
        /// ROW選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_RowEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // マニフェスト連携用のデータを設定
            Row row = this.gcMultiRow2.CurrentRow;
            Cell systemId = row.Cells[LogicClass.CELL_NAME_DENPYOU_SYSTEM_ID];
            Cell JissekiSeq = row.Cells[LogicClass.CELL_NAME_JISSEKI_SEQ];
            long renkeisysId = -1;
            long renkeiJissekiSeq = -1;

            if (systemId.Value != null
                && !string.IsNullOrEmpty(Convert.ToString(systemId.Value)))
            {
                if (long.TryParse(Convert.ToString(systemId.Value), out renkeisysId))
                {
                    this.RenkeiSystemId = renkeisysId;
                }
            }

            if (JissekiSeq.Value != null
                && !string.IsNullOrEmpty(Convert.ToString(JissekiSeq.Value)))
            {
                if (long.TryParse(Convert.ToString(JissekiSeq.Value), out renkeiJissekiSeq))
                {
                    this.RenkeiJissekiSeq = renkeiJissekiSeq;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細の行移動処理
        /// 明細の行が増減するたびに必ず実行してください
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_RowLeave(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (editingMultiRowFlag)
            {
                return;
            }
            editingMultiRowFlag = true;
            // ROW_NOを採番
            this.notEditingOperationFlg = true;
            if (!this.logic.NumberingRowNo2())
            {
                return;
            }

            this.notEditingOperationFlg = false;
            editingMultiRowFlag = false;
            LogUtility.DebugMethodStart(sender, e);
        }

        /// <summary>
        /// 明細行の項目入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckDetailColumn2()
        {
            decimal netTotal = 0;
            int rowNo = 0;
            int rowH = 0;
            int rowW = 0;
            int selectRow = 0;
            string MessageJ = string.Empty;

            foreach (Row row in this.gcMultiRow2.Rows)
            {
                if (!row.IsNewRow)
                {
                    rowNo = rowNo + 1;

                    if (row.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value == null
                        || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        if (!(row.Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value == null
                            || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value.ToString())))
                        {
                            rowH = rowH + 1;
                            var cellhinmeiCD = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                            cellhinmeiCD.IsInputErrorOccured = true;
                        }
                    }
                    else if (row.Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value == null
                        || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value.ToString()))
                    {
                        //if (!(row.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value == null
                        //    || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value.ToString())))
                        //{
                        //    rowW = rowW + 1;
                        //    var cellwariaiCD = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI];
                        //    cellwariaiCD.IsInputErrorOccured = true;
                        //}
                    }
                    else
                    {
                        decimal netSuuryouWariai = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value), out netSuuryouWariai);
                        // 数量割合
                        netTotal += netSuuryouWariai;
                        //行番号取得
                        selectRow = rowNo - 1;
                    }
                }
            }

            if (rowH > 0)
            {
                MessageJ = "品目CDは必須項目です。入力してください。\n";
            }
            //if (rowW > 0)
            //{
            //    MessageJ = MessageJ + "数量割合は必須項目です。入力してください。\n";
            //}
            if (rowNo > 0 && !(netTotal.Equals(100)))
            {
                //合計が100以外か
                MessageJ = MessageJ + "数量割合の合計が100％になるように入力してください。\n";
            }

            if (MessageJ.Length > 0)
            {
                this.DETAIL_TAB.SelectedIndex = 1;
                this.logic.msgLogic.MessageBoxShowError(MessageJ);
                return false;
            }

            //不要明細を下から削除
            this.gcMultiRow2.Focus();
            this.gcMultiRow2.Rows[selectRow].Selected = true;
            int maxRow = this.gcMultiRow2.RowCount - 1;

            for (int i = maxRow; i >= 0; i--)
            {
                if (!this.gcMultiRow2.Rows[i].IsNewRow)
                {
                    if (this.gcMultiRow2.Rows[i].Cells[LogicClass.CELL_NAME_HINMEI_CD].Value == null
                        || string.IsNullOrEmpty(this.gcMultiRow2.Rows[i].Cells[LogicClass.CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        if (this.gcMultiRow2.Rows[i].Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value == null
                            || string.IsNullOrEmpty(this.gcMultiRow2.Rows[i].Cells[LogicClass.CELL_NAME_SUURYOU_WARIAI].Value.ToString()))
                        {
                            this.gcMultiRow2.Rows.RemoveAt(i);
                        }
                    }
                }
            }
            this.logic.NumberingRowNo2();

            return true;
        }

        /// <summary>
        /// 作業開始_時の入力後チェック
        /// 時が空でなく分が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_HOUR_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!string.IsNullOrEmpty(this.SAGYOU_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_MINUTE.Text))
                {
                    this.SAGYOU_MINUTE.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 作業開始_分の入力後チェック
        /// 分が空でなく時が空だった場合、分に0をセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_MINUTE_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (string.IsNullOrEmpty(this.SAGYOU_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_MINUTE.Text))
                {
                    this.SAGYOU_HOUR.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        private void SAGYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckSagyousha())
            {
                return;
            }
        }

        /// <summary>
        /// 添付ファイルの更新ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TENPU_FILE_UPDATE_BTN_Click(object sender, EventArgs e)
        {
            // 添付ファイル一覧をクリアする。
            this.dgvTenpuFileDetail.Rows.Clear();

            // 添付ファイルデータを再取得する。
            this.logic.GetFileData();

            // 添付ファイル一覧に設定する。
            if (this.logic.dto.fileDataList.Count() > 0)
            {
                for (int i = 0; i < this.logic.dto.fileDataList.Count(); i++)
                {
                    if (this.logic.dto.fileDataList[i].FILE_ID.IsNull)
                    {
                        continue;
                    }

                    this.dgvTenpuFileDetail.Rows.Add();
                    this.dgvTenpuFileDetail.Rows[i].Cells["TENPU_FILE_NAME"].Value = this.logic.dto.fileDataList[i].FILE_PATH;
                    this.dgvTenpuFileDetail.Rows[i].Cells["HIDDEN_FILEID"].Value = this.logic.dto.fileDataList[i].FILE_ID;
                }
            }
        }

        /// <summary>
        /// 各CELLのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 閲覧ボタンをクリックする時
                if (this.dgvTenpuFileDetail.Columns[e.ColumnIndex].Name.Equals("ETSURAN"))
                {
                    // PDFを閲覧する。
                    this.logic.ExecEtsuran();
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }
    }
}