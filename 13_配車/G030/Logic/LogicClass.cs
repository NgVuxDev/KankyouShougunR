// $Id: LogicClass.cs 55042 2015-07-08 06:56:26Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.NioroshiNoSettei;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Seasar.Dao;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        public UIHeader headerForm;

        /// <summary>
        /// 定期配車入力情報のDao
        /// </summary>
        private IT_TEIKI_HAISHA_ENTRYDao teikiHaishaEntryDao;
        /// <summary>
        /// 定期配車明細情報のDao
        /// </summary>
        private IT_TEIKI_HAISHA_DETAILDao teikiHaishaDetailDao;
        /// <summary>
        /// 定期配車荷降のDao
        /// </summary>
        private IT_TEIKI_HAISHA_NIOROSHIDao teikiHaishaNioroshiDao;
        /// <summary>
        /// 定期配車詳細のDao
        /// </summary>
        private IT_TEIKI_HAISHA_SHOUSAIDao teikiHaishaShousaiDao;
        /// <summary>
        /// コース明細Dao
        /// </summary>
        private IM_COURSE_DETAILDao courseDetailDao;
        /// <summary>
        /// コース名称Dao
        /// </summary>
        private IM_COURSE_NAMEDao courseNameDao;
        // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 start
        /// <summary>
        /// コースENTRYDao
        /// </summary>
        private IM_COURSE_ENTRYDao courseEntryDao;
        // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 end
        /// <summary>
        /// 受付（収集）入力のDao
        /// </summary>
        private IT_UKETSUKE_SS_ENTRYDao uketsukeSSEntryDao;
        /// <summary>
        /// 受付（収集）明細のDao
        /// </summary>
        private IT_UKETSUKE_SS_DETAILDao uketsukeSSDetailDao;
        /// <summary>
        /// 受付（出荷）入力のDao
        /// </summary>
        private IT_UKETSUKE_SK_ENTRYDao uketsukeSKEntryDao;
        /// <summary>
        /// 受付（出荷）明細のDao
        /// </summary>
        private IT_UKETSUKE_SK_DETAILDao uketsukeSKDetailDao;
        /// <summary>
        /// コンテナ稼動予定のDao
        /// </summary>
        private IT_CONTENA_RESERVEDao contenaReserveDao;
        /// <summary>
        /// システムID採番Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;
        /// <summary>
        /// 伝種採番Dao
        /// </summary>
        private IS_NUMBER_DENSHUDao numberDenshuDao;
        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        private IM_GENBADao genbaDao;
        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;
        /// <summary>
        /// 車種マスタのDao
        /// </summary>
        private IM_SHASHUDao shashuDao;
        /// <summary>
        /// 社員マスタのDao
        /// </summary>
        private IM_SHAINDao shainDao;
        /// <summary>
        /// 拠点マスタDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;
        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        // 20141015 koukouei 休動管理機能追加 start
        /// <summary>
        /// 運転者休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workClosedUntenshaDao;
        /// <summary>
        /// 車輌休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workClosedSharyouDao;
        /// <summary>
        /// 荷降現場休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workClosedHanYuusakiDao;
        // 20141015 koukouei 休動管理機能追加 end
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// モバイル連携Dao
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// 車輌検索ポップアップをロジックで起動させたかのフラグ
        /// </summary>
        internal bool isCalledSharyouPopupFromLogic = false;

        internal MessageBoxShowLogic MsgBox;

        /// <summary>
        /// モバイル連携用の伝票番号
        /// </summary>
        internal long Renkei_TeikiHaishaNumber;

        /// <summary>
        /// モバイル連携用のDETAIL_SYSTEM_ID
        /// </summary>
        internal List<long> Renkei_DETAIL_SYSTEM_ID;

        /// <summary>
        /// モバイル連携用データテーブル
        /// </summary>
        private DataTable ResultTable;

        private int MobileTryTime;

        /// <summary>
        /// モバイル将軍業務TBLのentity
        /// </summary>
        private T_MOBISYO_RT entitysMobisyoRt { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentity
        /// </summary>
        private T_MOBISYO_RT_DTL entitysMobisyoRtDTL { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentity
        /// </summary>
        private T_MOBISYO_RT_HANNYUU entitysMobisyoRtHN { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT> entitysMobisyoRtList { get; set; }

        /// <summary>
        /// モバイル将軍業務詳細TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_DTL> entitysMobisyoRtDTLList { get; set; }

        /// <summary>
        /// モバイル将軍業務搬入TBLのentityList
        /// </summary>
        private List<T_MOBISYO_RT_HANNYUU> entitysMobisyoRtHNList { get; set; }

        /// <summary>
        /// モバイル将軍業務TBLのDao
        /// </summary>
        private IT_MOBISYO_RTDao TmobisyoRtDao;

        /// <summary>
        /// モバイル将軍業務詳細TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_DTLDao TmobisyoRtDTLDao;

        /// <summary>
        /// モバイル将軍業務搬入TBLのDao
        /// </summary>
        private IT_MOBISYO_RT_HANNYUUDao TmobisyoRtHNDao;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場のDao
        /// </summary>
        internal IM_SMS_RECEIVER_LINK_GENBADao smsReceiverLinkGenbaDao;

        internal bool is_mobile = false;

        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOClass searchDto { get; set; }

        /// <summary>
        /// 定期配車番号
        /// </summary>
        public string teikiHaishaNumber { get; set; }

        /// <summary>
        /// 作業日（参照モード用）
        /// </summary>
        public DateTime sagyoubi { get; set; }

        /// <summary>
        /// 曜日CD（参照モード用）
        /// </summary>
        public string dayCd { get; set; }

        /// <summary>
        /// コース名称CD（参照モード用）
        /// </summary>
        public string courseNameCd { get; set; }

        /// <summary>
        /// 作業日（参照モード用）
        /// </summary>
        public string furikaeKbn { get; set; }

        /// <summary>
        /// 検索結果（定期配車入力）
        /// </summary>
        public DataTable searchResultEntry { get; set; }
        /// <summary>
        /// 検索結果（定期配車明細）
        /// </summary>
        public DataTable searchResultDetail { get; set; }
        /// <summary>
        /// 検索結果（定期配車荷降）
        /// </summary>
        public DataTable searchResultNioroshi { get; set; }
        /// <summary>
        /// 検索結果（定期配車詳細）
        /// </summary>
        public DataTable searchResultShousai { get; set; }
        /// <summary>
        /// モバイル登録された荷降NoのList
        /// </summary>
        public List<string> tourokuZumiNioroshiList { get; set; }
        /// <summary>
        /// 検索結果（コース明細）
        /// </summary>
        public DataTable searchResultCourseDetail { get; set; }
        /// <summary>
        /// 検索結果（コース荷降先）
        /// </summary>
        public DataTable searchResultCourseNioroshi { get; set; }
        /// <summary>
        /// 検索結果（コース明細内訳）
        /// </summary>
        public DataTable searchResultCourseDetailItems { get; set; }
        // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 start
        /// <summary>
        /// 検索結果（コースEntry）
        /// </summary>
        public DataTable searchResultCourseEntry { get; set; }
        // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 end
        /// <summary>
        /// 定期配車詳細/コース明細内訳リスト（回収品名詳細ポップアップ用）
        /// </summary>
        public DataSet shousaiDataSet { get; set; }

        /// <summary>
        /// 定期配車入力テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_ENTRY entitysT_TEIKI_HAISHA_ENTRY { get; set; }
        /// <summary>
        /// 定期配車明細テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_DETAIL entitysT_TEIKI_HAISHA_DETAIL { get; set; }
        /// <summary>
        /// 定期配車荷降テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_NIOROSHI entitysT_TEIKI_HAISHA_NIOROSHI { get; set; }
        /// <summary>
        /// 定期配車詳細テーブルの情報
        /// </summary>
        private T_TEIKI_HAISHA_SHOUSAI entitysT_TEIKI_HAISHA_SHOUSAI { get; set; }

        /// <summary>
        /// 定期配車明細リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_DETAIL> entityDetailList { get; set; }
        /// <summary>
        /// 定期配車荷降リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_NIOROSHI> entityNioroshilList { get; set; }
        /// <summary>
        /// 定期配車詳細リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_SHOUSAI> entityShousailList { get; set; }

        /// <summary>
        /// 受付（収集）入力テーブルの情報
        /// </summary>
        private T_UKETSUKE_SS_ENTRY entitysT_UKETSUKE_SS_ENTRY { get; set; }
        /// <summary>
        /// 受付（収集）明細テーブルの情報
        /// </summary>
        private T_UKETSUKE_SS_DETAIL[] entitysT_UKETSUKE_SS_DETAIL { get; set; }
        /// <summary>
        /// 受付（出荷）入力テーブルの情報
        /// </summary>
        private T_UKETSUKE_SK_ENTRY entitysT_UKETSUKE_SK_ENTRY { get; set; }
        /// <summary>
        /// 受付（出荷）明細テーブルの情報
        /// </summary>
        private T_UKETSUKE_SK_DETAIL[] entitysT_UKETSUKE_SK_DETAIL { get; set; }
        /// <summary>
        /// コンテナ稼動予定テーブルの情報
        /// </summary>
        private T_CONTENA_RESERVE[] entitysT_CONTENA_RESERVE { get; set; }

        /// <summary>
        /// 受付（収集）入力リスト
        /// </summary>
        private List<T_UKETSUKE_SS_ENTRY> entitySSEntryList { get; set; }
        /// <summary>
        /// 受付（収集）明細リスト
        /// </summary>
        private List<T_UKETSUKE_SS_DETAIL> entitySSDetailList { get; set; }
        /// <summary>
        /// 受付（出荷）入力リスト
        /// </summary>
        private List<T_UKETSUKE_SK_ENTRY> entitySKEntryList { get; set; }
        /// <summary>
        /// 受付（出荷）明細リスト
        /// </summary>
        private List<T_UKETSUKE_SK_DETAIL> entitySKDetailList { get; set; }
        /// <summary>
        /// コンテナ稼動予定リスト
        /// </summary>
        private List<T_CONTENA_RESERVE> entityContenaReserveList { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// モバイル連携
        /// </summary>
        public bool isRenkei = false;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        internal bool SpaceChk = false;
        internal bool SpaceON = false;

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                this.searchDto = new DTOClass();

                this.teikiHaishaEntryDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_ENTRYDao>();         // 定期配車入力情報のDao
                this.teikiHaishaDetailDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_DETAILDao>();       // 定期配車明細情報のDao
                this.teikiHaishaNioroshiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_NIOROSHIDao>();   // 定期配車荷降のDao
                this.teikiHaishaShousaiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_SHOUSAIDao>();     // 定期配車詳細のDao
                this.uketsukeSSEntryDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SS_ENTRYDao>();           // 受付（収集）入力のDao
                this.uketsukeSSDetailDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SS_DETAILDao>();         // 受付（収集）明細のDao
                this.uketsukeSKEntryDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SK_ENTRYDao>();           // 受付（出荷）入力のDao
                this.uketsukeSKDetailDao = DaoInitUtility.GetComponent<IT_UKETSUKE_SK_DETAILDao>();         // 受付（出荷）明細のDao
                this.contenaReserveDao = DaoInitUtility.GetComponent<IT_CONTENA_RESERVEDao>();              // コンテナ稼動予定のDao
                this.courseDetailDao = DaoInitUtility.GetComponent<IM_COURSE_DETAILDao>();                  // コース明細Dao
                this.courseNameDao = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>();                      // コース名称Dao
                // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 start
                this.courseEntryDao = DaoInitUtility.GetComponent<IM_COURSE_ENTRYDao>();                    // コースENTRYDao
                // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 end
                this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();  // システムID採番Dao
                this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();  // 伝種採番Dao
                this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();                 // 現場マスタのDao
                this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();             // 車輌マスタのDao
                this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();               // 車種マスタのDao
                this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();                 // 社員マスタのDao
                this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();               // 拠点マスタDao
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();             // 業者マスタDao
                // 20141015 koukouei 休動管理機能追加 start
                this.workClosedUntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();     // 運転者休動マスタDao
                this.workClosedSharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();       // 車輌休動マスタDao
                this.workClosedHanYuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();// 搬入先休動マスタDao
                // 20141015 koukouei 休動管理機能追加 end
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();// モバイル連携Dao
                this.MsgBox = new MessageBoxShowLogic();
                //モバイル連携
                this.TmobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
                this.TmobisyoRtDTLDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_DTLDao>();
                this.TmobisyoRtHNDao = DaoInitUtility.GetComponent<IT_MOBISYO_RT_HANNYUUDao>();
                // ｼｮｰﾄﾒｯｾｰｼﾞ
                this.smsReceiverLinkGenbaDao = DaoInitUtility.GetComponent<IM_SMS_RECEIVER_LINK_GENBADao>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicClass", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="windowType">処理モード</param>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(windowType);

                // ベースフォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;

                // イベントの初期化処理
                this.EventInit();

                // グリッド→DataTableへの変換イベント
                this.form.DetailIchiran.CellParsing += new DataGridViewCellParsingEventHandler(Ichiran_CellParsing);
                this.form.NioroshiIchiran.CellParsing += new DataGridViewCellParsingEventHandler(Ichiran_CellParsing);
                //※全選択/未選択チェックボックス
                this.form.checkBoxAll.Visible = false;
                is_mobile = r_framework.Configuration.AppConfig.AppOptions.IsMobile();
                if (is_mobile)
                {
                    this.form.DetailIchiran.Columns["MOBILE_RENKEI"].Visible = true;
                }
                else
                {
                    this.form.DetailIchiran.Columns["MOBILE_RENKEI"].Visible = false;
                }

                // 処理モード別画面初期化
                if (!this.ModeInit(windowType)) { ret = false; }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 新規(F2)イベント生成
                parentForm.bt_func2.Click -= new EventHandler(this.form.CreateMode);
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                // 修正ボタン(F3)イベント生成
                parentForm.bt_func3.Click -= new EventHandler(this.form.UpdateMode);
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                // 順番確定ボタン(F4)イベント生成
                //parentForm.bt_func24.Click -= new EventHandler(this.form.recDefinition);
                //parentForm.bt_func24.Click += new EventHandler(this.form.recDefinition);

                // 上ボタン(F4)イベント生成
                parentForm.bt_func4.Click -= new EventHandler(this.form.rowUp);
                parentForm.bt_func4.Click += new EventHandler(this.form.rowUp);

                // 上ボタン(F5)イベント生成
                parentForm.bt_func5.Click -= new EventHandler(this.form.rowDown);
                parentForm.bt_func5.Click += new EventHandler(this.form.rowDown);

                // 一覧(F7)イベント生成
                parentForm.bt_func7.Click -= new EventHandler(this.form.ShowIchiran);
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

                // 順番整列(F8)イベント生成
                parentForm.bt_func8.Click -= new EventHandler(this.form.SortIchiran);
                parentForm.bt_func8.Click += new EventHandler(this.form.SortIchiran);

                // 登録(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click -= new EventHandler(this.form.Regist);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click -= new EventHandler(this.form.FormClose);
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 荷降行削除ボタン
                parentForm.bt_process1.Click -= new EventHandler(this.form.DeleteNioroshiRow);
                parentForm.bt_process1.Click += new EventHandler(this.form.DeleteNioroshiRow);

                // 荷降行削除ボタン
                parentForm.bt_process2.Click -= new EventHandler(this.form.NioroshiIkkatsu);
                parentForm.bt_process2.Click += new EventHandler(this.form.NioroshiIkkatsu);

                // 地図表示ボタン
                parentForm.bt_process3.Click -= new EventHandler(this.form.MapOpen);
                parentForm.bt_process3.Click += new EventHandler(this.form.MapOpen);

                // ｼｮｰﾄﾒｯｾｰｼﾞボタン
                this.form.C_Regist(parentForm.bt_process4);
                parentForm.bt_process4.Click -= new EventHandler(this.form.SmsNyuuryoku);
                parentForm.bt_process4.Click += new EventHandler(this.form.SmsNyuuryoku);

                // 再読込ボタン
                parentForm.bt_process5.Click -= new EventHandler(this.form.reLoad);
                parentForm.bt_process5.Click += new EventHandler(this.form.reLoad);

                // 配車番号変更後処理
                this.form.TEIKI_HAISHA_NUMBER.Validated -= new EventHandler(this.form.TeikiHaishaNumberValidated);
                this.form.TEIKI_HAISHA_NUMBER.Validated += new EventHandler(this.form.TeikiHaishaNumberValidated);

                // 作業日 Enterイベント
                this.form.SAGYOU_DATE.Enter -= new EventHandler(this.form.SagyouDateEnter);
                this.form.SAGYOU_DATE.Enter += new EventHandler(this.form.SagyouDateEnter);

                // 作業日更新後処理
                this.form.SAGYOU_DATE.Validated -= new EventHandler(this.form.SagyouDateValidated);
                this.form.SAGYOU_DATE.Validated += new EventHandler(this.form.SagyouDateValidated);

                // コースCD変更後処理
                this.form.COURSE_NAME_CD.Validated -= new EventHandler(this.form.CourseNameCdValidated);
                this.form.COURSE_NAME_CD.Validated += new EventHandler(this.form.CourseNameCdValidated);

                // コースCD Enterイベント
                this.form.COURSE_NAME_CD.Enter -= new EventHandler(this.form.CourseNameCdEnter);
                this.form.COURSE_NAME_CD.Enter += new EventHandler(this.form.CourseNameCdEnter);

                // 配車番号「前」クリック処理
                this.form.previousButton.Click -= new EventHandler(this.form.TeikiHaishaNumberPreviousClick);
                this.form.previousButton.Click += new EventHandler(this.form.TeikiHaishaNumberPreviousClick);

                // 配車番号「次」クリック処理
                this.form.nextButton.Click -= new EventHandler(this.form.TeikiHaishaNumberNextClick);
                this.form.nextButton.Click += new EventHandler(this.form.TeikiHaishaNumberNextClick);

                // 運転者
                this.form.UNTENSHA_CD.Validated -= new EventHandler(this.form.UNTENSHA_CDValidated);
                this.form.UNTENSHA_CD.Validated += new EventHandler(this.form.UNTENSHA_CDValidated);

                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Validated -= new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);
                this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);

                // 出発業者
                this.form.SHUPPATSU_GYOUSHA_CD.Validated -= new EventHandler(this.form.SHUPPATSU_GYOUSHA_CD_Validated);
                this.form.SHUPPATSU_GYOUSHA_CD.Validated += new EventHandler(this.form.SHUPPATSU_GYOUSHA_CD_Validated);

                // 出発現場
                this.form.SHUPPATSU_GENBA_CD.Validating -= new CancelEventHandler(this.form.SHUPPATSU_GENBA_CD_Validating);
                this.form.SHUPPATSU_GENBA_CD.Validating += new CancelEventHandler(this.form.SHUPPATSU_GENBA_CD_Validating);
                this.form.SHUPPATSU_GENBA_CD.Validated -= new EventHandler(this.form.SHUPPATSU_GENBA_CD_Validated);
                this.form.SHUPPATSU_GENBA_CD.Validated += new EventHandler(this.form.SHUPPATSU_GENBA_CD_Validated);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            // 拠点
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            this.headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, "拠点CD");
            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.headerForm.KYOTEN_CD.Text = this.headerForm.KYOTEN_CD.Text.ToString().PadLeft(this.headerForm.KYOTEN_CD.MaxLength, '0');
                this.CheckKyotenCd();
            }
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            // 初期化
            this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

            if (string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
            {
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(this.headerForm.KYOTEN_CD.Text, out kyoteCd))
            {
                return;
            }

            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyoteCd;
            var kyotens = this.kyotenDao.GetAllValidData(keyEntity);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                //msgLogic.MessageBoxShow("E020", "拠点");
                //this.headerForm.KYOTEN_CD.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                this.headerForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType">処理モード</param>
        internal bool ModeInit(WINDOW_TYPE windowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(windowType);

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // オプション非対応
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    // mapbox用ボタン無効化
                    this.parentForm.bt_process3.Text = string.Empty;
                    this.parentForm.bt_process3.Enabled = false;
                }

                // 画面モードチェック（ｼｮｰﾄﾒｯｾｰｼﾞ）
                this.CheckWindowTypeSms(this.form.WindowType);

                // 定期配車詳細/コース明細内訳リスト初期化
                this.shousaiDataSet = new DataSet();
                //※全選択/未選択チェックボックス
                this.form.checkBoxAll.Checked = false;

                switch (windowType)
                {
                    // 【新規、複写】モード
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.WindowInitNew();
                        break;

                    // 【参照】モード
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        if (!this.WindowInitReference()) { ret = false; }
                        break;

                    // 【修正】モード
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        if (!this.WindowInitUpdate()) { ret = false; }
                        break;

                    // 【削除】モード
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.WindowInitDelete();
                        break;

                    // デフォルトは【新規】モード
                    default:
                        this.WindowInitNew();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ModeInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規、複写】
        /// </summary>
        public void WindowInitNew()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.teikiHaishaNumber))
                {
                    //【新規】モードで初期化
                    WindowInitNewMode();

                    // 拠点初期値設定
                    this.SetInitKyoten();
                }
                else
                {
                    //【複写】モードで初期化
                    WindowInitNewCopyMode();
                }
                this.form.TEIKI_HAISHA_NUMBER.Focus();   // No.2409
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNew", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化
        /// </summary>
        public void WindowInitNewMode()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 全コントロール操作可能とする
                this.AllControlLock(true);

                #region Header部項目
                // 拠点CD、略称
                this.headerForm.KYOTEN_CD.Text = string.Empty;
                this.headerForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                // 初回登録者、登録日時
                this.headerForm.CreateDate.Text = string.Empty;
                this.headerForm.CreateUser.Text = string.Empty;
                // 最終更新者、更新日時
                this.headerForm.LastUpdateDate.Text = string.Empty;
                this.headerForm.LastUpdateUser.Text = string.Empty;
                #endregion

                #region Detail-Header部項目
                // 配車番号
                this.form.TEIKI_HAISHA_NUMBER.Text = string.Empty;
                // 振替配車
                this.form.FURIKAE_HAISHA_KBN.TextChanged -= new System.EventHandler(this.form.FURIKAE_HAISHA_KBN_TextChanged);
                this.form.FURIKAE_HAISHA_KBN.Text = "1";
                this.form.COURSE_NAME_CD.ReadOnly = false;
                this.form.FURIKAE_HAISHA_KBN.TextChanged += new System.EventHandler(this.form.FURIKAE_HAISHA_KBN_TextChanged);
                //// 伝票日付
                //this.form.DENPYOU_DATE.Value = null;
                // 作業日
                this.form.SAGYOU_DATE.Value = this.parentForm.sysDate;
                // 作業開始時間_時
                this.form.SAGYOU_BEGIN_HOUR.Text = string.Empty;
                // 作業開始時間_分
                this.form.SAGYOU_BEGIN_MINUTE.Text = string.Empty;
                // 作業終了時間_時
                this.form.SAGYOU_END_HOUR.Text = string.Empty;
                // 作業終了時間_分
                this.form.SAGYOU_END_MINUTE.Text = string.Empty;
                // コース
                this.form.COURSE_NAME_CD.Text = string.Empty;
                this.form.COURSE_NAME_RYAKU.Text = string.Empty;
                this.form.DAY_CD.Text = string.Empty;
                // 車種
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
                // 車輌
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                // 運転者
                this.form.UNTENSHA_CD.Text = string.Empty;
                this.form.UNTENSHA_NAME.Text = string.Empty;
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                // 補助員
                this.form.HOJOIN_CD.Text = string.Empty;
                this.form.HOJOIN_NAME.Text = string.Empty;
                // 出発業者
                this.form.SHUPPATSU_GYOUSHA_CD.Text = string.Empty;
                this.form.SHUPPATSU_GYOUSHA_NAME.Text = string.Empty;
                // 出発現場
                this.form.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.form.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                #endregion

                #region Detail-Detail-2部項目（回収明細部）
                // 明細クリア
                this.form.DetailIchiran.Rows.Clear();
                #endregion

                #region Detail-Detail-1部項目（荷降明細部）
                // 明細クリア
                this.form.NioroshiIchiran.Rows.Clear();
                #endregion

                #region Footer部項目
                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = false;    // 修正：使用不可
                parentForm.bt_func4.Enabled = true;     // 上：使用可
                parentForm.bt_func5.Enabled = true;     // 下：使用可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可
                parentForm.bt_func8.Enabled = true;     // 順番整列：使用可
                parentForm.bt_func9.Enabled = true;     // 登録：使用可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                parentForm.bt_process5.Enabled = false; // 再読込：使用不可

                // 処理No(ESC)
                parentForm.txb_process.Enabled = true;
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        public bool WindowInitUpdate()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // データをDBから取得
                this.SetWindowData();
                // 画面項目初期化
                this.WindowInitNewMode();
                // 検索結果を画面に設定
                this.SetDataForWindow();
                this.form.TEIKI_HAISHA_NUMBER.Enabled = false;
                #region Footer部項目設定
                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = true;     // 修正：使用可
                parentForm.bt_func4.Enabled = true;     // 上：使用可
                parentForm.bt_func5.Enabled = true;     // 下：使用可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可
                parentForm.bt_func8.Enabled = true;     // 順番整列：使用可
                parentForm.bt_func9.Enabled = true;     // 登録：使用可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                parentForm.bt_process5.Enabled = true;  // 再読込：使用可
                // 処理No(ESC)
                parentForm.txb_process.Enabled = false;
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        public bool WindowInitReference()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 画面項目初期化
                this.WindowInitNewMode();

                if (this.furikaeKbn == "1" || this.furikaeKbn == "2")
                {
                    this.form.FURIKAE_HAISHA_KBN.TextChanged -= new System.EventHandler(this.form.FURIKAE_HAISHA_KBN_TextChanged);
                    this.form.FURIKAE_HAISHA_KBN.Text = this.furikaeKbn;
                    this.form.FURIKAE_HAISHA_KBN.TextChanged += new System.EventHandler(this.form.FURIKAE_HAISHA_KBN_TextChanged);
                }

                if (!string.IsNullOrEmpty(this.teikiHaishaNumber))
                {
                    // 配車番号がある場合は、配車番号でデータを取得(配車割当画面から参照モードで表示)
                    // データをDBから取得
                    this.SetWindowData();
                    // 検索結果を画面に設定
                    this.SetDataForWindow();
                }
                else
                {
                    // 定期一括作成画面からのコースCDを画面に設定する
                    this.form.COURSE_NAME_CD.Text = this.courseNameCd;

                    // コースCD変更後処理を呼び出す
                    if (!this.CourseNameCdValidated(true))
                    {
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                // 全コントロールを操作不可とする
                this.AllControlLock(false);

                #region Footer部項目設定
                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = false;    // 修正：使用不可
                parentForm.bt_func4.Enabled = false;    // 上：使用不可
                parentForm.bt_func5.Enabled = false;    // 下：使用不可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可
                parentForm.bt_func8.Enabled = false;    // 順番整列：使用不可
                parentForm.bt_func9.Enabled = false;    // 登録：使用不可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                parentForm.bt_process5.Enabled = true;  // 再読込：使用可
                // 処理No(ESC)
                parentForm.txb_process.Enabled = false;
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        public void WindowInitDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // データをDBから取得
                this.SetWindowData();

                // 画面項目初期化
                this.WindowInitNewMode();
                // 検索結果を画面に設定
                this.SetDataForWindow();

                // 全コントロールを操作不可とする
                this.AllControlLock(false);

                #region Footer部項目設定
                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = true;     // 修正：使用可
                parentForm.bt_func4.Enabled = false;    // 上：使用不可
                parentForm.bt_func5.Enabled = false;    // 下：使用不可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可
                parentForm.bt_func8.Enabled = false;    // 順番整列：使用不可
                parentForm.bt_func9.Enabled = true;     // 登録：使用可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                parentForm.bt_process5.Enabled = true;     // 再読込：使用可
                // 処理No(ESC)
                parentForm.txb_process.Enabled = false;
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        private void WindowInitNewCopyMode()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // データをDBから取得
                this.SetWindowData();
                // 画面項目初期化
                this.WindowInitNewMode();
                // 検索結果を画面に設定
                this.SetDataForWindow();

                // 配車番号:ブランク
                this.form.TEIKI_HAISHA_NUMBER.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewCopyMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データをDBから取得
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 定期配車番号を検索条件に設定する
                this.searchDto.TeikiHaishaNumber = this.teikiHaishaNumber;

                // 定期配車入力情報を取得する
                this.searchResultEntry = teikiHaishaEntryDao.GetEntryData(this.searchDto);

                // データが存在しない場合
                if (this.searchResultEntry.Rows.Count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");
                    return;
                }

                // システムID、枝番を検索条件に設定する
                this.searchDto.SystemId = long.Parse(this.searchResultEntry.Rows[0]["SYSTEM_ID"].ToString());
                this.searchDto.Seq = int.Parse(this.searchResultEntry.Rows[0]["SEQ"].ToString());

                // 定期配車明細情報を取得する
                searchResultDetail = teikiHaishaDetailDao.GetDetailData(this.searchDto);
                // 定期配車荷降情報を取得する
                searchResultNioroshi = teikiHaishaNioroshiDao.GetNioroshiData(this.searchDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索結果を画面に設定
        /// </summary>
        public void SetDataForWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                #region Header部項目
                searchResultEntry.BeginLoadData();
                // 拠点CD、略称
                this.headerForm.KYOTEN_CD.Text = int.Parse(this.searchResultEntry.Rows[0]["KYOTEN_CD"].ToString()).ToString("D2");
                this.headerForm.KYOTEN_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                // 新規モード以外の場合
                if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))
                {
                    // 初回登録者、登録日時
                    this.headerForm.CreateDate.Text = this.searchResultEntry.Rows[0]["CREATE_DATE"].ToString();
                    this.headerForm.CreateUser.Text = this.searchResultEntry.Rows[0]["CREATE_USER"].ToString();
                    // 最終更新者、更新日時
                    this.headerForm.LastUpdateDate.Text = this.searchResultEntry.Rows[0]["UPDATE_DATE"].ToString();
                    this.headerForm.LastUpdateUser.Text = this.searchResultEntry.Rows[0]["UPDATE_USER"].ToString();
                }
                #endregion

                #region Detail-Header部項目
                // 配車番号
                this.form.TEIKI_HAISHA_NUMBER.Text = this.searchResultEntry.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                //// 伝票日付
                //if (!string.IsNullOrEmpty(this.searchResultEntry.Rows[0]["DENPYOU_DATE"].ToString()))
                //{
                //    this.form.DENPYOU_DATE.Value = DateTime.Parse(this.searchResultEntry.Rows[0]["DENPYOU_DATE"].ToString());
                //}                

                // 振替配車
                this.form.FURIKAE_HAISHA_KBN.TextChanged -= new System.EventHandler(this.form.FURIKAE_HAISHA_KBN_TextChanged);
                this.form.FURIKAE_HAISHA_KBN.Text = this.searchResultEntry.Rows[0]["FURIKAE_HAISHA_KBN"].ToString();
                if (this.form.FURIKAE_HAISHA_KBN.Text.Equals("2"))
                {
                    this.form.COURSE_NAME_CD.ReadOnly = true;
                }
                else
                {
                    this.form.COURSE_NAME_CD.ReadOnly = false;
                }
                this.form.FURIKAE_HAISHA_KBN.TextChanged += new System.EventHandler(this.form.FURIKAE_HAISHA_KBN_TextChanged);
                this.form.DAY_CD.Text = Convert.ToString(this.searchResultEntry.Rows[0]["DAY_CD"]);

                // 作業日
                if (!string.IsNullOrEmpty(this.searchResultEntry.Rows[0]["SAGYOU_DATE"].ToString()))
                {
                    // 新規モード以外の場合
                    if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))   // No.2411
                    {
                        this.form.SAGYOU_DATE.Value = DateTime.Parse(this.searchResultEntry.Rows[0]["SAGYOU_DATE"].ToString());
                    }
                }
                // 新規モード以外の場合
                if (!this.form.WindowType.Equals(WINDOW_TYPE.NEW_WINDOW_FLAG))   // No.2411
                {
                    // 作業開始時間_時
                    this.form.SAGYOU_BEGIN_HOUR.Text = this.searchResultEntry.Rows[0]["SAGYOU_BEGIN_HOUR"].ToString();
                    // 作業開始時間_分
                    this.form.SAGYOU_BEGIN_MINUTE.Text = this.searchResultEntry.Rows[0]["SAGYOU_BEGIN_MINUTE"].ToString();
                    // 作業終了時間_時
                    this.form.SAGYOU_END_HOUR.Text = this.searchResultEntry.Rows[0]["SAGYOU_END_HOUR"].ToString();
                    // 作業終了時間_分
                    this.form.SAGYOU_END_MINUTE.Text = this.searchResultEntry.Rows[0]["SAGYOU_END_MINUTE"].ToString();
                }
                // コース
                this.form.COURSE_NAME_CD.Text = this.searchResultEntry.Rows[0]["COURSE_NAME_CD"].ToString();
                this.form.COURSE_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["COURSE_NAME_RYAKU"].ToString();
                // 車種
                this.form.SHASHU_CD.Text = this.searchResultEntry.Rows[0]["SHASHU_CD"].ToString();
                this.form.SHASHU_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                // 車輌
                this.form.SHARYOU_CD.Text = this.searchResultEntry.Rows[0]["SHARYOU_CD"].ToString();
                this.form.SHARYOU_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                // 運転者
                this.form.UNTENSHA_CD.Text = this.searchResultEntry.Rows[0]["UNTENSHA_CD"].ToString();
                this.form.UNTENSHA_NAME.Text = this.searchResultEntry.Rows[0]["UNTENSHA_NAME"].ToString();
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = this.searchResultEntry.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                this.form.UNPAN_GYOUSHA_NAME.Text = this.searchResultEntry.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                // 補助員
                this.form.HOJOIN_CD.Text = this.searchResultEntry.Rows[0]["HOJOIN_CD"].ToString();
                this.form.HOJOIN_NAME.Text = this.searchResultEntry.Rows[0]["HOJOIN_NAME"].ToString();
                // 出発業者
                this.form.SHUPPATSU_GYOUSHA_CD.Text = this.searchResultEntry.Rows[0]["SHUPPATSU_GYOUSHA_CD"].ToString();
                this.form.SHUPPATSU_GYOUSHA_NAME.Text = this.searchResultEntry.Rows[0]["SHUPPATSU_GYOUSHA_NAME"].ToString();
                // 出発現場
                this.form.SHUPPATSU_GENBA_CD.Text = this.searchResultEntry.Rows[0]["SHUPPATSU_GENBA_CD"].ToString();
                this.form.SHUPPATSU_GENBA_NAME.Text = this.searchResultEntry.Rows[0]["SHUPPATSU_GENBA_NAME"].ToString();

                // 排他用タイムスタンプ（非表示）
                byte[] timeStamp = (byte[])this.searchResultEntry.Rows[0]["TIME_STAMP"];
                this.form.TIME_STAMP_ENTRY.Text = ConvertStrByte.ByteToString(timeStamp);
                // システムID（非表示）
                this.form.SYSTEM_ID.Text = this.searchResultEntry.Rows[0]["SYSTEM_ID"].ToString();
                // 枝番（非表示）
                this.form.SEQ.Text = this.searchResultEntry.Rows[0]["SEQ"].ToString();

                // No.2840-->
                if (this.form.insHaisyaList != null)
                {
                    //if (this.form.SHASHU_CD.Text.Equals("") || this.form.SHARYOU_CD.Text.Equals("") && this.form.UNTENSHA_CD.Text.Equals(""))
                    //{
                    this.form.SHASHU_CD.Text = this.form.insHaisyaList[0];
                    this.form.SHASHU_NAME_RYAKU.Text = this.form.insHaisyaList[1];
                    this.form.SHARYOU_CD.Text = this.form.insHaisyaList[2];
                    this.form.SHARYOU_NAME_RYAKU.Text = this.form.insHaisyaList[3];
                    this.form.UNTENSHA_CD.Text = this.form.insHaisyaList[4];
                    this.form.UNTENSHA_NAME.Text = this.form.insHaisyaList[5];
                    this.form.UNPAN_GYOUSHA_CD.Text = this.form.insHaisyaList[6];
                    this.form.UNPAN_GYOUSHA_NAME.Text = this.form.insHaisyaList[7];
                    //}
                }
                // No.2840<--

                #endregion

                #region Detail-Detail-1部項目（荷降明細部）
                // 検索結果（定期配車荷降）
                searchResultNioroshi.BeginLoadData();

                // 明細クリア
                this.form.NioroshiIchiran.Rows.Clear();
                // データが存在する場合
                if (searchResultNioroshi.Rows.Count > 0)
                {
                    // 明細行を追加
                    this.form.NioroshiIchiran.Rows.Add(searchResultNioroshi.Rows.Count);
                    // 検索結果設定
                    for (int j = 0; j < searchResultNioroshi.Rows.Count; j++)
                    {
                        // 荷降No
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_NUMBER, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_NUMBER], string.Empty);
                        // 荷降業者CD
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], string.Empty);
                        // 荷降業者名
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU], string.Empty);
                        // 荷降現場CD
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], string.Empty);
                        // 荷降現場名
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU], string.Empty);
                    }
                }
                #endregion

                #region Detail-Detail-2部項目（回収明細部）
                // 定期配車詳細リスト初期化（回収品名詳細ポップアップ用）
                this.shousaiDataSet = new DataSet();
                // モバイル登録された荷降NoのList初期化
                this.tourokuZumiNioroshiList = new List<string>();
                // 明細クリア
                this.form.DetailIchiran.Rows.Clear();
                // 連携区分初期化
                this.isRenkei = false;
                // データが存在する場合
                if (searchResultDetail.Rows.Count > 0)
                {
                    // 明細行を追加
                    searchResultDetail.BeginLoadData();
                    this.form.DetailIchiran.Rows.Add(searchResultDetail.Rows.Count);

                    string no;
                    DataTable dt;
                    string haishaNum = this.form.TEIKI_HAISHA_NUMBER.Text;
                    // 検索結果設定
                    for (int i = 0; i < searchResultDetail.Rows.Count; i++)
                    {
                        // No
                        this.form.DetailIchiran[ConstCls.DetailColName.NO, i].Value = i + 1;
                        // 順番
                        this.form.DetailIchiran[ConstCls.DetailColName.JUNNBANN, i].Value = decimal.Parse((i + 1).ToString());
                        // 回数
                        this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.ROUND_NO], string.Empty);
                        // 業者CD
                        this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GYOUSHA_CD], string.Empty);
                        // 業者名
                        this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GYOUSHA_NAME_RYAKU], string.Empty);
                        // 現場CD
                        this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GENBA_CD], string.Empty);
                        // 現場名
                        this.form.DetailIchiran[ConstCls.DetailColName.GENBA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GENBA_NAME_RYAKU], string.Empty);
                        // 明細システムID
                        this.form.DetailIchiran[ConstCls.DetailColName.DETAIL_SYSTEM_ID, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.DETAIL_SYSTEM_ID], string.Empty);
                        // 品名情報（定期配車明細の明細システムIDをもと）
                        this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_INFO, i].Value = EditHinmeiInfo(i, long.Parse(this.searchResultDetail.Rows[i][ConstCls.DetailColName.DETAIL_SYSTEM_ID].ToString()));
                        // 希望時間
                        var kibou = searchResultDetail.Rows[i][ConstCls.DetailColName.KIBOU_TIME];
                        DateTime dateTime;
                        if (kibou is DBNull || !DateTime.TryParse(kibou.ToString(), out dateTime))
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].Value = string.Empty;
                        }
                        else
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].Value = dateTime.ToString("HH:mm");
                        }
                        // 作業時間(分)
                        this.form.DetailIchiran[ConstCls.DetailColName.SAGYOU_TIME_MINUTE, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.SAGYOU_TIME_MINUTE], string.Empty);
                        // 明細備考
                        this.form.DetailIchiran[ConstCls.DetailColName.MEISAI_BIKOU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.MEISAI_BIKOU], string.Empty);
                        // 受付番号
                        this.form.DetailIchiran[ConstCls.DetailColName.UKETSUKE_NUMBER, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.UKETSUKE_NUMBER], string.Empty);
                        // 詳細ポップアップ用テーブル名
                        this.form.DetailIchiran[ConstCls.DetailColName.SHOUSAI_TABLE_NAME, i].Value = ConstCls.preTableName + i.ToString();

                        if (!string.IsNullOrEmpty(haishaNum) && this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                        {
                            no = Convert.ToString(this.form.DetailIchiran.Rows[i].Cells["ROW_NUMBER"].Value);
                            dt = this.mobisyoRtDao.GetRenkeiDataForTeikiHaisha(haishaNum, no);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                this.form.DetailIchiran[ConstCls.DetailColName.DELETE_FLG, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.MOBILE_RENKEI, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.JUNNBANN, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.SAGYOU_TIME_MINUTE, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.MEISAI_BIKOU, i].ReadOnly = true;
                                this.tourokuZumiNioroshiList.AddRange(this.shousaiDataSet.Tables[ConstCls.preTableName + i.ToString()]
                                                                                            .Rows.Cast<DataRow>()
                                                                                            .Where(w => w["NIOROSHI_NUMBER"] != null && !string.IsNullOrEmpty(w["NIOROSHI_NUMBER"].ToString()))
                                                                                            .Select(r => r["NIOROSHI_NUMBER"].ToString()).ToList());
                                this.isRenkei = true;
                            }

                            // ロジこんぱす連携済みであるかをチェックする。
                            string selectStr;
                            selectStr = "SELECT * FROM T_LOGI_LINK_STATUS LLS"
                                + " LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD ON LDD.SYSTEM_ID = LLS.SYSTEM_ID"
                                + " WHERE LDD.DENPYOU_ATTR = 3"  //3:定期
                                + " AND LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                                + " AND LDD.REF_DENPYOU_NO = " + haishaNum
                                + " AND LDD.REF_DETAIL_SYSTEM_ID = " + this.form.DetailIchiran[ConstCls.DetailColName.DETAIL_SYSTEM_ID, i].Value
                                + " AND LDD.DELETE_FLG = 0";

                            // データ取得
                            dt = this.dateDao.GetDateForStringSql(selectStr);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                this.form.DetailIchiran[ConstCls.DetailColName.DELETE_FLG, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.MOBILE_RENKEI, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.JUNNBANN, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.SAGYOU_TIME_MINUTE, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.MEISAI_BIKOU, i].ReadOnly = true;
                                this.tourokuZumiNioroshiList.AddRange(this.shousaiDataSet.Tables[ConstCls.preTableName + i.ToString()]
                                                                                            .Rows.Cast<DataRow>()
                                                                                            .Where(w => w["NIOROSHI_NUMBER"] != null && !string.IsNullOrEmpty(w["NIOROSHI_NUMBER"].ToString()))
                                                                                            .Select(r => r["NIOROSHI_NUMBER"].ToString()).ToList());
                                this.isRenkei = true;
                            }

                            // NAVITIME連携中であるかをチェックする。
                            selectStr = " SELECT * FROM T_TEIKI_HAISHA_ENTRY T "
                                    + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                                    + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                                    + " WHERE T.DELETE_FLG = 0 "
                                    + " AND T.TEIKI_HAISHA_NUMBER = " + haishaNum;

                            // データ取得
                            dt = this.dateDao.GetDateForStringSql(selectStr);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                this.form.DetailIchiran[ConstCls.DetailColName.DELETE_FLG, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.MOBILE_RENKEI, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.JUNNBANN, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.SAGYOU_TIME_MINUTE, i].ReadOnly = true;
                                this.form.DetailIchiran[ConstCls.DetailColName.MEISAI_BIKOU, i].ReadOnly = true;
                                this.tourokuZumiNioroshiList.AddRange(this.shousaiDataSet.Tables[ConstCls.preTableName + i.ToString()]
                                                                                            .Rows.Cast<DataRow>()
                                                                                            .Where(w => w["NIOROSHI_NUMBER"] != null && !string.IsNullOrEmpty(w["NIOROSHI_NUMBER"].ToString()))
                                                                                            .Select(r => r["NIOROSHI_NUMBER"].ToString()).ToList());
                                this.isRenkei = true;
                            }

                        }
                    }
                }

                // モバイル登録された荷降NoのListを生成
                this.tourokuZumiNioroshiList = this.tourokuZumiNioroshiList.Distinct().ToList();
                #endregion

                if (this.isRenkei)
                {
                    this.headerForm.KYOTEN_CD.Enabled = !this.isRenkei;
                    this.form.SAGYOU_DATE.Enabled = !this.isRenkei;
                    this.form.SAGYOU_BEGIN_HOUR.Enabled = !this.isRenkei;
                    this.form.SAGYOU_BEGIN_MINUTE.Enabled = !this.isRenkei;
                    this.form.SAGYOU_END_HOUR.Enabled = !this.isRenkei;
                    this.form.SAGYOU_END_MINUTE.Enabled = !this.isRenkei;
                    this.form.COURSE_NAME_CD.Enabled = !this.isRenkei;
                    this.form.UNPAN_GYOUSHA_CD.Enabled = !this.isRenkei;
                    this.form.SHASHU_CD.Enabled = !this.isRenkei;
                    this.form.SHARYOU_CD.Enabled = !this.isRenkei;
                    this.form.UNTENSHA_CD.Enabled = !this.isRenkei;
                    this.form.HOJOIN_CD.Enabled = !this.isRenkei;
                    this.form.SHUPPATSU_GYOUSHA_CD.Enabled = !this.isRenkei;
                    this.form.SHUPPATSU_GENBA_CD.Enabled = !this.isRenkei;
                    this.form.FURIKAE_HAISHA_KBN.Enabled = !this.isRenkei;
                    this.form.FURIKAE_HAISHA_PANEL.Enabled = !this.isRenkei;

                    foreach (var s in this.tourokuZumiNioroshiList)
                    {
                        var targetRow = this.form.NioroshiIchiran.Rows.Cast<DataGridViewRow>()
                                                                        .Where(w => !w.IsNewRow
                                                                        && w.Cells["NIOROSHI_NUMBER"].Value != null
                                                                        && w.Cells["NIOROSHI_NUMBER"].Value.ToString() == s);

                        foreach (DataGridViewRow NioroshiRow in targetRow)
                        {
                            NioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].ReadOnly = this.isRenkei;
                            NioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].ReadOnly = this.isRenkei;
                        }
                    }
                }
                else
                {
                    this.form.FURIKAE_HAISHA_KBN.Enabled = !this.isRenkei;
                    this.form.FURIKAE_HAISHA_PANEL.Enabled = !this.isRenkei;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForWindow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            try
            {
                LogUtility.DebugMethodStart(isBool);

                #region Header部項目
                // 拠点
                this.headerForm.KYOTEN_CD.Enabled = isBool;
                #endregion

                #region Detail-Header部項目
                // 配車番号
                this.form.TEIKI_HAISHA_NUMBER.Enabled = isBool;
                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    // 配車番号前ボタン
                    this.form.previousButton.Enabled = true;
                    // 配車番号次ボタン
                    this.form.nextButton.Enabled = true;
                }
                else
                {
                    // 配車番号前ボタン
                    this.form.previousButton.Enabled = isBool;
                    // 配車番号次ボタン
                    this.form.nextButton.Enabled = isBool;
                }
                //// 伝票日付
                //this.form.DENPYOU_DATE.Enabled = isBool;
                // 作業日
                this.form.SAGYOU_DATE.Enabled = isBool;
                // 作業開始時間_時
                this.form.SAGYOU_BEGIN_HOUR.Enabled = isBool;
                // 作業開始時間_分
                this.form.SAGYOU_BEGIN_MINUTE.Enabled = isBool;
                // 作業終了時間_時
                this.form.SAGYOU_END_HOUR.Enabled = isBool;
                // 作業終了時間_分
                this.form.SAGYOU_END_MINUTE.Enabled = isBool;
                // コース
                this.form.COURSE_NAME_CD.Enabled = isBool;
                // 車種
                this.form.SHASHU_CD.Enabled = isBool;
                // 車輌
                this.form.SHARYOU_CD.Enabled = isBool;
                // 運転者
                this.form.UNTENSHA_CD.Enabled = isBool;
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Enabled = isBool;
                // 補助員
                this.form.HOJOIN_CD.Enabled = isBool;
                // 出発業者
                this.form.SHUPPATSU_GYOUSHA_CD.Enabled = isBool;
                // 出発現場
                this.form.SHUPPATSU_GENBA_CD.Enabled = isBool;
                // 振替配車
                this.form.FURIKAE_HAISHA_KBN.Enabled = isBool;
                this.form.FURIKAE_HAISHA_KBN_1.Enabled = isBool;
                this.form.FURIKAE_HAISHA_KBN_2.Enabled = isBool;
                this.form.FURIKAE_HAISHA_PANEL.Enabled = isBool;
                #endregion

                #region Detail-Detail-1部項目（荷降明細部）
                this.form.NioroshiIchiran.Columns[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].ReadOnly = !isBool;
                this.form.NioroshiIchiran.Columns[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].ReadOnly = !isBool;

                var nioroshiRows = this.form.NioroshiIchiran.Rows.Cast<DataGridViewRow>().ToList();
                nioroshiRows.ForEach(r => r.Cells.Cast<DataGridViewCell>().OfType<ICustomAutoChangeBackColor>().ToList().ForEach(c => c.UpdateBackColor()));
                #endregion

                #region Detail-Detail-2部項目（回収明細部）
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.DELETE_FLG].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.JUNNBANN].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.ROUND_NO].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_CD].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_CD].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.KIBOU_TIME].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.SAGYOU_TIME_MINUTE].ReadOnly = !isBool;
                this.form.DetailIchiran.Columns[ConstCls.DetailColName.MEISAI_BIKOU].ReadOnly = !isBool;

                var detailRows = this.form.DetailIchiran.Rows.Cast<DataGridViewRow>().ToList();
                detailRows.ForEach(r => r.Cells.Cast<DataGridViewCell>().OfType<ICustomAutoChangeBackColor>().ToList().ForEach(c => c.UpdateBackColor()));
                #endregion

                #region foot部
                parentForm.bt_process1.Enabled = isBool;
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("AllControlLock", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 品名情報を編集する（定期配車明細の明細システムIDをもと）
        /// </summary>
        /// <param name="rowIndex">行番号</param>
        /// <param name="detailSystemId">明細システムID</param>
        /// <returns>品名情報</returns>
        private string EditHinmeiInfo(int rowIndex, long detailSystemId)
        {
            // 戻り値
            string hinmeiInfo = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(rowIndex, detailSystemId);

                // 明細システムIDを検索条件に設定する
                this.searchDto.DetailSystemId = detailSystemId;
                // 定期配車詳細情報を取得する
                searchResultShousai = teikiHaishaShousaiDao.GetShousaiData(this.searchDto);
                // 定期配車詳細リストに追加する
                searchResultShousai.TableName = ConstCls.preTableName + rowIndex.ToString();
                // No.3273-->
                searchResultShousai.Columns.Add(ConstCls.ShousaiColName.DELETE_FLG);
                foreach (DataRow dtRow in searchResultShousai.Rows)
                {
                    dtRow[ConstCls.ShousaiColName.DELETE_FLG] = 0;
                }
                // No.3273<--
                this.shousaiDataSet.Tables.Add(searchResultShousai);

                hinmeiInfo = setHinmeiInfo(searchResultShousai);
                return hinmeiInfo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditHinmeiInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hinmeiInfo);
            }
        }

        /// <summary>
        /// 品名情報を編集する（コース明細のレコードIDをもと）
        /// </summary>
        /// <param name="rowIndex">行番号</param>
        /// <param name="recId">レコードID</param>
        /// <returns>品名情報</returns>
        private string EditHinmeiInfoCourse(int rowIndex, int recId)
        {
            // 戻り値
            string hinmeiInfo = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(rowIndex, recId);

                // レコードIDを検索条件に設定する
                this.searchDto.RecId = recId;
                // コース_明細内訳を取得する
                searchResultCourseDetailItems = courseDetailDao.GetCourseDetailItemsData(this.searchDto);
                // コース明細内訳リストに追加する
                searchResultCourseDetailItems.TableName = ConstCls.preTableName + rowIndex.ToString();
                // No.3273-->
                searchResultCourseDetailItems.Columns.Add(ConstCls.ShousaiColName.DELETE_FLG);
                foreach (DataRow dtRow in searchResultCourseDetailItems.Rows)
                {
                    dtRow[ConstCls.ShousaiColName.DELETE_FLG] = 0;
                }
                // No.3273<--
                if (searchResultCourseDetailItems != null && 0 < searchResultCourseDetailItems.Rows.Count)
                {
                    this.shousaiDataSet.Tables.Add(searchResultCourseDetailItems);
                }

                hinmeiInfo = setHinmeiInfo(searchResultCourseDetailItems);
                return hinmeiInfo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditHinmeiInfoCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hinmeiInfo);
            }
        }

        /// <summary>
        /// 品名情報を設定する
        /// </summary>
        /// <param name="dtHinmeiInfo">品名情報テーブル</param>
        /// <returns>品名情報</returns>
        private string setHinmeiInfo(DataTable dtHinmeiInfo)
        {
            // 品名情報格納StringBuilder
            var hinmeiInfo = new StringBuilder();

            try
            {
                LogUtility.DebugMethodStart(dtHinmeiInfo);

                if (dtHinmeiInfo.Rows.Count == 0)
                {
                    return string.Empty;
                }

                // ソートする（品名CD、レコードSEQ）
                dtHinmeiInfo.DefaultView.Sort = ConstCls.ShousaiColName.HINMEI_CD + "," + ConstCls.ShousaiColName.REC_SEQ;
                DataTable dtTemp = dtHinmeiInfo.DefaultView.ToTable();

                // 品名CD
                String strHinmeiCd = string.Empty;
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i][ConstCls.ShousaiColName.DELETE_FLG].ToString().Equals("1"))
                    {
                        continue;
                    }
                    if (dtTemp.Rows[i][ConstCls.ShousaiColName.HINMEI_CD].ToString().Equals(strHinmeiCd))
                    {
                        // 半角カンマ
                        hinmeiInfo.Append(",");
                        // 単位名
                        hinmeiInfo.Append(dtTemp.Rows[i][ConstCls.ShousaiColName.UNIT_NAME_RYAKU].ToString());
                    }
                    else
                    {
                        if (i > 0)
                        {
                            // 半角スラッシュ
                            hinmeiInfo.Append("/");
                        }
                        // 品名
                        hinmeiInfo.Append(dtTemp.Rows[i][ConstCls.ShousaiColName.HINMEI_NAME_RYAKU].ToString());
                        // 半角スペース
                        hinmeiInfo.Append(" ");
                        // 単位名
                        hinmeiInfo.Append(dtTemp.Rows[i][ConstCls.ShousaiColName.UNIT_NAME_RYAKU].ToString());
                        // 品名CD再設定
                        strHinmeiCd = dtTemp.Rows[i][ConstCls.ShousaiColName.HINMEI_CD].ToString();
                    }
                }

                // 品名情報を戻す
                return hinmeiInfo.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setHinmeiInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(hinmeiInfo.ToString());
            }
        }

        /// <summary>
        /// グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // 空文字を入力された場合
            if ("".Equals(e.Value))
            {
                e.Value = System.DBNull.Value;  // AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true;        // 後続の解析不要
            }
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeader headerForm)
        {
            try
            {
                LogUtility.DebugMethodStart(headerForm);
                this.headerForm = headerForm;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Functionボタン 押下処理
        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                this.isRegist = false;

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // 定期配車入力テーブル登録
                        this.entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = false;
                        this.teikiHaishaEntryDao.Insert(this.entitysT_TEIKI_HAISHA_ENTRY);

                        // 定期配車明細テーブル登録           
                        foreach (T_TEIKI_HAISHA_DETAIL detail in this.entityDetailList)
                        {
                            this.teikiHaishaDetailDao.Insert(detail);
                        }
                        // 定期配車荷降テーブル登録           
                        foreach (T_TEIKI_HAISHA_NIOROSHI nioroshi in this.entityNioroshilList)
                        {
                            this.teikiHaishaNioroshiDao.Insert(nioroshi);
                        }
                        // 定期配車詳細テーブル登録           
                        foreach (T_TEIKI_HAISHA_SHOUSAI shousai in this.entityShousailList)
                        {
                            this.teikiHaishaShousaiDao.Insert(shousai);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                this.isRegist = false;

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // 定期配車入力テーブル更新（論理削除）
                        this.entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = true;
                        this.teikiHaishaEntryDao.Update(this.entitysT_TEIKI_HAISHA_ENTRY);

                        // 定期配車入力テーブル登録（新しい枝番での追加）
                        this.entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = false;
                        this.entitysT_TEIKI_HAISHA_ENTRY.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ + 1;
                        this.teikiHaishaEntryDao.Insert(this.entitysT_TEIKI_HAISHA_ENTRY);

                        // 定期配車明細テーブル登録（新しい枝番での追加）
                        foreach (T_TEIKI_HAISHA_DETAIL detail in this.entityDetailList)
                        {
                            detail.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ;
                            this.teikiHaishaDetailDao.Insert(detail);
                        }

                        // 定期配車荷降テーブル登録           
                        foreach (T_TEIKI_HAISHA_NIOROSHI nioroshi in this.entityNioroshilList)
                        {
                            nioroshi.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ;
                            this.teikiHaishaNioroshiDao.Insert(nioroshi);
                        }

                        // 定期配車詳細テーブル登録           
                        foreach (T_TEIKI_HAISHA_SHOUSAI shousai in this.entityShousailList)
                        {
                            shousai.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ;
                            this.teikiHaishaShousaiDao.Insert(shousai);
                        }

                        // 受付伝票更新
                        foreach (var detail in this.entityDetailList.Where(e => false == e.UKETSUKE_NUMBER.IsNull))
                        {
                            // 受付番号で収集受付取得
                            var uketsukeSSEntry = this.uketsukeSSEntryDao.GetUketsukeSSEntryData(new DTOClass() { UketsukeNumber = detail.UKETSUKE_NUMBER.Value });
                            if (null != uketsukeSSEntry)
                            {
                                // 収集受付詳細取得
                                var uketsukeSSDetailList = this.uketsukeSSDetailDao.GetUketsukeSSDetailData(new DTOClass() { SystemId = uketsukeSSEntry.SYSTEM_ID.Value, Seq = uketsukeSSEntry.SEQ.Value });

                                // 取得した収集受付を削除
                                uketsukeSSEntry.DELETE_FLG = true;
                                this.uketsukeSSEntryDao.Update(uketsukeSSEntry);

                                // 新しい収集受付を登録
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                uketsukeSSEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                uketsukeSSEntry.UPDATE_PC = SystemInformation.ComputerName;
                                uketsukeSSEntry.UPDATE_USER = SystemProperty.UserName;
                                uketsukeSSEntry.DELETE_FLG = false;
                                uketsukeSSEntry.SEQ = uketsukeSSEntry.SEQ + 1;
                                uketsukeSSEntry.SHASHU_CD = this.form.SHASHU_CD.Text;
                                uketsukeSSEntry.SHASHU_NAME = this.form.SHASHU_NAME_RYAKU.Text;
                                uketsukeSSEntry.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                                uketsukeSSEntry.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
                                uketsukeSSEntry.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
                                uketsukeSSEntry.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
                                uketsukeSSEntry.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                                uketsukeSSEntry.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
                                uketsukeSSEntry.HOJOIN_CD = this.form.HOJOIN_CD.Text;
                                uketsukeSSEntry.HOJOIN_NAME = this.form.HOJOIN_NAME.Text;
                                this.uketsukeSSEntryDao.Insert(uketsukeSSEntry);

                                foreach (var uketsukeSSDetail in uketsukeSSDetailList)
                                {
                                    // 新しい収集受付詳細を登録
                                    uketsukeSSDetail.SEQ = uketsukeSSEntry.SEQ;
                                    this.uketsukeSSDetailDao.Insert(uketsukeSSDetail);
                                }
                            }

                            // 受付番号で出荷受付取得
                            var uketsukeSKEntry = this.uketsukeSKEntryDao.GetUketsukeSKEntryData(new DTOClass() { UketsukeNumber = detail.UKETSUKE_NUMBER.Value });
                            if (null != uketsukeSKEntry)
                            {
                                // 出荷受付詳細取得
                                var uketsukeSKDetailList = this.uketsukeSKDetailDao.GetUketsukeSKDetailData(new DTOClass() { SystemId = uketsukeSKEntry.SYSTEM_ID.Value, Seq = uketsukeSKEntry.SEQ.Value });

                                // 取得した出荷受付を削除
                                uketsukeSKEntry.DELETE_FLG = true;
                                this.uketsukeSKEntryDao.Update(uketsukeSKEntry);

                                // 新しい出荷受付を登録
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                                //uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                                uketsukeSKEntry.UPDATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                                uketsukeSKEntry.UPDATE_PC = SystemInformation.ComputerName;
                                uketsukeSKEntry.UPDATE_USER = SystemProperty.UserName;
                                uketsukeSKEntry.DELETE_FLG = false;
                                uketsukeSKEntry.SEQ = uketsukeSKEntry.SEQ + 1;
                                uketsukeSKEntry.SHASHU_CD = this.form.SHASHU_CD.Text;
                                uketsukeSKEntry.SHASHU_NAME = this.form.SHASHU_NAME_RYAKU.Text;
                                uketsukeSKEntry.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                                uketsukeSKEntry.SHARYOU_NAME = this.form.SHARYOU_NAME_RYAKU.Text;
                                uketsukeSKEntry.UNTENSHA_CD = this.form.UNTENSHA_CD.Text;
                                uketsukeSKEntry.UNTENSHA_NAME = this.form.UNTENSHA_NAME.Text;
                                uketsukeSKEntry.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                                uketsukeSKEntry.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
                                uketsukeSKEntry.HOJOIN_CD = this.form.HOJOIN_CD.Text;
                                uketsukeSKEntry.HOJOIN_NAME = this.form.HOJOIN_NAME.Text;
                                this.uketsukeSKEntryDao.Insert(uketsukeSKEntry);

                                foreach (var uketsukeSKDetail in uketsukeSKDetailList)
                                {
                                    // 新しい出荷受付を登録
                                    uketsukeSKDetail.SEQ = uketsukeSKEntry.SEQ;
                                    this.uketsukeSKDetailDao.Insert(uketsukeSKDetail);
                                }
                            }
                        }

                        // 削除したデータに関連する受付データを更新
                        foreach (var entity in this.entitySSEntryList)
                        {
                            var oldEntity = this.uketsukeSSEntryDao.GetUketsukeSSEntryData(new DTOClass() { UketsukeNumber = entity.UKETSUKE_NUMBER.Value });

                            oldEntity.DELETE_FLG = true;

                            entity.SEQ = oldEntity.SEQ + 1;
                            entity.HAISHA_JOKYO_CD = 1;
                            entity.HAISHA_JOKYO_NAME = ConstCls.HAISHA_JOUKYOU_1;
                            entity.HAISHA_SHURUI_CD = 1;
                            entity.HAISHA_SHURUI_NAME = ConstCls.HAISHA_SHURUI_1;
                            entity.SHASHU_CD = null;
                            entity.SHASHU_NAME = null;
                            entity.SHARYOU_CD = null;
                            entity.SHARYOU_NAME = null;
                            entity.UNTENSHA_CD = null;
                            entity.UNTENSHA_NAME = null;
                            entity.COURSE_KUMIKOMI_CD = 2;
                            entity.COURSE_NAME_CD = null;

                            // 更新者情報設定
                            var newEntityDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_UKETSUKE_SS_ENTRY>(entity);
                            newEntityDataBinderLogic.SetSystemProperty(entity, false);

                            entity.CREATE_DATE = oldEntity.CREATE_DATE;
                            entity.CREATE_PC = oldEntity.CREATE_PC;
                            entity.CREATE_USER = oldEntity.CREATE_USER;

                            // 古いエンティティを削除
                            oldEntity.UPDATE_DATE = entity.UPDATE_DATE;
                            oldEntity.UPDATE_PC = entity.UPDATE_PC;
                            oldEntity.UPDATE_USER = entity.UPDATE_USER;
                            this.uketsukeSSEntryDao.Update(oldEntity);

                            // 新しいエンティティを追加
                            this.uketsukeSSEntryDao.Insert(entity);
                        }
                        foreach (var entity in this.entitySSDetailList)
                        {
                            entity.SEQ = entity.SEQ;
                            this.uketsukeSSDetailDao.Insert(entity);
                        }
                        foreach (var entity in this.entitySKEntryList)
                        {
                            var oldEntity = this.uketsukeSKEntryDao.GetUketsukeSKEntryData(new DTOClass() { UketsukeNumber = entity.UKETSUKE_NUMBER.Value });

                            oldEntity.DELETE_FLG = true;


                            entity.SEQ = oldEntity.SEQ + 1;
                            entity.HAISHA_JOKYO_CD = 1;
                            entity.HAISHA_JOKYO_NAME = ConstCls.HAISHA_JOUKYOU_1;
                            entity.HAISHA_SHURUI_CD = 1;
                            entity.HAISHA_SHURUI_NAME = ConstCls.HAISHA_SHURUI_1;
                            entity.SHASHU_CD = null;
                            entity.SHASHU_NAME = null;
                            entity.SHARYOU_CD = null;
                            entity.SHARYOU_NAME = null;
                            entity.UNTENSHA_CD = null;
                            entity.UNTENSHA_NAME = null;
                            entity.COURSE_KUMIKOMI_CD = 2;
                            entity.COURSE_NAME_CD = null;

                            // 更新者情報設定
                            var newEntityDataBinderLogic = new DataBinderLogic<r_framework.Entity.T_UKETSUKE_SK_ENTRY>(entity);
                            newEntityDataBinderLogic.SetSystemProperty(entity, false);

                            entity.CREATE_DATE = oldEntity.CREATE_DATE;
                            entity.CREATE_PC = oldEntity.CREATE_PC;
                            entity.CREATE_USER = oldEntity.CREATE_USER;

                            // 古いエンティティを削除
                            oldEntity.UPDATE_DATE = entity.UPDATE_DATE;
                            oldEntity.UPDATE_PC = entity.UPDATE_PC;
                            oldEntity.UPDATE_USER = entity.UPDATE_USER;
                            this.uketsukeSKEntryDao.Update(oldEntity);

                            // 新しいエンティティを追加
                            this.uketsukeSKEntryDao.Insert(entity);
                        }
                        foreach (var entity in this.entitySKDetailList)
                        {
                            entity.SEQ = entity.SEQ;
                            this.uketsukeSSDetailDao.Insert(entity);
                        }

                        // トランザクション終了
                        tran.Commit();
                    }
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.isRegist = false;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // 定期配車入力テーブル更新（論理削除）
                        this.entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = true;
                        this.teikiHaishaEntryDao.Update(this.entitysT_TEIKI_HAISHA_ENTRY);

                        /// 20141117 Houkakou 「更新日、登録日の見直し」　start
                        this.entitysT_TEIKI_HAISHA_ENTRY.DELETE_FLG = true;
                        this.entitysT_TEIKI_HAISHA_ENTRY.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ + 1;
                        this.teikiHaishaEntryDao.Insert(this.entitysT_TEIKI_HAISHA_ENTRY);
                        /// 20141117 Houkakou 「更新日、登録日の見直し」　end

                        foreach (T_TEIKI_HAISHA_DETAIL detail in this.entityDetailList)
                        {
                            detail.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ;
                            this.teikiHaishaDetailDao.Insert(detail);
                        }

                        foreach (T_TEIKI_HAISHA_NIOROSHI nioroshi in this.entityNioroshilList)
                        {
                            nioroshi.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ;
                            this.teikiHaishaNioroshiDao.Insert(nioroshi);
                        }

                        // 定期配車詳細テーブル登録           
                        foreach (T_TEIKI_HAISHA_SHOUSAI shousai in this.entityShousailList)
                        {
                            shousai.SEQ = this.entitysT_TEIKI_HAISHA_ENTRY.SEQ;
                            this.teikiHaishaShousaiDao.Insert(shousai);
                        }

                        // 受付と関するテーブル更新処理
                        updateUketsuke();

                        // トランザクション終了
                        tran.Commit();
                    }
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="windowType">処理モード</param>
        public bool CreateEntity(WINDOW_TYPE windowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(windowType);

                // 新規の場合、システムIDを採番する
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == windowType)
                {
                    this.form.SYSTEM_ID.Text = SaibanSystemId().ToString();
                    this.form.SEQ.Text = "1";
                    this.form.TEIKI_HAISHA_NUMBER.Text = SaibanTeikiHaishaNumber().ToString();
                    this.teikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;
                }

                #region 定期配車入力テーブル
                this.entitysT_TEIKI_HAISHA_ENTRY = new T_TEIKI_HAISHA_ENTRY();
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SYSTEM_ID);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SEQ);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.headerForm.KYOTEN_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.TEIKI_HAISHA_NUMBER);
                this.Renkei_TeikiHaishaNumber = long.Parse(this.form.TEIKI_HAISHA_NUMBER.Text);
                //if (this.form.DENPYOU_DATE.Value != null)
                //{
                //    this.entitysT_TEIKI_HAISHA_ENTRY.DENPYOU_DATE = (DateTime)this.form.DENPYOU_DATE.Value;
                //}
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.FURIKAE_HAISHA_KBN);
                if (this.form.FURIKAE_HAISHA_KBN.Text == "2")
                {

                    this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.DAY_CD);
                }
                if (this.form.SAGYOU_DATE.Value != null)
                {
                    this.entitysT_TEIKI_HAISHA_ENTRY.SAGYOU_DATE = (DateTime)this.form.SAGYOU_DATE.Value;
                }
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SAGYOU_BEGIN_HOUR);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SAGYOU_BEGIN_MINUTE);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SAGYOU_END_HOUR);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SAGYOU_END_MINUTE);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.COURSE_NAME_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SHARYOU_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SHASHU_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.UNTENSHA_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.UNPAN_GYOUSHA_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.HOJOIN_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SHUPPATSU_GYOUSHA_CD);
                this.entitysT_TEIKI_HAISHA_ENTRY.SetValue(this.form.SHUPPATSU_GENBA_CD);
                // TIME_STAMPを画面から取得、更新用エンティティに設定
                this.entitysT_TEIKI_HAISHA_ENTRY.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP_ENTRY.Text);

                // 更新者情報設定
                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_TEIKI_HAISHA_ENTRY>(this.entitysT_TEIKI_HAISHA_ENTRY);
                dataBinderLogic.SetSystemProperty(this.entitysT_TEIKI_HAISHA_ENTRY, false);

                // 修正モードの場合、定期配車入力Entityの作成情報を設定
                /// 20141117 Houkakou 「更新日、登録日の見直し」　start
                if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                /// 20141117 Houkakou 「更新日、登録日の見直し」　end
                {
                    // 作成者
                    this.entitysT_TEIKI_HAISHA_ENTRY.CREATE_USER = this.searchResultEntry.Rows[0]["CREATE_USER"].ToString();
                    // 作成日
                    this.entitysT_TEIKI_HAISHA_ENTRY.CREATE_DATE = DateTime.Parse(this.searchResultEntry.Rows[0]["CREATE_DATE"].ToString());
                    // 作成PC
                    this.entitysT_TEIKI_HAISHA_ENTRY.CREATE_PC = this.searchResultEntry.Rows[0]["CREATE_PC"].ToString();
                }
                #endregion

                #region 定期配車明細テーブル（定期配車詳細、受付と関するテーブルを含む）
                int rowNumber = 0;
                DataTable dtTable = null;
                string shousaiTableName = string.Empty;
                DataGridViewRow detailRow = null;
                this.entityDetailList = new List<T_TEIKI_HAISHA_DETAIL>();
                this.entityShousailList = new List<T_TEIKI_HAISHA_SHOUSAI>();
                this.entitySSEntryList = new List<T_UKETSUKE_SS_ENTRY>();
                this.entitySSDetailList = new List<T_UKETSUKE_SS_DETAIL>();
                this.entitySKEntryList = new List<T_UKETSUKE_SK_ENTRY>();
                this.entitySKDetailList = new List<T_UKETSUKE_SK_DETAIL>();
                this.entityContenaReserveList = new List<T_CONTENA_RESERVE>();
                this.Renkei_DETAIL_SYSTEM_ID = new List<long>();

                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];

                    // 削除モードの場合、受付と関連するテーブル情報を設定する
                    if (WINDOW_TYPE.DELETE_WINDOW_FLAG == windowType)
                    {
                        // 受付番号があるの場合
                        if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString()))
                        {
                            // 受付と関するテーブルのEntityを作成する
                            CreateUketsukeEntity(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString());
                        }
                    }
                    else
                    {
                        // 削除チェックオンの場合、更新対象外になります
                        if (detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value != null && bool.Parse(detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value.ToString()))
                        {
                            // 受付番号があるの場合
                            if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString()))
                            {
                                // 受付と関するテーブルのEntityを作成する
                                CreateUketsukeEntity(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString());
                            }

                            continue;
                        }
                    }
                    rowNumber++;
                    this.entitysT_TEIKI_HAISHA_DETAIL = new T_TEIKI_HAISHA_DETAIL();
                    // システムID
                    this.entitysT_TEIKI_HAISHA_DETAIL.SetValue(this.form.SYSTEM_ID);
                    // 枝番
                    this.entitysT_TEIKI_HAISHA_DETAIL.SetValue(this.form.SEQ);
                    // 明細システムID
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString()))
                    {
                        // DB参照元がある明細は、そのキーをそのまま。
                        this.entitysT_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = (SqlInt64)Int64.Parse(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString());
                    }
                    else
                    {
                        // 追加した明細の場合、システムID採番クラスより１つ採番した戻り番号
                        this.entitysT_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID = (SqlInt64)Int64.Parse(SaibanSystemId().ToString());
                    }

                    //モバイル連携ON
                    if (detailRow.Cells[ConstCls.DetailColName.MOBILE_RENKEI].Value != null && bool.Parse(detailRow.Cells[ConstCls.DetailColName.MOBILE_RENKEI].Value.ToString()))
                    {
                        if (detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value != null && bool.Parse(detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value.ToString()))
                        {
                            //
                        }
                        else
                        {
                            this.Renkei_DETAIL_SYSTEM_ID.Add((long)this.entitysT_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID);
                        }
                    }

                    // 定期配車番号
                    this.entitysT_TEIKI_HAISHA_DETAIL.SetValue(this.form.TEIKI_HAISHA_NUMBER);
                    // 行番号
                    this.entitysT_TEIKI_HAISHA_DETAIL.ROW_NUMBER = (SqlInt16)rowNumber;
                    // 回数
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_HAISHA_DETAIL.ROUND_NO = (SqlInt32)Int32.Parse(detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString());
                    }
                    // 業者CD
                    this.entitysT_TEIKI_HAISHA_DETAIL.GYOUSHA_CD = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                    // 現場CD
                    this.entitysT_TEIKI_HAISHA_DETAIL.GENBA_CD = detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                    // 希望時間
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.KIBOU_TIME].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_HAISHA_DETAIL.KIBOU_TIME = Convert.ToDateTime(detailRow.Cells[ConstCls.DetailColName.KIBOU_TIME].FormattedValue.ToString());
                    }
                    // 作業時間(分)
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.SAGYOU_TIME_MINUTE].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_HAISHA_DETAIL.SAGYOU_TIME_MINUTE = Int16.Parse(detailRow.Cells[ConstCls.DetailColName.SAGYOU_TIME_MINUTE].FormattedValue.ToString());
                    }
                    // 明細備考
                    this.entitysT_TEIKI_HAISHA_DETAIL.MEISAI_BIKOU = detailRow.Cells[ConstCls.DetailColName.MEISAI_BIKOU].FormattedValue.ToString();
                    // 受付番号
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_HAISHA_DETAIL.UKETSUKE_NUMBER = Int64.Parse(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString());
                    }
                    // 定期配車明細リストに追加
                    entityDetailList.Add(this.entitysT_TEIKI_HAISHA_DETAIL);

                    #region 定期配車詳細テーブル
                    // 詳細ポップアップ用テーブル名を取得する
                    shousaiTableName = detailRow.Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue.ToString();

                    // 存在するの場合、データテーブルを取得する
                    dtTable = new DataTable();
                    if (this.shousaiDataSet.Tables.Contains(shousaiTableName))
                    {
                        dtTable = this.shousaiDataSet.Tables[shousaiTableName];
                    }

                    for (int k = 0; k < dtTable.Rows.Count; k++)
                    {
                        // No.3287-->
                        // 削除フラグがtrueのものは登録しない
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.DELETE_FLG].ToString())
                            && Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.DELETE_FLG].ToString()) != 0)
                        {
                            continue;
                        }
                        // No.3287<--

                        this.entitysT_TEIKI_HAISHA_SHOUSAI = new T_TEIKI_HAISHA_SHOUSAI();
                        // システムID
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.SetValue(this.form.SYSTEM_ID);
                        // 枝番
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.SetValue(this.form.SEQ);
                        // 明細システムID
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.DETAIL_SYSTEM_ID = this.entitysT_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID;
                        // 行番号
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.ROW_NUMBER = k + 1;
                        // 定期配車番号
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.SetValue(this.form.TEIKI_HAISHA_NUMBER);
                        // 品名CD
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.HINMEI_CD = dtTable.Rows[k][ConstCls.ShousaiColName.HINMEI_CD].ToString();
                        // 単位CD
                        this.entitysT_TEIKI_HAISHA_SHOUSAI.UNIT_CD = (SqlInt16)Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.UNIT_CD].ToString());
                        // 換算値
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.KANSANCHI].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.KANSANCHI = decimal.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.KANSANCHI].ToString());
                        }
                        // 換算後単位CD
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.KANSAN_UNIT_CD].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.KANSAN_UNIT_CD = (SqlInt16)Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.KANSAN_UNIT_CD].ToString());
                        }

                        //2014/01/30 追加 仕様変更 喬 start　
                        // 契約区分
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.KEIYAKU_KBN].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.KEIYAKU_KBN = (SqlInt16)Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.KEIYAKU_KBN].ToString());
                        }
                        // 計上区分
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.KEIJYOU_KBN].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.KEIJYOU_KBN = (SqlInt16)Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.KEIJYOU_KBN].ToString());
                        }

                        // 伝票区分CD
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.DENPYOU_KBN_CD].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.DENPYOU_KBN_CD = (SqlInt16)Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.DENPYOU_KBN_CD].ToString());
                        }
                        // 換算後単位モバイル出力フラグ
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.KANSAN_UNIT_MOBILE_OUTPUT_FLG = bool.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].ToString());
                        }
                        //2014/01/30 追加 仕様変更 喬 end　

                        // 入力区分
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.INPUT_KBN].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.INPUT_KBN = Int16.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.INPUT_KBN].ToString());
                        }
                        // 荷降No
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.NIOROSHI_NUMBER].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.NIOROSHI_NUMBER = Int32.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.NIOROSHI_NUMBER].ToString());
                        }
                        // 実数
                        if (!string.IsNullOrEmpty(dtTable.Rows[k][ConstCls.ShousaiColName.ANBUN_FLG].ToString()))
                        {
                            this.entitysT_TEIKI_HAISHA_SHOUSAI.ANBUN_FLG = bool.Parse(dtTable.Rows[k][ConstCls.ShousaiColName.ANBUN_FLG].ToString());
                        }

                        // 定期配車荷降リストに追加
                        entityShousailList.Add(this.entitysT_TEIKI_HAISHA_SHOUSAI);
                    }
                    #endregion
                }
                #endregion

                #region 定期配車荷降テーブル
                this.entityNioroshilList = new List<T_TEIKI_HAISHA_NIOROSHI>();
                DataGridViewRow nioroshiRow = null;
                // 荷降業者CD
                string gyoushaCd = string.Empty;
                // 荷降現場CD
                string genbaCd = string.Empty;
                for (int j = 0; j < this.form.NioroshiIchiran.Rows.Count - 1; j++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[j];
                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
                    // 空行は登録対象外とする
                    if (string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                    {
                        continue;
                    }
                    this.entitysT_TEIKI_HAISHA_NIOROSHI = new T_TEIKI_HAISHA_NIOROSHI();
                    // システムID
                    this.entitysT_TEIKI_HAISHA_NIOROSHI.SetValue(this.form.SYSTEM_ID);
                    // 枝番
                    this.entitysT_TEIKI_HAISHA_NIOROSHI.SetValue(this.form.SEQ);
                    // 荷降No
                    this.entitysT_TEIKI_HAISHA_NIOROSHI.NIOROSHI_NUMBER = (SqlInt16)Int16.Parse(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].FormattedValue.ToString());
                    // 定期配車番号
                    this.entitysT_TEIKI_HAISHA_NIOROSHI.SetValue(this.form.TEIKI_HAISHA_NUMBER);
                    // 業者CD
                    this.entitysT_TEIKI_HAISHA_NIOROSHI.NIOROSHI_GYOUSHA_CD = gyoushaCd;
                    // 現場CD
                    this.entitysT_TEIKI_HAISHA_NIOROSHI.NIOROSHI_GENBA_CD = genbaCd;

                    // 定期配車荷降リストに追加
                    entityNioroshilList.Add(this.entitysT_TEIKI_HAISHA_NIOROSHI);
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        private int SaibanSystemId()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = 1;

                using (Transaction tran = new Transaction())
                {
                    // 処理区分：120（定期配車）
                    var entity = new S_NUMBER_SYSTEM();
                    entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;

                    // テーブルロックをかけつつ、既存データがあるかを検索する。
                    var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                    // システムIDの最大値+1を取得する
                    returnInt = this.numberSystemDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_SYSTEM();
                        updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        this.numberSystemDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        this.numberSystemDao.Update(updateEntity);
                    }

                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期配車番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        private int SaibanTeikiHaishaNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = -1;

                using (Transaction tran = new Transaction())
                {
                    // 処理区分：120（定期配車）
                    var entity = new S_NUMBER_DENSHU();
                    entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;

                    // テーブルロックをかけつつ、既存データがあるかを検索する。
                    var updateEntity = this.numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                    // 伝種連番の最大値+1を取得する
                    returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                    if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                    {
                        updateEntity = new S_NUMBER_DENSHU();
                        updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;
                        updateEntity.CURRENT_NUMBER = returnInt;
                        updateEntity.DELETE_FLG = false;
                        var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                        dataBinderEntry.SetSystemProperty(updateEntity, false);

                        this.numberDenshuDao.Insert(updateEntity);
                    }
                    else
                    {
                        updateEntity.CURRENT_NUMBER = returnInt;
                        this.numberDenshuDao.Update(updateEntity);
                    }

                    // コミット
                    tran.Commit();
                }

                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 受付と関するテーブルのEntityを作成する
        /// </summary>
        /// <param name="uketsukeNumber">受付番号</param>
        /// <returns></returns> 
        private void CreateUketsukeEntity(string uketsukeNumber)
        {
            try
            {
                LogUtility.DebugMethodStart(uketsukeNumber);

                #region 受付（収集）入力テーブル
                // 受付番号を検索条件に設定
                this.searchDto.UketsukeNumber = long.Parse(uketsukeNumber);

                // 受付（収集）入力情報を取得する
                this.entitysT_UKETSUKE_SS_ENTRY = this.uketsukeSSEntryDao.GetUketsukeSSEntryData(this.searchDto);

                if (this.entitysT_UKETSUKE_SS_ENTRY != null)
                {
                    // システムID、枝番を検索条件に設定する
                    this.searchDto.SystemId = long.Parse(this.entitysT_UKETSUKE_SS_ENTRY.SYSTEM_ID.ToString());
                    this.searchDto.Seq = int.Parse(this.entitysT_UKETSUKE_SS_ENTRY.SEQ.ToString());
                    entitySSEntryList.Add(this.entitysT_UKETSUKE_SS_ENTRY);
                }
                #endregion

                #region 受付（収集）明細テーブル
                if (this.entitysT_UKETSUKE_SS_ENTRY != null)
                {
                    // 受付（収集）入力明細情報を取得する
                    this.entitysT_UKETSUKE_SS_DETAIL = this.uketsukeSSDetailDao.GetUketsukeSSDetailData(this.searchDto);

                    // 受付（収集）明細リストに追加（新しい枝番での追加）                    
                    foreach (T_UKETSUKE_SS_DETAIL detail in this.entitysT_UKETSUKE_SS_DETAIL)
                    {
                        detail.SEQ = detail.SEQ + 1;
                        entitySSDetailList.Add(detail);
                    }
                }
                #endregion

                #region コンテナ稼動予定テーブル
                if (this.entitysT_UKETSUKE_SS_ENTRY != null)
                {
                    // コンテナ稼動予定情報を取得する
                    this.entitysT_CONTENA_RESERVE = this.contenaReserveDao.GetContenaReserveData(this.searchDto);

                    // コンテナ稼動予定リストに追加（新しい枝番での追加）
                    foreach (T_CONTENA_RESERVE detail in this.entitysT_CONTENA_RESERVE)
                    {
                        detail.SEQ = detail.SEQ + 1;
                        entityContenaReserveList.Add(detail);
                    }
                }
                #endregion

                #region 受付（出荷）入力テーブル
                // 受付番号を検索条件に設定
                this.searchDto.UketsukeNumber = long.Parse(uketsukeNumber);
                // 受付（出荷）入力情報を取得する
                this.entitysT_UKETSUKE_SK_ENTRY = this.uketsukeSKEntryDao.GetUketsukeSKEntryData(this.searchDto);

                if (this.entitysT_UKETSUKE_SK_ENTRY != null)
                {
                    // システムID、枝番を検索条件に設定する
                    this.searchDto.SystemId = long.Parse(this.entitysT_UKETSUKE_SK_ENTRY.SYSTEM_ID.ToString());
                    this.searchDto.Seq = int.Parse(this.entitysT_UKETSUKE_SK_ENTRY.SEQ.ToString());
                    entitySKEntryList.Add(this.entitysT_UKETSUKE_SK_ENTRY);
                }
                #endregion

                #region 受付（出荷）明細テーブル
                if (this.entitysT_UKETSUKE_SK_ENTRY != null)
                {
                    // 受付（収集）入力明細情報を取得する
                    this.entitysT_UKETSUKE_SK_DETAIL = this.uketsukeSKDetailDao.GetUketsukeSKDetailData(this.searchDto);

                    // 受付（収集）明細リストに追加（新しい枝番での追加）
                    foreach (T_UKETSUKE_SK_DETAIL detail in this.entitysT_UKETSUKE_SK_DETAIL)
                    {
                        detail.SEQ = detail.SEQ + 1;
                        entitySKDetailList.Add(detail);
                    }
                }
                #endregion

                #region コンテナ稼動予定テーブル
                if (this.entitysT_UKETSUKE_SK_ENTRY != null)
                {
                    // コンテナ稼動予定情報を取得する
                    this.entitysT_CONTENA_RESERVE = this.contenaReserveDao.GetContenaReserveData(this.searchDto);

                    // コンテナ稼動予定リストに追加（新しい枝番での追加）
                    foreach (T_CONTENA_RESERVE detail in this.entitysT_CONTENA_RESERVE)
                    {
                        detail.SEQ = detail.SEQ + 1;
                        entityContenaReserveList.Add(detail);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateUketsukeEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 受付と関するテーブル更新処理
        /// </summary>
        private void updateUketsuke()
        {
            try
            {
                LogUtility.DebugMethodStart();

                #region 受付（収集）入力テーブル：論理削除、登録
                foreach (T_UKETSUKE_SS_ENTRY uketsukeSSEntry in this.entitySSEntryList)
                {
                    // 論理削除
                    uketsukeSSEntry.DELETE_FLG = true;
                    this.uketsukeSSEntryDao.Update(uketsukeSSEntry);

                    // 新しい枝番での追加
                    uketsukeSSEntry.DELETE_FLG = false;
                    uketsukeSSEntry.SEQ = uketsukeSSEntry.SEQ + 1;  // 対象レコードの枝番 + 1
                    uketsukeSSEntry.HAISHA_JOKYO_CD = 1;            // 配車状況CD
                    uketsukeSSEntry.HAISHA_JOKYO_NAME = "受注";     // 配車状況名
                    uketsukeSSEntry.HAISHA_SHURUI_CD = 1;           // 配車種類CD
                    uketsukeSSEntry.HAISHA_SHURUI_NAME = "通常";    // 配車種類名
                    uketsukeSSEntry.COURSE_NAME_CD = null;            // コース名称CD
                    this.uketsukeSSEntryDao.Insert(uketsukeSSEntry);
                }
                #endregion

                #region 受付（収集）明細テーブル登録（新しい枝番での追加）
                foreach (T_UKETSUKE_SS_DETAIL uketsukeSSDetai in this.entitySSDetailList)
                {
                    this.uketsukeSSDetailDao.Insert(uketsukeSSDetai);
                }
                #endregion

                #region 受付（出荷）入力テーブル：論理削除、登録
                foreach (T_UKETSUKE_SK_ENTRY uketsukeSKEntry in this.entitySKEntryList)
                {
                    // 論理削除
                    uketsukeSKEntry.DELETE_FLG = true;
                    this.uketsukeSKEntryDao.Update(uketsukeSKEntry);

                    // 新しい枝番での追加
                    uketsukeSKEntry.DELETE_FLG = false;
                    uketsukeSKEntry.SEQ = uketsukeSKEntry.SEQ + 1;  // 対象レコードの枝番 + 1
                    uketsukeSKEntry.HAISHA_JOKYO_CD = 1;            // 配車状況CD
                    uketsukeSKEntry.HAISHA_JOKYO_NAME = "受注";     // 配車状況名
                    uketsukeSKEntry.HAISHA_SHURUI_CD = 1;           // 配車種類CD
                    uketsukeSKEntry.HAISHA_SHURUI_NAME = "通常";    // 配車種類名
                    uketsukeSKEntry.COURSE_NAME_CD = null;            // コース名称CD
                    this.uketsukeSKEntryDao.Insert(uketsukeSKEntry);
                }
                #endregion

                #region 受付（出荷）明細テーブル登録（新しい枝番での追加）
                foreach (T_UKETSUKE_SK_DETAIL uketsukeSKDetai in this.entitySKDetailList)
                {
                    this.uketsukeSKDetailDao.Insert(uketsukeSKDetai);
                }
                #endregion

                #region コンテナ稼動予定テーブル登録（新しい枝番での追加）
                foreach (T_CONTENA_RESERVE contenaReserve in this.entityContenaReserveList)
                {
                    this.contenaReserveDao.Insert(contenaReserve);
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("updateUketsuke", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 拠点CD、作業日チェック
        /// コース名を入力する場合の拠点CD、作業日は必須
        /// </summary>
        /// <returns>true:問題なし, false:問題あり</returns>
        internal bool CheckKyotenAndSagyouDate()
        {
            bool returnVal = true;
            try
            {
                String errMessage = string.Empty;
                // 拠点に入力が無い場合エラー
                if (string.IsNullOrEmpty(this.form.header_new.KYOTEN_CD.Text))
                {
                    errMessage = "拠点CD";
                }

                int errorKbn = 0;
                // 振替配車に入力が無い場合エラー
                if (string.IsNullOrEmpty(this.form.FURIKAE_HAISHA_KBN.Text))
                {
                    if (string.IsNullOrEmpty(errMessage))
                    {
                        errMessage = "振替配車";
                    }
                    else
                    {
                        errMessage = errMessage + "、振替配車";
                    }
                    errorKbn = 1;
                }

                // 作業日に入力が無い場合エラー
                if (this.form.SAGYOU_DATE.Value == null)
                {
                    if (string.IsNullOrEmpty(errMessage))
                    {
                        errMessage = "作業日";
                    }
                    else
                    {
                        errMessage = errMessage + "、作業日";
                    }
                    if (errorKbn == 0)
                    {
                        errorKbn = 2;
                    }
                }

                // エラーがあるの場合
                if (!string.IsNullOrEmpty(errMessage))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E034", errMessage);
                    if (errorKbn == 1)
                    {
                        this.form.FURIKAE_HAISHA_KBN.Focus();
                    }
                    else
                    {
                        this.form.SAGYOU_DATE.Focus();
                    }
                    returnVal = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKyotenAndSagyouDate", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            return returnVal;
        }

        /// <summary>
        /// コースマスタの存在チェック（拠点）
        /// </summary>
        /// <returns></returns>
        internal bool CheckCourseNameExsit1(out bool catchErr)
        {
            bool result = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件
                DTOClass courseNameData = new DTOClass();
                // コース名称CD
                courseNameData.CourseNameCd = this.form.COURSE_NAME_CD.Text;
                // 拠点CD
                courseNameData.KyotenCd = this.headerForm.KYOTEN_CD.Text;
                // 作業日
                courseNameData.SagyouDate = DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());

                // [コース名称CD]でM_COURSE_NAMEを検索する
                M_COURSE_NAME[] courseNameResult = courseNameDao.GetCourseNameData(courseNameData);
                // レコードが存在しない
                if (courseNameResult.Length == 0)
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckCourseNameExsit1", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// コースマスタの存在チェック（作業日）
        /// </summary>
        /// <returns></returns>
        internal bool CheckCourseNameExsit2(out bool catchErr)
        {
            bool result = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件設定
                DTOClass data = new DTOClass();
                if (this.form.FURIKAE_HAISHA_KBN.Text != "2")
                {
                    // 曜日CD
                    data.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString())).ToString();
                }
                // コース名称CD
                data.CourseNameCd = this.form.COURSE_NAME_CD.Text;
                // 拠点CD
                data.KyotenCd = this.headerForm.KYOTEN_CD.Text;
                // 作業日
                data.SagyouDate = DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());

                // [コースCD]でM_COURSEとM_COURSE_NAMEをJOINして検索する
                DataTable CourseNameList = courseDetailDao.GetCourseNameListForPopUp(data);
                // レコードが存在しない場合、エラー
                if (CourseNameList.Rows.Count == 0)
                {
                    result = false;
                }
                else
                {
                    // コース名称略称名設定
                    this.form.COURSE_NAME_RYAKU.Text = CourseNameList.Rows[0]["COURSE_NAME_RYAKU"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckCourseNameExsit2", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                catchErr = true;
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 車輌CD（＆車種CD）存在チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSharyouExsit(out bool catchErr)
        {
            bool result = true;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 車輌CDが入力されていない場合、チェックしない
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    return result;
                }

                // 検索条件設定
                M_SHARYOU entity = new M_SHARYOU();
                // 業者CD
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    entity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                // 車輌CD
                entity.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                // 車種CD
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    entity.SHASYU_CD = this.form.SHASHU_CD.Text;
                }

                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidData(entity);
                // レコードが存在しない場合、エラー
                if (returnEntitys.Length == 0)
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyouExsit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 荷降一覧と詳細にある荷卸Noの互換性をチェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckNioroshiNo(out bool catchErr, out string nioroshiNumber)
        {
            bool result = true;
            catchErr = false;
            string renNumber = string.Empty;
            try
            {
                // 荷卸場のNoListを取得
                var nioroshiNoList = this.form.NioroshiIchiran.Rows.Cast<DataGridViewRow>()
                    .Where(w => !w.IsNewRow
                        && !string.IsNullOrEmpty(w.Cells["NIOROSHI_GYOUSHA_CD"].Value.ToString())
                        && !string.IsNullOrEmpty(w.Cells["NIOROSHI_GENBA_CD"].Value.ToString()))
                    .Select(r => r.Cells["NIOROSHI_NUMBER"].Value.ToString()).ToList();

                // 詳細の荷降Noに荷降一覧に含まれていない値が設定されていないかチェック
                this.shousaiDataSet.Tables.Cast<DataTable>().ToList()
                    .ForEach(DT => DT.Rows.Cast<DataRow>()
                        .Where(w => !string.IsNullOrEmpty(w["NIOROSHI_NUMBER"].ToString())).ToList()
                        .ForEach(f =>
                        {
                            if (!nioroshiNoList.Contains(f["NIOROSHI_NUMBER"].ToString()))
                            {
                                renNumber = f["NIOROSHI_NUMBER"].ToString();
                                result = false;
                            }
                        }));
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiNoExsit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr, renNumber);
            }
            nioroshiNumber = renNumber;
            return result;
        }


        /// <summary>
        /// 作業時間入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSagyouTime()
        {
            bool result = false;
            try
            {
                var msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_HOUR.Text) && string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "作業開始時間");
                    this.form.SAGYOU_BEGIN_MINUTE.IsInputErrorOccured = true;
                    this.form.SAGYOU_BEGIN_MINUTE.Focus();
                    return result;
                }
                else if (string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_HOUR.Text) && !string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "作業開始時間");
                    this.form.SAGYOU_BEGIN_HOUR.IsInputErrorOccured = true;
                    this.form.SAGYOU_BEGIN_HOUR.Focus();
                    return result;
                }
                else if (!string.IsNullOrEmpty(this.form.SAGYOU_END_HOUR.Text) && string.IsNullOrEmpty(this.form.SAGYOU_END_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "作業終了時間");
                    this.form.SAGYOU_END_MINUTE.IsInputErrorOccured = true;
                    this.form.SAGYOU_END_MINUTE.Focus();
                    return result;
                }
                else if (string.IsNullOrEmpty(this.form.SAGYOU_END_HOUR.Text) && !string.IsNullOrEmpty(this.form.SAGYOU_END_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "作業終了時間");
                    this.form.SAGYOU_END_HOUR.IsInputErrorOccured = true;
                    this.form.SAGYOU_END_HOUR.Focus();
                    return result;
                }

                result = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntenshaCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 明細部の入力チェックを行う
        /// </summary>
        /// <returns></returns>
        internal bool IsInputCheckOK()
        {
            bool result = true;

            try
            {
                LogUtility.DebugMethodStart();

                // 荷降業者CD
                string gyoushaCd = string.Empty;
                // 荷降現場CD
                string genbaCd = string.Empty;
                // 荷降No
                string nioroshiNumber = string.Empty;
                // 荷降明細部のレコード
                DataGridViewRow nioroshiRow = null;
                // 回収明細部のレコード
                DataGridViewRow detailRow = null;

                #region Detail-Detail-1部（荷降明細部）荷降業者入力チェック
                bool gyoushaCdErrorFlag = false;
                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];
                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], false);

                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();

                    // 荷降現場CDに入力が有り、当該項目が入力されていない場合
                    if (!string.IsNullOrEmpty(genbaCd) && string.IsNullOrEmpty(gyoushaCd))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], true);
                        gyoushaCdErrorFlag = true;
                    }
                }

                if (gyoushaCdErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "荷降業者");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-1部（荷降明細部）荷降現場入力チェック
                bool genbaCdErrorFlag = false;

                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];
                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], false);

                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();

                    // 荷降業者CDに入力が有り、当該項目が入力されていない場合
                    if (!string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                        genbaCdErrorFlag = true;
                    }
                }

                if (genbaCdErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "荷降現場");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-1部（荷降明細部）存在チェック
                bool IsDBExsitErrorFlag = false;
                M_GENBA searchData = new M_GENBA();
                M_GENBA[] returnEntitys = null;

                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];
                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], false);

                    // 荷降業者CD、荷降現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();

                    // ブランクの場合、チェックしない
                    if (string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                    {
                        continue;
                    }

                    // 検索条件設定
                    searchData.GYOUSHA_CD = gyoushaCd;
                    searchData.GENBA_CD = genbaCd;

                    // [荷降業者CD,荷降現場CD]でM_GENBAを検索し、レコードが存在しない場合
                    returnEntitys = genbaDao.GetAllValidData(searchData);
                    if (returnEntitys.Length == 0)
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                        // 現場名をクリア
                        nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                        IsDBExsitErrorFlag = true;
                    }
                    // 積替保管区分 = 0 and 処分事業場区分 = 0 and 最終処分場区分 = 0 and 荷降現場区分 = 0
                    else if (!returnEntitys[0].TSUMIKAEHOKAN_KBN
                        && !returnEntitys[0].SHOBUN_NIOROSHI_GENBA_KBN
                        && !returnEntitys[0].SAISHUU_SHOBUNJOU_KBN)
                    {
                        // エラー項目背景色は赤色に設定 
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                        // 現場名をクリア
                        nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                        IsDBExsitErrorFlag = true;
                    }
                }

                if (IsDBExsitErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "現場");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-1部（荷降明細部）入力行の連続性チェック
                bool brankErrorFlag = false;
                // 荷降業者CD
                gyoushaCd = string.Empty;
                // 荷降現場CD
                genbaCd = string.Empty;

                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];

                    // [荷降業者CD,荷降現場CD]に入力有る行か上から連続していない場合
                    if (i > 0 && string.IsNullOrEmpty(genbaCd) && string.IsNullOrEmpty(gyoushaCd))
                    {
                        if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString())
                            || !string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString()))
                        {
                            // エラー項目背景色は赤色に設定
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[i - 1].Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[i - 1].Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                            brankErrorFlag = true;
                        }
                    }
                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
                }

                if (brankErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "空白行を空けずに荷降業者CD,荷降現場CD");
                    result = false;
                    return result;
                }
                #endregion

                //#region Detail-Detail-1部（荷降明細部）重複チェック 
                //// 重複チェック用リストを作成
                //List<string> strItemList = new List<string>();
                //for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                //{
                //    nioroshiRow = this.form.NioroshiIchiran.Rows[i];

                //    // 背景色をクリア
                //    ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], false);
                //    ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], false);

                //    // 業者CD + 現場CDをリストに追加
                //    strItemList.Add(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString()
                //                    + nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString());
                //}

                //// [荷降業者CD,荷降現場CD]の組み合わせが同じ行が有る場合
                //if (!IsExsitCheckOK(strItemList))
                //{
                //    // アラート表示
                //    var messageShowLogic = new MessageBoxShowLogic();
                //    messageShowLogic.MessageBoxShow("E031", "荷降業者CD,荷降現場CD");
                //    result = false;
                //    return result;
                //}
                //#endregion

                #region Detail-Detail-2部（回収明細部）存在チェック
                IsDBExsitErrorFlag = false;
                searchData = new M_GENBA();

                for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];
                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GENBA_CD], false);

                    // 業者CD、現場CDを取得する
                    searchData.GYOUSHA_CD = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                    searchData.GENBA_CD = detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();

                    // [業者CD,現場CD]でM_GENBAを検索し、レコードが存在しない場合
                    returnEntitys = genbaDao.GetAllValidData(searchData);
                    if (returnEntitys.Length == 0)
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GENBA_CD], true);
                        // 現場名をクリア
                        detailRow.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                        IsDBExsitErrorFlag = true;
                    }
                }

                if (IsDBExsitErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E020", "現場");
                    result = false;
                    return result;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsInputCheckOK", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }

        /// <summary>
        /// 作業時間のチェック
        /// </summary>
        /// <returns></returns>
        internal bool SagyouTimeCheck()
        {
            ArrayList errColName = new ArrayList();

            Boolean rtn = true;

            Boolean isErr = false;

            DataGridViewRow detailRow = null;

            isErr = false;
            for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
            {
                detailRow = this.form.DetailIchiran.Rows[i];
                if (!string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["KIBOU_TIME"].Value)))
                {
                    if (string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_HOUR.Text) || string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_MINUTE.Text))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("作業時間（開始）");
                            isErr = true;
                        }
                    }
                }
            }

            isErr = false;
            for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
            {
                detailRow = this.form.DetailIchiran.Rows[i];
                if (!string.IsNullOrEmpty(Convert.ToString(detailRow.Cells["KIBOU_TIME"].Value)))
                {
                    if (string.IsNullOrEmpty(this.form.SAGYOU_END_HOUR.Text) || string.IsNullOrEmpty(this.form.SAGYOU_END_MINUTE.Text))
                    {
                        if (false == isErr)
                        {
                            errColName.Add("作業時間（終了）");
                            isErr = true;
                        }
                    }
                }
            }

            rtn = (errColName.Count == 0);

            MessageUtility messageUtility = new MessageUtility();
            string message = messageUtility.GetMessage("E001").MESSAGE;
            List<string> nioroshiNoList = new List<string>();

            string errMsg = "";
            if (!rtn)
            {
                foreach (string colName in errColName)
                {
                    if (errMsg.Length > 0)
                    {
                        errMsg += "\n";
                    }
                    errMsg += message.Replace("{0}", colName);
                }
                MessageBox.Show(errMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            return true;
        }
        ///// <summary>
        ///// 荷降明細部の重複チェックを行う
        ///// </summary>
        ///// <returns>true:重複なし、false:重複あり</returns>
        //private bool IsExsitCheckOK(List<string> strItemList)
        //{
        //    bool result = true;

        //    try
        //    {
        //        LogUtility.DebugMethodStart(strItemList);

        //        for (int i = 0; i < strItemList.Count; i++)
        //        {
        //            for (int j = i + 1; j < strItemList.Count; j++)
        //            {
        //                if (strItemList[i].Equals(strItemList[j]) && !string.IsNullOrEmpty(strItemList[j]))
        //                {
        //                    // エラー項目背景色は赤色に設定
        //                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[j].Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], true);
        //                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[j].Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
        //                    result = false;
        //                    break;
        //                }
        //            }
        //        }

        //        return result;                
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("IsExsitCheckOK", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd();
        //    }
        //}
        #endregion

        #region 配車番号変更後処理
        /// <summary>
        /// 配車番号変更後処理
        /// </summary>
        public void TeikiHaishaNumberValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 定期配車番号を検索条件に設定する
                this.searchDto.TeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;

                // 定期配車入力情報を取得する
                this.searchResultEntry = teikiHaishaEntryDao.GetEntryData(this.searchDto);

                // データが存在しない場合
                if (this.searchResultEntry.Rows.Count == 0)
                {
                    // アラート表示し、フォーカス移動しない
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");
                    this.form.TEIKI_HAISHA_NUMBER.Focus();
                    return;
                }

                // 配車番号設定
                this.teikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;
                // 変更前の配車番号を更新する
                this.form.bakTeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;

                // 権限チェック（配車番号入力、前・次ボタン押下のイベントで修正・参照両方権限無しはチェック済みのためアラートは不要）
                if (r_framework.Authority.Manager.CheckAuthority("G030", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モードを修正に変更
                    this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                    // 画面項目初期化【修正】モード
                    if (!this.ModeInit(WINDOW_TYPE.UPDATE_WINDOW_FLAG)) { return; }
                }
                else
                {
                    // 処理モードを参照に変更
                    this.form.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                    // 画面項目初期化【参照】モード
                    if (!this.ModeInit(WINDOW_TYPE.REFERENCE_WINDOW_FLAG)) { return; }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaNumberValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 前の配車番号取得処理
        /// <summary>
        /// 現在表示している配車番号よりひとつ小さい配車番号を取得する
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">配車番号</param>
        /// <param name="kyoten">拠点</param>
        /// <returns>前の配車番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, String kyoten, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue, kyoten);

                // SQL文作成            
                string selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                if (!String.IsNullOrEmpty(numberValue))
                {
                    selectStr += "   AND " + fieldName + " < " + long.Parse(numberValue);
                }
                if (!String.IsNullOrEmpty(kyoten))
                {
                    selectStr += "   AND KYOTEN_CD = " + kyoten;
                }

                // データ取得
                DataTable dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 取得できない場合、配車番号の最大値を取得する
                if (!String.IsNullOrEmpty(numberValue) && Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    if (!String.IsNullOrEmpty(kyoten))
                    {
                        selectStr += "   AND KYOTEN_CD = " + kyoten;
                    }
                    // データ取得
                    dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                }

                // 前の配車番号を戻す
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                returnVal = string.Empty;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion

        #region 次の配車番号取得処理
        /// <summary>
        /// 現在表示している配車番号よりひとつ大きい配車番号を取得する
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">配車番号</param>
        /// <param name="kyoten">拠点</param>
        /// <returns>次の配車番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, String kyoten, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue, kyoten);

                // SQL文作成
                string selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                if (!String.IsNullOrEmpty(numberValue))
                {
                    selectStr += "   AND " + fieldName + " > " + int.Parse(numberValue);
                }
                if (!String.IsNullOrEmpty(kyoten))
                {
                    selectStr += "   AND KYOTEN_CD = " + kyoten;
                }

                // データ取得
                DataTable dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 取得できない場合、配車番号の最小値を取得する
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    if (!String.IsNullOrEmpty(kyoten))
                    {
                        selectStr += "   AND KYOTEN_CD = " + kyoten;
                    }
                    // データ取得
                    dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                }


                // 次の配車番号を戻す
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                returnVal = string.Empty;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion

        #region コースCD変更後処理
        /// <summary>
        /// コースCD変更後処理
        /// </summary>
        /// <param name="isReference">参照モードフラグ</param>
        public bool CourseNameCdValidated(bool isReference)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart(isReference);

                // 参照モード以外、コースマスタの存在チェックを行う
                if (!isReference)
                {
                    bool catchErr = false;
                    // ①コースマスタの存在チェック
                    // 検索条件
                    DTOClass courseNameData = new DTOClass();
                    // コース名称CD
                    courseNameData.CourseNameCd = this.form.COURSE_NAME_CD.Text;
                    // 作業日
                    courseNameData.SagyouDate = DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                    // 曜日
                    switch (this.form.DAY_NM.Text)
                    {
                        case "月":
                            courseNameData.DayCd = "1";
                            break;
                        case "火":
                            courseNameData.DayCd = "2";
                            break;
                        case "水":
                            courseNameData.DayCd = "3";
                            break;
                        case "木":
                            courseNameData.DayCd = "4";
                            break;
                        case "金":
                            courseNameData.DayCd = "5";
                            break;
                        case "土":
                            courseNameData.DayCd = "6";
                            break;
                        case "日":
                            courseNameData.DayCd = "7";
                            break;
                    }

                    // [コース名称CD]でM_COURSE_NAMEを検索する
                    M_COURSE_NAME[] courseNameResult = courseNameDao.GetCourseNameData(courseNameData);
                    // レコードが存在しない
                    if (courseNameResult.Length == 0)
                    {
                        // コース名称略称名をクリア
                        this.form.COURSE_NAME_RYAKU.Text = string.Empty;

                        // アラート表示し、フォーカス移動しない
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E020", "コース");
                        this.form.COURSE_NAME_CD.Focus();
                        this.isInputError = true;
                        return ret;
                    }

                    // ②コースマスタの存在チェック（拠点）
                    if (!this.CheckCourseNameExsit1(out catchErr))
                    {
                        if (!catchErr)
                        {
                            // アラート表示し、フォーカス移動しない
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E062", "コースCDは拠点");
                            this.isInputError = true;
                        }
                        this.form.COURSE_NAME_CD.Focus();
                        return ret;
                    }

                    // ③コースマスタの存在チェック（作業日）
                    if (this.form.FURIKAE_HAISHA_KBN.Text != "2" && !this.CheckCourseNameExsit2(out catchErr))
                    {
                        if (!catchErr)
                        {
                            // アラート表示し、フォーカス移動しない
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E062", "コースCDは作業日の曜日");
                            this.isInputError = true;
                        }
                        this.form.COURSE_NAME_CD.Focus();
                        return ret;
                    }

                    // コース名称略称名
                    if (this.form.FURIKAE_HAISHA_KBN.Text != "2" || courseNameResult.Length == 1)
                    {
                        this.form.COURSE_NAME_RYAKU.Text = courseNameResult[0].COURSE_NAME_RYAKU;
                    }
                    else
                    {
                        // 複数が該当する場合はポップアップを表示
                        this.CourseNamePopUpDataInit(this.form.COURSE_NAME_CD.Text);
                        CustomControlExtLogic.PopUp(this.form.COURSE_NAME_CD);

                        if (string.IsNullOrEmpty(this.form.DAY_NM.Text))
                        {
                            this.isInputError = true;
                            this.form.COURSE_NAME_CD.Focus();
                            return true;
                        }
                    }
                }

                #region 検索表示

                #region 検索条件設定
                // 曜日CD
                if (isReference)
                {
                    this.searchDto.DayCd = this.dayCd;
                }
                else
                {
                    this.searchDto.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString())).ToString();
                }

                if (!isReference && this.form.FURIKAE_HAISHA_KBN.Text == "2")
                {
                    switch (this.form.DAY_NM.Text)
                    {
                        case "月":
                            this.searchDto.DayCd = "1";
                            break;
                        case "火":
                            this.searchDto.DayCd = "2";
                            break;
                        case "水":
                            this.searchDto.DayCd = "3";
                            break;
                        case "木":
                            this.searchDto.DayCd = "4";
                            break;
                        case "金":
                            this.searchDto.DayCd = "5";
                            break;
                        case "土":
                            this.searchDto.DayCd = "6";
                            break;
                        case "日":
                            this.searchDto.DayCd = "7";
                            break;
                        default:
                            this.searchDto.DayCd = null;
                            break;
                    }
                }

                // コース名称CD
                this.searchDto.CourseNameCd = this.form.COURSE_NAME_CD.Text;

                // 拠点CD
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    this.searchDto.KyotenCd = this.headerForm.KYOTEN_CD.Text;
                }

                // 作業日
                DateTime dateDefault = new DateTime();
                if (this.sagyoubi.CompareTo(dateDefault) != 0)
                {
                    this.searchDto.SagyouDate = this.sagyoubi;
                }
                else
                {
                    this.searchDto.SagyouDate = DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                }
                #endregion

                #region DBデータ取得
                // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 start
                this.searchResultCourseEntry = courseEntryDao.GetCourseEntryData(this.searchDto);
                // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 end

                // コースCDをキーとしてコース明細のデータを取得する
                this.searchResultCourseDetail = courseDetailDao.GetCourseDetailData(this.searchDto);

                // コースCDをキーとしてコース荷降先のデータを取得する
                this.searchResultCourseNioroshi = courseDetailDao.GetCourseNioroshiData(this.searchDto);
                #endregion

                // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 start
                #region ヘーダ部項目
                // 検索結果（コース）
                if (this.searchResultCourseEntry.Rows.Count != 0)
                {
                    this.form.SHASHU_CD.Text = this.searchResultCourseEntry.Rows[0]["SHASHU_CD"].ToString();
                    this.form.SHASHU_NAME_RYAKU.Text = this.searchResultCourseEntry.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                    this.form.SHARYOU_CD.Text = this.searchResultCourseEntry.Rows[0]["SHARYOU_CD"].ToString();
                    this.form.SHARYOU_NAME_RYAKU.Text = this.searchResultCourseEntry.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                    this.form.UNTENSHA_CD.Text = this.searchResultCourseEntry.Rows[0]["UNTENSHA_CD"].ToString();
                    this.form.UNTENSHA_NAME.Text = this.searchResultCourseEntry.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                    this.form.UNPAN_GYOUSHA_CD.Text = this.searchResultCourseEntry.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                    this.form.UNPAN_GYOUSHA_NAME.Text = this.searchResultCourseEntry.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();
                    this.form.SHUPPATSU_GYOUSHA_CD.Text = this.searchResultCourseEntry.Rows[0]["SHUPPATSU_GYOUSHA_CD"].ToString();
                    this.form.SHUPPATSU_GYOUSHA_NAME.Text = this.searchResultCourseEntry.Rows[0]["SHUPPATSU_GYOUSHA_NAME"].ToString();
                    this.form.SHUPPATSU_GENBA_CD.Text = this.searchResultCourseEntry.Rows[0]["SHUPPATSU_GENBA_CD"].ToString();
                    this.form.SHUPPATSU_GENBA_NAME.Text = this.searchResultCourseEntry.Rows[0]["SHUPPATSU_GENBA_NAME"].ToString();
                    this.form.SAGYOU_BEGIN_HOUR.Text = this.searchResultCourseEntry.Rows[0]["SAGYOU_BEGIN_HOUR"].ToString();
                    this.form.SAGYOU_BEGIN_MINUTE.Text = this.searchResultCourseEntry.Rows[0]["SAGYOU_BEGIN_MINUTE"].ToString();
                    this.form.SAGYOU_END_HOUR.Text = this.searchResultCourseEntry.Rows[0]["SAGYOU_END_HOUR"].ToString();
                    this.form.SAGYOU_END_MINUTE.Text = this.searchResultCourseEntry.Rows[0]["SAGYOU_END_MINUTE"].ToString();
                }
                #endregion
                // 20140627 ria EV005059 コースマスタにて登録した運搬業者、車種、車輌、運転者がセットされない。 end

                #region Detail-Detail-1部項目（荷降明細部）
                // 検索結果（コース荷降先）
                searchResultCourseNioroshi.BeginLoadData();

                // 明細クリア
                this.form.NioroshiIchiran.Rows.Clear();
                if (searchResultCourseNioroshi.Rows.Count > 0)
                {
                    // 明細行を追加
                    this.form.NioroshiIchiran.Rows.Add(searchResultCourseNioroshi.Rows.Count);
                    // 検索結果設定
                    for (int j = 0; j < searchResultCourseNioroshi.Rows.Count; j++)
                    {
                        // 荷降No
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_NUMBER, j].Value = this.ChgDBNullToValue(searchResultCourseNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_NUMBER], string.Empty);
                        // 荷降業者CD
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD, j].Value = this.ChgDBNullToValue(searchResultCourseNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], string.Empty);
                        // 荷降業者名
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU, j].Value = this.ChgDBNullToValue(searchResultCourseNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU], string.Empty);
                        // 荷降現場CD
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD, j].Value = this.ChgDBNullToValue(searchResultCourseNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], string.Empty);
                        // 荷降現場名
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU, j].Value = this.ChgDBNullToValue(searchResultCourseNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU], string.Empty);
                    }
                }
                #endregion

                #region Detail-Detail-2部項目（回収明細部）
                // コース明細内訳リスト初期化（回収品名詳細ポップアップ用）
                this.shousaiDataSet = new DataSet();
                searchResultCourseDetail.BeginLoadData();

                // 明細クリア
                this.form.DetailIchiran.Rows.Clear();
                if (searchResultCourseDetail.Rows.Count > 0)
                {
                    // コース名称略称名設定
                    this.form.COURSE_NAME_RYAKU.Text = searchResultCourseDetail.Rows[0]["COURSE_NAME_RYAKU"].ToString();

                    // 検索結果設定
                    for (int i = 0; i < searchResultCourseDetail.Rows.Count; i++)
                    {
                        // 品名情報取得。適用期間外のデータの場合読込処理中止
                        var hinmeiInfo = EditHinmeiInfoCourse(i, int.Parse(this.searchResultCourseDetail.Rows[i][ConstCls.DetailColName.REC_ID].ToString()));
                        if (string.IsNullOrEmpty(hinmeiInfo))
                        {
                            continue;
                        }

                        // 明細行を1行ずつ追加
                        this.form.DetailIchiran.Rows.Add(1);

                        // No
                        this.form.DetailIchiran[ConstCls.DetailColName.NO, i].Value = i + 1;
                        // 順番
                        this.form.DetailIchiran[ConstCls.DetailColName.JUNNBANN, i].Value = decimal.Parse((i + 1).ToString());
                        // 回数
                        this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.ROUND_NO], string.Empty);
                        // 業者CD
                        this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.GYOUSHA_CD], string.Empty);
                        // 業者名
                        this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.GYOUSHA_NAME_RYAKU], string.Empty);
                        // 現場CD
                        this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.GENBA_CD], string.Empty);
                        // 現場名
                        this.form.DetailIchiran[ConstCls.DetailColName.GENBA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.GENBA_NAME_RYAKU], string.Empty);
                        // レコードID
                        this.form.DetailIchiran[ConstCls.DetailColName.REC_ID, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.REC_ID], string.Empty);
                        // 品名情報（コース明細のレコードIDをもと）
                        this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_INFO, i].Value = hinmeiInfo;
                        // 希望時間
                        var kibou = searchResultCourseDetail.Rows[i][ConstCls.DetailColName.KIBOU_TIME];
                        DateTime dateTime;
                        if (kibou is DBNull || !DateTime.TryParse(kibou.ToString(), out dateTime))
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].Value = string.Empty;
                        }
                        else
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.KIBOU_TIME, i].Value = dateTime.ToString("HH:mm");
                        }
                        // 作業時間(分)
                        this.form.DetailIchiran[ConstCls.DetailColName.SAGYOU_TIME_MINUTE, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.SAGYOU_TIME_MINUTE], string.Empty);
                        // 備考
                        this.form.DetailIchiran[ConstCls.DetailColName.MEISAI_BIKOU, i].Value = this.ChgDBNullToValue(searchResultCourseDetail.Rows[i][ConstCls.DetailColName.MEISAI_BIKOU], string.Empty);
                        // 詳細ポップアップ用テーブル名
                        this.form.DetailIchiran[ConstCls.DetailColName.SHOUSAI_TABLE_NAME, i].Value = ConstCls.preTableName + i.ToString();
                    }
                }
                #endregion

                // 20141015 koukouei 休動管理機能追加 start
                if (!isReference)
                {
                    ChkWordClose(false);
                }
                // 20141015 koukouei 休動管理機能追加 end
                #endregion

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CourseNameCdValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region コース名称 ポップアップデータ取得処理
        /// <summary>
        /// コース名称 ポップアップデータ取得処理
        /// </summary>
        public bool CourseNamePopUpDataInit(string courseNameCd = null)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(courseNameCd);

                // コース情報 ポップアップ取得
                // 表示用データ取得＆加工
                var CourseNameDataTable = this.GetPopUpData(this.form.COURSE_NAME_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()), courseNameCd);
                // TableNameを設定すれば、ポップアップのタイトルになる
                CourseNameDataTable.TableName = "コース名選択";

                // 列名とデータソース設定
                this.form.COURSE_NAME_CD.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称", "曜日" };
                this.form.COURSE_NAME_CD.PopupDataSource = CourseNameDataTable;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CourseNamePopUpDataInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns>マスタポップアップ用データテーブル</returns>
        public DataTable GetPopUpData(IEnumerable<string> displayCol, string courseNameCD = null)
        {
            var sortedDt = new DataTable();

            try
            {
                LogUtility.DebugMethodStart(displayCol, courseNameCD);

                DTOClass data = new DTOClass();
                // 曜日CD
                if (this.form.FURIKAE_HAISHA_KBN.Text != "2" && this.form.SAGYOU_DATE.Value != null)
                {
                    data.DayCd = DateUtility.GetShougunDayOfWeek(DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString())).ToString();
                }
                // 拠点CD
                if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text))
                {
                    data.KyotenCd = this.headerForm.KYOTEN_CD.Text;
                }
                // 作業日
                if (this.form.SAGYOU_DATE.Value != null)
                {
                    data.SagyouDate = DateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                }
                else
                {
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //data.SagyouDate = DateTime.Now.Date;
                    data.SagyouDate = this.parentForm.sysDate.Date;
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                }
                // コースCD
                if (!string.IsNullOrEmpty(courseNameCD))
                {
                    data.CourseNameCd = courseNameCD;
                }

                // コース名称ポップアップデータのリストを取得する
                DataTable CourseNameList = courseDetailDao.GetCourseNameListForPopUp(data);
                if (CourseNameList.Rows.Count == 0)
                {
                    return CourseNameList;
                }

                foreach (var col in displayCol)
                {
                    // 表示対象の列だけを順番に追加
                    sortedDt.Columns.Add(CourseNameList.Columns[col].ColumnName, CourseNameList.Columns[col].DataType);
                }

                foreach (DataRow r in CourseNameList.Rows)
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }

                return sortedDt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPopUpData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sortedDt);
            }
        }
        #endregion

        #region 「詳細」ボタン押下処理
        /// <summary>
        /// 回収品名詳細ポップアップを表示する
        /// </summary>
        /// <param name="windowType">処理モード</param>
        /// <param name="detailRow">選択された明細行</param>
        public bool ShowShousai(WINDOW_TYPE windowType, DataGridViewRow detailRow)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(windowType, detailRow);

                #region 回収品名詳細ポップアップ画面用の引数作成
                // 回収品名詳細用DTO、リスト
                Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass dto = null;
                List<Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass> kaishuHinmeiShousaiList = new List<Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass>();

                long recID = 0;
                // 明細システムIDがあるの場合、明細システムID（配車系）を取得する
                if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString()))
                {
                    recID = long.Parse(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString());
                }
                // レコードIDがあるの場合、レコードID（コース系）を取得する
                if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.REC_ID].FormattedValue.ToString()))
                {
                    recID = long.Parse(detailRow.Cells[ConstCls.DetailColName.REC_ID].FormattedValue.ToString());
                }

                DataTable dtTable = null;
                string strTableName = detailRow.Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue.ToString();
                // 存在するの場合、データテーブルを取得する
                if (this.shousaiDataSet.Tables.Contains(strTableName))
                {
                    dtTable = this.shousaiDataSet.Tables[strTableName];

                    // 回収品名詳細用DTOにデータを設定する
                    foreach (DataRow dtRow in dtTable.Rows)
                    {
                        dto = new Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass();
                        // レコードID
                        dto.REC_ID = recID;
                        // レコードSEQ
                        dto.REC_SEQ = long.Parse(dtRow[ConstCls.ShousaiColName.REC_SEQ].ToString());
                        // 品名CD
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.HINMEI_CD].ToString()))
                        {
                            dto.HINMEI_CD = dtRow[ConstCls.ShousaiColName.HINMEI_CD].ToString();
                        }
                        // 単位CD
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.UNIT_CD].ToString()))
                        {
                            dto.UNIT_CD = Int16.Parse(dtRow[ConstCls.ShousaiColName.UNIT_CD].ToString());
                        }
                        // 換算値
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.KANSANCHI].ToString()))
                        {
                            dto.KANSANCHI = decimal.Parse(dtRow[ConstCls.ShousaiColName.KANSANCHI].ToString());
                        }
                        // 換算後単位CD
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.KANSAN_UNIT_CD].ToString()))
                        {
                            dto.KANSAN_UNIT_CD = Int16.Parse(dtRow[ConstCls.ShousaiColName.KANSAN_UNIT_CD].ToString());
                        }
                        // 削除フラグ
                        dto.DELETE_FLG = 0;
                        // No.3273-->
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.DELETE_FLG].ToString()))
                        {
                            dto.DELETE_FLG = Int16.Parse(dtRow[ConstCls.ShousaiColName.DELETE_FLG].ToString());  // No.3273
                        }
                        // No.3273<--

                        //2014/01/30 追加 仕様変更 喬 start　
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.KEIYAKU_KBN].ToString()))
                        {
                            dto.KEIYAKU_KBN = Int16.Parse(dtRow[ConstCls.ShousaiColName.KEIYAKU_KBN].ToString());
                        }
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.KEIJYOU_KBN].ToString()))
                        {
                            dto.KEIJYOU_KBN = Int16.Parse(dtRow[ConstCls.ShousaiColName.KEIJYOU_KBN].ToString());
                        }
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.DENPYOU_KBN_CD].ToString()))
                        {
                            dto.DENPYOU_KBN_CD = Int16.Parse(dtRow[ConstCls.ShousaiColName.DENPYOU_KBN_CD].ToString());
                        }
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].ToString()))
                        {
                            dto.KANSAN_UNIT_MOBILE_OUTPUT_FLG = bool.Parse(dtRow[ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].ToString());
                        }
                        //2014/01/30 追加 仕様変更 喬 end　

                        // 入力区分
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.INPUT_KBN].ToString()))
                        {
                            dto.INPUT_KBN = Int16.Parse(dtRow[ConstCls.ShousaiColName.INPUT_KBN].ToString());
                        }
                        // 荷降No
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.NIOROSHI_NUMBER].ToString()))
                        {
                            dto.NIOROSHI_NUMBER = Int32.Parse(dtRow[ConstCls.ShousaiColName.NIOROSHI_NUMBER].ToString());
                        }
                        // 実数
                        if (!string.IsNullOrEmpty(dtRow[ConstCls.ShousaiColName.ANBUN_FLG].ToString()))
                        {
                            dto.ANBUN_FLG = bool.Parse(dtRow[ConstCls.ShousaiColName.ANBUN_FLG].ToString());
                        }

                        // 回収品名詳細用リストに追加
                        kaishuHinmeiShousaiList.Add(dto);
                    }
                }
                // 存在しない場合、データテーブルを初期化する
                else
                {
                    if (string.IsNullOrEmpty(strTableName))
                    {
                        // 詳細ポップアップ用テーブル名設定
                        strTableName = ConstCls.preTableName + detailRow.Index.ToString();
                        detailRow.Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].Value = strTableName;
                    }
                    dtTable = new DataTable(strTableName);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.REC_ID);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.REC_SEQ);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.HINMEI_CD);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.HINMEI_NAME_RYAKU);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.UNIT_CD);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.UNIT_NAME_RYAKU);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.KANSANCHI);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.KANSAN_UNIT_CD);

                    dtTable.Columns.Add(ConstCls.ShousaiColName.DELETE_FLG);    // No.3273

                    //2014/01/30 追加 仕様変更 喬 start　
                    dtTable.Columns.Add(ConstCls.ShousaiColName.KEIYAKU_KBN);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.KEIJYOU_KBN);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.DENPYOU_KBN_CD);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG);
                    //2014/01/30 追加 仕様変更 喬 end　

                    dtTable.Columns.Add(ConstCls.ShousaiColName.INPUT_KBN);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.NIOROSHI_NUMBER);
                    dtTable.Columns.Add(ConstCls.ShousaiColName.ANBUN_FLG);

                    dtTable.AcceptChanges();
                    this.shousaiDataSet.Tables.Add(dtTable);
                }

                // 業者CDを取得する
                string gyoushaCd = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                // 現場CDを取得する
                string genbaCd = detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                List<string> nioroshiList = new List<string>();
                string niorishiNum = string.Empty;
                foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    niorishiNum = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                    if (!string.IsNullOrEmpty(niorishiNum))
                    {
                        nioroshiList.Add(niorishiNum);
                    }
                }
                #endregion

                #region 回収品名詳細ポップアップを表示する及び戻り値設定
                DateTime now = ((BusinessBaseForm)this.form.Parent).sysDate;
                Shougun.Core.Common.KaisyuuHinmeShousai.UIForm callForm =
                    new Shougun.Core.Common.KaisyuuHinmeShousai.UIForm(windowType, recID, 0, gyoushaCd, genbaCd, kaishuHinmeiShousaiList, nioroshiList, "G030", now);
                var headerForm = new Shougun.Core.Common.KaisyuuHinmeShousai.UIHeader();
                var popForm = new BasePopForm(callForm, headerForm);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    popForm.ShowDialog();

                    // 返却回収品名詳細リストを取得する
                    List<Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass> retCntRetList = callForm.RetKaishuHinmeiSyousaiList;

                    if (retCntRetList.Count > 0)
                    {
                        dtTable.Rows.Clear();
                        DataRow dtNewRow = null;

                        // 返却結果繰り返す
                        foreach (Shougun.Core.Common.KaisyuuHinmeShousai.DTOClass dtoRet in retCntRetList)
                        {
                            // 削除しないレコードは対象になる
                            //if (dtoRet.DELETE_FLG == 0)   // No.3273
                            //{                             // No.3273
                            dtNewRow = dtTable.NewRow();
                            dtNewRow[ConstCls.ShousaiColName.REC_ID] = dtoRet.REC_ID;
                            dtNewRow[ConstCls.ShousaiColName.REC_SEQ] = dtoRet.REC_SEQ;
                            dtNewRow[ConstCls.ShousaiColName.HINMEI_CD] = dtoRet.HINMEI_CD;
                            dtNewRow[ConstCls.ShousaiColName.HINMEI_NAME_RYAKU] = dtoRet.HINMEI_NAME;
                            if (!dtoRet.UNIT_CD.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.UNIT_CD] = dtoRet.UNIT_CD.ToString();
                                dtNewRow[ConstCls.ShousaiColName.UNIT_NAME_RYAKU] = dtoRet.UNIT_NAME;
                            }

                            dtNewRow[ConstCls.ShousaiColName.DELETE_FLG] = dtoRet.DELETE_FLG.ToString();    // No.3273

                            //2014/01/30 追加 仕様変更 喬 start　
                            //「契約区分」
                            if (!dtoRet.KEIYAKU_KBN.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.KEIYAKU_KBN] = dtoRet.KEIYAKU_KBN.ToString();
                            }
                            //「計上区分」
                            if (!dtoRet.KEIJYOU_KBN.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.KEIJYOU_KBN] = dtoRet.KEIJYOU_KBN.ToString();
                            }
                            //「伝票区分」
                            if (!dtoRet.DENPYOU_KBN_CD.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.DENPYOU_KBN_CD] = dtoRet.DENPYOU_KBN_CD.ToString();
                            }
                            //「換算後単位モバイル出力フラグ」→「品名情報：要記入」から
                            if (!dtoRet.KANSAN_UNIT_MOBILE_OUTPUT_FLG.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG] = dtoRet.KANSAN_UNIT_MOBILE_OUTPUT_FLG.ToString();
                            }
                            else
                            {
                                dtNewRow[ConstCls.ShousaiColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG] = false;
                            }
                            //2014/01/30 追加 仕様変更 喬 end　

                            if (!dtoRet.KANSANCHI.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.KANSANCHI] = dtoRet.KANSANCHI.ToString();
                            }
                            if (!dtoRet.KANSAN_UNIT_CD.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.KANSAN_UNIT_CD] = dtoRet.KANSAN_UNIT_CD.ToString();
                            }

                            if (!dtoRet.INPUT_KBN.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.INPUT_KBN] = dtoRet.INPUT_KBN.ToString();
                            }
                            if (!dtoRet.NIOROSHI_NUMBER.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.NIOROSHI_NUMBER] = dtoRet.NIOROSHI_NUMBER.ToString();
                            }
                            if (!dtoRet.ANBUN_FLG.IsNull)
                            {
                                dtNewRow[ConstCls.ShousaiColName.ANBUN_FLG] = dtoRet.ANBUN_FLG.ToString();
                            }

                            dtTable.Rows.Add(dtNewRow);
                        }
                        dtTable.AcceptChanges();

                        // 品名情報をDataGridViewに設定する
                        detailRow.Cells[ConstCls.DetailColName.HINMEI_INFO].Value = setHinmeiInfo(dtTable);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowShousai", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChechSharyouCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 20141015 koukouei 休動管理機能追加 start
                /*// 前回値と変わらない場合は処理を行わない
                if (true == this.oldSharyouCD.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }*/
                // 20141015 koukouei 休動管理機能追加 end

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
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (sharyou == null)
                {
                    // 背景色変更
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    this.isInputError = true;
                    return returnVal;
                }

                // 20141015 koukouei 休動管理機能追加 start
                // 休動チェック
                if (!sharyou.DELETE_FLG.IsTrue && !this.ChkSharyouWordClose(true))
                {
                    return returnVal;
                }
                // 20141015 koukouei 休動管理機能追加 end

                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text) && !sharyou.SHASYU_CD.Equals(this.form.SHASHU_CD.Text))
                {
                    // 背景色変更
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E104", "車輌", "車種");
                    this.isInputError = true;
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
                        this.form.SHASHU_NAME_RYAKU.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                // 運転者入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    // 社員情報取得
                    var shain = this.GetShain(sharyou.SHAIN_CD);
                    if (shain != null)
                    {
                        // 運転者情報設定
                        this.form.UNTENSHA_CD.Text = shain.SHAIN_CD;
                        this.form.UNTENSHA_NAME.Text = shain.SHAIN_NAME_RYAKU;
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
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechSharyouCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
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
        public M_SHARYOU GetSharyou(string sharyouCd)
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
                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                SqlDateTime sagyouDate = SqlDateTime.Null;
                if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    sagyouDate = SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                }
                var returnEntitys = sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "9", sagyouDate, true, false, false);
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

                        //SendKeys.Send(" ");

                        this.isCalledSharyouPopupFromLogic = true;
                        // 検索ポップアップ起動
                        var result = CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);

                        // 車輌名を一旦クリアしているので入力されている場合は選択されたとする
                        if (!string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
                        {
                            returnVal = new M_SHARYOU();
                            returnVal.SHARYOU_NAME_RYAKU = this.form.SHARYOU_NAME_RYAKU.Text;
                            returnVal.SHASYU_CD = this.form.SHASHU_CD.Text;
                            returnVal.SHAIN_CD = this.form.UNTENSHA_CD.Text;
                            returnVal.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                            // 20141015 koukouei 休動管理機能 start
                            returnVal.DELETE_FLG = true;
                            // 20141015 koukouei 休動管理機能 end
                        }
                        else
                        {
                            // 返却値は空白をセット
                            returnVal = new M_SHARYOU();
                            returnVal.SHARYOU_NAME_RYAKU = "";
                            returnVal.SHASYU_CD = "";
                            returnVal.SHAIN_CD = "";
                            returnVal.GYOUSHA_CD = "";
                            // 20141015 koukouei 休動管理機能 start
                            returnVal.DELETE_FLG = true;
                            // 20141015 koukouei 休動管理機能 end
                        }
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
        public M_SHASHU GetSharshu(string shashuCd)
        {
            M_SHASHU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shashuCd);

                if (string.IsNullOrEmpty(shashuCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHASHU keyEntity = new M_SHASHU();
                keyEntity.SHASHU_CD = shashuCd;

                // [車種CD]でM_SHASHUを検索する
                var returnEntitys = this.shashuDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

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
        public M_SHAIN GetShain(string shainCd)
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
        #endregion

        #region 荷降業者チェック
        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd(DataGridViewRow nioroshiRow)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart(nioroshiRow);

                // 荷降業者CD
                String nioroshiGyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                // メッセージ対象
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // エラーフラグ
                bool errFlg = false;

                // 入力されてない場合
                if (String.IsNullOrEmpty(nioroshiGyoushaCd))
                {
                    // 荷降業者名をクリア
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = string.Empty;
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    // 処理終了
                    returnVal = true;
                    return true;
                }

                nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value =
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString().PadLeft(6, '0').ToUpper();

                // 変更されていたら現場をクリア
                if (!this.form.beforeNioroshiGyoushaCd.Equals(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value))
                {
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                }

                // 業者情報取得
                var gyoushaEntity = this.GetGyousha(nioroshiGyoushaCd);

                // 取得できない場合
                if (gyoushaEntity == null)
                {
                    errFlg = true;
                }
                // 運搬受託者 = 0 and 処分受託者 = 0 and 荷積降業者 = 0
                // 20151026 BUNN #12040 STR
                else if (!gyoushaEntity.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue
                    && !gyoushaEntity.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    errFlg = true;
                }

                SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }
                if (!errFlg)
                {
                    if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull)
                    {
                        errFlg = false;
                    }
                    else if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                    {
                        errFlg = false;
                    }
                    else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull
                         && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0)
                    {
                        errFlg = false;
                    }
                    else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                         && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0
                         && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                    {
                        errFlg = false;
                    }
                    else
                    {
                        errFlg = true;
                    }
                }

                if (errFlg)
                {
                    // 荷降業者名をクリア
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = string.Empty;
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    // 処理終了
                    return returnVal;
                }

                // 荷降業者名を設定
                nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyoushaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
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
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            M_GYOUSHA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    // PK指定のため1件
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }

                    if (gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull)
                    {
                        returnVal = gyousha[0];
                    }
                    else if (gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = gyousha[0];
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0)
                    {
                        returnVal = gyousha[0];
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = gyousha[0];
                    }
                }

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

        #region 荷降現場チェック
        /// <summary>
        /// 荷降現場チェック
        /// </summary>
        internal bool ChechNioroshiGenbaCd(DataGridViewRow nioroshiRow)
        {
            bool returnVal = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 荷降現場CD
                String nioroshiGenbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
                // メッセージ対象
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                // エラーフラグ
                bool errFlg = false;

                // 入力されてない場合
                if (String.IsNullOrEmpty(nioroshiGenbaCd))
                {
                    // 荷降現場名をクリア
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    // 処理終了
                    returnVal = true;
                    return true;
                }

                nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value =
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value.ToString().PadLeft(6, '0').ToUpper();

                // 荷降業者入力されてない場合
                String nioroshiGyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                if (string.IsNullOrEmpty(nioroshiGyoushaCd))
                {
                    // エラーメッセージ
                    // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    msgLogic.MessageBoxShow("E051", "荷降業者");
                    // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                    this.isInputError = true;
                    // 荷降現場名をクリア
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    if (this.form.NioroshiIchiran.EditingControl != null)
                    {
                        this.form.NioroshiIchiran.EditingControl.Text = string.Empty;
                    }
                    return returnVal;
                }

                // 現場情報を取得
                M_GENBA genbaEntity = this.GetGenba(nioroshiGyoushaCd, nioroshiGenbaCd);

                // 取得できない場合
                if (genbaEntity == null)
                {
                    errFlg = true;
                }
                // 積替保管区分 = 0 and 処分事業場区分 = 0 and 最終処分場区分 = 0 and 荷降現場区分 = 0
                // 20151026 BUNN #12040 STR
                else if (!genbaEntity.TSUMIKAEHOKAN_KBN.IsTrue
                    && !genbaEntity.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue
                    && !genbaEntity.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                // 20151026 BUNN #12040 END
                {
                    errFlg = true;
                }

                SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }
                if (!errFlg)
                {
                    if (genbaEntity.TEKIYOU_BEGIN.IsNull && genbaEntity.TEKIYOU_END.IsNull)
                    {
                        errFlg = false;
                    }
                    else if (genbaEntity.TEKIYOU_BEGIN.IsNull && !genbaEntity.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genbaEntity.TEKIYOU_END) <= 0)
                    {
                        errFlg = false;
                    }
                    else if (!genbaEntity.TEKIYOU_BEGIN.IsNull && genbaEntity.TEKIYOU_END.IsNull
                         && tekiyouDate.CompareTo(genbaEntity.TEKIYOU_BEGIN) >= 0)
                    {
                        errFlg = false;
                    }
                    else if (!genbaEntity.TEKIYOU_BEGIN.IsNull && !genbaEntity.TEKIYOU_END.IsNull
                         && tekiyouDate.CompareTo(genbaEntity.TEKIYOU_BEGIN) >= 0
                         && tekiyouDate.CompareTo(genbaEntity.TEKIYOU_END) <= 0)
                    {
                        errFlg = false;
                    }
                    else
                    {
                        errFlg = true;
                    }
                }

                if (errFlg)
                {
                    // 荷降現場名をクリア
                    nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    // 処理終了
                    return returnVal;
                }

                // 20141015 koukouei 休動管理機能追加 start
                if (!this.ChkGenbaWordClose(nioroshiRow, true))
                {
                    returnVal = false;
                    return returnVal;
                }
                // 20141015 koukouei 休動管理機能追加 end

                // 荷降現場名を設定
                nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genbaEntity.GENBA_NAME_RYAKU;

                // 荷降業者名を設定
                this.SetNioroshiGyoushaName(nioroshiRow);

                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
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
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            M_GENBA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    // PK指定のため1件
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        returnVal = genba[0];
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = genba[0];
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        returnVal = genba[0];
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        returnVal = genba[0];
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        /// <summary>
        /// 運転者CDバリデート
        /// </summary>
        public void UNTENSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNTENSHA_NAME.Text = "";

                var msgLogic = new MessageBoxShowLogic();
                var untenShain = this.shainDao.GetAllValidData(new M_SHAIN()).FirstOrDefault(s => (bool)s.UNTEN_KBN && s.SHAIN_CD == this.form.UNTENSHA_CD.Text);
                if (untenShain == null || untenShain.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.form.UNTENSHA_CD.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.HOJOIN_CD.Text)
                    && this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    // エラーメッセージ
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    this.form.UNTENSHA_CD.Focus();
                    return;
                }
                // 20141015 koukouei 休動管理機能追加 start
                // 休動チェック
                else if (!this.ChkUntenshaWordClose(true))
                {
                    this.form.UNTENSHA_CD.Focus();
                    return;
                }
                // 20141015 koukouei 休動管理機能追加 end

                this.form.UNTENSHA_NAME.Text = untenShain.SHAIN_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬業者CDバリデート
        /// </summary>
        public void UNPAN_GYOUSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = "";
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.ClearBeforeText(this.form.SHARYOU_CD.Name);
                M_GYOUSHA gyousha = new M_GYOUSHA();
                gyousha.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                var unpanGyousha = this.gyoushaDao.GetAllValidData(gyousha).FirstOrDefault();
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    this.form.ClearBeforeText(this.form.UNPAN_GYOUSHA_CD.Name);
                    return;
                }

                SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }

                if (unpanGyousha.TEKIYOU_BEGIN.IsNull && unpanGyousha.TEKIYOU_END.IsNull)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else if (unpanGyousha.TEKIYOU_BEGIN.IsNull && !unpanGyousha.TEKIYOU_END.IsNull
                    && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_END) <= 0)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else if (!unpanGyousha.TEKIYOU_BEGIN.IsNull && unpanGyousha.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_BEGIN) >= 0)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else if (!unpanGyousha.TEKIYOU_BEGIN.IsNull && !unpanGyousha.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_BEGIN) >= 0
                        && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_END) <= 0)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    this.form.ClearBeforeText(this.form.UNPAN_GYOUSHA_CD.Name);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        /// <summary>
        /// 回数重複チェック
        /// </summary>
        /// <param name="row">編集行</param>
        /// <returns>TRUE:重複あり, FALSE:重複なし</returns>
        /// <remarks>一覧より、編集行の回数・業者CD・現場CDが重複するものがあるかどうかをチェックする</remarks>
        internal bool roundNoOverlapCheck(DataGridViewRow editRow, out bool catcErr)
        {
            bool ret = false;
            catcErr = false;
            try
            {
                if ((editRow.Cells["ROUND_NO"].Value != null) && (editRow.Cells["GYOUSHA_CD"].Value != null) && (editRow.Cells["GENBA_CD"].Value != null))
                {
                    // 一覧内を検索
                    foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                    {
                        // 編集行はチェック対象外
                        if (editRow.Index != row.Index)
                        {
                            // 回数が一致した場合

                            if ((row.Cells["ROUND_NO"].Value != null) && (row.Cells["GYOUSHA_CD"].Value != null) && (row.Cells["GENBA_CD"].Value != null))
                            {
                                if (true == row.Cells["ROUND_NO"].Value.ToString().Equals(editRow.Cells["ROUND_NO"].Value.ToString()))
                                {
                                    // 業者CDが一致した場合
                                    if (true == row.Cells["GYOUSHA_CD"].Value.Equals(editRow.Cells["GYOUSHA_CD"].Value))
                                    {
                                        // 現場CDが一致した場合
                                        if (true == row.Cells["GENBA_CD"].Value.Equals(editRow.Cells["GENBA_CD"].Value))
                                        {
                                            // 全てが一致したら、重複ありとして返却
                                            ret = true;
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
                LogUtility.Error("roundNoOverlapCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = true;
                catcErr = true;
            }
            return ret;
        }

        // 20141015 koukouei 休動管理機能追加 start
        #region 補助員CDバリデート
        /// <summary>
        /// 補助員CDバリデート
        /// </summary>
        public void HOJOIN_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.HOJOIN_NAME.Text = "";

                var msgLogic = new MessageBoxShowLogic();
                var untenShain = this.shainDao.GetAllValidData(new M_SHAIN()).FirstOrDefault(s => (bool)s.UNTEN_KBN && s.SHAIN_CD == this.form.HOJOIN_CD.Text);
                if (untenShain == null || untenShain.DELETE_FLG.IsTrue)
                {
                    // エラーメッセージ
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.form.HOJOIN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.form.HOJOIN_CD.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text)
                    && this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    // エラーメッセージ
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.form.HOJOIN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    this.form.HOJOIN_CD.Focus();
                    return;
                }
                // 休動チェック
                else if (!this.ChkHojoinWordClose(true))
                {
                    this.form.HOJOIN_CD.Focus();
                    return;
                }

                this.form.HOJOIN_NAME.Text = untenShain.SHAIN_NAME_RYAKU; ;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HOJOIN_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 休動チェック
        /// <summary>
        /// 休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkWordClose(bool registFlg)
        {
            bool ret = false;
            try
            {
                if (!ChkSharyouWordClose(registFlg))
                {
                    this.form.SHARYOU_CD.Focus();
                    return ret;
                }

                if (!ChkUntenshaWordClose(registFlg))
                {
                    this.form.UNTENSHA_CD.Focus();
                    return ret;
                }

                if (registFlg && !ChkHojoinWordClose(registFlg))
                {
                    this.form.HOJOIN_CD.Focus();
                    return ret;
                }

                foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                {
                    if (!ChkGenbaWordClose(row, registFlg))
                    {
                        return ret;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkWordClose", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            // 処理終了
            return ret;
        }
        #endregion

        #region 車輌休動チェック
        /// <summary>
        /// 車輌休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkSharyouWordClose(bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text) || string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                return true;
            }

            M_WORK_CLOSED_SHARYOU sharyou = new M_WORK_CLOSED_SHARYOU();
            sharyou.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                sharyou.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            sharyou.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_SHARYOU[] sharyous = this.workClosedSharyouDao.GetAllValidData(sharyou);
            if (sharyous.Length > 0)
            {
                // エラーメッセージ
                this.form.SHARYOU_CD.IsInputErrorOccured = true;
                this.form.SHARYOU_CD.BackColor = Constans.ERROR_COLOR;
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", sharyou.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "車輛", date);
                }
                else
                {
                    msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "車輛", date);
                }
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 運転者休動チェック
        /// <summary>
        /// 運転者休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkUntenshaWordClose(bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                return true;
            }

            M_WORK_CLOSED_UNTENSHA untensha = new M_WORK_CLOSED_UNTENSHA();
            untensha.SHAIN_CD = this.form.UNTENSHA_CD.Text;
            untensha.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_UNTENSHA[] untenshas = this.workClosedUntenshaDao.GetAllValidData(untensha);
            if (untenshas.Length > 0)
            {
                // エラーメッセージ
                this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                this.form.UNTENSHA_CD.BackColor = Constans.ERROR_COLOR;
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", untensha.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "運転者", date);
                }
                else
                {
                    msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "運転者", date);
                }
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 補助員休動チェック
        /// <summary>
        /// 補助員休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkHojoinWordClose(bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                return true;
            }

            M_WORK_CLOSED_UNTENSHA untensha = new M_WORK_CLOSED_UNTENSHA();
            untensha.SHAIN_CD = this.form.HOJOIN_CD.Text;
            untensha.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_UNTENSHA[] sharyous = this.workClosedUntenshaDao.GetAllValidData(untensha);
            if (sharyous.Length > 0)
            {
                // エラーメッセージ
                this.form.HOJOIN_CD.IsInputErrorOccured = true;
                this.form.HOJOIN_CD.BackColor = Constans.ERROR_COLOR;
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", untensha.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "補助員", date);
                }
                else
                {
                    msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "補助員", date);
                }
                this.form.HOJOIN_CD.Focus();
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 荷降現場休動チェック
        /// <summary>
        /// 荷降現場休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkGenbaWordClose(DataGridViewRow nioroshiRow, bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                return true;
            }

            // 荷降業者CD
            String gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
            // 荷降現場CD
            String genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
            var cell = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD] as DgvCustomAlphaNumTextBoxCell;

            M_WORK_CLOSED_HANNYUUSAKI hannyuu = new M_WORK_CLOSED_HANNYUUSAKI();
            hannyuu.GYOUSHA_CD = gyoushaCd;
            hannyuu.GENBA_CD = genbaCd;
            hannyuu.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_HANNYUUSAKI[] hannyuus = this.workClosedHanYuusakiDao.GetAllValidData(hannyuu);
            if (hannyuus.Length > 0)
            {
                // エラーメッセージ
                ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                DateTime dt = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
                string date = string.Format("作業日：{0}", dt.ToString("yyyy/MM/dd"));
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "荷降現場", date);
                }
                else
                {
                    msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "荷降現場", date);
                }
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion
        // 20141015 koukouei 休動管理機能追加 start

        /// <summary>
        /// 現場CDと業者CDを元に業者名をセットします
        /// </summary>
        /// <param name="row">DataGridViewRow</param>
        internal bool SetGyoushaName(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                // 業者CDが入力され、かつ業者名が空の場合
                if ((row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value != null
                    && !string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString()))
                    && (row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value == null
                    || row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value.ToString() == string.Empty))
                {
                    // 業者を取得
                    var gyoushaEntity = this.GetGyousha(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString());

                    // 取得できた場合
                    if (gyoushaEntity != null)
                    {
                        SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }

                        if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                    }
                }

                // フォーカスを現場にセット
                //this.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, row.Index].Selected = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaName", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 荷降現場CDと荷降業者CDを元に荷降業者名をセットします
        /// </summary>
        /// <param name="row">DataGridViewRow</param>
        internal bool SetNioroshiGyoushaName(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                // 荷降業者CDが入力され、かつ荷降業者名が空の場合
                if ((row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value != null
                    && !string.IsNullOrEmpty(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString()))
                    && (row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value == null
                    || row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value.ToString() == string.Empty))
                {
                    // 業者を取得
                    var gyoushaEntity = this.GetGyousha(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString());

                    // 取得できた場合
                    if (gyoushaEntity != null)
                    {
                        SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetNioroshiGyoushaName", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #region 現場CDバリデート
        /// <summary>
        /// 業者CDバリデート
        /// </summary>
        public bool ChechiGyoushaCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                String gyoushaCd = row.Cells["GYOUSHA_CD"].FormattedValue.ToString();
                var cell = row.Cells["GYOUSHA_CD"] as DgvCustomAlphaNumTextBoxCell;

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var genba = this.gyoushaDao.GetAllValidData(keyEntity);
                if (genba == null || genba.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    row.Cells["GYOUSHA_NAME_RYAKU"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.DetailIchiran.CurrentRow.Index >= 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }

                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = genba[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = genba[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = genba[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = genba[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.isInputError = true;
                        row.Cells["GYOUSHA_NAME_RYAKU"].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiGyoushaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 現場CDバリデート
        /// <summary>
        /// 現場CDバリデート
        /// </summary>
        public bool ChechiGenbaCd(DataGridViewRow row)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 荷降業者CD
                String gyoushaCd = row.Cells["GYOUSHA_CD"].FormattedValue.ToString();
                // 荷降現場CD
                String genbaCd = row.Cells["GENBA_CD"].FormattedValue.ToString();
                var cell = row.Cells["GENBA_CD"] as DgvCustomAlphaNumTextBoxCell;

                if (string.IsNullOrEmpty(genbaCd))
                {
                    ret = true;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.isInputError = true;
                    row.Cells["GENBA_CD"].Value = string.Empty;
                    if (this.form.DetailIchiran.EditingControl != null)
                    {
                        this.form.DetailIchiran.EditingControl.Text = string.Empty;
                    }
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);
                if (genba == null || genba.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    row.Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.DetailIchiran.CurrentRow.Index >= 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }

                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        row.Cells["GENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["GENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        row.Cells["GENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["GENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.isInputError = true;
                        row.Cells["GENBA_NAME_RYAKU"].Value = string.Empty;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiGenbaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #region 「荷降No一括入力」ボタン押下処理
        /// <summary>
        /// 荷降No一括入力画面を表示する
        /// </summary>
        public bool ShowNioroshiIkkatsu()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                #region 荷降No一括入力画面用の引数作成
                // 荷降No一括入力画面用DTO、リスト
                NioroshiDto nioroshiDto = null;
                DetailDto detailDto = null;
                List<NioroshiDto> nioroshiList = new List<NioroshiDto>();
                List<DetailDto> detailList = new List<DetailDto>();

                foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                {
                    if (row.IsNewRow ||
                        string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value)))
                    {
                        continue;
                    }
                    nioroshiDto = new NioroshiDto();
                    nioroshiDto.NIOROSHI_NUMBER = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                    nioroshiDto.NIOROSHI_GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value);
                    nioroshiDto.NIOROSHI_GYOUSHA_NAME = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value);
                    nioroshiDto.NIOROSHI_GENBA_CD = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value);
                    nioroshiDto.NIOROSHI_GENBA_NAME = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value);
                    nioroshiList.Add(nioroshiDto);
                }

                foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                {
                    if (row.IsNewRow ||
                        string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.DetailColName.NO].Value)))
                    {
                        continue;
                    }
                    string strTableName = row.Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue.ToString();
                    if (string.IsNullOrEmpty(strTableName))
                    {
                        continue;
                    }
                    DataTable dt = this.shousaiDataSet.Tables[strTableName];
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        continue;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string no;
                        DataTable RenkeiDt;
                        string haishaNum = this.form.TEIKI_HAISHA_NUMBER.Text;
                        DataRow dr = dt.Rows[i];
                        detailDto = new DetailDto();
                        detailDto.TABLE_NAME = strTableName;
                        detailDto.ROW_NO = row.Index;
                        detailDto.ROUND_NO = Convert.ToString(row.Cells[ConstCls.DetailColName.ROUND_NO].Value);
                        detailDto.GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value);
                        detailDto.GYOUSHA_NAME = Convert.ToString(row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value);
                        detailDto.GENBA_CD = Convert.ToString(row.Cells[ConstCls.DetailColName.GENBA_CD].Value);
                        detailDto.GENBA_NAME = Convert.ToString(row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value);

                        detailDto.HINMEI_CD = Convert.ToString(dr[ConstCls.ShousaiColName.HINMEI_CD]);
                        detailDto.HINMEI_NAME = Convert.ToString(dr[ConstCls.ShousaiColName.HINMEI_NAME_RYAKU]);
                        detailDto.UNIT_CD = Convert.ToString(dr[ConstCls.ShousaiColName.UNIT_CD]);
                        detailDto.UNIT_NAME = Convert.ToString(dr[ConstCls.ShousaiColName.UNIT_NAME_RYAKU]);
                        detailDto.NIOROSHI_NUMBER_DETAIL = Convert.ToString(dr[ConstCls.ShousaiColName.NIOROSHI_NUMBER]);
                        detailDto.TABLE_ROW_NO = i;
                        no = Convert.ToString(row.Cells[ConstCls.DetailColName.NO].Value);
                        RenkeiDt = this.mobisyoRtDao.GetRenkeiDataForTeikiHaisha(haishaNum, no);
                        if (RenkeiDt != null && RenkeiDt.Rows.Count > 0)
                        {
                            detailDto.isRenkei = true;
                        }
                        else
                        {
                            detailDto.isRenkei = false;
                        }
                        detailList.Add(detailDto);
                    }
                }
                #endregion

                #region 荷降No一括入力ポップアップを表示する及び戻り値設定
                Shougun.Core.Common.NioroshiNoSettei.UIForm callForm =
                    new Shougun.Core.Common.NioroshiNoSettei.UIForm(WINDOW_TYPE.ICHIRAN_WINDOW_FLAG, nioroshiList, detailList);
                var headerForm = new Shougun.Core.Common.NioroshiNoSettei.UIHeader();
                var popForm = new BasePopForm(callForm, headerForm);
                var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                if (!isExistForm)
                {
                    popForm.ShowDialog();

                    // 返却リストを取得する
                    Dictionary<string, List<DetailDto>> retList = callForm.RetDetailList;

                    //DataRow dtNewRow = null;
                    DataTable dtTable = new DataTable();
                    List<DetailDto> dtoRetList = new List<DetailDto>();

                    // 返却結果繰り返す
                    foreach (string strTableName in retList.Keys)
                    {
                        dtTable = this.shousaiDataSet.Tables[strTableName];
                        dtoRetList = retList[strTableName];
                        DataRow dr = null;
                        int rowNo = -1;
                        int num = -1;
                        foreach (DetailDto dtoRet in dtoRetList)
                        {
                            dr = dtTable.Rows[dtoRet.TABLE_ROW_NO];
                            rowNo = dtoRet.ROW_NO;
                            if (Int32.TryParse(dtoRet.NIOROSHI_NUMBER_DETAIL, out num))
                            {
                                dr[ConstCls.ShousaiColName.NIOROSHI_NUMBER] = dtoRet.NIOROSHI_NUMBER_DETAIL;
                            }
                            else
                            {
                                dr[ConstCls.ShousaiColName.NIOROSHI_NUMBER] = DBNull.Value;
                            }
                        }
                        dtTable.AcceptChanges();

                        // 品名情報をDataGridViewに設定する
                        if (rowNo != -1)
                        {
                            this.form.DetailIchiran.Rows[rowNo].Cells[ConstCls.DetailColName.HINMEI_INFO].Value = setHinmeiInfo(dtTable);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowNioroshiIkkatsu", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 連携チェック
        internal bool RenkeiCheck(bool up)
        {
            bool ret = true;
            try
            {
                string haishaNum = this.form.TEIKI_HAISHA_NUMBER.Text;
                if (string.IsNullOrEmpty(haishaNum))
                {
                    return true;
                }

                if (this.form.DetailIchiran.CurrentRow == null)
                {
                    return true;
                }

                string no;
                DataTable dt;

                if (this.searchResultDetail == null || this.searchResultDetail.Rows.Count == 0)
                {
                    return true;
                }

                int index = this.form.DetailIchiran.CurrentRow.Index;
                DataGridViewRow row = this.form.DetailIchiran.CurrentRow;

                no = Convert.ToString(row.Cells["ROW_NUMBER"].Value);
                dt = this.mobisyoRtDao.GetRenkeiDataForTeikiHaisha(haishaNum, no);

                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E261", "現在配車中", "編集");
                    return false;
                }
                if (up)
                {
                    row = this.form.DetailIchiran.Rows[index - 1];
                }
                else
                {
                    row = this.form.DetailIchiran.Rows[index + 1];
                }

                no = Convert.ToString(row.Cells["ROW_NUMBER"].Value);
                dt = this.mobisyoRtDao.GetRenkeiDataForTeikiHaisha(haishaNum, no);

                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E261", "現在配車中", "編集");
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 3"  // 3：定期
                    + " and LDD.REF_DENPYOU_NO = " + haishaNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.MsgBox.MessageBoxShow("E261", "ロジこんぱす連携中", "編集");
                    return false;
                }

                // NAVITIME連携中であるかをチェックする。
                selectStr = " SELECT * FROM T_TEIKI_HAISHA_ENTRY T "
                        + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                        + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                        + " WHERE T.DELETE_FLG = 0 "
                        + " AND T.TEIKI_HAISHA_NUMBER = " + haishaNum;

                // データ取得
                dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.MsgBox.MessageBoxShow("E261", "NAVITIME連携中", "編集");
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return true;
        }

        internal bool RenkeiCheckForDelete()
        {
            bool ret = true;
            try
            {
                string haishaNum = this.form.TEIKI_HAISHA_NUMBER.Text;
                if (string.IsNullOrEmpty(haishaNum))
                {
                    return true;
                }

                if (this.form.DetailIchiran.CurrentRow == null)
                {
                    return true;
                }

                string no;
                DataTable dt;

                if (this.searchResultDetail == null || this.searchResultDetail.Rows.Count == 0)
                {
                    return true;
                }

                foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                {
                    if (row.Cells["ROW_NUMBER"].Value != null && !string.IsNullOrEmpty(row.Cells["ROW_NUMBER"].Value.ToString()))
                    {
                        no = Convert.ToString(row.Cells["ROW_NUMBER"].Value);
                        dt = this.mobisyoRtDao.GetRenkeiDataForTeikiHaisha(haishaNum, no);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E261", "現在配車中", "編集");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return true;
        }

        /// <summary>
        /// ロジこんぱす連携済みかチェック
        /// </summary>
        /// <returns>true:正常(未連携), false:エラー(連携済)</returns>
        internal bool RenkeiCheckForLogi()
        {
            bool ret = true;
            try
            {
                string haishaNum = this.form.TEIKI_HAISHA_NUMBER.Text;
                if (string.IsNullOrEmpty(haishaNum))
                {
                    return true;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 3"  // 3：定期
                    + " and LDD.REF_DENPYOU_NO = " + haishaNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                var dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (0 < dt.Rows.Count)
                {
                    this.MsgBox.MessageBoxShow("E261", "ロジこんぱす連携中", "編集");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return true;
        }

        /// <summary>
        /// NAVITIME連携中かチェック
        /// </summary>
        /// <returns>true:正常(未連携), false:エラー(連携済)</returns>
        internal bool RenkeiCheckForNavi()
        {
            bool ret = true;
            try
            {
                string haishaNum = this.form.TEIKI_HAISHA_NUMBER.Text;
                if (string.IsNullOrEmpty(haishaNum))
                {
                    return true;
                }

                // NAVITIME連携中であるかをチェックする。
                string selectStr;
                selectStr = " SELECT * FROM T_TEIKI_HAISHA_ENTRY T "
                            + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                            + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                            + " WHERE T.DELETE_FLG = 0 "
                            + " AND T.TEIKI_HAISHA_NUMBER = " + haishaNum;

                // データ取得
                var dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (0 < dt.Rows.Count)
                {
                    this.MsgBox.MessageBoxShow("E261", "NAVITIME連携中", "編集");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return true;
        }
        #endregion

        #region [1]荷降行削除ができるかどうか
        /// <summary>
        /// [1]荷降行削除ができるかどうか
        /// </summary>
        /// <param name="row">対象の荷降明細</param>
        /// <returns>true=できる、false=できない</returns>
        internal bool canNioroshiRemove(DataGridViewRow row)
        {
            var ren = true;
            if (this.tourokuZumiNioroshiList != null
                && this.tourokuZumiNioroshiList.Any(s => s == row.Cells["NIOROSHI_NUMBER"].Value.ToString()))
            {
                this.MsgBox.MessageBoxShow("E261", "現在配車中", "編集");
                ren = false;
            }
            return ren;
        }
        #endregion

        /// <summary>
        /// 荷降NUMBERチェック
        /// </summary>
        internal bool CheckNioroshiNumber(string NIOROSHI_NUMBER)
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrEmpty(NIOROSHI_NUMBER))
                {
                    DataTable dtTable = null;
                    string strTableName = string.Empty;
                    foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        strTableName = Convert.ToString(row.Cells[ConstCls.DetailColName.SHOUSAI_TABLE_NAME].FormattedValue);
                        // 存在するの場合、データテーブルを取得する
                        if (this.shousaiDataSet.Tables.Contains(strTableName))
                        {
                            dtTable = this.shousaiDataSet.Tables[strTableName];

                            DataRow[] rows = dtTable.Select(string.Format("NIOROSHI_NUMBER = '{0}'", NIOROSHI_NUMBER));
                            if (rows != null && rows.Length > 0)
                            {
                                this.MsgBox.MessageBoxShow("E123");
                                return ret = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiNumber", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 時間入力チェック処理
        /// </summary>
        /// <returns>bool(OK:true NG:false)</returns>
        internal bool IsTimeChkOK(DataGridViewCell ctrl)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart(ctrl);
                string value = Convert.ToString(ctrl.Value);
                if (ctrl == null || string.IsNullOrEmpty(value))
                {
                    return result;
                }
                // 未入力の場合、処理中止
                if (string.IsNullOrEmpty(value))
                {
                    return result;
                }
                value = value.Replace(":", "");
                var reg = @"^(20|21|22|23|[0-1]\d)[0-5]\d$";
                result = Regex.IsMatch(value, reg);
                if (!result)
                {
                    this.MsgBox.MessageBoxShow("E084", value);
                    this.isInputError = true;
                }
                else
                {
                    ctrl.Value = value.Substring(0, 2) + ":" + value.Substring(2, 2);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }

        #region 連携処理

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        internal List<mapDtoList> createMapboxDto()
        {
            try
            {
                int layerId = 1;


                List<mapDtoList> dtoLists = new List<mapDtoList>();

                // レイヤー追加
                mapDtoList dtoList = new mapDtoList();
                dtoList.layerId = layerId;

                List<mapDto> dtos = new List<mapDto>();

                // 出発業者のみ、または出発業者と出発現場をDTOにセット
                mapDto dto = this.GetMapboxForShuppatsuData(this.form.SHUPPATSU_GYOUSHA_CD.Text, this.form.SHUPPATSU_GENBA_CD.Text);
                if (dto != null)
                {
                    dto.id = 1;
                    dto.number = 0;
                    dto.rowNo = 0;
                    dto.roundNo = 0;
                    dto.hinmei = string.Empty;
                    dto.genbaChaku = string.Empty;
                    dto.shuppatsuFlag = true;
                    dtos.Add(dto);
                }

                // コース明細をDTOにセット
                for (int i = 0; i < this.form.DetailIchiran.Rows.Count; i++)
                {
                    if (this.form.DetailIchiran.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(this.form.DetailIchiran.Rows[i].Cells["GYOUSHA_CD"].Value)) ||
                        string.IsNullOrEmpty(Convert.ToString(this.form.DetailIchiran.Rows[i].Cells["GENBA_CD"].Value.ToString())))
                    {
                        continue;
                    }

                    mapDto dtoCourse = this.GetMapboxForShuppatsuData(this.form.DetailIchiran.Rows[i].Cells["GYOUSHA_CD"].Value.ToString()
                                                                , this.form.DetailIchiran.Rows[i].Cells["GENBA_CD"].Value.ToString());
                    if (dtoCourse != null)
                    {
                        dtoCourse.id = dtos.Count + 1;
                        dtoCourse.number = Convert.ToInt32(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.NO].Value);
                        dtoCourse.rowNo = Convert.ToInt32(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.JUNNBANN].Value);
                        dtoCourse.roundNo = Convert.ToInt32(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.ROUND_NO].Value);
                        dtoCourse.hinmei = Convert.ToString(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.HINMEI_INFO].Value);
                        dtoCourse.genbaChaku = Convert.ToString(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.KIBOU_TIME].Value);
                        dtoCourse.shuppatsuFlag = false;
                        dtos.Add(dtoCourse);
                        dtoList.dtos = dtos;
                    }

                }
                if (dtoList.dtos != null)
                {
                    if (dtoList.dtos.Count != 0)
                    {
                        dtoLists.Add(dtoList);
                    }
                }
                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.MsgBox.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Mapbox用の出発情報を取得する。
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        private mapDto GetMapboxForShuppatsuData(string gyoushaCd, string genbaCd)
        {
            mapDto dto = null;

            // 出発業者のみの場合
            if (!string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
            {
                string sql = string.Empty;
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat(" SELECT ");
                sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);
                sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstCls.TODOUFUKEN_NAME);
                sb.AppendFormat(",GYO.GYOUSHA_ADDRESS1 AS {0} ", ConstCls.GYOUSHA_ADDRESS1);
                sb.AppendFormat(",GYO.GYOUSHA_ADDRESS2 AS {0} ", ConstCls.GYOUSHA_ADDRESS2);
                sb.AppendFormat(",GYO.GYOUSHA_LATITUDE AS {0} ", ConstCls.GYOUSHA_LATITUDE);
                sb.AppendFormat(",GYO.GYOUSHA_LONGITUDE AS {0} ", ConstCls.GYOUSHA_LONGITUDE);
                sb.AppendFormat(",GYO.GYOUSHA_POST AS {0} ", ConstCls.GYOUSHA_POST);
                sb.AppendFormat(",GYO.GYOUSHA_TEL AS {0} ", ConstCls.GYOUSHA_TEL);
                sb.AppendFormat(",GYO.BIKOU1 AS {0} ", ConstCls.BIKOU1);
                sb.AppendFormat(",GYO.BIKOU2 AS {0} ", ConstCls.BIKOU2);
                sb.AppendFormat(" FROM M_GYOUSHA AS GYO ");
                sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                sb.AppendFormat(" WHERE GYO.DELETE_FLG = 0 ");
                sb.AppendFormat(" AND GYO.GYOUSHA_CD = '{0}'", gyoushaCd);

                DataTable dt = this.genbaDao.GetDateForStringSql(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                    dto = new mapDto();
                    dto.layerNo = 1;
                    dto.courseName = this.form.COURSE_NAME_RYAKU.Text;
                    if (string.IsNullOrEmpty(this.form.DAY_CD.Text))
                    {
                        dto.dayName = mapLogic.SetDayNameByDate(this.form.SAGYOU_DATE.Text);
                    }
                    else
                    {
                        dto.dayName = mapLogic.SetDayNameByCd(this.form.DAY_CD.Text);
                    }
                    dto.teikiHaishaNo = this.form.TEIKI_HAISHA_NUMBER.Text;
                    dto.torihikisakiCd = string.Empty;
                    dto.torihikisakiName = string.Empty;
                    dto.gyoushaCd = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_CD]);
                    dto.gyoushaName = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_NAME_RYAKU]);
                    dto.genbaCd = string.Empty;
                    dto.genbaName = string.Empty;
                    dto.post = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_POST]);
                    dto.address = Convert.ToString(dt.Rows[0][ConstCls.TODOUFUKEN_NAME])
                                + Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_ADDRESS1])
                                + Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_ADDRESS2]);
                    dto.tel = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_TEL]);
                    dto.bikou1 = Convert.ToString(dt.Rows[0][ConstCls.BIKOU1]);
                    dto.bikou2 = Convert.ToString(dt.Rows[0][ConstCls.BIKOU2]);
                    dto.latitude = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_LATITUDE]);
                    dto.longitude = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_LONGITUDE]);
                }
            }
            // 出発業者と出発現場の場合
            else if (!string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
            {
                string sql = string.Empty;
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat(" SELECT ");
                sb.AppendFormat(" GEN.GYOUSHA_CD AS {0} ", ConstCls.GYOUSHA_CD);
                sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstCls.GYOUSHA_NAME_RYAKU);
                sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstCls.GENBA_CD);
                sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstCls.GENBA_NAME_RYAKU);
                sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstCls.TODOUFUKEN_NAME);
                sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", ConstCls.GENBA_ADDRESS1);
                sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", ConstCls.GENBA_ADDRESS2);
                sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstCls.GENBA_LATITUDE);
                sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstCls.GENBA_LONGITUDE);
                sb.AppendFormat(",GEN.GENBA_POST AS {0} ", ConstCls.GENBA_POST);
                sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", ConstCls.GENBA_TEL);
                sb.AppendFormat(",GEN.BIKOU1 AS {0} ", ConstCls.BIKOU1);
                sb.AppendFormat(",GEN.BIKOU2 AS {0} ", ConstCls.BIKOU2);
                sb.AppendFormat(" FROM M_GENBA AS GEN ");
                sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON GEN.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                sb.AppendFormat(" WHERE GEN.DELETE_FLG = 0 ");
                sb.AppendFormat(" AND GEN.GYOUSHA_CD = '{0}'", gyoushaCd);
                sb.AppendFormat(" AND GEN.GENBA_CD = '{0}'", genbaCd);

                DataTable dt = this.genbaDao.GetDateForStringSql(sb.ToString());
                if (dt.Rows.Count > 0)
                {
                    MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                    dto = new mapDto();
                    dto.layerNo = 1;
                    dto.courseName = this.form.COURSE_NAME_RYAKU.Text;
                    if (string.IsNullOrEmpty(this.form.DAY_CD.Text))
                    {
                        dto.dayName = mapLogic.SetDayNameByDate(this.form.SAGYOU_DATE.Text);
                    }
                    else
                    {
                        dto.dayName = mapLogic.SetDayNameByCd(this.form.DAY_CD.Text);
                    }
                    dto.teikiHaishaNo = this.form.TEIKI_HAISHA_NUMBER.Text;
                    dto.torihikisakiCd = string.Empty;
                    dto.torihikisakiName = string.Empty;
                    dto.gyoushaCd = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_CD]);
                    dto.gyoushaName = Convert.ToString(dt.Rows[0][ConstCls.GYOUSHA_NAME_RYAKU]);
                    dto.genbaCd = Convert.ToString(dt.Rows[0][ConstCls.GENBA_CD]);
                    dto.genbaName = Convert.ToString(dt.Rows[0][ConstCls.GENBA_NAME_RYAKU]);
                    dto.post = Convert.ToString(dt.Rows[0][ConstCls.GENBA_POST]);
                    dto.address = Convert.ToString(dt.Rows[0][ConstCls.TODOUFUKEN_NAME])
                                + Convert.ToString(dt.Rows[0][ConstCls.GENBA_ADDRESS1])
                                + Convert.ToString(dt.Rows[0][ConstCls.GENBA_ADDRESS2]);
                    dto.tel = Convert.ToString(dt.Rows[0][ConstCls.GENBA_TEL]);
                    dto.bikou1 = Convert.ToString(dt.Rows[0][ConstCls.BIKOU1]);
                    dto.bikou2 = Convert.ToString(dt.Rows[0][ConstCls.BIKOU2]);
                    dto.latitude = Convert.ToString(dt.Rows[0][ConstCls.GENBA_LATITUDE]);
                    dto.longitude = Convert.ToString(dt.Rows[0][ConstCls.GENBA_LONGITUDE]);
                }
            }

            return dto;

        }

        #endregion

        /////////////////////////////////////
        //モバイル連携
        /////////////////////////////////////
        #region モバイル登録事前チェック
        /// <summary>
        /// モバイル登録チェック
        /// ※モバイルオプションかつ、作業日＝当日が前提条件
        /// 　かつ、モバイル連携可能条件を満たしている事
        /// </summary>
        /// <returns></returns>
        public bool MobileRegistCheck_pre()
        {
            int Mobile_flg = 0;
            string CheckGyousha = string.Empty;
            
            //モバイル連携チェック用
            //モバイル連携ON、削除OFFのデータを対象
            for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
            {
                if (!(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.DELETE_FLG].Value != null && bool.Parse(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.DELETE_FLG].Value.ToString())))
                {
                    if (this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.MOBILE_RENKEI].Value != null && bool.Parse(this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.MOBILE_RENKEI].Value.ToString()))
                    {
                        Mobile_flg = Mobile_flg + 1;
                    }
                    if (string.IsNullOrEmpty(CheckGyousha))
                    {
                        CheckGyousha = "'" + this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString() + "'";
                    }
                    else
                    {
                        CheckGyousha = CheckGyousha + ",'" + this.form.DetailIchiran.Rows[i].Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString() + "'";
                    }
                }
            }

            //モバイル連携がある時だけチェック
            if (Mobile_flg >= 1)
            {
                //作業日チェック
                if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    return false;
                }
                else
                {
                    if (!(DateTime.Parse(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd"))))
                    {
                        return false;
                    }
                }
                //運転者CDチェック
                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    return false;
                }
                //車輌CDチェック
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    return false;
                }
                //車種CDチェック
                if (string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    return false;
                }
                //車種名チェック
                if (string.IsNullOrEmpty(this.form.SHASHU_NAME_RYAKU.Text))
                {
                    return false;
                }

                //業者マスタの取引区分有無のチェック用
                //(削除ONのデータは対象外)
                DataTable Table = teikiHaishaEntryDao.GetTeikiHaishaTorihikisakiUmuall(CheckGyousha);
                if (Table.Rows.Count > 0)
                {
                    return false;
                }            
            }
            return true;

        }
        #endregion モバイル登録事前チェック

        #region モバイル登録
        /// <summary>
        /// モバイル登録
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public bool MobileRegist()
        {
            bool MobileRegistC = false;

            //リトライは5回まで
            for (MobileTryTime = 1; MobileTryTime <= 5; MobileTryTime++)
            {
                try
                {
                    if (!this.CreateMobileEntity())
                    {
                        //エラーならリトライ
                        continue;
                    }
                    using (Transaction tran = new Transaction())
                    {
                        // モバイル将軍業務TBLテーブル登録
                        foreach (T_MOBISYO_RT detail in this.entitysMobisyoRtList)
                        {
                            this.TmobisyoRtDao.Insert(detail);
                        }
                        //定期配車は、コンテナ無し
                        // モバイル将軍業務詳細TBLテーブル登録           
                        foreach (T_MOBISYO_RT_DTL detail in this.entitysMobisyoRtDTLList)
                        {
                            this.TmobisyoRtDTLDao.Insert(detail);
                        }
                        // モバイル将軍業務搬入TBL テーブル登録           
                        foreach (T_MOBISYO_RT_HANNYUU detail in this.entitysMobisyoRtHNList)
                        {
                            this.TmobisyoRtHNDao.Insert(detail);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }
                    return true;
                }
                catch (NotSingleRowUpdatedRuntimeException ex1)
                {
                    //該当データは他ユーザーにより更新されています
                    LogUtility.Error("MobileRegist", ex1);
                }
                catch (SQLRuntimeException ex2)
                {
                    //データの登録または検索に失敗しました
                    LogUtility.Error("MobileRegist", ex2);
                }
                catch (Exception ex)
                {
                    //予期しないエラーが発生しました。
                    LogUtility.Error("MobileRegist", ex);
                }
            }
            return MobileRegistC;
        }
        #endregion モバイル登録

        #region モバイル登録チェック
        /// <summary>
        /// モバイル登録チェック
        /// ※モバイルオプションかつ、作業日＝当日が前提条件
        /// 　かつ、モバイル連携可能条件を満たしている事
        /// </summary>
        /// <returns></returns>
        public bool MobileRegistCheck()
        {
            bool mobileRegist = false;

            if (is_mobile)
            {
                if (this.Renkei_DETAIL_SYSTEM_ID.Count <= 0)
                {
                    //どの明細にもチェックが付いてなかったら対象外
                    return false;
                }

                //取引先有無のチェックを行う
                DataTable Table = teikiHaishaEntryDao.GetTeikiHaishaTorihikisakiUmu(SqlInt64.Parse(this.Renkei_TeikiHaishaNumber.ToString()));
                if (Table.Rows.Count > 0)
                {
                    this.MsgBox.MessageBoxShow("モバイル将軍への連携が可能なデータではなかった為連携を実行出来ませんでした。");
                    return false;
                }

                this.searchDto = new DTOClass();
                this.searchDto.RenkeiTeikiHaishaNumber = this.Renkei_TeikiHaishaNumber;
                this.searchDto.SAGYOU_DATE_FROM = DateTime.Now.ToString("yyyy/MM/dd");
                this.searchDto.SAGYOU_DATE_TO = DateTime.Now.ToString("yyyy/MM/dd");
                this.ResultTable = new DataTable();
                this.ResultTable = this.teikiHaishaEntryDao.GetDataToMRDataTable(this.searchDto);

                if (0 < this.ResultTable.Rows.Count)
                {
                    DialogResult dr = new DialogResult();
                    dr = this.MsgBox.MessageBoxShow("C110");
                    if (dr == DialogResult.OK || dr == DialogResult.Yes)
                    {
                        mobileRegist = true;
                    }
                }
                else
                {
                    this.MsgBox.MessageBoxShow("モバイル将軍への連携が可能なデータではなかった為連携を実行出来ませんでした。");
                }
            }

            return mobileRegist;
        }
        #endregion モバイル登録チェック
        
        #region モバイル採番
        /// <summary>
        /// シーケンシャルナンバー採番処理
        /// Entry.SYSTEM_IDとDetail.DETAIL_SYSTEM_IDを通版と考え、
        /// 最新のID + 1の値を返す
        /// </summary>
        /// <returns>採番した数値</returns>
        public SqlInt64 CreateMobileSeqNo()
        {
            SqlInt64 returnInt = 1;

            var entity = new S_NUMBER_SYSTEM();
            entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode();

            // IS_NUMBER_SYSTEMDao(共通)
            IS_NUMBER_SYSTEMDao numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();

            var updateEntity = numberSystemDao.GetNumberSystemData(entity);
            returnInt = numberSystemDao.GetMaxPlusKey(entity);

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_SYSTEM();
                updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.MOBILE_RENKEI.GetHashCode();
                updateEntity.CURRENT_NUMBER = returnInt;
                updateEntity.DELETE_FLG = false;
                var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                dataBinderEntry.SetSystemProperty(updateEntity, false);

                numberSystemDao.Insert(updateEntity);
            }
            else
            {
                updateEntity.CURRENT_NUMBER = returnInt;
                numberSystemDao.Update(updateEntity);
            }

            return returnInt;
        }
        #endregion モバイル採番

        #region モバイルentity作成
        /// <summary>
        /// データチェックした時に取得した情報からEntityを作成する
        /// </summary>
        /// <param name="isDelete">True削除:False登録</param>
        /// <returns></returns>
        public bool CreateMobileEntity()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.entitysMobisyoRtList = new List<T_MOBISYO_RT>();
                this.entitysMobisyoRtDTLList = new List<T_MOBISYO_RT_DTL>();
                this.entitysMobisyoRtHNList = new List<T_MOBISYO_RT_HANNYUU>();
                int ZenHaishaNo = -1;
                int HaishaNo;
                int ZenHaishaRowNo = -1;
                int HaishaRowNo;

                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    //モバイル連携にチェックが付いてた明細だけ処理
                    if (this.Renkei_DETAIL_SYSTEM_ID.Contains(long.Parse(tableRow["DETAIL_SYSTEM_ID"].ToString())))
                    {
                        // 定期配車番号
                        HaishaNo = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                        // 定期配車行番号
                        HaishaRowNo = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                        #region T_MOBISYO_RT
                        // 定期配車番号行番号をカウント作成
                        if (ZenHaishaNo != HaishaNo || ZenHaishaRowNo != HaishaRowNo)
                        {
                            // entitys作成
                            this.entitysMobisyoRt = new T_MOBISYO_RT();
                            // シーケンシャルナンバー
                            this.entitysMobisyoRt.SEQ_NO = this.CreateMobileSeqNo();

                            // 車種CD
                            if (!string.IsNullOrEmpty(tableRow["SHASHU_CD"].ToString()))
                            {
                                this.entitysMobisyoRt.SHASHU_CD = tableRow["SHASHU_CD"].ToString();
                            }
                            // 車種名
                            if (!string.IsNullOrEmpty(tableRow["SHASHU_NAME"].ToString()))
                            {
                                this.entitysMobisyoRt.SHASHU_NAME = tableRow["SHASHU_NAME"].ToString();
                            }
                            // 車輌CD
                            if (!string.IsNullOrEmpty(tableRow["SHARYOU_CD"].ToString()))
                            {
                                this.entitysMobisyoRt.SHARYOU_CD = tableRow["SHARYOU_CD"].ToString();
                            }
                            // 車輌名
                            if (!string.IsNullOrEmpty(tableRow["SHARYOU_NAME"].ToString()))
                            {
                                this.entitysMobisyoRt.SHARYOU_NAME = tableRow["SHARYOU_NAME"].ToString();
                            }
                            // 運転者名
                            if (!string.IsNullOrEmpty(tableRow["UNTENSHA_NAME"].ToString()))
                            {
                                this.entitysMobisyoRt.UNTENSHA_NAME = tableRow["UNTENSHA_NAME"].ToString();
                            }
                            // 運転者名CD
                            if (!string.IsNullOrEmpty(tableRow["UNTENSHA_CD"].ToString()))
                            {
                                this.entitysMobisyoRt.UNTENSHA_CD = tableRow["UNTENSHA_CD"].ToString();
                            }
                            // (配車)作業日
                            if (!SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).IsNull)
                            {
                                this.entitysMobisyoRt.HAISHA_SAGYOU_DATE = SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString());
                            }
                            // (配車)伝票番号
                            this.entitysMobisyoRt.HAISHA_DENPYOU_NO = SqlInt64.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                            // (配車)コース名称CD
                            if (!string.IsNullOrEmpty(tableRow["HAISHA_COURSE_NAME_CD"].ToString()))
                            {
                                this.entitysMobisyoRt.HAISHA_COURSE_NAME_CD = tableRow["HAISHA_COURSE_NAME_CD"].ToString();
                            }
                            // (配車)コース名称
                            if (!string.IsNullOrEmpty(tableRow["HAISHA_COURSE_NAME"].ToString()))
                            {
                                this.entitysMobisyoRt.HAISHA_COURSE_NAME = tableRow["HAISHA_COURSE_NAME"].ToString();
                            }
                            // (配車)配車区分 0
                            this.entitysMobisyoRt.HAISHA_KBN = 0;
                            // 登録日時 Insertした日次
                            this.entitysMobisyoRt.GENBA_JISSEKI_CREATEDATE = parentForm.sysDate;
                            // 修正日時 Insertした日次
                            this.entitysMobisyoRt.GENBA_JISSEKI_UPDATEDATE = parentForm.sysDate;
                            // 業者CD
                            if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_GYOUSHACD"].ToString()))
                            {
                                this.entitysMobisyoRt.GENBA_JISSEKI_GYOUSHACD = tableRow["GENBA_JISSEKI_GYOUSHACD"].ToString();
                            }
                            // 現場CD
                            if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_GENBACD"].ToString()))
                            {
                                this.entitysMobisyoRt.GENBA_JISSEKI_GENBACD = tableRow["GENBA_JISSEKI_GENBACD"].ToString();
                            }
                            // 追加現場フラグ 基本的には0。データを登録するとき、作業日＝当日の場合、1
                            if (!SqlDateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).IsNull &&
                                (DateTime.Parse(tableRow["HAISHA_SAGYOU_DATE"].ToString()).ToString("yyyy/MM/dd") == (parentForm.sysDate).ToString("yyyy/MM/dd")))
                            {
                                this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = true;
                            }
                            else
                            {
                                this.entitysMobisyoRt.GENBA_JISSEKI_ADDGENBAFLG = false;
                            }
                            // 指示確認フラグ 0
                            this.entitysMobisyoRt.SHIJI_FLG = false;
                            // 除外フラグ 0
                            this.entitysMobisyoRt.GENBA_JISSEKI_JYOGAIFLG = false;
                            // マニフェスト区分 0
                            this.entitysMobisyoRt.GENBA_DETAIL_MANIKBN = 0;
                            // ステータス
                            this.entitysMobisyoRt.GENBA_STTS = "0";
                            // 実績登録フラグ
                            this.entitysMobisyoRt.JISSEKI_REGIST_FLG = false;
                            // 運搬業者CD
                            if (!string.IsNullOrEmpty(tableRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString()))
                            {
                                this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = tableRow["GENBA_JISSEKI_UPNGYOSHACD"].ToString();
                            }
                            else
                            {
                                this.entitysMobisyoRt.GENBA_JISSEKI_UPNGYOSHACD = string.Empty;
                            }
                            // (配車)行番号
                            this.entitysMobisyoRt.HAISHA_ROW_NUMBER = SqlInt32.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                            // 削除フラグ
                            this.entitysMobisyoRt.DELETE_FLG = false;

                            // 20170601 wangjm モバイル将軍#105481 start
                            this.entitysMobisyoRt.KAISHU_NO = SqlInt32.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                            this.entitysMobisyoRt.KAISHU_BIKOU = tableRow["GENBA_MEISAI_BIKOU"].ToString();
                            // 20170601 wangjm モバイル将軍#105481 end

                            // 自動設定
                            var dataBinderContenaResult = new DataBinderLogic<T_MOBISYO_RT>(this.entitysMobisyoRt);
                            dataBinderContenaResult.SetSystemProperty(this.entitysMobisyoRt, false);

                            // Listに追加
                            this.entitysMobisyoRtList.Add(this.entitysMobisyoRt);
                        }
                        #endregion

                        // 前回定期配車番号
                        ZenHaishaNo = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                        // 前回定期配車行番号
                        ZenHaishaRowNo = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                    }
                }

                int ZenHaishaNo2 = -1;
                int HaishaNo2;
                int ZenHaishaRowNo2 = -1;
                int HaishaRowNo2;
                string NiorosiNo2;
                int Edaban2 = 0;
                List<NiorosiClass> niorosiList = new List<NiorosiClass>();
                foreach (DataRow tableRow in this.ResultTable.Rows)
                {
                    //モバイル連携にチェックが付いてた明細だけ処理
                    if (this.Renkei_DETAIL_SYSTEM_ID.Contains(long.Parse(tableRow["DETAIL_SYSTEM_ID"].ToString())))
                    {
                        // 定期配車番号
                        HaishaNo2 = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                        // 定期配車行番号
                        HaishaRowNo2 = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());
                        // 荷降番号
                        if (string.IsNullOrEmpty(tableRow["NIOROSHI_NUMBER"].ToString()))
                        {
                            NiorosiNo2 = null;
                        }
                        else
                        {
                            NiorosiNo2 = tableRow["NIOROSHI_NUMBER"].ToString();
                        }

                        if (ZenHaishaNo2 != HaishaNo2 || ZenHaishaRowNo2 != HaishaRowNo2)
                        {
                            // 枝番
                            Edaban2 = 0;
                        }

                        if (ZenHaishaNo2 != HaishaNo2 && NiorosiNo2 != null)
                        {
                            niorosiList = new List<NiorosiClass>();
                            DataTable niorosiTable = this.teikiHaishaEntryDao.GetMobilNioroshiData(HaishaNo2, int.Parse(NiorosiNo2));
                            if (niorosiTable != null && niorosiTable.Rows.Count > 0)
                            {
                                foreach (DataRow niorosiRow in niorosiTable.Rows)
                                {
                                    NiorosiClass niorosi = new NiorosiClass();
                                    niorosi.TEIKI_HAISHA_NUMBER = niorosiRow["HAISHA_DENPYOU_NO"].ToString();
                                    niorosi.NIOROSHI_NUMBER = niorosiRow["NIOROSHI_NUMBER"].ToString();
                                    niorosi.HANYU_SEQ_NO = SqlInt64.Parse(niorosiRow["HANYU_SEQ_NO"].ToString());
                                    niorosiList.Add(niorosi);
                                }
                            }

                        }
                        // 前回定期配車番号
                        ZenHaishaNo2 = int.Parse(tableRow["HAISHA_DENPYOU_NO"].ToString());
                        // 前回定期配車行番号
                        ZenHaishaRowNo2 = int.Parse(tableRow["HAISHA_ROW_NUMBER"].ToString());

                        // 品名なしの場合、T_MOBISYO_RT_DTLデータを作成しない。
                        if (string.IsNullOrEmpty(tableRow["GENBA_DETAIL_HINMEICD"].ToString()))
                        {
                            continue;
                        }

                        #region T_MOBISYO_RT_DTL
                        // 枝番
                        Edaban2++;

                        // entitys作成
                        this.entitysMobisyoRtDTL = new T_MOBISYO_RT_DTL();
                        // シーケンシャルナンバー

                        List<T_MOBISYO_RT> data = (from temp in entitysMobisyoRtList
                                                    where temp.HAISHA_DENPYOU_NO.ToString().Equals(HaishaNo2.ToString()) &&
                                                            temp.HAISHA_ROW_NUMBER.ToString().Equals(HaishaRowNo2.ToString())
                                                    select temp).ToList();
                        this.entitysMobisyoRtDTL.SEQ_NO = data[0].SEQ_NO;
                        List<NiorosiClass> niorosiData = null;
                        if (NiorosiNo2 != null)
                        {
                            niorosiData = (from temp in niorosiList
                                            where temp.TEIKI_HAISHA_NUMBER.ToString().Equals(HaishaNo2.ToString()) &&
                                            temp.NIOROSHI_NUMBER.ToString().Equals(NiorosiNo2.ToString())
                                            select temp).ToList();
                        }
                        // 搬入シーケンシャルナンバー
                        if (niorosiData != null && niorosiData.Count > 0 && NiorosiNo2 != null)
                        {
                            this.entitysMobisyoRtDTL.HANYU_SEQ_NO = niorosiData[0].HANYU_SEQ_NO;
                        }
                        else
                        {
                            if (NiorosiNo2 != null)
                            {
                                this.entitysMobisyoRtDTL.HANYU_SEQ_NO = this.CreateMobileSeqNo();
                                NiorosiClass niorosi = new NiorosiClass();
                                niorosi.TEIKI_HAISHA_NUMBER = HaishaNo2.ToString();
                                niorosi.NIOROSHI_NUMBER = NiorosiNo2;
                                niorosi.HANYU_SEQ_NO = this.entitysMobisyoRtDTL.HANYU_SEQ_NO;
                                niorosiList.Add(niorosi);

                                #region T_MOBISYO_RT_HANNYUU
                                // entitys作成
                                this.entitysMobisyoRtHN = new T_MOBISYO_RT_HANNYUU();

                                // 搬入シーケンシャルナンバー
                                this.entitysMobisyoRtHN.HANYU_SEQ_NO = this.entitysMobisyoRtDTL.HANYU_SEQ_NO;
                                // 枝番1を設定する
                                this.entitysMobisyoRtHN.EDABAN = 1;

                                SqlInt64 SYSTEM_ID = SqlInt64.Parse(tableRow["SYSTEM_ID"].ToString());
                                SqlInt32 SEQ = SqlInt32.Parse(tableRow["SEQ"].ToString());
                                SqlInt32 NIOROSHI_NUMBER = SqlInt32.Parse(tableRow["NIOROSHI_NUMBER"].ToString());

                                DataTable NioroshiData = this.teikiHaishaEntryDao.GetTeikiHaishaNioroshiData(SYSTEM_ID, SEQ, NIOROSHI_NUMBER);

                                if (NioroshiData.Rows.Count > 0)
                                {
                                    // (搬入)業者CD
                                    if (!string.IsNullOrEmpty(NioroshiData.Rows[0]["HANNYUU_GYOUSHACD"].ToString()))
                                    {
                                        this.entitysMobisyoRtHN.HANNYUU_GYOUSHACD = NioroshiData.Rows[0]["HANNYUU_GYOUSHACD"].ToString();
                                    }

                                    // (搬入)現場CD
                                    if (!string.IsNullOrEmpty(NioroshiData.Rows[0]["HANNYUU_GENBACD"].ToString()))
                                    {
                                        this.entitysMobisyoRtHN.HANNYUU_GENBACD = NioroshiData.Rows[0]["HANNYUU_GENBACD"].ToString();
                                    }
                                }
                                // (搬入)搬入量
                                this.entitysMobisyoRtHN.HANNYUU_RYO = SqlDouble.Null;
                                this.entitysMobisyoRtHN.HANNYUU_JISSEKI_RYO = SqlDouble.Null;
                                // 搬入フラグ
                                this.entitysMobisyoRtHN.JISSEKI_REGIST_FLG = false;
                                // 削除フラグ
                                this.entitysMobisyoRtHN.DELETE_FLG = false;

                                // 自動設定
                                var dataBinderContenaResultHN = new DataBinderLogic<T_MOBISYO_RT_HANNYUU>(this.entitysMobisyoRtHN);
                                dataBinderContenaResultHN.SetSystemProperty(this.entitysMobisyoRtHN, false);

                                // Listに追加
                                this.entitysMobisyoRtHNList.Add(this.entitysMobisyoRtHN);

                                #endregion
                            }
                        }
                                
                        // 枝番
                        this.entitysMobisyoRtDTL.EDABAN = Edaban2;
                        // (現場明細)品名CD
                        if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_HINMEICD"].ToString()))
                        {
                            this.entitysMobisyoRtDTL.GENBA_DETAIL_HINMEICD = tableRow["GENBA_DETAIL_HINMEICD"].ToString();
                        }
                        // (現場明細)単位１
                        if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_UNIT_CD1"].ToString()))
                        {
                            this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD1 = SqlInt16.Parse(tableRow["GENBA_DETAIL_UNIT_CD1"].ToString());
                        }
                        // (現場明細)単位2
                        if (!string.IsNullOrEmpty(tableRow["GENBA_DETAIL_UNIT_CD2"].ToString()))
                        {
                            if (SqlBoolean.Parse(tableRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"].ToString()).IsTrue)
                            {
                                this.entitysMobisyoRtDTL.GENBA_DETAIL_UNIT_CD2 = SqlInt16.Parse(tableRow["GENBA_DETAIL_UNIT_CD2"].ToString());
                            }   
                        }
                        // (現場明細)数量１
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO1 = SqlDouble.Null;
                        // (現場明細)数量２
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_SUURYO2 = SqlDouble.Null;
                        // (現場明細)追加品名フラグ
                        this.entitysMobisyoRtDTL.GENBA_DETAIL_ADDHINMEIFLG = false;
                        // 回収実績フラグ
                        this.entitysMobisyoRtDTL.JISSEKI_REGIST_FLG = false;
                        // 削除フラグ
                        this.entitysMobisyoRtDTL.DELETE_FLG = false;

                        // 自動設定
                        var dataBinderContenaResultDTL = new DataBinderLogic<T_MOBISYO_RT_DTL>(this.entitysMobisyoRtDTL);
                        dataBinderContenaResultDTL.SetSystemProperty(this.entitysMobisyoRtDTL, false);

                        // Listに追加
                        this.entitysMobisyoRtDTLList.Add(this.entitysMobisyoRtDTL);
                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateMobileEntity", ex);

                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
        /// <summary>
        /// INXS受付連携
        /// </summary>
        /// <returns>true:有効、false:無効</returns>
        internal bool RenkeiCheckForConfirmDateInxs()
        {
            bool ret = true;
            try
            {

                if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    return ret;
                }

                List<string> uketsukeNumbers = new List<string>();
                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    DataGridViewRow detailRow = this.form.DetailIchiran.Rows[i];
                    if (detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value != null && bool.Parse(detailRow.Cells[ConstCls.DetailColName.DELETE_FLG].Value.ToString()))
                    {
                        continue;
                    }

                    // 受付番号
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString()))
                    {
                        uketsukeNumbers.Add(detailRow.Cells[ConstCls.DetailColName.UKETSUKE_NUMBER].FormattedValue.ToString());
                    }
                }
                if (!uketsukeNumbers.Any())
                {
                    return ret;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT T_USSE.UKETSUKE_NUMBER ");
                sql.Append(" FROM T_UKETSUKE_SS_ENTRY T_USSE ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_UKETSUKE_SS_INXS AS PRU_SS_INXS ");
                sql.Append(" ON PRU_SS_INXS.UKETSUKE_SYSTEM_ID = T_USSE.SYSTEM_ID ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_INXS ");
                sql.Append(" ON T_PICKUP_REQUEST_INXS.SYSTEM_ID = PRU_SS_INXS.REQUEST_SYSTEM_ID ");
                sql.Append(" AND T_PICKUP_REQUEST_INXS.DELETE_FLG = 0 ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_PREFERENCE_DATE_INXS AS PREFERENCE_DATE ");
                sql.Append(" ON PREFERENCE_DATE.SYSTEM_ID = T_PICKUP_REQUEST_INXS.SYSTEM_ID  ");
                sql.Append(" AND PREFERENCE_DATE.SEQ = T_PICKUP_REQUEST_INXS.SEQ ");
                sql.Append(" AND PREFERENCE_DATE.[ROW_NUMBER] = 10 ");
                sql.AppendFormat(" WHERE T_USSE.UKETSUKE_NUMBER in ({0}) ", string.Join(",", uketsukeNumbers));
                sql.AppendFormat(" AND PREFERENCE_DATE.PREFERENCE_DATE != CONVERT(DATETIME, '{0}', 102) ", DateTime.Parse(this.form.SAGYOU_DATE.Text).ToString("yyyy-MM-dd"));
                sql.Append(" AND T_USSE.DELETE_FLG = 0 ");

                var dt = this.teikiHaishaEntryDao.GetDateForStringSql(sql.ToString());
                // 連携済みの場合はアラートを表示する。
                if (dt != null && dt.Rows.Count > 0)
                {

                    IM_SYS_INFODao sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                    M_SYS_INFO[] sysInfoEntities = sysInfoDao.GetAllData();
                    if (sysInfoEntities == null)
                    {
                        return ret;
                    }

                    StringBuilder errorBuilder = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                    {
                        errorBuilder.AppendFormat("受付番号：{0}", row["UKETSUKE_NUMBER"]);
                        errorBuilder.AppendLine();
                    }

                    short haishaHenkouSagyouDatekbn = 1;
                    if (!sysInfoEntities[0].HAISHA_HENKOU_SAGYOU_DATE_KBN.IsNull)
                    {
                        haishaHenkouSagyouDatekbn = sysInfoEntities[0].HAISHA_HENKOU_SAGYOU_DATE_KBN.Value;
                    }

                    if (haishaHenkouSagyouDatekbn == 1) //Confirm
                    {
                        errorBuilder.Append("は、作業日とINXS依頼情報の確定日が異なりますが、作業日を変更します。よろしいですか？はいの場合は、収集受付担当者へ連絡してください");
                        if (this.MsgBox.MessageBoxShowConfirm(errorBuilder.ToString(), MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                    else //Error
                    {
                        errorBuilder.Append("は、作業日とINXS依頼情報の確定日が異なる為、作業日を変更を変更できません。収集受付担当者へ連絡し、INXS依頼情報の確定日を先に変更してください");

                        this.MsgBox.MessageBoxShowError(errorBuilder.ToString());
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return ret;
        }
        //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END

        #region 画面モードチェック（ｼｮｰﾄﾒｯｾｰｼﾞ）
        /// <summary>
        /// 画面モードチェック（ｼｮｰﾄﾒｯｾｰｼﾞ）
        /// </summary>
        /// <returns>現在の画面モード</returns>
        public void CheckWindowTypeSms(WINDOW_TYPE windowType)
        {
            if (!AppConfig.AppOptions.IsSMS())
            {
                // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン無効化
                parentForm.bt_process4.Text = string.Empty;
                parentForm.bt_process4.Enabled = false;
            }
            else
            {
                parentForm.bt_process4.Text = "[4]ｼｮｰﾄﾒｯｾｰｼﾞ";

                if (windowType == WINDOW_TYPE.NEW_WINDOW_FLAG || windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン活性
                    parentForm.bt_process4.Enabled = true;
                }
                else
                {
                    // ｼｮｰﾄﾒｯｾｰｼﾞ用ボタン非活性
                    parentForm.bt_process4.Enabled = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
        /// </summary>
        /// <returns></returns>
        internal string[] SmsParamListSetting()
        {
            string[] smsparamList = new string[7];

            // 登録・更新時の画面項目と選択行の情報を参照
            smsparamList[0] = "4";
            smsparamList[1] = this.teikiHaishaEntryDao.GetMaxSeq(this.form.TEIKI_HAISHA_NUMBER.Text);
            smsparamList[2] = this.form.TEIKI_HAISHA_NUMBER.Text;
            smsparamList[3] = this.form.DetailIchiran.CurrentRow.Cells["GYOUSHA_CD"].Value.ToString();
            smsparamList[4] = this.form.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value.ToString();
            smsparamList[5] = this.form.SAGYOU_DATE.Text;
            smsparamList[6] = this.form.DetailIchiran.CurrentRow.Cells["ROW_NUMBER"].Value.ToString();
            return smsparamList;
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト取得
        /// </summary>
        /// <returns></returns>
        internal List<int> SmsReceiverListSetting()
        {
            List<int> smsReceiverList = null;
            List<M_SMS_RECEIVER_LINK_GENBA> smsReceiverLink = null;

            string currentGyoushaCd = this.form.DetailIchiran.CurrentRow.Cells["GYOUSHA_CD"].Value.ToString();
            string currentGenbaCd = this.form.DetailIchiran.CurrentRow.Cells["GENBA_CD"].Value.ToString();

            // 選択行の情報を参照
            smsReceiverLink = this.smsReceiverLinkGenbaDao.GetDataForSmsNyuuryoku(currentGyoushaCd, currentGenbaCd);
            
            if (smsReceiverLink != null)
            {
                smsReceiverList = smsReceiverLink.Select(n => n.SYSTEM_ID.Value).ToList();
            }

            return smsReceiverList;
        }
    }
}
