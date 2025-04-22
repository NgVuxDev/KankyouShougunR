using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.Message;
using r_framework.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Dao;


namespace Shougun.Core.ElectronicManifest.RealInfoSearch.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド
        //区間番号1
        private static readonly string UPN_ROUTE_NO_1 = "1";

        //区間番号2
        private static readonly string UPN_ROUTE_NO_2 = "2";

        //区間番号3
        private static readonly string UPN_ROUTE_NO_3 = "3";

        //区間番号4
        private static readonly string UPN_ROUTE_NO_4 = "4";

        //区間番号5
        private static readonly string UPN_ROUTE_NO_5 = "5";

        //機能番号3100(マニフェスト情報照会)
        private static readonly string FUNCTION_ID_3100 = "3100";

        //マニ番条件：１．範囲
        private static readonly string MANI_JYOKEN_HANNI = "1";

        //マニ番条件：２．指定
        private static readonly string MANI_JYOKEN_SHITEI = "2";

        //日付：最終更新：１．３日以内
        private static readonly string HIDUKE_3DAY = "1";

        //日付：１．引渡日
        private static readonly string HIKIWATA_DATE = "1";

        // 一時間にチェックできる最大件数
        public readonly int MAX_CHECK = 100;

        // 照会中判定単位(Hour)
        public readonly int EXECUTING_DECISION_HOUR = 1;

        // 照会済判定単位(Hour)
        public readonly int CONPLETED_DECISION_HOUR = 24;

        // 他社ＥＤＩ使用:3.全て
        private static readonly string TASHAEDI_DEFUALT = "3";

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.RealInfoSearch.Setting.ButtonSetting.xml";

        /// <summary>
        /// 最新情報照会画面
        /// </summary>
        private UIForm form;
        private UIHeader header;
        private BusinessBaseForm footer;

        /// <summary>
        /// 最新情報検索処理用Dao
        /// </summary>
        private GetInfoDaoCls GetInfoDaoCls;

        /// <summary>
        /// 検索実行かどうか判断フラグ
        /// </summary>
        public Boolean kensakuFlg = false;

        /// <summary>
        /// キュー情報更新Dao
        /// </summary>
        private InsertQIDaoCls InsertQIDaoCls;

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
        /// 紐付したい対象検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 特定現場照会検索結果
        /// </summary>
        public DataTable SearchResultHST { get; set; }
        public DataTable SearchResultUPN { get; set; }
        public DataTable SearchResultSBN { get; set; }

        /// <summary>
        /// 紐付したい対象検索結果
        /// </summary>
        public List<QUE_INFO> ToukuteiGenbaSyoukaiList;

        /// <summary>
        /// 明細表示フラグ
        /// </summary>
        //public Boolean meisaihyoujiFlg;//2013.12.25 touti upd画面起動時に検索しない 処理方法修正

        /// <summary>
        /// 管理ID
        /// </summary>
        internal readonly string HIDDEN_KANRI_ID = "HIDDEN_KANRI_ID";

        /// <summary>
        /// 最新枝番
        /// </summary>
        internal readonly string HIDDEN_LATEST_SEQ = "LATEST_SEQ";

        /// <summary>
        /// 運搬区間番号
        /// </summary>
        internal readonly string UPN_ROUTE_NO_INSERT = "UPN_ROUTE_NO_INSERT";

        /// <summary>
        /// 排出事業者パスワード
        /// </summary>
        internal readonly string EDI_PASSWORD_HST_INSERT = "EDI_PASSWORD_HST_INSERT";

        /// <summary>
        /// 処分事業者パスワード
        /// </summary>
        internal readonly string EDI_PASSWORD_SBN_INSERT = "EDI_PASSWORD_SBN_INSERT";

        /// <summary>
        /// 運搬事業者パスワード
        /// </summary>
        internal readonly string EDI_PASSWORD_UPN_INSERT = "EDI_PASSWORD_UPN_INSERT";

        internal readonly string CAN_CHECK = "CAN_CHECK";

        internal readonly string COLUMN_NAME_MANIFEST_ID = "マニフェスト／予約番号";
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.GetInfoDaoCls = DaoInitUtility.GetComponent<GetInfoDaoCls>();
            this.InsertQIDaoCls = DaoInitUtility.GetComponent<InsertQIDaoCls>();
            this.ToukuteiGenbaSyoukaiList = new List<QUE_INFO>();
            //this.meisaihyoujiFlg = false;//2013.12.25 touti upd画面起動時に検索しない 処理方法修正
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンを初期化
                this.ButtonInit();
                //footボタン処理イベントを初期化
                EventInit();

                // 検索条件の初期化
                this.form.cntb_Jyoken_KBN.Text = MANI_JYOKEN_HANNI;
                this.form.cntb_Hiduke_KBN.Text = HIDUKE_3DAY;
                this.form.cntb_KoufuDate_KBN.Text = HIKIWATA_DATE;
                this.form.cntb_TashaEDI_KBN.Text = TASHAEDI_DEFUALT;

                this.form.searchString.Visible = false;

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
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
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }


        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            this.header = (UIHeader)parentForm.headerForm;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //プレビューボタン(F7)イベント生成
            parentform.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

            //検索ボタン(F8)イベント生成
            parentform.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //登録ボタン(F9)イベント生成
            parentform.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //並び順ボタン(F10)イベント生成
            parentform.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentform.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentform.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            //【3】特定現場照会(3)イベント生成
            //parentform.bt_process3.Click += new EventHandler(this.form.bt_process3_Click);

            //ESCテキストイベント生成
            parentform.txb_process.KeyDown += new KeyEventHandler(this.form.TXB_PROCESS_KeyDown);

            /// 20141023 Houkakou 「最新情報照会」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.cntb_ManifestNoTo.MouseDoubleClick += new MouseEventHandler(cntb_ManifestNoTo_MouseDoubleClick);
            this.form.cntb_KoufuDateTo.MouseDoubleClick += new MouseEventHandler(cntb_KoufuDateTo_MouseDoubleClick);
            /// 20141023 Houkakou 「最新情報照会」のダブルクリックを追加する　end
            /// 
            //this.form.customDataGridView1.CellClick += new DataGridViewCellEventHandler(this.form.customDataGridView1_CellClick);

            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.form.customDataGridView1_ColumnHeaderMouseClick);
            this.form.customDataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(this.form.customDataGridView1_CellMouseClick);
            this.form.customDataGridView1.CurrentCellDirtyStateChanged += new EventHandler(this.form.customDataGridView1_CurrentCellDirtyStateChanged);

            LogUtility.DebugMethodEnd();
        }



        /// <summary>
        /// 登録
        /// </summary>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ロジック削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新削除
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 画面一覧検索処理
        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public bool HeaderCheckBoxSupport()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                newColumn.Name = "";
                newColumn.Width = 25;
                CustomDgvCheckBoxHeaderCell_Ex newheader = new CustomDgvCheckBoxHeaderCell_Ex(0, this.form);
                newColumn.HeaderCell = newheader;
                newColumn.ReadOnly = false;

                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(1, newColumn);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(newColumn);
                }
                this.form.customDataGridView1.Columns[0].ToolTipText = "処理対象とする場合はチェックしてください";

                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HeaderCheckBoxSupport", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 選択用チェックボックスをクリアする
        /// </summary>
        /// <returns>true:正常、false:異常</returns>
        public bool ClearCheckBox()
        {
            try
            {
                if (this.form.customDataGridView1 == null
                    || this.form.customDataGridView1.Columns == null
                    || this.form.customDataGridView1.Rows == null
                    || this.form.customDataGridView1.Rows.Count < 1)
                {
                    return true;
                }

                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    dgvRow.Cells[0].Value = false;
                    if (dgvRow.Cells[7].Value.ToString() == "0")
                    {
                        dgvRow.Cells[0].ReadOnly = true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCheckBox", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                kensakuFlg = false;

                //SQL文格納StringBuilder
                StringBuilder sql = new StringBuilder();

                #region SELECT句

                sql.Append(" SELECT DISTINCT");

                sql.AppendFormat(" DT_MF_TOC.KANRI_ID AS {0}, ", this.HIDDEN_KANRI_ID);
                sql.AppendFormat(" DT_MF_TOC.LATEST_SEQ AS {0}, ", this.HIDDEN_LATEST_SEQ);

                sql.AppendFormat(" DT_R19.UPN_ROUTE_NO AS {0},  ", this.UPN_ROUTE_NO_INSERT);
                sql.AppendFormat(" MS_JWNET_MEMBER_HST.EDI_PASSWORD AS {0},  ", this.EDI_PASSWORD_HST_INSERT);
                sql.AppendFormat(" MS_JWNET_MEMBER_SBN.EDI_PASSWORD AS {0},  ", this.EDI_PASSWORD_SBN_INSERT);
                sql.AppendFormat(" MS_JWNET_MEMBER_UPN.EDI_PASSWORD AS {0},  ", this.EDI_PASSWORD_UPN_INSERT);
                sql.AppendFormat(" CASE WHEN QUE_INFO.KANRI_ID IS NULL THEN '1' ELSE '0' END AS {0}  ", this.CAN_CHECK);

                //データベースから検索項目
                String MOutputPatternSelect = this.selectQuery;
                if (String.IsNullOrEmpty(MOutputPatternSelect))
                {
                    this.form.customDataGridView1.DataSource = null;
                    this.form.customDataGridView1.Columns.Clear();
                    kensakuFlg = true;
                    return 0;
                }
                sql.Append(", ");
                sql.Append(MOutputPatternSelect);
                //string str1 = "CASE WHEN (DT_R18.HIKIWATASHI_DATE IS NULL OR DT_R18.HIKIWATASHI_DATE = '' OR (ISDATE(DT_R18.HIKIWATASHI_DATE) = 0)) " +
                //    "THEN NULL ELSE CONVERT(DATETIME,DT_R18.HIKIWATASHI_DATE) END";
                //string str2 = "CASE WHEN (DT_R19.UPN_END_DATE IS NULL OR DT_R19.UPN_END_DATE = '' OR (ISDATE(DT_R19.UPN_END_DATE) = 0)) " +
                //    "THEN NULL ELSE CONVERT(DATETIME,DT_R19.UPN_END_DATE) END";
                //string str3 = "CASE WHEN (DT_R18.SBN_END_DATE IS NULL OR DT_R18.SBN_END_DATE = '' OR (ISDATE(DT_R18.SBN_END_DATE) = 0)) " +
                //    "THEN NULL ELSE CONVERT(DATETIME,DT_R18.SBN_END_DATE) END";
                //string str4 = "CASE WHEN (DT_R18.LAST_SBN_END_DATE IS NULL OR DT_R18.LAST_SBN_END_DATE = '' OR (ISDATE(DT_R18.LAST_SBN_END_DATE) = 0)) " +
                //    "THEN NULL ELSE CONVERT(DATETIME,DT_R18.LAST_SBN_END_DATE) END";
                //sql.Replace("DT_R18.HIKIWATASHI_DATE", str1);
                //sql.Replace("DT_R19.UPN_END_DATE", str2);
                //sql.Replace("DT_R18.SBN_END_DATE", str3);
                //sql.Replace("DT_R18.LAST_SBN_END_DATE", str4);
                #endregion

                #region FROM句
                sql.Append(" FROM DT_MF_TOC ");
                sql.Append("    INNER JOIN DT_R18 ON ");
                sql.Append("      DT_R18.KANRI_ID = DT_MF_TOC.KANRI_ID ");
                sql.Append("      AND DT_R18.SEQ = DT_MF_TOC.LATEST_SEQ ");
                sql.Append("    INNER JOIN (SELECT R18EX.SYSTEM_ID,");
                sql.Append("                      R18EX.SEQ,");
                sql.Append("                      R18EX.KANRI_ID,");
                sql.Append("                      R18EX.MANIFEST_ID,");
                sql.Append("                      R18EX.HST_GYOUSHA_CD,");
                sql.Append("                      R18EX.HST_GENBA_CD,");
                sql.Append("                      R18EX.SBN_GYOUSHA_CD,");
                sql.Append("                      R18EX.SBN_GENBA_CD,");
                sql.Append("                      R18EX.NO_REP_SBN_EDI_MEMBER_ID,");
                sql.Append("                      R18EX.SBN_HOUHOU_CD,");
                sql.Append("                      R18EX.HOUKOKU_TANTOUSHA_CD,");
                sql.Append("                      R18EX.SBN_TANTOUSHA_CD,");
                sql.Append("                      R18EX.UPN_TANTOUSHA_CD,");
                sql.Append("                      R18EX.SHARYOU_CD,");
                sql.Append("                      R18EX.KANSAN_SUU,");
                sql.Append("                      CREATE_DATA.CREATE_USER,");
                sql.Append("                      R18EX.CREATE_DATE,");
                sql.Append("                      CREATE_DATA.CREATE_PC,");
                sql.Append("                      R18EX.UPDATE_USER,");
                sql.Append("                      R18EX.UPDATE_DATE,");
                sql.Append("                      R18EX.UPDATE_PC,");
                sql.Append("                      R18EX.DELETE_FLG,");
                sql.Append("                      R18EX.TIME_STAMP,");
                sql.Append("                      R18EX.HAIKI_NAME_CD,");
                sql.Append("                      R18EX.GENNYOU_SUU");
                sql.Append("                 FROM DT_R18_EX R18EX,");
                sql.Append("                      (SELECT R18EX.SYSTEM_ID,");
                sql.Append("                              R18EX.SEQ,");
                sql.Append("                              R18EX.KANRI_ID,");
                sql.Append("                              R18EX.CREATE_USER,");
                sql.Append("                              R18EX.CREATE_DATE,");
                sql.Append("                              R18EX.CREATE_PC");
                sql.Append("                         FROM DT_R18_EX R18EX,");
                sql.Append("                              (SELECT SYSTEM_ID, MIN(SEQ) MIN_SEQ");
                sql.Append("                                 FROM DT_R18_EX");
                sql.Append("                                GROUP BY SYSTEM_ID) SEQ_DATA");
                sql.Append("                        WHERE R18EX.SYSTEM_ID = seq_data.SYSTEM_ID");
                sql.Append("                          AND R18EX.SEQ = SEQ_DATA.MIN_SEQ) CREATE_DATA");
                sql.Append("                WHERE R18EX.SYSTEM_ID = CREATE_DATA.SYSTEM_ID) DT_R18_EX");
                sql.Append("      ON DT_R18.KANRI_ID = DT_R18_EX.KANRI_ID AND DT_R18_EX.DELETE_FLG = 0");
                sql.Append("    LEFT JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_HST ");
                sql.Append("      ON MS_JWNET_MEMBER_HST.EDI_MEMBER_ID = DT_R18.HST_SHA_EDI_MEMBER_ID ");
                sql.Append("    LEFT JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_SBN ");
                sql.Append("      ON MS_JWNET_MEMBER_SBN.EDI_MEMBER_ID = DT_R18.SBN_SHA_MEMBER_ID ");
                sql.Append("    LEFT JOIN ");
                sql.Append("    ( SELECT ");
                sql.Append("        R19.KANRI_ID, ");
                sql.Append("        R19.SEQ, ");
                sql.Append("        MAX(R19.UPN_ROUTE_NO) AS UPN_ROUTE_NO ");
                sql.Append("      FROM ");
                sql.Append("        DT_MF_TOC TOC ");
                sql.Append("        INNER JOIN DT_R19 R19 ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ ");
                sql.Append("        LEFT JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_UPN ON  MS_JWNET_MEMBER_UPN.EDI_MEMBER_ID = R19.UPN_SHA_EDI_MEMBER_ID ");
                sql.Append("      WHERE ");
                sql.Append("        MS_JWNET_MEMBER_UPN.EDI_MEMBER_ID IS NOT NULL ");
                sql.Append("      GROUP BY ");
                sql.Append("        R19.KANRI_ID, R19.SEQ ");
                sql.Append("    ) AS MAX_R19 ON  MAX_R19.KANRI_ID = DT_R18.KANRI_ID AND MAX_R19.SEQ = DT_R18.SEQ ");
                sql.Append("    LEFT JOIN DT_R19 ON DT_R19.KANRI_ID = MAX_R19.KANRI_ID AND DT_R19.SEQ = MAX_R19.SEQ AND DT_R19.UPN_ROUTE_NO = MAX_R19.UPN_ROUTE_NO ");
                sql.Append("    LEFT JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_UPN ON  DT_R19.UPN_SHA_EDI_MEMBER_ID = MS_JWNET_MEMBER_UPN.EDI_MEMBER_ID ");
                sql.Append("    LEFT JOIN DT_R19_EX");
                sql.Append("      ON DT_R18_EX.SYSTEM_ID = DT_R19_EX.SYSTEM_ID");
                sql.Append("     AND DT_R18_EX.SEQ = DT_R19_EX.SEQ");
                sql.Append("     AND DT_R18_EX.KANRI_ID = DT_R19_EX.KANRI_ID");
                sql.Append("     AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO");
                sql.Append("    LEFT JOIN ");
                sql.Append("    ( SELECT ");
                sql.Append("        R19.KANRI_ID, ");
                sql.Append("        R19.SEQ, ");
                sql.Append("        MAX(R19.UPN_ROUTE_NO) AS UPN_ROUTE_NO ");
                sql.Append("      FROM ");
                sql.Append("        DT_MF_TOC TOC ");
                sql.Append("        INNER JOIN DT_R19 R19 ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ ");
                sql.Append("      GROUP BY ");
                sql.Append("        R19.KANRI_ID, R19.SEQ ");
                sql.Append("    ) AS DISP_MAX_R19 ON  DISP_MAX_R19.KANRI_ID = DT_R18.KANRI_ID AND DISP_MAX_R19.SEQ = DT_R18.SEQ ");
                sql.Append("    LEFT JOIN DT_R19 AS DISP_DT_R19 ON DISP_DT_R19.KANRI_ID = DISP_MAX_R19.KANRI_ID AND DISP_DT_R19.SEQ = DISP_MAX_R19.SEQ AND DISP_DT_R19.UPN_ROUTE_NO = DISP_MAX_R19.UPN_ROUTE_NO ");
                sql.Append("    LEFT JOIN ");
                sql.Append("    ( SELECT ");
                sql.Append("        R19.KANRI_ID, ");
                sql.Append("        R19.SEQ, ");
                sql.Append("        MIN(R19.UPN_ROUTE_NO) AS UPN_ROUTE_NO ");
                sql.Append("      FROM ");
                sql.Append("        DT_MF_TOC TOC ");
                sql.Append("        INNER JOIN DT_R19 R19 ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ ");
                // 運搬受託者
                if (!string.IsNullOrEmpty(this.form.cntb_UpnJigyoushaCd.Text))
                {
                    sql.Append("   AND R19.UPN_SHA_EDI_MEMBER_ID = '");
                    sql.Append(this.form.cntb_UpnJigyoushaCd.Text);
                    sql.Append("' ");
                }
                sql.Append("      GROUP BY ");
                sql.Append("        R19.KANRI_ID, R19.SEQ ");
                sql.Append("    ) AS DISP_MIN_R19 ON  DISP_MIN_R19.KANRI_ID = DT_R18.KANRI_ID AND DISP_MIN_R19.SEQ = DT_R18.SEQ ");
                sql.Append("    LEFT JOIN DT_R19 AS DISP_MIN_DT_R19 ON DISP_MIN_DT_R19.KANRI_ID = DISP_MIN_R19.KANRI_ID AND DISP_MIN_DT_R19.SEQ = DISP_MIN_R19.SEQ AND DISP_MIN_DT_R19.UPN_ROUTE_NO = DISP_MIN_R19.UPN_ROUTE_NO ");
                sql.Append("    LEFT JOIN QUE_INFO ");
                sql.Append("      ON DT_MF_TOC.KANRI_ID = QUE_INFO.KANRI_ID ");
                sql.Append("      AND DT_MF_TOC.LATEST_SEQ = QUE_INFO.SEQ ");
                sql.Append("      AND QUE_INFO.FUNCTION_ID = '3100' ");
                sql.Append("      AND ( (QUE_INFO.STATUS_FLAG IN ('0', '1')) ");
                sql.Append("        OR (QUE_INFO.STATUS_FLAG = '2' ");
                sql.Append(string.Format("      AND SYSDATETIME() < DATEADD( HH, {0}, QUE_INFO.CREATE_DATE) ) ", this.EXECUTING_DECISION_HOUR));
                sql.Append("      ) ");
                sql.Append("    LEFT JOIN ( ");
                sql.Append("      SELECT ");
                sql.Append("        QUE_INFO.* ");
                sql.Append("      FROM ");
                sql.Append("        ( ");
                sql.Append("          SELECT ");
                sql.Append("            KANRI_ID, ");
                sql.Append("            MAX(QUE_SEQ) AS MAX_QUE_SEQ ");
                sql.Append("          FROM ");
                sql.Append("            QUE_INFO ");
                sql.Append("          WHERE ");
                sql.Append("            FUNCTION_ID = '3100' ");
                sql.Append("          GROUP BY ");
                sql.Append("            KANRI_ID ");
                sql.Append("        ) AS MAX_QUE ");
                sql.Append("        INNER JOIN QUE_INFO ");
                sql.Append("        ON MAX_QUE.KANRI_ID = QUE_INFO.KANRI_ID ");
                sql.Append("        AND MAX_QUE.MAX_QUE_SEQ = QUE_INFO.QUE_SEQ ");
                sql.Append("    ) AS EXECUTED_QUE ");
                sql.Append("      ON DT_MF_TOC.KANRI_ID = EXECUTED_QUE.KANRI_ID ");
                sql.Append("      AND EXECUTED_QUE.FUNCTION_ID = '3100' ");
                sql.Append("      AND (EXECUTED_QUE.STATUS_FLAG = '2' ");
                sql.Append(string.Format("      AND SYSDATETIME() < DATEADD( HH, {0}, EXECUTED_QUE.CREATE_DATE) ) ", this.CONPLETED_DECISION_HOUR));

                sql.Append(this.joinQuery);

                #endregion

                #region WHERE句
                sql.Append("WHERE ");
                //2013.12.25 touti upd画面起動時に検索しない 処理方法修正 start
                //if (meisaihyoujiFlg)
                //{
                sql.Append("    DT_MF_TOC.STATUS_DETAIL <> '1'  ");
                sql.Append("    AND (DT_MF_TOC.STATUS_FLAG = 3 OR DT_MF_TOC.STATUS_FLAG = 4) ");
                // 入力区分：手動は表示しない
                sql.Append("    AND  (DT_MF_TOC.KIND IS NULL OR DT_MF_TOC.KIND != 5)");

                //or条件作成用string
                string endrepFlag;
                endrepFlag = "";

                //運搬未完了
                if (this.form.ccb_Unpan.Checked == true)
                {
                    if (endrepFlag != "")
                    {
                        endrepFlag = endrepFlag + " OR ";
                    }
                    endrepFlag = endrepFlag + "DT_R18.UPN_ENDREP_FLAG = '0'";
                }

                //処分未完了
                if (this.form.ccb_Syobu.Checked == true)
                {
                    if (endrepFlag != "")
                    {
                        endrepFlag = endrepFlag + " OR ";
                    }
                    endrepFlag = endrepFlag + "DT_R18.SBN_ENDREP_FLAG = '0' AND DT_R18.SBN_SHA_MEMBER_ID <> '0000000'";
                }

                //最終処分未完了
                if (this.form.ccb_FinalSyobu.Checked == true)
                {
                    if (endrepFlag != "")
                    {
                        endrepFlag = endrepFlag + " OR ";
                    }
                    endrepFlag = endrepFlag + "DT_R18.LAST_SBN_ENDREP_FLAG = '0' AND DT_R18.SBN_SHA_MEMBER_ID <> '0000000'";
                }

                if (endrepFlag != "")
                {
                    sql.Append("   AND (" + endrepFlag + ")  ");
                }

                //マニフェスト情報.最終更新日　

                if (this.form.crdbtn_HidukeJyoken1.Checked == true)
                {
                    sql.Append("   AND DT_R18.LAST_UPDATE_DATE BETWEEN ");
                    sql.Append("    CONVERT (nvarchar(8),DATEADD(day, -2, SYSDATETIME())  ,112 ) ");
                    sql.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
                }
                else if (this.form.crdbtn_HidukeJyoken2.Checked == true)
                {
                    sql.Append("   AND DT_R18.LAST_UPDATE_DATE BETWEEN ");
                    sql.Append("    CONVERT (nvarchar(8),DATEADD(day, -6, SYSDATETIME())  ,112 ) ");
                    sql.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
                }
                else if (this.form.crdbtn_HidukeJyoken3.Checked == true)
                {
                    sql.Append("  AND  DT_R18.LAST_UPDATE_DATE BETWEEN ");
                    sql.Append("    CONVERT (nvarchar(8),DATEADD(day, -29, SYSDATETIME())  ,112 ) ");
                    sql.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
                }
                else if (this.form.crdbtn_HidukeJyoken4.Checked == true)
                {
                    sql.Append("  AND  DT_R18.LAST_UPDATE_DATE BETWEEN ");
                    sql.Append("    CONVERT (nvarchar(8),DATEADD(day, -89, SYSDATETIME())  ,112 ) ");
                    sql.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
                }
                else if (this.form.crdbtn_HidukeJyoken5.Checked == true)
                {
                    sql.Append("  AND  DT_R18.LAST_UPDATE_DATE BETWEEN ");
                    sql.Append("    CONVERT (nvarchar(8),DATEADD(day, -179, SYSDATETIME())  ,112 ) ");
                    sql.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
                }
                //マニ番号範囲
                if (this.form.crdbtn_ManiJyokenHan.Checked == true)
                {
                    if (!String.IsNullOrEmpty(this.form.cntb_ManifestNoFrom.Text))
                    {
                        sql.Append("    AND DT_R18.MANIFEST_ID >='");
                        sql.Append(this.form.cntb_ManifestNoFrom.Text);
                        sql.Append("'  ");
                    }
                    if (!String.IsNullOrEmpty(this.form.cntb_ManifestNoTo.Text))
                    {
                        sql.Append("    AND DT_R18.MANIFEST_ID <='");
                        sql.Append(this.form.cntb_ManifestNoTo.Text);
                        sql.Append("'  ");
                    }

                }

                //マニ番号指定
                else if (this.form.crdbtn_ManiJyokenSitei.Checked == true)
                {

                    sql.Append("   AND DT_R18.MANIFEST_ID IN ('");
                    sql.Append(this.form.cntb_ManifestNo1.Text);
                    sql.Append("','");
                    sql.Append(this.form.cntb_ManifestNo2.Text);
                    sql.Append("','");
                    sql.Append(this.form.cntb_ManifestNo3.Text);
                    sql.Append("','");
                    sql.Append(this.form.cntb_ManifestNo4.Text);
                    sql.Append("','");
                    sql.Append(this.form.cntb_ManifestNo5.Text);
                    sql.Append("') ");

                }
                //マニ番条件テキストに数値ありでもマニ番範囲、
                //マニ番指定テキストに値がなければ検索しないようにする(ITバグトラブル管理表_G_電子マニのバグ)
                else
                {
                    //sql.Append("  AND 1=2 ");
                }

                // 排出事業者
                if (!string.IsNullOrEmpty(this.form.cntb_HstJigyoushaCd.Text))
                {
                    sql.Append("   AND DT_R18.HST_SHA_EDI_MEMBER_ID = '");
                    sql.Append(this.form.cntb_HstJigyoushaCd.Text);
                    sql.Append("' ");
                }

                // 排出事業場
                if (!string.IsNullOrEmpty(this.form.cntb_HstJigyoujouCd.Text))
                {
                    // 排出事業場はCDがないため、名称と所在地１～４の一致で検索する
                    sql.Append("   AND ");
                    sql.Append("   EXISTS (SELECT * FROM DT_R18 DTR18,M_DENSHI_JIGYOUJOU ");
                    sql.Append("           WHERE  DTR18.KANRI_ID = DT_R18.KANRI_ID ");
                    sql.Append("              AND DTR18.SEQ = DT_R18.SEQ ");
                    sql.Append("              AND M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID = '" + this.form.cntb_HstJigyoushaCd.Text + "'");
                    sql.Append("              AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_CD = '" + this.form.cntb_HstJigyoujouCd.Text + "'");
                    sql.Append("              AND ISNULL(DTR18.HST_JOU_NAME, '') = ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME, '') ");
                    sql.Append("              AND (ISNULL(DTR18.HST_JOU_ADDRESS1, '') + ISNULL(DTR18.HST_JOU_ADDRESS2, '') + ISNULL(DTR18.HST_JOU_ADDRESS3, '') + ISNULL(DTR18.HST_JOU_ADDRESS4, '')) ");
                    sql.Append("                = (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, ''))) ");
                }

                // 運搬受託者
                if (!string.IsNullOrEmpty(this.form.cntb_UpnJigyoushaCd.Text))
                {
                    sql.Append("   AND DISP_MIN_DT_R19.UPN_SHA_EDI_MEMBER_ID = '");
                    sql.Append(this.form.cntb_UpnJigyoushaCd.Text);
                    sql.Append("' ");
                }

                // 処分受託者
                if (!string.IsNullOrEmpty(this.form.cntb_SbnJigyoushaCd.Text))
                {
                    sql.Append("   AND DT_R18.SBN_SHA_MEMBER_ID = '");
                    sql.Append(this.form.cntb_SbnJigyoushaCd.Text);
                    sql.Append("' ");
                }

                // 処分事業場
                if (!string.IsNullOrEmpty(this.form.cntb_SbnJigyoujouCd.Text))
                {
                    sql.Append("   AND ");

                    sql.Append("   EXISTS (SELECT * FROM DT_R19 DTR19,M_DENSHI_JIGYOUJOU ");
                    sql.Append("           WHERE  DTR19.KANRI_ID = DISP_DT_R19.KANRI_ID ");
                    sql.Append("              AND DTR19.SEQ = DISP_DT_R19.SEQ ");
                    sql.Append("              AND M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID = '" + this.form.cntb_SbnJigyoushaCd.Text + "'");
                    sql.Append("              AND M_DENSHI_JIGYOUJOU.JIGYOUJOU_CD = '" + this.form.cntb_SbnJigyoujouCd.Text + "'");
                    sql.Append("              AND ISNULL(DTR19.UPNSAKI_JOU_NAME, '') = ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME, '') ");
                    sql.Append("              AND (ISNULL(DTR19.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(DTR19.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DTR19.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DTR19.UPNSAKI_JOU_ADDRESS4, '')) ");
                    sql.Append("                = (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, ''))) ");
                }

                // 日付
                switch (this.form.cntb_KoufuDate_KBN.Text)
                {
                    // 引渡日
                    case "1":
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateFrom.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DT_R18.HIKIWATASHI_DATE) >= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateFrom.Text));
                            sql.Append("' ");
                        }
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateTo.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DT_R18.HIKIWATASHI_DATE) <= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateTo.Text));
                            sql.Append("' ");
                        }
                        break;
                    // 運搬終了日
                    case "2":
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateFrom.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DISP_DT_R19.UPN_END_DATE) >= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateFrom.Text));
                            sql.Append("' ");
                        }
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateTo.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DISP_DT_R19.UPN_END_DATE) <= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateTo.Text));
                            sql.Append("' ");
                        }
                        break;
                    // 処分終了日
                    case "3":
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateFrom.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DT_R18.SBN_END_DATE) >= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateFrom.Text));
                            sql.Append("' ");
                        }
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateTo.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DT_R18.SBN_END_DATE) <= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateTo.Text));
                            sql.Append("' ");
                        }
                        break;
                    // 最終処分終了日
                    case "4":
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateFrom.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DT_R18.LAST_SBN_END_DATE) >= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateFrom.Text));
                            sql.Append("' ");
                        }
                        if (!string.IsNullOrEmpty(this.form.cntb_KoufuDateTo.Text))
                        {
                            sql.Append("   AND CONVERT(DATETIME, DT_R18.LAST_SBN_END_DATE) <= '");
                            sql.Append(Convert.ToDateTime(this.form.cntb_KoufuDateTo.Text));
                            sql.Append("' ");
                        }
                        break;
                    // 無し
                    default:
                        break;
                }

                //検索条件表示テキスト
                if (!this.form.searchString.Text.Equals(String.Empty))
                {
                    sql.Append("   AND ");
                    sql.Append(this.form.searchString.Text);
                }
                //2013.12.25 touti upd画面起動時に検索しない 処理方法修正 end
                //}
                //else
                //{
                //    sql.Append("1 = 2 --初期とパターン押す後明細表示さらない");
                //    meisaihyoujiFlg = true;
                //}

                // 他社ＥＤＩ使用
                switch (this.form.cntb_TashaEDI_KBN.Text)
                {
                    // 使用排出表示
                    case "1":
                        sql.Append("AND EXISTS ( ");
  	                    sql.Append("SELECT *  ");
	                    sql.Append("FROM M_GYOUSHA  ");
	                    sql.Append("LEFT JOIN M_DENSHI_JIGYOUSHA  ");
	                    sql.Append("ON M_GYOUSHA.GYOUSHA_CD = M_DENSHI_JIGYOUSHA.GYOUSHA_CD ");
	                    sql.Append("WHERE M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID = DT_R18.HST_SHA_EDI_MEMBER_ID ");
	                    sql.Append("AND M_GYOUSHA.TASYA_EDI = 1) ");
                        break;
                    // 未使用排出表示
                    case "2":
                        sql.Append("AND EXISTS ( ");
  	                    sql.Append("SELECT *  ");
	                    sql.Append("FROM M_GYOUSHA  ");
	                    sql.Append("LEFT JOIN M_DENSHI_JIGYOUSHA  ");
	                    sql.Append("ON M_GYOUSHA.GYOUSHA_CD = M_DENSHI_JIGYOUSHA.GYOUSHA_CD ");
	                    sql.Append("WHERE M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID = DT_R18.HST_SHA_EDI_MEMBER_ID ");
	                    sql.Append("AND M_GYOUSHA.TASYA_EDI = 2) ");
                        break;
                    // 全て
                    default:
                        break;
                }
                #endregion

                #region ORDERBY句
                if (!string.IsNullOrEmpty(this.orderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.orderByQuery);
                }
                else
                {
                    sql.Append("   ORDER BY ");
                    sql.Append("   DT_R18.HIKIWATASHI_DATE DESC, ");
                    sql.Append("   DT_R18.MANIFEST_ID DESC, ");
                    sql.Append("   DT_R19.UPN_ROUTE_NO ASC ");
                }
                #endregion

                this.SearchResult = new DataTable();
                this.SearchResult = GetInfoDaoCls.getdataforstringsql(sql.ToString());

                //パターンが1件以上の場合
                if (!kensakuFlg)
                {
                    if (Set_Search() == -1)
                    {
                        return -1;
                    }
                }


                int count = this.SearchResult.Rows.Count;
                return count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 画面表示
        /// </summary>
        public int Set_Search()
        {
            LogUtility.DebugMethodStart();

            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.Columns.Clear();

            if (!HeaderCheckBoxSupport()) { return -1; }

            //一覧の項目を非表示
            this.form.ShowData();

            this.form.customDataGridView1.Columns[0].ReadOnly = false;
            this.form.customDataGridView1.AllowUserToAddRows = false;
            this.form.customDataGridView1.MultiSelect = false;

            int cnt = GetRequestDataInDay();
            if (cnt == -1)
            {
                return -1;
            }

            if (cnt == 100)
            {
                this.form.isShowMessage = true;
            }

            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                dgvRow.Cells[0].Value = false;
                if (dgvRow.Cells[7].Value.ToString() == "0")
                {
                    dgvRow.Cells[0].ReadOnly = true;
                }
            }

            LogUtility.DebugMethodEnd();

            return cnt;
        }

        #endregion

        #region 情報照会処理
        /// <summary>
        /// 情報情報照会
        /// </summary>
        public void infoSearch()
        {
            try
            {
                ToukuteiGenbaSyoukaiList.Clear();
                bool flg = inSertInfoData();

                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    if (flg == false)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("{0}件の情報照会を行います。（{1}の累計照会件数{2}件）\nよろしいでしょうか？",
                            GetCheckedRow().ToString(), string.Format("過去{0}時間以内", this.EXECUTING_DECISION_HOUR),
                            (this.GetRequestDataInDay() + GetCheckedRow()).ToString());

                        if (MessageBox.Show(sb.ToString(), Constans.CONFIRM_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            == DialogResult.Yes)
                        {
                            foreach (QUE_INFO data in ToukuteiGenbaSyoukaiList)
                            {
                                InsertQIDaoCls.Insert(data);
                            }

                            //検索処理
                            if (this.Search() == -1) { return; }

                            //QUE_INFO
                            //現場照会QUE_INFOリストの取得
                            if (0 < ToukuteiGenbaSyoukaiList.Count)
                            {
                                tran.Commit();
                                MessageBoxUtility.MessageBoxShow("I001", "情報照会");
                            }
                        }
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("infoSearch", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("infoSearch", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("infoSearch", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 一覧の選択チェックボックスにチェックが入っているマニフェストのQUE_INFOに登録
        /// </summary>
        /// /// <returns></returns>
        private bool inSertInfoData()
        {
            LogUtility.DebugMethodStart();

            //チェックフラグ
            Boolean checkFlg = false;

            //DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;


            //int index = 0;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        checkFlg = true;
                        T_LAST_SBN_SUSPEND lastsbnsuspend = new T_LAST_SBN_SUSPEND();
                        QUE_INFO queinfo = new QUE_INFO();

                        string kanriID = dgvRow.Cells[this.HIDDEN_KANRI_ID].Value.ToString();
                        SqlInt16 seq = SqlInt16.Parse(dgvRow.Cells[this.HIDDEN_LATEST_SEQ].Value.ToString());

                        //QUE_INFOの最大QUE_SEQを取得する


                        queinfo.KANRI_ID = kanriID;
                        queinfo.SEQ = seq;
                        queinfo.QUE_SEQ = getMaxQueSeq(kanriID);
                        queinfo.FUNCTION_ID = FUNCTION_ID_3100;

                        //区間番号の取得
                        String EdiPassWrodHst = dgvRow.Cells[this.EDI_PASSWORD_HST_INSERT].Value.ToString();
                        String EdiPassWrodUpn = dgvRow.Cells[this.EDI_PASSWORD_UPN_INSERT].Value.ToString();
                        String EdiPassWrodSbn = dgvRow.Cells[this.EDI_PASSWORD_SBN_INSERT].Value.ToString();
                        String UpnRouteNo = dgvRow.Cells[this.UPN_ROUTE_NO_INSERT].Value.ToString();


                        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                        //queinfo.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                        queinfo.CREATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                        queinfo.STATUS_FLAG = 0;

                        //QUE_INFOへの区間番号の設定は、条件にあてはまる最初の１つでINSERTする
                        insertQueINfo(queinfo, EdiPassWrodHst, EdiPassWrodUpn, EdiPassWrodSbn, UpnRouteNo);
                    }
                }
            }
            if (checkFlg == false) {
                //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                MessageBoxUtility.MessageBoxShow("E029", "情報照会するマニフェスト", "マニフェスト一覧");
                return true;
            }
            LogUtility.DebugMethodEnd();
            return false;

        }

        /// <summary>
        /// QUE_INFOの最大情報を取得する
        /// </summary>
        /// <param name="kanriID">管理ID</param>
        /// <returns name="decimal">レコード枝番</returns>
        /// <remarks>Insert用List＞QUE_INFOのDBの順番で参照する</remarks>
        private decimal getMaxQueSeq(string kanriID)
        {
            // Insert用Listから同管理番号で採番されたものがあるか検索
            decimal maxSeq = 0;
            foreach(var entity in this.ToukuteiGenbaSyoukaiList)
            {
                if(entity.KANRI_ID == kanriID)
                {
                    if(maxSeq < entity.QUE_SEQ.Value)
                    {
                        // 同管理番号のQUE_SEQ最大値を保持
                        maxSeq = entity.QUE_SEQ.Value;
                    }
                }
            }

            if(maxSeq != 0)
            {
                // 最大値+1を返却
                maxSeq++;
            }
            else
            {
                // Insert用Listに無ければ、DBから検索
                // SQL文格納StringBuilder
                StringBuilder sql = new StringBuilder();

                sql.Append(" SELECT MAX(QUE_SEQ) AS  QUE_SEQ");
                sql.Append("      FROM ");
                sql.Append("      QUE_INFO ");
                sql.Append("       WHERE  ");
                sql.Append("       KANRI_ID = '");
                sql.Append(kanriID);
                sql.Append("' ");

                this.SearchResult = new DataTable();
                this.SearchResult = GetInfoDaoCls.getdataforstringsql(sql.ToString());

                if(SearchResult != null)
                {

                    DataRow row = SearchResult.Rows[0];
                    if(String.IsNullOrEmpty(row["QUE_SEQ"].ToString()))
                    {
                        // 初期値を返却
                        maxSeq = 1;
                    }
                    else
                    {
                        // 最大値+1を返却
                        maxSeq = decimal.Parse(row["QUE_SEQ"].ToString()) + 1;
                    }
                }
                else
                {
                    // 初期値を返却
                    maxSeq = 1;
                }
            }

            return maxSeq;
        }

        /// <summary>
        /// /区間番号を取得する
        /// </summary>
        private void insertQueINfo(QUE_INFO queinfo,String EdiPassWrodHst, String EdiPassWrodUpn,
            String EdiPassWrodSbn, String UpnRouteNo)
        {
            SqlDecimal QUE_SEQ = queinfo.QUE_SEQ;

            //区間番号0
            if (!String.IsNullOrEmpty(EdiPassWrodHst))
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 0;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
            //区間番号1
            if (!String.IsNullOrEmpty(EdiPassWrodUpn)
                && UPN_ROUTE_NO_1.Equals(UpnRouteNo)
                )
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 1;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
            //区間番号2
            if (!String.IsNullOrEmpty(EdiPassWrodUpn)
                && UPN_ROUTE_NO_2.Equals(UpnRouteNo)
                )
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 2;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
            //区間番号3
            if (!String.IsNullOrEmpty(EdiPassWrodUpn)
               && UPN_ROUTE_NO_3.Equals(UpnRouteNo)
               )
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 3;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
            //区間番号4
            if (!String.IsNullOrEmpty(EdiPassWrodUpn)
               && UPN_ROUTE_NO_4.Equals(UpnRouteNo)
               )
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 4;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
            //区間番号5
            if (!String.IsNullOrEmpty(EdiPassWrodUpn)
               && UPN_ROUTE_NO_5.Equals(UpnRouteNo)
               )
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 5;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
            //区間番号6
            if (!String.IsNullOrEmpty(EdiPassWrodSbn))
            {
                QUE_INFO data = clone(queinfo);
                data.QUE_SEQ = QUE_SEQ;
                data.UPN_ROUTE_NO = 6;
                ToukuteiGenbaSyoukaiList.Add(data);
                QUE_SEQ = QUE_SEQ + 1;

                //イベント作成できたら抜ける
                return;
            }
        }

        #endregion

        #region 特定現場照会処理
        /// <summary>
        /// 特定現場照会
        /// </summary>
        public void specialInfoSearch()
        {
            try
            {
                //特定現場照会処理対象データ抽出
                specialInfoSearchForData();

                //if (SearchResult == null || SearchResult.Rows==null||SearchResult.Rows.Count < 1)
                //{
                //    MessageBoxUtility.MessageBoxShow("W002", "電子マニフェスト特定現場照会");
                //    return;
                //}
                ToukuteiGenbaSyoukaiList.Clear();
                //特定現場照会QUE_INFOリストの取得
                inSertSPInfoSData(SearchResultHST);
                inSertSPInfoSData(SearchResultUPN);
                inSertSPInfoSData(SearchResultSBN);

                if (ToukuteiGenbaSyoukaiList.Count < 1)
                {
                    MessageBoxUtility.MessageBoxShow("W002", "電子マニフェスト特定現場照会");
                    return;
                }

                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                    //SqlDateTime CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    SqlDateTime CREATE_DATE = SqlDateTime.Parse(this.getDBDateTime().ToString());
                    // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                    foreach (QUE_INFO data in ToukuteiGenbaSyoukaiList)
                    {
                        data.QUE_SEQ = getMaxQueSeq(data.KANRI_ID);
                        data.CREATE_DATE = CREATE_DATE;
                        InsertQIDaoCls.Insert(data);
                    }

                    tran.Commit();
                    MessageBoxUtility.MessageBoxShow("I001", "特定現場照会処理");
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("specialInfoSearch", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("specialInfoSearch", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("specialInfoSearch", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
        }


        /// <summary>
        /// 特定現場照会処理データを挿入する
        /// </summary>
        /// <param name="searchResult">データテーブル</param>
        private void inSertSPInfoSData(DataTable searchResult)
        {

            LogUtility.DebugMethodStart();

            foreach (DataRow row in searchResult.Rows)
            {
                // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                T_LAST_SBN_SUSPEND lastsbnsuspend = new T_LAST_SBN_SUSPEND();
                QUE_INFO queinfo = new QUE_INFO();

                string kanriID =  row["KANRI_ID"].ToString();
                SqlInt16 seq =   SqlInt16.Parse(row["LATEST_SEQ"].ToString());
                queinfo.KANRI_ID = kanriID;
                queinfo.SEQ =      seq;

                queinfo.FUNCTION_ID = FUNCTION_ID_3100;

                queinfo.STATUS_FLAG = 0;
                //queinfo.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());

                //区間番号の取得
                if (searchResult.Columns.Contains("EDI_PASSWORD_HST"))
                {
                    if (!string.IsNullOrEmpty(row["EDI_PASSWORD_HST"].ToString()))
                    {
                        queinfo.UPN_ROUTE_NO = 0;
                    }
                }
                else if (searchResult.Columns.Contains("EDI_PASSWORD_UPN"))
                {
                    if (!string.IsNullOrEmpty(row["EDI_PASSWORD_UPN"].ToString()))
                    {
                        if (UPN_ROUTE_NO_1.Equals(row["UPN_ROUTE_NO"].ToString()))
                        {
                            queinfo.UPN_ROUTE_NO = 1;
                        }
                        else if (UPN_ROUTE_NO_2.Equals(row["UPN_ROUTE_NO"].ToString()))
                        {
                            queinfo.UPN_ROUTE_NO = 2;
                        }
                        else if (UPN_ROUTE_NO_3.Equals(row["UPN_ROUTE_NO"].ToString()))
                        {
                            queinfo.UPN_ROUTE_NO = 3;
                        }
                        else if (UPN_ROUTE_NO_4.Equals(row["UPN_ROUTE_NO"].ToString()))
                        {
                            queinfo.UPN_ROUTE_NO = 4;
                        }
                        else if (UPN_ROUTE_NO_5.Equals(row["UPN_ROUTE_NO"].ToString()))
                        {
                            queinfo.UPN_ROUTE_NO = 5;
                        }
                    }
                }
                else if (searchResult.Columns.Contains("EDI_PASSWORD_SBN"))
                {
                    if (!string.IsNullOrEmpty(row["EDI_PASSWORD_SBN"].ToString()))
                    {
                        queinfo.UPN_ROUTE_NO = 6;
                    }
                }

                ToukuteiGenbaSyoukaiList.Add(queinfo);
            }

            LogUtility.DebugMethodEnd();

        }

        /// <summary>
        /// 特定現場照会処理対象データ抽出する
        /// </summary>
        private void specialInfoSearchForData()
        {

            LogUtility.DebugMethodStart();

            #region FROM句

            //SQL文格納StringBuilder
            //排出
            StringBuilder sqlHST = new StringBuilder();

            sqlHST.Append(" SELECT ");

            sqlHST.Append("       DT_MF_TOC.KANRI_ID AS KANRI_ID, ");
            sqlHST.Append("       DT_MF_TOC.LATEST_SEQ AS LATEST_SEQ, ");
            sqlHST.Append("       MS_JWNET_MEMBER_HST.EDI_PASSWORD AS EDI_PASSWORD_HST ");

            sqlHST.Append(" FROM DT_MF_TOC ");

            sqlHST.Append("    INNER JOIN DT_R18 ON ");
            sqlHST.Append("      DT_R18.KANRI_ID = DT_MF_TOC.KANRI_ID ");
            sqlHST.Append("      AND DT_R18.SEQ = DT_MF_TOC.LATEST_SEQ ");

            sqlHST.Append("    INNER JOIN M_DENSHI_JIGYOUJOU AS M_DENSHI_JIGYOUJOU_HST ");
            sqlHST.Append("      ON M_DENSHI_JIGYOUJOU_HST.EDI_MEMBER_ID = DT_R18.HST_SHA_EDI_MEMBER_ID ");
            sqlHST.Append("      AND M_DENSHI_JIGYOUJOU_HST.JIGYOUJOU_NAME = DT_R18.HST_JOU_NAME ");
            sqlHST.Append("      AND (M_DENSHI_JIGYOUJOU_HST.JIGYOUJOU_ADDRESS1 + M_DENSHI_JIGYOUJOU_HST.JIGYOUJOU_ADDRESS2 ");
            sqlHST.Append("       + M_DENSHI_JIGYOUJOU_HST.JIGYOUJOU_ADDRESS3 + M_DENSHI_JIGYOUJOU_HST.JIGYOUJOU_ADDRESS4) ");
            sqlHST.Append("       = (DT_R18.HST_JOU_ADDRESS1 + DT_R18.HST_JOU_ADDRESS2 + DT_R18.HST_JOU_ADDRESS3 + DT_R18.HST_JOU_ADDRESS4)");

            sqlHST.Append("    INNER JOIN M_GENBA ");
            sqlHST.Append("      ON M_GENBA.GYOUSHA_CD = M_DENSHI_JIGYOUJOU_HST.GYOUSHA_CD ");
            sqlHST.Append("      AND M_GENBA.GENBA_CD = M_DENSHI_JIGYOUJOU_HST.GENBA_CD ");
            sqlHST.Append("      AND M_GENBA.DEN_MANI_SHOUKAI_KBN = '1'  ");
            //適用期間
            sqlHST.Append("      AND  ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) ");
            sqlHST.Append("           AND CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) <= TEKIYOU_END) ");
            sqlHST.Append("           OR (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) ");
            sqlHST.Append("           AND TEKIYOU_END IS NULL) OR (TEKIYOU_BEGIN IS NULL ");
            sqlHST.Append("           AND CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) <= TEKIYOU_END) ");
            sqlHST.Append("           OR (TEKIYOU_BEGIN IS NULL AND TEKIYOU_END IS NULL)) ");
            sqlHST.Append("      AND  DELETE_FLG = 0  ");


            sqlHST.Append("    INNER JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_HST");
            sqlHST.Append("      ON MS_JWNET_MEMBER_HST.EDI_MEMBER_ID = M_DENSHI_JIGYOUJOU_HST.EDI_MEMBER_ID ");

            //収運
            StringBuilder sqlUPN = new StringBuilder();

            sqlUPN.Append(" SELECT ");

            sqlUPN.Append("       DT_MF_TOC.KANRI_ID AS KANRI_ID, ");
            sqlUPN.Append("       DT_MF_TOC.LATEST_SEQ AS LATEST_SEQ, ");
            sqlUPN.Append("       DT_R19.UPN_ROUTE_NO AS UPN_ROUTE_NO,  ");
            sqlUPN.Append("       MS_JWNET_MEMBER_UPN.EDI_PASSWORD AS EDI_PASSWORD_UPN  ");

            sqlUPN.Append(" FROM DT_MF_TOC ");

            sqlUPN.Append("    INNER JOIN DT_R18 ON ");
            sqlUPN.Append("      DT_R18.KANRI_ID = DT_MF_TOC.KANRI_ID ");
            sqlUPN.Append("      AND DT_R18.SEQ = DT_MF_TOC.LATEST_SEQ ");

            sqlUPN.Append("    INNER JOIN DT_R19 ON ");
            sqlUPN.Append("      DT_R19.KANRI_ID = DT_R18.KANRI_ID ");
            sqlUPN.Append("      AND DT_R19.SEQ = DT_R18.SEQ ");

            sqlUPN.Append("    INNER JOIN M_DENSHI_JIGYOUJOU AS M_DENSHI_JIGYOUJOU_UPN ");
            sqlUPN.Append("      ON M_DENSHI_JIGYOUJOU_UPN.EDI_MEMBER_ID = DT_R19.UPNSAKI_EDI_MEMBER_ID ");
            sqlUPN.Append("      AND M_DENSHI_JIGYOUJOU_UPN.JIGYOUJOU_NAME = DT_R19.UPNSAKI_JOU_NAME ");
            sqlUPN.Append("      AND (M_DENSHI_JIGYOUJOU_UPN.JIGYOUJOU_ADDRESS1 + M_DENSHI_JIGYOUJOU_UPN.JIGYOUJOU_ADDRESS2 ");
            sqlUPN.Append("       + M_DENSHI_JIGYOUJOU_UPN.JIGYOUJOU_ADDRESS3 + M_DENSHI_JIGYOUJOU_UPN.JIGYOUJOU_ADDRESS4) ");
            sqlUPN.Append("       = (DT_R19.UPNSAKI_JOU_ADDRESS1 + DT_R19.UPNSAKI_JOU_ADDRESS2 + DT_R19.UPNSAKI_JOU_ADDRESS3 + DT_R19.UPNSAKI_JOU_ADDRESS4)");

            sqlUPN.Append("    INNER JOIN M_GENBA ");
            sqlUPN.Append("      ON M_GENBA.GYOUSHA_CD = M_DENSHI_JIGYOUJOU_UPN.GYOUSHA_CD ");
            sqlUPN.Append("      AND M_GENBA.GENBA_CD = M_DENSHI_JIGYOUJOU_UPN.GENBA_CD ");
            sqlUPN.Append("      AND M_GENBA.DEN_MANI_SHOUKAI_KBN = '1'  ");
            //適用期間
            sqlUPN.Append("      AND  ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) ");
            sqlUPN.Append("           AND CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) <= TEKIYOU_END) ");
            sqlUPN.Append("           OR (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) ");
            sqlUPN.Append("           AND TEKIYOU_END IS NULL) OR (TEKIYOU_BEGIN IS NULL ");
            sqlUPN.Append("           AND CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) <= TEKIYOU_END) ");
            sqlUPN.Append("           OR (TEKIYOU_BEGIN IS NULL AND TEKIYOU_END IS NULL)) ");
            sqlUPN.Append("      AND  DELETE_FLG = 0  ");

            sqlUPN.Append("    INNER JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_UPN");
            sqlUPN.Append("    ON  MS_JWNET_MEMBER_UPN.EDI_MEMBER_ID = M_DENSHI_JIGYOUJOU_UPN.EDI_MEMBER_ID ");

            //処分
            StringBuilder sqlSBN = new StringBuilder();

            sqlSBN.Append(" SELECT ");

            sqlSBN.Append("       DT_MF_TOC.KANRI_ID AS KANRI_ID, ");
            sqlSBN.Append("       DT_MF_TOC.LATEST_SEQ AS LATEST_SEQ, ");
            sqlSBN.Append("       MS_JWNET_MEMBER_SBN.EDI_PASSWORD AS EDI_PASSWORD_SBN ");

            sqlSBN.Append(" FROM DT_MF_TOC ");

            sqlSBN.Append("    INNER JOIN DT_R18 ON ");
            sqlSBN.Append("      DT_R18.KANRI_ID = DT_MF_TOC.KANRI_ID ");
            sqlSBN.Append("      AND DT_R18.SEQ = DT_MF_TOC.LATEST_SEQ ");

            sqlSBN.Append("    INNER JOIN M_DENSHI_JIGYOUJOU AS M_DENSHI_JIGYOUJOU_SBN");
            sqlSBN.Append("      ON M_DENSHI_JIGYOUJOU_SBN.EDI_MEMBER_ID = DT_R18.SBN_SHA_MEMBER_ID ");
            sqlSBN.Append("      AND M_DENSHI_JIGYOUJOU_SBN.JIGYOUJOU_NAME = ");
            sqlSBN.Append("     (SELECT DT_R19.UPNSAKI_JOU_NAME FROM DT_R19 ");
            sqlSBN.Append("       WHERE DT_R19.KANRI_ID = DT_R18.KANRI_ID ");
            sqlSBN.Append("         AND DT_R19.SEQ = DT_R18.SEQ ");
            sqlSBN.Append("         AND (M_DENSHI_JIGYOUJOU_SBN.JIGYOUJOU_ADDRESS1 + M_DENSHI_JIGYOUJOU_SBN.JIGYOUJOU_ADDRESS2 ");
            sqlSBN.Append("            + M_DENSHI_JIGYOUJOU_SBN.JIGYOUJOU_ADDRESS3 + M_DENSHI_JIGYOUJOU_SBN.JIGYOUJOU_ADDRESS4) ");
            sqlSBN.Append("            = (DT_R19.UPNSAKI_JOU_ADDRESS1 + DT_R19.UPNSAKI_JOU_ADDRESS2 + DT_R19.UPNSAKI_JOU_ADDRESS3 + DT_R19.UPNSAKI_JOU_ADDRESS4)");
            sqlSBN.Append("         AND DT_R19.UPN_ROUTE_NO = (SELECT MAX(UPN_ROUTE_NO) FROM DT_R19 ");
            sqlSBN.Append("                                WHERE DT_R19.KANRI_ID = DT_R18.KANRI_ID ");
            sqlSBN.Append("                                AND DT_R19.SEQ = DT_R18.SEQ)) ");

            sqlSBN.Append("    INNER JOIN M_GENBA ");
            sqlSBN.Append("      ON M_GENBA.GYOUSHA_CD = M_DENSHI_JIGYOUJOU_SBN.GYOUSHA_CD ");
            sqlSBN.Append("      AND M_GENBA.GENBA_CD = M_DENSHI_JIGYOUJOU_SBN.GENBA_CD ");
            sqlSBN.Append("      AND M_GENBA.DEN_MANI_SHOUKAI_KBN = '1'  ");
            //適用期間
            sqlSBN.Append("      AND  ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) ");
            sqlSBN.Append("           AND CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) <= TEKIYOU_END) ");
            sqlSBN.Append("           OR (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) ");
            sqlSBN.Append("           AND TEKIYOU_END IS NULL) OR (TEKIYOU_BEGIN IS NULL ");
            sqlSBN.Append("           AND CONVERT(DATETIME, CONVERT(NVARCHAR, GETDATE(), 111), 120) <= TEKIYOU_END) ");
            sqlSBN.Append("           OR (TEKIYOU_BEGIN IS NULL AND TEKIYOU_END IS NULL)) ");
            sqlSBN.Append("      AND  DELETE_FLG = 0  ");

            sqlSBN.Append("    INNER JOIN MS_JWNET_MEMBER AS MS_JWNET_MEMBER_SBN ");
            sqlSBN.Append("      ON MS_JWNET_MEMBER_SBN.EDI_MEMBER_ID =  M_DENSHI_JIGYOUJOU_SBN.EDI_MEMBER_ID ");

            #endregion

            #region WHERE句
            StringBuilder sqlWHERE = new StringBuilder();

            sqlWHERE.Append("WHERE ");
            sqlWHERE.Append("    DT_MF_TOC.STATUS_DETAIL <> '1'  ");

            //sqlWHERE.Append("   AND M_GENBA.GYOUSHA_CD IS NOT NULL AND M_GENBA.GENBA_CD IS NOT NULL ");
            //sqlWHERE.Append("   AND M_GENBA.GYOUSHA_CD!='' AND M_GENBA.GENBA_CD!='' ");

            sqlWHERE.Append("   AND DT_R18.UPN_ENDREP_FLAG = '0'  ");
            sqlWHERE.Append("   AND DT_R18.SBN_ENDREP_FLAG = '0'  ");
            sqlWHERE.Append("   AND DT_R18.LAST_SBN_ENDREP_FLAG = '0'  ");




            //マニフェスト情報.最終更新日　

            if (this.form.crdbtn_HidukeJyoken1.Checked == true)
            {
                sqlWHERE.Append("   AND DT_R18.LAST_UPDATE_DATE BETWEEN ");
                sqlWHERE.Append("    CONVERT (nvarchar(8),DATEADD(day, -2, SYSDATETIME())  ,112 ) ");
                sqlWHERE.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
            }
            else if (this.form.crdbtn_HidukeJyoken2.Checked == true)
            {
                sqlWHERE.Append("   AND DT_R18.LAST_UPDATE_DATE BETWEEN ");
                sqlWHERE.Append("    CONVERT (nvarchar(8),DATEADD(day, -6, SYSDATETIME())  ,112 ) ");
                sqlWHERE.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
            }
            else if (this.form.crdbtn_HidukeJyoken3.Checked == true)
            {
                sqlWHERE.Append("  AND  DT_R18.LAST_UPDATE_DATE BETWEEN ");
                sqlWHERE.Append("    CONVERT (nvarchar(8),DATEADD(day, -29, SYSDATETIME())  ,112 ) ");
                sqlWHERE.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
            }
            else if (this.form.crdbtn_HidukeJyoken4.Checked == true)
            {
                sqlWHERE.Append("  AND  DT_R18.LAST_UPDATE_DATE BETWEEN ");
                sqlWHERE.Append("    CONVERT (nvarchar(8),DATEADD(day, -89, SYSDATETIME())  ,112 ) ");
                sqlWHERE.Append("    AND CONVERT (nvarchar(8),  SYSDATETIME() ,112 ) ");
            }
            //マニ番号範囲
            //マニ番号範囲
            if (this.form.crdbtn_ManiJyokenHan.Checked == true)
            {
                if (!String.IsNullOrEmpty(this.form.cntb_ManifestNoFrom.Text))
                {
                    sqlWHERE.Append("    AND DT_R18.MANIFEST_ID >='");
                    sqlWHERE.Append(this.form.cntb_ManifestNoFrom.Text);
                    sqlWHERE.Append("'  ");
                }
                if (!String.IsNullOrEmpty(this.form.cntb_ManifestNoTo.Text))
                {
                    sqlWHERE.Append("    AND DT_R18.MANIFEST_ID <='");
                    sqlWHERE.Append(this.form.cntb_ManifestNoTo.Text);
                    sqlWHERE.Append("'  ");
                }

            }

            //マニ番号指定
            else if (this.form.crdbtn_ManiJyokenSitei.Checked == true)
            {

                sqlWHERE.Append("   AND DT_R18.MANIFEST_ID IN ('");
                sqlWHERE.Append(this.form.cntb_ManifestNo1.Text);
                sqlWHERE.Append("','");
                sqlWHERE.Append(this.form.cntb_ManifestNo2.Text);
                sqlWHERE.Append("','");
                sqlWHERE.Append(this.form.cntb_ManifestNo3.Text);
                sqlWHERE.Append("','");
                sqlWHERE.Append(this.form.cntb_ManifestNo4.Text);
                sqlWHERE.Append("','");
                sqlWHERE.Append(this.form.cntb_ManifestNo5.Text);
                sqlWHERE.Append("') ");

            }
            else {
                //sqlWHERE.Append("  AND 1=2 ");
            }



            //検索条件表示テキスト
            if (!this.form.searchString.Text.Equals(String.Empty))
            {
                sqlWHERE.Append("   AND ");
                sqlWHERE.Append(this.form.searchString.Text);
            }

            sqlWHERE.Append("   ORDER BY ");
            sqlWHERE.Append("   DT_R18.HIKIWATASHI_DATE DESC, ");
            sqlWHERE.Append("   DT_R18.MANIFEST_ID DESC ");

            StringBuilder sqlR19ORDERBY = new StringBuilder();
            sqlR19ORDERBY.Append("   ,DT_R19.UPN_ROUTE_NO ASC ");


            #endregion

            sqlHST.Append(sqlWHERE);

            sqlUPN.Append(sqlWHERE);
            sqlUPN.Append(sqlR19ORDERBY);

            sqlSBN.Append(sqlWHERE);

            this.SearchResultHST = new DataTable();
            this.SearchResultUPN = new DataTable();
            this.SearchResultSBN = new DataTable();
            this.SearchResultHST = GetInfoDaoCls.getdataforstringsql(sqlHST.ToString());
            this.SearchResultUPN = GetInfoDaoCls.getdataforstringsql(sqlUPN.ToString());
            this.SearchResultSBN = GetInfoDaoCls.getdataforstringsql(sqlSBN.ToString());
        }

        #endregion

        /// <summary>
        /// QUE_INFOのオブジェクトコッピする用
        /// </summary>
        public QUE_INFO clone(QUE_INFO queInfo) {
            QUE_INFO data = new QUE_INFO();
            data.KANRI_ID = queInfo.KANRI_ID;
            data.SEQ = queInfo.SEQ;
            data.QUE_SEQ = queInfo.QUE_SEQ;
            data.FUNCTION_ID = queInfo.FUNCTION_ID;
            data.CREATE_DATE = queInfo.CREATE_DATE;
            data.STATUS_FLAG = queInfo.STATUS_FLAG;
            return data;
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            LogUtility.DebugMethodStart();

            try
            {
                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //タイトル
                        this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_SAISHIN_JOHO_SHOKAI);

                        //検索条件
                        this.form.searchString.Text = "";

                        //ヒント
                        this.footer.lb_hint.Text = "";

                        //処理No（ESC）
                        this.footer.txb_process.Text = "";

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        this.SearchResult.Clear();

                        break;
                }

                return true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("ClearScreen", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ClearScreen", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// マニ番　範囲・指定コントロールの制御
        /// </summary>
        public void SetEnabledManiNumberControl()
        {
            var jyoken = this.form.cntb_Jyoken_KBN.Text;

            // 初期化
            this.form.cntb_ManifestNoFrom.Enabled = true;
            this.form.cntb_ManifestNoTo.Enabled = true;

            this.form.cntb_ManifestNo1.Enabled = true;
            this.form.cntb_ManifestNo2.Enabled = true;
            this.form.cntb_ManifestNo3.Enabled = true;
            this.form.cntb_ManifestNo4.Enabled = true;
            this.form.cntb_ManifestNo5.Enabled = true;

            if (MANI_JYOKEN_HANNI == jyoken)
            {
                // 範囲選択時は指定条件コントロールを非活性
                this.form.cntb_ManifestNo1.Enabled = false;
                this.form.cntb_ManifestNo2.Enabled = false;
                this.form.cntb_ManifestNo3.Enabled = false;
                this.form.cntb_ManifestNo4.Enabled = false;
                this.form.cntb_ManifestNo5.Enabled = false;
            }
            else if (MANI_JYOKEN_SHITEI == jyoken)
            {
                // 指定選択時は範囲条件コントロールを非活性
                this.form.cntb_ManifestNoFrom.Enabled = false;
                this.form.cntb_ManifestNoTo.Enabled = false;
            }
        }

        #region 画面イベント処理(FW側処理)
        ///// <summary>
        ///// 処理No フォーカス移動
        ///// </summary>
        //public void SetFocusTxbProcess()
        //{
        //    LogUtility.DebugMethodStart();

        //    try
        //    {
        //        this.footer.txb_process.Focus();
        //        this.footer.lb_hint.Text = "処理Noを入力してください";
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Debug(ex);

        //        if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
        //        {

        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    LogUtility.DebugMethodEnd();
        //}

        ///// <summary>
        ///// 処理No ボタン選択
        ///// </summary>
        //public void SelectButton()
        //{
        //    LogUtility.DebugMethodStart();
        //    try
        //    {
        //        switch (this.footer.txb_process.Text)
        //        {
        //            case "1"://【1】パターン一覧
        //                this.footer.bt_process1.PerformClick();
        //                break;

        //            case "2"://【2】検索条件設定
        //                this.footer.bt_process2.PerformClick();
        //                break;

        //            case "3"://【2】検索条件設定
        //                this.footer.bt_process3.PerformClick();
        //                break;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Debug(ex);

        //        if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
        //        {

        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion

        /// 20141023 Houkakou 「最新情報照会」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntb_ManifestNoTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cntb_ManifestNoFrom;
            var ToTextBox = this.form.cntb_ManifestNoTo;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141023 Houkakou 「最新情報照会」のダブルクリックを追加する　end

        #region 照会中チェック関連
        /// <summary>
        /// 本日の依頼データを取得する
        /// </summary>
        public int GetRequestDataInDay()
        {
            try
            {
                string query = "";
                query = string.Format("SELECT * FROM QUE_INFO WHERE FUNCTION_ID = '3100' AND SYSDATETIME() < DATEADD( HH, {0}, QUE_INFO.CREATE_DATE)", this.EXECUTING_DECISION_HOUR);
                DataTable dt = InsertQIDaoCls.GetRequestDataInDay(query);

                return dt.Rows.Count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetRequestDataInDay", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetRequestDataInDay", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                return -1;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCheckedRow()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        if (!list.Contains(dgvRow.Cells[this.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                        {
                            list.Add(dgvRow.Cells[this.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                        }
                    }

                }
            }

            return list.Count;
        }
        #endregion

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

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntb_KoufuDateTo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cntb_KoufuDateFrom;
            var ToTextBox = this.form.cntb_KoufuDateTo;
            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.cntb_KoufuDateFrom.BackColor = Constans.NOMAL_COLOR;
            this.form.cntb_KoufuDateTo.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.cntb_KoufuDateFrom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.cntb_KoufuDateTo.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.form.cntb_KoufuDateFrom.Text);
            DateTime date_to = DateTime.Parse(this.form.cntb_KoufuDateTo.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.cntb_KoufuDateFrom.IsInputErrorOccured = true;
                this.form.cntb_KoufuDateTo.IsInputErrorOccured = true;
                this.form.cntb_KoufuDateFrom.BackColor = Constans.ERROR_COLOR;
                this.form.cntb_KoufuDateTo.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.form.cntb_KoufuDate_KBN.Text == "1")
                {
                    string[] errorMsg = { "引渡日From", "引渡日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.cntb_KoufuDate_KBN.Text == "2")
                {
                    string[] errorMsg = { "運搬終了日From", "運搬終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.cntb_KoufuDate_KBN.Text == "3")
                {
                    string[] errorMsg = { "処分終了日From", "処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.form.cntb_KoufuDate_KBN.Text == "4")
                {
                    string[] errorMsg = { "最終処分終了日From", "最終処分終了日To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.form.cntb_KoufuDateFrom.Focus();
                return true;
            }
            return false;
        }
        #endregion
    }
}
