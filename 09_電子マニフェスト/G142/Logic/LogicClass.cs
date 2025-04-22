using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.Authority;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;
using Shougun.Core.Message;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Common.BusinessCommon.Dto;
using r_framework.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace Shougun.Core.ElectronicManifest.SousinhoryuuTouroku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン定義ファイルパス
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ElectronicManifest.SousinhoryuuTouroku.Setting.ButtonSetting.xml";

        /// <summary>
        /// 紐付したい対象検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// マニフェスト一覧検索処理用Dao
        /// </summary>
        private GetSHTJDaoCls GetSHTJDaoCls;

        /// <summary>
        /// 排出事業者CD存在チェック処理用Dao
        /// </summary>
        private SonzaiCheckDaoCls SonzaiCheckDaoCls;

        /// <summary>
        /// JWNET送信Dao
        /// </summary>
        private JWNETSoshinDaoCls JWNETSoshinDaoCls;

        /// <summary>
        /// 検索条(組込み条件DTO)
        /// </summary>
        private TOUROKUJYOUHOU_DTOCls dtoMani;

        /// <summary>
        /// List<QUE_INFO> mqueinfoMsList
        /// </summary>
        private List<QUE_INFO> mqueinfoMsList;

        /// List<DT_MF_TOC> mdtmftocMsList
        /// </summary>
        private List<DT_MF_TOC> mdtmftocMsList;

        /// <summary>
        /// 検索実行かどうか判断フラグ
        /// </summary>
        public Boolean kensakuFlg;

        /// <summary>
        /// 明細表示フラグ
        /// </summary>
        //public Boolean meisaihyoujiFlg;//2013.12.25 touti upd画面起動時に検索しない 処理方法修正

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
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 画面フォーム
        /// </summary>
        private UIForm form;
        private UIHeader header;
        private BusinessBaseForm footer;

        /// <summary> 親フォーム</summary>
        public BusinessBaseForm parentbaseform { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        internal readonly string HIDDEN_KANRI_ID = "HIDDEN_KANRI_ID";

        /// <summary>
        /// 枝番
        /// </summary>
        internal readonly string HIDDEN_SEQ = "HIDDEN_SEQ";

        /// <summary>
        /// 行番
        /// </summary>
        internal readonly string HIDDEN_QUE_SEQ = "HIDDEN_QUE_SEQ";

        /// <summary>
        /// タイムスタンプ(QUE_INFO)
        /// </summary>
        internal readonly string HIDDEN_QI_UPDATE_TS = "HIDDEN_QI_UPDATE_TS";

        /// <summary>
        /// タイムスタンプ(DT_MF_TOC)
        /// </summary>
        internal readonly string HIDDEN_DMT_UPDATE_TS = "HIDDEN_DMT_UPDATE_TS";

        /// <summary>
        /// 委託契約チェック用
        /// </summary>
        internal readonly string HST_GYOUSHA_CD_ERROR = "HST_GYOUSHA_CD_ERROR";
        internal readonly string HST_GENBA_CD_ERROR = "HST_GENBA_CD_ERROR";
        internal readonly string HAIKI_SHURUI_CD_ERROR = "HAIKI_SHURUI_CD_ERROR";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.GetSHTJDaoCls = DaoInitUtility.GetComponent<GetSHTJDaoCls>();
            this.JWNETSoshinDaoCls = DaoInitUtility.GetComponent<JWNETSoshinDaoCls>();
            this.SonzaiCheckDaoCls = DaoInitUtility.GetComponent<SonzaiCheckDaoCls>();

            this.dtoMani = new TOUROKUJYOUHOU_DTOCls();
            //meisaihyoujiFlg = false;//2013.12.25 touti upd画面起動時に検索しない 処理方法修正

            //フォーカス時のバックカラーが（水色）
            this.form.customDataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.form.customDataGridView1.DefaultCellStyle.SelectionBackColor = Color.Aqua;
            this.form.customDataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            LogUtility.DebugMethodEnd();
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

        /// <summary>
        /// 検索
        /// </summary>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            try
            {
                Get_Search();
                if (!kensakuFlg)
                {
                    if (!Set_Search())
                    {
                        return -1;
                    }
                }
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

            LogUtility.DebugMethodEnd();

            return 0;
        }

        /// <summary>
        /// データ取得
        /// </summary>
        public void Get_Search()
        {
            LogUtility.DebugMethodStart();

            kensakuFlg = false;

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region SELECT句

            sql.Append(" SELECT ");
            sql.Append(" CASE ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0101,0102) THEN '予約登録' ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0201,0202,0203,0204,0205,0206) THEN '予約修正' ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0300) THEN '予約取消' ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0401,0402) THEN '予約確定' ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0501,0502) THEN 'マニ登録' ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0601,0603) THEN 'マニ修正' ");
            sql.Append(" WHEN QUE_INFO.FUNCTION_ID in (0800) THEN 'マニ取消' ");
            sql.Append(" ELSE 'マニ取消' END AS '区分' ");

            String MOutputPatternSelect = this.selectQuery;

            if (String.IsNullOrEmpty(MOutputPatternSelect))
            {
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
                kensakuFlg = true;
                return;
            }
            sql.Append(", ");
            sql.Append(MOutputPatternSelect);
            //string str = "CASE WHEN (DT_R18.HIKIWATASHI_DATE IS NULL OR DT_R18.HIKIWATASHI_DATE = '' OR (ISDATE(DT_R18.HIKIWATASHI_DATE) = 0)) " +
            //    "THEN NULL ELSE CONVERT(DATETIME,DT_R18.HIKIWATASHI_DATE) END";
            //sql.Replace("DT_R18.HIKIWATASHI_DATE", str);

            sql.AppendFormat(" , QUE_INFO.KANRI_ID AS {0} ", this.HIDDEN_KANRI_ID);
            sql.AppendFormat(" , QUE_INFO.SEQ AS {0} ", this.HIDDEN_SEQ);
            sql.AppendFormat(" , QUE_INFO.QUE_SEQ AS {0} ", this.HIDDEN_QUE_SEQ);
            sql.AppendFormat(" , QUE_INFO.UPDATE_TS AS {0} ", this.HIDDEN_QI_UPDATE_TS);
            sql.AppendFormat(" , DT_MF_TOC.UPDATE_TS AS {0} ", this.HIDDEN_DMT_UPDATE_TS);
            string errorDefault = "\'0\'";
            sql.AppendFormat(" ,{0} AS HST_GYOUSHA_CD_ERROR", errorDefault);
            sql.AppendFormat(" ,{0} AS HST_GENBA_CD_ERROR", errorDefault);
            sql.AppendFormat(" ,{0} AS HAIKI_SHURUI_CD_ERROR ", errorDefault);
            #endregion

            #region FROM句
            sql.Append("FROM QUE_INFO ");
            sql.Append("    INNER JOIN DT_R18 ON ");
            sql.Append("       QUE_INFO.KANRI_ID = DT_R18.KANRI_ID ");
            sql.Append("     AND QUE_INFO.SEQ = DT_R18.SEQ ");
            sql.Append("    INNER JOIN DT_MF_TOC ON ");
            sql.Append("       QUE_INFO.KANRI_ID = DT_MF_TOC.KANRI_ID ");
            sql.Append("    LEFT JOIN DT_R05 ON ");
            sql.Append("       QUE_INFO.KANRI_ID = DT_R05.KANRI_ID ");
            sql.Append("     AND QUE_INFO.SEQ = DT_R05.SEQ ");
            sql.Append("     AND DT_R05.RENRAKU_ID_NO = '1' ");
            //sql.Append("    LEFT JOIN M_UNIT ON ");
            //sql.Append("       DT_R18.HAIKI_UNIT_CODE = M_UNIT.UNIT_CD ");
            //sql.Append("      AND M_UNIT.DENSHI_USE_KBN = '1' ");
            sql.Append(this.joinQuery);
            #endregion

            #region WHERE句
            sql.Append("WHERE ");

            //2013.12.25 touti upd画面起動時に検索しない 処理方法修正 start
            //if (meisaihyoujiFlg)
            //{

                sql.Append("    QUE_INFO.STATUS_FLAG = '7' ");
                sql.Append("    AND QUE_INFO.SEQ = (SELECT MAX(SEQ) FROM QUE_INFO QUE_INFO_MAX WHERE QUE_INFO.KANRI_ID = QUE_INFO_MAX.KANRI_ID) ");

                //予約／マニフェスト区分
                string MANIFEST_KBN = this.form.cntxt_ManifesutokubunCd.Text.ToString();
                if (MANIFEST_KBN != "")
                {
                    switch (MANIFEST_KBN)
                    {
                        case "1": sql.Append("             AND DT_R18.MANIFEST_KBN =  '2' ");
                            break;
                        case "2": sql.Append("             AND DT_R18.MANIFEST_KBN =  '1' ");
                            break;
                        case "3":
                            break;
                    }
                }

                //引渡し日（開始）
                string cDtPicker_StartDay = this.form.cDtPicker_StartDay.Text.ToString();
                if (!string.IsNullOrWhiteSpace(cDtPicker_StartDay))
                {
                    sql.Append("             AND CONVERT(nvarchar,DT_R18.HIKIWATASHI_DATE,120) >= '" + cDtPicker_StartDay.Substring(0, 10).Replace("/", "") + "'");
                }
                //引渡し日（終了）
                string cDtPicker_EndDay = this.form.cDtPicker_EndDay.Text.ToString();
                if (!string.IsNullOrWhiteSpace(cDtPicker_EndDay))
                {
                    sql.Append("             AND CONVERT(nvarchar,DT_R18.HIKIWATASHI_DATE,120) <= '" + cDtPicker_EndDay.Substring(0, 10).Replace("/", "") + "'");
                }

                //マニフェスト／予約番号（開始）
                string Manifesutobangou_From = this.form.Manifesutobangou_From.Text.ToString();
                if (Manifesutobangou_From != "")
                {
                    sql.Append("             AND DT_R18.MANIFEST_ID >= '" + Manifesutobangou_From + "'");
                }
                //マニフェスト／予約番号（終了）
                string Manifesutobangou_To = this.form.Manifesutobangou_To.Text.ToString();
                if (Manifesutobangou_To != "")
                {
                    sql.Append("             AND DT_R18.MANIFEST_ID <= '" + Manifesutobangou_To + "'");
                }

                //排出事業者の加入者番号
                string cantxt_HaisyutujigyousyaCD = this.form.cantxt_HaisyutuGyoushaCd.Text.ToString();
                if (cantxt_HaisyutujigyousyaCD != "")
                {
                    sql.Append("             AND DT_R18.HST_SHA_EDI_MEMBER_ID = '" + cantxt_HaisyutujigyousyaCD + "'");
                }

                //連絡番号
                string cantxt_Renrakubangou1 = this.form.cantxt_Renrakubangou1.Text.ToString();
                if (cantxt_Renrakubangou1 != "")
                {
                    sql.Append("             AND DT_R05.RENRAKU_ID = '" + cantxt_Renrakubangou1 + "'");
                }

                //機能番号
                string cntxt_JyoutaikunbunCd = this.form.cntxt_JyoutaikunbunCd.Text.ToString();
                if (cntxt_JyoutaikunbunCd != "")
                {
                    switch (this.form.cntxt_JyoutaikunbunCd.Text.ToString())
                    {
                        case "1":
                            sql.Append(" AND FUNCTION_ID in (0101,0102,0201,0202,0203,0204,0205,0206,0300,0401,0402,0501,0502,0601,0603,0800) ");
                            break;
                        case "2":
                            sql.Append(" AND FUNCTION_ID in (0101,0102,0501,0502) ");
                            break;
                        case "3":
                            sql.Append(" AND FUNCTION_ID in (0201,0202,0203,0204,0205,0206,0601,0603) ");
                            break;
                        case "4":
                            sql.Append(" AND FUNCTION_ID in (0300,0800) ");
                            break;
                        case "5":
                            sql.Append(" AND FUNCTION_ID in (0401,0402) ");
                            break;
                    }
                }
            //}
            //else {
            //    sql.Append("1 = 2 --初期明細表示さらない");
            //    meisaihyoujiFlg = true;
            //}
            //2013.12.25 touti upd画面起動時に検索しない 処理方法修正 end

            #endregion

            #region ORDERBY句

            if (!string.IsNullOrEmpty(this.orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");

            if (!string.IsNullOrWhiteSpace(MOutputPatternSelect))
            {
                this.SearchResult = GetSHTJDaoCls.getdataforstringsql(this.createSql);
            }

            this.SearchResult.Columns["HST_GYOUSHA_CD_ERROR"].ReadOnly = false;
            this.SearchResult.Columns["HST_GENBA_CD_ERROR"].ReadOnly = false;
            this.SearchResult.Columns["HAIKI_SHURUI_CD_ERROR"].ReadOnly = false;

            LogUtility.DebugMethodEnd();

        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

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

        #region ボタン初期化処理
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
        /// 画面初期化処理
        /// </summary>
        internal Boolean WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentbaseform = (BusinessBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // ボタンを初期化
                this.ButtonInit();

                //footボタン処理イベントを初期化
                EventInit();

                //マニフェスト区分初期化
                this.form.cntxt_ManifesutokubunCd.Text = "1";

                //状態区分初期化
                this.form.cntxt_JyoutaikunbunCd.Text = "1";

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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            BusinessBaseForm parentform = (BusinessBaseForm)this.form.Parent;

            //検索ボタン(F3)イベント生成
            parentform.bt_func3.Click += new EventHandler(this.form.bt_func3_Click);

            //検索ボタン(F4)イベント生成
            parentform.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);

            //プレビューボタン(F6)イベント生成
            parentform.bt_func6.Click += new EventHandler(this.form.bt_func6_Click);

            //検索ボタン(F8)イベント生成
            parentform.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

            //登録ボタン(F9)イベント生成
            parentform.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //登録ボタン(F9)イベント生成
            parentform.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

            //取消ボタン(F11)イベント生成
            parentform.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

            //閉じるボタン(F12)イベント生成
            parentform.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            //【1】パターン一覧(1)イベント生成
            parentform.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            //【2】検索条件設定(2)イベント生成
            parentform.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);

            /// 20141023 Houkakou 「電マニ仮登録」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.form.cDtPicker_EndDay.MouseDoubleClick += new MouseEventHandler(cDtPicker_EndDay_MouseDoubleClick);
            this.form.Manifesutobangou_To.MouseDoubleClick += new MouseEventHandler(Manifesutobangou_To_MouseDoubleClick);
            /// 20141023 Houkakou 「電マニ仮登録」のダブルクリックを追加する　end

            this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// 画面表示
        /// </summary>
        public bool Set_Search()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //DataGridの初期化
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();

                if (!HeaderCheckBoxSupport())
                {
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                //一覧の項目を非表示
                this.form.ShowData();
                this.form.customDataGridView1.Columns[0].ReadOnly = false;
                this.form.customDataGridView1.Columns["HST_GYOUSHA_CD_ERROR"].Visible = false;
                this.form.customDataGridView1.Columns["HST_GENBA_CD_ERROR"].Visible = false;
                this.form.customDataGridView1.Columns["HAIKI_SHURUI_CD_ERROR"].Visible = false;

                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.MultiSelect = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Set_Search", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public bool HeaderCheckBoxSupport()
        {
            try
            {
                DataGridViewCheckBoxColumn newColumn = new DataGridViewCheckBoxColumn();

                newColumn.Name = "";
                newColumn.Width = 25;
                DatagridViewCheckBoxHeaderCell newheader = new DatagridViewCheckBoxHeaderCell(0);
                newColumn.HeaderCell = newheader;
                //newColumn.HeaderText = "削   ";
                newColumn.ReadOnly = false;
                newColumn.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;

                if (this.form.customDataGridView1.Columns.Count > 0)
                {
                    this.form.customDataGridView1.Columns.Insert(1, newColumn);
                }
                else
                {
                    this.form.customDataGridView1.Columns.Add(newColumn);
                }
                this.form.customDataGridView1.Columns[0].ToolTipText = "更新または削除する場合はチェックしてください";

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

        }

        /// <summary>
        /// 削除ボタンクリック後ロジック処理
        /// </summary>
        public void Delete()
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
                    string errorMessage = string.Empty;

                    //チェックされた送信保留登録情報を削除するかを確認する。

                    DialogResult result = MessageBoxUtility.MessageBoxShow("C046", "画面の内容で削除処理を");
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                }
                else
                {
                    //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                    MessageBoxUtility.MessageBoxShow("E051", "削除するマニフェスト");
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
                            MessageBoxUtility.MessageBoxShow("E093");
                        }
                        //更新後、DataGridViewを更新する。
                        this.Search();
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Delete", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Delete", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Delete", ex);
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

            var result = false;
            catchErr = false;

            //明細部データ取得
            //キュー情報の排他制御結果がＮＧの場合はfalseを返す
            if (!GetMeisaiIchiranData_delete())
            {
                return result;
            }

            try
            {
                using (Transaction tran = new Transaction()) //トランザクション処理
                {
                    //QUE_INFO
                    if (mqueinfoMsList != null && mqueinfoMsList.Count() > 0)
                    {
                        foreach (QUE_INFO queinfo in mqueinfoMsList)
                        {
                            int CntMopkUpd = GetSHTJDaoCls.Update(queinfo);
                        }
                    }

                    tran.Commit();
                }

                result = true;
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
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 明細部データ取得（削除）
        /// </summary>
        /// /// <returns></returns>
        private Boolean GetMeisaiIchiranData_delete()
        {
            LogUtility.DebugMethodStart();

            Boolean blnSuccess = true;

            DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
            List<QUE_INFO> queinfoMsList = new List<QUE_INFO>();

            String UsrName = System.Environment.UserName;
            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
            DateTime datatime = DateTime.Now;
            string pcname = System.Environment.MachineName;

            //int index = 0;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                if (dgvRow.Cells[0].Value != null)
                {
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        QUE_INFO currentQueinfo = new QUE_INFO();

                        //QUE_INFO
                        //排他制御
                        if (dgvRow.Cells[this.HIDDEN_QI_UPDATE_TS].Value.ToString().Equals(datatime.ToString()))
                        {
                            blnSuccess = false;
                            break;
                        }
                        else
                        {
                            currentQueinfo.KANRI_ID = dgvRow.Cells[this.HIDDEN_KANRI_ID].Value.ToString();
                            currentQueinfo.QUE_SEQ = SqlInt16.Parse(dgvRow.Cells[this.HIDDEN_QUE_SEQ].Value.ToString());

                            currentQueinfo.STATUS_FLAG = 6;
                            currentQueinfo.UPDATE_TS = (DateTime)dgvRow.Cells[this.HIDDEN_QI_UPDATE_TS].Value;
                            queinfoMsList.Add(currentQueinfo);
                        }
                    }
                }
            }
            //mlastsbnsuspendMsList = lastsbnsuspendMsList;
            mqueinfoMsList = queinfoMsList;
            LogUtility.DebugMethodEnd();

            return blnSuccess;
        }

        /// <summary>
        /// JWNET送信ボタンクリック後ロジック処理
        /// </summary>
        public void Update()
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
                            if (this.CheckHikiwatashiDate(dgvRow))
                            {
                                return;
                            }
                        }
                    }
                }
                if (updataflag)
                {
                    //チェックされた最終処分保留を更新するかを確認する。
                    DialogResult result = MessageBoxUtility.MessageBoxShow("C046", "JWNET送信処理を");
                    if (result == DialogResult.No)
                    {
                        return;
                    }

                }
                else
                {
                    //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                    MessageBoxUtility.MessageBoxShow("E051", "JWNET送信するマニフェスト");
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
                            MessageBoxUtility.MessageBoxShow("I001", "JWNET送信処理");
                        }
                        else
                        {
                            MessageBoxUtility.MessageBoxShow("E093");
                        }
                        //更新後、DataGridViewを更新する。
                        this.Search();
                    }
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Update", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Update", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
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
                    //QUE_INFO
                    if (mqueinfoMsList != null && mqueinfoMsList.Count() > 0)
                    {
                        foreach (QUE_INFO queinfo in mqueinfoMsList)
                        {
                            int CntMopkUpd = GetSHTJDaoCls.Update(queinfo);
                        }
                    }

                    //DT_MF_TOC
                    if (mdtmftocMsList != null && mdtmftocMsList.Count() > 0)
                    {
                        foreach (DT_MF_TOC dtmftoc in mdtmftocMsList)
                        {
                            int CntMopkUpd = JWNETSoshinDaoCls.Update(dtmftoc);
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

            DataTable dbkoshin = (DataTable)this.form.customDataGridView1.DataSource;
            List<QUE_INFO> queinfoMsList = new List<QUE_INFO>();
            List<DT_MF_TOC> dtmftocMsList = new List<DT_MF_TOC>();

            String UsrName = System.Environment.UserName;
            UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
            DateTime datatime = DateTime.Now;
            string pcname = System.Environment.MachineName;

            //int index = 0;
            foreach (DataGridViewRow dgvRow in this.form.customDataGridView1.Rows)
            {
                if (dgvRow.Cells[0].Value != null)
                {
                    // チェックがついているレコードのシステムIDを取得し、更新情報としてセットする
                    if (dgvRow.Cells[0].Value.ToString().Equals("True"))
                    {
                        T_LAST_SBN_SUSPEND lastsbnsuspend = new T_LAST_SBN_SUSPEND();
                        QUE_INFO queinfo = new QUE_INFO();
                        DT_MF_TOC dtmftoc = new DT_MF_TOC();


                        //QUE_INFO
                        //排他制御
                        if (dgvRow.Cells[this.HIDDEN_QI_UPDATE_TS].Value.ToString().Equals(datatime.ToString()))
                        {
                            blnSuccess = false;
                            break;
                        }
                        else
                        {
                            queinfo.KANRI_ID = dgvRow.Cells[this.HIDDEN_KANRI_ID].Value.ToString();
                            queinfo.QUE_SEQ = SqlInt16.Parse(dgvRow.Cells[this.HIDDEN_QUE_SEQ].Value.ToString());

                            queinfo.STATUS_FLAG = 0;
                            queinfo.UPDATE_TS = (DateTime)dgvRow.Cells[this.HIDDEN_QI_UPDATE_TS].Value;
                            queinfoMsList.Add(queinfo);
                        }



                        //DT_MF_TOC
                        //排他制御
                        if (dgvRow.Cells[this.HIDDEN_DMT_UPDATE_TS].Value.ToString().Equals(datatime.ToString()))
                        {
                            blnSuccess = false;
                            break;
                        }
                        else
                        {
                            dtmftoc.KANRI_ID = dgvRow.Cells[this.HIDDEN_KANRI_ID].Value.ToString();

                            dtmftoc.STATUS_DETAIL = 1;
                            dtmftoc.UPDATE_TS = (DateTime)dgvRow.Cells[this.HIDDEN_DMT_UPDATE_TS].Value;
                            dtmftocMsList.Add(dtmftoc);
                        }
                    }
                }
            }
            //mlastsbnsuspendMsList = lastsbnsuspendMsList;
            mqueinfoMsList = queinfoMsList;
            mdtmftocMsList = dtmftocMsList;
            LogUtility.DebugMethodEnd();

            return blnSuccess;
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

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

                        //タイトル
                        this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_SOUSHIN_HORYU_JYOHO);

                        //検索条件
                        this.form.searchString.Text = "";

                        //ヒント
                        this.footer.lb_hint.Text = "";

                        //2013.12.26 touti 追加　デフォルト値は本日とする start
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        ////引渡し日From初期化
                        //this.form.cDtPicker_StartDay.Value = DateTime.Now.Date;

                        ////引渡し日To初期化
                        //this.form.cDtPicker_EndDay.Value = DateTime.Now.Date;
                        //引渡し日From初期化
                        this.form.cDtPicker_StartDay.Value = this.parentbaseform.sysDate.Date;

                        //引渡し日To初期化
                        this.form.cDtPicker_EndDay.Value = this.parentbaseform.sysDate.Date;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                        //2013.12.26 touti 追加　デフォルト値は本日とする end

                        break;

                    case "ClsSearchCondition"://検索条件をクリア

                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
                        //並び順ソートヘッダー
                        this.form.customSortHeader1.ClearCustomSortSetting();
                        //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

                        //検索結果クリア
                        //2013-12-26 upd touti PT 起動時クリアボタンを押すとエラーになって バグ修正 start
                        //this.SearchResult.Clear();
                        if (this.SearchResult != null)
                        {
                            this.SearchResult.Clear();
                        }
                        //2013-12-26 upd touti PT 起動時クリアボタンを押すとエラーになって バグ修正 end

                        //検索条件
                        this.form.searchString.Text = "";

                        //マニフェスト区分初期化
                        this.form.cntxt_ManifesutokubunCd.Text = "1";

                        //排出事業者CD初期化
                        this.form.cantxt_HaisyutuGyoushaCd.Text = "";

                        //排出事業者名初期化
                        this.form.ctxt_HaisyutujigyousyaName.Text = "";

                        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                        ////引渡し日From初期化
                        //this.form.cDtPicker_StartDay.Value = DateTime.Now.Date;

                        ////引渡し日To初期化
                        //this.form.cDtPicker_EndDay.Value = DateTime.Now.Date;
                        //引渡し日From初期化
                        this.form.cDtPicker_StartDay.Value = this.parentbaseform.sysDate.Date;

                        //引渡し日To初期化
                        this.form.cDtPicker_EndDay.Value = this.parentbaseform.sysDate.Date;
                        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                        //連絡番号1初期化
                        this.form.cantxt_Renrakubangou1.Text = "";

                        //マニフェスト番号From初期化
                        this.form.Manifesutobangou_From.Text = "";

                        //マニフェスト番号To初期化
                        this.form.Manifesutobangou_To.Text = "";

                        //状態区分初期化
                        this.form.cntxt_JyoutaikunbunCd.Text = "1";

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

        #region FW側処理
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
        #endregion

        /// <summary>
        /// Func3クリックと電子マニフェスト入力画面に遷移制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void func3_GamenSennyi()
        {
            try
            {
                //一覧内に選択行(チェックボックスではない)があった場合判断
                if (this.form.customDataGridView1.CurrentRow != null)
                {
                    if ("予約取消".Equals(this.form.customDataGridView1.CurrentRow.Cells["区分"].Value.ToString()) ||
                        "マニ取消".Equals(this.form.customDataGridView1.CurrentRow.Cells["区分"].Value.ToString()))
                    {
                        return;
                    }
                    //管理ID
                    String kanriId = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentCell.RowIndex].Cells[this.HIDDEN_KANRI_ID].Value.ToString();

                    //枝番
                    String seq = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentCell.RowIndex].Cells[this.HIDDEN_SEQ].Value.ToString();

                    //2013.12.18 naitou upd start
                    //var headeForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIHeader();

                    ////管理ID、枝番と修正モードを電子マニフェスト入力画面に渡す
                    //var callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriId, seq);

                    //var BusinessBaseForm = new BusinessBaseForm(callForm, headeForm);

                    ////共通画面を起動する
                    //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                    //if (!isExistForm)
                    //{
                    //    BusinessBaseForm.ShowDialog();
                    //}
                    // 権限チェック
                    if (Manager.CheckAuthority("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        //管理ID、枝番と修正モードを電子マニフェスト入力画面に渡す
                        //マニ一覧から、保留/JWNETエラーデータをマニ入力画面へ遷移する際も同じ引数で使用しているので注意！！
                        //FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriId, seq);
                        FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriId, seq, null, null, null, null, true);
                    }
                    else if (Manager.CheckAuthority("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        //管理ID、枝番と参照モードを電子マニフェスト入力画面に渡す
                        FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, seq);
                    }
                    else
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E158", "修正");
                        return;
                    }
                    //2013.12.18 naitou upd end
                }
                else
                {
                    //明細行の更新チェックボックス(１番左端)がTRUEのものが１件以上無い場合エラー。
                    MessageBoxUtility.MessageBoxShow("E029", "修正するマニフェスト", "マニフェスト一覧");
                    return;

                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("func3_GamenSennyi", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("func3_GamenSennyi", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.form.customDataGridView1.CurrentRow == null)
            {
                return;
            }

            if ("予約取消".Equals(this.form.customDataGridView1.CurrentRow.Cells["区分"].Value.ToString()) ||
                "マニ取消".Equals(this.form.customDataGridView1.CurrentRow.Cells["区分"].Value.ToString()))
            {
                return;
            }
            
            if ((e.RowIndex != -1))
            {
                //管理ID
                String kanriId = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentCell.RowIndex].Cells[this.HIDDEN_KANRI_ID].Value.ToString();

                //枝番
                String seq = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentCell.RowIndex].Cells[this.HIDDEN_SEQ].Value.ToString();

                //2013.12.18 naitou upd start
                //var headeForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIHeader();

                ////管理ID、枝番と修正モードを電子マニフェスト入力画面に渡す
                //var callForm = new Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku.UIForm(WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriId, seq);

                //var BusinessBaseForm = new BusinessBaseForm(callForm, headeForm);

                ////共通画面を起動する
                //var isExistForm = new FormControlLogic().ScreenPresenceCheck(callForm);
                //if (!isExistForm)
                //{
                //    BusinessBaseForm.ShowDialog();
                //}

                // 権限チェック
                if (Manager.CheckAuthority("G141", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    //管理ID、枝番と修正モードを電子マニフェスト入力画面に渡す
                    //マニ一覧から、保留/JWNETエラーデータをマニ入力画面へ遷移する際も同じ引数で使用しているので注意！！
                    FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, kanriId, seq, null, null, null, null, true);
                }
                else if (Manager.CheckAuthority("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    //管理ID、枝番と参照モードを電子マニフェスト入力画面に渡す
                    FormManager.OpenFormWithAuth("G141", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, kanriId, seq);
                }
                else
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    return;
                }
                //2013.12.18 naitou upd end
            }
        }

        //排出事業者マスタチェックと設置
        public void HaisyutuGyoushaCdCheckANDSet(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.form.cantxt_HaisyutuGyoushaCd.Text != "")
                {
                    //SQL文格納StringBuilder
                    var sql = new StringBuilder();
                    sql.Append(" SELECT EDI_MEMBER_ID,JIGYOUSHA_NAME,HST_KBN ");
                    sql.Append(" FROM M_DENSHI_JIGYOUSHA ");
                    sql.Append(" WHERE EDI_MEMBER_ID = '" + this.form.cantxt_HaisyutuGyoushaCd.Text.PadLeft(7, '0') + "'");
                    sql.Append(" AND HST_KBN = 1 ");

                    this.createSql = sql.ToString();
                    sql.Append("");

                    this.SearchResult = SonzaiCheckDaoCls.getdataforstringsql(this.createSql);

                    int count = this.SearchResult.Rows.Count;

                    if (count == 1)
                    {
                        this.form.ctxt_HaisyutujigyousyaName.Text = this.SearchResult.Rows[0].ItemArray[1].ToString();
                    }
                    else
                    {
                        //電子事業者マスタにが１件も存在しないの場合エラー。
                        MessageBoxUtility.MessageBoxShow("E020", "電子事業者");

                        //this.form.cantxt_HaisyutuGyoushaCd.Focus();
                        this.form.ctxt_HaisyutujigyousyaName.Text = "";
                        e.Cancel = true;
                        this.form.cantxt_HaisyutuGyoushaCd.IsInputErrorOccured = e.Cancel;
                    }
                }
                else
                {
                    this.form.ctxt_HaisyutujigyousyaName.Text = "";
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("HaisyutuGyoushaCdCheckANDSet", ex1);
                MessageBoxUtility.MessageBoxShow("E080", "");
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("HaisyutuGyoushaCdCheckANDSet", ex2);
                MessageBoxUtility.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("HaisyutuGyoushaCdCheckANDSet", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck(out bool catchErr)
        {
            catchErr = false;

            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                this.form.cDtPicker_StartDay.BackColor = Constans.NOMAL_COLOR;
                this.form.cDtPicker_EndDay.BackColor = Constans.NOMAL_COLOR;

                //nullチェック
                if (string.IsNullOrEmpty(this.form.cDtPicker_StartDay.Text))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.form.cDtPicker_EndDay.Text))
                {
                    return false;
                }

                DateTime date_from = DateTime.Parse(this.form.cDtPicker_StartDay.Text);
                DateTime date_to = DateTime.Parse(this.form.cDtPicker_EndDay.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.cDtPicker_StartDay.IsInputErrorOccured = true;
                    this.form.cDtPicker_EndDay.IsInputErrorOccured = true;
                    this.form.cDtPicker_StartDay.BackColor = Constans.ERROR_COLOR;
                    this.form.cDtPicker_EndDay.BackColor = Constans.ERROR_COLOR;
                    string[] errorMsg = { "引渡し日From", "引渡し日To" };
                    msgLogic.MessageBoxShow("E030", errorMsg);
                    this.form.cDtPicker_StartDay.Focus();
                    return true;
                }

                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DateCheck", ex1);
                MessageBoxUtility.MessageBoxShow("E093", "");
                catchErr = true;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DateCheck", ex);
                MessageBoxUtility.MessageBoxShow("E245", "");
                catchErr = true;
                return false;
            }
        }
        #endregion
        /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　end

        /// 20141128 Houkakou 「電マニ仮登録」のダブルクリックを追加する　start
        #region cDtPicker_EndDayダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cDtPicker_EndDay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.cDtPicker_StartDay;
            var ToTextBox = this.form.cDtPicker_EndDay;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region Manifesutobangou_Toダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manifesutobangou_To_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.form.Manifesutobangou_From;
            var ToTextBox = this.form.Manifesutobangou_To;

            ToTextBox.Text = FromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141128 Houkakou 「電マニ仮登録」のダブルクリックを追加する　end
        /// 
        //thongh 2015/08/21 #12441
        #region 引渡日に未来日チェック
        internal bool CheckHikiwatashiDate(DataGridViewRow dgvRow)
        {
            LogUtility.DebugMethodStart(dgvRow);
            DateTime Today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //入力区分[手動自動]
            string InputKB = string.Empty;
            //マニフェスト区分
            string ManiKBN = string.Empty;
            //引渡日
            string Hikiwatashi_date = string.Empty;

            DT_R18 dtR18 = new DT_R18();
            dtR18.KANRI_ID = Convert.ToString(dgvRow.Cells[this.HIDDEN_KANRI_ID].Value);
            dtR18.SEQ = Convert.ToDecimal(dgvRow.Cells[this.HIDDEN_SEQ].Value);
            DT_R18DaoCls DT_R18Dao = DaoInitUtility.GetComponent<DT_R18DaoCls>();
            DataTable dt = DT_R18Dao.GetDataByCd(dtR18);
            if (dt != null && dt.Rows.Count > 0)
            {
                ManiKBN = Convert.ToString(dt.Rows[0].Field<decimal>("MANIFEST_KBN"));
                Hikiwatashi_date = dt.Rows[0].Field<string>("HIKIWATASHI_DATE");
            }

            DT_MF_TOC dt_mf_toc = new DT_MF_TOC();
            dt_mf_toc = this.JWNETSoshinDaoCls.GetDataForEntity(dtR18.KANRI_ID);
            if (dt_mf_toc != null && !dt_mf_toc.KIND.IsNull)
            {
                InputKB = (dt_mf_toc.KIND == 5) ? "2" : "1";
            }

            if (!string.IsNullOrEmpty(ManiKBN) && !string.IsNullOrEmpty(InputKB) && !string.IsNullOrEmpty(Hikiwatashi_date))
            {
                if (ManiKBN == "2" && InputKB == "1" && DateTime.Parse(Hikiwatashi_date) > Today)
                {
                    MessageBoxUtility.MessageBoxShow("E243");
                    return true;
                }
            }
            LogUtility.DebugMethodStart(dgvRow);
            return false;
        }
        #endregion
        // thongh 2015/08/21 #12441

        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DatagridViewCheckBoxHeaderCell header = col.HeaderCell as DatagridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                    this.form.customDataGridView1.Refresh();
                }
            }
        }
    }
}
