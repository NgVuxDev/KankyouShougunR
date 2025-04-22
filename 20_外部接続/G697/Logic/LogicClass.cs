using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

//メモ
//CSVファイルは、ログと同じディレクトリに保存しています

namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
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
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.HaisouKeikakuTeiki.Setting.ButtonSetting.xml";

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        #region 定数
        /// <summary>
        /// 連携状況(1.未送信)
        /// </summary>
        private const int RENKEI_MISOUSHIN = 1;
        /// <summary>
        /// 連携状況(2.送信済)
        /// </summary>
        private const int RENKEI_SOUSHIN = 2;
        /// <summary>
        /// 連携状況(3.受信済)
        /// </summary>
        private const int RENKEI_JYUSHIN = 3;

        /// <summary>
        /// 最適化対象(1.コースマスタ)
        /// </summary>
        private const int RENKEI_TAISHOU_COURSE = 1;
        /// <summary>
        /// 最適化対象(2.定期配車伝票)
        /// </summary>
        private const int RENKEI_TAISHOU_TEIKI = 2;
        #endregion

        #region Dao

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// コース用
        /// </summary>
        private DAOClass dao;

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

        /// <summary>
        /// 社員Dao
        /// </summary>
        private IM_SHAINDao shainDao;

        /// <summary>
        /// 配車計画一時テーブル
        /// </summary>
        private NaviCollaboEventDAO naviCollaboEventDao;

        /// <summary>
        /// 配車計画
        /// </summary>
        private NaviDeliveryDAO naviDeliveryDao;

        /// <summary>
        /// 配車計画連携情報管理
        /// </summary>
        private NaviLinkStatusDAO naviLinkStatusDao;

        /// <summary>
        /// 社員出力済みDao
        /// </summary>
        internal IM_NAVI_OUTPUT_SHAINDao naviShainDao;
        /// <summary>
        /// 現場出力済みDao
        /// </summary>
        private IM_NAVI_OUTPUT_GENBADao naviGenbaDao;
        /// <summary>
        /// 出発現場、荷降現場出力済みDao
        /// </summary>
        internal IM_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBADao naviShuppatsuGenbaDao;
        /// <summary>
        /// 車種出力済みDao
        /// </summary>
        private IM_NAVI_OUTPUT_SHASHUDao naviShashuDao;

        /// <summary>
        /// 伝種採番
        /// </summary>
        r_framework.Dao.IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// NAVITIME接続管理Dao
        /// </summary>
        private IS_NAVI_CONNECTDao naviConnectDao;

        #endregion

        #endregion

        #region プロパティ

        /// <summary>
        /// チェックを付けた行を保持するためのDto
        /// </summary>
        internal List<NaviCheckDetail> cDtoList { get; set; }
        private NaviCheckDetail cDto { get; set; }

        /// <summary>
        /// 登録用DTO
        /// </summary>
        internal NaviDeliveryDTO RegistDTO { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable SearchResult { get; set; }

        /// <summary>
        /// コース名称データ
        /// </summary>
        internal M_COURSE_NAME[] mCourseNameAll { get; set; }

        /// <summary>
        /// １便に紐付く案件コードのリスト
        /// </summary>
        private List<SqlInt64> MatterNoList { get; set; }

        /// <summary>
        /// 連携状況フラグ
        /// </summary>
        internal int renkei_kbn { get; set; }

        /// <summary>
        /// 最適化対象
        /// </summary>
        internal int renkei_taishou { get; set; }

        /// <summary>
        /// 抽出条件
        /// </summary>
        private SearchDto searchDto { get; set; }

        /// <summary>
        /// エラー時にポップアップに表示するDataTable
        /// </summary>
        private DataTable popTable { get; set; }

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

            // DAO
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.shainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.naviCollaboEventDao = DaoInitUtility.GetComponent<NaviCollaboEventDAO>();
            this.naviDeliveryDao = DaoInitUtility.GetComponent<NaviDeliveryDAO>();
            this.naviLinkStatusDao = DaoInitUtility.GetComponent<NaviLinkStatusDAO>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.naviShainDao = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_SHAINDao>();
            this.naviGenbaDao = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_GENBADao>();
            this.naviShuppatsuGenbaDao = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBADao>();
            this.naviShashuDao = DaoInitUtility.GetComponent<IM_NAVI_OUTPUT_SHASHUDao>();
            this.naviConnectDao = DaoInitUtility.GetComponent<IS_NAVI_CONNECTDao>();

            // DTO
            this.cDto = new NaviCheckDetail();
            this.cDtoList = new List<NaviCheckDetail>();
            this.searchDto = new SearchDto();

            // メッセージ用
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
            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();

            // 親フォームのボタン表示
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 一括設定(F1)イベント作成
            parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);
            // 削除(F4)イベント作成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            // 条件クリア(F7)イベント作成
            parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);
            // 検索ボタン(F8)イベント作成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            // データ連携ボタン(F9)イベント作成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            // 閉じるボタン(F12)イベント作成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);
            // コース最適化ボタンイベント作成
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //連携イベント作成
            this.form.txtNum_RenkeiJyoukyou.TextChanged += new EventHandler(this.form.txtNum_RenkeiJyoukyou_TextChanged);
            this.form.txtNum_RenkeiJyoukyou.Leave += new EventHandler(this.form.txtNum_RenkeiJyoukyou_Leave);

            this.form.txtNum_RenkeiTaishou.TextChanged += new EventHandler(this.form.txtNum_RenkeiTaishou_TextChanged);
            this.form.txtNum_RenkeiTaishou.Leave += new EventHandler(this.form.txtNum_RenkeiTaishou_Leave);
        }

        /// <summary>
        /// 抽出条件の初期化
        /// </summary>
        internal void SetInitialRenkeiCondition()
        {
            this.SetUserProfile();

            // 最適化対象
            this.form.txtNum_RenkeiTaishou.Text = "1";

            // 連携 1.未連携
            this.form.txtNum_RenkeiJyoukyou.Text = RENKEI_MISOUSHIN.ToString();
            this.renkei_kbn = int.Parse(this.form.txtNum_RenkeiJyoukyou.Text);

            // 曜日
            this.form.DAY_CD.Text = "8";

            // 作業日 今日
            this.form.SAGYOU_DATE.Value = parentForm.sysDate.Date;

            // コース
            this.form.COURSE_NAME_CD.Text = string.Empty;
            this.form.COURSE_NAME_RYAKU.Text = string.Empty;

            // 車種
            this.form.SHASHU_CD.Text = string.Empty;
            this.form.SHASHU_NAME_RYAKU.Text = string.Empty;

            // 車輌
            this.form.SHARYOU_CD.Text = string.Empty;
            this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

            // 運転者
            this.form.SHAIN_CD.Text = string.Empty;
            this.form.SHAIN_NAME.Text = string.Empty;

            // 運搬業者
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

            // 作業者
            this.form.txtNum_Sagyousha.Text = "1";

            // 出発時刻
            this.form.SHUPPATSU_TIME.Text = string.Empty;

            // 荷降時刻
            this.form.NIOROSHI_TIME.Text = string.Empty;

            // 出発業者
            this.form.SHUPPATSU_GYOUSHA_CD.Text = string.Empty;
            this.form.SHUPPATSU_GYOUSHA_NAME.Text = string.Empty;

            // 出発現場
            this.form.SHUPPATSU_GENBA_CD.Text = string.Empty;
            this.form.SHUPPATSU_GENBA_NAME.Text = string.Empty;

            // 荷降業者
            this.form.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
            this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;

            // 荷降現場
            this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
            this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
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

                // キー入力設定
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // 一覧内のチェックボックスの設定
                this.HeaderCheckBoxSupport();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // 検索条件の初期化処理
                this.SetInitialRenkeiCondition();

                // ボタン制御
                this.ButtonEnabledControl();

                // イベントの初期化処理
                this.EventInit();

                // コースポップアップの初期化
                this.PopUpDataInit();
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

        #region ボタン制御
        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        internal void ButtonEnabledControl()
        {
            // 初期化
            parentForm.bt_func1.Enabled = true;
            parentForm.bt_func4.Enabled = true;
            parentForm.bt_func7.Enabled = true;
            parentForm.bt_func8.Enabled = true;
            parentForm.bt_func9.Enabled = true;
            parentForm.bt_func12.Enabled = true;
            parentForm.bt_process1.Enabled = true;

            this.form.Ichiran1.Columns["DATA_DEPARTURE_TIME"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_ARRIVAL_TIME"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_SAGYOUSHA_CD"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_SHUPPATSU_GYOUSHA_CD"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_SHUPPATSU_GENBA_CD"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_NIOROSHI_GYOUSHA_CD"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_NIOROSHI_GENBA_CD"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_TRAFFIC_CONSIDERATION"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_SMART_IC_CONSIDERATION"].ReadOnly = true;
            this.form.Ichiran1.Columns["DATA_PRIORITY"].ReadOnly = true;

            // 連携状況による制御
            switch (this.renkei_kbn)
            {
                case RENKEI_MISOUSHIN:
                    parentForm.bt_func4.Enabled = false;
                    parentForm.bt_process1.Enabled = false;
                    this.form.Ichiran1.Columns["DATA_DEPARTURE_TIME"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_ARRIVAL_TIME"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_SAGYOUSHA_CD"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_SHUPPATSU_GYOUSHA_CD"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_SHUPPATSU_GENBA_CD"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_NIOROSHI_GYOUSHA_CD"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_NIOROSHI_GENBA_CD"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_TRAFFIC_CONSIDERATION"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_SMART_IC_CONSIDERATION"].ReadOnly = false;
                    this.form.Ichiran1.Columns["DATA_PRIORITY"].ReadOnly = false;
                    break;
                case RENKEI_SOUSHIN:
                    parentForm.bt_func1.Enabled = false;
                    parentForm.bt_func9.Enabled = false;
                    break;
                case RENKEI_JYUSHIN:
                    parentForm.bt_func1.Enabled = false;
                    break;
                default:
                    break;
            }

            // 最適化対象による入力制限
            switch (this.renkei_taishou)
            {
                case RENKEI_TAISHOU_COURSE:
                    this.form.DAY_CD.Enabled = true;
                    this.form.customPanel2.Enabled = true;
                    this.form.COURSE_NAME_CD.Enabled = true;
                    this.form.COURSE_NAME_RYAKU.Enabled = true;
                    break;
                case RENKEI_TAISHOU_TEIKI:
                    this.form.DAY_CD.Enabled = false;
                    this.form.customPanel2.Enabled = false;
                    this.form.COURSE_NAME_CD.Enabled = false;
                    this.form.COURSE_NAME_CD.Text = string.Empty;
                    this.form.COURSE_NAME_RYAKU.Enabled = false;
                    this.form.COURSE_NAME_RYAKU.Text = string.Empty;
                    break;
            }
            this.form.BT_GYOUSHA_SEARCH.Enabled = true;
            this.form.BT_GYOUSHA2_SEARCH.Enabled = true;
            this.form.BT_GENBA_SEARCH.Enabled = true;
            this.form.BT_GENBA2_SEARCH.Enabled = true;
        }

        /// <summary>
        /// 全ファンクションを入力不可にする
        /// </summary>
        internal void ButtonEnabledFalse()
        {
            parentForm.bt_func1.Enabled = false;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func7.Enabled = false;
            parentForm.bt_func8.Enabled = false;
            parentForm.bt_func9.Enabled = false;
            parentForm.bt_func12.Enabled = false;
            parentForm.bt_process1.Enabled = false;
            this.form.BT_GYOUSHA_SEARCH.Enabled = false;
            this.form.BT_GYOUSHA2_SEARCH.Enabled = false;
            this.form.BT_GENBA_SEARCH.Enabled = false;
            this.form.BT_GENBA2_SEARCH.Enabled = false;
        }
        #endregion

        #region 一括設定
        /// <summary>
        /// 一括設定
        /// </summary>
        internal void IkkatsuSettei()
        {

            var cnt = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true)).Count();
            if (cnt == 0)
            {
                this.msgLogic.MessageBoxShowError("一括設定を行う明細行を選択してください。");
                return;
            }

            int i = 0;

            // チェックがついているデータのみ抽出
            var dtos = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true));
            foreach (var DetailDto in dtos)
            {
                i++;

                // 作業者セット
                if (this.form.txtNum_Sagyousha.Text == "1")
                {
                    // 運転者を作業者にセット(NAVITIME連携済みの社員のみ)
                    DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value = this.naviShainDao.GetStringDataByCd(DetailDto.Cells["DATA_UNTENSHA_CD"].Value.ToString());
                }
                else
                {
                    // NAVITIME連携済みの社員を上から順にセット
                    //DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value = "test";
                    DataTable tmp = this.naviShainDao.GetDataForStringSql("SELECT NAVI_SHAIN_CD FROM M_NAVI_OUTPUT_SHAIN WHERE OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0 ORDER BY NAVI_SHAIN_CD");
                    int j = 0;
                    foreach (DataRow row in tmp.Rows)
                    {
                        j++;
                        if (i == j)
                        {
                            DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value = row["NAVI_SHAIN_CD"];
                        }
                    }
                }

                // 出発時刻、荷降時刻セット
                if (this.form.SHUPPATSU_TIME.Text != string.Empty || this.form.NIOROSHI_TIME.Text != string.Empty)
                {
                    DetailDto.Cells["DATA_DEPARTURE_TIME"].Value = this.form.SHUPPATSU_TIME.Text;
                    DetailDto.Cells["DATA_ARRIVAL_TIME"].Value = this.form.NIOROSHI_TIME.Text;
                }

                // 出発・荷降/業者・現場セット(未入力の場合はセットしない)
                if (this.form.SHUPPATSU_GYOUSHA_CD.Text != string.Empty)
                {
                    DetailDto.Cells["DATA_SHUPPATSU_GYOUSHA_CD"].Value = this.form.SHUPPATSU_GYOUSHA_CD.Text;
                }
                if (this.form.SHUPPATSU_GENBA_CD.Text != string.Empty)
                {
                    DetailDto.Cells["DATA_SHUPPATSU_GENBA_CD"].Value = this.form.SHUPPATSU_GENBA_CD.Text;
                    DetailDto.Cells["DATA_SHUPPATSU_GENBA_NAME"].Value = this.form.SHUPPATSU_GENBA_NAME.Text;
                    DetailDto.Cells["DATA_SHUPPATSU_EIGYOUSHO_CD"].Value = this.naviShuppatsuGenbaDao.GetStringDataByCd(this.form.SHUPPATSU_GYOUSHA_CD.Text, this.form.SHUPPATSU_GENBA_CD.Text);
                }
                if (this.form.NIOROSHI_GYOUSHA_CD.Text != string.Empty)
                {
                    DetailDto.Cells["DATA_NIOROSHI_GYOUSHA_CD"].Value = this.form.NIOROSHI_GYOUSHA_CD.Text;
                }
                if (this.form.NIOROSHI_GENBA_CD.Text != string.Empty)
                {
                    DetailDto.Cells["DATA_NIOROSHI_GENBA_CD"].Value = this.form.NIOROSHI_GENBA_CD.Text;
                    DetailDto.Cells["DATA_NIOROSHI_GENBA_NAME"].Value = this.form.NIOROSHI_GENBA_NAME.Text;
                    DetailDto.Cells["DATA_NIOROSHI_EIGYOUSHO_CD"].Value = this.naviShuppatsuGenbaDao.GetStringDataByCd(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
                }
            }
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

                this.renkei_taishou = int.Parse(this.form.txtNum_RenkeiTaishou.Text);

                // 抽出条件をDTOにセット
                this.SetSearchDto();

                if (this.renkei_taishou == 1)
                {
                    //検索実行
                    this.SearchResult = this.dao.GetNaviCourseData(this.searchDto);
                }
                else
                {
                    this.SearchResult = this.dao.GetNaviTeikiData(this.searchDto);
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

        #region 抽出条件をDTOにセット
        /// <summary>
        /// 抽出条件をDTOにセット
        /// </summary>
        private void SetSearchDto()
        {
            this.searchDto = new SearchDto();
            searchDto.KYOTEN_CD = Convert.ToInt32(this.headerForm.KYOTEN_CD.Text);
            searchDto.RENKEI_KBN = this.renkei_kbn;
            searchDto.SAGYOU_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString();
            if (this.form.DAY_CD.Text != "8")
            {
                searchDto.DAY_CD = this.form.DAY_CD.Text;
            }
            if (this.form.COURSE_NAME_CD.Text != string.Empty)
            {
                searchDto.COURSE_NAME_CD = this.form.COURSE_NAME_CD.Text;
            }
            if (this.form.SHARYOU_CD.Text != string.Empty)
            {
                searchDto.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            }
            if (this.form.SHASHU_CD.Text != string.Empty)
            {
                searchDto.SHASHU_CD = this.form.SHASHU_CD.Text;
            }
            if (this.form.SHAIN_CD.Text != string.Empty)
            {
                searchDto.UNTENSHA_CD = this.form.SHAIN_CD.Text;
            }
            if (this.form.UNPAN_GYOUSHA_CD.Text != string.Empty)
            {
                searchDto.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
        }
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

                // ヘッダーチェックボックスのクリア
                this.HeaderCheckBoxFalse();

                //抽出結果をDGVにセット
                if (this.SearchResult != null && this.SearchResult.Rows.Count > 0)
                {
                    this.form.Ichiran1.Rows.Add(this.SearchResult.Rows.Count);
                    for (int i = 0; i < this.form.Ichiran1.Rows.Count; i++)
                    {
                        DataGridViewRow row = this.form.Ichiran1.Rows[i];
                        DataRow dr = this.SearchResult.Rows[i];

                        row.Cells["DATA_TAISHO"].Value = false; //対象
                        row.Cells["DATA_PROCESSING_ID"].Value = dr["PROCESSING_ID"];
                        row.Cells["DATA_DELIVERY_DATE"].Value = dr["DELIVERY_DATE"];
                        row.Cells["DATA_NAVI_DELIVERY_ORDER"].Value = dr["NAVI_DELIVERY_ORDER"];
                        row.Cells["DATA_DAY_CD"].Value = dr["DAY_CD"];
                        row.Cells["DATA_DAY_NAME"].Value = dr["DAY_NAME"];
                        row.Cells["DATA_COURSE_NAME_CD"].Value = dr["COURSE_NAME_CD"];
                        row.Cells["DATA_COURSE_NAME"].Value = dr["COURSE_NAME"];
                        if (this.renkei_taishou == RENKEI_TAISHOU_TEIKI)
                        {
                            row.Cells["DATA_TEIKI_SYSTEM_ID"].Value = dr["TEIKI_SYSTEM_ID"];
                            row.Cells["DATA_TEIKI_SEQ"].Value = dr["TEIKI_SEQ"];
                        }
                        row.Cells["DATA_SHASHU_CD"].Value = dr["SHASHU_CD"];
                        row.Cells["DATA_SHARYOU_TYPE"].Value = dr["SHARYOU_TYPE"];
                        row.Cells["DATA_SHASHU_NAME"].Value = dr["SHASHU_NAME"];
                        row.Cells["DATA_SHARYOU_CD"].Value = dr["SHARYOU_CD"];
                        row.Cells["DATA_SHARYOU_NAME"].Value = dr["SHARYOU_NAME"];
                        row.Cells["DATA_UNTENSHA_CD"].Value = dr["UNTENSHA_CD"];
                        row.Cells["DATA_UNTENSHA_NAME"].Value = dr["UNTENSHA_NAME"];
                        row.Cells["DATA_UNPAN_GYOUSHA_CD"].Value = dr["UNPAN_GYOUSHA_CD"];
                        row.Cells["DATA_UNPAN_GYOUSHA_NAME"].Value = dr["UNPAN_GYOUSHA_NAME"];
                        row.Cells["DATA_SAGYOUSHA_CD"].Value = dr["SAGYOUSHA_CD"];
                        row.Cells["DATA_SHUPPATSU_GYOUSHA_CD"].Value = dr["SHUPPATSU_GYOUSHA_CD"];
                        row.Cells["DATA_SHUPPATSU_GENBA_CD"].Value = dr["SHUPPATSU_GENBA_CD"];
                        row.Cells["DATA_SHUPPATSU_GENBA_NAME"].Value = dr["SHUPPATSU_GENBA_NAME"];
                        row.Cells["DATA_SHUPPATSU_EIGYOUSHO_CD"].Value = dr["SHUPPATSU_EIGYOUSHO_CD"];
                        row.Cells["DATA_NIOROSHI_GYOUSHA_CD"].Value = dr["NIOROSHI_GYOUSHA_CD"];
                        row.Cells["DATA_NIOROSHI_GENBA_CD"].Value = dr["NIOROSHI_GENBA_CD"];
                        row.Cells["DATA_NIOROSHI_GENBA_NAME"].Value = dr["NIOROSHI_GENBA_NAME"];
                        row.Cells["DATA_NIOROSHI_EIGYOUSHO_CD"].Value = dr["NIOROSHI_EIGYOUSHO_CD"];
                        row.Cells["DATA_TRAFFIC_CONSIDERATION"].Value = dr["TRAFFIC_CONSIDERATION"];
                        row.Cells["DATA_SMART_IC_CONSIDERATION"].Value = dr["SMART_IC_CONSIDERATION"];
                        row.Cells["DATA_PRIORITY"].Value = dr["PRIORITY"];
                        row.Cells["DATA_SYSTEM_ID"].Value = dr["SYSTEM_ID"];
                        if (String.IsNullOrEmpty(Convert.ToString(dr["DEPARTURE_TIME"])))
                        {
                            row.Cells["DATA_DEPARTURE_TIME"].Value = string.Empty;
                            if (String.IsNullOrEmpty(Convert.ToString(dr["ARRIVAL_TIME"])))
                            {
                                row.Cells["DATA_ARRIVAL_TIME"].Value = string.Empty;
                            }
                            else
                            {
                                row.Cells["DATA_ARRIVAL_TIME"].Value = Convert.ToDateTime(dr["ARRIVAL_TIME"]).ToString("HH:mm");
                            }
                        }
                        else
                        {
                            row.Cells["DATA_DEPARTURE_TIME"].Value = Convert.ToDateTime(dr["DEPARTURE_TIME"]).ToString("HH:mm");
                        }
                        row.Cells["DATA_BIN_NO"].Value = dr["BIN_NO"];
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

        #region 登録前チェック
        /// <summary>
        /// 登録前のチェック処理
        /// </summary>
        /// <returns></returns>
        internal bool RegistChk(bool ret)
        {
            // チェック0件は弾く
            var count = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true)).Count();
            if (count == 0)
            {
                this.msgLogic.MessageBoxShowError("データ連携を行う明細行を選択してください。");
                return false;
            }

            // F9以外はここで抜ける
            if (ret)
            {
                return true;
            }

            // 運転者CDと日付の組み合わせを文字列化した状態でDistinctして件数カウント
            var dtos = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true));
            var distinctCount = new List<string>();
            int i = 0;
            foreach (var DetailDto in dtos)
            {
                i++;
                distinctCount.Add(Convert.ToString(DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value)
                                + Convert.ToString(this.form.SAGYOU_DATE.Text));
            }
            int cnt = distinctCount.Select(n => n).Distinct().Count();

            // 「同一日付・作業者」の複数チェックは弾く
            if (i != cnt)
            {
                // 今はまだ1件のみとしておく
                this.msgLogic.MessageBoxShowError("同じ作業者、日付の組み合わせのデータが選択されています。");
                return false;
            }

            // 明細の必須項目チェック
            dtos = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true));
            foreach (var DetailDto in dtos)
            {
                // 出発時刻or荷降時刻
                if (Convert.ToString(DetailDto.Cells["DATA_DEPARTURE_TIME"].Value) == string.Empty &&
                    Convert.ToString(DetailDto.Cells["DATA_ARRIVAL_TIME"].Value) == string.Empty)
                {
                    this.msgLogic.MessageBoxShow("E001", "出発時刻か荷降時刻のどちらか");
                    this.form.Ichiran1.CurrentCell = DetailDto.Cells["DATA_DEPARTURE_TIME"];
                    this.form.ActiveControl = this.form.Ichiran1;
                    return false;
                }

                // 作業者のチェック
                if (Convert.ToString(DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value) == string.Empty)
                {
                    this.msgLogic.MessageBoxShow("E001", "作業者CD");
                    this.form.Ichiran1.CurrentCell = DetailDto.Cells["DATA_SAGYOUSHA_CD"];
                    this.form.ActiveControl = this.form.Ichiran1;
                    return false;
                }

                // 出発現場
                if (Convert.ToString(DetailDto.Cells["DATA_SHUPPATSU_GENBA_CD"].Value) == string.Empty)
                {
                    this.msgLogic.MessageBoxShow("E001", "出発現場CD");
                    this.form.Ichiran1.CurrentCell = DetailDto.Cells["DATA_SHUPPATSU_GENBA_CD"];
                    this.form.ActiveControl = this.form.Ichiran1;
                    return false;
                }

                // 荷降現場
                if (Convert.ToString(DetailDto.Cells["DATA_NIOROSHI_GENBA_CD"].Value) == string.Empty)
                {
                    this.msgLogic.MessageBoxShow("E001", "荷降現場CD");
                    this.form.Ichiran1.CurrentCell = DetailDto.Cells["DATA_NIOROSHI_GENBA_CD"];
                    this.form.ActiveControl = this.form.Ichiran1;
                    return false;
                }

                // 車輌タイプのチェック
                if (Convert.ToString(DetailDto.Cells["DATA_SHARYOU_TYPE"].Value) == string.Empty)
                {
                    this.msgLogic.MessageBoxShowError("「NAVITIMEマスタ連携」に車種タイプCDが未登録の車種は、データ連携が行えません。");
                    this.form.Ichiran1.CurrentCell = DetailDto.Cells["DATA_TAISHO"];
                    this.form.ActiveControl = this.form.Ichiran1;
                    return false;
                }
            }


            // 荷降時刻チェック(日付跨ぎの場合はアラート)
            dtos = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true));
            foreach (var DetailDto in dtos)
            {
                if (Convert.ToString(DetailDto.Cells["DATA_ARRIVAL_TIME"].Value) != string.Empty)
                {
                    DataTable dt = null;

                    if (this.renkei_taishou == RENKEI_TAISHOU_COURSE)
                    {
                        dt = this.dao.GetCourseDetail(Convert.ToInt32(DetailDto.Cells["DATA_DAY_CD"].Value), Convert.ToString(DetailDto.Cells["DATA_COURSE_NAME_CD"].Value));
                    }
                    else
                    {
                        dt = this.dao.GetTeikiDetail(Convert.ToInt64(DetailDto.Cells["DATA_TEIKI_SYSTEM_ID"].Value), Convert.ToInt32(DetailDto.Cells["DATA_TEIKI_SEQ"].Value));
                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToString(dr["KIBOU_TIME"]) != string.Empty)
                        {
                            if (Convert.ToDateTime(dr["SAGYOU_TIME_FROM"]) > Convert.ToDateTime(dr["SAGYOU_TIME_TO"]))
                            {
                                // アラート
                                this.msgLogic.MessageBoxShowError("日付をまたぐコースの場合、出発時刻をセットしデータ連携を行ってください。");
                                ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran1.Rows[DetailDto.Index].Cells["DATA_ARRIVAL_TIME"], true);
                                return false;
                            }
                        }
                    }
                }
            }

            // 出発・荷降時刻と回収現場の希望時間のチェック
            dtos = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true));
            foreach (var DetailDto in dtos)
            {

                DataTable dt = null;

                if (this.renkei_taishou == RENKEI_TAISHOU_COURSE)
                {
                    dt = this.dao.GetCourseDetail(Convert.ToInt32(DetailDto.Cells["DATA_DAY_CD"].Value), Convert.ToString(DetailDto.Cells["DATA_COURSE_NAME_CD"].Value));
                }
                else
                {
                    dt = this.dao.GetTeikiDetail(Convert.ToInt64(DetailDto.Cells["DATA_TEIKI_SYSTEM_ID"].Value), Convert.ToInt32(DetailDto.Cells["DATA_TEIKI_SEQ"].Value));
                }

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime shuppatsuTime;
                    DateTime kibouTime;
                    DateTime nioroshiTime;

                    // 出発時刻と回収現場の希望時間を比較
                    if (Convert.ToString(DetailDto.Cells["DATA_DEPARTURE_TIME"].Value) != string.Empty)
                    {
                        if (Convert.ToString(dr["KIBOU_TIME"]) != string.Empty)
                        {
                            if (Convert.ToDateTime(dr["SAGYOU_TIME_FROM"]) > Convert.ToDateTime(dr["SAGYOU_TIME_TO"]))
                            {
                                // 日付を跨ぐケースの判定
                                shuppatsuTime = Convert.ToDateTime(DetailDto.Cells["DATA_DEPARTURE_TIME"].Value);
                                nioroshiTime = Convert.ToDateTime(dr["SAGYOU_TIME_TO"]).AddDays(1);
                                // 希望時間の算出
                                if (Convert.ToDateTime(dr["SAGYOU_TIME_TO"]) >= Convert.ToDateTime(dr["KIBOU_TIME"]))
                                {
                                    kibouTime = Convert.ToDateTime(dr["KIBOU_TIME"]).AddDays(1);
                                }
                                else
                                {
                                    kibouTime = Convert.ToDateTime(dr["KIBOU_TIME"]);
                                }
                                // 希望時間のチェック
                                if (shuppatsuTime < kibouTime && kibouTime < nioroshiTime)
                                {
                                    // 問題なし
                                }
                                else
                                {
                                    // アラート
                                    this.msgLogic.MessageBoxShowError("回収現場の希望時間が出発時刻以前で設定されています。出発時刻を早くしてくだい。");
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran1.Rows[DetailDto.Index].Cells["DATA_DEPARTURE_TIME"], true);
                                    return false;
                                }
                            }
                            else
                            {
                                // 日付を跨がないケースの判定
                                if (Convert.ToDateTime(DetailDto.Cells["DATA_DEPARTURE_TIME"].Value) >= Convert.ToDateTime(dr["KIBOU_TIME"]))
                                {
                                    this.msgLogic.MessageBoxShowError("回収現場の希望時間が出発時刻以前で設定されています。出発時刻を早くしてくだい。");
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran1.Rows[DetailDto.Index].Cells["DATA_DEPARTURE_TIME"], true);
                                    return false;
                                }
                            }
                        }
                    }

                    // 荷降時刻と回収現場の希望時間を比較
                    if (Convert.ToString(DetailDto.Cells["DATA_ARRIVAL_TIME"].Value) != string.Empty)
                    {
                        if (Convert.ToString(dr["KIBOU_TIME"]) != string.Empty)
                        {
                            if (Convert.ToDateTime(dr["SAGYOU_TIME_FROM"]) > Convert.ToDateTime(dr["SAGYOU_TIME_TO"]))
                            {
                                // 日付を跨ぐケースの判定
                                // 事前に弾かれているはずなので処理なし
                            }
                            else
                            {
                                // 日付を跨がないケースの判定
                                if (Convert.ToDateTime(DetailDto.Cells["DATA_ARRIVAL_TIME"].Value) <= Convert.ToDateTime(dr["KIBOU_TIME"]))
                                {
                                    this.msgLogic.MessageBoxShowError("回収現場の希望時間が荷降時刻以降で設定されています。荷降時刻を遅くしてくだい。");
                                    ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran1.Rows[DetailDto.Index].Cells["DATA_ARRIVAL_TIME"], true);
                                    return false;
                                }
                            }
                        }
                    }

                }
            }
            return true;
        }
        #endregion

        #region 登録用Entity作成
        /// <summary>
        /// 登録用Entityを作成
        /// </summary>
        /// <param name="linkStatus">連携状態(1.未連携,2.連携済,3.配送計画取得済)</param>
        internal void CreateEntity(string mode, short linkStatus)
        {
            this.RegistDTO = new NaviDeliveryDTO();

            this.RegistDTO.NAVI_DELIVERY = new List<T_NAVI_DELIVERY>();
            this.RegistDTO.NAVI_LINK_STATUS = new List<T_NAVI_LINK_STATUS>();
            foreach (var deliveryList in this.cDtoList)
            {
                if (mode == "DEL")
                {
                    // 削除
                    this.RegistDTO.NAVI_DELIVERY.Add(this.naviDeliveryDao.GetDataByCourse(deliveryList.SYSTEM_ID));
                    this.RegistDTO.NAVI_LINK_STATUS.Add(this.naviLinkStatusDao.GetDataByCd(deliveryList.SYSTEM_ID));
                    return;
                }
                else if (mode == "UPD")
                {
                    // 修正

                    // T_NAVI_DELIVERY
                    var entry = new T_NAVI_DELIVERY();

                    // 現状は項目変更不可なのでそのまま呼び出す
                    entry = this.naviDeliveryDao.GetDataByCourse(deliveryList.SYSTEM_ID);

                    var dataBinderDelivery = new DataBinderLogic<T_NAVI_DELIVERY>(entry);
                    dataBinderDelivery.SetSystemProperty(entry, false);

                    this.RegistDTO.NAVI_DELIVERY.Add(entry);

                    // T_NAVI_LINK_STATUS
                    var link = new T_NAVI_LINK_STATUS();
                    link = this.naviLinkStatusDao.GetDataByCd(deliveryList.SYSTEM_ID);
                    link.LINK_STATUS = linkStatus;

                    var dataBinderLink = new DataBinderLogic<T_NAVI_LINK_STATUS>(link);
                    dataBinderLink.SetSystemProperty(link, false);

                    this.RegistDTO.NAVI_LINK_STATUS.Add(link);

                }
                else if (mode == "INS")
                {
                    // 新規

                    // T_NAVI_DELIVERY
                    var entry = new T_NAVI_DELIVERY();

                    SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.HAISOU_KEIKAKU_TEIKI).ToString());
                    entry.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                    entry.SAITEKIKA_TAISHO = Convert.ToInt16(this.renkei_taishou);
                    // 定期の場合DAY_CDが存在しないケースもある
                    if (deliveryList.DAY_CD != string.Empty)
                    {
                        entry.DAY_CD = Convert.ToInt16(deliveryList.DAY_CD);
                    }
                    entry.COURSE_NAME_CD = deliveryList.COURSE_NAME_CD;
                    if (this.renkei_taishou == RENKEI_TAISHOU_TEIKI)
                    {
                        entry.TEIKI_SYSTEM_ID = deliveryList.TEIKI_SYSTEM_ID;
                        entry.TEIKI_SEQ = deliveryList.TEIKI_SEQ;
                    }
                    entry.DELIVERY_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
                    entry.SHASHU_CD = deliveryList.SHASHU_CD;
                    entry.SHARYOU_CD = deliveryList.SHARYOU_CD;
                    entry.UNTENSHA_CD = deliveryList.UNTENSHA_CD;
                    entry.UNPAN_GYOUSHA_CD = deliveryList.UNPAN_GYOUSHA_CD;
                    entry.SAGYOUSHA_CD = deliveryList.SAGYOUSHA_CD;
                    entry.SHUPPATSU_GYOUSHA_CD = deliveryList.SHUPPATSU_GYOUSHA_CD;
                    entry.SHUPPATSU_GENBA_CD = deliveryList.SHUPPATSU_GENBA_CD;
                    entry.SHUPPATSU_EIGYOUSHO_CD = deliveryList.SHUPPATSU_EIGYOUSHO_CD;
                    entry.NIOROSHI_GYOUSHA_CD = deliveryList.NIOROSHI_GYOUSHA_CD;
                    entry.NIOROSHI_GENBA_CD = deliveryList.NIOROSHI_GENBA_CD;
                    entry.NIOROSHI_EIGYOUSHO_CD = deliveryList.NIOROSHI_EIGYOUSHO_CD;
                    entry.SHITEI_SHARYOU_TYPE = deliveryList.SHARYOU_TYPE;
                    entry.NAVI_DELIVERY_ORDER = Convert.ToInt16(deliveryList.NAVI_DELIVERY_ORDER);
                    entry.TRAFFIC_CONSIDERATION = Convert.ToInt16(cnvTrafficConsideration(Convert.ToBoolean(deliveryList.TRAFFIC_CONSIDERATION)));
                    entry.SMART_IC_CONSIDERATION = Convert.ToInt16(cnvSmartICConsideration(Convert.ToBoolean(deliveryList.SMART_IC_CONSIDERATION)));
                    entry.PRIORITY = Convert.ToInt16(cnvPriority(Convert.ToBoolean(deliveryList.PRIORITY)));
                    entry.BIN_NO = deliveryList.BIN_NO;

                    // 案件コード採番＆設定
                    DataTable dt = null;
                    if (this.renkei_taishou == RENKEI_TAISHOU_COURSE)
                    {
                        dt = this.dao.GetCourseDetail(entry.DAY_CD, entry.COURSE_NAME_CD);
                    }
                    else
                    {
                        dt = this.dao.GetTeikiDetail(entry.TEIKI_SYSTEM_ID, entry.TEIKI_SEQ);
                    }

                    this.MatterNoList = GetMatterNoList(dt.Rows.Count);
                    if (this.MatterNoList != null && this.MatterNoList.Any())
                    {
                        entry.MATTER_CODE_BEGIN = this.MatterNoList.Min();
                        entry.MATTER_CODE_END = this.MatterNoList.Max();
                    }

                    if (deliveryList.DEPARTURE_TIME != null)
                    {
                        // 日付は作業日を基準に作成
                        var ymd = entry.DELIVERY_DATE.Value;
                        // 時刻は出発時刻を基準に作成
                        var hhmm = Convert.ToDateTime(deliveryList.DEPARTURE_TIME);

                        entry.DEPARTURE_TIME = new SqlDateTime(ymd.Year, ymd.Month, ymd.Day, hhmm.Hour, hhmm.Minute, 0);
                    }
                    if (deliveryList.ARRIVAL_TIME != null)
                    {
                        // 日付は作業日を基準に作成
                        var ymd = entry.DELIVERY_DATE.Value;
                        // 時刻は希望時刻を基準に作成
                        var hhmm = Convert.ToDateTime(deliveryList.ARRIVAL_TIME);

                        entry.ARRIVAL_TIME = new SqlDateTime(ymd.Year, ymd.Month, ymd.Day, hhmm.Hour, hhmm.Minute, 0);
                    }

                    var dataBinderDelivery = new DataBinderLogic<T_NAVI_DELIVERY>(entry);
                    dataBinderDelivery.SetSystemProperty(entry, false);

                    this.RegistDTO.NAVI_DELIVERY.Add(entry);

                    // T_NAVI_LINK_STATUS
                    var link = new T_NAVI_LINK_STATUS();
                    link.SYSTEM_ID = entry.SYSTEM_ID;
                    link.LINK_STATUS = linkStatus;

                    var dataBinderLink = new DataBinderLogic<T_NAVI_LINK_STATUS>(link);
                    dataBinderLink.SetSystemProperty(link, false);

                    this.RegistDTO.NAVI_LINK_STATUS.Add(link);
                }


            }
        }
        #endregion

        #region 値の読み替え
        /// <summary>
        /// 渋滞考慮の値変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int cnvTrafficConsideration(bool value)
        {
            int i = 0;

            if (value)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            return i;
        }
        /// <summary>
        /// スマートIC考慮の値変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int cnvSmartICConsideration(bool value)
        {
            int i = 0;

            if (value)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }
            return i;
        }
        /// <summary>
        /// 優先の値読み替え(とりあえずチェックONを有料道路優先としている)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int cnvPriority(bool value)
        {
            int i = 0;

            if (value)
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            return i;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        internal bool RegistData(string mode)
        {
            bool ret_cnt = false;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    //INSERT
                    foreach (var delivery in this.RegistDTO.NAVI_DELIVERY)
                    {
                        delivery.DELETE_FLG = false;
                        if (mode == "INS")
                        {
                            this.naviDeliveryDao.Insert(delivery);
                        }
                        else
                        {
                            this.naviDeliveryDao.Update(delivery);
                        }
                    }

                    foreach (var link in this.RegistDTO.NAVI_LINK_STATUS)
                    {
                        link.DELETE_FLG = false;
                        if (mode == "INS")
                        {
                            this.naviLinkStatusDao.Insert(link);
                        }
                        else
                        {
                            this.naviLinkStatusDao.Update(link);
                        }
                    }

                    tran.Commit();
                    ret_cnt = true;
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

        #region 削除処理
        /// <summary>
        /// 削除処理
        /// </summary>
        [Transaction]
        internal bool DeleteData()
        {
            bool ret_cnt = false;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    foreach (var delivery in this.RegistDTO.NAVI_DELIVERY)
                    {
                        delivery.DELETE_FLG = true;
                        this.naviDeliveryDao.Update(delivery);
                    }

                    foreach (var link in this.RegistDTO.NAVI_LINK_STATUS)
                    {
                        link.DELETE_FLG = true;
                        this.naviLinkStatusDao.Update(link);
                    }
                    tran.Commit();
                    ret_cnt = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteData", ex);
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

        #region 入力チェック

        #region 業者チェック
        /// <summary>
        /// 出発業者チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckShuppatsuGyousha()
        {
            bool returnVal = false;
            try
            {
                var gyoushaName = this.naviShuppatsuGenbaDao.GetStringDataByGyoushaName(this.form.SHUPPATSU_GYOUSHA_CD.Text);
                if (gyoushaName != null && 0 < gyoushaName.Length)
                {
                    this.form.SHUPPATSU_GYOUSHA_NAME.Text = gyoushaName;
                }
                else
                {
                    this.form.SHUPPATSU_GYOUSHA_NAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.SHUPPATSU_GYOUSHA_CD.Focus();
                    return returnVal;
                }

                // 処理終了
                returnVal = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShuppatsuGyousha", ex2);
                this.msgLogic.MessageBoxShow("E093");
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShuppatsuGyousha", ex);
                this.msgLogic.MessageBoxShow("E245");
                returnVal = false;
            }
            return returnVal;
        }

        /// <summary>
        /// 荷降チェック業者
        /// </summary>
        /// <returns></returns>
        internal bool CheckNioroshiGyousha()
        {
            bool returnVal = false;
            try
            {
                var gyoushaName = this.naviShuppatsuGenbaDao.GetStringDataByGyoushaName(this.form.NIOROSHI_GYOUSHA_CD.Text);
                if (gyoushaName != null && 0 < gyoushaName.Length)
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = gyoushaName;
                }
                else
                {
                    this.form.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.NIOROSHI_GYOUSHA_CD.Focus();
                    return returnVal;
                }

                // 処理終了
                returnVal = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckNioroshiGyousha", ex2);
                this.msgLogic.MessageBoxShow("E093");
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGyousha", ex);
                this.msgLogic.MessageBoxShow("E245");
                returnVal = false;
            }
            return returnVal;
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 出発現場チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckShuppatsuGenba()
        {
            bool returnVal = false;
            try
            {
                var genbaName = this.naviShuppatsuGenbaDao.GetStringDataByGenbaName(this.form.SHUPPATSU_GYOUSHA_CD.Text, this.form.SHUPPATSU_GENBA_CD.Text);
                if (genbaName != null && 0 < genbaName.Length)
                {
                    this.form.SHUPPATSU_GENBA_NAME.Text = genbaName;
                }
                else
                {
                    this.form.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.SHUPPATSU_GENBA_CD.Focus();
                    return returnVal;
                }

                // 処理終了
                returnVal = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckShuppatsuGenba", ex2);
                this.msgLogic.MessageBoxShow("E093");
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckShuppatsuGenba", ex);
                this.msgLogic.MessageBoxShow("E245");
                returnVal = false;
            }
            return returnVal;
        }

        /// <summary>
        /// 荷降現場チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckNioroshiGenba()
        {
            bool returnVal = false;
            try
            {
                var genbaName = this.naviShuppatsuGenbaDao.GetStringDataByGenbaName(this.form.NIOROSHI_GYOUSHA_CD.Text, this.form.NIOROSHI_GENBA_CD.Text);
                if (genbaName != null && 0 < genbaName.Length)
                {
                    this.form.NIOROSHI_GENBA_NAME.Text = genbaName;
                }
                else
                {
                    this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.NIOROSHI_GENBA_CD.Focus();
                    return returnVal;
                }

                // 処理終了
                returnVal = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CheckNioroshiGenba", ex2);
                this.msgLogic.MessageBoxShow("E093");
                returnVal = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckNioroshiGenba", ex);
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
                        this.form.SHASHU_NAME_RYAKU.Text = shashu.SHASHU_NAME_RYAKU;
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

                M_SHASHU keyEntity = new M_SHASHU() { SHASHU_CD = shashuCd };
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

        #region 時間入力チェック
        /// <summary>
        /// 時間入力チェック処理
        /// </summary>
        /// <returns>bool(OK:true NG:false)</returns>
        internal bool IsTimeChkOKDGV(DataGridViewCell ctrl)
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
        /// 時間入力チェック処理
        /// </summary>
        /// <returns>string(OK:XX:XX NG:string.empty)</returns>
        internal string IsTimeChkOKForm(string value)
        {
            bool result = true;
            string ret = string.Empty;
            try
            {
                // 未入力の場合、処理中止
                if (string.IsNullOrEmpty(value))
                {
                    return ret;
                }
                value = value.Replace(":", "");
                var reg = @"^(20|21|22|23|[0-1]\d)[0-5]\d$";
                result = Regex.IsMatch(value, reg);
                if (!result)
                {
                    this.msgLogic.MessageBoxShow("E084", value);
                }
                else
                {
                    ret = value.Substring(0, 2) + ":" + value.Substring(2, 2);
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }
        #endregion

        #endregion

        #region 選択行をDtoにセット
        /// <summary>
        /// 選択行をDtoにセットする処理
        /// </summary>
        internal void SetNaviCheckDetail()
        {
            // 初期化
            this.cDtoList = new List<NaviCheckDetail>();

            // チェックONのデータ読み込み
            var dtos = this.form.Ichiran1.Rows.Cast<DataGridViewRow>().Where(n => n.Cells["DATA_TAISHO"].Value.Equals(true));

            foreach (var DetailDto in dtos)
            {
                this.cDto = new NaviCheckDetail();
                cDto.SYSTEM_ID = Convert.ToInt64(DetailDto.Cells["DATA_SYSTEM_ID"].Value);
                cDto.DAY_CD = DetailDto.Cells["DATA_DAY_CD"].Value.ToString();
                cDto.COURSE_NAME_CD = DetailDto.Cells["DATA_COURSE_NAME_CD"].Value.ToString();
                if (this.renkei_taishou == RENKEI_TAISHOU_TEIKI)
                {
                    cDto.TEIKI_SYSTEM_ID = Convert.ToInt64(DetailDto.Cells["DATA_TEIKI_SYSTEM_ID"].Value);
                    cDto.TEIKI_SEQ = Convert.ToInt32(DetailDto.Cells["DATA_TEIKI_SEQ"].Value);
                }
                cDto.DELIVERY_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd");
                cDto.NAVI_DELIVERY_ORDER = DetailDto.Cells["DATA_NAVI_DELIVERY_ORDER"].Value.ToString();
                cDto.PROCESSING_ID = DetailDto.Cells["DATA_PROCESSING_ID"].Value.ToString();
                cDto.SHARYOU_CD = DetailDto.Cells["DATA_SHARYOU_CD"].Value.ToString();
                cDto.SHARYOU_TYPE = DetailDto.Cells["DATA_SHARYOU_TYPE"].Value.ToString();
                cDto.SHASHU_CD = DetailDto.Cells["DATA_SHASHU_CD"].Value.ToString();
                cDto.UNTENSHA_CD = DetailDto.Cells["DATA_UNTENSHA_CD"].Value.ToString();
                cDto.UNPAN_GYOUSHA_CD = DetailDto.Cells["DATA_UNPAN_GYOUSHA_CD"].Value.ToString();
                cDto.SAGYOUSHA_CD = DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value.ToString();
                cDto.SHUPPATSU_GYOUSHA_CD = DetailDto.Cells["DATA_SHUPPATSU_GYOUSHA_CD"].Value.ToString();
                cDto.SHUPPATSU_GENBA_CD = DetailDto.Cells["DATA_SHUPPATSU_GENBA_CD"].Value.ToString();
                cDto.SHUPPATSU_EIGYOUSHO_CD = DetailDto.Cells["DATA_SHUPPATSU_EIGYOUSHO_CD"].Value.ToString();
                cDto.NIOROSHI_GYOUSHA_CD = DetailDto.Cells["DATA_NIOROSHI_GYOUSHA_CD"].Value.ToString();
                cDto.NIOROSHI_GENBA_CD = DetailDto.Cells["DATA_NIOROSHI_GENBA_CD"].Value.ToString();
                cDto.NIOROSHI_EIGYOUSHO_CD = DetailDto.Cells["DATA_NIOROSHI_EIGYOUSHO_CD"].Value.ToString();
                cDto.TRAFFIC_CONSIDERATION = Convert.ToBoolean(DetailDto.Cells["DATA_TRAFFIC_CONSIDERATION"].Value);
                cDto.SMART_IC_CONSIDERATION = Convert.ToBoolean(DetailDto.Cells["DATA_SMART_IC_CONSIDERATION"].Value);
                cDto.PRIORITY = Convert.ToBoolean(DetailDto.Cells["DATA_PRIORITY"].Value);
                if (String.IsNullOrEmpty(Convert.ToString(DetailDto.Cells["DATA_DEPARTURE_TIME"].Value)))
                {
                    cDto.DEPARTURE_TIME = null;
                }
                else
                {
                    cDto.DEPARTURE_TIME = Convert.ToDateTime(DetailDto.Cells["DATA_DEPARTURE_TIME"].Value).ToString("HH:mm");
                }
                if (String.IsNullOrEmpty(Convert.ToString(DetailDto.Cells["DATA_ARRIVAL_TIME"].Value)))
                {
                    cDto.ARRIVAL_TIME = null;
                }
                else
                {
                    cDto.ARRIVAL_TIME = Convert.ToDateTime(DetailDto.Cells["DATA_ARRIVAL_TIME"].Value).ToString("HH:mm");
                }

                if (String.IsNullOrEmpty(Convert.ToString(DetailDto.Cells["DATA_BIN_NO"].Value)))
                {
                    // 最新の便番号を取得
                    cDto.BIN_NO = this.naviDeliveryDao.GetDeliveryBinCnt(Convert.ToString(DetailDto.Cells["DATA_SAGYOUSHA_CD"].Value), Convert.ToDateTime(this.form.SAGYOU_DATE.Text)) + 1;
                }
                else
                {
                    cDto.BIN_NO = Convert.ToInt32(DetailDto.Cells["DATA_BIN_NO"].Value);
                }
                cDtoList.Add(cDto);
            }
        }
        #endregion

        #region API通信絡みで使う処理

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        internal string OutputCSV(string Mode)
        {
            string filePath = string.Empty;

            try
            {
                // 出力データを整形する。
                var dataList = new List<string>();
                StringBuilder sb = new StringBuilder();

                // ヘッダ情報(空行で構わない)
                sb.Append("");
                dataList.Add(sb.ToString());

                // エラーポップアップ用
                this.popTable = new DataTable();
                this.popTable.Columns.Add("CD1", typeof(string));
                this.popTable.Columns.Add("VALUE1", typeof(string));
                this.popTable.Columns.Add("CD2", typeof(string));
                this.popTable.Columns.Add("VALUE2", typeof(string));
                this.popTable.Columns[0].ReadOnly = true;
                this.popTable.Columns[1].ReadOnly = true;
                this.popTable.Columns[2].ReadOnly = true;
                this.popTable.Columns[3].ReadOnly = true;
                this.popTable.TableName = "未連携現場(NAVITIME)";

                foreach (var delivery in this.RegistDTO.NAVI_DELIVERY)
                {
                    if (Mode == "INS")
                    {
                        #region 新規用・追加用

                        // 訪問順初期化
                        int gyo = 0;

                        // 出発時刻を事前にセット(荷降時刻はループの中でセットする)
                        string departureTime = Convert.ToString(delivery.DEPARTURE_TIME);
                        if (departureTime == "Null")
                        {
                            departureTime = string.Empty;
                        }
                        else
                        {
                            departureTime = Convert.ToDateTime(departureTime).ToString("yyyyMMdd HH:mm");
                        }
                        string arrivalTime = string.Empty;

                        DataTable dt = null;
                        //Entityにセットしたキーを元に情報抽出
                        if (this.renkei_taishou == RENKEI_TAISHOU_COURSE)
                        {
                            dt = this.dao.GetCourseDetail(delivery.DAY_CD, delivery.COURSE_NAME_CD);
                        }
                        else
                        {
                            dt = this.dao.GetTeikiDetail(delivery.TEIKI_SYSTEM_ID, delivery.TEIKI_SEQ);
                        }


                        DataRow popDr;

                        foreach (DataRow dr in dt.Rows)
                        {
                            string kibouDate = string.Empty;

                            if (string.IsNullOrEmpty(Convert.ToString(dr["VISIT_CODE"])))
                            {
                                // VISIT_CODEがなければリストに追加していく
                                popDr = popTable.NewRow();
                                popDr["CD1"] = dr["GYOUSHA_CD"];
                                popDr["VALUE1"] = dr["GYOUSHA_NAME_RYAKU"];
                                popDr["CD2"] = dr["GENBA_CD"];
                                popDr["VALUE2"] = dr["GENBA_NAME_RYAKU"];
                                popTable.Rows.Add(popDr);
                            }
                            else
                            {
                                // VISIT_CODEがあれば文字列作成
                                gyo++;

                                // 荷降時間をセット
                                arrivalTime = Convert.ToString(delivery.ARRIVAL_TIME);
                                if (Convert.ToBoolean(dr["DATE_CHANGE_FLG"]))
                                {
                                    if (arrivalTime == "Null")
                                    {
                                        arrivalTime = string.Empty;
                                    }
                                    else
                                    {
                                        // 日付跨ぎフラグがONの場合は荷降時刻に+1日する
                                        arrivalTime = Convert.ToDateTime(arrivalTime).AddDays(1).ToString("yyyyMMdd HH:mm");
                                    }
                                }
                                else
                                {
                                    if (arrivalTime == "Null")
                                    {
                                        arrivalTime = string.Empty;
                                    }
                                    else
                                    {
                                        arrivalTime = Convert.ToDateTime(arrivalTime).ToString("yyyyMMdd HH:mm");
                                    }
                                }

                                // Entryの作業時間(開始)＞明細の希望時間だったら作業日の翌日をセット
                                if (dr["KIBOU_TIME"] != DBNull.Value)
                                {
                                    // 残：あとここの判定だけ
                                    if (Convert.ToDateTime(dr["SAGYOU_TIME_FROM"]) > Convert.ToDateTime(dr["KIBOU_TIME"]))
                                    {
                                        kibouDate = Convert.ToDateTime(this.form.SAGYOU_DATE.Text).AddDays(1).ToString("yyyyMMdd");
                                    }
                                    else
                                    {
                                        kibouDate = string.Empty;
                                    }
                                }
                                sb.Clear();
                                sb.Append(delivery.DELIVERY_DATE.Value.ToString("yyyyMMdd"))    // 対象日
                                    .AppendFormat(",{0}", delivery.SAGYOUSHA_CD)                // 作業者ユーザーコード
                                    .Append(",4")                                               // スケジュール作成区分
                                    .AppendFormat(",{0}", gyo)                                  // 訪問順番
                                    .AppendFormat(",{0}", dr["VISIT_CODE"])                     // 訪問先コード
                                    .Append(",")                                                // 案件名称
                                    .Append(",")                                                // 案件詳細
                                    .AppendFormat(",{0}", kibouDate)                            // 到着希望日
                                    .AppendFormat(",{0}", dr["KIBOU_TIME"])                     // 到着希望時刻
                                    .AppendFormat(",{0}", dr["SAGYOU_TIME_MINUTE"])             // 作業時間(分)
                                    .Append(",0")                                               // 更新モード
                                    .AppendFormat(",{0}", delivery.MATTER_CODE_BEGIN + gyo - 1) // 案件コード
                                    .AppendFormat(",{0}", 0)                                    // ルート作成方法(0固定)
                                    .AppendFormat(",{0}", delivery.SHUPPATSU_EIGYOUSHO_CD)      // 出発営業所コード
                                    .AppendFormat(",{0}", delivery.NIOROSHI_EIGYOUSHO_CD)       // 到着営業所コード
                                    .AppendFormat(",{0}", departureTime)                        // 出発時刻
                                    .AppendFormat(",{0}", arrivalTime)                          // 到着時刻
                                    .AppendFormat(",{0}", delivery.TRAFFIC_CONSIDERATION)       // 渋滞考慮
                                    .AppendFormat(",{0}", delivery.SMART_IC_CONSIDERATION)      // スマートIC考慮
                                    .AppendFormat(",{0}", delivery.SHITEI_SHARYOU_TYPE)         // 車両タイプ
                                    .AppendFormat(",{0}", delivery.PRIORITY)                    // 優先
                                    .AppendFormat(",{0}", delivery.BIN_NO)                      // 便番号
                                    .Append(",");                                               // エラー事由
                                dataList.Add(sb.ToString());
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 削除用(Detail情報は不要なのでデータ抽出はしない)
                        //対象日や作業者を動的に取得する場合はEntityから取ればよい
                        sb.Clear();
                        sb.Append(delivery.DELIVERY_DATE.Value.ToString("yyyyMMdd"))// 対象日
                            .AppendFormat(",{0}", delivery.SAGYOUSHA_CD)            // 作業者ユーザーコード
                            .Append(",4")                                           // スケジュール作成区分(全便:2　単便:4)
                            .Append(",")                                            // 訪問順番
                            .Append(",")                                            // 訪問先コード
                            .Append(",")                                            // 案件名称
                            .Append(",")                                            // 案件詳細
                            .Append(",")                                            // 到着希望日
                            .Append(",")                                            // 到着希望時刻
                            .Append(",")                                            // 作業時間(分)
                            .Append(",1")                                           // 更新モード(1:削除)
                            .Append(",")                                            // 案件コード
                            .Append(",")                                            // ルート作成方法
                            .Append(",")                                            // 出発営業所コード
                            .Append(",")                                            // 到着営業所コード
                            .Append(",")                                            // 出発時刻
                            .Append(",")                                            // 到着時刻
                            .Append(",")                                            // 渋滞考慮
                            .Append(",")                                            // スマートIC考慮
                            .Append(",")                                            // 車両タイプ
                            .Append(",")                                            // 優先
                            .AppendFormat(",{0}", delivery.BIN_NO)                  // 便番号(全便:未指定　単便:便番号指定)
                            .Append(",");                                           // エラー事由
                        dataList.Add(sb.ToString());
                        #endregion
                    }
                }

                // 未連携現場がある場合はポップアップを表示して抜ける
                if (popTable.Rows.Count != 0)
                {
                    this.MirenkeiPopup();
                    return string.Empty;
                }

                // 件数が1=ヘッダしかない=データなし かつ 未連携現場がない
                if (!dataList.Count.Equals(1))
                {
                    string fileName = string.Empty;
                    if (Mode == "INS")
                    {
                        fileName = "配送計画一括送信";
                    }
                    else
                    {
                        fileName = "配送計画一括削除";
                    }

                    var navilogic = new NaviLogic();
                    filePath = navilogic.OutputCSV(fileName, dataList);

                    if (filePath == string.Empty)
                    {
                        return filePath;
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                this.msgLogic.MessageBoxShow("E245");
                return filePath;
            }
        }
        #endregion

        #region 同一の日付・ユーザーのデータが何件あるかチェック
        /// <summary>
        /// 登録時チェック/同一日、ユーザーの件数チェック
        /// </summary>
        /// <returns></returns>
        internal bool RegistUserChk()
        {
            foreach (var list in this.cDtoList)
            {
                //// 登録時に、既にナビタイム送信済みデータで同じ作業日、作業者のデータがある場合
                //// 登録させないためのロジック
                //// デモではログインユーザーCD
                //var userCd = this.naviCollaboEventDao.GetUserCd(list.SAGYOUSHA_CD);
                //if (!string.IsNullOrEmpty(userCd))
                //{
                //    // 既に同一ユーザーCDのデータがある場合はアラート表示
                //    this.msgLogic.MessageBoxShowError("このユーザーは割り当て済みの配車計画が存在します。"
                //                                        + System.Environment.NewLine
                //                                        + "処理を全て実行した後、再度実行してください。");
                //    return false;
                //}

                //// 一応同一日付・作業者の最大30便制限のチェックをする
                //int cnt = this.naviDeliveryDao.GetDeliveryCnt(list.SAGYOUSHA_CD, Convert.ToDateTime(list.DELIVERY_DATE));
                //if (cnt >= 30)
                //{
                //    this.msgLogic.MessageBoxShowError("作業者CD:" + list.SAGYOUSHA_CD + "には30件以上登録できません");
                //    return false;
                //}

                int cnt = this.naviDeliveryDao.GetDeliveryCnt(list.SAGYOUSHA_CD, Convert.ToDateTime(list.DELIVERY_DATE));
                if (cnt > 0)
                {
                    this.msgLogic.MessageBoxShowError("この日付、ユーザーの組み合わせは割り当て済みの配車計画が存在します。");
                    return false;
                }

            }
            return true;
        }
        #endregion

        #region API通信(配車計画情報一括登録)
        /// <summary>
        /// 配車計画情報一括登録のAPI
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal int RegistAPI(string filePath)
        {
            string msg = string.Empty;

            var navilogic = new NaviLogic();

            var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_UPLOAD);
            if (connectDto == null)
            {
                return -1;
            }

            // 配送計画送信
            var postDto = new NaviRequestDto();
            postDto.filePath = filePath;

            RES_UPLOAD dto = new RES_UPLOAD();
            //RES_UPLOAD dto = navilogic.HttpPOST<RES_UPLOAD>(NaviConst.naviUPLOAD, WebAPI_ContentType.MULTIPART_FORM_DATA, postDto);
            var result = navilogic.HttpPOST<RES_UPLOAD>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
            if (!result || dto == null)
            {
                // 例外が発生時はNULLなので処理終了
                return -1;
            }

            if (dto.Success)
            {
                return int.Parse(dto.ProcessingId);
            }
            else
            {
                // ナビタイム処理中にリクエスト送信すると通る
                foreach (var err in dto.ErrorMessage)
                {
                    msg += err + System.Environment.NewLine;
                }
                this.msgLogic.MessageBoxShowError(msg);
                return 0;
            }
        }
        #endregion

        #region API通信(配車計画情報一括登録の結果確認)
        /// <summary>
        /// 配車計画情報一括登録の結果確認のAPI連携
        /// </summary>
        /// <returns>0:チェックついてない 1:リクエスト失敗 2:レスポンス成功 3:レスポンス処理中 4:レスポンスエラー情報有り</returns>
        internal string CheckAPI(int index, out bool returnFlg)
        {

            // 渡すアラート用文字列を初期化
            string NaviAlertMsg = string.Empty;

            string pId = Convert.ToString(this.form.Ichiran1.Rows[index].Cells["DATA_PROCESSING_ID"].Value);
            string dDate = Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd");
            string driver = Convert.ToString(this.form.Ichiran1.Rows[index].Cells["DATA_UNTENSHA_CD"].Value);

            // DtoにプロセスIDをセット
            var postDto = new NaviRequestDto();
            postDto.processingId = pId;

            // チェックがついてるデータがない場合抜ける
            if (postDto.processingId == null)
            {
                returnFlg = true;
                return string.Empty;
            }

            var navilogic = new NaviLogic();

            var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_CHECK_UPLOAD);
            if (connectDto == null)
            {
                returnFlg = true;
                return string.Empty;
            }

            RES_CHECK_UPLOAD dto = new RES_CHECK_UPLOAD();
            // チェックのリクエストを送信
            //var dto = navilogic.HttpPOST<RES_CHECK_UPLOAD>(NaviConst.naviCHECK_UPLOAD, WebAPI_ContentType.MULTIPART_FORM_DATA, postDto);
            var results = navilogic.HttpPOST<RES_CHECK_UPLOAD>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
            if (!results || dto == null)
            {
                // 例外が発生時はNULLなので処理終了
                returnFlg = true;
                return string.Empty;
            }

            // リクエストのエラー
            if (dto.ErrorMessage != null)
            {
                string msg = string.Empty;
                foreach (var err in dto.ErrorMessage)
                {
                    msg += err.ToString() + System.Environment.NewLine;
                    this.msgLogic.MessageBoxShowError(msg);
                }
                returnFlg = true;
                return string.Empty;
            }

            if (dto.Results.UploadingStatus != "0")
            {
                this.msgLogic.MessageBoxShowInformation("まだナビタイムへ反映されておりません。" + System.Environment.NewLine + "もう暫しお待ちください。");
                returnFlg = true;
                return string.Empty;
            }

            // 結果
            if (dto.Results.ResultStatus == "0")
            {
                // 成功時はメッセージ表示せずコース最適化画面を開くので不要
                //this.msgLogic.MessageBoxShow("I017", "ナビタイムへ反映");
                returnFlg = false;
                return string.Empty;
            }
            else
            {
                this.msgLogic.MessageBoxShowWarn("エラー情報が存在します。");
            }

            // 文字列
            var alert = string.Empty;

            // エラー理由の抽出
            if (dto.Results.ContentError == null)
            {
                this.msgLogic.MessageBoxShowError("送信から24時間以上経過したため、エラー内容の取得ができません。");
                returnFlg = true;
                return string.Empty;
            }

            // ポップアップに表示するための詳細エラー内容を文字列に格納
            if (dto.Results.ContentError.Count != 0)
            {
                string er = string.Empty;

                // 配送計画
                foreach (RES_CHECK_UPLOAD_CONTENT_ERROR errDto in dto.Results.ContentError)
                {
                    if (er == errDto.ErrorReason)
                    {
                        // 同じアラート内容だったら省く
                        continue;
                    }

                    er = errDto.ErrorReason;

                    alert += "作業日:" + dDate + "運転者:" + driver
                          + System.Environment.NewLine + errDto.ErrorReason + System.Environment.NewLine;
                }
            }
            NaviAlertMsg = alert;

            returnFlg = false;
            return NaviAlertMsg;
        }

        #endregion

        #region T_NAVI_COLLABORATION_EVENTSにプロセスIDを保存
        /// <summary>
        /// 一時テーブルに登録
        /// </summary>
        /// <param name="dgvRow"></param>
        /// <returns></returns>
        [Transaction]
        internal bool NaviEventRegist(string Mode, int processingId)
        {
            bool result = false;

            try
            {
                using (var tran = new TransactionUtility())
                {

                    foreach (var list in this.RegistDTO.NAVI_DELIVERY)
                    {
                        if (Mode == "INS")
                        {
                            string sql = string.Format("INSERT INTO T_NAVI_COLLABORATION_EVENTS VALUES ({0}, {1}, '{2}')", list.SYSTEM_ID, processingId, list.SAGYOUSHA_CD);
                            this.dao.ExecuteForStringSql(sql);
                        }
                        else
                        {
                            string sql = string.Format("DELETE FROM T_NAVI_COLLABORATION_EVENTS WHERE SYSTEM_ID = {0}", list.SYSTEM_ID);
                            this.dao.ExecuteForStringSql(sql);
                        }
                    }
                    tran.Commit();
                    result = true;
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex1)
            {
                //排他は警告
                LogUtility.Warn(ex1); //排他は警告
                this.msgLogic.MessageBoxShow("E080");
                result = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error(ex2); //その他SQLエラー
                this.msgLogic.MessageBoxShow("E093");
                result = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他エラー
                this.msgLogic.MessageBoxShow("E245");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return result;
        }
        #endregion

        #region 案件コードの採番処理
        /// <summary>
        /// 案件コードの採番処理
        /// </summary>
        /// <param name="count">レコード数</param>
        /// <returns></returns>
        private List<SqlInt64> GetMatterNoList(int count)
        {
            if (count < 1)
            {
                return null;
            }

            List<SqlInt64> result = new List<SqlInt64>();

            SqlInt64 returnInt = -1;
            SqlInt64 minValue = 1;
            SqlInt64 maxValue = 1;

            var entity = new S_NUMBER_DENSHU();
            var denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.HAISOU_KEIKAKU_TEIKI).ToString());
            entity.DENSHU_KBN_CD = denshuKbn;

            var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
            returnInt = this.numberDenshuDao.GetMaxIncrementKey(entity, count);

            // 採番された番号の最小・最大値を保持
            if (updateEntity != null)
            {
                minValue = updateEntity.CURRENT_NUMBER + 1;
            }
            maxValue = returnInt;

            if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
            {
                updateEntity = new S_NUMBER_DENSHU();
                updateEntity.DENSHU_KBN_CD = denshuKbn;
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

            for (long i = minValue.Value; i <= maxValue.Value; i++)
            {
                result.Add(i);
            }

            return result;
        }
        #endregion

        #endregion

        #region コースポップアップ
        /// <summary>
        /// コース情報 ポップアップ初期化
        /// </summary>
        private void PopUpDataInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // ｺｰｽ情報 ポップアップ取得
                // 表示用データ取得＆加工
                var ShainDataTable = this.GetPopUpData(this.form.COURSE_NAME_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
                // TableNameを設定すれば、ポップアップのタイトルになる
                ShainDataTable.TableName = "ｺｰｽ名称情報";

                // 列名とデータソース設定
                this.form.COURSE_NAME_CD.PopupDataHeaderTitle = new string[] { "ｺｰｽ名称CD", "ｺｰｽ名称" };
                this.form.COURSE_NAME_CD.PopupDataSource = ShainDataTable;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PopUpDataInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        private DataTable GetPopUpData(IEnumerable<string> displayCol)
        {
            DataTable ret = new DataTable();
            try
            {
                LogUtility.DebugMethodStart(displayCol);
                M_COURSE_NAME[] CourseNameAll;
                M_COURSE_NAME entity = new M_COURSE_NAME();
                entity.ISNOT_NEED_DELETE_FLG = false;

                CourseNameAll = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>().GetAllValidData(entity);

                this.mCourseNameAll = CourseNameAll;
                if (displayCol.Any(s => s.Length == 0))
                {
                    return new DataTable();
                }
                var dt = EntityUtility.EntityToDataTable(CourseNameAll);
                if (dt.Rows.Count == 0)
                {
                    ret = dt;
                    return dt;
                }
                var sortedDt = new DataTable();
                foreach (var col in displayCol)
                {
                    // 表示対象の列だけを順番に追加
                    sortedDt.Columns.Add(dt.Columns[col].ColumnName, dt.Columns[col].DataType);
                }

                foreach (DataRow r in dt.Rows)
                {
                    sortedDt.Rows.Add(sortedDt.Columns.OfType<DataColumn>().Select(s => r[s.ColumnName]).ToArray());
                }
                ret = sortedDt;
                return sortedDt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPopUpData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }
        #endregion

        #region 作業者ポップアップ
        /// <summary>
        /// ポップアップ設定
        /// </summary>
        /// <param name="e"></param>
        public void CheckPopup(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.Ichiran1.Columns[this.form.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_SAGYOUSHA_CD"))
                {
                    // 作業者
                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;

                    string sql = "SELECT NS.NAVI_SHAIN_CD AS CD, S.SHAIN_NAME_RYAKU AS VALUE FROM M_NAVI_OUTPUT_SHAIN AS NS INNER JOIN M_SHAIN AS S ON NS.SHAIN_CD = S.SHAIN_CD WHERE NS.OUTPUT_DATE IS NOT NULL ORDER BY NS.NAVI_SHAIN_CD";
                    dt = this.naviShainDao.GetDataForStringSql(sql);

                    dt.TableName = "運転者(NAVITIME)";
                    form.table = dt;
                    form.PopupTitleLabel = "運転者(NAVITIME)";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "社員CD", "社員名" };

                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.Ichiran1.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                    }
                }
            }
        }
        #endregion

        #region 未連携現場ポップアップ
        /// <summary>
        /// NAVITIME未連携現場をポップアップに表示
        /// </summary>
        private void MirenkeiPopup()
        {
            var returnParams = this.mirenkeiPop();

        }

        /// <summary>
        /// 出発現場
        /// </summary>
        /// <returns></returns>
        internal Dictionary<int, List<PopupReturnParam>> mirenkeiPop()
        {
            PopupForm form = new PopupForm();
            // DataTableの中身はCSV出力箇所で作成されている
            form.table = this.popTable;
            form.PopupTitleLabel = "未連携現場(NAVITIME)";
            form.PopupGetMasterField = "CD1,VALUE1,CD2,VALUE2";
            form.PopupDataHeaderTitle = new string[] { "業者CD", "業者名", "現場CD", "現場名" };

            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            return form.ReturnParams;
        }
        #endregion

        #region 明細ヘッダーにチェックボックスを追加
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();

            if (!this.form.Ichiran1.Columns.Contains("DATA_TAISHO"))
            {
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = "DATA_TAISHO";
                    newColumn.HeaderText = "連携";
                    newColumn.DataPropertyName = "TAISHO";
                    newColumn.Width = 70;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "連携   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.Resizable = DataGridViewTriState.False;
                    newColumn.ReadOnly = false;

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
            if (this.form.Ichiran1.Columns.Contains("DATA_TAISHO"))
            {
                DataGridViewCheckBoxHeaderCell header = this.form.Ichiran1.Columns["DATA_TAISHO"].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }
        #endregion

        #region ユーザー定義反映処理(XML)

        /// <summary>
        /// ユーザー定義情報設定処理
        /// </summary>
        private void SetUserProfile()
        {
            LogUtility.DebugMethodStart();

            // XMLから拠点CDを取得
            const string XML_KYOTEN_CD_KEY_NAME = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, XML_KYOTEN_CD_KEY_NAME);
            if (!string.IsNullOrEmpty(headerForm.KYOTEN_CD.Text.ToString()))
            {
                headerForm.KYOTEN_CD.Text = headerForm.KYOTEN_CD.Text.ToString().PadLeft(headerForm.KYOTEN_CD.MaxLength, '0');

                // 拠点略称を設定
                this.CheckKyotenCd(headerForm.KYOTEN_CD, headerForm.KYOTEN_NAME_RYAKU);
            }

            //// 拠点変更イベント
            //headerForm.KYOTEN_CD_TextChanged();

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
