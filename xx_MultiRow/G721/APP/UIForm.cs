// $Id$
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.ContenaShitei.DTO;
using Shougun.Core.Common.ContenaShitei.Utility;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Core.Scale.Keiryou.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using System.Drawing;
using System.Runtime.InteropServices;
using Shougun.Function.ShougunCSCommon.Utility;
using MasterKyoutsuPopup2.APP;
using System.Data;
using r_framework.Dao;
using Shougun.Core.SalesPayment.TankaRirekiIchiran;

using ShainHoshu.Logic;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku2
{
    /// <summary>
    /// 受入入力
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
        /// 受入入力のSYSTE_ID
        /// </summary>
        public long UkeireSysId = -1;

        /// <summary>
        /// 受入入力のSEQ
        /// </summary>
        public int UkeireSEQ = -1;

        /// <summary>
        /// 受入明細のSYSTE_ID
        /// </summary>
        public long MeisaiSysId = -1;

        /// <summary>
        /// 受入番号
        /// </summary>
        public long UkeireNumber = -1;

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
        /// 受付番号
        /// </summary>
        public long UketsukeNumber = -1;

        /// <summary>
        /// 計量番号
        /// </summary>
        public long KeiryouNumber = -1;

        /// <summary>
        /// 受付番号テキストチェンジフラグ
        /// </summary>
        internal bool UketsukeNumberTextChangeFlg = false;

        /// <summary>
        /// この画面が呼び出された時、受付番号が引数に存在したかを示します
        /// /// </summary>
        internal bool isArgumentUketsukeNumber = false;

        /// <summary>
        /// 計量番号テキストチェンジフラグ
        /// </summary>
        internal bool KeiryouNumberTextChangeFlg = false;

        /// <summary>
        /// この画面が呼び出された時、計量番号が引数に存在したかを示します
        /// /// </summary>
        internal bool isArgumentKeiryouNumber = false;

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

        private bool uriageDateVisible;
        /// <summary>売上日付コントロールのVisible</summary>
        /// <remarks>請求支払タブ以外を開いているときに値が取れない対策</remarks>
        internal bool UriageDateVisible
        {
            get { return this.uriageDateVisible; }
            set
            {
                this.uriageDateVisible = value;
                this.URIAGE_DATE.Visible = value;
            }
        }

        private bool shiharaiDateVisible;
        /// <summary>支払日付コントロールのVisible</summary>
        /// <remarks>請求支払タブ以外を開いているときに値が取れない対策</remarks>
        internal bool ShiharaiDateVisible
        {
            get { return this.shiharaiDateVisible; }
            set
            {
                this.shiharaiDateVisible = value;
                this.SHIHARAI_DATE.Visible = value;
            }
        }

        #endregion

        public bool IsDataLoading { get; set; }

        internal bool validateFlag = false;

        internal bool isInputError = false;

        internal Row row;

        internal bool ismobile_mode = false;

        internal bool isfile_upload = false;

        #region 初期化処理
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private UIHeaderForm header;

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;

        /// <summary>
        /// 品名ポップアップから品名が選択されたかどうか判断するためのフラグ
        /// </summary>
        internal bool bSelectHinmeiPopup = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        internal bool hinnmeiFocusFlg = false;

        //PhuocLoc 2020/12/01 #136219 -Start
        private string beforeSharyo = string.Empty;
        private string beforeUnpan = string.Empty;
        //PhuocLoc 2020/12/01 #136219 -End

        //20211231 Thanh 158923 s
        internal Int16 WarifuriInputMode = 0;//0:None, 1:kg 2:%
        internal Int16 ChoiseiInputMode = 0;//0:None, 1:kg 2:%

        //20211231 Thanh 158923 e
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="windowType">モード</param>
        /// <param name="ukeireNumber">受入入力 UKEIRE_NUMBER</param>
        /// <param name="lastRunMethod">受入入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, long ukeireNumber = -1, LastRunMethod lastRunMethod = null, long uketsukeNumber = -1
            , long keiryouNumber = -1, bool keizokuKeiryouFlg = false, bool newChangeFlg = false, string SEQ = "0")
            : base(WINDOW_ID.T_UKEIRE, windowType)
        {
            LogUtility.DebugMethodStart(windowId, windowType, ukeireNumber, lastRunMethod, uketsukeNumber, keiryouNumber, keizokuKeiryouFlg, newChangeFlg, SEQ);

            CommonShogunData.Create(SystemProperty.Shain.CD);

            TairyuuNewFlg = newChangeFlg;   // No.2334

            this.InitializeComponent();

            // 時間コンボボックスのItemsをセット
            this.SAGYOU_HOUR.SetItems();
            this.SAGYOU_MINUTE.SetItems(1);

            this.WindowId = windowId;
            this.WindowType = windowType;
            this.UkeireNumber = ukeireNumber;
            this.closeMethod = lastRunMethod;
            if (uketsukeNumber != -1)
            {
                this.isArgumentUketsukeNumber = true;
            }
            this.UketsukeNumber = uketsukeNumber;
            if (keiryouNumber != -1)
            {
                this.isArgumentKeiryouNumber = true;
            }
            this.KeiryouNumber = keiryouNumber;
            this.KeizokuKeiryouFlg = keizokuKeiryouFlg;
            if (string.IsNullOrEmpty(SEQ))
            {
                SEQ = "0";
            }
            this.SEQ = SEQ;
            this.uriageDateVisible = this.URIAGE_DATE.Visible;
            this.shiharaiDateVisible = this.SHIHARAI_DATE.Visible;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.header = new UIHeaderForm(this);

            // マニフェスト連携用変数の初期化
            RenkeiDenshuKbnCd = (short)SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE;
            RenkeiSystemId = -1;
            RenkeiMeisaiSystemId = -1;
            RenkeiJissekiSeq = -1;

            LogUtility.DebugMethodEnd(windowType, windowType, ukeireNumber, lastRunMethod, uketsukeNumber, keiryouNumber, keizokuKeiryouFlg, newChangeFlg, SEQ);
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

            //PhuocLoc 2020/05/20 #137147 -Start
            this.ParentBaseForm.Controls.Add(this.rirekeIchiran);
            this.ParentBaseForm.inForm.SendToBack();

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
            //PhuocLoc 2020/05/20 #137147 -End

            //PhuocLoc 2020/12/01 #136219 -Start
            if (this.dgvTairyuuDetail != null)
            {
                this.dgvTairyuuDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            //PhuocLoc 2020/12/01 #136219 -End

            if (this.JISSEKI_TAB != null)
            {
                this.JISSEKI_TAB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            if (this.dgvTenpuFileDetail != null)
            {
                this.dgvTenpuFileDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }

            //重量値表示プロセス起動
            truckScaleWeight1.ProcessWeight();

            long tempUkeireNumber = this.UkeireNumber;
            long tempUketsukeNumber = this.UketsukeNumber;
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

            this.UkeireNumber = tempUkeireNumber;
            this.UketsukeNumber = tempUketsukeNumber;

            if (!isOpenFormError)
            {
                base.CloseTopForm();
            }
            //受付番号と受入番号判定
            if (this.UkeireNumber == -1 && this.UketsukeNumber != -1)
            {
                //base.OnLoad(e);
                catchErr = true;
                retDate = this.logic.GetAllEntityData(out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (!retDate)
                {
                    return;
                }
                ((UIHeaderForm)this.logic.footerForm.headerForm).Text = this.UketsukeNumber.ToString();
                if (!this.logic.GetUketsukeNumber())
                {
                    return;
                }

                // 初期フォーカス位置
                ((UIHeaderForm)this.logic.footerForm.headerForm).Focus();
            }

            // 継続入力の初期値を設定
            // システム設定値がない場合は、「2:しない」を初期値とする
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            var keizokuNyuuryoku = userProfile.Settings.DefaultValue.Where(v => v.Name == "継続入力").Select(v => v.Value).DefaultIfEmpty("2").FirstOrDefault();
            this.KEIZOKU_NYUURYOKU_VALUE.Text = keizokuNyuuryoku;

            if (!isOpenFormError)
            {
                base.CloseTopForm();
            }
        }

        /// <summary>
        /// 初回表出イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
            this.logic.SetTopControlFocus();

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
        /// 受入番号更新後処理
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
                long ukeireNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text)
                    && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out ukeireNumber))
                {
                    if (this.UkeireNumber != ukeireNumber)  // No.2175
                    {
                        this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        this.UkeireNumber = ukeireNumber;

                        bool catchErr = true;
                        bool retDate = this.logic.GetAllEntityData(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (!retDate)
                        {
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.UkeireNumber = -1;

                            // 再描画を有効にして最新の状態に更新
                            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));

                            this.ENTRY_NUMBER.Focus();
                            nowLoding = false;
                            return;
                        }

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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

        /// <summary>変更前売上日付</summary>
        private string beforeUrageDate = string.Empty;
        /// <summary>変更前支払日付</summary>
        private string beforeShiharaiDate = string.Empty;

        /// <summary>
        /// 売上日付Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_DATE_Enter(object sender, EventArgs e)
        {
            this.beforeUrageDate = this.URIAGE_DATE.Text;
        }

        /// <summary>
        /// 支払日付Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_DATE_Enter(object sender, EventArgs e)
        {
            this.beforeShiharaiDate = this.SHIHARAI_DATE.Text;
        }

        /// <summary>
        /// 売上日付更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void URIAGE_DATE_OnLeave(object sender, EventArgs e)
        {
            this.ActiveControl = this.ActiveControl;    // 一桁入力された場合にここで値を確定
            if (!beforeUrageDate.Equals(this.URIAGE_DATE.Text))
            {
                this.logic.SetUriageShouhizeiRate();        // 売上消費税率設定
            }
        }

        /// <summary>
        /// 支払日付更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SHIHARAI_DATE_OnLeave(object sender, EventArgs e)
        {
            this.ActiveControl = this.ActiveControl;    // 一桁入力された場合にここで値を確定
            if (!beforeShiharaiDate.Equals(this.SHIHARAI_DATE.Text))
            {
                this.logic.SetShiharaiShouhizeiRate();
            }
        }

        /// <summary>
        /// 運搬業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット
            beforeUnpan = this.UNPAN_GYOUSHA_CD.Text; //PhuocLoc 2020/12/01 #136219

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

            this.logic.RirekeDisplay();

            this.isNotMoveFocus = this.SetUnpanGyousha();

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            //PhuocLoc 2020/12/01 #136219 -Start
            if (beforeUnpan != this.UNPAN_GYOUSHA_CD.Text)
            {
            	//PhuocLoc 2021/03/12 #148086 -Start
                if (!string.IsNullOrEmpty(this.logic.TairyuHyoujiKbn) && this.logic.TairyuHyoujiKbn != Const.TAIRYU_ICHIRAN_HIDDEN
                    && (this.DETAIL_TAB.TabPages.Contains(this.TAIRYU_LIST_TAB) == true))
                {
                    this.logic.SharyouFocusOut();
                }
                //PhuocLoc 2021/03/12 #148086 -End
            }
            //PhuocLoc 2020/12/01 #136219 -End

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

            // 業者CD＝未入力　かつ　車輌名＝入力済み
            if(string.IsNullOrEmpty(this.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.SHARYOU_NAME_RYAKU.Text))
            {
                this.logic.GyoushaCursorInRirekeShow();
            }
        }

        /// <summary>
        /// 業者前回値保存
        /// </summary>
        public void gyoushaEnter()
        {
            if (!this.isInputError)
            {
                this.logic.EnterGyousyaCdSet();

                if (!this.isFromSearchButton)
                {
                    this.logic.GyousyaCdSet();  //比較用業者CDをセット
                }
            }
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GYOUSHA_CD_Validated(object sender, EventArgs e)
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
        /// 車輌フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            if (!this.editingSharyouCdFlag)
            {
                this.logic.ShayouCdSet();   // 比較用車輌CDをセット
                beforeSharyo = this.SHARYOU_CD.Text; //PhuocLoc 2020/12/01 #136219
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

            this.logic.RirekeDisplay();

            if (!this.logic.CheckSharyou())
            {
                return;
            }

            if (String.IsNullOrEmpty(this.KUUSHA_JYURYO.Text))
            {
                this.logic.footerForm.bt_func8.Enabled = false;
            }
            else
            {
                this.logic.footerForm.bt_func8.Enabled = true;
            }

            //PhuocLoc 2020/12/01 #136219 -Start
            if (beforeSharyo != this.SHARYOU_CD.Text)
            {
            	//PhuocLoc 2021/03/12 #148086 -Start
                if (!string.IsNullOrEmpty(this.logic.TairyuHyoujiKbn) && this.logic.TairyuHyoujiKbn != Const.TAIRYU_ICHIRAN_HIDDEN
                    && (this.DETAIL_TAB.TabPages.Contains(this.TAIRYU_LIST_TAB) == true))
                {
                    this.logic.SharyouFocusOut();
                }
                //PhuocLoc 2021/03/12 #148086 -End
            }
            //PhuocLoc 2020/12/01 #136219 -End

            this.editingSharyouCdFlag = false;
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
        /// 形態区分再検索処理
        /// </summary>
        public void KEITAI_KBN_CD_Research()
        {
            // 形態区分選択ポップアップ用DataSource再生成
            this.KEITAI_KBN_CD.PopupDataSource = this.logic.CreateKeitaiKbnPopupDataSource();
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
                this.beforeUrageDate = this.URIAGE_DATE.Text;
                this.beforeShiharaiDate = this.SHIHARAI_DATE.Text;

                this.URIAGE_DATE.Value = this.DENPYOU_DATE.Value;
                this.SHIHARAI_DATE.Value = this.DENPYOU_DATE.Value;
                this.URIAGE_DATE_OnLeave(sender, e);
                this.SHIHARAI_DATE_OnLeave(sender, e);
            }

            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 受付番号前ボタンクリック処理
        /// 現在入力されている番号の前の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void previousButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            this.GetUkeireDataForPreOrNextButton(true, sender, e);

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            if (!this.logic.SetTopControlFocus())
            {
                return;
            }
        }

        /// <summary>
        /// 受付番号後ボタンクリック処理
        /// 現在入力されている番号の後の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void nextButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            this.GetUkeireDataForPreOrNextButton(false, sender, e);

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            this.logic.SetTopControlFocus();
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

            bool isChangeNewModeFlg = true;

            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                || this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                DialogResult dr = this.logic.msgLogic.MessageBoxShow("C108");
                if (dr != DialogResult.OK && dr != DialogResult.Yes)
                {
                    isChangeNewModeFlg = false;
                }
            }

            if (isChangeNewModeFlg)
            {
                // ウィンドウの再描画を無効にする
                SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

                // 追加権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.InitNumbers();
                    this.SEQ = "0";

                    this.KUUSHA_JYURYO.Text = String.Empty;

                    // Entity等初期化
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
                this.logic.SetTopControlFocus();

            }
            LogUtility.DebugMethodEnd();
        }

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
                if (this.UkeireNumber > 0)
                {
                    this.TairyuuNewFlg = true;

                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.UketsukeNumber = -1;
                    this.KeiryouNumber = -1;
                    this.ENTRY_NUMBER.Text = this.UkeireNumber.ToString();

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
                    // 本メソッドは呼び出されることはないが、念のため初期フォーカス位置を設定
                    this.logic.SetTopControlFocus();

                    this.TairyuuNewFlg = false;
                }
                nowLoding = false;
            }

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

            FormManager.OpenFormWithAuth("G055", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.UKEIRE, CommonShogunData.LOGIN_USER_INFO.SHAIN_CD);

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

            if (this.DETAIL_TAB.SelectedTab != this.MEISAI_PAGE)
            {
                this.logic.msgLogic.MessageBoxShow("E275", "明細タブ", "[F8]車輌空車取込を実行");
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

            var lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();
            if (null != lastRow)
            {
                var isInput = false;

                var sharyouEmptyJyuuryou = this.KUUSHA_JYURYO.Text;
                var stackJyuuryou = lastRow.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value;
                var emptyJyuuryou = lastRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value;
                var warifuriJyuuryou = lastRow.Cells[Const.CELL_NAME_WARIFURI_JYUURYOU].Value;

                bool catchErr = true;
                if (null == emptyJyuuryou || String.IsNullOrEmpty(emptyJyuuryou.ToString()))
                {
                    this.EMPTY_JYUURYOU.Text = this.KUUSHA_JYURYO.Text;
                    this.EMPTY_KEIRYOU_TIME.Text = this.logic.GetDate(out catchErr);
                }

                if(!string.IsNullOrEmpty(this.STACK_JYUURYOU.Text) && !string.IsNullOrEmpty(this.EMPTY_JYUURYOU.Text))
                {
                    this.NET_TOTAL.Text = Convert.ToString(Convert.ToDecimal(this.STACK_JYUURYOU.Text) - Convert.ToDecimal(this.EMPTY_JYUURYOU.Text));
                }

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
                        lastRow.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value = emptyJyuuryou;
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
                    var emptyJyuuryouRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false && (r.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value != null && !String.IsNullOrEmpty(r.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value.ToString()))).LastOrDefault();
                    if (null != prevRow)
                    {
                        var prevRowEmptyJyuuryou = prevRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value;
                        var targetEmptyJyuuryou = String.Empty;
                        if (null != emptyJyuuryouRow && null != emptyJyuuryouRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value)
                        {
                            targetEmptyJyuuryou = emptyJyuuryouRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value.ToString();
                        }

                        // 最終行が
                        //         「割り振り行ではない」かつ
                        //         「総重量がブランク」かつ
                        //         「空車重量がブランク」かつ
                        //         「1つ上の行の空車重量が入力されている」かつ
                        //         「1つ上の行の空車重量が車輌の空車重量と一致していない」の場合、空車重量をセットする
                        if ((null == warifuriJyuuryou || String.IsNullOrEmpty(warifuriJyuuryou.ToString())) &&
                            (null == stackJyuuryou || String.IsNullOrEmpty(stackJyuuryou.ToString())) &&
                            (null == emptyJyuuryou || String.IsNullOrEmpty(emptyJyuuryou.ToString())) &&
                            null != prevRowEmptyJyuuryou && !String.IsNullOrEmpty(prevRowEmptyJyuuryou.ToString()) &&
                            0 != Decimal.Parse(prevRowEmptyJyuuryou.ToString()).CompareTo(Decimal.Parse(sharyouEmptyJyuuryou))
                        )
                        {
                            isInput = true;

                            // このとき、1つ上の空車重量を最終行の総重量にコピーする
                            lastRow.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value = targetEmptyJyuuryou;
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
                            lastRow.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value = targetEmptyJyuuryou;
                        }

                        // 最終行が
                        //         「割り振り行」かつ
                        //         「空車重量が入力されている」かつ
                        //         「空車重量と車輌の空車重量が一致していない」の場合、新規行を追加し、空車重量をセットする
                        if (null != warifuriJyuuryou && !String.IsNullOrEmpty(warifuriJyuuryou.ToString()) &&
                            !String.IsNullOrEmpty(targetEmptyJyuuryou) &&
                            0 != Decimal.Parse(targetEmptyJyuuryou).CompareTo(Decimal.Parse(sharyouEmptyJyuuryou)))
                        {
                            this.gcMultiRow1.Rows.Add();

                            lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();

                            isInput = true;

                            // このとき、1つ上の空車重量を最終行の総重量にコピーする
                            lastRow.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value = targetEmptyJyuuryou;
                        }
                    }
                }

                if (isInput)
                {
                    lastRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value = sharyouEmptyJyuuryou;

                    // 再計算
                    // 数量にフォーカスがある場合、数量計算に不都合があるので一度フォーカスを外す。
                    if (this.gcMultiRow1.CurrentCell != null && this.gcMultiRow1.CurrentCell.Name.Equals(Const.CELL_NAME_SUURYOU))
                    {
                        this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(this.gcMultiRow1.CurrentRow.Index, Const.CELL_NAME_ROW_NO);
                    }
                    this.gcMultiRow1.ClearSelection();
                    this.gcMultiRow1.AddSelection(lastRow.Index);
                    this.beforeValuesForDetail[Const.CELL_NAME_EMPTY_JYUURYOU] = String.Empty;
                    this.gcMultiRow1_CellValidated(
                        lastRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU],
                        new CellEventArgs(lastRow.Index, lastRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].CellIndex, Const.CELL_NAME_EMPTY_JYUURYOU)
                        );
                }
            }

            // 処理前のコントロールにフォーカスを戻す
            if (!string.IsNullOrEmpty(beforCtrlName))
            {
                if (beforCtrlName == this.gcMultiRow1.Name)
                {
                    if (this.gcMultiRow1[int.Parse(beforeRowIndex), int.Parse(beforeCellIndex)].Visible)
                    {
                        this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(this.gcMultiRow1.CurrentRow.Index, Const.CELL_NAME_ROW_NO);
                        this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(int.Parse(beforeRowIndex), int.Parse(beforeCellIndex));
                    }
                }
                else if (this.Contains(this.Controls[beforCtrlName]))
                {
                    this.Controls[beforCtrlName].Focus();
                }
                else if (((UIHeaderForm)this.logic.footerForm.headerForm).Contains(((UIHeaderForm)this.logic.footerForm.headerForm).Controls[beforCtrlName]))
                {
                    (((UIHeaderForm)this.logic.footerForm.headerForm).Controls[beforCtrlName]).Focus();
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

            if (!this.logic.IsTimeChk(this.STACK_KEIRYOU_TIME))
            {
                return;
            }

            if (!this.logic.IsTimeChk(this.EMPTY_KEIRYOU_TIME))
            {
                return;
            }

            bool isKotaiKanri = false;
            if (this.logic.dto.sysInfoEntity != null
                && !this.logic.dto.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                && (int)this.logic.dto.sysInfoEntity.CONTENA_KANRI_HOUHOU == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                isKotaiKanri = true;
            }

            var contenaShiteiUtil = new ContenaShiteiUtility();
            long sysId = -1;
            int seq = -1;
            if (this.TairyuuNewFlg)
            {
                sysId = (long)this.logic.beforDto.entryEntity.SYSTEM_ID;
                seq = (int)this.logic.beforDto.entryEntity.SEQ;
            }

            /**
             * 設置、引揚可能チェック
             */
            List<UtilityDto> denpyouInfoList = new List<UtilityDto>();
            UtilityDto ukeireInfo = new UtilityDto();
            ukeireInfo.SysId = sysId;
            ukeireInfo.Seq = seq;
            ukeireInfo.DenshuKbn = (int)DENSHU_KBN.UKEIRE;
            denpyouInfoList.Add(ukeireInfo);

            if (this.logic.tUketsukeSsEntry != null)
            {
                UtilityDto uketsukeInfo = new UtilityDto();
                uketsukeInfo.SysId = (long)this.logic.tUketsukeSsEntry.SYSTEM_ID;
                uketsukeInfo.Seq = (int)this.logic.tUketsukeSsEntry.SEQ;
                uketsukeInfo.DenshuKbn = (int)DENSHU_KBN.UKETSUKE;
                denpyouInfoList.Add(uketsukeInfo);
            }

            if (!base.RegistErrorFlag
                && isKotaiKanri
                && !contenaShiteiUtil.CheckContenaInfo(this.logic.dto.contenaResultList, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, denpyouInfoList))
            {
                base.RegistErrorFlag = true;
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 月次処理中、月次ロックチェック
            if (this.GetsujiLockCheck())
            {
                return;
            }

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.WindowType) && this.UkeireNumber != -1 && this.TairyuuNewFlg == false)
            {
            }
            else
            {
                if (this.logic.CheckAllShimeStatus())
                {
                    // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                    msgLogic.MessageBoxShow("I011", "修正");
                    return;
                }
            }

            bool ret = true;

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

            //受入実績明細チェック
            if (this.ismobile_mode)
            {
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

                if (isKotaiKanri
                    && this.TairyuuNewFlg)
                {
                    // コンテナ修正時のインフォメーション
                    if (this.logic.dto.contenaResultList != null
                        && this.logic.dto.contenaResultList.Count > 0)
                    {
                        msgLogic.MessageBoxShow("I018");
                    }
                }

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
                msgLogic.MessageBoxShow("I001", "登録");

                // 滞留一覧画面を更新
                FormManager.UpdateForm("G303");

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:

                        if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.Text))
                        {
                            // 新規モードに切り替え、再度入力可能状態とする
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.InitNumbers();
                            //base.OnLoad(e);

                            // Entity等の初期化
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

                            // スクロールバーが下がる場合があるため、
                            // 強制的にバーを先頭にする
                            this.AutoScrollPosition = new Point(0, 0);
                        }
                        else
                        {
                            //画面を閉じる
                            base.CloseTopForm();
                            return;
                        }
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.Text)
                            && r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 新規モードに切り替え、再度入力可能状態とする
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.UkeireNumber = -1;
                            this.InitNumbers();
                            //base.OnLoad(e);

                            // Entity等の初期化
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

                            // スクロールバーが下がる場合があるため、
                            // 強制的にバーを先頭にする
                            this.AutoScrollPosition = new Point(0, 0);
                        }
                        else
                        {
                            //画面を閉じる
                            base.CloseTopForm();
                            return;
                        }
                        break;
                    default:
                        break;
                }

            }

            if (!base.RegistErrorFlag)
            {
                this.logic.SetTopControlFocus();
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
            bool isF9 = true;
            M_SYS_INFO mSysInfo = this.logic.sysInfoDao.GetAllData()[0];
            // 計量票出力区分
            if (!mSysInfo.DENPYOU_HAKOU_HYOUJI.IsNull)
            {
               if(Convert.ToString(mSysInfo.DENPYOU_HAKOU_HYOUJI).Equals("1"))
               {
                    isF9 = false;
               }
            }
            this.RegistDataProcess(sender, e, isF9);

            if ((SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.PrevText)
                && r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.NEW_WINDOW_FLAG, false)
                && this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                || base.RegistErrorFlag
                || !this.logic.isRegistered)
            {
                if (!base.RegistErrorFlag)
                {
                    this.logic.SetTopControlFocus();
                }
                else
                {
                    SetControlFocus();
                }
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 登録メイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool RegistDataProcess(object sender, EventArgs e, bool isF9)
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

                // ブン 「2重登録チェック」に完了を追加する　start
                if (this.logic.footerForm.bt_func9.Enabled)
                {
                    this.setFunctionEnabled(allFalseFuncState);
                }
                else
                {
                    return ret;
                }
                // ブン 「2重登録チェック」に完了を追加する　end

                // 取引先と拠点コードの関連チェック
                if (!this.logic.CheckTorihikisakiAndKyotenCd(null, this.TORIHIKISAKI_CD.Text))
                {
                    // ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // ブン 「2重登録チェック」に完了を追加する　end
                    return ret;
                }

                //「受入入力画面」の休動Checkを追加する　start
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
                    // ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // ブン 「2重登録チェック」に完了を追加する　end
                    return ret;
                }
                else if (!retCheck2)
                {
                    this.UNTENSHA_CD.Focus();
                    // ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // ブン 「2重登録チェック」に完了を追加する　end
                    return ret;
                }
                else if (!retCheck3)
                {
                    this.NIOROSHI_GENBA_CD.Focus();
                    // ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // ブン 「2重登録チェック」に完了を追加する　end
                    return ret;
                }
                // 「受入入力画面」の休動Checkを追加する　end

                // 登録前にもう一度計算する
                // CalcDetailを実行すると空行を削除する処理が実行される。
                // その時に削除する行がCurrentだった場合にエラーが出てしまうので、Currentを移動しておく。(#23898)
                this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, Const.CELL_NAME_ROW_NO);
                if (!this.logic.CalcDetail(false))
                {
                    this.setFunctionEnabled(currentFuncState);
                    ret = false;
                    return ret;
                }
                bool isKotaiKanri = false;
                if (this.logic.dto.sysInfoEntity != null
                    && !this.logic.dto.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                    && (int)this.logic.dto.sysInfoEntity.CONTENA_KANRI_HOUHOU == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    isKotaiKanri = true;
                }

                // 明細（最終行）ー　空車重量チェック
                if (!this.logic.CheckLastRowEmptyJyuuryou())
                {
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }

                if (!this.logic.IsTimeChk(this.STACK_KEIRYOU_TIME))
                {
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }

                if (!this.logic.IsTimeChk(this.EMPTY_KEIRYOU_TIME))
                {
                    this.setFunctionEnabled(currentFuncState);
                    return ret;
                }

                // フッター空車重量セット
                this.logic.SetEmptyJyuuryou();

                this.logic.IsRegist = true;

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
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E001", "明細行");
                            base.RegistErrorFlag = true;
                        }
                        else if (!base.RegistErrorFlag)
                        {
                            catchErr = true;
                            bool retRegist = this.logic.ZaikoRegistCheck(out catchErr);
                            if (!catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                ret = false;
                                return ret;
                            }

                            if (!retRegist)
                            {
                                // コンテナ入力がある場合は現場(&業者)必須
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShowError("在庫品名が選択されている場合、自社の荷降現場を選択する必要があります。");
                                base.RegistErrorFlag = true;
                            }
                        }
                        // go 在庫品名振分処理追加 Start
                        else if (!base.RegistErrorFlag &&
                            this.logic.dto.contenaResultList.Count > 0 && string.IsNullOrEmpty(GENBA_CD.Text))
                        {
                            // コンテナ入力がある場合は現場(&業者)必須
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E180");
                            this.isInputError = true;
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

                        //受入実績明細チェック
                        if (this.ismobile_mode)
                        {
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

                        /* 月次処理中 or 月次処理ロックチェック */
                        if (!base.RegistErrorFlag)
                        {
                            if (this.GetsujiLockCheck())
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

                        /* 月次処理中 or 月次処理ロックチェック */
                        if (!base.RegistErrorFlag)
                        {
                            if (this.GetsujiLockCheck())
                            {
                                base.RegistErrorFlag = true;
                            }
                        }

                        break;

                    default:
                        break;
                }

                // 登録処理
                if (!base.RegistErrorFlag)
                {
                    var lastRow = this.gcMultiRow1.Rows.Where(r => r.IsNewRow == false).LastOrDefault();
                    this.denpyouHakouPopUpDTO.Keiryou_Prirnt_Kbn_Value = this.KEIRYOU_PRIRNT_KBN_VALUE.Text;
                    if (null != lastRow)
                    {
                        var kuushaJyuuryou = this.KUUSHA_JYURYO.Text;
                        var stackJyuuryou = lastRow.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value;
                        var emptyJyuuryou = lastRow.Cells[Const.CELL_NAME_EMPTY_JYUURYOU].Value;

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
                                // ブン 「2重登録チェック」に完了を追加する　start
                                this.setFunctionEnabled(currentFuncState);
                                // ブン 「2重登録チェック」に完了を追加する　end
                                this.logic.IsRegist = false;

                                return ret;
                            }
                        }
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult result = new DialogResult();

                    /**
                     * 設置、引揚可能チェック用データ
                     */
                    var contenaShiteiUtil = new ContenaShiteiUtility();
                    long sysId = -1;
                    int seq = -1;
                    if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.WindowType
                        || this.TairyuuNewFlg)
                    {
                        sysId = (long)this.logic.beforDto.entryEntity.SYSTEM_ID;
                        seq = (int)this.logic.beforDto.entryEntity.SEQ;
                    }

                    List<UtilityDto> denpyouInfoList = new List<UtilityDto>();
                    UtilityDto ukeireInfo = new UtilityDto();
                    ukeireInfo.SysId = sysId;
                    ukeireInfo.Seq = seq;
                    ukeireInfo.DenshuKbn = (int)DENSHU_KBN.UKEIRE;
                    denpyouInfoList.Add(ukeireInfo);

                    if (this.logic.tUketsukeSsEntry != null)
                    {
                        UtilityDto uketsukeInfo = new UtilityDto();
                        uketsukeInfo.SysId = (long)this.logic.tUketsukeSsEntry.SYSTEM_ID;
                        uketsukeInfo.Seq = (int)this.logic.tUketsukeSsEntry.SEQ;
                        uketsukeInfo.DenshuKbn = (int)DENSHU_KBN.UKETSUKE;
                        denpyouInfoList.Add(uketsukeInfo);
                    }

                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:

                            if (!base.RegistErrorFlag
                                && isKotaiKanri
                                && !contenaShiteiUtil.CheckContenaInfo(this.logic.dto.contenaResultList, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, denpyouInfoList))
                            {
                                // ブン 「2重登録チェック」に完了を追加する　start
                                this.setFunctionEnabled(currentFuncState);
                                // ブン 「2重登録チェック」に完了を追加する　end
                                return ret;
                            }

                            // コンテナ修正時のインフォメーション
                            if (isKotaiKanri
                                && this.TairyuuNewFlg
                                && this.logic.dto.contenaResultList != null
                                && this.logic.dto.contenaResultList.Count > 0)
                            {
                                bool isExistingContenaData = false;
                                foreach (var tempContena in this.logic.dto.contenaResultList)
                                {
                                    if (!string.IsNullOrEmpty(tempContena.CONTENA_SHURUI_CD)
                                        && !string.IsNullOrEmpty(tempContena.CONTENA_CD))
                                    {
                                        isExistingContenaData = true;
                                    }
                                }

                                if (isExistingContenaData)
                                {
                                    msgLogic.MessageBoxShow("I018");
                                }
                            }

                            catchErr = true;
                            retCheck = this.logic.CheckItakukeiyaku(out catchErr);
                            if (!catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                ret = false;
                                return ret;
                            }
                            // 委託契約チェック start
                            ret = retCheck;
                            if (!ret)
                            {
                                // ブン 「2重登録チェック」に完了を追加する　start
                                this.setFunctionEnabled(currentFuncState);
                                // ブン 「2重登録チェック」に完了を追加する　end
                                this.logic.IsRegist = false;
                                return ret;
                            }
                            // 委託契約チェック end

                            // 伝票発行ポップアップ表示
                            if (this.ShowDenpyouHakouPopup(!isF9, out catchErr) && !catchErr)
                            {

                                //締チェックの位置を変更 start --
                                catchErr = true;
                                retCheck = this.logic.SeikyuuDateCheck(out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }

                                retCheck2 = this.logic.SeisanDateCheck(out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }

                                /// 「受入入力」の締済期間チェックの追加　start
                                if (!retCheck)
                                {
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    return ret;
                                }
                                else if (!retCheck2)
                                {
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    return ret;
                                }
                                /// 「受入入力」の締済期間チェックの追加　end
                                //締チェックの位置を変更 end --

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
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;

                                    return ret;
                                }
                            }
                            else
                            {
                                // ブン 「2重登録チェック」に完了を追加する　start
                                this.setFunctionEnabled(currentFuncState);
                                // ブン 「2重登録チェック」に完了を追加する　end
                                this.logic.IsRegist = false;

                                return ret;
                            }

                            //仕切書
                            if (!this.logic.PrintShikirisyo())
                            {
                                this.setFunctionEnabled(currentFuncState);
                                ret = false;
                                return ret;
                            }

                            // 計量票
                            if (this.denpyouHakouPopUpDTO != null
                                && ConstClass.KEIRYOU_PRIRNT_KBN_1.Equals(this.denpyouHakouPopUpDTO.Keiryou_Prirnt_Kbn_Value))
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
                            if (this.denpyouHakouPopUpDTO != null
                                && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                            {
                                this.logic.Print();
                            }

                            // 完了メッセージ表示
                            msgLogic.MessageBoxShow("I001", "登録");

                            this.logic.isRegistered = true;

                            // 滞留一覧画面を更新
                            FormManager.UpdateForm("G303");

                            if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.Text))
                            {
                                // 【追加】モード初期表示処理
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                this.InitNumbers();
                                //base.OnLoad(e);

                                // Entity等の初期化
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
                            bool isZeiKbnChanged = false;
                            if (!this.logic.dto.entryEntity.TAIRYUU_KBN)
                                // 税区分、税計算区分、取引区分をセット
                                isZeiKbnChanged = this.logic.zeiKbnChanged();
                                if (!isZeiKbnChanged)
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    return isZeiKbnChanged;
                                }
                                result = msgLogic.MessageBoxShow("C038");
                            if (result == DialogResult.Yes || this.logic.dto.entryEntity.TAIRYUU_KBN)
                            {

                                if (!base.RegistErrorFlag
                                    && isKotaiKanri
                                    && !contenaShiteiUtil.CheckContenaInfo(this.logic.dto.contenaResultList, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, denpyouInfoList))
                                {
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    return ret;
                                }

                                // コンテナ修正時のインフォメーション
                                if (isKotaiKanri
                                    && this.logic.dto.contenaResultList != null
                                    && this.logic.dto.contenaResultList.Count > 0)
                                {
                                    bool isExistingContenaData = false;
                                    foreach (var tempContena in this.logic.dto.contenaResultList)
                                    {
                                        if (!string.IsNullOrEmpty(tempContena.CONTENA_SHURUI_CD)
                                            && !string.IsNullOrEmpty(tempContena.CONTENA_CD))
                                        {
                                            isExistingContenaData = true;
                                        }
                                    }

                                    if (isExistingContenaData)
                                    {
                                        msgLogic.MessageBoxShow("I018");
                                    }
                                }

                                catchErr = true;
                                retCheck = this.logic.CheckItakukeiyaku(out catchErr);
                                if (!catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }
                                // 委託契約チェック start
                                ret = retCheck;
                                if (!ret)
                                {
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;
                                    return ret;
                                }
                                // 委託契約チェック end

                                // 伝票発行ポップアップ表示
                                if (this.ShowDenpyouHakouPopup(!isF9, out catchErr) && !catchErr)
                                {

                                    //チェックの位置を変更 start
                                    if (this.logic.CheckAllShimeStatus())
                                    {
                                        // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                        msgLogic.MessageBoxShow("I011", "修正");
                                        this.setFunctionEnabled(currentFuncState);
                                        return ret;
                                    }

                                    catchErr = true;
                                    retCheck = this.logic.SeikyuuDateCheck(out catchErr);
                                    if (!catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        ret = false;
                                        return ret;
                                    }
                                    retCheck2 = this.logic.SeisanDateCheck(out catchErr);
                                    if (!catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        ret = false;
                                        return ret;
                                    }

                                    ///「受入入力」の締済期間チェックの追加　start
                                    if (!retCheck)
                                    {
                                        // ブン 「2重登録チェック」に完了を追加する　start
                                        this.setFunctionEnabled(currentFuncState);
                                        // ブン 「2重登録チェック」に完了を追加する　end
                                        return ret;
                                    }
                                    else if (!retCheck2)
                                    {
                                        // ブン 「2重登録チェック」に完了を追加する　start
                                        this.setFunctionEnabled(currentFuncState);
                                        // ブン 「2重登録チェック」に完了を追加する　end
                                        return ret;
                                    }
                                    /// 「受入入力」の締済期間チェックの追加　end
                                    //チェックの位置を変更 end

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
                                        // ブン 「2重登録チェック」に完了を追加する　start
                                        this.setFunctionEnabled(currentFuncState);
                                        // ブン 「2重登録チェック」に完了を追加する　end
                                        this.logic.IsRegist = false;

                                        return ret;
                                    }
                                }
                                else
                                {
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;

                                    return ret;
                                }

                                //仕切書
                                if (!this.logic.PrintShikirisyo())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    ret = false;
                                    return ret;
                                }

                                // 計量票
                                if (this.denpyouHakouPopUpDTO != null
                                    && ConstClass.KEIRYOU_PRIRNT_KBN_1.Equals(this.denpyouHakouPopUpDTO.Keiryou_Prirnt_Kbn_Value))
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
                                if (this.denpyouHakouPopUpDTO != null
                                    && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                                {
                                    this.logic.Print();
                                }

                                // 完了メッセージ表示
                                msgLogic.MessageBoxShow("I001", "更新");

                                this.logic.isRegistered = true;

                                // 滞留一覧画面を更新
                                FormManager.UpdateForm("G303");

                                if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.Text)
                                    && r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    // 継続入力ON かつ 追加権限がある場合
                                    // 【追加】モード初期表示処理
                                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                    this.UkeireNumber = -1;

                                    // Entity等の初期化
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

                                    // スクロールバーが下がる場合があるため、
                                    // 強制的にバーを先頭にする
                                    this.AutoScrollPosition = new Point(0, 0);
                                }
                                else
                                {
                                    //画面を閉じる
                                    base.CloseTopForm();
                                }
                            }

                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            result = msgLogic.MessageBoxShow("C026");
                            if (result == DialogResult.Yes)
                            {

                                //締済チェック start
                                if (this.logic.CheckAllShimeStatus())
                                {
                                    // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                    msgLogic.MessageBoxShow("I011", "削除");
                                    this.setFunctionEnabled(currentFuncState);
                                    return ret;
                                }
                                //締済チェック end

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
                                    // ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;

                                    return ret;
                                }

                                // 完了メッセージ表示
                                msgLogic.MessageBoxShow("I001", "削除");
                                this.logic.isRegistered = true;

                                // 滞留一覧画面を更新
                                FormManager.UpdateForm("G303");

                                //画面を閉じる
                                base.CloseTopForm();
                            }

                            break;

                        default:
                            break;
                    }
                }

                // ブン 「2重登録チェック」に完了を追加する　start
                this.setFunctionEnabled(currentFuncState);
                // ブン 「2重登録チェック」に完了を追加する　end
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
                this.logic.IsRegist = false;
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
            if (this.ismobile_mode && this.DETAIL_TAB.SelectedTab == this.UKEIRE_JISSEKI_PAGE)
            {
                editingMultiRowFlag = true;
                // 行を追加
                this.logic.AddNewRow2();
                editingMultiRowFlag = false;
            }
            else
            {
                editingMultiRowFlag = true;
                bool catchErr = true;
                bool retWarihuri = this.logic.JudgeWarihuri(out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (retWarihuri)
                {
                    // 割振行の場合はエラー
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E127");
                }
                else
                {
                    // 割振行でなければ追加
                    this.logic.AddNewRow();
                    // 合計系計算
                    if (!this.logic.CalcTotalValues())
                    {
                        return;
                    }
                }
                editingMultiRowFlag = false;
            }
            LogUtility.DebugMethodEnd(sender, e);
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

            if (this.ismobile_mode && this.DETAIL_TAB.SelectedTab == this.UKEIRE_JISSEKI_PAGE)
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
                var targetRow = this.gcMultiRow1.CurrentRow;
                if (targetRow != null)
                {
                    var warifuriJyuuryou = targetRow.Cells[Const.CELL_NAME_WARIFURI_JYUURYOU].Value;
                    if (warifuriJyuuryou != null && !String.IsNullOrEmpty(warifuriJyuuryou.ToString()))
                    {
                        var messageLogic = new MessageBoxShowLogic();
                        messageLogic.MessageBoxShow("E164");

                        return;
                    }
                }

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

                editingMultiRowFlag = false;
                this.logic.gcMultiRowHenkou();
            }
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            // 明細行を確定する。
            if (this.gcMultiRow1.CurrentCell != null)
            {
                this.gcMultiRow1.BeginEdit(false);
                this.gcMultiRow1.EndEdit();
                this.gcMultiRow1.NotifyCurrentCellDirty(false);
            }

            if (this.ismobile_mode && this.gcMultiRow2.CurrentCell != null)
            {
                this.gcMultiRow2.BeginEdit(false);
                this.gcMultiRow2.EndEdit();
                this.gcMultiRow2.NotifyCurrentCellDirty(false);
            }

            bool isFormColseFlg = true;
            bool isKakuNinDailogFlg = false;

            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text)
                    || !string.IsNullOrEmpty(this.GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.GENBA_CD.Text)
                    || !string.IsNullOrEmpty(this.EIGYOU_TANTOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.SHUUKEI_KOUMOKU_CD.Text) //PhuocLoc 2020/12/01 #136219
                    || !string.IsNullOrEmpty(this.SHARYOU_CD.Text)
                    || !string.IsNullOrEmpty(this.SHASHU_CD.Text)
                    || !string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.UNTENSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.NINZUU_CNT.Text)
                    || !string.IsNullOrEmpty(this.CONTENA_SOUSA_CD.Text)
                    || !string.IsNullOrEmpty(this.MANIFEST_SHURUI_CD.Text)
                    || !string.IsNullOrEmpty(this.MANIFEST_TEHAI_CD.Text))
                {
                    isKakuNinDailogFlg = true;
                }
                else if (this.gcMultiRow1.Rows.Count > 0)
                {
                    foreach (Row row in this.gcMultiRow1.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        if ((row.Cells["STACK_JYUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["STACK_JYUURYOU"].Value.ToString()))
                            || (row.Cells["EMPTY_JYUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["EMPTY_JYUURYOU"].Value.ToString()))
                            || (row.Cells["YOUKI_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["YOUKI_CD"].Value.ToString()))
                            || (row.Cells["HINMEI_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["HINMEI_CD"].Value.ToString()))
                            || (row.Cells["SUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["SUURYOU"].Value.ToString()))
                            || (row.Cells["UNIT_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["UNIT_CD"].Value.ToString()))
                            || (row.Cells["NET_JYUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["NET_JYUURYOU"].Value.ToString()))
                            || (row.Cells["KINGAKU"].Value != null && !string.IsNullOrEmpty(row.Cells["KINGAKU"].Value.ToString()))
                            || (row.Cells["NISUGATA_SUURYOU"].Value != null && !string.IsNullOrEmpty(row.Cells["NISUGATA_SUURYOU"].Value.ToString()))
                            || (row.Cells["NISUGATA_UNIT_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["NISUGATA_UNIT_CD"].Value.ToString()))
                            || (row.Cells["MEISAI_BIKOU"].Value != null && !string.IsNullOrEmpty(row.Cells["MEISAI_BIKOU"].Value.ToString())))
                        {
                            isKakuNinDailogFlg = true;
                            break;
                        }
                    }
                }

                if (this.ismobile_mode && isKakuNinDailogFlg == false && this.gcMultiRow2.Rows.Count > 0)
                {
                    foreach (Row row in this.gcMultiRow2.Rows)
                    {
                        if (row.IsNewRow)
                            continue;

                        if ((row.Cells["HINMEI_CD"].Value != null && !string.IsNullOrEmpty(row.Cells["HINMEI_CD"].Value.ToString()))
                            || (row.Cells["SUURYOU_WARIAI"].Value != null && !string.IsNullOrEmpty(row.Cells["SUURYOU_WARIAI"].Value.ToString())))
                        {
                            isKakuNinDailogFlg = true;
                            break;
                        }
                    }
                }
            }
            else if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                isKakuNinDailogFlg = true;
            }
            else
            {
                isKakuNinDailogFlg = false;
            }

            if (isKakuNinDailogFlg)
            {
                DialogResult dr = new DialogResult();
                if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    dr = this.logic.msgLogic.MessageBoxShow("C109", "登録中", "受入入力");
                else
                    dr = this.logic.msgLogic.MessageBoxShow("C109", "修正中", "受入入力");

                if (dr == DialogResult.OK || dr == DialogResult.Yes)
                {
                    isFormColseFlg = true;
                }
                else
                {
                    isFormColseFlg = false;
                }
            }

            if (isFormColseFlg)
            {
                base.CloseTopForm();
            }
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
            LogUtility.DebugMethodEnd(sender, e);
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
                case Const.CELL_NAME_CHOUSEI_JYUURYOU:

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

                case Const.CELL_NAME_CHOUSEI_PERCENT:
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

                case Const.CELL_NAME_WARIFURI_JYUURYOU:
                    catchErr = true;
                    retChousei = this.logic.ValidateWarifuriJyuuryou(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case Const.CELL_NAME_WARIFURI_PERCENT:
                    catchErr = true;
                    retChousei = this.logic.ValidateWarifuriPercent(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case Const.CELL_NAME_HINMEI_CD:
                    object denpyouKbn = this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_NAME].Value == null ? string.Empty : this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_NAME].Value;

                    if (beforeValuesForDetail.ContainsKey(e.CellName)
                        && beforeValuesForDetail[e.CellName].Equals(
                            Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)) && !this.isInputError
                        && !this.bSelectHinmeiPopup
                        && !string.IsNullOrEmpty(denpyouKbn.ToString()))
                    {
                        // 品名CDの入力値が前回入力時と同じならば処理中断（ただし、品名ポップアップからの選択は例外）
                        return;
                    }
                    else
                    {
                        this.isInputError = false;
                        var value = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value);
                        if (string.IsNullOrEmpty(value))
                        {
                            this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_NAME].Value = "";
                            this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_ZEI_KBN_CD].Value = "";
                        }
                        catchErr = true;
                        retChousei = this.logic.GetHinmei(this.gcMultiRow1.CurrentRow, out catchErr);
                        if (!catchErr)
                        {
                            this.isInputError = true;
                            e.Cancel = true;
                            return;
                        }
                        if (retChousei)
                        {

                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "品名");
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
                        this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_UNIT_CD].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_TANKA].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_KINGAKU].Value = string.Empty;
                        this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                        this.gcMultiRow1.Rows[e.RowIndex].Cells[Const.CELL_NAME_HINMEI_CD].UpdateBackColor(false);

                        if (!this.logic.ZaikoHinmeiHuriwakesClear(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        return;
                    }

                    // 空だったら処理中断
                    this.gcMultiRow1.BeginEdit(false);
                    this.gcMultiRow1.EndEdit();
                    this.gcMultiRow1.NotifyCurrentCellDirty(false);
                    if (string.IsNullOrEmpty((string)this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value))
                    {
                        this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
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
                            this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                            control.TextBoxChanged = false;
                        }
                    }

                    bool bResult = true;

                    if (string.IsNullOrEmpty(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_CD].Value))
                        || string.IsNullOrEmpty(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_DENPYOU_KBN_NAME].Value)))
                    {
                        bResult = this.logic.SetDenpyouKbn();
                    }

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
                            this.logic.ResetTankaCheck(); // MAILAN #158991 START

                            if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                            {
                                return;
                            }

                            if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }

                        }
                        else if (this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value == null)
                        {
                            // 品名CDに入力がなければ、単位コードとその略称もクリアする
                            this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_UNIT_CD].Value = string.Empty;
                            this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                            this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_TANKA].Value = string.Empty;

                            // 在庫品名クリア追加
                            if (!this.logic.ZaikoHinmeiHuriwakesClear(this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }
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

                        // 品名CD変更した場合、在庫品名・比率を再検索する
                        this.logic.ZaikoHinmeiHuriwakesSearch(this.gcMultiRow1.CurrentRow);
                    }
                    break;

                case Const.CELL_NAME_HINMEI_NAME:
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

                case Const.CELL_NAME_STAK_JYUURYOU:
                case Const.CELL_NAME_EMPTY_JYUURYOU:
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

                case Const.CELL_NAME_KEIRYOU_TIME:
                    var cell = this.gcMultiRow1[e.RowIndex, e.CellIndex];
                    if (!this.logic.IsTimeChkOK(cell))
                    {
                        e.Cancel = true;
                    }
                    break;

                // 在庫品名チェック
                case Const.CELL_NAME_ZAIKO_HINMEI_CD:
                    catchErr = true;
                    retChousei = this.logic.ZaikoChangeCheck(this.gcMultiRow1.CurrentRow, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                default:
                    break;
            }

            SetIchranHaikeiiro(e.RowIndex);

            // 単価と金額の活性/非活性制御
            if (e.CellName.Equals(Const.CELL_NAME_TANKA) &&
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
            if (e.CellName != Const.CELL_NAME_HINMEI_CD &&
                e.CellName != Const.CELL_NAME_ZAIKO_HINMEI_CD)
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
                    case Const.CELL_NAME_UNIT_CD:
                        if (!this.logic.SearchAndCalcForUnit(false, this.gcMultiRow1.CurrentRow))
                        {
                            editingMultiRowFlag = false; // MAILAN #158991 START
                            return;
                        }
                        this.logic.ResetTankaCheck(); // MAILAN #158991 START
                        if (!this.logic.CalcSuuryou(this.gcMultiRow1.CurrentRow))    // 数量再計算
                        {
                            return;
                        }
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_STAK_JYUURYOU:
                        if (!this.logic.ChangeWarihuriAndChouseiInputStatus())
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
                        if (!this.logic.SetStakJyuuryo(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        if (!this.logic.SetEmptyJyuuryou(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_EMPTY_JYUURYOU:
                        if (!this.logic.ChangeWarihuriAndChouseiInputStatus())
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
                        if (!this.logic.SetStakJyuuryo(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        if (!this.logic.SetEmptyJyuuryou(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_CHOUSEI_JYUURYOU:
                        this.logic.SetModeInputPercent();
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
                        if (!this.logic.SetKeiryouTime(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        this.logic.SetActiveWarifuriAndChoisei();
                        break;

                    case Const.CELL_NAME_CHOUSEI_PERCENT:
                        this.logic.SetModeInputPercent();
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
                        this.logic.SetActiveWarifuriAndChoisei();
                        break;

                    case Const.CELL_NAME_YOUKI_CD:
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
                        if (!this.logic.CalcChouseiJyuuryou())
                        {
                            return;
                        }
                        if (!this.logic.SetKeiryouTime(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_YOUKI_SUURYOU:
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
                        if (!this.logic.CalcChouseiJyuuryou())
                        {
                            return;
                        }
                        if (!this.logic.SetKeiryouTime(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_YOUKI_JYUURYOU:
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
                        if (!this.logic.CalcChouseiJyuuryou())
                        {
                            return;
                        }
                        if (!this.logic.SetKeiryouTime(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_WARIFURI_JYUURYOU:
                        this.logic.SetModeInputPercent();
                        if (!this.logic.ExecuteWarifuri(true))
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
                        this.logic.SetActiveWarifuriAndChoisei();
                        break;

                    case Const.CELL_NAME_WARIFURI_PERCENT:
                        this.logic.SetModeInputPercent();
                        if (!this.logic.ExecuteWarifuri(false))
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
                        this.logic.SetActiveWarifuriAndChoisei();
                        break;

                    case Const.CELL_NAME_SUURYOU:
                    case Const.CELL_NAME_TANKA:
                        if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_URIAGESHIHARAI_DATE:
                        // 消費税の取得
                        if (!this.logic.SetShouhizeiRateForDetail(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        break;

                    case Const.CELL_NAME_HINMEI_CD:
                        // 品名をセット
                        if (!this.logic.SetHinmeiName(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        this.logic.RirekeDisplay();
                        break;

                    // go 在庫品名単独設定追加 End
                    case Const.CELL_NAME_ZAIKO_HINMEI_CD:
                        if (!beforeValuesForDetail.ContainsKey(e.CellName) ||
                            !beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
                        {
                            if (!this.logic.ZaikoHinmeiSingleSearch(this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (!this.logic.ZaikoHinmeiKakunou(this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }
                        }
                        break;
                    // go 在庫品名単独設定追加 End

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
                if (e.CellName.Equals(Const.CELL_NAME_HINMEI_CD) && !this.isHinmeiReLoad)
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
            this.logic.gcMultiRowHenkou();
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            row = this.gcMultiRow1.CurrentRow;

            if (row == null)
            {
                return;
            }

            // 品名でPopup表示後処理追加
            if (e.CellName == Const.CELL_NAME_HINMEI_CD)
            {
                // PopupResult取得できるようにPopupAfterExecuteにデータ設定
                GcCustomTextBoxCell cell = (GcCustomTextBoxCell)row.Cells[Const.CELL_NAME_HINMEI_CD];
                cell.PopupAfterExecute = PopupAfter_HINMEI_CD;

                // 履歴品名設定
                if (!this.hinnmeiFocusFlg && (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text)
                    || !string.IsNullOrEmpty(this.GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.GENBA_CD.Text))
                    && (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG || this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    this.logic.HinmeiCursorInRirekeShow();
                }

                this.hinnmeiFocusFlg = false;
                this.bSelectHinmeiPopup = false;
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
                case Const.CELL_NAME_HINMEI_NAME:
                    // 品名をセット
                    if (!this.logic.SetHinmeiName(row))
                    {
                        return;
                    }
                    break;
                case Const.CELL_NAME_EMPTY_JYUURYOU:   // 空車重量
                    // 総重量変更時の再計算
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
            Cell systemId = row.Cells[Const.CELL_NAME_SYSTEM_ID];
            Cell detailSystemId = row.Cells[Const.CELL_NAME_DETAIL_SYSTEM_ID];
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
        /// 切替(F4)
        /// </summary>
        internal void UketsukeDenpyo(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            int index = this.DETAIL_TAB.SelectedIndex;
            int selectedIndex = 0;

            //PhuocLoc 2020/12/01 #136219 -Start
            if (!string.IsNullOrEmpty(this.logic.TairyuHyoujiKbn) && this.logic.TairyuHyoujiKbn == Const.TAIRYU_ICHIRAN_SHOW)
            {
                switch (index)
                {
                    case 0:
                        selectedIndex = 1;
                        break;
                    case 1:
                        selectedIndex = 2;
                        break;
                    case 2:
                        selectedIndex = 3;
                        if (!this.ismobile_mode)
                        {
                            this.logic.GetTairyuData();
                        }
                        break;
                    case 3:
                        if (this.ismobile_mode)
                        {
                            selectedIndex = 4;
                            this.logic.GetTairyuData();
                        }
                        else
                        {
                            selectedIndex = 0;
                        }
                        break;
                    case 4:
                        if (this.ismobile_mode)
                        {
                            selectedIndex = 0;
                        }
                        break;
                }
            }
            else
            {
                switch (index)
                {
                    case 0:
                        selectedIndex = 1;
                        break;
                    case 1:
                        selectedIndex = 2;
                        break;
                    case 2:
                        if (this.ismobile_mode)
                        {
                            selectedIndex = 3;
                        }
                        else
                        {
                            selectedIndex = 0;
                        }
                        break;
                    case 3:
                        if (this.ismobile_mode)
                        {
                            selectedIndex = 0;
                        }
                        break;
                }
            }
            //PhuocLoc 2020/12/01 #136219 -End

            this.DETAIL_TAB.SelectedIndex = selectedIndex;

            if (selectedIndex == 0)
            {
                this.gcMultiRow1.Focus();
                this.gcMultiRow1.Rows[0].Cells[Const.CELL_NAME_STAK_JYUURYOU].Selected = true;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンテナ(F6)
        /// </summary>
        internal void ContenaWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.logic.OpenContena())
            {
                return;
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 受付番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKETSUKE_NUMBER_Validated(object sender, EventArgs e)
        {
            // 新規のみ
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 受付番号が変更されていない時は何もしない
                if (this.UketsukeNumberTextChangeFlg)
                {
                    // 受付番号入力時かつ
                    // 値に変更がある場合 もしくは 同じ受付番号でも再入力された場合は実行
                    if (!string.IsNullOrEmpty(this.logic.headerForm.UKETSUKE_NUMBER.Text))
                    {
                        // 初期化
                        this.UketsukeNumberTextChangeFlg = false;

                        if (!this.logic.headerForm.UKETSUKE_NUMBER.Text.Equals(this.UketsukeNumber.ToString()))
                        {
                            bool catchErr = true;

                            this.gcMultiRow1.BeginEdit(false);
                            this.gcMultiRow1.Rows.Clear();
                            this.gcMultiRow1.EndEdit();

                            if (this.ismobile_mode)
                            {
                                this.gcMultiRow2.BeginEdit(false);
                                this.gcMultiRow2.Rows.Clear();
                                this.gcMultiRow2.EndEdit();
                            }

                            // コンテナ情報初期化
                            this.logic.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                            this.logic.dto.contenaResultList = new List<T_CONTENA_RESULT>();

                            this.notEditingOperationFlg = true;
                            catchErr = this.logic.GetUketsukeNumber();
                            this.notEditingOperationFlg = false;
                            if (!catchErr) return;

                            // 「受入入力画面」の休動Check　start
                            this.logic.UketukeBangoCheck(out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
                            // 「受入入力画面」の休動Check　end

                            long uketsukeNum = -1;
                            if (long.TryParse(this.logic.headerForm.UKETSUKE_NUMBER.Text, out uketsukeNum))
                            {
                                this.UketsukeNumber = uketsukeNum;
                            }
                            else
                            {
                                this.UketsukeNumber = -1;
                            }
                        }
                        else
                        {
                            if (this.logic.tUketsukeSsEntry == null)
                            {
                                // 更新用受付データ再取得
                                this.logic.GetUketsukeData();
                            }
                        }
                    }
                    else
                    {
                        this.logic.ClearTUketsukeSsEntry();
                        this.logic.ClearTUketsukeMkEntry();

                        // コンテナ情報初期化
                        this.logic.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                    }

                }
            }
        }

        /// <summary>
        /// 計量番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void KEIRYOU_NUMBER_Validated(object sender, EventArgs e)
        {
            // 新規のみ
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 計量番号が変更されていない時は何もしない
                if (this.KeiryouNumberTextChangeFlg)
                {
                    // 計量番号入力時かつ
                    // 値に変更がある場合 もしくは 同じ計量番号でも再入力された場合は実行
                    if (!string.IsNullOrEmpty(this.logic.headerForm.KEIRYOU_NUMBER.Text))
                    {
                        // 初期化
                        if (!this.logic.headerForm.KEIRYOU_NUMBER.Text.Equals(this.KeiryouNumber.ToString()))
                        {
                            this.gcMultiRow1.BeginEdit(false);
                            this.gcMultiRow1.Rows.Clear();
                            this.gcMultiRow1.EndEdit();

                            if (this.ismobile_mode)
                            {
                                this.gcMultiRow2.BeginEdit(false);
                                this.gcMultiRow2.Rows.Clear();
                                this.gcMultiRow2.EndEdit();
                            }

                            if(!string.IsNullOrEmpty(this.logic.headerForm.KEIRYOU_NUMBER.Text))
                            {
                                this.KeiryouNumber = long.Parse(this.logic.headerForm.KEIRYOU_NUMBER.Text);
                            }
                            else
                            {
                                return;
                            }
                            if (!this.logic.GetKeiryouNumber())
                            {
                                return;
                            }
                            bool catchErr = true;
                            bool PattenName = this.logic.KeiryouBangoCheck(out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        // 在庫品名CDが活性化の上、
        // グリッドのKeyDownが効かないため、EditingControlShowingに変更して、
        // 毎回Enter編集モードに入る時、該当セルにKeyDownイベントをバインドする。
        /// <summary>
        /// 明細_在庫品名CDスペースキー押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zaikoHinmeiCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (this.gcMultiRow1.SelectedCells != null &&
                    this.gcMultiRow1.SelectedCells.Count == 1 &&
                    this.gcMultiRow1.SelectedCells[0].Name == Const.CELL_NAME_ZAIKO_HINMEI_CD)
                {
                    bool catchErr = true;
                    bool retCheck = this.logic.ZaikoChangeCheck(this.gcMultiRow1.CurrentRow, out catchErr, false);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (retCheck)
                    {
                        if (!this.logic.ZaikoHinmeiHuriwakesGamenSeni(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }

                        if (sender != null && sender is TextBox)
                        {
                            TextBox ctrl = sender as TextBox;
                            ctrl.Text = string.Empty;
                            this.beforeValuesForDetail[Const.CELL_NAME_ZAIKO_HINMEI_CD] = string.Empty;
                            if (this.logic.dto.rowZaikoHinmeiHuriwakes[this.gcMultiRow1.CurrentRow].Count == 1)
                            {
                                ctrl.Text =
                                    this.logic.dto.rowZaikoHinmeiHuriwakes[this.gcMultiRow1.CurrentRow][0].ZAIKO_HINMEI_CD;
                                this.beforeValuesForDetail[Const.CELL_NAME_ZAIKO_HINMEI_CD] =
                                    this.logic.dto.rowZaikoHinmeiHuriwakes[this.gcMultiRow1.CurrentRow][0].ZAIKO_HINMEI_CD;
                            }
                        }
                    }
                }

                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 明細.在庫品名CDが編集モードに入る時、
        /// KeyDownイベントをバインドする。
        /// </remarks>
        /// <see cref="http://gcdn.gcpowertools.com.cn/showtopic-1367.html"/>
        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            var gcmr = sender as GcMultiRow;
            if (gcmr.SelectedCells != null &&
                gcmr.SelectedCells.Count == 1 &&
                gcmr.SelectedCells[0].Name == Const.CELL_NAME_ZAIKO_HINMEI_CD)
            {
                e.Control.KeyDown -= this.zaikoHinmeiCd_KeyDown;
                e.Control.KeyDown += this.zaikoHinmeiCd_KeyDown;
            }
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

            //PhuocLoc 2020/12/01 #136219 -Start
            if (this.DETAIL_TAB.SelectedTab == this.TAIRYU_LIST_TAB
                && this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                && string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
            {
                if (this.gcMultiRow1.Rows.Count > 0)
                {
                    var row = this.gcMultiRow1.Rows[0];
                    if (row.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value != null && !string.IsNullOrEmpty(Convert.ToString(row.Cells[Const.CELL_NAME_STAK_JYUURYOU].Value)))
                    {
                        this.logic.msgLogic.MessageBoxShow("E275", "明細タブ", "[F1]重量取込を実行");
                        return;
                    }
                    else
                    {
                        this.DETAIL_TAB.SelectedTab = this.MEISAI_PAGE;
                    }
                }
            }
            //PhuocLoc 2020/12/01 #136219 -End

            if (this.DETAIL_TAB.SelectedTab != this.MEISAI_PAGE)
            {
                this.logic.msgLogic.MessageBoxShow("E275", "明細タブ", "[F1]重量取込を実行");
                return;
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

            // 重量値表示プロセス起動
            truckScaleWeight1.ProcessWeight();

            // 自動手動重量表示の値を読み込んでSetJyuuryouに渡す変数セット
            bool weightDisplaySwitch = truckScaleWeight1.WeightDisplaySwitch();

            // 取込ファイルの作成or更新を行う。
            var jyuryoTorikomiUtil = new JyuryoTorikomiUtility();
            jyuryoTorikomiUtil.MakeTorikomiFile();

            if (!this.logic.SetJyuuryou(weightDisplaySwitch))
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
        /// [1]計量のみ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.SharyouTouroku();
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// [2]計量票発行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, System.EventArgs e)
        {
            // フッター空車重量セット
            this.logic.SetEmptyJyuuryou();

            if (!this.logic.PrintKeiryouhyou())
            {
                return;
            }
        }
        /// <summary>
        /// [3]伝票発行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process3_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.RegistDataProcess(sender, e, false);

            if ((SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.PrevText)
                && r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.NEW_WINDOW_FLAG, false)
                && this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                || base.RegistErrorFlag
                || !this.logic.isRegistered)
            {
                if (!base.RegistErrorFlag)
                {
                    this.logic.SetTopControlFocus();
                }
                else
                {
                    SetControlFocus();
                }
            }

            LogUtility.DebugMethodEnd(sender, e);
        }
        /// <summary>
        /// [4]運賃入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process4_Click(object sender, System.EventArgs e)
        {
            if (!this.logic.OpenUnchinNyuuryoku(sender, e))
            {
                return;
            }
        }
        /// <summary>
        /// [5]個別品名単価
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process5_Click(object sender, System.EventArgs e)
        {
            //次期開発のため未実装
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
            this.logic.AddRowDic(e.RowIndex);
        }
        /// <summary>
        /// 行削除中イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow1_RowsRemoving(object sender, RowsRemovingEventArgs e)
        {
            // Dictionaryからの削除
            this.logic.RemoveRowDic(e.RowIndex);
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
        /// コンストラクタで渡された受入番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistUkeireData()
        {
            bool catchErr = true;
            bool retExist = this.logic.IsExistUkeireData(this.UkeireNumber, out catchErr);
            if (!catchErr)
            {
                return false;
            }

            return retExist;
        }

        /// <summary>
        /// 受入番号、SEQのデータで滞留登録された受入伝票用の権限チェック
        /// </summary>
        /// <returns></returns>
        public bool HasAuthorityTairyuu()
        {
            bool catchErr = true;
            bool retExist = this.logic.HasAuthorityTairyuu(this.UkeireNumber, this.SEQ, out catchErr);
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
        /// 伝票発行ポップアップ表示
        /// </summary>
        /// <returns>true:実行された場合, false:キャンセルされた場合</returns>
        private bool ShowDenpyouHakouPopup(bool isShowDialog, out bool catchErr)
        {
            try
            {
                catchErr = false;
                bool returnVal = false;
                string denpyouMode = string.Empty;
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        denpyouMode = ConstClass.TENPYO_MODEL_1;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        if (this.logic.dto.entryEntity.TAIRYUU_KBN)
                        {
                            // 滞留登録された伝票は新規として扱う
                            denpyouMode = ConstClass.TENPYO_MODEL_1;
                        }
                        else
                        {
                            denpyouMode = ConstClass.TENPYO_MODEL_2;
                        }
                        break;

                    default:
                        break;
                }

                this.denpyouHakouPopUpDTO = this.logic.CreateParameterDTOClass();
                bool kakuteiKbn = this.logic.IsKakuteiDenpyou();

                var callForm
                    = new Shougun.Core.SalesPayment.DenpyouHakou.UIForm(
                        this.denpyouHakouPopUpDTO,
                        denpyouMode,
                        SalesPaymentConstans.DENSHU_KBN_CD_UKEIRE_STR,
                        kakuteiKbn);
                var callBaseForm = new MasterBaseForm(callForm, WINDOW_TYPE.NONE, false);
                DialogResult result = DialogResult.None;
                if (isShowDialog)
                {
                    result = callBaseForm.ShowDialog();
                }
                else
                {
                    result = callForm.UKEIRE_SHUKKA_Regist();
                    //result = DialogResult.OK;
                }
                this.denpyouHakouPopUpDTO = callForm.ParameterDTO;
                if (result == DialogResult.OK)
                {
                    returnVal = true;
                }

                callBaseForm.Dispose();

                return returnVal;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("ShowDenpyouHakouPopup", ex);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E245", "");
                return true;
            }
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
        public virtual void customPopupOpenButton1PopupBefore()
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

            if (this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd || this.GENBA_CD.Text != this.logic.tmpGenbaCd)
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
        public virtual void customPopupOpenButton2PopupBefore()
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

        //PhuocLoc 2020/12/01 #136219 -Start
        /// <summary>
        /// 集計項目CDへフォーカス移動する
        /// 集計項目CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToShuukeiKoumokuCd()
        {
            this.SHUUKEI_KOUMOKU_CD.Focus();
        }
        //PhuocLoc 2020/12/01 #136219 -End

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
            this.PopupAfter_SHARYOU_CD();
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
        /// 形態区分CDへフォーカス移動する
        /// 形態区分CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToKeitaiKbnCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.KEITAI_KBN_CD.Focus();
            if (this.KEITAI_KBN_CD.Text != this.logic.tmpKeitaiKbnCd)
            {
                this.logic.CheckKeitaiKbn();
            }

            // 初期化
            this.isFromSearchButton = false;
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

        /// <summary>
        /// 受入番号、受付番号、計量番号を初期化
        /// </summary>
        internal void InitNumbers()
        {
            this.UkeireNumber = -1;
            this.UketsukeNumber = -1;
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
        /// 受入データ取得メソッド
        /// </summary>
        /// <param name="isPrevious">true:前伝票を取得する、false:次伝票を取得する</param>
        private void GetUkeireDataForPreOrNextButton(bool isPrevious, object sender, EventArgs e)
        {
            if (this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                return;
            }

            if (!nowLoding)
            {
                nowLoding = true;
                long ukeireNumber = 0;
                long preOrNextUkeireNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                bool EmptyCheck = false;

                if (string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                {
                    ukeireNumber = this.logic.GetMaxUkeireNumber();
                    EmptyCheck = false;
                }
                else
                {
                    long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out ukeireNumber);
                    EmptyCheck = true;
                }
                // 受入番号の入力がある場合
                if (isPrevious)
                {
                    preOrNextUkeireNumber = this.logic.GetPreUkeireNumber(ukeireNumber, EmptyCheck);
                }
                else
                {
                    preOrNextUkeireNumber = this.logic.GetNextUkeireNumber(ukeireNumber, EmptyCheck);
                }
                if (preOrNextUkeireNumber <= 0)
                {
                    if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                    {
                        nowLoding = false;
                        return;
                    }
                }
                if (preOrNextUkeireNumber > 0)
                {
                    // 入力されている受入番号の後の受入番号が取得できた場合
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.UkeireNumber = preOrNextUkeireNumber;
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
                    bool retExist = this.logic.HasAuthorityTairyuu(this.UkeireNumber, this.SEQ, out catchErr);
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
                    if (!r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G721", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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

                    // スクロールバーが下がる場合があるため、
                    // 強制的にバーを先頭にする
                    this.AutoScrollPosition = new Point(0, 0);

                    // 初期フォーカス位置の設定は本メソッドの呼び出し元で制御する
                }
                else
                {
                    // 入力されている受入番号の後の受入番号が取得できなかった場合
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
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
                // 履歴備考設定  
                this.logic.RirekeShow();

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

            if (this.isInputError || this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd)
            {
                this.logic.IsSuuryouKesannFlg = true;
                ret = this.logic.CheckGyousha(out catchErr);
                this.logic.IsSuuryouKesannFlg = false;

                if (!catchErr)
                {
                    return false;
                }
            }
            // 品名手入力に関する機能修正 start
            this.logic.hasShow = false;
            // 品名手入力に関する機能修正 end

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
            if (this.isInputError || (String.IsNullOrEmpty(this.GYOUSHA_CD.Text) || !this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text) ||
                    (this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_NAME_RYAKU.Text)))
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

            // 品名手入力に関する機能修正 start
            this.logic.hasShow = false;
            // 品名手入力に関する機能修正 end
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
            if (String.IsNullOrEmpty(this.KUUSHA_JYURYO.Text))
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
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShowError("総重量と１つ前の空車重量が不整合です。");
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
                this.bSelectHinmeiPopup = true;
            }
        }

        private bool execEntryNumberEvent = false;

        /// <summary>
        /// TABにのCONTROLかどうかの判断
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private bool IS_TAB_CONTROL_ACTIVED(Control control)
        {
            bool rlt = false;

            if (control == null)
                return rlt;

            if (control.Name == "DETAIL_TAB")
            {
                rlt = true;
                return rlt;
            }

            if (control.Parent != null)
            {
                rlt = IS_TAB_CONTROL_ACTIVED(control.Parent);
                return rlt;
            }

            return rlt;
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
            if (!this.isFromSearchButton)
            {
                // 比較用形態区分CDをセット
                this.logic.KeitaiKbnCdSet();
            }
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

        public void upDataForm()
        {
        }

        /// <summary>
        /// 明細行の項目入力チェック
        /// </summary>
        /// <returns></returns>
        private bool CheckDetailColumn()
        {
            var msgLogic = new MessageBoxShowLogic();
            foreach (Row row in this.gcMultiRow1.Rows)
            {
                if (!row.IsNewRow)
                {
                    // 金額
                    if (row.Cells[Const.CELL_NAME_KINGAKU].FormattedValue != null
                        && string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_KINGAKU].FormattedValue.ToString()))
                    {
                        msgLogic.MessageBoxShow("E148", "金額");
                        var cellKingaku = (GcCustomTextBoxCell)row.Cells[Const.CELL_NAME_KINGAKU];
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
                if (row.Cells[Const.CELL_NAME_SUURYOU].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_SUURYOU].FormattedValue.ToString()) &&
                    row.Cells[Const.CELL_NAME_TANKA].FormattedValue != null && !string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_TANKA].FormattedValue.ToString()))
                {
                    decimal suryou = decimal.Parse(row.Cells[Const.CELL_NAME_SUURYOU].FormattedValue.ToString());
                    decimal tanka = decimal.Parse(row.Cells[Const.CELL_NAME_TANKA].FormattedValue.ToString());
                    decimal kingaku = decimal.Parse(row.Cells[Const.CELL_NAME_KINGAKU].FormattedValue.ToString());
                    short kingakuHasuuCd = 3;

                    // 金額端数取得
                    if (row.Cells[Const.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                    {
                        short.TryParse(Convert.ToString(this.logic.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    else if (row.Cells[Const.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                    {
                        short.TryParse(Convert.ToString(this.logic.dto.torihikisakiShiharaiEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }

                    decimal tmpKingaku = Shougun.Function.ShougunCSCommon.Utility.CommonCalc.FractionCalc(suryou * tanka, kingakuHasuuCd);

                    if (!tmpKingaku.Equals(kingaku))
                    {
                        new MessageBoxShowLogic().MessageBoxShowError("数量と単価の乗算が金額と一致しない明細が存在します。");
                        return false;
                    }

                }
            }
            return true;
        }

        /// <summary>
        /// 受入番号のテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void UKETSUKE_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // この画面（G721）が呼び出された時の引数に受付番号が含まれていた場合、
            // 初回起動時にフラグが立たないようにするための対策
            if (!this.isArgumentUketsukeNumber)
            {
                // 受付番号のテキストが変更されたらフラグをたてる
                this.UketsukeNumberTextChangeFlg = true;
            }
            else
            {
                this.UketsukeNumberTextChangeFlg = false;
                this.isArgumentUketsukeNumber = false;
            }
        }

        /// <summary>
        /// 計量番号のテキストチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void KEIRYOU_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // この画面（G721）が呼び出された時の引数に計量番号が含まれていた場合、
            // 初回起動時にフラグが立たないようにするための対策
            if (!this.isArgumentKeiryouNumber)
            {
                // 計量番号のテキストが変更されたらフラグをたてる
                this.KeiryouNumberTextChangeFlg = true;
            }
            else
            {
                this.KeiryouNumberTextChangeFlg = false;
                this.isArgumentKeiryouNumber = false;
            }
        }

        // 「受入入力画面」の休動Checkを追加する　start
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

        #region 月次ロックチェック

        /// <summary>
        /// [登録処理用] 月次ロックされているのかの判定を行います
        /// </summary>
        /// <returns>月次ロック中：True</returns>
        internal bool GetsujiLockCheck()
        {
            bool returnVal = false;

            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if ((this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG) ||
                (this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                // 新規・削除は画面に表示されている伝票日付を使用
                DateTime getsujiShoriCheckDate = DateTime.Parse(this.DENPYOU_DATE.Value.ToString());

                // 月次処理中チェック
                if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(getsujiShoriCheckDate))
                {
                    returnVal = true;
                    if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        msgLogic.MessageBoxShow("E224", "登録");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E224", "削除");
                    }
                }
                // 月次ロックチェック
                else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(getsujiShoriCheckDate.Year.ToString()), short.Parse(getsujiShoriCheckDate.Month.ToString())))
                {
                    returnVal = true;
                    if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
                    {
                        msgLogic.MessageBoxShow("E223", "登録");
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E222", "削除");
                    }
                }
            }
            else
            {
                // 修正は伝票日付が変更されている可能性があるため変更前データと違う場合は画面起動から
                // 登録までの間に月次処理が行われていないか確認する。
                // 上記が問題なければ現在表示されている変更後の日付が月次処理中、月次処理済期間内かをチェックする
                DateTime beforDate = DateTime.Parse(this.logic.beforDto.entryEntity.DENPYOU_DATE.ToString());
                DateTime updateDate = DateTime.Parse(this.DENPYOU_DATE.Value.ToString());

                // 月次処理中チェック
                if ((beforDate.CompareTo(updateDate) != 0) &&
                    getsujiShoriCheckLogic.CheckGetsujiShoriChu(beforDate))
                {
                    returnVal = true;
                    msgLogic.MessageBoxShow("E224", "修正");
                }
                else if (getsujiShoriCheckLogic.CheckGetsujiShoriChu(updateDate))
                {
                    returnVal = true;
                    msgLogic.MessageBoxShow("E224", "修正");
                }
                // 月次ロックチェック
                else if ((beforDate.CompareTo(updateDate) != 0) &&
                    getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(beforDate.Year.ToString()), short.Parse(beforDate.Month.ToString())))
                {
                    returnVal = true;
                    msgLogic.MessageBoxShow("E223", "修正");
                }
                else if (getsujiShoriCheckLogic.CheckGetsujiShoriLock(short.Parse(updateDate.Year.ToString()), short.Parse(updateDate.Month.ToString())))
                {
                    returnVal = true;
                    msgLogic.MessageBoxShow("E223", "修正");
                }
            }

            return returnVal;
        }

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

            if (string.IsNullOrEmpty(this.KAKUTEI_KBN.Text))
            {
                this.KAKUTEI_KBN.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.DENPYOU_DATE.Text))
            {
                this.DENPYOU_DATE.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.URIAGE_DATE.Text) && !this.KAKUTEI_KBN.Text.Equals("2") && this.URIAGE_DATE.IsInputErrorOccured)
            {
                this.URIAGE_DATE.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.SHIHARAI_DATE.Text) && !this.KAKUTEI_KBN.Text.Equals("2") && this.SHIHARAI_DATE.IsInputErrorOccured)
            {
                this.SHIHARAI_DATE.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.GYOUSHA_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                this.TORIHIKISAKI_CD.Focus();
                return;
            }

            if (this.gcMultiRow1.Rows.Count == 1)
            {
                var stackcell = this.gcMultiRow1.Rows[0].Cells[Const.CELL_NAME_STAK_JYUURYOU];
                stackcell.GcMultiRow.Focus();
                stackcell.Selected = true;
            }
            else
            {
                foreach (var row in this.gcMultiRow1.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    var hinmeicell = row.Cells[Const.CELL_NAME_HINMEI_CD];
                    var suuryoucell = row.Cells[Const.CELL_NAME_SUURYOU];
                    var unitcell = row.Cells[Const.CELL_NAME_UNIT_CD];
                    var kingakucell = row.Cells[Const.CELL_NAME_KINGAKU];
                    this.gcMultiRow1.Rows[0].Cells[Const.CELL_NAME_STAK_JYUURYOU].Selected = false;

                    if (hinmeicell.Value == null || string.IsNullOrEmpty(hinmeicell.Value.ToString()))
                    {
                        this.DETAIL_TAB.SelectedTab = this.MEISAI_PAGE;
                        hinmeicell.Style.BackColor = Constans.ERROR_COLOR;
                        hinmeicell.GcMultiRow.Focus();
                        hinmeicell.Selected = true;
                        break;
                    }

                    if (suuryoucell.Value == null || string.IsNullOrEmpty(suuryoucell.Value.ToString()))
                    {
                        this.DETAIL_TAB.SelectedTab = this.MEISAI_PAGE;
                        suuryoucell.Style.BackColor = Constans.ERROR_COLOR;
                        suuryoucell.GcMultiRow.Focus();
                        suuryoucell.Selected = true;
                        return;
                    }

                    if (unitcell.Value == null || string.IsNullOrEmpty(unitcell.Value.ToString()))
                    {
                        this.DETAIL_TAB.SelectedTab = this.MEISAI_PAGE;
                        unitcell.Style.BackColor = Constans.ERROR_COLOR;
                        unitcell.GcMultiRow.Focus();
                        unitcell.Selected = true;
                        return;
                    }

                    if (kingakucell.Value == null || string.IsNullOrEmpty(kingakucell.Value.ToString()))
                    {
                        this.DETAIL_TAB.SelectedTab = this.MEISAI_PAGE;
                        kingakucell.Style.BackColor = Constans.ERROR_COLOR;
                        kingakucell.GcMultiRow.Focus();
                        kingakucell.Selected = true;
                        return;
                    }
                }
                this.gcMultiRow1.Rows[0].Cells[Const.CELL_NAME_STAK_JYUURYOU].Selected = false;
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

        // 品名手入力に関する機能 start
        internal string hinmeiCd = "";
        internal string hinmeiName = "";
        public void HINMEI_CD_PopupBeforeExecuteMethod()
        {
            this.hinmeiCd = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value);
            this.hinmeiName = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_NAME].Value);
        }

        public void HINMEI_CD_PopupAfterExecuteMethod()
        {            
            if (this.hinmeiCd == Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value) && this.hinmeiName == Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_NAME].Value))
            {
                return;
            }
            this.logic.GetHinmeiForPop(this.gcMultiRow1.CurrentRow);
            if (beforeValuesForDetail.ContainsKey(Const.CELL_NAME_HINMEI_CD))
            {
                beforeValuesForDetail[Const.CELL_NAME_HINMEI_CD] = Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value);
            }
        }
        // 品名手入力に関する機能 end

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
            this.logic.GyousyaCdSet();
        }

        public void NioroshiGyoushaPopupBefore()
        {
            this.logic.NioroshiGyoushaCdSet();
        }


        /// <summary>
        /// 取引先設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_TORIHIKISAKI_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 取引先チェック呼び出し
            this.SetTorihikisaki();
        }

        /// <summary>
        /// 業者設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_GYOUSHA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 業者チェック呼び出し
            this.MoveToGyoushaCd();
        }

        /// <summary>
        /// 現場設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_GENBA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 現場チェック呼び出し
            this.MoveToGenbaCd();
        }

        /// <summary>
        /// 荷降業者設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_NIOROSHI_GYOUSHA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 荷降業者チェック呼び出し
            this.SetNioroshiGyousha();
        }

        /// <summary>
        /// 荷降現場設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_NIOROSHI_GENBA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 荷降現場チェック呼び出し
            this.MoveToNioroshiGenbaCd();
        }

        /// <summary>
        /// 運搬業者設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_UNPAN_GYOUSHA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 運搬業者チェック呼び出し
            this.SetUnpanGyousha();
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

            if ((row.Cells[Const.CELL_NAME_TANKA].Value == null || string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_TANKA].Value.ToString())) &&
                (row.Cells[Const.CELL_NAME_KINGAKU].Value == null || string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_KINGAKU].Value.ToString())))
            {
                // 「単価」、「金額」どちらも空の場合、両方操作可
                this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_TANKA].ReadOnly = false;
                this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_KINGAKU].ReadOnly = false;
            }
            else if (row.Cells[Const.CELL_NAME_TANKA].Value != null && !string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_TANKA].Value.ToString()))
            {
                // 「単価」のみ入力済みの場合、「金額」操作不可
                this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_TANKA].ReadOnly = false;
                this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_KINGAKU].ReadOnly = true;
            }
            else if (row.Cells[Const.CELL_NAME_KINGAKU].Value != null && !string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_KINGAKU].Value.ToString()))
            {
                // 「金額」のみ入力済みの場合、「単価」操作不可
                this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_TANKA].ReadOnly = true;
                this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_KINGAKU].ReadOnly = false;
            }

            // 設定した背景色を反映
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_TANKA].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_KINGAKU].UpdateBackColor(false);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 明細背景色更新
        /// <summary>
        /// 明細背景色更新
        /// </summary>
        /// <param name="rowIndex"></param>
        internal void SetIchranHaikeiiro(int rowIndex)
        {
            LogUtility.DebugMethodStart(rowIndex);

            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_STAK_JYUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_EMPTY_JYUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_WARIFURI_JYUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_CHOUSEI_JYUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_WARIFURI_PERCENT].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_CHOUSEI_PERCENT].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_KEIRYOU_TIME].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_YOUKI_CD].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_YOUKI_SUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_YOUKI_JYUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_HINMEI_CD].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_HINMEI_NAME].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_SUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_UNIT_CD].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_TANKA].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_NET_JYUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_KINGAKU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_NISUGATA_SUURYOU].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_NISUGATA_UNIT_CD].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_ZAIKO_HINMEI_CD].UpdateBackColor(false);
            this.gcMultiRow1.Rows[rowIndex].Cells[Const.CELL_NAME_MEISAI_BIKOU].UpdateBackColor(false);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        private void GYOUSHA_CD_TextChanged(object sender, EventArgs e)
        {
            // 業者CD＝未入力　かつ　車輌名＝入力済み
            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.SHARYOU_NAME_RYAKU.Text))
            {
                //this.logic.GyoushaCursorInRirekeShow();
            }
        }

        private void rirekeIchiran_DoubleClick(object sender, EventArgs e)
        {
            // 履歴設定
            this.logic.RirekeSet();
        }

        private void STACK_KEIRYOU_TIME_Enter(object sender, EventArgs e)
        {
            this.STACK_KEIRYOU_TIME.Text = this.STACK_KEIRYOU_TIME.Text.Replace(":", "");
        }

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
        /// 確定区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KAKUTEI_KBN_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckKakuteiKbn())
            {
                return;
            }
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

        // <summary>
        /// ROW選択時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void rirekeIchiran_RowEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].ReadOnly = true;
            this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].Style.ForeColor = Color.Black;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細の行移動処理
        /// 明細の行が増減するたびに必ず実行してください
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void rirekeIchiran_RowLeave(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.rirekeIchiran.Focused)
            {
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].ReadOnly = false;
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].Style.ForeColor = Color.White;
            }
            else
            {
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].ReadOnly = true;
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].Style.ForeColor = Color.Black;
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_UE"].Style.SelectionForeColor = Color.Black;
            }

            LogUtility.DebugMethodEnd();
        }

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

                    if (forward)
                    {
                        if (this.ActiveControl != null && (this.ActiveControl.Name == "DETAIL_TAB" && this.DETAIL_TAB.SelectedTab == this.MEISAI_PAGE))
                        {
                            this.gcMultiRow1[0, this.logic.firstIndexDetailCellName].Selected = true;
                            return;
                        }

                        if (this.ActiveControl != null && (this.ActiveControl.Name == "gcMultiRow1" || this.ActiveControl.Name == "DETAIL_TAB" && this.DETAIL_TAB.SelectedTab == this.MEISAI_PAGE))
                        {
                            this.gcMultiRow1[0, this.logic.firstIndexDetailCellName].Selected = true;
                            return;
                        }
                        if (this.ismobile_mode && this.ActiveControl != null && (this.ActiveControl.Name == "gcMultiRow2" || (this.ActiveControl.Name == "DETAIL_TAB" && this.DETAIL_TAB.SelectedTab == this.UKEIRE_JISSEKI_PAGE)))
                        {
                            this.gcMultiRow2[0, this.logic.firstIndexJissekiCellName].Selected = true;
                        }
                        if (this.IS_TAB_CONTROL_ACTIVED(this.ActiveControl))
                            return;

                        if (this.beforbeforControlName == this.SHIHARAI_DATE.Name || this.beforbeforControlName == this.KEIRYOU_PRIRNT_KBN_VALUE.Name)
                            return;

                        if (this.isSetShokuchiForcus)
                        {
                            this.isSetShokuchiForcus = false;
                        }
                        else
                        {
                            this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();
                            this.logic.GotoNextControl(true);
                        }
                    }
                    else
                    {
                        if (this.IS_TAB_CONTROL_ACTIVED(this.ActiveControl))
                            return;

                        if (this.isSetShokuchiForcus)
                        {
                            // 諸口区分によるフォーカス移動の場合、ここで判定用のフラグを戻す
                            this.isSetShokuchiForcus = false;

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

                            this.logic.GotoNextControl(false);
                        }
                        else
                        {
                            this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();
                            this.logic.GotoNextControl(false);
                        }
                    }
                }
            }
            //20211230 Thanh 158916 s
            if (e.KeyChar == (char)Keys.Space)
            {
                if (this.gcMultiRow1.CurrentCell != null && this.gcMultiRow1.CurrentCell.Name == "TANKA")
                {
                    if (this.gcMultiRow1.CurrentCell.IsInEditMode)
                    {
                        if (e.KeyChar == (Char)Keys.Space)
                        {
                            this.OpenTankaRireki(this.gcMultiRow1.CurrentRow.Index);
                        }
                    }
                }
            }
            //20211230 Thanh 158916 e
        }

        public void PopupAfter_SHARYOU_CD()
        {
            this.editingSharyouCdFlag = true;

            if (!this.logic.CheckSharyou())
            {
                return;
            }

            this.editingSharyouCdFlag = false;
        }

        //////////////////////////////////////////////////
        ///受入実績
        //////////////////////////////////////////////////

        /// <summary>
        /// 行追加後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            //// Dictionaryへの追加
            //this.logic.AddRowDic(e.RowIndex);
        }

        /// <summary>
        /// 行削除中イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_RowsRemoving(object sender, RowsRemovingEventArgs e)
        {
            //// Dictionaryからの削除
            //this.logic.RemoveRowDic(e.RowIndex);
        }
        
        internal string hinmeiCd2 = "";
        internal string hinmeiName2 = "";
        public void HINMEI_CD_PopupBeforeExecuteMethod2()
        {
            this.hinmeiCd2 = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value);
            this.hinmeiName2 = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[Const.CELL_NAME_HINMEI_NAME].Value);
        }

        public void HINMEI_CD_PopupAfterExecuteMethod2()
        {
            if (this.hinmeiCd2 == Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value) && this.hinmeiName2 == Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[Const.CELL_NAME_HINMEI_NAME].Value))
            {
                return;
            }
            this.logic.GetHinmeiForPop2(this.gcMultiRow2.CurrentRow);
            if (beforeValuesForJisseki.ContainsKey(Const.CELL_NAME_HINMEI_CD))
            {
                beforeValuesForJisseki[Const.CELL_NAME_HINMEI_CD] = Convert.ToString(this.gcMultiRow2.CurrentRow.Cells[Const.CELL_NAME_HINMEI_CD].Value);
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

            row = this.gcMultiRow2.CurrentRow;

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
            if (e.CellName != Const.CELL_NAME_HINMEI_CD)
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
                    case Const.CELL_NAME_HINMEI_CD:
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

            this.isHinmeiReLoad = false;

            switch (e.CellName)
            {
                case Const.CELL_NAME_HINMEI_NAME:

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
                case Const.CELL_NAME_HINMEI_CD:
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
                            this.gcMultiRow2.CurrentRow.Cells[Const.CELL_NAME_HINMEI_NAME].Value = "";
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
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E020", "品名");
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
        /// 行追加後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMultiRow2_RowEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // マニフェスト連携用のデータを設定
            Row row = this.gcMultiRow2.CurrentRow;
            Cell systemId = row.Cells[Const.CELL_NAME_DENPYOU_SYSTEM_ID];
            Cell JissekiSeq = row.Cells[Const.CELL_NAME_JISSEKI_SEQ];
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
        /// 行削除中イベント
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
            LogUtility.DebugMethodEnd(sender, e);
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

                    if (row.Cells[Const.CELL_NAME_HINMEI_CD].Value == null
                        || string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        if (!(row.Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value == null
                            || string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value.ToString())))
                        {
                            rowH = rowH + 1;
                            var cellhinmeiCD = (GcCustomTextBoxCell)row.Cells[Const.CELL_NAME_HINMEI_CD];
                            cellhinmeiCD.IsInputErrorOccured = true;
                        }
                    }
                    else if (row.Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value == null
                        || string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value.ToString()))
                    {
                        //if (!(row.Cells[Const.CELL_NAME_HINMEI_CD].Value == null
                        //    || string.IsNullOrEmpty(row.Cells[Const.CELL_NAME_HINMEI_CD].Value.ToString())))
                        //{
                        //    rowW = rowW + 1;
                        //    var cellwariaiCD = (GcCustomTextBoxCell)row.Cells[Const.CELL_NAME_SUURYOU_WARIAI];
                        //    cellwariaiCD.IsInputErrorOccured = true;
                        //}
                    }
                    else
                    {
                        decimal netSuuryouWariai = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value), out netSuuryouWariai);
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
                    if (this.gcMultiRow2.Rows[i].Cells[Const.CELL_NAME_HINMEI_CD].Value == null
                        || string.IsNullOrEmpty(this.gcMultiRow2.Rows[i].Cells[Const.CELL_NAME_HINMEI_CD].Value.ToString()))
                    {
                        if (this.gcMultiRow2.Rows[i].Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value == null
                            || string.IsNullOrEmpty(this.gcMultiRow2.Rows[i].Cells[Const.CELL_NAME_SUURYOU_WARIAI].Value.ToString()))
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

        private void SAGYOU_BIKOU_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = this.ActiveControl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENPYOU_RIREKI_BTN_Click(object sender, EventArgs e)
        {
            this.logic.CalDenpyouRireki();
        }

        //PhuocLoc 2020/12/01 #136219 -Start
        public void TairyuDataShow(string strDenpyouNo)
        {
            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
            this.UkeireNumber = Convert.ToInt32(strDenpyouNo);
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
        }

        private void DETAIL_TAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DETAIL_TAB.SelectedTab == this.TAIRYU_LIST_TAB)
            {
                this.logic.GetTairyuData();
            }
        }
        //PhuocLoc 2020/12/01 #136219 -End

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

        private void SHARYOU_NAME_RYAKU_Validated(object sender, EventArgs e)
        {
            this.logic.RirekeDisplay();
        }

        //20211230 Thanh 158916 s
        /// <summary>
        /// OpenTankaRireki
        /// </summary>
        private void OpenTankaRireki(int index)
        {
            string kyotenCd = string.Empty;
            string torihikisakiCd = string.Empty;
            string gyoushaCd = string.Empty;
            string genbaCd = string.Empty;
            string unpanGyoushaCd = string.Empty;
            string nizumiGyoushaCd = string.Empty;
            string nizumiGenbaCd = string.Empty;
            string nioroshiGyoushaCd = string.Empty;
            string nioroshiGenbaCd = string.Empty;
            string HinmeiCd = Convert.ToString(this.gcMultiRow1.Rows[index].Cells[Const.CELL_NAME_HINMEI_CD].Value);
            string UnitCd = Convert.ToString(this.gcMultiRow1.Rows[index].Cells[Const.CELL_NAME_UNIT_CD].Value);

            if (!string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
            {
                kyotenCd = this.logic.headerForm.KYOTEN_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
            {
                torihikisakiCd = this.TORIHIKISAKI_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                gyoushaCd = this.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                genbaCd = this.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text))
            {
                unpanGyoushaCd = this.UNPAN_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                nioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                nioroshiGenbaCd = this.NIOROSHI_GENBA_CD.Text;
            }
            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G721",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, HinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
            if (tankaForm.dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tankaForm.returnTanka.IsNull)
                {
                    this.gcMultiRow1.EditingControl.Text = string.Empty;
                }
                else
                {
                    this.gcMultiRow1.EditingControl.Text = tankaForm.returnTanka.Value.ToString(this.logic.dto.sysInfoEntity.SYS_TANKA_FORMAT);
                }

                if (!UnitCd.Equals(tankaForm.returnUnitCd))
                {
                    if (string.IsNullOrEmpty(tankaForm.returnUnitCd))
                    {
                        this.gcMultiRow1.Rows[index].Cells[Const.CELL_NAME_UNIT_CD].Value = string.Empty;
                        this.gcMultiRow1.Rows[index].Cells[Const.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                    }
                    else
                    {
                        var units = this.logic.accessor.GetUnit(Convert.ToInt16(tankaForm.returnUnitCd));

                        if (units != null)
                        {
                            this.gcMultiRow1.Rows[index].Cells[Const.CELL_NAME_UNIT_CD].Value = units[0].UNIT_CD.ToString();
                            this.gcMultiRow1.Rows[index].Cells[Const.CELL_NAME_UNIT_NAME_RYAKU].Value = units[0].UNIT_NAME_RYAKU.ToString();

                            if (!this.logic.CalcSuuryou(this.gcMultiRow1.Rows[index]))
                            {
                                return;
                            }
                            if (!this.logic.CalcDetaiKingaku(this.gcMultiRow1.Rows[index]))
                            {
                                return;
                            }
                            if (!this.logic.SetHinmeiSuuryou(Const.CELL_NAME_UNIT_CD, this.gcMultiRow1.Rows[index], true))
                            {
                                return;
                            }
                            if (!this.logic.CalcTotalValues())
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
        //20211230 Thanh 158916 e
    }
}