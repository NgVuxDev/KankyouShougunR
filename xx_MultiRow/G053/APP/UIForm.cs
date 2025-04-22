// $Id: UIForm.cs 57300 2015-07-30 14:46:09Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.SalesPayment.DenpyouHakou;
using Shougun.Core.SalesPayment.DenpyouHakou.Const;
using Shougun.Core.Scale.Keiryou.Const;
using Shougun.Function.ShougunCSCommon.Const;
using Shougun.Function.ShougunCSCommon.Dto;
using System.Drawing;
using System.Runtime.InteropServices;
using Shougun.Function.ShougunCSCommon.Utility;

namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku
{
    /// <summary>
    /// 出荷入力
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
        /// 出荷番号
        /// </summary>
        public long ShukkaNumber = -1;

        /// <summary>
        /// SEQ
        /// このパラメータが０以外だとDeleteFlgを無視して表示する
        /// </summary>
        public string SEQ = "0";

        /// <summary>
        /// 受付番号
        /// </summary>
        public long UketukeNumber = -1;

        /// <summary>
        /// 計量番号
        /// </summary>
        public long KeiryouNumber = -1;

        /// <summary>
        /// 受付番号テキストチェンジフラグ
        /// </summary>
        public bool UketsukeNumberTextChangeFlg = false;

        /// <summary>
        /// この画面が呼び出された時、受付番号が引数に存在したかを示します
        /// /// </summary>
        internal bool isArgumentUketsukeNumber = false;

        /// <summary>
        /// 計量番号テキストチェンジフラグ
        /// </summary>
        public bool KeiryouNumberTextChangeFlg = false;

        /// <summary>
        /// この画面が呼び出された時、受付番号が引数に存在したかを示します
        /// /// </summary>
        internal bool isArgumentKeiryouNumber = false;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

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
        /// 伝票発行ポップアップ用DTO
        /// </summary>
        internal ParameterDTOClass denpyouHakouPopUpDTO = new ParameterDTOClass();

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;

        /// <summary>
        /// 継続計量フラグ
        /// </summary>
        internal bool KeizokuKeiryouFlg = false;

        // No.2334-->
        /// <summary>
        /// 滞留新規フラグ
        /// </summary>
        internal bool TairyuuNewFlg = false;
        // No.2334<--

        /// <summary>
        /// 車輌CDが編集中かどうかのフラグ
        /// </summary>
        private bool editingSharyouCdFlag = false;

        /// <summary>
        /// 空車重量にフォーカスインしたタイミングかを示します
        /// </summary>
        private bool isKuushaJuuryouOnEnter = false;

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
        /// EnterかTabボタンが押下されたかどうかの判定フラグ
        /// </summary>
        internal bool pressedEnterOrTab = false;

        /// <summary>
        /// 諸口区分によるフォーカス移動用
        /// 諸口区分設定によってフォーカスを設定した場合に入力項目設定によるフォーカス移動処理を行いたくない場合にTrueを設定
        /// 入力項目設定によるフォーカス移動処理時にTrueだった場合にFalseにし、処理を中断させている
        /// </summary>
        internal bool isSetShokuchiForcus = false;

        /// <summary>
        /// 変更前荷済業者
        /// </summary>
        public string beforNizumiGyousha { get; set; }

        internal bool validateFlag = false;

        /// <summary>
        /// 品名を再読み込みしたかのフラグ
        /// Detailの金額計算で使用する。
        /// </summary>
        internal bool isHinmeiReLoad = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        internal bool isInputError = false;
        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowId"></param>
        /// <param name="windowType">モード</param>
        /// <param name="shukkaNumber">出荷入力 SHUUKA_NUMBER</param>
        /// <param name="lastRunMethod">受入入力で閉じる前に実行するメソッド(別画面からの遷移用)</param>
        public UIForm(WINDOW_ID windowId, WINDOW_TYPE windowType, long shukkaNumber = -1, LastRunMethod lastRunMethod = null,
            long uketsukeNumber = -1, long keiryouNumber = -1, bool keizokuKeiryouFlg = false, bool newChangeFlg = false, string SEQ = "0")
            : base(WINDOW_ID.T_SHUKKA, windowType)
        {
            LogUtility.DebugMethodStart(windowId, windowType, shukkaNumber, lastRunMethod, uketsukeNumber, keiryouNumber, keizokuKeiryouFlg, newChangeFlg, SEQ);

            CommonShogunData.Create(SystemProperty.Shain.CD);

            TairyuuNewFlg = newChangeFlg;   // No.2334

            this.InitializeComponent();
            this.WindowId = windowId;
            this.WindowType = windowType;
            this.ShukkaNumber = shukkaNumber;
            this.closeMethod = lastRunMethod;
            if (uketsukeNumber != -1)
            {
                this.isArgumentUketsukeNumber = true;
            }
            this.UketukeNumber = uketsukeNumber;
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

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // マニフェスト連携用変数の初期化
            RenkeiDenshuKbnCd = (short)SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA;
            RenkeiSystemId = -1;
            RenkeiMeisaiSystemId = -1;
            LogUtility.DebugMethodEnd(windowType, windowType, shukkaNumber, lastRunMethod, uketsukeNumber, keiryouNumber, keizokuKeiryouFlg, newChangeFlg, SEQ);
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
            bool catchErr = false;
            bool isOpenFormError = this.logic.GetAllEntityData(out catchErr);
            if (catchErr)
            {
                return;
            }
            ParentBaseForm = (BusinessBaseForm)this.Parent;

            //PhuocLoc 2020/05/20 #137147 -Start
            // Anchorの設定は必ずOnLoadで行うこと
            if (this.gcMultiRow1 != null)
            {
                this.gcMultiRow1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            //PhuocLoc 2020/05/20 #137147 -End

            //重量値表示プロセス起動
            truckScaleWeight1.ProcessWeight();

            // 20140604 katen 不具合No.4477 start‏
            long tempUkeireNumber = this.ShukkaNumber;
            long tempUketsukeNumber = this.UketukeNumber;
            // 20140604 katen 不具合No.4477 end‏
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

            // 20140604 katen 不具合No.4477 start‏
            this.ShukkaNumber = tempUkeireNumber;
            this.UketukeNumber = tempUketsukeNumber;
            // 20140604 katen 不具合No.4477 end‏

            if (!isOpenFormError)
            {
                base.CloseTopForm();
            }
            // 20140604 katen 不具合No.4477 start‏
            //受付番号と出荷番号判定
            if (this.ShukkaNumber == -1 && this.UketukeNumber != -1)
            {
                //base.OnLoad(e);
                bool retDate = this.logic.GetAllEntityData(out catchErr);
                if (catchErr)
                {
                    return;
                }
                if (!retDate)
                {
                    return;
                }
                this.UKETSUKE_NUMBER.Text = this.UketukeNumber.ToString();
                if (!this.logic.GetUketsukeNumber())
                {
                    return;
                }

                // 初期フォーカス位置  
                this.UKETSUKE_NUMBER.Focus();
            }
            // 20140604 katen 不具合No.4477 end‏

            // 継続入力の初期値を設定
            // システム設定値がない場合は、「2:しない」を初期値とする
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            var keizokuNyuuryoku = userProfile.Settings.DefaultValue.Where(v => v.Name == "継続入力").Select(v => v.Value).DefaultIfEmpty("2").FirstOrDefault();
            this.KEIZOKU_NYUURYOKU_VALUE.Text = keizokuNyuuryoku;
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
            this.logic.SetTopControlFocus();   // No.3822

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
            this.logic.ToAmountValue(sender);
        }

        /// <summary>
        /// 出荷番号更新後処理
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

            if (!this.IsLoading)
            {
                this.IsLoading = true;
                long shukkaNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text)
                    && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out shukkaNumber))
                {
                    if (this.ShukkaNumber != shukkaNumber)  // No.2175
                    {
                        this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        this.ShukkaNumber = shukkaNumber;

                        //base.OnLoad(e);
                        bool catchErr = false;
                        bool retDate = this.logic.GetAllEntityData(out catchErr);
                        if (catchErr)
                        {
                            return;
                        }
                        if (!retDate)
                        {
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.ShukkaNumber = -1;

                            // 再描画を有効にして最新の状態に更新
                            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));

                            this.ENTRY_NUMBER.Focus();
                            this.IsLoading = false;
                            return;
                        }

                        // 権限チェック
                        if (!r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 修正権限がない場合
                            if (r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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
                                this.IsLoading = false;
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

                this.IsLoading = false;
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
        /// 売上消費税値変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URIAGE_SHOUHIZEI_RATE_VALUE_TextChanged(object sender, EventArgs e)
        {
            bool catchErr = false;
            string retString = this.logic.ToPercentForUriageShouhizeiRate(out catchErr);
            if (catchErr)
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
            bool catchErr = false;
            string retString = this.logic.ToPercentForShiharaiShouhizeiRate(out catchErr);
            if (catchErr)
            {
                return;
            }
            this.SHIHARAI_SHOUHIZEI_RATE_VALUE.Text = retString;
        }

        /// <summary>
        /// 荷積業者CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 荷積業者を取得
            bool catchErr = false;
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

            if (!this.isFromSearchButton)
            {
                this.logic.NizumiGyoushaCdSet();  //比較用業者CDをセット
            }
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
        /// 荷積現場CDフォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 荷降現場を取得
            bool catchErr = false;
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
        /// 運搬業者フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

            // 運搬業者を取得
            bool catchErr = false;
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
            this.logic.CheckNyuuryokuTantousha();
        }

        #region 取引先イベント
        /// <summary>
        /// 取引先フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            //// 取引先を取得
            //bool catchErr = false;
            //var torihikisaki = this.logic.accessor.GetTorihikisaki(this.TORIHIKISAKI_CD.Text, this.DENPYOU_DATE.Value, this.logic.footerForm.sysDate, out catchErr);
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
        /// 取引先更新イベント
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
            //// 業者を取得
            //bool catchErr = false;
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

            if (!this.isFromSearchButton && !this.isInputError)
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
        #endregion　業者イベント

        #region 現場イベント
        /// <summary>
        /// 現場フォーカスイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            //// 現場を取得
            //bool catchErr = false;
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
            this.logic.CheckEigyouTantousha();
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
            this.logic.CheckKeitaiKbn();
        }

        /// <summary>
        /// 形態区分再検索処理
        /// </summary>
        public void KEITAI_KBN_CD_Research()
        {
            // 形態区分選択ポップアップ用DataSource再生成
            this.KEITAI_KBN_CD.PopupDataSource = this.logic.CreateKeitaiKbnPopupDataSource();
            this.KEITAI_KBN_SEARCH_BUTTON.PopupDataSource = this.logic.CreateKeitaiKbnPopupDataSource();
        }

        /// <summary>
        /// 台貫区分更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAIKAN_KBN_Validated(object sender, EventArgs e)
        {
            this.logic.CheckDaikanKbn();
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
            this.logic.IsSuuryouKesannFlg = true;
            var inputDenpyouDate = this.DENPYOU_DATE.Text;
            if (!string.IsNullOrEmpty(inputDenpyouDate) && !this.logic.tmpDenpyouDate.Equals(inputDenpyouDate))
            {
                if (!this.logic.CheckDenpyouDate())
                {
                    return;
                }
                //ThangNguyen [Add] 20150828 #12553 Start
                this.beforeUrageDate = this.URIAGE_DATE.Text;
                this.beforeShiharaiDate = this.SHIHARAI_DATE.Text;

                this.URIAGE_DATE.Value = this.DENPYOU_DATE.Value;
                this.SHIHARAI_DATE.Value = this.DENPYOU_DATE.Value;
                this.URIAGE_DATE_OnLeave(sender, e);
                this.SHIHARAI_DATE_OnLeave(sender, e);
                //ThangNguyen [Add] 20150828 #12553 End
            }
            this.logic.IsSuuryouKesannFlg = false;
        }

        /// <summary>
        /// 出荷番号前ボタンクリック処理
        /// 現在入力されている番号の前の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void previousButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            this.previousButtonMainProcess(sender, e);

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            // 初期フォーカス位置
            if (!this.logic.SetTopControlFocus())
            {
                return;
            }
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

            if (!this.IsLoading)
            {
                this.IsLoading = true;
                long shukkaNumber = 0;
                long preshukkaNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                bool preEmptyCheck = false;

                bool catchErr = false;
                // No.1767
                if (string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                {
                    shukkaNumber = this.logic.GetMaxShukkaNumber(out catchErr);
                    preEmptyCheck = false;
                    if (catchErr)
                    {
                        return;
                    }
                    //this.ENTRY_NUMBER.Text = shukkaNumber.ToString();
                }
                else
                {
                    long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out shukkaNumber);
                    preEmptyCheck = true;
                }

                //if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text) && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out shukkaNumber))
                //if (!string.IsNullOrEmpty(shukkaNumber.ToString()))
                //{
                // 出荷番号の入力がある場合
                preshukkaNumber = this.logic.GetPreShukkaNumber(shukkaNumber, preEmptyCheck);
                if (preshukkaNumber <= 0)
                {
                    if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                    {
                        long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out preshukkaNumber);
                    }
                }
                if (preshukkaNumber > 0)
                {
                    // 入力されている出荷番号の前の出荷番号が取得できた場合
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.ShukkaNumber = preshukkaNumber;
                    //base.OnLoad(e);
                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retDate)
                    {
                        // エラー発生時には値をクリアする
                        this.ChangeNewWindow(sender, e);
                        this.IsLoading = false;
                        return;
                    }

                    // 滞留登録された出荷伝票用権限チェック
                    retDate = this.logic.HasAuthorityTairyuu(this.ShukkaNumber, this.SEQ,out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retDate)
                    {
                        // 滞留登録された出荷伝票かつ新規権限がない場合はアラートを表示して処理中断
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E158", "新規");
                        this.WindowType = tmpType;
                        HeaderFormInit();
                        this.IsLoading = false;
                        return;
                    }

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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
                            this.IsLoading = false;
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
                }
                else
                {
                    // 入力されている出荷番号の前の出荷番号が取得できなかった場合
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    this.IsLoading = false;
                    return;
                }
                //}
                this.IsLoading = false;
            }
        }

        /// <summary>
        /// 出荷番号後ボタンクリック処理
        /// 現在入力されている番号の後の番号の情報を取得する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void nextButton_OnClick(object sender, EventArgs e)
        {
            // ウィンドウの再描画を無効にする
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(0), new IntPtr(0));

            this.nextButtonMainProcess(sender, e);

            // 再描画を有効にして最新の状態に更新
            SendMessage(this.Handle, WM_SETREDRAW, new IntPtr(1), new IntPtr(0));
            this.Refresh();
            // 初期フォーカス位置
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

            bool catchErr = false;

            if (!this.IsLoading)
            {
                this.IsLoading = true;
                long shukkaNumber = 0;
                long preshukkaNumber = 0;
                WINDOW_TYPE tmpType = this.WindowType;
                bool nextEmptyCheck = false;
                // No.3341-->
                if (string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                {

                    shukkaNumber = this.logic.GetMaxShukkaNumber(out catchErr);
                    nextEmptyCheck = false;
                    if (catchErr)
                    {
                        return;
                    }
                }
                else
                {
                    long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out shukkaNumber);
                    nextEmptyCheck = true;
                }
                // No.3341<--
                //if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text)
                //    && long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out shukkaNumber))
                //{
                // 出荷番号の入力がある場合
                long PattenName = this.logic.GetNextShukkaNumber(shukkaNumber, out catchErr, nextEmptyCheck);
                if (catchErr)
                {
                    return;
                }
                preshukkaNumber = PattenName;
                if (preshukkaNumber <= 0)
                {
                    if (!string.IsNullOrEmpty(this.ENTRY_NUMBER.Text))
                    {
                        long.TryParse(this.ENTRY_NUMBER.Text.ToString(), out preshukkaNumber);
                    }
                }
                if (preshukkaNumber > 0)
                {
                    // 入力されている出荷番号の後の出荷番号が取得できた場合
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.ShukkaNumber = preshukkaNumber;
                    //base.OnLoad(e);
                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retDate)
                    {
                        // エラー発生時には値をクリアする
                        this.ChangeNewWindow(sender, e);
                        this.IsLoading = false;
                        return;
                    }

                    // 滞留登録された出荷伝票用権限チェック
                    retDate = this.logic.HasAuthorityTairyuu(this.ShukkaNumber, this.SEQ,out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retDate)
                    {
                        // 滞留登録された出荷伝票かつ新規権限がない場合はアラートを表示して処理中断
                        MessageBoxShowLogic msg = new MessageBoxShowLogic();
                        msg.MessageBoxShow("E158", "新規");
                        this.WindowType = tmpType;
                        HeaderFormInit();
                        this.IsLoading = false;
                        return;
                    }

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        // 修正権限がない場合
                        if (r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
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
                            this.IsLoading = false;
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
                }
                else
                {
                    // 入力されている出荷番号の後の出荷番号が取得できなかった場合
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E045");
                    this.IsLoading = false;
                    return;
                }
                //}     // No.3341
                this.IsLoading = false;
            }
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
                if (r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.InitNumbers();
                    this.SEQ = "0";

                    //base.OnLoad(e);
                    this.logic = new LogicClass(this);
                    bool catchErr = false;
                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                    if (catchErr)
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

        // No.2334-->
        /// <summary>
        /// 新規を指定受入番号に変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeWindow(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (!this.IsLoading)
            {
                this.IsLoading = true;
                if (this.ShukkaNumber > 0)
                {
                    this.TairyuuNewFlg = true;

                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.UketukeNumber = -1;
                    this.KeiryouNumber = -1;
                    this.ENTRY_NUMBER.Text = this.ShukkaNumber.ToString();

                    //base.OnLoad(e);

                    bool catchErr = false;
                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                    if (catchErr)
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
                    this.logic.SetTopControlFocus();

                    this.TairyuuNewFlg = false;
                }
                this.IsLoading = false;
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

            FormManager.OpenFormWithAuth("G055", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.SHUKKA, CommonShogunData.LOGIN_USER_INFO.SHAIN_CD);

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

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 月次処理中、月次ロックチェック
            if (this.GetsujiLockCheck())
            {
                return;
            }

            if (WINDOW_TYPE.NEW_WINDOW_FLAG.Equals(this.WindowType) && this.ShukkaNumber != -1 && this.TairyuuNewFlg == false)
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

            if (!base.RegistErrorFlag)
            {

                bool catchErr = false;
                bool retCheck = this.logic.CreateEntityAndUpdateTables(true, base.RegistErrorFlag, out catchErr);
                if (catchErr)
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
                            bool retDate = this.logic.GetAllEntityData(out catchErr);
                            if (catchErr)
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
                        }
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:

                        if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.Text)
                            && r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 新規モードに切り替え、再度入力可能状態とする
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            this.ShukkaNumber = -1;
                            this.InitNumbers();
                            //base.OnLoad(e);

                            // Entity等の初期化
                            this.logic = new LogicClass(this);
                            bool retDate = this.logic.GetAllEntityData(out catchErr);
                            if (catchErr)
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
                        }
                        break;
                    default:
                        break;
                }

            }

            if (!base.RegistErrorFlag)
            {
                // 初期フォーカス位置
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

            this.RegistDataProcess(sender, e);

            if ((SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.PrevText)
                && r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.NEW_WINDOW_FLAG, false)
                && this.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                || base.RegistErrorFlag
                || !this.logic.isRegistered)
            {
                if (!base.RegistErrorFlag)
                {
                    // 初期フォーカス位置
                    this.logic.SetTopControlFocus();
                }
                else
                {
                    SetControlFocus();
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録メイン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistDataProcess(object sender, EventArgs e)
        {
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

                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                if (this.logic.footerForm.bt_func9.Enabled)
                {
                    this.setFunctionEnabled(allFalseFuncState);
                }
                else
                {
                    return;
                }
                // 20141114 ブン 「2重登録チェック」に完了を追加する　end

                // 取引先と拠点コードの関連チェック
                if (!this.logic.CheckTorihikisakiAndKyotenCd(null, this.TORIHIKISAKI_CD.Text))
                {
                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                    return;
                }

                bool catchErr = false;
                bool retCheck = this.logic.SharyouDateCheck(out catchErr);
                if (catchErr)
                {
                    this.setFunctionEnabled(currentFuncState);
                    return;
                }

                bool retCheck2 = this.logic.UntenshaDateCheck(out catchErr);
                if (catchErr)
                {
                    this.setFunctionEnabled(currentFuncState);
                    return;
                }

                // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
                // 車輛チェック
                if (!retCheck)
                {
                    this.SHARYOU_CD.Focus();
                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                    return;
                }
                else if (!retCheck2)
                {
                    this.UNTENSHA_CD.Focus();
                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                    this.setFunctionEnabled(currentFuncState);
                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                    return;
                }
                // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

                this.logic.IsRegist = true;
                // 登録前にもう一度計算する
                // CalcDetailを実行すると空行を削除する処理が実行される。
                // その時に削除する行がCurrentだった場合にエラーが出てしまうので、Currentを移動しておく。
                this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, LogicClass.CELL_NAME_ROW_NO);
                if (!this.logic.CalcDetail())
                {
                    this.setFunctionEnabled(currentFuncState);
                    return;
                }

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                        if (!this.logic.SetRequiredSetting(false))
                        {
                            this.setFunctionEnabled(currentFuncState);
                            return;
                        }
                        var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                        base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
                        if (!this.logic.RequiredSettingInit())
                        {
                            this.setFunctionEnabled(currentFuncState);
                            return;
                        }
                        retCheck = this.logic.CheckRequiredDataForDeital(out catchErr);
                        if (catchErr)
                        {
                            this.setFunctionEnabled(currentFuncState);
                            return;
                        }

                        // Detailの行数チェックはFWでできないので自前でチェック
                        if (!base.RegistErrorFlag && !retCheck)
                        {

                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
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
                        // No.4578-->
                        // 20150415 go 在庫品名振分チェック(修正後のG051からコピー) Start
                        else if (!base.RegistErrorFlag)
                        {
                            bool retRegist = this.logic.ZaikoRegistCheck(out catchErr);
                            if (catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                return;
                            }

                            if (!retRegist)
                            {
                                // コンテナ入力がある場合は現場(&業者)必須
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShowError("在庫品名が選択されている場合、自社の荷積現場を選択する必要があります。");
                                base.RegistErrorFlag = true;
                            }
                        }
                        // 20150415 go 在庫品名振分処理追加 Start
                        // No.4578<--
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

                        // 現金取引チェック
                        if (!base.RegistErrorFlag)
                        {
                            retCheck = this.logic.GenkinTorihikiCheck(out catchErr);
                            if (catchErr)
                            {
                                this.setFunctionEnabled(currentFuncState);
                                return;
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

                        //20210825 Thanh 154360 s
                        if (!base.RegistErrorFlag)
                        {
                            if (!this.logic.CheckDetailShukkaAndKenshu())
                            {
                                base.RegistErrorFlag = true;
                            }
                        }
                        //20210825 Thanh 154360 e
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

                if (!base.RegistErrorFlag)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult result = new DialogResult();
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:

                            // 伝票発行ポップアップ表示
                            if (this.ShowDenpyouHakouPopup())
                            {

                                //締チェック位置の変更 start
                                /// 20141112 Houkakou 「出荷入力」の締済期間チェックの追加　start
                                retCheck = this.logic.SeikyuuDateCheck(out catchErr);
                                if (catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }

                                retCheck2 = this.logic.SeisanDateCheck(out catchErr);
                                if (catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                                if (!retCheck)
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    return;
                                }
                                else if (!retCheck2)
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    return;
                                }
                                /// 20141112 Houkakou 「出荷入力」の締済期間チェックの追加　end
                                //締チェック位置の変更 end

                                bool retDate = this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag, out catchErr);
                                if (catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                                if (!retDate)
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;
                                    return;
                                }
                            }
                            else
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.setFunctionEnabled(currentFuncState);
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                this.logic.IsRegist = false;
                                return;
                            }

                            //仕切書
                            if (!this.logic.PrintShikirisyo())
                            {
                                this.setFunctionEnabled(currentFuncState);
                                return;
                            }

                            // 計量票
                            if (this.denpyouHakouPopUpDTO != null
                                && ConstClass.KEIRYOU_PRIRNT_KBN_1.Equals(this.denpyouHakouPopUpDTO.Keiryou_Prirnt_Kbn_Value))
                            {
                                this.logic.isSubFunctionCall = false;
                                if (!this.logic.PrintKeiryouhyou())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                                this.logic.isSubFunctionCall = true;
                            }

                            // 帳票出力
                            if (this.denpyouHakouPopUpDTO != null
                                && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                            {
                                if (!this.logic.Print())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                            }

                            // 完了メッセージ表示
                            msgLogic.MessageBoxShow("I001", "登録");

                            // 4935_7 出荷入力 jyokou 20150505 str
                            this.logic.isRegistered = true;
                            // 4935_7 出荷入力 jyokou 20150505 end

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
                                bool retDate = this.logic.GetAllEntityData(out catchErr);
                                if (catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                                if (!this.logic.WindowInit())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }

                                if (!this.logic.ButtonInit())
                                {
                                    this.setFunctionEnabled(currentFuncState);
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
                            bool isZeiKbnChanged = false;
                            if (!this.logic.dto.entryEntity.TAIRYUU_KBN)
                                // 税区分、税計算区分、取引区分をセット
                                isZeiKbnChanged = this.logic.zeiKbnChanged();
                            if (!isZeiKbnChanged)
                            {
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                this.setFunctionEnabled(currentFuncState);
                                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                return;
                            }
                            result = msgLogic.MessageBoxShow("C038");
                            if (result == DialogResult.Yes || this.logic.dto.entryEntity.TAIRYUU_KBN)
                            {
                                // 伝票発行ポップアップ表示
                                if (this.ShowDenpyouHakouPopup())
                                {

                                    //締チェックの位置を変更 start
                                    if (this.logic.CheckAllShimeStatus())
                                    {
                                        // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                        msgLogic.MessageBoxShow("I011", "修正");
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }

                                    retCheck = this.logic.SeikyuuDateCheck(out catchErr);
                                    if (catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }

                                    retCheck2 = this.logic.SeisanDateCheck(out catchErr);
                                    if (catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }

                                    /// 20141112 Houkakou 「出荷入力」の締済期間チェックの追加　start
                                    if (!retCheck)
                                    {
                                        // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                        this.setFunctionEnabled(currentFuncState);
                                        // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                        return;
                                    }
                                    else if (!retCheck2)
                                    {
                                        // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                        this.setFunctionEnabled(currentFuncState);
                                        // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                        return;
                                    }
                                    /// 20141112 Houkakou 「出荷入力」の締済期間チェックの追加　end
                                    //締チェックの位置を変更 end

                                    bool retDate = this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag, out catchErr);
                                    if (catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }
                                    if (!retDate)
                                    {
                                        // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                        this.setFunctionEnabled(currentFuncState);
                                        // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                        this.logic.IsRegist = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;
                                    return;
                                }

                                //仕切書
                                if (!this.logic.PrintShikirisyo())
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }

                                // 計量票
                                if (this.denpyouHakouPopUpDTO != null
                                    && ConstClass.KEIRYOU_PRIRNT_KBN_1.Equals(this.denpyouHakouPopUpDTO.Keiryou_Prirnt_Kbn_Value))
                                {
                                    this.logic.isSubFunctionCall = false;
                                    if (!this.logic.PrintKeiryouhyou())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }
                                    this.logic.isSubFunctionCall = true;
                                }

                                // 帳票出力
                                if (this.denpyouHakouPopUpDTO != null
                                    && ConstClass.RYOSYUSYO_KBN_1.Equals(this.denpyouHakouPopUpDTO.Ryousyusyou))
                                {
                                    if (!this.logic.Print())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }
                                }

                                // 完了メッセージ表示
                                msgLogic.MessageBoxShow("I001", "更新");

                                // 4935_7 出荷入力 jyokou 20150505 str
                                this.logic.isRegistered = true;
                                // 4935_7 出荷入力 jyokou 20150505 end

                                // 滞留一覧画面を更新
                                FormManager.UpdateForm("G303");

                                if (SalesPaymentConstans.KEIZOKU_NYUURYOKU_ON.Equals(this.KEIZOKU_NYUURYOKU_VALUE.Text)
                                    && r_framework.Authority.Manager.CheckAuthority("G053", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                                {
                                    // 継続入力ON かつ 追加権限がある場合
                                    // 【追加】モード初期表示処理
                                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                                    this.ShukkaNumber = -1;

                                    //base.OnLoad(e);

                                    // Entity等の初期化
                                    this.logic = new LogicClass(this);
                                    bool retDate = this.logic.GetAllEntityData(out catchErr);
                                    if (catchErr)
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }
                                    if (!this.logic.WindowInit())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
                                        return;
                                    }

                                    if (!this.logic.ButtonInit())
                                    {
                                        this.setFunctionEnabled(currentFuncState);
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

                            }

                            break;

                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            result = msgLogic.MessageBoxShow("C026");
                            if (result == DialogResult.Yes)
                            {

                                //締チェック start
                                if (this.logic.CheckAllShimeStatus())
                                {
                                    // 登録時点で既に該当伝票について締処理がなされていた場合は、登録を中断する
                                    msgLogic.MessageBoxShow("I011", "削除");
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                                //締チェック end

                                bool retDate = this.logic.CreateEntityAndUpdateTables(false, base.RegistErrorFlag, out catchErr);
                                if (catchErr)
                                {
                                    this.setFunctionEnabled(currentFuncState);
                                    return;
                                }
                                if (!retDate)
                                {
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                                    this.setFunctionEnabled(currentFuncState);
                                    // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                                    this.logic.IsRegist = false;
                                    return;
                                }

                                // 完了メッセージ表示
                                msgLogic.MessageBoxShow("I001", "削除");

                                // 4935_7 出荷入力 jyokou 20150505 str
                                this.logic.isRegistered = true;
                                // 4935_7 出荷入力 jyokou 20150505 end

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

                // 20141114 ブン 「2重登録チェック」に完了を追加する　start
                this.setFunctionEnabled(currentFuncState);
                // 20141114 ブン 「2重登録チェック」に完了を追加する　end
                this.logic.IsRegist = false;

                // フォーカス制御はRegistメソッドで制御する

            }
            finally
            {
                this.logic.IsRegist = false;
                LogUtility.DebugMethodEnd();
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
            editingMultiRowFlag = true;
            bool catchErr = false;
            bool retWarihuri = this.logic.JudgeWarihuri(out catchErr);
            if (catchErr)
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

            var targetRow = this.gcMultiRow1.CurrentRow;
            if (targetRow != null)
            {
                var warifuriJyuuryou = targetRow.Cells[LogicClass.CELL_NAME_WARIFURI_JYUURYOU].Value;
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
            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            bool isFormColseFlg = true;
            bool isKakuNinDailogFlg = false;

            if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text)
                    || !string.IsNullOrEmpty(this.GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.GENBA_CD.Text)
                    || !string.IsNullOrEmpty(this.EIGYOU_TANTOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.SHARYOU_CD.Text)
                    || !string.IsNullOrEmpty(this.SHASHU_CD.Text)
                    || !string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.UNTENSHA_CD.Text)
                    || !string.IsNullOrEmpty(this.NINZUU_CNT.Text)
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
                    dr = this.logic.msgLogic.MessageBoxShow("C109", "登録中", "出荷入力");
                else
                    dr = this.logic.msgLogic.MessageBoxShow("C109", "修正中", "出荷入力");

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

            LogUtility.DebugMethodEnd(sender, e);
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
            bool catchErr = false;

            switch (e.CellName)
            {
                case LogicClass.CELL_NAME_CHOUSEI_JYUURYOU:
                    bool retChousei = this.logic.ValidateChouseiJyuuryou(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;
                case LogicClass.CELL_NAME_CHOUSEI_PERCENT:
                    retChousei = this.logic.ValidateChouseiPercent(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case LogicClass.CELL_NAME_WARIFURI_JYUURYOU:
                    retChousei = this.logic.ValidateWarifuriJyuuryou(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                case LogicClass.CELL_NAME_WARIFURI_PERCENT:
                    retChousei = this.logic.ValidateWarifuriPercent(out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;
                case LogicClass.CELL_NAME_HINMEI_CD:
                    // No.4256-->コメントアウト
                    // 前回値と変更が無かったら処理中断
                    //if (beforeValuesForDetail.ContainsKey(e.CellName)
                    //    && beforeValuesForDetail[e.CellName].Equals(
                    //        Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
                    //{
                    //    return;
                    //}
                    // No.4256<--

                    // No.3086-->
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if (beforeValuesForDetail.ContainsKey(e.CellName)
                        && beforeValuesForDetail[e.CellName].Equals(
                            Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)) && !this.isInputError)
                    {
                        // 品名CDの入力値が前回入力時と同じならば処理中断（ただし、品名ポップアップからの選択は例外）
                        return;
                    }
                    else
                    {
                        this.isInputError = false;
                        if (string.IsNullOrEmpty(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
                        {
                            this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_HINMEI_NAME].Value = "";
                        }
                        retChousei = this.logic.GetHinmei(this.gcMultiRow1.CurrentRow, out catchErr);
                        if (catchErr)
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
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                    // 先に品名コードのエラーチェックし伝種区分が合わないものは伝票ポップアップ表示しないようにする
                    retChousei = this.logic.CheckHinmeiCd(this.gcMultiRow1.CurrentRow, out catchErr);
                    if (catchErr)
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

                        // 20151021 katen #13337 品名手入力に関する機能修正 start
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                        // 20151021 katen #13337 品名手入力に関する機能修正 end

                        // No.4578-->
                        // 20150411 go 在庫品名クリア追加 Start
                        if (!this.logic.ZaikoHinmeiHuriwakesClear(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }
                        // 20150411 go 在庫品名クリア追加 End
                        // No.4578<--
                        return;
                    }
                    // No.3086<--

                    // 空だったら処理中断
                    this.gcMultiRow1.BeginEdit(false);
                    this.gcMultiRow1.EndEdit();
                    this.gcMultiRow1.NotifyCurrentCellDirty(false);
                    if (string.IsNullOrEmpty((string)this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value))
                    {
                        this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_NAME].Value = string.Empty;
                        return;
                    }

                    // No.4256-->
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
                    // No.4256<--

                    bool bResult = true;

                    if (string.IsNullOrEmpty(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[LogicClass.CELL_NAME_DENPYOU_KBN_CD].Value)))
                    {
                        bResult = this.logic.SetDenpyouKbn();
                    }

                    if (!bResult)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        retChousei = this.logic.CheckHinmeiCd(this.gcMultiRow1.CurrentRow, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }
                        // 伝票ポップアップがキャンセルされなかった場合、または伝票ポップアップが表示されない場合
                        if (retChousei)    // 品名コードの存在チェック（伝種区分が出荷、または共通）
                        {
                            // 品名再読込フラグを立てる
                            this.isHinmeiReLoad = true;

                            if (!this.logic.SearchAndCalcForUnit(true, this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }
                            this.logic.ResetTankaCheck(); // MAILAN #158992 START

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

                            // No.4578-->
                            // 20150411 go 在庫品名クリア追加 Start
                            if (!this.logic.ZaikoHinmeiHuriwakesClear(this.gcMultiRow1.CurrentRow))
                            {
                                return;
                            }
                            // 20150411 go 在庫品名クリア追加 End
                            // No.4578<--
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

                        // No.4578-->
                        // 20150411 go 在庫品名振分処理追加 Start
                        // 品名CD変更した場合、在庫品名・比率を再検索する
                        this.logic.ZaikoHinmeiHuriwakesSearch(this.gcMultiRow1.CurrentRow);
                        // 20150411 go 在庫品名振分処理追加 End
                        // No.4578<--
                    }
                    break;
                case LogicClass.CELL_NAME_HINMEI_NAME:
                    retChousei = this.logic.ValidateHinmeiName(out catchErr);
                    if (catchErr)
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
                    retChousei = this.logic.ValidateJyuryouFormat(this.gcMultiRow1.CurrentRow, e.CellName, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (!retChousei)
                    {
                        e.Cancel = true;
                    }
                    break;

                // No.4578-->
                // 20150415 go 在庫品名チェック追加(修正後のG051と同様処理) Start
                case LogicClass.CELL_NAME_ZAIKO_HINMEI_CD:
                    if (!this.logic.ZaikoChangeCheck(this.gcMultiRow1.CurrentRow))
                    {
                        e.Cancel = true;
                    }
                    break;
                // 20150415 go 在庫品名チェック追加 End
                // No.4578<--

                default:
                    break;
            }

            // 単価と金額の活性/非活性制御
            if (e.CellName.Equals(LogicClass.CELL_NAME_TANKA) &&
                beforeValuesForDetail.ContainsKey(e.CellName) &&
                !beforeValuesForDetail[e.CellName].Equals(Convert.ToString(this.gcMultiRow1.CurrentRow.Cells[e.CellName].Value)))
            {
                // 単価の場合のみCellValidatedでReadOnly設定が変わる場合があるのでここで一旦計算を行う
                this.logic.CalcDetaiKingaku(this.gcMultiRow1.CurrentRow);
                if (!this.logic.CalcTotalValues()) { return; }
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
            //if (this.gcMultiRow1.CurrentRow.Cells[e.CellName].Name != LogicClass.CELL_NAME_HINMEI_CD)
            // 20150120 判断条件修正(有価在庫不具合一覧108) Start
            //if (e.CellName != LogicClass.CELL_NAME_HINMEI_CD ||
            //    e.CellName != LogicClass.CELL_NAME_ZAIKO_HINMEI_CD)
            if (e.CellName != LogicClass.CELL_NAME_HINMEI_CD &&
                e.CellName != LogicClass.CELL_NAME_ZAIKO_HINMEI_CD)
            // 20150120 判断条件修正(有価在庫不具合一覧108) End
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
                            editingMultiRowFlag = false; // MAILAN #158992 START
                            return;
                        }
                        this.logic.ResetTankaCheck(); // MAILAN #158992 START
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
                        break;

                    case LogicClass.CELL_NAME_EMPTY_JYUURYOU:
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
                        if (!this.logic.CalcChouseiJyuuryou())
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
                        if (!this.logic.CalcChouseiJyuuryou())
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
                        if (!this.logic.CalcChouseiJyuuryou())
                        {
                            return;
                        }
                        break;

                    case LogicClass.CELL_NAME_WARIFURI_JYUURYOU:
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
                        break;

                    case LogicClass.CELL_NAME_WARIFURI_PERCENT:
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

                    case LogicClass.CELL_NAME_URIAGESHIHARAI_DATE:
                        // 消費税の取得
                        if (!this.logic.SetShouhizeiRateForDetail(this.gcMultiRow1.CurrentRow))
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

                    // No.4578-->
                    // 20150415 go 在庫品名単独設定追加(修正後のG051と同様処理) End
                    case LogicClass.CELL_NAME_ZAIKO_HINMEI_CD:
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
                            this.logic.ZaikoHinmeiKakunou(this.gcMultiRow1.CurrentRow);
                        }
                        break;
                    // 20150415 go 在庫品名単独設定追加 End
                    // No.4578<--

                    default:
                        break;
                }

                // 高々十数件の明細行を計算するだけなので
                // 毎回合計系計算を実行する
                // 合計系計算
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
            LogUtility.DebugMethodEnd(sender, e);
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

            // No.4256-->
            // 品名でPopup表示後処理追加
            //if (this.gcMultiRow1.Columns[e.CellIndex].Name.Equals(LogicClass.CELL_NAME_HINMEI_CD))
            if (e.CellName == LogicClass.CELL_NAME_HINMEI_CD)
            {
                // PopupResult取得できるようにPopupAfterExecuteにデータ設定
                GcCustomTextBoxCell cell = (GcCustomTextBoxCell)row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                cell.PopupAfterExecute = PopupAfter_HINMEI_CD;
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
                    if (!this.logic.SetHinmeiName(row))
                    {
                        return;
                    }
                    break;
                case LogicClass.CELL_NAME_EMPTY_JYUURYOU:   // 空車重量
                    //if (!this.logic.ChangeWarihuriAndChouseiInputStatus())
                    //{
                    //    return;
                    //}
                    //if (!this.logic.CalcYoukiJyuuryou())                         // 容器重量にも影響するので再計算
                    //{
                    //    return;
                    //}
                    //if (!this.logic.CalcDetail())
                    //{
                    //    return;
                    //}
                    //if (!this.logic.CalcSuuryou(row))    // 数量再計算
                    //{
                    //    return;
                    //}
                    //if (!this.logic.CalcDetaiKingaku(row))
                    //{
                    //    return;
                    //}
                    //if (!this.logic.CalcTotalValues())           // 合計計算
                    //{
                    //    return;
                    //}
                    //// 合計系計算
                    //if (!this.logic.CalcTotalValues())
                    //{
                    //    return;
                    //}
                    //this.isKuushaJuuryouOnEnter = true;
                    ////this.SetEmptyAddNewRow();				// 新規行追加
                    //this.isKuushaJuuryouOnEnter = false;
                    break;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 予定一覧(F4)
        /// </summary>
        internal void UketsukeDenpyo(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenForm("G292", WINDOW_TYPE.NONE, DENSHU_KBN.SHUKKA);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 受付番号ロストフォーカス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NUMBER_Validated(object sender, EventArgs e)
        {
            // 検収データの場合何もしない
            if (this.KENSHU_MUST_KBN.Checked && (this.logic.kenshuZumi.Equals(this.txtKensyuu.Text))) return;

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
                            bool catchErr = false;

                            // No.2599-->
                            this.gcMultiRow1.BeginEdit(false);
                            this.gcMultiRow1.Rows.Clear();
                            this.gcMultiRow1.EndEdit();
                            // No.2599<--

                            this.notEditingOperationFlg = true;
                            catchErr = this.logic.GetUketsukeNumber();
                            this.notEditingOperationFlg = false;
                            if (!catchErr) return;

                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
                            this.logic.UketukeBangoCheck(out catchErr);
                            if (catchErr)
                            {
                                return;
                            }
                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

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
                            if (this.logic.tUketsukeSkEntry == null)
                            {
                                // 更新用受付データ再取得
                                this.logic.GetUketsukeData();
                            }
                        }
                        this.ActiveControl = this.UKETSUKE_NUMBER;
                        this.beforbeforControlName = "UKETSUKE_NUMBER";
                        this.beforControlName = "UKETSUKE_NUMBER";
                    }
                    else
                    {
                        this.logic.ClearTUketsukeSkEntry();
                    }
                }
            }
        }

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
                // 計量番号が変更されていない時は何もしない
                if (this.KeiryouNumberTextChangeFlg)
                {
                    // 計量番号入力時かつ
                    // 値に変更がある場合 もしくは 同じ計量番号でも再入力された場合は実行
                    if (!string.IsNullOrEmpty(this.KEIRYOU_NUMBER.Text))
                    {
                        // 初期化
                        this.KeiryouNumberTextChangeFlg = false;

                        if (!this.KEIRYOU_NUMBER.Text.Equals(this.KeiryouNumber.ToString()))
                        {
                            // No.2599-->
                            this.gcMultiRow1.BeginEdit(false);
                            this.gcMultiRow1.Rows.Clear();
                            this.gcMultiRow1.EndEdit();
                            // No.2599<--

                            this.KeiryouNumber = long.Parse(this.KEIRYOU_NUMBER.Text);
                            if (!this.logic.GetKeiryouNumber())
                            {
                                return;
                            }
                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
                            bool catchErr = false;
                            bool PattenName = this.logic.KeiryouBangoCheck(out catchErr);
                            if (catchErr)
                            {
                                return;
                            }
                            // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end
                        }
                    }
                }
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

            // 取込ファイルの作成or更新を行う。
            var jyuryoTorikomiUtil = new JyuryoTorikomiUtility();
            jyuryoTorikomiUtil.MakeTorikomiFile();

            if (!this.logic.SetJyuuryou(WeightDisplaySwitch))
            {
                this.setFunctionEnabled(currentFuncState);
                return;
            }
            // 合計系計算
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

            // 計量入力画面に遷移
            FormManager.OpenForm("G045");

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// [2]計量票発行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, System.EventArgs e)
        {
            this.logic.PrintKeiryouhyou();
        }
        /// <summary>
        /// [3]手入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process3_Click(object sender, System.EventArgs e)
        {
            //次期開発のため未実装
        }
        /// <summary>
        /// [4]運賃入力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process4_Click(object sender, System.EventArgs e)
        {
            this.logic.OpenUnchinNyuuryoku(sender, e);
        }
        /// <summary>
        /// [5]検収明細
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process5_Click(object sender, System.EventArgs e)
        {
            this.logic.OpenKenshuMeisaiNyuuryoku();
        }
        #endregion プロセスボタン押下処理

        // No.4578-->
        // 20150415 在庫品名CD KeyDownイベント設定(修正後のG051からコピー) Start
        // 在庫品名CDが活性化の上、
        // グリッドのKeyDownが効かないため、EditingControlShowingに変更して、
        // 毎回Enter編集モードに入る時、該当セルにKeyDownイベントをバインドする。
        /// <summary>
        /// 明細_在庫品名CDスペースキー押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void gcMultiRow1_KeyDown(object sender, KeyEventArgs e)
        private void zaikoHinmeiCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (this.gcMultiRow1.SelectedCells != null &&
                    this.gcMultiRow1.SelectedCells.Count == 1 &&
                    this.gcMultiRow1.SelectedCells[0].Name == LogicClass.CELL_NAME_ZAIKO_HINMEI_CD)
                {
                    if (this.logic.ZaikoChangeCheck(this.gcMultiRow1.CurrentRow, false))
                    {
                        if (!this.logic.ZaikoHinmeiHuriwakesGamenSeni(this.gcMultiRow1.CurrentRow))
                        {
                            return;
                        }

                        if (sender != null && sender is TextBox)
                        {
                            TextBox ctrl = sender as TextBox;
                            ctrl.Text = string.Empty;
                            this.beforeValuesForDetail[LogicClass.CELL_NAME_ZAIKO_HINMEI_CD] = string.Empty;
                            if (this.logic.dto.rowZaikoHinmeiHuriwakes[this.gcMultiRow1.CurrentRow].Count == 1)
                            {
                                ctrl.Text =
                                    this.logic.dto.rowZaikoHinmeiHuriwakes[this.gcMultiRow1.CurrentRow][0].ZAIKO_HINMEI_CD;
                                this.beforeValuesForDetail[LogicClass.CELL_NAME_ZAIKO_HINMEI_CD] =
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
                gcmr.SelectedCells[0].Name == LogicClass.CELL_NAME_ZAIKO_HINMEI_CD)
            {
                e.Control.KeyDown -= this.zaikoHinmeiCd_KeyDown;
                e.Control.KeyDown += this.zaikoHinmeiCd_KeyDown;
            }
        }
        // 20150415 在庫品名CD KeyDownイベント設定(修正後のG051からコピー) End
        // No.4578<--

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
            this.logic.CheckUntensha();
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
        public bool IsExistShukkaData()
        {
            bool catchErr = false;
            bool retExist = this.logic.IsExistShukkaData(this.ShukkaNumber, out catchErr);
            if (catchErr)
            {
                return false;
            }

            return retExist;
        }

        /// <summary>
        /// 出荷番号、SEQのデータで滞留登録された出荷伝票用の権限チェック
        /// </summary>
        /// <returns></returns>
        public bool HasAuthorityTairyuu()
        {
            bool catchErr = false;
            bool retDate = this.logic.HasAuthorityTairyuu(this.ShukkaNumber, this.SEQ, out catchErr);
            if (catchErr)
            {
                return false;
            }
            return retDate;
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
        private bool ShowDenpyouHakouPopup()
        {
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
                    SalesPaymentConstans.DENSHU_KBN_CD_SHUKKA_STR,
                    kakuteiKbn);
            var callBaseForm = new MasterBaseForm(callForm, WINDOW_TYPE.NONE, false);
            var result = callBaseForm.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                this.denpyouHakouPopUpDTO = callForm.ParameterDTO;
                returnVal = true;
            }
            callBaseForm.Dispose();

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
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            if (this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd)
            {
                this.SetGyousha();
                this.GYOUSHA_CD.Focus();
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

            if (this.GYOUSHA_CD.Text != this.logic.tmpGyousyaCd || this.GENBA_CD.Text != this.logic.tmpGenbaCd)
            {
                this.logic.IsSuuryouKesannFlg = true;
                this.SetGenba();
                this.logic.IsSuuryouKesannFlg = false;

                this.GENBA_CD.Focus();
            }

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷積業者CDへフォーカス移動する
        /// 荷積業者CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNiZumiGyoushaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.SetNizumiGyousha();
            this.NIZUMI_GYOUSHA_CD.Focus();

            // 初期化
            this.isFromSearchButton = false;
        }

        /// <summary>
        /// 荷積現場CDへフォーカス移動する
        /// 荷積現場CDフォーカスアウトチェックをさせたいときに実行
        /// </summary>
        public virtual void MoveToNiZumiGenbaCd()
        {
            // 検索ボタンから入力された
            this.isFromSearchButton = true;

            this.logic.IsSuuryouKesannFlg = true;
            this.SetNizumiGenba();
            this.logic.IsSuuryouKesannFlg = false;

            this.NIZUMI_GENBA_CD.Focus();

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

            this.SetUnpanGyousha();
            this.UNPAN_GYOUSHA_CD.Focus();

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
        /// 出荷番号、受付番号、計量番号の初期化
        /// </summary>
        private void InitNumbers()
        {
            this.ShukkaNumber = -1;
            this.UketukeNumber = -1;
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

        #region 取引先CD・業者CD・現場CDの関連情報セット処理

        /// <summary>
        /// 取引先CDに関連する情報をセット
        /// </summary>
        public bool SetTorihikisaki()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckTorihikisaki(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (catchErr)
            {
                return false;
            }

            this.logic.TorihikisakiCdSet();

            return ret;
        }

        /// <summary>
        /// 業者CDに関連する情報をセット
        /// </summary>
        public bool SetGyousha()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckGyousha(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (catchErr)
            {
                return false;
            }

            // 20151021 katen #13337 品名手入力に関する機能修正 start
            this.logic.hasShow = false;
            // 20151021 katen #13337 品名手入力に関する機能修正 end

            return ret;
        }

        /// <summary>
        /// 現場に関連する情報をセット
        /// </summary>
        public bool SetGenba()
        {
            // 初期化
            bool ret = false;
            bool catchErr = false;

            if ((String.IsNullOrEmpty(this.GYOUSHA_CD.Text) || !this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text)
                    || (this.logic.tmpGyousyaCd.Equals(this.GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.GYOUSHA_NAME_RYAKU.Text)))
                    || (String.IsNullOrEmpty(this.GENBA_CD.Text) || !this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text)
                    || (this.logic.tmpGenbaCd.Equals(this.GENBA_CD.Text) && string.IsNullOrEmpty(this.GENBA_NAME_RYAKU.Text))))
            {
                ret = this.logic.CheckGenba(out catchErr);
                if (catchErr || !ret)
                {
                    return false;
                }

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                this.logic.hasShow = false;
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                // 要検収の設定
                if (!this.logic.SetDefultKenshuMustKbn())
                {
                    return false;
                }
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
            bool catchErr = false;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckNizumiGyoushaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (catchErr)
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
            bool catchErr = false;

            ret = this.logic.CheckNizumiGenbaCd(out catchErr);

            if (catchErr)
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
            bool catchErr = false;

            this.logic.IsSuuryouKesannFlg = true;
            ret = this.logic.CheckUnpanGyoushaCd(out catchErr);
            this.logic.IsSuuryouKesannFlg = false;

            if (catchErr)
            {
                return false;
            }

            this.logic.UnpanGyoushaCdSet();  //比較用業者CDをセット

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

        // No.3875-->
        internal void KUUSHA_JYURYO_TextChanged(object sender, EventArgs e)
        {
            if (!this.IsLoading && !string.IsNullOrEmpty(this.KUUSHA_JYURYO.Text))
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
                    if (!this.logic.CalcDetaiKingaku(targetRow))
                    {
                        return;
                    }
                    // 数量計算
                    if (!this.logic.CalcSuuryou(targetRow))
                    {
                        return;
                    }
                    // 新規行追加
                    SetEmptyAddNewRow();
                }

                // ReadOnlyの制御がうまくいかないので、ここでReadOnlyを再調整
                if (!this.logic.WarifuriReadOnlyCheck(targetRow))
                {
                    return;
                }
            }
        }
        // No.3875<--

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
                var targetRow = this.gcMultiRow1.CurrentRow;
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
        // No.3822<--

        /// <summary>
        /// 明細欄の空車重量セット時、新規行を追加
        /// </summary>
        public void SetEmptyAddNewRow()
        {
            LogUtility.DebugMethodStart();

            // 明細欄の行が１行以下の場合
            if (this.gcMultiRow1.Rows.Count <= 1 && this.gcMultiRow1.Rows[0].Cells["EMPTY_JYUURYOU"].Value != null)
            {
                // フォーカスを明細備考にセット
                this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, "MEISAI_BIKOU");

                this.gcMultiRow1.BeginEdit(false);

                // 行追加
                this.gcMultiRow1.Rows.Add();

                // NEWROWが上に追加されるのでデータを上の行に移動して
                // 行が下に追加されたように見せる
                for (int i = 0; i < this.gcMultiRow1.Rows[0].Cells.Count; i++)
                {
                    this.gcMultiRow1.Rows[0].Cells[i].Value = this.gcMultiRow1.Rows[1].Cells[i].Value;
                    this.gcMultiRow1.Rows[1].Cells[i].Value = null;
                }

                this.gcMultiRow1.NotifyCurrentCellDirty(false);
                this.gcMultiRow1.EndEdit();

                //フォーカスを明細備考にセット
                this.gcMultiRow1.CurrentCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, "MEISAI_BIKOU");

            }

            // 空車重量のフォーカスアウト時は車輌にフォーカスを当てない
            // （車輌検索ポップアップ後に車輌CDにフォーカスを戻すための対策）
            if (!this.isKuushaJuuryouOnEnter)
            {
                this.SHARYOU_CD.Focus();
            }

            LogUtility.DebugMethodEnd();
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
                    if (row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue != null
                        && string.IsNullOrEmpty(row.Cells[LogicClass.CELL_NAME_KINGAKU].FormattedValue.ToString()))
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
            // この画面（G053）が呼び出された時の引数に受付番号が含まれていた場合、
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
        private void KEIRYOU_NUMBER_TextChanged(object sender, EventArgs e)
        {
            // この画面（G053）が呼び出された時の引数に受付番号が含まれていた場合、
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

        /// <summary>
        /// データロード中フラグ
        /// </summary>
        private bool isLoading;

        /// <summary>
        /// データロード中フラグを取得・設定します
        /// </summary>
        internal bool IsLoading
        {
            get { return this.isLoading; }
            set { this.isLoading = value; }
        }

        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　start
        #region 車輌更新Validating
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool catchErr = false;
            bool retCheck = this.logic.SharyouDateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (!retCheck)
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
            bool catchErr = false;
            bool retCheck = this.logic.UntenshaDateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }

            if (!retCheck)
            {
                e.Cancel = true;
            }
        }
        #endregion

        // 20141015 luning 「出荷入力画面」の休動Checkを追加する　end

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
                var stackcell = this.gcMultiRow1.Rows[0].Cells[LogicClass.CELL_NAME_STAK_JYUURYOU];
                stackcell.GcMultiRow.Focus();
                stackcell.Selected = true;
            }
            else
            {
                foreach (var row in this.gcMultiRow1.Rows)
                {
                    if (row == null) continue;
                    if (row.IsNewRow) continue;

                    var hinmeicell = row.Cells[LogicClass.CELL_NAME_HINMEI_CD];
                    var suuryoucell = row.Cells[LogicClass.CELL_NAME_SUURYOU];
                    var unitcell = row.Cells[LogicClass.CELL_NAME_UNIT_CD];
                    var kingakucell = row.Cells[LogicClass.CELL_NAME_KINGAKU];
                    this.gcMultiRow1.Rows[0].Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Selected = false;

                    if (hinmeicell.Value == null || string.IsNullOrEmpty(hinmeicell.Value.ToString()))
                    {
                        hinmeicell.Style.BackColor = Constans.ERROR_COLOR;
                        hinmeicell.GcMultiRow.Focus();
                        hinmeicell.Selected = true;
                        return;
                    }

                    if (suuryoucell.Value == null || string.IsNullOrEmpty(suuryoucell.Value.ToString()))
                    {
                        suuryoucell.Style.BackColor = Constans.ERROR_COLOR;
                        suuryoucell.GcMultiRow.Focus();
                        suuryoucell.Selected = true;
                        return;
                    }

                    if (unitcell.Value == null || string.IsNullOrEmpty(unitcell.Value.ToString()))
                    {
                        unitcell.Style.BackColor = Constans.ERROR_COLOR;
                        unitcell.GcMultiRow.Focus();
                        unitcell.Selected = true;
                        return;
                    }

                    if (kingakucell.Value == null || string.IsNullOrEmpty(kingakucell.Value.ToString()))
                    {
                        kingakucell.Style.BackColor = Constans.ERROR_COLOR;
                        kingakucell.GcMultiRow.Focus();
                        kingakucell.Selected = true;
                        return;
                    }
                }
                this.gcMultiRow1.Rows[0].Cells[LogicClass.CELL_NAME_STAK_JYUURYOU].Selected = false;
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



        // 20151021 katen #13337 品名手入力に関する機能修正 start
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
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        #region POPUP_BEF
        /// <summary>
        /// 取引先POPUP_BEF
        /// </summary>
        public void TorihikisakiPopupBefore()
        {
            this.logic.TorihikisakiCdSet();
        }

        /// <summary>
        /// 業者POPUP_BEF
        /// </summary>
        public void GyoushaPopupBefore()
        {
            this.logic.GyousyaCdSet();
        }

        /// <summary>
        /// 現場POPUP_BEF
        /// </summary>
        public void GenbaPopupBefore()
        {
            this.logic.GyousyaCdSet();
            this.logic.GenbaCdSet();
        }

        /// <summary>
        /// 運搬業者POPUP_BEF
        /// </summary>
        public void UpanGyoushaPopupBefore()
        {
            this.logic.UnpanGyoushaCdSet();
        }

        /// <summary>
        /// 荷済業者POPUP_BEF
        /// </summary>
        public void SetNizumiGyoushaPopupBef()
        {
            this.beforNizumiGyousha = this.NIZUMI_GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 荷積現場POPUP_BEF
        /// </summary>
        public void NizumiGyoushaPopupBefore()
        {
            this.logic.NizumiGyoushaCdSet();
            this.logic.NizumiGenbaCdSet();
        }
        #endregion POPUP_BEF

        /// <summary>
        /// 荷済業者POPUP_AFT
        /// </summary>
        public void SetNizumiGyoushaPopupAft()
        {
            this.SetNizumiGyousha();
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
        /// 荷済業者設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_NIZUMI_GYOUSHA_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result != DialogResult.OK && result != DialogResult.Yes)
            {
                return;
            }
            // 荷済業者チェック呼び出し
            this.SetNizumiGyoushaPopupAft();
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
            this.MoveToNiZumiGenbaCd();
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
    }
}
