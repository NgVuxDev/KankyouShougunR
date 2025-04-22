using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DataGridViewCheckBoxColumnHeeader;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.ElectronicManifest.SousinHoryuSaisyuSyobunhoukoku
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
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.SousinHoryuSaisyuSyobunhoukoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// DAO
        /// </summary>
        private SearchTLSSDaoCls SearchTLSSDao;

        /// <summary>
        /// 最終処分保留更新Dao
        /// </summary>
        private UpdateTLSSDaoCls UpdateTLSSDaoCls;

        /// <summary>
        /// キュー情報更新Dao
        /// </summary>
        private UpdateQIDaoCls UpdateQIDaoCls;

        /// <summary>
        /// マニフェスト目次情報更新Dao
        /// </summary>
        private UpdateDMTDaoCls UpdateDMTDaoCls;

        /// <summary>
        /// DTO
        /// </summary>
        private SearchTLSSDtoCls SearchTLSSDto;

        /// <summary>
        /// Form
        /// </summary>
        private UIHeader header;
        private UIForm form;
        private BusinessBaseForm footer;

        /// <summary>
        /// List<T_LAST_SBN_SUSPEND> mlastsbnsuspendMsList
        /// </summary>
        private List<T_LAST_SBN_SUSPEND> mlastsbnsuspendMsList;

        /// <summary>
        /// List<QUE_INFO> mqueinfoMsList
        /// </summary>
        private List<QUE_INFO> mqueinfoMsList;

        /// <summary>
        /// List<DT_MF_TOC> mdtmftocMsList
        /// </summary>
        private List<DT_MF_TOC> mdtmftocMsList;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        #endregion

        #region プロパティ

        private Control[] allControl;

        /// <summary>
        /// 検索結果(システム設定)
        /// </summary>
        public DataTable Search_MSI { get; set; }

        /// <summary>
        /// SELECT句
        /// </summary>
        internal string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        internal string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        internal string joinQuery { get; set; }

        /// <summary>
        /// 検索結果(表示用)
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(データ操作用)
        /// </summary>
        public DataTable SearchResultForUpdateData { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// 管理ID
        /// </summary>
        internal readonly string HIDDEN_KANRI_ID = "HIDDEN_KANRI_ID";

        /// <summary>
        /// 行枝番
        /// </summary>
        internal readonly string HIDDEN_QUE_SEQ = "HIDDEN_QUE_SEQ";

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        internal readonly string HIDDEN_TIME_STAMP = "HIDDEN_TIME_STAMP";

        /// <summary>
        /// 更新日時（QUE_INFO）
        /// </summary>
        internal readonly string HIDDEN_QI_UPDATE_TS = "HIDDEN_QI_UPDATE_TS";

        /// <summary>
        /// 更新日時（DT_MF_TOC）
        /// </summary>
        internal readonly string HIDDEN_DMT_UPDATE_TS = "HIDDEN_DMT_UPDATE_TS";

        /// <summary>
        /// 機能ID
        /// </summary>
        internal readonly string HIDDEN_FUNCTION_ID = "HIDDEN_FUNCTION_ID";

        /// <summary>
        /// 廃棄区分
        /// </summary>
        internal readonly string HIDDEN_NEXT_HAIKI_KBN_CD = "NEXT_HAIKI_KBN_CD";

        /// <summary>非表示項目配列</summary>
        internal string[] DisableColumnNames { get; private set; }
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.SearchTLSSDto = new SearchTLSSDtoCls();
            this.SearchTLSSDao = DaoInitUtility.GetComponent<SearchTLSSDaoCls>();
            this.UpdateTLSSDaoCls = DaoInitUtility.GetComponent<UpdateTLSSDaoCls>();
            this.UpdateQIDaoCls = DaoInitUtility.GetComponent<UpdateQIDaoCls>();
            this.UpdateDMTDaoCls = DaoInitUtility.GetComponent<UpdateDMTDaoCls>();
            this.DisableColumnNames = new string[] {this.HIDDEN_SYSTEM_ID, this.HIDDEN_KANRI_ID, this.HIDDEN_QUE_SEQ,
                    this.HIDDEN_QI_UPDATE_TS, this.HIDDEN_DMT_UPDATE_TS, this.HIDDEN_FUNCTION_ID, 
                    this.HIDDEN_TIME_STAMP, this.HIDDEN_NEXT_HAIKI_KBN_CD};

            this.SearchResult = new DataTable();

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
            this.header = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //JWNET送信ボタン(F9)イベント生成
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //並び替えボタン(F10)イベント生成
            parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.customDataGridView1_ColumnHeaderMouseClick);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;                                //行の追加オプション(false)
                this.form.customDataGridView1.ColumnHeadersVisible = true;

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
                        this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_SOUSHIN_HORYU_SAISHU_SYOBUN);

                        //検索条件
                        this.form.searchString.Text = "";

                        //ヒント
                        this.footer.lb_hint.Text = "";

                        //処理No（ESC）
                        this.footer.txb_process.Text = "";
                        this.footer.txb_process.Tag = "処理Noを入力してください";

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

                        break;

                    case "ClsSearchCondition"://検索条件をクリア
                        this.SearchResult.Clear();

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

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
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            int ret = 0;
            try
            {
                Get_SearchTLSS("false");
                Set_SearchTLSS();

                // 検索結果が0件の場合はメッセージ表示
                if (this.form.isLoaded == true && this.SearchResult.Rows.Count == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E076");
                }

                ret = this.SearchResult.Rows.Count;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                ret = -1;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                ret = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                ret = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// データ取得
        /// </summary>
        public void Get_SearchTLSS(String DELETE_FLG)
        {
            LogUtility.DebugMethodStart(DELETE_FLG);

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句

            sql.AppendFormat(" SELECT SUMMARY.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID); // システムID
            sql.AppendFormat("      , SUMMARY.NEXT_HAIKI_KBN_CD AS {0} ", this.HIDDEN_NEXT_HAIKI_KBN_CD); // システムIDに紐付く廃棄区分
            sql.AppendFormat("      , SUMMARY.KANRI_ID AS {0} ", this.HIDDEN_KANRI_ID); // 管理番号
            sql.AppendFormat("      , SUMMARY.QUE_SEQ AS {0} ", this.HIDDEN_QUE_SEQ); // レコード枝番
            sql.AppendFormat("      , CAST(SUMMARY.TIME_STAMP AS int) AS {0} ", this.HIDDEN_TIME_STAMP); //　タイムスタンプ（最終処分保留）
            sql.AppendFormat("      , SUMMARY.QI_UPDATE_TS AS {0} ", this.HIDDEN_QI_UPDATE_TS); // タイムスタンプ（キュー情報）
            sql.AppendFormat("      , SUMMARY.DMT_UPDATE_TS AS {0} ", this.HIDDEN_DMT_UPDATE_TS); // タイムスタンプ（マニフェスト目次情報）
            sql.AppendFormat("      , SUMMARY.FUNCTION_ID AS {0} ", this.HIDDEN_FUNCTION_ID); // 操作

            String MOutputPatternSelect = this.selectQuery;
            if (String.IsNullOrEmpty(MOutputPatternSelect))
            {
            }
            else
            {
                sql.Append(", ");
                sql.Append(MOutputPatternSelect);
            }

            #endregion

            #region FROM句

            sql.Append(" FROM ( ");

            // 1次：電子、2次：紙
            sql.Append("     SELECT DISTINCT");
            sql.Append("            T_LAST_SBN_SUSPEND.SYSTEM_ID ");
            sql.Append("          , T_MANIFEST_RELATION.NEXT_HAIKI_KBN_CD ");
            sql.Append("          , QUE_INFO.KANRI_ID ");
            sql.Append("          , QUE_INFO.QUE_SEQ ");
            sql.Append("          , T_LAST_SBN_SUSPEND.TIME_STAMP ");
            sql.Append("          , QUE_INFO.UPDATE_TS AS QI_UPDATE_TS ");
            sql.Append("          , DT_MF_TOC.UPDATE_TS AS DMT_UPDATE_TS ");
            sql.Append("          , CASE  ");
            sql.Append("                WHEN QUE_INFO.FUNCTION_ID = '2000' THEN '報告' ");
            sql.Append("                WHEN QUE_INFO.FUNCTION_ID = '2100' THEN '取消' ");
            sql.Append("            ELSE '' END AS FUNCTION_ID  ");
            sql.Append("          , T_MANIFEST_ENTRY.MANIFEST_ID AS MANIFEST_ID");
            sql.Append("          , T_MANIFEST_ENTRY.KOUFU_DATE AS HIKIWATASHI_DATE");
            sql.Append("          , T_MANIFEST_ENTRY.HST_GYOUSHA_NAME AS HST_SHA_NAME");
            sql.Append("          , T_MANIFEST_ENTRY.HST_GENBA_NAME AS HST_JOU_NAME");
            sql.Append("          , T_MANIFEST_DETAIL.HAIKI_SUU AS HAIKI_SUU");
            sql.Append("          , M_UNIT.UNIT_NAME AS UNIT_NAME");
            sql.Append("          , M_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS HAIKI_SHURUI");
            sql.Append("       FROM ");
            sql.Append("            T_LAST_SBN_SUSPEND WITH(NOLOCK) ");
            sql.Append("       INNER JOIN ");
            sql.Append("                (SELECT ");
            sql.Append("                    T_MANIFEST_ENTRY.*,");
            sql.Append("                    T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID");
            sql.Append("                 FROM T_MANIFEST_ENTRY WITH(NOLOCK)");
            sql.Append("                 LEFT JOIN T_MANIFEST_DETAIL WITH(NOLOCK)");
            sql.Append("                        ON T_MANIFEST_DETAIL.SYSTEM_ID = T_MANIFEST_ENTRY.SYSTEM_ID");
            sql.Append("                       AND T_MANIFEST_DETAIL.SEQ = T_MANIFEST_ENTRY.SEQ) T_MANIFEST_ENTRY");
            sql.Append("            ON  T_LAST_SBN_SUSPEND.SYSTEM_ID = T_MANIFEST_ENTRY.DETAIL_SYSTEM_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  T_MANIFEST_DETAIL WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_ENTRY.SYSTEM_ID = T_MANIFEST_DETAIL.SYSTEM_ID");
            sql.Append("              AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ");
            sql.Append("       INNER JOIN ");
            sql.Append("                  T_MANIFEST_RELATION WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID = T_MANIFEST_RELATION.NEXT_SYSTEM_ID");
            sql.Append("              AND T_MANIFEST_RELATION.NEXT_HAIKI_KBN_CD != 4");
            sql.Append("              AND T_MANIFEST_RELATION.FIRST_HAIKI_KBN_CD = 4");
            sql.Append("       INNER JOIN ");
            sql.Append("                  (");
            sql.Append("                      SELECT");
            sql.Append("                          DT_R18_EX.KANRI_ID,");
            sql.Append("                          CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL");
            sql.Append("                              THEN DT_R18_MIX.DETAIL_SYSTEM_ID");
            sql.Append("                              ELSE DT_R18_EX.SYSTEM_ID");
            sql.Append("                          END");
            sql.Append("                          AS SYSTEM_ID,");
            sql.Append("                          DT_R18_EX.DELETE_FLG");
            sql.Append("                      FROM");
            sql.Append("                          DT_R18_EX WITH(NOLOCK) ");
            sql.Append("                          LEFT JOIN DT_R18_MIX WITH(NOLOCK) ");
            sql.Append("                              ON DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID");
            sql.Append("                              AND DT_R18_MIX.DELETE_FLG = 0");
            sql.Append("                      WHERE");
            sql.Append("                          DT_R18_EX.DELETE_FLG = 0");
            sql.Append("                  ) AS DT_R18_EX");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_RELATION.FIRST_SYSTEM_ID = DT_R18_EX.SYSTEM_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  DT_MF_TOC WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_R18_EX.KANRI_ID = DT_MF_TOC.KANRI_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  QUE_INFO WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_MF_TOC.KANRI_ID = QUE_INFO.KANRI_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  M_HAIKI_SHURUI WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_ENTRY.HAIKI_KBN_CD = M_HAIKI_SHURUI.HAIKI_KBN_CD");
            sql.Append("              AND T_MANIFEST_DETAIL.HAIKI_SHURUI_CD = M_HAIKI_SHURUI.HAIKI_SHURUI_CD");
            sql.Append("        LEFT JOIN ");
            sql.Append("                  M_UNIT WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_DETAIL.HAIKI_UNIT_CD = M_UNIT.UNIT_CD");
            sql.Append("              AND M_UNIT.DENSHI_USE_KBN = 1");
            sql.Append("      WHERE ");
            sql.Append("            T_LAST_SBN_SUSPEND.DELETE_FLG = 0 ");
            sql.Append("        AND T_MANIFEST_ENTRY.DELETE_FLG = 0 ");
            sql.Append("        AND T_MANIFEST_RELATION.DELETE_FLG = 0 ");
            sql.Append("        AND DT_R18_EX.DELETE_FLG = 0 ");
            sql.Append("        AND QUE_INFO.STATUS_FLAG = 7 ");
            sql.Append("        AND QUE_INFO.FUNCTION_ID IN (2000,2100) ");

            sql.Append("     UNION ALL ");
            // 1次：電子、2次：電子
            sql.Append("     SELECT DISTINCT");
            sql.Append("            T_LAST_SBN_SUSPEND.SYSTEM_ID ");
            sql.Append("          , T_MANIFEST_RELATION.NEXT_HAIKI_KBN_CD ");
            sql.Append("          , QUE_INFO.KANRI_ID ");
            sql.Append("          , QUE_INFO.QUE_SEQ ");
            sql.Append("          , T_LAST_SBN_SUSPEND.TIME_STAMP ");
            sql.Append("          , QUE_INFO.UPDATE_TS AS QI_UPDATE_TS ");
            sql.Append("          , DT_MF_TOC_FIRST.UPDATE_TS AS DMT_UPDATE_TS ");
            sql.Append("          , CASE  ");
            sql.Append("                WHEN QUE_INFO.FUNCTION_ID = '2000' THEN '報告' ");
            sql.Append("                WHEN QUE_INFO.FUNCTION_ID = '2100' THEN '取消' ");
            sql.Append("            ELSE '' END AS FUNCTION_ID  ");
            sql.Append("          , DT_R18.MANIFEST_ID AS MANIFEST_ID");
            sql.Append("          , DT_R18.HIKIWATASHI_DATE AS HIKIWATASHI_DATE");
            sql.Append("          , DT_R18.HST_SHA_NAME AS HST_SHA_NAME");
            sql.Append("          , DT_R18.HST_JOU_NAME AS HST_JOU_NAME");
            sql.Append("          , DT_R18.HAIKI_SUU AS HAIKI_SUU");
            sql.Append("          , M_UNIT.UNIT_NAME AS UNIT_NAME");
            sql.Append("          , M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS HAIKI_SHURUI");
            sql.Append("       FROM ");
            sql.Append("            T_LAST_SBN_SUSPEND WITH(NOLOCK) ");
            sql.Append("       INNER JOIN ");
            sql.Append("                  T_MANIFEST_RELATION WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  T_LAST_SBN_SUSPEND.SYSTEM_ID = T_MANIFEST_RELATION.NEXT_SYSTEM_ID");
            sql.Append("              AND T_MANIFEST_RELATION.NEXT_HAIKI_KBN_CD = 4");
            sql.Append("              AND T_MANIFEST_RELATION.FIRST_HAIKI_KBN_CD = 4");
            sql.Append("       INNER JOIN ");
            sql.Append("                  DT_R18_EX AS DT_R18_EX_NEXT WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_RELATION.NEXT_SYSTEM_ID = DT_R18_EX_NEXT.SYSTEM_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  DT_MF_TOC AS DT_MF_TOC_NEXT WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_R18_EX_NEXT.KANRI_ID = DT_MF_TOC_NEXT.KANRI_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  DT_R18 WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_R18_EX_NEXT.KANRI_ID = DT_R18.KANRI_ID");
            sql.Append("              AND DT_MF_TOC_NEXT.LATEST_SEQ = DT_R18.SEQ");
            sql.Append("       INNER JOIN ");
            sql.Append("                  M_DENSHI_HAIKI_SHURUI WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_R18.HAIKI_DAI_CODE + DT_R18.HAIKI_CHU_CODE + DT_R18.HAIKI_SHO_CODE = M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD");
            sql.Append("        LEFT JOIN ");
            sql.Append("                  M_UNIT WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_R18.HAIKI_UNIT_CODE = M_UNIT.UNIT_CD");
            sql.Append("              AND M_UNIT.DENSHI_USE_KBN = 1");
            sql.Append("       INNER JOIN ");
            sql.Append("                  (");
            sql.Append("                      SELECT");
            sql.Append("                          DT_R18_EX.KANRI_ID,");
            sql.Append("                          CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL");
            sql.Append("                              THEN DT_R18_MIX.DETAIL_SYSTEM_ID");
            sql.Append("                              ELSE DT_R18_EX.SYSTEM_ID");
            sql.Append("                          END");
            sql.Append("                          AS SYSTEM_ID,");
            sql.Append("                          DT_R18_EX.DELETE_FLG");
            sql.Append("                      FROM");
            sql.Append("                          DT_R18_EX WITH(NOLOCK) ");
            sql.Append("                          LEFT JOIN DT_R18_MIX WITH(NOLOCK) ");
            sql.Append("                              ON DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID");
            sql.Append("                              AND DT_R18_MIX.DELETE_FLG = 0");
            sql.Append("                      WHERE");
            sql.Append("                          DT_R18_EX.DELETE_FLG = 0");
            sql.Append("                  ) AS DT_R18_EX_FIRST ");
            sql.Append("               ON ");
            sql.Append("                  T_MANIFEST_RELATION.FIRST_SYSTEM_ID = DT_R18_EX_FIRST.SYSTEM_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  DT_MF_TOC AS DT_MF_TOC_FIRST WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_R18_EX_FIRST.KANRI_ID = DT_MF_TOC_FIRST.KANRI_ID");
            sql.Append("       INNER JOIN ");
            sql.Append("                  QUE_INFO WITH(NOLOCK) ");
            sql.Append("               ON ");
            sql.Append("                  DT_MF_TOC_FIRST.KANRI_ID = QUE_INFO.KANRI_ID");
            sql.Append("      WHERE ");
            sql.Append("            T_LAST_SBN_SUSPEND.DELETE_FLG = 0 ");
            sql.Append("        AND T_MANIFEST_RELATION.DELETE_FLG = 0 ");
            sql.Append("        AND DT_R18_EX_FIRST.DELETE_FLG = 0 ");
            sql.Append("        AND DT_R18_EX_NEXT.DELETE_FLG = 0 ");
            sql.Append("        AND QUE_INFO.STATUS_FLAG = 7 ");
            sql.Append("        AND QUE_INFO.FUNCTION_ID IN (2000,2100) ");

            sql.Append(" )   AS SUMMARY ");

            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句


            #endregion

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");

            this.SearchResultForUpdateData = SearchTLSSDao.GetDateForStringSql(this.createSql);
            this.SearchResult = this.createDataTableForDispData(this.SearchResultForUpdateData);

            LogUtility.DebugMethodEnd(DELETE_FLG);

        }

        /// <summary>
        /// 検索結果から表示用DataTableの作成
        /// </summary>
        /// <param name="dt">検索結果DataTable</param>
        /// <returns>表示用</returns>
        private DataTable createDataTableForDispData(DataTable dt)
        {
            DataTable returnDt = new DataTable();
            DataTable tempDt = dt.Copy();

            if (tempDt == null || tempDt.Rows.Count < 1)
            {
                return returnDt;
            }

            // IchiranSuperFormのhiddenColumnsとは別にグルーピング用に不要な表示カラムを削除する
            string[] hiddenColmuns = new string[] {this.HIDDEN_KANRI_ID, this.HIDDEN_QUE_SEQ, this.HIDDEN_QI_UPDATE_TS,
                                            this.HIDDEN_DMT_UPDATE_TS, this.HIDDEN_FUNCTION_ID, this.HIDDEN_TIME_STAMP};

            // グルーピングに不要な非表示カラムを削除
            foreach (var hiddenCol in hiddenColmuns)
            {
                if (tempDt.Columns.Contains(hiddenCol))
                {
                    tempDt.Columns.Remove(hiddenCol);
                }
            }

            // 表示カラム一覧作成(パターンを使用しているため)
            List<string> colNameList = new List<string>();
            foreach (DataColumn dispCol in tempDt.Columns)
            {
                colNameList.Add(dispCol.ColumnName);
            }


            // 重複行を削除
            returnDt = tempDt.AsDataView().ToTable(true, colNameList.ToArray());

            return returnDt;
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        public void Set_SearchTLSS()
        {
            LogUtility.DebugMethodStart();

            //DataGridの初期化
            this.form.customDataGridView1.DataSource = null;
            this.form.customDataGridView1.Columns.Clear();

            if (this.form.isLoaded == false)
            {
                this.SearchResult.Clear();
            }

            //一覧の項目を非表示
            this.form.ShowData();

            //選択チェックボックス作成
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.Width = 50;
            column.DefaultCellStyle.Tag = "処理対象とする場合はチェックしてください";
            DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
            newheader.ToolTipText = "処理対象とする場合はチェックしてください";
            newheader.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                    datagridviewcheckboxHeaderEventHander(ch_OnCheckBoxClicked);
            column.HeaderCell = newheader;

            // パターンが登録されていない場合は表示しない
            if (this.selectQuery != null)
            {
                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(0, column);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(column);
                }

                if (this.SearchResult.Rows.Count > 0)
                {
                    this.form.customDataGridView1.Focus();
                    this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[0].Cells[0];
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処理No ボタン選択
        /// </summary>
        public void SelectButton()
        {
            LogUtility.DebugMethodStart();
            try
            {
                switch (this.footer.txb_process.Text)
                {
                    case "1"://【1】パターン一覧
                        this.footer.bt_process1.PerformClick();
                        break;

                    case "2"://【2】検索条件設定
                        this.footer.bt_process2.PerformClick();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 処理No フォーカス移動
        /// </summary>
        public void SetFocusTxbProcess()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.footer.txb_process.Focus();
                this.footer.lb_hint.Text = "処理Noを入力してください";
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {

                }
                else
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        public void delete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上有った場合判断
                bool updataflag = false;
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    if (dgvRow.Cells[0].Value != null)
                    {
                        if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                        {
                            updataflag = true;
                        }
                    }
                }
                if (updataflag)
                {
                    //チェックされた最終処分保留を更新するかを確認する。
                    DialogResult result = MessageBoxUtility.MessageBoxShow("C046", "画面の内容で削除処理を");
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                }
                else
                {
                    //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                    MessageBoxUtility.MessageBoxShow("E029", "削除する最終処分終了報告", "マニフェスト一覧");
                    return;

                }

                //DataGridViewのデータを取得する。
                DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
                if (dbkoshin != null)
                {
                    if (dbkoshin.Rows.Count > 0)
                    {
                        //データベースに更新する。
                        bool catchErr = false;
                        bool ret = this.Sakuzyo(out catchErr);
                        if (catchErr) { return; }
                        if (ret)
                        {
                            MessageBoxUtility.MessageBoxShow("I001", "削除処理");
                        }
                        else
                        {
                            MessageBoxUtility.MessageBoxShow("E080");
                        }
                        //更新後、DataGridViewを更新する。
                        this.Search();
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("delete", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("delete", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("delete", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// 削除
        /// </summary>
        /// /// <returns></returns>
        private bool Sakuzyo(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            bool ret = false;
            //明細部データ取得
            //キュー情報の排他制御結果がＮＧの場合はfalseを返す
            if (!GetMeisaiIchiranData_delete())
            {
                return ret;
            }

            try
            {
                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    //T_LAST_SBN_SUSPEND
                    if (mlastsbnsuspendMsList != null && mlastsbnsuspendMsList.Count() > 0)
                    {
                        foreach (T_LAST_SBN_SUSPEND lastsbnsuspend in mlastsbnsuspendMsList)
                        {
                            int CntMopkUpd = UpdateTLSSDaoCls.Delete(lastsbnsuspend);
                        }
                    }
                    //QUE_INFO
                    if (mqueinfoMsList != null && mqueinfoMsList.Count() > 0)
                    {
                        foreach (QUE_INFO queinfo in mqueinfoMsList)
                        {
                            int CntMopkUpd = UpdateQIDaoCls.Update(queinfo);
                        }
                    }

                    tran.Commit();
                }
                ret = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Sakuzyo", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                catchErr = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Sakuzyo", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Sakuzyo", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 明細部データ取得（削除）
        /// </summary>
        /// /// <returns></returns>
        private Boolean GetMeisaiIchiranData_delete()
        {
            LogUtility.DebugMethodStart();

            Boolean blnSuccess = true;

            DataTable dbkoshin = this.SearchResultForUpdateData;
            List<T_LAST_SBN_SUSPEND> lastsbnsuspendMsList = new List<T_LAST_SBN_SUSPEND>();
            List<QUE_INFO> queinfoMsList = new List<QUE_INFO>();

            String UsrName = System.Environment.UserName;
            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
            DateTime datatime = this.footer.sysDate;
            string pcname = System.Environment.MachineName;

            //int index = 0;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                if (dgvRow.Cells[0].Value != null)
                {
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {

                        // チェックがついているレコードに対応した情報を取得
                        var updateRecorde = dbkoshin.AsEnumerable().Where(w => w[HIDDEN_SYSTEM_ID].ToString().Equals(dgvRow.Cells[HIDDEN_SYSTEM_ID].Value.ToString())
                                                                            && w[HIDDEN_NEXT_HAIKI_KBN_CD].ToString().Equals(dgvRow.Cells[HIDDEN_NEXT_HAIKI_KBN_CD].Value.ToString()));

                        if (updateRecorde == null)
                        {
                            continue;
                        }

                        foreach (DataRow updateRow in updateRecorde)
                        {
                            T_LAST_SBN_SUSPEND lastsbnsuspend = new T_LAST_SBN_SUSPEND();
                            QUE_INFO queinfo = new QUE_INFO();

                            //T_LAST_SBN_SUSPEND
                            lastsbnsuspend.SYSTEM_ID = SqlInt64.Parse(updateRow[this.HIDDEN_SYSTEM_ID].ToString());

                            lastsbnsuspend.TIME_STAMP = ConvertStrByte.In32ToByteArray((int)updateRow[this.HIDDEN_TIME_STAMP]);
                            lastsbnsuspend.UPDATE_DATE = SqlDateTime.Parse(datatime.ToString());
                            lastsbnsuspend.UPDATE_USER = UsrName;
                            lastsbnsuspend.UPDATE_PC = pcname;
                            // 重複更新を避けるためチェック
                            var lastSbnSuspendCheckData = lastsbnsuspendMsList.Where(w => (bool)(lastsbnsuspend.SYSTEM_ID == w.SYSTEM_ID));
                            if (lastSbnSuspendCheckData == null || lastSbnSuspendCheckData.Count() < 1)
                            {
                                lastsbnsuspendMsList.Add(lastsbnsuspend);
                            }

                            //QUE_INFO
                            //排他制御
                            if (updateRow[this.HIDDEN_QI_UPDATE_TS].ToString().Equals(datatime.ToString()))
                            {
                                blnSuccess = false;
                                break;
                            }
                            else
                            {
                                queinfo.KANRI_ID = updateRow[this.HIDDEN_KANRI_ID].ToString();
                                queinfo.QUE_SEQ = SqlInt16.Parse(updateRow[this.HIDDEN_QUE_SEQ].ToString());

                                queinfo.STATUS_FLAG = 6;
                                queinfo.TRF_STATUS = 0;
                                queinfo.UPDATE_TS = (DateTime)updateRow[this.HIDDEN_QI_UPDATE_TS];

                                // 重複更新を避けるためチェック
                                var queInfoCheckData = queinfoMsList.Where(w => updateRow[this.HIDDEN_KANRI_ID].ToString().Equals(w.KANRI_ID)
                                                                                && updateRow[this.HIDDEN_QUE_SEQ].ToString().Equals(w.QUE_SEQ.ToString()));
                                if (queInfoCheckData == null || queInfoCheckData.Count() < 1)
                                {
                                    queinfoMsList.Add(queinfo);
                                }
                            }
                        }
                    }
                }
            }
            mlastsbnsuspendMsList = lastsbnsuspendMsList;
            mqueinfoMsList = queinfoMsList;
            LogUtility.DebugMethodEnd();

            return blnSuccess;
        }

        public void update()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上有った場合判断
                bool updataflag = false;
                foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
                {
                    if (dgvRow.Cells[0].Value != null)
                    {
                        if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                        {
                            updataflag = true;
                        }
                    }
                }
                if (updataflag)
                {
                    //チェックされた最終処分保留を更新するかを確認する。
                    DialogResult result = MessageBoxUtility.MessageBoxShow("C046", "画面の内容で登録処理を");
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                }
                else
                {
                    //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                    MessageBoxUtility.MessageBoxShow("E029", "JWNET送信する最終処分終了報告", "マニフェスト一覧");
                    return;

                }

                //DataGridViewのデータを取得する。
                DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
                if (dbkoshin != null)
                {
                    if (dbkoshin.Rows.Count > 0)
                    {
                        //データベースに更新する。
                        bool catchErr = false;
                        bool ret = this.Touroku(out catchErr);
                        if (catchErr) { return; }
                        if (ret)
                        {
                            MessageBoxUtility.MessageBoxShow("I001", "登録処理");
                        }
                        else
                        {
                            MessageBoxUtility.MessageBoxShow("E080");
                        }
                        //更新後、DataGridViewを更新する。
                        this.Search();
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("update", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("update", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("update", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// /// <returns></returns>
        private bool Touroku(out bool catchErr)
        {
            LogUtility.DebugMethodStart();
            catchErr = false;
            bool ret = false;
            //明細部データ取得
            //キュー情報とマニフェスト目次情報の排他制御結果がＮＧの場合はfalseを返す
            if (!GetMeisaiIchiranData())
            {
                return ret;
            }

            try
            {
                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    //T_LAST_SBN_SUSPEND
                    if (mlastsbnsuspendMsList != null && mlastsbnsuspendMsList.Count() > 0)
                    {
                        foreach (T_LAST_SBN_SUSPEND lastsbnsuspend in mlastsbnsuspendMsList)
                        {
                            int CntMopkUpd = UpdateTLSSDaoCls.Delete(lastsbnsuspend);
                        }
                    }
                    //QUE_INFO
                    if (mqueinfoMsList != null && mqueinfoMsList.Count() > 0)
                    {
                        foreach (QUE_INFO queinfo in mqueinfoMsList)
                        {
                            int CntMopkUpd = UpdateQIDaoCls.Update(queinfo);
                        }
                    }

                    //DT_MF_TOC
                    if (mdtmftocMsList != null && mdtmftocMsList.Count() > 0)
                    {
                        foreach (DT_MF_TOC dtmftoc in mdtmftocMsList)
                        {
                            int CntMopkUpd = UpdateDMTDaoCls.Update(dtmftoc);
                        }
                    }

                    tran.Commit();
                }
                ret = true;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Touroku", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
                catchErr = true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Touroku", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Touroku", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }

        /// <summary>
        /// 明細部データ取得
        /// </summary>
        /// /// <returns></returns>
        private Boolean GetMeisaiIchiranData()
        {
            LogUtility.DebugMethodStart();

            Boolean blnSuccess = true;

            DataTable dbkoshin = this.SearchResultForUpdateData;
            List<T_LAST_SBN_SUSPEND> lastsbnsuspendMsList = new List<T_LAST_SBN_SUSPEND>();
            List<QUE_INFO> queinfoMsList = new List<QUE_INFO>();
            List<DT_MF_TOC> dtmftocMsList = new List<DT_MF_TOC>();

            String UsrName = System.Environment.UserName;
            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
            DateTime datatime = this.footer.sysDate;
            string pcname = System.Environment.MachineName;

            //int index = 0;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        // チェックがついているレコードに対応した情報を取得
                        var updateRecorde = dbkoshin.AsEnumerable().Where(w => w[HIDDEN_SYSTEM_ID].ToString().Equals(dgvRow.Cells[HIDDEN_SYSTEM_ID].Value.ToString())
                                                                            && w[HIDDEN_NEXT_HAIKI_KBN_CD].ToString().Equals(dgvRow.Cells[HIDDEN_NEXT_HAIKI_KBN_CD].Value.ToString()));

                        if (updateRecorde == null)
                        {
                            continue;
                        }

                        foreach (DataRow updateRow in updateRecorde)
                        {
                            T_LAST_SBN_SUSPEND lastsbnsuspend = new T_LAST_SBN_SUSPEND();
                            QUE_INFO queinfo = new QUE_INFO();
                            DT_MF_TOC dtmftoc = new DT_MF_TOC();

                            //T_LAST_SBN_SUSPEND
                            lastsbnsuspend.SYSTEM_ID = SqlInt64.Parse(updateRow[this.HIDDEN_SYSTEM_ID].ToString());

                            lastsbnsuspend.TIME_STAMP = ConvertStrByte.In32ToByteArray((int)updateRow[this.HIDDEN_TIME_STAMP]);
                            lastsbnsuspend.UPDATE_DATE = SqlDateTime.Parse(datatime.ToString());
                            lastsbnsuspend.UPDATE_USER = UsrName;
                            lastsbnsuspend.UPDATE_PC = pcname;

                            // 重複更新を避けるためチェック
                            var lastSbnSuspendCheckData = lastsbnsuspendMsList.Where(w => (bool)(lastsbnsuspend.SYSTEM_ID == w.SYSTEM_ID));
                            if (lastSbnSuspendCheckData == null || lastSbnSuspendCheckData.Count() < 1)
                            {
                                lastsbnsuspendMsList.Add(lastsbnsuspend);
                            }

                            //QUE_INFO
                            //排他制御
                            if (updateRow[this.HIDDEN_QI_UPDATE_TS].ToString().Equals(datatime.ToString()))
                            {
                                blnSuccess = false;
                                break;
                            }
                            else
                            {
                                queinfo.KANRI_ID = updateRow[this.HIDDEN_KANRI_ID].ToString();
                                queinfo.QUE_SEQ = SqlInt16.Parse(updateRow[this.HIDDEN_QUE_SEQ].ToString());

                                queinfo.STATUS_FLAG = 0;
                                queinfo.TRF_STATUS = 0;
                                queinfo.UPDATE_TS = (DateTime)updateRow[this.HIDDEN_QI_UPDATE_TS];

                                // 重複更新を避けるためチェック
                                var queInfoCheckData = queinfoMsList.Where(w => updateRow[this.HIDDEN_KANRI_ID].ToString().Equals(w.KANRI_ID)
                                                                                && updateRow[this.HIDDEN_QUE_SEQ].ToString().Equals(w.QUE_SEQ.ToString()));
                                if (queInfoCheckData == null || queInfoCheckData.Count() < 1)
                                {
                                    queinfoMsList.Add(queinfo);
                                }
                            }



                            //DT_MF_TOC
                            //排他制御
                            if (updateRow[this.HIDDEN_DMT_UPDATE_TS].ToString().Equals(datatime.ToString()))
                            {
                                blnSuccess = false;
                                break;
                            }
                            else
                            {
                                // 【操作】が取消の場合のみ更新する
                                if (updateRow[this.HIDDEN_FUNCTION_ID].ToString().Equals("取消"))
                                {
                                    dtmftoc.KANRI_ID = updateRow[this.HIDDEN_KANRI_ID].ToString();

                                    dtmftoc.STATUS_DETAIL = 1;
                                    dtmftoc.UPDATE_TS = (DateTime)updateRow[this.HIDDEN_DMT_UPDATE_TS];

                                    // 重複更新を避けるためチェック
                                    var mfTocCheckData = dtmftocMsList.Where(w => updateRow[this.HIDDEN_KANRI_ID].ToString().Equals(w.KANRI_ID.ToString()));
                                    if (mfTocCheckData == null || mfTocCheckData.Count() < 1)
                                    {
                                        dtmftocMsList.Add(dtmftoc);
                                    }
                                }
                            }

                        }
                    }
                }
            }
            mlastsbnsuspendMsList = lastsbnsuspendMsList;
            mqueinfoMsList = queinfoMsList;
            mdtmftocMsList = dtmftocMsList;
            LogUtility.DebugMethodEnd();

            return blnSuccess;
        }

        public void ch_OnCheckBoxClicked(object sender, datagridviewCheckboxHeaderEventArgs e)
        {

            // 全選択前にフォーカスを変えておく
            if (this.form.customDataGridView1.Rows.Count > 0)
            {
                this.form.customDataGridView1.CurrentCell = null;
            }

            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (e.CheckedState)
                {
                    dgvRow.Cells[0].Value = true;
                }
                else
                {
                    dgvRow.Cells[0].Value = false;
                }
            }

            if (this.form.customDataGridView1.Rows.Count > 0)
            {
                this.form.customDataGridView1.CurrentCell = this.form.customDataGridView1.Rows[0].Cells[0];
            }
        }

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

        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DataGridviewCheckboxHeaderCell header = col.HeaderCell as DataGridviewCheckboxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
            }
        }
    }
}
