// $Id: LogicCls.cs 55241 2015-07-09 09:26:57Z nagata $
using System;
using System.Collections;
using System.Collections.Generic;
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
using r_framework.Dao;
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
using Shougun.Core.Master.CourseNyuryoku.APP;
using Shougun.Core.Master.CourseNyuryoku.Const;
using Shougun.Core.Master.CourseNyuryoku.Dao;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.DirectionsAPI;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Master.CourseNyuryoku.Logic
{
    /// <summary>
    /// コンテナ状況画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.CourseNyuryoku.Setting.ButtonSetting.xml";

        private bool isSelect = false;
        public bool isRegist = true;

        /// <summary>
        /// コンテナ状況画面Form
        /// </summary>
        private Shougun.Core.Master.CourseNyuryoku.APP.UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 車輌Dao
        /// </summary>
        private IM_SHARYOUDao sharyouFWDao;

        /// <summary>
        /// 車種Dao
        /// </summary>
        private IM_SHASHUDao shashuFWDao;

        /// <summary>
        /// 社員Dao
        /// </summary>
        private IM_SHAINDao shainFWDao;

        /// <summary>
        /// 業者Dao
        /// </summary>
        private IM_GYOUSHADao gyoushaFWDao;

        /// <summary>
        /// メッセージLogic
        /// </summary>
        private MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 車輌CD前回値
        /// </summary>
        internal string oldSharyouCD;

        /// <summary>
        /// 前回値保存用Dictionary
        /// Kye = コントロール名、Value = 値
        /// </summary>
        internal Dictionary<string, string> oldValueDic = new Dictionary<string, string>();
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region プロパティ

        #region システム情報

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        #endregion

        #region コース_明細

        /// <summary>
        /// コース_明細のDao
        /// </summary>
        internal DaoCls courseDetailDao;

        /// <summary>
        /// コース_明細のエンティティ
        /// </summary>
        private M_COURSE_DETAIL[] courseDetailEntitys;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable courseDetail { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable courseDetailSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_COURSE_DETAIL courseDetailSearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtDetailList = new DataTable();

        #endregion

        #region コース_明細内訳

        /// <summary>
        /// コース_明細のDao
        /// </summary>
        private M_COURSE_DETAIL_ITEMS_DaoCls courseDetailItemsDao;

        /// <summary>
        /// コース_明細のエンティティ
        /// </summary>
        //private M_COURSE_DETAIL_ITEMS[] courseDetailItemsEntitys;
        private M_COURSE_DETAIL_ITEMS_add_DELETE_FLG[] courseDetailItemsEntitys;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable courseDetailItemsSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_COURSE_DETAIL_ITEMS courseDetailItemsSearchString { get; set; }

        /// <summary>
        /// dtDetailItemsList
        /// </summary>
        public DataTable dtDetailItemsList = new DataTable();

        #endregion

        #region コース_荷降先

        /// <summary>
        /// コース_荷降先のDao
        /// </summary>
        private M_COURSE_NIOROSHI_DaoCls courseNioroshiDao;

        /// <summary>
        /// コース_荷降先のエンティティ
        /// </summary>
        private M_COURSE_NIOROSHI[] courseNioroshiEntitys;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable courseNioroshiSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_COURSE_NIOROSHI courseNioroshiSearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtCourseNioroshiDetailList = new DataTable();

        #endregion

        #region コースマスタ

        /// <summary>
        /// コースマスタのDao
        /// </summary>
        private M_COURSE_DaoCls courseDao;

        /// <summary>
        /// コースマスタのエンティティ
        /// </summary>
        private M_COURSE[] courseEntitys;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable courseSearchResult { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable fukushaCourseSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_COURSE courseSearchString { get; set; }

        //public M_COURSE_NAME courseNameSearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtCourseDetailList = new DataTable();

        /// <summary>
        /// コース一覧「F5」から画面を開くかどうかのフラッグ
        /// </summary>
        public Boolean FromCourseIchiran = false;

        #endregion

        #region 現場_定期品名

        /// <summary>
        /// 現場_定期品名のDao
        /// </summary>
        private M_GENBA_TEIKI_HINMEI_DaoCls genbaTeikiHinmeiDao;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable genbaTeikiHinmeiSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_GENBA_TEIKI_HINMEI genbaTeikiHinmeiSearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtgenbaTeikiHinmeiDetailList = new DataTable();

        #endregion

        #region 業者マスタ

        /// <summary>
        /// 業者マスタのDao
        /// </summary>
        private M_GYOUSHA_DaoCls gyoushaDao;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable GyoushaSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_GYOUSHA gyoushaSearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtGyoushaDetailList = new DataTable();

        #endregion

        #region 現場マスタ

        /// <summary>現場取得区分検索Dao</summary>
        private GetGenbaDaoCls GetGenbaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        internal IM_GENBADao genbaDao;

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_GENBA genbaSearchString { get; set; }

        /// <summary>地図表示用Dao</summary>
        private GetMapGenbaDaoCls GetMapGenbaDao;

        #endregion

        #region コース名称マスタ

        /// <summary>
        /// コース名称マスタのDao
        /// </summary>
        private M_COURSE_NAME_DaoCls courseNameDao;

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable courseNameSearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_COURSE_NAME courseNameSearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtCourseNameDetailList = new DataTable();

        #endregion

        // ---20140122 oonaka add 拠点CDをXMLから取得 start ---

        #region 拠点マスタ

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        private r_framework.Dao.IM_KYOTENDao kyotenDao;

        #endregion

        // ---20140122 oonaka add 拠点CDをXMLから取得 end ---

        #region 更新履歴
        /// <summary>
        /// 更新履歴コースのDao
        /// </summary>
        internal ICHANGE_LOG_M_COURSEDao changeCourseDao;

        /// <summary>
        /// 更新履歴コース_明細のDao
        /// </summary>
        internal ICHANGE_LOG_M_COURSE_DETAILDao changeCourseDetailDao;

        /// <summary>
        /// 更新履歴コース_明細内訳のDao
        /// </summary>
        internal ICHANGE_LOG_M_COURSE_DETAIL_ITEMSDao changeCourseDetailItemsDao;

        /// <summary>
        /// 更新履歴コース_荷降先のDao
        /// </summary>
        internal ICHANGE_LOG_M_COURSE_NIOROSHIDao changeCourseNioroshiDao;

        /// <summary>
        /// システムID採番Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// 更新履歴コースのエンティティ
        /// </summary>
        private List<CHANGE_LOG_M_COURSE> changeCourseEntitys;

        /// <summary>
        /// 更新履歴コース_明細のエンティティ
        /// </summary>
        private List<CHANGE_LOG_M_COURSE_DETAIL> changeCourseDetailEntitys;

        /// <summary>
        /// 更新履歴コース_明細内訳のエンティティ
        /// </summary>
        private List<CHANGE_LOG_M_COURSE_DETAIL_ITEMS> changeCourseDetailItemsEntitys;

        /// <summary>
        /// 更新履歴コース_荷降先のエンティティ
        /// </summary>
        private List<CHANGE_LOG_M_COURSE_NIOROSHI> changeCourseNioroshiEntitys;

        #endregion

        #endregion

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(Shougun.Core.Master.CourseNyuryoku.APP.UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.courseDetailDao = DaoInitUtility.GetComponent<DaoCls>();
            this.courseNioroshiDao = DaoInitUtility.GetComponent<M_COURSE_NIOROSHI_DaoCls>();
            this.courseDao = DaoInitUtility.GetComponent<M_COURSE_DaoCls>();
            this.courseNameDao = DaoInitUtility.GetComponent<M_COURSE_NAME_DaoCls>();
            this.courseDetailItemsDao = DaoInitUtility.GetComponent<M_COURSE_DETAIL_ITEMS_DaoCls>();
            this.genbaTeikiHinmeiDao = DaoInitUtility.GetComponent<M_GENBA_TEIKI_HINMEI_DaoCls>();
            this.gyoushaDao = DaoInitUtility.GetComponent<M_GYOUSHA_DaoCls>();
            this.GetGenbaDao = DaoInitUtility.GetComponent<GetGenbaDaoCls>();
            this.GetMapGenbaDao = DaoInitUtility.GetComponent<GetMapGenbaDaoCls>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            this.msgLogic = new MessageBoxShowLogic();
            this.shashuFWDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
            this.sharyouFWDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.shainFWDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.gyoushaFWDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.oldSharyouCD = string.Empty;

            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao

            this.changeCourseDao = DaoInitUtility.GetComponent<ICHANGE_LOG_M_COURSEDao>();
            this.changeCourseDetailDao = DaoInitUtility.GetComponent<ICHANGE_LOG_M_COURSE_DETAILDao>();
            this.changeCourseDetailItemsDao = DaoInitUtility.GetComponent<ICHANGE_LOG_M_COURSE_DETAIL_ITEMSDao>();
            this.changeCourseNioroshiDao = DaoInitUtility.GetComponent<ICHANGE_LOG_M_COURSE_NIOROSHIDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            LogUtility.DebugMethodEnd();
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.allControl = this.form.allControl;

                // イベントの初期化処理
                this.EventInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // 親フォームオブジェクト取得
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // システム情報を取得し、初期値をセットする
                this.GetSysInfoInit();

                // ---20140122 oonaka add 前回保存値のセット start ---
                this.SetZenkaiHozonSettei();
                // ---20140122 oonaka add 前回保存値のセット end ---

                // 検索項目を初期値にセットする
                this.form.customTextBoxCoureseName.Text = "";
                this.form.customTextBoxCoureseName.DBFieldsName = "";
                this.form.customTextBoxCoureseName.ItemDefinedTypes = "";
                this.form.customTextBoxCoureseNameCd.Text = "";
                this.form.SHASHU_CD.Text = "";
                this.form.SHASHU_NAME.Text = "";
                this.form.SHARYOU_CD.Text = "";
                this.form.SHARYOU_NAME.Text = "";
                this.form.UNTENSHA_CD.Text = "";
                this.form.UNTENSHA_NAME.Text = "";
                this.form.UNPAN_GYOUSHA_CD.Text = "";
                this.form.UNPAN_GYOUSHA_NAME.Text = "";
                this.form.KUMIKOMI_ROUND_NO.Text = "1";

                // 作業開始時間_時
                this.form.SAGYOU_BEGIN_HOUR.Text = string.Empty;
                // 作業開始時間_分
                this.form.SAGYOU_BEGIN_MINUTE.Text = string.Empty;
                // 作業終了時間_時
                this.form.SAGYOU_END_HOUR.Text = string.Empty;
                // 作業終了時間_分
                this.form.SAGYOU_END_MINUTE.Text = string.Empty;

                this.form.customRadioButton1.Checked = true;

                this.parentForm.bt_func2.Enabled = false;
                this.parentForm.bt_func3.Enabled = false;
                this.parentForm.bt_func4.Enabled = false;
                this.parentForm.bt_func5.Enabled = false;
                this.parentForm.bt_func6.Enabled = false;
                this.parentForm.bt_func8.Enabled = false;
                this.parentForm.bt_func9.Enabled = false;
                this.parentForm.bt_func10.Enabled = false;
                this.parentForm.bt_func11.Enabled = true;
                this.parentForm.bt_func12.Enabled = true;
                this.parentForm.bt_process1.Enabled = true;
                this.parentForm.bt_process2.Enabled = false;
                this.parentForm.bt_process3.Enabled = false;
                this.parentForm.bt_process4.Enabled = false;
                this.parentForm.bt_process5.Enabled = true;

                // オプション非対応
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    // mapbox用ボタン無効化
                    this.parentForm.bt_func10.Text = string.Empty;
                    this.parentForm.bt_func10.Enabled = false;
                }

                // ---20140122 oonaka add 拠点CDをXMLから取得 start ---
                this.SetUserProfile();
                // ---20140122 oonaka add 拠点CDをXMLから取得 end ---
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region ボタン初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン設定の読込

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] rtn;

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            rtn = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            LogUtility.DebugMethodEnd(rtn);
            return rtn;
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            //var parentForm = (BusinessBaseForm)this.form.Parent;

            // 切替ボタン(F1)イベント生成
            this.parentForm.bt_func1.Click += new EventHandler(this.form.dispChange);

            // 行追加ボタン(F2)イベント生成
            this.parentForm.bt_func2.Click += new EventHandler(this.form.RecAdd);

            // 行削除ボタン(F3)イベント生成
            this.parentForm.bt_func3.Click += new EventHandler(this.form.RecDel);

            // 上ボタン(F4)イベント生成
            this.parentForm.bt_func4.Click += new EventHandler(this.form.RecUp);

            // 下ボタン(F5)イベント生成
            this.parentForm.bt_func5.Click += new EventHandler(this.form.RecDown);

            // CSV出力ボタン(F6)イベント生成
            this.parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

            // 一覧ボタン(F7)イベント生成
            this.parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

            // 順番整列ボタン(F8)イベント生成
            this.parentForm.bt_func8.Click += new EventHandler(this.form.Sort);

            // 登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            this.parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            // 地図表示ボタン(F10)イベント生成
            this.parentForm.bt_func10.Click += new EventHandler(this.form.MapOpen);

            // 取消ボタン(F11)イベント生成
            this.parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            // 閉じるボタン(F12)イベント生成
            this.parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            // [1]一括選択(一括解除)ボタン
            this.parentForm.bt_process1.Click += new EventHandler(this.form.SelectedAll);

            // [2]組込ボタン
            this.parentForm.bt_process2.Click += new EventHandler(this.form.Kumikomi);

            // [3]荷降№一括入力ボタン
            this.parentForm.bt_process3.Click += new EventHandler(this.form.IkkatsuInput);

            // [4]荷降行削除ボタン
            this.parentForm.bt_process4.Click += new EventHandler(this.form.DeleteNioroshiRow);

            // [5]再読込ボタン
            this.form.C_Regist(parentForm.bt_process5);
            this.parentForm.bt_process5.Click += new EventHandler(this.form.Search);
            this.parentForm.bt_process5.ProcessKbn = PROCESS_KBN.NEW;

            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new EventHandler(ICHIRAN_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new EventHandler(ICHIRAN_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new EventHandler(ICHIRAN_JOUKEN_CheckedChanged);

            // CDEnterイベント
            this.form.SHARYOU_CD.Enter += new EventHandler(this.SHARYOU_CD_Enter);
            this.form.UNPAN_GYOUSHA_CD.Enter += new EventHandler(this.UNPAN_GYOUSHA_CD_Enter);

            // CDValidatedイベント
            this.form.SHARYOU_CD.Validated += new EventHandler(this.SHARYOU_CD_Validated);
            this.form.UNTENSHA_CD.Validated += new EventHandler(this.UNTENSHA_CD_Validated);
            this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.UNPAN_GYOUSHA_CD_Validated);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            this.form.customTextBoxCoureseName.Text = string.Empty;
            this.form.customTextBoxCoureseName.DBFieldsName = string.Empty;
            this.form.customTextBoxCoureseName.ItemDefinedTypes = string.Empty;
            this.form.customTextBoxCoureseNameCd.Text = string.Empty;
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = false;
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public bool SetSearchString()
        {
            LogUtility.DebugMethodStart();

            SqlInt16 DAY_CD;
            String COURSE_NAME_CD;

            if (this.FromCourseIchiran)
            {
                if (this.form.dayCd.Length > 0)
                {
                    DAY_CD = SqlInt16.Parse(this.form.dayCd);
                }
                else
                {
                    DAY_CD = SqlInt16.Null;
                }

                COURSE_NAME_CD = this.form.courseNameCd;
            }
            else
            {
                if (this.form.customTextBoxDayCd.Text.Length > 0)
                {
                    DAY_CD = SqlInt16.Parse(this.form.customTextBoxDayCd.Text);
                }
                else
                {
                    DAY_CD = SqlInt16.Null;
                }

                COURSE_NAME_CD = this.form.customTextBoxCoureseNameCd.Text;
            }

            BusinessBaseForm parentForm = (BusinessBaseForm)this.form.Parent;
            SqlInt16 KYOTEN_CD;
            if (((HeaderForm)parentForm.headerForm).KYOTEN_CD.Text.Length > 0)
            {
                KYOTEN_CD = SqlInt16.Parse(((HeaderForm)parentForm.headerForm).KYOTEN_CD.Text);
            }
            else
            {
                KYOTEN_CD = SqlInt16.Null;
            }

            // M_COURSE
            M_COURSE courseEntity = new M_COURSE();
            courseEntity.DAY_CD = DAY_CD;
            courseEntity.COURSE_NAME_CD = COURSE_NAME_CD;
            this.courseSearchString = courseEntity;

            M_COURSE_NAME courseNameEntity = new M_COURSE_NAME();
            courseNameEntity.KYOTEN_CD = KYOTEN_CD;

            this.courseNameSearchString = courseNameEntity;

            // M_COURSE_NIOROSHI
            M_COURSE_NIOROSHI courseNioroshiEntity = new M_COURSE_NIOROSHI();
            courseNioroshiEntity.DAY_CD = DAY_CD;
            courseNioroshiEntity.COURSE_NAME_CD = COURSE_NAME_CD;
            this.courseNioroshiSearchString = courseNioroshiEntity;

            // M_COURSE_DETAIL
            M_COURSE_DETAIL courseDetailEntity = new M_COURSE_DETAIL();
            courseDetailEntity.DAY_CD = DAY_CD;
            courseDetailEntity.COURSE_NAME_CD = COURSE_NAME_CD;
            this.courseDetailSearchString = courseDetailEntity;

            // M_COURSE_DETAIL_ITEMS
            M_COURSE_DETAIL_ITEMS courseDetailItemsEntity = new M_COURSE_DETAIL_ITEMS();
            courseDetailItemsEntity.DAY_CD = DAY_CD;
            courseDetailItemsEntity.COURSE_NAME_CD = COURSE_NAME_CD;
            this.courseDetailItemsSearchString = courseDetailItemsEntity;

            // M_GYOUSHA
            M_GYOUSHA gyoushaDetailEntity = new M_GYOUSHA();
            //gyoushaDetailEntity.UNPAN_JUTAKUSHA_KBN = 1;
            //gyoushaDetailEntity.NIOROSHI_GHOUSHA_KBN = 1;
            gyoushaDetailEntity.KYOTEN_CD = KYOTEN_CD;
            this.gyoushaSearchString = gyoushaDetailEntity;

            M_GENBA genbaDetailEntiy = new M_GENBA();
            genbaDetailEntiy.KYOTEN_CD = KYOTEN_CD;
            this.genbaSearchString = genbaDetailEntiy;

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            var table = this.courseDetailSearchResult;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.form.Ichiran.DataSource = table;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        // ---20140122 oonaka add 前回保存値のセット start ---

        #region ログアウト前に保存した前回値の反映

        /// <summary>
        /// ログアウト前に保存した前回値の反映
        /// </summary>
        private void SetZenkaiHozonSettei()
        {
            LogUtility.DebugMethodStart();

            // いづれかにTrueがある（前回保存値がある）
            if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED
                || Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
                || Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
                )
            {
                // システム設定値を上書き
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion ログアウト前に保存した前回値の反映

        #region 表示条件チェックボックスの入力制限

        /// <summary>
        /// 表示条件チェックボックスの入力制限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ICHIRAN_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                // すべてチェックOFFの状態
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                    && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                    && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    // 必須チェックエラー
                    this.msgLogic.MessageBoxShow("E001", "表示条件");
                    item.Checked = true;
                }
            }
        }

        #endregion

        // ---20140122 oonaka add 前回保存値のセット end ---

        // ---20140122 oonaka add 拠点CDをXMLから取得 start ---

        #region ユーザー定義反映処理(XML)

        /// <summary>
        /// ユーザー定義情報設定処理
        /// </summary>
        private void SetUserProfile()
        {
            LogUtility.DebugMethodStart();

            // ヘッダクラス
            HeaderForm header = (HeaderForm)((BusinessBaseForm)this.form.Parent).headerForm;

            // XMLから拠点CDを取得
            const string XML_KYOTEN_CD_KEY_NAME = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            header.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, XML_KYOTEN_CD_KEY_NAME);
            if (!string.IsNullOrEmpty(header.KYOTEN_CD.Text.ToString()))
            {
                header.KYOTEN_CD.Text = header.KYOTEN_CD.Text.ToString().PadLeft(header.KYOTEN_CD.MaxLength, '0');

                // 拠点略称を設定
                this.CheckKyotenCd(header.KYOTEN_CD, header.KYOTEN_NAME_RYAKU);
            }

            // 拠点変更イベント
            header.KYOTEN_CD_TextChanged();

            LogUtility.DebugMethodEnd();
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
        internal void CheckKyotenCd(CustomNumericTextBox2 headerKyotenCd, CustomTextBox headerKyotenNameRyaku)
        {
            // 初期化
            headerKyotenNameRyaku.Text = string.Empty;

            if (string.IsNullOrEmpty(headerKyotenCd.Text))
            {
                headerKyotenNameRyaku.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", headerKyotenCd.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                this.msgLogic.MessageBoxShow("E020", "拠点");
                headerKyotenCd.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                headerKyotenNameRyaku.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }
        }

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        internal M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd)
        {
            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        #endregion

        // ---20140122 oonaka add 拠点CDをXMLから取得 end ---

        #endregion

        #region 業務処理

        #region DataTableのクローン処理

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            LogUtility.DebugMethodEnd(table);
            return table;
        }

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable2(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            table.Columns["KAISYUUHIN_NAME"].MaxLength = 200;

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            LogUtility.DebugMethodEnd(table);
            return table;
        }

        #endregion

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            LogUtility.DebugMethodEnd();
            throw new NotImplementedException();
        }

        #endregion

        #region 論理削除処理

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();
            this.isRegist = true;

            try
            {
                if (!isSelect)
                {
                    this.msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    var result = this.msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.Yes)
                    {
                        using (Transaction tran = new Transaction())
                        {
                            foreach (M_COURSE_DETAIL contenashuruiEntity in this.courseDetailEntitys)
                            {
                                M_COURSE_DETAIL entity = this.courseDetailDao.GetDataByCd(contenashuruiEntity.DAY_CD.ToString(), contenashuruiEntity.COURSE_NAME_CD, contenashuruiEntity.ROW_NO.ToString());
                                if (entity != null)
                                {
                                    this.courseDetailDao.Update(contenashuruiEntity);
                                }
                            }
                            tran.Commit();
                        }
                        this.msgLogic.MessageBoxShow("I001", "削除");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //検索エラー
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
            throw new NotImplementedException();
        }

        #endregion

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        public bool CSVOutput()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    // CSV出力用DataGridView生成
                    var dgv = new CustomDataGridView();
                    dgv.Visible = false;
                    this.form.Controls.Add(dgv);

                    // DataTableを生成
                    var table = new DataTable();

                    // header部のColumnを生成
                    table.Columns.Add("曜日", typeof(string));
                    table.Columns.Add("コース名CD", typeof(string));
                    table.Columns.Add("コース名", typeof(string));
                    table.Columns.Add("コース備考", typeof(string));
                    table.Columns.Add("車種CD", typeof(string));
                    table.Columns.Add("車種名", typeof(string));
                    table.Columns.Add("車輌CD", typeof(string));
                    table.Columns.Add("車輌名", typeof(string));
                    table.Columns.Add("運転者CD", typeof(string));
                    table.Columns.Add("運転者名", typeof(string));
                    table.Columns.Add("運搬業者CD", typeof(string));
                    table.Columns.Add("運搬業者名", typeof(string));

                    foreach (DataGridViewColumn col in this.form.Ichiran.Columns)
                    {
                        if (col.Visible == true)
                        {
                            // 必須記号は削除
                            var name = col.HeaderText.Replace("※", "");

                            if (name != "詳細")
                            {
                                // 表示列かつボタン以外だった場合、明細部のColumnを生成
                                table.Columns.Add(name, typeof(string));
                            }
                        }
                    }

                    // Header値をセット
                    var day = this.ConvertYoubi(this.form.customTextBoxDayCd.Text);
                    var courseCD = this.form.customTextBoxCoureseNameCd.Text;
                    var courseName = this.form.customTextBoxCoureseName.Text;
                    var courseRemarks = this.form.customTextBox_bikou.Text;
                    var shashuCD = this.form.SHASHU_CD.Text;
                    var shashuName = this.form.SHASHU_NAME.Text;
                    var sharyouCD = this.form.SHARYOU_CD.Text;
                    var sharyouName = this.form.SHARYOU_NAME.Text;
                    var untenshaCD = this.form.UNTENSHA_CD.Text;
                    var untenshaName = this.form.UNTENSHA_NAME.Text;
                    var unpanGyoushaCD = this.form.UNPAN_GYOUSHA_CD.Text;
                    var unpanGyoushaName = this.form.UNPAN_GYOUSHA_NAME.Text;

                    // 各DataをDataTableにセット
                    foreach (DataGridViewRow dgvRow in this.form.Ichiran.Rows)
                    {
                        if (dgvRow.IsNewRow)
                        {
                            continue;
                        }
                        // 新規行生成
                        var row = table.NewRow();

                        // Header部
                        row["曜日"] = day;
                        row["コース名CD"] = courseCD;
                        row["コース名"] = courseName;
                        row["コース備考"] = courseRemarks;
                        row["車種CD"] = shashuCD;
                        row["車種名"] = shashuName;
                        row["車輌CD"] = sharyouCD;
                        row["車輌名"] = sharyouName;
                        row["運転者CD"] = untenshaCD;
                        row["運転者名"] = untenshaName;
                        row["運搬業者CD"] = unpanGyoushaCD;
                        row["運搬業者名"] = unpanGyoushaName;

                        // 明細部
                        foreach (DataGridViewColumn col in this.form.Ichiran.Columns)
                        {
                            if (col.Visible == true)
                            {
                                // 必須記号は削除
                                var name = col.HeaderText.Replace("※", "");

                                if (name != "詳細")
                                {
                                    // 表示列かつボタン以外だった場合、明細部の値を格納
                                    row[name] = dgvRow.Cells[col.Name].FormattedValue.ToString();
                                }
                            }
                        }

                        // DataTableに追加
                        table.Rows.Add(row);
                    }

                    // DataGridViewにDataTableを格納
                    dgv.DataSource = table;
                    dgv.Refresh();

                    // CSV出力用DataGridViewをCSVに出力する
                    var CSVExport = new CSVExport();
                    CSVExport.ConvertCustomDataGridViewToCsv(dgv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_COURSE), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        // ---20140116 oonaka add [CSV出力] 曜日の出力内容を変更 start ---
        private string ConvertYoubi(string youbiCd)
        {
            LogUtility.DebugMethodStart(youbiCd);
            string res = string.Empty;

            if (youbiCd.Equals("1"))
            {
                res = "月";
            }
            if (youbiCd.Equals("2"))
            {
                res = "火";
            }
            if (youbiCd.Equals("3"))
            {
                res = "水";
            }
            if (youbiCd.Equals("4"))
            {
                res = "木";
            }
            if (youbiCd.Equals("5"))
            {
                res = "金";
            }
            if (youbiCd.Equals("6"))
            {
                res = "土";
            }
            if (youbiCd.Equals("7"))
            {
                res = "日";
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        // ---20140116 oonaka add [CSV出力] 曜日の出力内容を変更 end ---

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetIchiran();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                if (false == SetSearchString())
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                this.courseSearchResult = courseDao.GetIchiranDataSql(this.courseSearchString
                                                            , this.courseNameSearchString);

                if (this.FromCourseIchiran)
                {
                    this.courseSearchString.DAY_CD = SqlInt16.Parse(this.form.customTextBoxDayCd.Text);
                    this.courseSearchString.COURSE_NAME_CD = this.form.customTextBoxCoureseNameCd.Text;
                    this.fukushaCourseSearchResult = courseDao.GetIchiranDataSql(this.courseSearchString, this.courseNameSearchString);
                }
                ColumnAllowDBNull(this.courseSearchResult);

                this.courseNioroshiSearchResult = courseNioroshiDao.GetIchiranDataSql(courseNioroshiSearchString
                                                            , this.gyoushaSearchString
                                                            , this.genbaSearchString);
                ColumnAllowDBNull(this.courseNioroshiSearchResult);

                this.courseDetailSearchResult = courseDetailDao.GetIchiranDataSql(this.courseDetailSearchString);
                // 手入力、可にさせる
                this.courseDetailSearchResult.Columns["KIBOU_TIME"].ReadOnly = false;
                ColumnAllowDBNull(this.courseDetailSearchResult);

                this.courseDetailItemsSearchResult = courseDetailItemsDao.GetIchiranDataSql(this.courseDetailItemsSearchString
                                                , this.gyoushaSearchString
                                                , this.genbaSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked
                                                , this.FromCourseIchiran
                                                , this.form.dayCdF);
                ColumnAllowDBNull(this.courseDetailItemsSearchResult);

                this.GyoushaSearchResult = gyoushaDao.GetDataSql(this.gyoushaSearchString);

                Properties.Settings.Default.ConditionValue_Text = this.form.customTextBoxCoureseName.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_DBFIELD.Text;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_TYPE.Text;
                Properties.Settings.Default.ConditionItem_Text = this.form.customTextBoxCoureseNameCd.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                Properties.Settings.Default.Save();

                dtDetailList = this.courseDetailSearchResult.Copy();

                if (this.courseDetailSearchResult.Rows != null && this.courseDetailSearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-2);
                return -2;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-2);
                return -2;
            }
            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public void SearchGenbaTeikiHinmei()
        {
            LogUtility.DebugMethodStart();

            // M_GENBA_TEIKI_HINMEI
            M_GENBA_TEIKI_HINMEI genbaTeikiHinmeiSearchString = new M_GENBA_TEIKI_HINMEI();
            if (0 < this.form.customTextBoxHinmeiNameCD.Text.Length)
            {
                // ---20140117 oonaka delete 品名CDによる制限不正 start ---
                //genbaTeikiHinmeiSearchString.HINMEI_CD = this.form.customTextBoxHinmeiNameCD.Text;
                // ---20140117 oonaka delete 品名CDによる制限不正 end ---

                // ---20140117 oonaka add 品名CDによる制限不正 start ---
                genbaTeikiHinmeiSearchString.HINMEI_CD = this.form.customTextBoxHinmeiNameCD.Text.PadLeft(6, '0');
                // ---20140117 oonaka add 品名CDによる制限不正 end ---
            }

            var shikuChousonCd = String.Empty;
            if (!String.IsNullOrEmpty(this.form.shikuChousonCdTextBox.Text))
            {
                shikuChousonCd = this.form.shikuChousonCdTextBox.Text;
            }

            if (!String.IsNullOrEmpty(this.form.gyoushaCdTextBox.Text))
            {
                genbaTeikiHinmeiSearchString.GYOUSHA_CD = this.form.gyoushaCdTextBox.Text;
            }

            if (!String.IsNullOrEmpty(this.form.genbaCdTextBox.Text))
            {
                genbaTeikiHinmeiSearchString.GENBA_CD = this.form.genbaCdTextBox.Text;
            }

            this.genbaTeikiHinmeiSearchString = genbaTeikiHinmeiSearchString;

            this.genbaTeikiHinmeiSearchResult = genbaTeikiHinmeiDao.GetIchiranDataSql(this.genbaTeikiHinmeiSearchString, shikuChousonCd, this.form.dayCdF);
            ColumnAllowDBNull(this.genbaTeikiHinmeiSearchResult);

            // ---20140115 oonaka add 曜日選択値不正対応 start ---
            string youbi = string.Empty;
            if (this.FromCourseIchiran)
            {
                youbi = this.form.dayCd.Trim();
            }
            else
            {
                youbi = this.form.customTextBoxDayCd.Text.Trim();
            }
            this.genbaTeikiHinmeiSearchResult = this.GenbaTeikiHinmeiWhereYoubi(youbi, this.genbaTeikiHinmeiSearchResult);
            // ---20140115 oonaka add 曜日選択値不正対応 end ---

            LogUtility.DebugMethodEnd();
        }

        // ---20140115 oonaka add 曜日選択値不正対応 start ---
        private DataTable GenbaTeikiHinmeiWhereYoubi(string youbi, DataTable tbl)
        {
            LogUtility.DebugMethodStart(youbi, tbl);
            DataTable res = new DataTable();

            // カラム追加
            tbl.Columns.Cast<DataColumn>().ToList().ForEach(t =>
                                                    res.Columns.Add(
                                                        new DataColumn(t.ColumnName, t.DataType, t.Expression, t.ColumnMapping)
                                                    )
                                                );
            // データ絞込み
            var rowList = tbl.Rows.Cast<DataRow>().ToList();

            if (youbi.Equals("1"))
            {
                rowList = rowList.Where(t => true.Equals(t["MONDAY"])).ToList();
            }
            if (youbi.Equals("2"))
            {
                rowList = rowList.Where(t => true.Equals(t["TUESDAY"])).ToList();
            }
            if (youbi.Equals("3"))
            {
                rowList = rowList.Where(t => true.Equals(t["WEDNESDAY"])).ToList();
            }
            if (youbi.Equals("4"))
            {
                rowList = rowList.Where(t => true.Equals(t["THURSDAY"])).ToList();
            }
            if (youbi.Equals("5"))
            {
                rowList = rowList.Where(t => true.Equals(t["FRIDAY"])).ToList();
            }
            if (youbi.Equals("6"))
            {
                rowList = rowList.Where(t => true.Equals(t["SATURDAY"])).ToList();
            }
            if (youbi.Equals("7"))
            {
                rowList = rowList.Where(t => true.Equals(t["SUNDAY"])).ToList();
            }

            // データ追加
            foreach (DataRow row in rowList)
            {
                DataRow addRow = res.NewRow();
                foreach (DataColumn col in res.Columns)
                {
                    addRow[col.ColumnName] = row[col.ColumnName];
                }
                res.Rows.Add(addRow);
            }

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        // ---20140115 oonaka add 曜日選択値不正対応 end ---

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            this.isRegist = true;
            //独自チェックの記述例を書く
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // M_COURSE
                        if (null != this.courseEntitys)
                        {
                            foreach (M_COURSE courseEntity in this.courseEntitys)
                            {
                                M_COURSE entity = this.courseDao.GetDataByCd(courseEntity.DAY_CD.ToString(), courseEntity.COURSE_NAME_CD);

                                // ---20140114 oonaka add 更新データ(システム設定)不正対処 start ---
                                // システム設定取得(Whoカラム)
                                M_COURSE systemData = new M_COURSE();
                                var binder = new DataBinderLogic<M_COURSE>(systemData);
                                binder.SetSystemProperty(systemData, false);
                                systemData = binder.Entitys[0];

                                courseEntity.UPDATE_DATE = systemData.UPDATE_DATE;
                                courseEntity.UPDATE_USER = systemData.UPDATE_USER;
                                courseEntity.UPDATE_PC = systemData.UPDATE_PC;
                                // ---20140114 oonaka add 更新データ(システム設定)不正対処 end ---

                                if (entity == null)
                                {
                                    // ---20140114 oonaka add 更新データ(システム設定)不正対処 start ---
                                    courseEntity.CREATE_DATE = systemData.CREATE_DATE;
                                    courseEntity.CREATE_USER = systemData.CREATE_USER;
                                    courseEntity.CREATE_PC = systemData.CREATE_PC;
                                    // ---20140114 oonaka add 更新データ(システム設定)不正対処 end ---
                                    this.courseDao.Insert(courseEntity);
                                }
                                else
                                {
                                    this.courseDao.Update(courseEntity);
                                }
                            }
                        }

                        // M_COURSE_NIOROSHI
                        // 古い情報は一旦全て削除
                        var nioroshiFindEntity = new M_COURSE_NIOROSHI();
                        nioroshiFindEntity.DAY_CD = SqlInt16.Parse(this.form.customTextBoxDayCd.Text);
                        nioroshiFindEntity.COURSE_NAME_CD = this.form.customTextBoxCoureseNameCd.Text;
                        var delNioroshiEntitys = this.courseNioroshiDao.GetDataForEntity(nioroshiFindEntity);
                        foreach (var entity in delNioroshiEntitys)
                        {
                            // 削除
                            this.courseNioroshiDao.Delete(entity);
                        }
                        if (null != this.courseNioroshiEntitys)
                        {

                            // 荷降マスタ新規作成
                            foreach (var entity in this.courseNioroshiEntitys)
                            {
                                this.courseNioroshiDao.Insert(entity);
                            }
                        }

                        // M_COURSE_DETAIL

                        // 古い情報は一旦全て削除
                        var detailFindEntity = new M_COURSE_DETAIL();
                        detailFindEntity.DAY_CD = SqlInt16.Parse(this.form.customTextBoxDayCd.Text);
                        detailFindEntity.COURSE_NAME_CD = this.form.customTextBoxCoureseNameCd.Text;
                        var delDetailEntitys = this.courseDetailDao.GetDataForEntity(detailFindEntity);
                        foreach (var entity in delDetailEntitys)
                        {
                            // 削除
                            this.courseDetailDao.Delete(entity);
                        }
                        if (null != this.courseDetailEntitys)
                        {

                            foreach (M_COURSE_DETAIL courseDetailEntity in this.courseDetailEntitys)
                            {
                                this.courseDetailDao.Insert(courseDetailEntity);
                            }
                        }

                        // M_COURSE_DETAIL_ITEMS

                        // 古い情報は一旦全て削除
                        var itemFindEntity = new M_COURSE_DETAIL_ITEMS();
                        itemFindEntity.DAY_CD = SqlInt16.Parse(this.form.customTextBoxDayCd.Text);
                        itemFindEntity.COURSE_NAME_CD = this.form.customTextBoxCoureseNameCd.Text;
                        var delItemEntitys = this.courseDetailItemsDao.GetDataForEntity(itemFindEntity);
                        foreach (var entity in delItemEntitys)
                        {
                            // 削除
                            this.courseDetailItemsDao.Delete(entity);
                        }

                        if (null != this.courseDetailItemsEntitys)
                        {

                            foreach (M_COURSE_DETAIL_ITEMS_add_DELETE_FLG courseDetailItemsEntity in courseDetailItemsEntitys)
                            {
                                DataTable max = this.courseDetailItemsDao.GetMaxIdByCd(courseDetailItemsEntity);

                                if (null == max || 0 == max.Rows.Count)
                                {
                                    courseDetailItemsEntity.REC_SEQ = 1;
                                }
                                else
                                {
                                    courseDetailItemsEntity.REC_SEQ = (SqlInt32)((int)max.Rows[0]["MAX_REC_SEQ"] + 1);
                                }

                                this.courseDetailItemsDao.Insert(courseDetailItemsEntity);
                            }
                        }

                        // CHANGE_LOG_M_COURSE
                        if (this.changeCourseEntitys != null)
                        {
                            foreach (CHANGE_LOG_M_COURSE entity in this.changeCourseEntitys)
                            {
                                this.changeCourseDao.Insert(entity);
                            }
                        }

                        // CHANGE_LOG_M_COURSE_DETAIL
                        if (this.changeCourseDetailEntitys != null)
                        {
                            foreach (CHANGE_LOG_M_COURSE_DETAIL entity in this.changeCourseDetailEntitys)
                            {
                                this.changeCourseDetailDao.Insert(entity);
                            }
                        }

                        // CHANGE_LOG_M_COURSE_DETAIL_ITEMS
                        if (this.changeCourseDetailItemsEntitys != null)
                        {
                            foreach (CHANGE_LOG_M_COURSE_DETAIL_ITEMS entity in this.changeCourseDetailItemsEntitys)
                            {
                                this.changeCourseDetailItemsDao.Insert(entity);
                            }
                        }

                        // CHANGE_LOG_M_COURSE_NIOROSHI
                        if (this.changeCourseNioroshiEntitys != null)
                        {
                            foreach (CHANGE_LOG_M_COURSE_NIOROSHI entity in this.changeCourseNioroshiEntitys)
                            {
                                this.changeCourseNioroshiDao.Insert(entity);
                            }
                        }
                        tran.Commit();
                    }
                    this.msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //DBエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            LogUtility.DebugMethodStart();
            bool ret = true;
            try
            {
                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                // 検索項目を初期値にセットする
                // ---20140122 oonaka delete 拠点CDをXMLから取得 start ---
                //((HeaderForm)((BusinessBaseForm)this.form.Parent).headerForm).KYOTEN_CD.Text="";
                // ---20140122 oonaka delete 拠点CDをXMLから取得 end ---
                this.form.customTextBoxCoureseName.Text = "";
                this.form.customTextBoxCoureseName.DBFieldsName = "";
                this.form.customTextBoxCoureseName.ItemDefinedTypes = "";
                this.form.customTextBoxCoureseNameCd.Text = "";
                this.form.customRadioButton1.Checked = true;

                // ---20140122 oonaka add 拠点CDをXMLから取得 start ---
                this.SetUserProfile();
                // ---20140122 oonaka add 拠点CDをXMLから取得 end ---

                //********************20131212 by heyong**********修正開始**************************
                //備考
                this.form.customTextBox_bikou.Text = "";
                //現場定期品名グリドビュー
                if (this.genbaTeikiHinmeiSearchResult != null)
                {
                    this.genbaTeikiHinmeiSearchResult.Rows.Clear();
                }
                this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.DataSource = this.genbaTeikiHinmeiSearchResult;
                this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Refresh();
                //品名CD
                this.form.customTextBoxHinmeiNameCD.Text = "";
                //品名
                this.form.customTextBoxHinmeiNameRyku.Text = "";
                //********************20131212 by heyong**********修正終了**************************

                if (this.form.Ichiran.DataSource != null)
                {
                    // 一覧クリア
                    var table = (DataTable)this.form.Ichiran.DataSource;
                    this.form.Ichiran.DataSource = table.Clone();
                }

                if (null != this.courseNioroshiSearchResult)
                {
                    this.courseNioroshiSearchResult.Rows.Clear();
                }
                this.form.customDataGridView_M_COURSE_NIOROSHI.DataSource = this.courseNioroshiSearchResult;

                this.form.shikuChousonCdTextBox.Text = String.Empty;
                this.form.shikuChousonNameRyakuTextBox.Text = String.Empty;
                this.form.gyoushaCdTextBox.Text = String.Empty;
                this.form.gyoushaNameRyakuTextBox.Text = String.Empty;
                this.form.genbaCdTextBox.Text = String.Empty;
                this.form.genbaNameRyakuTextBox.Text = String.Empty;
                this.form.customTextBoxHinmeiNameCD.Text = String.Empty;
                this.form.customTextBoxHinmeiNameRyku.Text = String.Empty;

                // 車種
                this.form.SHASHU_CD.Text = "";
                this.form.SHASHU_NAME.Text = "";

                // 車輌
                this.form.SHARYOU_CD.Text = "";
                this.form.SHARYOU_NAME.Text = "";

                // 運転者
                this.form.UNTENSHA_CD.Text = "";
                this.form.UNTENSHA_NAME.Text = "";

                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = "";
                this.form.UNPAN_GYOUSHA_NAME.Text = "";

                // 出発業者
                this.form.SHUPPATSU_GYOUSHA_CD.Text = "";
                this.form.SHUPPATSU_GYOUSHA_NAME.Text = "";

                // 出発現場
                this.form.SHUPPATSU_GENBA_CD.Text = "";
                this.form.SHUPPATSU_GENBA_NAME.Text = "";

                this.form.KUMIKOMI_ROUND_NO.Text = "1";

                // 作業開始時間_時
                this.form.SAGYOU_BEGIN_HOUR.Text = string.Empty;
                // 作業開始時間_分
                this.form.SAGYOU_BEGIN_MINUTE.Text = string.Empty;
                // 作業終了時間_時
                this.form.SAGYOU_END_HOUR.Text = string.Empty;
                // 作業終了時間_分
                this.form.SAGYOU_END_MINUTE.Text = string.Empty;

                this.FromCourseIchiran = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Cancel", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region コンテナ状況CDの重複チェック

        /// <summary>
        /// コンテナ状況CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string day_Cd, string course_Name_Cd, string row_No)
        {
            LogUtility.DebugMethodStart(day_Cd, course_Name_Cd, row_No);

            // 画面で種類CD重複チェック
            int recCount = 0;
            for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
            {
                if (this.form.Ichiran.Rows[i].Cells["DAY_CD"].Value.ToString().Equals(day_Cd) &&
                    this.form.Ichiran.Rows[i].Cells["COURSE_NAME_CD"].Value.ToString().Equals(course_Name_Cd) &&
                    this.form.Ichiran.Rows[i].Cells["ROW_NO"].Value.ToString().Equals(row_No))
                {
                    recCount++;
                }
            }

            if (recCount > 1)
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            // 検索結果で種類CD重複チェック
            for (int i = 0; i < dtDetailList.Rows.Count; i++)
            {
                // if (contena_Shurui_Cd.Equals(dtDetailList.Rows[i][1].ToString()))
                if (day_Cd.Equals(dtDetailList.Rows[i]["DAY_CD"].ToString()) &&
                    course_Name_Cd.Equals(dtDetailList.Rows[i]["COURSE_NAME_CD"].ToString()) &&
                    row_No.Equals(dtDetailList.Rows[i]["ROW_NO"].ToString()))
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }

            // DBで種類CD重複チェック
            M_COURSE_DETAIL entity = this.courseDetailDao.GetDataByCd(day_Cd, course_Name_Cd, row_No);

            if (entity != null)
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }

            LogUtility.DebugMethodEnd(false);
            return false;
        }

        #endregion

        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <param name="iRow">チェックカラム名称</param>
        /// <returns>true：正常　false：エラー</returns>
        public bool ChkGridGenba(int iRow)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //if (this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value == null ||
                //    this.form.Ichiran.Rows[iRow].Cells["GYOUSHA_CD"].Value == null ||
                //    string.IsNullOrEmpty(this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value.ToString()) ||
                //    string.IsNullOrEmpty(this.form.Ichiran.Rows[iRow].Cells["GYOUSHA_CD"].Value.ToString()))
                //{
                //    return true;
                //}

                string GenbaCd = this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value as string;
                if (string.IsNullOrWhiteSpace(GenbaCd))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value =
                    this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                if (this.form.Ichiran.EditingControl != null)
                {
                    this.form.Ichiran.EditingControl.Text = this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                }

                string GyousyaCd = this.form.Ichiran.Rows[iRow].Cells["GYOUSHA_CD"].Value as string;
                if (string.IsNullOrWhiteSpace(GyousyaCd))
                {
                    this.msgLogic.MessageBoxShow("E051", "業者");
                    this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value = string.Empty;
                    if (this.form.Ichiran.EditingControl != null)
                    {
                        this.form.Ichiran.EditingControl.Text = string.Empty;
                    }
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value.ToString();
                keyEntity.GYOUSHA_CD = this.form.Ichiran.Rows[iRow].Cells["GYOUSHA_CD"].Value.ToString();
                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba == null || genba.Length < 1)
                {
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                GetGenbaDtoCls Serch = new GetGenbaDtoCls();
                Serch.GENBA_CD = this.form.Ichiran.Rows[iRow].Cells["GENBA_CD"].Value.ToString();
                Serch.GYOUSHA_CD = this.form.Ichiran.Rows[iRow].Cells["GYOUSHA_CD"].Value.ToString();

                DataTable SearchResult = new DataTable();
                DataTable dt = GetGenbaDao.GetDataForEntity(Serch);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.form.Ichiran.Rows[iRow].Cells["GENBA_NAME_RYAKU"].Value = dt.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }
                }
                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.msgLogic.MessageBoxShow("E062", "業者");
                this.msgLogic.MessageBoxShow("E020", "現場");
                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChkGridGenba", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 現場チェック（荷降現場）
        /// </summary>
        /// <param name="iRow">チェックカラム名称</param>
        /// <returns>true：正常　false：エラー</returns>
        public bool ChkGridNioroshiGenba(int iRow)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ---20140117 oonaka delete 荷降現場CD、荷降業者CDの値チェック不正対応 start ---
                //if (this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value == null ||
                //    this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value == null ||
                //    string.IsNullOrEmpty(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString()) ||
                //    string.IsNullOrEmpty(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString()))
                //{
                //    return true;
                //}
                // ---20140117 oonaka delete 荷降現場CD、荷降業者CDの値チェック不正対応 end ---

                // ---20140117 oonaka add 荷降現場CD、荷降業者CDの値チェック不正対応 start ---
                string niorosiGenbaCd = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value as string;
                if (string.IsNullOrWhiteSpace(niorosiGenbaCd))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                // ---20140117 oonaka add 荷降現場CD、荷降業者CDの値チェック不正対応 end ---

                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start

                this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value =
                    this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                if (this.form.customDataGridView_M_COURSE_NIOROSHI.EditingControl != null)
                {
                    this.form.customDataGridView_M_COURSE_NIOROSHI.EditingControl.Text = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString().PadLeft(6, '0').ToUpper();
                }

                string niorosiGyousyaCd = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value as string;
                if (string.IsNullOrWhiteSpace(niorosiGyousyaCd))
                {
                    this.msgLogic.MessageBoxShow("E051", "荷降業者");
                    this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value = string.Empty;
                    if (this.form.customDataGridView_M_COURSE_NIOROSHI.EditingControl != null)
                    {
                        this.form.customDataGridView_M_COURSE_NIOROSHI.EditingControl.Text = string.Empty;
                    }
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString();
                keyEntity.GYOUSHA_CD = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString();
                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba == null || genba.Length < 1)
                {
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
                GetGenbaDtoCls Serch = new GetGenbaDtoCls();
                Serch.GENBA_CD = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString();
                Serch.GYOUSHA_CD = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString();

                DataTable SearchResult = new DataTable();
                // --20140117 oonaka delete 現場フォーカスアウトチェック修正 start ---
                //DataTable dt = GetGenbaDao.GetDataForEntity(Serch);
                // --20140117 oonaka delete 現場フォーカスアウトチェック修正 start ---
                // --20140117 oonaka add 現場フォーカスアウトチェック修正 start ---
                DataTable dt = GetGenbaDao.GetDataForEntity(Serch, true);
                // --20140117 oonaka add 現場フォーカスアウトチェック修正 start ---
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow].Cells["M_COURSE_NIOROSHI_GENBA_NAME_RYAKU"].Value =
                            dt.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }
                }

                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //this.msgLogic.MessageBoxShow("E062", "荷降業者");
                this.msgLogic.MessageBoxShow("E020", "現場");
                // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChkGridNioroshiGenba", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridNioroshiGenba", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        // --20140120 oonaka add 業者フォーカスアウトチェック追加 start ---

        /// <summary>
        /// 業者チェック（荷降業者）
        /// </summary>
        /// <param name="iRow">チェックカラム名称</param>
        /// <returns>true：正常　false：エラー</returns>
        public bool ChkGridNioroshiGyousha(int iRow)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 対象行
                DataGridViewRow row = this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[iRow];

                // 業者CD
                string niorosiGyoshaCd = row.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value as string;

                // 空文字の場合は正常
                if (string.IsNullOrWhiteSpace(niorosiGyoshaCd))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                // 0パディング
                row.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value = niorosiGyoshaCd.PadLeft(6, '0').ToUpper();

                // 業者検索
                string gyoushaSql = this.GetNioroshiGyoushaSql(niorosiGyoshaCd);
                DataTable gyoushaTbl = this.gyoushaDao.GetDateForStringSql(gyoushaSql);

                // 件数調査
                if (gyoushaTbl == null || gyoushaTbl.Rows.Count <= 0)
                {
                    this.msgLogic.MessageBoxShow("E062", "荷降業者");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ChkGridNioroshiGyousha", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkGridNioroshiGyousha", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            // 正常
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 荷降業者CDの有効判断SQL
        /// </summary>
        /// <param name="niorosiGyoshaCd"></param>
        /// <returns></returns>
        private string GetNioroshiGyoushaSql(string niorosiGyoshaCd)
        {
            LogUtility.DebugMethodStart(niorosiGyoshaCd);
            StringBuilder sql = new StringBuilder();

            // SELECT句
            sql.AppendLine(" SELECT DISTINCT ");
            sql.AppendLine(" 	gs.GYOUSHA_CD ");

            // FROM句
            sql.AppendLine(" FROM M_GYOUSHA AS gs ");

            // JOIN句
            sql.AppendLine(" INNER JOIN M_GENBA gb ");
            sql.AppendLine("  ON gb.GYOUSHA_CD = gs.GYOUSHA_CD  ");
            sql.AppendLine("  AND gb.DELETE_FLG = 0 ");
            sql.AppendLine("  AND (gb.TSUMIKAEHOKAN_KBN = 1 ");
            // 20151022 BUNN #12040 STR
            sql.AppendLine("       OR gb.SHOBUN_NIOROSHI_GENBA_KBN = 1 ");
            sql.AppendLine("       OR gb.SAISHUU_SHOBUNJOU_KBN = 1 ");
            // 20151022 BUNN #12040 END
            sql.AppendLine("   ) ");

            // WHERE句
            sql.AppendLine(" WHERE ");
            sql.AppendLine(" gs.DELETE_FLG = 0  ");
            // 20151022 BUNN #12040 STR
            sql.AppendLine(" AND (gs.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1 ");
            sql.AppendLine("      OR gs.UNPAN_JUTAKUSHA_KAISHA_KBN = 1 ");
            // 20151022 BUNN #12040 END
            sql.AppendLine(" ) ");
            sql.AppendLine(string.Format(" AND gs.GYOUSHA_CD = '{0}'", niorosiGyoshaCd));

            // 返却
            string res = sql.ToString();
            LogUtility.DebugMethodEnd(res);
            return res;
        }

        // --20140120 oonaka add 業者フォーカスアウトチェック追加 end ---

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity_M_COURSE(bool isDelete, out bool catchErr)
        {
            bool change = false;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                this.courseEntitys = null;

                var entityList = new M_COURSE[this.courseSearchResult.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_COURSE();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_COURSE>(entityList);
                DataTable dt = this.courseSearchResult; // as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    change = false;
                    return change;
                }

                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                if (!isDelete)
                {
                    // 変更分のみ取得
                    List<M_COURSE> addList = new List<M_COURSE>();

                    dt.Rows[0].EndEdit();

                    if (dt.GetChanges() == null)
                    {
                        change = false;
                    }
                    else
                    {
                        change = true;
                    }
                    var courseEntityList = CreateEntityForTable_M_COURSE(dt);
                    for (int i = 0; i < courseEntityList.Count; i++)
                    {
                        var courseEntity = courseEntityList[i];
                        {
                            {
                                dataBinderLogic.SetSystemProperty(courseEntity, false);

                                addList.Add(courseEntity);
                            }
                        }
                        this.courseEntitys = addList.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity_M_COURSE", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(true, catchErr);
            return change;
        }

        /// <summary>
        /// コース荷降先Entity作成
        /// </summary>
        /// <param name="isDelete">TRUE:正常, FALSE:エラー</param>
        /// <returns></returns>
        public bool CreateEntity_M_COURSE_NIOROSHI(bool isDelete, out bool catchErr)
        {
            var bRet = false;
            catchErr = true;
            try
            {
                // 一旦クリア
                this.courseNioroshiEntitys = null;

                // 荷降Tableに情報の登録がある場合
                if (this.form.customDataGridView_M_COURSE_NIOROSHI.RowCount > 1)
                {
                    // DataGridTableからEntityListを生成
                    var entityList = CreateEntityForDataGrid_M_COURSE_NIOROSHI(this.form.customDataGridView_M_COURSE_NIOROSHI);
                    this.courseNioroshiEntitys = entityList.ToArray();

                    // 正常
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity_M_COURSE_NIOROSHI", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            return bRet;
        }

        public bool CreateEntity_M_COURSE_DETAIL(bool isDelete, out bool catchErr)
        {
            bool change = false;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                courseDetailSearchResult.Columns["KAISYUUHIN_NAME"].MaxLength = 200;

                foreach (DataColumn column in this.courseDetailSearchResult.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                this.form.Ichiran.DataSource = this.form.GetDataSource();
                DataTable dt = (DataTable)this.form.Ichiran.DataSource;
                if (dt.Rows.Count == this.courseDetail.Rows.Count)
                {
                    DataRow row;
                    DataRow dr;
                    DataColumn col;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        row = dt.Rows[i];
                        dr = this.courseDetail.Rows[i];
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            col = dt.Columns[k];
                            if (Convert.ToString(row[col.ColumnName]) != Convert.ToString(dr[col.ColumnName]))
                            {
                                change = true;
                                break;
                            }
                        }
                        if (change)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    change = true;
                }

                this.courseDetailEntitys = new M_COURSE_DETAIL[this.form.Ichiran.Rows.Cast<DataGridViewRow>().Count(c => !c.IsNewRow)];
                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_COURSE_DETAIL>(this.courseDetailEntitys);
                for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                {
                    if (this.form.Ichiran.Rows[i].IsNewRow)
                    {
                        continue;
                    }
                    this.courseDetailEntitys[i] = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity_M_COURSE_DETAIL", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(true, catchErr);
            return change;
        }

        public bool CreateEntity_M_COURSE_DETAIL_ITEMS(bool isDelete, out bool catchErr)
        {
            bool change = false;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                // 一旦クリア
                this.courseDetailItemsEntitys = null;

                var entityList = new M_COURSE_DETAIL_ITEMS[this.courseDetailItemsSearchResult.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_COURSE_DETAIL_ITEMS();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_COURSE_DETAIL_ITEMS>(entityList);
                DataTable dt = this.courseDetailItemsSearchResult;

                if (dt == null || dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(change, catchErr);
                    return change;
                }

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                if (!isDelete)
                {
                    // 変更分のみ取得
                    List<M_COURSE_DETAIL_ITEMS_add_DELETE_FLG> addList = new List<M_COURSE_DETAIL_ITEMS_add_DELETE_FLG>();

                    var contenashuruiEntityList = CreateEntityForTable_M_COURSE_DETAIL(dt);
                    for (int i = 0; i < contenashuruiEntityList.Count; i++)
                    {
                        var contenashuruiEntity = contenashuruiEntityList[i];
                        {
                            if (contenashuruiEntity.DELETE_FLG == 1)
                            {
                                continue;
                            }
                            string where = "DAY_CD = " + contenashuruiEntity.DAY_CD + " AND COURSE_NAME_CD = '" + contenashuruiEntity.COURSE_NAME_CD + "' AND REC_ID = " + contenashuruiEntity.REC_ID;
                            DataRow[] drs = this.courseDetailSearchResult.Select(where);
                            if (1 == drs.Length)
                            {
                                dataBinderLogic.SetSystemProperty(contenashuruiEntity, false);
                                addList.Add(contenashuruiEntity);
                            }
                        }
                    }
                    this.courseDetailItemsEntitys = addList.ToArray();

                    if (dt.GetChanges() == null)
                    {
                        change = false;
                        return change;
                    }
                    else
                    {
                        change = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity_M_COURSE_DETAIL_ITEMS", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(true, catchErr);
            return change;
        }

        #endregion

        #region CreateEntityForDataGrid

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_COURSE_DETAIL> CreateEntityForDataGrid(CustomDataGridView gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_COURSE_DETAIL>();
            if (gridView == null)
            {
                return entityList;
            }
            for (int i = 0; i < gridView.RowCount; i++)
            {
                entityList.Add(CreateEntityForDataGridRow(gridView.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_COURSE> CreateEntityForTable_M_COURSE(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            var entityList = new List<M_COURSE>();
            if (dt == null)
            {
                return entityList;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                entityList.Add(CreateEntityForDataTable_M_COURSE(dt.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_COURSE_NIOROSHI> CreateEntityForDataGrid_M_COURSE_NIOROSHI(CustomDataGridView gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_COURSE_NIOROSHI>();
            if (gridView == null)
            {
                return entityList;
            }
            for (int i = 0; i < gridView.RowCount - 1; i++)
            {
                // 荷降Noの入力があるものは登録を行う
                if (false == string.IsNullOrEmpty(gridView.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value.ToString()))
                {
                    entityList.Add(CreateEntityForDataGridRow_M_COURSE_NIOROSHI(gridView.Rows[i]));
                }
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_COURSE_DETAIL_ITEMS_add_DELETE_FLG> CreateEntityForTable_M_COURSE_DETAIL(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            var entityList = new List<M_COURSE_DETAIL_ITEMS_add_DELETE_FLG>();
            if (dt == null)
            {
                return entityList;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                entityList.Add(CreateEntityForDataTable_M_COURSE_DETAIL_ITEMS(dt.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mContenaShurui
        /// </returns>
        internal M_COURSE_DETAIL CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_COURSE_DETAIL mCourseDetail = new M_COURSE_DETAIL();

            // DAY_CD
            if (!DBNull.Value.Equals(row.Cells["DAY_CD"].Value))
            {
                mCourseDetail.DAY_CD = SqlInt16.Parse(row.Cells["DAY_CD"].Value.ToString());
            }

            // COURSE_NAME_CD
            if (!DBNull.Value.Equals(row.Cells["COURSE_NAME_CD"].Value))
            {
                mCourseDetail.COURSE_NAME_CD = (string)row.Cells["COURSE_NAME_CD"].Value;
            }

            // REC_ID
            if (!DBNull.Value.Equals(row.Cells["REC_ID"].Value))
            {
                mCourseDetail.REC_ID = SqlInt32.Parse(row.Cells["REC_ID"].Value.ToString());
            }

            // ROW_NO
            if (!DBNull.Value.Equals(row.Cells["ROW_NO"].Value))
            {
                mCourseDetail.ROW_NO = SqlInt32.Parse(row.Cells["ROW_NO"].Value.ToString());
            }

            // ROUND_NO
            if (!DBNull.Value.Equals(row.Cells["ROUND_NO"].Value))
            {
                mCourseDetail.ROUND_NO = SqlInt32.Parse(row.Cells["ROUND_NO"].Value.ToString());
            }

            // GYOUSHA_CD
            if (!DBNull.Value.Equals(row.Cells["GYOUSHA_CD"].Value))
            {
                mCourseDetail.GYOUSHA_CD = (string)row.Cells["GYOUSHA_CD"].Value;
            }

            // GENBA_CD
            if (!DBNull.Value.Equals(row.Cells["GENBA_CD"].Value))
            {
                mCourseDetail.GENBA_CD = (string)row.Cells["GENBA_CD"].Value;
            }

            // BIKOU
            if (!DBNull.Value.Equals(row.Cells["BIKOU"].Value))
            {
                mCourseDetail.BIKOU = (string)row.Cells["BIKOU"].Value;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mCourseDetail.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }
            // KIBOU_TIME
            if (!DBNull.Value.Equals(row.Cells["KIBOU_TIME"].Value)
                && !string.IsNullOrEmpty(row.Cells["KIBOU_TIME"].Value.ToString()))
            {
                mCourseDetail.KIBOU_TIME = Convert.ToString(row.Cells["KIBOU_TIME"].Value);
            }
            // SAGYOU_TIME_MINUTE
            if (!DBNull.Value.Equals(row.Cells["SAGYOU_TIME_MINUTE"].Value))
            {
                mCourseDetail.SAGYOU_TIME_MINUTE = SqlInt16.Parse(row.Cells["SAGYOU_TIME_MINUTE"].Value.ToString());
            }


            LogUtility.DebugMethodEnd(mCourseDetail);
            return mCourseDetail;
        }

        internal M_COURSE_DETAIL CreateEntityForDataGridRow(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            M_COURSE_DETAIL mCourseDetail = new M_COURSE_DETAIL();

            // DAY_CD
            if (!DBNull.Value.Equals(dr["DAY_CD"]))
            {
                mCourseDetail.DAY_CD = SqlInt16.Parse(dr["DAY_CD"].ToString());
            }

            // COURSE_NAME_CD
            if (!DBNull.Value.Equals(dr["COURSE_NAME_CD"]))
            {
                mCourseDetail.COURSE_NAME_CD = (string)dr["COURSE_NAME_CD"];
            }

            // REC_ID
            if (!DBNull.Value.Equals(dr["REC_ID"]))
            {
                mCourseDetail.REC_ID = SqlInt32.Parse(dr["REC_ID"].ToString());
            }

            // ROW_NO
            if (!DBNull.Value.Equals(dr["ROW_NO"]))
            {
                mCourseDetail.ROW_NO = SqlInt32.Parse(dr["ROW_NO"].ToString());
            }

            // ROUND_NO
            if (!DBNull.Value.Equals(dr["ROUND_NO"]))
            {
                mCourseDetail.ROUND_NO = SqlInt32.Parse(dr["ROUND_NO"].ToString());
            }

            // GYOUSHA_CD
            if (!DBNull.Value.Equals(dr["GYOUSHA_CD"]))
            {
                mCourseDetail.GYOUSHA_CD = (string)dr["GYOUSHA_CD"];
            }

            // GENBA_CD
            if (!DBNull.Value.Equals(dr["GENBA_CD"]))
            {
                mCourseDetail.GENBA_CD = (string)dr["GENBA_CD"];
            }

            // BIKOU
            if (!DBNull.Value.Equals(dr["BIKOU"]))
            {
                mCourseDetail.BIKOU = (string)dr["BIKOU"];
            }

            // KIBOU_TIME
            if (!DBNull.Value.Equals(dr["KIBOU_TIME"]))
            {
                mCourseDetail.KIBOU_TIME = (string)dr["KIBOU_TIME"];
            }

            // SAGYOU_TIME_MINUTE
            if (!DBNull.Value.Equals(dr["SAGYOU_TIME_MINUTE"]))
            {
                mCourseDetail.SAGYOU_TIME_MINUTE = SqlInt16.Parse(dr["SAGYOU_TIME_MINUTE"].ToString());
            }

            //// TIME_STAMP
            if (!DBNull.Value.Equals(dr["TIME_STAMP"]))
            {
                mCourseDetail.TIME_STAMP = (byte[])dr["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mCourseDetail);
            return mCourseDetail;
        }

        internal M_COURSE CreateEntityForDataTable_M_COURSE(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            M_COURSE mCourse = new M_COURSE();

            // DAY_CD
            if (!DBNull.Value.Equals(dr["DAY_CD"]))
            {
                mCourse.DAY_CD = SqlInt16.Parse(dr["DAY_CD"].ToString());
            }

            // COURSE_NAME_CD
            if (!DBNull.Value.Equals(dr["COURSE_NAME_CD"]))
            {
                mCourse.COURSE_NAME_CD = (string)dr["COURSE_NAME_CD"];
            }

            // COURSE_BIKOU
            if (!DBNull.Value.Equals(dr["COURSE_BIKOU"]))
            {
                mCourse.COURSE_BIKOU = (string)dr["COURSE_BIKOU"];
            }

            // 車種CD
            if (!DBNull.Value.Equals(dr["SHASHU_CD"]))
            {
                mCourse.SHASHU_CD = (string)dr["SHASHU_CD"];
            }

            // 車輌CD
            if (!DBNull.Value.Equals(dr["SHARYOU_CD"]))
            {
                mCourse.SHARYOU_CD = (string)dr["SHARYOU_CD"];
            }

            // 運転者CD
            if (!DBNull.Value.Equals(dr["UNTENSHA_CD"]))
            {
                mCourse.UNTENSHA_CD = (string)dr["UNTENSHA_CD"];
            }

            // 運搬業者CD
            if (!DBNull.Value.Equals(dr["UNPAN_GYOUSHA_CD"]))
            {
                mCourse.UNPAN_GYOUSHA_CD = (string)dr["UNPAN_GYOUSHA_CD"];
            }

            //// TIME_STAMP
            if (!DBNull.Value.Equals(dr["TIME_STAMP"]))
            {
                mCourse.TIME_STAMP = (byte[])dr["TIME_STAMP"];
            }

            // 作業開始時間_時
            if (!string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_HOUR.Text))
            {
                mCourse.SAGYOU_BEGIN_HOUR = SqlInt16.Parse(this.form.SAGYOU_BEGIN_HOUR.Text);
            }
            // 作業開始時間_分
            if (!string.IsNullOrEmpty(this.form.SAGYOU_BEGIN_MINUTE.Text))
            {
                mCourse.SAGYOU_BEGIN_MINUTE = SqlInt16.Parse(this.form.SAGYOU_BEGIN_MINUTE.Text);
            }
            // 作業終了時間_時
            if (!string.IsNullOrEmpty(this.form.SAGYOU_END_HOUR.Text))
            {
                mCourse.SAGYOU_END_HOUR = SqlInt16.Parse(this.form.SAGYOU_END_HOUR.Text);
            }
            // 作業終了時間_分
            if (!string.IsNullOrEmpty(this.form.SAGYOU_END_MINUTE.Text))
            {
                mCourse.SAGYOU_END_MINUTE = SqlInt16.Parse(this.form.SAGYOU_END_MINUTE.Text);
            }

            // 出発業者CD
            if (!DBNull.Value.Equals(dr["SHUPPATSU_GYOUSHA_CD"]))
            {
                mCourse.SHUPPATSU_GYOUSHA_CD = (string)dr["SHUPPATSU_GYOUSHA_CD"];
            }
            // 出発現場CD
            if (!DBNull.Value.Equals(dr["SHUPPATSU_GENBA_CD"]))
            {
                mCourse.SHUPPATSU_GENBA_CD = (string)dr["SHUPPATSU_GENBA_CD"];
            }

            mCourse.DELETE_FLG = false;

            LogUtility.DebugMethodEnd(mCourse);
            return mCourse;
        }

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mContenaShurui
        /// </returns>
        internal M_COURSE_NIOROSHI CreateEntityForDataGridRow_M_COURSE_NIOROSHI(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_COURSE_NIOROSHI mCourseNioroshiDetail = new M_COURSE_NIOROSHI();

            //public SqlInt16 DAY_CD { get; set; }
            //public string COURSE_NAME_CD { get; set; }
            //public SqlInt32 NIOROSHI_NO { get; set; }
            //public string NIOROSHI_GYOUSHA_CD { get; set; }
            //public string NIOROSHI_GENBA_CD { get; set; }

            // DAY_CD
            if (!DBNull.Value.Equals(row.Cells["M_COURSE_NIOROSHI_DAY_CD"].Value))
            {
                mCourseNioroshiDetail.DAY_CD = SqlInt16.Parse(row.Cells["M_COURSE_NIOROSHI_DAY_CD"].Value.ToString());
            }

            // COURSE_NAME_CD
            if (!DBNull.Value.Equals(row.Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value))
            {
                mCourseNioroshiDetail.COURSE_NAME_CD = (string)row.Cells["M_COURSE_NIOROSHI_COURSE_NAME_CD"].Value;
            }

            // NIOROSHI_NO
            if (!DBNull.Value.Equals(row.Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value))
            {
                mCourseNioroshiDetail.NIOROSHI_NO = SqlInt32.Parse(row.Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value.ToString());
            }

            // NIOROSHI_GYOUSHA_CD
            if (!DBNull.Value.Equals(row.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value))
            {
                mCourseNioroshiDetail.NIOROSHI_GYOUSHA_CD = (string)row.Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value;
            }

            // NIOROSHI_GENBA_CD
            if (!DBNull.Value.Equals(row.Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value))
            {
                mCourseNioroshiDetail.NIOROSHI_GENBA_CD = (string)row.Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value;
            }

            //// TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["M_COURSE_NIOROSHI_TIME_STAMP"].Value))
            {
                mCourseNioroshiDetail.TIME_STAMP = (byte[])row.Cells["M_COURSE_NIOROSHI_TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mCourseNioroshiDetail);
            return mCourseNioroshiDetail;
        }

        internal M_COURSE_DETAIL_ITEMS_add_DELETE_FLG CreateEntityForDataTable_M_COURSE_DETAIL_ITEMS(DataRow dr)
        {
            LogUtility.DebugMethodStart(dr);

            M_COURSE_DETAIL_ITEMS_add_DELETE_FLG mCourseDetailItems = new M_COURSE_DETAIL_ITEMS_add_DELETE_FLG();

            if (dr.RowState != DataRowState.Deleted)
            {
                // DAY_CD
                if (!DBNull.Value.Equals(dr["DAY_CD"]))
                {
                    mCourseDetailItems.DAY_CD = SqlInt16.Parse(dr["DAY_CD"].ToString());
                }

                // COURSE_NAME_CD
                if (!DBNull.Value.Equals(dr["COURSE_NAME_CD"]))
                {
                    mCourseDetailItems.COURSE_NAME_CD = (string)dr["COURSE_NAME_CD"];
                }

                // REC_ID
                if (!DBNull.Value.Equals(dr["REC_ID"]))
                {
                    mCourseDetailItems.REC_ID = SqlInt32.Parse(dr["REC_ID"].ToString());
                }

                // REC_SEQ
                if (!DBNull.Value.Equals(dr["REC_SEQ"]))
                {
                    mCourseDetailItems.REC_SEQ = SqlInt32.Parse(dr["REC_SEQ"].ToString());
                }

                // HINMEI_CD
                if (!DBNull.Value.Equals(dr["HINMEI_CD"]))
                {
                    mCourseDetailItems.HINMEI_CD = (string)dr["HINMEI_CD"];
                }

                // UNIT_CD
                if (!DBNull.Value.Equals(dr["UNIT_CD"]))
                {
                    mCourseDetailItems.UNIT_CD = SqlInt16.Parse(dr["UNIT_CD"].ToString());
                }

                // KANSANCHI
                if (!DBNull.Value.Equals(dr["KANSANCHI"]))
                {
                    mCourseDetailItems.KANSANCHI = SqlDecimal.Parse(dr["KANSANCHI"].ToString());
                }

                // KANSAN_UNIT_CD
                if (!DBNull.Value.Equals(dr["KANSAN_UNIT_CD"]))
                {
                    mCourseDetailItems.KANSAN_UNIT_CD = SqlInt16.Parse(dr["KANSAN_UNIT_CD"].ToString());
                }

                // KEIYAKU_KBN
                if (!DBNull.Value.Equals(dr["KEIYAKU_KBN"]))
                {
                    mCourseDetailItems.KEIYAKU_KBN = SqlInt16.Parse(dr["KEIYAKU_KBN"].ToString());
                }

                // INPUT_KBN
                if (!DBNull.Value.Equals(dr["INPUT_KBN"]))
                {
                    mCourseDetailItems.INPUT_KBN = SqlInt16.Parse(dr["INPUT_KBN"].ToString());
                }

                // NIOROSHI_NO
                if (!DBNull.Value.Equals(dr["NIOROSHI_NUMBER"]))
                {
                    mCourseDetailItems.NIOROSHI_NO = SqlInt32.Parse(dr["NIOROSHI_NUMBER"].ToString());
                }

                // ANBUN_FLG
                if (!DBNull.Value.Equals(dr["ANBUN_FLG"]))
                {
                    mCourseDetailItems.ANBUN_FLG = (Boolean)dr["ANBUN_FLG"];
                }
                else
                {
                    mCourseDetailItems.ANBUN_FLG = false;
                }

                //// TIME_STAMP
                if (!DBNull.Value.Equals(dr["TIME_STAMP"]))
                {
                    mCourseDetailItems.TIME_STAMP = (byte[])dr["TIME_STAMP"];
                }

                //// DELETE_FLG
                if (!DBNull.Value.Equals(dr["DELETE_FLG"]))
                {
                    mCourseDetailItems.DELETE_FLG = (int)dr["DELETE_FLG"];
                }

                //DENPYOU_KBN_CD
                if (!DBNull.Value.Equals(dr["DENPYOU_KBN_CD"]))
                {
                    mCourseDetailItems.DENPYOU_KBN_CD = SqlInt16.Parse(dr["DENPYOU_KBN_CD"].ToString());
                }

                //KANSAN_UNIT_MOBILE_OUTPUT_FLG
                if (!DBNull.Value.Equals(dr["KANSAN_UNIT_MOBILE_OUTPUT_FLG"]))
                {
                    mCourseDetailItems.KANSAN_UNIT_MOBILE_OUTPUT_FLG = (Boolean)dr["KANSAN_UNIT_MOBILE_OUTPUT_FLG"];
                }
                else
                {
                    mCourseDetailItems.KANSAN_UNIT_MOBILE_OUTPUT_FLG = false;
                }

                // KEIJYOU_KBN
                if (!DBNull.Value.Equals(dr["KEIJYOU_KBN"]))
                {
                    mCourseDetailItems.KEIJYOU_KBN = SqlInt16.Parse(dr["KEIJYOU_KBN"].ToString());
                }

                // TEKIYOU_BEGIN
                if (!DBNull.Value.Equals(dr["TEKIYOU_BEGIN"]))
                {
                    mCourseDetailItems.TEKIYOU_BEGIN = SqlDateTime.Parse(dr["TEKIYOU_BEGIN"].ToString());
                }

                // KEIJYOU_KBN
                if (!DBNull.Value.Equals(dr["TEKIYOU_END"]))
                {
                    mCourseDetailItems.TEKIYOU_END = SqlDateTime.Parse(dr["TEKIYOU_END"].ToString());
                }
            }

            LogUtility.DebugMethodEnd(mCourseDetailItems);
            return mCourseDetailItems;
        }

        #endregion

        #region 登録前のチェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            try
            {
                ArrayList errColName = new ArrayList();

                Boolean rtn = true;

                Boolean isErr;

                // 必須入力チェック
                this.form.GetDataSource();
                {
                    isErr = false;
                    for (int i = 0; i < this.form.Ichiran.RowCount; i++)
                    {
                        if (this.form.Ichiran.Rows[i].IsNewRow)
                        {
                            continue;
                        }

                        if (null == this.form.Ichiran.Rows[i].Cells["ROUND_NO"].Value ||
                            "".Equals(this.form.Ichiran.Rows[i].Cells["ROUND_NO"].Value.ToString().Trim()))
                        {
                            if (false == isErr)
                            {
                                errColName.Add("回数");
                                isErr = true;
                            }
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["ROUND_NO"], true);
                        }
                    }

                    isErr = false;
                    for (int i = 0; i < this.form.Ichiran.RowCount; i++)
                    {
                        if (this.form.Ichiran.Rows[i].IsNewRow)
                        {
                            continue;
                        }

                        if (null == this.form.Ichiran.Rows[i].Cells["GYOUSHA_CD"].Value ||
                            "".Equals(this.form.Ichiran.Rows[i].Cells["GYOUSHA_CD"].Value.ToString().Trim()))
                        {
                            if (false == isErr)
                            {
                                errColName.Add("業者CD");
                                isErr = true;
                            }
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["GYOUSHA_CD"], true);
                        }
                    }

                    isErr = false;
                    for (int i = 0; i < this.form.Ichiran.RowCount; i++)
                    {
                        if (this.form.Ichiran.Rows[i].IsNewRow)
                        {
                            continue;
                        }

                        if (null == this.form.Ichiran.Rows[i].Cells["GENBA_CD"].Value ||
                            "".Equals(this.form.Ichiran.Rows[i].Cells["GENBA_CD"].Value.ToString().Trim()))
                        {
                            if (false == isErr)
                            {
                                errColName.Add("現場CD");
                                isErr = true;
                            }
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["GENBA_CD"], true);
                        }
                    }

                    #region オプションによる必須項目チェック
                    // コース最適化(NAVITIME連携)がONの場合のみ
                    if (AppConfig.AppOptions.IsNAVITIME())
                    {
                        // 希望時間が入力されている明細がある場合、作業時間Fromは必須とする
                        isErr = false;
                        for (int i = 0; i < this.form.Ichiran.RowCount; i++)
                        {
                            if (this.form.Ichiran.Rows[i].IsNewRow)
                            {
                                continue;
                            }

                            if (null == this.form.Ichiran.Rows[i].Cells["KIBOU_TIME"].Value ||
                                "".Equals(this.form.Ichiran.Rows[i].Cells["KIBOU_TIME"].Value.ToString().Trim()))
                            {
                                // 未入力ならチェックはしない
                            }
                            else
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

                        // 希望時間が入力されている明細がある場合、作業時間Toは必須とする
                        isErr = false;
                        for (int i = 0; i < this.form.Ichiran.RowCount; i++)
                        {
                            if (this.form.Ichiran.Rows[i].IsNewRow)
                            {
                                continue;
                            }

                            if (null == this.form.Ichiran.Rows[i].Cells["KIBOU_TIME"].Value ||
                                "".Equals(this.form.Ichiran.Rows[i].Cells["KIBOU_TIME"].Value.ToString().Trim()))
                            {
                                // 未入力ならチェックはしない
                            }
                            else
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
                    }
                    #endregion
                }

                this.form.Ichiran.Refresh();
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
                else
                {
                    {
                        for (int i = 0; i < this.form.customDataGridView_M_COURSE_NIOROSHI.RowCount; i++)
                        {
                            if (null != this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value &&
                                !"".Equals(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value.ToString().Trim()))
                            {
                                // 荷降Noのリストを格納
                                nioroshiNoList.Add(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value.ToString().Trim());

                                if ((null == this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value ||
                                "".Equals(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString().Trim())) &&
                                    (null != this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value &&
                                !"".Equals(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString().Trim()))
                                    )
                                {
                                    rtn = false;
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"], true);
                                }
                            }
                        }
                        if (!rtn)
                        {
                            this.msgLogic.MessageBoxShow("E012", "荷降業者CD");
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    {
                        for (int i = 0; i < this.form.customDataGridView_M_COURSE_NIOROSHI.RowCount; i++)
                        {
                            if (null != this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value &&
                                !"".Equals(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_NIOROSHI_NO"].Value.ToString().Trim()))
                            {
                                if ((null == this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value ||
                                "".Equals(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GENBA_CD"].Value.ToString().Trim())) &&
                                    (null != this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value &&
                                !"".Equals(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GYOUSHA_CD"].Value.ToString().Trim()))
                                    )
                                {
                                    rtn = false;
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.customDataGridView_M_COURSE_NIOROSHI.Rows[i].Cells["M_COURSE_NIOROSHI_GENBA_CD"], true);
                                }
                            }
                        }
                        if (!rtn)
                        {
                            this.msgLogic.MessageBoxShow("E012", "荷降現場CD");
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    foreach (DataRow row in this.courseDetailItemsSearchResult.Rows)
                    {
                        if ("1".Equals(Convert.ToString(row["DELETE_FLG"]))
                                || "True".Equals(Convert.ToString(row["DELETE_FLG"])))
                        {
                            continue;
                        }

                        string tekiyouBegin = Convert.ToString(row["TEKIYOU_BEGIN"]);
                        string tekiyouEnd = Convert.ToString(row["TEKIYOU_END"]);
                        string genbaTekiyouBegin = Convert.ToString(row["GENBA_TEKIYOU_BEGIN"]);
                        string genbaTekiyouEnd = Convert.ToString(row["GENBA_TEKIYOU_END"]);
                        string nioroshiNumber = Convert.ToString(row["NIOROSHI_NUMBER"]);

                        if (!string.IsNullOrEmpty(nioroshiNumber))
                        {
                            if (!nioroshiNoList.Contains(nioroshiNumber))
                            {
                                this.msgLogic.MessageBoxShow("E012", string.Format("荷降No{0}の荷降明細", nioroshiNumber));
                                LogUtility.DebugMethodEnd(false);
                                return false;
                            }
                        }

                        if (!string.IsNullOrEmpty(tekiyouBegin))
                        {
                            DateTime begin = DateTime.Parse(tekiyouBegin);
                            if (!string.IsNullOrEmpty(genbaTekiyouBegin) && begin.CompareTo(DateTime.Parse(genbaTekiyouBegin)) < 0)
                            {
                                this.msgLogic.MessageBoxShow("E256", "適用期間", "現場適用期間");
                                LogUtility.DebugMethodEnd(false);
                                return false;
                            }
                        }
                        else if (!string.IsNullOrEmpty(genbaTekiyouBegin))
                        {
                            this.msgLogic.MessageBoxShow("E256", "適用期間", "現場適用期間");
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }

                        if (string.IsNullOrEmpty(tekiyouBegin) && !string.IsNullOrEmpty(genbaTekiyouBegin))
                        {
                            this.msgLogic.MessageBoxShow("E256", "適用期間", "現場適用期間");
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }

                        if (!string.IsNullOrEmpty(tekiyouEnd))
                        {
                            DateTime end = DateTime.Parse(tekiyouEnd);
                            if (!string.IsNullOrEmpty(genbaTekiyouEnd) && end.CompareTo(DateTime.Parse(genbaTekiyouEnd)) > 0)
                            {
                                this.msgLogic.MessageBoxShow("E256", "適用期間", "現場適用期間");
                                LogUtility.DebugMethodEnd(false);
                                return false;
                            }
                        }
                        else if (!string.IsNullOrEmpty(genbaTekiyouEnd))
                        {
                            this.msgLogic.MessageBoxShow("E256", "適用期間", "現場適用期間");
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }

                        if (!string.IsNullOrEmpty(tekiyouBegin) && !string.IsNullOrEmpty(tekiyouEnd))
                        {
                            DateTime begin = DateTime.Parse(tekiyouBegin);
                            DateTime end = DateTime.Parse(tekiyouEnd);
                            if (begin.CompareTo(end) > 0)
                            {
                                this.msgLogic.MessageBoxShow("E030", "適用開始日", "適用終了日");
                                LogUtility.DebugMethodEnd(false);
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region DataGridViewデータ件数チェック処理

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            LogUtility.DebugMethodStart();

            if (this.form.Ichiran.Rows.Count > 0)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region NOT NULL制約を一時的に解除

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            DataTable preDt = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                // NOT NULL制約を一時的に解除(新規追加行対策)
                column.AllowDBNull = true;

                // TIME_STAMPがなぜか一意制約有のため、解除
                if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        public bool getCourseName(string kyotenCD, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday)
        {
            LogUtility.DebugMethodStart(kyotenCD, monday, tuesday, wednesday, thursday, friday, saturday, sunday);
            bool ret = true;
            try
            {
                M_COURSE_NAME courseNameEntity = new M_COURSE_NAME();

                if (0 < kyotenCD.Length)
                {
                    courseNameEntity.KYOTEN_CD = SqlInt16.Parse(kyotenCD);
                }
                else
                {
                    courseNameEntity.KYOTEN_CD = SqlInt16.Null;
                }

                this.courseNameSearchResult = courseNameDao.GetDataSql(courseNameEntity, monday, tuesday, wednesday, thursday, friday, saturday, sunday);
                ColumnAllowDBNull(this.courseNameSearchResult);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getCourseName", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getCourseName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        public DataRow getRecId(string DayCd, string courseNameCd, string gyoushaCd, string genbaCd, string roundNo, out bool catchErr)
        {
            LogUtility.DebugMethodStart(DayCd, courseNameCd, gyoushaCd, genbaCd, roundNo);

            M_COURSE_DETAIL entity = new M_COURSE_DETAIL();
            catchErr = true;
            DataTable find = new DataTable();
            try
            {
                entity.DAY_CD = Convert.ToInt16(DayCd);
                entity.COURSE_NAME_CD = courseNameCd;
                entity.GYOUSHA_CD = gyoushaCd;
                entity.GENBA_CD = genbaCd;
                entity.ROUND_NO = Convert.ToInt32(roundNo);

                DataTable tb = courseDetailDao.GetMaxIdByCd(entity);

                if (null == tb || 0 == tb.Rows.Count)
                {
                    LogUtility.DebugMethodEnd(null, catchErr);
                    return null;
                }

                entity.REC_ID = (int)tb.Rows[0]["MAX_REC_ID"];

                find = courseDetailDao.GetIchiranDataSql(entity);

                if (null == find || 0 == find.Rows.Count)
                {
                    LogUtility.DebugMethodEnd(null, catchErr);
                    return null;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getRecId", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getRecId", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }

            LogUtility.DebugMethodEnd(find.Rows[0], catchErr);
            return find.Rows[0];
        }

        #endregion

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

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に設定します
        /// </summary>
        internal void SetReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;
            this.form.customDataGridView_M_COURSE_NIOROSHI.ReadOnly = true;
            this.form.customDataGridView_M_COURSE_NIOROSHI.AllowUserToAddRows = false;

            // 備考
            this.form.customTextBox_bikou.ReadOnly = true;
            // 車種CD
            this.form.SHASHU_CD.ReadOnly = true;
            // 車輌CD
            this.form.SHARYOU_CD.ReadOnly = true;
            // 運転者CD
            this.form.UNTENSHA_CD.ReadOnly = true;
            // 運搬業者CD
            this.form.UNPAN_GYOUSHA_CD.ReadOnly = true;
            // 車種ポップ
            this.form.SHASHU_SEARCH_BUTTON.Enabled = false;
            // 車輌ポップ
            this.form.SHARYOU_SEARCH_BUTTON.Enabled = false;
            // 運転者ポップ
            this.form.UNTENSHA_SEARCH_BUTTON.Enabled = false;
            // 運搬業者ポップ
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.Enabled = false;
            // 出発業者
            this.form.SHUPPATSU_GYOUSHA_CD.ReadOnly = true;
            // 出発現場
            this.form.SHUPPATSU_GENBA_CD.ReadOnly = true;

            // FunctionButton
            this.parentForm.bt_func2.Enabled = false;
            this.parentForm.bt_func3.Enabled = false;
            this.parentForm.bt_func4.Enabled = false;
            this.parentForm.bt_func5.Enabled = false;
            this.parentForm.bt_func9.Enabled = false;
            this.parentForm.bt_func10.Enabled = false;

            // SubFunction
            this.parentForm.bt_process2.Enabled = false;
            this.parentForm.bt_process3.Enabled = false;
            this.parentForm.bt_process4.Enabled = false;
            this.parentForm.bt_process5.Enabled = false;
        }

        /// <summary>
        /// 参照モード表示を解除します
        /// </summary>
        internal void ReleaseReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = false;
            this.form.customDataGridView_M_COURSE_NIOROSHI.ReadOnly = false;
            this.form.customDataGridView_M_COURSE_NIOROSHI.AllowUserToAddRows = true;

            // 備考
            this.form.customTextBox_bikou.ReadOnly = false;
            // 車種CD
            this.form.SHASHU_CD.ReadOnly = false;
            // 車輌CD
            this.form.SHARYOU_CD.ReadOnly = false;
            // 運転者CD
            this.form.UNTENSHA_CD.ReadOnly = false;
            // 運搬業者CD
            this.form.UNPAN_GYOUSHA_CD.ReadOnly = false;
            // 車種ポップ
            this.form.SHASHU_SEARCH_BUTTON.Enabled = true;
            // 車輌ポップ
            this.form.SHARYOU_SEARCH_BUTTON.Enabled = true;
            // 運転者ポップ
            this.form.UNTENSHA_SEARCH_BUTTON.Enabled = true;
            // 運搬業者ポップ
            this.form.UNPAN_GYOUSHA_SEARCH_BUTTON.Enabled = true;
            // 出発業者
            this.form.SHUPPATSU_GYOUSHA_CD.ReadOnly = false;
            // 出発現場
            this.form.SHUPPATSU_GENBA_CD.ReadOnly = false;

            this.parentForm = (BusinessBaseForm)this.form.Parent;

            // FunctionButton
            this.parentForm.bt_func2.Enabled = true;
            this.parentForm.bt_func3.Enabled = true;
            this.parentForm.bt_func4.Enabled = true;
            this.parentForm.bt_func5.Enabled = true;
            this.parentForm.bt_func9.Enabled = true;
            // オプション
            if (AppConfig.AppOptions.IsMAPBOX())
            {
                this.parentForm.bt_func10.Enabled = true;
            }
            // SubFunction
            this.parentForm.bt_process2.Enabled = true;
            this.parentForm.bt_process3.Enabled = true;
            this.parentForm.bt_process4.Enabled = true;
            this.parentForm.bt_process5.Enabled = true;
        }

        #endregion

        #region イベント

        /// <summary>
        /// 終了する。
        /// </summary>
        internal void FormClose()
        {
            try
            {
                Properties.Settings.Default.ConditionValue_Text = this.form.customTextBoxCoureseName.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.customTextBoxCoureseName.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.customTextBoxCoureseName.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.customTextBoxCoureseNameCd.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                Properties.Settings.Default.Save();
                this.form.Close();
                this.parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Enter(object sender, EventArgs e)
        {
            // 前回値を保持する
            var ctrl = (CustomAlphaNumTextBox)sender;
            //this.oldSharyouCD = ctrl.Text;
        }

        /// <summary>
        /// 車輌CDValidated処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validated(object sender, EventArgs e)
        {
            // Validated処理
            var ctrl = (CustomAlphaNumTextBox)sender;
            this.sharyouCDValidatedProc(ctrl.Text);
        }

        /// <summary>
        /// 運転者CDValidated処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            // Validated処理
            var ctrl = (CustomAlphaNumTextBox)sender;
            this.untenshaCDValidatedProc(ctrl.Text);
        }

        /// <summary>
        /// 運搬業者CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            // 前回値保存
            this.form.SetOldValue(sender);
        }

        /// <summary>
        /// 運搬業者CDValidated処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validated(object sender, EventArgs e)
        {
            // Validated処理
            var ctrl = (CustomAlphaNumTextBox)sender;
            this.unpanGyoushaCDValidatedProc(ctrl.Text);
        }

        /// <summary>
        /// 車輌CDValidated処理
        /// </summary>
        /// <param name="val">対象のCD</param>
        private void sharyouCDValidatedProc(string cd)
        {
            // 前回チェック時と変更がなければ処理しない
            if (cd == this.oldSharyouCD)
            {
                return;
            }

            // 一旦初期化
            this.form.SHARYOU_NAME.Text = string.Empty;

            // 車輌CDが入力されていなければ処理しない
            if (string.IsNullOrEmpty(cd))
            {
                this.oldSharyouCD = string.Empty;
                return;
            }

            // 入力CDが車輌として登録されているかのチェック
            var sharyouFindEntity = new M_SHARYOU();
            if (false == string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                sharyouFindEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            sharyouFindEntity.SHARYOU_CD = cd;
            var sharyouEntitys = this.sharyouFWDao.GetAllValidData(sharyouFindEntity);
            bool UnpanGyousyaCheck = false;
            if ((sharyouEntitys != null) && (sharyouEntitys.Length > 0))
            {
                var Unpangyousha = this.gyoushaFWDao.GetDataByCd(sharyouEntitys[0].GYOUSHA_CD);
                if (Unpangyousha != null && Unpangyousha.DELETE_FLG == false && Unpangyousha.UNPAN_JUTAKUSHA_KAISHA_KBN == true)
                {
                    // 運搬業者
                    UnpanGyousyaCheck = true;
                }
            }

            if ((sharyouEntitys != null) && (sharyouEntitys.Length > 0) && UnpanGyousyaCheck)
            {
                if (sharyouEntitys.Length == 1)
                {
                    // ヒット数が１件の場合、取得したデータをそのまま格納
                    this.form.SHARYOU_NAME.Text = sharyouEntitys[0].SHARYOU_NAME_RYAKU;

                    // 各CDよりそれぞれ情報を取得
                    var shashu = this.shashuFWDao.GetDataByCd(sharyouEntitys[0].SHASYU_CD);
                    if (shashu != null)
                    {
                        // 車種をセット
                        this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                        this.form.SHASHU_NAME.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                    else
                    {
                        // 該当情報が存在しない場合はブランク
                        this.form.SHASHU_CD.Text = "";
                        this.form.SHASHU_NAME.Text = "";
                    }
                    var shain = this.shainFWDao.GetDataByCd(sharyouEntitys[0].SHAIN_CD);
                    if (shain != null)
                    {
                        // 運転者をセット
                        this.form.UNTENSHA_CD.Text = shain.SHAIN_CD;
                        this.form.UNTENSHA_NAME.Text = shain.SHAIN_NAME_RYAKU;
                    }
                    else
                    {
                        // 該当情報が存在しない場合はブランク
                        this.form.UNTENSHA_CD.Text = "";
                        this.form.UNTENSHA_NAME.Text = "";
                    }
                    var gyousha = this.gyoushaFWDao.GetDataByCd(sharyouEntitys[0].GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 運搬業者をセット
                        this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        // 該当情報が存在しない場合はブランク
                        this.form.UNPAN_GYOUSHA_CD.Text = "";
                        this.form.UNPAN_GYOUSHA_NAME.Text = "";
                    }

                    // データソース更新
                    this.sharyouDataSourceUpdate();
                    this.oldSharyouCD = this.form.SHARYOU_CD.Text;
                }
                else
                {
                    // ヒット数が複数件の場合、ポップアップ表示
                    this.form.SHARYOU_CD.Focus();
                    SendKeys.Send(" ");

                    this.oldSharyouCD = this.form.SHARYOU_CD.Text;
                }
            }
            else
            {
                // CDが車輌に該当していなかったため、エラー
                this.msgLogic.MessageBoxShow("E020", "車輌");
                this.form.SHARYOU_CD.Focus();
                this.oldSharyouCD = string.Empty;
            }
        }

        /// <summary>
        /// 運転者CDValidated処理
        /// </summary>
        /// <param name="cd">対象のCD</param>
        private void untenshaCDValidatedProc(string cd)
        {
            // 一旦初期化
            this.form.UNTENSHA_NAME.Text = string.Empty;

            // 入力CDが運転者として登録されているかのチェック
            var shainFindEntity = new M_SHAIN();
            shainFindEntity.SHAIN_CD = cd;
            var shainEntitys = this.shainFWDao.GetAllValidData(shainFindEntity);

            if ((shainEntitys != null) && (shainEntitys.Length > 0))
            {
                // 社員マスタは社員CDがKeyになっているため唯一がヒットする
                if (shainEntitys[0].UNTEN_KBN.Value == true)
                {
                    // 運転者名をセット
                    this.form.UNTENSHA_NAME.Text = shainEntitys[0].SHAIN_NAME_RYAKU;
                }
                else
                {
                    // CDが運転者に該当していなかったため、エラー
                    this.msgLogic.MessageBoxShow("E020", "社員");
                    this.form.UNTENSHA_CD.Focus();
                }
            }
            else
            {
                // そもそも該当外のCDはFocusOutCheckでチェックが行われる
            }
        }

        /// <summary>
        /// 運搬業者CDValidated処理
        /// </summary>
        /// <param name="cd">対象のCD</param>
        internal void unpanGyoushaCDValidatedProc(string cd)
        {
            // 一旦初期化
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            if (this.form.IsChangedValue(this.form.UNPAN_GYOUSHA_CD))
            {
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME.Text = string.Empty;
                this.oldSharyouCD = string.Empty;
            }

            if (string.IsNullOrEmpty(cd))
            {
                return;
            }
            // 入力CDが運搬業者として登録されているかのチェック
            var gyoushaFindEntity = new M_GYOUSHA();
            gyoushaFindEntity.GYOUSHA_CD = cd;
            var gyoushaEntitys = this.gyoushaFWDao.GetAllValidData(gyoushaFindEntity);
            if ((gyoushaEntitys != null) && (gyoushaEntitys.Length > 0))
            {
                // 業者マスタは業者CDがKeyになっているため唯一がヒットする
                if ((gyoushaEntitys[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                {
                    // 運搬業者名をセット
                    this.form.UNPAN_GYOUSHA_NAME.Text = gyoushaEntitys[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // CDが運搬業者に該当していなかったため、エラー
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                }
            }
            else
            {
                // そもそも該当外のCDはFocusOutCheckでチェックが行われる
                this.msgLogic.MessageBoxShow("E020", "業者");
                this.form.UNPAN_GYOUSHA_CD.Focus();
            }
        }

        /// <summary>
        /// 車輌データソース更新
        /// </summary>
        internal void sharyouDataSourceUpdate()
        {
            if ((this.courseSearchResult != null) && (this.courseSearchResult.Rows.Count > 0))
            {
                // データソース更新
                this.form.SHASHU_CD.DataBindings["Text"].WriteValue();
                this.form.SHASHU_NAME.DataBindings["Text"].WriteValue();
                this.form.SHARYOU_CD.DataBindings["Text"].WriteValue();
                this.form.SHARYOU_NAME.DataBindings["Text"].WriteValue();
                this.form.UNTENSHA_CD.DataBindings["Text"].WriteValue();
                this.form.UNTENSHA_NAME.DataBindings["Text"].WriteValue();
                this.form.UNPAN_GYOUSHA_CD.DataBindings["Text"].WriteValue();
                this.form.UNPAN_GYOUSHA_NAME.DataBindings["Text"].WriteValue();
            }
        }

        #endregion

        #region 回数

        /// <summary>
        /// 回数重複チェック
        /// </summary>
        /// <param name="row">編集行</param>
        /// <returns>TRUE:重複あり, FALSE:重複なし</returns>
        /// <remarks>一覧より、編集行の回数・業者CD・現場CDが重複するものがあるかどうかをチェックする</remarks>
        internal bool roundNoOverlapCheck(DataGridViewRow editRow)
        {
            bool ret = false;

            if (((editRow.Cells["ROUND_NO"].Value != null) && (false == string.IsNullOrEmpty(editRow.Cells["ROUND_NO"].Value.ToString()))) &&
                ((editRow.Cells["GYOUSHA_CD"].Value != null) && (false == string.IsNullOrEmpty(editRow.Cells["GYOUSHA_CD"].Value.ToString()))) &&
                ((editRow.Cells["GENBA_CD"].Value != null) && (false == string.IsNullOrEmpty(editRow.Cells["GENBA_CD"].Value.ToString()))))
            {
                // 一覧内を検索
                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    // 編集行はチェック対象外
                    if (editRow.Index != row.Index && !row.IsNewRow)
                    {
                        // 回数が一致した場合

                        if (((row.Cells["ROUND_NO"].Value != null) && (false == string.IsNullOrEmpty(row.Cells["ROUND_NO"].Value.ToString()))) &&
                            ((row.Cells["GYOUSHA_CD"].Value != null) && (false == string.IsNullOrEmpty(row.Cells["GYOUSHA_CD"].Value.ToString()))) &&
                            ((row.Cells["GENBA_CD"].Value != null) && (false == string.IsNullOrEmpty(row.Cells["GENBA_CD"].Value.ToString()))))
                        {
                            if (true == row.Cells["ROUND_NO"].Value.Equals(editRow.Cells["ROUND_NO"].Value))
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

            return ret;
        }

        /// <summary>
        /// 回数変更時処理
        /// </summary>
        /// <remarks>
        /// 回数に指定された数値分の回数組み込みを行える業者・現場にて
        /// 現場定期品名一覧の再抽出を行う
        /// </remarks>
        internal string roundChanged()
        {
            // 回数取得
            var roundNo = Int32.Parse(this.form.KUMIKOMI_ROUND_NO.Text);

            // 抽出条件文字列
            var findStr = string.Empty;

            if (this.form.Ichiran.DataSource != null)
            {
                // 回数が１以下の場合は全件検索とする
                // 回数が１より大きい場合のみフィルタリングを行う
                if (roundNo > 1)
                {
                    // 入力されている回数-1の業者・現場を抽出
                    var ichiranTable = (DataTable)this.form.Ichiran.DataSource;
                    var ichiranFilRows = ichiranTable.Select("ROUND_NO = '" + (roundNo - 1) + "'");

                    if (ichiranFilRows.Length <= 0)
                    {
                        // 回数-1条件に当てはまる業者・現場がなかった場合はブランクにて検索を行う
                        // ※現場定期品名一覧は業者・現場は必須項目のためブランク＝検索結果0となる
                        findStr = "((GYOUSHA_CD = '' AND GENBA_CD = '')";
                    }
                    else
                    {
                        foreach (var ichiranRow in ichiranFilRows)
                        {
                            // 明細に列挙されている全ての業者・現場を抽出条件とする
                            if (true == string.IsNullOrEmpty(findStr))
                            {
                                findStr = "((GYOUSHA_CD = '" + ichiranRow["GYOUSHA_CD"] + "' AND GENBA_CD = '" + ichiranRow["GENBA_CD"] + "')";
                            }
                            else
                            {
                                findStr += " OR (GYOUSHA_CD = '" + ichiranRow["GYOUSHA_CD"] + "' AND GENBA_CD = '" + ichiranRow["GENBA_CD"] + "')";
                            }
                        }
                    }

                    findStr += ")";

                    // 入力されている回数の業者・現場を抽出
                    ichiranTable = (DataTable)this.form.Ichiran.DataSource;
                    ichiranFilRows = ichiranTable.Select("ROUND_NO = '" + roundNo + "'");
                    if (ichiranFilRows.Length > 0)
                    {
                        // 品名抽出条件文字列
                        var hinmeiStr = string.Empty;

                        foreach (var ichiranRow in ichiranFilRows)
                        {
                            // 同回数の品名を抽出
                            var table = this.courseDetailItemsSearchResult;
                            var rows = table.Select("DAY_CD = " + ichiranRow["DAY_CD"] + " AND COURSE_NAME_CD = '" + ichiranRow["COURSE_NAME_CD"] + "' AND REC_ID = " + ichiranRow["REC_ID"]);

                            foreach (DataRow row in rows)
                            {
                                // 同回数で既に組み込まれている品名は除外する
                                if (true == string.IsNullOrEmpty(hinmeiStr))
                                {
                                    hinmeiStr = "(NOT(GYOUSHA_CD = '" + ichiranRow["GYOUSHA_CD"] + "' AND GENBA_CD = '" + ichiranRow["GENBA_CD"] + "' AND HINMEI_CD = '" + row["HINMEI_CD"] + "')";
                                }
                                else
                                {
                                    hinmeiStr += "AND NOT(GYOUSHA_CD = '" + ichiranRow["GYOUSHA_CD"] + "' AND GENBA_CD = '" + ichiranRow["GENBA_CD"] + "' AND HINMEI_CD = '" + row["HINMEI_CD"] + "')";
                                }
                            }
                        }

                        if (false == string.IsNullOrEmpty(hinmeiStr))
                        {
                            // 品名条件追加
                            findStr += (" AND " + hinmeiStr + ")");
                        }
                    }
                }
            }

            return findStr;
        }

        /// <summary>
        /// 回数作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns name="bool">TRUE:エラー有、FALSE:エラーなし</returns>
        internal bool createRoundNo(object sender, DataGridViewCellEventArgs e)
        {
            bool bRet = true;

            // 編集行のセット
            var row = this.form.Ichiran.Rows[e.RowIndex];

            // 編集列名のセット
            var colName = this.form.Ichiran.Columns[e.ColumnIndex].Name;

            var gyoushaCd = Convert.ToString(row.Cells["GYOUSHA_CD"].Value);
            var genbaCd = Convert.ToString(row.Cells["GENBA_CD"].Value);
            var roundNo = Convert.ToString(row.Cells["ROUND_NO"].Value);

            // 業者CDもしくは現場CDの変更があった場合
            if ((colName == "GYOUSHA_CD") || (colName == "GENBA_CD"))
            {
                // 入力値に変化があった場合
                if (!Convert.ToString(row.Cells[colName].Value).Equals(Convert.ToString(this.form.Ichiran.CellValidatingOldValue)))
                {
                    if (false == string.IsNullOrEmpty(roundNo))
                    {
                        bool edit = false;
                        if (colName == "GYOUSHA_CD")
                        {
                            // 業者CD編集時
                            edit = this.editCheck(Convert.ToString(this.form.Ichiran.CellValidatingOldValue), genbaCd, Int32.Parse(roundNo));
                        }
                        else
                        {
                            // 現場CD編集時
                            edit = this.editCheck(gyoushaCd, Convert.ToString(this.form.Ichiran.CellValidatingOldValue), Int32.Parse(roundNo));
                        }

                        // 編集行の業者・現場に合致する業者・現場を持つ行の回数が、編集行の回数以上であれば編集を取り消す
                        if (edit == false)
                        {
                            // エラー表示
                            this.showEditingError();
                            this.form.Ichiran.CancelEdit();
                            bRet = false;
                        }
                    }

                    if (bRet == true)
                    {
                        // 業者CD・現場CD双方に入力があった場合
                        if ((false == string.IsNullOrEmpty(gyoushaCd)) && (false == string.IsNullOrEmpty(genbaCd)))
                        {
                            // 回数を算出
                            var no = this.searchRoundNo(gyoushaCd, genbaCd);

                            // 新規追加チェック
                            if (false == this.addCheck(gyoushaCd, genbaCd, no, row.Index))
                            {
                                // 新規追加不可であれば、エラー表示
                                this.msgLogic.MessageBoxShowError("入力行以前に削除予定が存在します。\n追加する場合は該当のデータから再度編集を行ってください。");
                                this.form.Ichiran.CancelEdit();
                                bRet = false;
                            }
                            else
                            {
                                // 回数セット
                                row.Cells["ROUND_NO"].Value = no;
                            }
                        }
                        else
                        {
                            // 回数クリア
                            row.Cells["ROUND_NO"].Value = DBNull.Value;
                        }
                    }
                }
            }

            return bRet;
        }

        /// <summary>
        /// 回数作成(PopUpから戻ってきたとき用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns name="bool">TRUE:エラー有、FALSE:エラーなし</returns>
        internal bool createRoundNo()
        {
            bool bRet = true;

            try
            {
                DataGridViewRow row;
                DataGridViewCell cell;
                if (this.form.Ichiran.CurrentRow != null && this.form.Ichiran.CurrentCell != null)
                {
                    row = this.form.Ichiran.CurrentRow;
                    cell = this.form.Ichiran.CurrentCell;

                    // 編集列名のセット
                    var colName = this.form.Ichiran.Columns[cell.ColumnIndex].Name;

                    var gyoushaCd = row.Cells["GYOUSHA_CD"].Value.ToString();
                    var genbaCd = row.Cells["GENBA_CD"].Value.ToString();
                    var roundNo = row.Cells["ROUND_NO"].Value.ToString();

                    // 業者CDもしくは現場CDの変更があった場合
                    if ((colName == "GYOUSHA_CD") || (colName == "GENBA_CD"))
                    {
                        // 入力値に変化があった場合
                        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                        if ((colName == "GENBA_CD" && false == row.Cells["GENBA_CD"].Value.Equals(this.form.popupBeforeGenbaCd)) || (colName == "GYOUSHA_CD" && false == row.Cells["GYOUSHA_CD"].Value.Equals(this.form.popupBeforeGyoushaCd)))
                        // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                        {
                            // 業者CDに変化があった場合は現場をクリア
                            if (colName == "GYOUSHA_CD")
                            {
                                this.form.Ichiran["GENBA_CD", row.Index].Value = null;
                                this.form.Ichiran["GENBA_NAME_RYAKU", row.Index].Value = null;
                                if (this.form.Ichiran["GYOUSHA_CD", row.Index].Value == null)
                                {
                                    this.form.Ichiran["GYOUSHA_NAME_RYAKU", row.Index].Value = null;
                                }
                            }

                            if (false == string.IsNullOrEmpty(roundNo))
                            {
                                bool edit = false;
                                if (colName == "GYOUSHA_CD")
                                {
                                    // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                    // 業者CD編集時
                                    edit = this.editCheck(this.form.popupBeforeGyoushaCd, genbaCd, Int32.Parse(roundNo));
                                    // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                }
                                else
                                {
                                    // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                                    // 現場CD編集時
                                    edit = this.editCheck(gyoushaCd, this.form.popupBeforeGenbaCd, Int32.Parse(roundNo));
                                    // 20150924 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
                                }

                                // 編集行の業者・現場に合致する業者・現場を持つ行の回数が、編集行の回数以上であれば編集を取り消す
                                if (edit == false)
                                {
                                    // エラー表示
                                    this.showEditingError();
                                    this.form.Ichiran.CancelEdit();
                                    bRet = false;
                                }
                            }

                            if (bRet == true)
                            {
                                // 業者CD・現場CD双方に入力があった場合
                                if ((false == string.IsNullOrEmpty(gyoushaCd)) && (false == string.IsNullOrEmpty(genbaCd)))
                                {
                                    // 回数を算出
                                    var no = this.searchRoundNo(gyoushaCd, genbaCd);

                                    // 新規追加チェック
                                    if (false == this.addCheck(gyoushaCd, genbaCd, no, row.Index))
                                    {
                                        // 新規追加不可であれば、エラー表示
                                        this.msgLogic.MessageBoxShowError("入力行以前に削除予定が存在します。\n追加する場合は該当のデータから再度編集を行ってください。");
                                        this.form.Ichiran.CancelEdit();
                                        bRet = false;
                                    }
                                    else
                                    {
                                        // 回数セット
                                        row.Cells["ROUND_NO"].Value = no;
                                    }
                                }
                                else
                                {
                                    // 回数クリア
                                    row.Cells["ROUND_NO"].Value = DBNull.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("createRoundNo", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                bRet = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createRoundNo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 回数算出
        /// </summary>
        /// <param name="gyoushaCD">キーとなる業者CD</param>
        /// <param name="genbaCD">キーとなる現場CD</param>
        /// <returns name="Int32">回数</returns>
        /// <remarks>キーとなる業者・現場CDと割当枠の情報から回数を算出する</remarks>
        private Int32 searchRoundNo(string gyoushaCD, string genbaCD)
        {
            Int32 roundNo = 1;

            var table = (DataTable)this.form.Ichiran.DataSource;

            var list = table.Select("GYOUSHA_CD = '" + gyoushaCD + "' AND GENBA_CD = '" + genbaCD + "'");

            int no = 0;
            if (list != null && list.Length > 0)
            {
                // 該当情報が存在すれば、回数の最大値+1をInsert対象の回数とする
                foreach (DataRow r in list)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(r["ROUND_NO"]))
                        && Int32.TryParse(Convert.ToString(r["ROUND_NO"]), out no)
                        && roundNo <= no)
                    {
                        roundNo = no;
                    }
                }
                roundNo += 1;
            }

            return roundNo;
        }

        #endregion

        /// <summary>
        /// 新規追加チェック
        /// </summary>
        /// <param name="gyoushaCD">キーとなる業者CD</param>
        /// <param name="genbaCD">キーとなる現場CD</param>
        /// <param name="roundNo">キーとなる回数</param>
        /// <returns name="bool">TRUE:新規追加許可、FALSE:新規追加不可</returns>
        /// <remarks>
        /// キーとなる業者・現場CDの回数以下の回数を持った業者・現場が
        /// 既に削除チェックが付与されていた場合、追加を取り消す
        /// </remarks>
        private bool addCheck(string gyoushaCD, string genbaCD, Int32 roundNo, Int32 rowIndex)
        {
            bool bRet = true;

            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                if (row.Index == rowIndex || row.IsNewRow)
                {
                    continue;
                }
                if (gyoushaCD == row.Cells["GYOUSHA_CD"].Value.ToString())
                {
                    if (genbaCD == row.Cells["GENBA_CD"].Value.ToString())
                    {
                        if (false == string.IsNullOrEmpty(row.Cells["ROUND_NO"].Value.ToString()))
                        {
                            // 業者・現場合致、かつ削除予定行の回数がキー回数より小さければ、追加取り消し
                            if (roundNo <= Int32.Parse(row.Cells["ROUND_NO"].Value.ToString()))
                            {
                                // 新規追加不可
                                bRet = false;
                                break;
                            }
                        }
                    }
                }
            }

            return bRet;
        }

        /// <summary>
        /// 編集チェック
        /// </summary>
        /// <param name="gyoushaCD">キーとなる業者CD</param>
        /// <param name="genbaCD">キーとなる現場CD</param>
        /// <param name="roundNo">キーとなる回数</param>
        /// <returns name="bool">TRUE:修正許可、FALSE:修正不可</returns>
        /// <remarks>
        /// キーとなる業者・現場CDの回数以上の回数を持った業者・現場が存在した場合
        /// 修正不可とするようチェックを行う
        /// </remarks>
        private bool editCheck(string gyoushaCD, string genbaCD, Int32 roundNo)
        {
            bool bRet = true;

            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                if (gyoushaCD == row.Cells["GYOUSHA_CD"].Value.ToString())
                {
                    if (genbaCD == row.Cells["GENBA_CD"].Value.ToString())
                    {
                        if (false == string.IsNullOrEmpty(row.Cells["ROUND_NO"].Value.ToString()))
                        {
                            // 業者・現場合致行の回数がキー回数より大きければ、修正不可
                            if (roundNo < Int32.Parse(row.Cells["ROUND_NO"].Value.ToString()))
                            {
                                // 修正不可
                                bRet = false;
                                break;
                            }
                        }
                    }
                }
            }

            return bRet;
        }

        /// <summary>
        /// 削除フラグチェックボックス変更時処理
        /// </summary>
        /// <param name="row">変更した削除チェックボックスの編集行</param>
        /// <returns name="bool">TRUE:修正許可、FALSE:修正不可</returns>
        internal bool deleteCheck(DataGridViewRow row)
        {
            bool bRet = true;

            // 編集行の値セット
            var gyoushaCd = row.Cells["GYOUSHA_CD"].Value.ToString();
            var genbaCd = row.Cells["GENBA_CD"].Value.ToString();
            var roundNo = row.Cells["ROUND_NO"].Value.ToString();

            if (false == string.IsNullOrEmpty(roundNo))
            {
                // 編集行の業者・現場に合致する業者・現場を持つ行の回数が、編集行の回数以上であれば編集を取り消す
                if (false == this.editCheck(gyoushaCd, genbaCd, Int32.Parse(roundNo)))
                {
                    // エラー表示
                    this.showEditingError();
                    bRet = false;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 編集禁止メッセージ表示
        /// </summary>
        private void showEditingError()
        {
            this.msgLogic.MessageBoxShowError("入力行以降に回収予定が存在するため変更できません。");
        }

        /// <summary>
        /// オブジェクトをboolに変換します
        /// </summary>
        /// <param name="value">変換対象のオブジェクト</param>
        /// <returns>変換後の値（nullはfalse）</returns>
        private bool ConvertToBool(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = false;

            if (null != value)
            {
                if (false == bool.TryParse(value.ToString(), out ret))
                {
                    if (value.Equals(1))
                    {
                        ret = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 業者CDの前回値チェックをします
        /// </summary>
        internal void GyoushaCdBeforeCheck()
        {
            // 業者CDが前回値と異なる場合
            if (this.oldValueDic["gyoushaCdTextBox"] != this.form.gyoushaCdTextBox.Text)
            {
                // 現場情報をクリア
                this.form.genbaCdTextBox.Text = string.Empty;
                this.form.genbaNameRyakuTextBox.Text = string.Empty;
            }
        }

        #region - 荷降行削除 -
        /// <summary>
        /// 荷降行削除処理
        /// </summary>
        internal bool deleteNioroshiRow()
        {
            bool ret = true;
            try
            {
                if (null != this.form.customDataGridView_M_COURSE_NIOROSHI.CurrentRow && false == this.form.customDataGridView_M_COURSE_NIOROSHI.CurrentRow.IsNewRow)
                {
                    DataGridViewRow row = this.form.customDataGridView_M_COURSE_NIOROSHI.CurrentRow;
                    string nioroshiNo = Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value);
                    if (string.IsNullOrEmpty(nioroshiNo))
                    {
                        this.form.customDataGridView_M_COURSE_NIOROSHI.Rows.Remove(this.form.customDataGridView_M_COURSE_NIOROSHI.CurrentRow);
                        return ret;
                    }

                    DataRow[] rows = this.courseDetailItemsSearchResult.Select(string.Format("NIOROSHI_NUMBER = '{0}'", nioroshiNo));
                    if (rows == null || rows.Length == 0)
                    {
                        this.form.customDataGridView_M_COURSE_NIOROSHI.Rows.Remove(this.form.customDataGridView_M_COURSE_NIOROSHI.CurrentRow);

                        if (this.form.customDataGridView_M_COURSE_NIOROSHI.Rows.Contains(row) && string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value)))
                        {
                            this.form.customDataGridView_M_COURSE_NIOROSHI.Rows.Remove(this.form.customDataGridView_M_COURSE_NIOROSHI.CurrentRow);
                        }
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShow("E123");
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("deleteNioroshiRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion - 荷降行削除 -

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        internal DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

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
                LogUtility.Error("CheckSagyouTime", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                result = false;
            }
            return result;
        }

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity_CHANGE_LOG(bool isDelete)
        {
            bool catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                this.changeCourseEntitys = new List<CHANGE_LOG_M_COURSE>();
                this.changeCourseDetailEntitys = new List<CHANGE_LOG_M_COURSE_DETAIL>();
                this.changeCourseDetailItemsEntitys = new List<CHANGE_LOG_M_COURSE_DETAIL_ITEMS>();
                this.changeCourseNioroshiEntitys = new List<CHANGE_LOG_M_COURSE_NIOROSHI>();

                if (this.courseSearchResult == null || this.courseSearchResult.Rows.Count == 0)
                {
                    return catchErr;
                }

                string dayCd = string.Empty;
                string courseNameCd = string.Empty;

                M_COURSE systemData = new M_COURSE();
                var binder = new DataBinderLogic<M_COURSE>(systemData);
                binder.SetSystemProperty(systemData, false);
                systemData = binder.Entitys[0];

                foreach (DataRow row in this.courseSearchResult.Rows)
                {
                    dayCd = Convert.ToString(row["DAY_CD"]);
                    courseNameCd = Convert.ToString(row["COURSE_NAME_CD"]);
                    M_COURSE course = this.courseDao.GetDataByCd(dayCd, courseNameCd);
                    if (course == null)
                    {
                        continue;
                    }

                    CHANGE_LOG_M_COURSE entity = new CHANGE_LOG_M_COURSE();
                    entity = Copy(course);
                    entity.UPDATE_DATE = systemData.UPDATE_DATE;
                    entity.UPDATE_USER = systemData.UPDATE_USER;
                    entity.UPDATE_PC = systemData.UPDATE_PC;
                    this.changeCourseEntitys.Add(entity);

                    M_COURSE_DETAIL detail = new M_COURSE_DETAIL();
                    detail.DAY_CD = course.DAY_CD;
                    detail.COURSE_NAME_CD = course.COURSE_NAME_CD;
                    List<M_COURSE_DETAIL> courseDetails = this.courseDetailDao.GetDataForEntity(detail);
                    AddToList(courseDetails);

                    M_COURSE_DETAIL_ITEMS item = new M_COURSE_DETAIL_ITEMS();
                    item.DAY_CD = course.DAY_CD;
                    item.COURSE_NAME_CD = course.COURSE_NAME_CD;
                    List<M_COURSE_DETAIL_ITEMS> detailItems = this.courseDetailItemsDao.GetDataForEntity(item);
                    AddToList(detailItems);

                    M_COURSE_NIOROSHI nioroshi = new M_COURSE_NIOROSHI();
                    nioroshi.DAY_CD = course.DAY_CD;
                    nioroshi.COURSE_NAME_CD = course.COURSE_NAME_CD;
                    List<M_COURSE_NIOROSHI> courseNioroshis = this.courseNioroshiDao.GetDataForEntity(nioroshi);
                    AddToList(courseNioroshis);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity_CHANGE_LOG", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(true, catchErr);
            return catchErr;
        }

        #endregion

        /// <summary>
        /// 画面の曜日、コース名CDをEntityに反映します
        /// </summary>
        /// <returns></returns>
        public bool UpdateEntiry()
        {
            bool catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                SqlInt16 dayCd = SqlInt16.Parse(this.form.customTextBoxDayCd.Text);
                string courseNameCd = this.form.customTextBoxCoureseNameCd.Text;

                foreach (M_COURSE entity in this.courseEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;

                    // TIME_STAMP
                    if (this.fukushaCourseSearchResult != null && this.fukushaCourseSearchResult.Rows.Count > 0)
                    {
                        var dr = this.fukushaCourseSearchResult.Rows[0];
                        if (!DBNull.Value.Equals(dr["TIME_STAMP"]))
                        {
                            entity.TIME_STAMP = (byte[])dr["TIME_STAMP"];
                        }
                    }
                }

                foreach (M_COURSE_NIOROSHI entity in this.courseNioroshiEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }

                foreach (M_COURSE_DETAIL entity in this.courseDetailEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }

                foreach (M_COURSE_DETAIL_ITEMS entity in this.courseDetailItemsEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }

                foreach (CHANGE_LOG_M_COURSE entity in this.changeCourseEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }

                foreach (CHANGE_LOG_M_COURSE_DETAIL entity in this.changeCourseDetailEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }

                foreach (CHANGE_LOG_M_COURSE_DETAIL_ITEMS entity in this.changeCourseDetailItemsEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }

                foreach (CHANGE_LOG_M_COURSE_NIOROSHI entity in this.changeCourseNioroshiEntitys)
                {
                    entity.DAY_CD = dayCd;
                    entity.COURSE_NAME_CD = courseNameCd;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateEntiry", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(catchErr);
            return catchErr;
        }

        /// <summary>
        /// コントロールから対象のEntityをコピー作成する
        /// </summary>
        public CHANGE_LOG_M_COURSE Copy(M_COURSE entity)
        {
            CHANGE_LOG_M_COURSE course = new CHANGE_LOG_M_COURSE();
            if (entity == null)
            {
                return course;
            }
            course.SYSTEM_ID = GetSystemId();
            course.DAY_CD = entity.DAY_CD;
            course.COURSE_NAME_CD = entity.COURSE_NAME_CD;
            course.COURSE_BIKOU = entity.COURSE_BIKOU;
            course.SAGYOU_BEGIN_HOUR = entity.SAGYOU_BEGIN_HOUR;
            course.SAGYOU_BEGIN_MINUTE = entity.SAGYOU_BEGIN_MINUTE;
            course.SAGYOU_END_HOUR = entity.SAGYOU_END_HOUR;
            course.SAGYOU_END_MINUTE = entity.SAGYOU_END_MINUTE;
            course.SHASHU_CD = entity.SHASHU_CD;
            course.SHARYOU_CD = entity.SHARYOU_CD;
            course.UNTENSHA_CD = entity.UNTENSHA_CD;
            course.UNPAN_GYOUSHA_CD = entity.UNPAN_GYOUSHA_CD;
            course.SHUPPATSU_GYOUSHA_CD = entity.SHUPPATSU_GYOUSHA_CD;
            course.SHUPPATSU_GENBA_CD = entity.SHUPPATSU_GENBA_CD;
            course.CREATE_USER = entity.CREATE_USER;
            course.CREATE_DATE = entity.CREATE_DATE;
            course.CREATE_PC = entity.CREATE_PC;
            course.DELETE_FLG = false;
            return course;
        }

        /// <summary>
        /// コントロールから対象のEntityをコピー作成する
        /// </summary>
        public void AddToList(List<M_COURSE_DETAIL> entitys)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return;
            }

            foreach (M_COURSE_DETAIL entity in entitys)
            {
                CHANGE_LOG_M_COURSE_DETAIL detail = new CHANGE_LOG_M_COURSE_DETAIL();
                detail.SYSTEM_ID = GetSystemId();
                detail.DAY_CD = entity.DAY_CD;
                detail.COURSE_NAME_CD = entity.COURSE_NAME_CD;
                detail.REC_ID = entity.REC_ID;
                detail.ROW_NO = entity.ROW_NO;
                detail.ROUND_NO = entity.ROUND_NO;
                detail.GYOUSHA_CD = entity.GYOUSHA_CD;
                detail.GENBA_CD = entity.GENBA_CD;
                detail.KIBOU_TIME = entity.KIBOU_TIME;
                detail.SAGYOU_TIME_MINUTE = entity.SAGYOU_TIME_MINUTE;
                detail.BIKOU = entity.BIKOU;
                changeCourseDetailEntitys.Add(detail);
            }
            return;
        }

        /// <summary>
        /// コントロールから対象のEntityをコピー作成する
        /// </summary>
        public void AddToList(List<M_COURSE_DETAIL_ITEMS> entitys)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return;
            }

            foreach (M_COURSE_DETAIL_ITEMS entity in entitys)
            {
                CHANGE_LOG_M_COURSE_DETAIL_ITEMS item = new CHANGE_LOG_M_COURSE_DETAIL_ITEMS();
                item.SYSTEM_ID = GetSystemId();
                item.DAY_CD = entity.DAY_CD;
                item.COURSE_NAME_CD = entity.COURSE_NAME_CD;
                item.REC_ID = entity.REC_ID;
                item.REC_SEQ = entity.REC_SEQ;
                item.INPUT_KBN = entity.INPUT_KBN;
                item.HINMEI_CD = entity.HINMEI_CD;
                item.UNIT_CD = entity.UNIT_CD;
                item.KANSANCHI = entity.KANSANCHI;
                item.KANSAN_UNIT_CD = entity.KANSAN_UNIT_CD;
                item.ANBUN_FLG = entity.ANBUN_FLG;
                item.KEIYAKU_KBN = entity.KEIYAKU_KBN;
                item.KEIJYOU_KBN = entity.KEIJYOU_KBN;
                item.NIOROSHI_NO = entity.NIOROSHI_NO;
                item.TEKIYOU_BEGIN = entity.TEKIYOU_BEGIN;
                item.TEKIYOU_END = entity.TEKIYOU_END;
                item.DENPYOU_KBN_CD = entity.DENPYOU_KBN_CD;
                item.KANSAN_UNIT_MOBILE_OUTPUT_FLG = entity.KANSAN_UNIT_MOBILE_OUTPUT_FLG;
                changeCourseDetailItemsEntitys.Add(item);
            }
            return;
        }

        /// <summary>
        /// コントロールから対象のEntityをコピー作成する
        /// </summary>
        public void AddToList(List<M_COURSE_NIOROSHI> entitys)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return;
            }

            foreach (M_COURSE_NIOROSHI entity in entitys)
            {
                CHANGE_LOG_M_COURSE_NIOROSHI nioroshi = new CHANGE_LOG_M_COURSE_NIOROSHI();
                nioroshi.SYSTEM_ID = GetSystemId();
                nioroshi.DAY_CD = entity.DAY_CD;
                nioroshi.COURSE_NAME_CD = entity.COURSE_NAME_CD;
                nioroshi.NIOROSHI_NO = entity.NIOROSHI_NO;
                nioroshi.NIOROSHI_GYOUSHA_CD = entity.NIOROSHI_GYOUSHA_CD;
                nioroshi.NIOROSHI_GENBA_CD = entity.NIOROSHI_GENBA_CD;
                changeCourseNioroshiEntitys.Add(nioroshi);
            }
            return;
        }

        /// <summary>
        /// システムIDの最大値+1を取得する
        /// </summary>
        /// <returns>システムID</returns>
        public long GetSystemId()
        {
            long systemId = 1;

            using (Transaction tran = new Transaction())
            {
                // 処理区分：270（コース）
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.COURSE;

                // テーブルロックをかけつつ、既存データがあるかを検索する。
                var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                systemId = this.numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.COURSE;
                    updateEntity.CURRENT_NUMBER = systemId;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = systemId;
                    this.numberSystemDao.Update(updateEntity);
                }
                tran.Commit();
            }

            return systemId;
        }

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

                foreach (DataGridViewRow row in this.form.customDataGridView_M_COURSE_NIOROSHI.Rows)
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

                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
                {
                    if (row.IsNewRow ||
                        string.IsNullOrEmpty(Convert.ToString(row.Cells[ConstCls.DetailColName.NO].Value)))
                    {
                        continue;
                    }

                    int dayCd = (short)row.Cells["DAY_CD"].Value;
                    string courseNameCd = Convert.ToString(row.Cells["COURSE_NAME_CD"].Value);
                    int recId = (int)row.Cells["REC_ID"].Value;
                    string gyoushaCd = Convert.ToString(row.Cells["GYOUSHA_CD"].Value);
                    string genbaCd = Convert.ToString(row.Cells["GENBA_CD"].Value);

                    DataView dv = this.courseDetailItemsSearchResult.DefaultView;
                    dv.RowFilter = "DAY_CD = " + dayCd.ToString() + " AND COURSE_NAME_CD = '" + courseNameCd + "' AND REC_ID = " + recId.ToString();
                    dv.Sort = "HINMEI_CD ASC";

                    DataTable dt = dv.ToTable();
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        continue;
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        detailDto = new DetailDto();
                        detailDto.TABLE_NAME = row.Index.ToString();
                        detailDto.ROW_NO = row.Index;
                        detailDto.ROUND_NO = Convert.ToString(row.Cells[ConstCls.DetailColName.ROUND_NO].Value);
                        detailDto.GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value);
                        detailDto.GYOUSHA_NAME = Convert.ToString(row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value);
                        detailDto.GENBA_CD = Convert.ToString(row.Cells[ConstCls.DetailColName.GENBA_CD].Value);
                        detailDto.GENBA_NAME = Convert.ToString(row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value);

                        detailDto.HINMEI_CD = Convert.ToString(dr[ConstCls.ShousaiColName.HINMEI_CD]);
                        detailDto.HINMEI_NAME = Convert.ToString(dr[ConstCls.ShousaiColName.HINMEI_NAME_RYAKU]);
                        detailDto.UNIT_CD = Convert.ToString(dr[ConstCls.ShousaiColName.UNIT_CD]);
                        detailDto.UNIT_NAME = Convert.ToString(dr[ConstCls.ShousaiColName.UNIT_NAME]);
                        detailDto.NIOROSHI_NUMBER_DETAIL = Convert.ToString(dr[ConstCls.ShousaiColName.NIOROSHI_NUMBER]);
                        detailDto.TABLE_ROW_NO = i;
                        detailDto.isRenkei = false;
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

                    List<DetailDto> dtoRetList = new List<DetailDto>();

                    // 返却結果繰り返す
                    foreach (string strTableName in retList.Keys)
                    {
                        dtoRetList = retList[strTableName];
                        DataGridViewRow row = this.form.Ichiran.Rows[dtoRetList[0].ROW_NO];
                        int dayCd = (short)row.Cells["DAY_CD"].Value;
                        string courseNameCd = (string)row.Cells["COURSE_NAME_CD"].Value;
                        int recId = (int)row.Cells["REC_ID"].Value;
                        string gyoushaCd = (string)row.Cells["GYOUSHA_CD"].Value;
                        string genbaCd = (string)row.Cells["GENBA_CD"].Value;

                        DataView dv = this.courseDetailItemsSearchResult.DefaultView;
                        dv.RowFilter = "DAY_CD = " + dayCd.ToString() + " AND COURSE_NAME_CD = '" + courseNameCd + "' AND REC_ID = " + recId.ToString();
                        dv.Sort = "HINMEI_CD ASC";

                        foreach (DetailDto dtoRet in dtoRetList)
                        {
                            if (!string.IsNullOrEmpty(dtoRet.NIOROSHI_NUMBER_DETAIL))
                            {
                                dv[dtoRet.TABLE_ROW_NO][ConstCls.ShousaiColName.NIOROSHI_NUMBER] = dtoRet.NIOROSHI_NUMBER_DETAIL;
                            }
                            else
                            {
                                dv[dtoRet.TABLE_ROW_NO][ConstCls.ShousaiColName.NIOROSHI_NUMBER] = DBNull.Value;
                            }

                        }
                    }
                    this.form.resetKaisyuuhinmei();
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowNioroshiIkkatsu", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region NAVITIME追加項目
        /// <summary>
        /// 出発業者CDの前回値チェックをします
        /// </summary>
        internal void ShuppatsuGyoushaCdBeforeCheck()
        {
            // 出発業者CDが前回値と異なる場合
            if (this.oldValueDic["SHUPPATSU_GYOUSHA_CD"] != this.form.SHUPPATSU_GYOUSHA_CD.Text)
            {
                // 出発現場情報をクリア
                this.form.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.form.SHUPPATSU_GENBA_NAME.Text = string.Empty;
            }
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
                    this.msgLogic.MessageBoxShow("E084", value);
                    //this.isInputError = true;
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

        /// <summary>
        /// 出発業者現場データソース更新
        /// </summary>
        internal void shuppatsuDataSourceUpdate()
        {
            if ((this.courseSearchResult != null) && (this.courseSearchResult.Rows.Count > 0))
            {
                // データソース更新
                this.form.SHUPPATSU_GYOUSHA_CD.DataBindings["Text"].WriteValue();
                this.form.SHUPPATSU_GYOUSHA_NAME.DataBindings["Text"].WriteValue();
                this.form.SHUPPATSU_GENBA_CD.DataBindings["Text"].WriteValue();
                this.form.SHUPPATSU_GENBA_NAME.DataBindings["Text"].WriteValue();
            }
        }

        #endregion

        #region mapbox追加項目

        #region mapbox連携

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        internal List<mapDtoList> createMapboxDto()
        {
            try
            {
                int layerId = 0;

                List<mapDtoList> dtoLists = new List<mapDtoList>();

                // レイヤー追加
                mapDtoList dtoList = new mapDtoList();

                List<mapDto> dtos = new List<mapDto>();

                StringBuilder sb = new StringBuilder();
                DataTable dt = null;

                // 地図出力に必要な情報を収集
                #region 明細1件のコースの内容を取得する

                #region 組込明細の情報

                // 組み込み明細をDTOにセット
                for (int i = 0; i < this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows.Count; i++)
                {
                    if (this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].IsNewRow)
                    {
                        continue;
                    }
                    // 組込がONの明細のみ対象
                    if (!this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].Cells["M_GENBA_TEIKI_HINMEI_KUMIKOMI"].Value.Equals(1))
                    {
                        continue;
                    }

                    // 地図出力に必要な情報を収集
                    GetGenbaDtoCls Serch = new GetGenbaDtoCls();
                    Serch.GENBA_CD = this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].Cells["M_GENBA_TEIKI_HINMEI_GENBA_CD"].Value.ToString();
                    Serch.GYOUSHA_CD = this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].Cells["M_GENBA_TEIKI_HINMEI_GYOUSHA_CD"].Value.ToString();

                    dt = GetMapGenbaDao.GetMapDataForEntity(Serch);
                    if (dt.Rows.Count > 0)
                    {
                        //layerId = 1;
                        dtoList.layerId = layerId;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = string.Empty;
                            dto.dayName = string.Empty;
                            dto.teikiHaishaNo = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = string.Empty;
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = string.Empty;
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstCls.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.GENBA_POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS1])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.GENBA_TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.hinmei = this.form.customDataGridView_M_GENBA_TEIKI_HINMEI.Rows[i].Cells["M_GENBA_TEIKI_HINMEI_HINMEI_NAME_RYAKU"].Value.ToString();
                            dto.genbaChaku = string.Empty;
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LONGITUDE]);
                            dto.NoCount = true;
                            dtos.Add(dto);
                        }
                    }
                    // 1コース終わったらリストにセット
                    dtoList.dtos = dtos;
                }
                if (dtoList.dtos != null)
                {
                    if (dtoList.dtos.Count != 0)
                    {
                        dtoLists.Add(dtoList);
                    }
                }

                #endregion

                #region 明細の情報

                dtoList = new mapDtoList();
                dtos = new List<mapDto>();

                layerId++;
                dtoList.layerId = layerId;

                // 地図出力に必要な情報を収集
                GetGenbaDtoCls SerchMeisai = new GetGenbaDtoCls();

                // 出発業者のみ、または出発業者と出発現場が設定されている場合、コースの先頭とする。
                if (!string.IsNullOrEmpty(this.form.SHUPPATSU_GYOUSHA_CD.Text) && string.IsNullOrEmpty(this.form.SHUPPATSU_GENBA_CD.Text))
                {
                    SerchMeisai.GYOUSHA_CD = this.form.SHUPPATSU_GYOUSHA_CD.Text;
                    dt = GetMapGenbaDao.GetMapGyoushaDataForEntity(SerchMeisai);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = this.form.customTextBoxCoureseName.Text;
                            dto.dayName = mapLogic.SetDayNameByCd(this.form.customTextBoxDayCd.Text);
                            dto.teikiHaishaNo = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_CD]);
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = string.Empty;
                            dto.genbaName = string.Empty;
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_ADDRESS1])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.rowNo = 0;
                            dto.roundNo = 0;
                            dto.hinmei = string.Empty;
                            dto.genbaChaku = string.Empty;
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_LONGITUDE]);
                            dto.shuppatsuFlag = true;
                            dtos.Add(dto);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(this.form.SHUPPATSU_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.SHUPPATSU_GENBA_CD.Text))
                {
                    SerchMeisai.GENBA_CD = this.form.SHUPPATSU_GENBA_CD.Text;
                    SerchMeisai.GYOUSHA_CD = this.form.SHUPPATSU_GYOUSHA_CD.Text;
                    dt = GetMapGenbaDao.GetMapDataForEntity(SerchMeisai);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = this.form.customTextBoxCoureseName.Text;
                            dto.dayName = mapLogic.SetDayNameByCd(this.form.customTextBoxDayCd.Text);
                            dto.teikiHaishaNo = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_CD]);
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = Convert.ToString(dt.Rows[j][ConstCls.GENBA_CD]);
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstCls.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.GENBA_POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS1])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.GENBA_TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.rowNo = 0;
                            dto.roundNo = 0;
                            dto.hinmei = string.Empty;
                            dto.genbaChaku = string.Empty;
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LONGITUDE]);
                            dto.shuppatsuFlag = true;
                            dtos.Add(dto);
                        }
                    }

                }

                // コース明細をDTOにセット
                for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                {
                    if (this.form.Ichiran.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["GYOUSHA_CD"].Value.ToString()) ||
                        string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["GENBA_CD"].Value.ToString()))
                    {
                        continue;
                    }

                    // 地図出力に必要な情報を収集
                    GetGenbaDtoCls Serch = new GetGenbaDtoCls();
                    Serch.GENBA_CD = this.form.Ichiran.Rows[i].Cells["GENBA_CD"].Value.ToString();
                    Serch.GYOUSHA_CD = this.form.Ichiran.Rows[i].Cells["GYOUSHA_CD"].Value.ToString();

                    dt = GetMapGenbaDao.GetMapDataForEntity(Serch);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = this.form.customTextBoxCoureseName.Text;
                            dto.dayName = mapLogic.SetDayNameByCd(this.form.customTextBoxDayCd.Text);
                            dto.teikiHaishaNo = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_CD]);
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstCls.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = Convert.ToString(dt.Rows[j][ConstCls.GENBA_CD]);
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstCls.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstCls.GENBA_POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstCls.TODOUFUKEN_NAME])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS1])
                                        + Convert.ToString(dt.Rows[j][ConstCls.GENBA_ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstCls.GENBA_TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstCls.BIKOU2]);
                            dto.rowNo = Convert.ToInt32(this.form.Ichiran.Rows[i].Cells["ROW_NO2"].Value);
                            dto.roundNo = Convert.ToInt32(this.form.Ichiran.Rows[i].Cells["ROUND_NO"].Value);
                            dto.hinmei = this.form.Ichiran.Rows[i].Cells["KAISYUUHIN_NAME"].Value.ToString();
                            string time = Convert.ToString(this.form.Ichiran.Rows[i].Cells["KIBOU_TIME"].Value);
                            dto.genbaChaku = time;
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstCls.GENBA_LONGITUDE]);
                            dto.shuppatsuFlag = false;
                            dtos.Add(dto);
                        }
                    }
                    // 1コース終わったらリストにセット
                    dtoList.dtos = dtos;
                }
                if (dtoList.dtos != null)
                {
                    if (dtoList.dtos.Count != 0)
                    {
                        dtoLists.Add(dtoList);
                    }
                }
                #endregion

                #endregion

                return dtoLists;
            }
            catch (Exception ex)
            {
                LogUtility.Error("createMapboxDto", ex);
                this.msgLogic.MessageBoxShowError(ex.Message);
                return null;
            }
        }

        #endregion

        #endregion
    }
}