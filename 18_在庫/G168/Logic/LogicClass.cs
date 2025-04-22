using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Stock.ZaikoTyouseiIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Stock.ZaikoTyouseiIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 在庫量フォーマット(システム参照しない)
        /// </summary>
        /// <remarks>DBはDecimal(10,3)のため、小数部最大3桁を設定する。</remarks>
        internal readonly string ZAIKO_RYOU_FORMAT = "#,##0.###";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string SELECT =
            "  T_ZAIKO_TYOUSEI_ENTRY.TYOUSEI_NUMBER AS '調整番号'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.TYOUSEI_DATE AS '入力日付'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.GYOUSHA_CD AS '業者CD'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.GYOUSHA_NAME AS '業者名'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.GENBA_CD AS '現場CD'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.GENBA_NAME AS '現場名'" +
            ", T_ZAIKO_TYOUSEI_DETAIL.ZAIKO_HINMEI_CD AS '在庫品名CD'" +
            ", T_ZAIKO_TYOUSEI_DETAIL.ZAIKO_HINMEI_NAME AS '在庫品名'" +
            ", T_ZAIKO_TYOUSEI_DETAIL.TYOUSEI_BEFORE_ZAIKO_RYOU AS '調整前在庫量'" +
            ", T_ZAIKO_TYOUSEI_DETAIL.TYOUSEI_RYOU AS '調整量'" +
            ", T_ZAIKO_TYOUSEI_DETAIL.TYOUSEI_AFTER_ZAIKO_RYOU AS '調整後在庫量'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.CREATE_USER AS '作成者'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.CREATE_DATE AS '作成日時'" +
            ", T_ZAIKO_TYOUSEI_ENTRY.CREATE_PC AS '作成PC'";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private UIHeader headForm;
        private BusinessBaseForm footer;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        private MessageBoxShowLogic MsgBox;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

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

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass ichiranDao;

        /// <summary>
        /// システム設定Dao
        /// </summary>
        internal IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private IM_ZAIKO_HINMEIDao zaikoHinmeiDao;

        private Control[] allControl;

        /// <summary>
        /// システム設定エンティティを取得・設定します
        /// </summary>
        internal M_SYS_INFO sysInfo { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();

            this.ichiranDao = DaoInitUtility.GetComponent<DAOClass>();
            this.zaikoHinmeiDao = DaoInitUtility.GetComponent<IM_ZAIKO_HINMEIDao>();

            this.sysInfo = this.sysInfoDao.GetAllData().FirstOrDefault();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.headForm.lb_title.Text = "在庫調整一覧";
                this.allControl = this.form.allControl;
                //this.form.customSortHeader1.Location = new System.Drawing.Point(3, 101);
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.Location = new System.Drawing.Point(3, 147);
                this.form.customDataGridView1.Size = new System.Drawing.Size(997, 278);
                this.form.customDataGridView1.TabStop = false;

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.HIDUKE_FROM.Value = parentForm.sysDate;
                this.form.HIDUKE_TO.Value = parentForm.sysDate;
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
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.headForm = targetHeader;

            // フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();

            // Functionボタンのイベント生成
            footer.bt_func2.Click += new System.EventHandler(this.bt_func2_Click);          // 新規
            footer.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);          // 参照
            footer.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);          // CSV出力
            footer.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);          // 検索条件クリア
            footer.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);          // 検索
            footer.bt_func10.Click += new EventHandler(this.bt_func10_Click);               // 並替移動
            footer.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);        //フィルタ
            footer.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);        // 閉じる
            // 明細画面上でダブルクリック時のイベント生成
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionボタン押下処理
        /// <summary>
        /// F2 新規ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            FormManager.OpenFormWithAuth("G167", WINDOW_TYPE.NEW_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F3 参照ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
            if (row == null)
            {
                return;
            }

            string tyouseiNumber = Convert.ToString(row.Cells["調整番号"].Value);
            FormManager.OpenFormWithAuth("G167", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, tyouseiNumber);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            // 一覧にデータ行がない場合
            if (this.form.customDataGridView1.RowCount == 0)
            {
                // アラートを表示し、CSV出力処理はしない
                msgLogic.MessageBoxShow("E044");
            }
            else
            {
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport exp = new CSVExport();
                    exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, "在庫調整一覧", this.form);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // タイトル
            this.headForm.lb_title.Text = "在庫調整一覧";

            this.form.GYOUSHA_CD.Text = "";
            this.form.GYOUSHA_NAME.Text = "";
            this.form.GENBA_CD.Text = "";
            this.form.GENBA_NAME.Text = "";
            this.form.ZAIKO_HINMEI_CD.Text = "";
            this.form.ZAIKO_HINMEI_NAME.Text = "";
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.HIDUKE_FROM.Value = parentForm.sysDate;
            this.form.HIDUKE_TO.Value = parentForm.sysDate;
            this.form.beforeGyoushaCd = "";
            this.form.beforeGenbaCd = "";
            this.form.beforeZaikoHinmeiCd = "";

            // 検索条件
            this.form.searchString.Clear();

            // 並び順をクリア
            this.form.customSortHeader1.ClearCustomSortSetting();

            // フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Search();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F10 並替移動ボタン
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.customDataGridView1.Rows.Count < 1)
            {
                return;
            }
            this.form.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            LogUtility.DebugMethodEnd();

        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region HeaderForm

        /// <summary>
        /// HeaderForm設定
        /// </summary>
        /// <returns>hs</returns>
        public void SetHeader(UIHeader hs)
        {
            LogUtility.DebugMethodStart(hs);
            this.headForm = hs;
            LogUtility.DebugMethodEnd(hs);
        }

        #endregion

        #region 検索SQL作成
        /// <summary>
        /// 検索SQL作成
        /// </summary>
        public void MakeSearchCondition()
        {
            LogUtility.DebugMethodStart();

            // SQL文格納StringBuilder
            var sql = new StringBuilder();

            sql.Append(" SELECT DISTINCT ");

            sql.Append(this.selectQuery);

            #region FROM句

            // FROM句作成
            sql.Append(" FROM ");
            sql.Append(" T_ZAIKO_TYOUSEI_ENTRY ");
            sql.Append(" LEFT JOIN T_ZAIKO_TYOUSEI_DETAIL ON T_ZAIKO_TYOUSEI_ENTRY.SYSTEM_ID = T_ZAIKO_TYOUSEI_DETAIL.SYSTEM_ID AND T_ZAIKO_TYOUSEI_ENTRY.SEQ = T_ZAIKO_TYOUSEI_DETAIL.SEQ ");

            #endregion

            #region WHERE句

            sql.Append(" WHERE ");
            sql.Append(" T_ZAIKO_TYOUSEI_ENTRY.DELETE_FLG = 0 ");

            // 日付
            if (!string.IsNullOrEmpty(this.form.HIDUKE_FROM.Text))
            {
                sql.Append(" AND T_ZAIKO_TYOUSEI_ENTRY.TYOUSEI_DATE >= '" + DateTime.Parse(this.form.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00' ");
            }
            if (!string.IsNullOrEmpty(this.form.HIDUKE_TO.Text))
            {
                sql.Append(" AND T_ZAIKO_TYOUSEI_ENTRY.TYOUSEI_DATE <= '" + DateTime.Parse(this.form.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59' ");
            }
            // 業者
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                sql.Append(" AND T_ZAIKO_TYOUSEI_ENTRY.GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "' ");
            }
            // 現場
            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                sql.Append(" AND T_ZAIKO_TYOUSEI_ENTRY.GENBA_CD = '" + this.form.GENBA_CD.Text + "' ");
            }
            // 在庫品名
            if (!string.IsNullOrEmpty(this.form.ZAIKO_HINMEI_CD.Text))
            {
                sql.Append(" AND T_ZAIKO_TYOUSEI_DETAIL.ZAIKO_HINMEI_CD = '" + this.form.ZAIKO_HINMEI_CD.Text + "' ");
            }

            #region ORDERBY句

            sql.Append(" ORDER BY '調整番号' ASC");

            #endregion

            #endregion

            this.createSql = sql.ToString();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            if (this.CheckDate())
            {
                return 0;
            }

            // SQLの作成
            this.form.SetLogicSelect();
            this.MakeSearchCondition();

            // 検索実行
            this.SearchResult = new DataTable();
            this.SearchResult = this.ichiranDao.GetDateForStringSql(this.createSql);
            
            this.form.SetSearch();

            if (this.SearchResult.Rows.Count == 0)
            {
                msgLogic.MessageBoxShow("C001");
                // 在庫品名CDにフォーカス移動
                this.form.GYOUSHA_CD.Focus();
            }

            LogUtility.DebugMethodEnd();

            return 0;
        }

        #endregion

        #region 明細データダブルクリックイベント
        /// <summary>
        /// 明細データダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                DataGridViewRow row = this.form.customDataGridView1.CurrentRow;
                if (row == null)
                {
                    return;
                }

                string idouNumber = Convert.ToString(row.Cells["調整番号"].Value);
                FormManager.OpenFormWithAuth("G167", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, idouNumber);
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("customDataGridView1_MouseDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 業者CDValidated
        /// <summary>
        /// 業者CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GYOUSHA_CD_Validated()
        {
            var messageLogic = new MessageBoxShowLogic();
            try
            {
                if (this.form.beforeGyoushaCd != this.form.GYOUSHA_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                    {
                        this.form.GYOUSHA_NAME.Text = "";
                        this.form.GENBA_CD.Text = "";
                        this.form.GENBA_NAME.Text = "";
                        this.form.beforeGyoushaCd = "";
                        this.form.beforeGenbaCd = "";
                        return;
                    }

                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    gyousha.JISHA_KBN = true;
                    gyousha.ISNOT_NEED_DELETE_FLG = true;
                    gyousha = this.gyoushaDao.GetAllValidData(gyousha).FirstOrDefault();

                    if (gyousha == null)
                    {
                        this.form.isError = true;
                        this.form.GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.GYOUSHA_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "業者");
                        this.form.GYOUSHA_CD.Focus();
                        return;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }

                    this.form.GENBA_CD.Text = "";
                    this.form.GENBA_NAME.Text = "";
                    this.form.beforeGyoushaCd = this.form.GYOUSHA_CD.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GYOUSHA_CD_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region 現場CDValidated
        /// <summary>
        /// 現場CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void GENBA_CD_Validated()
        {
            var messageLogic = new MessageBoxShowLogic();
            try
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    messageLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    return;
                }

                if (this.form.beforeGenbaCd != this.form.GENBA_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                    {
                        this.form.GENBA_NAME.Text = "";
                        this.form.beforeGenbaCd = "";

                        return;
                    }

                    M_GENBA genba = new M_GENBA();
                    genba.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    genba.GENBA_CD = this.form.GENBA_CD.Text;
                    genba.JISHA_KBN = true;
                    genba.ISNOT_NEED_DELETE_FLG = true;
                    genba = this.genbaDao.GetAllValidData(genba).FirstOrDefault();

                    if (genba == null)
                    {
                        this.form.isError = true;
                        this.form.GENBA_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.GENBA_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "現場");
                        this.form.GENBA_CD.Focus();
                        return;
                    }
                    else
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }

                    this.form.beforeGenbaCd = this.form.GENBA_CD.Text;

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GENBA_CD_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region 在庫品名CDValidated
        /// <summary>
        /// 在庫品名CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void ZAIKO_HINMEI_CD_Validated()
        {
            var messageLogic = new MessageBoxShowLogic();
            try
            {
                if (this.form.beforeZaikoHinmeiCd != this.form.ZAIKO_HINMEI_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.ZAIKO_HINMEI_CD.Text))
                    {
                        this.form.ZAIKO_HINMEI_NAME.Text = "";
                        this.form.beforeZaikoHinmeiCd = "";
                        return;
                    }

                    M_ZAIKO_HINMEI zaikoHinmei = new M_ZAIKO_HINMEI();
                    zaikoHinmei.ZAIKO_HINMEI_CD = this.form.ZAIKO_HINMEI_CD.Text;
                    zaikoHinmei.ISNOT_NEED_DELETE_FLG = true;
                    zaikoHinmei = this.zaikoHinmeiDao.GetAllValidData(zaikoHinmei).FirstOrDefault();

                    if (zaikoHinmei == null)
                    {
                        this.form.isError = true;
                        this.form.ZAIKO_HINMEI_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.ZAIKO_HINMEI_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "在庫品名");
                        this.form.ZAIKO_HINMEI_CD.Focus();
                        return;
                    }
                    else
                    {
                        this.form.ZAIKO_HINMEI_NAME.Text = zaikoHinmei.ZAIKO_HINMEI_NAME_RYAKU;
                    }

                    this.form.beforeZaikoHinmeiCd = this.form.ZAIKO_HINMEI_CD.Text;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ZAIKO_HINMEI_CD_Validated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
            }
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
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
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();

                string[] errorMsg = { "入力日付From", "入力日付To" };
                msglogic.MessageBoxShow("E030", errorMsg);

                this.form.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion

        #region 未実装ベースメソッド
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
    }
}