// $Id: UIForm.cs 56556 2015-07-23 08:56:27Z wuq@oec-h.com $
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Function.ShougunCSCommon.Const;
using r_framework.CustomControl;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Function.ShougunCSCommon.Dto;
using r_framework.Dto;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using r_framework.FormManager;
using System.Collections.ObjectModel;
using Shougun.Core.Common.ContenaShitei.Utility;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.ContenaShitei.DTO;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Seasar.Framework.Exceptions;
using Shougun.Core.SalesPayment.TankaRirekiIchiran;

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        #region マニフェスト連携用変数
        public short RenkeiDenshuKbnCd { get; private set; }
        public long RenkeiSystemId { get; private set; }
        public long RenkeiMeisaiSystemId { get; private set; }
        #endregion

        //伝票発行時に渡すパラメータ
        const string DENPYO_KBN = "3";

        #region フィールド
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 受入/支払入力のSYSTE_ID
        /// </summary>
        public long UrShSysId = -1;

        /// <summary>
        /// 受入/支払のSEQ
        /// </summary>
        public int UrShSEQ = -1;

        /// <summary>
        /// 受入/支払明細のSYSTE_ID
        /// </summary>
        public long MeisaiSysId = -1;

        /// <summary>
        /// 受入/支払番号
        /// </summary>
        public long UrShNumber = -1;

        /// <summary>
        /// SEQ
        /// このパラメータが０以外だとDeleteFlgを無視して表示する
        /// </summary>
        public string SEQ = "0";

        /// <summary>
        /// 受付番号
        /// </summary>
        public long UketukeNumber = -1;

        // 20140512 kayo No.679 計量番号連携 start
        /// <summary>
        /// 計量番号
        /// </summary>
        public long KeiryouNumber = -1;
        // 20140512 kayo No.679 計量番号連携 end

        /// <summary>
        /// 受付番号テキストチェンジフラグ
        /// </summary>
        public bool UketsukeNumberTextChangeFlg = false;

        /// <summary>
        /// この画面が呼び出された時、受付番号が引数に存在したかを示します
        /// /// </summary>
        internal bool isArgumentUketsukeNumber = false;

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
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 画面遷移が発生するコントロール名一覧
        /// </summary>
        private string[] controlNamesForChangeScreenEvents = new string[] { "bt_func7", "bt_func12" };

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 伝票発行ポップアップ用DTO
        /// </summary>
        internal ParameterDTOClass denpyouHakouPopUpDTO = new ParameterDTOClass();

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;

                /// <summary>
        /// 品名ポップアップから品名が選択されたかどうか判断するためのフラグ
        /// </summary>
        internal bool bSelectHinmeiPopup = false;

        /// <summary>
        /// この画面で登録が実行されたかどうかの判定フラグ
        /// 継続入力時の初期化で使用するフラグ
        /// </summary>
        internal bool isRegisted = false;

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
        /// セルの編集状態の設定やコミットをやるか判断するフラグ
        /// </summary>
        internal bool notEditingOperationFlg = false;

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
        /// 車輌CDが編集中かどうかのフラグ
        /// </summary>
        private bool editingSharyouCdFlag = false;

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

        internal bool validateFlag = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        internal bool isInputError = false;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="windowType">モード</param>
        /// <param name="urShNumber">売上／支払入力 UR_SH_NUMBER</param>
        /// <param name="lastRunMethod">受入入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, long urShNumber = -1, LastRunMethod lastRunMethod = null, long uketsukeNumber = -1, long keiryouNumber = -1, string SEQ = "0")
            : base(WINDOW_ID.T_URIAGE_SHIHARAI, windowType)
        {
            // 20140512 kayo No.679 計量番号連携 start
            //LogUtility.DebugMethodStart(windowId, windowType, urShNumber, lastRunMethod, uketsukeNumber);
            LogUtility.DebugMethodStart(windowId, windowType, urShNumber, lastRunMethod, uketsukeNumber, keiryouNumber, SEQ);
            // 20140512 kayo No.679 計量番号連携 end
            CommonShogunData.Create(SystemProperty.Shain.CD);

            this.InitializeComponent();
            this.UrShNumber = urShNumber;
            this.closeMethod = lastRunMethod;
            if (uketsukeNumber != -1)
            {
                this.isArgumentUketsukeNumber = true;
            }
            this.UketukeNumber = uketsukeNumber;
            // 20140512 kayo No.679 計量番号連携 start
            this.KeiryouNumber = keiryouNumber;
            // 20140512 kayo No.679 計量番号連携 end
            this.WindowId = windowId;
            if (string.IsNullOrEmpty(SEQ))
            {
                SEQ = "0";
            }
            this.SEQ = SEQ;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // マニフェスト連携用変数の初期化
            RenkeiDenshuKbnCd = (short)SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI;
            RenkeiSystemId = -1;
            RenkeiMeisaiSystemId = -1;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            // 20140512 kayo No.679 計量番号連携 start
            //LogUtility.DebugMethodEnd(windowType, windowType, urShNumber, lastRunMethod, uketsukeNumber);
            LogUtility.DebugMethodEnd(windowType, windowType, urShNumber, lastRunMethod, uketsukeNumber, keiryouNumber, SEQ);
            // 20140512 kayo No.679 計量番号連携 end
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


        #region イベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool catchErr = false;
            bool isOpenFormError = this.logic.GetAllEntityData(out catchErr);
            if (catchErr)
            {
                return;
            }
            ParentBaseForm = (BusinessBaseForm)this.Parent;

            //PhuocLoc 2020/05/20 #137147 -Start
            // Anchorの設定は必ずOnLoadで行うこと
            if (this.mrwDetail != null)
            {
                this.mrwDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            //PhuocLoc 2020/05/20 #137147 -End

            catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }
            catchErr = this.logic.ButtonInit();
            if (catchErr)
            {
                return;
            }
            catchErr = this.logic.EventInit();
            if (catchErr)
            {
                return;
            }

            if (!isOpenFormError)
            {
                base.CloseTopForm();
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
            this.logic.SetTopControlFocus();   // No.3822

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

        /// <summary>
        /// 重量値、金額値用フォーマット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NumberFormat(object sender, EventArgs e)
        {
            this.logic.ToAmountValue(sender);
        }

        /// <summary>
        /// 売上/支払番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ENTRY_NUMBER_Validated(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            if (!nowLoding)
            {
                nowLoding = true;
                long urshNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text)
                    && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out urshNumber))
                {
                    if (this.UrShNumber != urshNumber)  // No.2175
                    {
                        this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        this.UrShNumber = urshNumber;

                        //base.OnLoad(e);
                        bool catchErr = false;
                        if (!this.logic.GetAllEntityData(out catchErr) && !catchErr)
                        {
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.UrShNumber = -1;

                            // 再描画を有効にして最新の状態に更新
                            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));

                            this.ENTRY_NUMBER.Focus();
                            nowLoding = false;
                            return;
                        }
                        else if (catchErr)
                        {
                            return;
                        }

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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

                        catchErr = this.logic.WindowInit();
                        if (catchErr)
                        {
                            return;
                        }
                        catchErr = this.logic.ButtonInit();
                        if (catchErr)
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

        #region 20151002 hoanghm #12553

        ///// <summary>
        ///// 売上日付更新後処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void URIAGE_DATE_OnLeave(object sender, EventArgs e)
        //{
        //    this.ActiveControl = this.ActiveControl;    // 一桁入力された場合にここで値を確定
        //    if (!beforeUrageDate.Equals(this.URIAGE_DATE.Text))
        //    {
        //        this.logic.SetUriageShouhizeiRate();        // 売上消費税率設定
        //    }
        //}

        ///// <summary>
        ///// 支払日付更新後処理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void SHIHARAI_DATE_OnLeave(object sender, EventArgs e)
        //{
        //    this.ActiveControl = this.ActiveControl;    // 一桁入力された場合にここで値を確定
        //    if (!beforeShiharaiDate.Equals(this.SHIHARAI_DATE.Text))
        //    {
        //        this.logic.SetShiharaiShouhizeiRate();
        //    }
        //}

        /// <summary>
        /// Validatedの売上日付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_DATE_Validated(object sender, EventArgs e)
        {
            this.ActiveControl = this.ActiveControl;    // 一桁入力された場合にここで値を確定
            if (!beforeUrageDate.Equals(this.URIAGE_DATE.Text))
            {
                this.logic.SetUriageShouhizeiRate();        // 売上消費税率設定
            }
        }

        /// <summary>
        /// Validatedの支払日付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_DATE_Validated(object sender, EventArgs e)
        {
            this.ActiveControl = this.ActiveControl;    // 一桁入力された場合にここで値を確定
            if (!beforeShiharaiDate.Equals(this.SHIHARAI_DATE.Text))
            {
                this.logic.SetShiharaiShouhizeiRate();
            }
        }

        #endregion

        /// <summary>
        /// 売上消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_SHOUHIZEI_RATE_VALUE_TextChanged(object sender, EventArgs e)
        {
            bool catchErr = false;
            this.URIAGE_SHOUHIZEI_RATE_VALUE.Text = this.logic.ToPercentForUriageShouhizeiRate(out catchErr);
        }

        /// <summary>
        /// 売上消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHIHARAI_SHOUHIZEI_RATE_VALUE_TextChanged(object sender, EventArgs e)
        {
            bool catchErr = false;
            this.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = this.logic.ToPercentForShiharaiShouhizeiRate(out catchErr);
        }

        /// <summary>
        /// ヘッダーの拠点更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void KYOTEN_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.CheckKyotenCd();
        }

        #region 取引先イベント
        /// <summary>
        /// 取引先フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            //bool catchErr = false;
            //// 取引先を取得
            //var torihikisaki = this.logic.accessor.GetTorihikisaki(this.TORIHIKISAKI_CD.Text, this.DENPYOU_DATE.Text, this.logic.footerForm.sysDate, out catchErr);
            //if (catchErr)
            //{
            //    return;
            //}
            //if (torihikisaki != null)
            //{
            //    // 諸口区分の前回値を取得
            //    this.oldShokuchiKbn = (bool)torihikisaki.SHOKUCHI_KBN;
            //}

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

            bool catchErr = false;
            this.isNotMoveFocus = this.SetTorihikisaki(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
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
            //bool catchErr = false;
            //// 業者を取得
            //var gyousha = this.logic.accessor.GetGyousha(this.GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate, out catchErr);
            //if (catchErr)
            //{
            //    return;
            //}
            //if (gyousha != null)
            //{
            //    // 諸口区分の前回値を取得
            //    this.oldShokuchiKbn = (bool)gyousha.SHOKUCHI_KBN;
            //}

            if (!this.isInputError)
            {
                this.logic.GyousyaCdSet();  //比較用業者CDをセット
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
            bool catchErr = false;
            this.isNotMoveFocus = this.SetGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.validateFlag = false;
            this.oldShokuchiKbn = false;
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
            //bool catchErr = false;
            //// 現場を取得
            //var genba = this.logic.accessor.GetGenba(this.GYOUSHA_CD.Text, this.GENBA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate, out catchErr);
            //if (catchErr)
            //{
            //    return;
            //}
            //if (genba != null)
            //{
            //    // 諸口区分の前回値を取得
            //    this.oldShokuchiKbn = (bool)genba.SHOKUCHI_KBN;
            //}

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
            bool catchErr = false;
            this.isNotMoveFocus = this.SetGenba(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }
            this.validateFlag = false;
            this.oldShokuchiKbn = false;
        }
        #endregion 現場イベント

        /// <summary>
        /// 荷積業者CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷積業者を取得
            var nizumiGyousha = this.logic.accessor.GetGyousha(this.NIZUMI_GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (nizumiGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nizumiGyousha.SHOKUCHI_KBN;
            }

            this.logic.NizumiGyoushaCdSet();  //比較用業者CDをセット
        }

        /// 荷積業者更新後処理
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNizumiGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 荷積現場CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷降現場を取得
            var nioroshiGenba = this.logic.accessor.GetGenba(this.NIZUMI_GYOUSHA_CD.Text, this.NIZUMI_GENBA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (nioroshiGenba != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGenba.SHOKUCHI_KBN;
            }

            this.logic.NizumiGenbaCdSet();  //比較用業者CDをセット

        }

        /// <summary>
        /// 荷積現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIZUMI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNizumiGenba(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// 荷降業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷降業者を取得
            var nioroshiGyousha = this.logic.accessor.GetGyousha(this.NIOROSHI_GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (nioroshiGyousha != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)nioroshiGyousha.SHOKUCHI_KBN;
            }

            this.logic.NioroshiGyoushaCdSet();   // 比較用現場CDをセット               
        }

        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.isNotMoveFocus = true;

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNioroshiGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 荷降現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 荷降現場を取得
            var nioroshiGenba = this.logic.accessor.GetGenba(this.NIOROSHI_GYOUSHA_CD.Text, this.NIOROSHI_GENBA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                return;
            }
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

            bool catchErr = false;
            this.isNotMoveFocus = this.SetNioroshiGenba(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 運搬業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            bool catchErr = false;
            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            // 運搬業者を取得
            var unpanGyousha = this.logic.accessor.GetGyousha(this.UNPAN_GYOUSHA_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate.Date, out catchErr);
            if (catchErr)
            {
                return;
            }
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

            bool catchErr = false;
            this.isNotMoveFocus = this.SetUnpanGyousha(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (isNotMoveFocus)
            {
                base.OnKeyDown(this.keyEventArgs);
            }

            this.oldShokuchiKbn = false;
        }

        /// <summary>
        /// 入力担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NYUURYOKU_TANTOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            this.logic.CheckNyuuryokuTantousha();
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
            this.logic.CheckSharyou();
            this.editingSharyouCdFlag = false;
        }

        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckEigyouTantousha();
        }

        //private void SHARYOU_CD_Enter(object sender, EventArgs e)
        //{
        //    this.SHARYOU_CD.PopupWindowName = string.Empty;
        //}


        /// <summary>
        /// 車輌名フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_NAME_RYAKU_Enter(object sender, EventArgs e)
        {
            //参照モード、削除モードの場合は処理を行わない
            if (WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                return;
            }

            isSelectingSharyouCd = false;
            if (this.SHARYOU_NAME_RYAKU.ReadOnly)
            {
                var isPressShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                this.SelectNextControl(this.SHARYOU_NAME_RYAKU, !isPressShift, false, false, true);
            }
        }

        /// <summary>
        /// 形態区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEITAI_KBN_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKeitaiKbn();
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
        /// 確定区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KAKUTEI_KBN_Validated(object sender, EventArgs e)
        {
            this.logic.CheckKakuteiKbn();
        }

        /// <summary>
        /// 伝票日付検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENPYOU_DATE_Validated(object sender, EventArgs e)
        {
            //20151002 hoanghm start
            var inputDenpyouDate = this.DENPYOU_DATE.Text;
            if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.logic.tmpDenpyouDate.Equals(inputDenpyouDate))
            {
                bool catchErr = this.logic.CheckDenpyouDate();
                if (catchErr)
                {
                    return;
                }
                
                //ThangNguyen [Add] 20150828 #12553 Start
                this.beforeUrageDate = this.URIAGE_DATE.Text;
                this.beforeShiharaiDate = this.SHIHARAI_DATE.Text;

                this.URIAGE_DATE.Value = this.DENPYOU_DATE.Value;
                this.SHIHARAI_DATE.Value = this.DENPYOU_DATE.Value;
                this.URIAGE_DATE_Validated(sender, e);
                this.SHIHARAI_DATE_Validated(sender, e);
                //ThangNguyen [Add] 20150828 #12553 End
            }
            //20151002 hoanghm end
        }

        /// <summary>
        /// 売上/支払番号前ボタンクリック処理
        /// 現在入力されている番号の前の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void previousButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));
            this.Enabled = false;

            this.previousButtonMainProcess(sender, e);

            // 再描画を有効にして最新の状態に更新
            this.Enabled = true;
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            this.logic.SetTopControlFocus();
        }

        /// <summary>
        /// 前ボタンメイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousButtonMainProcess(object sender, EventArgs e)
        {
            if (this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                return;
            }

            if (!nowLoding)
            {
                bool catchErr = false;
                nowLoding = true;
                long urshNumber = 0;
                long preurshNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;

                // No.1767
                if (string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                {
                    urshNumber = this.logic.GetMaxUrshNumber(out catchErr);
                    //this.ENTRY_NUMBER.Text = urshNumber.ToString();
                    if (catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out urshNumber);
                }

                //if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text) && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out urshNumber))
                //if (!string.IsNullOrEmpty(urshNumber.ToString()))
                //{
                // 売上／支払番号の入力がある場合
                preurshNumber = this.logic.GetPreUrshNumber(urshNumber, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (preurshNumber <= 0)
                {
                    if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                    {
                        long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out preurshNumber);
                    }
                }
                if (preurshNumber > 0)
                {
                    // 入力されている売上／支払番号の前の売上／支払番号が取得できた場合
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.UrShNumber = preurshNumber;
                    //base.OnLoad(e);
                    if (!this.logic.GetAllEntityData(out catchErr) && !catchErr)
                    {
                        // エラー発生時には値をクリアする
                        this.ChangeNewWindow(sender, e);
                        nowLoding = false;
                        return;
                    }
                    else if (catchErr)
                    {
                        return;
                    }

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            // 修正権限は無いが参照権限がある場合は参照モードで起動
                            this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            HeaderFormInit();
                        }
                        else
                        {
                            // どちらも無い場合はアラートを表示して処理中断
                            this.ENTRY_NUMBER.Text = string.Empty;
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            msg.MessageBoxShow("E158", "修正");
                            this.WindowType = tmpType;
                            HeaderFormInit();
                            nowLoding = false;
                            return;
                        }
                    }

                    catchErr = this.logic.WindowInit();
                    if (catchErr)
                    {
                        return;
                    }
                    catchErr = this.logic.ButtonInit();
                    if (catchErr)
                    {
                        return;
                    }

                    // スクロールバーが下がる場合があるため、
                    // 強制的にバーを先頭にする
                    this.AutoScrollPosition = new Point(0, 0);
                }
                else
                {
                    // 入力されている売上／支払番号の前の売上／支払番号が取得できなかった場合
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    nowLoding = false;
                    return;
                }
                //}
                nowLoding = false;
            }
        }

        /// <summary>
        /// 売上/支払番号後ボタンクリック処理
        /// 現在入力されている番号の後の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void nextButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));
            this.Enabled = false;

            this.nextButtonMainProcess(sender, e);

            // 再描画を有効にして最新の状態に更新
            this.Enabled = true;
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            this.logic.SetTopControlFocus();
        }

        /// <summary>
		/// 次ボタンメイン処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void nextButtonMainProcess(object sender, EventArgs e)
        {
            if (this.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
            {
                return;
            }

            if (!nowLoding)
            {
                bool catchErr = false;
                nowLoding = true;
                long urshNumber = 0;
                long preurshNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;

                // No.3341-->
                if (string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                {
                    urshNumber = this.logic.GetMaxUrshNumber(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out urshNumber);
                }
                //if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text)
                //    && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out urshNumber))
                //{
                // No.3341<--
                // 売上／支払番号の入力がある場合
                preurshNumber = this.logic.GetNextUrshNumber(urshNumber, out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (preurshNumber <= 0)
                {
                    if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                    {
                        long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out preurshNumber);
                    }
                }
                if (preurshNumber > 0)
                {
                    // 入力されている売上／支払番号の後の売上／支払番号が取得できた場合
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.UrShNumber = preurshNumber;
                    //base.OnLoad(e);
                    if (!this.logic.GetAllEntityData(out catchErr) && !catchErr)
                    {
                        // エラー発生時には値をクリアする
                        this.ChangeNewWindow(sender, e);
                        nowLoding = false;
                        return;
                    }
                    else if (catchErr)
                    {
                        return;
                    }

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            // 修正権限は無いが参照権限がある場合は参照モードで起動
                            this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                            HeaderFormInit();
                        }
                        else
                        {
                            // どちらも無い場合はアラートを表示して処理中断
                            this.ENTRY_NUMBER.Text = string.Empty;
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            msg.MessageBoxShow("E158", "修正");
                            this.WindowType = tmpType;
                            HeaderFormInit();
                            nowLoding = false;
                            return;
                        }
                    }

                    catchErr = this.logic.WindowInit();
                    if (catchErr)
                    {
                        return;
                    }
                    catchErr = this.logic.ButtonInit();
                    if (catchErr)
                    {
                        return;
                    }

                    // スクロールバーが下がる場合があるため、
                    // 強制的にバーを先頭にする
                    this.AutoScrollPosition = new Point(0, 0);
                }
                else
                {
                    // 入力されている売上／支払番号の後の売上／支払番号が取得できなかった場合
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    nowLoding = false;
                    return;
                }
                //}     // No.3341
                nowLoding = false;
            }
        }

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const uint WM_SETREDRAW = 0x000B;

        /// <summary>
        /// 運転者CDチェック
        /// </summary>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            this.logic.CheckUntensha();
        }

        /// <summary>
        /// 新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeNewWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            // 追加権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.NEW_WINDOW_FLAG))
            {
                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                bool catchErr = this.InitNumbers(true);
                if (catchErr)
                {
                    return;
                }
                this.SEQ = "0";

                //base.OnLoad(e);
                //this.logic = new LogicClass(this);    // No.3822関連 logic.pressedEnterOrTabが二重になるため処理変更
                catchErr = this.logic.LocalDataInit();             // No.3822
                if (catchErr)
                {
                    return;
                }
                this.logic.GetAllEntityData(out catchErr);
                if (catchErr)
                {
                    return;
                }

                catchErr = this.logic.WindowInit();
                if (catchErr)
                {
                    return;
                }
                catchErr = this.logic.ButtonInit();
                if (catchErr)
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
            this.logic.SetTopControlFocus();

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

            FormManager.OpenFormWithAuth("G055", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.URIAGE_SHIHARAI, CommonShogunData.LOGIN_USER_INFO.SHAIN_CD);

            LogUtility.DebugMethodEnd();
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

            if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.NEW_WINDOW_FLAG))
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
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool isF9 = true;
            M_SYS_INFO mSysInfo = this.logic.sysInfoDao.GetAllData()[0];
            // 計量票出力区分
            if (!mSysInfo.DENPYOU_HAKOU_HYOUJI.IsNull)
            {
                if (Convert.ToString(mSysInfo.DENPYOU_HAKOU_HYOUJI).Equals("1"))
                {
                    isF9 = false;
                }
            }

            this.RegistDataProcess(sender, e, isF9);

            if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.NEW_WINDOW_FLAG))
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
        private void RegistDataProcess(object sender, EventArgs e, bool isF9)
        {
            // 20141114 ブン 「2重登録チェック」に完了を追加する　start
            if (this.logic.footerForm.bt_func9.Enabled)
            {
                this.logic.footerForm.bt_func9.Enabled = false;
            }
            else
            {
                return;
            }
            // 20141114 ブン 「2重登録チェック」に完了を追加する　end

            /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
            //作業日check
            if (!this.logic.SharyouDateCheck())
            {
                this.SHARYOU_CD.Focus();
                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                this.logic.footerForm.bt_func9.Enabled = true;
                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                return;
            }
            else if (!this.logic.UntenshaDateCheck())
            {
                this.UNTENSHA_CD.Focus();
                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                this.logic.footerForm.bt_func9.Enabled = true;
                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                return;
            }
            else if (!this.logic.HannyuusakiDateCheck())
            {
                this.NIOROSHI_GENBA_CD.Focus();
                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                this.logic.footerForm.bt_func9.Enabled = true;
                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                return;
            }
            /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end

            base.RegistErrorFlag = false;
            bool catchErr = false;
            // 取引先と拠点コードの関連チェック
            if (!this.logic.CheckTorihikisakiAndKyotenCd(null, this.TORIHIKISAKI_CD.Text, out catchErr))
            {
                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                this.logic.footerForm.bt_func9.Enabled = true;
                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                return;
            }

            // 登録前にもう一度計算する
            catchErr = this.logic.CalcDetail();
            if (catchErr)
            {
                this.logic.footerForm.bt_func9.Enabled = true;
                return;
            }
            // 売上日付、支払日付の入力チェック要不要を確定区分の入力により再判断
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> excitChecks = new Collection<SelectCheckDto>();
            excitChecks.Add(existCheck);
            catchErr = this.logic.RequiredSettingInit();
            if (catchErr)
            {
                this.logic.footerForm.bt_func9.Enabled = true;
                return;
            }
            catchErr = this.logic.SetRequiredSetting();
            if (catchErr)
            {
                this.logic.footerForm.bt_func9.Enabled = true;
                return;
            }
            bool isKotaiKanri = false;
            if (this.logic.dto.sysInfoEntity != null
                && !this.logic.dto.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                && (int)this.logic.dto.sysInfoEntity.CONTENA_KANRI_HOUHOU == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
            {
                isKotaiKanri = true;
            }

            switch (this.WindowType)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                    var autoCheckLogic = new AutoRegistCheckLogic(allControl, allControl);
                    RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                    catchErr = this.logic.RequiredSettingInit();
                    if (catchErr)
                    {
                        this.logic.footerForm.bt_func9.Enabled = true;
                        return;
                    }

                    // Ditailの行数チェックはFWでできないので自前でチェック
                    if (!base.RegistErrorFlag
                        && !this.logic.CheckRequiredDataForDeital(out catchErr) && !catchErr)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E001", "明細行");
                        base.RegistErrorFlag = true;
                    }
                    else if (catchErr)
                    {
                        this.logic.footerForm.bt_func9.Enabled = true;
                        return;
                    }

                    if (!base.RegistErrorFlag &&
                        this.logic.dto.contenaResultList.Count > 0 && string.IsNullOrEmpty(GENBA_CD.Text))
                    {
                        // コンテナ入力がある場合は現場(&業者)必須
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E180");
                        base.RegistErrorFlag = true;
                        GENBA_CD.IsInputErrorOccured = true;
                    }

                    //必須チェックでエラーになった必須項目で一番最初(TabIndexの若い)の項目にフォーカスを当てる。
                    if (base.RegistErrorFlag)
                    {
                        catchErr = this.logic.MoveErrorFocus();
                        if (catchErr)
                        {
                            this.logic.footerForm.bt_func9.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        // 明細行項目の入力チェック
                        if (!this.CheckDetailColumn(out catchErr) && !catchErr)
                        {
                            base.RegistErrorFlag = true;
                        }
                        else if (catchErr)
                        {
                            this.logic.footerForm.bt_func9.Enabled = true;
                            return;
                        }
                    }

                    // 現金取引チェック
                    if (!base.RegistErrorFlag)
                    {
                        if (!this.logic.GenkinTorihikiCheck(out catchErr) && !catchErr)
                        {
                            base.RegistErrorFlag = true;
                        }
                        else if (catchErr)
                        {
                            this.logic.footerForm.bt_func9.Enabled = true;
                            return;
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

                    if (!base.RegistErrorFlag)
                    {
                        // 月次処理中 or 月次処理ロックチェック
                        if (this.GetsujiLockCheck(out catchErr) && !catchErr)
                        {
                            base.RegistErrorFlag = true;
                        }
                        else if (catchErr)
                        {
                            this.logic.footerForm.bt_func9.Enabled = true;
                            return;
                        }
                    }

                    break;

                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    if (!base.RegistErrorFlag)
                    {
                        // 月次処理中 or 月次処理ロックチェック
                        if (this.GetsujiLockCheck(out catchErr) && !catchErr)
                        {
                            base.RegistErrorFlag = true;
                        }
                        else if (catchErr)
                        {
                            this.logic.footerForm.bt_func9.Enabled = true;
                            return;
                        }
                    }

                    break;

                default:
                    break;
            }

            /**
             * 設置、引揚可能チェック用データ
             */
            var contenaShiteiUtil = new ContenaShiteiUtility();
            long sysId = -1;
            int seq = -1;
            if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.WindowType)
            {
                sysId = (long)this.logic.beforDto.entryEntity.SYSTEM_ID;
                seq = (int)this.logic.beforDto.entryEntity.SEQ;
            }

            List<UtilityDto> denpyouInfoList = new List<UtilityDto>();
            UtilityDto urshInfo = new UtilityDto();
            urshInfo.SysId = sysId;
            urshInfo.Seq = seq;
            urshInfo.DenshuKbn = (int)DENSHU_KBN.URIAGE_SHIHARAI;
            denpyouInfoList.Add(urshInfo);

            if (this.logic.tUketsukeSsEntry != null)
            {
                UtilityDto uketsukeInfo = new UtilityDto();
                uketsukeInfo.SysId = (long)this.logic.tUketsukeSsEntry.SYSTEM_ID;
                uketsukeInfo.Seq = (int)this.logic.tUketsukeSsEntry.SEQ;
                uketsukeInfo.DenshuKbn = (int)DENSHU_KBN.UKETSUKE;
                denpyouInfoList.Add(uketsukeInfo);
            }

            if (!base.RegistErrorFlag)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                DialogResult result = new DialogResult();

                bool ret = true;
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:

                        if (!base.RegistErrorFlag
                            && isKotaiKanri
                            && !contenaShiteiUtil.CheckContenaInfo(this.logic.dto.contenaResultList, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, denpyouInfoList))
                        {
                            // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                            this.logic.footerForm.bt_func9.Enabled = true;
                            // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                            return;
                        }

                        // 20141030 koukouei 委託契約チェック start
                        ret = this.logic.CheckItakukeiyaku();
                        if (!ret)
                        {
                            // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                            this.logic.footerForm.bt_func9.Enabled = true;
                            // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                            return;
                        }
                        // 20141030 koukouei 委託契約チェック end

                        if (this.ShowDenpyouHakouPopup(!isF9, out catchErr) && !catchErr)
                        {

                            //締チェックの位置を変更 start
                            /// 20141112 Houkakou 「売上/支払入力」の締済期間チェックの追加　start
                            if (!this.logic.SeikyuuDateCheck())
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.logic.footerForm.bt_func9.Enabled = true;
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                return;
                            }
                            else if (!this.logic.SeisanDateCheck())
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.logic.footerForm.bt_func9.Enabled = true;
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                return;
                            }
                            /// 20141112 Houkakou 「売上/支払入力」の締済期間チェックの追加　end
                            //締チェックの位置を変更 end 

                            if (!this.logic.CreateEntityAndUpdateTables(base.RegistErrorFlag) && !base.RegistErrorFlag)
                            {
                                break;
                            }
                            else if (base.RegistErrorFlag)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }

                            // 【追加】モード初期表示処理
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            catchErr = this.InitNumbers(false);
                            if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }

                            // 帳票出力
                            //仕切書
                            catchErr = this.logic.PrintShikirisyo();
                            if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }

                            //領収書
                            if (this.denpyouHakouPopUpDTO != null
                                && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                            {
                                catchErr = this.logic.Print();
                                if (catchErr)
                                {
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }
                            }

                            // 完了メッセージ表示
                            msgLogic.MessageBoxShow("I001", "登録");
                            this.isRegisted = true;

                            // 4935_8 売上支払入力 jyokou 20150505 str
                            this.logic.isRegistered = true;
                            // 4935_8 売上支払入力 jyokou 20150505 end

                            //base.OnLoad(e);

                            //this.logic = new LogicClass(this);    // No.3822関連 logic.pressedEnterOrTabが二重になるため処理変更
                            catchErr = this.logic.LocalDataInit();             // No.3822関連
                            if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }
                            this.logic.GetAllEntityData(out catchErr);
                            if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }
                            catchErr = this.logic.WindowInit();
                            if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }
                            catchErr = this.logic.ButtonInit();
                            if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }

                            // スクロールバーが下がる場合があるため、
                            // 強制的にバーを先頭にする
                            this.AutoScrollPosition = new Point(0, 0);

                            // 計量番号クリア
                            this.KEIRYOU_NUMBER.Text = string.Empty;
                        }
                        else if (catchErr)
                        {
                            this.logic.footerForm.bt_func9.Enabled = true;
                            return;
                        }

                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 税区分、税計算区分、取引区分をセット
                        bool isZeiKbnChanged = this.logic.zeiKbnChanged();
                        if (!isZeiKbnChanged)
                        {
                            // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                            this.logic.footerForm.bt_func9.Enabled = true;
                            // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                            return;
                        }
                        result = msgLogic.MessageBoxShow("C038");
                        if (result == DialogResult.Yes)
                        {

                            if (!base.RegistErrorFlag
                                && isKotaiKanri
                                && !contenaShiteiUtil.CheckContenaInfo(this.logic.dto.contenaResultList, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, denpyouInfoList))
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.logic.footerForm.bt_func9.Enabled = true;
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                return;
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

                            // 20141030 koukouei 委託契約チェック start
                            ret = this.logic.CheckItakukeiyaku();
                            if (!ret)
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.logic.footerForm.bt_func9.Enabled = true;
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                return;
                            }
                            // 20141030 koukouei 委託契約チェック end

                            if (this.ShowDenpyouHakouPopup(!isF9, out catchErr) && !catchErr)
                            {

                                //締チェックの位置を変更 start
                                if (this.logic.CheckAllShimeStatus(out catchErr) && !catchErr)
                                {
                                    // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                    msgLogic.MessageBoxShow("I011", "修正");
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }
                                else if (catchErr)
                                {
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }

                                /// 20141112 Houkakou 「売上/支払入力」の締済期間チェックの追加　start
                                if (!this.logic.SeikyuuDateCheck())
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    return;
                                }
                                else if (!this.logic.SeisanDateCheck())
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    return;
                                }
                                /// 20141112 Houkakou 「売上/支払入力」の締済期間チェックの追加　end
                                //締チェックの位置を変更 end

                                if (!this.logic.CreateEntityAndUpdateTables(base.RegistErrorFlag))
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    return;
                                }

                                //仕切書
                                catchErr = this.logic.PrintShikirisyo();
                                if (catchErr)
                                {
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }

                                // 帳票出力
                                if (this.denpyouHakouPopUpDTO != null
                                    && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                                {
                                    catchErr = this.logic.Print();
                                    if (catchErr)
                                    {
                                        this.logic.footerForm.bt_func9.Enabled = true;
                                        return;
                                    }
                                }

                                // 完了メッセージ表示
                                msgLogic.MessageBoxShow("I001", "更新");
                                this.isRegisted = true;

                                if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    // 追加権限がある場合
                                    // 【追加】モード初期表示処理
                                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                    catchErr = this.InitNumbers(true);
                                    if (catchErr)
                                    {
                                        this.logic.footerForm.bt_func9.Enabled = true;
                                        return;
                                    }

                                    //base.OnLoad(e);

                                    catchErr = this.logic.LocalDataInit();
                                    if (catchErr)
                                    {
                                        this.logic.footerForm.bt_func9.Enabled = true;
                                        return;
                                    }
                                    catchErr = this.logic.WindowInit();
                                    if (catchErr)
                                    {
                                        this.logic.footerForm.bt_func9.Enabled = true;
                                        return;
                                    }
                                    catchErr = this.logic.ButtonInit();
                                    if (catchErr)
                                    {
                                        this.logic.footerForm.bt_func9.Enabled = true;
                                        return;
                                    }

                                    // スクロールバーが下がる場合があるため、
                                    // 強制的にバーを先頭にする
                                    this.AutoScrollPosition = new Point(0, 0);
                                }
                                else
                                {
                                    // 追加権限がない場合は画面を閉じる
                                    base.CloseTopForm();
                                }

                            }
                            else if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }
                        }

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        result = msgLogic.MessageBoxShow("C026");
                        if (result == DialogResult.Yes)
                        {

                            //締チェック
                            if (this.logic.CheckAllShimeStatus(out catchErr) && !catchErr)
                            {
                                // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                msgLogic.MessageBoxShow("I011", "削除");
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }
                            else if (catchErr)
                            {
                                this.logic.footerForm.bt_func9.Enabled = true;
                                return;
                            }
                            //締チェック

                            if (!this.logic.CreateEntityAndUpdateTables(base.RegistErrorFlag))
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.logic.footerForm.bt_func9.Enabled = true;
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                return;
                            }

                            // 完了メッセージ表示
                            msgLogic.MessageBoxShow("I001", "削除");

                            if (r_framework.Authority.Manager.CheckAuthority("G054", WINDOW_TYPE.NEW_WINDOW_FLAG))
                            {
                                // 【追加】モード初期表示処理
                                this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                catchErr = this.InitNumbers(true);
                                if (catchErr)
                                {
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }

                                //base.OnLoad(e);

                                catchErr = this.logic.WindowInit();
                                if (catchErr)
                                {
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }
                                catchErr = this.logic.ButtonInit();
                                if (catchErr)
                                {
                                    this.logic.footerForm.bt_func9.Enabled = true;
                                    return;
                                }

                                // スクロールバーが下がる場合があるため、
                                // 強制的にバーを先頭にする
                                this.AutoScrollPosition = new Point(0, 0);
                            }
                            else
                            {
                                // 追加権限がない場合は画面を閉じる
                                base.CloseTopForm();
                            }
                        }

                        break;

                    default:
                        break;
                }

                //コントロールの背景色を初期化
                catchErr = this.logic.ChangeAutoChangeBackColorEnabled();
                if (catchErr)
                {
                    this.logic.footerForm.bt_func9.Enabled = true;
                    return;
                }

                //((UIHeaderForm)this.logic.footerForm.headerForm).windowTypeLabel.Text = "新規";
                //((UIHeaderForm)this.logic.footerForm.headerForm).windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                //((UIHeaderForm)this.logic.footerForm.headerForm).windowTypeLabel.ForeColor = System.Drawing.Color.Black;
            }

            // 20141114 ブン 「2重登録チェック」に完了を追加する　start
            this.logic.footerForm.bt_func9.Enabled = true;
            // 20141114 ブン 「2重登録チェック」に完了を追加する　end

            // フォーカス制御はRegistメソッドで制御する

            LogUtility.DebugMethodEnd(sender, e, isF9);
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
            bool catchErr = this.logic.AddNewRow();
            if (catchErr)
            {
                return;
            }

            // 合計系計算
            this.logic.CalcTotalValues();
            editingMultiRowFlag = false;
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
            editingMultiRowFlag = true;
            bool catchErr = this.logic.RemoveSelectedRow();
            if (catchErr)
            {
                return;
            }
            // 合計系計算
            this.logic.CalcTotalValues();
            editingMultiRowFlag = false;
            LogUtility.DebugMethodEnd(sender, e);
        }

        // 明細のイベント

        /// <summary>
        /// 明細の行移動処理
        /// 明細の行が増減するたびに必ず実行してください
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrwDetail_RowLeave(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (editingMultiRowFlag)
            {
                return;
            }
            editingMultiRowFlag = true;
            // ROW_NOを採番
            this.notEditingOperationFlg = true;
            bool catchErr = this.logic.NumberingRowNo();
            if (catchErr)
            {
                return;
            }
            this.notEditingOperationFlg = false;
            editingMultiRowFlag = false;
            LogUtility.DebugMethodEnd(sender, e);
        }

        private void mrwDetail_RowEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // マニフェスト連携用のデータを設定
            Row row = this.mrwDetail.CurrentRow;
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

            LogUtility.DebugMethodEnd(sender, e);
        }

        private void mrwDetail_CellValidated(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 品名CDは前回値との比較を行わない
            if (this.mrwDetail.CurrentRow.Cells[e.CellName].Name != LogicClass.CELL_NAME_HINMEI_CD)
            {
                // 前回値と変更が無かったら処理中断
                if (beforeValuesForDetail.ContainsKey(e.CellName)
                    && beforeValuesForDetail[e.CellName].Equals(
                        Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value)))
                {
                    return;
                }
            }

            if (editingMultiRowFlag == false)
            {
                bool catchErr = false;
                bool bExecuteCalc = false;
                editingMultiRowFlag = true;
                switch (e.CellName)
                {
                    case LogicClass.CELL_NAME_UNIT_CD:
                        catchErr = this.logic.SearchAndCalcForUnit(false, this.mrwDetail.CurrentRow);
                        if (catchErr)
                        {
                            editingMultiRowFlag = false; // MAILAN #158993 START
                            return;
                        }
                        this.logic.ResetTankaCheck(); // MAILAN #158993 START
                        bExecuteCalc = true;
                        break;
                    case LogicClass.CELL_NAME_SUURYOU:
                    case LogicClass.CELL_NAME_TANKA:
                        catchErr = this.logic.CalcDetaiKingaku(this.mrwDetail.CurrentRow);
                        if (catchErr)
                        {
                            return;
                        }
                        bExecuteCalc = true;
                        break;

                    case LogicClass.CELL_NAME_URIAGESHIHARAI_DATE:
                        // 消費税の取得
                        catchErr = this.logic.SetShouhizeiRateForDetail(this.mrwDetail.CurrentRow);
                        if (catchErr)
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_HINMEI_CD:
                        // 品名をセット
                        Row row = this.mrwDetail.CurrentRow;
                        if (row == null)
                        {
                            return;
                        }
                        catchErr = this.logic.setHinmeiName(row);
                        if (catchErr)
                        {
                            return;
                        }
                        break;

                    default:
                        break;
                }

                //再計算対象の項目だった場合、金額の再計算を行う。
                if (bExecuteCalc)
                {
                    // 毎回合計系計算を実行する
                    catchErr = this.logic.CalcDetaiKingaku(this.mrwDetail.CurrentRow);
                    if (catchErr)
                    {
                        return;
                    }
                }
                this.logic.CalcTotalValues();

                editingMultiRowFlag = false;
            }
            LogUtility.DebugMethodEnd(sender, e);

        }

        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void mrwDetail_OnCellEnter(object sender, CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            Row row = this.mrwDetail.CurrentRow;

            if (row == null)
            {
                return;
            }

            // No.4256-->
            // 品名でPopup表示後処理追加
            if (this.mrwDetail.Columns[e.CellIndex].Name.Equals(LogicClass.CELL_NAME_HINMEI_CD))
            {
                // PopupResult取得できるようにPopupAfterExecuteにデータ設定
                GcCustomTextBoxCell cell = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                cell.PopupAfterExecute = PopupAfter_HINMEI_CD;
                this.bSelectHinmeiPopup = false;
            }
            // No.4256<--

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
                    this.logic.setHinmeiName(row);
                    break;
                case LogicClass.CELL_NAME_UNIT_CD:
                    // 数量変更時の金額再計算
                    //this.logic.CalcDetaiKingaku(row);
                    //this.logic.CalcTotalValues();       // 合計計算
                    break;
                case LogicClass.CELL_NAME_KINGAKU:      // 金額
                    // 単価変更時の金額再計算
                    //this.logic.CalcDetaiKingaku(row);
                    //this.logic.CalcTotalValues();       // 合計計算
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 各CELLの値検証
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrwDetail_CellValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool catchErr = false;
                switch (e.CellName)
                {
                    case LogicClass.CELL_NAME_HINMEI_CD:
                        // No.4256-->コメントアウト
                        // 前回値と変更が無かったら処理中断
                        //if (beforeValuesForDetail.ContainsKey(e.CellName)
                        //    && beforeValuesForDetail[e.CellName].Equals(
                        //        Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value)))
                        //{
                        //    return;
                        //}
                        // No.4256<--

                        // 空だったら処理中断
                        this.mrwDetail.BeginEdit(false);
                        this.mrwDetail.EndEdit();
                        this.mrwDetail.NotifyCurrentCellDirty(false);

                        //受入入力と同様の処理を行うために定義
                        object denpyouKbn = this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value == null ? string.Empty : this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value;

                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        if (beforeValuesForDetail.ContainsKey(e.CellName)
                        && beforeValuesForDetail[e.CellName].Equals(
                            Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value)) && !this.isInputError
                        && !this.bSelectHinmeiPopup
                        && !string.IsNullOrEmpty(denpyouKbn.ToString()))
                        {
                            // 品名CDの入力値が前回入力時と同じならば処理中断（ただし、品名ポップアップからの選択は例外）
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value)))
                            {
                                this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = "";
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text)
                                && string.IsNullOrEmpty(this.GYOUSHA_CD.Text)
                                && string.IsNullOrEmpty(this.GENBA_CD.Text))
                                {
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E284");
                                    e.Cancel = true;
                                    return;
                                }
                                if (this.logic.GetHinmei(this.mrwDetail.CurrentRow, out catchErr) && !catchErr)
                                {
                                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                    msgLogic.MessageBoxShow("E020", "品名");
                                    e.Cancel = true;
                                    return;
                                }
                                else if (catchErr)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                            }
                        }
                        // 20151021 katen #13337 品名手入力に関する機能修正 end
                        if (string.IsNullOrEmpty((string)this.mrwDetail.CurrentRow.Cells[e.CellName].Value))
                        {
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_TANKA].Value = string.Empty;
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_KINGAKU].Value = string.Empty;
                            return;
                        }

                        // No.4256-->
                        var targetRow = this.mrwDetail.CurrentRow;
                        if (targetRow != null)
                        {
                            GcCustomTextBoxCell control = (GcCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                            if (control.TextBoxChanged == true
                                || (beforeValuesForDetail.ContainsKey(e.CellName) && !beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value)))
                                )
                            {
                                this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value = string.Empty; // 伝票区分をクリア
                                control.TextBoxChanged = false;
                            }
                        }
                        // No.4256<--

                        // No.3086-->
                        // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                        if (!(this.logic.CheckHinmeiCd(this.mrwDetail.CurrentRow)))
                        {
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_CD].Value = string.Empty;
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_TANKA].Value = string.Empty;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            return;
                        }
                        // No.3086<--

                        bool bResult = true;

                        if (string.IsNullOrEmpty(Convert.ToString(this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value)))
                        {
                            bResult = this.logic.SetDenpyouKbn(out catchErr);
                        }

                        if (catchErr)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (!bResult)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                            if ((this.logic.CheckHinmeiCd(this.mrwDetail.CurrentRow)))    // 品名コードの存在チェック（伝種区分が売上/支払、または共通）
                            {
                                catchErr = this.logic.SearchAndCalcForUnit(true, this.mrwDetail.CurrentRow);
                                if (catchErr)
                                {
                                    return;
                                }
                                this.logic.ResetTankaCheck(); // MAILAN #158993 START
                            }
                            else if (this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value == null)
                            {
                                // 品名CDに入力がなければ、単位コードとその略称もクリアする
                                this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_CD].Value = string.Empty;
                                this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                                this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_TANKA].Value = string.Empty;
                            }
                            // 合計系計算
                            catchErr = this.logic.CalcTotalValues();
                            if (catchErr)
                            {
                                return;
                            }
                            // 前回値チェック用データをセット
                            if (beforeValuesForDetail.ContainsKey(e.CellName))
                            {
                                beforeValuesForDetail[e.CellName] = Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value);
                            }
                            else
                            {
                                beforeValuesForDetail.Add(e.CellName, Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value));
                            }
                        }
                        break;
                    case LogicClass.CELL_NAME_HINMEI_NAME:
                        if (!this.logic.ValidateHinmeiName(out catchErr) && !catchErr)
                        {
                            e.Cancel = true;
                        }
                        else if (catchErr)
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    default:
                        break;
                }

                // 単価と金額の活性/非活性制御
                if (e.CellName.Equals(LogicClass.CELL_NAME_TANKA) &&
                    beforeValuesForDetail.ContainsKey(e.CellName) &&
                    !beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.mrwDetail.CurrentRow.Cells[e.CellName].Value)))
                {
                    // 単価の場合のみCellValidatedでReadOnly設定が変わる場合があるのでここで一旦計算を行う
                    catchErr = this.logic.CalcDetaiKingaku(this.mrwDetail.CurrentRow);
                    if (catchErr)
                    {
                        return;
                    }
                    this.logic.CalcTotalValues();
                }
                SetIchranReadOnly(e.RowIndex);

            }
            finally
            {
                this.mrwDetail[e.RowIndex, e.CellIndex].UpdateBackColor(false);
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌選択ポップアップ起動後処理
        /// </summary>
        public virtual void SHARYO_CD_RemovePopUpNameAfterExecutePopUp()
        {
            SHARYOU_CD.PopupWindowName = string.Empty;
        }

        /// <summary>
        /// 受付伝票(F4)
        /// </summary>
        internal void UketsukeDenpyo(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenForm("G292", WINDOW_TYPE.NONE, DENSHU_KBN.URIAGE_SHIHARAI);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コンテナ(F6)
        /// </summary>
        internal void ContenaWindow(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.OpenContena();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NUMBER_Validated(object sender, EventArgs e)
        {
            bool catchErr = false;
            // 新規のみ
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 受付番号が変更されていない時は何もしない
                if (this.UketsukeNumberTextChangeFlg)
                {
                    // 受付番号入力時かつ
                    // 値に変更がある場合 もしくは 同じ受付番号でも再入力された場合は実行
                    if (!string.IsNullOrEmpty(this.UKETSUKE_NUMBER.Text))
                    {
                        // 初期化
                        this.UketsukeNumberTextChangeFlg = false;

                        if (!this.UKETSUKE_NUMBER.Text.Equals(this.UketukeNumber.ToString()))
                        {
                            // No.2599-->
                            this.mrwDetail.BeginEdit(false);
                            this.mrwDetail.Rows.Clear();
                            this.mrwDetail.EndEdit();
                            // No.2599<--

                            // コンテナ情報初期化
                            this.logic.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                            this.logic.dto.contenaResultList = new List<T_CONTENA_RESULT>();

                            if (!this.logic.RenkeiCheck(this.UKETSUKE_NUMBER.Text))
                            {
                                this.UKETSUKE_NUMBER.Text = string.Empty;
                                return;
                            }

                            this.notEditingOperationFlg = true;
                            catchErr = this.logic.GetUketsukeNumber();
                            this.notEditingOperationFlg = false;
                            if (catchErr)
                            {
                                return;
                            }
                            // 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
                            this.logic.UketukeBangoCheck(out catchErr);
                            // 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end
                            if (catchErr)
                            {
                                return;
                            }

                            long uketsukeNum = -1;
                            if (long.TryParse(this.UKETSUKE_NUMBER.Text, out uketsukeNum))
                            {
                                this.UketukeNumber = uketsukeNum;
                            }
                            else
                            {
                                this.UketukeNumber = -1;
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
                        this.logic.ClearTUketsukeSkEntry();
                        this.logic.ClearTUketsukeMkEntry();

                        // コンテナ情報初期化
                        this.logic.dto.contenaReserveList = new List<T_CONTENA_RESERVE>();
                    }
                    
                }
            }
        }
        #region プロセスボタン押下処理
        /// <summary>
        /// [1]運賃入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, System.EventArgs e)
        {
            this.logic.OpenUnchinNyuuryoku(sender,e);
        }
        #endregion プロセスボタン押下処理
        #endregion

        #region ユーティリティ
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        private Control[] GetAllControl()
        {
            List<Control> allControl = new List<Control>();
            allControl.AddRange(this.allControl);
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm.headerForm));
            allControl.AddRange(controlUtil.GetAllControls(ParentBaseForm));

            return allControl.ToArray();
        }

        /// <summary>
        /// コンストラクタで渡された売上支払番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistUrShData()
        {
            return this.logic.IsExistUrShData(this.UrShNumber);
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
                // TODO: 伝票発行ポップアップ起動
                string denpyouMode = string.Empty;
                // TODO: 伝票モードのConstを定義してもらうよう依頼
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        denpyouMode = ConstClass.TENPYO_MODEL_1;
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        denpyouMode = ConstClass.TENPYO_MODEL_2;
                        break;

                    default:
                        break;
                }

                this.denpyouHakouPopUpDTO = this.logic.createParameterDTOClass();
                bool kakuteiKbn = this.logic.IsKakuteiDenpyou();

                var callForm
                    = new Shougun.Core.SalesPayment.DenpyouHakou.UIForm(
                        this.denpyouHakouPopUpDTO,
                        denpyouMode,
                        SalesPaymentConstans.DENSHU_KBN_CD_URIAGESHIHARAI_STR,
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
                this.errmessage.MessageBoxShow("E245", "");
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
                bool catchErr = false;
                this.SetTorihikisaki(out catchErr);
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
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.GYOUSHA_CD.Focus();
            if (this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd)
            {
                bool catchErr = false;
                this.SetGyousha(out catchErr);
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 現場CDへフォーカス移動する
        /// 現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.GENBA_CD.Focus();

            if (this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd || this.GENBA_CD.Text != this.logic.tmpGenbaCd)
            {
                bool catchErr = false;
                this.SetGenba(out catchErr);
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷積業者CDへフォーカス移動する
        /// 荷積業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNizumiGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.NIZUMI_GYOUSHA_CD.Focus();
            bool catchErr = false;
            this.SetNizumiGyousha(out catchErr);

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷積現場CDへフォーカス移動する
        /// 荷積現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNizumiGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.NIZUMI_GENBA_CD.Focus();
            bool catchErr = false;
            this.SetNizumiGenba(out catchErr);

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

            this.NIOROSHI_GYOUSHA_CD.Focus();
            bool catchErr = false;
            this.SetNioroshiGyousha(out catchErr);

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷降現場CDへフォーカス移動する
        /// 荷降現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNioroshiGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.NIOROSHI_GENBA_CD.Focus();
            bool catchErr = false;
            this.SetNioroshiGenba(out catchErr);

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 運搬業者CDへフォーカス移動する
        /// 運搬業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToUnpanGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            bool catchErr = false;
            this.SetUnpanGyousha(out catchErr);
            this.UNPAN_GYOUSHA_CD.Focus();

            // 初期化
            this.isFromSearchButton = false;
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
        /// 受入/支払番号、受付番号、計量番号の初期化
        /// </summary>
        /// <param name="clearTextNumber"></param>
        private bool InitNumbers(bool clearTextNumber)
        {
            try
            {
                this.UrShNumber = -1;
                this.UketukeNumber = -1;
                this.KeiryouNumber = -1;

                if (clearTextNumber)
                {
                    // Logicでのクリア処理がないため、暫定対応。
                    // 登録後、計量番号をクリア
                    this.KEIRYOU_NUMBER.Text = string.Empty;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitNumbers", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        #region 取引先CD・業者CD・現場CDの関連情報セット処理

        /// <summary>
        /// 取引先CDに関連する情報をセット
        /// </summary>
        public bool SetTorihikisaki(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                ret = this.logic.CheckTorihikisaki();
                this.logic.TorihikisakiCdSet();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetTorihikisaki", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetTorihikisaki", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 取引先 PopupAfter
        /// </summary>
        public void PopupAfterTorihikisaki()
        {
            bool catchErr = false;
            this.SetTorihikisaki(out catchErr);
        }

        /// <summary>
        /// 取引先 PopupBefore
        /// </summary>
        public void PopupBeforeTorihikisaki()
        {
            this.logic.TorihikisakiCdSet();
        }

        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        public bool SetGyousha(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                ret = this.logic.CheckGyousha();

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                this.logic.hasShow = false;
                // 20151021 katen #13337 品名手入力に関する機能修正 end
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            if (this.logic.tmpGyousyaCd == this.GYOUSHA_CD.Text)
            {
                return;
            }

            bool catchErr = false;
            this.SetGyousha(out catchErr);
        }

        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            this.logic.tmpGyousyaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        public bool SetGenba(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                if (this.isInputError || (String.IsNullOrEmpty(this.GYOUSHA_CD.Text) || !this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text)
                        || (this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_NAME_RYAKU.Text)))
                        || (String.IsNullOrEmpty(this.GENBA_CD.Text) || !this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text)
                        || (this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text) && string.IsNullOrEmpty(this.GENBA_NAME_RYAKU.Text))))
                {
                    ret = this.logic.CheckGenba();
                }

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                this.logic.hasShow = false;
                // 20151021 katen #13337 品名手入力に関する機能修正 end
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetGenba", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        public void GENBA_PopupAfterExecuteMethod()
        {
            if (this.logic.tmpGyousyaCd == this.GYOUSHA_CD.Text && this.logic.tmpGenbaCd == this.GENBA_CD.Text)
            {
                return;
            }

            bool catchErr = false;
            this.SetGenba(out catchErr);
        }

        public void GENBA_PopupBeforeExecuteMethod()
        {
            this.logic.tmpGyousyaCd = this.GYOUSHA_CD.Text;
            this.logic.tmpGenbaCd = this.GENBA_CD.Text;
        }

        /// <summary>
        /// 荷積業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGyousha(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNizumiGyoushaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNizumiGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNizumiGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 荷積現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNizumiGenba(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNizumiGenbaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNizumiGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNizumiGenba", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 荷降業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNioroshiGyousha(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNioroshiGyoushaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNioroshiGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNioroshiGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 荷降現場に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetNioroshiGenba(out bool catchErr)
        {
            try
            {
                // 初期化
                bool ret = false;
                catchErr = false;

                ret = this.logic.CheckNioroshiGenbaCd();

                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("SetNioroshiGenba", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetNioroshiGenba", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                return true;
            }
        }

        /// <summary>
        /// 運搬業者 PopupAfter
        /// </summary>
        public void PopupAfterUnpanGyousha()
        {
            bool catchErr = false;
            this.SetUnpanGyousha(out catchErr);
        }

        /// <summary>
        /// 運搬業者 PopupBrefore
        /// </summary>
        public void PopupBeforeUnpanGyousha()
        {
            this.logic.UnpanGyoushaCdSet();
        }

        /// <summary>
        /// 運搬業者に関連する情報をセット
        /// </summary>
        /// <returns></returns>
        public bool SetUnpanGyousha(out bool catchErr)
        {
            // 初期化
            bool ret = false;
            catchErr = false;

            try
            {
                ret = this.logic.CheckUnpanGyoushaCd();
                this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetUnpanGyousha", ex2);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = true;
                ret = true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("SetUnpanGyousha", ex);
                    this.errmessage.MessageBoxShow("E245", "");
                }
                catchErr = true;
                ret = true;
            }
            return ret;
        }

        #endregion 取引先CD・業者CD・現場CDの関連情報セット処理

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

        // 20140512 kayo No.679 計量番号連携 start
        /// <summary>
        /// 計量番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIRYOU_NUMBER_Validated(object sender, EventArgs e)
        {
            // 新規のみ
            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 計量番号入力時、かつ値に変更がある場合に実行
                if (!string.IsNullOrEmpty(this.KEIRYOU_NUMBER.Text) && !this.KEIRYOU_NUMBER.Text.Equals(this.KeiryouNumber.ToString()))
                {
                    this.mrwDetail.BeginEdit(false);
                    this.mrwDetail.Rows.Clear();
                    this.mrwDetail.EndEdit();

                    this.KeiryouNumber = long.Parse(this.KEIRYOU_NUMBER.Text);
                    bool catchErr = this.logic.GetKeiryouNumber();
                    if (catchErr)
                    {
                        return;
                    }
                    // 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
                    this.logic.KeiryouBangoCheck();
                    // 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end
                }
            }
        }
        // 20140512 kayo No.679 計量番号連携 end

        /// <summary>
        /// 画面表示時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            this.logic.SetMoveData();
        }

        // No.4256-->
        /// <summary>
        /// 品名設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_HINMEI_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var targetRow = this.mrwDetail.CurrentRow;
                this.bSelectHinmeiPopup = true;
            }
        }
        // No.4256<--

        private bool execEntryNumberEvent = false;

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

                    if ("ENTRY_NUMBER".Equals(this.beforbeforControlName) && this.execEntryNumberEvent)
                    {
                        this.logic.SetTopControlFocus();
                        this.execEntryNumberEvent = false;
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
                                var control = controlUtil.FindControl(this.logic.footerForm.headerForm, this.beforbeforControlName);
                                if (control == null)
                                {
                                    // ヘッダーフォームをアクティブコントロールにしようとすると失敗するのでヘッダーフォームのコントロールはアクティブにしない。
                                    this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();
                                }
                            }

                            this.logic.GotoNextControl(forward);
                        }
                    }
                    else
                    {
                        //20151026 hoanghm #13404 start
                        //this.SelectNextControl(ActiveControl, !forward, true, true, true);  // Activeが変更になっているため前の位置に戻す
                        var control = controlUtil.FindControl(this.logic.footerForm.headerForm, this.beforbeforControlName);
                        if (control == null)
                        {
                            // ヘッダーフォームをアクティブコントロールにしようとすると失敗するのでヘッダーフォームのコントロールはアクティブにしない。

                            // 明細に一度フォーカスしてしまう場合がある。その場合、明細のvalidatingやvalidatedイベントが実行され正常にフォーカス移動できないので
                            // ActiveControlを一度nullにして時間を稼ぎ、validatedイベント等が完了してからフォーカス移動させるようにする。
                            this.ActiveControl = null;
                            this.ActiveControl = this.allControl.Where(c => c.Name == this.beforbeforControlName).FirstOrDefault();
                        }
                        //20151026 hoanghm #13404 end
                        this.logic.GotoNextControl(forward);
                    }
                }
            }
            //20211230 Thanh 158916 s
            if (e.KeyChar == (char)Keys.Space)
            {
                if (this.mrwDetail.CurrentCell.Name == "TANKA")
                {
                    if (this.mrwDetail.CurrentCell.IsInEditMode)
                    {
                        if (e.KeyChar == (Char)Keys.Space)
                        {
                            this.OpenTankaRireki(this.mrwDetail.CurrentRow.Index);
                        }
                    }
                }
            }
            //20211230 Thanh 158916 e
        }
        // No.3822<--

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
            // 比較用運転者CDをセット
            this.logic.UntenshaCdSet();
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
        private bool CheckDetailColumn(out bool catchErr)
        {
            try
            {
                var msgLogic = new MessageBoxShowLogic();
                catchErr = false;
                foreach (Row row in this.mrwDetail.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // 金額
                        if ((row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue != null
                            && string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue.ToString())) && !row.IsNewRow)
                        {
                            msgLogic.MessageBoxShow("E148", "金額");
                            var cellKingaku = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_KINGAKU];
                            cellKingaku.IsInputErrorOccured = true;
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("CheckDetailColumn", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 登録時に数量*単価が金額に一致するかの金額チェックを実行します
        /// </summary>
        /// <returns></returns>
        private bool CheckDetailKingaku()
        {
            /* ここで行っている金額チェックの計算方法は明細の金額計算と同様です。 */
            /* どちらかの変更を行った際にはもう一方も修正してください。           */

            foreach (Row row in this.mrwDetail.Rows)
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
                        short.TryParse(Convert.ToString(this.logic.dto.torihikisakiSeikyuuEntity.KINGAKU_HASUU_CD), out kingakuHasuuCd);
                    }
                    else if (row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
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
        private void UKETSUKE_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // この画面（G054）が呼び出された時の引数に受付番号が含まれていた場合、
            // 初回起動時にフラグが立たないようにするための対策
            if (!this.isArgumentUketsukeNumber)
            {
                // 受付番号のテキストが変更されたらフラグをたてる
                this.UketsukeNumberTextChangeFlg = true;
            }
            else
            {
                this.isArgumentUketsukeNumber = false;
            }
        }

        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　start
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
        /// 20141014 teikyou 「売上・支払入力画面」の休動Checkを追加する　end

        #region 月次ロックチェック

        /// <summary>
        /// [登録処理用] 月次ロックされているのかの判定を行います
        /// </summary>
        /// <returns>月次ロック中：True</returns>
        internal bool GetsujiLockCheck(out bool catchErr)
        {
            try
            {
                catchErr = false;
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
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetsujiLockCheck", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion

        public bool SetControlFocus()
        {
            try
            {
                var hasURData = false;
                var hasSHData = false;

                if (string.IsNullOrEmpty(((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Text))
                {
                    ((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(this.NYUURYOKU_TANTOUSHA_CD.Text))
                {
                    this.NYUURYOKU_TANTOUSHA_CD.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(this.KAKUTEI_KBN.Text))
                {
                    this.KAKUTEI_KBN.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(this.DENPYOU_DATE.Text))
                {
                    this.DENPYOU_DATE.Focus();
                    return false;
                }

                foreach (var row in this.mrwDetail.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    if (row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value != null
                        && row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("1"))
                    {
                        hasURData = true;
                    }

                    if (row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value != null
                        && row.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value.ToString().Equals("2"))
                    {
                        hasSHData = true;
                    }
                }

                if (hasSHData == false && hasURData == false)
                {
                    if (string.IsNullOrEmpty(this.URIAGE_DATE.Text) && !this.KAKUTEI_KBN.Text.Equals("2") && this.URIAGE_DATE.IsInputErrorOccured)
                    {
                        this.URIAGE_DATE.Focus();
                        return false;
                    }

                    if (string.IsNullOrEmpty(this.SHIHARAI_DATE.Text) && !this.KAKUTEI_KBN.Text.Equals("2") && this.SHIHARAI_DATE.IsInputErrorOccured)
                    {
                        this.SHIHARAI_DATE.Focus();
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.URIAGE_DATE.Text) && !this.KAKUTEI_KBN.Text.Equals("2") && hasURData)
                    {
                        this.URIAGE_DATE.Focus();
                        return false;
                    }

                    if (string.IsNullOrEmpty(this.SHIHARAI_DATE.Text) && !this.KAKUTEI_KBN.Text.Equals("2") && hasSHData)
                    {
                        this.SHIHARAI_DATE.Focus();
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    this.GYOUSHA_CD.Focus();
                    return false;
                }

                if (string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text))
                {
                    this.TORIHIKISAKI_CD.Focus();
                    return false;
                }

                if (this.mrwDetail.Rows.Count == 1)
                {
                    var hinmeicell = this.mrwDetail.Rows[0].Cells[LogicClass.CELL_NAME_HINMEI_CD];
                    hinmeicell.GcMultiRow.Focus();
                    hinmeicell.Selected = true;
                }
                else
                {
                    foreach (var row in this.mrwDetail.Rows)
                    {
                        if (row == null) continue;
                        if (row.IsNewRow) continue;

                        var hinmeicell = row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                        var suuryoucell = row.Cells[LogicClass.CELL_NAME_SUURYOU];
                        var unitcell = row.Cells[LogicClass.CELL_NAME_UNIT_CD];
                        var kingakucell = row.Cells[LogicClass.CELL_NAME_KINGAKU];

                        if (hinmeicell.Value == null || string.IsNullOrEmpty(hinmeicell.Value.ToString()))
                        {
                            hinmeicell.Style.BackColor = Constans.ERROR_COLOR;
                            hinmeicell.Selected = true;
                            return false;
                        }

                        if (suuryoucell.Value == null || string.IsNullOrEmpty(suuryoucell.Value.ToString()))
                        {
                            suuryoucell.Style.BackColor = Constans.ERROR_COLOR;
                            suuryoucell.Selected = true;
                            return false;
                        }

                        if (unitcell.Value == null || string.IsNullOrEmpty(unitcell.Value.ToString()))
                        {
                            unitcell.Style.BackColor = Constans.ERROR_COLOR;
                            unitcell.Selected = true;
                            return false;
                        }

                        if (kingakucell.Value == null || string.IsNullOrEmpty(kingakucell.Value.ToString()))
                        {
                            kingakucell.Style.BackColor = Constans.ERROR_COLOR;
                            kingakucell.Selected = true;
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetControlFocus", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        internal string hinmeiCd = "";
        internal string hinmeiName = "";
        public void HINMEI_CD_PopupBeforeExecuteMethod()
        {
            this.hinmeiCd = Convert.ToString(this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            this.hinmeiName = Convert.ToString(this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value);
        }
        public void HINMEI_CD_PopupAfterExecuteMethod()
        {
            if (this.hinmeiCd == Convert.ToString(this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value) && this.hinmeiName == Convert.ToString(this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value))
            {
                return;
            }
            bool catchErr = false;
            this.logic.GetHinmeiForPop(this.mrwDetail.CurrentRow, out catchErr);
            if (catchErr)
            {
                return;
            }
            if (beforeValuesForDetail.ContainsKey(LogicClass.CELL_NAME_HINMEI_CD))
            {
                beforeValuesForDetail[LogicClass.CELL_NAME_HINMEI_CD] = Convert.ToString(this.mrwDetail.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            }
        }
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        #region 単価と金額の活性/非活性制御
        /// <summary>
        /// 単価と金額の活性/非活性制御
        /// </summary>
        /// <param name="rowIndex"></param>
        internal bool SetIchranReadOnly(int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(rowIndex);

                if (rowIndex < 0) return false;
                var row = this.mrwDetail.Rows[rowIndex];

                if ((row.Cells[LogicClass.CELL_NAME_TANKA].Value == null || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_TANKA].Value.ToString())) &&
                    (row.Cells[LogicClass.CELL_NAME_KINGAKU].Value == null || string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].Value.ToString())))
                {
                    // 「単価」、「金額」どちらも空の場合、両方操作可
                    this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].ReadOnly = false;
                    this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].ReadOnly = false;
                }
                else if (row.Cells[LogicClass.CELL_NAME_TANKA].Value != null && !string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_TANKA].Value.ToString()))
                {
                    // 「単価」のみ入力済みの場合、「金額」操作不可
                    this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].ReadOnly = false;
                    this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].ReadOnly = true;
                }
                else if (row.Cells[LogicClass.CELL_NAME_KINGAKU].Value != null && !string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].Value.ToString()))
                {
                    // 「金額」のみ入力済みの場合、「単価」操作不可
                    this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].ReadOnly = true;
                    this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].ReadOnly = false;
                }

                // 設定した背景色を反映
                this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_TANKA].UpdateBackColor(false);
                this.mrwDetail.Rows[rowIndex].Cells[LogicClass.CELL_NAME_KINGAKU].UpdateBackColor(false);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchranReadOnly", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 荷積業者POPUP_AFT
        /// </summary>
        public void PopupAftZizumiGyousha()
        {
            bool catchErr = false;
            this.SetNizumiGyousha(out catchErr);
        }

        /// <summary>
        /// 荷積業者POPUP_BEF
        /// </summary>
        public void PopupBefZizumiGyousha()
        {
            this.logic.tmpNizumiGyoushaCd = this.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場POPUP_AFT
        /// </summary>
        public void PopupAftZizumiGenba()
        {
            bool catchErr = false;
            this.SetNizumiGenba(out catchErr);
        }

        /// <summary>
        /// 荷積現場POPUP_BEF
        /// </summary>
        public void PopupBefZizumiGenba()
        {
            this.logic.tmpNizumiGenbaCd = this.NIZUMI_GENBA_CD.Text;
        }

        /// <summary>
        /// 荷降業者POPUP_AFT
        /// </summary>
        public void PopupAftZioroshiGyousha()
        {
            bool catchErr = false;
            this.SetNioroshiGyousha(out catchErr);
        }

        /// <summary>
        /// 荷降業者POPUP_BEF
        /// </summary>
        public void PopupBefZioroshiGyousha()
        {
            this.logic.tmpNioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷降現場POPUP_AFT
        /// </summary>
        public void PopupAftZioroshiGenba()
        {
            bool catchErr = false;
            this.SetNioroshiGenba(out catchErr);
        }

        /// <summary>
        /// 荷降現場POPUP_BEF
        /// </summary>
        public void PopupBefZioroshiGenba()
        {
            this.logic.tmpNioroshiGenbaCd = this.NIOROSHI_GENBA_CD.Text;
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
            this.PopupAfterTorihikisaki();
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
            this.PopupAftZioroshiGyousha();
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
            this.PopupAftZioroshiGenba();
        }

        /// <summary>
        /// 荷済業者設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_NIZUMI_GYOUSHA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 荷済業者チェック呼び出し
            this.PopupAftZizumiGyousha();
        }

        /// <summary>
        /// 荷済現場設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_NIZUMI_GENBA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 荷済現場チェック呼び出し
            this.PopupAftZizumiGenba();
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
            this.PopupAfterUnpanGyousha();
        }

        #region 車輌登録ボタン押下処理
        /// <summary>
        /// [2]車輌登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.SharyouTouroku();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 単価履歴ボタン押下処理
        /// <summary>
        /// [4]単価履歴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process4_Click(object sender, System.EventArgs e)
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

            if (!string.IsNullOrEmpty(((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Text))
            {
                kyotenCd = ((UIHeaderForm)this.logic.footerForm.headerForm).KYOTEN_CD.Text;
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
            if (!string.IsNullOrEmpty(this.NIZUMI_GYOUSHA_CD.Text))
            {
                nizumiGyoushaCd = this.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.NIZUMI_GENBA_CD.Text))
            {
                nizumiGenbaCd = this.NIZUMI_GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                nioroshiGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                nioroshiGenbaCd = this.NIOROSHI_GENBA_CD.Text;
            }
            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G054",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
        }
        #endregion

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
            string HinmeiCd = Convert.ToString(this.mrwDetail.Rows[index].Cells[LogicClass.CELL_NAME_HINMEI_CD].Value);
            string UnitCd = Convert.ToString(this.mrwDetail.Rows[index].Cells[LogicClass.CELL_NAME_UNIT_CD].Value);


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
            if (!string.IsNullOrEmpty(this.NIZUMI_GYOUSHA_CD.Text))
            {
                nizumiGyoushaCd = this.NIZUMI_GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.NIZUMI_GENBA_CD.Text))
            {
                nizumiGenbaCd = this.NIZUMI_GENBA_CD.Text;
            }
            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G054",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, HinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
            if (tankaForm.dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tankaForm.returnTanka.IsNull)
                {
                    this.mrwDetail.EditingControl.Text = string.Empty;
                }
                else
                {
                    this.mrwDetail.EditingControl.Text = tankaForm.returnTanka.Value.ToString(this.logic.dto.sysInfoEntity.SYS_TANKA_FORMAT);
                }

                if (!UnitCd.Equals(tankaForm.returnUnitCd))
                {
                    if (string.IsNullOrEmpty(tankaForm.returnUnitCd))
                    {
                        this.mrwDetail.Rows[index].Cells[LogicClass.CELL_NAME_UNIT_CD].Value = string.Empty;
                        this.mrwDetail.Rows[index].Cells[LogicClass.CELL_NAME_UNIT_NAME_RYAKU].Value = string.Empty;
                    }
                    else
                    {
                        var units = this.logic.accessor.GetUnit(Convert.ToInt16(tankaForm.returnUnitCd));

                        if (units != null)
                        {
                            this.mrwDetail.Rows[index].Cells[LogicClass.CELL_NAME_UNIT_CD].Value = units[0].UNIT_CD.ToString();
                            this.mrwDetail.Rows[index].Cells[LogicClass.CELL_NAME_UNIT_NAME_RYAKU].Value = units[0].UNIT_NAME_RYAKU.ToString();

                            if (!this.logic.CalcDetaiKingaku(this.mrwDetail.Rows[index]))
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

        /// <summary>
        /// DENPYOU_RIREKI_BTN_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DENPYOU_RIREKI_BTN_Click(object sender, EventArgs e)
        {
            this.logic.CalDenpyouRireki();
        }
        //20211230 Thanh 158916 e
    }
}
