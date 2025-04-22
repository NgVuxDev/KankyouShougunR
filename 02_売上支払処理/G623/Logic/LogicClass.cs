using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.SalesPayment.UriageJunihyo
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// 売上順位表画面クラス
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// 売上順位表Dtoを取得・設定します
        /// </summary>
        internal UriageJunihyoDto dto;

        /// <summary>
        /// 売上順位表データテーブルを取得・設定します
        /// </summary>
        internal DataTable UriageJunihyoDataTable { get; set; }

        /// <summary>
        /// 社員マスタのDao
        /// </summary>
        private IM_SHAINDao shainDao;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new UriageJunihyoDto();

            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>(); // 社員マスタのDao

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.ButtonInit();
                this.EventInit();

                this.parentForm = (BusinessBaseForm)this.form.Parent;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// ボタン初期化処理を行います
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込を行います
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            Type cType = this.GetType();
            string strButtonInfoXmlPath = cType.Namespace;
            strButtonInfoXmlPath += ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath));

            return buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            this.form.C_Regist(parentForm.bt_func5);
            this.form.C_Regist(parentForm.bt_func7);

            parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
            parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
            parentForm.bt_func4.Click += new EventHandler(this.form.ButtonFunc4_Clicked);
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

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

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 売上順位表出力用データを検索します
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int searchResult = 0;
            try
            {

                // 検索条件設定
                this.SetUriageJunihyoDto();

                // 検索実行
                DAOClass dao = DaoInitUtility.GetComponent<DAOClass>();
                this.UriageJunihyoDataTable = dao.GetGetUriageJunihyoData(this.dto);

                if (this.UriageJunihyoDataTable != null)
                {
                    searchResult = this.UriageJunihyoDataTable.Rows.Count;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(searchResult);

            return searchResult;
        }

        /// <summary>
        /// 運転者CDバリデート
        /// </summary>
        /// <param name="cdText">運転者CDテキスト</param>
        /// <param name="nameText">運転者名テキスト</param>
        public bool UNTENSHA_CDValidated(CustomAlphaNumTextBox cdText, CustomTextBox nameText)
        {
            try
            {
                LogUtility.DebugMethodStart(cdText, nameText);

                // 一旦初期化
                nameText.Text = "";

                M_SHAIN entity = new M_SHAIN();
                entity.ISNOT_NEED_DELETE_FLG = true;
                var untenShain = this.shainDao.GetAllValidData(entity).FirstOrDefault(s => (bool)s.UNTEN_KBN && s.SHAIN_CD == cdText.Text);
                if (untenShain == null)
                {
                    // エラーメッセージ
                    cdText.IsInputErrorOccured = true;
                    cdText.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    cdText.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                nameText.Text = untenShain.SHAIN_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UNTENSHA_CDValidated", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CDValidated", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// CSV
        /// </summary>
        internal bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                string headStr = "";
                string[] csvHead;

                if (this.UriageJunihyoDataTable != null && 0 < this.UriageJunihyoDataTable.Rows.Count)
                {
                    var headerTable = this.CreateReportHeaderTable();
                    if (headerTable.Rows[0]["COL_CD_1"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_CD_1"].ToString()))
                    {
                        headStr = headStr + headerTable.Rows[0]["COL_CD_1"].ToString() + ",";
                    }

                    if (headerTable.Rows[0]["COL_NAME_1"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_NAME_1"].ToString()))
                    {
                        headStr = headStr + headerTable.Rows[0]["COL_NAME_1"].ToString() + ",";
                    }

                    if (headerTable.Rows[0]["COL_CD_2"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_CD_2"].ToString()))
                    {
                        headStr = headStr + headerTable.Rows[0]["COL_CD_2"].ToString() + ",";
                    }

                    if (headerTable.Rows[0]["COL_NAME_2"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_NAME_2"].ToString()))
                    {
                        headStr = headStr + headerTable.Rows[0]["COL_NAME_2"].ToString() + ",";
                    }

                    headStr = headStr + "金額";

                    csvHead = headStr.Split(',');

                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }
                    for (int i = 0; i < UriageJunihyoDataTable.Rows.Count; i++)
                    {
                        rowTmp = csvDT.NewRow();
                        if (headerTable.Rows[0]["COL_CD_1"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_CD_1"].ToString()))
                        {
                            if (UriageJunihyoDataTable.Rows[i]["CD_1"] != null && !string.IsNullOrEmpty(UriageJunihyoDataTable.Rows[i]["CD_1"].ToString()))
                            {
                                rowTmp[headerTable.Rows[0]["COL_CD_1"].ToString()] = UriageJunihyoDataTable.Rows[i]["CD_1"].ToString();
                            }
                        }

                        if (headerTable.Rows[0]["COL_NAME_1"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_NAME_1"].ToString()))
                        {
                            if (UriageJunihyoDataTable.Rows[i]["NAME_1"] != null && !string.IsNullOrEmpty(UriageJunihyoDataTable.Rows[i]["NAME_1"].ToString()))
                            {
                                rowTmp[headerTable.Rows[0]["COL_NAME_1"].ToString()] = UriageJunihyoDataTable.Rows[i]["NAME_1"].ToString();
                            }
                        }

                        if (headerTable.Rows[0]["COL_CD_2"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_CD_2"].ToString()))
                        {
                            if (UriageJunihyoDataTable.Rows[i]["CD_2"] != null && !string.IsNullOrEmpty(UriageJunihyoDataTable.Rows[i]["CD_2"].ToString()))
                            {
                                rowTmp[headerTable.Rows[0]["COL_CD_2"].ToString()] = UriageJunihyoDataTable.Rows[i]["CD_2"].ToString();
                            }
                        }

                        if (headerTable.Rows[0]["COL_NAME_2"] != null && !string.IsNullOrEmpty(headerTable.Rows[0]["COL_NAME_2"].ToString()))
                        {
                            if (UriageJunihyoDataTable.Rows[i]["NAME_2"] != null && !string.IsNullOrEmpty(UriageJunihyoDataTable.Rows[i]["NAME_2"].ToString()))
                            {
                                rowTmp[headerTable.Rows[0]["COL_NAME_2"].ToString()] = UriageJunihyoDataTable.Rows[i]["NAME_2"].ToString();
                            }
                        }

                        if (UriageJunihyoDataTable.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(UriageJunihyoDataTable.Rows[i]["KINGAKU"].ToString()))
                        {
                            rowTmp["金額"] = Convert.ToDecimal(UriageJunihyoDataTable.Rows[i]["KINGAKU"].ToString()).ToString("#,##0");
                        }

                        csvDT.Rows.Add(rowTmp);
                    }
                    this.form.CsvReport(csvDT);
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateJunihyo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 順位表作成
        /// </summary>
        internal bool CreateJunihyo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.UriageJunihyoDataTable != null && 0 < this.UriageJunihyoDataTable.Rows.Count)
                {
                    var headerTable = this.CreateReportHeaderTable();
                    var reportInfo = new ReportInfoR624(headerTable, this.UriageJunihyoDataTable);
                    reportInfo.SetRecord(this.UriageJunihyoDataTable);
                    reportInfo.CreateR624();
                    reportInfo.Title = headerTable.Rows[0]["TITLE"].ToString();
                    reportInfo.ReportID = "R624";

                    // 印刷ポツプアップ画面表示
                    using (FormReportPrintPopup report = new FormReportPrintPopup(reportInfo))
                    {
                        report.ShowDialog();
                        report.Dispose();
                    }
                }
                else
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateJunihyo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 検索条件用Dtoに画面の値を設定
        /// </summary>
        private void SetUriageJunihyoDto()
        {
            this.dto = new UriageJunihyoDto();

            /* 拠点 */
            this.dto.KYOTEN_CD = int.Parse(this.form.KYOTEN_CD.Text);

            /* 日付範囲 */
            DateTime dateFrom = (DateTime)this.form.HIDUKE_FROM.Value;
            DateTime dateTo = (DateTime)this.form.HIDUKE_TO.Value;

            /* 日付種類 */
            if (this.form.HIDUKE_SHURUI.Text.Equals(ConstClass.DATE_CD_DENPYOU))
            {
                /* 伝票日付 */
                // 伝票日付_From
                this.dto.DENPYOU_DATE_FROM = dateFrom.ToString("yyyy/MM/dd");
                // 伝票日付_To
                this.dto.DENPYOU_DATE_TO = dateTo.ToString("yyyy/MM/dd");
            }
            else if (this.form.HIDUKE_SHURUI.Text.Equals(ConstClass.DATE_CD_URIAGE))
            {
                /* 売上日付 */
                // 売上日付_From
                this.dto.URIAGE_DATE_FROM = dateFrom.ToString("yyyy/MM/dd");
                // 売上日付_To
                this.dto.URIAGE_DATE_TO = dateTo.ToString("yyyy/MM/dd");
            }
            else if (this.form.HIDUKE_SHURUI.Text.Equals(ConstClass.DATE_CD_INPUT))
            {
                /* 入力日付 */
                // 入力日付_From
                this.dto.UPDATE_DATE_FROM = dateFrom.ToString("yyyy/MM/dd");
                // 入力日付_To
                this.dto.UPDATE_DATE_TO = dateTo.ToString("yyyy/MM/dd");
            }

            /* 伝票種類 */
            this.dto.DENPYOU_SHURUI = int.Parse(this.form.DENPYOU_SHURUI.Text);
            /* 取引区分 */
            this.dto.TORIHIKI_KBN = int.Parse(this.form.TORIHIKI_KBN.Text);
            /* 確定区分 */
            this.dto.KAKUTEI_KBN = int.Parse(this.form.KAKUTEI_KBN.Text);
            /* 締処理状況 */
            this.dto.SHIME_JOKYO = int.Parse(this.form.SHIME_KBN.Text);

            /* 取引先 */
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_FROM.Text))
            {
                // 取引先CD_From
                this.dto.TORIHIKISAKI_CD_FROM = this.form.TORIHIKISAKI_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD_TO.Text))
            {
                // 取引先CD_To
                this.dto.TORIHIKISAKI_CD_TO = this.form.TORIHIKISAKI_CD_TO.Text;
            }

            /* 業者 */
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_FROM.Text))
            {
                // 業者CD_From
                this.dto.GYOUSHA_CD_FROM = this.form.GYOUSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD_TO.Text))
            {
                // 業者CD_To
                this.dto.GYOUSHA_CD_TO = this.form.GYOUSHA_CD_TO.Text;
            }

            /* 現場 */
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_FROM.Text))
            {
                // 現場CD_From
                this.dto.GENBA_CD_FROM = this.form.GENBA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GENBA_CD_TO.Text))
            {
                // 現場CD_To
                this.dto.GENBA_CD_TO = this.form.GENBA_CD_TO.Text;
            }

            /* 営業担当者 */
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD_FROM.Text))
            {
                // 営業担当者CD_From
                this.dto.EIGYOU_TANTOUSHA_CD_FROM = this.form.EIGYOU_TANTOUSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.EIGYOU_TANTOUSHA_CD_TO.Text))
            {
                // 営業担当者CD_To
                this.dto.EIGYOU_TANTOUSHA_CD_TO = this.form.EIGYOU_TANTOUSHA_CD_TO.Text;
            }

            /* 運転者 */
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD_FROM.Text))
            {
                // 運転者CD_From
                this.dto.UNTENSHA_CD_FROM = this.form.UNTENSHA_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD_TO.Text))
            {
                // 運転者CD_To
                this.dto.UNTENSHA_CD_TO = this.form.UNTENSHA_CD_TO.Text;
            }

            /* 品名 */
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD_FROM.Text))
            {
                // 品名CD_From
                this.dto.HINMEI_CD_FROM = this.form.HINMEI_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.HINMEI_CD_TO.Text))
            {
                // 品名CD_To
                this.dto.HINMEI_CD_TO = this.form.HINMEI_CD_TO.Text;
            }

            /* 種類 */
            if (!string.IsNullOrEmpty(this.form.SHURUI_CD_FROM.Text))
            {
                // 種類CD_From
                this.dto.SHURUI_CD_FROM = this.form.SHURUI_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHURUI_CD_TO.Text))
            {
                // 種類CD_To
                this.dto.SHURUI_CD_TO = this.form.SHURUI_CD_TO.Text;
            }

            /* 分類 */
            if (!string.IsNullOrEmpty(this.form.BUNRUI_CD_FROM.Text))
            {
                // 分類CD_From
                this.dto.BUNRUI_CD_FROM = this.form.BUNRUI_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.BUNRUI_CD_TO.Text))
            {
                // 分類CD_To
                this.dto.BUNRUI_CD_TO = this.form.BUNRUI_CD_TO.Text;
            }

            /* 抽出順位 */
            if (!string.IsNullOrEmpty(this.form.RANK.Text))
            {
                // 抽出順位
                this.dto.RANK = Convert.ToInt32(this.form.RANK.Text);
            }

            //PhuocLoc 2020/12/07 #136225 -Start
            /* 集計項目 */
            if (!string.IsNullOrEmpty(this.form.SHUUKEI_KOUMOKU_CD_FROM.Text))
            {
                // 集計項目CD_From
                this.dto.SHUUKEI_KOUMOKU_CD_FROM = this.form.SHUUKEI_KOUMOKU_CD_FROM.Text;
            }
            if (!string.IsNullOrEmpty(this.form.SHUUKEI_KOUMOKU_CD_TO.Text))
            {
                // 集計項目CD_To
                this.dto.SHUUKEI_KOUMOKU_CD_TO = this.form.SHUUKEI_KOUMOKU_CD_TO.Text;
            }
            //PhuocLoc 2020/12/07 #136225 -End

            // データ抽出用SQLの情報設定
            this.dto.SELECT_COLUMN = GetSelectedColumns(true);
            this.dto.GROUP_COLUMN = GetSelectedColumns(false);

            // データ抽出用SQLのソート情報設定
            this.dto.SORT_COLUMN = GetOrderColumns();
        }

        /// <summary>
        /// 鑑と一覧のヘッダー名称を保持するデータテーブルを作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateReportHeaderTable()
        {
            DataTable dt = new DataTable();

            // Column定義
            dt.Columns.Add("TITLE");
            dt.Columns.Add("JISHA");
            dt.Columns.Add("KYOTEN");
            dt.Columns.Add("HAKKOU_DATE");
            dt.Columns.Add("JOUKEN_1");
            dt.Columns.Add("JOUKEN_2");
            dt.Columns.Add("COL_CD_1");
            dt.Columns.Add("COL_NAME_1");
            dt.Columns.Add("COL_CD_2");
            dt.Columns.Add("COL_NAME_2");

            var row = dt.NewRow();

            #region Header項目設定

            // 自社情報マスタを取得して会社略称名を取得
            var jisha = string.Empty;
            var mCorpInfo = DaoInitUtility.GetComponent<IM_CORP_INFODao>().GetAllData().FirstOrDefault();
            if (mCorpInfo != null)
            {
                jisha = mCorpInfo.CORP_RYAKU_NAME ?? string.Empty;
            }

            string title = string.Empty;
            var dto = this.form.PATTERN_LIST_BOX.SelectedItem as PatternDto;
            if (dto != null)
            {
                title = dto.PATTERN_NAME;
            }

            row["TITLE"] = string.Format("売上順位表（{0}）", title);
            row["JISHA"] = jisha;
            row["KYOTEN"] = this.form.KYOTEN_NAME.Text;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //row["HAKKOU_DATE"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
            row["HAKKOU_DATE"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            row["JOUKEN_1"] = CreateJouken1FieldData(this.dto);
            row["JOUKEN_2"] = CreateJouken2FieldData();

            #endregion

            #region PageHeader項目

            var ronriName1 = GetKoumokuRonriName(1);
            var ronriName2 = GetKoumokuRonriName(2);

            var cd1 = string.Empty;
            var name1 = string.Empty;
            var cd2 = string.Empty;
            var name2 = string.Empty;

            if (!string.IsNullOrEmpty(ronriName1))
            {
                cd1 = ronriName1 + "CD";

                string name = string.Empty;
                if (!ronriName1.Equals("品名"))
                {
                    name = "名";
                }

                name1 = ronriName1 + name;
            }

            if (!string.IsNullOrEmpty(ronriName2))
            {
                cd2 = ronriName2 + "CD";

                string name = string.Empty;
                if (!ronriName2.Equals("品名"))
                {
                    name = "名";
                }

                name2 = ronriName2 + name;
            }

            row["COL_CD_1"] = cd1;
            row["COL_NAME_1"] = name1;
            row["COL_CD_2"] = cd2;
            row["COL_NAME_2"] = name2;

            #endregion

            dt.Rows.Add(row);

            return dt;
        }

        /// <summary>
        /// レポート鏡の抽出条件文字列を作成（上段）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private string CreateJouken1FieldData(UriageJunihyoDto dto)
        {
            if (dto == null)
            {
                return string.Empty;
            }

            var conditionSb = new StringBuilder();
            conditionSb.AppendLine("[抽出条件]");

            // 日付
            {
                // 日付種類
                string dateStr = string.Empty;
                if (ConstClass.DATE_CD_DENPYOU.Equals(this.form.HIDUKE_SHURUI.Text))
                {
                    dateStr = ConstClass.DATE_NAME_DENPYOU;
                }
                else if (ConstClass.DATE_CD_URIAGE.Equals(this.form.HIDUKE_SHURUI.Text))
                {
                    dateStr = ConstClass.DATE_NAME_URIAGE;
                }
                else if (ConstClass.DATE_CD_INPUT.Equals(this.form.HIDUKE_SHURUI.Text))
                {
                    dateStr = ConstClass.DATE_NAME_INPUT;
                }

                // 日付範囲
                var dateSb = new StringBuilder();
                if (this.form.HIDUKE_FROM.Value != null)
                {
                    dateSb.Append(((DateTime)this.form.HIDUKE_FROM.Value).ToString("yyyy/MM/dd"));
                }

                dateSb.Append(" ～ ");

                if (this.form.HIDUKE_TO.Value != null)
                {
                    dateSb.Append(((DateTime)this.form.HIDUKE_TO.Value).ToString("yyyy/MM/dd"));
                }

                conditionSb.AppendFormat("　[{0}] ", dateStr).AppendLine(dateSb.ToString());
            }

            // 伝票種類
            {
                string denpyoStr = string.Empty;

                if (ConstClass.DENPYOU_SHURUI_CD_UKEIRE.Equals(this.form.DENPYOU_SHURUI.Text))
                {
                    denpyoStr = ConstClass.DENPYOU_SHURUI_NAME_UKEIRE;
                }
                else if (ConstClass.DENPYOU_SHURUI_CD_SHUKKA.Equals(this.form.DENPYOU_SHURUI.Text))
                {
                    denpyoStr = ConstClass.DENPYOU_SHURUI_NAME_SHUKKA;
                }
                else if (ConstClass.DENPYOU_SHURUI_CD_UR_SH.Equals(this.form.DENPYOU_SHURUI.Text))
                {
                    denpyoStr = ConstClass.DENPYOU_SHURUI_NAME_UR_SH;
                }
                // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
                else if (ConstClass.DENPYOU_SHURUI_CD_DAINOU.Equals(this.form.DENPYOU_SHURUI.Text))
                {
                    denpyoStr = ConstClass.DENPYOU_SHURUI_NAME_DAINOU;
                }
                // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
                else if (ConstClass.DENPYOU_SHURUI_CD_ALL.Equals(this.form.DENPYOU_SHURUI.Text))
                {
                    denpyoStr = ConstClass.DENPYOU_SHURUI_NAME_ALL;
                }

                // ラベル名の必須マーク(※)除去
                var label = this.form.label20.Text.Replace("※", string.Empty);
                conditionSb.AppendFormat("　[{0}] ", label).AppendLine(denpyoStr);
            }

            // 取引区分
            {
                string torihikiStr = string.Empty;

                if (ConstClass.TORIHIKI_KBN_CD_GENKIN.Equals(this.form.TORIHIKI_KBN.Text))
                {
                    torihikiStr = ConstClass.TORIHIKI_KBN_NAME_GENKIN;
                }
                else if (ConstClass.TORIHIKI_KBN_CD_KAKE.Equals(this.form.TORIHIKI_KBN.Text))
                {
                    torihikiStr = ConstClass.TORIHIKI_KBN_NAME_KAKE;
                }
                else if (ConstClass.TORIHIKI_KBN_CD_ALL.Equals(this.form.TORIHIKI_KBN.Text))
                {
                    torihikiStr = ConstClass.TORIHIKI_KBN_NAME_ALL;
                }

                // ラベル名の必須マーク(※)除去
                var label = this.form.label21.Text.Replace("※", string.Empty);
                conditionSb.AppendFormat("　[{0}] ", label).AppendLine(torihikiStr);
            }

            // 確定区分
            {
                string kakuteiStr = string.Empty;

                if (ConstClass.KAKUTEI_KBN_CD_KAKUTEI.Equals(this.form.KAKUTEI_KBN.Text))
                {
                    kakuteiStr = ConstClass.KAKUTEI_KBN_NAME_KAKUTEI;
                }
                else if (ConstClass.KAKUTEI_KBN_CD_MIKAKUTEI.Equals(this.form.KAKUTEI_KBN.Text))
                {
                    kakuteiStr = ConstClass.KAKUTEI_KBN_NAME_MIKAKUTEI;
                }
                else if (ConstClass.KAKUTEI_KBN_CD_ALL.Equals(this.form.KAKUTEI_KBN.Text))
                {
                    kakuteiStr = ConstClass.KAKUTEI_KBN_NAME_ALL;
                }

                // ラベル名の必須マーク(※)除去
                var label = this.form.label22.Text.Replace("※", string.Empty);
                conditionSb.AppendFormat("　[{0}] ", label).AppendLine(kakuteiStr);
            }

            // 締処理状況
            {
                string shimeStr = string.Empty;

                if (ConstClass.SHIME_KBN_CD_ZUMI.Equals(this.form.SHIME_KBN.Text))
                {
                    shimeStr = ConstClass.SHIME_KBN_NAME_ZUMI;
                }
                else if (ConstClass.SHIME_KBN_CD_MISHIME.Equals(this.form.SHIME_KBN.Text))
                {
                    shimeStr = ConstClass.SHIME_KBN_NAME_MISHIME;
                }
                else if (ConstClass.SHIME_KBN_CD_ALL.Equals(this.form.SHIME_KBN.Text))
                {
                    shimeStr = ConstClass.SHIME_KBN_NAME_ALL;
                }

                // ラベル名の必須マーク(※)除去
                var label = this.form.label23.Text.Replace("※", string.Empty);
                conditionSb.AppendFormat("　[{0}] ", label).AppendLine(shimeStr);
            }

            // 抽出順位
            if (0 < dto.RANK)
            {
                conditionSb.AppendFormat("　[{0}] ", this.form.label7.Text)
                           .AppendLine(dto.RANK.ToString());
            }

            // 拠点
            var kyotenLabel = this.form.label6.Text.Replace("※", string.Empty);
            conditionSb.AppendFormat("　[{0}] ", kyotenLabel)
                       .Append(dto.KYOTEN_CD)
                       .Append(" ")
                       .AppendLine(this.form.KYOTEN_NAME.Text);

            // 取引先
            var torihikisakiStr = CreateJouken1Cd(this.form.label9
                                                , dto.TORIHIKISAKI_CD_FROM
                                                , this.form.TORIHIKISAKI_NAME_FROM
                                                , dto.TORIHIKISAKI_CD_TO
                                                , this.form.TORIHIKISAKI_NAME_TO);
            if (!string.IsNullOrEmpty(torihikisakiStr))
            {
                conditionSb.AppendLine(torihikisakiStr);
            }

            // 業者
            var gyoushaStr = CreateJouken1Cd(this.form.label10
                                           , dto.GYOUSHA_CD_FROM
                                           , this.form.GYOUSHA_NAME_FROM
                                           , dto.GYOUSHA_CD_TO
                                           , this.form.GYOUSHA_NAME_TO);
            if (!string.IsNullOrEmpty(gyoushaStr))
            {
                conditionSb.AppendLine(gyoushaStr);
            }

            // 現場
            var genbaStr = CreateJouken1Cd(this.form.label15
                                         , dto.GENBA_CD_FROM
                                         , this.form.GENBA_NAME_FROM
                                         , dto.GENBA_CD_TO
                                         , this.form.GENBA_NAME_TO);
            if (!string.IsNullOrEmpty(genbaStr))
            {
                conditionSb.AppendLine(genbaStr);
            }

            // 営業担当者
            var eigyouStr = CreateJouken1Cd(this.form.label11
                                          , dto.EIGYOU_TANTOUSHA_CD_FROM
                                          , this.form.EIGYOU_TANTOUSHA_NAME_FROM
                                          , dto.EIGYOU_TANTOUSHA_CD_TO
                                          , this.form.EIGYOU_TANTOUSHA_NAME_TO);
            if (!string.IsNullOrEmpty(eigyouStr))
            {
                conditionSb.AppendLine(eigyouStr);
            }

            // 運転者
            var untenshaStr = CreateJouken1Cd(this.form.label12
                                          , dto.UNTENSHA_CD_FROM
                                          , this.form.UNTENSHA_NAME_FROM
                                          , dto.UNTENSHA_CD_TO
                                          , this.form.UNTENSHA_NAME_TO);
            if (!string.IsNullOrEmpty(untenshaStr))
            {
                conditionSb.AppendLine(untenshaStr);
            }

            // 品名
            var hinmeiStr = CreateJouken1Cd(this.form.label13
                                          , dto.HINMEI_CD_FROM
                                          , this.form.HINMEI_NAME_FROM
                                          , dto.HINMEI_CD_TO
                                          , this.form.HINMEI_NAME_TO);
            if (!string.IsNullOrEmpty(hinmeiStr))
            {
                conditionSb.AppendLine(hinmeiStr);
            }

            // 種類
            var shuruiStr = CreateJouken1Cd(this.form.label13
                                          , dto.SHURUI_CD_FROM
                                          , this.form.SHURUI_NAME_FROM
                                          , dto.SHURUI_CD_TO
                                          , this.form.SHURUI_NAME_TO);
            if (!string.IsNullOrEmpty(shuruiStr))
            {
                conditionSb.AppendLine(shuruiStr);
            }

            // 分類
            var bunruiStr = CreateJouken1Cd(this.form.label13
                                          , dto.BUNRUI_CD_FROM
                                          , this.form.BUNRUI_NAME_FROM
                                          , dto.BUNRUI_CD_TO
                                          , this.form.BUNRUI_NAME_TO);
            if (!string.IsNullOrEmpty(bunruiStr))
            {
                conditionSb.AppendLine(bunruiStr);
            }

            //PhuocLoc 2020/12/07 #136225 -Start
            // 集計項目
            var shuukeiKoumokuStr = CreateJouken1Cd(this.form.label45
                                          , dto.SHUUKEI_KOUMOKU_CD_FROM
                                          , this.form.SHUUKEI_KOUMOKU_NAME_FROM
                                          , dto.SHUUKEI_KOUMOKU_CD_TO
                                          , this.form.SHUUKEI_KOUMOKU_NAME_TO);
            if (!string.IsNullOrEmpty(shuukeiKoumokuStr))
            {
                conditionSb.AppendLine(shuukeiKoumokuStr);
            }
            //PhuocLoc 2020/12/07 #136225 -End

            return conditionSb.ToString();
        }

        /// <summary>
        /// レポート鏡の抽出条件文字列を作成（下段）
        /// </summary>
        /// <returns></returns>
        private string CreateJouken2FieldData()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[集計項目]");
            sb.Append("　[1] ");
            sb.AppendLine(GetKoumokuRonriName(1));
            sb.Append("　[2] ");
            sb.AppendLine(GetKoumokuRonriName(2));

            sb.AppendLine(string.Empty);
            sb.AppendLine(string.Empty);

            sb.AppendLine("[明細項目]");
            sb.Append("　[1] ");
            sb.AppendLine("金額");

            return sb.ToString();
        }

        /// <summary>
        /// 抽出条件のコード(To ～ From)用の文言作成
        /// </summary>
        /// <param name="label">ラベル</param>
        /// <param name="cdFrom">コードFrom</param>
        /// <param name="nameFrom">名称From</param>
        /// <param name="cdTo">コードTo</param>
        /// <param name="nameTo">名称To</param>
        /// <returns></returns>
        private string CreateJouken1Cd(Label label, string cdFrom, CustomTextBox nameFrom, string cdTo, CustomTextBox nameTo)
        {
            if (string.IsNullOrEmpty(cdFrom) && string.IsNullOrEmpty(cdTo))
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.AppendFormat("　[{0}] ", label.Text);

            if (!string.IsNullOrEmpty(cdFrom))
            {
                sb.Append(cdFrom)
                    .Append(" ")
                    .Append(nameFrom.Text);
            }

            sb.Append(" ～ ");

            if (!string.IsNullOrEmpty(cdTo))
            {
                sb.Append(cdTo)
                    .Append(" ")
                    .Append(nameTo.Text);
            }

            return sb.ToString();
        }

        /// <summary>
        /// パターンで選択されたカラム名を取得
        /// </summary>
        /// <param name="isSelect">true:SELECT用, false:GROUP BY用</param>
        /// <returns></returns>
        private string GetSelectedColumns(bool addName)
        {
            var cd1 = GetButsuriName(1);
            var cd2 = GetButsuriName(2);

            // パターン登録で選択されたカラム名を取得
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(cd1))
            {
                var name = cd1.Replace("_CD", "_NAME");
                var asName = string.Empty;
                var asCd = string.Empty;

                if (addName)
                {
                    // SELECT句用に別名定義
                    asCd = " AS CD_1";
                    asName = " AS NAME_1";
                }

                sb.AppendFormat(",DENPYOU_DATA.{0}{1}", cd1, asCd);
                sb.AppendFormat(",DENPYOU_DATA.{0}{1}", name, asName);
            }
            if (!string.IsNullOrEmpty(cd2))
            {
                var name = cd2.Replace("_CD", "_NAME");
                var asName = string.Empty;
                var asCd = string.Empty;

                if (addName)
                {
                    // SELECT句用に別名定義
                    asCd = " AS CD_2";
                    asName = " AS NAME_2";
                }

                sb.AppendFormat(",DENPYOU_DATA.{0}{1}", cd2, asCd);
                sb.AppendFormat(",DENPYOU_DATA.{0}{1}", name, asName);
            }
            else
            {
                if (addName)
                {
                    // 空の項目として取得
                    sb.Append(", '' AS CD_2");
                    sb.Append(", '' AS NAME_2");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 物理名を取得する
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns></returns>
        private string GetButsuriName(int shuukeiKoumokuNo)
        {
            var patternDto = this.form.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            if (patternDto == null)
            {
                return string.Empty;
            }

            var select = patternDto.GetColumnSelectDetail(shuukeiKoumokuNo);
            if (select == null)
            {
                return string.Empty;
            }

            return select.BUTSURI_NAME;
        }

        /// <summary>
        /// 項目論理名を取得する
        /// </summary>
        /// <param name="shuukeiKoumokuNo">集計項目番号</param>
        /// <returns></returns>
        private string GetKoumokuRonriName(int shuukeiKoumokuNo)
        {
            var patternDto = this.form.PATTERN_LIST_BOX.SelectedItem as PatternDto;

            if (patternDto == null)
            {
                return string.Empty;
            }

            var select = patternDto.GetColumnSelect(shuukeiKoumokuNo);
            if (select == null)
            {
                return string.Empty;
            }

            return select.KOUMOKU_RONRI_NAME;
        }

        /// <summary>
        /// ソート用文字列を取得します
        /// </summary>
        /// <returns>ソート用文字列</returns>
        private string GetOrderColumns()
        {
            StringBuilder sb = new StringBuilder();

            var cd1 = GetButsuriName(1);
            var cd2 = GetButsuriName(2);

            sb.Append("RANK");

            if (!string.IsNullOrEmpty(cd1))
            {
                sb.Append(",CD_1");
            }

            if (!string.IsNullOrEmpty(cd2))
            {
                sb.Append(",CD_2");
            }

            return sb.ToString();
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
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
    }

}
