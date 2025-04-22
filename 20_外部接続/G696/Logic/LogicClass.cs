using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Const;
using Shougun.Function.ShougunCSCommon.Utility;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ロジコンパス
        /// </summary>
        private LogiLogic logiLogic;

        /// <summary>
        /// G283専用DBアクセッサー
        /// </summary>
        private Shougun.Core.Allocation.MobileShougunTorikomi.Accessor.DBAccessor accessor;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        private string mcreateSql { get; set; }

        /// <summary>
        /// 「F9:取込確認」機能のON,OFF
        /// </summary>
        /// <remarks>
        /// true:チェックON, false:チェックOFF
        /// 開発時はチェックOFFとすること(車載機がないためAPIデータ変更不可のため)
        /// 最終的にはチェックONにしてビルドすること
        /// </remarks>
        private readonly bool CHECK_IMPORT = true;

        #region 画面コントロール系
        /// <summary>
        /// 配車区分(1.定期配車)
        /// </summary>
        const int HAISHA_TEIKI_KBN = 1;
        /// <summary>
        /// 配車区分(2.スポット)
        /// </summary>
        const int HAISHA_SPOT_KBN = 2;
        /// <summary>
        /// 配車区分フラグ
        /// </summary>
        internal int haisha_kbn { get; set; }
        /// <summary>
        /// 連携状況(1.未送信)
        /// </summary>
        internal const int RENKEI_MISOUSHIN = 1;
        /// <summary>
        /// 連携状況(2.送信済)
        /// </summary>
        internal const int RENKEI_SOUSHIN = 2;
        /// <summary>
        /// 連携状況(3.受信済)
        /// </summary>
        internal const int RENKEI_JYUSHIN = 3;
        /// <summary>
        /// 連携状況フラグ
        /// </summary>
        internal int renkei_kbn { get; set; }
        /// <summary>
        /// 検索ボタン押下時の連携状況
        /// </summary>
        internal int search_renkei_kbn { get; set; }
        #endregion

        #region Dao
        /// <summary>
        /// ロジこん連携用Dao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// ロジこん接続管理Dao
        /// </summary>
        private IS_LOGI_CONNECTDao logiConnectDao;

        /// <summary>
        /// 配送計画Dao
        /// </summary>
        private LogiDeliveryDAO logiDeliveryDao;

        /// <summary>
        /// 配送計画明細Dao
        /// </summary>
        private LogiDeliveryDetailDAO logiDeliveryDetailDao;

        /// <summary>
        /// 配送計画連携状況管理Dao
        /// </summary>
        private LogiLinkStatusDAO logiLinkStatusDao;

        /// <summary>
        /// 配送計画to定期実績Dao
        /// </summary>
        private LogiToTeikiDAO logiToTeikiDao;

        /// <summary>
        /// 配送計画to売上支払Dao
        /// </summary>
        private LogiToUrshDAO logiToUrshDao;

        /// <summary>
        /// デジタコ連携車輛Dao
        /// </summary>
        private IM_DIGI_OUTPUT_SHARYOUDao digiOutputSharyouDao;

        /// <summary>
        /// 社員Dao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// デジタコ連携単位
        /// </summary>
        private IM_DIGI_OUTPUT_UNITDao digiOutputUnitDao;

        /// <summary>
        /// デジタコ連携品名
        /// </summary>
        private IM_DIGI_OUTPUT_HINMEIDao digiOutputHinmeiDao;

        /// <summary>
        /// 品名Dao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// 個別品名Dao
        /// </summary>
        private IM_KOBETSU_HINMEIDao kobetsuHinmeiDao;

        /// <summary>
        /// 収集受付伝票Dao
        /// </summary>
        private UketsukeSsEntryDAO uketsukeSsEntryDao;

        /// <summary>
        /// 収集受付明細Dao
        /// </summary>
        private UketsukeSsDetailDAO uketsukeSsDetailDao;

        /// <summary>
        /// 出荷受付伝票Dao
        /// </summary>
        private UketsukeSkEntryDAO uketsukeSkEntryDao;

        /// <summary>
        /// 出荷受付明細Dao
        /// </summary>
        private UketsukeSkDetailDAO uketsukeSkDetailDao;

        /// <summary>
        /// 売上/支払伝票Dao
        /// </summary>
        private UrShEntryDAO urShEntryDao;

        /// <summary>
        /// 売上/支払明細Dao
        /// </summary>
        private UrShDetailDAO urShDetailDao;

        /// <summary>
        /// 定期配車入力Dao
        /// </summary>
        private TeikiHaishaEntryDAO teikiHaishaEntryDao;

        /// <summary>
        /// 定期配車明細Dao
        /// </summary>
        private TeikiHaishaDetailDAO teikiHaishaDetailDao;

        /// <summary>
        /// 定期配車詳細Dao
        /// </summary>
        private TeikiHaishaShousaiDAO teikiHaishaShousaiDao;

        /// <summary>
        /// 定期配車荷卸Dao
        /// </summary>
        private TeikiHaishaNioroshiDAO teikiHaishaNioroshiDao;

        /// <summary>
        /// 定期実績入力Dao
        /// </summary>
        private TeikiJissekiEntryDAO teikiJissekiEntryDao;

        /// <summary>
        /// 定期実績明細Dao
        /// </summary>
        private TeikiJissekiDetailDAO teikiJissekiDetailDao;

        /// <summary>
        /// 定期実績荷卸Dao
        /// </summary>
        private TeikiJissekiNioroshiDAO teikiJissekiNioroshiDao;

        /// <summary>
        /// コンテナ情報Dao
        /// </summary>
        private ContenaResultDAO contenaResultDao;

        /// <summary>
        /// コンテナ稼動予定情報Dao
        /// </summary>
        private ContenaReserveDAO contenaReserveDao;

        #endregion

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable SearchResult { get; set; }

        /// <summary>
        /// デジタコ連携車輛マスタリスト
        /// </summary>
        private List<M_DIGI_OUTPUT_SHARYOU> DigiOutputSharyou { get; set; }

        /// <summary>
        /// ロジこん接続管理リスト
        /// </summary>
        private List<S_LOGI_CONNECT> LogiConnect { get; set; }

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO SysInfo { get; set; }

        /// <summary>
        /// デジタコ連携単位
        /// </summary>
        private List<M_DIGI_OUTPUT_UNIT> DigiOutputUnit { get; set; }

        /// <summary>
        /// デジタコ連携品名
        /// </summary>
        private List<M_DIGI_OUTPUT_HINMEI> DigiOutputHinmei { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        private List<M_HINMEI> Hinmei { get; set; }

        /// <summary>
        /// 取込データ登録用DTO
        /// </summary>
        private TorikomiDTO RegistTorikomiDTO { get; set; }

        /// <summary>
        /// データ取込確認用DTO
        /// </summary>
        private List<TorikomiCheckDTO> TorikomiCheckDTOList { get; set; }

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
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.logiConnectDao = DaoInitUtility.GetComponent<IS_LOGI_CONNECTDao>();
            this.logiDeliveryDao = DaoInitUtility.GetComponent<LogiDeliveryDAO>();
            this.logiDeliveryDetailDao = DaoInitUtility.GetComponent<LogiDeliveryDetailDAO>();
            this.logiLinkStatusDao = DaoInitUtility.GetComponent<LogiLinkStatusDAO>();
            this.logiToTeikiDao = DaoInitUtility.GetComponent<LogiToTeikiDAO>();
            this.logiToUrshDao = DaoInitUtility.GetComponent<LogiToUrshDAO>();
            this.digiOutputSharyouDao = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_SHARYOUDao>();
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.digiOutputUnitDao = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_UNITDao>();
            this.digiOutputHinmeiDao = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_HINMEIDao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.kobetsuHinmeiDao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEIDao>();
            this.uketsukeSsEntryDao = DaoInitUtility.GetComponent<UketsukeSsEntryDAO>();
            this.uketsukeSsDetailDao = DaoInitUtility.GetComponent<UketsukeSsDetailDAO>();
            this.uketsukeSkEntryDao = DaoInitUtility.GetComponent<UketsukeSkEntryDAO>();
            this.uketsukeSkDetailDao = DaoInitUtility.GetComponent<UketsukeSkDetailDAO>();
            this.urShEntryDao = DaoInitUtility.GetComponent<UrShEntryDAO>();
            this.urShDetailDao = DaoInitUtility.GetComponent<UrShDetailDAO>();
            this.teikiHaishaEntryDao = DaoInitUtility.GetComponent<TeikiHaishaEntryDAO>();
            this.teikiHaishaDetailDao = DaoInitUtility.GetComponent<TeikiHaishaDetailDAO>();
            this.teikiHaishaShousaiDao = DaoInitUtility.GetComponent<TeikiHaishaShousaiDAO>();
            this.teikiHaishaNioroshiDao = DaoInitUtility.GetComponent<TeikiHaishaNioroshiDAO>();
            this.teikiJissekiEntryDao = DaoInitUtility.GetComponent<TeikiJissekiEntryDAO>();
            this.teikiJissekiDetailDao = DaoInitUtility.GetComponent<TeikiJissekiDetailDAO>();
            this.teikiJissekiNioroshiDao = DaoInitUtility.GetComponent<TeikiJissekiNioroshiDAO>();
            this.contenaResultDao = DaoInitUtility.GetComponent<ContenaResultDAO>();
            this.contenaReserveDao = DaoInitUtility.GetComponent<ContenaReserveDAO>();

            //メッセージ用
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

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
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //新規(F2)イベント作成
            parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);
            //修正(F3)イベント作成
            parentForm.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);
            //削除(F4)イベント作成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            //条件クリア(F7)イベント作成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);
            //検索ボタン(F8)イベント作成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            //取込確認ボタン(F9)イベント作成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            //データ取込ボタン(F10)イベント作成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);
            //閉じるボタン(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //配車区分イベント作成
            this.form.txtNum_HaishaKBN.TextChanged += new EventHandler(this.form.txtNum_HaishaKBN_TextChanged);
            this.form.txtNum_HaishaKBN.Leave += new EventHandler(this.form.txtNum_HaishaKBN_Leave);

            //連携状態イベント作成
            this.form.txtNum_RenkeiJyoukyou.TextChanged += new EventHandler(this.form.txtNum_RenkeiJyoukyou_TextChanged);
            this.form.txtNum_RenkeiJyoukyou.Leave += new EventHandler(this.form.txtNum_RenkeiJyoukyou_Leave);

            //作業日イベント作成
            this.form.TEKIYOU_END.MouseDoubleClick += new MouseEventHandler(this.form.TEKIYOU_END_MouseDoubleClick);
            this.form.TEKIYOU_BEGIN.Leave += new System.EventHandler(this.form.TEKIYOU_BEGIN_Leave);
            this.form.TEKIYOU_END.Leave += new System.EventHandler(this.form.TEKIYOU_END_Leave);

            //明細ダブルクリック時のイベント
            this.form.Ichiran1.CellDoubleClick += new DataGridViewCellEventHandler(this.form.DetailCellDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 抽出条件の初期化
        /// </summary>
        internal void SetInitialRenkeiCondition()
        {
            //作業日 今日～今日
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.TEKIYOU_BEGIN.Value = parentForm.sysDate.Date;
            this.form.TEKIYOU_END.Value = parentForm.sysDate.Date;

            //連携状態 1.未送信
            this.form.txtNum_RenkeiJyoukyou.Text = RENKEI_MISOUSHIN.ToString();
            this.renkei_kbn = int.Parse(this.form.txtNum_RenkeiJyoukyou.Text);

            //配車区分 1.定期配車
            this.form.txtNum_HaishaKBN.Text = HAISHA_TEIKI_KBN.ToString();
            this.haisha_kbn = int.Parse(this.form.txtNum_HaishaKBN.Text);
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                //ボタンのテキストを初期化
                this.ButtonInit();

                //検索条件の初期化処理
                this.SetInitialRenkeiCondition();

                //ボタン制御
                this.ButtonEnabledControl();

                //イベントの初期化処理
                this.EventInit();
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

        #region 配送計画入力呼出し
        /// <summary>
        /// 外部連携現場入力画面の呼び出し
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="newFlg"></param>
        /// <param name="f4BtnUnLock"></param>
        internal void OpenWindow(WINDOW_TYPE windowType, bool newFlg = false, bool f4BtnUnLock = false)
        {
            LogUtility.DebugMethodStart(windowType, newFlg, f4BtnUnLock);

            // 引数へのオブジェクトを作成する
            // 新規の場合は引数なし、ただし参照の場合は引数あり
            if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG && newFlg)
            {
                r_framework.FormManager.FormManager.OpenFormWithAuth("G695", WINDOW_TYPE.NEW_WINDOW_FLAG, windowType);
            }
            else
            {
                // 表示されている行のシステムIDを取得する
                DataGridViewRow row = this.form.Ichiran1.CurrentRow;
                if (row != null)
                {
                    string cd = row.Cells["SYSTEM_ID"].Value.ToString();

                    // 権限チェック
                    // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                    if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                        !r_framework.Authority.Manager.CheckAuthority("G695", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (!r_framework.Authority.Manager.CheckAuthority("G695", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                        {
                            msgLogic.MessageBoxShow("E158", "修正");
                            return;
                        }

                        windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                    }

                    r_framework.FormManager.FormManager.OpenFormWithAuth("G695", windowType, windowType, cd, f4BtnUnLock);
                }
            }

            LogUtility.DebugMethodEnd();
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

                // 日付チェック
                if (this.DateCheck())
                {
                    return -1;
                }

                // 検索条件保持
                this.search_renkei_kbn = int.Parse(this.form.txtNum_RenkeiJyoukyou.Text);

                var sql = new StringBuilder();

                //SQL生成
                //SELECT
                this.CreateSQLSelect(sql);
                //JOIN
                this.CreateSQLJoin(sql);
                //WHERE
                this.CreateSQLWhere(sql);
                //ORDER BY
                this.CreateSQLOrderBy(sql);

                this.mcreateSql = sql.ToString();

                //検索実行
                this.SearchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.mcreateSql))
                {
                    this.SearchResult = this.logiDeliveryDao.getdateforstringsql(this.mcreateSql);
                }

                ret_cnt = SearchResult.Rows.Count;

                //検索結果表示
                this.setIchiran();
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

        #region ボタン制御
        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        internal void ButtonEnabledControl()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 初期化
            parentForm.bt_func2.Enabled = true;
            parentForm.bt_func3.Enabled = true;
            parentForm.bt_func4.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func9.Enabled = true;
            parentForm.bt_func10.Enabled = true;
            parentForm.bt_func12.Enabled = true;

            // データ取込
            parentForm.bt_func10.Enabled = false;

            // 連携状況による制御
            switch (renkei_kbn)
            {
                case RENKEI_MISOUSHIN:
                    parentForm.bt_func9.Enabled = false;
                    break;
                case RENKEI_SOUSHIN:
                    parentForm.bt_func4.Enabled = false;
                    break;
                case RENKEI_JYUSHIN:
                    parentForm.bt_func3.Enabled = false;
                    parentForm.bt_func4.Enabled = false;
                    parentForm.bt_func9.Enabled = false;
                    break;
                default:
                    break;
            }

            // 修正、削除
            if (this.form.Ichiran1.Rows.Count == 0)
            {
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func4.Enabled = false;
            }
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        private bool DateCheck()
        {
            this.form.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            this.form.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.TEKIYOU_BEGIN.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TEKIYOU_END.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.TEKIYOU_BEGIN.Text);
            DateTime date_to = DateTime.Parse(this.form.TEKIYOU_END.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.TEKIYOU_BEGIN.IsInputErrorOccured = true;
                this.form.TEKIYOU_END.IsInputErrorOccured = true;
                this.form.TEKIYOU_BEGIN.BackColor = Constans.ERROR_COLOR;
                this.form.TEKIYOU_END.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                this.msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.TEKIYOU_BEGIN.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region SQL生成
        #region SELECT
        /// <summary>
        /// SELECT句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLSelect(StringBuilder sql)
        {

            //T_LOGI_DELIVERY
            sql.Append(" SELECT DISTINCT ");
            sql.Append(" TLD.SYSTEM_ID ");
            sql.Append(" ,TLD.HAISHA_KBN ");
            sql.Append(" ,TLD.SHASHU_CD ");
            sql.Append(" ,TLD.SHARYOU_CD ");
            sql.Append(" ,TLD.UNTENSHA_CD ");
            sql.Append(" ,TLD.UPN_GYOUSHA_CD ");
            sql.Append(" ,TLD.DELIVERY_DATE ");
            sql.Append(" ,TLD.DELIVERY_NO ");
            sql.Append(" ,TLD.DELIVERY_NAME ");

            //T_LOGI_LINK_STATUS
            sql.Append(" ,TLLS.LINK_STATUS ");

            //計上
            sql.Append(" ,CASE WHEN TUSE.UR_SH_NUMBER IS NOT NULL THEN TUSE.UR_SH_NUMBER WHEN TTJE.TEIKI_JISSEKI_NUMBER IS NOT NULL THEN TTJE.TEIKI_JISSEKI_NUMBER ELSE NULL END AS KEIJYOU");

            //JOINした項目
            sql.Append(" ,MSS.SHASHU_NAME ");
            sql.Append(" ,MSR.SHARYOU_NAME ");
            sql.Append(" ,MU.SHAIN_NAME ");
            sql.Append(" ,MG.GYOUSHA_NAME1 ");

            sql.Append(" FROM T_LOGI_DELIVERY TLD ");
        }
        #endregion
        #region JOIN
        /// <summary>
        /// JOIN句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLJoin(StringBuilder sql)
        {
            //JOIN句
            sql.Append(" INNER JOIN T_LOGI_LINK_STATUS TLLS ON TLD.SYSTEM_ID = TLLS.SYSTEM_ID ");
            sql.Append(" LEFT JOIN M_SHASHU MSS ON TLD.SHASHU_CD = MSS.SHASHU_CD ");
            sql.Append(" LEFT JOIN M_SHARYOU MSR ON TLD.UPN_GYOUSHA_CD = MSR.GYOUSHA_CD AND TLD.SHARYOU_CD = MSR.SHARYOU_CD ");
            sql.Append(" LEFT JOIN M_SHAIN MU ON TLD.UNTENSHA_CD = MU.SHAIN_CD ");
            sql.Append(" LEFT JOIN M_GYOUSHA MG ON TLD.UPN_GYOUSHA_CD = MG.GYOUSHA_CD ");
            sql.Append(" LEFT JOIN T_LOGI_TO_TEIKI TLTT ON TLD.SYSTEM_ID = TLTT.SYSTEM_ID ");
            sql.Append(" LEFT JOIN T_TEIKI_JISSEKI_ENTRY TTJE ON  TLTT.TEIKI_SYSTEM_ID = TTJE.SYSTEM_ID AND TTJE.DELETE_FLG = 0 ");
            sql.Append(" LEFT JOIN T_LOGI_TO_URSH TLTU ON TLD.SYSTEM_ID = TLTU.SYSTEM_ID ");
            sql.Append(" LEFT JOIN T_UR_SH_ENTRY TUSE ON  TLTU.URSH_SYSTEM_ID = TUSE.SYSTEM_ID AND TUSE.DELETE_FLG = 0 ");
        }
        #endregion
        #region WHERE
        /// <summary>
        /// WHERE句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLWhere(StringBuilder sql)
        {
            //WHERE句
            sql.Append(" WHERE ");

            //連携状態
            sql.AppendFormat(" TLLS.LINK_STATUS = {0} ", renkei_kbn);

            //配車区分
            sql.AppendFormat(" AND TLD.HAISHA_KBN = {0} ", haisha_kbn);

            //削除フラグ
            sql.Append(" AND TLD.DELETE_FLG = 0 ");

            //作業日
            // 日時は日付のみにしてから変換
            if (this.form.TEKIYOU_BEGIN.Value != null)
            {
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, TLD.DELIVERY_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '");
                sql.Append(DateTime.Parse(this.form.TEKIYOU_BEGIN.Value.ToString()).ToShortDateString() + "', 111), 120) ");
            }
            if (this.form.TEKIYOU_END.Value != null)
            {
                sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, TLD.DELIVERY_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '");
                sql.Append(DateTime.Parse(this.form.TEKIYOU_END.Value.ToString()).ToShortDateString() + "', 111), 120) ");
            }


        }
        #endregion
        #region ORDER BY
        /// <summary>
        /// ORDERBY句生成
        /// </summary>
        /// <param name="sql"></param>
        private void CreateSQLOrderBy(StringBuilder sql)
        {
            //ORDERBY句
            sql.Append(" ORDER BY ");

            // 配送開始日,配送NO
            sql.Append(" TLD.DELIVERY_DATE ");
            sql.Append(" ,TLD.DELIVERY_NO ");
            sql.Append(" ,KEIJYOU ");
        }
        #endregion
        #endregion

        #region 検索結果表示
        /// <summary>
        /// 一覧にセット
        /// </summary>
        private void setIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //明細をクリアする
                this.form.Ichiran1.Rows.Clear();

                //抽出結果をDGVにセット
                if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                {
                    this.form.Ichiran1.Rows.Add(this.SearchResult.Rows.Count);
                    for (int i = 0; i < this.form.Ichiran1.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.form.Ichiran1.Rows[i];
                        DataRow dr = this.SearchResult.Rows[i];

                        row.Cells["TARGET"].Value = false; //対象
                        row.Cells["DELIVERY_DATE"].Value = dr["DELIVERY_DATE"]; //作業日
                        row.Cells["DELIVERY_NO"].Value = dr["DELIVERY_NO"]; //送信No
                        switch (int.Parse(dr["LINK_STATUS"].ToString()))
                        {
                            case 1:
                                row.Cells["LINK_STATUS"].Value = "未送信";
                                break;
                            case 2:
                                row.Cells["LINK_STATUS"].Value = "送信済(未受信)";
                                break;
                            case 3:
                                row.Cells["LINK_STATUS"].Value = "受信済(完了)";
                                break;
                        }
                        row.Cells["KEIJYOU"].Value = dr["KEIJYOU"];  //計上

                        //以下、とりあえず上記以外のテーブルの項目を表示しておく
                        row.Cells["SHASHU_CD"].Value = dr["SHASHU_CD"];
                        row.Cells["SHASHU_NAME"].Value = dr["SHASHU_NAME"];
                        row.Cells["SHARYOU_CD"].Value = dr["SHARYOU_CD"];
                        row.Cells["SHARYOU_NAME"].Value = dr["SHARYOU_NAME"];
                        row.Cells["UNTENSHA_CD"].Value = dr["UNTENSHA_CD"];
                        row.Cells["UNTENSHA_NAME"].Value = dr["SHAIN_NAME"];
                        row.Cells["UPN_GYOUSHA_CD"].Value = dr["UPN_GYOUSHA_CD"];
                        row.Cells["UPN_GYOUSHA_NAME"].Value = dr["GYOUSHA_NAME1"];
                        row.Cells["DELIVERY_NAME"].Value = dr["DELIVERY_NAME"];
                        row.Cells["SYSTEM_ID"].Value = dr["SYSTEM_ID"];
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 取込確認
        /// <summary>
        /// 取込確認
        /// </summary>
        /// <remarks>一覧に表示されている配送計画用APIを取得し、実績有無が全て有なら対象セルのReadOnlyを解除</remarks>
        /// <returns>true:正常終了, false:異常終了</returns>
        internal bool CanImport()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran1.Rows.Count == 0)
                {
                    return false;
                }

                var deliveryPlansList = new List<DELIVERY_PLANS>();
                if (CHECK_IMPORT)
                {
                    // 取込確認用に配送計画用API取得
                    deliveryPlansList = GetDeliveryPlans();
                    if (deliveryPlansList == null)
                    {
                        return false;
                    }
                }

                // 対象セルのReadOnly解除
                return EditTargetCells(deliveryPlansList);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CanImport", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 配送計画用API取得
        /// </summary>
        /// <returns>配送計画用APIリスト</returns>
        private List<DELIVERY_PLANS> GetDeliveryPlans()
        {
            // 各種マスタ系データ取得
            this.LogiConnect = this.LogiConnect ?? this.logiConnectDao.GetAllData().ToList();
            this.SysInfo = this.SysInfo ?? this.sysInfoDao.GetAllDataForCode("0");
            this.logiLogic = this.logiLogic ?? new LogiLogic();

            List<DELIVERY_PLANS> result = new List<DELIVERY_PLANS>();

            try
            {
                // 対象データを下にJSONデータ取得
                foreach (DataGridViewRow row in this.form.Ichiran1.Rows)
                {
                    // GETに必要な各パラメータ情報取得
                    var digiCarId = string.Empty;
                    {
                        var upnGyoushaCd = row.Cells[ConstCls.CELL_UPN_GYOUSHA_CD].Value.ToString();
                        var sharyouCd = row.Cells[ConstCls.CELL_SHARYOU_CD].Value.ToString();

                        // デジタコ用の車輌ID取得
                        digiCarId = ConvertLogiCarId(upnGyoushaCd, sharyouCd);
                        if (digiCarId == null)
                        {
                            return null;
                        }
                    }
                    var deliveryDate = ((DateTime)row.Cells[ConstCls.CELL_DELIVERY_DATE].Value).ToString("yyyyMMdd");
                    var deliveryNo = row.Cells[ConstCls.CELL_DELIVERY_NO].Value.ToString();

                    // コネクト情報取得
                    var deliConnect = this.LogiConnect.Where(n => n.CONTENT_NAME.Equals(LogiConst.CONTENT_NAME_DELIVERY_PLANS)).First();

                    var apiURL = string.Format(deliConnect.URL + LogiConst.API_PARAMETER_URL_DELIVERY_PLANS, SysInfo.DIGI_CORP_ID, digiCarId, deliveryDate, deliveryNo);

                    // GET
                    var deliveryPlans = logiLogic.HttpGET<DELIVERY_PLANS>(apiURL, deliConnect.CONTENT_TYPE);

                    if (deliveryPlans != null)
                    {
                        result.Add(deliveryPlans);
                    }
                }
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                result = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDeliveryPlans", ex);
                this.msgLogic.MessageBoxShow("E245");
                result = null;
            }
            return result;
        }

        /// <summary>
        /// 車輌CDをデジタコ用にコンバートする(将軍R⇒システック)
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns>車輌CD(デジタコ用)</returns>
        private string ConvertLogiCarId(string gyoushaCd, string sharyouCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(sharyouCd))
            {
                return null;
            }

            this.DigiOutputSharyou = this.DigiOutputSharyou ?? this.digiOutputSharyouDao.GetAllData().ToList();

            var result = this.DigiOutputSharyou.Where(n => n.GYOUSHA_CD.Equals(gyoushaCd) && n.SHARYOU_CD.Equals(sharyouCd)).FirstOrDefault();
            if (result == null)
            {
                this.msgLogic.MessageBoxShowError(string.Format("運搬業者CD【{0}】、車輌CD【{1}】のデータがデジタコ連携車輌が未登録です。", gyoushaCd, sharyouCd));
                return null;
            }

            return result.DIGI_SHARYOU_CD;
        }

        /// <summary>
        /// 対象セルのReadOnlyを解除
        /// </summary>
        /// <param name="deliveryPlansList">配送計画用APIリスト</param>
        /// <returns>true:正常終了, false:異常終了</returns>
        private bool EditTargetCells(List<DELIVERY_PLANS> deliveryPlansList)
        {
            this.TorikomiCheckDTOList = new List<TorikomiCheckDTO>();

            foreach (DataGridViewRow row in this.form.Ichiran1.Rows)
            {
                if (CHECK_IMPORT)
                {
                    // GETに必要な各パラメータ情報取得
                    var digiCarId = string.Empty;
                    {
                        var upnGyoushaCd = row.Cells[ConstCls.CELL_UPN_GYOUSHA_CD].Value.ToString();
                        var sharyouCd = row.Cells[ConstCls.CELL_SHARYOU_CD].Value.ToString();

                        // デジタコ用の車輌ID取得
                        digiCarId = ConvertLogiCarId(upnGyoushaCd, sharyouCd);
                        if (digiCarId == null)
                        {
                            return false;
                        }
                    }
                    var deliveryDate = ((DateTime)row.Cells[ConstCls.CELL_DELIVERY_DATE].Value).ToString("yyyyMMdd");
                    var deliveryNo = row.Cells[ConstCls.CELL_DELIVERY_NO].Value.ToString();

                    var deliveryPlans = deliveryPlansList.Where(n => n.Car_Id.Equals(digiCarId) && n.Delivery_Date.Equals(deliveryDate) && n.Delivery_No.ToString().Equals(deliveryNo)).First();

                    // Is_Actual(実績有無)で1件でも実績無し(false)があれば取込み不可
                    bool readOnly = deliveryPlans.Delivery_Detail.Any(n => !n.Is_Actual);
                    row.Cells[ConstCls.CELL_TARGET].ReadOnly = readOnly;
                    if (!readOnly)
                    {
                        row.Cells[ConstCls.CELL_TARGET].Value = true;

                        // データ取込確認用DTO作成
                        // 地点IDは必須のため、配送明細情報のリスト数でカウント
                        int pointCount = deliveryPlans.Delivery_Detail.Count;
                        var checkDto = new TorikomiCheckDTO() { DELIVERY_DATE = deliveryDate, DELIVERY_NO = deliveryNo, POINT_COUNT = pointCount };
                        this.TorikomiCheckDTOList.Add(checkDto);
                    }
                }
                else
                {
                    // 取込確認未チェックの場合、全て読取専用を解除
                    row.Cells[ConstCls.CELL_TARGET].ReadOnly = false;
                    row.Cells[ConstCls.CELL_TARGET].Value = true;
                }
            }
            this.form.Ichiran1.RefreshEdit();

            // 
            if (CHECK_IMPORT)
            {
                this.msgLogic.MessageBoxShow("I001", "取込確認");
            }
            else
            {
                // 開発用に取込確認をしないため、警告用アラート出力
                this.msgLogic.MessageBoxShowWarn("【開発版】暫定で対象カラムの読取専用を全て解除してます。\r\n管理ツールで配送実績データ作成後、取込み処理を行ってください。");
            }

            return true;
        }
        #endregion

        #region データ取込

        /// <summary>
        /// データ取込のチェック
        /// </summary>
        /// <returns>true:エラー, false:正常</returns>
        internal bool HasErrorImportData()
        {
            if (this.form.Ichiran1 == null || this.form.Ichiran1.Rows.Count == 0)
            {
                this.msgLogic.MessageBoxShow("E268");
                return true;
            }

            // 対象データを下にJSONデータ取得
            bool containsCheckRow = false;
            foreach (DataGridViewRow row in this.form.Ichiran1.Rows)
            {
                if ((bool)row.Cells[ConstCls.CELL_TARGET].Value)
                {
                    containsCheckRow = true;
                    break;
                }
            }
            if (!containsCheckRow)
            {
                this.msgLogic.MessageBoxShow("E051", "データ取込の対象");
                return true;
            }

            return false;
        }

        /// <summary>
        /// データ取込
        /// </summary>
        /// <rereturns>true:正常, false:エラー</rereturns>
        internal bool ImportData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.msgLogic.MessageBoxShow("C055", "データ取込") != DialogResult.Yes)
                {
                    return false;
                }

                // 配送実績API取得
                bool containsMikakuteiPoint;
                var deliveryList = GetDeliveryPerformances(out containsMikakuteiPoint);
                if (deliveryList == null)
                {
                    return false;
                }

                // 登録用データ作成
                if (!TryCreateEntity(deliveryList))
                {
                    return false;
                }

                // データ登録
                RegistData();

                if (containsMikakuteiPoint)
                {
                    // 事務処理側での未確定現場があれば、アラート内容変更
                    this.msgLogic.MessageBoxShowWarn("確定処理が行われていない回収現場があります。\nロジこんぱすにて確定処理を実行後に再度実施してください。");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("I001", "データ取込");
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ImportData", ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 配送実績API取得
        /// </summary>
        /// <param name="containsMikakuteiPoint">true:事務処理側での未確定地点有り, false:事務処理側での未確定地点無し</param>
        /// <returns>配送実績用APIリスト</returns>
        private List<DELIVERY_PERFORMANCES> GetDeliveryPerformances(out bool containsMikakuteiPoint)
        {
            containsMikakuteiPoint = false;

            // 各種マスタ系データ取得
            this.LogiConnect = this.LogiConnect ?? this.logiConnectDao.GetAllData().ToList();
            this.SysInfo = this.SysInfo ?? this.sysInfoDao.GetAllDataForCode("0");
            this.logiLogic = this.logiLogic ?? new LogiLogic();

            List<DELIVERY_PERFORMANCES> result = new List<DELIVERY_PERFORMANCES>();

            try
            {
                StringBuilder warn = new StringBuilder();

                // 対象データを下にJSONデータ取得
                foreach (DataGridViewRow row in this.form.Ichiran1.Rows)
                {
                    var value = (bool)row.Cells[ConstCls.CELL_TARGET].Value;
                    if (!value)
                    {
                        continue;
                    }

                    // GETに必要な各パラメータ情報取得
                    var digiCarId = string.Empty;
                    {
                        var upnGyoushaCd = row.Cells[ConstCls.CELL_UPN_GYOUSHA_CD].Value.ToString();
                        var sharyouCd = row.Cells[ConstCls.CELL_SHARYOU_CD].Value.ToString();

                        // デジタコ用の車輌ID取得
                        digiCarId = ConvertLogiCarId(upnGyoushaCd, sharyouCd);
                        if (digiCarId == null)
                        {
                            return null;
                        }
                    }

                    var deliveryDate = ((DateTime)row.Cells[ConstCls.CELL_DELIVERY_DATE].Value).ToString("yyyyMMdd");
                    var deliveryNo = row.Cells[ConstCls.CELL_DELIVERY_NO].Value.ToString();

                    // コネクト情報取得
                    var deliConnect = this.LogiConnect.Where(n => n.CONTENT_NAME.Equals(LogiConst.CONTENT_NAME_DELIVERY_PERFORMANCES))
                                                      .First();
                    var apiURL = string.Format(deliConnect.URL + LogiConst.API_PARAMETER_URL_DELIVERY_PERFORMANCES, SysInfo.DIGI_CORP_ID, digiCarId, deliveryDate, deliveryNo);

                    // GET
                    var deliveryPerformancs = logiLogic.HttpGET<DELIVERY_PERFORMANCES>(apiURL, deliConnect.CONTENT_TYPE);

                    if (deliveryPerformancs != null)
                    {
                        // 事務側ロジこんぱすでの確定処理が未実施かチェック
                        var checkDto = this.TorikomiCheckDTOList.FirstOrDefault(n => n.DELIVERY_DATE.Equals(deliveryDate) && n.DELIVERY_NO.Equals(deliveryNo));
                        var pointCount = deliveryPerformancs.Delivery_Header.Delivery_Detail.Count;
                        if (checkDto != null && checkDto.POINT_COUNT <= pointCount)
                        {
                            // 計画≦実績の現場数であれば、事務側ロジこんぱすでの確定処理済みとみなし処理対象とする
                            result.Add(deliveryPerformancs);

                            // 配送明細Noが1001以上(他システムで追加された現場)のリストを取得
                            var addGenbaList = deliveryPerformancs.Delivery_Header.Delivery_Detail.Where(n => ConstCls.ADD_EXTERNAL_DETAIL_NO <= n.Delivery_Detail_No).ToList();
                            foreach (var addGenba in addGenbaList)
                            {
                                var dt = GetGenbaForPointId(addGenba.Point_Id);
                                if (dt == null || dt.Rows.Count == 0)
                                {
                                    continue;
                                }

                                var genbaRow = dt.Rows[0];
                                // 「業者CD：現場CD　現場名略称」のフォーマットでエラー情報格納
                                warn.Append(Environment.NewLine)
                                    .AppendFormat("{0}:{1}  {2}", genbaRow["GYOUSHA_CD"].ToString()
                                                                , genbaRow["GENBA_CD"].ToString()
                                                                , genbaRow["GENBA_NAME_RYAKU"].ToString());
                            }
                        }
                        else
                        {
                            if (CHECK_IMPORT)
                            {
                                // 現場数が一致しなければ未実施と判定。取得したAPIも破棄して処理対象外とする
                                containsMikakuteiPoint = true;
                            }
                            else
                            {
                                // 取込確認機能がOFFの場合は無条件で処理対象とする
                                result.Add(deliveryPerformancs);
                            }
                        }
                    }
                }

                var warnStr = warn.ToString();
                if (!string.IsNullOrEmpty(warnStr))
                {
                    var warnMessage = string.Format("外部システムで追加した回収現場は、環境将軍Rに取り込むことはできません。別途、追加現場での回収情報を登録してください。{0}", warnStr);
                    this.msgLogic.MessageBoxShowWarn(warnMessage);
                }
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                result = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDeliveryPerformances", ex);
                this.msgLogic.MessageBoxShow("E245");
                result = null;
            }
            return result;
        }

        /// <summary>
        /// 地点CDから将軍Rの現場マスタを取得する
        /// </summary>
        /// <param name="pointId">地点ID</param>
        /// <returns>現場マスタ</returns>
        private DataTable GetGenbaForPointId(string pointId)
        {
            if (string.IsNullOrEmpty(pointId))
            {
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT GENBA.* FROM M_GENBA_DIGI DIGI ")
                .Append("INNER JOIN M_GENBA GENBA ")
                .Append("ON GENBA.GYOUSHA_CD = DIGI.GYOUSHA_CD AND GENBA.GENBA_CD = DIGI.GENBA_CD ")
                .AppendFormat("WHERE DIGI.POINT_ID = '{0}'", pointId);

            var dt = this.logiDeliveryDao.getdateforstringsql(sql.ToString());

            return dt;
        }

        /// <summary>
        /// 登録データ作成
        /// </summary>
        /// <param name="apiList">配送実績APIリスト</param>
        /// <returns>true:正常, false:エラー</returns>
        private bool TryCreateEntity(List<DELIVERY_PERFORMANCES> apiList)
        {
            bool result = false;

            // 各種マスタ系データ取得
            this.DigiOutputUnit = this.DigiOutputUnit ?? this.digiOutputUnitDao.GetAllData().ToList();
            this.DigiOutputHinmei = this.DigiOutputHinmei ?? this.digiOutputHinmeiDao.GetAllData().ToList();
            this.Hinmei = this.Hinmei ?? this.hinmeiDao.GetAllData().ToList();

            // 登録用DTO初期化
            this.RegistTorikomiDTO = new TorikomiDTO();

            foreach (var api in apiList)
            {
                var apiHeader = api.Delivery_Header;

                // 配送計画取得
                DateTime deliDate = DateTime.ParseExact(apiHeader.Delivery_Date, "yyyyMMdd", null);
                T_LOGI_DELIVERY logiDelivery = this.logiDeliveryDao.GetDataByLogiKey(deliDate, apiHeader.Delivery_No);
                List<T_LOGI_DELIVERY_DETAIL> logiDeliveryDetailList = this.logiDeliveryDetailDao.GetDataBySystemId(logiDelivery.SYSTEM_ID.Value);

                // 作成済み伝票か判定用
                var dicTeiki = new Dictionary<long, long>();// 定期配車.システムID, 定期配車実績.システムID

                foreach (var deliveryDetailApi in apiHeader.Delivery_Detail)
                {
                    if (ConstCls.ADD_EXTERNAL_DETAIL_NO <= deliveryDetailApi.Delivery_Detail_No)
                    {
                        // 1001以上の配送明細Noは外部システム(ロジこんぱす(事務側))で追加された現場のため、処理対象外
                        continue;
                    }
                    
                    // API[配送実績用API.配送実績明細情報.配送明細NO]とDB[配送計画明細.配送No]で配送計画と配送実績を紐つける
                    var logiDeliveryDetailApi = logiDeliveryDetailList.Where(n => n.DELIVERY_ORDER.Value == deliveryDetailApi.Delivery_Detail_No).First();

                    if (logiDeliveryDetailApi.NIZUMI_NIOROSHI_FLG.IsTrue)
                    {
                        // 荷積挿入・荷降挿入で作成されたデータは処理対象外
                        continue;
                    }

                    if (logiDeliveryDetailApi.DENPYOU_ATTR.Value == ConstCls.DENPYOU_ATTR_SS)
                    {
                        // 収集受付
                        var ssEntry = this.uketsukeSsEntryDao.GetDataBySystemId(logiDeliveryDetailApi.REF_SYSTEM_ID.Value);

                        if (deliveryDetailApi.Delivery_Pass_Flag.Equals(ConstCls.DELIVERY_PASS_FLAG_PASS))
                        {
                            // 配送パスした時は、連携もとの受付伝票の配車状況＝５．回収なしに更新
                            CreateSs(ssEntry.UKETSUKE_NUMBER.Value, ConstCls.HAISHA_JOKYO_CD_5, ConstCls.HAISHA_JOKYO_NAME_5);
                            CreateLogiLinkStatus(logiDelivery);

                            // 売上支払伝票は作成しない
                            continue;
                        }

                        var searchSsDetail = new T_UKETSUKE_SS_DETAIL() { SYSTEM_ID = ssEntry.SYSTEM_ID, SEQ = ssEntry.SEQ };
                        var ssDetailArray = this.uketsukeSsDetailDao.GetDataForEntity(searchSsDetail);

                        // 売上支払伝票作成
                        long urshSystemId;
                        if (!TryCreateUrShForSs(ssEntry, ssDetailArray.ToList(), deliveryDetailApi, out urshSystemId))
                        {
                            // 処理失敗時は終了
                            return result;
                        }

                        // コンテナ稼動予定情報からコンテナ稼動実績を作成
                        CreateContenaResult(ssEntry.SYSTEM_ID, ssEntry.SEQ, urshSystemId);

                        // 収集受付伝票の配車状況更新
                        CreateSs(ssEntry.UKETSUKE_NUMBER.Value, ConstCls.HAISHA_JOKYO_CD_3, ConstCls.HAISHA_JOKYO_NAME_3);

                        // 配送計画系のデータ作成
                        CreateLogiLinkStatus(logiDelivery);
                        CreateLogiToUrsh(urshSystemId, logiDelivery.SYSTEM_ID.Value);
                    }
                    else if (logiDeliveryDetailApi.DENPYOU_ATTR.Value == ConstCls.DENPYOU_ATTR_SK)
                    {
                        // 出荷受付
                        var skEntry = this.uketsukeSkEntryDao.GetDataBySystemId(logiDeliveryDetailApi.REF_SYSTEM_ID.Value);

                        if (deliveryDetailApi.Delivery_Pass_Flag.Equals(ConstCls.DELIVERY_PASS_FLAG_PASS))
                        {
                            // 配送パスした時は、連携もとの受付伝票の配車状況＝５．回収なしに更新
                            CreateSk(skEntry.UKETSUKE_NUMBER.Value, ConstCls.HAISHA_JOKYO_CD_5, ConstCls.HAISHA_JOKYO_NAME_5);
                            CreateLogiLinkStatus(logiDelivery);

                            // 売上支払伝票は作成しない
                            continue;
                        }

                        var searchSkDetail = new T_UKETSUKE_SK_DETAIL() { SYSTEM_ID = skEntry.SYSTEM_ID, SEQ = skEntry.SEQ };
                        var skDetailArray = this.uketsukeSkDetailDao.GetDataForEntity(searchSkDetail);

                        // 売上支払伝票作成
                        long urshSystemId;
                        if (!TryCreateUrShForSk(skEntry, skDetailArray.ToList(), deliveryDetailApi, out urshSystemId))
                        {
                            // 処理失敗時は終了
                            return result;
                        }

                        // コンテナ稼動予定情報からコンテナ稼動実績を作成
                        CreateContenaResult(skEntry.SYSTEM_ID, skEntry.SEQ, urshSystemId);

                        // 出荷受付伝票の配車状況更新
                        CreateSk(skEntry.UKETSUKE_NUMBER.Value, ConstCls.HAISHA_JOKYO_CD_3, ConstCls.HAISHA_JOKYO_NAME_3);

                        // 配送計画系のデータ作成
                        CreateLogiLinkStatus(logiDelivery);
                        CreateLogiToUrsh(urshSystemId, logiDelivery.SYSTEM_ID.Value);
                    }
                    else if (logiDeliveryDetailApi.DENPYOU_ATTR.Value == ConstCls.DENPYOU_ATTR_TEIKI)
                    {
                        // 定期配車
                        if (deliveryDetailApi.Delivery_Pass_Flag.Equals(ConstCls.DELIVERY_PASS_FLAG_PASS))
                        {
                            // 配送パスした時は処理終了
                            continue;
                        }

                        var teikiEntry = this.teikiHaishaEntryDao.GetDataBySystemId(logiDeliveryDetailApi.REF_SYSTEM_ID.Value);
                        var teikiNioroshiList = this.teikiHaishaNioroshiDao.GetDataBySystemIdSeq(teikiEntry.SYSTEM_ID.Value, teikiEntry.SEQ.Value);
                        var teikiDetail = this.teikiHaishaDetailDao.GetDataByPK(teikiEntry.SYSTEM_ID.Value, teikiEntry.SEQ.Value, logiDeliveryDetailApi.REF_DETAIL_SYSTEM_ID.Value);
                        var teikiShousaiList = this.teikiHaishaShousaiDao.GetDataByDetailSystemIdEtc(teikiDetail.SYSTEM_ID.Value, teikiDetail.SEQ.Value, teikiDetail.DETAIL_SYSTEM_ID.Value);
                        long jissekiSysmteId;

                        if (dicTeiki.ContainsKey(logiDeliveryDetailApi.REF_SYSTEM_ID.Value))
                        {
                            // 既に作成済みの定期配車実績伝票を取得
                            var jissekiEntry = this.RegistTorikomiDTO.TEIKI_JISSEKI_ENTRY_LIST.Where(n => n.SYSTEM_ID.Value.Equals(dicTeiki[logiDeliveryDetailApi.REF_SYSTEM_ID.Value])).First();
                            if (!TryCreateTeikiJisseki(teikiEntry, teikiNioroshiList, teikiDetail, teikiShousaiList, deliveryDetailApi, jissekiEntry, out jissekiSysmteId))
                            {
                                // 処理失敗時は終了
                                return result;
                            }
                        }
                        else
                        {
                            // 定期配車実績伝票作成
                            if (!TryCreateTeikiJisseki(teikiEntry, teikiNioroshiList, teikiDetail, teikiShousaiList, deliveryDetailApi, null, out jissekiSysmteId))
                            {
                                // 処理失敗時は終了
                                return result;
                            }

                            this.RegistTorikomiDTO.CONTENA_RESULT_LIST = new List<T_CONTENA_RESULT>();

                            // 定期配車のシステムID,定期実績番号で保存。作成済みの定期実績伝票かの判定に使用
                            dicTeiki.Add(logiDeliveryDetailApi.REF_SYSTEM_ID.Value, jissekiSysmteId);

                            // 配送計画系のデータ作成
                            CreateLogiLinkStatus(logiDelivery);
                            CreateLogiToTeiki(jissekiSysmteId, logiDelivery.SYSTEM_ID.Value);
                        }
                    }
                }
            }

            result = true;
            return result;
        }

        /// <summary>
        /// データ登録
        /// </summary>
        private void RegistData()
        {
            if (this.RegistTorikomiDTO == null)
            {
                return;
            }

            using (Transaction tran = new Transaction())
            {
                // 共通
                {
                    // 配送計画連携状況管理テーブル
                    this.RegistTorikomiDTO.LOGI_LINK_STATUS_LIST.ForEach(n => this.logiLinkStatusDao.Update(n));
                }

                // スポット
                {
                    // 収集受付EntrySEQ
                    this.RegistTorikomiDTO.DEL_UKETSUKE_SS_ENTRY_LIST.ForEach(n => this.uketsukeSsEntryDao.Update(n));
                    // 収集受付Entry計上
                    this.RegistTorikomiDTO.INS_UKETSUKE_SS_ENTRY_LIST.ForEach(n => this.uketsukeSsEntryDao.Insert(n));
                    // 収集受付Detail
                    this.RegistTorikomiDTO.INS_UKETSUKE_SS_DETAIL_LIST.ForEach(n => this.uketsukeSsDetailDao.Insert(n));
                    // 出荷受付EntrySEQ
                    this.RegistTorikomiDTO.DEL_UKETSUKE_SK_ENTRY_LIST.ForEach(n => this.uketsukeSkEntryDao.Update(n));
                    // 出荷受付Entry計上
                    this.RegistTorikomiDTO.INS_UKETSUKE_SK_ENTRY_LIST.ForEach(n => this.uketsukeSkEntryDao.Insert(n));
                    // 出荷受付Detail
                    this.RegistTorikomiDTO.INS_UKETSUKE_SK_DETAIL_LIST.ForEach(n => this.uketsukeSkDetailDao.Insert(n));
                    // 売上支払entryテーブル登録
                    this.RegistTorikomiDTO.UR_SH_ENTRY_LIST.ForEach(n => this.urShEntryDao.Insert(n));
                    // 売上支払detailテーブル登録
                    this.RegistTorikomiDTO.UR_SH_DETAIL_LIST.ForEach(n => this.urShDetailDao.Insert(n));
                    // 配送計画to売上支払テーブル登録
                    this.RegistTorikomiDTO.LOGI_TO_URSH_LIST.ForEach(n => this.logiToUrshDao.Insert(n));
                    // コンテナ
                    this.RegistTorikomiDTO.CONTENA_RESULT_LIST.ForEach(n => this.contenaResultDao.Insert(n));
                }

                // 定期配車
                {
                    // 定期配車実績entryテーブル登録
                    this.RegistTorikomiDTO.TEIKI_JISSEKI_ENTRY_LIST.ForEach(n => this.teikiJissekiEntryDao.Insert(n));
                    // 定期配車実績detailテーブル登録
                    this.RegistTorikomiDTO.TEIKI_JISSEKI_DETAIL_LIST.ForEach(n => this.teikiJissekiDetailDao.Insert(n));
                    // 定期配車実績niorosoテーブル登録
                    this.RegistTorikomiDTO.TEIKI_JISSEKI_NIOROSHI_LIST.ForEach(n => this.teikiJissekiNioroshiDao.Insert(n));
                    // 配送計画to定期実績テーブル登録
                    this.RegistTorikomiDTO.LOGI_TO_TEIKI_LIST.ForEach(n => this.logiToTeikiDao.Insert(n));
                }

                // トランザクション終了
                tran.Commit();
            }
        }

        #region 定期・スポット共通
        /// <summary>
        /// 品名CDを将軍R用にコンバートする(システック⇒将軍R)
        /// </summary>
        /// <param name="goodsId">品名ID(システック用)</param>
        /// <param name="hinmeiCd">品名CD(将軍R用)</param>
        /// <returns>true:正常, false:エラー</returns>
        private bool ConvertRHinmeiCd(string goodsId, out string hinmeiCd)
        {
            var digiOutputHinmei = DigiOutputHinmei.Where(n => !string.IsNullOrEmpty(n.DIGI_HINMEI_CD) && n.DIGI_HINMEI_CD.Equals(goodsId)).FirstOrDefault();
            if (digiOutputHinmei == null)
            {
                hinmeiCd = goodsId;

                // コンバート処理失敗
                LogUtility.Error(string.Format("デジタコ品名ID:{0}　コンバート失敗", goodsId));
                this.msgLogic.MessageBoxShowError("デジタコ品名IDの変換に失敗しました。\n配送計画一覧画面を再度開きなおしてください。");

                return false;
            }

            hinmeiCd = digiOutputHinmei.HINMEI_CD;
            return true;
        }

        /// <summary>
        /// 単位CDを将軍R用にコンバートする(システック⇒将軍R)
        /// </summary>
        /// <param name="goodsUnitId">品名単位ID(システック用)</param>
        /// <param name="unitCd">単位CD(将軍R用)</param>
        /// <returns></returns>
        private bool ConvertRUnitCd(string goodsUnitId, out SqlInt16 unitCd)
        {
            var digiOutputUnit = DigiOutputUnit.Where(n => !string.IsNullOrEmpty(n.DIGI_UNIT_CD) && n.DIGI_UNIT_CD.Equals(goodsUnitId)).FirstOrDefault();
            if (digiOutputUnit == null)
            {
                unitCd = SqlInt16.Null;

                // コンバート処理失敗
                LogUtility.Error(string.Format("デジタコ品名単位ID:{0}　コンバート失敗", goodsUnitId));
                this.msgLogic.MessageBoxShowError("デジタコ品名単位IDの変換に失敗しました。\n配送計画一覧画面を再度開きなおしてください。");

                return false;
            }

            unitCd = digiOutputUnit.UNIT_CD;
            return true;
        }

        /// <summary>
        /// 数量を将軍R用にコンバートする(システック⇒将軍R)
        /// </summary>
        /// <param name="goodsCount"></param>
        /// <returns></returns>
        private decimal ConvertRSuuryou(decimal goodsCount)
        {
            // TODO:数量フォーマットと差異がある場合は、ここで処理予定
            return goodsCount;
        }

        /// <summary>
        /// 配送計画をもとに配送計画連携状況管理を作成
        /// </summary>
        /// <param name="logiDelivery">配送計画</param>
        private void CreateLogiLinkStatus(T_LOGI_DELIVERY logiDelivery)
        {
            if (logiDelivery == null || logiDelivery.SYSTEM_ID.IsNull)
            {
                return;
            }

            if (this.RegistTorikomiDTO.LOGI_LINK_STATUS_LIST.Any(n => n.SYSTEM_ID.Value == logiDelivery.SYSTEM_ID.Value))
            {
                // 重複登録対策。スポットで複数の受付伝票を１つの配送計画にした時の対策
                return;
            }

            var logiLinkStatus = this.logiLinkStatusDao.GetDataByCd(logiDelivery.SYSTEM_ID.Value);
            if (logiLinkStatus == null)
            {
                return;
            }

            logiLinkStatus.LINK_STATUS = ConstCls.LINK_STATUS_3;

            string CREATE_USER = logiLinkStatus.CREATE_USER;
            SqlDateTime CREATE_DATE = logiLinkStatus.CREATE_DATE;
            string CREATE_PC = logiLinkStatus.CREATE_PC;
            var dataBinderLinkEntry = new DataBinderLogic<T_LOGI_LINK_STATUS>(logiLinkStatus);
            dataBinderLinkEntry.SetSystemProperty(logiLinkStatus, false);
            logiLinkStatus.CREATE_USER = CREATE_USER;
            logiLinkStatus.CREATE_DATE = CREATE_DATE;
            logiLinkStatus.CREATE_PC = CREATE_PC;

            // Listに追加
            this.RegistTorikomiDTO.LOGI_LINK_STATUS_LIST.Add(logiLinkStatus);
        }
        #endregion

        #region 定期
        /// <summary>
        /// 定期配車伝票をもとに定期配車実績伝票を作成
        /// </summary>
        /// <param name="teikiEntry">定期配車伝票</param>
        /// <param name="teikiNioroshiList">定期配車荷卸リスト</param>
        /// <param name="teikiDetail">定期配車明細</param>
        /// <param name="teikiShousaiList">定期配車詳細リスト</param>
        /// <param name="deliveryDetailAPi">配送実績明細情報</param>
        /// <param name="teikiJissekiEntry">作成済み定期配車実績伝票</param>
        /// <param name="jissekiSysmteId">定期実績入力.システムID</param>
        /// <returns>true:正常, false:エラー</returns>
        /// <remarks>13_配車/G667/Logic/LogicCls.cs/CreateEntityメソッドの「読込系配車明細Entityを作成」箇所を参考に作成</remarks>
        private bool TryCreateTeikiJisseki(T_TEIKI_HAISHA_ENTRY teikiEntry
                                        , List<T_TEIKI_HAISHA_NIOROSHI> teikiNioroshiList
                                        , T_TEIKI_HAISHA_DETAIL teikiDetail
                                        , List<T_TEIKI_HAISHA_SHOUSAI> teikiShousaiList
                                        , DELIVERY_DETAIL_PERFORMANCES deliveryDetailAPi
                                        , T_TEIKI_JISSEKI_ENTRY teikiJissekiEntry
                                        , out long jissekiSysmteId)
        {
            bool result = false;
            jissekiSysmteId = -1;
            Int16 denshuKbn = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;

            var ttje = new T_TEIKI_JISSEKI_ENTRY();
            // 既に作成済みの定期配車実績か判定
            if (teikiJissekiEntry == null)
            {
                #region T_TEIKI_JISSEKI_ENTRY
                ttje.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                ttje.SEQ = 1;
                ttje.KYOTEN_CD = teikiEntry.KYOTEN_CD;
                ttje.FURIKAE_HAISHA_KBN = teikiEntry.FURIKAE_HAISHA_KBN;
                ttje.DAY_CD = teikiEntry.DAY_CD;
                ttje.TEIKI_JISSEKI_NUMBER = SaibanUtil.createDenshuNumber(denshuKbn);
                ttje.WEATHER = string.Empty;
                ttje.DENPYOU_DATE = teikiEntry.SAGYOU_DATE;
                ttje.SAGYOU_DATE = teikiEntry.SAGYOU_DATE;
                ttje.COURSE_NAME_CD = teikiEntry.COURSE_NAME_CD;
                ttje.SHARYOU_CD = teikiEntry.SHARYOU_CD;
                ttje.SHASHU_CD = teikiEntry.SHASHU_CD;
                ttje.UNTENSHA_CD = teikiEntry.UNTENSHA_CD;
                ttje.UNPAN_GYOUSHA_CD = teikiEntry.UNPAN_GYOUSHA_CD;
                ttje.HOJOIN_CD = string.Empty;
                ttje.TEIKI_HAISHA_NUMBER = teikiEntry.TEIKI_HAISHA_NUMBER;
                ttje.MOBILE_SHOGUN_FILE_NAME = string.Empty;
                ttje.DELETE_FLG = false;

                // 自動設定
                var dataBinderJissekiEntry = new DataBinderLogic<T_TEIKI_JISSEKI_ENTRY>(ttje);
                dataBinderJissekiEntry.SetSystemProperty(ttje, false);

                // Listに追加
                this.RegistTorikomiDTO.TEIKI_JISSEKI_ENTRY_LIST.Add(ttje);
                #endregion

                #region T_TEIKI_JISSEKI_NIOROSHI
                int nioroshiRowNo = 0;
                foreach (var teikiNioroshi in teikiNioroshiList)
                {
                    var ttjn = new T_TEIKI_JISSEKI_NIOROSHI();
                    nioroshiRowNo++;

                    ttjn.SYSTEM_ID = ttje.SYSTEM_ID;
                    ttjn.SEQ = 1;
                    ttjn.TEIKI_JISSEKI_NUMBER = ttje.TEIKI_JISSEKI_NUMBER;
                    ttjn.NIOROSHI_NUMBER = nioroshiRowNo;
                    ttjn.ROW_NUMBER = SqlInt16.Parse(nioroshiRowNo.ToString());

                    ttjn.NIOROSHI_GYOUSHA_CD = teikiNioroshi.NIOROSHI_GYOUSHA_CD;
                    ttjn.NIOROSHI_GENBA_CD = teikiNioroshi.NIOROSHI_GENBA_CD;

                    // 自動設定
                    var dataBinderNioroshi = new DataBinderLogic<T_TEIKI_JISSEKI_NIOROSHI>(ttjn);
                    dataBinderNioroshi.SetSystemProperty(ttjn, false);

                    // Listに追加
                    this.RegistTorikomiDTO.TEIKI_JISSEKI_NIOROSHI_LIST.Add(ttjn);
                }
                #endregion
            }
            else
            {
                ttje = teikiJissekiEntry;
            }

            // DETAIL
            #region T_TEIKI_JISSEKI_DETAIL
            int rowNo = this.RegistTorikomiDTO.TEIKI_JISSEKI_DETAIL_LIST.Count(n => n.TEIKI_JISSEKI_NUMBER.Equals(ttje.TEIKI_JISSEKI_NUMBER));
            foreach (var unloadingDetail in deliveryDetailAPi.Unloading_Detail.OrderBy(n => n.Unloading_Detail_No))
            {
                T_TEIKI_JISSEKI_DETAIL ttjd = new T_TEIKI_JISSEKI_DETAIL();

                ttjd.SYSTEM_ID = ttje.SYSTEM_ID;
                ttjd.SEQ = 1;
                ttjd.DETAIL_SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                ttjd.TEIKI_JISSEKI_NUMBER = ttje.TEIKI_JISSEKI_NUMBER;

                rowNo++;
                ttjd.ROW_NUMBER = (short)rowNo;

                ttjd.GYOUSHA_CD = teikiDetail.GYOUSHA_CD;
                ttjd.GENBA_CD = teikiDetail.GENBA_CD;

                string hinmeiCd;
                if (!ConvertRHinmeiCd(unloadingDetail.Goods_Id, out hinmeiCd))
                {
                    return result;
                }
                ttjd.HINMEI_CD = hinmeiCd;

                ttjd.SUURYOU = ConvertRSuuryou(unloadingDetail.Goods_Count);

                SqlInt16 unitCd;
                if (!ConvertRUnitCd(unloadingDetail.Goods_Unit_Id, out unitCd))
                {
                    return result;
                }
                ttjd.UNIT_CD = unitCd;

                // ロジコン連携でKANSAN_SUURYOU,KANSAN_UNIT_CD,SHUUSHUU_TIMEは設定する項目がないため未設定

                T_TEIKI_HAISHA_SHOUSAI shousai;
                {
                    // 積卸連番、品名ID、品名単位IDに合致する定期配車伝票_明細（明細No、品名ID、品名単位）を取得
                    shousai = teikiShousaiList.Where(n => (short)n.ROW_NUMBER.Value == unloadingDetail.Unloading_Detail_No
                                                        && n.HINMEI_CD.Equals(hinmeiCd)
                                                        && n.UNIT_CD.Value == unitCd.Value)
                                              .FirstOrDefault();

                    // 品名ID、品名単位IDに合致する定期配車伝票_明細（明細No、品名ID、品名単位）を取得
                    if (shousai == null)
                    {
                        var list = teikiShousaiList.Where(n => n.HINMEI_CD.Equals(hinmeiCd) && n.UNIT_CD.Value == unitCd.Value)
                                                   .OrderBy(n => n.ROW_NUMBER.Value)
                                                   .ToList();
                        if (list.Count == 0)
                        {
                            // ０件（該当明細ナシ）（例：ドライバーが品名を車載機で追加）
                            // 新規明細行として、定期配車実績伝票明細を登録
                            shousai = null;
                        }
                        else if (list.Count == 1)
                        {
                            // 1件該当した場合 （例：ドライバーが他の品名を削除したことにより積卸Noと明細Noがずれた）
                            // 該当した明細Noで、定期配車実績明細行を登録
                            shousai = list.First();
                        }
                    }
                }

                if (shousai != null)
                {
                    // 定期配車実績伝票の横連携を参考に回収品名詳細を設定
                    ttjd.NIOROSHI_NUMBER = shousai.NIOROSHI_NUMBER;
                }
                ttjd.KAISHUU_BIKOU = string.Empty;
                ttjd.HINMEI_BIKOU = string.Empty;

                var dto = new SearchTeikiTorikomiDTO();
                dto.UNIT_CD = ttjd.UNIT_CD;
                dto.GYOUSHA_CD = ttjd.GYOUSHA_CD;
                dto.HINMEI_CD = ttjd.HINMEI_CD;
                dto.GENBA_CD = ttjd.GENBA_CD;
                dto.ROW_NO = teikiDetail.ROW_NUMBER;
                dto.HAISHA_DENPYOU_NO = int.Parse(teikiDetail.TEIKI_HAISHA_NUMBER.ToString());

                // 品名情報取得
                var hinmeiInfo = this.getTeikiHinmeiInfo(dto);
                ttjd.DENPYOU_KBN_CD = hinmeiInfo.DENPYOU_KBN_CD;
                ttjd.KEIYAKU_KBN = hinmeiInfo.KEIYAKU_KBN;
                ttjd.TSUKIGIME_KBN = hinmeiInfo.KEIJYOU_KBN;
                ttjd.ANBUN_FLG = hinmeiInfo.ANBUN_FLG.IsNull ? false : hinmeiInfo.ANBUN_FLG;

                // INPUT_KBN(入力区分)
                if (shousai == null)
                {
                    // ロジコン連携で新規追加の場合は「直接入力」固定
                    ttjd.INPUT_KBN = ConstCls.INPUT_KBN_1;
                }
                else
                {
                    // 検索条件に紐付く、定期配車情報を取得
                    ttjd.INPUT_KBN = getGenbaTeikiHinmeiInfo(dto);
                }

                // 換算後単位CD、換算数量、換算後単位モバイル出力フラグ
                var kansanData = this.GetKansanData(ttjd, ttjd.TEIKI_JISSEKI_NUMBER.ToString());
                if (kansanData != null)
                {
                    if (false == kansanData.KANSAN_UNIT_CD.IsNull && true == ttjd.KANSAN_UNIT_CD.IsNull)
                    {
                        ttjd.KANSAN_UNIT_CD = kansanData.KANSAN_UNIT_CD;
                    }
                    if (false == kansanData.KANSAN_SUURYOU.IsNull && true == ttjd.KANSAN_SUURYOU.IsNull)
                    {
                        ttjd.KANSAN_SUURYOU = kansanData.KANSAN_SUURYOU;
                    }
                    ttjd.KANSAN_UNIT_MOBILE_OUTPUT_FLG = kansanData.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
                }

                if (!ttjd.KANSAN_SUURYOU.IsNull)
                {
                    var mlogic = new Common.BusinessCommon.Logic.ManifestoLogic();
                    ttjd.KANSAN_SUURYOU = mlogic.GetSuuryoRound(decimal.Parse(ttjd.KANSAN_SUURYOU.ToString()), this.SysInfo.SYS_SUURYOU_FORMAT_CD.ToString());
                }
                // Ver2.2から新たに追加された項目 KAKUTEI_FLG（確定フラグ）False（固定値）
                ttjd.KAKUTEI_FLG = false;

                // 定期実績番号,業者CD,現場CD,品名CDを元に回数決定
                int count = this.RegistTorikomiDTO.TEIKI_JISSEKI_DETAIL_LIST.Count(n => n.TEIKI_JISSEKI_NUMBER.Equals(ttjd.TEIKI_JISSEKI_NUMBER)
                                                                                     && n.GYOUSHA_CD.Equals(ttjd.GYOUSHA_CD)
                                                                                     && n.GENBA_CD.Equals(ttjd.GENBA_CD)
                                                                                     && n.HINMEI_CD.Equals(ttjd.HINMEI_CD));
                ttjd.ROUND_NO = count + 1;

                // 自動設定
                var dataBinderContenaResult = new DataBinderLogic<T_TEIKI_JISSEKI_DETAIL>(ttjd);
                dataBinderContenaResult.SetSystemProperty(ttjd, false);

                this.RegistTorikomiDTO.TEIKI_JISSEKI_DETAIL_LIST.Add(ttjd);
            }
            #endregion

            jissekiSysmteId = ttje.SYSTEM_ID.Value;
            result = true;

            return result;
        }

        /// <summary>
        /// 定期における品名詳細を取得する
        /// </summary>
        /// <param name="dto">検索条件</param>
        /// <returns>定期品名詳細情報</returns>
        /// <remarks>
        /// 定期配車 > 現場定期品名 > 品名マスタ の順で参照する
        /// </remarks>
        internal M_GENBA_TEIKI_HINMEI getTeikiHinmeiInfo(SearchTeikiTorikomiDTO dto)
        {
            var retEntity = new M_GENBA_TEIKI_HINMEI();
            bool anbunGetFlag = false;

            // 検索条件に紐付く、定期配車情報を取得
            DataTable table = this.teikiHaishaEntryDao.GetTeikiHinmeiInfo(dto);
            if ((table != null) && (table.Rows.Count > 0))
            {
                // 伝票区分
                var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                if (false == string.IsNullOrEmpty(denpyouKbnCD))
                {
                    retEntity.DENPYOU_KBN_CD = Int16.Parse(denpyouKbnCD);
                }

                // 契約区分
                var keiyakuKbn = table.Rows[0]["KEIYAKU_KBN"].ToString();
                if (false == string.IsNullOrEmpty(keiyakuKbn))
                {
                    retEntity.KEIYAKU_KBN = Int16.Parse(keiyakuKbn);

                }

                // 按分フラグ
                anbunGetFlag = true;
                var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                if (false == string.IsNullOrEmpty(anbunFlag))
                {
                    retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                }

                // 契約区分が単価の場合のみ計上区分をセットする
                if (retEntity.KEIYAKU_KBN == CommonConst.KEIYAKU_KBN_TANKA)
                {
                    // 計上区分(月極区分)
                    var keijyouKbn = table.Rows[0]["KEIJYOU_KBN"].ToString();
                    if (false == string.IsNullOrEmpty(keijyouKbn))
                    {
                        retEntity.KEIJYOU_KBN = Int16.Parse(keijyouKbn);
                    }
                }
            }
            else
            {
                // 定期配車情報が存在しなかった場合、現場定期品名から取得
                table = this.teikiHaishaEntryDao.GetGenbaTeikiHinmeiDataForEntity(dto);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    if (table.Rows.Count == 1)
                    {
                        // 伝票区分
                        var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                        if (false == string.IsNullOrEmpty(denpyouKbnCD))
                        {
                            retEntity.DENPYOU_KBN_CD = Int16.Parse(denpyouKbnCD);
                        }
                    }
                    else
                    {
                        // 該当情報が複数件の場合は、1.売上をセット
                        retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                    }

                    // 契約区分
                    var keiyakuKbn = table.Rows[0]["KEIYAKU_KBN"].ToString();
                    if (false == string.IsNullOrEmpty(keiyakuKbn))
                    {
                        retEntity.KEIYAKU_KBN = Int16.Parse(keiyakuKbn);

                    }

                    // 契約区分が単価の場合のみ計上区分をセットする
                    if (retEntity.KEIYAKU_KBN == CommonConst.KEIYAKU_KBN_TANKA)
                    {
                        // 計上区分(月極区分)
                        var keijyouKbn = table.Rows[0]["KEIJYOU_KBN"].ToString();
                        if (false == string.IsNullOrEmpty(keijyouKbn))
                        {
                            retEntity.KEIJYOU_KBN = Int16.Parse(keijyouKbn);
                        }
                    }

                    // 按分フラグ
                    anbunGetFlag = true;
                    var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                    if (false == string.IsNullOrEmpty(anbunFlag))
                    {
                        retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                    }
                }
                else
                {
                    // 現場定期品名情報が存在しなかった場合、品名マスタから取得
                    table = this.teikiHaishaEntryDao.GetHinmeiDataForEntity(dto);
                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        // 伝票区分
                        var denpyouKbnCD = table.Rows[0]["DENPYOU_KBN_CD"].ToString();
                        if (false == string.IsNullOrEmpty(denpyouKbnCD))
                        {
                            var cd = Int16.Parse(denpyouKbnCD);
                            if (cd == CommonConst.DENPYOU_KBN_KYOUTSUU)
                            {
                                // 9.共通の場合は、1.売上をセット
                                retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                            }
                            else
                            {
                                retEntity.DENPYOU_KBN_CD = cd;
                            }
                        }

                        // 契約区分は単価をセット
                        retEntity.KEIYAKU_KBN = CommonConst.KEIYAKU_KBN_TANKA;

                        // 計上区分は伝票をセット
                        retEntity.KEIJYOU_KBN = CommonConst.KEIJYOU_KBN_DENPYOU;
                    }
                }
            }

            if ((table == null) || (table.Rows.Count <= 0))
            {
                // 該当情報が無ければ、デフォルト値をセット

                // 伝票区分は売上をセット
                retEntity.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;

                // 契約区分は単価をセット
                retEntity.KEIYAKU_KBN = CommonConst.KEIYAKU_KBN_TANKA;

                // 計上区分は伝票をセット
                retEntity.KEIJYOU_KBN = CommonConst.KEIJYOU_KBN_DENPYOU;
            }

            // 按分フラグは現場定期品名から取得する
            if (anbunGetFlag == false)
            {
                table = this.teikiHaishaEntryDao.GetGenbaTeikiHinmeiDataForEntity(dto);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    var anbunFlag = table.Rows[0]["ANBUN_FLG"].ToString();
                    if (false == string.IsNullOrEmpty(anbunFlag))
                    {
                        retEntity.ANBUN_FLG = Boolean.Parse(anbunFlag);
                    }
                }
            }

            return retEntity;
        }

        /// <summary>
        /// 定期における現場品名入力区分を取得する
        /// </summary>
        /// <param name="dto">検索条件</param>
        /// <returns>定期品名詳細情報</returns>
        /// <remarks>
        /// 定期配車 > 現場定期品名
        /// </remarks>
        internal SqlInt16 getGenbaTeikiHinmeiInfo(SearchTeikiTorikomiDTO dto)
        {
            SqlInt16 ret = ConstCls.INPUT_KBN_1;

            // 定期配車情報が存在しなかった場合、現場定期品名から取得
            var table = this.teikiHaishaEntryDao.GetGenbaTeikiHinmeiDataForEntity(dto);
            if ((table != null) && (table.Rows.Count > 0))
            {
                ret = ConstCls.INPUT_KBN_2;
            }
            else
            {
                ret = ConstCls.INPUT_KBN_1;
            }

            return ret;
        }

        /// <summary>
        /// 換算値を取得する
        /// </summary>
        /// <param name="data">登録しようとしている実績明細情報</param>
        /// <param name="teikiHaishaNumber">定期配車番号</param>
        /// <returns name="DataTable">条件にヒットした換算値&換算単位を格納したEntity</returns>
        /// <remarks>該当するものが無い場合はnullを返却</remarks>
        private T_TEIKI_JISSEKI_DETAIL GetKansanData(T_TEIKI_JISSEKI_DETAIL data, string teikiHaishaNumber)
        {
            // 単位CD:[Kg]
            const string unitCdKg = "3";
            var returnData = new T_TEIKI_JISSEKI_DETAIL();

            // 要記入フラグ
            if (data.KANSAN_UNIT_MOBILE_OUTPUT_FLG.IsNull)
            {
                returnData.KANSAN_UNIT_MOBILE_OUTPUT_FLG = SqlBoolean.False;
            }
            else
            {
                returnData.KANSAN_UNIT_MOBILE_OUTPUT_FLG = data.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
            }

            // 取込データに換算後単位があればそれを使用（ここでは処理する必要なし）
            // 回収品名詳細に換算後単位があればそれを使用
            // 現場定期品名に換算後単位があればそれを使用

            // ① 他の単位をKgへ換算の場合
            // ② Kgを他の単位へ換算の場合
            // ③ Kg→Kgへの単位換算の場合
            //《公式》:[換算値] × [数量] = [換算後数量]
            // 数量が未入力の場合は換算後数量の計算を行わない

            // 現場定期品名から取得する
            var genbaTeikiEntity = this.teikiHaishaEntryDao.GetGenbaTeikiHinmeiDataForEntity(new SearchTeikiTorikomiDTO()
            {
                GYOUSHA_CD = data.GYOUSHA_CD,
                GENBA_CD = data.GENBA_CD,
                HINMEI_CD = data.HINMEI_CD,
                UNIT_CD = data.UNIT_CD,
                DENPYOU_KBN_CD = data.DENPYOU_KBN_CD
            });
            if (genbaTeikiEntity.Rows.Count > 0)
            {
                var kansanUnitCd = genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSAN_UNIT_CD")];
                if (null != kansanUnitCd && !String.IsNullOrEmpty(kansanUnitCd.ToString()))
                {
                    returnData.KANSAN_UNIT_CD = SqlInt16.Parse(kansanUnitCd.ToString());
                    if (false == data.SUURYOU.IsNull
                        && (null != genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")] && !String.IsNullOrEmpty(genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")].ToString()))
                        && (unitCdKg == returnData.KANSAN_UNIT_CD.ToString() || unitCdKg == data.UNIT_CD.ToString())
                        )
                    {
                        returnData.KANSAN_SUURYOU = SqlDecimal.Parse(genbaTeikiEntity.Rows[0].ItemArray[genbaTeikiEntity.Columns.IndexOf("KANSANCHI")].ToString()) * data.SUURYOU;
                    }
                }
            }

            // 回収品名詳細から取得する
            var dtKansanData = this.teikiHaishaEntryDao.GetKansanData(new SearchTeikiTorikomiDTO()
            {
                HAISHA_DENPYOU_NO = Int32.Parse(teikiHaishaNumber),
                GYOUSHA_CD = data.GYOUSHA_CD,
                GENBA_CD = data.GENBA_CD,
                HINMEI_CD = data.HINMEI_CD,
                UNIT_CD = data.UNIT_CD,
                DENPYOU_KBN_CD = data.DENPYOU_KBN_CD
            });
            if (dtKansanData.Rows.Count > 0)
            {
                var kansanUnitCd = dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSAN_UNIT_CD")];
                if (null != kansanUnitCd && !String.IsNullOrEmpty(kansanUnitCd.ToString()))
                {
                    returnData.KANSAN_UNIT_CD = SqlInt16.Parse(kansanUnitCd.ToString());
                    if (false == data.SUURYOU.IsNull
                        && (null != dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")] && !String.IsNullOrEmpty(dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")].ToString()))
                        && (unitCdKg == returnData.KANSAN_UNIT_CD.ToString() || unitCdKg == data.UNIT_CD.ToString())
                        )
                    {
                        returnData.KANSAN_SUURYOU = SqlDecimal.Parse(dtKansanData.Rows[0].ItemArray[dtKansanData.Columns.IndexOf("KANSANCHI")].ToString()) * data.SUURYOU;
                    }
                }
            }

            if (false == data.KANSAN_UNIT_CD.IsNull)
            {
                returnData.KANSAN_UNIT_CD = data.KANSAN_UNIT_CD;
            }

            return returnData;
        }

        /// <summary>
        /// 配送計画to定期実績データ作成
        /// </summary>
        /// <param name="teikiSystemId">定期実績システムID</param>
        /// <param name="systemId">システムID</param>
        private void CreateLogiToTeiki(long teikiSystemId, long systemId)
        {
            var entity = new T_LOGI_TO_TEIKI();
            entity.TEIKI_SYSTEM_ID = teikiSystemId;
            entity.SYSTEM_ID = systemId;
            entity.DELETE_FLG = false;

            var dataBinderLinkEntry = new DataBinderLogic<T_LOGI_TO_TEIKI>(entity);
            dataBinderLinkEntry.SetSystemProperty(entity, false);

            // Listに追加
            this.RegistTorikomiDTO.LOGI_TO_TEIKI_LIST.Add(entity);
        }

        #endregion

        #region スポット
        /// <summary>
        /// 収集受付伝票をもとに売上支払伝票を作成
        /// </summary>
        /// <param name="ssEntry">収集受付伝票</param>
        /// <param name="ssDetailList">収集受付明細リスト</param>
        /// <param name="deliveryDetailAPi">配送実績明細情報</param>
        /// <param name="urshSystemId">売上支払入力.システムID</param>
        /// <returns>true:正常, false:エラー</returns>
        /// <remarks>13_配車/G667/Logic/LogicCls.cs/CreateEntityメソッドの「読込系受付明細Entityを作成」箇所を参考に作成</remarks>
        private bool TryCreateUrShForSs(T_UKETSUKE_SS_ENTRY ssEntry, List<T_UKETSUKE_SS_DETAIL> ssDetailList, DELIVERY_DETAIL_PERFORMANCES deliveryDetailAPi, out long urshSystemId)
        {
            bool result = false;
            urshSystemId = -1;
            SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.URIAGE_SHIHARAI).ToString());
            DateTime date = DateTime.Parse(ssEntry.SAGYOU_DATE);

            #region T_UR_SH_ENTRY
            T_UR_SH_ENTRY urshe = new T_UR_SH_ENTRY();
            urshe.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
            urshe.SEQ = 1;
            urshe.KYOTEN_CD = ssEntry.KYOTEN_CD;
            urshe.UR_SH_NUMBER = SaibanUtil.createDenshuNumber(denshuKbn);
            urshe.KAKUTEI_KBN = this.SysInfo.UR_SH_KAKUTEI_FLAG;

            urshe.DENPYOU_DATE = date;
            urshe.URIAGE_DATE = date;
            urshe.SHIHARAI_DATE = date;

            urshe.TORIHIKISAKI_CD = ssEntry.TORIHIKISAKI_CD;
            urshe.TORIHIKISAKI_NAME = ssEntry.TORIHIKISAKI_NAME;
            urshe.GYOUSHA_CD = ssEntry.GYOUSHA_CD;
            urshe.GYOUSHA_NAME = ssEntry.GYOUSHA_NAME;
            urshe.GENBA_CD = ssEntry.GENBA_CD;
            urshe.GENBA_NAME = ssEntry.GENBA_NAME;

            // 
            urshe.NIOROSHI_GYOUSHA_CD = ssEntry.NIOROSHI_GYOUSHA_CD;
            urshe.NIOROSHI_GYOUSHA_NAME = ssEntry.NIOROSHI_GYOUSHA_NAME;
            urshe.NIOROSHI_GENBA_CD = ssEntry.NIOROSHI_GENBA_CD;
            urshe.NIOROSHI_GENBA_NAME = ssEntry.NIOROSHI_GENBA_NAME;

            urshe.EIGYOU_TANTOUSHA_CD = ssEntry.EIGYOU_TANTOUSHA_CD;
            urshe.EIGYOU_TANTOUSHA_NAME = ssEntry.EIGYOU_TANTOUSHA_NAME;

            var nyuuryoku = this.shainDao.GetDataByCd(SystemProperty.Shain.CD);
            if (nyuuryoku != null && nyuuryoku.NYUURYOKU_TANTOU_KBN.IsTrue)
            {
                urshe.NYUURYOKU_TANTOUSHA_CD = SystemProperty.Shain.CD;
                urshe.NYUURYOKU_TANTOUSHA_NAME = SystemProperty.Shain.Name;
            }
            urshe.SHARYOU_CD = ssEntry.SHARYOU_CD;
            urshe.SHARYOU_NAME = ssEntry.SHARYOU_NAME;
            urshe.SHASHU_CD = ssEntry.SHASHU_CD;
            urshe.SHASHU_NAME = ssEntry.SHASHU_NAME;
            urshe.UNPAN_GYOUSHA_CD = ssEntry.UNPAN_GYOUSHA_CD;
            urshe.UNPAN_GYOUSHA_NAME = ssEntry.UNPAN_GYOUSHA_NAME;
            urshe.UNTENSHA_CD = ssEntry.UNTENSHA_CD;
            urshe.UNTENSHA_NAME = ssEntry.UNTENSHA_NAME;

            urshe.CONTENA_SOUSA_CD = ssEntry.CONTENA_SOUSA_CD;

            urshe.MANIFEST_SHURUI_CD = ssEntry.MANIFEST_SHURUI_CD;
            urshe.MANIFEST_TEHAI_CD = ssEntry.MANIFEST_TEHAI_CD;

            urshe.UKETSUKE_NUMBER = ssEntry.UKETSUKE_NUMBER;
            urshe.DAINOU_FLG = false;

            // 取引先請求支払情報
            M_TORIHIKISAKI_SEIKYUU torihikiSeikyuInfo = null;
            M_TORIHIKISAKI_SHIHARAI torihikiShiharaiInfo = null;

            // 税計算区分、税区分、取引区分
            if (!string.IsNullOrEmpty(ssEntry.TORIHIKISAKI_CD))
            {
                var seikyuu = new M_TORIHIKISAKI_SEIKYUU() { TORIHIKISAKI_CD = ssEntry.TORIHIKISAKI_CD };
                torihikiSeikyuInfo = MasterUtility.GetTorihikisakiSeikyuu(seikyuu);
                if (torihikiSeikyuInfo != null)
                {
                    urshe.URIAGE_ZEI_KEISAN_KBN_CD = torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD;
                    urshe.URIAGE_ZEI_KBN_CD = torihikiSeikyuInfo.ZEI_KBN_CD;
                    urshe.URIAGE_TORIHIKI_KBN_CD = torihikiSeikyuInfo.TORIHIKI_KBN_CD;
                }

                var shiharai = new M_TORIHIKISAKI_SHIHARAI() { TORIHIKISAKI_CD = ssEntry.TORIHIKISAKI_CD };
                torihikiShiharaiInfo = MasterUtility.GetTorihikisakiShiharai(shiharai);
                if (torihikiShiharaiInfo != null)
                {
                    urshe.SHIHARAI_ZEI_KEISAN_KBN_CD = torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD;
                    urshe.SHIHARAI_ZEI_KBN_CD = torihikiShiharaiInfo.ZEI_KBN_CD;
                    urshe.SHIHARAI_TORIHIKI_KBN_CD = torihikiShiharaiInfo.TORIHIKI_KBN_CD;
                }
            }

            // 日連番取得
            urshe.DATE_NUMBER = SaibanUtil.CreateNumberDay(date, denshuKbn, ssEntry.KYOTEN_CD);

            // 年連番取得
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(date, (short)MasterUtility.GetCorpInfo().KISHU_MONTH);
            urshe.YEAR_NUMBER = SaibanUtil.CreateNumberYear(numberedYear, denshuKbn, ssEntry.KYOTEN_CD);
            #endregion

            // DETAIL
            #region T_UR_SH_DETAIL
            List<T_UR_SH_DETAIL> tempUrShDetail = new List<T_UR_SH_DETAIL>();
            short rowNo = 0;
            foreach (var unloadingDetail in deliveryDetailAPi.Unloading_Detail.OrderBy(n => n.Unloading_Detail_No))
            {
                // 対象品名の伝種区分CDチェック
                M_HINMEI hinmei = null;
                string hinmeiCd;
                if (!ConvertRHinmeiCd(unloadingDetail.Goods_Id, out hinmeiCd))
                {
                    return result;
                }
                if (!string.IsNullOrEmpty(hinmeiCd))
                {
                    hinmei = this.Hinmei.Where(n => n.HINMEI_CD.Equals(hinmeiCd)).FirstOrDefault();
                    if (hinmei != null && (hinmei.DENSHU_KBN_CD == CommonConst.DENSHU_KBN_UKEIRE || hinmei.DENSHU_KBN_CD == CommonConst.DENSHU_KBN_SHUKKA))
                    {
                        // 伝種区分が「受入」or「出荷」の場合は処理対象外
                        continue;
                    }
                }

                T_UR_SH_DETAIL urshd = new T_UR_SH_DETAIL();
                rowNo++;

                urshd.SYSTEM_ID = urshe.SYSTEM_ID;
                urshd.SEQ = 1;
                urshd.DETAIL_SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                urshd.UR_SH_NUMBER = urshe.UR_SH_NUMBER;
                //urshd.ROW_NO = (SqlInt16)unloadingDetail.Unloading_Detail_No;
                urshd.ROW_NO = rowNo;
                urshd.KAKUTEI_KBN = this.SysInfo.UR_SH_KAKUTEI_FLAG;
                urshd.URIAGESHIHARAI_DATE = date;

                SqlInt16 unitCd;
                if (!ConvertRUnitCd(unloadingDetail.Goods_Unit_Id, out unitCd))
                {
                    return result;
                }

                T_UKETSUKE_SS_DETAIL ssDetail;
                {
                    // 積卸連番、品名ID、品名単位IDに合致する受付明細（明細No、品名ID、品名単位）を取得
                    ssDetail = ssDetailList.Where(n => n.ROW_NO.Value == unloadingDetail.Unloading_Detail_No
                                                    && n.HINMEI_CD.Equals(hinmeiCd)
                                                    && n.UNIT_CD.Value == unitCd.Value)
                                           .FirstOrDefault();

                    // 品名ID、品名単位IDに合致する受付明細（明細No、品名ID、品名単位）を取得
                    if (ssDetail == null)
                    {
                        var list = ssDetailList.Where(n => n.HINMEI_CD.Equals(hinmeiCd) && n.UNIT_CD.Value == unitCd.Value)
                                               .OrderBy(n => n.ROW_NO.Value)
                                               .ToList();
                        if (list.Count == 0)
                        {
                            // ０件（該当明細ナシ）（例：ドライバーが品名を車載機で追加）
                            // 新規明細行として、売上伝票明細を登録
                            ssDetail = null;
                        }
                        else if (list.Count == 1)
                        {
                            // 1件該当した場合 （例：ドライバーが他の品名を削除したことにより積卸Noと明細Noがずれた）
                            // 該当した明細Noで、売上伝票明細行を登録
                            ssDetail = list.First();
                        }
                        else
                        {
                            // 複数件該当した場合　（例：可燃という品名を単価違いで2行登録している）
                            // 明細Noの小さい行の単価情報で、売上伝票明細行を登録

                            // 既に作成済みの売上／支払明細のリストを元に同品名・単位の登録件数を調べ、それをインデックスとして取得する
                            int index = this.RegistTorikomiDTO.UR_SH_DETAIL_LIST.Count(n => n.SYSTEM_ID.Value == urshd.SYSTEM_ID.Value
                                                                                         && n.HINMEI_CD.Equals(hinmeiCd)
                                                                                         && n.UNIT_CD.Value == unitCd.Value);
                            if (index < list.Count)
                            {
                                ssDetail = list[index];
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(hinmeiCd))
                {
                    urshd.HINMEI_CD = hinmeiCd;
                    if (hinmei != null)
                    {
                        // 品名
                        var kobetsuHinmei = new M_KOBETSU_HINMEI() { GYOUSHA_CD = urshe.GYOUSHA_CD, GENBA_CD = urshe.GENBA_CD, HINMEI_CD = hinmeiCd };
                        var hinmeiName = this.kobetsuHinmeiDao.GetDataByCd(kobetsuHinmei);
                        if (hinmeiName != null)
                        {
                            urshd.HINMEI_NAME = hinmeiName.SEIKYUU_HINMEI_NAME;
                        }
                        else
                        {
                            var kobetsuHinmei2 = new M_KOBETSU_HINMEI() { GYOUSHA_CD = urshe.GYOUSHA_CD, GENBA_CD = string.Empty, HINMEI_CD = hinmeiCd };
                            var hinmeiName2 = this.kobetsuHinmeiDao.GetDataByCd(kobetsuHinmei2);
                            if (hinmeiName2 != null)
                            {
                                urshd.HINMEI_NAME = hinmeiName2.SEIKYUU_HINMEI_NAME;
                            }
                            else
                            {
                                urshd.HINMEI_NAME = hinmei.HINMEI_NAME;
                            }
                        }

                        // 税区分CD
                        if (!hinmei.ZEI_KBN_CD.IsNull)
                        {
                            urshd.HINMEI_ZEI_KBN_CD = hinmei.ZEI_KBN_CD;
                        }

                        // 伝票区分CD
                        if (ssDetail != null && ssDetail.HINMEI_CD.Equals(urshd.HINMEI_CD))
                        {
                            urshd.DENPYOU_KBN_CD = ssDetail.DENPYOU_KBN_CD;
                        }
                        else
                        {
                            // 伝票区分：支払以外は全て売上として扱う
                            if (CommonConst.DENPYOU_KBN_SHIHARAI == hinmei.DENPYOU_KBN_CD)
                            {
                                // 伝票区分：支払
                                urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_SHIHARAI;
                            }
                            else
                            {
                                // 伝票区分：売上
                                urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                            }
                        }
                    }
                }

                // 数量
                urshd.SUURYOU = ConvertRSuuryou(unloadingDetail.Goods_Count);
                // 単位CD
                urshd.UNIT_CD = unitCd;
                // 単価
                SqlDecimal kingaku = SqlDecimal.Null;
                // 受付番号に紐付く受付収集伝票が存在する場合
                if (ssEntry != null)
                {
                    if (!string.IsNullOrEmpty(urshd.HINMEI_CD) &&
                        !urshd.UNIT_CD.IsNull &&
                        !urshd.DENPYOU_KBN_CD.IsNull)
                    {
                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD, ssEntry, ssDetail, urshd, out kingaku);
                    }
                }
                else
                {
                    // 伝票登録情報をキーに単価を取得
                    if (!string.IsNullOrEmpty(urshd.HINMEI_CD) &&
                        !urshd.UNIT_CD.IsNull &&
                        !urshd.DENPYOU_KBN_CD.IsNull)
                    {
                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD,
                                                                         urshe,
                                                                         urshd.HINMEI_CD,
                                                                         (Int16)urshd.UNIT_CD);
                    }
                }

                if (ssDetail != null && !string.IsNullOrEmpty(ssDetail.MEISAI_BIKOU))
                {
                    urshd.MEISAI_BIKOU = ssDetail.MEISAI_BIKOU;
                }
                else
                {
                    urshd.MEISAI_BIKOU = string.Empty;
                }

                // NISUGATA_SUURYOU,NISUGATA_UNIT_CDは設定元の項目がないため除外

                // 伝票区分によった消費税端数CDの格納
                Int16 taxHasuCD = 0;
                Int16 kinHasuCD = 0;
                if (false == urshd.DENPYOU_KBN_CD.IsNull)
                {
                    if (CommonConst.DENPYOU_KBN_SHIHARAI == urshd.DENPYOU_KBN_CD)
                    {
                        if (torihikiShiharaiInfo != null)
                        {
                            // 取引先支払情報より端数CDの取得
                            taxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
                            kinHasuCD = (Int16)torihikiShiharaiInfo.KINGAKU_HASUU_CD;
                        }
                    }
                    else
                    {
                        if (torihikiSeikyuInfo != null)
                        {
                            // 取引先請求情報より端数CDの取得
                            taxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
                            kinHasuCD = (Int16)torihikiSeikyuInfo.KINGAKU_HASUU_CD;
                        }
                    }
                }

                // 明細金額再計算
                this.DetailCalc(urshe, urshd, taxHasuCD, kinHasuCD, kingaku);

                // 売上_支払明細テーブル登録
                this.RegistTorikomiDTO.UR_SH_DETAIL_LIST.Add(urshd);
                tempUrShDetail.Add(urshd);
            }
            #endregion

            // 総計取得
            this.GetMoneyTotal(urshe, tempUrShDetail);

            // 端数CDの取得
            Int16 shiTaxHasuCD = 0;
            Int16 uriTaxHasuCD = 0;
            if (torihikiShiharaiInfo != null)
            {
                // 取引先支払情報より端数CDの取得
                shiTaxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
            }
            if (torihikiSeikyuInfo != null)
            {
                // 取引先請求情報より端数CDの取得
                uriTaxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
            }

            // 伝票消費税計算
            this.EntryTaxCalc(urshe, shiTaxHasuCD, "支払");
            this.EntryTaxCalc(urshe, uriTaxHasuCD, "売上");

            // 売上支払伝票登録
            var bindLogic = new DataBinderLogic<T_UR_SH_ENTRY>(urshe);
            bindLogic.SetSystemProperty(urshe, false);
            urshe.DELETE_FLG = false;

            this.RegistTorikomiDTO.UR_SH_ENTRY_LIST.Add(urshe);

            urshSystemId = urshe.SYSTEM_ID.Value;
            result = true;

            return result;
        }

        /// <summary>
        /// 出荷受付伝票をもとに売上支払伝票を作成
        /// </summary>
        /// <param name="skEntry">出荷受付伝票</param>
        /// <param name="skDetailList">出荷受付明細リスト</param>
        /// <param name="deliveryDetailAPi">配送実績明細情報</param>
        /// <param name="urshSystemId">売上支払入力.システムID</param>
        /// <returns>true:正常, false:エラー</returns>
        /// <remarks>13_配車/G667/Logic/LogicCls.cs/CreateEntityメソッドの「読込系受付明細Entityを作成」箇所を参考に作成</remarks>
        private bool TryCreateUrShForSk(T_UKETSUKE_SK_ENTRY skEntry, List<T_UKETSUKE_SK_DETAIL> skDetailList, DELIVERY_DETAIL_PERFORMANCES deliveryDetailAPi, out long urshSystemId)
        {
            bool result = false;
            urshSystemId = -1;
            SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.URIAGE_SHIHARAI).ToString());
            DateTime date = DateTime.Parse(skEntry.SAGYOU_DATE);

            #region T_UR_SH_ENTRY
            T_UR_SH_ENTRY urshe = new T_UR_SH_ENTRY();
            urshe.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
            urshe.SEQ = 1;
            urshe.KYOTEN_CD = skEntry.KYOTEN_CD;
            urshe.UR_SH_NUMBER = SaibanUtil.createDenshuNumber(denshuKbn);
            urshe.KAKUTEI_KBN = this.SysInfo.UR_SH_KAKUTEI_FLAG;

            urshe.DENPYOU_DATE = date;
            urshe.URIAGE_DATE = date;
            urshe.SHIHARAI_DATE = date;

            urshe.TORIHIKISAKI_CD = skEntry.TORIHIKISAKI_CD;
            urshe.TORIHIKISAKI_NAME = skEntry.TORIHIKISAKI_NAME;
            urshe.GYOUSHA_CD = skEntry.GYOUSHA_CD;
            urshe.GYOUSHA_NAME = skEntry.GYOUSHA_NAME;
            urshe.GENBA_CD = skEntry.GENBA_CD;
            urshe.GENBA_NAME = skEntry.GENBA_NAME;

            //// 
            //urshe.NIOROSHI_GYOUSHA_CD = skEntry.NIOROSHI_GYOUSHA_CD;
            //urshe.NIOROSHI_GYOUSHA_NAME = skEntry.NIOROSHI_GYOUSHA_NAME;
            //urshe.NIOROSHI_GENBA_CD = skEntry.NIOROSHI_GENBA_CD;
            //urshe.NIOROSHI_GENBA_NAME = skEntry.NIOROSHI_GENBA_NAME;

            urshe.EIGYOU_TANTOUSHA_CD = skEntry.EIGYOU_TANTOUSHA_CD;
            urshe.EIGYOU_TANTOUSHA_NAME = skEntry.EIGYOU_TANTOUSHA_NAME;

            var nyuuryoku = this.shainDao.GetDataByCd(SystemProperty.Shain.CD);
            if (nyuuryoku != null && nyuuryoku.NYUURYOKU_TANTOU_KBN.IsTrue)
            {
                urshe.NYUURYOKU_TANTOUSHA_CD = SystemProperty.Shain.CD;
                urshe.NYUURYOKU_TANTOUSHA_NAME = SystemProperty.Shain.Name;
            }
            urshe.SHARYOU_CD = skEntry.SHARYOU_CD;
            urshe.SHARYOU_NAME = skEntry.SHARYOU_NAME;
            urshe.SHASHU_CD = skEntry.SHASHU_CD;
            urshe.SHASHU_NAME = skEntry.SHASHU_NAME;
            urshe.UNPAN_GYOUSHA_CD = skEntry.UNPAN_GYOUSHA_CD;
            urshe.UNPAN_GYOUSHA_NAME = skEntry.UNPAN_GYOUSHA_NAME;
            urshe.UNTENSHA_CD = skEntry.UNTENSHA_CD;
            urshe.UNTENSHA_NAME = skEntry.UNTENSHA_NAME;

            urshe.MANIFEST_SHURUI_CD = skEntry.MANIFEST_SHURUI_CD;
            urshe.MANIFEST_TEHAI_CD = skEntry.MANIFEST_TEHAI_CD;

            urshe.UKETSUKE_NUMBER = skEntry.UKETSUKE_NUMBER;
            urshe.DAINOU_FLG = false;

            // 取引先請求支払情報
            M_TORIHIKISAKI_SEIKYUU torihikiSeikyuInfo = null;
            M_TORIHIKISAKI_SHIHARAI torihikiShiharaiInfo = null;

            // 税計算区分、税区分、取引区分
            if (!string.IsNullOrEmpty(skEntry.TORIHIKISAKI_CD))
            {
                var seikyuu = new M_TORIHIKISAKI_SEIKYUU() { TORIHIKISAKI_CD = skEntry.TORIHIKISAKI_CD };
                torihikiSeikyuInfo = MasterUtility.GetTorihikisakiSeikyuu(seikyuu);
                if (torihikiSeikyuInfo != null)
                {
                    urshe.URIAGE_ZEI_KEISAN_KBN_CD = torihikiSeikyuInfo.ZEI_KEISAN_KBN_CD;
                    urshe.URIAGE_ZEI_KBN_CD = torihikiSeikyuInfo.ZEI_KBN_CD;
                    urshe.URIAGE_TORIHIKI_KBN_CD = torihikiSeikyuInfo.TORIHIKI_KBN_CD;
                }

                var shiharai = new M_TORIHIKISAKI_SHIHARAI() { TORIHIKISAKI_CD = skEntry.TORIHIKISAKI_CD };
                torihikiShiharaiInfo = MasterUtility.GetTorihikisakiShiharai(shiharai);
                if (torihikiShiharaiInfo != null)
                {
                    urshe.SHIHARAI_ZEI_KEISAN_KBN_CD = torihikiShiharaiInfo.ZEI_KEISAN_KBN_CD;
                    urshe.SHIHARAI_ZEI_KBN_CD = torihikiShiharaiInfo.ZEI_KBN_CD;
                    urshe.SHIHARAI_TORIHIKI_KBN_CD = torihikiShiharaiInfo.TORIHIKI_KBN_CD;
                }
            }

            // 日連番取得
            urshe.DATE_NUMBER = SaibanUtil.CreateNumberDay(date, denshuKbn, skEntry.KYOTEN_CD);

            // 年連番取得
            SqlInt32 numberedYear = CorpInfoUtility.GetCurrentYear(date, (short)MasterUtility.GetCorpInfo().KISHU_MONTH);
            urshe.YEAR_NUMBER = SaibanUtil.CreateNumberYear(numberedYear, denshuKbn, skEntry.KYOTEN_CD);
            #endregion

            // DETAIL
            #region T_UR_SH_DETAIL
            List<T_UR_SH_DETAIL> tempUrShDetail = new List<T_UR_SH_DETAIL>();
            short rowNo = 0;
            foreach (var unloadingDetail in deliveryDetailAPi.Unloading_Detail.OrderBy(n => n.Unloading_Detail_No))
            {
                // 対象品名の伝種区分CDチェック
                M_HINMEI hinmei = null;
                string hinmeiCd;
                if (!ConvertRHinmeiCd(unloadingDetail.Goods_Id, out hinmeiCd))
                {
                    return result;
                }
                if (!string.IsNullOrEmpty(hinmeiCd))
                {
                    hinmei = this.Hinmei.Where(n => n.HINMEI_CD.Equals(hinmeiCd)).FirstOrDefault();
                    if (hinmei != null && (hinmei.DENSHU_KBN_CD == CommonConst.DENSHU_KBN_UKEIRE || hinmei.DENSHU_KBN_CD == CommonConst.DENSHU_KBN_SHUKKA))
                    {
                        // 伝種区分が「受入」or「出荷」の場合は処理対象外
                        continue;
                    }
                }

                T_UR_SH_DETAIL urshd = new T_UR_SH_DETAIL();
                rowNo++;

                urshd.SYSTEM_ID = urshe.SYSTEM_ID;
                urshd.SEQ = 1;
                urshd.DETAIL_SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                urshd.UR_SH_NUMBER = urshe.UR_SH_NUMBER;
                //urshd.ROW_NO = (SqlInt16)unloadingDetail.Unloading_Detail_No;
                urshd.ROW_NO = rowNo;
                urshd.KAKUTEI_KBN = this.SysInfo.UR_SH_KAKUTEI_FLAG;
                urshd.URIAGESHIHARAI_DATE = date;

                SqlInt16 unitCd;
                if (!ConvertRUnitCd(unloadingDetail.Goods_Unit_Id, out unitCd))
                {
                    return result;
                }

                T_UKETSUKE_SK_DETAIL skDetail;
                {
                    // 積卸連番、品名ID、品名単位IDに合致する受付明細（明細No、品名ID、品名単位）を取得
                    skDetail = skDetailList.Where(n => n.ROW_NO.Value == unloadingDetail.Unloading_Detail_No
                                                    && n.HINMEI_CD.Equals(hinmeiCd)
                                                    && n.UNIT_CD.Value == unitCd.Value)
                                           .FirstOrDefault();

                    // 品名ID、品名単位IDに合致する受付明細（明細No、品名ID、品名単位）を取得
                    if (skDetail == null)
                    {
                        var list = skDetailList.Where(n => n.HINMEI_CD.Equals(hinmeiCd) && n.UNIT_CD.Value == unitCd.Value)
                                               .OrderBy(n => n.ROW_NO.Value)
                                               .ToList();
                        if (list.Count == 0)
                        {
                            // ０件（該当明細ナシ）（例：ドライバーが品名を車載機で追加）
                            // 新規明細行として、売上伝票明細を登録
                            skDetail = null;
                        }
                        else if (list.Count == 1)
                        {
                            // 1件該当した場合 （例：ドライバーが他の品名を削除したことにより積卸Noと明細Noがずれた）
                            // 該当した明細Noで、売上伝票明細行を登録
                            skDetail = list.First();
                        }
                        else
                        {
                            // 複数件該当した場合　（例：可燃という品名を単価違いで2行登録している）
                            // 明細Noの小さい行の単価情報で、売上伝票明細行を登録

                            // 既に作成済みの売上／支払明細のリストを元に同品名・単位の登録件数を調べ、それをインデックスとして取得する
                            int index = this.RegistTorikomiDTO.UR_SH_DETAIL_LIST.Count(n => n.SYSTEM_ID.Value == urshd.SYSTEM_ID.Value
                                                                                         && n.HINMEI_CD.Equals(hinmeiCd)
                                                                                         && n.UNIT_CD.Value == unitCd.Value);
                            if (index < list.Count)
                            {
                                skDetail = list[index];
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(hinmeiCd))
                {
                    urshd.HINMEI_CD = hinmeiCd;
                    if (hinmei != null)
                    {
                        // 品名
                        var kobetsuHinmei = new M_KOBETSU_HINMEI() { GYOUSHA_CD = urshe.GYOUSHA_CD, GENBA_CD = urshe.GENBA_CD, HINMEI_CD = hinmeiCd };
                        var hinmeiName = this.kobetsuHinmeiDao.GetDataByCd(kobetsuHinmei);
                        if (hinmeiName != null)
                        {
                            urshd.HINMEI_NAME = hinmeiName.SEIKYUU_HINMEI_NAME;
                        }
                        else
                        {
                            var kobetsuHinmei2 = new M_KOBETSU_HINMEI() { GYOUSHA_CD = urshe.GYOUSHA_CD, GENBA_CD = string.Empty, HINMEI_CD = hinmeiCd };
                            var hinmeiName2 = this.kobetsuHinmeiDao.GetDataByCd(kobetsuHinmei2);
                            if (hinmeiName2 != null)
                            {
                                urshd.HINMEI_NAME = hinmeiName2.SEIKYUU_HINMEI_NAME;
                            }
                            else
                            {
                                urshd.HINMEI_NAME = hinmei.HINMEI_NAME;
                            }
                        }

                        // 税区分CD
                        if (!hinmei.ZEI_KBN_CD.IsNull)
                        {
                            urshd.HINMEI_ZEI_KBN_CD = hinmei.ZEI_KBN_CD;
                        }

                        // 伝票区分CD
                        if (skDetail != null && skDetail.HINMEI_CD.Equals(urshd.HINMEI_CD))
                        {
                            urshd.DENPYOU_KBN_CD = skDetail.DENPYOU_KBN_CD;
                        }
                        else
                        {
                            // 伝票区分：支払以外は全て売上として扱う
                            if (CommonConst.DENPYOU_KBN_SHIHARAI == hinmei.DENPYOU_KBN_CD)
                            {
                                // 伝票区分：支払
                                urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_SHIHARAI;
                            }
                            else
                            {
                                // 伝票区分：売上
                                urshd.DENPYOU_KBN_CD = CommonConst.DENPYOU_KBN_URIAGE;
                            }
                        }
                    }
                }

                // 数量
                urshd.SUURYOU = ConvertRSuuryou(unloadingDetail.Goods_Count);
                // 単位CD
                urshd.UNIT_CD = unitCd;
                // 単価
                SqlDecimal kingaku = SqlDecimal.Null;
                // 受付番号に紐付く出荷受付伝票が存在する場合
                if (skEntry != null)
                {
                    if (!string.IsNullOrEmpty(urshd.HINMEI_CD) &&
                        !urshd.UNIT_CD.IsNull &&
                        !urshd.DENPYOU_KBN_CD.IsNull)
                    {
                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD, skEntry, skDetail, urshd, out kingaku);
                    }
                }
                else
                {
                    // 伝票登録情報をキーに単価を取得
                    if (!string.IsNullOrEmpty(urshd.HINMEI_CD) &&
                        !urshd.UNIT_CD.IsNull &&
                        !urshd.DENPYOU_KBN_CD.IsNull)
                    {
                        urshd.TANKA = this.GetTanka((Int16)urshd.DENPYOU_KBN_CD,
                                                                         urshe,
                                                                         urshd.HINMEI_CD,
                                                                         (Int16)urshd.UNIT_CD);
                    }
                }

                if (skDetail != null && !string.IsNullOrEmpty(skDetail.MEISAI_BIKOU))
                {
                    urshd.MEISAI_BIKOU = skDetail.MEISAI_BIKOU;
                }
                else
                {
                    urshd.MEISAI_BIKOU = string.Empty;
                }

                // NISUGATA_SUURYOU,NISUGATA_UNIT_CDは設定元の項目がないため除外

                // 伝票区分によった消費税端数CDの格納
                Int16 taxHasuCD = 0;
                Int16 kinHasuCD = 0;
                if (false == urshd.DENPYOU_KBN_CD.IsNull)
                {
                    if (CommonConst.DENPYOU_KBN_SHIHARAI == urshd.DENPYOU_KBN_CD)
                    {
                        if (torihikiShiharaiInfo != null)
                        {
                            // 取引先支払情報より端数CDの取得
                            taxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
                            kinHasuCD = (Int16)torihikiShiharaiInfo.KINGAKU_HASUU_CD;
                        }
                    }
                    else
                    {
                        if (torihikiSeikyuInfo != null)
                        {
                            // 取引先請求情報より端数CDの取得
                            taxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
                            kinHasuCD = (Int16)torihikiSeikyuInfo.KINGAKU_HASUU_CD;
                        }
                    }
                }

                // 明細金額再計算
                this.DetailCalc(urshe, urshd, taxHasuCD, kinHasuCD, kingaku);

                // 売上_支払明細テーブル登録
                this.RegistTorikomiDTO.UR_SH_DETAIL_LIST.Add(urshd);
                tempUrShDetail.Add(urshd);
            }
            #endregion

            // 総計取得
            this.GetMoneyTotal(urshe, tempUrShDetail);

            // 端数CDの取得
            Int16 shiTaxHasuCD = 0;
            Int16 uriTaxHasuCD = 0;
            if (torihikiShiharaiInfo != null)
            {
                // 取引先支払情報より端数CDの取得
                shiTaxHasuCD = (Int16)torihikiShiharaiInfo.TAX_HASUU_CD;
            }
            if (torihikiSeikyuInfo != null)
            {
                // 取引先請求情報より端数CDの取得
                uriTaxHasuCD = (Int16)torihikiSeikyuInfo.TAX_HASUU_CD;
            }

            // 伝票消費税計算
            this.EntryTaxCalc(urshe, shiTaxHasuCD, "支払");
            this.EntryTaxCalc(urshe, uriTaxHasuCD, "売上");

            // 売上支払伝票登録
            var bindLogic = new DataBinderLogic<T_UR_SH_ENTRY>(urshe);
            bindLogic.SetSystemProperty(urshe, false);
            urshe.DELETE_FLG = false;

            this.RegistTorikomiDTO.UR_SH_ENTRY_LIST.Add(urshe);

            urshSystemId = urshe.SYSTEM_ID.Value;
            result = true;

            return result;
        }

        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="entry">収集受付伝票</param>
        /// <param name="detail">収集受付明細</param>
        /// <param name="urshDetail">売上支払明細</param>
        /// <param name="kingaku">単価</param>
        /// <remarks>検索項目に該当する単価を取得する。受付明細⇒個別品名単価⇒基本品名単価の順で取得する</remarks>
        private SqlDecimal GetTanka(Int16 kbnCD, T_UKETSUKE_SS_ENTRY entry, T_UKETSUKE_SS_DETAIL detail, T_UR_SH_DETAIL urshDetail, out SqlDecimal kingaku)
        {
            SqlDecimal tanka = 0;
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            kingaku = SqlDecimal.Null;

            if (detail != null)
            {
                // 受付明細から取得
                if (!detail.TANKA.IsNull)
                {
                    tanka = detail.TANKA;
                }
                else
                {
                    tanka = SqlDecimal.Null;
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull)
                    {
                        if (!detail.HINMEI_KINGAKU.IsNull)
                        {
                            kingaku = detail.HINMEI_KINGAKU;
                        }
                    }
                    else
                    {
                        if (!detail.KINGAKU.IsNull)
                        {
                            kingaku = detail.KINGAKU;
                        }
                    }
                }
            }
            else
            {
                // 収集受付伝票からマスタの各単価取得
                if (entry != null)
                {
                    // 個別品名単価から取得
                    var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                        kbnCD,
                                                                        entry.TORIHIKISAKI_CD,
                                                                        entry.GYOUSHA_CD,
                                                                        entry.GENBA_CD,
                                                                        entry.UNPAN_GYOUSHA_CD,
                                                                        entry.NIOROSHI_GYOUSHA_CD,
                                                                        entry.NIOROSHI_GENBA_CD,
                                                                        urshDetail.HINMEI_CD,
                                                                        urshDetail.UNIT_CD.Value,
                                                                        entry.SAGYOU_DATE);
                    if (kobetsuEntity != null)
                    {
                        // 単価をセット
                        if (!kobetsuEntity.TANKA.IsNull)
                        {
                            tanka = kobetsuEntity.TANKA.Value;
                        }
                    }
                    else
                    {
                        // 基本品名単価から取得
                        var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                        kbnCD,
                                                                        entry.UNPAN_GYOUSHA_CD,
                                                                        entry.NIOROSHI_GYOUSHA_CD,
                                                                        entry.NIOROSHI_GENBA_CD,
                                                                        urshDetail.HINMEI_CD,
                                                                        urshDetail.UNIT_CD.Value,
                                                                        entry.SAGYOU_DATE);
                        if (kihonEntity != null)
                        {
                            // 単価をセット
                            if (!kihonEntity.TANKA.IsNull)
                            {
                                tanka = kihonEntity.TANKA.Value;
                            }
                        }
                        else
                        {
                            // 登録情報がない場合は0
                            tanka = 0;
                        }
                    }
                }
                else
                {
                    // 登録情報がない場合は0
                    tanka = 0;
                }
            }

            return tanka;
        }

        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="entry">出荷受付伝票</param>
        /// <param name="detail">出荷受付明細</param>
        /// <param name="urshDetail">売上支払明細</param>
        /// <param name="kingaku">単価</param>
        /// <remarks>検索項目に該当する単価を取得する。受付明細⇒個別品名単価⇒基本品名単価の順で取得する</remarks>
        private SqlDecimal GetTanka(Int16 kbnCD, T_UKETSUKE_SK_ENTRY entry, T_UKETSUKE_SK_DETAIL detail, T_UR_SH_DETAIL urshDetail, out SqlDecimal kingaku)
        {
            SqlDecimal tanka = 0;
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            kingaku = SqlDecimal.Null;

            if (detail != null)
            {
                // 受付明細から取得
                if (!detail.TANKA.IsNull)
                {
                    tanka = detail.TANKA;
                }
                else
                {
                    tanka = SqlDecimal.Null;
                    if (!detail.HINMEI_ZEI_KBN_CD.IsNull)
                    {
                        if (!detail.HINMEI_KINGAKU.IsNull)
                        {
                            kingaku = detail.HINMEI_KINGAKU;
                        }
                    }
                    else
                    {
                        if (!detail.KINGAKU.IsNull)
                        {
                            kingaku = detail.KINGAKU;
                        }
                    }
                }
            }
            else
            {
                // 出荷受付伝票からマスタの各単価取得
                if (entry != null)
                {
                    // 個別品名単価から取得
                    var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                        kbnCD,
                                                                        entry.TORIHIKISAKI_CD,
                                                                        entry.GYOUSHA_CD,
                                                                        entry.GENBA_CD,
                                                                        entry.UNPAN_GYOUSHA_CD,
                                                                        "",
                                                                        "",
                                                                        urshDetail.HINMEI_CD,
                                                                        urshDetail.UNIT_CD.Value,
                                                                        entry.SAGYOU_DATE);
                    if (kobetsuEntity != null)
                    {
                        // 単価をセット
                        if (!kobetsuEntity.TANKA.IsNull)
                        {
                            tanka = kobetsuEntity.TANKA.Value;
                        }
                    }
                    else
                    {
                        // 基本品名単価から取得
                        var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                        kbnCD,
                                                                        entry.UNPAN_GYOUSHA_CD,
                                                                        "",
                                                                        "",
                                                                        urshDetail.HINMEI_CD,
                                                                        urshDetail.UNIT_CD.Value,
                                                                        entry.SAGYOU_DATE);
                        if (kihonEntity != null)
                        {
                            // 単価をセット
                            if (!kihonEntity.TANKA.IsNull)
                            {
                                tanka = kihonEntity.TANKA.Value;
                            }
                        }
                        else
                        {
                            // 登録情報がない場合は0
                            tanka = 0;
                        }
                    }
                }
                else
                {
                    // 登録情報がない場合は0
                    tanka = 0;
                }
            }

            return tanka;
        }

        /// <summary>
        /// 単価取得処理
        /// </summary>
        /// <param name="kbnCD">伝票区分CD</param>
        /// <param name="entryEntity">Entry伝票</param>
        /// <param name="hinmeiCD">品名CD</param>
        /// <param name="unitCD">単位CD</param>
        /// <returns>単価</returns>
        /// <remarks>対象項目を直接指定し単価を取得する。個別品名単価⇒基本品名単価の順で取得する</remarks>
        private SqlDecimal GetTanka(Int16 kbnCD, T_UR_SH_ENTRY entryEntity, string hinmeiCD, Int16 unitCD)
        {
            var CommonDBAccessor = new Common.BusinessCommon.DBAccessor();
            SqlDecimal tanka = 0;

            // 個別品名単価から取得
            var kobetsuEntity = CommonDBAccessor.GetKobetsuhinmeiTanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                kbnCD,
                                                                entryEntity.TORIHIKISAKI_CD,
                                                                entryEntity.GYOUSHA_CD,
                                                                entryEntity.GENBA_CD,
                                                                entryEntity.UNPAN_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GENBA_CD,
                                                                hinmeiCD,
                                                                unitCD,
                                                                Convert.ToString(entryEntity.DENPYOU_DATE.Value));
            if (kobetsuEntity != null)
            {
                // 単価をセット
                if (false == string.IsNullOrEmpty(kobetsuEntity.TANKA.ToString()))
                {
                    tanka = decimal.Parse(kobetsuEntity.TANKA.ToString());
                }
            }
            else
            {
                // 基本品名単価から取得
                var kihonEntity = CommonDBAccessor.GetKihonHinmeitanka((short)DENSHU_KBN.URIAGE_SHIHARAI,
                                                                kbnCD,
                                                                entryEntity.UNPAN_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GYOUSHA_CD,
                                                                entryEntity.NIOROSHI_GENBA_CD,
                                                                hinmeiCD,
                                                                unitCD,
                                                                Convert.ToString(entryEntity.DENPYOU_DATE.Value));
                if (kihonEntity != null)
                {
                    // 単価をセット
                    if (false == string.IsNullOrEmpty(kihonEntity.TANKA.ToString()))
                    {
                        tanka = decimal.Parse(kihonEntity.TANKA.ToString());
                    }
                }
                else
                {
                    // 登録情報がない場合は0
                    tanka = 0;
                }
            }

            return tanka;
        }

        /// <summary>
        /// 明細金額再計算
        /// </summary>
        /// <param name="entryEntity">Entry伝票</param>
        /// <param name="detailEntity">Detail伝票</param>
        /// <param name="taxHasuCD">消費税端数CD</param>
        /// <param name="kinHasuCD">金額端数CD</param>
        /// <remarks>
        /// 明細金額の再計算を行い
        /// 明細金額、消費税を格納する
        /// </remarks>
        private void DetailCalc(T_UR_SH_ENTRY entryEntity, T_UR_SH_DETAIL detailEntity, Int16 taxHasuCD, Int16 kinHasuCD, SqlDecimal detailKingaku)
        {
            // 一旦初期化
            detailEntity.HINMEI_KINGAKU = 0;
            detailEntity.HINMEI_TAX_SOTO = 0;
            detailEntity.HINMEI_TAX_UCHI = 0;
            detailEntity.KINGAKU = 0;
            detailEntity.TAX_SOTO = 0;
            detailEntity.TAX_UCHI = 0;

            // 明細金額計算
            decimal kingaku = 0;
            if ((false == detailEntity.SUURYOU.IsNull) && (false == detailEntity.TANKA.IsNull))
            {
                // 数量x単価
                decimal tempSuuryou = 0;
                decimal.TryParse(detailEntity.SUURYOU.ToString(), out tempSuuryou);
                kingaku = tempSuuryou * (decimal)detailEntity.TANKA;
            }
            else
            {
                // 数量もしくは単価が未格納の場合は0
                kingaku = 0;
            }

            if (true == detailEntity.TANKA.IsNull)
            {
                if (false == detailKingaku.IsNull)
                {
                    kingaku = decimal.Parse(detailKingaku.ToString());
                }
                else
                {
                    kingaku = 0;
                    detailKingaku = 0;
                }
            }

            // 税区分取得
            Int16 zeiKbn = 0;
            if (false == detailEntity.HINMEI_ZEI_KBN_CD.IsNull)
            {
                // 品名税区分が登録されている場合
                zeiKbn = (Int16)detailEntity.HINMEI_ZEI_KBN_CD;
            }
            else
            {
                // 伝票区分によった税区分の格納
                if (false == detailEntity.DENPYOU_KBN_CD.IsNull)
                {
                    if (CommonConst.DENPYOU_KBN_SHIHARAI == detailEntity.DENPYOU_KBN_CD)
                    {
                        // 支払税区分
                        if (false == entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
                        {
                            zeiKbn = (Int16)entryEntity.SHIHARAI_ZEI_KBN_CD;
                        }
                    }
                    else
                    {
                        // 売上税区分
                        if (false == entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
                        {
                            zeiKbn = (Int16)entryEntity.URIAGE_ZEI_KBN_CD;
                        }
                    }
                }
            }

            // 一旦初期化
            entryEntity.URIAGE_SHOUHIZEI_RATE = 0;
            entryEntity.SHIHARAI_SHOUHIZEI_RATE = 0;

            // 消費税計算
            decimal sotoZei = 0;
            decimal uchiZei = 0;
            if (zeiKbn != 0)
            {
                if (accessor == null)
                {
                    accessor = new Allocation.MobileShougunTorikomi.Accessor.DBAccessor();
                }

                // 消費税率の取得
                var rate = this.accessor.GetShouhizeiRate((DateTime)entryEntity.DENPYOU_DATE);
                entryEntity.URIAGE_SHOUHIZEI_RATE = rate;
                entryEntity.SHIHARAI_SHOUHIZEI_RATE = rate;

                // 税区分によった消費税の格納
                var zei = this.TaxCalc(zeiKbn, rate, kingaku);
                if (CommonConst.ZEI_KBN_SOTO == zeiKbn)
                {
                    // 外税
                    sotoZei = zei;
                }
                else if (CommonConst.ZEI_KBN_UCHI == zeiKbn)
                {
                    // 内税
                    uchiZei = zei;
                }
                else
                {
                    // それ以外(非課税等)は0
                    sotoZei = 0;
                    uchiZei = 0;
                }
            }

            // 消費税端数処理
            if (taxHasuCD != 0)
            {
                sotoZei = CommonCalc.FractionCalc(sotoZei, taxHasuCD);
                uchiZei = CommonCalc.FractionCalc(uchiZei, taxHasuCD);
            }

            // 金額端数処理
            if (kinHasuCD != 0)
            {
                kingaku = CommonCalc.FractionCalc(kingaku, kinHasuCD);
            }

            // 金額、消費税のセット
            if (false == detailEntity.HINMEI_ZEI_KBN_CD.IsNull)
            {
                // 品名税区分CDが登録されていた場合
                if (false == detailEntity.TANKA.IsNull)
                {
                    detailEntity.HINMEI_KINGAKU = kingaku;
                }
                else
                {
                    detailEntity.HINMEI_KINGAKU = detailKingaku;
                }
                detailEntity.HINMEI_TAX_SOTO = sotoZei;
                detailEntity.HINMEI_TAX_UCHI = uchiZei;
            }
            else
            {
                // 品名税区分CDが登録されていなかった場合
                if (false == detailEntity.TANKA.IsNull)
                {
                    detailEntity.KINGAKU = kingaku;
                }
                else
                {
                    detailEntity.KINGAKU = detailKingaku;
                }
                detailEntity.TAX_SOTO = sotoZei;
                detailEntity.TAX_UCHI = uchiZei;
            }
        }

        /// <summary>
        /// 消費税計算
        /// </summary>
        /// <param name="zeiKbn">税区分</param>
        /// <param name="rate">消費税率</param>
        /// <param name="kingaku">算出対象金額</param>
        /// <remarks>
        /// 税区分に従い、消費税の計算を行う
        /// </remarks>
        private decimal TaxCalc(Int16 zeiKbn, decimal rate, decimal kingaku)
        {
            decimal zei = 0;

            if (zeiKbn != 0)
            {
                // 税区分によった消費税の格納
                if (CommonConst.ZEI_KBN_SOTO == zeiKbn)
                {
                    // 外税
                    zei = kingaku * rate;
                }
                else if (CommonConst.ZEI_KBN_UCHI == zeiKbn)
                {
                    // 内税
                    zei = kingaku - (kingaku / (rate + 1));
                }
                else
                {
                    // それ以外(非課税等)は0
                    zei = 0;
                }
            }

            return zei;
        }

        /// <summary>
        /// 金額・消費税総計取得
        /// </summary>
        /// <param name="detailList">算出対象明細のリスト</param>
        /// <param name="entryEntity">格納先のEntry伝票</param>
        /// <remarks>
        /// 明細のListより、金額・消費税の総計を算出し、
        /// Entry伝票に格納する
        /// </remarks>
        private void GetMoneyTotal(T_UR_SH_ENTRY entryEntity, List<T_UR_SH_DETAIL> detailList)
        {
            // 一旦初期化
            entryEntity.URIAGE_AMOUNT_TOTAL = 0;
            entryEntity.URIAGE_TAX_SOTO_TOTAL = 0;
            entryEntity.URIAGE_TAX_UCHI_TOTAL = 0;
            entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = 0;
            entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = 0;
            entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = 0;
            entryEntity.SHIHARAI_AMOUNT_TOTAL = 0;
            entryEntity.SHIHARAI_TAX_SOTO_TOTAL = 0;
            entryEntity.SHIHARAI_TAX_UCHI_TOTAL = 0;
            entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = 0;
            entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = 0;
            entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = 0;

            foreach (T_UR_SH_DETAIL detail in detailList)
            {
                if (CommonConst.DENPYOU_KBN_SHIHARAI == detail.DENPYOU_KBN_CD)
                {
                    // 支払金額を積算
                    entryEntity.SHIHARAI_AMOUNT_TOTAL += detail.KINGAKU;
                    entryEntity.SHIHARAI_TAX_SOTO_TOTAL += detail.TAX_SOTO;
                    entryEntity.SHIHARAI_TAX_UCHI_TOTAL += detail.TAX_UCHI;
                    entryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL += detail.HINMEI_KINGAKU;
                    entryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL += detail.HINMEI_TAX_SOTO;
                    entryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL += detail.HINMEI_TAX_UCHI;
                }
                else
                {
                    // 売上金額を積算
                    entryEntity.URIAGE_AMOUNT_TOTAL += detail.KINGAKU;
                    entryEntity.URIAGE_TAX_SOTO_TOTAL += detail.TAX_SOTO;
                    entryEntity.URIAGE_TAX_UCHI_TOTAL += detail.TAX_UCHI;
                    entryEntity.HINMEI_URIAGE_KINGAKU_TOTAL += detail.HINMEI_KINGAKU;
                    entryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL += detail.HINMEI_TAX_SOTO;
                    entryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL += detail.HINMEI_TAX_UCHI;
                }
            }
        }

        /// <summary>
        /// Entry伝票消費税計算
        /// </summary>
        /// <param name="entryEntity">Entry伝票</param>
        /// <param name="taxHasuCD">消費税端数CD</param>
        /// <param name="strProcType">処理種別("売上"or"支払")</param>
        /// <remarks>
        /// Entry伝票の消費税計算を行う
        /// </remarks>
        private void EntryTaxCalc(T_UR_SH_ENTRY entryEntity, Int16 taxHasuCD, string strProcType)
        {
            Int16 zeiKbn = 0;
            decimal rate = 0;
            decimal kingaku = 0;

            if (true == strProcType.Equals("支払"))
            {
                // 一旦初期化
                entryEntity.SHIHARAI_TAX_SOTO = 0;
                entryEntity.SHIHARAI_TAX_UCHI = 0;

                // 支払消費税計算
                if (false == entryEntity.SHIHARAI_ZEI_KBN_CD.IsNull)
                {
                    zeiKbn = (Int16)entryEntity.SHIHARAI_ZEI_KBN_CD;
                }
                rate = (decimal)entryEntity.SHIHARAI_SHOUHIZEI_RATE;
                kingaku = (decimal)entryEntity.SHIHARAI_AMOUNT_TOTAL;
            }
            else
            {
                // 一旦初期化
                entryEntity.URIAGE_TAX_SOTO = 0;
                entryEntity.URIAGE_TAX_UCHI = 0;

                // 売上消費税計算
                if (false == entryEntity.URIAGE_ZEI_KBN_CD.IsNull)
                {
                    zeiKbn = (Int16)entryEntity.URIAGE_ZEI_KBN_CD;
                }
                rate = (decimal)entryEntity.URIAGE_SHOUHIZEI_RATE;
                kingaku = (decimal)entryEntity.URIAGE_AMOUNT_TOTAL;
            }

            // 消費税計算
            decimal sotoZei = 0;
            decimal uchiZei = 0;
            var zei = this.TaxCalc(zeiKbn, rate, kingaku);
            if (zeiKbn != 0)
            {
                // 税区分によった消費税の格納
                if (CommonConst.ZEI_KBN_SOTO == zeiKbn)
                {
                    // 外税
                    sotoZei = zei;
                }
                else if (CommonConst.ZEI_KBN_UCHI == zeiKbn)
                {
                    // 内税
                    uchiZei = zei;
                }
                else
                {
                    // それ以外(非課税等)は0
                    sotoZei = 0;
                    uchiZei = 0;
                }
            }

            // 消費税端数処理
            if (taxHasuCD != 0)
            {
                sotoZei = CommonCalc.FractionCalc(sotoZei, taxHasuCD);
                uchiZei = CommonCalc.FractionCalc(uchiZei, taxHasuCD);
            }

            // 消費税のセット
            if (true == strProcType.Equals("支払"))
            {
                // 支払
                entryEntity.SHIHARAI_TAX_SOTO = sotoZei;
                entryEntity.SHIHARAI_TAX_UCHI = uchiZei;
            }
            else
            {
                // 売上
                entryEntity.URIAGE_TAX_SOTO = sotoZei;
                entryEntity.URIAGE_TAX_UCHI = uchiZei;
            }
        }

        /// <summary>
        /// 収集受付伝票の配車状況を更新
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <param name="haishaJokyoCd">配車状況CD</param>
        /// <param name="haishaJokyoName">配車状況名</param>
        private void CreateSs(long uketsukeNumber, SqlInt16 haishaJokyoCd, string haishaJokyoName)
        {
            // T_UKETSUKE_SS_ENTRY
            T_UKETSUKE_SS_ENTRY ssEntry = new T_UKETSUKE_SS_ENTRY();
            ssEntry.UKETSUKE_NUMBER = uketsukeNumber;
            ssEntry.DELETE_FLG = true;
            var delSSentry = this.uketsukeSsEntryDao.GetDataForEntity(ssEntry);
            if (delSSentry != null)
            {
                delSSentry.DELETE_FLG = false;
                this.RegistTorikomiDTO.DEL_UKETSUKE_SS_ENTRY_LIST.Add(delSSentry);
            }

            var intSSentry = this.uketsukeSsEntryDao.GetDataForEntity(ssEntry);
            if (intSSentry != null)
            {
                intSSentry.SEQ = intSSentry.SEQ + 1;
                intSSentry.HAISHA_JOKYO_CD = haishaJokyoCd;
                intSSentry.HAISHA_JOKYO_NAME = haishaJokyoName;
                string CREATE_USER = intSSentry.CREATE_USER;
                SqlDateTime CREATE_DATE = intSSentry.CREATE_DATE;
                string CREATE_PC = intSSentry.CREATE_PC;
                var dataBinderContenaResultSSentry = new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(intSSentry);
                dataBinderContenaResultSSentry.SetSystemProperty(intSSentry, false);
                intSSentry.CREATE_USER = CREATE_USER;
                intSSentry.CREATE_DATE = CREATE_DATE;
                intSSentry.CREATE_PC = CREATE_PC;

                intSSentry.CONTENA_SOUSA_CD = SqlInt16.Null;
                this.RegistTorikomiDTO.INS_UKETSUKE_SS_ENTRY_LIST.Add(intSSentry);
            }

            // T_UKETSUKE_SS_DETAIL
            T_UKETSUKE_SS_DETAIL ssDetail = new T_UKETSUKE_SS_DETAIL();
            ssDetail.SYSTEM_ID = delSSentry.SYSTEM_ID;
            ssDetail.SEQ = delSSentry.SEQ;
            var intSSDetail = this.uketsukeSsDetailDao.GetDataForEntity(ssDetail);
            if (intSSDetail.Length > 0)
            {
                foreach (T_UKETSUKE_SS_DETAIL detail in intSSDetail)
                {
                    detail.SEQ = detail.SEQ + 1;
                    var dataBinderContenaResultSSdetail = new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(detail);
                    dataBinderContenaResultSSdetail.SetSystemProperty(detail, false);
                    this.RegistTorikomiDTO.INS_UKETSUKE_SS_DETAIL_LIST.Add(detail);
                }
            }
        }

        /// <summary>
        /// 出荷受付伝票の配車状況を更新
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <param name="haishaJokyoCd">配車状況CD</param>
        /// <param name="haishaJokyoName">配車状況名</param>
        private void CreateSk(long uketsukeNumber, SqlInt16 haishaJokyoCd, string haishaJokyoName)
        {
            // T_UKETSUKE_SK_ENTRY
            T_UKETSUKE_SK_ENTRY skEntry = new T_UKETSUKE_SK_ENTRY();
            skEntry.UKETSUKE_NUMBER = uketsukeNumber;
            skEntry.DELETE_FLG = true;
            var delSKentry = this.uketsukeSkEntryDao.GetDataForEntity(skEntry);
            if (delSKentry != null)
            {
                delSKentry.DELETE_FLG = false;
                this.RegistTorikomiDTO.DEL_UKETSUKE_SK_ENTRY_LIST.Add(delSKentry);
            }

            var intSKentry = this.uketsukeSkEntryDao.GetDataForEntity(skEntry);
            if (intSKentry != null)
            {
                intSKentry.SEQ = intSKentry.SEQ + 1;
                intSKentry.HAISHA_JOKYO_CD = haishaJokyoCd;
                intSKentry.HAISHA_JOKYO_NAME = haishaJokyoName;
                string CREATE_USER = intSKentry.CREATE_USER;
                SqlDateTime CREATE_DATE = intSKentry.CREATE_DATE;
                string CREATE_PC = intSKentry.CREATE_PC;
                var dataBinderContenaResultSKentry = new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(intSKentry);
                dataBinderContenaResultSKentry.SetSystemProperty(intSKentry, false);
                intSKentry.CREATE_USER = CREATE_USER;
                intSKentry.CREATE_DATE = CREATE_DATE;
                intSKentry.CREATE_PC = CREATE_PC;

                this.RegistTorikomiDTO.INS_UKETSUKE_SK_ENTRY_LIST.Add(intSKentry);
            }

            // T_UKETSUKE_SK_DETAIL
            T_UKETSUKE_SK_DETAIL skDetail = new T_UKETSUKE_SK_DETAIL();
            skDetail.SYSTEM_ID = delSKentry.SYSTEM_ID;
            skDetail.SEQ = delSKentry.SEQ;
            var intSKDetail = this.uketsukeSkDetailDao.GetDataForEntity(skDetail);
            if (intSKDetail.Length > 0)
            {
                foreach (T_UKETSUKE_SK_DETAIL detail in intSKDetail)
                {
                    detail.SEQ = detail.SEQ + 1;
                    var dataBinderContenaResultSKdetail = new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(detail);
                    dataBinderContenaResultSKdetail.SetSystemProperty(detail, false);
                    this.RegistTorikomiDTO.INS_UKETSUKE_SK_DETAIL_LIST.Add(detail);
                }
            }
        }

        /// <summary>
        /// 配送計画to売上支払データ作成
        /// </summary>
        /// <param name="urshSystemId">売上支払システムID</param>
        /// <param name="systemId">システムID</param>
        private void CreateLogiToUrsh(long urshSystemId, long systemId)
        {
            var entity = new T_LOGI_TO_URSH();
            entity.URSH_SYSTEM_ID = urshSystemId;
            entity.SYSTEM_ID = systemId;
            entity.DELETE_FLG = false;

            var dataBinderLinkEntry = new DataBinderLogic<T_LOGI_TO_URSH>(entity);
            dataBinderLinkEntry.SetSystemProperty(entity, false);

            // Listに追加
            this.RegistTorikomiDTO.LOGI_TO_URSH_LIST.Add(entity);
        }

        /// <summary>
        /// コンテナデータ作成
        /// </summary>
        /// <param name="systemId">売上支払システムID</param>
        /// <param name="seq">システムID</param>
        private void CreateContenaResult(SqlInt64 systemId, SqlInt32 seq, long urSystemId)
        {
            if (systemId == 0 ||
                seq == 0 ||
                urSystemId == 0)
            {
                return;
            }

            var contenaReserveEntity = this.contenaReserveDao.GetContenaReserve(systemId.ToString(), seq.ToString());
            if (contenaReserveEntity.Count() <= 0)
            {
                return;
            }

            this.RegistTorikomiDTO.CONTENA_RESERVE_LIST.Clear();
            foreach (T_CONTENA_RESERVE entity in contenaReserveEntity)
            {
                this.RegistTorikomiDTO.CONTENA_RESERVE_LIST.Add(entity);
            }

            List<T_CONTENA_RESULT> tmpList = new List<T_CONTENA_RESULT>();
            foreach (T_CONTENA_RESERVE entity in this.RegistTorikomiDTO.CONTENA_RESERVE_LIST)
            {
                if (entity != null)
                {
                    //コンテナ稼動予定をコンテナ稼動実績に移行
                    T_CONTENA_RESULT resultEntity = new T_CONTENA_RESULT();
                    resultEntity.DENSHU_KBN_CD = 3;
                    resultEntity.SYSTEM_ID = urSystemId;
                    resultEntity.SEQ = 1;
                    resultEntity.CONTENA_SET_KBN = entity.CONTENA_SET_KBN;
                    resultEntity.CONTENA_SHURUI_CD = entity.CONTENA_SHURUI_CD;
                    resultEntity.CONTENA_CD = entity.CONTENA_CD;
                    resultEntity.DAISUU_CNT = (SqlInt16)entity.DAISUU_CNT;
                    // 自動設定
                    var dataBinderContenaResult = new DataBinderLogic<T_CONTENA_RESULT>(resultEntity);
                    dataBinderContenaResult.SetSystemProperty(resultEntity, false);

                    this.RegistTorikomiDTO.CONTENA_RESULT_LIST.Add(resultEntity);
                }
            }
            
        }
        #endregion

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
    }
}
