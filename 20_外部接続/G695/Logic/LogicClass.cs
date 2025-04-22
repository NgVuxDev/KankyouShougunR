using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数

        private static readonly string HAISHA_KBN_TEIKI = "1";
        private static readonly string HAISHA_KBN_SPOT = "2";

        /// <summary>伝票区別:1.収集受付</summary>
        private static readonly string DENPYOU_ATTR_SS = "1";
        /// <summary>伝票区別:2.出荷受付</summary>
        private static readonly string DENPYOU_ATTR_SK = "2";
        /// <summary>伝票区別:3.定期</summary>
        private static readonly string DENPYOU_ATTR_TEIKI = "3";

        /// <summary>現場連携状況:未連携</summary>
        private static readonly string RENKEI_JYOUKYOU_MIRENKEI = "未連携";
        /// <summary>現場連携状況:済</summary>
        private static readonly string RENKEI_JYOUKYOU_ZUMI = "済";

        /// <summary>
        /// 明細部のカラム名
        /// </summary>
        /// <summary></summary>
        private static readonly string TAISHO = "TAISHO";
        /// <summary></summary>
        private static readonly string DATA_TAISHO = "DATA_TAISHO";
        /// <summary></summary>
        private static readonly string DATA_ROW_NUMBER = "DATA_ROW_NUMBER";
        /// <summary></summary>
        private static readonly string DATA_NIOROSHI = "DATA_NIOROSHI";
        /// <summary></summary>
        private static readonly string DATA_UKETSUKE_NUMBER = "DATA_UKETSUKE_NUMBER";
        /// <summary></summary>
        private static readonly string DATA_GENCHAKU_TIME_NAME = "DATA_GENCHAKU_TIME_NAME";
        /// <summary></summary>
        private static readonly string DATA_GENCHAKU_TIME = "DATA_GENCHAKU_TIME";
        /// <summary></summary>
        private static readonly string DATA_TEIKI_HAISHA_NUMBER = "DATA_TEIKI_HAISHA_NUMBER";
        /// <summary></summary>
        private static readonly string DATA_RENKEI_JYOUKYOU = "DATA_RENKEI_JYOUKYOU";
        /// <summary></summary>
        private static readonly string DATA_DELIVERY_DATE = "DATA_DELIVERY_DATE";
        /// <summary></summary>
        private static readonly string DATA_NIZUMI_NIOROSHI_FLG = "DATA_NIZUMI_NIOROSHI_FLG";

        #endregion

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader HeaderForm { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// コース名称データ
        /// </summary>
        internal M_COURSE_NAME[] mCourseNameAll { get; set; }

        /// <summary>
        /// T_LOGI_LINK_STATUS.LINK_STATUS保持用
        /// </summary>
        private int link_status { get; set; }

        /// <summary>
        /// システム日付保持
        /// </summary>
        internal DateTime date_today { get; set; }

        #region 画面コントロール系
        /// <summary>
        /// 配車区分(1.定期配車)
        /// </summary>
        const int TEIKI_KBN = 1;

        /// <summary>
        /// 配車区分(2.スポット)
        /// </summary>
        const int SPOT_KBN = 2;

        /// <summary>
        /// 配車区分フラグ
        /// </summary>
        internal int haisha_kbn { get; set; }

        #endregion

        #region Dao
        /// <summary>
        /// 社員Dao
        /// </summary>
        private IM_SHAINDao shainDao;
        /// <summary>
        /// 配送計画DAO
        /// </summary>
        private LogiDeliveryDAO logiDeliveryDao;

        /// <summary>
        /// 配送計画明細DAO
        /// </summary>
        private LogiDeliveryDetailDAO logiDeliveryDetailDao;

        /// <summary>
        /// 配送計画候補DAO
        /// </summary>
        private LogiLinkStatusDAO logiLinkStatusDao;

        /// <summary>
        /// 配送計画候補DAO
        /// </summary>
        private DeliveryPlanDAO deliveryPlanDao;

        /// <summary>
        /// 配送データDAO
        /// </summary>
        private DeliveryDataDAO deliveryDataDao;

        /// <summary>
        /// 送信データDAO
        /// </summary>
        private DeliveryDataRequestDao deliveryDataRequestDao;

        /// <summary>
        /// 現場_デジタコ連携情報DAO
        /// </summary>
        private IM_GENBA_DIGIDao genbaDigiDao;

        /// <summary>
        /// ロジこん連携用Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        private IS_LOGI_CONNECTDao logiConnectDao;
        private IS_SYSTEC_TOKENDao systecTokenDao;

        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;

        #endregion

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件保持用のDTO
        /// </summary>
        internal SearchDeliveryPlanDTO SearchDTO { get; set; }

        /// <summary>
        /// 配車区分：定期配車で検索されたか判定
        /// </summary>
        private bool SearchByTeiki
        {
            get
            {
                return this.SearchDTO.HAISHA_KBN.Equals(HAISHA_KBN_TEIKI);
            }
        }

        /// <summary>
        /// 配車区分：スポットで検索されたか判定
        /// </summary>
        private bool SearchBySpot
        {
            get
            {
                return this.SearchDTO.HAISHA_KBN.Equals(HAISHA_KBN_SPOT);
            }
        }

        /// <summary>
        /// 登録用DTO
        /// </summary>
        internal LogiDeliveryDTO RegistDTO { get; set; }

        /// <summary>
        /// 修正、削除モード時の表示用DTO
        /// </summary>
        private LogiDeliveryDTO DisplayDTO { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        internal string SYSTEM_ID { get; set; }

        /// <summary>
        /// F4連携削除ボタン制御
        /// </summary>
        /// <remarks>
        /// 参照モード時の連携削除の活性・非活性判定用
        /// </remarks>
        internal bool F4_BTN_UNLOCK { get; set; }

        /// <summary>
        /// 荷積・荷降挿入ボタン押下判定フラグ
        /// </summary>
        /// <remarks>true:荷積挿入ボタン押下, false:荷降挿入ボタン押下</remarks>
        internal bool AddNizumiGenbaFlg { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            //DAO(一覧用)
            this.logiDeliveryDao = DaoInitUtility.GetComponent<LogiDeliveryDAO>();
            this.logiDeliveryDetailDao = DaoInitUtility.GetComponent<LogiDeliveryDetailDAO>();
            this.logiLinkStatusDao = DaoInitUtility.GetComponent<LogiLinkStatusDAO>();
            this.deliveryPlanDao = DaoInitUtility.GetComponent<DeliveryPlanDAO>();
            this.deliveryDataDao = DaoInitUtility.GetComponent<DeliveryDataDAO>();
            this.deliveryDataRequestDao = DaoInitUtility.GetComponent<DeliveryDataRequestDao>();

            //ロジこん連携用Dao
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.logiConnectDao = DaoInitUtility.GetComponent<IS_LOGI_CONNECTDao>();
            this.systecTokenDao = DaoInitUtility.GetComponent<IS_SYSTEC_TOKENDao>();

            //他
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.genbaDigiDao = DaoInitUtility.GetComponent<IM_GENBA_DIGIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();

            //メッセージ用
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <returns></returns>
        internal bool WindowInit(WINDOW_TYPE windowType)
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                // 明細ヘッダーのチェックボックス設定
                this.HeaderCheckBoxSupport();

                // キー入力設定
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.parentForm.headerForm;

                // システム日付保持
                this.date_today = DateTime.Parse(this.parentForm.sysDate.ToString("yyyy/MM/dd"));

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 検索条件の初期化処理
                this.SetInitialRenkeiCondition();

                // 処理モード別画面初期化
                this.ModeInit(windowType);

                // 初期フォーカス位置を設定します
                this.form.HAISHA_KBN.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }
        #endregion

        #region ボタン設定
        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント初期化
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //送信連携ボタン(F1)イベント作成
            this.parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            //送信連携ボタン(F2)イベント作成
            this.parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);
            //削除連携(F4)イベント作成
            this.parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            // 一覧(F6)イベント作成
            this.parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);
            //条件クリア(F7)イベント作成
            this.parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);
            //検索ボタン(F8)イベント作成
            this.parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            //仮登録ボタン(F9)イベント作成
            this.form.C_Regist(this.parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            this.parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
            //閉じるボタン(F12)イベント作成
            this.parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // [1]組込ボタンイベント作成
            this.parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
            // [2]順番整列ボタンイベント作成
            this.parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
            // [3]荷積挿入ボタンイベント作成
            this.parentForm.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);
            // [4]荷降挿入ボタンイベント作成
            this.parentForm.bt_process4.Click += new EventHandler(this.form.bt_process4_Click);

            //配車区分イベント作成
            this.form.HAISHA_KBN.TextChanged += new EventHandler(this.form.HAISHA_KBN_TextChanged);
            this.form.HAISHA_KBN.Leave += new EventHandler(this.form.HAISHA_KBN_Leave);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 抽出条件初期化
        /// <summary>
        /// 抽出条件の初期化
        /// </summary>
        internal void SetInitialRenkeiCondition()
        {
            // 事前に必要情報を取得
            var sysInfo = this.sysInfoDao.GetAllDataForCode("0");

            // システム設定から初期値設定
            this.HeaderForm.CARRY_OVER_NEXT_DAY.Text = sysInfo.DIGI_CARRY_ORVER_NEXT_DAY.ToString();

            // 配車区分より先に作業日範囲を設定しないとCreateSearchDtoでエラーするケースがある
            // 前回値保持 配車区分/作業日FromTo
            if (string.IsNullOrEmpty(this.form.beforSagyouBegin))
            {
                this.form.SAGYOU_BEGIN.Value = this.parentForm.sysDate.Date;
            }
            else
            {
                this.form.SAGYOU_BEGIN.Value = this.form.beforSagyouBegin;
            }

            if (string.IsNullOrEmpty(this.form.beforSagyouEnd))
            {
                this.form.SAGYOU_END.Value = this.parentForm.sysDate.Date;
            }
            else
            {
                this.form.SAGYOU_END.Value = this.form.beforSagyouEnd;
            }

            if (string.IsNullOrEmpty(this.form.beforHaishaKBN))
            {
                this.form.HAISHA_KBN.Text = HAISHA_KBN_TEIKI;
            }
            else
            {
                this.form.HAISHA_KBN.Text = this.form.beforHaishaKBN;
            }
            this.haisha_kbn = int.Parse(this.form.HAISHA_KBN.Text);

            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_RNAME.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_RNAME.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME.Text = string.Empty;
            this.form.SHAIN_CD.Text = string.Empty;
            this.form.SHAIN_NAME.Text = string.Empty;
            this.form.DELIVERY_NO.Text = string.Empty;
            this.form.DATA_OUTPUT.Text = string.Empty;
            this.form.DELIVERY_NAME.Text = string.Empty;

            // 非表示項目
            this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIZUMI_GYOUSHA_NAME.Text = string.Empty;
            this.form.NIZUMI_GENBA_CD.Text = string.Empty;
            this.form.NIZUMI_GENBA_NAME.Text = string.Empty;
        }
        #endregion

        #region 画面初期化処理(モード別)
        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType">画面区分</param>
        internal void ModeInit(WINDOW_TYPE windowType)
        {
            this.ControlUnLock(true);

            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNew();
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.WindowInitUpdate(windowType);
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(windowType);
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    this.WindowInitReference(windowType);
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew();
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        private void WindowInitNew()
        {
            try
            {
                this.SetInitialRenkeiCondition();

                this.parentForm.bt_func4.Enabled = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNew", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        private void WindowInitUpdate(WINDOW_TYPE windowType)
        {
            try
            {
                //ヘッダーやファンクションをロック
                this.ControlUnLock(false);

                //実績作成済みかどうかで入力制御が必要か？
                this.form.Ichiran1.Columns[DATA_TAISHO].ReadOnly = false;
                this.form.Ichiran1.Columns[DATA_ROW_NUMBER].ReadOnly = false;

                // 検索結果を画面に設定
                this.SetWindowData(windowType);

                this.parentForm.bt_func1.Enabled = true;
                this.parentForm.bt_func4.Enabled = true;

                // 未送信はF9使用可
                // 送信済(未受信)はF9使用不可
                // 受信済(完了)のデータは参照モードで開くためこのメソッドは通らない
                if (this.link_status == 2)
                {
                    this.parentForm.bt_func9.Enabled = false;
                }
                else
                {
                    this.parentForm.bt_func9.Enabled = true;
                }

                this.parentForm.bt_func12.Enabled = true;
                this.parentForm.bt_process2.Enabled = true;
                this.parentForm.bt_process3.Enabled = true;
                this.parentForm.bt_process4.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        private void WindowInitDelete(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                //ヘッダーやファンクションをロック
                this.ControlUnLock(false);

                // データをDBから取得
                this.SetWindowData(windowType);

                this.parentForm.bt_func9.Enabled = true;
                this.parentForm.bt_func12.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitDelete", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        private void WindowInitReference(WINDOW_TYPE windowType)
        {
            try
            {
                //ヘッダーやファンクションをロック
                this.ControlUnLock(false);

                // 検索結果を画面に設定
                this.SetWindowData(windowType);

                // 配送計画一覧の修正ボタン押下時は「削除連携」は押下可能
                if (this.F4_BTN_UNLOCK)
                {
                    this.parentForm.bt_func4.Enabled = true;
                }

                this.parentForm.bt_func12.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }

        /// <summary>
        /// マスタ情報を取得して画面に設定
        /// </summary>
        private void SetWindowData(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var result = GetDeliveryDataForSystemId();
                this.form.Ichiran1.DataSource = result;

                this.form.HAISHA_KBN.Text = result[0].HAISHA_KBN;
                this.form.DELIVERY_NO.Text = result[0].DELIVERY_NO;
                this.form.DELIVERY_NAME.Text = result[0].DELIVERY_NAME;
                this.link_status = result[0].LINK_STATUS;
                if (this.link_status == 1)
                {
                    this.form.DATA_OUTPUT.Text = "未";
                }
                else
                {
                    this.form.DATA_OUTPUT.Text = "済";
                }
                //SearchByTeikiでエラーしないように読み込み
                this.SearchDTO = CreateSearchDto();

                //項目の可視、不可視設定
                this.ColumnVisibleData();

                this.ColumnReadOnly();
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetWindowData", ex2);
                this.msgLogic.MessageBoxShow("E093");
                return;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                this.msgLogic.MessageBoxShow("E245");
                return;
            }
        }
        #endregion

        #region コントロールの活性制御
        /// <summary>
        /// 全入力コントロールの活性制御
        /// </summary>
        /// <param name="isUnLock">true:活性,false:非活性</param>
        private void ControlUnLock(bool isUnLock)
        {
            //抽出条件
            this.form.HAISHA_KBN.Enabled = isUnLock;
            this.form.radbtnTeiki.Enabled = isUnLock;
            this.form.radbtnSpot.Enabled = isUnLock;
            this.form.SAGYOU_BEGIN.Enabled = isUnLock;
            this.form.SAGYOU_END.Enabled = isUnLock;
            this.form.GYOUSHA_CD.Enabled = isUnLock;
            this.form.GENBA_CD.Enabled = isUnLock;
            this.form.UNPAN_GYOUSHA_CD.Enabled = isUnLock;
            this.form.SHARYOU_CD.Enabled = isUnLock;
            this.form.SHASHU_CD.Enabled = isUnLock;
            this.form.SHAIN_CD.Enabled = isUnLock;
            this.form.DELIVERY_NAME.Enabled = isUnLock;

            //ファンクション
            this.parentForm.bt_func1.Enabled = isUnLock;
            this.parentForm.bt_func4.Enabled = isUnLock;
            this.parentForm.bt_func7.Enabled = isUnLock;
            this.parentForm.bt_func8.Enabled = isUnLock;
            this.parentForm.bt_func9.Enabled = isUnLock;
            this.parentForm.bt_func12.Enabled = isUnLock;
            this.parentForm.bt_process1.Enabled = isUnLock;
            this.parentForm.bt_process2.Enabled = isUnLock;
            this.parentForm.bt_process3.Enabled = isUnLock;
            this.parentForm.bt_process4.Enabled = isUnLock;

            //明細
            this.form.Ichiran1.Columns[DATA_TAISHO].ReadOnly = !isUnLock;
            this.form.Ichiran1.Columns[DATA_ROW_NUMBER].ReadOnly = !isUnLock;
        }
        #endregion

        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                // 抽出条件読み込み
                this.SearchDTO = CreateSearchDto();

                List<DeliveryPlanDTO> dtoList;
                if (this.SearchByTeiki)
                {
                    dtoList = deliveryPlanDao.GetDeliveryPlanForTeiki(this.SearchDTO);
                }
                else
                {
                    dtoList = deliveryPlanDao.GetDeliveryPlanForSpot(this.SearchDTO);
                }
                this.form.customDataGridView1.DataSource = dtoList;

                // 表示切替
                this.ColumnVisiblePlan();

                return dtoList.Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245");
                ret_cnt = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret_cnt;
        }
        #endregion

        #region データ送信
        /// <summary>
        /// データの送信
        /// JSON絡みの処理
        /// 登録用に作成したEntityを元に、必要な情報をクエリで引っ張る
        /// </summary>
        /// <param name="ReqMethod">HTTPメソッド</param>
        /// <returns></returns>
        internal int JsonConnection(HTTP_METHOD ReqMethod)
        {
            int ret_cnt = 0;

            try
            {
                LogUtility.DebugMethodStart();

                //body作成　この途中で未連携データや不備があったら抜ける

                // 事前に必要情報を取得
                var sysInfo = this.sysInfoDao.GetAllDataForCode("0");

                var logic = new LogiLogic();

                //DELETEで使用するURL用変数初期化
                var apiURL = string.Empty;

                //URLとRequestBodyを各々作る
                var deliveryConnect = this.logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_DELIVERY_PLANS);
                if (deliveryConnect == null)
                {
                    this.msgLogic.MessageBoxShowError("ロジコン接続管理情報がありません。");
                    return -1;
                }

                //配送計画情報作成
                var deliveryplanDto = new INFO_DELIVERY_PLANS();
                //配送明細情報作成
                deliveryplanDto.Delivery_Detail = new List<INFO_DELIVERY_DETAIL_PLANS>();

                SqlInt64 SysId = 0;
                SqlInt64 SysDetId = 0;

                //配送計画情報Dto呼出し
                var logiDto = this.RegistDTO.LOGI_DELIVERY;

                //配送明細情報Dto呼出し
                foreach (var dto in this.RegistDTO.LOGI_DELIVERY_DETAIL_LIST)
                {
                    if (dto.NIZUMI_NIOROSHI_FLG)
                    {
                        #region 荷積荷降データ
                        // 荷積荷降データの場合、必ず0になるので個別にリクエストデータを作る必要がある
                        DeliveryDataDTO dto2 = null;

                        // DGVの情報から行が一致するデータを無理矢理引っ張ってくる
                        var dtos = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
                        dto2 = dtos.Where(n => n.TAISHO && n.ROW_NUMBER.Equals(dto.DELIVERY_ORDER.ToString())).FirstOrDefault();
                        if (dto2 == null)
                        {
                            return -1;
                        }

                        // 車輌連携マスタ情報チェック
                        if (String.IsNullOrEmpty(Convert.ToString(dto2.DIGI_SHARYOU_CD)) != false)
                        {
                            this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の車輌が含まれています。" + Environment.NewLine + "デジタコマスタ連携画面でマスタ登録を行ってください。");
                            return -1;
                        }

                        // 外部連携マスタ情報チェック
                        if (String.IsNullOrEmpty(Convert.ToString(dto2.POINT_ID)) != false)
                        {
                            this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の現場が含まれています。" + Environment.NewLine + "外部連携現場入力画面でマスタ登録を行ってください。");
                            return -1;
                        }

                        // 配送計画情報セット
                        deliveryplanDto.Company_Id = sysInfo.DIGI_CORP_ID;
                        deliveryplanDto.Car_Id = dto2.DIGI_SHARYOU_CD.ToString();
                        deliveryplanDto.Delivery_Date = DateTime.Parse(logiDto.DELIVERY_DATE.ToString()).ToString("yyyyMMdd");
                        deliveryplanDto.Delivery_No = int.Parse(logiDto.DELIVERY_NO.ToString());
                        deliveryplanDto.Delivery_Name = logiDto.DELIVERY_NAME.ToString();

                        // 配送明細情報作成
                        var detail = new INFO_DELIVERY_DETAIL_PLANS();

                        // 配送明細情報セット
                        detail.Delivery_Detail_No = int.Parse(dto.DELIVERY_ORDER.ToString());
                        detail.Point_Id = dto2.POINT_ID.ToString();
                        detail.Arrival_Time = string.Empty;
                        detail.Departure_Time = string.Empty;
                        detail.Delivery_Order = int.Parse(dto.DELIVERY_ORDER.ToString());
                        // 荷積荷降データは配送品明細情報ナシ
                        detail.Goods_Detail = null;

                        deliveryplanDto.Delivery_Detail.Add(detail);
                        #endregion
                    }
                    else
                    {
                        #region 通常データ
                        // 通常データ
                        DataTable dt = null;

                        if (this.SearchByTeiki)
                        {
                            //定期の場合
                            SysId = dto.REF_SYSTEM_ID;
                            SysDetId = dto.REF_DETAIL_SYSTEM_ID;
                            dt = this.deliveryDataRequestDao.GetRequestDataTeikiForSql(SysId, SysDetId);
                        }
                        else
                        {
                            //スポットの場合
                            SysId = dto.REF_SYSTEM_ID;
                            dt = this.deliveryDataRequestDao.GetRequestDataSpotForSql(SysId);
                        }

                        if (dt.Rows.Count == 0)
                        {
                            return -1;
                        }

                        //マスタ連携が不足していたらアラート出して送信停止
                        if (String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["CAR_ID"])) != false)
                        {
                            this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の車輌が含まれています。" + Environment.NewLine + "デジタコマスタ連携画面でマスタ登録を行ってください。");
                            return -1;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["POINT_ID"])) != false)
                        {
                            this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の現場が含まれています。" + Environment.NewLine + "外部連携現場入力画面でマスタ登録を行ってください。");
                            return -1;
                        }

                        //配送計画情報セット
                        deliveryplanDto.Company_Id = sysInfo.DIGI_CORP_ID;
                        deliveryplanDto.Car_Id = dt.Rows[0]["CAR_ID"].ToString();
                        deliveryplanDto.Delivery_Date = DateTime.Parse(logiDto.DELIVERY_DATE.ToString()).ToString("yyyyMMdd");
                        deliveryplanDto.Delivery_No = int.Parse(logiDto.DELIVERY_NO.ToString());
                        deliveryplanDto.Delivery_Name = logiDto.DELIVERY_NAME.ToString();

                        //配送明細情報作成
                        var detail = new INFO_DELIVERY_DETAIL_PLANS();

                        //配送明細情報セット
                        detail.Delivery_Detail_No = int.Parse(dto.DELIVERY_ORDER.ToString());
                        detail.Point_Id = dt.Rows[0]["POINT_ID"].ToString();

                        // スポット、現着時間有り、翌日持越無し
                        // の場合のみ値をセットする
                        if (this.SearchBySpot
                            && !string.IsNullOrEmpty(dt.Rows[0]["GENCHAKU_TIME"].ToString())
                            && this.HeaderForm.CARRY_OVER_NEXT_DAY.Text == "2")
                        {
                            string sagyouTime = string.Empty;
                            sagyouTime = DateTime.Parse(dt.Rows[0]["GENCHAKU_TIME"].ToString()).ToString("HHmm");

                            detail.Arrival_Time = DateTime.Parse(dt.Rows[0]["ARRIVAL_TIME"].ToString()).ToString("yyyyMMdd") + sagyouTime;
                            detail.Departure_Time = DateTime.Parse(dt.Rows[0]["DEPARTURE_TIME"].ToString()).ToString("yyyyMMdd") + sagyouTime;
                        }
                        else
                        {
                            detail.Arrival_Time = string.Empty;
                            detail.Departure_Time = string.Empty;
                        }

                        detail.Delivery_Order = int.Parse(dto.DELIVERY_ORDER.ToString());

                        detail.Goods_Detail = new List<INFO_GOODS_DETAIL_PLANS>();

                        //クエリで抽出した配送品明細情報をループ
                        foreach (DataRow dr in dt.Rows)
                        {
                            //配送品明細情報作成
                            var goodsdetail = new INFO_GOODS_DETAIL_PLANS();
                            if (String.IsNullOrEmpty(Convert.ToString(dr["GOODS_DETAIL_NO"])) == true
                                || int.Parse(dr["GOODS_DETAIL_NO"].ToString()) == 0)
                            {
                                //配送品明細Noがない＝必須にならない
                                detail.Goods_Detail = null;
                            }
                            else
                            {
                                // 伝種区分CDのチェック
                                var cnt = this.ChkDenshuKBN(dto.DENPYOU_ATTR, SysId, SysDetId);
                                if (cnt != 0)
                                {
                                    this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の品名が含まれています。" + Environment.NewLine + "デジタコマスタ連携画面でマスタ登録を行ってください。");
                                    return -1;
                                }
                                //マスタ連携が不足していたらアラート出して送信停止
                                if (String.IsNullOrEmpty(Convert.ToString(dr["GOODS_ID"])) != false)
                                {
                                    this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の品名が含まれています。" + Environment.NewLine + "デジタコマスタ連携画面でマスタ登録を行ってください。");
                                    return -1;
                                }
                                if (String.IsNullOrEmpty(Convert.ToString(dr["GOODS_UNIT_ID"])) != false)
                                {
                                    this.msgLogic.MessageBoxShowError("デジタコのマスタへ未登録の単位が含まれています。" + Environment.NewLine + "デジタコマスタ連携画面でマスタ登録を行ってください。");
                                    return -1;
                                }

                                //配送品明細情報セット
                                goodsdetail.Goods_Detail_No = int.Parse(dr["GOODS_DETAIL_NO"].ToString());
                                goodsdetail.Goods_Id = dr["GOODS_ID"].ToString();
                                //小数点第二位まで必ず入っていないとエラーする模様
                                goodsdetail.Goods_Count = Convert.ToDecimal(dr["GOODS_COUNT"]).ToString("#.00");
                                goodsdetail.Goods_Unit_Id = dr["GOODS_UNIT_ID"].ToString();
                                detail.Goods_Detail.Add(goodsdetail);
                            }
                        }
                        deliveryplanDto.Delivery_Detail.Add(detail);
                        #endregion
                    }
                }

                if (ReqMethod == HTTP_METHOD.PUT)
                {
                    //配送計画PUT
                    logic.HttpPut(deliveryConnect.URL, deliveryConnect.CONTENT_TYPE, deliveryplanDto);
                }
                else
                {
                    //配送計画DELETE
                    apiURL = string.Format(deliveryConnect.URL + LogiConst.API_PARAMETER_URL_DELIVERY_PLANS, sysInfo.DIGI_CORP_ID, deliveryplanDto.Car_Id, deliveryplanDto.Delivery_Date, deliveryplanDto.Delivery_No);
                    logic.HttpDelete(apiURL, deliveryConnect.CONTENT_TYPE);
                }

                //完了
                ret_cnt++;
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("JsonConnection", ex);
                this.msgLogic.MessageBoxShow("E245");
                ret_cnt = -1;
            }

            LogUtility.DebugMethodEnd();

            return ret_cnt;
        }
        #endregion

        #region 伝種区分チェック
        /// <summary>
        /// 伝種区分のチェックロジック
        /// </summary>
        /// <param name="DenpyouAtter">1: 収集受付 2:出荷受付 3:定期配車</param>
        /// <returns>0:問題無し 1:アラート</returns>
        private int ChkDenshuKBN(SqlInt16 DenpyouAtter, SqlInt64 sysid, SqlInt64 sysdid)
        {
            int cnt = 0;

            try
            {
                string sql = "";

                switch (int.Parse(DenpyouAtter.ToString()))
                {
                    case 1:
                        sql = "SELECT COUNT(ENT.SYSTEM_ID) CHECK_COUNT FROM T_UKETSUKE_SS_ENTRY ENT LEFT JOIN T_UKETSUKE_SS_DETAIL AS DEL ON ENT.SYSTEM_ID = DEL.SYSTEM_ID AND ENT.SEQ = DEL.SEQ"
                            + " WHERE ENT.DELETE_FLG=0 AND ENT.SYSTEM_ID="
                            + sysid
                            + " AND EXISTS (SELECT 1 FROM M_HINMEI WHERE DENSHU_KBN_CD IN (1,2) AND HINMEI_CD=DEL.HINMEI_CD)";
                        break;
                    case 2:
                        sql = "SELECT COUNT(ENT.SYSTEM_ID) CHECK_COUNT FROM T_UKETSUKE_SK_ENTRY ENT LEFT JOIN T_UKETSUKE_SK_DETAIL AS DEL ON ENT.SYSTEM_ID = DEL.SYSTEM_ID AND ENT.SEQ = DEL.SEQ"
                            + " WHERE ENT.DELETE_FLG=0 AND ENT.SYSTEM_ID="
                            + sysid
                            + " AND EXISTS (SELECT 1 FROM M_HINMEI WHERE DENSHU_KBN_CD IN (1,2) AND HINMEI_CD=DEL.HINMEI_CD)";
                        break;
                    case 3:
                        sql = "SELECT COUNT(SHO.SYSTEM_ID) CHECK_COUNT FROM T_TEIKI_HAISHA_ENTRY AS ENT"
                            + " LEFT JOIN T_TEIKI_HAISHA_DETAIL AS DEL ON ENT.SYSTEM_ID = DEL.SYSTEM_ID AND ENT.SEQ = DEL.SEQ"
                            + " LEFT JOIN T_TEIKI_HAISHA_SHOUSAI AS SHO ON DEL.SYSTEM_ID = SHO.SYSTEM_ID AND DEL.SEQ = SHO.SEQ AND DEL.DETAIL_SYSTEM_ID = SHO.DETAIL_SYSTEM_ID "
                            + " WHERE ENT.DELETE_FLG=0 AND ENT.SYSTEM_ID=" + sysid
                            + " AND DEL.DETAIL_SYSTEM_ID=" + sysdid
                            + " AND EXISTS (SELECT 1 FROM M_HINMEI WHERE DENSHU_KBN_CD IN (1,2) AND HINMEI_CD=SHO.HINMEI_CD)";
                        break;
                }


                DataTable dt = this.deliveryDataRequestDao.CheckForStringSql(sql);
                cnt = int.Parse(dt.Rows[0]["CHECK_COUNT"].ToString());
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkDenshuKBN", ex);
                cnt = -1;
            }
            return cnt;
        }
        #endregion

        #region 配送計画候補検索用DTO作成
        /// <summary>
        /// 配送計画候補検索用のDTO作成
        /// </summary>
        /// <returns></returns>
        internal SearchDeliveryPlanDTO CreateSearchDto()
        {
            var dto = new SearchDeliveryPlanDTO();

            dto.HAISHA_KBN = this.form.HAISHA_KBN.Text;
            dto.SAGYOU_DATE_FROM = this.form.SAGYOU_BEGIN.Value.ToString();
            dto.SAGYOU_DATE_TO = this.form.SAGYOU_END.Value.ToString();
            dto.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            dto.GENBA_CD = this.form.GENBA_CD.Text;
            dto.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            dto.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            dto.SHASHU_CD = this.form.SHASHU_CD.Text;
            dto.UNTENSHA_CD = this.form.SHAIN_CD.Text;
            return dto;
        }
        #endregion

        #region 組込ボタン押下チェック
        /// <summary>
        /// 組込ボタン押下時のチェック処理
        /// </summary>
        /// <returns>true:エラー有, false:エラー無</returns>
        internal bool HasErrorCheckKumikomi()
        {
            // 共通チェック
            List<DeliveryPlanDTO> planList = this.form.customDataGridView1.DataSource as List<DeliveryPlanDTO>;
            if (planList == null || planList.Count == 0)
            {
                msgLogic.MessageBoxShowError("配送計画候補が検索結果が存在しません。");
                return true;
            }

            var kumikomiList = planList.Where(n => n.KUMIKOMI);
            if (kumikomiList.Count() == 0)
            {
                msgLogic.MessageBoxShowError("組込対象を選択してください。");
                return true;
            }

            // 定期配車
            if (this.SearchByTeiki)
            {
                if (1 < kumikomiList.Count())
                {
                    msgLogic.MessageBoxShowError("組込対象は１つのみ選択してください。");
                    return true;
                }
            }
            // スポット
            else
            {
                if (1 < kumikomiList.Select(n => n.SHARYOU_CD).Distinct().Count())
                {
                    msgLogic.MessageBoxShowError("同一の車輌のみ選択してください。");
                    return true;
                }

                if (0 < this.form.Ichiran1.Rows.Count)
                {
                    var dataList = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
                    var selectedSharyouCd = dataList.Select(n => n.SHARYOU_CD).First();

                    if (kumikomiList.Any(n => !n.SHARYOU_CD.Equals(selectedSharyouCd)))
                    {
                        msgLogic.MessageBoxShowError("既に選択済みの車輌のみ選択してください。");
                        return true;
                    }
                }
            }

            return false;
        }
        #endregion

        #region 配送データ設定(subF1組込メイン処理)
        /// <summary>
        /// 配送データの設定
        /// </summary>
        internal void SetDeliveryData()
        {
            // 配送データ取得
            //   配送No
            //   配車区分：定期
            //   配車区分：スポット

            // 抽出条件読み込み
            this.SearchDTO = CreateSearchDto();

            if (this.SearchByTeiki)
            {
                // 配車区分：定期
                List<DeliveryPlanDTO> list = this.form.customDataGridView1.DataSource as List<DeliveryPlanDTO>;
                var selectDto = list.Where(n => n.KUMIKOMI).First();
                var result = GetDeliveryDataForTeiki(selectDto, this.SearchDTO);

                this.form.Ichiran1.DataSource = result;
            }
            else
            {
                // 配車区分：スポット
                List<DeliveryPlanDTO> list = this.form.customDataGridView1.DataSource as List<DeliveryPlanDTO>;
                var selectDtos = list.Where(n => n.KUMIKOMI);
                var result = GetDeliveryDataForSpot(selectDtos, this.SearchDTO);

                this.form.Ichiran1.DataSource = result;
            }

            //項目の可視、不可視設定
            this.ColumnVisibleData();
            this.ColumnReadOnly();

            if (this.form.Ichiran1.RowCount == 0)
            {
                this.msgLogic.MessageBoxShow("C001");
            }

        }

        /// <summary>
        /// 配送データ取得(定期配車)
        /// </summary>
        /// <param name="selectDto"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        private List<DeliveryDataDTO> GetDeliveryDataForTeiki(DeliveryPlanDTO selectDto, SearchDeliveryPlanDTO searchDto)
        {
            if (selectDto == null)
            {
                return null;
            }

            var result = this.deliveryDataDao.GetDeliveryDataForTeiki(selectDto, searchDto);

            return result;
        }

        /// <summary>
        /// 配送データ取得(スポット)
        /// </summary>
        /// <param name="selectDtos"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        private List<DeliveryDataDTO> GetDeliveryDataForSpot(IEnumerable<DeliveryPlanDTO> selectDtos, SearchDeliveryPlanDTO searchDto)
        {
            if (selectDtos == null || selectDtos.Count() == 0)
            {
                return null;
            }

            var result = new List<DeliveryDataDTO>();

            foreach (var selectDto in selectDtos)
            {
                var dtoList = this.deliveryDataDao.GetDeliveryDataForSpot(selectDto, searchDto);
                result.AddRange(dtoList);
            }

            var list = result.OrderBy(n => n.DELIVERY_DATE)
                             .ThenBy(n => n.GENCHAKU_TIME)
                             .ThenBy(n => n.DENPYOU_ATTR)
                             .ThenBy(n => Int64.Parse(n.UKETSUKE_NUMBER))
                             .ToList();

            int i = 1;
            list.ForEach(n => n.ROW_NUMBER = (i++).ToString());
            return list;
        }

        /// <summary>
        /// 配送データ取得(システムID)
        /// </summary>
        /// <param name="selectDto"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        private List<DeliveryDataDTO> GetDeliveryDataForSystemId()
        {
            if (SYSTEM_ID == null)
            {
                return null;
            }

            var result = this.deliveryDataDao.GetDeliveryDataForSystemId(SYSTEM_ID);

            if (result == null || result.Count == 0)
            {
                return result;
            }

            // TimeStamp取得用にEntity単位で取得
            this.DisplayDTO = new LogiDeliveryDTO();
            var longSystemId = long.Parse(SYSTEM_ID);

            this.DisplayDTO.LOGI_DELIVERY = this.logiDeliveryDao.GetDataByCd(longSystemId);
            this.DisplayDTO.LOGI_DELIVERY_DETAIL_LIST = this.logiDeliveryDetailDao.GetDataBySystemId(longSystemId);
            this.DisplayDTO.LOGI_LINK_STATUS = this.logiLinkStatusDao.GetDataByCd(longSystemId);

            // スポット
            if (HAISHA_KBN_SPOT.Equals(result.First().HAISHA_KBN))
            {
                return result.OrderBy(n => Int32.Parse(n.ROW_NUMBER))
                             .ToList();
            }
            // 定期
            else
            {
                return result;
            }
        }
        #endregion

        #region 一覧項目の可視、不可視設定(右上明細)
        /// <summary>
        /// 一覧項目の可視、不可視設定
        /// </summary>
        internal void ColumnVisiblePlan()
        {
            // 一覧項目の可視・非可視設定
            foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns)
            {
                var name = col.Name;

                if (name.Equals("PLAN_TEIKI_HAISHA_NUMBER")
                    || name.Equals("PLAN_COURSE_NAME"))
                {
                    // 定期配車
                    col.Visible = this.SearchByTeiki;
                }
            }
        }
        #endregion

        #region 一覧項目の可視、不可視設定(下部明細)
        /// <summary>
        /// 一覧項目の可視、不可視設定
        /// </summary>
        internal void ColumnVisibleData()
        {
            // 一覧項目の可視・非可視設定
            foreach (DataGridViewColumn col in this.form.Ichiran1.Columns)
            {
                var name = col.Name;

                if (name.Equals(DATA_UKETSUKE_NUMBER)
                    || name.Equals(DATA_GENCHAKU_TIME_NAME)
                    || name.Equals(DATA_GENCHAKU_TIME))
                {
                    // スポット
                    col.Visible = !this.SearchByTeiki;
                }
                else if (name.Equals(DATA_TEIKI_HAISHA_NUMBER))
                {
                    // 定期配車
                    col.Visible = this.SearchByTeiki;
                }
            }
        }
        #endregion

        #region 一覧項目の活性、非活性設定
        /// <summary>
        /// 読取専用の設定
        /// </summary>
        internal void ColumnReadOnly()
        {
            // 新規、修正モード以外は通さない
            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG
                && this.form.WindowType != WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                return;
            }

            // 一覧項目の可視・非可視設定
            foreach (DataGridViewRow row in this.form.Ichiran1.Rows)
            {
                // 現場未連携データは対象チェック不可
                // 作業日がシステム日付より前のデータも対象チェック不可
                if (row.Cells[DATA_RENKEI_JYOUKYOU].Value.ToString() == RENKEI_JYOUKYOU_MIRENKEI
                    || (DateTime.Parse(row.Cells[DATA_DELIVERY_DATE].Value.ToString()) < this.date_today))
                {
                    row.Cells[DATA_TAISHO].ReadOnly = true;

                    // ReadOnlyを変更するとBackColorが変わらない場合がある
                    var cell = row.Cells[DATA_TAISHO] as DataGridViewCheckBoxCell;
                    cell.UpdateBackColor(false);
                }
                else
                {
                    row.Cells[DATA_TAISHO].ReadOnly = false;
                }
            }
        }
        #endregion

        #region 登録チェック
        /// <summary>
        /// 登録ボタン押下時のチェック
        /// </summary>
        /// <returns>true:エラー有, false:エラー無</returns>
        internal bool HasErrorRegist()
        {
            // 共通チェック
            var dtos = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            if (dtos == null)
            {
                this.msgLogic.MessageBoxShowError("データがありません。");
                return true;
            }

            // 対象チェック
            var selectDtos = dtos.Where(n => n.TAISHO);
            if (selectDtos.Count() == 0)
            {
                this.msgLogic.MessageBoxShowError("登録対象がありません。一覧の対象を選択してください。");
                return true;
            }

            // 順番未入力チェック
            if (selectDtos.Any(n => string.IsNullOrEmpty(n.ROW_NUMBER)))
            {
                this.msgLogic.MessageBoxShowError("順番を入力してください。");
                return true;
            }

            // 順番重複チェック
            // LINQを使用して、対象にチェックがついている元の行数と重複行を除いた行数を取得
            // 比較して違ったら重複アリという判定
            int baseCount = selectDtos.Count();
            int distinctCount = selectDtos.Select(n => n.ROW_NUMBER).Distinct().Count();
            if (baseCount != distinctCount)
            {
                this.msgLogic.MessageBoxShowError("重複した順番を入力する事はできません。");
                return true;
            }

            // 定期配車
            if (this.SearchByTeiki)
            {
                if (1 < selectDtos.Where(n => !string.IsNullOrEmpty(n.TEIKI_HAISHA_NUMBER)).Select(n => n.TEIKI_HAISHA_NUMBER).Distinct().Count())
                {
                    this.msgLogic.MessageBoxShowError("複数の定期配車番号を登録する事はできません。");
                    return true;
                }
                if (selectDtos.All(n => string.IsNullOrEmpty(n.TEIKI_HAISHA_NUMBER)))
                {
                    this.msgLogic.MessageBoxShowError("定期配車番号が入力された対象を１つ以上選択してください。");
                    return true;
                }
            }
            // スポット
            else
            {
                if (selectDtos.All(n => string.IsNullOrEmpty(n.UKETSUKE_NUMBER)))
                {
                    this.msgLogic.MessageBoxShowError("受付番号が入力された対象を１つ以上選択してください。");
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 登録用Entity作成
        /// <summary>
        /// 登録用Entityを作成
        /// </summary>
        /// <param name="linkStatus">連携状態(1.未送信,2.送信済,3.受信済)</param>
        internal void CreateEntity(short linkStatus)
        {
            this.RegistDTO = new LogiDeliveryDTO();
            this.RegistDTO.LOGI_DELIVERY_DETAIL_LIST = new List<T_LOGI_DELIVERY_DETAIL>();

            var dtos = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            var selectDtos = dtos.Where(n => n.TAISHO).OrderBy(n => Int32.Parse(n.ROW_NUMBER));
            // 荷積挿入・荷降挿入対策のため、伝票番号が設定されている伝票情報を取得(事前チェックで伝票番号は必ず１つ以上設定済)
            DeliveryDataDTO dto = null;
            if (this.SearchByTeiki)
            {
                dto = selectDtos.Where(n => !string.IsNullOrEmpty(n.TEIKI_HAISHA_NUMBER))
                                .FirstOrDefault();
            }
            else
            {
                // スポットの場合は、選択された中で一番日付が若い作業日かつ受付番号が設定されている伝票情報を取得
                var date = Convert.ToDateTime(selectDtos.Min(n => n.DELIVERY_DATE));
                dto = selectDtos.Where(n => DateTime.Parse(n.DELIVERY_DATE).Equals(date) && !string.IsNullOrEmpty(n.UKETSUKE_NUMBER))
                                .FirstOrDefault();
            }

            var entry = new T_LOGI_DELIVERY();
            SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.HAISOU_KEIKAKU_NYUURYOKU).ToString());
            //INSERTのみ採番
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                entry.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
            }
            else
            {
                entry.SYSTEM_ID = SqlInt32.Parse(dto.SYSTEM_ID);
            }
            entry.HAISHA_KBN = Int16.Parse(this.SearchDTO.HAISHA_KBN);
            entry.SHASHU_CD = dto.SHASHU_CD;
            entry.SHARYOU_CD = dto.SHARYOU_CD;
            entry.UNTENSHA_CD = dto.UNTENSHA_CD;
            entry.UPN_GYOUSHA_CD = dto.UNPAN_GYOUSHA_CD;
            entry.DELIVERY_DATE = Convert.ToDateTime(dto.DELIVERY_DATE);

            //INSERTのみ採番
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                entry.DELIVERY_NO = SaibanUtil.CreateNumberDay(entry.DELIVERY_DATE.Value, denshuKbn, 0);// 拠点CDは暫定で0固定で対応
            }
            else
            {
                entry.DELIVERY_NO = SqlInt32.Parse(dto.DELIVERY_NO);
            }

            //配送名が未入力の場合は自動作成
            if (this.form.DELIVERY_NAME.Text == string.Empty)
            {
                // 社員名取得
                // 配送名"配送No"："+配送No+全角スペース+運転者名+"："+コース名
                // コース名は定期配車の場合のみ
                var shain = this.shainDao.GetDataByCd(dto.UNTENSHA_CD);
                if (this.SearchByTeiki)
                {
                    this.form.DELIVERY_NAME.Text = "配送No：" + entry.DELIVERY_NO + "　" + shain.SHAIN_NAME + "：" + dto.COURSE_NAME;
                }
                else
                {
                    this.form.DELIVERY_NAME.Text = "配送No：" + entry.DELIVERY_NO + "　" + shain.SHAIN_NAME;
                }
            }
            entry.DELIVERY_NAME = this.form.DELIVERY_NAME.Text;

            var dataBinderDelivery = new DataBinderLogic<T_LOGI_DELIVERY>(entry);
            dataBinderDelivery.SetSystemProperty(entry, false);

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && this.DisplayDTO != null && this.DisplayDTO.LOGI_DELIVERY != null)
            {
                entry.TIME_STAMP = this.DisplayDTO.LOGI_DELIVERY.TIME_STAMP;
            }

            this.RegistDTO.LOGI_DELIVERY = entry;

            int rowNumber = 0;
            foreach (var selectDto in selectDtos)
            {
                var detail = new T_LOGI_DELIVERY_DETAIL();
                detail.SYSTEM_ID = entry.SYSTEM_ID;
                //新規 or 修正モードで荷積、荷降を挿入された場合に採番
                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                    || (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG && selectDto.NIZUMI_NIOROSHI_FLG && selectDto.DETAIL_SYSTEM_ID == null))
                {
                    detail.DETAIL_SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                }
                else
                {
                    detail.DETAIL_SYSTEM_ID = SqlInt64.Parse(selectDto.DETAIL_SYSTEM_ID);
                }
                detail.DELIVERY_NO = entry.DELIVERY_NO;
                //順番は未入力不可
                //画面上で入力した順に連番
                rowNumber++;
                detail.DELIVERY_ORDER = rowNumber;
                detail.NOT_URIAGE_KBN = selectDto.NIOROSHI;
                detail.DENPYOU_ATTR = SqlInt16.Parse(selectDto.DENPYOU_ATTR);
                if (!string.IsNullOrEmpty(selectDto.REF_SYSTEM_ID))
                {
                    detail.REF_SYSTEM_ID = SqlInt64.Parse(selectDto.REF_SYSTEM_ID);
                }
                if (this.SearchByTeiki)
                {
                    if (!string.IsNullOrEmpty(selectDto.REF_DETAIL_SYSTEM_ID))
                    {
                        detail.REF_DETAIL_SYSTEM_ID = SqlInt64.Parse(selectDto.REF_DETAIL_SYSTEM_ID);
                    }
                    if (!string.IsNullOrEmpty(selectDto.TEIKI_HAISHA_NUMBER))
                    {
                        detail.REF_DENPYOU_NO = SqlInt64.Parse(selectDto.TEIKI_HAISHA_NUMBER);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(selectDto.UKETSUKE_NUMBER))
                    {
                        detail.REF_DENPYOU_NO = SqlInt64.Parse(selectDto.UKETSUKE_NUMBER);
                    }
                }
                detail.NIZUMI_NIOROSHI_FLG = selectDto.NIZUMI_NIOROSHI_FLG;
                // 荷積荷降フラグに関わらずセット
                detail.GYOUSHA_CD = selectDto.GYOUSHA_CD;
                detail.GYOUSHA_NAME = selectDto.GYOUSHA_NAME_RYAKU;
                detail.GENBA_CD = selectDto.GENBA_CD;
                detail.GENBA_NAME = selectDto.GENBA_NAME_RYAKU;

                if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && this.DisplayDTO != null && this.DisplayDTO.LOGI_DELIVERY_DETAIL_LIST != null)
                {
                    var detailDto = this.DisplayDTO.LOGI_DELIVERY_DETAIL_LIST.Where(n => n.SYSTEM_ID.Value == detail.SYSTEM_ID.Value
                                                                                      && n.DETAIL_SYSTEM_ID.Value == detail.DETAIL_SYSTEM_ID.Value)
                                                                             .FirstOrDefault();
                    if (detailDto != null)
                    {
                        detail.TIME_STAMP = detailDto.TIME_STAMP;
                    }
                }

                this.RegistDTO.LOGI_DELIVERY_DETAIL_LIST.Add(detail);
            }

            var link = new T_LOGI_LINK_STATUS();
            link.SYSTEM_ID = entry.SYSTEM_ID;
            link.LINK_STATUS = linkStatus;

            var dataBinderLink = new DataBinderLogic<T_LOGI_LINK_STATUS>(link);
            dataBinderLink.SetSystemProperty(link, false);

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG && this.DisplayDTO != null && this.DisplayDTO.LOGI_LINK_STATUS != null)
            {
                link.TIME_STAMP = this.DisplayDTO.LOGI_LINK_STATUS.TIME_STAMP;
            }

            this.RegistDTO.LOGI_LINK_STATUS = link;

            this.form.DELIVERY_NO.Text = entry.DELIVERY_NO.ToString();
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        internal int RegistData()
        {
            int ret_cnt = 0;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //INSERT
                    this.RegistDTO.LOGI_DELIVERY.DELETE_FLG = false;
                    this.logiDeliveryDao.Insert(this.RegistDTO.LOGI_DELIVERY);

                    foreach (var dto in this.RegistDTO.LOGI_DELIVERY_DETAIL_LIST)
                    {
                        dto.DELETE_FLG = false;
                        this.logiDeliveryDetailDao.Insert(dto);
                    }
                    this.RegistDTO.LOGI_LINK_STATUS.DELETE_FLG = false;
                    this.logiLinkStatusDao.Insert(this.RegistDTO.LOGI_LINK_STATUS);

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion

        #region 修正登録
        /// <summary>
        /// 修正登録
        /// </summary>
        [Transaction]
        internal int UpdateData()
        {
            int ret_cnt = 0;
            try
            {
                using (Transaction tran = new Transaction())
                {
                    //UPDATE
                    this.RegistDTO.LOGI_DELIVERY.DELETE_FLG = false;
                    this.logiDeliveryDao.Update(this.RegistDTO.LOGI_DELIVERY);
                    // 
                    foreach (var dto in this.DisplayDTO.LOGI_DELIVERY_DETAIL_LIST)
                    {
                        this.logiDeliveryDetailDao.Delete(dto);
                    }
                    foreach (var dto in this.RegistDTO.LOGI_DELIVERY_DETAIL_LIST)
                    {
                        dto.DELETE_FLG = false;
                        this.logiDeliveryDetailDao.Insert(dto);
                    }
                    this.RegistDTO.LOGI_LINK_STATUS.DELETE_FLG = false;
                    this.logiLinkStatusDao.Update(this.RegistDTO.LOGI_LINK_STATUS);

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion

        #region 削除登録
        /// <summary>
        /// 削除登録
        /// </summary>
        [Transaction]
        internal int LogicalDeleteData()
        {
            int ret_cnt = 0;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //DELETE
                    //削除フラグを立ててUPDATE
                    this.RegistDTO.LOGI_DELIVERY.DELETE_FLG = true;
                    this.logiDeliveryDao.Update(this.RegistDTO.LOGI_DELIVERY);
                    foreach (var dto in this.RegistDTO.LOGI_DELIVERY_DETAIL_LIST)
                    {
                        dto.DELETE_FLG = true;
                        this.logiDeliveryDetailDao.Update(dto);
                    }
                    this.RegistDTO.LOGI_LINK_STATUS.DELETE_FLG = true;
                    this.logiLinkStatusDao.Update(this.RegistDTO.LOGI_LINK_STATUS);

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDeleteData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion

        #region 順番整列
        /// <summary>
        /// 順番整列
        /// </summary>
        internal void SortIchiran()
        {
            try
            {
                // 明細行が空行１行の場合
                if (this.form.Ichiran1.Rows.Count == 0)
                {
                    return;
                }

                // 順番の必須チェック
                bool isErrorFlag = false;
                for (int i = 0; i < this.form.Ichiran1.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.Ichiran1.Rows[i];
                    if (string.IsNullOrEmpty(row.Cells[DATA_ROW_NUMBER].FormattedValue.ToString()))
                    {
                        isErrorFlag = true;
                        break;
                    }
                }

                if (isErrorFlag)
                {
                    //コース明細が表示されていない場合順番確定処理を行わない。
                    if (0 < this.form.Ichiran1.Rows.Count)
                    {
                        // 順番を採番
                        for (int j = 0; j < this.form.Ichiran1.Rows.Count; j++)
                        {
                            this.form.Ichiran1.Rows[j].Cells[DATA_ROW_NUMBER].Value = Int32.Parse((j + 1).ToString());
                        }
                    }
                }

                // 明細リストを「順番」の昇順に並びかえる
                var dtos = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
                var sortedDtos = dtos.OrderBy(n => Int32.Parse(n.ROW_NUMBER)).ToList();
                this.form.Ichiran1.DataSource = sortedDtos;

                this.ColumnVisibleData();
                this.ColumnReadOnly();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SortIchiran", ex);
                throw ex;
            }
        }
        #endregion

        #region 荷積挿入、荷降挿入チェック
        /// <summary>
        /// 荷積挿入、荷降挿入ボタン押下時のチェック
        /// </summary>
        /// <returns>true:エラー有, false:エラー無</returns>
        internal bool HasErrorAddNizumiNiorosi()
        {
            var list = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            if (list == null || list.Count == 0)
            {
                this.msgLogic.MessageBoxShowError("配送計画の一覧が存在しません。");
                return true;
            }

            var deliveryDataDto = list[this.form.Ichiran1.CurrentRow.Index];
            if (deliveryDataDto.RENKEI_JYOUKYOU.Equals(RENKEI_JYOUKYOU_MIRENKEI))
            {
                this.msgLogic.MessageBoxShowError("未連携のデータには挿入できません。");
                return true;
            }

            return false;
        }
        #endregion

        #region 荷積挿入
        /// <summary>
        /// 荷積挿入
        /// </summary>
        internal void AddNizumiGenba()
        {
            var list = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            if (list == null || list.Count == 0)
            {
                return;
            }

            this.AddNizumiGenbaFlg = true;
            var rowIndex = this.form.Ichiran1.CurrentRow.Index;
            var deliveryDataDto = list[rowIndex];

            if (deliveryDataDto.DENPYOU_ATTR.Equals(DENPYOU_ATTR_SS))
            {
                // 収集受付：荷積現場ポップアップ呼出
                CustomControlExtLogic.PopUp(this.form.NIZUMI_GENBA_CD);
            }
            else if (deliveryDataDto.DENPYOU_ATTR.Equals(DENPYOU_ATTR_SK))
            {
                bool addFlg = false;
                if (!string.IsNullOrEmpty(deliveryDataDto.UKETSUKE_NUMBER))
                {
                    SqlInt64 uketsukeNum = SqlInt64.Parse(deliveryDataDto.UKETSUKE_NUMBER);
                    var dt = deliveryDataRequestDao.GetUketsukeSK(uketsukeNum);
                    if (dt != null || 0 < dt.Rows.Count)
                    {
                        DataRow row = dt.Rows[0];
                        var nizumiGenbaCd = row["NIZUMI_GENBA_CD"].ToString();
                        if (!string.IsNullOrEmpty(nizumiGenbaCd))
                        {
                            // 出荷受付伝票に登録されている荷積現場を上段に登録
                            DeliveryDataDTO dto = CreateBaseDeliveryDataDTO(deliveryDataDto);
                            dto.GYOUSHA_CD = row["NIZUMI_GYOUSHA_CD"].ToString();
                            dto.GYOUSHA_NAME_RYAKU = row["NIZUMI_GYOUSHA_NAME"].ToString();
                            dto.GENBA_CD = nizumiGenbaCd;
                            dto.GENBA_NAME_RYAKU = row["NIZUMI_GENBA_NAME"].ToString();
                            dto.RENKEI_JYOUKYOU = GetRenkeiJyoukyou(dto.GYOUSHA_CD, dto.GENBA_CD);
                            dto.POINT_ID = GetPointID(dto.GYOUSHA_CD, dto.GENBA_CD);

                            list.Insert(rowIndex, dto);

                            this.form.Ichiran1.DataSource = new List<DeliveryDataDTO>();
                            this.form.Ichiran1.DataSource = list;

                            this.ColumnReadOnly();
                            addFlg = true;
                        }
                    }
                }

                if (!addFlg)
                {
                    // 出荷受付：荷積現場ポップアップ呼出
                    CustomControlExtLogic.PopUp(this.form.NIZUMI_GENBA_CD);
                }
            }
            else if (deliveryDataDto.DENPYOU_ATTR.Equals(DENPYOU_ATTR_TEIKI))
            {
                // 定期配車：現場ポップアップ呼出
                CustomControlExtLogic.PopUp(this.form.TEIKI_GENBA_CD);
            }
        }
        #endregion

        #region 荷降挿入
        /// <summary>
        /// 荷降挿入
        /// </summary>
        internal void AddNioroshiGenba()
        {
            var list = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            if (list == null || list.Count == 0)
            {
                return;
            }

            this.AddNizumiGenbaFlg = false;
            var rowIndex = this.form.Ichiran1.CurrentRow.Index;
            var deliveryDataDto = list[rowIndex];

            if (deliveryDataDto.DENPYOU_ATTR.Equals(DENPYOU_ATTR_SS))
            {
                bool addFlg = false;
                if (!string.IsNullOrEmpty(deliveryDataDto.UKETSUKE_NUMBER))
                {
                    SqlInt64 uketsukeNum = SqlInt64.Parse(deliveryDataDto.UKETSUKE_NUMBER);
                    var dt = deliveryDataRequestDao.GetUketsukeSS(uketsukeNum);
                    if (dt != null || 0 < dt.Rows.Count)
                    {
                        DataRow row = dt.Rows[0];
                        var nioroshiGenbaCd = row["NIOROSHI_GENBA_CD"].ToString();
                        if (!string.IsNullOrEmpty(nioroshiGenbaCd))
                        {
                            // 収集受付伝票に登録されている荷降現場を下段に登録
                            DeliveryDataDTO dto = CreateBaseDeliveryDataDTO(deliveryDataDto);
                            dto.GYOUSHA_CD = row["NIOROSHI_GYOUSHA_CD"].ToString();
                            dto.GYOUSHA_NAME_RYAKU = row["NIOROSHI_GYOUSHA_NAME"].ToString();
                            dto.GENBA_CD = nioroshiGenbaCd;
                            dto.GENBA_NAME_RYAKU = row["NIOROSHI_GENBA_NAME"].ToString();
                            dto.RENKEI_JYOUKYOU = GetRenkeiJyoukyou(dto.GYOUSHA_CD, dto.GENBA_CD);
                            dto.POINT_ID = GetPointID(dto.GYOUSHA_CD, dto.GENBA_CD);

                            rowIndex++;
                            list.Insert(rowIndex, dto);

                            this.form.Ichiran1.DataSource = new List<DeliveryDataDTO>();
                            this.form.Ichiran1.DataSource = list;

                            this.ColumnReadOnly();

                            addFlg = true;
                        }
                    }
                }

                if (!addFlg)
                {
                    // 収集受付：荷降現場ポップアップ呼出
                    CustomControlExtLogic.PopUp(this.form.NIOROSHI_GENBA_CD);
                }
            }
            else if (deliveryDataDto.DENPYOU_ATTR.Equals(DENPYOU_ATTR_SK))
            {
                // 出荷受付：荷降現場ポップアップ呼出
                CustomControlExtLogic.PopUp(this.form.NIOROSHI_GENBA_CD);
            }
            else if (deliveryDataDto.DENPYOU_ATTR.Equals(DENPYOU_ATTR_TEIKI))
            {
                // 定期配車：現場ポップアップ呼出
                CustomControlExtLogic.PopUp(this.form.TEIKI_GENBA_CD);
            }
        }
        #endregion

        #region 荷積・荷降挿入の現場検索処理
        /// <summary>
        /// 荷降現場検索ポップアップ後処理(荷降挿入ボタン押下時)
        /// </summary>
        /// <param name="isSpot">true:スポット, false:定期</param>
        internal void NioroshiGenbaPopupBefore(bool isSpot)
        {
            if ((isSpot && string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
                || (!isSpot && string.IsNullOrEmpty(this.form.TEIKI_GENBA_CD.Text)))
            {
                return;
            }

            var list = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            if (list == null || list.Count == 0)
            {
                return;
            }

            var rowIndex = this.form.Ichiran1.CurrentRow.Index;
            var deliveryDataDto = list[rowIndex];

            DeliveryDataDTO dto = CreateBaseDeliveryDataDTO(deliveryDataDto);
            if (isSpot)
            {
                // スポット
                dto.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                dto.GYOUSHA_NAME_RYAKU = this.form.NIOROSHI_GYOUSHA_NAME.Text;
                dto.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;
                dto.GENBA_NAME_RYAKU = this.form.NIOROSHI_GENBA_NAME.Text;
            }
            else
            {
                // 定期
                dto.GYOUSHA_CD = this.form.TEIKI_GYOUSHA_CD.Text;
                dto.GYOUSHA_NAME_RYAKU = this.form.TEIKI_GYOUSHA_NAME.Text;
                dto.GENBA_CD = this.form.TEIKI_GENBA_CD.Text;
                dto.GENBA_NAME_RYAKU = this.form.TEIKI_GENBA_NAME.Text;
            }
            dto.RENKEI_JYOUKYOU = GetRenkeiJyoukyou(dto.GYOUSHA_CD, dto.GENBA_CD);
            dto.POINT_ID = GetPointID(dto.GYOUSHA_CD, dto.GENBA_CD);

            rowIndex++;
            list.Insert(rowIndex, dto);

            this.form.Ichiran1.DataSource = new List<DeliveryDataDTO>();
            this.form.Ichiran1.DataSource = list;

            this.ColumnReadOnly();
        }

        /// <summary>
        /// 荷積現場検索ポップアップ後処理(荷積挿入ボタン押下時)
        /// </summary>
        /// <param name="isSpot">true:スポット, false:定期</param>
        internal void NizumiGenbaPopupBefore(bool isSpot)
        {
            if ((isSpot && string.IsNullOrEmpty(this.form.NIZUMI_GENBA_CD.Text))
                || (!isSpot && string.IsNullOrEmpty(this.form.TEIKI_GENBA_CD.Text)))
            {
                return;
            }

            var list = this.form.Ichiran1.DataSource as List<DeliveryDataDTO>;
            if (list == null || list.Count == 0)
            {
                return;
            }

            var rowIndex = this.form.Ichiran1.CurrentRow.Index;
            var deliveryDataDto = list[rowIndex];

            DeliveryDataDTO dto = CreateBaseDeliveryDataDTO(deliveryDataDto);
            if (isSpot)
            {
                // スポット
                dto.GYOUSHA_CD = this.form.NIZUMI_GYOUSHA_CD.Text;
                dto.GYOUSHA_NAME_RYAKU = this.form.NIZUMI_GYOUSHA_NAME.Text;
                dto.GENBA_CD = this.form.NIZUMI_GENBA_CD.Text;
                dto.GENBA_NAME_RYAKU = this.form.NIZUMI_GENBA_NAME.Text;
            }
            else
            {
                // 定期
                dto.GYOUSHA_CD = this.form.TEIKI_GYOUSHA_CD.Text;
                dto.GYOUSHA_NAME_RYAKU = this.form.TEIKI_GYOUSHA_NAME.Text;
                dto.GENBA_CD = this.form.TEIKI_GENBA_CD.Text;
                dto.GENBA_NAME_RYAKU = this.form.TEIKI_GENBA_NAME.Text;
            }
            dto.RENKEI_JYOUKYOU = GetRenkeiJyoukyou(dto.GYOUSHA_CD, dto.GENBA_CD);
            dto.POINT_ID = GetPointID(dto.GYOUSHA_CD, dto.GENBA_CD);

            list.Insert(rowIndex, dto);

            this.form.Ichiran1.DataSource = new List<DeliveryDataDTO>();
            this.form.Ichiran1.DataSource = list;

            this.ColumnReadOnly();
        }
        #endregion

        #region 荷積挿入、荷降挿入ボタン押下時のベース配送DTO作成
        /// <summary>
        /// 荷積挿入、荷降挿入ボタン押下時に作成されるベース配送DTO
        /// </summary>
        /// <param name="deliveryDataDto">配送一覧で選択済みの配送DTO</param>
        /// <returns></returns>
        private DeliveryDataDTO CreateBaseDeliveryDataDTO(DeliveryDataDTO deliveryDataDto)
        {
            var dto = new DeliveryDataDTO();

            // 順番ブランク、非売上はチェック固定
            dto.TAISHO = false;
            dto.ROW_NUMBER = string.Empty;
            dto.NIOROSHI = true;
            dto.NIZUMI_NIOROSHI_FLG = true;

            // システム的に必要な項目は一覧の選択行をベースにコピー
            dto.DELIVERY_DATE = deliveryDataDto.DELIVERY_DATE;
            dto.DENPYOU_ATTR = deliveryDataDto.DENPYOU_ATTR;
            dto.HAISHA_KBN = deliveryDataDto.HAISHA_KBN;

            // デジタコ連携用の車輌IDも取得
            dto.DIGI_SHARYOU_CD = deliveryDataDto.DIGI_SHARYOU_CD;

            // 業者CD、業者名、現場CD、現場名については呼出もとでそれぞれ設定
            return dto;
        }
        #endregion

        #region 現場連携状況の名称取得
        /// <summary>
        /// 現場連携状況の名称取得
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場連携状況</returns>
        private string GetRenkeiJyoukyou(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            var entity = this.genbaDigiDao.GetDataByCd(gyoushaCd, genbaCd);
            if (entity == null)
            {
                return RENKEI_JYOUKYOU_MIRENKEI;
            }
            else
            {
                return RENKEI_JYOUKYOU_ZUMI;
            }
        }
        #endregion

        #region ポイントID取得
        /// <summary>
        /// 現場連携状況の名称取得
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>ポイントID</returns>
        private string GetPointID(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return null;
            }

            var entity = this.genbaDigiDao.GetDataByCd(gyoushaCd, genbaCd);
            if (entity == null)
            {
                return string.Empty;
            }
            else
            {
                return entity.POINT_ID;
            }
        }
        #endregion

        #region 未使用
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region 明細ヘッダーにチェックボックスを追加
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {
            LogUtility.DebugMethodStart();

            if (!this.form.Ichiran1.Columns.Contains(DATA_TAISHO))
            {
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                newColumn.Name = DATA_TAISHO;
                newColumn.HeaderText = "対象";
                newColumn.DataPropertyName = TAISHO;
                newColumn.Width = 70;
                DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                newheader.Value = "対象   ";
                newColumn.HeaderCell = newheader;
                newColumn.Resizable = DataGridViewTriState.False;

                if (this.form.Ichiran1.Columns.Count > 0)
                {
                    this.form.Ichiran1.Columns.Insert(0, newColumn);
                }
                else
                {
                    this.form.Ichiran1.Columns.Add(newColumn);
                }
                this.form.Ichiran1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 明細ヘッダーのチェックボックス解除
        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        internal void HeaderCheckBoxFalse()
        {

            if (this.form.Ichiran1.Columns.Contains(DATA_TAISHO))
            {
                DataGridViewCheckBoxHeaderCell header = this.form.Ichiran1.Columns[DATA_TAISHO].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckGenba()
        {
            bool returnVal = false;
            try
            {
                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var genba = this.genbaDao.GetAllValidData(entity);
                if (genba != null && 0 < genba.Length)
                {
                    this.form.GENBA_RNAME.Text = genba[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.GENBA_RNAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                    return returnVal;
                }

                // 処理終了
                returnVal = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckGenba", ex2);
                this.msgLogic.MessageBoxShow("E093");
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckGenba", ex);
                this.msgLogic.MessageBoxShow("E245");
                returnVal = false;
            }
            return returnVal;
        }
        #endregion

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool ChechSharyouCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 車輌名をクリア
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.form.SHARYOU_CD.Text);
                if (sharyou == null)
                {
                    // メッセージ表示
                    this.msgLogic.MessageBoxShow("E020", "車輌");
                    this.form.SHARYOU_CD.Focus();
                    return returnVal;
                }

                // 車輌名設定
                this.form.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 車種入力されてない場合
                if (string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    // 車種情報取得
                    var shashu = this.GetSharshu(sharyou.SHASYU_CD);
                    if (shashu != null)
                    {
                        // 車種情報設定
                        this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                        this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                // 運転者入力されてない場合
                if (string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                {
                    // 社員情報取得
                    var shain = this.GetShain(sharyou.SHAIN_CD);
                    if (shain != null)
                    {
                        // 運転者情報設定
                        this.form.SHAIN_CD.Text = shain.SHAIN_CD;
                        this.form.SHAIN_NAME.Text = shain.SHAIN_NAME_RYAKU;
                    }
                }

                // 運搬業者が入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 業者情報取得
                    var gyousha = this.GetGyousha(sharyou.GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 業者情報設定
                        this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechSharyouCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 車輌情報取得
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        private M_SHARYOU GetSharyou(string sharyouCd)
        {
            M_SHARYOU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHARYOU keyEntity = new M_SHARYOU();
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                // 車種入力されている場合
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    keyEntity.SHASYU_CD = this.form.SHASHU_CD.Text;
                }
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    if (returnEntitys.Length == 1)
                    {
                        // 1件
                        returnVal = returnEntitys[0];
                    }
                    else
                    {
                        // ヒット数が複数件の場合、ポップアップ表示
                        this.form.SHARYOU_CD.Focus();
                        SendKeys.Send(" ");

                        // 返却値は空白をセット
                        returnVal = new M_SHARYOU();
                        returnVal.SHARYOU_NAME_RYAKU = "";
                        returnVal.SHASYU_CD = "";
                        returnVal.SHAIN_CD = "";
                        returnVal.GYOUSHA_CD = "";
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 車種情報取得
        /// </summary>
        /// <param name="shashuCd">車種CD</param>
        /// <returns></returns>
        private M_SHASHU GetSharshu(string shashuCd)
        {
            M_SHASHU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shashuCd);

                if (string.IsNullOrEmpty(shashuCd))
                {
                    return returnVal;
                }

                M_SHASHU keyEntity = new M_SHASHU() { SHASHU_CD = shashuCd};
                returnVal = MasterUtility.GetShashu(keyEntity, MasterUtility.DELETE_FLAG.NODELETE);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharshu", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 社員情報取得
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns></returns>
        private M_SHAIN GetShain(string shainCd)
        {
            M_SHAIN returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shainCd);

                if (string.IsNullOrEmpty(shainCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                keyEntity.UNTEN_KBN = true;

                // [社員CD,運転者フラグ=true]でM_SHAINを検索する
                var returnEntitys = this.shainDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetShain", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        private M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            M_GYOUSHA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd };
                returnVal = MasterUtility.GetGyousha(keyEntity, MasterUtility.DELETE_FLAG.NODELETE);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 運転者チェック
        /// <summary>
        /// 運転者チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckUntenshaCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 社員名をクリア
                this.form.SHAIN_NAME.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                M_SHAIN entity = new M_SHAIN();
                entity.SHAIN_CD = this.form.SHAIN_CD.Text;
                entity.UNTEN_KBN = true;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(entity).FirstOrDefault();
                if (untenShain == null)
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E020", "運転者");
                    this.form.SHAIN_CD.Focus();
                    return returnVal;
                }

                this.form.SHAIN_NAME.Text = untenShain.SHAIN_NAME_RYAKU;

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion

        #region 運搬業者チェック
        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckUnpanGyoushaCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.ISNOT_NEED_DELETE_FLG = true;
                var unpanGyousha = this.gyoushaDao.GetAllValidData(entity).FirstOrDefault(s => s.GYOUSHA_CD == this.form.UNPAN_GYOUSHA_CD.Text);
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return returnVal;
                }

                // 名称セット
                this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyoushaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }
        #endregion
    }
}
