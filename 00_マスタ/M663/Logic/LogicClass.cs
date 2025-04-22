using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.Master.CourseIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.CourseIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// フッター
        /// </summary>
        public BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッター
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// ソート設定情報
        /// </summary>
        private SortSettingInfo sortSettingInfo = null;

        /// <summary>
        /// フィルタ設定情報
        /// </summary>
        private SearchSettingInfo searchSettingInfo = null;

        internal DataTable searchResult;
        internal DataTable viewData;
        internal DAOClass dao;
        internal DTOClass dto;
        private IM_COURSE_NAMEDao courseNameDao;
        private IM_GYOUSHADao gyoushaDao;
        private IM_GENBADao genbaDao;
        private IM_HINMEIDao hinmeiDao;
        private IM_SYS_INFODao sysInfoDao;
        internal MessageBoxShowLogic msgLogic;
        internal M_SYS_INFO sysInfo;

        string sortSql = string.Empty;
        string filterSql = string.Empty;

        /// <summary>
        /// チェックボックスのスペースキー対応用
        /// </summary>
        private bool SpaceChk = false;
        private bool SpaceON = false;

        #endregion

        #region プロパティ

        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.courseNameDao = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.sortSettingInfo = SortSettingHelper.LoadSortSettingInfo("UIForm.customDataGridView1");
            this.searchSettingInfo = SearchSettingHelper.LoadSearchSettingInfo("UIForm.customDataGridView1");
            this.msgLogic = new MessageBoxShowLogic();

            this.sysInfo = sysInfoDao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            this.allControl = this.form.allControl;

            var ribbon = (RibbonMainMenu)this.parentForm.ribbonForm;

            // ヘッダー（フッター）を初期化
            this.HeaderInit();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            Init();

            // オプション非対応
            if (!AppConfig.AppOptions.IsMAPBOX())
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // mapbox用ボタン無効化
                parentForm.bt_process2.Text = string.Empty;
                parentForm.bt_process2.Enabled = false;
            }
            else
            {
                // 一覧内のチェックボックスの設定
                this.HeaderCheckBoxSupport();
            }

            // 表示条件初期化
            this.RemoveIchiranHyoujiJoukenEvent();
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            this.AddIchiranHyoujiJoukenEvent();
            if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
            {
                this.SetHyoujiJoukenInit();
            }

            this.form.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.headForm = targetHeader;

            LogUtility.DebugMethodEnd();
        }

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            parentForm = (BusinessBaseForm)this.form.Parent;

            //Functionボタンのイベント生成
            parentForm.bt_func3.Click += new System.EventHandler(this.form.bt_func3_Click);       // F3 修正
            parentForm.bt_func5.Click += new System.EventHandler(this.form.bt_func5_Click);       // F5 複写
            parentForm.bt_func6.Click += new System.EventHandler(this.form.bt_func6_Click);       // F6 CSV出力
            parentForm.bt_func7.Click += new System.EventHandler(this.form.bt_func7_Click);       // F7 検索条件クリア
            this.form.C_Regist(parentForm.bt_func8);
            parentForm.bt_func8.Click += new System.EventHandler(this.form.bt_func8_Click);       // F8 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.form.bt_func10_Click);     // F10 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.form.bt_func11_Click);     // F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.form.bt_func12_Click);     // 閉じる
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);        // パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);        // 地図を表示

            this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(this.DetailCellClick);
            this.form.customDataGridView1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.DetailPreviewKeyDown);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        public bool Shuusei()
        {
            LogUtility.DebugMethodStart();
            try
            {
                DataGridViewCell cell = this.form.customDataGridView1.CurrentCell;
                if (cell != null)
                {
                    int index = cell.RowIndex;
                    string dayCd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_DAY_CD"].Value);
                    string courseNameCd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_COURSE_NAME_CD"].Value);
                    string courseName = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_COURSE_NAME"].Value);
                    string kyotenCd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_KYOTEN_CD"].Value);
                    string kyotenName = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_KYOTEN_NAME"].Value);

                    // 修正モードの権限チェック
                    if (Manager.CheckAuthority("M232", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth("M232", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, dayCd, courseNameCd, courseName, kyotenCd, kyotenName);
                    }
                    // 参照モードの権限チェック
                    else if (Manager.CheckAuthority("M232", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth("M232", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, dayCd, courseNameCd, courseName, kyotenCd, kyotenName);
                    }
                    else
                    {
                        // 修正モードの権限なしのアラームを上げる
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                }
                else
                {
                    //アラートを表示し、画面遷移しない
                    this.msgLogic.MessageBoxShow("E051", "対象データ");
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Shuusei", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #region CSV
        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            LogUtility.DebugMethodStart();
            try
            {
                // CSV出力用DataGridView生成
                var dgv = new CustomDataGridView();
                dgv.Visible = false;
                this.form.Controls.Add(dgv);

                // DataTableを生成
                var table = new DataTable();

                foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns)
                {
                    if (col.Visible == true)
                    {
                        // 必須記号は削除
                        var name = col.HeaderText.Replace("※", "");

                        // 表示列かつボタン以外だった場合、明細部のColumnを生成
                        table.Columns.Add(name, typeof(string));
                    }
                }

                // 各DataをDataTableにセット
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    // 新規行生成
                    var row = table.NewRow();

                    foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns)
                    {
                        if (col.Visible == true)
                        {
                            // 必須記号は削除
                            var name = col.HeaderText.Replace("※", "");

                            // 表示列かつボタン以外だった場合、明細部の値を格納
                            row[name] = dgvRow.Cells[col.Name].FormattedValue.ToString();
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
                CSVExport.ConvertCustomDataGridViewToCsv(dgv, true, true, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_COURSE_ICHIRAN), this.form);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
        #endregion

        #region 条件ｸﾘｱ

        /// <summary>
        /// 条件ｸﾘｱ
        /// </summary>
        public void ClearScreen()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // 曜日
                this.form.DAY_CD.Text = "8";

                // コース名
                this.form.COURSE_NAME_CD.Text = string.Empty;
                this.form.COURSE_NAME.Text = string.Empty;

                // 業者
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;

                // 現場
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME.Text = string.Empty;

                // 品名
                this.form.HINMEI_CD.Text = string.Empty;
                this.form.HINMEI_NAME.Text = string.Empty;

                this.headForm.SEARCH_CNT.Text = "0";

                this.form.customSortHeader1.ClearCustomSortSetting();
                this.form.customSearchHeader1.ClearCustomSearchSetting();

                if (this.searchResult != null)
                {
                    this.searchResult.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索

        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int ret_cnt = 0;

            try
            {
                if (this.form.RegistErrorFlag)
                {
                    return -1;
                }

                //SELECT句未取得なら検索できない
                if (!string.IsNullOrEmpty(this.form.SelectQuery))
                {
                    // ベースロジッククラスで作成したクエリをセット
                    this.selectQuery = this.form.SelectQuery;
                    this.orderByQuery = this.form.OrderByQuery;
                    this.joinQuery = this.form.JoinQuery;

                    var sql = new StringBuilder();
                    sql.Append(" SELECT DISTINCT ");
                    sql.Append(this.selectQuery);
                    sql.Append(", MC.DAY_CD AS HIDDEN_DAY_CD");
                    sql.Append(", MC.COURSE_NAME_CD AS HIDDEN_COURSE_NAME_CD");
                    sql.Append(", MCNAME.COURSE_NAME AS HIDDEN_COURSE_NAME");
                    sql.Append(", MCNAME.KYOTEN_CD AS HIDDEN_KYOTEN_CD");
                    sql.Append(", KYOTEN.KYOTEN_NAME_RYAKU AS HIDDEN_KYOTEN_NAME");
                    sql.Append(", LOCATION_IS_NULL.LOCATION AS HIDDEN_LOCATION");
                    sql.Append(" FROM M_COURSE MC");
                    sql.Append(" LEFT JOIN M_COURSE_DETAIL MCD");
                    sql.Append(" ON MC.DAY_CD = MCD.DAY_CD");
                    sql.Append(" AND MC.COURSE_NAME_CD = MCD.COURSE_NAME_CD");
                    sql.Append(" LEFT JOIN M_COURSE_DETAIL_ITEMS MCDI");
                    sql.Append(" ON MCD.DAY_CD = MCDI.DAY_CD");
                    sql.Append(" AND MCD.COURSE_NAME_CD = MCDI.COURSE_NAME_CD");
                    sql.Append(" AND MCD.REC_ID = MCDI.REC_ID");
                    sql.Append(" LEFT JOIN M_COURSE_NIOROSHI MCN");
                    sql.Append(" ON MCDI.DAY_CD = MCN.DAY_CD");
                    sql.Append(" AND MCDI.COURSE_NAME_CD = MCN.COURSE_NAME_CD");
                    sql.Append(" AND MCDI.NIOROSHI_NO = MCN.NIOROSHI_NO");
                    sql.Append(" LEFT JOIN M_COURSE_NAME MCNAME");
                    sql.Append(" ON MC.COURSE_NAME_CD = MCNAME.COURSE_NAME_CD");
                    sql.Append(" LEFT JOIN M_KYOTEN KYOTEN");
                    sql.Append(" ON MCNAME.KYOTEN_CD = KYOTEN.KYOTEN_CD");
                    sql.Append(" LEFT JOIN M_GENBA MG ");
                    sql.Append(" ON MG.GYOUSHA_CD = MCD.GYOUSHA_CD ");
                    sql.Append(" AND MG.GENBA_CD = MCD.GENBA_CD ");

                    sql.Append(this.joinQuery);

                    sql.Append(" LEFT JOIN (SELECT DAY_CD, COURSE_NAME_CD, 1 AS 'LOCATION' FROM M_COURSE_DETAIL LEFT JOIN M_GENBA ON M_COURSE_DETAIL.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_COURSE_DETAIL.GENBA_CD = M_GENBA.GENBA_CD WHERE M_GENBA.GENBA_LATITUDE IS NOT NULL AND M_GENBA.GENBA_LATITUDE != '') AS LOCATION_IS_NULL ON LOCATION_IS_NULL.DAY_CD = MC.DAY_CD and LOCATION_IS_NULL.COURSE_NAME_CD = MC.COURSE_NAME_CD ");

                    sql.Append(" WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(this.form.DAY_CD.Text) && this.form.DAY_CD.Text != "8")
                    {
                        sql.AppendFormat(" AND MC.DAY_CD = '{0}'", this.form.DAY_CD.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.COURSE_NAME_CD.Text))
                    {
                        sql.AppendFormat(" AND MC.COURSE_NAME_CD = '{0}'", this.form.COURSE_NAME_CD.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        sql.AppendFormat(" AND MCD.GYOUSHA_CD = '{0}'", this.form.GYOUSHA_CD.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        sql.AppendFormat(" AND MCD.GENBA_CD = '{0}'", this.form.GENBA_CD.Text);
                    }
                    if (!string.IsNullOrEmpty(this.form.HINMEI_CD.Text))
                    {
                        sql.AppendFormat(" AND MCDI.HINMEI_CD = '{0}'", this.form.HINMEI_CD.Text);
                    }
                    if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                    {
                        sql.Append(" AND MG.DELETE_FLG = 0");
                    }
                    if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                    {
                        sql.Append(" AND (1 = 0");
                    }
                    if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked)
                    {
                        sql.Append(" OR (((MG.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MG.TEKIYOU_END) or (MG.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and MG.TEKIYOU_END IS NULL) or (MG.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= MG.TEKIYOU_END) or (MG.TEKIYOU_BEGIN IS NULL and MG.TEKIYOU_END IS NULL)) and MG.DELETE_FLG = 0)");
                    }
                    if (this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                    {
                        sql.Append(" OR MG.DELETE_FLG = 1");
                    }
                    if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                    {
                        sql.Append(" OR ((MG.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > MG.TEKIYOU_END) and MG.DELETE_FLG = 0)");
                    }
                    if (this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked || this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                    {
                        sql.Append(")");
                    }

                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);

                    this.searchResult = new DataTable();
                    if (!string.IsNullOrEmpty(sql.ToString()))
                    {
                        this.searchResult = this.dao.GetDataBySql(sql.ToString());
                    }
                    ret_cnt = this.searchResult.Rows.Count;

                    this.HeaderCheckBoxFalse();

                    //検索結果表示
                    this.form.ShowData();

                    if (this.searchResult == null || this.searchResult.Rows.Count == 0)
                    {
                        this.headForm.SEARCH_CNT.Text = "0";
                        msgLogic.MessageBoxShow("C001");

                        return 0;
                    }
                }
                else
                {
                    var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                ret_cnt = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret_cnt = -1;
            }
            LogUtility.DebugMethodEnd();

            return ret_cnt;
        }

        #endregion

        #region 画面クリア

        /// <summary>
        /// 画面クリア
        /// </summary>
        public void Init()
        {
            LogUtility.DebugMethodStart();

            try
            {
                if (!sysInfo.ICHIRAN_ALERT_KENSUU.IsNull)
                {
                    this.headForm.ALERT_CNT.Text = Convert.ToString(sysInfo.ICHIRAN_ALERT_KENSUU.Value);
                }
                else
                {
                    this.headForm.ALERT_CNT.Text = "1";
                }
                this.headForm.SEARCH_CNT.Text = "0";

                // 曜日
                this.form.DAY_CD.Text = "8";
                this.form.DAY_CD_8.Checked = true;

                // コース名
                if (string.IsNullOrEmpty(Properties.Settings.Default.COURSE_NAME_CD))
                {
                    this.form.COURSE_NAME_CD.Text = string.Empty;
                    this.form.COURSE_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.COURSE_NAME_CD.Text = Properties.Settings.Default.COURSE_NAME_CD;
                    M_COURSE_NAME key = new M_COURSE_NAME();
                    key.COURSE_NAME_CD = this.form.COURSE_NAME_CD.Text;
                    M_COURSE_NAME data = this.courseNameDao.GetAllValidData(key).FirstOrDefault();
                    if (data != null)
                    {
                        this.form.COURSE_NAME.Text = data.COURSE_NAME_RYAKU;
                    }
                }

                // 業者
                if (dto != null)
                {
                    this.form.GYOUSHA_CD.Text = dto.GYOUSHA_CD;
                    dto.GYOUSHA_CD = string.Empty;
                    M_GYOUSHA data = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (data != null)
                    {
                        this.form.GYOUSHA_NAME.Text = data.GYOUSHA_NAME_RYAKU;
                    }
                }
                else if (string.IsNullOrEmpty(Properties.Settings.Default.GYOUSHA_CD))
                {
                    this.form.GYOUSHA_CD.Text = string.Empty;
                    this.form.GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD;
                    M_GYOUSHA data = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (data != null)
                    {
                        this.form.GYOUSHA_NAME.Text = data.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 現場
                if (dto != null)
                {
                    this.form.GENBA_CD.Text = dto.GENBA_CD;
                    dto.GENBA_CD = string.Empty;
                    M_GENBA data = new M_GENBA();
                    data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    data.GENBA_CD = this.form.GENBA_CD.Text;
                    data = this.genbaDao.GetDataByCd(data);
                    if (data != null)
                    {
                        this.form.GENBA_NAME.Text = data.GENBA_NAME_RYAKU;
                    }
                }
                else if (string.IsNullOrEmpty(Properties.Settings.Default.GENBA_CD) || string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD;
                    M_GENBA data = new M_GENBA();
                    data.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    data.GENBA_CD = this.form.GENBA_CD.Text;
                    data = this.genbaDao.GetDataByCd(data);
                    if (data != null)
                    {
                        this.form.GENBA_NAME.Text = data.GENBA_NAME_RYAKU;
                    }
                }

                // 品名
                if (string.IsNullOrEmpty(Properties.Settings.Default.HINMEI_CD))
                {
                    this.form.HINMEI_CD.Text = string.Empty;
                }
                else
                {
                    this.form.HINMEI_CD.Text = Properties.Settings.Default.HINMEI_CD;
                    M_HINMEI data = this.hinmeiDao.GetDataByCd(this.form.HINMEI_CD.Text);
                    if (data != null)
                    {
                        this.form.HINMEI_NAME.Text = data.HINMEI_NAME_RYAKU;
                    }
                }
                getCourseName();
                //並び順ソートヘッダー
                sortSettingInfo.Clear();
                this.form.customSortHeader1.Text = sortSettingInfo.SortSettingCaption;

                //フィルタ
                searchSettingInfo.Clear();
                this.form.searchString.Text = searchSettingInfo.SearchSettingCaption;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        /// <summary>
        /// システム上必須な項目を非表示にします
        /// </summary>
        internal void HideColumnHeader()
        {
            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (
                    column.Name.Equals("HIDDEN_DAY_CD") ||
                    column.Name.Equals("HIDDEN_COURSE_NAME_CD") ||
                    column.Name.Equals("HIDDEN_COURSE_NAME") ||
                    column.Name.Equals("HIDDEN_KYOTEN_CD") ||
                    column.Name.Equals("HIDDEN_KYOTEN_NAME") ||
                    column.Name.Equals("HIDDEN_LOCATION")
                    )
                {
                    column.Visible = false;
                }
            }
        }

        /// <summary>
        /// 地図表示のチェックボックスを使用可能にする
        /// </summary>
        internal void notReadOnlyColumns()
        {
            foreach (DataGridViewColumn col in this.form.customDataGridView1.Columns)
            {
                // 現状「地図表示」のチェックのみ
                if (col.Name == ConstClass.DATA_TAISHO)
                {
                    col.ReadOnly = false;
                }
            }
        }

        public bool getCourseName()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // コース名称ポップアップ設定
                M_COURSE_NAME courseNameEntity = new M_COURSE_NAME();
                DataTable dt = dao.GetCourseNameData(courseNameEntity);

                // TableNameを設定すれば、ポップアップのタイトルになる
                dt.TableName = "コース名称検索";

                // ポップアップ設定
                this.form.COURSE_NAME_CD.PopupWindowId = WINDOW_ID.M_COURSE_NAME;
                this.form.COURSE_NAME_CD.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
                this.form.COURSE_NAME_CD.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称" };
                this.form.COURSE_NAME_CD.PopupDataSource = dt;
                this.form.COURSE_NAME_POPUP.PopupWindowId = WINDOW_ID.M_COURSE_NAME;
                this.form.COURSE_NAME_POPUP.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
                this.form.COURSE_NAME_POPUP.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称" };
                this.form.COURSE_NAME_POPUP.PopupDataSource = dt;
                this.form.COURSE_NAME_CD_FUKUSHA.PopupWindowId = WINDOW_ID.M_COURSE_NAME;
                this.form.COURSE_NAME_CD_FUKUSHA.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
                this.form.COURSE_NAME_CD_FUKUSHA.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称" };
                this.form.COURSE_NAME_CD_FUKUSHA.PopupDataSource = dt;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getCourseName", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getCourseName", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #region Validated

        #region コース Validated

        /// <summary>
        /// コース Validated
        /// </summary>
        internal void COURSE_NAME_CD_Validated()
        {
            string cd = this.form.COURSE_NAME_CD.Text;
            if (cd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;
                if (string.IsNullOrEmpty(cd))
                {
                    this.form.COURSE_NAME.Text = "";
                    return;
                }

                M_COURSE_NAME data = new M_COURSE_NAME();
                data.COURSE_NAME_CD = cd;
                data.ISNOT_NEED_DELETE_FLG = true;
                data = courseNameDao.GetAllValidData(data).FirstOrDefault();
                if (data == null)
                {
                    this.form.isError = true;
                    this.form.COURSE_NAME.Text = "";
                    this.form.COURSE_NAME_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "コース");
                    this.form.COURSE_NAME_CD.Focus();
                }
                else
                {
                    this.form.COURSE_NAME.Text = data.COURSE_NAME_RYAKU;
                }
            }
        }

        #endregion

        #region 業者 Validated

        /// <summary>
        /// 業者 Validated
        /// </summary>
        internal void GYOUSHA_CD_Validated()
        {
            string cd = this.form.GYOUSHA_CD.Text;
            if (cd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;

                if (string.IsNullOrEmpty(cd))
                {
                    this.form.GYOUSHA_NAME.Text = "";
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                    return;
                }

                M_GYOUSHA data = gyoushaDao.GetDataByCd(cd);
                if (data == null)
                {
                    this.form.isError = true;
                    this.form.GYOUSHA_NAME.Text = "";
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                    this.form.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.GYOUSHA_CD.Focus();
                }
                else
                {
                    this.form.GYOUSHA_NAME.Text = data.GYOUSHA_NAME_RYAKU;
                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                }
            }
        }

        #endregion

        #region 現場 Validated

        /// <summary>
        /// 現場 Validated
        /// </summary>
        internal void GENBA_CD_Validated()
        {
            string gyoushaCd = this.form.GYOUSHA_CD.Text;
            string genbaCd = this.form.GENBA_CD.Text;
            if (genbaCd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;

                if (string.IsNullOrEmpty(genbaCd))
                {
                    this.form.GENBA_NAME.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    this.form.isError = true;
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = "";
                    this.form.GENBA_CD.Focus();
                    return;
                }

                M_GENBA genba = new M_GENBA();
                genba.GYOUSHA_CD = gyoushaCd;
                genba.GENBA_CD = genbaCd;
                genba = genbaDao.GetDataByCd(genba);
                if (genba == null)
                {
                    this.form.isError = true;
                    this.form.GENBA_NAME.Text = "";
                    this.form.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                }
                else
                {
                    this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                }
                return;
            }
        }

        #endregion

        #region 品名 Validated

        /// <summary>
        /// 品名 Validated
        /// </summary>
        internal void HINMEI_CD_Validated()
        {
            string cd = this.form.HINMEI_CD.Text;
            if (cd != this.form.beforeCd || this.form.isError)
            {
                this.form.isError = false;

                if (string.IsNullOrEmpty(cd))
                {
                    this.form.HINMEI_NAME.Text = "";
                    return;
                }

                M_HINMEI data = hinmeiDao.GetDataByCd(cd);
                if (data == null)
                {
                    this.form.isError = true;
                    this.form.HINMEI_NAME.Text = "";
                    this.form.HINMEI_CD.BackColor = Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "品名");
                    this.form.HINMEI_CD.Focus();
                }
                else
                {
                    this.form.HINMEI_NAME.Text = data.HINMEI_NAME_RYAKU;
                }
            }
        }

        #endregion

        #endregion

        #region 必須
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// 一覧表示イベントの削除
        /// </summary>
        public void RemoveIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        /// <summary>
        /// 一覧表示イベントの追加
        /// </summary>
        public void AddIchiranHyoujiJoukenEvent()
        {
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            // 一覧表示イベントの削除
            this.RemoveIchiranHyoujiJoukenEvent();

            if (this.sysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.sysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            }
            else
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
            }

            // 一覧表示イベントの追加
            this.AddIchiranHyoujiJoukenEvent();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 複写曜日変更
        /// </summary>
        public void SetFukushaDay()
        {
            LogUtility.DebugMethodStart();
            M_COURSE_NAME courseNameEntity = new M_COURSE_NAME();

            switch (this.form.DAY_CD_FUKUSHA.Text)
            {
                case "1":
                    this.form.DAY_NAME_FUKUSHA.Text = "月";
                    courseNameEntity.MONDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                case "2":
                    this.form.DAY_NAME_FUKUSHA.Text = "火";
                    courseNameEntity.TUESDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                case "3":
                    this.form.DAY_NAME_FUKUSHA.Text = "水";
                    courseNameEntity.WEDNESDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                case "4":
                    this.form.DAY_NAME_FUKUSHA.Text = "木";
                    courseNameEntity.THURSDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                case "5":
                    this.form.DAY_NAME_FUKUSHA.Text = "金";
                    courseNameEntity.FRIDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                case "6":
                    this.form.DAY_NAME_FUKUSHA.Text = "土";
                    courseNameEntity.SATURDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                case "7":
                    this.form.DAY_NAME_FUKUSHA.Text = "日";
                    courseNameEntity.SUNDAY = true;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
                default:
                    this.form.DAY_NAME_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    break;
            }
            // コース名称ポップアップ設定
            DataTable dt = dao.GetCourseNameData(courseNameEntity);

            // TableNameを設定すれば、ポップアップのタイトルになる
            dt.TableName = "コース名称検索";
            this.form.COURSE_NAME_CD_FUKUSHA.PopupWindowId = WINDOW_ID.M_COURSE_NAME;
            this.form.COURSE_NAME_CD_FUKUSHA.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.form.COURSE_NAME_CD_FUKUSHA.PopupDataHeaderTitle = new string[] { "コース名称CD", "コース名称" };
            this.form.COURSE_NAME_CD_FUKUSHA.PopupDataSource = dt;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// カーソル選択時、複写曜日がBLANKの場合、メッセージを表示
        /// </summary>
        public void CheckFukushaDay()
        {
            LogUtility.DebugMethodStart();

            if (string.IsNullOrEmpty(this.form.DAY_CD_FUKUSHA.Text))
            {
                this.msgLogic.MessageBoxShow("E282");
                this.form.COURSE_NAME_CD_FUKUSHA.BackColor = System.Drawing.SystemColors.Window;
                this.form.DAY_CD_FUKUSHA.Focus();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 曜日、コースCDの組み合わせがコース名入力に存在しない場合、メッセージを表示
        /// </summary>
        public void CheckCourseCd()
        {
            LogUtility.DebugMethodStart();

            if (!string.IsNullOrEmpty(this.form.COURSE_NAME_CD_FUKUSHA.Text))
            {
                M_COURSE_NAME data = new M_COURSE_NAME();
                data.COURSE_NAME_CD = this.form.COURSE_NAME_CD_FUKUSHA.Text;
                data.ISNOT_NEED_DELETE_FLG = true;
                switch (this.form.DAY_CD_FUKUSHA.Text)
                {
                    case "1":
                        data.MONDAY = true;
                        break;
                    case "2":
                        data.TUESDAY = true;
                        break;
                    case "3":
                        data.WEDNESDAY = true;
                        break;
                    case "4":
                        data.THURSDAY = true;
                        break;
                    case "5":
                        data.FRIDAY = true;
                        break;
                    case "6":
                        data.SATURDAY = true;
                        break;
                    case "7":
                        data.SUNDAY = true;
                        break;
                    default:
                        break;
                }
                data = courseNameDao.GetAllValidData(data).FirstOrDefault();
                if (data == null)
                {
                    this.msgLogic.MessageBoxShow("E020", "コース名称");
                    this.form.COURSE_NAME_CD_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
                    this.form.COURSE_NAME_CD_FUKUSHA.Focus();
                }
                else
                {
                    this.form.COURSE_NAME_FUKUSHA.Text = data.COURSE_NAME_RYAKU;
                }
            }
            else
            {
                this.form.COURSE_NAME_FUKUSHA.Text = string.Empty;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 複写押下
        /// </summary>
        public void Fukusha()
        {
            LogUtility.DebugMethodStart();

            // 入力チェック
            if (string.IsNullOrEmpty(this.form.DAY_CD_FUKUSHA.Text)
                || string.IsNullOrEmpty(this.form.COURSE_NAME_CD_FUKUSHA.Text))
            {
                this.msgLogic.MessageBoxShow("E283");
                return;
            }

            DataGridViewCell cell = this.form.customDataGridView1.CurrentCell;
            if (cell != null)
            {
                string dayCdF = this.form.DAY_CD_FUKUSHA.Text;
                string courseNameCdF = this.form.COURSE_NAME_CD_FUKUSHA.Text;
                string courseNameF = this.form.COURSE_NAME_FUKUSHA.Text;
                int index = cell.RowIndex;
                string dayCd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_DAY_CD"].Value);
                string courseNameCd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_COURSE_NAME_CD"].Value);
                string courseName = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_COURSE_NAME"].Value);
                string kyotenCd = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_KYOTEN_CD"].Value);
                string kyotenName = Convert.ToString(this.form.customDataGridView1.Rows[index].Cells["HIDDEN_KYOTEN_NAME"].Value);

                // 修正モードの権限チェック
                if (Manager.CheckAuthority("M232", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FormManager.OpenFormWithAuth("M232", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, dayCd, courseNameCd, courseName, kyotenCd, kyotenName, dayCdF, courseNameCdF, courseNameF);
                }
                // 参照モードの権限チェック
                else if (Manager.CheckAuthority("M232", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    FormManager.OpenFormWithAuth("M232", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, dayCd, courseNameCd, courseName, kyotenCd, kyotenName, dayCdF, courseNameCdF, courseNameF);
                }
                else
                {
                    // 修正モードの権限なしのアラームを上げる
                    msgLogic.MessageBoxShow("E158", "修正");
                }
            }
            else
            {
                //アラートを表示し、画面遷移しない
                this.msgLogic.MessageBoxShow("E051", "対象データ");
            }
            LogUtility.DebugMethodEnd();
        }

        #region mapbox連携

        #region 明細ヘッダーにチェックボックスを追加

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        private void HeaderCheckBoxSupport()
        {

            LogUtility.DebugMethodStart();

            if (!this.form.customDataGridView1.Columns.Contains(ConstClass.DATA_TAISHO))
            {
                {
                    DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                    newColumn.Name = ConstClass.DATA_TAISHO;
                    newColumn.HeaderText = "地図";
                    newColumn.DataPropertyName = "TAISHO";
                    newColumn.Width = 70;
                    DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
                    newheader.Value = "地図   ";
                    newColumn.HeaderCell = newheader;
                    newColumn.Resizable = DataGridViewTriState.False;
                    newColumn.ReadOnly = false;

                    if (this.form.customDataGridView1.Columns.Count > 0)
                    {
                        this.form.customDataGridView1.Columns.Insert(0, newColumn);
                    }
                    else
                    {
                        this.form.customDataGridView1.Columns.Add(newColumn);
                    }
                    this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 明細ヘッダーのチェックボックス解除

        /// <summary>
        /// 明細ヘッダーチェックボックスを解除する
        /// </summary>
        private void HeaderCheckBoxFalse()
        {
            if (this.form.customDataGridView1.Columns.Contains(ConstClass.DATA_TAISHO))
            {
                DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns[ConstClass.DATA_TAISHO].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header._checked = false;
                }
            }
        }

        #endregion

        #region 地図表示件数チェック

        /// <summary>
        /// 一覧で選択がチェックされているか確認する。
        /// また、チェックされた行で地図表示できるデータがない場合もアラートを出す。
        /// </summary>
        /// <returns></returns>
        internal bool CheckForCheckBox()
        {
            bool ret = false;

            // チェックが1件もない場合のチェック
            foreach (DataGridViewRow row in this.form.customDataGridView1.Rows)
            {
                // 選択のチェックボックスの値を取得する。
                if (row.Cells[ConstClass.DATA_TAISHO].Value != null)
                {
                    ret = bool.Parse(Convert.ToString(row.Cells[ConstClass.DATA_TAISHO].Value));
                    if (ret)
                    {
                        break;
                    }
                }
            }
            if (!ret)
            {
                this.msgLogic.MessageBoxShowError("地図表示対象の明細がありません。");
                return ret;
            }

            return ret;
        }

        #endregion

        #region 明細チェック切替処理

        /// <summary>
        /// クリック処理
        /// 同一のコースにもチェックをつける
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0)
            {
                return;
            }

            // チェックした行の曜日とコース名CDを取得
            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            string dayCd = Convert.ToString(row.Cells["HIDDEN_DAY_CD"].Value);
            string courseNameCd = Convert.ToString(row.Cells["HIDDEN_COURSE_NAME_CD"].Value);

            //スペースで、OFFの場合は特殊処理
            if (this.SpaceChk && !this.SpaceON)
            {
                // チェックON⇒OFFにする時の処理
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    // 選択行は変更しない
                    if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                    // チェックした行と同一コースにチェックをつける
                    if (dgvRow.Cells["HIDDEN_DAY_CD"].Value.ToString() == dayCd &&
                        dgvRow.Cells["HIDDEN_COURSE_NAME_CD"].Value.ToString() == courseNameCd)
                    {
                        dgvRow.Cells[ConstClass.DATA_TAISHO].Value = false;
                    }
                }
                this.form.customDataGridView1.RefreshEdit();
                this.form.customDataGridView1.Refresh();
                return;
            }
            this.SpaceON = false;


            if (this.form.customDataGridView1.CurrentCell.Value == null)
            {
                if (Convert.ToString(row.Cells["HIDDEN_LOCATION"].Value) != "1")
                {
                    this.msgLogic.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                    if (!this.SpaceChk)
                    {
                        this.form.customDataGridView1[0, e.RowIndex].Value = true;
                    }
                    this.SpaceChk = false;
                    return;
                }
                if (this.SpaceChk)
                {
                    if (this.form.customDataGridView1[0, e.RowIndex].Value == null)
                    {
                        this.form.customDataGridView1[0, e.RowIndex].Value = true;
                    }
                    else
                    {
                        this.form.customDataGridView1[0, e.RowIndex].Value = !(bool)this.form.customDataGridView1[0, e.RowIndex].Value;
                    }
                    this.SpaceChk = false;
                }

                // チェックNULL(OFF)⇒ONにする時の処理
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    // 選択行は変更しない
                    if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                    // チェックした行と同一コースにチェックをつける
                    if (dgvRow.Cells["HIDDEN_DAY_CD"].Value.ToString() == dayCd &&
                        dgvRow.Cells["HIDDEN_COURSE_NAME_CD"].Value.ToString() == courseNameCd)
                    {
                        dgvRow.Cells[ConstClass.DATA_TAISHO].Value = true;
                    }
                }
            }
            else if (this.form.customDataGridView1.CurrentCell.Value.Equals(false))
            {
                if (Convert.ToString(row.Cells["HIDDEN_LOCATION"].Value) != "1")
                {
                    this.msgLogic.MessageBoxShowError("緯度経度が登録されているデータがないため、地図を表示できません。");
                    if (!this.SpaceChk)
                    {
                        this.form.customDataGridView1[0, e.RowIndex].Value = true;
                    }
                    this.SpaceChk = false;
                    return;
                }
                if (this.SpaceChk)
                {
                    if (this.form.customDataGridView1[0, e.RowIndex].Value == null)
                    {
                        this.form.customDataGridView1[0, e.RowIndex].Value = true;
                    }
                    else
                    {
                        this.form.customDataGridView1[0, e.RowIndex].Value = !(bool)this.form.customDataGridView1[0, e.RowIndex].Value;
                    }
                    this.SpaceChk = false;
                }

                // チェックOFF⇒ONにする時の処理
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    // 選択行は変更しない
                    if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                    // チェックした行と同一コースにチェックをつける
                    if (dgvRow.Cells["HIDDEN_DAY_CD"].Value.ToString() == dayCd &&
                        dgvRow.Cells["HIDDEN_COURSE_NAME_CD"].Value.ToString() == courseNameCd)
                    {
                        dgvRow.Cells[ConstClass.DATA_TAISHO].Value = true;
                    }
                }
            }
            else
            {
                // チェックON⇒OFFにする時の処理
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    // 選択行は変更しない
                    if (dgvRow.Index.Equals(e.RowIndex)) { continue; }

                    // チェックした行と同一コースにチェックをつける
                    if (dgvRow.Cells["HIDDEN_DAY_CD"].Value.ToString() == dayCd &&
                        dgvRow.Cells["HIDDEN_COURSE_NAME_CD"].Value.ToString() == courseNameCd)
                    {
                        dgvRow.Cells[ConstClass.DATA_TAISHO].Value = false;
                    }
                }
            }

            this.form.customDataGridView1.RefreshEdit();
            this.form.customDataGridView1.Refresh();
        }

        #endregion

        #region 明細チェックボックスのスペースキー押下時の制御

        /// <summary>
        /// [地図]で、スペースキーでチェック処理が走るように下準備
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                DataGridViewCell curCell = this.form.customDataGridView1.CurrentCell;

                if (curCell.RowIndex < 0 || curCell.ColumnIndex != 0)
                {
                    return;
                }

                this.SpaceChk = true;
                this.SpaceON = false;
                //[地図]OFFにする場合は、何もしない。
                //[地図]ONにする場合は、一度チェックボックスを反転させておく(チェック処理中に画面上ONになってしまうので)
                if (this.form.customDataGridView1[0, curCell.RowIndex].Value == null)
                {
                    this.SpaceON = true;
                    this.form.customDataGridView1[0, curCell.RowIndex].Value = true;
                }
                else
                {
                    if (!(bool)this.form.customDataGridView1[0, curCell.RowIndex].Value)
                    {
                        this.SpaceON = true;
                        this.form.customDataGridView1[0, curCell.RowIndex].Value = !(bool)this.form.customDataGridView1[0, curCell.RowIndex].Value;
                    }
                }
                this.form.customDataGridView1.Refresh();

            }
        }

        #endregion

        #region 連携処理

        /// <summary>
        /// mapbox表示用Dto作成
        /// </summary>
        /// <returns></returns>
        internal List<mapDtoList> createMapboxDto()
        {
            try
            {
                int layerId = 0;

                List<SummaryKeyCode> summaryKeyCodeList = new List<SummaryKeyCode>();

                // 出力対象となるコース情報のPK情報を取得
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    // チェックなしデータを排除する
                    if (this.form.customDataGridView1.Rows[i].Cells[ConstClass.DATA_TAISHO].Value == null) continue;
                    if ((bool)this.form.customDataGridView1.Rows[i].Cells[ConstClass.DATA_TAISHO].Value == false) continue;

                    SummaryKeyCode summaryKeyCode = new SummaryKeyCode();
                    summaryKeyCode.DAY_CD = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells["HIDDEN_DAY_CD"].Value);
                    summaryKeyCode.COURSE_NAME_CD = Convert.ToString(this.form.customDataGridView1.Rows[i].Cells["HIDDEN_COURSE_NAME_CD"].Value);
                    summaryKeyCodeList.Add(summaryKeyCode);
                }

                List<mapDtoList> dtoLists = new List<mapDtoList>();

                // LINQでグループ化する
                var roopList = summaryKeyCodeList.GroupBy(a => new { DAY_CD = a.DAY_CD, COURSE_NAME_CD = a.COURSE_NAME_CD });
                foreach (var group in roopList)
                {
                    layerId++;

                    // レイヤー追加
                    mapDtoList dtoList = new mapDtoList();
                    dtoList.layerId = layerId;

                    List<mapDto> dtos = new List<mapDto>();

                    // 地図出力に必要な情報を収集
                    #region 明細1件のコースの内容を取得する

                    string sql = string.Empty;
                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT ");
                    sb.AppendFormat(" DET.ROW_NO AS {0} ", ConstClass.ROW_NO);
                    sb.AppendFormat(",DET.ROUND_NO AS {0} ", ConstClass.ROUND_NO);
                    sb.AppendFormat(",CON.COURSE_NAME AS {0} ", ConstClass.COURSE_NAME);
                    sb.AppendFormat(",DET.GYOUSHA_CD AS {0} ", ConstClass.GYOUSHA_CD);
                    sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstClass.GYOUSHA_NAME_RYAKU);
                    sb.AppendFormat(",DET.GENBA_CD AS {0} ", ConstClass.GENBA_CD);
                    sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstClass.GENBA_NAME_RYAKU);
                    sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstClass.TODOUFUKEN_NAME);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", ConstClass.GENBA_ADDRESS1);
                    sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", ConstClass.GENBA_ADDRESS2);
                    sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstClass.GENBA_LATITUDE);
                    sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstClass.GENBA_LONGITUDE);
                    sb.AppendFormat(",GEN.GENBA_POST AS {0} ", ConstClass.GENBA_POST);
                    sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", ConstClass.GENBA_TEL);
                    sb.AppendFormat(",GEN.BIKOU1 AS {0} ", ConstClass.BIKOU1);
                    sb.AppendFormat(",GEN.BIKOU2 AS {0} ", ConstClass.BIKOU2);
                    sb.AppendFormat(",DET.KIBOU_TIME ");
                    sb.AppendFormat(",DET.DAY_CD ");
                    sb.AppendFormat(",DET.COURSE_NAME_CD ");
                    sb.AppendFormat(",DET.REC_ID ");
                    sb.AppendFormat(",ENT.SHUPPATSU_GYOUSHA_CD ");
                    sb.AppendFormat(",ENT.SHUPPATSU_GENBA_CD ");
                    sb.AppendFormat(" FROM M_COURSE AS ENT ");
                    sb.AppendFormat(" LEFT JOIN M_COURSE_NAME CON ON ENT.COURSE_NAME_CD = CON.COURSE_NAME_CD ");
                    sb.AppendFormat(" LEFT JOIN M_COURSE_DETAIL DET ON ENT.DAY_CD = DET.DAY_CD AND ENT.COURSE_NAME_CD = DET.COURSE_NAME_CD ");
                    sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON DET.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_GENBA GEN ON DET.GYOUSHA_CD = GEN.GYOUSHA_CD AND DET.GENBA_CD = GEN.GENBA_CD ");
                    sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                    sb.AppendFormat(" WHERE ENT.DELETE_FLG = 0 ");
                    sb.AppendFormat(" AND ENT.DAY_CD = {0}", group.Key.DAY_CD);
                    sb.AppendFormat(" AND ENT.COURSE_NAME_CD = '{0}'", group.Key.COURSE_NAME_CD);
                    sb.AppendFormat(" ORDER BY DET.ROW_NO ");

                    DataTable dt = this.dao.GetDataBySql(sb.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        // 出発業者のみ、または出発業者と出発現場が設定されている場合、コースの先頭とする。
                        string gyoushaCd = dt.Rows[0]["SHUPPATSU_GYOUSHA_CD"].ToString();
                        string genbaCd = dt.Rows[0]["SHUPPATSU_GENBA_CD"].ToString();

                        if (!string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                        {
                            sql = string.Empty;
                            sb = new StringBuilder();

                            sb.AppendFormat(" SELECT ");
                            sb.AppendFormat(" GYO.GYOUSHA_CD AS {0} ", ConstClass.GYOUSHA_CD);
                            sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstClass.GYOUSHA_NAME_RYAKU);
                            sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstClass.TODOUFUKEN_NAME);
                            sb.AppendFormat(",GYO.GYOUSHA_ADDRESS1 AS {0} ", ConstClass.GYOUSHA_ADDRESS1);
                            sb.AppendFormat(",GYO.GYOUSHA_ADDRESS2 AS {0} ", ConstClass.GYOUSHA_ADDRESS2);
                            sb.AppendFormat(",GYO.GYOUSHA_LATITUDE AS {0} ", ConstClass.GYOUSHA_LATITUDE);
                            sb.AppendFormat(",GYO.GYOUSHA_LONGITUDE AS {0} ", ConstClass.GYOUSHA_LONGITUDE);
                            sb.AppendFormat(",GYO.GYOUSHA_POST AS {0} ", ConstClass.GYOUSHA_POST);
                            sb.AppendFormat(",GYO.GYOUSHA_TEL AS {0} ", ConstClass.GYOUSHA_TEL);
                            sb.AppendFormat(",GYO.BIKOU1 AS {0} ", ConstClass.BIKOU1);
                            sb.AppendFormat(",GYO.BIKOU2 AS {0} ", ConstClass.BIKOU2);
                            sb.AppendFormat(" FROM M_GYOUSHA AS GYO ");
                            sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GYO.GYOUSHA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                            sb.AppendFormat(" WHERE GYO.DELETE_FLG = 0 ");
                            sb.AppendFormat(" AND GYO.GYOUSHA_CD = '{0}'", gyoushaCd);

                            DataTable dtShuppatsu = this.dao.GetDataBySql(sb.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                mapDto dto = new mapDto();
                                dto.id = layerId;
                                dto.layerNo = layerId;
                                dto.courseName = Convert.ToString(dt.Rows[0][ConstClass.COURSE_NAME]);
                                dto.dayName = mapLogic.SetDayNameByCd(group.Key.DAY_CD);
                                dto.teikiHaishaNo = string.Empty;
                                dto.torihikisakiCd = string.Empty;
                                dto.torihikisakiName = string.Empty;
                                dto.gyoushaCd = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_CD]);
                                dto.gyoushaName = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_NAME_RYAKU]);
                                dto.genbaCd = string.Empty;
                                dto.genbaName = string.Empty;
                                dto.post = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_POST]);
                                dto.address = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.TODOUFUKEN_NAME])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_ADDRESS1])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_ADDRESS2]);
                                dto.tel = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_TEL]);
                                dto.bikou1 = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.BIKOU1]);
                                dto.bikou2 = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.BIKOU2]);
                                dto.latitude = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_LATITUDE]);
                                dto.longitude = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_LONGITUDE]);
                                dto.rowNo = 0;
                                dto.roundNo = 0;
                                dto.genbaChaku = string.Empty;
                                dto.hinmei = string.Empty;
                                dto.shuppatsuFlag = true;
                                dtos.Add(dto);
                            }
                        }
                        else if (!string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                        {
                            sql = string.Empty;
                            sb = new StringBuilder();

                            sb.AppendFormat(" SELECT ");
                            sb.AppendFormat(" GEN.GYOUSHA_CD AS {0} ", ConstClass.GYOUSHA_CD);
                            sb.AppendFormat(",GYO.GYOUSHA_NAME_RYAKU AS {0} ", ConstClass.GYOUSHA_NAME_RYAKU);
                            sb.AppendFormat(",GEN.GENBA_CD AS {0} ", ConstClass.GENBA_CD);
                            sb.AppendFormat(",GEN.GENBA_NAME_RYAKU AS {0} ", ConstClass.GENBA_NAME_RYAKU);
                            sb.AppendFormat(",TDF.TODOUFUKEN_NAME AS {0} ", ConstClass.TODOUFUKEN_NAME);
                            sb.AppendFormat(",GEN.GENBA_ADDRESS1 AS {0} ", ConstClass.GENBA_ADDRESS1);
                            sb.AppendFormat(",GEN.GENBA_ADDRESS2 AS {0} ", ConstClass.GENBA_ADDRESS2);
                            sb.AppendFormat(",GEN.GENBA_LATITUDE AS {0} ", ConstClass.GENBA_LATITUDE);
                            sb.AppendFormat(",GEN.GENBA_LONGITUDE AS {0} ", ConstClass.GENBA_LONGITUDE);
                            sb.AppendFormat(",GEN.GENBA_POST AS {0} ", ConstClass.GENBA_POST);
                            sb.AppendFormat(",GEN.GENBA_TEL AS {0} ", ConstClass.GENBA_TEL);
                            sb.AppendFormat(",GEN.BIKOU1 AS {0} ", ConstClass.BIKOU1);
                            sb.AppendFormat(",GEN.BIKOU2 AS {0} ", ConstClass.BIKOU2);
                            sb.AppendFormat(" FROM M_GENBA AS GEN ");
                            sb.AppendFormat(" LEFT JOIN M_GYOUSHA GYO ON GEN.GYOUSHA_CD = GYO.GYOUSHA_CD ");
                            sb.AppendFormat(" LEFT JOIN M_TODOUFUKEN TDF ON GEN.GENBA_TODOUFUKEN_CD = TDF.TODOUFUKEN_CD ");
                            sb.AppendFormat(" WHERE GEN.DELETE_FLG = 0 ");
                            sb.AppendFormat(" AND GEN.GYOUSHA_CD = '{0}'", gyoushaCd);
                            sb.AppendFormat(" AND GEN.GENBA_CD = '{0}'", genbaCd);

                            DataTable dtShuppatsu = this.dao.GetDataBySql(sb.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                                mapDto dto = new mapDto();
                                dto.id = layerId;
                                dto.layerNo = layerId;
                                dto.courseName = Convert.ToString(dt.Rows[0][ConstClass.COURSE_NAME]);
                                dto.dayName = mapLogic.SetDayNameByCd(group.Key.DAY_CD);
                                dto.teikiHaishaNo = string.Empty;
                                dto.torihikisakiCd = string.Empty;
                                dto.torihikisakiName = string.Empty;
                                dto.gyoushaCd = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_CD]);
                                dto.gyoushaName = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GYOUSHA_NAME_RYAKU]);
                                dto.genbaCd = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_CD]);
                                dto.genbaName = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_NAME_RYAKU]);
                                dto.post = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_POST]);
                                dto.address = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.TODOUFUKEN_NAME])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_ADDRESS1])
                                            + Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_ADDRESS2]);
                                dto.tel = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_TEL]);
                                dto.bikou1 = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.BIKOU1]);
                                dto.bikou2 = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.BIKOU2]);
                                dto.latitude = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_LATITUDE]);
                                dto.longitude = Convert.ToString(dtShuppatsu.Rows[0][ConstClass.GENBA_LONGITUDE]);
                                dto.rowNo = 0;
                                dto.roundNo = 0;
                                dto.genbaChaku = string.Empty;
                                dto.hinmei = string.Empty;
                                dto.shuppatsuFlag = true;
                                dtos.Add(dto);
                            }
                        }

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[j]["COURSE_NAME_CD"])))
                            {
                                continue;
                            }
                            MapboxGLJSLogic mapLogic = new MapboxGLJSLogic();
                            mapDto dto = new mapDto();
                            dto.id = layerId;
                            dto.layerNo = layerId;
                            dto.courseName = Convert.ToString(dt.Rows[j][ConstClass.COURSE_NAME]);
                            dto.dayName = mapLogic.SetDayNameByCd(group.Key.DAY_CD);
                            dto.teikiHaishaNo = string.Empty;
                            dto.torihikisakiCd = string.Empty;
                            dto.torihikisakiName = string.Empty;
                            dto.gyoushaCd = Convert.ToString(dt.Rows[j][ConstClass.GYOUSHA_CD]);
                            dto.gyoushaName = Convert.ToString(dt.Rows[j][ConstClass.GYOUSHA_NAME_RYAKU]);
                            dto.genbaCd = Convert.ToString(dt.Rows[j][ConstClass.GENBA_CD]);
                            dto.genbaName = Convert.ToString(dt.Rows[j][ConstClass.GENBA_NAME_RYAKU]);
                            dto.post = Convert.ToString(dt.Rows[j][ConstClass.GENBA_POST]);
                            dto.address = Convert.ToString(dt.Rows[j][ConstClass.TODOUFUKEN_NAME]) + Convert.ToString(dt.Rows[j][ConstClass.GENBA_ADDRESS1]) + Convert.ToString(dt.Rows[j][ConstClass.GENBA_ADDRESS2]);
                            dto.tel = Convert.ToString(dt.Rows[j][ConstClass.GENBA_TEL]);
                            dto.bikou1 = Convert.ToString(dt.Rows[j][ConstClass.BIKOU1]);
                            dto.bikou2 = Convert.ToString(dt.Rows[j][ConstClass.BIKOU2]);
                            dto.rowNo = Convert.ToInt32(dt.Rows[j][ConstClass.ROW_NO]);
                            dto.roundNo = Convert.ToInt32(dt.Rows[j][ConstClass.ROUND_NO]);
                            string time = string.Empty;
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[j]["KIBOU_TIME"])))
                                time = Convert.ToDateTime(Convert.ToString(dt.Rows[j]["KIBOU_TIME"])).ToString("HH:mm");
                            dto.genbaChaku = time;

                            sql = " SELECT H.HINMEI_NAME_RYAKU FROM M_COURSE_DETAIL_ITEMS DI "
                                + " LEFT JOIN M_HINMEI H ON DI.HINMEI_CD = H.HINMEI_CD "
                                + " WHERE DAY_CD = " + group.Key.DAY_CD
                                + "   AND COURSE_NAME_CD = '" + group.Key.COURSE_NAME_CD + "'"
                                + "   AND REC_ID = " + Convert.ToInt32(dt.Rows[j]["REC_ID"]);
                            DataTable hinmeiDt = this.sysInfoDao.GetDateForStringSql(sql);
                            string hinmei = string.Empty;
                            foreach (DataRow dr in hinmeiDt.Rows)
                            {
                                if (string.IsNullOrEmpty(hinmei))
                                {
                                    hinmei += dr["HINMEI_NAME_RYAKU"];
                                }
                                else
                                {
                                    hinmei += "/ " + dr["HINMEI_NAME_RYAKU"];
                                }
                            }
                            dto.hinmei = hinmei;
                            dto.latitude = Convert.ToString(dt.Rows[j][ConstClass.GENBA_LATITUDE]);
                            dto.longitude = Convert.ToString(dt.Rows[j][ConstClass.GENBA_LONGITUDE]);
                            dto.shuppatsuFlag = false;
                            dtos.Add(dto);
                        }
                        // 1コース終わったらリストにセット
                        dtoList.dtos = dtos;
                    }

                    #endregion

                    if (dtoList.dtos != null)
                    {
                        if (dtoList.dtos.Count != 0)
                        {
                            dtoLists.Add(dtoList);
                        }
                    }
                }
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
