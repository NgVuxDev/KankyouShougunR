using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup.Setting.ButtonSetting.xml";

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// DAOClass
        /// </summary>
        private DAOClass mDao;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;
        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>
        /// IM_GYOUSHUDao
        /// </summary>
        private IM_GYOUSHUDao gyoushuDao;
        /// <summary>
        /// Header
        /// </summary>
        private UIHeader header;
        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BasePopForm parentForm;

        /// <summary>
        /// 検索結果データ
        /// </summary>
        private DataTable mSearchData;
        /// <summary>
        /// 帳票情報データ
        /// </summary>
        private DataTable mReportInfoData;        
        /// <summary>
        /// 検索条件
        /// </summary>
        private DtoCls SearchCon;
        #endregion

        #region プロパティ
        /// <summary>
        /// 外部からの受け渡しデーターテーブルリストを保持するプロパティ
        /// </summary>
        public  Dictionary<string, object> ReportInfoList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                //DAO
                this.mDao = DaoInitUtility.GetComponent<DAOClass>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();    

                // 共通Dao               
                this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                this.gyoushuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHUDao>();

                msgLogic = new MessageBoxShowLogic();
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
        #endregion        

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();             

                // ヘッダー（フッター）を初期化
                this.HeaderInit();
                //　ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.parentForm = (BasePopForm)this.form.Parent;
                // 画面初期化処理
                this.InitDisplay();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        private void InitDisplay()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //提出先:1.全体
                this.form.txtTeishutusakiKbn.Text = "1";
                //混合:1.混合出力有
                this.form.txtKongouKbn.Text = "1";
                //出力区分:1.合算
                this.form.txtShuturyokuKbn.Text = "1";
                //他社運搬許可番号の記載
                this.form.txtUPN_KYOKA_KBN.Text = "1";
                //社処分許可番号の記載
                this.form.txtSBN_KYOKA_KBN.Text = "1";
                //提出業者設定(1行目)
                this.form.txtGYOUSHASetKbn1.Text = "1";
                //提出業者設定(2行目):4:全て
                this.form.txtGYOUSHASetKbn2.Text = "4";
                //現場一括集計
                this.form.txtGenbaShukeiKbn.Text = "2";
                //地域名の印字
                this.form.txtChiikiNmKbn.Text = "1";
                //交付年月日
                this.form.HIDUKE_FROM.Value = DateTime.Parse((this.parentForm.sysDate.Year - 1).ToString() + "/04/01");
                this.form.HIDUKE_TO.Value = DateTime.Parse((this.parentForm.sysDate.Year).ToString() + "/04/01").AddDays(-1);
                //作成日
                this.form.dtpCreadDate.Value = this.parentForm.sysDate;

                this.SetControlEnabled();
               
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
        /// 提出先:１．全体	２．個別
        /// 提出業者設定:１．個別	２．全体
        /// 現場一括の集計:１．する	２．しない
        /// の指定により住所、事業場の名称、事業場の所在地の使用可否を変更する。
        /// </summary>
        public void SetControlEnabled()
        {
            if (this.form.txtTeishutusakiKbn.Text.Equals("1")
                   && this.form.txtGYOUSHASetKbn1.Text.Equals("2")
                   && this.form.txtGenbaShukeiKbn.Text.Equals("1"))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = false;
                this.form.txtCHIIKI_CD.Text = string.Empty;
                this.form.txtCHIIKI_NM.Text = string.Empty;

                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = true;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = false;
                this.form.txtGENBA_CD_TO.Enabled = false;
                this.form.txtGENBA_CD.Text = string.Empty;
                this.form.txtGENBA_NAME.Text = string.Empty;
                this.form.txtGENBA_CD_TO.Text = string.Empty;
                this.form.txtGENBA_NAME_TO.Text = string.Empty;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = true;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = true;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = true;
            }
            else if (this.form.txtTeishutusakiKbn.Text.Equals("1")
                  && this.form.txtGYOUSHASetKbn1.Text.Equals("2")
                  && this.form.txtGenbaShukeiKbn.Text.Equals("2"))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = false;
                this.form.txtCHIIKI_CD.Text = string.Empty;
                this.form.txtCHIIKI_NM.Text = string.Empty;

                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = true;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = false;
                this.form.txtGENBA_CD_TO.Enabled = false;
                this.form.txtGENBA_CD.Text = string.Empty;
                this.form.txtGENBA_NAME.Text = string.Empty;
                this.form.txtGENBA_CD_TO.Text = string.Empty;
                this.form.txtGENBA_NAME_TO.Text = string.Empty;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = false;
                this.form.txtGenbaNm.Text = string.Empty;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = false;
                this.form.txtGenbaAddress.Text = string.Empty;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = false;

            }
            else if (this.form.txtTeishutusakiKbn.Text.Equals("2")
                 && this.form.txtGYOUSHASetKbn1.Text.Equals("2")
                 && (this.form.txtGenbaShukeiKbn.Text.Equals("1")))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = true;
                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = true;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = false;
                this.form.txtGENBA_CD_TO.Enabled = false;
                this.form.txtGENBA_CD.Text = string.Empty;
                this.form.txtGENBA_NAME.Text = string.Empty;
                this.form.txtGENBA_CD_TO.Text = string.Empty;
                this.form.txtGENBA_NAME_TO.Text = string.Empty;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = true;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = true;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = true;
            }
            else if (this.form.txtTeishutusakiKbn.Text.Equals("2")
                 && this.form.txtGYOUSHASetKbn1.Text.Equals("2")
                 && (this.form.txtGenbaShukeiKbn.Text.Equals("2")))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = true;
                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = true;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = false;
                this.form.txtGENBA_CD_TO.Enabled = false;
                this.form.txtGENBA_CD.Text = string.Empty;
                this.form.txtGENBA_NAME.Text = string.Empty;
                this.form.txtGENBA_CD_TO.Text = string.Empty;
                this.form.txtGENBA_NAME_TO.Text = string.Empty;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = false;
                this.form.txtGenbaNm.Text = string.Empty;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = false;
                this.form.txtGenbaAddress.Text = string.Empty;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = false;
            }           
            else if (this.form.txtTeishutusakiKbn.Text.Equals("1")
            && this.form.txtGYOUSHASetKbn1.Text.Equals("1")
            && this.form.txtGenbaShukeiKbn.Text.Equals("1"))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = false;
                this.form.txtCHIIKI_CD.Text = string.Empty;
                this.form.txtCHIIKI_NM.Text = string.Empty;
                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = false;
                this.form.txtGYOUSHA_CD_TO.Text = string.Empty;
                this.form.txtGYOUSHA_NAME_TO.Text = string.Empty;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = false;
                this.form.txtGENBA_CD_TO.Enabled = false;
                this.form.txtGENBA_CD.Text = string.Empty;
                this.form.txtGENBA_NAME.Text = string.Empty;
                this.form.txtGENBA_CD_TO.Text = string.Empty;
                this.form.txtGENBA_NAME_TO.Text = string.Empty;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = true;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = true;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = true;
            }
            else if (this.form.txtTeishutusakiKbn.Text.Equals("1")
           && this.form.txtGYOUSHASetKbn1.Text.Equals("1")
           && this.form.txtGenbaShukeiKbn.Text.Equals("2"))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = false;
                this.form.txtCHIIKI_CD.Text = string.Empty;
                this.form.txtCHIIKI_NM.Text = string.Empty;
                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = false;
                this.form.txtGYOUSHA_CD_TO.Text = string.Empty;
                this.form.txtGYOUSHA_NAME_TO.Text = string.Empty;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = true;
                this.form.txtGENBA_CD_TO.Enabled = true;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = false;
                this.form.txtGenbaNm.Text = string.Empty;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = false;
                this.form.txtGenbaAddress.Text = string.Empty;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = false;

            }
            else if (this.form.txtTeishutusakiKbn.Text.Equals("2")
                && this.form.txtGYOUSHASetKbn1.Text.Equals("1")
                && this.form.txtGenbaShukeiKbn.Text.Equals("1"))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = true;
                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = false;
                this.form.txtGYOUSHA_CD_TO.Text = string.Empty;
                this.form.txtGYOUSHA_NAME_TO.Text = string.Empty;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = false;
                this.form.txtGENBA_CD_TO.Enabled = false;
                this.form.txtGENBA_CD.Text = string.Empty;
                this.form.txtGENBA_NAME.Text = string.Empty;
                this.form.txtGENBA_CD_TO.Text = string.Empty;
                this.form.txtGENBA_NAME_TO.Text = string.Empty;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = true;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = true;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = true;
            }
            else if (this.form.txtTeishutusakiKbn.Text.Equals("2")
              && this.form.txtGYOUSHASetKbn1.Text.Equals("1")
              && this.form.txtGenbaShukeiKbn.Text.Equals("2"))
            {
                //都道府県政令
                this.form.txtCHIIKI_CD.Enabled = true;
                //提出業者From
                this.form.txtGYOUSHA_CD.Enabled = true;
                //提出業者To
                this.form.txtGYOUSHA_CD_TO.Enabled = false;
                this.form.txtGYOUSHA_CD_TO.Text = string.Empty;
                this.form.txtGYOUSHA_NAME_TO.Text = string.Empty;
                //提出事業場FromTo
                this.form.txtGENBA_CD.Enabled = true;
                this.form.txtGENBA_CD_TO.Enabled = true;
                //事業場の名称
                this.form.txtGenbaNm.Enabled = false;
                this.form.txtGenbaNm.Text = string.Empty;
                //事業場の所在地
                this.form.txtGenbaAddress.Enabled = false;
                this.form.txtGenbaAddress.Text = string.Empty;
                //地域名の印字
                this.form.pnlChiikiNmBkn.Enabled = false;
            }

            if (this.form.pnlChiikiNmBkn.Enabled && string.IsNullOrEmpty(this.form.txtChiikiNmKbn.Text))
            {
                this.form.txtChiikiNmKbn.Text = "1";
            }
            else if (!this.form.pnlChiikiNmBkn.Enabled)
            {
                this.form.txtChiikiNmKbn.Text = string.Empty;
            }
        }
        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BasePopForm)this.form.Parent;

                //ヘッダーの初期化
                UIHeader targetHeader = (UIHeader)parentForm.headerForm;
                this.header = targetHeader;
              
                this.header.windowTypeLabel.Visible = false;
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BasePopForm)this.form.Parent;

                //前年ボタン(F1)イベント生成
                parentForm.bt_func1.Click += new EventHandler(this.form.btnPrevious_Click);  
                //次年ボタン(F2)イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.btnNext_Click);  
                //実行ボタン(F9)イベント生成                  
                parentForm.bt_func9.Click += new EventHandler(this.form.btnSearch_Click);              
                //クローズ処理(F12)イベント生成              
                parentForm.bt_func12.Click += new EventHandler(this.form.btnClosed_Click);

                /// 20141023 Houkakou 「交付等状況報告書」のダブルクリックを追加する　start
                // 「To」のイベント生成
                this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                this.form.txtGYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(txtGYOUSHA_CD_TO_MouseDoubleClick);
                this.form.txtGENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(txtGENBA_CD_TO_MouseDoubleClick);
                /// 20141023 Houkakou 「交付等状況報告書」のダブルクリックを追加する　end

                this.form.txtGYOUSHA_CD.Validated += new EventHandler(this.form.GYOUSHA_CD_Validated);
                this.form.txtGYOUSHA_CD_TO.Validated += new EventHandler(this.form.GYOUSHA_CD_Validated);

                this.form.txtGENBA_CD.Validated += new EventHandler(this.form.GENBA_CD_Validated);
                this.form.txtGENBA_CD_TO.Validated += new EventHandler(this.form.GENBA_CD_Validated);
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
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();              
                var parentForm = (BasePopForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
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
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 地域情報 ポップアップ初期化
        /// </summary>
        public void PopUpDataInit(CustomTextBox argTextBox)
        {
            // ｺｰｽ情報 ポップアップ取得
            // 表示用データ取得＆加工
            var popUpDataTable = this.GetPopUpData(argTextBox.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()));
            // TableNameを設定すれば、ポップアップのタイトルになる
            popUpDataTable.TableName = "地域名称情報";

            // 列名とデータソース設定
            argTextBox.PopupDataHeaderTitle = new string[] {"地域CD","地域名"};
            argTextBox.PopupDataSource = popUpDataTable;
        }
        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        private DataTable GetPopUpData(IEnumerable<string> displayCol)
        {

            M_CHIIKI[] mNameAll = DaoInitUtility.GetComponent<IM_CHIIKIDao>().GetAllData();//.GetAllData();

            if (displayCol.Any(s => s.Length == 0))
            {
                return new DataTable();
            }

            var dt = EntityUtility.EntityToDataTable(mNameAll);
            if (dt.Rows.Count == 0)
            {
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

            return sortedDt;
        }
        #endregion

        #region 実行処理（F9）

        /// <summary>
        /// 実行処理
        /// </summary>
        [Transaction]
        public void Jikou()
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.form.isSearchErr = false;
                var messageShowLogic = new MessageBoxShowLogic();
                /// 20141021 Houkakou 「交付等状況報告書」の日付チェックを追加する　start
                if (this.DateCheck())
                {
                    return;
                }
                /// 20141021 Houkakou 「交付等状況報告書」の日付チェックを追加する　end
                
                //検索条件取得
                this.SearchCon =this.GetCondition();

                //紙データ
                DataTable searchData = null;
                //電子データ
                DataTable SearchDensiData = null;
                DataTable tempData = null;

                if (this.SearchCon.SHUTURYOKU_KBN.Equals("2"))
                {
                    //紙データ
                    searchData = this.mDao.GetReportData(this.SearchCon);
                    this.mSearchData = searchData;
                }
                else if (this.SearchCon.SHUTURYOKU_KBN.Equals("3"))
                {
                    //電子データ
                    SearchDensiData = this.mDao.GetDensiReportData(this.SearchCon);
                    this.mSearchData = SearchDensiData;
                }
                else 
                {
                   //合算…紙マニフェスト＋電子マニフェストのデータを取得し 
                    //紙データ
                    searchData = this.mDao.GetReportData(this.SearchCon);
                    //電子データ
                    SearchDensiData = this.mDao.GetDensiReportData(this.SearchCon);
                    if (searchData != null && searchData.Rows.Count > 0)
                    {
                        //合算…紙マニフェスト＋電子マニフェストのデータを取得し 処理
                         tempData = searchData.AsEnumerable().CopyToDataTable();
                        if (SearchDensiData != null && SearchDensiData.Rows.Count > 0)
                        {
                            foreach (DataRow dtR in SearchDensiData.Rows)
                            {
                                DataRow newRow = tempData.NewRow();
                                foreach (DataColumn dtC in SearchDensiData.Columns)
                                {
                                    if (!string.IsNullOrEmpty(this.ChgDBNullToValue(dtR[dtC.ColumnName], string.Empty).ToString()))
                                    {
                                        newRow[dtC.ColumnName] = dtR[dtC.ColumnName];
                                    }
                                }

                                tempData.Rows.Add(newRow);
                            }
                        }
                    }
                    else if (SearchDensiData != null && SearchDensiData.Rows.Count > 0)
                    {
                        tempData = SearchDensiData;
                    }
                    this.mSearchData = tempData;
                }

                //読込み件数チェック
                if (this.mSearchData == null || this.mSearchData.Rows.Count <= 0)
                {
                    //読込データ件数を0にする                 
                    messageShowLogic.MessageBoxShow("W002", "交付等状況報告書");
                    return;
                }

                if (this.SearchCon.SHUTURYOKU_KBN.Equals("1"))
                {
                    //データ整備前に統合されたデータを並び替え
                    DataView dv = new DataView(mSearchData);
                    dv.Sort = "HST_GYOUSHAGENBA,CHOKKOU_ROUTE_NO,SYSTEM_ID,HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU,UPN_ROUTE_NO,UPN_GYOUSHA_NAME,UPN_SAKI_GENBA_ADDRESS";

                    tempData = null;
                    tempData = mSearchData.Clone();

                    foreach (DataRowView drv in dv)
                    {
                        tempData.ImportRow(drv.Row);
                    }
                    mSearchData = tempData;
                }

                //①事業場の名称、事業場の所在地を編集
                this.EditJGBNameToAdress();
                //②業種を編集
                this.EditGyoushu();
                //他社許可番号の記載処理
                this.EditTasyaKyokabanngou();
                //交付枚数編集
                this.EditConngouShuturyokuData();

                this.ReportInfoList = new Dictionary<string, object>();
                // データを設定する処理を入れる
                this.ReportInfoList.Add("Cread_Date", this.SearchCon.CREAD_DATE);
                this.ReportInfoList.Add("Koufu_Date_From", Convert.ToDateTime(this.form.HIDUKE_FROM.Value));
                this.ReportInfoList.Add("Genbashukei_Kbn", this.SearchCon.GENBASHUKEI_KBN);
                this.ReportInfoList.Add("SearchResult", this.mSearchData);

                ////ダイアログClose処理
                //var parentForm = (BasePopForm)this.form.Parent;
                //this.form.Close();
                //parentForm.Close();
                //交付等状況報告書一覧
                FormManager.OpenForm("G511", this.ReportInfoList);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Jikou", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                this.form.isSearchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 画面の検索条件取得
        /// </summary>
        private DtoCls GetCondition()
        {
            DtoCls SearchCon = new DtoCls();

            SearchCon.TEISHUTUSAKI_KBN = this.form.txtTeishutusakiKbn.Text.ToString();
            SearchCon.CHIIKI_CD = this.form.txtCHIIKI_CD.Text.ToString();
            SearchCon.CHIIKI_NM = this.form.txtCHIIKI_NM.Text.ToString();

            SearchCon.KONGOU_KBN = this.form.txtKongouKbn.Text.ToString();
            SearchCon.KOUFU_DATE_FROM = ((DateTime)this.form.HIDUKE_FROM.Value).ToString("yyyyMMdd");
            SearchCon.KOUFU_DATE_TO = ((DateTime)this.form.HIDUKE_TO.Value).ToString("yyyyMMdd");

            SearchCon.CREAD_DATE = this.form.dtpCreadDate.Text.ToString();
            SearchCon.SHUTURYOKU_KBN = this.form.txtShuturyokuKbn.Text.ToString();

            SearchCon.UPN_KYOKA_KBN = this.form.txtUPN_KYOKA_KBN.Text.ToString();
            SearchCon.SBN_KYOKA_KBN = this.form.txtSBN_KYOKA_KBN.Text.ToString();

            SearchCon.TITLE1 = this.form.txtTitle1.Text.ToString();

            SearchCon.GYOUSHU = this.form.txtGYOUSHU_CD.Text.ToString();
            SearchCon.GYOUSHASET_KBN1 = this.form.txtGYOUSHASetKbn1.Text.ToString();            
            SearchCon.GYOUSHASET_KBN2 = this.form.txtGYOUSHASetKbn2.Text.ToString();


            SearchCon.GYOUSHA_CD_FROM = this.form.txtGYOUSHA_CD.Text.ToString();
            SearchCon.GYOUSHA_CD_TO = this.form.txtGYOUSHA_CD_TO.Text.ToString();
            SearchCon.GENBA_CD_FROM = this.form.txtGENBA_CD.Text.ToString();
            SearchCon.GENBA_CD_TO = this.form.txtGENBA_CD_TO.Text.ToString();

            SearchCon.GENBASHUKEI_KBN = this.form.txtGenbaShukeiKbn.Text.ToString();

            SearchCon.JGB_NAME = this.form.txtGenbaNm.Text.ToString();
            SearchCon.JGB_ADDRESS = this.form.txtGenbaAddress.Text.ToString();
            SearchCon.CHIIKINM_KBN = this.form.txtChiikiNmKbn.Text.ToString();
            
            return SearchCon;
        }
        /// <summary>
        /// ①事業場の名称、事業場の所在地を編集
        /// </summary>
        private void EditJGBNameToAdress()
        {
            try
            {
                LogUtility.DebugMethodStart();
                DataTable dtData = this.mSearchData.AsEnumerable().CopyToDataTable();
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    foreach (DataRow dt in dtData.Rows)
                    {
                        //①事業場の名称を編集
                        // [現場一括の集計] = 1．する
                        if ("1".Equals(this.SearchCon.GENBASHUKEI_KBN))
                        {
                            // [提出先] = １．全体
                            if ("1".Equals(this.SearchCon.TEISHUTUSAKI_KBN))
                            {
                                // [地域名の印字] = ２．しない 
                                if ("2".Equals(this.SearchCon.CHIIKINM_KBN))
                                {
                                    // 事業場の名称に、画面上の事業場の名称を使う
                                    dt["JGB_NAME"] = this.SearchCon.JGB_NAME;
                                }
                                else
                                {
                                    //事業場の名称に、検出した現場ごとに地域名を頭に文字列結合し、画面上の事業場の名称を使う
                                    dt["JGB_NAME"] = dt["CHIIKI_NAME_RYAKU"] + this.SearchCon.JGB_NAME;
                                }
                            }
                            else
                            {
                                // [地域名の印字] = ２．しない
                                if ("2".Equals(this.SearchCon.CHIIKINM_KBN))
                                {
                                    //事業場の名称に、画面上の事業場の名称を使う
                                    dt["JGB_NAME"] = this.SearchCon.JGB_NAME;
                                }
                                else
                                {
                                    //事業場の名称に、都道府県政令市名を頭に文字列結合し、画面上の事業場の名称を使う
                                    dt["JGB_NAME"] = this.form.txtCHIIKI_NM.Text + this.SearchCon.JGB_NAME;
                                }
                            }
                        }

                        // 事業場の所在地：編集
                        // [現場一括の集計] = 1．する
                        if ("1".Equals(this.SearchCon.GENBASHUKEI_KBN))
                        {
                            // [地域名の印字] = ２．しない
                            if ("2".Equals(this.SearchCon.CHIIKINM_KBN))
                            {
                                // 抽出画面の入力値
                                dt["JGB_ADDRESS"] = this.SearchCon.JGB_ADDRESS;
                            }
                            else
                            {
                                // 現場の地域名 + 抽出画面の入力値
                                dt["JGB_ADDRESS"] = dt["CHIIKI_NAME_RYAKU"].ToString() + this.SearchCon.JGB_ADDRESS;
                            }
                        }
                        else
                        {
                            // 現場の都道府県 + 住所
                            dt["JGB_ADDRESS"] = dt["JGB_TODOUFUKEN_NAME"].ToString() + dt["JGB_ADDRESS"].ToString();
                        }

                        // 事業場の電話番号：編集
                        // [現場一括の集計] = 1. する
                        if ("1".Equals(this.SearchCon.GENBASHUKEI_KBN))
                        {
                            //一括集計なので電話番号は出せない。
                            dt["GENBA_TEL"] = "";
                        }
                    }
                }
                this.mSearchData = dtData;
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
        /// ②業種を編集
        /// </summary>
        private void EditGyoushu()
        {
            DataTable orgData = this.mSearchData.Copy();
            if (orgData != null && orgData.Rows.Count > 0)
            {
                foreach (DataRow dt in orgData.Rows)
                {
                    // 提出業者・事業場に紐付く業種をそれぞれ取得
                    var gyoushaGyoushu = dt["GYOUSHA_GYOUSHU_CD"].ToString();
                    var genbaGyoushu = dt["GENBA_GYOUSHU_CD"].ToString();

                    // 業種は以下の優先度で表示を行う
                    // 交付等状況報告書画面上の業種＞提出事業場に紐付く業種＞提出業者に紐付く業種＞ブランク
                    if(false == string.IsNullOrEmpty(this.form.txtGYOUSHU_CD.Text))
                    {
                        // 交付等状況報告書画面上の業種をセット
                        dt["GYOUSHU_CD"] = this.form.txtGYOUSHU_CD.Text;

                        // 画面上の表示は略称でいいが、結果は正式名称を使用する。
                        M_GYOUSHU gyoushu = gyoushuDao.GetDataByCd(this.form.txtGYOUSHU_CD.Text);
                        if (gyoushu != null)
                        {
                            dt["GYOUSHU_NAME"] = ChgDBNullToValue(gyoushu.GYOUSHU_NAME, string.Empty).ToString();
                        }
                    }
                    else if(false == string.IsNullOrEmpty(genbaGyoushu))
                    {
                        // 提出事業場に紐付く業種をセット
                        dt["GYOUSHU_CD"] = genbaGyoushu;
                        dt["GYOUSHU_NAME"] = dt["GENBA_GYOUSHU_NM"];
                    }
                    else if(false == string.IsNullOrEmpty(gyoushaGyoushu))
                    {
                        // 提出業者に紐付く業種をセット
                        dt["GYOUSHU_CD"] = gyoushaGyoushu;
                        dt["GYOUSHU_NAME"] = dt["GYOUSHA_GYOUSHU_NM"];
                    }
                    else
                    {
                        // 該当業種が存在しないためブランク表示
                        dt["GYOUSHU_CD"] = string.Empty;
                        dt["GYOUSHU_NAME"] = string.Empty;
                    }
                }

                orgData.AcceptChanges();
                //・「業者_業種CD」列を削除する「現場_業種CD」列を削除する	
                orgData.Columns.Remove("GYOUSHA_GYOUSHU_CD");
                orgData.Columns.Remove("GENBA_GYOUSHU_CD");
                //・「業者_業種名」列を削除する「現場_業種名」列を削除する	
                orgData.Columns.Remove("GYOUSHA_GYOUSHU_NM");
                orgData.Columns.Remove("GENBA_GYOUSHU_NM");

                this.mSearchData = orgData;
            }
        }
        /// <summary>
        /// 混合廃棄物、複数区間時の交付枚数編集
        /// </summary>
        private void EditConngouShuturyokuData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable objData = this.mSearchData.Copy();
                //詳細データテーブルを作成する。
                DataTable newData = this.mSearchData.Clone();
                //システムIDと運運搬情報を組み合わせ
                var resultBySYSTEMID_HAIKISHURUI = objData.AsEnumerable()
                       .GroupBy(
                           r => string.Format("{0},{1}",
                               r.Field<Int64>("SYSTEM_ID"),
                               r.Field<Int16>("HAIKI_KBN_CD")),
                           (ym, ymGroup) => new
                           {
                               ym,
                               ymggGroups = ymGroup.GroupBy(
                                   r2 => string.Format("{0},{1}",
                                       r2.Field<string>("UPN_GYOUSHA_NAME"),
                                       r2.Field<string>("UPN_SAKI_GENBA_ADDRESS")),
                                   (gg, ymggGroup) => new
                                   {
                                       gg,
                                       ymggGroup
                                   }
                               ).ToList()
                           }
                       ).ToList();

              
                //システムidGRUOPデータ取得
                foreach (var grpSYSTEMID in resultBySYSTEMID_HAIKISHURUI)
                {
                    if (grpSYSTEMID.ymggGroups.Count > 1)
                    {
                        string[] arrKeyTmp = grpSYSTEMID.ym.ToString().Split(',');
                        Int64 sysId;
                        Int16 haikiKbnCd;
                        Int64.TryParse(arrKeyTmp[0].ToString(), out sysId);
                        Int16.TryParse(arrKeyTmp[1].ToString(), out haikiKbnCd);

                        //複数廃棄物の場合
                        int count = grpSYSTEMID.ymggGroups.Count;
                        ////運搬受託者名、運搬先の住所GRUOPデータ取得
                        foreach (var grpHAIKISHURUI_UPN in grpSYSTEMID.ymggGroups)
                        {
                            var grp = grpHAIKISHURUI_UPN.ymggGroup;
                          
                            string[] arrTmp = grpHAIKISHURUI_UPN.gg.ToString().Split(',');
                            //産業廃棄物の種類GRUOP
                            var GrpUPNInfo = grpHAIKISHURUI_UPN.ymggGroup.GroupBy(r => new
                            {
                                HAIKI_SHURUI_NAME_RYAKU = r.Field<string>("HAIKI_SHURUI_NAME_RYAKU"),
                            },
                            (k, g) => new
                            {
                                HAIKI_SHURUI_NAME_RYAKU = k.HAIKI_SHURUI_NAME_RYAKU,
                                UPN_GYOUSHA_NAME = g.First().Field<string>("UPN_GYOUSHA_NAME"),
                                UPN_SAKI_GENBA_ADDRESS = g.First().Field<string>("UPN_SAKI_GENBA_ADDRESS"),
                             
                            }).ToList();
                            for (int i = 0; i < GrpUPNInfo.Count; i++)
                            {
                                foreach (DataRow dtRow in this.mSearchData.Rows)
                                {
                                    if (dtRow["SYSTEM_ID"].Equals(sysId)
                                        && dtRow["HAIKI_KBN_CD"].Equals(haikiKbnCd)
                                        && dtRow["UPN_GYOUSHA_NAME"].Equals(arrTmp[0])
                                        && dtRow["UPN_SAKI_GENBA_ADDRESS"].Equals(arrTmp[1]))
                                    {
                                        //交付枚数
                                        dtRow["COUFUMAISUU"] = 1;
                                    }
                                }
                            }                      
                        }
                    }
                    else
                    {
                        // SYSTEM_IDの1廃棄物の場合交付枚数設定
                        foreach (DataRow dtRow in this.mSearchData.Rows)
                        {
                            string[] arrKeyTmp = grpSYSTEMID.ym.ToString().Split(',');
                            Int64 sysId;
                            Int16 haikiKbnCd;
                            Int64.TryParse(arrKeyTmp[0].ToString(), out sysId);
                            Int16.TryParse(arrKeyTmp[1].ToString(), out haikiKbnCd);

                            if (dtRow["SYSTEM_ID"].Equals(sysId) && dtRow["HAIKI_KBN_CD"].Equals(haikiKbnCd))
                            {
                                dtRow["COUFUMAISUU"] = 1;
                                break;
                            }
                        }
                    }
                }
                this.mSearchData.AcceptChanges();


                #region   登録データの区間空っぽデータ時は削除です。
                objData = this.mSearchData.Copy();

                //システムIDと運運搬情報を組み合わせ
                var resultBySYSTEMID_HAIKISHURUI1 = objData.AsEnumerable()
                       .GroupBy(
                           r => string.Format("{0},{1}",
                               r.Field<Int64>("SYSTEM_ID"),
                               r.Field<Int16>("HAIKI_KBN_CD")),
                           (ym, ymGroup) => new
                           {
                               ym,
                               ymggGroups = ymGroup.GroupBy(
                                   r2 => string.Format("{0},{1}",
                                       r2.Field<string>("UPN_GYOUSHA_NAME"),
                                       r2.Field<string>("UPN_SAKI_GENBA_ADDRESS")),
                                   (gg, ymggGroup) => new
                                   {
                                       gg,
                                       ymggGroup
                                   }
                               ).ToList()
                           }
                       ).ToList();
                DataTable changeData = objData.Copy();

                //システムidGRUOPデータ取得
                foreach (var grpSYSTEMID in resultBySYSTEMID_HAIKISHURUI1)
                {
                    if (grpSYSTEMID.ymggGroups.Count > 1)
                    {
                        string[] arrKeyTmp = grpSYSTEMID.ym.ToString().Split(',');
                        Int64 sysId;
                        Int16 haikiKbnCd;
                        Int64.TryParse(arrKeyTmp[0].ToString(), out sysId);
                        Int16.TryParse(arrKeyTmp[1].ToString(), out haikiKbnCd);

                        //複数廃棄物の場合
                        int count = grpSYSTEMID.ymggGroups.Count;
                        ////運搬受託者名、運搬先の住所GRUOPデータ取得
                        foreach (var grpHAIKISHURUI_UPN in grpSYSTEMID.ymggGroups)
                        {
                            var grp = grpHAIKISHURUI_UPN.ymggGroup;

                            string[] arrTmp = grpHAIKISHURUI_UPN.gg.ToString().Split(',');
                            //産業廃棄物の種類GRUOP
                            var GrpUPNInfo = grpHAIKISHURUI_UPN.ymggGroup.GroupBy(r => new
                            {
                                HAIKI_SHURUI_NAME_RYAKU = r.Field<string>("HAIKI_SHURUI_NAME_RYAKU"),
                            },
                            (k, g) => new
                            {
                                HAIKI_SHURUI_NAME_RYAKU = k.HAIKI_SHURUI_NAME_RYAKU,
                                UPN_GYOUSHA_NAME = g.First().Field<string>("UPN_GYOUSHA_NAME"),
                                UPN_SAKI_GENBA_ADDRESS = g.First().Field<string>("UPN_SAKI_GENBA_ADDRESS"),

                            }).ToList();

                            for (int i = 0; i < GrpUPNInfo.Count; i++)
                            {
                                foreach (DataRow dtRow in objData.Rows)
                                {
                                    if (GrpUPNInfo.Count > 1 &&
                                        dtRow["SYSTEM_ID"].Equals(sysId)
                                        && dtRow["HAIKI_KBN_CD"].Equals(haikiKbnCd)
                                        && string.IsNullOrEmpty(this.ChgDBNullToValue(dtRow["UPN_GYOUSHA_NAME"], string.Empty).ToString())
                                        && string.IsNullOrEmpty(this.ChgDBNullToValue(dtRow["UPN_SAKI_GENBA_ADDRESS"], string.Empty).ToString())
                                        && dtRow["UPN_GYOUSHA_NAME"].Equals(arrTmp[0])
                                        && dtRow["UPN_SAKI_GENBA_ADDRESS"].Equals(arrTmp[1]))
                                    {
                                        string condition = " SYSTEM_ID = '" + sysId
                                                                                + "' AND HAIKI_KBN_CD = '" + haikiKbnCd
                                                                                + "' AND UPN_GYOUSHA_NAME = '" + GrpUPNInfo[i].UPN_GYOUSHA_NAME
                                                                                + "' AND UPN_SAKI_GENBA_ADDRESS = '" + GrpUPNInfo[i].UPN_SAKI_GENBA_ADDRESS
                                                                                + "' AND HAIKI_SHURUI_NAME_RYAKU = '" + GrpUPNInfo[i].HAIKI_SHURUI_NAME_RYAKU + "'";
                                        DataRow[] deleteRow = changeData.Select(condition);

                                        if (deleteRow.Length > 0)
                                        {
                                            for (int t = 0; t < deleteRow.Length; t++)
                                            {
                                                changeData.Rows.Remove(deleteRow[t]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                this.mSearchData = changeData;
                #endregion

                #region システムID重複時、2レコード目以降の交付枚数はブランクにする
                objData = this.mSearchData.Copy();
                List<string> sysIdList = new List<string>();

                for (int i = 0; i < objData.Rows.Count; i++)
                {
                    if (sysIdList.Contains(objData.Rows[i]["HAIKI_KBN_CD"].ToString() + ":" + objData.Rows[i]["SYSTEM_ID"].ToString()))
                    {
                        objData.Rows[i]["COUFUMAISUU"] = "0";
                    }
                    else
                    {
                        sysIdList.Add(objData.Rows[i]["HAIKI_KBN_CD"].ToString() + ":" + objData.Rows[i]["SYSTEM_ID"].ToString());
                    }
                }
                this.mSearchData = objData;
                #endregion


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
        /// 他社許可番号の記載処理
        /// </summary>
        private void EditTasyaKyokabanngou()
        {
            try
            {
                LogUtility.DebugMethodStart();
                string  strUPN_KYOKA_KBN =this.form.txtUPN_KYOKA_KBN.Text;
                string strSBN_KYOKA_KBN = this.form.txtSBN_KYOKA_KBN.Text;
                
                DataTable dtData = this.mSearchData.AsEnumerable().CopyToDataTable();
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    foreach (DataRow dt in dtData.Rows)
                    {
                        // ■他社運搬許可番号の記載（1:記載する 2:しない）
                        if (strUPN_KYOKA_KBN.Equals("2"))
                        {
                            // 0:他社 1:自社
                            if (dt["UPN_JISHA_KBN"].Equals(0) || dt["UPN_JISHA_KBN"].Equals(false))
                            {
                                dt["UPN_FUTSUU_KYOKA_NO"] = string.Empty;
                            }
                        }

                        // ■他社処分許可番号の記載（1:記載する 2:しない）
                        if (strSBN_KYOKA_KBN.Equals("2"))
                        {
                            // 0:他社 1:自社
                            if (dt["SBN_GENBA_JISHA_KBN"].Equals(0) || dt["SBN_GENBA_JISHA_KBN"].Equals(false))
                            {
                                dt["SBN_FUTSUU_KYOKA_NO"] = string.Empty;
                            }
                        }
                    }
                }
                dtData.AcceptChanges();
                this.mSearchData = dtData;
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
        #endregion

        #region 終了処理（F12）

        /// <summary>
        /// 終了する。
        /// </summary>
        public void FormClose()
        {
            try
            { 
                var parentForm = (BasePopForm)this.form.Parent;             

                this.form.Close();
                parentForm.Close();
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

        #endregion                           

        #region uitility

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return value;
                    else
                        return obj;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// 20141021 Houkakou 「交付等状況報告書」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.HIDUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.HIDUKE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                this.form.HIDUKE_TO.IsInputErrorOccured = true;
                this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "交付年月日From", "交付年月日To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.HIDUKE_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion
        /// 20141021 Houkakou 「交付等状況報告書」の日付チェックを追加する　end


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

        #endregion

        #region ベースフォームのメッソドの実例化(ロジックは実装しない)

        /// <summary>
        ///データ検索処理
        /// </summary>
        public int Search()
        {
            return 0;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {

        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
        {

        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {

        }
        #endregion   

        /// 20141023 Houkakou 「交付等状況報告書」のダブルクリックを追加する　start
        #region HIDUKE_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.HIDUKE_FROM;
            var ToTextBox = this.form.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region txtGYOUSHA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGYOUSHA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtGYOUSHA_CD;
            var ToTextBox = this.form.txtGYOUSHA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtGYOUSHA_NAME_TO.Text = this.form.txtGYOUSHA_NAME.Text;


            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region txtGENBA_CD_TOダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGENBA_CD_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.txtGENBA_CD;
            var ToTextBox = this.form.txtGENBA_CD_TO;

            ToTextBox.Text = FromTextBox.Text;
            this.form.txtGENBA_NAME_TO.Text = this.form.txtGENBA_NAME.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141023 Houkakou 「交付等状況報告書」のダブルクリックを追加する　end

        #region 交付年月日範囲チェック
        /// <summary>
        /// 交付年月日範囲チェック
        /// </summary>
        /// <param name="limitKbn">
        /// 上限と下限どちらをチェックするか
        /// 0 = 上限チェック
        /// 1 = 下限チェック
        /// </param>
        /// <returns></returns>
        internal bool KoufuDateRangeCheck(int limitKbn)
        {
            // 戻り値初期化
            bool ren = true;
            try
            {
                // 上限チェック
                if (limitKbn == 0)
                {
                    // Fromチェック
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        if (this.form.HIDUKE_FROM.MaxValue.Year <= Convert.ToDateTime(this.form.HIDUKE_FROM.Text).Year)
                        {
                            this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                            this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E221", "［" + this.form.HIDUKE_FROM.MaxValue.ToString("yyyy/MM/dd") + "］");
                            this.form.HIDUKE_FROM.Focus();
                            ren = false;
                        }
                    }
                    // Toチェック
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text) && ren)
                    {
                        if (this.form.HIDUKE_TO.MaxValue.Year <= Convert.ToDateTime(this.form.HIDUKE_TO.Text).Year)
                        {
                            this.form.HIDUKE_TO.IsInputErrorOccured = true;
                            this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E221", "［" + this.form.HIDUKE_TO.MaxValue.ToString("yyyy/MM/dd") + "］");
                            this.form.HIDUKE_TO.Focus();
                            ren = false;
                        }
                    }
                }
                // 下限チェック
                else if (limitKbn == 1)
                {
                    // Fromチェック
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
                    {
                        if (Convert.ToDateTime(this.form.HIDUKE_FROM.Text).Year <= this.form.HIDUKE_FROM.MinValue.Year)
                        {
                            this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                            this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E078", "［" + this.form.HIDUKE_FROM.MinValue.ToString("yyyy/MM/dd") + "］");
                            this.form.HIDUKE_FROM.Focus();
                            ren = false;
                        }
                    }
                    // Toチェック
                    if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text) && ren)
                    {
                        if (Convert.ToDateTime(this.form.HIDUKE_TO.Text).Year <= this.form.HIDUKE_TO.MinValue.Year)
                        {
                            this.form.HIDUKE_TO.IsInputErrorOccured = true;
                            this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E078", "［" + this.form.HIDUKE_TO.MinValue.ToString("yyyy/MM/dd") + "］");
                            this.form.HIDUKE_TO.Focus();
                            ren = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("KoufuDateRangeCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ren = false;
            }
            return ren;
        }
        #endregion 交付年月日範囲チェック

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        internal void GyoushaValidated(object sender)
        {
            try
            {
                var gyousha = (CustomAlphaNumTextBox)sender;
                var gyoushaNm = new CustomTextBox();
                var message = new MessageBoxShowLogic();

                switch (gyousha.Name)
                {
                    case "txtGYOUSHA_CD":
                        gyoushaNm = this.form.txtGYOUSHA_NAME;
                        break;
                    case "txtGYOUSHA_CD_TO":
                        gyoushaNm = this.form.txtGYOUSHA_NAME_TO;
                        break;
                }

                if (string.IsNullOrEmpty(gyousha.Text))
                {
                    gyoushaNm.Text = string.Empty;
                    return;
                }

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = gyousha.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var entitys = this.gyoushaDao.GetAllValidData(entity);
                if (entitys == null || entitys.Length == 0)
                {
                    message.MessageBoxShow("E020", "業者");
                    gyoushaNm.Text = string.Empty;
                    gyousha.Focus();
                }
                else
                {
                    if (entitys[0].HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsTrue)
                    {
                        gyoushaNm.Text = entitys[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        message.MessageBoxShow("E028");
                        gyoushaNm.Text = string.Empty;
                        gyousha.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        internal void GenbaValidated(object sender)
        {
            try
            {
                var genba = (CustomAlphaNumTextBox)sender;
                var genbaNm = new CustomTextBox();
                var message = new MessageBoxShowLogic();

                switch (genba.Name)
                {
                    case "txtGENBA_CD":
                        genbaNm = this.form.txtGENBA_NAME;
                        break;
                    case "txtGENBA_CD_TO":
                        genbaNm = this.form.txtGENBA_NAME_TO;
                        break;
                }

                if (string.IsNullOrEmpty(genba.Text))
                {
                    genbaNm.Text = string.Empty;
                    return;
                }
                if (string.IsNullOrEmpty(this.form.txtGYOUSHA_CD.Text))
                {
                    message.MessageBoxShow("E051", "提出業者");
                    genba.Text = string.Empty;
                    genba.Focus();
                    return;
                }

                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.txtGYOUSHA_CD.Text;
                entity.GENBA_CD = genba.Text;
                entity.ISNOT_NEED_DELETE_FLG = true;
                var entitys = this.genbaDao.GetAllValidData(entity);
                if (entitys == null || entitys.Length == 0)
                {
                    message.MessageBoxShow("E020", "現場");
                    genbaNm.Text = string.Empty;
                    genba.Focus();
                }
                else
                {
                    if (entitys[0].HAISHUTSU_NIZUMI_GENBA_KBN.IsTrue)
                    {
                        genbaNm.Text = entitys[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        message.MessageBoxShow("E028");
                        genbaNm.Text = string.Empty;
                        genba.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
        }

        #region 提出業者が未設定の時に、最小と最大を設定する

        internal bool SetTeishutsuGyousha()
        {
            bool ret = true;
            try
            {
                DataTable dtMin = this.mDao.GetMinTeishutsuGyousha(new DtoCls());
                if (dtMin != null && dtMin.Rows.Count > 0)
                {
                    this.form.txtGYOUSHA_CD.Text = dtMin.Rows[0]["CD"].ToString();
                    this.form.txtGYOUSHA_NAME.Text = dtMin.Rows[0]["NAME"].ToString();
                }

                DataTable dtMax = this.mDao.GetMaxTeishutsuGyousha(new DtoCls());
                if (dtMax != null && dtMax.Rows.Count > 0)
                {
                    this.form.txtGYOUSHA_CD_TO.Text = dtMax.Rows[0]["CD"].ToString();
                    this.form.txtGYOUSHA_NAME_TO.Text = dtMax.Rows[0]["NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            return ret;
        }

        internal bool SetTeishutsuJigyoujou()
        {
            bool ret = true;
            try
            {
                if (this.form.txtGYOUSHA_CD.Text == "")
                {
                    DataTable dtGyoMin = this.mDao.GetMinTeishutsuGyousha(new DtoCls());
                    if (dtGyoMin != null && dtGyoMin.Rows.Count > 0)
                    {
                        this.form.txtGYOUSHA_CD.Text = dtGyoMin.Rows[0]["CD"].ToString();
                        this.form.txtGYOUSHA_NAME.Text = dtGyoMin.Rows[0]["NAME"].ToString();
                    }
                    else
                    {
                        ret = false;
                    }
                }

                if (ret)
                {
                    DtoCls dto = new DtoCls();
                    dto.GYOUSHA_CD_FROM = this.form.txtGYOUSHA_CD.Text;

                    DataTable dtMin = this.mDao.GetMinTeishutsuJigyoujou(dto);
                    if (dtMin != null && dtMin.Rows.Count > 0)
                    {
                        this.form.txtGENBA_CD.Text = dtMin.Rows[0]["CD"].ToString();
                        this.form.txtGENBA_NAME.Text = dtMin.Rows[0]["NAME"].ToString();
                    }

                    DataTable dtMax = this.mDao.GetMaxTeishutsuJigyoujou(dto);
                    if (dtMax != null && dtMax.Rows.Count > 0)
                    {
                        this.form.txtGENBA_CD_TO.Text = dtMax.Rows[0]["CD"].ToString();
                        this.form.txtGENBA_NAME_TO.Text = dtMax.Rows[0]["NAME"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            return ret;
        }

        #endregion
    }
}