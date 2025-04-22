using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.KensakuKekkaIchiran.DAO;
using System.Linq;

namespace Shougun.Core.Common.KensakuKekkaIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.KensakuKekkaIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        //private DAOClass daoKensakuKekkaIchiran;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// SQL文
        /// </summary>
        private StringBuilder sql;

        /// <summary>
        /// 取引先CD
        /// </summary>
        internal readonly string HIDDEN_TORIHIKISAKI_CD = "HIDDEN_TORIHIKISAKI_CD";

        /// <summary>
        /// 業者CD
        /// </summary>
        internal readonly string HIDDEN_GYOUSHA_CD = "HIDDEN_GYOUSHA_CD";

        /// <summary>
        /// 現場CD
        /// </summary>
        internal readonly string HIDDEN_GENBA_CD = "HIDDEN_GENBA_CD";

        #region プロパティ

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string SearchString { get; set; }

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
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }


        private Control[] allControl;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        #endregion

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">targetForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.form = targetForm;
            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            LogUtility.DebugMethodStart();

            //システム情報を取得し、初期値をセットする
            M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
            if (sysInfo != null)
            {
                //システム情報からアラート件数を取得
                this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
            }

            //this.form.l
            // ボタンのテキストを初期化
            this.ButtonInit();
            // イベントの初期化処理
            this.EventInit();
            this.allControl = this.form.allControl;
            this.form.customDataGridView1.AllowUserToAddRows = false;                             //行の追加オプション(false)

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンの設定情報をファイルから読み込む
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //Functionボタンのイベント生成
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //並替移動
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);         //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);             //検索条件設定画面へ遷移

            //customTextBoxでのエンターキー押下イベント生成
            this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);       //汎用検索(SearchString)
            parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)

            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick); //ダブルクリックで顧客カルテに遷移

            // 入力ボックス無効化＆ヒントラベルを空白表示
            this.form.searchString.ReadOnly = true;
            parentForm.lb_hint.Text = "";
            parentForm.bt_func7.Focus();

            //Thang Nguyen 20150626 #10664 Start
            // 最大化ボタン、最小化ボタン非表示
            //parentForm.MaximizeBox = false;
            //parentForm.MinimizeBox = false;
            //Thang Nguyen 20150626 #10664 End

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            this.form.SearchValue.Clear();
            this.form.SearchValue.Focus();
            this.SearchResult.Clear();

            //並び順クリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            //フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            if (this.form.PatternNo == 0)
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            string getSearchString = this.form.SearchValue.Text;
            this.SearchString = getSearchString;

            // 20140627 syunrei EV004040_検索結果一覧で検索結果が無い時にアラートが表示されない　start
            int cnt = this.Search();
            if (cnt <= 0)
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C001");
                return;
            }
            // 20140627 syunrei EV004040_検索結果一覧で検索結果が無い時にアラートが表示されない　end

        }

        /// <summary>
        /// F10 並替移動
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {   //フォーカス設定
            this.form.customSortHeader1.Focus();
            // 一覧がクリアされた場合、リターンする
            if (this.form.customDataGridView1.RowCount == 0) return;
            //並び順画面ポップアップ
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
            this.form.customSortHeader1.LinkedDataGridViewName = "customDataGridView1";
            //テーブル処理
            DataTable dt = this.form.customDataGridView1.DataSource as DataTable;
            this.form.customSortHeader1.SortDataTable(dt);
            this.form.customDataGridView1.DataSource = dt;
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

        }

        #endregion

        #region プロセスボタン押下処理
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.SearchResult = this.form.Table;
                this.form.ShowData();
            }
            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {
            //LogUtility.DebugMethodStart();

            //var us = new KensakujoukenSetteiForm(this.form.DenshuKbn);
            //us.Show();

            //LogUtility.DebugMethodEnd();

        }
        #endregion

        #region 汎用検索(SearchString)内でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void SearchStringKeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                    this.SearchString = getSearchString;
                    this.Search();

                }
                else
                {
                    this.form.searchString.Text = "";
                    this.form.searchString.SelectionLength = this.form.searchString.Text.Length;
                    this.form.searchString.Focus();
                }

            }
        }

        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント
        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            if ("1".Equals(parentForm.txb_process.Text))
            {
                //パターン一覧画面へ遷移
                this.bt_process1_Click(sender, e);
            }
            else if ("2".Equals(parentForm.txb_process.Text))
            {
                //検索条件設定画面へ遷移
            }
        }

        #endregion

        #region 検索文字列の作成
        /// <summary>
        /// 検索文字列を作成する
        /// </summary>
        /// <param name="beforeSeachArray">beforeSeachArray</param>
        /// <param name="hankakuChangeArray">hankakuChangeArray</param>
        private void MakeSearchCondition(string[] beforeSeachArray, string[] hankakuChangeArray)
        {
            LogUtility.DebugMethodStart(beforeSeachArray, hankakuChangeArray);

            // SQL文格納StringBuilder
            var sql = new StringBuilder();

            #region 現場有り

            #region SELECT句作成

            sql.Append(" SELECT DISTINCT ");
            sql.Append(this.selectQuery);
            sql.AppendFormat(" , M_TORIHIKISAKI.TORIHIKISAKI_CD AS {0} ", this.HIDDEN_TORIHIKISAKI_CD);
            sql.AppendFormat(" , M_GYOUSHA.GYOUSHA_CD AS {0} ", this.HIDDEN_GYOUSHA_CD);
            sql.AppendFormat(" , M_GENBA.GENBA_CD AS {0} ", this.HIDDEN_GENBA_CD);

            #endregion

            #region FROM句作成

            // 現場マスタ
            sql.Append("FROM M_GENBA ");

            // 業者マスタ
            sql.Append("INNER JOIN M_GYOUSHA ");
            sql.Append("ON M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD ");

            // 取引先マスタ
            sql.Append("LEFT JOIN M_TORIHIKISAKI ");
            sql.Append("ON M_GENBA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");

            // JOIN句
            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句作成

            // 文字列に対してエスケープシーケンス対策を行う
            string SqlStr = string.Empty;
            if (beforeSeachArray != null && hankakuChangeArray != null &&
                beforeSeachArray.Length > 0 && hankakuChangeArray.Length > 0)
            {
                //取引先マスタの取引先ふりがな
                sql.Append(" WHERE ");
                sql.Append(" ((M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                    }
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr);
                    sql.Append("%' OR ");
                    sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                //業者マスタの業者ふりがな
                sql.Append(" ((M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                    }
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr);
                    sql.Append("%' OR ");
                    sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                //現場マスタの現場ふりがな
                sql.Append(" ((M_GENBA.GENBA_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_GENBA.GENBA_FURIGANA LIKE '%");
                    }
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr);
                    sql.Append("%' OR ");
                    sql.Append(" M_GENBA.GENBA_FURIGANA LIKE '%");
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                // 検索条件にハイフンが含まれていればハイフンを含んだ状態で検索する
                var isIncludeHyphen = false;
                if (hankakuChangeArray.Where(s => s.Contains("-")).FirstOrDefault() != null)
                {
                    isIncludeHyphen = true;
                }

                if (isIncludeHyphen)
                {
                    //取引先マスタの電話番号
                    sql.Append(" (M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                        }
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' OR");
                        sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(") OR");

                    //業者マスタの電話番号
                    sql.Append(" (M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                        }
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' OR");
                        sql.Append(" M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(") OR");

                    //現場マスタの電話番号
                    sql.Append(" (M_GENBA.GENBA_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_GENBA.GENBA_TEL LIKE '%");
                        }
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' OR");
                        sql.Append(" M_GENBA.GENBA_TEL LIKE '%");
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(")");
                }
                else
                {
                    //取引先マスタの電話番号
                    sql.Append(" (((replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(")) OR ");

                    //業者マスタの電話番号
                    sql.Append(" (((replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(")) OR ");

                    //現場マスタの電話番号
                    sql.Append(" (((replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(beforeSeachArray[i]);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                        SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(" )) ");
                }
            }

            #endregion

            #endregion

            #region 現場無し、業者のみ

            #region SELECT句作成

            sql.Append(" UNION ALL ");
            sql.Append(" SELECT DISTINCT ");
            sql.Append(this.selectQuery);
            sql.AppendFormat(" , M_TORIHIKISAKI.TORIHIKISAKI_CD AS {0} ", this.HIDDEN_TORIHIKISAKI_CD);
            sql.AppendFormat(" , M_GYOUSHA.GYOUSHA_CD AS {0} ", this.HIDDEN_GYOUSHA_CD);
            sql.AppendFormat(" , M_GENBA.GENBA_CD AS {0} ", this.HIDDEN_GENBA_CD);

            #endregion

            #region FROM句作成

            // 業者マスタ
            sql.Append("FROM M_GYOUSHA ");

            // 取引先マスタ
            sql.Append("LEFT JOIN M_TORIHIKISAKI ");
            sql.Append("ON M_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");

            // 現場マスタ
            sql.Append("LEFT JOIN M_GENBA ");
            sql.Append("ON M_GENBA.GYOUSHA_CD IS NULL AND M_GENBA.GENBA_CD IS NULL ");

            // JOIN句
            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句作成

            // 文字列に対してエスケープシーケンス対策を行う
            string SqlStr2 = string.Empty;
            if (beforeSeachArray != null && hankakuChangeArray != null &&
                beforeSeachArray.Length > 0 && hankakuChangeArray.Length > 0)
            {
                //取引先マスタの取引先ふりがな
                sql.Append(" WHERE ");
                sql.Append(" NOT EXISTS ( ");
                sql.Append(" SELECT * FROM M_GENBA WHERE M_GENBA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GENBA.TORIHIKISAKI_CD = M_GYOUSHA.TORIHIKISAKI_CD");
                sql.Append(" ) ");
                sql.Append(" AND ( ((M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                    }
                    SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr2);
                    sql.Append("%' OR ");
                    sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                    SqlStr = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr2);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                //業者マスタの業者ふりがな
                sql.Append(" ((M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                    }
                    SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr2);
                    sql.Append("%' OR ");
                    sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                    SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr2);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                //現場マスタの現場ふりがな
                sql.Append(" ((M_GENBA.GENBA_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_GENBA.GENBA_FURIGANA LIKE '%");
                    }
                    SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr2);
                    sql.Append("%' OR ");
                    sql.Append(" M_GENBA.GENBA_FURIGANA LIKE '%");
                    SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr2);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                // 検索条件にハイフンが含まれていればハイフンを含んだ状態で検索する
                var isIncludeHyphen = false;
                if (hankakuChangeArray.Where(s => s.Contains("-")).FirstOrDefault() != null)
                {
                    isIncludeHyphen = true;
                }

                if (isIncludeHyphen)
                {
                    //取引先マスタの電話番号
                    sql.Append(" (M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                        }
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' OR");
                        sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(") OR");

                    //業者マスタの電話番号
                    sql.Append(" (M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                        }
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' OR");
                        sql.Append(" M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(") OR");

                    //現場マスタの電話番号
                    sql.Append(" (M_GENBA.GENBA_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_GENBA.GENBA_TEL LIKE '%");
                        }
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' OR");
                        sql.Append(" M_GENBA.GENBA_TEL LIKE '%");
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append("))");
                }
                else
                {
                    //取引先マスタの電話番号
                    sql.Append(" (((replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(")) OR ");

                    //業者マスタの電話番号
                    sql.Append(" (((replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(")) OR ");

                    //現場マスタの電話番号
                    sql.Append(" (((replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(beforeSeachArray[i]);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                        SqlStr2 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr2);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(" )) )");
                }
            }

            #endregion

            #endregion

            #region どのマスタからも参照されない取引先

            #region SELECT句作成

            sql.Append(" UNION ALL ");
            sql.Append(" SELECT DISTINCT ");
            sql.Append(this.selectQuery);
            sql.AppendFormat(" , M_TORIHIKISAKI.TORIHIKISAKI_CD AS {0} ", this.HIDDEN_TORIHIKISAKI_CD);
            sql.AppendFormat(" , M_GYOUSHA.GYOUSHA_CD AS {0} ", this.HIDDEN_GYOUSHA_CD);
            sql.AppendFormat(" , M_GENBA.GENBA_CD AS {0} ", this.HIDDEN_GENBA_CD);

            #endregion

            #region FROM句作成

            // 取引先マスタ
            sql.Append("FROM M_TORIHIKISAKI ");

            // 業者マスタ
            sql.Append("LEFT JOIN M_GYOUSHA ");
            sql.Append("ON M_GYOUSHA.GYOUSHA_CD IS NULL ");

            // 現場マスタ
            sql.Append("LEFT JOIN M_GENBA ");
            sql.Append("ON M_GENBA.GYOUSHA_CD IS NULL AND M_GENBA.GENBA_CD IS NULL ");

            // JOIN句
            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句作成

            // 文字列に対してエスケープシーケンス対策を行う
            string SqlStr3 = string.Empty;
            if (beforeSeachArray != null && hankakuChangeArray != null &&
                beforeSeachArray.Length > 0 && hankakuChangeArray.Length > 0)
            {
                //取引先マスタの取引先ふりがな
                sql.Append(" WHERE ");
                sql.Append(" NOT EXISTS ( ");
                sql.Append(" SELECT * FROM M_GYOUSHA WHERE M_GYOUSHA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");
                sql.Append(" ) ");
                sql.Append(" AND NOT EXISTS ( ");
                sql.Append(" SELECT * FROM M_GENBA WHERE M_GENBA.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD ");
                sql.Append(" ) ");
                sql.Append(" AND (((M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                    }
                    SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr3);
                    sql.Append("%' OR ");
                    sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_FURIGANA LIKE '%");
                    SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr3);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                //業者マスタの業者ふりがな
                sql.Append(" ((M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                    }
                    SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr3);
                    sql.Append("%' OR ");
                    sql.Append(" M_GYOUSHA.GYOUSHA_FURIGANA LIKE '%");
                    SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr3);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                //現場マスタの現場ふりがな
                sql.Append(" ((M_GENBA.GENBA_FURIGANA LIKE '%");
                for (int i = 0; i < beforeSeachArray.Length; i++)
                {
                    if (i != 0)
                    {
                        sql.Append(" ) AND ");
                        sql.Append(" (M_GENBA.GENBA_FURIGANA LIKE '%");
                    }
                    SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                    sql.Append(SqlStr3);
                    sql.Append("%' OR ");
                    sql.Append(" M_GENBA.GENBA_FURIGANA LIKE '%");
                    SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                    sql.Append(SqlStr3);
                    sql.Append("%' ");
                    i++;
                }
                sql.Append(")) OR ");

                // 検索条件にハイフンが含まれていればハイフンを含んだ状態で検索する
                var isIncludeHyphen = false;
                if (hankakuChangeArray.Where(s => s.Contains("-")).FirstOrDefault() != null)
                {
                    isIncludeHyphen = true;
                }

                if (isIncludeHyphen)
                {
                    //取引先マスタの電話番号
                    sql.Append(" (M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                        }
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' OR");
                        sql.Append(" M_TORIHIKISAKI.TORIHIKISAKI_TEL LIKE '%");
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(") OR");

                    //業者マスタの電話番号
                    sql.Append(" (M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                        }
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' OR");
                        sql.Append(" M_GYOUSHA.GYOUSHA_TEL LIKE '%");
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append(") OR");

                    //現場マスタの電話番号
                    sql.Append(" (M_GENBA.GENBA_TEL LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" AND");
                            sql.Append(" M_GENBA.GENBA_TEL LIKE '%");
                        }
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' OR");
                        sql.Append(" M_GENBA.GENBA_TEL LIKE '%");
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%'");
                        i++;
                    }
                    sql.Append("))");
                }
                else
                {
                    //取引先マスタの電話番号
                    sql.Append(" (((replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_TORIHIKISAKI.TORIHIKISAKI_TEL, '-', '')) LIKE '%");
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(")) OR ");

                    //業者マスタの電話番号
                    sql.Append(" (((replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_GYOUSHA.GYOUSHA_TEL, '-', '')) LIKE '%");
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(")) OR ");

                    //現場マスタの電話番号
                    sql.Append(" (((replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                    for (int i = 0; i < beforeSeachArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" ) AND ");
                            sql.Append(" ((replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                        }
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(beforeSeachArray[i]);
                        sql.Append(beforeSeachArray[i]);
                        sql.Append("%' OR ");
                        sql.Append(" (replace(M_GENBA.GENBA_TEL, '-', '')) LIKE '%");
                        SqlStr3 = SqlCreateUtility.CounterplanEscapeSequence(hankakuChangeArray[i]);
                        sql.Append(SqlStr3);
                        sql.Append("%' ");
                        i++;
                    }
                    sql.Append(" )) ) ");
                }
            }

            #endregion

            #endregion

            //ORDERBY句作成

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            this.createSql = sql.ToString();
            sql.Append("");

            LogUtility.DebugMethodEnd();
        }

        #endregion

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }

        //全角の英数字の文字列を半角に変換

        /// <summary>
        /// 全角スペースを半角スペースに変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankakuSpace(string param)
        {
            Regex re = new Regex("[　]+");
            string output = re.Replace(param, MyReplacer);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankaku(string param)
        {
            Regex re = new Regex("[０-９Ａ-Ｚａ-ｚ　]+");
            string output = re.Replace(param, MyReplacer);
            output = Strings.StrConv(output, VbStrConv.Narrow, 0);
            return output;
        }

        static string MyReplacer(Match m)
        {
            return Strings.StrConv(m.Value, VbStrConv.Narrow, 0);
        }


        public static bool IsFullwidthKatakana(char c)
        {
            //「ダブルハイフン」から「コト」までと、カタカナフリガナ拡張と、
            //濁点と半濁点を全角カタカナとする
            //中点と長音記号も含む
            return ('\u30A0' <= c && c <= '\u30FF')
                || ('\u31F0' <= c && c <= '\u31FF')
                || ('\u3099' <= c && c <= '\u309C');
        }


        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string[] ToHankakuArray(string[] param)
        {
            String[] reoutput = new String[param.Length];

            int i = 0;
            foreach (String search in param)
            {

                // 英数字の全角⇒半角をする
                int j = 0;

                String strhenkan = "";

                for (j = 0; j < search.Length; j++)
                {
                    Regex regex = new Regex("[０-９Ａ-Ｚａ-ｚ：－　]+");
                    char[] check = new char[] { search[j] };
                    String checkstr = new String(check);

                    if (IsFullwidthKatakana(search[j]))
                    {
                        strhenkan = strhenkan + checkstr;
                    }
                    else
                    {
                        String henkan = regex.Replace(checkstr, MyReplacer);
                        henkan = Strings.StrConv(henkan, VbStrConv.Narrow, 0);
                        strhenkan = strhenkan + henkan;
                    }
                }

                reoutput[i] = strhenkan;
                i = i + 1;
            }

            return reoutput;
        }

        public static bool IsHalfwidthKatakana(char c)
        {
            //「･」から「ﾟ」までを半角カタカナとする
            return '\uFF65' <= c && c <= '\uFF9F';

        }

        /// <summary>
        /// 半角の英数字の文字列を全角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string[] ToZenkakuArray(string[] param)
        {
            String[] reoutput = new String[param.Length];

            int i = 0;
            foreach (String search in param)
            {

                // 英数字の半角⇒全角をする
                int j = 0;

                String strhenkan = "";

                for (j = 0; j < search.Length; j++)
                {
                    Regex regex = new Regex("[a-zA-Z0-9]+");
                    char[] check = new char[] { search[j] };
                    String checkstr = new String(check);

                    if (IsHalfwidthKatakana(search[j]))
                    {
                        strhenkan = strhenkan + checkstr;
                    }
                    else
                    {
                        String henkan = regex.Replace(checkstr, MyReplacerZen);
                        henkan = Strings.StrConv(henkan, VbStrConv.Wide, 0);
                        strhenkan = strhenkan + henkan;
                    }
                }

                reoutput[i] = strhenkan;
                i = i + 1;
            }

            return reoutput;
        }

        static string MyReplacerZen(Match m)
        {
            return Strings.StrConv(m.Value, VbStrConv.Wide, 0);
        }

        //検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        [Transaction]
        public int Search()
        {
            LogUtility.DebugMethodStart();

            String beforeString = "";
            String afterString = "";

            if (this.SearchString != null)
            {
                // シングルクォートは２つ重ねる
                var replaceStr = this.SearchString.Replace("'", "''");
                //全角スペースを半角スペースに変換
                beforeString = this.ToHankakuSpace(replaceStr);
                //全角英数字を半角変換後の文字列
                afterString = this.ToHankaku(beforeString);
            }

            // 検索文字列を取得
            this.selectQuery = this.form.logic.SelectQeury;
            this.orderByQuery = this.form.logic.OrderByQuery;
            this.joinQuery = this.form.JoinQuery;

            string[] beforeSeachArray = beforeString.Split(' ');
            string[] hankakuChangeArray = afterString.Split(' ');

            // ★一旦保留
            //string[] beforeSeachArray = beforeString.Split(' ');
            //string[] hankakuChangeArray = ToHankakuArray(beforeSeachArray);

            //string[] ZenkakuChangeArray = ToZenkakuArray(beforeSeachArray);


            //SQLの作成
            MakeSearchCondition(beforeSeachArray, hankakuChangeArray);

            //検索実行
            this.SearchResult = new DataTable();

            if (!String.IsNullOrEmpty(this.selectQuery))
            {
                this.SearchResult = daoIchiran.getdateforstringsql(this.createSql);
            }

            //this.SearchResult = daoIchiran.getdateforstringsql(this.createSql);
            int count = SearchResult.Rows == null ? 0 : SearchResult.Rows.Count;

            //結果を表示
            this.form.ShowData();

            LogUtility.DebugMethodEnd(count);
            return count;
        }

        /// <summary>
        /// 参照イベント処理
        /// </summary>
        /// <returns></returns>
        public virtual void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.form.customDataGridView1.RowCount == 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = this.form.customDataGridView1.Rows.SharedRow(e.RowIndex);

            // 顧客カルテ
            string strFormId = "G173";

            // 引数 arg[0] : 取引先CD
            string torihikisakiCd = row.Cells[this.HIDDEN_TORIHIKISAKI_CD].Value == null ? string.Empty : row.Cells[this.HIDDEN_TORIHIKISAKI_CD].Value.ToString();
            // 引数 arg[1] : 業者CD
            string gyoushaCd = row.Cells[this.HIDDEN_GYOUSHA_CD].Value == null ? string.Empty : row.Cells[this.HIDDEN_GYOUSHA_CD].Value.ToString();
            // 引数 arg[2] : 現場CD
            string genbaCd = row.Cells[this.HIDDEN_GENBA_CD].Value == null ? string.Empty : row.Cells[this.HIDDEN_GENBA_CD].Value.ToString();
            //フォーム起動
            FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, torihikisakiCd, gyoushaCd, genbaCd);
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
    }
}
