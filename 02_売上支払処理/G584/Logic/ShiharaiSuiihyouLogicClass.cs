using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.SalesPayment.ShiharaiSuiiChouhyou
{
    /// <summary>
    /// G584 支払推移票 ビジネスロジック
    /// </summary>
    internal class ShiharaiSuiihyouLogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン情報を格納しているＸＭＬファイルのパス（リソース）を保持するフィールド
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.SalesPayment.ShiharaiSuiiChouhyou.Setting.ShiharaiSuiihyouButtonSetting.xml";

        private HeaderBaseForm header;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        private M_CORP_INFO[] entitys;

        /// <summary>
        /// 自社情報入力のDao
        /// </summary>
        private IM_CORP_INFODao daoCorpInfo;

        //CSV/帳票出力
        internal bool printFlg = true;

        // 項目１－４
        internal List<string> nameList;

        // 年月
        internal List<string> dateList;

        /// <summary>
        /// フォーム
        /// </summary>
        private UIForm_ShiharaiSuiihyou form;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>Initializes a new instance of the 
        /// <see cref="UriageShiharaiMeisaihyouLogicClass"/> class.</summary>
        /// <param name="targetForm">targetForm</param>
        public ShiharaiSuiihyouLogicClass(UIForm_ShiharaiSuiihyou targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // 日付設定
                this.daoCorpInfo = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
                this.entitys = daoCorpInfo.GetAllData();
                this.form.DATE_FROM.Text = this.parentForm.sysDate.Year + "/" + ((int)this.entitys[0].KISHU_MONTH).ToString("00") + "/01";
                this.form.DATE_TO.Text = DateTime.Parse(this.form.DATE_FROM.Text).AddMonths(12).AddDays(-1.0).ToString("yyyy/MM/dd");
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
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
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderBaseForm targetHeader = (HeaderBaseForm)parentForm.headerForm;
            this.header = targetHeader;
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId) + " 出力画面";

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public int Search()
        {
            var dto = this.form.FormDataDto;
            var dao = DaoInitUtility.GetComponent<IShiharaiSuiiHyouDao>();
            var dt = new DataTable();
            try
            {
                string sql;
                if (CreateSuiiHyouDataQuery(out sql))
                {
                    dt = dao.GetSuiiHyouDataShiharai(sql);
                }

                if (0 < dt.Rows.Count)
                {
                    if(this.printFlg)
                    {
                        var reportLogic = new UriageSuiihyouReportClass();
                        reportLogic.CreateReport(dt, dto);
                    }
                    else
                    {
                        //CSV出力
                        this.CSVPrint(dt, dto);
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
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return -1;
            }

            return dt.Rows.Count;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CSVPrint(DataTable dt, ShiharaiSuiihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            // ヘッダー情報のDataTableを作成
            this.CreateHeaderDt(dto);
            string headStr = "";

            string[] csvHead;

            DataTable csvDT = new DataTable();
            DataRow rowTmp;

            if (this.nameList != null && this.nameList.Count > 0)
            {
                for (int i = 0; i < this.nameList.Count; i++)
                {
                    if (this.nameList[i] != null && !string.IsNullOrEmpty(this.nameList[i].ToString()))
                    {
                        headStr = headStr + this.nameList[i].ToString() + ",";
                    }
                }
            }

            if (this.dateList != null && this.dateList.Count > 0)
            {
                for (int i = 8; i < this.dateList.Count + 8; i++)
                {
                    if (this.dateList[i - 8] != null && !string.IsNullOrEmpty(this.dateList[i - 8].ToString()))
                    {
                        headStr = headStr + this.dateList[i - 8].ToString() + ",";
                    }
                }
            }

            headStr = headStr + "合計" + ",";

            headStr = headStr + "月平均";

            csvHead = headStr.Split(',');
            for (int i = 0; i < csvHead.Length; i++)
            {
                csvDT.Columns.Add(csvHead[i]);
            }

            // レポート定義ファイルをLoad
            var chouhyouDataTable = this.CreateChouhyouDt(dt, dto);
            foreach (DataRow dataRow in chouhyouDataTable.Rows)
            {
                rowTmp = csvDT.NewRow();

                if(this.nameList.Count/2 >= 1)
                {
                    rowTmp[this.nameList[0].ToString()] = dataRow["CD_1"];
                    rowTmp[this.nameList[1].ToString()] = dataRow["NAME_1"];
                }

                if (this.nameList.Count / 2 >= 2)
                {
                    rowTmp[this.nameList[2].ToString()] = dataRow["CD_2"];
                    rowTmp[this.nameList[3].ToString()] = dataRow["NAME_2"];
                }

                if (this.nameList.Count / 2 >= 3)
                {
                    rowTmp[this.nameList[4].ToString()] = dataRow["CD_3"];
                    rowTmp[this.nameList[5].ToString()] = dataRow["NAME_3"];
                }

                if (this.nameList.Count / 2 == 4)
                {
                    rowTmp[this.nameList[6].ToString()] = dataRow["CD_4"];
                    rowTmp[this.nameList[7].ToString()] = dataRow["NAME_4"];
                }

                for (int i = 8; i < dto.Pivot.Count + 8; i++)
                {
                    int j = i - 7;
                    rowTmp[this.dateList[i - 8].ToString()] = dataRow["VAL_" + j];
                }

                rowTmp["合計"] = dataRow["VAL_TOTAL"];
                rowTmp["月平均"] = dataRow["VAL_AVG"];

                csvDT.Rows.Add(rowTmp);

            }

            // 一覧に明細行がない場合、アラートを表示し、CSV出力処理はしない
            if (csvDT.Rows.Count == 0)
            {
                this.errmessage.MessageBoxShow("E044");
                return;
            }
            this.form.CsvReport(csvDT);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ヘッダー用CSV作成
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private void CreateHeaderDt(ShiharaiSuiihyouDtoClass dto)
        {
            string[] csvHead = new string[18];

            // PageHeader項目の値
            // 項目1～4
            this.nameList = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                if (dto.Pattern.ColumnSelectList.Count > i)
                {
                    this.nameList.Add(dto.Pattern.ColumnSelectList[i].KOUMOKU_RONRI_NAME + "CD");
                    this.nameList.Add(dto.Pattern.ColumnSelectList[i].KOUMOKU_RONRI_NAME);
                }
            }

            // 年月
            this.dateList = new List<string>();
            for (int x = 0; x < 12; x++)
            {
                if (dto.Pivot.Count > x)
                {
                    dateList.Add(dto.Pivot[x].ToString());
                }
                else
                {
                    dateList.Add(string.Empty);
                }
            }
        }

        /// <summary>
        /// CSVダーた用作成
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public DataTable CreateChouhyouDt(DataTable dt, ShiharaiSuiihyouDtoClass dto)
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("CD_1");
            retDt.Columns.Add("NAME_1");
            retDt.Columns.Add("CD_2");
            retDt.Columns.Add("NAME_2");
            retDt.Columns.Add("CD_3");
            retDt.Columns.Add("NAME_3");
            retDt.Columns.Add("CD_4");
            retDt.Columns.Add("NAME_4");

            for (int i = 0; i < 12; i++)
            {
                retDt.Columns.Add("VAL_" + (i + 1));
            }

            retDt.Columns.Add("VAL_TOTAL");
            retDt.Columns.Add("VAL_AVG");

            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = retDt.NewRow();

                // 集計項目
                for (int x = 0; x < 4; x++)
                {
                    string col = "CD_" + (x + 1);
                    string name = "NAME_" + (x + 1);

                    dr[col] = string.Empty;
                    dr[name] = string.Empty;

                    if (dto.Select.Count > x)
                    {
                        if (dto.Select[x].ToString() != "DAIKAN_KBN")
                        {
                            dr[col] = row[dto.Select[x].ToString()];
                            dr[name] = row[dto.Select[x].ToString().Replace("_CD", "_NAME")];
                        }
                        else
                        {
                            dr[col] = row[dto.Select[x].ToString() + "_CD"];
                            dr[name] = row[dto.Select[x].ToString() + "_NAME"];
                        }
                    }
                }

                // 月別の値
                for (int i = 0; i < 12; i++)
                {
                    string colName = "VAL_" + (i + 1);
                    dr[colName] = string.Empty;
                    if (dto.Pivot.Count > i)
                    {
                        dr[colName] = row[dto.Pivot[i].ToString()].ToString();
                    }
                }

                dr["VAL_TOTAL"] = row["SUM_KINGAKU"].ToString();
                dr["VAL_AVG"] = row["AVR"].ToString();

                retDt.Rows.Add(dr);
            }

            return retDt;
        }

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>登録処理を実行する</summary>
        /// <param name="errorFlag">かどうかを表す値</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>更新処理を実行する</summary>
        /// <param name="errorFlag">エラーフラグかどうかを表す値</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return null;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            try
            {
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                var parentForm = (BusinessBaseForm)this.form.Parent;

                this.form.C_Regist(parentForm.bt_func7);
                // 新規
                parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);
                // 修正
                parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);
                // 削除
                parentForm.bt_func4.Click += new EventHandler(this.form.ButtonFunc4_Clicked);
                // CSV
                this.form.C_Regist(parentForm.bt_func5);
                parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);
                // 表示
                parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);
                // 閉じる
                parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>
        /// 車輌リストを取得します
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns>車輌リスト</returns>
        internal List<M_SHARYOU> GetSharyou(String sharyouCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(sharyouCd);

            var ret = new List<M_SHARYOU>();
            catchErr = true;
            try
            {
                var dao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                var keyEntity = new M_SHARYOU();
                keyEntity.SHARYOU_CD = sharyouCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                ret = dao.GetAllValidData(keyEntity).ToList();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSharyou", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);
            return ret;
        }

        /// <summary>
        /// 現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場</returns>
        internal M_GENBA GetGenba(String gyoushaCd, String genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();

            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 入力担当者を取得します
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns>入力担当者</returns>
        internal M_SHAIN GetNyuuryokuTantousha(String shainCd)
        {
            LogUtility.DebugMethodStart(shainCd);

            M_SHAIN ret = null;

            var dao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            ret = dao.GetAllValidData(new M_SHAIN() { SHAIN_CD = shainCd, NYUURYOKU_TANTOU_KBN = true, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 荷積降業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>荷積降現場</returns>
        internal M_GYOUSHA GetNizumioroshiGyousha(String gyoushaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd);

            M_GYOUSHA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            M_GYOUSHA entity = new M_GYOUSHA();
            entity.GYOUSHA_CD = gyoushaCd;
            entity.ISNOT_NEED_DELETE_FLG = true;
            
            // 区分に関係なく取得する
            ret = dao.GetAllValidData(entity).FirstOrDefault();
            
            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 荷積降現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>荷積降現場</returns>
        internal M_GENBA GetNizumioroshiGenba(String gyoushaCd, String genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            M_GENBA ret = null;

            var dao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // 区分に関係なく取得する
            ret = dao.GetAllValidData(new M_GENBA() { GYOUSHA_CD = gyoushaCd, GENBA_CD = genbaCd, ISNOT_NEED_DELETE_FLG = true }).FirstOrDefault();

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate(out long monthDiff, out string strSelectDate, out ArrayList arrayPiv)
        {
            monthDiff = 0;
            strSelectDate = string.Empty;
            arrayPiv = new ArrayList();
            try
            {

                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();

                DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

                // 日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.DATE_FROM.IsInputErrorOccured = true;
                    this.form.DATE_TO.IsInputErrorOccured = true;
                    this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;

                    if (this.form.DATE_SHURUI_1.Checked)
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else if (this.form.DATE_SHURUI_2.Checked)
                    {
                        string[] errorMsg = { "支払日付From", "支払日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    else
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        msglogic.MessageBoxShow("E030", errorMsg);
                    }
                    this.form.DATE_FROM.Focus();
                    return false;
                }

                // 12か月を上回っている場合
                DateTime fromBeginMonth = DateTime.Parse(date_from.Year.ToString() + "/" + date_from.Month.ToString("00") + "/01");
                DateTime toBeginMonth = DateTime.Parse(date_to.Year.ToString() + "/" + date_to.Month.ToString("00") + "/01");

                monthDiff = DateAndTime.DateDiff(DateInterval.Month, fromBeginMonth, toBeginMonth);

                if (monthDiff > 11)
                {
                    string strMsg = "12カ月を超える日付範囲は指定出来ません。";
                    MessageBox.Show(strMsg, "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.form.DATE_TO.Focus();
                    return false;
                }
                else
                {
                    // 12カ月分の文字列を作成
                    for (int i = 0; i < monthDiff + 1; i++)
                    {
                        DateTime dt = date_from.AddMonths(i);
                        arrayPiv.Add(dt.Year + "/" + dt.Month.ToString("00"));
                        strSelectDate += "REPLACE(CONVERT(VARCHAR, CONVERT(MONEY, ISNULL(\"" + dt.Year + "/" + dt.Month.ToString("00")
                                      + "\",0)),1),'.00','') " + "\"" + dt.Year + "/" + dt.Month.ToString("00") + "\",";
                    }

                    strSelectDate = strSelectDate.TrimEnd(',');
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        /// <summary>
        /// SQL作成
        /// </summary>
        /// <returns></returns>
        private bool CreateSuiiHyouDataQuery(out string sql)
        {
            sql = string.Empty;
            string BaseSql = CreateBaseSql(string.Empty);

            if (string.IsNullOrEmpty(BaseSql))
            {
                return false;
            }

            // 一番外側のSELECT句(マスタから名称取得用)
            StringBuilder joinSelect = new StringBuilder();
            // 一番外側のJOIN句(マスタから名称取得用)
            StringBuilder joinFrom = new StringBuilder();
            // 内側のSELECT句
            StringBuilder selectIn = new StringBuilder();
            // ORDER句
            string order = string.Empty;

            // 業者カウント
            int gyoushaCnt = 1;
            // 現場カウント
            int genbaCnt = 1;
            // 社員カウント
            int shainCnt = 1;

            foreach (string col in this.form.FormDataDto.Select)
            {
                switch (col)
                {
                    case "SHIHARAI_TORIHIKI_KBN_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_TORIHIKI_KBN.TORIHIKI_KBN_NAME SHIHARAI_TORIHIKI_KBN_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_TORIHIKI_KBN ON M_TORIHIKI_KBN.TORIHIKI_KBN_CD = MAIN.SHIHARAI_TORIHIKI_KBN_CD ");
                        break;
                    case "KYOTEN_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_KYOTEN.KYOTEN_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_KYOTEN ON M_KYOTEN.KYOTEN_CD = MAIN.KYOTEN_CD ");
                        break;
                    case "KEITAI_KBN_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_KEITAI_KBN.KEITAI_KBN_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_KEITAI_KBN ON M_KEITAI_KBN.KEITAI_KBN_CD = MAIN.KEITAI_KBN_CD ");
                        break;
                    case "DAIKAN_KBN":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" " + col + " DAIKAN_KBN_CD,")
                            .AppendLine(" CASE DAIKAN_KBN WHEN 1 THEN '自社' WHEN 2 THEN '他社' ELSE '' END DAIKAN_KBN_NAME, ");
                        break;
                    case "SHURUI_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_SHURUI.SHURUI_NAME_RYAKU SHURUI_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_SHURUI ON M_SHURUI.SHURUI_CD = MAIN.SHURUI_CD ");
                        break;
                    case "BUNRUI_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_BUNRUI.BUNRUI_NAME_RYAKU BUNRUI_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_BUNRUI ON M_BUNRUI.BUNRUI_CD = MAIN.BUNRUI_CD ");
                        break;
                    case "TORIHIKISAKI_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU TORIHIKISAKI_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_TORIHIKISAKI ON M_TORIHIKISAKI.TORIHIKISAKI_CD = MAIN.TORIHIKISAKI_CD ");
                        break;
                    case "GYOUSHA_CD":
                    case "NIOROSHI_GYOUSHA_CD":
                    case "NIZUMI_GYOUSHA_CD":
                    case "UNPAN_GYOUSHA_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" G" + gyoushaCnt + ".GYOUSHA_NAME_RYAKU " + col.Replace("CD", "NAME") + ",");
                        joinFrom.AppendLine(" LEFT JOIN M_GYOUSHA G" + gyoushaCnt + " ON G" + gyoushaCnt + ".GYOUSHA_CD = MAIN." + col);
                        gyoushaCnt++;
                        break;
                    case "GENBA_CD":
                    case "NIOROSHI_GENBA_CD":
                    case "NIZUMI_GENBA_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" GE" + genbaCnt + ".GENBA_NAME_RYAKU " + col.Replace("CD", "NAME") + ",");
                        joinFrom.AppendLine(" LEFT JOIN M_GENBA GE" + genbaCnt + " ON GE" + genbaCnt + ".GYOUSHA_CD = MAIN." + col.Replace("GENBA_CD", "GYOUSHA_CD") + " AND GE" + genbaCnt + ".GENBA_CD = MAIN." + col);
                        genbaCnt++;
                        break;
                    case "HINMEI_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_HINMEI.HINMEI_NAME_RYAKU HINMEI_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_HINMEI ON M_HINMEI.HINMEI_CD = MAIN.HINMEI_CD ");
                        break;
                    case "EIGYOU_TANTOUSHA_CD":
                    case "NYUURYOKU_TANTOUSHA_CD":
                    case "UNTENSHA_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" S" + shainCnt + ".SHAIN_NAME_RYAKU " + col.Replace("CD", "NAME") + ",");
                        joinFrom.AppendLine(" LEFT JOIN M_SHAIN S" + shainCnt + " ON S" + shainCnt + ".SHAIN_CD = MAIN." + col);
                        shainCnt++;
                        break;
                    case "SHARYOU_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_SHARYOU.SHARYOU_NAME_RYAKU SHARYOU_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_SHARYOU ON M_SHARYOU.GYOUSHA_CD = MAIN.UNPAN_GYOUSHA_CD AND M_SHARYOU.SHARYOU_CD = MAIN.SHARYOU_CD ");
                        break;
                    case "SHASHU_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_SHASHU.SHASHU_NAME_RYAKU SHASHU_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_SHASHU ON M_SHASHU.SHASHU_CD = MAIN.SHASHU_CD ");
                        break;
                    //PhuocLoc 2020/12/07 #136227 -Start
                    case "MOD_SHUUKEI_KOUMOKU_CD":
                        selectIn.Append(col + ",");
                        joinSelect.AppendLine(" MAIN." + col + ",")
                            .AppendLine(" M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU AS MOD_SHUUKEI_KOUMOKU_NAME,");
                        joinFrom.AppendLine(" LEFT JOIN M_SHUUKEI_KOUMOKU ON M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_CD = MAIN.MOD_SHUUKEI_KOUMOKU_CD ");
                        break;
                    //PhuocLoc 2020/12/07 #136227 -End
                    default:
                        selectIn.Append(col + ",")
                            .Append(col.Replace("_CD", "_NAME")).Append(",");
                        joinSelect.AppendLine(" " + col + ",")
                            .AppendLine(" " + col.Replace("_CD", "_NAME") + ",");
                        break;
                }

                order += col + ",";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Empty);
            sb.AppendLine("SELECT ");
            sb.Append(joinSelect.ToString());
            sb.AppendLine(" \"" + string.Join("\",\"", this.form.FormDataDto.Pivot.ToArray()) + "\"");
            sb.AppendLine(" ,SUM_KINGAKU ");
            sb.AppendLine(" ,AVR ");
            sb.AppendLine("FROM ");
            sb.AppendLine("( ");
            sb.AppendLine(" SELECT ");
            sb.AppendLine("   PIV.* ");
            sb.AppendLine("  ,REPLACE(CONVERT(VARCHAR, CONVERT(MONEY, SUM_TABLE.SUM_KINGAKU), 1),'.00','') SUM_KINGAKU ");
            //sb.AppendLine("  ,REPLACE(CONVERT(VARCHAR, CONVERT(MONEY, (SUM_TABLE.SUM_KINGAKU / " + (this.form.FormDataDto.MonthCount) + ")),1),'.00','') AVR ");
            sb.AppendLine("  ,REPLACE(CONVERT(VARCHAR, CONVERT(MONEY, ROUND((SUM_TABLE.SUM_KINGAKU / " + (this.form.FormDataDto.MonthCount) + "), 0)),1),'.00','') AVR ");
            sb.AppendLine(" FROM ");
            sb.AppendLine(" ( ");
            sb.AppendLine("  SELECT ");
            sb.AppendLine("   " + selectIn.ToString());
            sb.AppendLine("   " + this.form.FormDataDto.SelectDate);
            sb.Append("  ,ROW_NUMBER() OVER (ORDER BY ").Append(selectIn.ToString().TrimEnd(',')).AppendLine(" ) ROWNUM ");
            sb.AppendLine("  FROM ");
            sb.AppendLine("  ( ");
            sb.Append(CreateBaseSql(selectIn.ToString()));
            sb.AppendLine(" ) TEMP PIVOT (SUM(KINGAKU) FOR DATE in (" + "\"" + string.Join("\",\"", this.form.FormDataDto.Pivot.ToArray()) + "\"" + ")) PV) PIV, ");
            sb.AppendLine(" ( ");
            sb.AppendLine("  SELECT ");
            sb.AppendLine("   " + selectIn.ToString());
            sb.Append("   ROW_NUMBER() OVER (ORDER BY ").Append(selectIn.ToString().TrimEnd(',')).AppendLine(" ) ROWNUM ");
            sb.AppendLine("  ,SUM(KINGAKU) SUM_KINGAKU ");
            sb.AppendLine("  FROM ");
            sb.AppendLine("  ( ");
            sb.Append(CreateBaseSql(selectIn.ToString()));
            sb.AppendLine("  ) A ");
            sb.AppendLine("  GROUP BY ");
            sb.AppendLine("   " + selectIn.ToString().TrimEnd(','));
            sb.AppendLine(" ) SUM_TABLE ");
            sb.AppendLine(" WHERE ");

            string where = "  PIV.ROWNUM = SUM_TABLE.ROWNUM ";
            //string[] selectInArray = selectIn.ToString().TrimEnd(',').Split(',');
            //for (int i = 0; i < selectInArray.Count(); i++)
            //{
            //    where += string.Format(" AND ISNULL(PIV.{0}, '') = ISNULL(SUM_TABLE.{0}, '')", selectInArray[i]);
            //}

            sb.AppendLine(where);
            sb.AppendLine(") MAIN ");
            sb.Append(joinFrom.ToString());
            sb.AppendLine("ORDER BY ");
            sb.AppendLine(" " + order.TrimEnd(','));

            sql = sb.ToString();

            return true;
        }

        /// <summary>
        /// 各伝票種類をUNIONしたベースのSQL
        /// </summary>
        /// <returns></returns>
        private string CreateBaseSql(string selectGroupBy)
        {
            StringBuilder sbUnion = new StringBuilder();
            if (this.form.FormDataDto.DenpyouShuruiCd == 1 || this.form.FormDataDto.DenpyouShuruiCd == 5)
            {
                if (String.IsNullOrEmpty(this.form.FormDataDto.NizumiGyoushaCdFrom) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.NizumiGyoushaCdTo) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.NizumiGenbaCdFrom) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.NizumiGenbaCdTo))
                {
                    sbUnion.Append(CreateUkeireBaseSql()).Append(CreateBaseWhere(1));
                }
            }
            if (this.form.FormDataDto.DenpyouShuruiCd == 2 || this.form.FormDataDto.DenpyouShuruiCd == 5)
            {
                if (String.IsNullOrEmpty(this.form.FormDataDto.NioroshiGyoushaCdFrom) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.NioroshiGyoushaCdTo) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.NioroshiGenbaCdFrom) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.NioroshiGenbaCdTo))
                {
                    if (!String.IsNullOrEmpty(sbUnion.ToString()))
                    {
                        sbUnion.AppendLine("    UNION ALL ");
                    }
                    sbUnion.Append(CreateShukkaBaseSql());
                }
            }
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
            if (this.form.FormDataDto.DenpyouShuruiCd == 3 ||
                this.form.FormDataDto.DenpyouShuruiCd == 4 ||
                this.form.FormDataDto.DenpyouShuruiCd == 5)
            {
                if (String.IsNullOrEmpty(this.form.FormDataDto.DaikanCdFrom) &&
                    String.IsNullOrEmpty(this.form.FormDataDto.DaikanCdTo))
                {
                    if (!String.IsNullOrEmpty(sbUnion.ToString()))
                    {
                        sbUnion.AppendLine("    UNION ALL ");
                    }
                    sbUnion.Append(CreateShiharaiBaseSql(this.form.FormDataDto.DenpyouShuruiCd)).Append(CreateBaseWhere(3));
                }
            }
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End

            if (string.IsNullOrEmpty(sbUnion.ToString()))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("   SELECT ");
            sb.AppendLine("     " + selectGroupBy + "DATE");
            sb.AppendLine("    ,SUM(KINGAKU) KINGAKU ");
            sb.AppendLine("   FROM ");
            sb.AppendLine("   ( ");
            sb.Append(sbUnion);
            sb.AppendLine("   ) BASE ");
            sb.AppendLine("   GROUP BY ");
            sb.AppendLine("    " + selectGroupBy + "DATE");

            return sb.ToString();
        }

        /// <summary>
        /// 受入のベースSQL
        /// </summary>
        /// <returns></returns>
        private string CreateUkeireBaseSql()
        {
            StringBuilder baseSql = new StringBuilder();

            baseSql.AppendLine("    SELECT ");
            foreach (string select in this.form.FormDataDto.Select)
            {
                string s = "     " + select + ",";
                if (select == "NIZUMI_GYOUSHA_CD" || select == "NIZUMI_GENBA_CD")
                {
                    //s = "     '' " + s + "'' " + select.Replace("_CD", "_NAME") + ",";
                    s = "     '' " + s;
                }
                else
                {
                    if (select == "HINMEI_CD")
                    {
                        s = "     CONVERT(NVARCHAR, DETAIL." + select + ") " + select + ",";
                    }
                    else
                    {
                        s = "     CONVERT(NVARCHAR, " + select + ") " + select + ",";
                    }
                }
                baseSql.AppendLine(s);
            }
            baseSql.Append("     SUBSTRING(CONVERT(VARCHAR(10), ENTRY.");
            if (this.form.FormDataDto.DateShuruiCd == 1)
            {
                baseSql.Append("DENPYOU_DATE");
            }
            else if (this.form.FormDataDto.DateShuruiCd == 2)
            {
                baseSql.Append("URIAGE_DATE");
            }
            else
            {
                baseSql.Append("UPDATE_DATE");
            }
            baseSql.AppendLine(", 111), 1 , 7) DATE");
            baseSql.AppendLine("    ,(ISNULL(DETAIL.KINGAKU, 0) + ISNULL(DETAIL.HINMEI_KINGAKU, 0)) - (ISNULL(DETAIL.TAX_UCHI, 0) + ISNULL(DETAIL.HINMEI_TAX_UCHI, 0)) KINGAKU ");
            baseSql.AppendLine("    FROM ");
            baseSql.AppendLine("     T_UKEIRE_ENTRY ENTRY ");
            baseSql.AppendLine("     INNER JOIN ");
            baseSql.AppendLine("     T_UKEIRE_DETAIL DETAIL ");
            baseSql.AppendLine("     ON ENTRY.SYSTEM_ID = DETAIL.SYSTEM_ID ");
            baseSql.AppendLine("      AND ENTRY.SEQ = DETAIL.SEQ ");
            baseSql.AppendLine("     LEFT JOIN M_HINMEI ");
            baseSql.AppendLine("     ON DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD ");
            baseSql.AppendLine("     LEFT JOIN ");
            baseSql.AppendLine("     ( ");
            baseSql.AppendLine("       SELECT ");
            baseSql.AppendLine("         DENPYOU.SEISAN_NUMBER ");
            baseSql.AppendLine("        ,DETAIL.DENPYOU_SYSTEM_ID ");
            baseSql.AppendLine("        ,DETAIL.DENPYOU_SEQ ");
            baseSql.AppendLine("        ,DETAIL.DETAIL_SYSTEM_ID ");
            baseSql.AppendLine("        ,DETAIL.DENPYOU_NUMBER ");
            baseSql.AppendLine("       FROM ");
            baseSql.AppendLine("        T_SEISAN_DENPYOU DENPYOU ");
            baseSql.AppendLine("        JOIN T_SEISAN_DENPYOU_KAGAMI KAGAMI ");
            baseSql.AppendLine("        ON DENPYOU.SEISAN_NUMBER = KAGAMI.SEISAN_NUMBER ");
            baseSql.AppendLine("        JOIN T_SEISAN_DETAIL DETAIL ");
            baseSql.AppendLine("        ON KAGAMI.SEISAN_NUMBER = DETAIL.SEISAN_NUMBER ");
            baseSql.AppendLine("         AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER ");
            baseSql.AppendLine("       WHERE DETAIL.DENPYOU_SHURUI_CD = 1 ");
            baseSql.AppendLine("        AND DENPYOU.DELETE_FLG = 0 ");
            baseSql.AppendLine("     ) SEISAN ");
            baseSql.AppendLine("     ON SEISAN.DENPYOU_SYSTEM_ID = DETAIL.SYSTEM_ID ");
            baseSql.AppendLine("      AND SEISAN.DENPYOU_SEQ = DETAIL.SEQ ");
            baseSql.AppendLine("      AND SEISAN.DETAIL_SYSTEM_ID = DETAIL.DETAIL_SYSTEM_ID ");
            baseSql.AppendLine("      AND SEISAN.DENPYOU_NUMBER = DETAIL.UKEIRE_NUMBER");
            baseSql.AppendLine("    WHERE");
            baseSql.AppendLine("         ENTRY.DELETE_FLG = 0 ");
            baseSql.AppendLine("     AND ENTRY.TAIRYUU_KBN = 0 ");
            baseSql.AppendLine("     AND DETAIL.DENPYOU_KBN_CD = 2");

            return baseSql.ToString();
        }

        /// <summary>
        /// 出荷のベースSQL
        /// </summary>
        /// <returns></returns>
        private string CreateShukkaBaseSql()
        {
            StringBuilder baseSql = new StringBuilder();

            // 検収入力を行わない
            baseSql.AppendLine("    SELECT ");
            foreach (string select in this.form.FormDataDto.Select)
            {
                string s = "     " + select + ",";
                if (select == "NIOROSHI_GYOUSHA_CD" || select == "NIOROSHI_GENBA_CD")
                {
                    //s = "     '' " + s + "'' " + select.Replace("_CD", "_NAME") + ",";
                    s = "     '' " + s;
                }
                else
                {
                    if (select == "HINMEI_CD")
                    {
                        s = "     CONVERT(NVARCHAR, DETAIL." + select + ") " + select + ",";
                    }
                    else
                    {
                        s = "     CONVERT(NVARCHAR, " + select + ") " + select + ",";
                    }
                }
                baseSql.AppendLine(s);
            }
            baseSql.Append("     SUBSTRING(CONVERT(VARCHAR(10), ENTRY.");
            if (this.form.FormDataDto.DateShuruiCd == 1)
            {
                baseSql.Append("DENPYOU_DATE");
            }
            else if (this.form.FormDataDto.DateShuruiCd == 2)
            {
                baseSql.Append("URIAGE_DATE");
            }
            else
            {
                baseSql.Append("UPDATE_DATE");
            }
            baseSql.AppendLine(", 111), 1 , 7) DATE");
            baseSql.AppendLine("    ,(ISNULL(DETAIL.KINGAKU, 0) + ISNULL(DETAIL.HINMEI_KINGAKU, 0)) - (ISNULL(DETAIL.TAX_UCHI, 0) + ISNULL(DETAIL.HINMEI_TAX_UCHI, 0)) KINGAKU ");
            baseSql.AppendLine("    FROM ");
            baseSql.AppendLine("     T_SHUKKA_ENTRY ENTRY ");
            baseSql.AppendLine("     INNER JOIN T_SHUKKA_DETAIL DETAIL ");
            baseSql.AppendLine("     ON ENTRY.SYSTEM_ID = DETAIL.SYSTEM_ID ");
            baseSql.AppendLine("      AND ENTRY.SEQ = DETAIL.SEQ ");
            baseSql.AppendLine("     LEFT JOIN M_HINMEI ");
            baseSql.AppendLine("     ON DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD ");
            baseSql.AppendLine("     LEFT JOIN ");
            baseSql.AppendLine("     ( ");
            baseSql.AppendLine("      SELECT ");
            baseSql.AppendLine("        DENPYOU.SEISAN_NUMBER ");
            baseSql.AppendLine("       ,DETAIL.DENPYOU_SYSTEM_ID ");
            baseSql.AppendLine("       ,DETAIL.DENPYOU_SEQ ");
            baseSql.AppendLine("       ,DETAIL.DETAIL_SYSTEM_ID ");
            baseSql.AppendLine("       ,DETAIL.DENPYOU_NUMBER ");
            baseSql.AppendLine("      FROM T_SEISAN_DENPYOU DENPYOU ");
            baseSql.AppendLine("      JOIN T_SEISAN_DENPYOU_KAGAMI KAGAMI ");
            baseSql.AppendLine("      ON DENPYOU.SEISAN_NUMBER = KAGAMI.SEISAN_NUMBER ");
            baseSql.AppendLine("      JOIN T_SEISAN_DETAIL DETAIL ");
            baseSql.AppendLine("      ON KAGAMI.SEISAN_NUMBER = DETAIL.SEISAN_NUMBER ");
            baseSql.AppendLine("       AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER ");
            baseSql.AppendLine("      WHERE DETAIL.DENPYOU_SHURUI_CD = 2 ");
            baseSql.AppendLine("      AND DENPYOU.DELETE_FLG = 0 ");
            baseSql.AppendLine("     ) SEISAN ");
            baseSql.AppendLine("     ON SEISAN.DENPYOU_SYSTEM_ID = DETAIL.SYSTEM_ID ");
            baseSql.AppendLine("      AND SEISAN.DENPYOU_SEQ = DETAIL.SEQ ");
            baseSql.AppendLine("      AND SEISAN.DETAIL_SYSTEM_ID = DETAIL.DETAIL_SYSTEM_ID ");
            baseSql.AppendLine("      AND SEISAN.DENPYOU_NUMBER = DETAIL.SHUKKA_NUMBER ");
            baseSql.AppendLine("    WHERE ");
            baseSql.AppendLine("         ENTRY.DELETE_FLG = 0 ");
            baseSql.AppendLine("     AND ENTRY.KENSHU_MUST_KBN = 0 ");
            baseSql.AppendLine("     AND ENTRY.TAIRYUU_KBN = 0 ");
            baseSql.AppendLine("     AND DETAIL.DENPYOU_KBN_CD = 2 ");
            baseSql.Append(CreateBaseWhere(2));
            baseSql.AppendLine("    UNION ALL ");

            // 検収入力を行う
            baseSql.AppendLine("    SELECT ");
            foreach (string select in this.form.FormDataDto.Select)
            {
                string s = "     " + select + ",";
                if (select == "NIOROSHI_GYOUSHA_CD" || select == "NIOROSHI_GENBA_CD")
                {
                    //s = "     '' " + s + "'' " + select.Replace("_CD", "_NAME") + ",";
                    s = "     '' " + s;
                }
                else
                {
                    if (select == "HINMEI_CD")
                    {
                        s = "     CONVERT(NVARCHAR, DETAIL." + select + ") " + select + ",";
                    }
                    else
                    {
                        s = "     CONVERT(NVARCHAR, " + select + ") " + select + ",";
                    }
                }
                baseSql.AppendLine(s);
            }
            baseSql.Append("     SUBSTRING(CONVERT(VARCHAR(10), ENTRY.");
            if (this.form.FormDataDto.DateShuruiCd == 1)
            {
                baseSql.Append("KENSHU_DATE");
            }
            else if (this.form.FormDataDto.DateShuruiCd == 2)
            {
                baseSql.Append("KENSHU_SHIHARAI_DATE");
            }
            else
            {
                baseSql.Append("UPDATE_DATE");
            }
            baseSql.AppendLine(", 111), 1 , 7) DATE");
            baseSql.AppendLine("     ,(ISNULL(DETAIL.KINGAKU, 0) + ISNULL(DETAIL.HINMEI_KINGAKU, 0)) - (ISNULL(DETAIL.TAX_UCHI, 0) + ISNULL(DETAIL.HINMEI_TAX_UCHI, 0)) KINGAKU ");
            baseSql.AppendLine("    FROM ");
            baseSql.AppendLine("    T_SHUKKA_ENTRY ENTRY ");
            baseSql.AppendLine("    INNER JOIN ");
            baseSql.AppendLine("    ( ");
            baseSql.AppendLine("     SELECT ");
            baseSql.AppendLine("      T_KENSHU_DETAIL.* ");
            baseSql.AppendLine("     FROM ");
            baseSql.AppendLine("      T_SHUKKA_DETAIL ");
            baseSql.AppendLine("     INNER JOIN T_KENSHU_DETAIL ");
            baseSql.AppendLine("     ON T_SHUKKA_DETAIL.SYSTEM_ID = T_KENSHU_DETAIL.SYSTEM_ID ");
            baseSql.AppendLine("      AND T_SHUKKA_DETAIL.SEQ = T_KENSHU_DETAIL.SEQ ");
            baseSql.AppendLine("      AND T_SHUKKA_DETAIL.DETAIL_SYSTEM_ID = T_KENSHU_DETAIL.DETAIL_SYSTEM_ID ");
            baseSql.AppendLine("    ) DETAIL ");
            baseSql.AppendLine("    ON ENTRY.SYSTEM_ID = DETAIL.SYSTEM_ID ");
            baseSql.AppendLine("     AND ENTRY.SEQ = DETAIL.SEQ ");
            baseSql.AppendLine("    LEFT JOIN M_HINMEI ");
            baseSql.AppendLine("    ON DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD ");
            baseSql.AppendLine("    WHERE ");
            baseSql.AppendLine("         ENTRY.DELETE_FLG = 0 ");
            baseSql.AppendLine("     AND ENTRY.KENSHU_MUST_KBN = 1 ");
            baseSql.AppendLine("     AND ENTRY.TAIRYUU_KBN = 0 ");
            baseSql.AppendLine("     AND DETAIL.DENPYOU_KBN_CD = 2 ");
            baseSql.Append(CreateBaseWhere(2, true));

            return baseSql.ToString();
        }

        // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
        /// <summary>
        /// 支払のベースSQL
        /// </summary>
        /// <param name="denpyouShuruiCd">
        /// 伝票種類(3：支払、4：代納、5：全て(デフォルト))
        /// </param>
        /// <returns></returns>
        /// <remarks>
        /// 伝票種類により、支払又は支払(代納)データを取得する
        /// 伝票種類しない場合、全ての支払データを取得する
        /// </remarks>
        //private string CreateShiharaiBaseSql()
        private string CreateShiharaiBaseSql(int denpyouShuruiCd = 5)
        // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
        {
            StringBuilder baseSql = new StringBuilder();

            baseSql.AppendLine("    SELECT ");
            foreach (string select in this.form.FormDataDto.Select)
            {
                string s = "     " + select + ",";
                if (select == "DAIKAN_KBN")
                {
                    s = "     '' " + s;
                }
                else
                {
                    if (select == "HINMEI_CD")
                    {
                        s = "     CONVERT(NVARCHAR, DETAIL." + select + ") " + select + ",";
                    }
                    else
                    {
                        s = "     CONVERT(NVARCHAR, " + select + ") " + select + ",";
                    }
                }
                baseSql.AppendLine(s);
            }
            baseSql.Append("     SUBSTRING(CONVERT(VARCHAR(10), ENTRY.");
            if (this.form.FormDataDto.DateShuruiCd == 1)
            {
                baseSql.Append("DENPYOU_DATE");
            }
            else if (this.form.FormDataDto.DateShuruiCd == 2)
            {
                baseSql.Append("URIAGE_DATE");
            }
            else
            {
                baseSql.Append("UPDATE_DATE");
            }
            baseSql.AppendLine(", 111), 1 , 7) DATE");
            baseSql.AppendLine("    ,(ISNULL(DETAIL.KINGAKU, 0) + ISNULL(DETAIL.HINMEI_KINGAKU, 0)) - (ISNULL(DETAIL.TAX_UCHI, 0) + ISNULL(DETAIL.HINMEI_TAX_UCHI, 0)) KINGAKU ");
            baseSql.AppendLine("    FROM");
            baseSql.AppendLine("    T_UR_SH_ENTRY ENTRY ");
            baseSql.AppendLine("    INNER JOIN");
            baseSql.AppendLine("    T_UR_SH_DETAIL DETAIL");
            baseSql.AppendLine("    ON (ENTRY.SYSTEM_ID = DETAIL.SYSTEM_ID AND ENTRY.SEQ = DETAIL.SEQ)");
            baseSql.AppendLine("    LEFT JOIN M_HINMEI ");
            baseSql.AppendLine("    ON DETAIL.HINMEI_CD = M_HINMEI.HINMEI_CD ");
            baseSql.AppendLine("    LEFT JOIN ");
            baseSql.AppendLine("    ( ");
            baseSql.AppendLine("      SELECT ");
            baseSql.AppendLine("        DENPYOU.SEISAN_NUMBER");
            baseSql.AppendLine("       ,DETAIL.DENPYOU_SYSTEM_ID");
            baseSql.AppendLine("       ,DETAIL.DENPYOU_SEQ");
            baseSql.AppendLine("       ,DETAIL.DETAIL_SYSTEM_ID");
            baseSql.AppendLine("       ,DETAIL.DENPYOU_NUMBER");
            baseSql.AppendLine("      FROM ");
            baseSql.AppendLine("       T_SEISAN_DENPYOU DENPYOU ");
            baseSql.AppendLine("       JOIN T_SEISAN_DENPYOU_KAGAMI KAGAMI");
            baseSql.AppendLine("       ON DENPYOU.SEISAN_NUMBER = KAGAMI.SEISAN_NUMBER");
            baseSql.AppendLine("       JOIN T_SEISAN_DETAIL DETAIL");
            baseSql.AppendLine("       ON KAGAMI.SEISAN_NUMBER = DETAIL.SEISAN_NUMBER");
            baseSql.AppendLine("        AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER");
            baseSql.AppendLine("      WHERE DETAIL.DENPYOU_SHURUI_CD = 3");
            baseSql.AppendLine("        AND DENPYOU.DELETE_FLG = 0 ");
            baseSql.AppendLine("    ) SEISAN ");
            baseSql.AppendLine("    ON SEISAN.DENPYOU_SYSTEM_ID = DETAIL.SYSTEM_ID");
            baseSql.AppendLine("     AND SEISAN.DENPYOU_SEQ = DETAIL.SEQ");
            baseSql.AppendLine("     AND SEISAN.DETAIL_SYSTEM_ID = DETAIL.DETAIL_SYSTEM_ID");
            baseSql.AppendLine("     AND SEISAN.DENPYOU_NUMBER = DETAIL.UR_SH_NUMBER");
            baseSql.AppendLine("    WHERE");
            baseSql.AppendLine("         ENTRY.DELETE_FLG = 0");
            baseSql.AppendLine("     AND DETAIL.DENPYOU_KBN_CD = 2");

            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
            // 伝種 = 3の場合、支払データを取得する
            if (denpyouShuruiCd == 3)
            {
                baseSql.AppendLine("     AND (ENTRY.DAINOU_FLG IS NULL OR ENTRY.DAINOU_FLG != 1) ");
            }
            // 伝種 = 3の場合、支払(代納)データを取得する
            else if (denpyouShuruiCd == 4)
            {
                baseSql.AppendLine("     AND ENTRY.DAINOU_FLG = 1 ");
            }
            // 伝種 = 5の場合、全てデータを取得する
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End

            return baseSql.ToString();
        }

        /// <summary>
        /// ベースSQLの条件
        /// </summary>
        /// <param name="denpyo">
        ///  1 : 受入
        ///  2 : 出荷
        ///  3 : 支払
        /// </param>
        /// <returns></returns>
        private string CreateBaseWhere(int denpyo, bool kenshu = false)
        {
            StringBuilder whereSb = new StringBuilder();
            // 拠点
            if (this.form.FormDataDto.KyotenCd != 99)
            {
                whereSb.AppendFormat("     AND ENTRY.KYOTEN_CD = {0} ", this.form.FormDataDto.KyotenCd);
                whereSb.AppendLine(string.Empty);
            }
            // 入力担当者
            if (!string.IsNullOrEmpty(this.form.FormDataDto.NyuuryokuTantoushaCdFrom))
            {
                whereSb.AppendFormat("     AND (ENTRY.NYUURYOKU_TANTOUSHA_CD >= '{0}'", this.form.FormDataDto.NyuuryokuTantoushaCdFrom);
                whereSb.AppendLine(string.Empty);
                whereSb.AppendLine("     OR ENTRY.NYUURYOKU_TANTOUSHA_CD IS NULL)");
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.NyuuryokuTantoushaCdTo))
            {
                whereSb.AppendFormat("     AND (ENTRY.NYUURYOKU_TANTOUSHA_CD <= '{0}'", this.form.FormDataDto.NyuuryokuTantoushaCdTo);
                whereSb.AppendLine(string.Empty);
                whereSb.AppendLine("     OR ENTRY.NYUURYOKU_TANTOUSHA_CD IS NULL)");
            }
            // 伝票日付
            if (this.form.FormDataDto.DateShuruiCd == 1)
            {
                if (kenshu)
                {
                    whereSb.AppendFormat("     AND ENTRY.KENSHU_DATE >= '{0}' ", this.form.FormDataDto.DateFrom);
                    whereSb.AppendLine(string.Empty);
                    whereSb.AppendFormat("     AND ENTRY.KENSHU_DATE <= '{0}' ", this.form.FormDataDto.DateTo);
                    whereSb.AppendLine(string.Empty);
                }
                else
                {
                    whereSb.AppendFormat("     AND ENTRY.DENPYOU_DATE >= '{0}' ", this.form.FormDataDto.DateFrom);
                    whereSb.AppendLine(string.Empty);
                    whereSb.AppendFormat("     AND ENTRY.DENPYOU_DATE <= '{0}' ", this.form.FormDataDto.DateTo);
                    whereSb.AppendLine(string.Empty);
                }
            }
            //支払日付
            else if (this.form.FormDataDto.DateShuruiCd == 2)
            {
                if (kenshu)
                {
                    whereSb.AppendFormat("     AND ENTRY.KENSHU_SHIHARAI_DATE >= '{0}' ", this.form.FormDataDto.DateFrom);
                    whereSb.AppendLine(string.Empty);
                    whereSb.AppendFormat("     AND ENTRY.KENSHU_SHIHARAI_DATE <= '{0}' ", this.form.FormDataDto.DateTo);
                    whereSb.AppendLine(string.Empty);
                }
                else
                {
                    whereSb.AppendFormat("     AND ENTRY.SHIHARAI_DATE >= '{0}' ", this.form.FormDataDto.DateFrom);
                    whereSb.AppendLine(string.Empty);
                    whereSb.AppendFormat("     AND ENTRY.SHIHARAI_DATE <= '{0}' ", this.form.FormDataDto.DateTo);
                    whereSb.AppendLine(string.Empty);
                }
            }
            // 入力日付
            else
            {
                whereSb.AppendFormat("     AND CONVERT(date, ENTRY.UPDATE_DATE, 111) >= '{0}' ", this.form.FormDataDto.DateFrom);
                whereSb.AppendLine(string.Empty);
                whereSb.AppendFormat("     AND CONVERT(date, ENTRY.UPDATE_DATE, 111) <= '{0}' ", this.form.FormDataDto.DateTo);
                whereSb.AppendLine(string.Empty);
            }
            // 締状況
            if (denpyo == 2 && kenshu)
            {
                if (this.form.FormDataDto.ShimeJoukyouCd == 1)
                {
                    whereSb.AppendLine("     AND EXISTS");
                    whereSb.AppendLine("       (");
                    whereSb.AppendLine("         SELECT * FROM");
                    whereSb.AppendLine("           ( ");
                    whereSb.AppendLine("             SELECT ");
                    whereSb.AppendLine("               DENPYOU.SEISAN_NUMBER ");
                    whereSb.AppendLine("               ,DETAIL.DENPYOU_SYSTEM_ID ");
                    whereSb.AppendLine("               ,DETAIL.DENPYOU_SEQ ");
                    whereSb.AppendLine("               ,DETAIL.DETAIL_SYSTEM_ID ");
                    whereSb.AppendLine("               ,DETAIL.DENPYOU_NUMBER ");
                    whereSb.AppendLine("             FROM T_SEISAN_DENPYOU DENPYOU ");
                    whereSb.AppendLine("             JOIN T_SEISAN_DENPYOU_KAGAMI KAGAMI ");
                    whereSb.AppendLine("               ON DENPYOU.SEISAN_NUMBER = KAGAMI.SEISAN_NUMBER ");
                    whereSb.AppendLine("             JOIN T_SEISAN_DETAIL DETAIL ");
                    whereSb.AppendLine("               ON KAGAMI.SEISAN_NUMBER = DETAIL.SEISAN_NUMBER ");
                    whereSb.AppendLine("               AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER ");
                    whereSb.AppendLine("             WHERE DETAIL.DENPYOU_SHURUI_CD = 2 ");
                    whereSb.AppendLine("               AND DENPYOU.DELETE_FLG = 0 ");
                    whereSb.AppendLine("           ) SEIKYUU ");
                    whereSb.AppendLine("         WHERE SEIKYUU.DENPYOU_SYSTEM_ID = DETAIL.SYSTEM_ID ");
                    whereSb.AppendLine("           AND SEIKYUU.DENPYOU_SEQ = DETAIL.SEQ ");
                    whereSb.AppendLine("           AND SEIKYUU.DETAIL_SYSTEM_ID = DETAIL.DETAIL_SYSTEM_ID ");
                    whereSb.AppendLine("           AND SEIKYUU.DENPYOU_NUMBER = DETAIL.SHUKKA_NUMBER ");
                    whereSb.AppendLine("       )");
                    whereSb.AppendLine(string.Empty);
                }
                else if (this.form.FormDataDto.ShimeJoukyouCd == 2)
                {
                    whereSb.AppendLine("     AND NOT EXISTS");
                    whereSb.AppendLine("       (");
                    whereSb.AppendLine("         SELECT * FROM");
                    whereSb.AppendLine("           ( ");
                    whereSb.AppendLine("             SELECT ");
                    whereSb.AppendLine("               DENPYOU.SEISAN_NUMBER ");
                    whereSb.AppendLine("               ,DETAIL.DENPYOU_SYSTEM_ID ");
                    whereSb.AppendLine("               ,DETAIL.DENPYOU_SEQ ");
                    whereSb.AppendLine("               ,DETAIL.DETAIL_SYSTEM_ID ");
                    whereSb.AppendLine("               ,DETAIL.DENPYOU_NUMBER ");
                    whereSb.AppendLine("             FROM T_SEISAN_DENPYOU DENPYOU ");
                    whereSb.AppendLine("             JOIN T_SEISAN_DENPYOU_KAGAMI KAGAMI ");
                    whereSb.AppendLine("               ON DENPYOU.SEISAN_NUMBER = KAGAMI.SEISAN_NUMBER ");
                    whereSb.AppendLine("             JOIN T_SEISAN_DETAIL DETAIL ");
                    whereSb.AppendLine("               ON KAGAMI.SEISAN_NUMBER = DETAIL.SEISAN_NUMBER ");
                    whereSb.AppendLine("               AND KAGAMI.KAGAMI_NUMBER = DETAIL.KAGAMI_NUMBER ");
                    whereSb.AppendLine("             WHERE DETAIL.DENPYOU_SHURUI_CD = 2 ");
                    whereSb.AppendLine("               AND DENPYOU.DELETE_FLG = 0 ");
                    whereSb.AppendLine("           ) SEIKYUU ");
                    whereSb.AppendLine("         WHERE SEIKYUU.DENPYOU_SYSTEM_ID = DETAIL.SYSTEM_ID ");
                    whereSb.AppendLine("           AND SEIKYUU.DENPYOU_SEQ = DETAIL.SEQ ");
                    whereSb.AppendLine("           AND SEIKYUU.DETAIL_SYSTEM_ID = DETAIL.DETAIL_SYSTEM_ID ");
                    whereSb.AppendLine("           AND SEIKYUU.DENPYOU_NUMBER = DETAIL.SHUKKA_NUMBER ");
                    whereSb.AppendLine("       )");
                    whereSb.AppendLine(string.Empty);
                }
            }
            else
            {
                if (this.form.FormDataDto.ShimeJoukyouCd == 1)
                {
                    whereSb.Append("     AND SEISAN.SEISAN_NUMBER IS NOT NULL");
                    whereSb.AppendLine(string.Empty);
                }
                else if (this.form.FormDataDto.ShimeJoukyouCd == 2)
                {
                    whereSb.Append("     AND SEISAN.SEISAN_NUMBER IS NULL");
                    whereSb.AppendLine(string.Empty);
                }
            }

            // 確定区分
            if (this.form.FormDataDto.KakuteiKbnCd != 3)
            {
                whereSb.AppendFormat("     AND ENTRY.KAKUTEI_KBN = {0}", this.form.FormDataDto.KakuteiKbnCd);
                whereSb.AppendLine(string.Empty);
            }
            // 取引区分
            if (this.form.FormDataDto.TorihikiKbnCd != 3)
            {
                whereSb.AppendFormat("     AND ENTRY.SHIHARAI_TORIHIKI_KBN_CD = {0} ", this.form.FormDataDto.TorihikiKbnCd);
                whereSb.AppendLine(string.Empty);
            }
            // 取引先
            if (!string.IsNullOrEmpty(this.form.FormDataDto.TorihikisakiCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.TORIHIKISAKI_CD >= '{0}'", this.form.FormDataDto.TorihikisakiCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.TorihikisakiCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.TORIHIKISAKI_CD <= '{0}'", this.form.FormDataDto.TorihikisakiCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 業者
            if (!string.IsNullOrEmpty(this.form.FormDataDto.GyoushaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.GYOUSHA_CD >= '{0}'", this.form.FormDataDto.GyoushaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.GyoushaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.GYOUSHA_CD <= '{0}'", this.form.FormDataDto.GyoushaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 現場
            if (!string.IsNullOrEmpty(this.form.FormDataDto.GenbaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.GENBA_CD >= '{0}'", this.form.FormDataDto.GenbaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.GenbaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.GENBA_CD <= '{0}'", this.form.FormDataDto.GenbaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 品名
            if (!string.IsNullOrEmpty(this.form.FormDataDto.HinmeiCdFrom))
            {
                whereSb.AppendFormat("     AND DETAIL.HINMEI_CD >= '{0}'", this.form.FormDataDto.HinmeiCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.HinmeiCdTo))
            {
                whereSb.AppendFormat("     AND DETAIL.HINMEI_CD <= '{0}'", this.form.FormDataDto.HinmeiCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 種類
            if (!string.IsNullOrEmpty(this.form.FormDataDto.ShuruiCdFrom))
            {
                whereSb.AppendFormat("     AND M_HINMEI.SHURUI_CD >= '{0}'", this.form.FormDataDto.ShuruiCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.ShuruiCdTo))
            {
                whereSb.AppendFormat("     AND M_HINMEI.SHURUI_CD <= '{0}'", this.form.FormDataDto.ShuruiCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 分類
            if (!string.IsNullOrEmpty(this.form.FormDataDto.BunruiCdFrom))
            {
                whereSb.AppendFormat("     AND M_HINMEI.BUNRUI_CD >= '{0}'", this.form.FormDataDto.BunruiCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.BunruiCdTo))
            {
                whereSb.AppendFormat("     AND M_HINMEI.BUNRUI_CD <= '{0}'", this.form.FormDataDto.BunruiCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 荷降業者
            if (denpyo != 2 && !string.IsNullOrEmpty(this.form.FormDataDto.NioroshiGyoushaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.NIOROSHI_GYOUSHA_CD >= '{0}'", this.form.FormDataDto.NioroshiGyoushaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.NioroshiGyoushaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.NIOROSHI_GYOUSHA_CD <= '{0}'", this.form.FormDataDto.NioroshiGyoushaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 荷降現場
            if (denpyo != 2 && !string.IsNullOrEmpty(this.form.FormDataDto.NioroshiGenbaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.NIOROSHI_GENBA_CD >= '{0}'", this.form.FormDataDto.NioroshiGenbaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.NioroshiGenbaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.NIOROSHI_GENBA_CD <= '{0}'", this.form.FormDataDto.NioroshiGenbaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 荷積業者
            if (denpyo != 1 && !string.IsNullOrEmpty(this.form.FormDataDto.NizumiGyoushaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.NIZUMI_GYOUSHA_CD >= '{0}'", this.form.FormDataDto.NizumiGyoushaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (denpyo != 1 && !string.IsNullOrEmpty(this.form.FormDataDto.NizumiGyoushaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.NIZUMI_GYOUSHA_CD <= '{0}'", this.form.FormDataDto.NizumiGyoushaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 荷積現場
            if (denpyo != 1 && !string.IsNullOrEmpty(this.form.FormDataDto.NizumiGenbaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.NIZUMI_GENBA_CD >= '{0}'", this.form.FormDataDto.NizumiGenbaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (denpyo != 1 && !string.IsNullOrEmpty(this.form.FormDataDto.NizumiGenbaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.NIZUMI_GENBA_CD <= '{0}'", this.form.FormDataDto.NizumiGenbaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 営業担当者
            if (!string.IsNullOrEmpty(this.form.FormDataDto.EigyouTantoushaCdForm))
            {
                whereSb.AppendFormat("     AND ENTRY.EIGYOU_TANTOUSHA_CD >= '{0}'", this.form.FormDataDto.EigyouTantoushaCdForm);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.EigyouTantoushaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.EIGYOU_TANTOUSHA_CD <= '{0}'", this.form.FormDataDto.EigyouTantoushaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 入力担当者
            if (!string.IsNullOrEmpty(this.form.FormDataDto.NyuuryokuTantoushaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.NYUURYOKU_TANTOUSHA_CD >= '{0}'", this.form.FormDataDto.NyuuryokuTantoushaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.NyuuryokuTantoushaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.NYUURYOKU_TANTOUSHA_CD <= '{0}'", this.form.FormDataDto.NyuuryokuTantoushaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 運搬業者
            if (!string.IsNullOrEmpty(this.form.FormDataDto.UnpanGyoushaCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.UNPAN_GYOUSHA_CD >= '{0}'", this.form.FormDataDto.UnpanGyoushaCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.UnpanGyoushaCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.UNPAN_GYOUSHA_CD <= '{0}'", this.form.FormDataDto.UnpanGyoushaCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 車種
            if (!string.IsNullOrEmpty(this.form.FormDataDto.ShashuCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.SHASHU_CD >= '{0}'", this.form.FormDataDto.ShashuCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.ShashuCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.SHASHU_CD <= '{0}'", this.form.FormDataDto.ShashuCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 車輛
            if (!string.IsNullOrEmpty(this.form.FormDataDto.SharyouCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.SHARYOU_CD >= '{0}'", this.form.FormDataDto.SharyouCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.SharyouCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.SHARYOU_CD <= '{0}'", this.form.FormDataDto.SharyouCdTo);
                whereSb.AppendLine(string.Empty);
            }
            // 形態区分
            if (!string.IsNullOrEmpty(this.form.FormDataDto.KeitaiKbnCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.KEITAI_KBN_CD >= '{0}'", this.form.FormDataDto.KeitaiKbnCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.KeitaiKbnCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.KEITAI_KBN_CD <= '{0}'", this.form.FormDataDto.KeitaiKbnCdTo);
                whereSb.AppendLine(string.Empty);
            }
            //PhuocLoc 2020/12/07 #136227 -Start
            // 集計項目
            if (!string.IsNullOrEmpty(this.form.FormDataDto.ShuukeiKoumokuCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.MOD_SHUUKEI_KOUMOKU_CD >= '{0}'", this.form.FormDataDto.ShuukeiKoumokuCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (!string.IsNullOrEmpty(this.form.FormDataDto.ShuukeiKoumokuCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.MOD_SHUUKEI_KOUMOKU_CD <= '{0}'", this.form.FormDataDto.ShuukeiKoumokuCdTo);
                whereSb.AppendLine(string.Empty);
            }
            //PhuocLoc 2020/12/07 #136227 -End
            // 台貫
            if (denpyo != 3 && !string.IsNullOrEmpty(this.form.FormDataDto.DaikanCdFrom))
            {
                whereSb.AppendFormat("     AND ENTRY.DAIKAN_KBN >= '{0}'", this.form.FormDataDto.DaikanCdFrom);
                whereSb.AppendLine(string.Empty);
            }
            if (denpyo != 3 && !string.IsNullOrEmpty(this.form.FormDataDto.DaikanCdTo))
            {
                whereSb.AppendFormat("     AND ENTRY.DAIKAN_KBN <= '{0}'", this.form.FormDataDto.DaikanCdTo);
                whereSb.AppendLine(string.Empty);
            }

            return whereSb.ToString();
        }
    }
}
