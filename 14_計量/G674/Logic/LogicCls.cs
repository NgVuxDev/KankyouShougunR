using System;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Scale.KeiryouHoukoku.APP;
using Shougun.Core.Scale.KeiryouHoukoku.Const;
using Shougun.Core.Scale.KeiryouHoukoku.DAO;
using Shougun.Core.Scale.KeiryouHoukoku.DTO;
using Shougun.Core.Scale.KeiryouHoukoku.Report;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon;

namespace Shougun.Core.Scale.KeiryouHoukoku.Logic
{
    /// <summary>
    /// G674 計量報告 ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region - Fields -

        /// <summary>フォーム</summary>
        private UIForm form;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="LogicCls"/> class.</summary>
        /// <param name="targetForm">targetForm</param>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        #endregion - Constructors -

        #region - Methods -

        /// <summary>画面初期化処理</summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            try
            {
                // ヘッダーを初期化
                this.HeaderInit();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
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
            //ヘッダーの初期化
            this.form.HeaderForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            ButtonControlUtility.SetButtonInfo(
                new ButtonSetting().LoadButtonSetting(Assembly.GetExecutingAssembly(), ConstCls.BUTTON_INFO_XML_PATH),
                this.form.BaseForm, this.form.WindowType);
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

                // CSV出力ボタン(F5)イベント生成
                parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

                // 明細項目表示ボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

                this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(this.form.HIDUKE_TO_MouseDoubleClick);
                this.form.TORIHIKISAKI_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.TORIHIKISAKI_CD_TO_MouseDoubleClick);
                this.form.GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.GYOUSHA_CD_TO_MouseDoubleClick);
                this.form.GENBA_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.GENBA_CD_TO_MouseDoubleClick);
                this.form.UNPAN_GYOUSHA_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.UNPAN_GYOUSHA_CD_TO_MouseDoubleClick);
                this.form.HINMEI_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.HINMEI_CD_TO_MouseDoubleClick);
                this.form.SHURUI_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.SHURUI_CD_TO_MouseDoubleClick);
                this.form.BUNRUI_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.BUNRUI_CD_TO_MouseDoubleClick);
                this.form.KEITAI_KBN_CD_TO.MouseDoubleClick += new MouseEventHandler(this.form.KEITAI_KBN_CD_TO_MouseDoubleClick);

                this.form.GYOUSHA_CD_FROM.PopupAfterExecute += new Action<ICustomControl, DialogResult>(this.form.GYOUSHA_POPUPAFTEREXECUTE);
                this.form.GYOUSHA_FROM_POPUP.PopupAfterExecute += new Action<ICustomControl, DialogResult>(this.form.GYOUSHA_POPUPAFTEREXECUTE);
                this.form.GYOUSHA_CD_TO.PopupAfterExecute += new Action<ICustomControl, DialogResult>(this.form.GYOUSHA_POPUPAFTEREXECUTE);
                this.form.GYOUSHA_TO_POPUP.PopupAfterExecute += new Action<ICustomControl, DialogResult>(this.form.GYOUSHA_POPUPAFTEREXECUTE);
                this.form.GYOUSHA_CD_FROM.Validated += new EventHandler(this.form.GYOUSHA_CD_FROM_Validated);
                this.form.GYOUSHA_CD_TO.Validated += new EventHandler(this.form.GYOUSHA_CD_TO_Validated);

                this.form.rbtMeisaihyo.CheckedChanged += new System.EventHandler(this.form.RadioButtonMeisaihyo_CheckedChanged);
                this.form.rbtMotocho.CheckedChanged += new System.EventHandler(this.form.RadioButtonMeisaihyo_CheckedChanged);
                this.form.rbtSuiihyo.CheckedChanged += new System.EventHandler(this.form.RadioButtonMeisaihyo_CheckedChanged);

                this.form.rbtToday.CheckedChanged += new System.EventHandler(this.form.RadioButtonHidukeHaniShitei_CheckedChanged);
                this.form.rbtMonth.CheckedChanged += new System.EventHandler(this.form.RadioButtonHidukeHaniShitei_CheckedChanged);
                this.form.rbtKikan.CheckedChanged += new System.EventHandler(this.form.RadioButtonHidukeHaniShitei_CheckedChanged);
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        #region - Function Key Proc -

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        /// 
        internal bool CSVPrint(DTOCls dto)
        {
            try
            {
                // とりあえず固定のクエリでデータ取ってくるだけ
                var dao = DaoInitUtility.GetComponent<DAOClass>();
                var dt = dao.GetDetailData(dto);
                string out_put_head;
                if (0 < dt.Rows.Count)
                {
                    // 出力

                    //計量明細
                    if (1 == dto.HoukokuShurui)
                    {
                        out_put_head = "取引先CD,取引先,業者CD,業者,現場CD,現場,伝票日付,伝票番号,基本計量,形態区分,運搬業者CD,運搬業者,品名CD,品名,種類CD,種類,分類CD,分類,正味,数量,単位CD,単位,金額,消費税,合計金額";
                        Creat_CSV(dt, out_put_head,1);
                    }
                    //計量元帳
                    else if (2 == dto.HoukokuShurui)
                    {
                        out_put_head = "";
                        switch (dto.GroupTani)
                        {
                        	//取引先
                            case 1: out_put_head = "取引先CD,取引先,伝票日付,伝票番号,基本計量,業者CD,業者,現場CD,現場,車種CD,車種,車輌CD,車輌,運転者名,品名CD,品名,正味重量,数量,単位CD,単位,備考";
                                break;
                            //業者
                            case 2: out_put_head = "業者CD,業者,伝票日付,伝票番号,基本計量,現場CD,現場,車種CD,車種,車輌CD,車輌,運転者名,品名CD,品名,正味重量,数量,単位CD,単位,備考";
                                break;
                            //現場
                            case 3: out_put_head = "業者CD,業者,現場CD,現場,伝票日付,伝票番号,基本計量,車種CD,車種,車輌CD,車輌,運転者名,品名CD,品名,正味重量,数量,単位CD,単位,備考";
                                break;
                        }
                        Creat_CSV(dt, out_put_head,2);
                    }
                    //計量推移
                    else if (3 == dto.HoukokuShurui)
                    {
                        DateTime dateTimeStartTmp = Convert.ToDateTime(dto.DateFrom);
                        DateTime dateTimeEndTmp = Convert.ToDateTime(dto.DateTo);
                        out_put_head = "";
                        string key_name = "";
                        string key_name2 = "";
                        switch (dto.GroupTani)
                        {
                            //取引先
                            case 1:
                                out_put_head = "取引先CD,取引先";
                                key_name = "取引先CD";
                                break;
                            //業者
                            case 2:
                                out_put_head = "業者CD,業者";
                                key_name = "業者CD";
                                break;
                            //現場
                            case 3:
                                out_put_head = "業者CD,業者,現場CD,現場";
                                key_name = "現場CD";
                                key_name2 = "業者CD";
                                break;
                            //品名
                            case 4:
                                out_put_head = "品名CD,品名";
                                key_name = "品名CD";
                                break;
                            //分類
                            case 5:
                                out_put_head = "品名CD,品名,分類CD,分類";
                                key_name = "分類CD";
                                key_name2 = "品名CD";
                                break;
                            //種類
                            case 6:
                                out_put_head = "品名CD,品名,種類CD,種類";
                                key_name = "種類CD";
                                key_name2 = "品名CD";
                                break;
                        }
                        Creat_MoveCSV(dt, dateTimeStartTmp, dateTimeEndTmp, out_put_head, key_name, key_name2);
                    }
                }
                else
                {
                    this.form.MsgLogic.MessageBoxShow("C001");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.MsgLogic.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                return false;
            }
            return true;
        }

        /// <summary>
        /// headによって対したCSV出力
        /// </summary>
        /// 
        public void Creat_CSV(DataTable dt, string out_put_head,int id)
        {
            try
            {
                DataTable csvDT = new DataTable();
                string[] out_put_head_array = out_put_head.Split(',');
                //書式設定
                string strFormat = "#,##0";
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                string jyuryouFormat = mSysInfo.SYS_JYURYOU_FORMAT.ToString();
                string suuryoFormat = mSysInfo.SYS_SUURYOU_FORMAT.ToString();
                foreach (var n in out_put_head_array)
                {
                    csvDT.Columns.Add(n);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = csvDT.NewRow();
                    for (int m = 0; m < csvDT.Columns.Count; m++)
                    {
                        switch (csvDT.Columns[m].ColumnName)
                        {
                            case "取引先CD": row[m] = dt.Rows[i]["TORIHIKISAKI_CD"]; break;
                            case "取引先": row[m] = dt.Rows[i]["TORIHIKISAKI_NAME"]; break;
                            case "業者CD": row[m] = dt.Rows[i]["GYOUSHA_CD"]; break;
                            case "業者名": row[m] = dt.Rows[i]["GYOUSHA_NAME"]; break;
                            case "業者": row[m] = dt.Rows[i]["GYOUSHA_NAME"]; break;
                            case "現場CD": row[m] = dt.Rows[i]["GENBA_CD"]; break;
                            case "現場": row[m] = dt.Rows[i]["GENBA_NAME"]; break;
                            case "現場名": row[m] = dt.Rows[i]["GENBA_NAME"]; break;
                            case "伝票日付": row[m] = Convert.ToDateTime(dt.Rows[i]["DENPYOU_DATE"]).ToString("yyyy/MM/dd"); break;
                            case "伝票番号": row[m] = dt.Rows[i]["DENPYOU_NUMBER"]; break;
                            case "基本計量":
                                // 計量区分
                                if (dt.Rows[i]["KEIRYOU_KBN"].ToString() == "1")
                                {
                                    row[m] = ConstCls.KEIRYOU_KBN_1;
                                }
                                else if (dt.Rows[i]["KEIRYOU_KBN"].ToString() == "2")
                                {
                                    row[m] = ConstCls.KEIRYOU_KBN_2;
                                }
                                else
                                {
                                    row[m] = ConstCls.ALL;
                                }
                                 break;
                            case "形態区分": row[m] = dt.Rows[i]["KEITAI_KBN_NAME"]; break;
                            case "運搬業者CD": row[m] = dt.Rows[i]["UNPAN_GYOUSHA_CD"]; break;
                            case "運搬業者": row[m] = dt.Rows[i]["UNPAN_GYOUSHA_NAME"]; break;
                            case "品名CD": row[m] = dt.Rows[i]["HINMEI_CD"]; break;
                            case "品名": row[m] = dt.Rows[i]["HINMEI_NAME"]; break;
                            case "種類CD": row[m] = dt.Rows[i]["SHURUI_CD"]; break;
                            case "種類": row[m] = dt.Rows[i]["SHURUI_NAME"]; break;
                            case "分類CD": row[m] = dt.Rows[i]["BUNRUI_CD"]; break;
                            case "分類": row[m] = dt.Rows[i]["BUNRUI_NAME"]; break;
                            case "正味": row[m] = Convert.ToDecimal(dt.Rows[i]["NET_JYUURYOU"]).ToString(jyuryouFormat); break;
                            case "数量": row[m] = Convert.ToDecimal(dt.Rows[i]["SUURYOU"]).ToString(suuryoFormat); break;
                            case "単位CD": row[m] = dt.Rows[i]["UNIT_CD"]; break;
                            case "単位": row[m] = dt.Rows[i]["UNIT_NAME"]; break;
                            case "金額": row[m] = Convert.ToDecimal(dt.Rows[i]["KINGAKU"]).ToString(strFormat); break;
                            case "消費税": row[m] = Convert.ToDecimal(dt.Rows[i]["TAX"]).ToString(strFormat); break;
                            case "合計金額": row[m] = Convert.ToDecimal(dt.Rows[i]["SUM_KINGAKU"]).ToString(strFormat); break;
                            case "車種CD": row[m] = dt.Rows[i]["SHASHU_CD"]; break;
                            case "車種": row[m] = dt.Rows[i]["SHASHU_NAME"]; break;
                            case "車輌CD": row[m] = dt.Rows[i]["SHARYOU_CD"]; break;
                            case "車輌": row[m] = dt.Rows[i]["SHARYOU_NAME"]; break;
                            case "運転者名": row[m] = dt.Rows[i]["UNTENSHA_NAME"]; break;
                            case "正味重量": row[m] = Convert.ToDecimal(dt.Rows[i]["NET_JYUURYOU"]).ToString(jyuryouFormat); break;
                            case "調整": row[m] = Convert.ToDecimal(dt.Rows[i]["CHOUSEI_JYUURYOU"]).ToString(jyuryouFormat); break;
                            case "総重量-空車重量": row[m] = Convert.ToDecimal(dt.Rows[i]["SE_JYUURYOU"]).ToString(jyuryouFormat); break;
                            case "備考": row[m] = dt.Rows[i]["MEISAI_BIKOU"]; break;
                        }
                    }
                    csvDT.Rows.Add(row);
                }
                if (csvDT == null || csvDT.Rows.Count == 0)
                {
                    this.form.MsgLogic.MessageBoxShow("E044");

                }
                // 出力先指定のポップアップを表示させる。
                if (this.form.MsgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    // CSV出力
                    if(id==1)
                        csvExport.ConvertDataTableToCsv(csvDT, true, true, ConstCls.KEIRYOU_MEISAIHYOU_TITLE, this.form);
                    else
                        csvExport.ConvertDataTableToCsv(csvDT, true, true, ConstCls.KEIRYOU_MOTOCHO_TITLE, this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                this.form.MsgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 入力時間範囲によって対したCSV出力
        /// </summary>
        public void Creat_MoveCSV(DataTable dt, DateTime start, DateTime end, string out_put_head, string key_cd, string key_cd2)
        {
            try
            {
                DataTable csvDT = new DataTable();
                DataTable csvDT_temp = new DataTable();
                //書式設定
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                string strFormat = "#,##0";
                string jyuryouFormat = mSysInfo.SYS_JYURYOU_FORMAT.ToString();
                int start_month = start.Month;
                int year_start = start.Year;
                int end_month = end.Month;
                if (end_month < start_month)
                    end_month = end_month + 12;
                string[] out_put_head_array = out_put_head.Split(',');
                foreach (var n in out_put_head_array)
                {
                    csvDT.Columns.Add(n);
                    csvDT_temp.Columns.Add(n);
                }
                csvDT_temp.Columns.Add("正味重量");
                csvDT_temp.Columns.Add("伝票日付");
                for (int i = start_month, m = start_month; i <=end_month; i++, m++)
                {
                    if (m == 13)
                    {
                        m = 1;
                        year_start++;
                    }

                    csvDT.Columns.Add(year_start + "/" + m);
                }
                csvDT.Columns.Add("合計");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = csvDT_temp.NewRow();
                    for (int m = 0; m < csvDT_temp.Columns.Count; m++)
                    {
                        switch (csvDT_temp.Columns[m].ColumnName)
                        {
                            case "伝票日付": row[m] = Convert.ToDateTime(dt.Rows[i]["DENPYOU_DATE"]).ToString("yyyy/MM/dd"); break;
                            case "取引先CD": row[m] = dt.Rows[i]["TORIHIKISAKI_CD"]; break;
                            case "取引先": row[m] = dt.Rows[i]["TORIHIKISAKI_NAME"]; break;
                            case "業者CD": row[m] = dt.Rows[i]["GYOUSHA_CD"]; break;
                            case "業者": row[m] = dt.Rows[i]["GYOUSHA_NAME"]; break;
                            case "現場CD": row[m] = dt.Rows[i]["GENBA_CD"]; break;
                            case "現場": row[m] = dt.Rows[i]["GENBA_NAME"]; break;
                            case "品名CD": row[m] = dt.Rows[i]["HINMEI_CD"]; break;
                            case "品名": row[m] = dt.Rows[i]["HINMEI_NAME"]; break;
                            case "種類CD": row[m] = dt.Rows[i]["SHURUI_CD"]; break;
                            case "種類": row[m] = dt.Rows[i]["SHURUI_NAME"]; break;
                            case "分類CD": row[m] = dt.Rows[i]["BUNRUI_CD"]; break;
                            case "分類": row[m] = dt.Rows[i]["BUNRUI_NAME"]; break;
                            case "正味重量": row[m] = Convert.ToDecimal(dt.Rows[i]["NET_JYUURYOU"]).ToString(jyuryouFormat); break;
                        }
                    }
                    csvDT_temp.Rows.Add(row);
                }
                DataRow tablerow = csvDT.NewRow();
                for (int i = 0; i < csvDT_temp.Rows.Count; i++)
                {
                    for (int m = 0; m < csvDT_temp.Columns.Count; m++)
                    {
                        if (string.IsNullOrEmpty(tablerow[m].ToString()) || csvDT_temp.Columns[m].ColumnName == "正味重量")
                        {
                            switch (csvDT_temp.Columns[m].ColumnName)
                            {
                                case "取引先CD": tablerow["取引先CD"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "取引先":tablerow["取引先"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "業者CD": tablerow["業者CD"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "業者":tablerow["業者"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "現場CD": tablerow["現場CD"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "現場":tablerow["現場"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "品名CD": tablerow["品名CD"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "品名": tablerow["品名"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "種類CD": tablerow["種類CD"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "種類": tablerow["種類"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "分類CD": tablerow["分類CD"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "分類": tablerow["分類"] = csvDT_temp.Rows[i][m].ToString(); break;
                                case "正味重量":
                                    if (!string.IsNullOrEmpty(csvDT_temp.Rows[i][m].ToString()))
                                    {
                                        string year = Convert.ToDateTime(csvDT_temp.Rows[i]["伝票日付"].ToString()).Year.ToString();
                                        string month = Convert.ToDateTime(csvDT_temp.Rows[i]["伝票日付"].ToString()).Month.ToString();
                                        if (month.Substring(0, 1) == "0")
                                            month = month.Substring(1, 1);
                                        if (string.IsNullOrEmpty(tablerow[year + "/" + month].ToString()))
                                            tablerow[year + "/" + month] = 0;
                                        tablerow[year + "/" + month] = Convert.ToDecimal(tablerow[year + "/" + month].ToString()) + Convert.ToDecimal(csvDT_temp.Rows[i][m].ToString());

                                        if (string.IsNullOrEmpty(tablerow["合計"].ToString()))
                                            tablerow["合計"] = 0;
                                        tablerow["合計"] = Convert.ToDecimal(tablerow["合計"].ToString()) + Convert.ToDecimal(csvDT_temp.Rows[i][m].ToString());

                                    }
                                    break;
                            }
 
                        }

                    }
                    if (key_cd2 == "")
                    {
                        if (i < csvDT_temp.Rows.Count - 1 && csvDT_temp.Rows[i][key_cd].ToString().Equals(csvDT_temp.Rows[i + 1][key_cd].ToString()))
                        {
                            continue;
                        }
                    }
                    else if (i < csvDT_temp.Rows.Count - 1 && csvDT_temp.Rows[i][key_cd].ToString().Equals(csvDT_temp.Rows[i + 1][key_cd].ToString()) && key_cd2 != "" && csvDT_temp.Rows[i][key_cd2].ToString().Equals(csvDT_temp.Rows[i + 1][key_cd2].ToString()))
                    {
                        continue;
                    }

                    if ((i < csvDT_temp.Rows.Count - 1) 
                    	 && ((string.IsNullOrEmpty(csvDT_temp.Rows[i][key_cd].ToString()))
                    	     || (csvDT_temp.Rows[i][key_cd].ToString() != csvDT_temp.Rows[i + 1][key_cd].ToString()) 
                    	     || ((key_cd2 != "" && i < csvDT_temp.Rows.Count - 1 
                    	          && csvDT_temp.Rows[i][key_cd2].ToString() != csvDT_temp.Rows[i + 1][key_cd2].ToString())) 
                    	     || string.IsNullOrEmpty(csvDT_temp.Rows[i][key_cd].ToString())))
                    {
                        csvDT.Rows.Add(tablerow);
                        tablerow = csvDT.NewRow();
                    }

                }
                    csvDT.Rows.Add(tablerow);
                    for (int i = 0; i < csvDT.Rows.Count; i++)
                    {
                        for (int n = out_put_head_array.Length; n < csvDT.Columns.Count; n++)
                        {

                            if(string.IsNullOrEmpty((csvDT.Rows[i][n]).ToString()))
                            {
                                csvDT.Rows[i][n] = "0";
                            }
                            csvDT.Rows[i][n] = Convert.ToDecimal(csvDT.Rows[i][n]).ToString(jyuryouFormat);
                           
                        }
                    }
             
                    // 出力先指定のポップアップを表示させる。
                    if (this.form.MsgLogic.MessageBoxShow("C013") == DialogResult.Yes)
                    {
                        CSVExport csvExport = new CSVExport();
                        // CSV出力
                        string title = key_cd.Replace("CD", "別");
                        csvExport.ConvertDataTableToCsv(csvDT, true, true, ConstCls.KEIRYOU_SUIIHYOU_TITLE + "（"+title+"）", this.form);
                    }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVPrint", ex);
                this.form.MsgLogic.MessageBoxShow("E245", "");
            }
        }
        #endregion

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool Search(DTOCls dto)
        {
            try
            {
                // とりあえず固定のクエリでデータ取ってくるだけ
                var dao = DaoInitUtility.GetComponent<DAOClass>();
                var dt = dao.GetDetailData(dto);

                if (0 < dt.Rows.Count)
                {
                    if (1 == dto.HoukokuShurui)
                    {
                        var reportLogic = new ReportClsR675(this.form);
                        reportLogic.CreateReport(dt, dto);

                    }
                    else if (2 == dto.HoukokuShurui)
                    {
                        var reportLogic = new ReportClsR676(this.form);
                        reportLogic.CreateReport(dt, dto);
                    }
                    else if (3 == dto.HoukokuShurui)
                    {
                        var reportLogic = new ReportClsR677(this.form);
                        reportLogic.CreateReport(dt, dto);
                    }
                }
                else
                {
                    this.form.MsgLogic.MessageBoxShow("C001");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.MsgLogic.MessageBoxShow("E093");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                return false;
            }
            return true;
        }

        #endregion - Function Key Proc -

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

        /// <summary>検索処理を実行し数値を取得する</summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Search()
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

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            try
            {
                this.form.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
                this.form.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

                //入力されない場合
                if (string.IsNullOrWhiteSpace(this.form.HIDUKE_FROM.Text) ||
                    string.IsNullOrWhiteSpace(this.form.HIDUKE_TO.Text))
                    return false;

                DateTime date_from = DateTime.Parse(this.form.HIDUKE_FROM.Text);
                DateTime date_to = DateTime.Parse(this.form.HIDUKE_TO.Text);

                //日付FROM > 日付TO 場合
                if (date_to.CompareTo(date_from) < 0)
                {
                    this.form.HIDUKE_FROM.IsInputErrorOccured = true;
                    this.form.HIDUKE_TO.IsInputErrorOccured = true;
                    this.form.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                    this.form.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;

                    if (this.form.rbtDenpyouDate.Checked)
                    {
                        string[] errorMsg = { "伝票日付From", "伝票日付To" };
                        this.form.MsgLogic.MessageBoxShow("E030", errorMsg);
                    }
                    else
                    {
                        string[] errorMsg = { "入力日付From", "入力日付To" };
                        this.form.MsgLogic.MessageBoxShow("E030", errorMsg);
                    }
                    this.form.HIDUKE_FROM.Focus();
                    return true;
                }

                DateTime dateTimeTmp = date_from.AddMonths(12);

                if (this.form.rbtSuiihyo.Checked)
                {
                    if (date_to >= dateTimeTmp)
                    {
                        string strMsg = "12カ月を超える日付範囲は指定出来ません。";
                        MessageBox.Show(strMsg, "アラート", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        this.form.HIDUKE_TO.Focus();

                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDate", ex);
                this.form.MsgLogic.MessageBoxShow("E245");
                return true;
            }
            return false;
        }

        #endregion

        #region 運搬業者チェック
        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        /// <returns></returns>
        internal bool UnpanGyoushaCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.UNPAN_GYOUSHA_CD_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.UNPAN_GYOUSHA_CD_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD_TO.Text))
            {
                return false;
            }

            string from = this.form.UNPAN_GYOUSHA_CD_FROM.Text;
            string to = this.form.UNPAN_GYOUSHA_CD_TO.Text;

            // 運搬業者FROM > 運搬業者TO 場合
            if (to.CompareTo(from) < 0)
            {
                this.form.UNPAN_GYOUSHA_CD_FROM.IsInputErrorOccured = true;
                this.form.UNPAN_GYOUSHA_CD_TO.IsInputErrorOccured = true;
                this.form.UNPAN_GYOUSHA_CD_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.UNPAN_GYOUSHA_CD_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "運搬業者From", "運搬業者To" };
                msgLogic.MessageBoxShow("E032", errorMsg);
                this.form.UNPAN_GYOUSHA_CD_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 品名チェック
        /// <summary>
        /// 品名チェック
        /// </summary>
        /// <returns></returns>
        internal bool HinmeiCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.HINMEI_CD_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.HINMEI_CD_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.HINMEI_CD_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.HINMEI_CD_TO.Text))
            {
                return false;
            }

            string from = this.form.HINMEI_CD_FROM.Text;
            string to = this.form.HINMEI_CD_TO.Text;

            // 品名FROM > 品名TO 場合
            if (to.CompareTo(from) < 0)
            {
                this.form.HINMEI_CD_FROM.IsInputErrorOccured = true;
                this.form.HINMEI_CD_TO.IsInputErrorOccured = true;
                this.form.HINMEI_CD_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.HINMEI_CD_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "品名From", "品名To" };
                msgLogic.MessageBoxShow("E032", errorMsg);
                this.form.HINMEI_CD_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 種類チェック
        /// <summary>
        /// 種類チェック
        /// </summary>
        /// <returns></returns>
        internal bool ShuruiCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.SHURUI_CD_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.SHURUI_CD_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.SHURUI_CD_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.SHURUI_CD_TO.Text))
            {
                return false;
            }

            string from = this.form.SHURUI_CD_FROM.Text;
            string to = this.form.SHURUI_CD_TO.Text;

            // 種類FROM > 種類TO 場合
            if (to.CompareTo(from) < 0)
            {
                this.form.SHURUI_CD_FROM.IsInputErrorOccured = true;
                this.form.SHURUI_CD_TO.IsInputErrorOccured = true;
                this.form.SHURUI_CD_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.SHURUI_CD_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "品名From", "品名To" };
                msgLogic.MessageBoxShow("E032", errorMsg);
                this.form.SHURUI_CD_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 分類チェック
        /// <summary>
        /// 分類チェック
        /// </summary>
        /// <returns></returns>
        internal bool BunruiCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.BUNRUI_CD_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.BUNRUI_CD_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.BUNRUI_CD_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.BUNRUI_CD_TO.Text))
            {
                return false;
            }

            string from = this.form.BUNRUI_CD_FROM.Text;
            string to = this.form.BUNRUI_CD_TO.Text;

            // 種類FROM > 種類TO 場合
            if (to.CompareTo(from) < 0)
            {
                this.form.BUNRUI_CD_FROM.IsInputErrorOccured = true;
                this.form.BUNRUI_CD_TO.IsInputErrorOccured = true;
                this.form.BUNRUI_CD_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.BUNRUI_CD_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "種類From", "種類To" };
                msgLogic.MessageBoxShow("E032", errorMsg);
                this.form.BUNRUI_CD_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 形態区分チェック
        /// <summary>
        /// 形態区分チェック
        /// </summary>
        /// <returns></returns>
        internal bool keitaiKbnCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.KEITAI_KBN_CD_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.KEITAI_KBN_CD_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.KEITAI_KBN_CD_TO.Text))
            {
                return false;
            }

            string from = this.form.KEITAI_KBN_CD_FROM.Text;
            string to = this.form.KEITAI_KBN_CD_TO.Text;

            // 形態区分FROM > 形態区分TO 場合
            if (to.CompareTo(from) < 0)
            {
                this.form.KEITAI_KBN_CD_FROM.IsInputErrorOccured = true;
                this.form.KEITAI_KBN_CD_TO.IsInputErrorOccured = true;
                this.form.KEITAI_KBN_CD_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.KEITAI_KBN_CD_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "形態区分From", "形態区分To" };
                msgLogic.MessageBoxShow("E032", errorMsg);
                this.form.KEITAI_KBN_CD_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region 業者CDテキストボックスの入力状態に応じて現場項目の状態を変更します
        /// <summary>
        /// 業者CDテキストボックスの入力状態に応じて現場項目の状態を変更します
        /// </summary>
        public void ChangeGenbaState()
        {
            LogUtility.DebugMethodStart();

            var gyoushaCdFrom = this.form.GYOUSHA_CD_FROM.Text;
            var gyoushaCdTo = this.form.GYOUSHA_CD_TO.Text;

            if (!String.IsNullOrEmpty(gyoushaCdFrom) && !String.IsNullOrEmpty(gyoushaCdTo) && gyoushaCdFrom == gyoushaCdTo)
            {
                this.form.GENBA_CD_FROM.Enabled = true;
                this.form.GENBA_NAME_FROM.Enabled = true;
                this.form.GENBA_FROM_POPUP.Enabled = true;
                this.form.GENBA_CD_TO.Enabled = true;
                this.form.GENBA_NAME_TO.Enabled = true;
                this.form.GENBA_TO_POPUP.Enabled = true;
            }
            else
            {
                this.form.GENBA_CD_FROM.Enabled = false;
                this.form.GENBA_NAME_FROM.Enabled = false;
                this.form.GENBA_FROM_POPUP.Enabled = false;
                this.form.GENBA_CD_TO.Enabled = false;
                this.form.GENBA_NAME_TO.Enabled = false;
                this.form.GENBA_TO_POPUP.Enabled = false;

                this.form.GENBA_CD_FROM.Text = String.Empty;
                this.form.GENBA_NAME_FROM.Text = String.Empty;
                this.form.GENBA_CD_TO.Text = String.Empty;
                this.form.GENBA_NAME_TO.Text = String.Empty;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 報告種類テキストボックスの入力状態に応じて現場項目の状態を変更します
        /// <summary>
        /// 報告種類テキストボックスの入力状態に応じて現場項目の状態を変更します
        /// </summary>
        public void ChangeHoukokuShuruiState()
        {
            LogUtility.DebugMethodStart();
            if (this.form.rbtMeisaihyo.Checked)
            {
                this.form.customPanelOutput_2.Enabled = false;
                this.form.customPanelOutput_3.Enabled = true;
                this.form.GROUP_TANI.Text = string.Empty;
            }

            if (this.form.rbtMotocho.Checked)
            {
                this.form.customPanelOutput_2.Enabled = true;
                this.form.GROUP_TANI.Text = string.Empty;
                this.form.GROUP_TANI.Tag = "【1、2、3】のいずれかで入力してください";
                this.form.GROUP_TANI.RangeSetting.Max = new decimal(new int[] { 3, 0, 0, 0 });
                this.form.GROUP_TANI.Text = "1";
                this.form.rbtTorihikisaki.Enabled = true;
                this.form.rbtGyousha.Enabled = true;
                this.form.rbtGenba.Enabled = true;
                this.form.rbtHinmei.Enabled = false;
                this.form.rbtBunrui.Enabled = false;
                this.form.rbtShurui.Enabled = false;
                this.form.customPanelOutput_3.Enabled = false;
                this.form.GROUP_TORIHIKISAKI.Checked = false;
                this.form.GROUP_GYOUSHA.Checked = false;
                this.form.GROUP_GENBA.Checked = false;
                this.form.GROUP_KEIRYOU_NUMBER.Checked = false;
            }

            if (this.form.rbtSuiihyo.Checked)
            {
                this.form.GROUP_TANI.Text = string.Empty;
                this.form.customPanelOutput_2.Enabled = true;
                this.form.GROUP_TANI.Tag = "【1、2、3、4、5、6】のいずれかで入力してください";
                this.form.GROUP_TANI.RangeSetting.Max = new decimal(new int[] { 6, 0, 0, 0 });
                this.form.GROUP_TANI.Text = "1";
                this.form.rbtTorihikisaki.Enabled = true;
                this.form.rbtGyousha.Enabled = true;
                this.form.rbtGenba.Enabled = true;
                this.form.rbtHinmei.Enabled = true;
                this.form.rbtBunrui.Enabled = true;
                this.form.rbtShurui.Enabled = true;
                this.form.customPanelOutput_3.Enabled = false;
                this.form.GROUP_TORIHIKISAKI.Checked = false;
                this.form.GROUP_GYOUSHA.Checked = false;
                this.form.GROUP_GENBA.Checked = false;
                this.form.GROUP_KEIRYOU_NUMBER.Checked = false;

                if (this.form.rbtKikan.Checked)
                {
                    var date = this.form.BaseForm.sysDate;
                    this.form.HIDUKE_FROM.Value = new DateTime(date.Year, date.Month, 1);
                    this.form.HIDUKE_TO.Value = new DateTime(date.Year, date.Month, 1).AddYears(1).AddDays(-1);
                }
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion
        #endregion - Methods -
    }
}