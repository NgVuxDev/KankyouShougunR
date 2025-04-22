// $Id: UIForm.cs 56005 2015-07-17 06:49:19Z sanbongi $

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.ContenaShitei.DTO;
using Shougun.Core.Common.ContenaShitei.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using r_framework.Dto;
using Shougun.Core.ExternalConnection.CommunicateLib;
using Shougun.Core.ExternalConnection.CommunicateLib.Utility;
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;
using System.Web.Script.Serialization;
using Shougun.Core.SalesPayment.TankaRirekiIchiran;

namespace Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku
{
    /// <summary>
    /// 受付（収集）入力画面
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 受付番号
        /// </summary>
        public string UketsukeNumber { get; set; }

        /// <summary>
        /// SEQ
        /// このパラメータが０以外だとDeleteFlgを無視して表示する
        /// </summary>
        public string SEQ = "0";

        // No.2840-->
        /// <summary>
        /// 配車割当からの車両情報
        /// </summary>
        public string[] insHaisyaList { get; set; }
        // No.2840<--

        /// <summary>
        /// コントロールFocus時値格納
        /// </summary>
        public Dictionary<string, string> dicControl = new Dictionary<string, string>();

        /// <summary>
        /// フォーカス設定コントロール格納
        /// </summary>
        private Control focusControl;

        /// <summary>
        /// 前回値チェック用変数(Detial用)
        /// </summary>
        internal Dictionary<string, string> beforeValuesForDetail = new Dictionary<string, string>();

        /// <summary>
        /// 伝票発行ポップアップがキャンセルされたかどうか判断するためのフラグ
        /// </summary>
        internal bool bCancelDenpyoPopup = false;

        /// <summary>
        /// キーイベントを保持用
        /// </summary>
        internal KeyEventArgs Key;

        /// <summary>
        /// フレームワークのフォーカス処理をするかしないか判断するフラグ
        /// </summary>
        internal bool isNotMoveFocusFW = false;

        /// <summary>
        /// 諸口区分の前回値を保持する
        /// </summary>
        internal bool oldShokuchiKbn;

        /// <summary>
        /// ポップアップからの入力か判断するフラグ
        /// </summary>
        internal bool popupFlg = false;

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
        /// 明細のReadOnly設定時にイベントの多重ループを阻止するためのフラグ
        /// </summary>
        private bool isSetDetailReadOnly = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 画面表示時配車種類CDを保存する
        /// </summary>
        internal string dispHaishaShruiCd = string.Empty;

        internal string transactionId;

        /// <summary>
        /// [2]ｼｮｰﾄﾒｯｾｰｼﾞ押下フラグ
        /// </summary>
        private bool smsFlg = false;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報
        /// </summary>
        string[] paramList;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト
        /// </summary>
        List<int> smsReceiverList;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">モード(WINDOW_TYPE)</param>
        /// <param name="UketsukeNumber">受付番号</param>
        /// <param name="slist">車両情報</param>
        public UIForm(WINDOW_TYPE windowType, String UketsukeNumber, string SEQ, string[] slist = null)
            : base(WINDOW_ID.UKETSUKE_SHUSHU, windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, UketsukeNumber, SEQ, slist);

                this.InitializeComponent();
                // 明細の行のヘッダを表示
                //this.dgvDetail.RowHeadersVisible = true;

                //時間コンボボックスのItemsをセット
                this.comboboxInit();

                this.UketsukeNumber = UketsukeNumber;

                if (string.IsNullOrEmpty(SEQ))
                {
                    SEQ = "0";
                }
                this.SEQ = SEQ;

                this.insHaisyaList = slist;   // No.2840

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowType, UketsukeNumber);
            }
        }


        /// <summary>
        /// データ移動用モード用のコンストラクタ
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="torihikisakiCd"></param>
        /// <param name="gyousyaCd"></param>
        /// <param name="genbaCd"></param>
        public UIForm(WINDOW_TYPE windowType, string torihikisakiCd, string gyousyaCd, string genbaCd)
            : this(windowType, string.Empty, null)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, torihikisakiCd, gyousyaCd, genbaCd);

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
                LogUtility.DebugMethodEnd(windowType, torihikisakiCd, gyousyaCd, genbaCd);
            }
        }

        /// <summary>
        /// 時間コンボボックスのItemsをセット
        /// </summary>
        private void comboboxInit()
        {
            this.UKETSUKE_DATE_HOUR.SetItems();
            this.UKETSUKE_DATE_MINUTE.SetItems(1);

            this.GENCHAKU_TIME_HOUR.SetItems();
            this.GENCHAKU_TIME_MINUTE.SetItems(1);

            this.SAGYOU_TIME_HOUR.SetItems();
            this.SAGYOU_TIME_MINUTE.SetItems(1);

            this.SAGYOU_TIME_BEGIN_HOUR.SetItems();
            this.SAGYOU_TIME_BEGIN_MINUTE.SetItems(1);

            this.SAGYOU_TIME_END_HOUR.SetItems();
            this.SAGYOU_TIME_END_MINUTE.SetItems(1);

        }
        #endregion

        #region 画面ロード処理
        /// <summary>
        /// 画面ロード処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                
                if (string.IsNullOrEmpty(this.transactionId))
                {
                    this.transactionId = Guid.NewGuid().ToString();
                }

                if (!this.logic.WindowInit())
                {
                    return;
                }

				//VUNGUYEN 20150717 #11589 検索での呼び出しに時間がかかる START
                this.logic.getDefaultList();
				//VUNGUYEN 20150717 #11589 検索での呼び出しに時間がかかる END

                // Anchorの設定はOnLoadで行う
                if (this.dgvDetail != null)
                {
                    // デザイナの設定が初期化されてしまうので、ここで設定
                    this.dgvDetail.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                //CongBinh 20210713 #152801 S
                if (this.rirekeIchiran != null)
                {
                    this.rirekeIchiran.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                this.ListSagyouBi = new List<string>();
                this.OutDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //CongBinh 20210713 #152801 E
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
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            //Communicate InxsSubApplication - Compare sagyou date with request confirm date 
            this.logic.CompareSagyouDateAndConfirmDate();

            base.OnShown(e);
        }

        #endregion

        #region コンストラクタで渡された受付番号のデータ存在するかチェック
        /// <summary>
        /// コンストラクタで渡された受付番号のデータ存在するかチェック
        /// </summary>
        /// <returns>true:存在する, false:存在しない</returns>
        public bool IsExistData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                return this.logic.IsExistData(this.UketsukeNumber);
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

        #region UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// <summary>
        /// UIForm, HeaderForm, FooterFormのすべてのコントロールを返す
        /// </summary>
        /// <returns></returns>
        internal Control[] GetAllControl()
        {
            try
            {
                LogUtility.DebugMethodStart();

                List<Control> allControl = new List<Control>();
                allControl.AddRange(this.allControl);
                allControl.AddRange(controlUtil.GetAllControls(this.logic.headerForm));
                allControl.AddRange(controlUtil.GetAllControls(this.logic.parentForm));

                return allControl.ToArray();
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

        #region イベント処理

        #region [F2]新規
        /// <summary>
        /// [F2]新規
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeNewWindow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 新規モードに変更
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    //base.OnLoad(e);

                    // 受付番号クリア
                    this.UketsukeNumber = string.Empty;

                    //CongBinh 202107013 #152801 S
                    this.ListSagyouBi = new List<string>();
                    this.OutDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    this.SAGYOU_DATE_TMP.Visible = false;
                    //CongBinh 202107013 #152801 E

                    // SEQクリア
                    this.SEQ = "0";

                    // 表示初期化
                    bool catchErr = true;
                    this.logic.DisplayInit(out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    // フォーカス設定
                    //this.HAISHA_JOKYO_CD.Focus();
                    this.GYOUSHA_CD.Focus();
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

        #region [F3]修正
        /// <summary>
        /// [F3]修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ChangeUpdateWindow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 修正モードに変更
                    this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                }
                else if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    // 参照モードに変更
                    this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
                    return;
                }

                //base.OnLoad(e);

                //// 受付番号クリア
                //this.UketsukeNumber = string.Empty;

                // 表示初期化
                bool catchErr = true;
                this.logic.DisplayInit(out catchErr);
                if (!catchErr)
                {
                    return;
                }

                // フォーカス設定
                //this.HAISHA_JOKYO_CD.Focus();
                this.GYOUSHA_CD.Focus();
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

        #region [F7]一覧
        /// <summary>
        /// [F7]一覧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowIchiran(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool retResult;
                // 受付一覧を表示
                retResult = FormManager.OpenFormWithAuth("G021", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, this.logic.headerForm.KYOTEN_CD.Text, 1);

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

        #region [F8]指示書
        /// <summary>
        /// [F8]指示書
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Print(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // TODO:修正モードで変更ありの場合、処理終了(検討中)

                // 指示書印刷フラグを更新用Entity作成
                if (!this.logic.CreateEntityForUpdPrintFlg())
                {
                    return;
                }

                // 印刷を行う
                if (this.logic.Print())
                {
                    // 正常完了メッセージ通知
                    this.logic.msgLogic.MessageBoxShow("I001", "印刷");
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

        #region [F9]登録
        /// <summary>
        /// [F9]登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 初期化
                base.RegistErrorFlag = false;
                bool catchErr = true;
                bool MobieRegistCheck = false;
                this.logic.Renkei_UketsukeNumber = string.Empty;

                if (!this.logic.RenkeiCheck(this.UKETSUKE_NUMBER.Text))
                {
                    return;
                }
                //CongBinh 20210713 #152801 S
                if (this.ListSagyouBi == null || (this.ListSagyouBi.Count < 2))
                {
                    /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
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
                    else if (!this.logic.HojoinDateCheck())
                    {
                        this.HOJOIN_CD.Focus();
                        return;
                    }
                    else if (!this.logic.HannyuusakiDateCheck())
                    {
                        this.NIOROSHI_GENBA_CD.Focus();
                        return;
                    }
                    /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
                }
                //CongBinh 20210713 #152801 E

                // 取引先と拠点の関係をチェック
                if (!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text) &&
                !string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    if (false == this.logic.CheckTorihikisakiKyoten(this.logic.headerForm.KYOTEN_CD.Text, this.TORIHIKISAKI_CD.Text))
                    {
                        this.TORIHIKISAKI_CD.Focus();
                        this.TORIHIKISAKI_NAME.Text = string.Empty;
                        return;
                    }
                }

                if (!this.CheckSharyouAndUntensha(false))
                {
                    return;
                }

                this.logic.SetRegistCheckMethod();

                var haisyaJoukyou = -1;
                if (!string.IsNullOrEmpty(this.HAISHA_JOKYO_CD.Text))
                {
                    haisyaJoukyou = short.Parse(this.HAISHA_JOKYO_CD.Text);
                }

                bool isKotaiKanri = false;
                if (this.logic.sysInfoEntity != null
                    && !this.logic.sysInfoEntity.CONTENA_KANRI_HOUHOU.IsNull
                    && (int)this.logic.sysInfoEntity.CONTENA_KANRI_HOUHOU == CommonConst.CONTENA_KANRI_HOUHOU_KOTAI)
                {
                    isKotaiKanri = true;
                }

                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // 必須チェックの項目を設定(押されたボタンにより動的に変わる)
                        var autoCheckLogic = new AutoRegistCheckLogic(this.GetAllControl(), this.GetAllControl());
                        base.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

                        if (!base.RegistErrorFlag &&
                            this.logic.listContenaParam.Count > 0 && string.IsNullOrEmpty(GENBA_CD.Text))
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E180");
                            base.RegistErrorFlag = true;
                            GENBA_CD.IsInputErrorOccured = true;
                        }

                        var contenaShiteiUtil = new ContenaShiteiUtility();
                        long sysId = -1;
                        int seq = -1;
                        string beforeSagyouDate = null;
                        string befoerHaisyaJoukyou = string.Empty;
                        if (WINDOW_TYPE.UPDATE_WINDOW_FLAG == this.WindowType)
                        {
                            sysId = (long)this.logic.dtUketsukeEntry.Rows[0]["SYSTEM_ID"];
                            seq = (int)this.logic.dtUketsukeEntry.Rows[0]["SEQ"];
                            beforeSagyouDate = this.logic.dtUketsukeEntry.Rows[0]["SAGYOU_DATE"].ToString();
                            befoerHaisyaJoukyou =  this.logic.dtUketsukeEntry.Rows[0]["HAISHA_JOKYO_CD"].ToString();
                        }

                        /**
                         * 設置、引揚可能チェック
                         */
                         List<UtilityDto> denpyouInfoList = new List<UtilityDto>();
                         UtilityDto uketsukeInfo = new UtilityDto();
                         uketsukeInfo.SysId = sysId;
                         uketsukeInfo.Seq = seq;
                         uketsukeInfo.DenshuKbn = (int)DENSHU_KBN.UKETSUKE;
                         denpyouInfoList.Add(uketsukeInfo);

                        if (ConstClass.HAISHA_JOKYO_CD_KEIJO.ToString().Equals(this.HAISHA_JOKYO_CD.Text)
                                || ConstClass.HAISHA_JOKYO_CD_NASHI.ToString().Equals(this.HAISHA_JOKYO_CD.Text))
                            {
                            if (!base.RegistErrorFlag
                                && isKotaiKanri
                                && !contenaShiteiUtil.CheckContenaInfo(this.logic.listContenaParam, this.GYOUSHA_CD.Text, this.GENBA_CD.Text, denpyouInfoList))
                            {
                                base.RegistErrorFlag = true;
                            }
                        }

                        // 金額チェック
                        if (!base.RegistErrorFlag &&
                            !this.logic.CheckDetailKingaku())
                        {
                            base.RegistErrorFlag = true;
                        }

                        //CongBinh 20210713 #152801 S
                        if (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG &&
                            !base.RegistErrorFlag &&
                            !this.logic.CheckListSagyouBi())
                        {
                            base.RegistErrorFlag = true;
                        }
                        //CongBinh 20210713 #152801 E

                        // エラーの場合
                        if (base.RegistErrorFlag)
                        {
                            // 必須チェックエラーフォーカス処理
                            this.logic.SetErrorFocus();
                            // 処理中止
                            return;
                        }

                        // 運転者補助員チェック
                        if (!this.logic.UnpanGyoushaHojoinCheck())
                        {
                            // 処理中止
                            return;
                        }

                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        break;

                    default:
                        break;
                }

                bool ret = true;

                // 登録処理
                switch (this.WindowType)
                {
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:

                        // 20141031 Houkakou 委託契約チェック start
                        ret = this.logic.CheckItakukeiyaku();
                        if (!ret)
                        {
                            return;
                        }
                        // 20141031 Houkakou 委託契約チェック end

                        // データ登録（異常）
                        if (!this.logic.RegistData())
                        {
                            return;
                        }

                        //CongBinh 20210713 #152801 S
                        this.ListSagyouBi = new List<string>();
                        this.OutDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        this.SAGYOU_DATE.Enabled = true;
                        this.SAGYOU_DATE_TMP.Visible = false;
                        //CongBinh 20210713 #152801 E

                        // 印刷フラグがtrueの場合
                        bool isOk = this.logic.IsPrint(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (isOk)
                        {
                            // 印刷を行う
                            catchErr = this.logic.Print();
                            if (!catchErr)
                            {
                                return;
                            }
                        }

                        // 正常完了メッセージ通知
                        this.logic.msgLogic.MessageBoxShow("I001", "登録");

                        if (this.logic.MobileRegistCheck())
                        {
                            //モバイルデータ登録チェック→モバイルデータ登録
                            MobieRegistCheck = this.logic.MobileRegist();
                            if (MobieRegistCheck)
                            {
                                this.logic.msgLogic.MessageBoxShow("I001", "連携");
                            }
                            else
                            {
                                this.logic.msgLogic.MessageBoxShowWarn("モバイル将軍への連携処理に失敗しました。\r\n再度実行してください。");
                            }
                        }

                        // [2]ｼｮｰﾄﾒｯｾｰｼﾞで呼び出された場合、クリア前に情報取得
                        if (smsFlg)
                        {
                            // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
                            paramList = this.logic.SmsParamListSetting();

                            smsReceiverList = this.logic.SmsReceiverListSetting();
                        }

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // ベースフォームロード
                            //base.OnLoad(e);
                            // 画面初期化
                            this.logic.DisplayInit(out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }

                            // フォーカス設定
                            this.HAISHA_JOKYO_CD.Focus();
                        }
                        else
                        {
                            // 新規権限が無い場合は画面を閉じる
                            this.FormClose(sender, e);
                        }
                        break;

                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        // コンテナ修正時のインフォメーション
                        if ((ConstClass.HAISHA_JOKYO_CD_KEIJO.ToString().Equals(this.HAISHA_JOKYO_CD.Text)
                                || ConstClass.HAISHA_JOKYO_CD_NASHI.ToString().Equals(this.HAISHA_JOKYO_CD.Text))
                            && isKotaiKanri
                            && this.logic.listContenaParam != null
                            && this.logic.listContenaParam.Count > 0)
                        {
                            bool isExistingContenaData = false;
                            foreach (var tempContena in this.logic.listContenaParam)
                            {
                                if (!string.IsNullOrEmpty(tempContena.CONTENA_SHURUI_CD)
                                    && !string.IsNullOrEmpty(tempContena.CONTENA_CD))
                                {
                                    isExistingContenaData = true;
                                }
                            }

                            if (isExistingContenaData)
                            {
                                this.logic.msgLogic.MessageBoxShow("I018");
                            }
                        }

                        // 20141031 Houkakou 委託契約チェック start
                        ret = this.logic.CheckItakukeiyaku();
                        if (!ret)
                        {
                            return;
                        }
                        // 20141031 Houkakou 委託契約チェック end

                        // データ更新（異常）
                        if (!this.logic.UpdateData())
                        {
                            return;
                        }

                        // 印刷フラグがtrueの場合
                        bool isOk2 = this.logic.IsPrint(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (isOk2)
                        {
                            // 指示書印刷
                            catchErr = this.logic.Print();
                            if (!catchErr)
                            {
                                return;
                            }
                        }
                        // [2]ｼｮｰﾄﾒｯｾｰｼﾞで呼び出された場合、クリア前に情報取得
                        if (smsFlg)
                        {
                            // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
                            paramList = this.logic.SmsParamListSetting();

                            smsReceiverList = this.logic.SmsReceiverListSetting();
                        }

                        // 正常完了メッセージ通知
                        this.logic.msgLogic.MessageBoxShow("I001", "更新");

                        if (this.logic.MobileRegistCheck())
                        {
                            //モバイルデータ登録チェック→モバイルデータ登録
                            MobieRegistCheck = this.logic.MobileRegist();
                            if (MobieRegistCheck)
                            {
                                this.logic.msgLogic.MessageBoxShow("I001", "連携");
                            }
                            else
                            {
                                this.logic.msgLogic.MessageBoxShowWarn("モバイル将軍への連携処理に失敗しました。\r\n再度実行してください。");
                            }
                        }

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 【モードなし】モードに変更
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

                            // ベースフォームロード
                            //base.OnLoad(e);

                            //Communicate InxsSubApplication Start
                            this.CloseSubAppForm();
                            //Communicate InxsSubApplication End

                            //受付番号クリア
                            this.UketsukeNumber = string.Empty;

                            // 画面初期化
                            this.logic.DisplayInit(out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }
                        }
                        else
                        {
                            // 新規権限が無い場合は画面を閉じる
                            this.FormClose(sender, e);
                        }
                        break;

                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        // 確認メッセージ
                        var result = this.logic.msgLogic.MessageBoxShow("C026");
                        if (result != DialogResult.Yes)
                        {
                            return;
                        }

                        // データ削除(異常)
                        if (!this.logic.LogicalDeleteData())
                        {
                            // 処理終了
                            return;
                        }

                        // 正常完了メッセージ通知
                        this.logic.msgLogic.MessageBoxShow("I001", "削除");

                        // 権限チェック
                        if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.NEW_WINDOW_FLAG, false))
                        {
                            // 【新規】モードに変更
                            this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                            // ベースフォームロード
                            //base.OnLoad(e);

                            //Communicate InxsSubApplication Start
                            this.CloseSubAppForm();
                            //Communicate InxsSubApplication End

                            // 受付番号クリア
                            this.UketsukeNumber = string.Empty;
                            // 画面初期化
                            this.logic.DisplayInit(out catchErr);
                            if (!catchErr)
                            {
                                return;
                            }

                            // フォーカス設定
                            this.HAISHA_JOKYO_CD.Focus();
                        }
                        else
                        {
                            // 新規権限が無い場合は画面を閉じる
                            this.FormClose(sender, e);
                        }
                        break;

                    default:
                        break;
                }

                // フィールドクリア
                if (!this.logic.ClearFields())
                {
                    return;
                }

                this.GYOUSHA_CD.Focus();
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

        #region [F10]行挿入
        /// <summary>
        /// [F10]行挿入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AddRow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 行未選択の場合
                //if (this.dgvDetail.SelectedRows.Count.Equals(0))
                //{
                //    // メッセージを表示
                //    this.logic.msgLogic.MessageBoxShow("E029", "受付明細", "一覧");
                //    // 処理中止
                //    return;
                //}
                if (this.dgvDetail.CurrentRow == null)
                {
                    // メッセージを表示
                    this.logic.msgLogic.MessageBoxShow("E029", "受付明細", "一覧");
                    // 処理中止
                    return;
                }

                this.logic.AddNewRow();

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

        #region [F11]行削除
        /// <summary>
        /// [F11]行削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void RemoveRow(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //// 行未選択の場合
                //if (this.dgvDetail.SelectedRows.Count.Equals(0))
                //{
                //    // メッセージを表示
                //    this.logic.msgLogic.MessageBoxShow("E029", "受付明細", "一覧");
                //    // 処理中止
                //    return;
                //}
                if (this.dgvDetail.CurrentRow == null)
                {
                    // メッセージを表示
                    this.logic.msgLogic.MessageBoxShow("E029", "受付明細", "一覧");
                    // 処理中止
                    return;
                }

                this.logic.RemoveSelectedRow();

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

        #region [F12]閉じる
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CloseForm();

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

        #region [subF1]地図表示
        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MapOpen(object sender, EventArgs e)
        {
            try
            {
                // 業者のチェック
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    this.logic.msgLogic.MessageBoxShowError("業者を指定してください。");
                    return;
                }

                // 緯度経度のチェック
                if (string.IsNullOrEmpty(this.GENBA_CD.Text))
                {
                    // 現場がない場合は業者の緯度経度チェック
                    if (!this.logic.gyoushaLocationChk())
                    {
                        this.logic.msgLogic.MessageBoxShowError("業者の緯度/経度が入力されていません。");
                        return;
                    }
                }
                else
                {
                    // 現場がある場合は現場の緯度経度チェック
                    if (!this.logic.genbaLocationChk())
                    {
                        this.logic.msgLogic.MessageBoxShowError("現場の緯度/経度が入力されていません。");
                        return;
                    }
                }

                if (this.logic.msgLogic.MessageBoxShowConfirm("地図を表示しますか？") == DialogResult.No)
                {
                    return;
                }

                MapboxGLJSLogic gljsLogic = new MapboxGLJSLogic();

                // 地図に渡すDTO作成
                List<mapDtoList> dtos = new List<mapDtoList>();
                dtos = this.logic.createMapboxDto();
                if (dtos == null)
                {
                    this.logic.msgLogic.MessageBoxShowError("表示する対象がありません。");
                    return;
                }

                // 地図表示
                gljsLogic.mapbox_HTML_Open(dtos, WINDOW_ID.T_UKETSUKE_SHUSHU);
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

        #region [subF2]ｼｮｰﾄﾒｯｾｰｼﾞ
        /// <summary>
        /// [2]ｼｮｰﾄﾒｯｾｰｼﾞ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SmsNyuuryoku(object sender, EventArgs e)
        {
            try
            {
                // 作業日の必須チェック
                if (string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
                {
                    this.SAGYOU_DATE.BackColor = Constans.ERROR_COLOR;
                    this.logic.msgLogic.MessageBoxShow("E001", "作業日");
                    this.SAGYOU_DATE.Focus();
                    return;
                }

                // 業者のチェック
                if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
                {
                    this.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                    this.logic.msgLogic.MessageBoxShowError("業者を指定してください。");
                    this.GYOUSHA_CD.Focus();
                    return;
                }

                // 現場のチェック
                if (string.IsNullOrEmpty(this.GENBA_CD.Text))
                {
                    this.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                    this.logic.msgLogic.MessageBoxShowError("現場を指定してください。");
                    this.GENBA_CD.Focus();
                    return;
                }

                // ｼｮｰﾄﾒｯｾｰｼﾞ受信者チェック
                var dao = this.logic.smsReceiverLinkGenbaDao.CheckDataForSmsNyuuryoku(this.GYOUSHA_CD.Text, this.GENBA_CD.Text);
                if(dao == null)
                {
                    this.logic.msgLogic.MessageBoxShowError("現場入力（マスタ）に受信者情報が登録されていません。\r\n受信者情報を登録してください。");
                    return;
                }
                else
                {
                    // チェック完了後、[2]押下フラグをtrueに変更
                    smsFlg = true;

                    // 登録処理
                    switch (this.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            // 確認メッセージ表示
                            if (this.logic.msgLogic.MessageBoxShowConfirm("受付伝票を登録し、ショートメッセージ入力画面を表示します") == DialogResult.No)
                            {
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                    // データ登録（[F9]登録処理）
                    this.Regist(sender, e);

                    #region [F9]登録時のチェック処理
                
                    // データ登録時にエラーが発生した場合は処理中断
                    if (base.RegistErrorFlag)
                    {
                        // 必須チェックエラーフォーカス処理
                        this.logic.SetErrorFocus();
                        return;
                    }

                    // 運転者補助員チェック
                    if (!this.logic.UnpanGyoushaHojoinCheck())
                    {
                        // 処理中止
                        return;
                    }

                    #endregion
                }

                // 不具合等でｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報が存在しない場合は、エラー表示
                if (smsReceiverList == null || paramList == null)
                {
                    this.logic.msgLogic.MessageBoxShowError("ｼｮｰﾄﾒｯｾｰｼﾞ入力への連携処理に失敗しました。");
                    return;
                }

                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面を起動
                FormManager.OpenForm("G767", smsReceiverList, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_ID.T_UKETSUKE_SHUSHU, paramList);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                // [2]押下フラグ、ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報の初期化
                smsFlg = false;
                smsReceiverList = null;
                paramList = null;
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region プロセスボタン押下処理
        /// <summary>
        /// [1]コンテナ指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //// コンテナ指定を表示
                //////TODO: 戻り値のやり方確認必要
                ////FormManager.OpenForm("G184", this.logic.arrInsContenaReserve);
                //MessageBox.Show("コンテナ指定を表示");

                // 編集不可
                string mode = "2";

                switch (this.WindowType)
                {
                    // 新規・修正モードの場合
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        // 編集可
                        mode = "1";
                        break;
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        if (ConstClass.HAISHA_SHURUI_CD_KAKUTEI != this.HAISHA_SHURUI_CD.Text ||
                            ConstClass.HAISHA_SHURUI_CD_KAKUTEI != this.dispHaishaShruiCd)
                        {
                            // 編集可
                            mode = "1";
                        }
                        break;
                }

                string sagyouDate = this.SAGYOU_DATE.Value == null ? null : this.SAGYOU_DATE.Value.ToString();
                var callForm = new Shougun.Core.Common.ContenaShitei.UIForm(mode, "3", this.logic.listContenaParam, sagyouDate, this.GYOUSHA_CD.Text, this.GENBA_CD.Text);
                var callHeader = new Shougun.Core.Common.ContenaShitei.UIHeader();
                var popForm = new BasePopForm(callForm, callHeader);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    // コンテナ指定を表示
                    popForm.ShowDialog();
                    // 戻り値取得
                    var result = callForm.RetCntResList;
                    if (result != null)
                    {
                        this.logic.listContenaParam = result;
                        this.logic.settiDaisu = callForm.SeichiGoukei;
                        this.logic.hikiageDaisu = callForm.HikiageGoukei;
                        // コンテナ操作設定
                        this.logic.SetContenaInfo();
                    }

                    popForm.Dispose();
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

        #region フォーカス取得処理
        /// <summary>
        /// フォーカス取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Control_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //CongBinh 20210713 #152801 S
                bool isDisplay = true;
                Type typeTmp = sender.GetType();
                if (typeTmp.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)sender;
                    if (ctrl.Name.Contains(this.UNPAN_GYOUSHA_CD.Name) ||
                        ctrl.Name.Contains(this.SHARYOU_CD.Name) ||
                        ctrl.Name.Contains(this.SHASHU_CD.Name))
                    {
                        isDisplay = false;
                    }
                }
                if (isDisplay)
                {
                    if (!this.rirekeIchiran.Focused)
                    {
                        this.logic.RirekeShow();
                    }
                }
                //CongBinh 20210713 #152801 E


                // 入力で例外が発生した場合はその前回値を保持しない
                //if (!this.logic.isInputError)
                //{
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

                        //CongBinh 20210713 #152801 S
                        if (ctrl.Name.Contains(this.UNPAN_GYOUSHA_CD.Name) ||
                            ctrl.Name.Contains(this.SHARYOU_CD.Name) ||
                            ctrl.Name.Contains(this.SHASHU_CD.Name))
                        {
                            this.logic.RirekeSharyouShow();
                        }
                        //CongBinh 20210713 #152801 E

                        // イベント削除
                        //ctrl.Enter -= this.Control_Enter;
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
                        //ctrl.Enter -= this.Control_Enter;
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
                        //control.Enter -= this.Control_Enter;
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
                //}
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
        /// フォーカス取得処理(ポップアップの後処理用)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ControlEnterForPopUpAfter(Control control)
        {
            try
            {
                LogUtility.DebugMethodStart(control);

                Type type = control.GetType();
                if (type.Name == "CustomAlphaNumTextBox")
                {
                    CustomAlphaNumTextBox ctrl = (CustomAlphaNumTextBox)control;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
                else if (type.Name == "CustomNumericTextBox2")
                {
                    CustomNumericTextBox2 ctrl = (CustomNumericTextBox2)control;
                    if (dicControl.ContainsKey(ctrl.Name))
                    {
                        dicControl[ctrl.Name] = ctrl.Text;
                    }
                    else
                    {
                        dicControl.Add(ctrl.Name, ctrl.Text);
                    }
                }
                else if (type.Name == "CustomPopupOpenButton")
                {
                    CustomPopupOpenButton ctrl = (CustomPopupOpenButton)control;
                    // テキスト名を取得
                    String textName = this.logic.GetTextName(ctrl.Name);
                    Control ctl = controlUtil.FindControl(this, textName);

                    if (dicControl.ContainsKey(textName))
                    {
                        dicControl[textName] = ctl.Text;
                    }
                    else
                    {
                        dicControl.Add(textName, ctl.Text);
                    }
                }
                else if (type.Name == "CustomDateTimePicker")
                {
                    CustomDateTimePicker ctrl = (CustomDateTimePicker)control;
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

        #region KeyDownイベントを発生させます
        /// <summary>
        /// KeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.Key = e;
            base.OnKeyDown(e);
        }
        #endregion KeyDownイベントを発生させます

        #region 配車種類　Validatingイベント
        /// <summary>
        /// 配車種類　Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HAISHA_SHURUI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.HAISHA_SHURUI_NAME.Text = "";

                if (!string.IsNullOrEmpty(this.HAISHA_SHURUI_CD.Text))
                {
                    int cd;
                    int.TryParse(this.HAISHA_SHURUI_CD.Text, out cd);

                    switch (cd)
                    {
                        case 1:
                            this.HAISHA_SHURUI_NAME.Text = "通常";
                            break;
                        case 2:
                            this.HAISHA_SHURUI_NAME.Text = "仮押";
                            break;
                        case 3:
                            this.HAISHA_SHURUI_NAME.Text = "確定";
                            break;
                        default:
                            this.HAISHA_SHURUI_CD.IsInputErrorOccured = true;
                            this.logic.msgLogic.MessageBoxShow("E011", "配車種類");
                            e.Cancel = true;
                            return;
                    }

                    this.HAISHA_SHURUI_CD.Text = cd.ToString();
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

        #region 取引先更新後処理

        /// <summary>
        /// 取引先CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 初期化
            this.isNotMoveFocusFW = true;

            var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

            if (!this.SetTorihikisaki())
            {
                return;
            }

            if (!isNotMoveFocusFW)
            {
                base.OnKeyDown(this.Key);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 取引先検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void TorihikisakiBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();
                
                var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

                if (this.logic.isInputError || this.TORIHIKISAKI_CD.Text != before)
                {
                    this.SetTorihikisaki();
                }

                // フォーカスセット
                this.TORIHIKISAKI_CD.Focus();
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
        /// 取引先検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void TorihikisakiBtnPopupAfterExecute(object sender, DialogResult result)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, result);

                if (result != DialogResult.Yes && result != DialogResult.OK)
                    return;

                var before = this.GetBeforeText(this.TORIHIKISAKI_CD.Name);

                if (this.logic.isInputError || this.TORIHIKISAKI_CD.Text != before)
                {
                    this.SetTorihikisaki();
                }

                // フォーカスセット
                this.TORIHIKISAKI_CD.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, result);
            }
        }
        #endregion

        #region 業者更新後処理
        /// <summary>
        /// 業者CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.isInputError = false; //QN_QUAN add 20220415 #162487

            if (!this.SetGyousha())
            {
                return;
            }

            if (!isNotMoveFocusFW)
            {
                base.OnKeyDown(this.Key);
            }
            this.logic.RirekeShow();//CongBinh 20210713 #152801

            // 業者はバリデーション時も前回値をセット
            this.Control_Enter(sender, e);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 業者検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void GyoushaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // フォーカスセット
                this.GYOUSHA_CD.Focus();

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

        #region 現場更新後処理
        /// <summary>
        /// 現場CDのバリデーションが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void GENBA_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.SetGenba())
            {
                return;
            }
            if (!isNotMoveFocusFW)
            {
                base.OnKeyDown(this.Key);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場検索ポップアップから戻ってきたときに処理します
        /// </summary>
        public void GenbaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var gyoushaCd = this.GYOUSHA_CD.Text;
                bool catchErr = true;

                var gyousha = this.logic.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (null != gyousha)
                {
                    this.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    this.GYOSHA_TEL.Text = gyousha.GYOUSHA_TEL;

                    this.logic.SetCtrlReadonly(this.GYOUSHA_NAME, !(bool)gyousha.SHOKUCHI_KBN);
                    this.logic.SetCtrlReadonly(this.GYOSHA_TEL, !(bool)gyousha.SHOKUCHI_KBN);

                    var genba = this.logic.GetGenba(this.GENBA_CD.Text, gyoushaCd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (null != genba)
                    {
                        this.GENBA_ADDRESS1.Text = genba.GENBA_ADDRESS1;
                        this.GENBA_ADDRESS2.Text = genba.GENBA_ADDRESS2;
                        this.GENBA_TEL.Text = genba.GENBA_TEL;
                        this.TANTOSHA_NAME.Text = genba.TANTOUSHA;
                        this.TANTOSHA_TEL.Text = genba.GENBA_KEITAI_TEL;
                        this.logic.SetCtrlReadonly(this.GENBA_ADDRESS1, !(bool)genba.SHOKUCHI_KBN);
                        this.logic.SetCtrlReadonly(this.GENBA_ADDRESS2, !(bool)genba.SHOKUCHI_KBN);
                        this.logic.SetCtrlReadonly(this.GENBA_TEL, !(bool)genba.SHOKUCHI_KBN);
                        this.logic.SetCtrlReadonly(this.TANTOSHA_NAME, !(bool)genba.SHOKUCHI_KBN);
                        this.logic.SetCtrlReadonly(this.TANTOSHA_TEL, !(bool)genba.SHOKUCHI_KBN);
                    }
                }

                this.GENBA_CD.Focus();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("GenbaBtnPopupMethod", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 運搬業者更新後処理
        /// <summary>
        /// 運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void UNPAN_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //CongBinh 20210713 #152801 S
                if (!this.rirekeIchiran.Focused)
                {
                    this.logic.RirekeShow();
                }
                //CongBinh 20210713 #152801 E

                // チェックNGの場合
                var before = this.GetBeforeText(this.UNPAN_GYOUSHA_CD.Name);
                if (this.UNPAN_GYOUSHA_CD.Text == before && !this.logic.isInputError)
                {
                    return;
                }

                this.logic.isInputError = false;
                if (!this.logic.CheckUnpanGyoushaCd())
                {
                    // フォーカス設定
                    this.UNPAN_GYOUSHA_CD.Focus();
                    return;
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                if (!this.logic.MoveToNextControlForShokuchikbnCheck(this.UNPAN_GYOUSHA_CD))
                {
                    return;
                }

                // 変更なしの場合
                if (this.dicControl.ContainsKey("UNPAN_GYOUSHA_CD") && this.dicControl["UNPAN_GYOUSHA_CD"].Equals(this.UNPAN_GYOUSHA_CD.Text))
                {
                    return;
                }
                else
                {
                    // 変更有の場合は車輌をクリアする。
                    this.SHARYOU_CD.Text = string.Empty;
                    this.SHARYOU_NAME.Text = string.Empty;
                    this.logic.sharyouCd = string.Empty;
                }

                // 全ての明細と合計の計算
                this.logic.CalcAllDetailAndTotal();

                // イベント追加
                //this.UNPAN_GYOUSHA_CD.Enter += this.Control_Enter;

                //CongBinh 20210713 #152801 S
                if (this.rirekeIchiran.Focused)
                {
                    this.logic.RirekeSharyouShow();
                }
                //CongBinh 20210713 #152801 E

                this.ChangeHaishaJokyo();
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

        private string beforeUpnGyousha = "";
        private string beforeUpnGyoushaName = "";
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ前の処理
        /// </summary>
        public void UnpanGyoushaPopupBeforeMethod()
        {
            this.beforeUpnGyousha = this.UNPAN_GYOUSHA_CD.Text;
            this.beforeUpnGyoushaName = this.UNPAN_GYOUSHA_NAME.Text;
            this.UNPAN_GYOUSHA_CD.Text = "";
        }

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void UnpanGyoushaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text) || this.beforeUpnGyousha == this.UNPAN_GYOUSHA_CD.Text)
                {
                    this.UNPAN_GYOUSHA_CD.Text = this.beforeUpnGyousha;
                    this.UNPAN_GYOUSHA_NAME.Text = this.beforeUpnGyoushaName;
                    return;
                }

                this.UNPAN_GYOUSHA_CD_OnValidated(null, null);

                // フォーカスセット
                this.UNPAN_GYOUSHA_CD.Focus();

                var before = this.GetBeforeText(this.UNPAN_GYOUSHA_CD.Name);
                if (this.UNPAN_GYOUSHA_CD.Text != before || this.logic.isInputError)
                {
                    // Popupから戻ってきたとき値が変わっていれば前回値を保存
                    this.ControlEnterForPopUpAfter(this.UNPAN_GYOUSHA_CD);
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

        #region 荷降業者更新後処理
        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GYOUSHA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 変更なしの場合
                var before = this.GetBeforeText(this.NIOROSHI_GYOUSHA_CD.Name);
                if (this.NIOROSHI_GYOUSHA_CD.Text == before && !this.logic.isInputError)
                {
                    return;
                }

                this.logic.isInputError = false;
                // チェックNGの場合
                if (!this.logic.CheckNioroshiGyoushaCd())
                {
                    // フォーカス設定
                    this.NIOROSHI_GYOUSHA_CD.Focus();
                    return;
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                if (!this.logic.MoveToNextControlForShokuchikbnCheck(this.NIOROSHI_GYOUSHA_CD))
                {
                    return;
                }

                // 変更なしの場合
                if (this.dicControl.ContainsKey("NIOROSHI_GYOUSHA_CD") && this.dicControl["NIOROSHI_GYOUSHA_CD"].Equals(this.NIOROSHI_GYOUSHA_CD.Text))
                {
                    return;
                }

                // [荷降現場CD]、[荷降現場名]を初期値に設定する。
                this.NIOROSHI_GENBA_CD.Text = String.Empty;
                this.NIOROSHI_GENBA_NAME.Text = String.Empty;
                // Readonly設定
                if (!this.NIOROSHI_GENBA_NAME.ReadOnly)
                {
                    this.logic.SetCtrlReadonly(this.NIOROSHI_GENBA_NAME, true);
                }

                // 全ての明細と合計の計算
                this.logic.CalcAllDetailAndTotal();

                // イベント追加
                //this.NIOROSHI_GYOUSHA_CD.Enter += this.Control_Enter;

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

        private string beforeNioroshiGyousha = "";
        private string beforeNioroshiGyoushaName = "";
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ前の処理
        /// </summary>
        public void NioroshiGyoushaBtnPopupBeforeMethod()
        {
            beforeNioroshiGyousha = this.NIOROSHI_GYOUSHA_CD.Text;
            beforeNioroshiGyoushaName = this.NIOROSHI_GYOUSHA_NAME.Text;
            this.NIOROSHI_GYOUSHA_CD.Text = "";
        }

        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void NioroshiGyoushaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                if (!this.dicControl["NIOROSHI_GYOUSHA_CD"].Equals(this.NIOROSHI_GYOUSHA_CD.Text))
                {
                    this.NIOROSHI_GENBA_CD.Text = string.Empty;
                    this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    this.NIOROSHI_GENBA_NAME.ReadOnly = true;
                }
                // 20150918 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text) || beforeNioroshiGyousha == this.NIOROSHI_GYOUSHA_CD.Text)
                {
                    this.NIOROSHI_GYOUSHA_CD.Text = beforeNioroshiGyousha;
                    this.NIOROSHI_GYOUSHA_NAME.Text = beforeNioroshiGyoushaName;
                    return;
                }
                bool catchErr = true;
                var gyoushaEntity = this.logic.GetGyousha(this.NIOROSHI_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (gyoushaEntity != null)
                {
                    if (gyoushaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }

                    // 全ての明細と合計の計算
                    this.logic.CalcAllDetailAndTotal();
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end

                this.NIOROSHI_GYOUSHA_CD.Focus();

                var before = this.GetBeforeText(this.NIOROSHI_GYOUSHA_CD.Name);
                if (this.NIOROSHI_GYOUSHA_CD.Text != before || this.logic.isInputError)
                {
                    // Popupから戻ってきたとき値が変わっていれば前回値を保存
                    this.ControlEnterForPopUpAfter(this.NIOROSHI_GYOUSHA_CD);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NioroshiGyoushaBtnPopupMethod", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("NioroshiGyoushaBtnPopupMethod", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 荷降現場更新後処理
        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void NIOROSHI_GENBA_CD_OnValidated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 変更なしの場合
                var before = this.GetBeforeText(this.NIOROSHI_GENBA_CD.Name);
                if (this.NIOROSHI_GENBA_CD.Text == before && !this.logic.isInputError)
                {
                    return;
                }

                this.logic.isInputError = false;
                // チェックNGの場合
                if (!this.logic.ChechNioroshiGenbaCd())
                {
                    //// イベント削除
                    //this.NIOROSHI_GENBA_CD.Enter -= this.Control_Enter;
                    //if (!string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
                    //{
                    //    // 背景色変更
                    //    this.NIOROSHI_GENBA_CD.IsInputErrorOccured = true;
                    //}
                    // フォーカス設定
                    this.NIOROSHI_GENBA_CD.Focus();
                    return;
                }

                // Escキーが押されたときのためにEnterかTabが押されたときだけフォーカスの移動を制御する
                if (!this.logic.MoveToNextControlForShokuchikbnCheck(this.NIOROSHI_GENBA_CD))
                {
                    return;
                }

                // 変更なしの場合
                if (this.dicControl.ContainsKey("NIOROSHI_GENBA_CD") && this.dicControl["NIOROSHI_GENBA_CD"].Equals(this.NIOROSHI_GENBA_CD.Text))
                {
                    return;
                }

                // 全ての明細と合計の計算
                this.logic.CalcAllDetailAndTotal();

                // イベント追加
                //this.NIOROSHI_GENBA_CD.Enter += this.Control_Enter;
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
        private string beforeNioroshiGenba = "";
        private string beforeNioroshiGenbaName = "";
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ前の処理
        /// </summary>
        public void NioroshiGenbaBtnPopupBeforeMethod()
        {
            this.beforeNioroshiGenba = this.NIOROSHI_GENBA_CD.Text;
            this.beforeNioroshiGenbaName = this.NIOROSHI_GENBA_NAME.Text;
            this.NIOROSHI_GENBA_CD.Text = "";
        }
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void NioroshiGenbaBtnPopupMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();
                bool catchErr = true;

                // 荷降業者CD入力された且つ荷降業者名が入力不可且つ未入力の場合
                if (!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text) &&
                    this.NIOROSHI_GYOUSHA_NAME.ReadOnly &&
                    string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_NAME.Text))
                {
                    // 業者を取得
                    var gyoushaEntity = this.logic.GetGyousha(this.NIOROSHI_GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 取得できない場合
                    if (gyoushaEntity != null)
                    {
                        // 荷降業者名設定
                        this.NIOROSHI_GYOUSHA_NAME.Text = gyoushaEntity.SHOKUCHI_KBN.IsTrue ? gyoushaEntity.GYOUSHA_NAME1 : gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 20151021 katen #13337 品名手入力に関する機能修正 start
                if (string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text) || this.beforeNioroshiGenba == this.NIOROSHI_GENBA_CD.Text)
                {
                    this.NIOROSHI_GENBA_CD.Text = beforeNioroshiGenba;
                    this.NIOROSHI_GENBA_NAME.Text = beforeNioroshiGenbaName;
                    return;
                }
                //var ctrl = (TextBox)this.NIOROSHI_GENBA_CD;
                //var before = this.GetBeforeText(ctrl.Name);
                //if (ctrl.Text == before) { return; }
                var genbaEntity = this.logic.GetGenba(this.NIOROSHI_GENBA_CD.Text, this.NIOROSHI_GYOUSHA_CD.Text, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (genbaEntity != null)
                {
                    if (genbaEntity.SHOKUCHI_KBN.IsTrue)
                    {
                        this.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME1;
                    }
                    else
                    {
                        this.NIOROSHI_GENBA_NAME.Text = genbaEntity.GENBA_NAME_RYAKU;
                    }

                    // 全ての明細と合計の計算
                    this.logic.CalcAllDetailAndTotal();
                }
                // 20151021 katen #13337 品名手入力に関する機能修正 end
                // フォーカスセット
                this.NIOROSHI_GENBA_CD.Focus();

                var before = this.GetBeforeText(this.NIOROSHI_GENBA_CD.Name);
                if (this.NIOROSHI_GENBA_CD.Text != before || this.logic.isInputError)
                {
                    // Popupから戻ってきたとき値が変わっていれば前回値を保存
                    this.ControlEnterForPopUpAfter(this.NIOROSHI_GENBA_CD);
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("NioroshiGenbaBtnPopupMethod", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error("NioroshiGenbaBtnPopupMethod", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
        #region 荷降現場Validating
        /// <summary>
        /// 荷降現場Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_OnValidating(object sender, CancelEventArgs e)
        {
            if (!this.logic.HannyuusakiDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion
        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　end

        #region 営業担当者更新後処理
        /// <summary>
        /// 営業担当者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOU_TANTOUSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.CheckEigyouTantousha();
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

        #region 車種更新後処理
        /// <summary>
        /// 車種更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //CongBinh 20210713 #152801 S
                if (!this.rirekeIchiran.Focused)
                {
                    this.logic.RirekeShow();
                }
                //CongBinh 20210713 #152801 E

                if (!this.logic.ChechShashuCd())
                {
                    // フォーカス設定
                    this.SHASHU_CD.Focus();
                    return;
                }
                //CongBinh 20210713 #152801 S
                if (this.rirekeIchiran.Focused)
                {
                    this.logic.RirekeSharyouShow();
                }
                //CongBinh 20210713 #152801 E
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

        #region 車輌更新後処理
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            //CongBinh 20210713 #152801 S
            if (!this.rirekeIchiran.Focused)
            {
                this.logic.RirekeShow();
            }
            //CongBinh 20210713 #152801 E
            if (!this.logic.CheckSharyou())
            {
                return;
            }
            //CongBinh 20210713 #152801 S
            if (this.rirekeIchiran.Focused)
            {
                this.logic.RirekeSharyouShow();
            }
            //CongBinh 20210713 #152801 E
            this.ChangeHaishaJokyo();
        }

        /// <summary>
        /// ポップアップ後の処理
        /// </summary>
        public void SharyouPopupAfterMethod()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 運搬業者CD入力された且つ運搬業者名が入力不可且つ未入力の場合
                if (!string.IsNullOrEmpty(this.UNPAN_GYOUSHA_CD.Text) &&
                    this.UNPAN_GYOUSHA_NAME.ReadOnly &&
                    string.IsNullOrEmpty(this.UNPAN_GYOUSHA_NAME.Text))
                {
                    // 業者を取得
                    bool catchErr = true;
                    var gyoushaEntity = this.logic.GetGyousha(this.UNPAN_GYOUSHA_CD.Text, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 取得できない場合
                    if (gyoushaEntity != null)
                    {
                        // 運搬業者名設定
                        this.UNPAN_GYOUSHA_NAME.Text = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharyouPopupAfterMethod", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
        #region 車輌更新Validating
        /// <summary>
        /// 車輌CDのバリデートが完了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void SHARYOU_CD_Validating(object sender, CancelEventArgs e)
        {
            if (!this.logic.SharyouDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion
        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　end

        #region 運転者更新後処理
        /// <summary>
        /// 運転者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.CheckUntenshasha())
                {
                    // フォーカス設定
                    this.UNTENSHA_CD.Focus();
                    return;
                }

                this.ChangeHaishaJokyo();
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

        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
        #region 運転者Validating
        /// <summary>
        /// 運転者Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            if (!this.logic.UntenshaDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion
        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　end

        #region 補助員更新後処理
        /// <summary>
        /// 補助員更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOJOIN_CD_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!this.logic.CheckHojyoin())
                {
                    // フォーカス設定
                    this.HOJOIN_CD.Focus();
                    return;
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

        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　start
        #region 補助員Validating
        /// <summary>
        /// 補助員Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HOJOIN_CD_Validating(object sender, CancelEventArgs e)
        {
            if (!this.logic.HojoinDateCheck())
            {
                e.Cancel = true;
            }
        }
        #endregion
        /// 20141009 Houkakou 「収集受付入力画面」の休動Checkを追加する　end

        #region 車種台数　Validating
        /// <summary>
        /// 車種台数　Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHASHU_DAISU_NUMBER_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 「非活性」 または　「未入力」の場合、処理終了
                if (this.SHASHU_DAISU_NUMBER.ReadOnly ||
                    string.IsNullOrEmpty(this.SHASHU_DAISU_NUMBER.Text))
                {
                    return;
                }

                // 入力範囲チェック
                int cnt;
                int.TryParse(this.SHASHU_DAISU_NUMBER.Text, out cnt);
                if (cnt < 1 || cnt > 99)
                {
                    this.logic.msgLogic.MessageBoxShow("E002", "車種台数", "1～99");
                    // 背景色変更
                    this.SHASHU_DAISU_NUMBER.IsInputErrorOccured = true;
                    e.Cancel = true;
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

        #region コース組込　Validatingイベント
        /// <summary>
        /// コース組込　Validatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COURSE_KUMIKOMI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.COURSE_KUMIKOMI_NAME.Text = "";

                if (!string.IsNullOrEmpty(this.COURSE_KUMIKOMI_CD.Text))
                {
                    int cd;
                    int.TryParse(this.COURSE_KUMIKOMI_CD.Text, out cd);

                    switch (cd)
                    {
                        case 1:
                            this.COURSE_KUMIKOMI_NAME.Text = "臨時";
                            break;
                        case 2:
                            this.COURSE_KUMIKOMI_NAME.Text = "組込";
                            break;
                        default:
                            this.COURSE_KUMIKOMI_CD.IsInputErrorOccured = true;
                            this.logic.msgLogic.MessageBoxShow("E011", "コース組込");
                            e.Cancel = true;
                            return;
                    }

                    this.COURSE_KUMIKOMI_CD.Text = cd.ToString();
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

        #region 受付日　Validatedイベント
        /// <summary>
        /// 受付日　Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_DATE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 新規モード時
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 作業日(指定)未設定の場合
                    if (this.SAGYOU_DATE.Value == null)
                    {
                        // [作業日(指定)]に[受付日]に設定する。
                        this.SAGYOU_DATE.Value = this.UKETSUKE_DATE.Value;

                        // 作業日が空じゃないかつ、変更ありの場合
                        if (!string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
                        {
                            // 全ての明細と合計の計算
                            this.logic.CalcAllDetailAndTotal();
                        }
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

        #region 受付日（時）Validatedイベント
        /// <summary>
        /// 受付日（時）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_DATE_HOUR_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrEmpty(this.UKETSUKE_DATE_HOUR.Text) && string.IsNullOrEmpty(this.UKETSUKE_DATE_MINUTE.Text))
                {
                    this.UKETSUKE_DATE_MINUTE.Text = "0";
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

        #region 受付日（分）Validatedイベント
        /// <summary>
        /// 受付日（分）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_DATE_MINUTE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.UKETSUKE_DATE_HOUR.Text) && !string.IsNullOrEmpty(this.UKETSUKE_DATE_MINUTE.Text))
                {
                    this.UKETSUKE_DATE_HOUR.Text = "0";
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

        #region 作業日(指定)　Validatedイベント
        /// <summary>
        /// 作業日(指定)　Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_DATE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //作業日の有無に応じて荷降日のEnabledを切り替える
                this.ChangeNioroshiDateEnabled();

                //荷降日が活性の場合
                if (this.NIOROSHI_DATE.Enabled)
                {
                    //荷降日にフォーカスを移動する。
                    this.NIOROSHI_DATE.Focus();
                }

                // 新規モード時
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 荷降日初期値設定
                    if (this.NIOROSHI_DATE.Value == null)
                    {
                        // [荷降日]の設定がない場合、[作業日(指定)]を[荷降日]に設定する。
                        this.NIOROSHI_DATE.Value = this.SAGYOU_DATE.Value;
                    }
                }

                // 作業日が空じゃないかつ、変更ありの場合
                if (!string.IsNullOrEmpty(this.SAGYOU_DATE.Text) && (this.dicControl.ContainsKey("SAGYOU_DATE") && !this.dicControl["SAGYOU_DATE"].Equals(this.SAGYOU_DATE.Text)))
                {
                    // 全ての明細と合計の計算
                    this.logic.CalcAllDetailAndTotal();

                    //Communicate InxsSubApplication - Compare sagyou date with request confirm date 
                    this.logic.CompareSagyouDateAndConfirmDate();
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

        #region 現着時間（時）Validatedイベント
        /// <summary>
        /// 現着時間（時）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENCHAKU_TIME_HOUR_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrEmpty(this.GENCHAKU_TIME_HOUR.Text) && string.IsNullOrEmpty(this.GENCHAKU_TIME_MINUTE.Text))
                {
                    this.GENCHAKU_TIME_MINUTE.Text = "0";
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

        #region 現着時間（分）Validatedイベント
        /// <summary>
        /// 現着時間（分）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENCHAKU_TIME_MINUTE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.GENCHAKU_TIME_HOUR.Text) && !string.IsNullOrEmpty(this.GENCHAKU_TIME_MINUTE.Text))
                {
                    this.GENCHAKU_TIME_HOUR.Text = "0";
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

        #region 作業時間（時）Validatedイベント
        /// <summary>
        /// 作業時間（時）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_TIME_HOUR_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrEmpty(this.SAGYOU_TIME_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_TIME_MINUTE.Text))
                {
                    this.SAGYOU_TIME_MINUTE.Text = "0";
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

        #region 作業時間（分）Validatedイベント
        /// <summary>
        /// 作業時間（分）Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_TIME_MINUTE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.SAGYOU_TIME_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_TIME_MINUTE.Text))
                {
                    this.SAGYOU_TIME_HOUR.Text = "0";
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

        #region 移動時間(時）FROM　Validatingイベント
        ///// <summary>
        ///// 移動時間(時）FROM　Validatingイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SAGYOU_TIME_BEGIN_HOUR_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        // 期間関連チェック
        //        if (!string.IsNullOrEmpty(this.SAGYOU_TIME_END_HOUR.Text))
        //        {
        //            // [移動時間TO]の設定がある場合
        //            string from = this.SAGYOU_TIME_BEGIN_HOUR.Text.PadLeft(2, '0');
        //            string to = this.SAGYOU_TIME_END_HOUR.Text.PadLeft(2, '0');
        //            if (from.CompareTo(to) == 1)
        //            {
        //                //this.SAGYOU_TIME_BEGIN_HOUR.Focus();
        //                this.logic.msgLogic.MessageBoxShow("E021");
        //                e.Cancel = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // 例外エラー
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        #endregion

        #region 移動時間(分）FROM　Validatingイベント
        ///// <summary>
        ///// 移動時間(分）FROM　Validatingイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SAGYOU_TIME_BEGIN_MINUTE_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        // 期間関連チェック
        //        if (!string.IsNullOrEmpty(this.SAGYOU_TIME_END_HOUR.Text) ||
        //            !string.IsNullOrEmpty(this.SAGYOU_TIME_END_MINUTE.Text) )
        //        {
        //            // [移動時間TO]の設定がある場合
        //            string from = this.SAGYOU_TIME_BEGIN_HOUR.Text.PadLeft(2, '0') + this.SAGYOU_TIME_BEGIN_MINUTE.Text.PadLeft(2, '0');
        //            string to = this.SAGYOU_TIME_END_HOUR.Text.PadLeft(2, '0') + this.SAGYOU_TIME_END_MINUTE.Text.PadLeft(2, '0');
        //            if (!string.IsNullOrEmpty(to) && from.CompareTo(to) == 1)
        //            {
        //                //this.SAGYOU_TIME_BEGIN_MINUTE.Focus();
        //                this.logic.msgLogic.MessageBoxShow("E021");
        //                e.Cancel = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // 例外エラー
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        #endregion

        #region 移動時間(時）FROM Validatedイベント
        /// <summary>
        /// 移動時間(時）FROM Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_TIME_BEGIN_HOUR_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_MINUTE.Text))
                {
                    this.SAGYOU_TIME_BEGIN_MINUTE.Text = "0";
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

        #region 移動時間(分）FROM Validatedイベント
        /// <summary>
        /// 移動時間(分）FROM Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_TIME_BEGIN_MINUTE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_MINUTE.Text))
                {
                    this.SAGYOU_TIME_BEGIN_HOUR.Text = "0";
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

        #region 移動時間(時）TO　Validatingイベント
        ///// <summary>
        ///// 移動時間(時）TO　Validatingイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SAGYOU_TIME_END_HOUR_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        // 期間関連チェック
        //        if (!string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_HOUR.Text))
        //        {
        //            // [移動時間FROM]の設定がある場合
        //            string from = this.SAGYOU_TIME_BEGIN_HOUR.Text.PadLeft(2, '0');
        //            string to = this.SAGYOU_TIME_END_HOUR.Text.PadLeft(2, '0');
        //            if (from.CompareTo(to) == 1)
        //            {
        //                //this.SAGYOU_TIME_END_HOUR.Focus();
        //                this.logic.msgLogic.MessageBoxShow("E021");
        //                e.Cancel = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // 例外エラー
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        #endregion

        #region 移動時間(分）TO　Validatingイベント
        ///// <summary>
        ///// 移動時間(分）TO　Validatingイベント
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SAGYOU_TIME_END_MINUTE_Validating(object sender, CancelEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(sender, e);

        //        // 期間関連チェック
        //        if (!string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_HOUR.Text) ||
        //            !string.IsNullOrEmpty(this.SAGYOU_TIME_BEGIN_MINUTE.Text) )
        //        {
        //            // [移動時間FROM]の設定がある場合
        //            string from = this.SAGYOU_TIME_BEGIN_HOUR.Text.PadLeft(2, '0') + this.SAGYOU_TIME_BEGIN_MINUTE.Text.PadLeft(2, '0');
        //            string to = this.SAGYOU_TIME_END_HOUR.Text.PadLeft(2, '0') + this.SAGYOU_TIME_END_MINUTE.Text.PadLeft(2, '0');
        //            if (!string.IsNullOrEmpty(to) && from.CompareTo(to) == 1)
        //            {
        //                //this.SAGYOU_TIME_END_MINUTE.Select();
        //                this.logic.msgLogic.MessageBoxShow("E021");
        //                e.Cancel = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // 例外エラー
        //        LogUtility.Error(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        #endregion

        #region 移動時間(時）TO Validatedイベント
        /// <summary>
        /// 移動時間(時）TO Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_TIME_END_HOUR_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!string.IsNullOrEmpty(this.SAGYOU_TIME_END_HOUR.Text) && string.IsNullOrEmpty(this.SAGYOU_TIME_END_MINUTE.Text))
                {
                    this.SAGYOU_TIME_END_MINUTE.Text = "0";
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

        #region 移動時間(分）TO Validatedイベント
        /// <summary>
        /// 移動時間(分）TO Validatedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_TIME_END_MINUTE_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.SAGYOU_TIME_END_HOUR.Text) && !string.IsNullOrEmpty(this.SAGYOU_TIME_END_MINUTE.Text))
                {
                    this.SAGYOU_TIME_END_HOUR.Text = "0";
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

        #region 受付番号更新後処理
        /// <summary>
        /// 受付番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NUMBER_Validated(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                bool catchErr = true;

                // イベント追加
                //this.UKETSUKE_NUMBER.Enter += this.Control_Enter;

                // 変更なしの場合
                if (this.dicControl.ContainsKey("UKETSUKE_NUMBER") &&
                    this.dicControl["UKETSUKE_NUMBER"].Equals(this.UKETSUKE_NUMBER.Text))
                {
                    return;
                }

                // 新規モード and 未入力　の場合
                if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) && string.IsNullOrEmpty(this.UKETSUKE_NUMBER.Text))
                {
                    return;
                }
                else if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 受付番号をセット
                    this.UketsukeNumber = this.UKETSUKE_NUMBER.Text;

                    // 権限チェック
                    if (!r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                        !r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        this.logic.msgLogic.MessageBoxShow("E158", "修正");

                        // 受付番号の初期化
                        this.UketsukeNumber = string.Empty;
                        this.UKETSUKE_NUMBER.Text = this.UketsukeNumber;

                        bool isInit = this.logic.DisplayInit(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }

                        if (!isInit)
                        {
                            // イベント削除
                            this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                            // フォーカス設定
                            this.UKETSUKE_NUMBER.Focus();
                        }

                        // 処理終了
                        return;
                    }

                    // 新規モード and 受付番号データが存在しない場合
                    int count = this.logic.Search();
                    if (count == -1)
                    {
                        return;
                    }
                    else if (count == 0)
                    {
                        // メッセージ表示
                        this.logic.msgLogic.MessageBoxShow("E045");

                        // 受付番号の初期化
                        this.UKETSUKE_NUMBER.Text = this.UketsukeNumber;
                        // イベント削除
                        this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;

                        this.UKETSUKE_NUMBER.Focus();

                        this.UKETSUKE_NUMBER.Enter += this.Control_Enter;
                        // 処理終了
                        return;
                    }
                    else
                    {
                        if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                        {
                            // 編集モードに変更
                            this.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                        }
                        else
                        {
                            // ここに到達する前に修正＆参照なしをチェックしているので参照権限チェックは行っていない

                            // 参照モードに変更
                            this.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                        }
                        //base.OnLoad(e);
                        // 画面初期化
                        this.logic.DisplayInit(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                    }
                }

                // 編集 or 参照モード処理を行う
                this.UketsukeNumber = this.UKETSUKE_NUMBER.Text;
                bool result = this.logic.DisplayInit(out catchErr);
                if (!catchErr)
                {
                    return;
                }

                if (!result)
                {
                    // イベント削除
                    this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                    // フォーカス設定
                    this.UKETSUKE_NUMBER.Focus();
                    return;
                }

                //// イベント追加
                //this.UKETSUKE_NUMBER.Enter += this.Control_Enter;

                // 前・次ﾎﾞﾀﾝクリック場合、値退避
                if (this.dicControl.ContainsKey("UKETSUKE_NUMBER"))
                {
                    this.dicControl["UKETSUKE_NUMBER"] = this.UketsukeNumber;
                }
                else
                {
                    this.dicControl.Add("UKETSUKE_NUMBER", this.UketsukeNumber);
                }
                this.SAGYOU_DATE_TMP.Visible = false;//CongBinh 20210713 #152801

                //Communicate InxsSubApplication - Compare sagyou date with request confirm date 
                this.logic.CompareSagyouDateAndConfirmDate();
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

        #region 受付番号「前」
        /// <summary>
        /// 受付番号「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_PREVIOUS_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 拠点の必須チェック
                if (string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点");
                    this.logic.headerForm.KYOTEN_CD.Focus();
                    return;
                }

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    String previousUketsukeNumber;
                    String tableName = "T_UKETSUKE_SS_ENTRY";
                    String fieldName = "UKETSUKE_NUMBER";
                    String UketsukeNumber = this.UKETSUKE_NUMBER.Text;
                    // 前の受付番号を取得
                    bool catchErr = true;
                    previousUketsukeNumber = this.logic.GetPreviousNumber(tableName, fieldName, UketsukeNumber, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    //ThangNguyen [Add] 20150814 #11409 Start
                    if (previousUketsukeNumber == "")
                    {
                        this.logic.msgLogic.MessageBoxShow("E045");
                        return;
                    }
                    //ThangNguyen [Add] 20150814 #11409 End
                    // 受付番号を設定
                    this.UKETSUKE_NUMBER.Text = previousUketsukeNumber;
                    // モードを初期化
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    // 受付番号更新後処理
                    UKETSUKE_NUMBER_Validated(sender, e);
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
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

        #region 受付番号「次」
        /// <summary>
        /// 受付番号「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UKETSUKE_NEXT_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 拠点の必須チェック
                if (string.IsNullOrEmpty(this.logic.headerForm.KYOTEN_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E001", "拠点");
                    this.logic.headerForm.KYOTEN_CD.Focus();
                    return;
                }

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    String nextUketsukeNumber;
                    String tableName = "T_UKETSUKE_SS_ENTRY";
                    String fieldName = "UKETSUKE_NUMBER";
                    String UketsukeNumber = this.UKETSUKE_NUMBER.Text;
                    // 次の受付番号を取得
                    bool catchErr = true;
                    nextUketsukeNumber = this.logic.GetNextNumber(tableName, fieldName, UketsukeNumber, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    //ThangNguyen [Add] 20150814 #11409 Start
                    if (nextUketsukeNumber == "")
                    {
                        this.logic.msgLogic.MessageBoxShow("E045");
                        return;
                    }
                    //ThangNguyen [Add] 20150814 #11409 End
                    // 受付番号を設定
                    this.UKETSUKE_NUMBER.Text = nextUketsukeNumber;
                    // モードを初期化
                    this.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    // 受付番号更新後処理
                    UKETSUKE_NUMBER_Validated(sender, e);
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
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

        #region 車種台数「前」
        /// <summary>
        /// 車種台数「前」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAISUU_PREVIOUS_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    String previousUketsukeNumber;
                    String tableName = "T_UKETSUKE_SS_ENTRY";

                    // 車種台数番号
                    String shashuDaisuNumber = this.SHASHU_DAISU_NUMBER.Text;

                    // 車種台数番号は既に最小の場合
                    if (shashuDaisuNumber == "1")
                    {
                        return;
                    }
                    // 前の台数の受付番号を取得
                    bool catchErr = true;
                    previousUketsukeNumber = this.logic.GetPreviousDaisuuNumber(tableName, shashuDaisuNumber, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 受付番号を設定
                    this.UKETSUKE_NUMBER.Text = previousUketsukeNumber;
                    // イベント削除
                    //this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                    // 受付番号更新後処理
                    UKETSUKE_NUMBER_Validated(sender, e);
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
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

        #region 車種台数「次」
        /// <summary>
        /// 車種台数「次」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAISUU_NEXT_BUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) ||
                    r_framework.Authority.Manager.CheckAuthority("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    String nextUketsukeNumber;
                    String tableName = "T_UKETSUKE_SS_ENTRY";
                    // 車種台数番号
                    String shashuDaisuNumber = this.SHASHU_DAISU_NUMBER.Text;

                    // 車種台数番号は既に最大の場合
                    if (shashuDaisuNumber == this.logic.groupNumber.ToString())
                    {
                        return;
                    }
                    // 次の台数の受付番号を取得
                    bool catchErr = true;
                    nextUketsukeNumber = this.logic.GetNextDaisuuNumber(tableName, shashuDaisuNumber, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    // 受付番号を設定
                    this.UKETSUKE_NUMBER.Text = nextUketsukeNumber;
                    // イベント削除
                    //this.UKETSUKE_NUMBER.Enter -= this.Control_Enter;
                    // 受付番号更新後処理
                    UKETSUKE_NUMBER_Validated(sender, e);
                }
                else
                {
                    this.logic.msgLogic.MessageBoxShow("E158", "修正");
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

        #region 各CELLのフォーカス取得時処理
        /// <summary>
        /// 各CELLのフォーカス取得時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dgvDetail_OnCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // IMEMode制御
                dgvDetail.ImeMode = ImeMode.Off;
                // 品名、明細備考入力場合，ひらがな
                if (this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("HINMEI_NAME") ||
                    this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("MEISAI_BIKOU"))
                {
                    dgvDetail.ImeMode = ImeMode.Hiragana;
                }
                else if (this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("DENPYOU_KBN_NAME_RYAKU"))
                {
                    dgvDetail.ImeMode = ImeMode.Off;
                }
                else
                {
                    dgvDetail.ImeMode = ImeMode.Disable;
                }

                DataGridViewRow row = this.dgvDetail.CurrentRow;
                if (row == null)
                {
                    return;
                }

                // No.4255-->
                // 品名でPopup表示後処理追加
                if (this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("HINMEI_CD"))
                {
                    // PopupResult取得できるようにPopupAfterExecuteにデータ設定
                    DgvCustomTextBoxCell cell = (DgvCustomTextBoxCell)row.Cells["HINMEI_CD"];
                    cell.PopupAfterExecute = PopupAfter_HINMEI_CD;
                }
                // No.4255<--

                //CongBinh 20210713 #152801 S
                if ((!string.IsNullOrEmpty(this.TORIHIKISAKI_CD.Text) ||
                    !string.IsNullOrEmpty(this.GYOUSHA_CD.Text) ||
                    !string.IsNullOrEmpty(this.GENBA_CD.Text)) &&
                    (this.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG ||
                    this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG))
                {
                    if (this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("HINMEI_CD"))
                    {
                        this.logic.RirekeHinmeiShow();
                    }
                    else
                    {
                        this.logic.RirekeShow();
                    }
                }
                //CongBinh 20210713 #152801 E


                // 前回値チェック用データをセット
                String cellname = this.dgvDetail.Columns[e.ColumnIndex].Name;
                String cellvalue = Convert.ToString(this.dgvDetail[cellname, e.RowIndex].Value);
                if (beforeValuesForDetail.ContainsKey(cellname))
                {
                    beforeValuesForDetail[cellname] = cellvalue;
                }
                else
                {
                    beforeValuesForDetail.Add(cellname, cellvalue);
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

        #region CELLのCellValidatingイベント

        /// <summary>
        /// 各セルのValidateが開始したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void dgvDetail_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (isSetDetailReadOnly) return;

                // カラム名
                String cellName = this.dgvDetail.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.dgvDetail[cellName, e.RowIndex].Value);

                switch (cellName)
                {
                    case "HINMEI_CD":

                        // 大文字変換
                        if (this.dgvDetail.Rows[e.RowIndex].Cells[cellName].Value != null &&
                            !string.IsNullOrEmpty(this.dgvDetail.Rows[e.RowIndex].Cells[cellName].Value.ToString()))
                        {
                            string tmp = this.dgvDetail.Rows[e.RowIndex].Cells[cellName].Value.ToString();
                            this.dgvDetail.Rows[e.RowIndex].Cells[cellName].Value = tmp.ToUpper();
                        }

                        //入力された品名CDの伝種区分が、１．受入　３．売上支払　９．共通　かチェック
                        bool catchErr = true;
                        bool checkHinmeiDenshu = this.logic.CheckHinmeiDensyu(out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }

                        // No.4255-->
                        var targetRow = this.dgvDetail.CurrentRow;
                        if (targetRow != null)
                        {
                            // 伝票区分CD設定
                            if ((targetRow.Cells["DENPYOU_KBN_NAME_RYAKU"].Value != null && !string.IsNullOrEmpty(targetRow.Cells["DENPYOU_KBN_NAME_RYAKU"].Value.ToString()))
                                && (targetRow.Cells["DENPYOU_KBN_CD"].Value == null || string.IsNullOrEmpty(targetRow.Cells["DENPYOU_KBN_CD"].Value.ToString())))
                            {
                                // 品名検索から伝票区分CDが設定できない(伝票区分名は可)ため、強制的に設定。
                                var name = targetRow.Cells["DENPYOU_KBN_NAME_RYAKU"].Value.ToString();
                                if ("売上".Equals(name))
                                {
                                    targetRow.Cells["DENPYOU_KBN_CD"].Value = ConstClass.DENPYOU_KBN_CD_URIAGE_STR;
                                }
                                else if ("支払".Equals(name))
                                {
                                    targetRow.Cells["DENPYOU_KBN_CD"].Value = ConstClass.DENPYOU_KBN_CD_SHIHARAI_STR;
                                }
                            }

                            DgvCustomTextBoxCell control = (DgvCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                            if (control.TextBoxChanged == true)
                            {
                                targetRow.Cells["DENPYOU_KBN_CD"].Value = string.Empty; // 伝票区分をクリア
                            }
                        }
                        // No.4255<--

                        if (String.IsNullOrEmpty(cellValue) || !checkHinmeiDenshu)
                        {
                            // 品名CDが空の場合は関連項目をクリアする
                            this.dgvDetail["HINMEI_NAME", e.RowIndex].Value = String.Empty;
                            this.dgvDetail["DENPYOU_KBN_CD", e.RowIndex].Value = String.Empty;
                            this.dgvDetail["DENPYOU_KBN_NAME_RYAKU", e.RowIndex].Value = String.Empty;
                            this.dgvDetail["UNIT_CD", e.RowIndex].Value = String.Empty;
                            this.dgvDetail["UNIT_NAME_RYAKU", e.RowIndex].Value = String.Empty;
                            this.dgvDetail["TANKA", e.RowIndex].Value = String.Empty;

                            if (!String.IsNullOrEmpty(cellValue) && !checkHinmeiDenshu)
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                var cell = this.dgvDetail[cellName, e.RowIndex];
                                ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                                msgLogic.MessageBoxShow("E020", "品名");
                                e.Cancel = true;
                                this.logic.isInputError = true;
                                return;
                            }

                        }
                        else if (string.IsNullOrEmpty(Convert.ToString(this.dgvDetail["DENPYOU_KBN_CD", e.RowIndex].Value)) || beforeValuesForDetail[cellName] != cellValue)
                        {
                            if (this.logic.HasWarnKobetsuHinmeiTanka(targetRow.Cells["HINMEI_CD"].Value.ToString()))
                            {
                                // 警告メッセージを表示する。
                                this.logic.msgLogic.MessageBoxShowWarn("個別単価（契約単価）が未登録の品名ＣＤをセットしました。");
                            }

                            // 伝票区分をセット
                            var result = true;
                            if(string.IsNullOrEmpty(Convert.ToString(this.dgvDetail["DENPYOU_KBN_CD", e.RowIndex].Value)))
                            {
                                result = this.logic.SetDenpyouKbn();
                            }

                            if (false == result)
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                // 現在の品名CDの値を取得
                                // 売上/支払入力画面(G054)等とは品名設定のタイミングが違うが、
                                // 一番影響が少ないであろう箇所で品名を設定する。
                                var targetHimei = this.logic.GetHinmeiDataByCd(targetRow.Cells["HINMEI_CD"].Value.ToString());
                                if (targetHimei != null)
                                {
                                    targetRow.Cells["HINMEI_NAME"].Value = targetHimei.HINMEI_NAME_RYAKU;
                                }

                                // 単位をセット
                                if (targetRow.Cells["UNIT_CD"].Value == null 
                                    || string.IsNullOrEmpty(targetRow.Cells["UNIT_CD"].Value.ToString()))
                                {
                                    if (!this.logic.SetUnit(e))
                                    {
                                        return;
                                    }
                                }

                                // 単価をセット
                                this.logic.CalcTanka(this.dgvDetail.Rows[e.RowIndex]);
                                this.logic.ResetTankaCheck(); // MAILAN #158988 START
                            }
                        }
                        break;

                    case "UNIT_CD":
                        bool checkUnit = this.logic.CheckUnit();
                        if (!checkUnit)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            var cell = this.dgvDetail[cellName, e.RowIndex];
                            ControlUtility.SetInputErrorOccuredForDgvCell(cell, true);
                            msgLogic.MessageBoxShow("E020", "単位");
                            e.Cancel = true;
                            this.logic.isInputError = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(this.dgvDetail["DENPYOU_KBN_CD", e.RowIndex].Value)) || beforeValuesForDetail[cellName] != cellValue)
                        {
                            // 単価をセット
                            this.logic.CalcTanka(this.dgvDetail.Rows[e.RowIndex]);
                            this.logic.ResetTankaCheck(); // MAILAN #158988 START
                        }
                        break;
                    default:
                        break;
                }

                // 単価と金額の活性/非活性制御
                isSetDetailReadOnly = true;
                if (cellName.Equals("TANKA") && !this.dgvDetail.Rows[e.RowIndex].Cells[cellName].ReadOnly)
                {
                    // 単価の場合のみCellValidatedでReadOnly設定が変わる場合があるのでここで一旦計算を行う
                    // 明細金額計算
                    if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow))
                    {
                        return;
                    }
                    // 合計系の計算
                    if (!this.logic.CalcTotalValues())
                    {
                        return;
                    }
                }
                SetIchranReadOnly(e.RowIndex);
                isSetDetailReadOnly = false;

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

        #region 各CELLの更新後処理
        /// <summary>
        /// 各セルのValidateが終了したときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void dgvDetail_OnValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // カラム名
                String cellName = this.dgvDetail.Columns[e.ColumnIndex].Name;
                String cellValue = Convert.ToString(this.dgvDetail[cellName, e.RowIndex].Value);
                bool isReCalc = false;
                bool isTotalReCalc = false;
                switch (cellName)
                {
                    case "HINMEI_CD":
                        var targetRow = this.dgvDetail.CurrentRow;
                        if (targetRow != null)
                        {
                            DgvCustomTextBoxCell control = (DgvCustomTextBoxCell)targetRow.Cells["HINMEI_CD"];
                            if (control.TextBoxChanged == true)
                            {
                                isReCalc = true;
                            }
                            else if (!String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["TANKA"].Value))
                                && targetRow.Cells["HINMEI_KINGAKU"].ReadOnly)
                            {
                                // ポップアップから品名CDを設定した場合に金額再計算がされない問題の回避策(#19944)
                                isReCalc = true;
                            }
                        }
                        //CongBinh 20210713 #152801 S
                        if (!this.rirekeIchiran.Focused)
                        {
                            this.logic.RirekeShow();
                        }
                        //CongBinh 20210713 #152801 E
                        break;

                    case "UNIT_CD":
                    case "SUURYOU":
                    case "TANKA":

                        if (beforeValuesForDetail[cellName] != cellValue || this.bCancelDenpyoPopup == true)
                        {
                            isReCalc = true;
                        }

                        break;
                    case "HINMEI_KINGAKU":

                        if (beforeValuesForDetail[cellName] != cellValue || this.bCancelDenpyoPopup == true)
                        {
                            isTotalReCalc = true;
                        }

                        break;
                    default:
                        break;
                }

                if (isReCalc)
                {
                    // 明細金額計算
                    if (!this.logic.CalcDetailKingaku(this.dgvDetail.CurrentRow))
                    {
                        return;
                    }
                    // 合計系の計算
                    if (!this.logic.CalcTotalValues())
                    {
                        return;
                    }
                }
                else if (isTotalReCalc)
                {
                    // 合計系の計算
                    if (!this.logic.CalcTotalValues())
                    {
                        return;
                    }
                }

                // 前回値チェック用の値をセット
                if (beforeValuesForDetail.ContainsKey(cellName))
                {
                    beforeValuesForDetail[cellName] = cellValue;
                }
                else
                {
                    beforeValuesForDetail.Add(cellName, cellValue);
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

        #region 各CELLのクリックイベント
        /// <summary>
        /// 各CELLのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //品名ポップアップボタンをクリックする時
                if (this.dgvDetail.Columns[e.ColumnIndex].Name.Equals("HINMEI_SEARCH_BUTTON") && !this.dgvDetail[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // 品名CDをフォーカス
                    this.dgvDetail.Rows[e.RowIndex].Cells["HINMEI_CD"].Selected = true;
                    // スペースキー押す
                    SendKeys.Send(" ");
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

        #region GridWiewのcellを結合する
        /// <summary>
        /// 列ヘッダーの罫線を消して結合しているように表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void GridWiew_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //■■■■■■■■■■■■
                //ヘッダセルの結合処理開始
                //■■■■■■■■■■■■
                if (e.RowIndex > -1)
                {
                    // 虫眼鏡イメージ描く（ボタンカラム）
                    if (e.ColumnIndex == 3)
                    {
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                        Bitmap findBmp = Properties.Resources.虫眼鏡;
                        Image img = Image.FromHbitmap(findBmp.GetHbitmap());
                        e.Graphics.DrawImage(img, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                        e.Handled = true;
                    }

                    // ヘッダー以外は処理なし
                    return;
                }

                // 結合開始列のIndex
                int colIndex = 2;

                // 結合する列の場合
                if (e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1)
                {
                    // セルの矩形を取得
                    Rectangle rect = e.CellBounds;

                    DataGridView dgv = (DataGridView)sender;

                    // 1列目の場合
                    if (e.ColumnIndex == colIndex)
                    {
                        // 2列目の幅を取得して、1列目の幅に足す
                        rect.Width += dgv.Columns[colIndex + 1].Width;
                        rect.Y = e.CellBounds.Y + 1;
                    }
                    else
                    {
                        // 1列目の幅を取得して、2列目の幅に足す
                        rect.Width += dgv.Columns[colIndex].Width;
                        rect.Y = e.CellBounds.Y + 1;

                        // Leftを1列目に合わせる
                        rect.X -= dgv.Columns[colIndex].Width;
                    }
                    // 背景、枠線、セルの値を描画
                    using (SolidBrush brush = new SolidBrush(this.dgvDetail.ColumnHeadersDefaultCellStyle.BackColor))
                    {
                        // 背景の描画
                        e.Graphics.FillRectangle(brush, rect);

                        using (Pen pen = new Pen(dgv.GridColor))
                        {
                            // 枠線の描画
                            e.Graphics.DrawRectangle(pen, rect);
                        }

                        using (Pen pen1 = new Pen(Color.DarkGray))
                        {
                            // 直線を描画(ヘッダ上部)
                            e.Graphics.DrawLine(pen1, rect.X, rect.Y - 1, rect.X + rect.Width, rect.Y - 1);

                            // 直線を描画(ヘッダ下部)
                            e.Graphics.DrawLine(pen1, rect.X, rect.Y + rect.Height - 2, rect.X + rect.Width, rect.Y + rect.Height - 2);
                        }
                    }

                    // セルに表示するテキストを描画
                    TextRenderer.DrawText(e.Graphics,
                                    "品名",
                                    e.CellStyle.Font,
                                    rect,
                                    e.CellStyle.ForeColor,
                                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
                }

                // 結合セル以外は既定の描画を行う
                if (!(e.ColumnIndex == colIndex || e.ColumnIndex == colIndex + 1))
                {
                    e.Paint(e.ClipBounds, e.PaintParts);
                }

                // イベントハンドラ内で処理を行ったことを通知
                e.Handled = true;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// カラムサイズ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDetail_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvDetail.Refresh();
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

        #region RowsAddedイベント
        /// <summary>
        /// RowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                // 行NO設定
                this.logic.SetRowNo();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }
        #endregion

        #region RowsRemovedイベント
        /// <summary>
        /// RowsRemovedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                // 行NO設定
                this.logic.SetRowNo();
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

        #region UIForm_Shownイベント

        /// <summary>
        /// Formが初めて表示されたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // データ移動初期表示処理
            if (!this.logic.SetMoveData())
            {
                return;
            }

            this.SetFocusControl(this.GYOUSHA_CD);
            this.boolMoveFocusControl();
        }
        #endregion UIForm_Shownイベント

        #region フォーカス設定処理
        /// <summary>
        /// フォーカスさせるコントロールを設定します
        /// </summary>
        /// <param name="control">フォーカスを設定したいコントロール</param>
        internal void SetFocusControl(Control control)
        {
            //引数にフォーカスを設定
            this.focusControl = control;
        }

        /// <summary>
        /// コントロールにフォーカスを設定します
        /// </summary>
        internal bool boolMoveFocusControl()
        {
            // 初期化
            this.isNotMoveFocusFW = false;
            bool ret = false;

            if (null != this.focusControl)
            {
                this.isNotMoveFocusFW = true;
                this.focusControl.Focus();
                ret = true;
            }

            return ret;

        }
        #endregion フォーカス設定処理

        #region 作業日の有無に応じて荷降日のEnabledを切り替える

        /// <summary>
        /// 作業日の有無に応じて荷降日のEnabledを切り替える
        /// </summary>
        internal void ChangeNioroshiDateEnabled()
        {
            // 新規モードまたは、修正モード時
            if (this.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG) ||
                this.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG))
            {
                //作業日(指定)が入力されていない場合
                if (string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
                {
                    //荷降日の値を空にする
                    this.NIOROSHI_DATE.Text = string.Empty;
                    //荷降日を非活性にする
                    this.NIOROSHI_DATE.Enabled = false;
                }
                else
                {
                    //荷降日を活性にする
                    this.NIOROSHI_DATE.Enabled = true;
                }
            }

            //荷降日のEnabledによってラベルを切り替えます
            this.ChangeNioroshiLabel();

        }
        #endregion 作業日の有無に応じて荷降日のEnabledを切り替える

        #region 荷降日のEnabledによってラベルを切り替えます
        /// <summary>
        /// 荷降日のEnabledによってラベルを切り替えます
        /// </summary>
        private void ChangeNioroshiLabel()
        {
            //荷降日のEnabledがFalseの時
            if (!this.NIOROSHI_DATE.Enabled)
            {
                //荷降日のラベルから「※」をとる
                this.label44.Text = "荷降日";
            }
            else
            {
                //荷積日のラベルに「※」をつける
                this.label44.Text = "荷降日※";
            }
        }
        #endregion 荷降日のEnabledによってラベルを切り替えます

        #endregion

        /// <summary>
        /// 配車状況のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HAISHA_JOKYO_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var haishaJokyoCd = this.HAISHA_JOKYO_CD.Text;
            if (!this.logic.CheckHaishaJokyoCd(haishaJokyoCd))
            {
                this.HAISHA_JOKYO_NAME.Text = String.Empty;
                this.HAISHA_JOKYO_CD.IsInputErrorOccured = true;
                this.logic.msgLogic.MessageBoxShow("E011", "配車状況");
                this.SetFocusControl(this.HAISHA_JOKYO_CD);
                this.boolMoveFocusControl();
                this.HAISHA_JOKYO_CD.IsInputErrorOccured = true;
            }
            else
            {
                bool catchErr = true;
                var haishaJokyo = this.logic.GetHaishaJokyo(haishaJokyoCd, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                this.HAISHA_JOKYO_NAME.Text = haishaJokyo;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力状態に応じて配車状況を変更します
        /// </summary>
        private void ChangeHaishaJokyo()
        {
            LogUtility.DebugMethodStart();

            var haishaJokyoCd = this.HAISHA_JOKYO_CD.Text;
            var sharyouCd = this.SHARYOU_CD.Text;
            var untenshaCd = this.UNTENSHA_CD.Text;

            if ((ConstClass.HAISHA_JOKYO_CD_JUCHU == haishaJokyoCd || ConstClass.HAISHA_JOKYO_CD_CANCEL == haishaJokyoCd) && !String.IsNullOrEmpty(sharyouCd) && !String.IsNullOrEmpty(untenshaCd))
            {
                this.HAISHA_JOKYO_CD.Text = ConstClass.HAISHA_JOKYO_CD_HAISHA;
                this.HAISHA_JOKYO_NAME.Text = ConstClass.HAISHA_JOKYO_NAME_HAISHA;
            }
            else if (ConstClass.HAISHA_JOKYO_CD_HAISHA == haishaJokyoCd && (String.IsNullOrEmpty(sharyouCd) || String.IsNullOrEmpty(untenshaCd)))
            {
                this.HAISHA_JOKYO_CD.Text = ConstClass.HAISHA_JOKYO_CD_JUCHU;
                this.HAISHA_JOKYO_NAME.Text = ConstClass.HAISHA_JOKYO_NAME_JUCHU;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// コントロールに入力されていた値を取得します
        /// </summary>
        /// <param name="key">コントロール名</param>
        /// <returns>前回値</returns>
        internal String GetBeforeText(string key)
        {
            LogUtility.DebugMethodStart(key);

            string ret = this.dicControl.Where(r => r.Key == key).Select(r => r.Value).DefaultIfEmpty(String.Empty).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 配車種類のバリデートが完了したときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void HAISHA_SHURUI_CD_Validated(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (!this.CheckSharyouAndUntensha(true))
            {
                this.SetFocusControl(this.HAISHA_SHURUI_CD);
                this.boolMoveFocusControl();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 車輌CDと運転者CDの入力チェックを行います
        /// </summary>
        /// <param name="isHaishaShuruiValidatd">配車種類のバリデートから呼び出す場合は、true</param>
        /// <returns>エラーがある場合は、false</returns>
        private bool CheckSharyouAndUntensha(bool isHaishaShuruiValidatd)
        {
            LogUtility.DebugMethodStart(isHaishaShuruiValidatd);

            var ret = true;
            try
            {
                if (ConstClass.HAISHA_SHURUI_CD_KAKUTEI == this.HAISHA_SHURUI_CD.Text)
                {
                    // 「3:確定」の場合は、車輌と運転者のチェックを行う
                    var sharyouCd = this.SHARYOU_CD.Text;
                    var untenshaCd = this.UNTENSHA_CD.Text;
                    var messageId = String.Empty;
                    // 両方が入力されていない
                    if (String.IsNullOrEmpty(sharyouCd) && String.IsNullOrEmpty(untenshaCd))
                    {
                        messageId = "E193";
                        if (!isHaishaShuruiValidatd)
                        {
                            this.SHARYOU_CD.IsInputErrorOccured = true;
                            this.SHARYOU_CD.UpdateBackColor();
                            this.UNTENSHA_CD.IsInputErrorOccured = true;
                            this.UNTENSHA_CD.UpdateBackColor();

                            this.SHARYOU_CD.Focus();
                        }
                    }
                    // 運転者のみ入力されていない
                    if (!String.IsNullOrEmpty(sharyouCd) && String.IsNullOrEmpty(untenshaCd))
                    {
                        messageId = "E194";
                        if (!isHaishaShuruiValidatd)
                        {
                            this.UNTENSHA_CD.IsInputErrorOccured = true;
                            this.UNTENSHA_CD.UpdateBackColor();

                            this.UNTENSHA_CD.Focus();
                        }
                    }
                    // 車輌のみ入力されていない
                    if (String.IsNullOrEmpty(sharyouCd) && !String.IsNullOrEmpty(untenshaCd))
                    {
                        messageId = "E195";
                        if (!isHaishaShuruiValidatd)
                        {
                            this.SHARYOU_CD.IsInputErrorOccured = true;
                            this.SHARYOU_CD.UpdateBackColor();

                            this.SHARYOU_CD.Focus();
                        }
                    }

                    if (!String.IsNullOrEmpty(messageId))
                    {
                        this.logic.msgLogic.MessageBoxShow(messageId);

                        ret = false;
                    }
                }

                // エラーがないときは背景色を元に戻しておく
                if (ret)
                {
                    this.SHARYOU_CD.IsInputErrorOccured = false;
                    this.SHARYOU_CD.UpdateBackColor();
                    this.UNTENSHA_CD.IsInputErrorOccured = false;
                    this.UNTENSHA_CD.UpdateBackColor();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyouAndUntensha", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 取引先CDに関連する項目をセットする
        /// </summary>
        public bool SetTorihikisaki()
        {
            bool ret = true;
            try
            {
                var ctrl = (TextBox)this.TORIHIKISAKI_CD;

                // 取引先を取得
                var torihikisakiCd = this.TORIHIKISAKI_CD.Text;
                bool catchErr = true;
                var torihikisaki = this.logic.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    ret = false;
                    return ret;
                }
                // 取引先と拠点の関係をチェック
                if (false == this.logic.CheckTorihikisakiKyoten(this.logic.headerForm.KYOTEN_CD.Text, torihikisakiCd))
                {
                    //フォーカス設定処理
                    this.SetFocusControl(this.TORIHIKISAKI_CD);
                    this.boolMoveFocusControl();


                    this.TORIHIKISAKI_NAME.Text = string.Empty;
                    this.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    this.logic.isInputError = true;
                    return false;
                }

                var before = this.GetBeforeText(ctrl.Name);
                if (this.logic.isInputError || ctrl.Text != before)
                {
                    if (this.logic.CheckTorihikisaki())
                    {
                        if (!this.logic.isInputError)
                        {
                            this.logic.CalcAllDetailAndTotal();
                        }
                    }
                    else
                    {
                        //フォーカス設定処理
                        this.boolMoveFocusControl();

                    }
                    this.logic.RirekeShow(); //CongBinh 202010713 #152801
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetTorihikisaki", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetTorihikisaki", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        private string beforeGyousha = "";
        private string beforeGyoushaName = "";
        /// <summary>
        /// ﾎﾞﾀﾝポップアップ前の処理
        /// </summary>
        public void GyoushaPopupBeforeMethod()
        {
            this.beforeGyousha = this.GYOUSHA_CD.Text;
            this.beforeGyoushaName = this.GYOUSHA_NAME.Text;
            //this.GYOUSHA_CD.Text = "";
        }

        /// <summary>
        /// 業者ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void GyoushaPopupAfterMethod()
        {
            if (!this.logic.isInputError && this.GYOUSHA_CD.Text == this.beforeGyousha)
            {
                this.GYOUSHA_CD.Text = this.beforeGyousha;
                this.GYOUSHA_NAME.Text = this.beforeGyoushaName;
            }
            else
            {
                this.SetGyousha();
            }

            var before = this.GetBeforeText(this.GYOUSHA_CD.Name);
            if (this.GYOUSHA_CD.Text != before || this.logic.isInputError)
            {
                // Popupから戻ってきたとき値が変わっていれば前回値を保存
                this.ControlEnterForPopUpAfter(this.GYOUSHA_CD);
            }
        }

        /// <summary>
        /// 業者ﾎﾞﾀﾝポップアップ後の処理
        /// </summary>
        public void GyoushaPopupAfterExecute(object sender, DialogResult result)
        {
            if (result != DialogResult.Yes && result != DialogResult.OK)
                return;

            if (!this.logic.isInputError && this.GYOUSHA_CD.Text == this.beforeGyousha)
            {
                this.GYOUSHA_CD.Text = this.beforeGyousha;
                this.GYOUSHA_NAME.Text = this.beforeGyoushaName;
            }
            else
            {
                this.SetGyousha();
            }

            var before = this.GetBeforeText(this.GYOUSHA_CD.Name);
            if (this.GYOUSHA_CD.Text != before || this.logic.isInputError)
            {
                // Popupから戻ってきたとき値が変わっていれば前回値を保存
                this.ControlEnterForPopUpAfter(this.GYOUSHA_CD);
            }
        }

        /// <summary>
        /// 業者CDに関連する項目をセットする
        /// </summary>
        public bool SetGyousha()
        {
            bool ret = true;
            try
            {
                var ctrl = (TextBox)this.GYOUSHA_CD;

                var before = this.GetBeforeText(ctrl.Name);
                if (this.logic.isInputError || ctrl.Text != before)
                {
                    // 業者CDをチェック
                    if (this.logic.ErrorCheckGyousha())
                    {
                        this.GENBA_CD.Text = string.Empty;
                        this.GENBA_NAME.Text = String.Empty;
                        this.GENBA_ADDRESS1.Text = String.Empty;
                        this.GENBA_ADDRESS2.Text = String.Empty;
                        this.GENBA_TEL.Text = String.Empty;
                        this.TANTOSHA_NAME.Text = String.Empty;
                        this.TANTOSHA_TEL.Text = String.Empty;

                        if (this.logic.CheckGyousha())
                        {
                            this.logic.CalcAllDetailAndTotal();
                        }
                        else
                        {
                            //フォーカス設定処理
                            this.boolMoveFocusControl();
                        }
                    }
                    else
                    {
                        //フォーカス設定処理
                        this.boolMoveFocusControl();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGyousha", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyousha", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 現場CDに関連する項目をセットする
        /// </summary>
        public bool SetGenba()
        {
            bool ret = true;
            try
            {
                var genbaCtrl = (TextBox)this.GENBA_CD;
                var beforeGenba = this.GetBeforeText(genbaCtrl.Name);

                if (this.logic.isInputError || genbaCtrl.Text != beforeGenba)
                {
                    // 入力されてない場合
                    if (string.IsNullOrEmpty(this.GENBA_CD.Text))
                    {
                        this.logic.GenbaCdEnptyProcess();
                        return true;
                    }

                    if (this.logic.ErrorCheckGenba())
                    {
                        if (this.logic.CheckGenba())
                        {
                            // 全ての明細と合計の計算
                            this.logic.CalcAllDetailAndTotal();

                            if (this.logic.pressedEnterOrTab && this.GENBA_NAME.ReadOnly)
                            {
                                if (this.Key.Shift)
                                {
                                    this.SetFocusControl(this.GYOUSHA_CD);
                                }
                                else
                                {
                                    this.SetFocusControl(this.TORIHIKISAKI_CD);
                                }
                                //フォーカス設定処理
                                this.boolMoveFocusControl();
                            }
                        }
                        else
                        {
                            //フォーカス設定処理
                            this.boolMoveFocusControl();
                        }
                    }
                    else
                    {
                        //フォーカス設定処理
                        this.boolMoveFocusControl();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetGenba", ex1);
                this.logic.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGenba", ex);
                this.logic.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理
        /// </summary>
        public void GenbaPopupAfter()
        {
            if (!this.logic.isInputError && this.GENBA_CD.Text == this.beforeGenba)
            {
                this.GENBA_CD.Text = this.beforeGenba;
                this.GENBA_NAME.Text = this.beforeGenbaName;
            }
            else
            {
                this.SetGenba();
                this.GENBA_CD.Focus();
            }

            this.ControlEnterForPopUpAfter(this.GENBA_CD);
        }

        /// <summary>
        /// 現場ポップアップ表示後の処理
        /// </summary>
        public void GenbaPopupAfterExecute(object sender, DialogResult result)
        {
            if (result != DialogResult.Yes && result != DialogResult.OK)
                return;

            if (!this.logic.isInputError && this.GENBA_CD.Text == this.beforeGenba)
            {
                this.GENBA_CD.Text = this.beforeGenba;
                this.GENBA_NAME.Text = this.beforeGenbaName;
            }
            else
            {
                this.SetGenba();
                this.GENBA_CD.Focus();
            }

            this.ControlEnterForPopUpAfter(this.GENBA_CD);
        }

        private string beforeGenba = "";
        private string beforeGenbaName = "";

        /// <summary>
        /// 現場ポップアップ表示前の処理
        /// </summary>
        public void GenbaPopupBefore()
        {
            // ポップアップからの入力
            this.popupFlg = true;

            // フォーカスアウトせずに再度検索ポップアップを表示した際、値が変更されないための対応
            Control_Enter(this.GENBA_CD, null);
            this.beforeGenba = this.GENBA_CD.Text;
            this.beforeGenbaName = this.GENBA_NAME.Text;
            //this.GENBA_CD.Text = "";
        }

        /// <summary>
        ///  取引先エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_Enter(object sender, EventArgs e)
        {
            // 取引先を取得
            bool catchErr = true;
            var torihikisaki = this.logic.GetTorihikisaki(this.TORIHIKISAKI_CD.Text, out catchErr);
            if (torihikisaki != null)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)torihikisaki.SHOKUCHI_KBN;
            }
        }

        /// <summary>
        /// 業者エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 業者を取得
            bool catchErr = true;
            var gyousha = this.logic.GetGyousha(this.GYOUSHA_CD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != gyousha)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)gyousha.SHOKUCHI_KBN;
            }
        }

        /// <summary>
        /// 現場エンターイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            // 業者、現場の前回値を取得
            dicControl[this.GYOUSHA_CD.Name] = this.GYOUSHA_CD.Text;
            dicControl[this.GENBA_CD.Name] = this.GENBA_CD.Text;

            // 現場を取得
            bool catchErr = true;
            var genba = this.logic.GetGenba(this.GENBA_CD.Text, this.GYOUSHA_CD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            if (null != genba)
            {
                // 諸口区分の前回値を取得
                this.oldShokuchiKbn = (bool)genba.SHOKUCHI_KBN;
            }
        }

        // No.4255-->
        /// <summary>
        /// 品名設定ポップアップ終了後処理
        /// </summary>
        public void PopupAfter_HINMEI_CD(ICustomControl control, System.Windows.Forms.DialogResult result)
        {
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var targetRow = this.dgvDetail.CurrentRow;
                if (targetRow != null)
                {
                    targetRow.Cells["DENPYOU_KBN_CD"].Value = string.Empty; // 伝票区分をクリア
                }
            }
        }
        // No.4255<--

        #region 単価と金額の活性/非活性制御
        /// <summary>
        /// 単価と金額の活性/非活性制御
        /// </summary>
        /// <param name="rowIndex"></param>
        internal void SetIchranReadOnly(int rowIndex)
        {
            LogUtility.DebugMethodStart(rowIndex);

            if (rowIndex < 0)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            if (this.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG ||
                this.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                // 参照モード、削除モード時にはRaadOnly設定を変更しない
                LogUtility.DebugMethodEnd();
                return;
            }

            var row = this.dgvDetail.Rows[rowIndex];

            if ((row.Cells["TANKA"].Value == null || string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString())) &&
                (row.Cells["HINMEI_KINGAKU"].Value == null || string.IsNullOrEmpty(row.Cells["HINMEI_KINGAKU"].Value.ToString())))
            {
                // 「単価」、「金額」どちらも空の場合、両方操作可
                this.dgvDetail.Rows[rowIndex].Cells["TANKA"].ReadOnly = false;
                this.dgvDetail.Rows[rowIndex].Cells["HINMEI_KINGAKU"].ReadOnly = false;
            }
            else if (row.Cells["TANKA"].Value != null && !string.IsNullOrEmpty(row.Cells["TANKA"].Value.ToString()))
            {
                // 「単価」のみ入力済みの場合、「金額」操作不可
                this.dgvDetail.Rows[rowIndex].Cells["TANKA"].ReadOnly = false;
                this.dgvDetail.Rows[rowIndex].Cells["HINMEI_KINGAKU"].ReadOnly = true;
            }
            else if (row.Cells["HINMEI_KINGAKU"].Value != null && !string.IsNullOrEmpty(row.Cells["HINMEI_KINGAKU"].Value.ToString()))
            {
                // 「金額」のみ入力済みの場合、「単価」操作不可
                this.dgvDetail.Rows[rowIndex].Cells["TANKA"].ReadOnly = true;
                this.dgvDetail.Rows[rowIndex].Cells["HINMEI_KINGAKU"].ReadOnly = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 明細全体に単価と金額の活性/非活性制御
        /// </summary>
        /// <param name="rowIndex"></param>
        internal void SetIchranReadOnlyForAll()
        {
            LogUtility.DebugMethodStart();

            this.dgvDetail.Rows.Cast<DataGridViewRow>()
                .Where(w => !w.IsNewRow).ToList()
                .ForEach(r => this.SetIchranReadOnly(r.Index));

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region Communicate InxsSubApplication

        internal void REQUEST_INXS_BUTTON_Click(object sender, EventArgs e)
        {
            if (this.logic.dtPickupRequestInxs != null && this.logic.dtPickupRequestInxs.Rows.Count > 0)
            {
                RemoteAppCls remoteAppCls = new RemoteAppCls();
                var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                {
                    TransactionId = transactionId,
                    ReferenceID = this.UKETSUKE_NUMBER.Text
                });

                var args = new
                {
                    UketsukeSystemId = this.logic.dtPickupRequestInxs.Rows[0]["UKETSUKE_SYSTEM_ID"].ToString(),
                    Token = token,
                    ShougunParentWindowName = this.logic.parentForm.Text,
                    SagyouDate = this.SAGYOU_DATE.Value != null ? ((DateTime)this.SAGYOU_DATE.Value).ToString("yyyy-MM-dd") : string.Empty
                };
                FormManager.OpenFormSubApp("S008", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, new JavaScriptSerializer().Serialize(args));
            }
        }

        internal void ParentForm_OnReceiveMessageEvent(string message)
        {
            try
            {
                if (!r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                {
                    return;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    var arg = JsonUtility.DeserializeObject<CommunicateAppDto>(message);
                    if (arg != null)
                    {
                        var msgDto = (CommunicateAppDto)arg;
                        var token = JsonUtility.DeserializeObject<CommunicateTokenDto>(msgDto.Token);
                        if (token != null)
                        {
                            var tokenDto = (CommunicateTokenDto)token;
                            if (tokenDto.TransactionId == this.transactionId
                                && tokenDto.ReferenceID != null && tokenDto.ReferenceID.ToString() == this.UKETSUKE_NUMBER.Text)
                            {
                                if (msgDto.Args.Length > 0 && msgDto.Args[0] != null)
                                {
                                    var responeDto = JsonUtility.DeserializeObject<InxsSubAppResponseDto>(msgDto.Args[0].ToString());
                                    if (responeDto != null)
                                    {
                                        if (responeDto.RequestStatus == CommonConst.RequestStatusInxs.DISCARD_VALUE
                                            && responeDto.ExecuteDeleteShougunDenpyou)
                                        {
                                            if (this.logic.LogicalDeleteData())
                                            {
                                                this.logic.CloseForm();
                                            }
                                            return;
                                        }
                                        var inxsCls = new InxsClass();
                                        var requestStatusInfo = inxsCls.GetRequestStatusInfo(responeDto.RequestStatus);

                                        if (requestStatusInfo != null)
                                        {
                                            this.REQUEST_INXS_BUTTON.Text = requestStatusInfo.DisplayText;
                                            this.REQUEST_INXS_BUTTON.BackColor = requestStatusInfo.BackColor;
                                            this.REQUEST_INXS_BUTTON.ForeColor = requestStatusInfo.ForeColor;
                                            this.REQUEST_INXS_BUTTON.Visible = true;
                                        }

                                        if (this.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && responeDto.RequestStatus == CommonConst.RequestStatusInxs.CONFIRMED_VALUE)
                                        {
                                            if (responeDto.Detail != null && responeDto.Detail.Any())
                                            {
                                                foreach (var responseDetail in responeDto.Detail)
                                                {
                                                    var row = this.dgvDetail.Rows.Cast<DataGridViewRow>()
                                                                        .Where(r => !r.IsNewRow
                                                                                    && (r.Cells["ROW_NO"].Value != null && int.Parse(r.Cells["ROW_NO"].Value.ToString()) == responseDetail.RowNo)
                                                                                    && (r.Cells["HINMEI_CD"].Value != null && r.Cells["HINMEI_CD"].Value.ToString() == responseDetail.HinmeiCd)
                                                                              ).FirstOrDefault();
                                                    if (row != null)
                                                    {
                                                        row.Cells["SUURYOU"].Value = this.logic.SuuryouAndTankFormat(responseDetail.Suuryou, this.logic.sysInfoEntity.SYS_SUURYOU_FORMAT);
                                                    }
                                                }
                                            }

                                            if (responeDto.IsUpdateSagyouDate)
                                            {
                                                var sagyuoDate = new DateTime();
                                                var kakuteiDate = new DateTime();
                                                if ((string.IsNullOrEmpty(this.SAGYOU_DATE.Text) || DateTime.TryParse(this.SAGYOU_DATE.Text, out sagyuoDate))
                                                    && DateTime.TryParse(responeDto.SagyouDate, out kakuteiDate)
                                                    && DateTime.Compare(sagyuoDate, kakuteiDate) != 0)
                                                {
                                                    this.SAGYOU_DATE.Value = kakuteiDate;
                                                }
                                            }
                                            this.logic.CalcAllDetailAndTotal();
                                            this.logic.GetInxsRequestData();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        internal void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSubAppForm();
        }

        private void CloseSubAppForm()
        {
            try
            {
                RemoteAppCls remoteAppCls = new RemoteAppCls();
                var token = remoteAppCls.GenerateToken(new CommunicateTokenDto()
                {
                    TransactionId = transactionId,
                    ReferenceID = this.UketsukeNumber
                });
                var closeFromDto = new CloseFormDto()
                {
                    FormID = "S008",
                    Token = token,
                    Type = ExternalConnection.CommunicateLib.Enums.NotificationType.CloseForm,
                    Args = null
                };
                remoteAppCls.CloseForm(Constans.StartFormText, closeFromDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }

        #endregion

        #region CongBinh 20210713 #152801
        internal List<string> ListSagyouBi = null;
        internal DateTime OutDate;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSayouDate_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var callForm = new SagyouBiPopup.UIForm();
            var headerForm = new SagyouBiPopup.UIHeader();
            callForm.GyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            callForm.GyoushaName = this.NIOROSHI_GYOUSHA_NAME.Text;
            callForm.GenbaCd = this.NIOROSHI_GENBA_CD.Text;
            callForm.GenbaName = this.NIOROSHI_GENBA_NAME.Text;
            callForm.UnpanGyoushaCd = this.UNPAN_GYOUSHA_CD.Text;
            callForm.UnpanGyoushaName = this.UNPAN_GYOUSHA_NAME.Text;
            callForm.ShashuCd = this.SHASHU_CD.Text;
            callForm.ShashuName = this.SHASHU_NAME.Text;
            callForm.SharyouCd = this.SHARYOU_CD.Text;
            callForm.SharyouName = this.SHARYOU_NAME.Text;
            callForm.UntenshaCd = this.UNTENSHA_CD.Text;
            callForm.UntenshaName = this.UNTENSHA_NAME.Text;
            callForm.Sagyoubi = this.ListSagyouBi;
            callForm.InOutDate = this.OutDate;
            var popForm = new Shougun.Core.Common.BusinessCommon.Base.BaseForm.BasePopForm(callForm, headerForm);
            var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
            if (!isExistForm)
            {
                popForm.ShowDialog();
                if (callForm.Sagyoubi != null)
                {
                    this.SAGYOU_DATE_TMP.Visible = false;
                    this.OutDate = callForm.InOutDate;
                    this.ListSagyouBi = callForm.Sagyoubi;
                    this.SAGYOU_DATE.Enabled = true;
                    if (this.ListSagyouBi.Count == 0)
                    {
                        this.SAGYOU_DATE.Text = string.Empty;
                    }
                    else if (this.ListSagyouBi.Count == 1)
                    {
                        this.SAGYOU_DATE.Text = this.ListSagyouBi[0];
                    }
                    else
                    {
                        this.SAGYOU_DATE.Text = this.ListSagyouBi[0];
                        this.SAGYOU_DATE.Enabled = false;
                        this.SAGYOU_DATE_TMP.Visible = true;
                    }
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rirekeIchiran_DoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.RirekeSet();
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rirekeIchiran_RowLeave(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.rirekeIchiran.Focused)
            {
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].ReadOnly = false;
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].Style.ForeColor = Color.White;
            }
            else
            {
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].ReadOnly = true;
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].Style.ForeColor = Color.Black;
                this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].Style.SelectionForeColor = Color.Black;
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rirekeIchiran_RowEnter(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].ReadOnly = true;
            this.rirekeIchiran.Rows[e.RowIndex]["CELL_RIREKE_SS"].Style.ForeColor = Color.Black;
            LogUtility.DebugMethodEnd();
        }
        #endregion

        //20211230 Thanh 158916 s
        /// <summary>
        /// UIForm_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dgvDetail.CurrentCell != null && this.dgvDetail.Columns[this.dgvDetail.CurrentCell.ColumnIndex].Name == "TANKA")
            {
                if (this.dgvDetail.CurrentCell.IsInEditMode)
                {
                    if (e.KeyChar == (Char)Keys.Space)
                    {
                        this.OpenTankaRireki(this.dgvDetail.CurrentRow.Index);
                    }
                }
            }
        }

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
            string HinmeiCd = Convert.ToString(this.dgvDetail.Rows[index].Cells["HINMEI_CD"].Value);
            string UnitCd = Convert.ToString(this.dgvDetail.Rows[index].Cells["UNIT_CD"].Value);

            if (!string.IsNullOrEmpty((this.logic.headerForm).KYOTEN_CD.Text))
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
            TankaRirekiIchiranUIForm tankaForm = new TankaRirekiIchiranUIForm(WINDOW_ID.T_TANKA_RIREKI_ICHIRAN, "G015",
                kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, HinmeiCd);
            tankaForm.StartPosition = FormStartPosition.CenterParent;
            tankaForm.ShowDialog();
            tankaForm.Dispose();
            if (tankaForm.dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tankaForm.returnTanka.IsNull)
                {
                    this.dgvDetail.EditingControl.Text = string.Empty;
                }
                else
                {
                    this.dgvDetail.EditingControl.Text = tankaForm.returnTanka.Value.ToString(this.logic.sysInfoEntity.SYS_TANKA_FORMAT);
                }

                if (!UnitCd.Equals(tankaForm.returnUnitCd))
                {
                    if (string.IsNullOrEmpty(tankaForm.returnUnitCd))
                    {
                        this.dgvDetail.Rows[index].Cells["UNIT_CD"].Value = string.Empty;
                        this.dgvDetail.Rows[index].Cells["UNIT_NAME_RYAKU"].Value = string.Empty;
                    }
                    else
                    {
                        var targetUnit = this.logic.GetUnit(Convert.ToInt16(tankaForm.returnUnitCd));
                        if (targetUnit.Length >= 0)
                        {
                            this.dgvDetail.Rows[index].Cells["UNIT_CD"].Value = targetUnit[0].UNIT_CD.ToString();
                            this.dgvDetail.Rows[index].Cells["UNIT_NAME_RYAKU"].Value = targetUnit[0].UNIT_NAME_RYAKU.ToString();
                        }
                    }
                }
            }
        }
        //20211230 Thanh 158916 e

    }
}
