using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using System.Collections.Generic;

namespace Shougun.Core.ReceiptPayManagement.NyuukinShuukeiChouhyou
{
    /// <summary>
    /// 入金集計表出力画面ロジッククラス
    /// </summary>
    internal class NyuukinShuukeihyouLogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.NyuukinShuukeiChouhyou.Setting.NyuukinShuukeihyouButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm_NyuukinShuukeihyou form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public NyuukinShuukeihyouLogicClass(UIForm_NyuukinShuukeihyou targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面を初期化します
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

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
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
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
            this.header.lb_title.Text = WINDOW_TITLEExt.ToTitleString(this.form.WindowId);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public void CSV(NyuukinShuukeihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dto);
            DataTable csvDT = new DataTable();
            DataRow rowTmp;
            string headStr = "";
            string[] csvHead;

            var dao = DaoInitUtility.GetComponent<INyuukinShuukeihyouDao>();
            DataTable dt;
            Dictionary<string, string> GetData = new Dictionary<string,string>();
            dt = dao.GetNyuukinShuukeiData(CreateNyuukinShuukeiDataForCsv(dto));

            if (0 < dt.Rows.Count)
            {
                for (int i = 0; i < dto.Pattern.PatternColumnList.Count; i++)
                {
                    string butsuriName = dto.Pattern.GetColumnSelectDetail(i + 1).BUTSURI_NAME;
                    switch (butsuriName)
                    {
                        case "TORIHIKISAKI_CD":
                            headStr = headStr + "取引先CD,";
                            headStr = headStr + "取引先,";
                            GetData.Add("取引先CD", butsuriName);
                            GetData.Add("取引先", butsuriName + "_NAME");
                            break;
                        case "NYUUKINSAKI_CD":
                            headStr = headStr + "入金先CD,";
                            headStr = headStr + "入金先,";
                            GetData.Add("入金先CD", butsuriName);
                            GetData.Add("入金先", butsuriName + "_NAME");
                            break;
                        case "BANK_CD":
                            headStr = headStr + "銀行CD,";
                            headStr = headStr + "銀行,";
                            GetData.Add("銀行CD", butsuriName);
                            GetData.Add("銀行", butsuriName + "_NAME");
                            break;
                        case "BANK_SHITEN_CD":
                            headStr = headStr + "銀行支店CD,";
                            headStr = headStr + "銀行支店,";
                            GetData.Add("銀行支店CD", butsuriName);
                            GetData.Add("銀行支店", butsuriName + "_NAME");
                            break;
                        case "NYUUSHUKKIN_KBN_CD":
                            headStr = headStr + "入金区分CD,";
                            headStr = headStr + "入金区分,";
                            GetData.Add("入金区分CD", butsuriName);
                            GetData.Add("入金区分", butsuriName + "_NAME");
                            break;
                    }
                }

                headStr = headStr + "金額";

                csvHead = headStr.Split(',');
                for (int i = 0; i < csvHead.Length; i++)
                {
                    csvDT.Columns.Add(csvHead[i]);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowTmp = csvDT.NewRow();
                    foreach (string key in GetData.Keys)
                    {
                        if (dt.Rows[i][GetData[key]] != null && !string.IsNullOrEmpty(dt.Rows[i][GetData[key]].ToString()))
                        {
                            rowTmp[key] = dt.Rows[i][GetData[key]].ToString();
                        }
                    }

                    if (dt.Rows[i]["KINGAKU"] != null && !string.IsNullOrEmpty(dt.Rows[i]["KINGAKU"].ToString()))
                    {
                        rowTmp["金額"] = Convert.ToDecimal(dt.Rows[i]["KINGAKU"].ToString()).ToString("#,##0");
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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public void Search(NyuukinShuukeihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dto);

            var dao = DaoInitUtility.GetComponent<INyuukinShuukeihyouDao>();
            DataTable dt;

            //dt = dao.GetNyuukinShuukeiData(dto);
            dt = dao.GetNyuukinShuukeiData(CreateNyuukinShuukeiDataQuery(dto));

            if (0 < dt.Rows.Count)
            {
                var reportLogic = new NyuukinShuukeihyouReportClass();
                reportLogic.CreateReport(dt, dto);
            }
            else
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C001");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定を作成します
        /// </summary>
        /// <returns>ボタン設定</returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] ret;

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            ret = buttonSetting.LoadButtonSetting(thisAssembly, this.buttonInfoXmlPath);

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// ボタンを初期化します
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
        /// イベントを初期化します
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 新規ボタン(F1)イベント生成
            parentForm.bt_func1.Click += new EventHandler(this.form.ButtonFunc1_Clicked);

            // 修正ボタン(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.ButtonFunc2_Clicked);

            // 削除ボタン(F4)イベント生成
            parentForm.bt_func4.Click += new EventHandler(this.form.ButtonFunc4_Clicked);

            // CSVボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // 表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 銀行支店を取得します（支店が複数ある場合は、先頭の支店のみ。口座情報が必要な場合は使用しないこと。）
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <param name="bankShitenCd">銀行支店CD</param>
        /// <returns>銀行支店エンティティ</returns>
        internal M_BANK_SHITEN GetBankShiten(string bankCd, string bankShitenCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(bankCd, bankShitenCd);

            M_BANK_SHITEN ret = null;
            catchErr = true;

            try
            {
                var dao = DaoInitUtility.GetComponent<IM_BANK_SHITENDao>();
                var mBankShitenList = dao.GetAllValidData(new M_BANK_SHITEN() { BANK_CD = bankCd, BANK_SHITEN_CD = bankShitenCd, ISNOT_NEED_DELETE_FLG= true });
                if (mBankShitenList.Count() > 0)
                {
                    ret = mBankShitenList.FirstOrDefault();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetBankShiten", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetBankShiten", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(ret, catchErr);

            return ret;
        }

        public int Search()
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

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// SQL作成
        /// </summary>
        /// <returns></returns>
        private string CreateNyuukinShuukeiDataQuery(NyuukinShuukeihyouDtoClass dto)
        {
            StringBuilder select = new StringBuilder();
            StringBuilder selectIn = new StringBuilder();
            StringBuilder analysis = new StringBuilder();
            StringBuilder caseJudge = new StringBuilder();

            PatternDto patternDto = dto.Pattern;
            ArrayList cond = new ArrayList();
            for (int i = 0; i < patternDto.PatternColumnList.Count; i++)
            {
                string butsuriName = patternDto.GetColumnSelectDetail(i + 1).BUTSURI_NAME;
                switch (butsuriName)
                {
                    case "TORIHIKISAKI_CD":
                        selectIn.Append("   CONVERT(VARCHAR, ET." + butsuriName + ") + ' ' + ");
                        selectIn.Append("TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU ");
                        break;
                    case "NYUUKINSAKI_CD":
                        selectIn.Append("   CONVERT(VARCHAR, ET." + butsuriName + ") + ' ' + ");
                        selectIn.Append("NYUUKINSAKI.NYUUKINSAKI_NAME_RYAKU ");
                        break;
                    case "BANK_CD":
                        selectIn.Append("   CONVERT(VARCHAR, ET." + butsuriName + ") + ' ' + ");
                        selectIn.Append("BANK.BANK_NAME_RYAKU ");
                        break;
                    case "BANK_SHITEN_CD":
                        selectIn.Append("   CONVERT(VARCHAR, ET." + butsuriName + ") + ' ' + ");
                        selectIn.Append("BANK_SHITEN.BANK_SHIETN_NAME_RYAKU ");
                        break;
                    case "NYUUSHUKKIN_KBN_CD":
                        selectIn.Append("   CONVERT(VARCHAR, DT." + butsuriName + ") + ' ' + ");
                        selectIn.Append("DT.NYUUSHUKKIN_KBN_NAME ");
                        break;
                }
                selectIn.Append(butsuriName + ",");
                select.Append(butsuriName + ",");
                analysis.Append(" ,(SELECT " + butsuriName + " FROM TEMP B WHERE B.ROWNUM = A.ROWNUM - 1) PREV_" + butsuriName);
                analysis.Append(" ,(SELECT " + butsuriName + " FROM TEMP B WHERE B.ROWNUM = A.ROWNUM + 1) NEXT_" + butsuriName);

                cond.Add(butsuriName + " = PREV_" + butsuriName);
                caseJudge.Append(" CASE ");
                caseJudge.Append("  WHEN ROWNUM = 1 THEN ");
                caseJudge.Append(butsuriName);
                caseJudge.Append("  ELSE ");
                caseJudge.Append("   CASE ");
                caseJudge.Append("    WHEN " + string.Join(" AND ", cond.ToArray()) + " THEN ");
                caseJudge.Append("     '' ");
                caseJudge.Append("    ELSE ");
                caseJudge.Append(butsuriName);
                caseJudge.Append("   END ");
                caseJudge.Append(" END " + butsuriName + ",");

                if (dto.ShuukeiIsChecked.Values.Contains(butsuriName))
                {
                    foreach (string s in dto.ShuukeiIsChecked.Values)
                    {
                        if (s == butsuriName)
                        {
                            analysis.Append(" ,(SELECT " + butsuriName + "_RNK FROM TEMP B WHERE B.ROWNUM = A.ROWNUM + 1) NEXT_" + butsuriName + "_RNK");

                            caseJudge.Append(" '" + dto.ShuukeiIsChecked.First(x => x.Value == butsuriName).Key + "合計' ");
                            caseJudge.Append(butsuriName + "_NAME, ");
                            break;
                        }
                    }
                }
            }

            ArrayList array = new ArrayList();
            string rank = string.Empty;
            foreach (string name in dto.ShuukeiIsChecked.Values)
            {
                array.Add(name);
                rank += "   ,DENSE_RANK() OVER (ORDER BY " + string.Join(",", array.ToArray()) + ") " + name + "_RNK ";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" WITH TEMP ");
            sb.Append("( ");
            sb.Append(select);
            sb.Append("  KINGAKU ");
            sb.Append(" ,ROWNUM ");
            foreach (string rankName in array)
            {
                sb.Append(" ," + rankName + "_RNK ");
            }
            sb.Append(") AS ");
            sb.Append("( ");
            sb.Append("SELECT ");
            sb.Append(select);
            sb.Append("  SUM(KINGAKU) KINGAKU ");
            sb.Append(" ,ROW_NUMBER() OVER (ORDER BY " + select.ToString().TrimEnd(',') + ") ROWNUM ");
            sb.Append(rank);
            sb.Append("FROM ");
            sb.Append("( ");
            sb.Append(" SELECT ");
            sb.Append(selectIn);
            sb.Append("  ISNULL(DT.KINGAKU, 0) KINGAKU ");
            sb.Append(" FROM T_NYUUKIN_ENTRY ET ");
            sb.Append(" LEFT JOIN ( ");
            sb.Append("  SELECT SYSTEM_ID ");
            sb.Append("   ,SEQ ");
            sb.Append("   ,D.NYUUSHUKKIN_KBN_CD ");
            sb.Append("   ,K.NYUUSHUKKIN_KBN_NAME ");
            sb.Append("   ,D.KINGAKU ");
            sb.Append("  FROM T_NYUUKIN_DETAIL D ");
            sb.Append("   ,M_NYUUSHUKKIN_KBN K ");
            sb.Append("  WHERE D.NYUUSHUKKIN_KBN_CD = K.NYUUSHUKKIN_KBN_CD ");
            sb.Append("  ) AS DT ON ET.SYSTEM_ID = DT.SYSTEM_ID ");
            sb.Append("  AND ET.SEQ = DT.SEQ ");
            sb.Append(" LEFT JOIN M_KYOTEN KYOTEN ON KYOTEN.KYOTEN_CD = ET.KYOTEN_CD ");
            sb.Append(" LEFT JOIN M_TORIHIKISAKI TORIHIKISAKI ON TORIHIKISAKI.TORIHIKISAKI_CD = ET.TORIHIKISAKI_CD ");
            sb.Append(" LEFT JOIN M_NYUUKINSAKI NYUUKINSAKI ON NYUUKINSAKI.NYUUKINSAKI_CD = ET.NYUUKINSAKI_CD ");
            sb.Append(" LEFT JOIN M_BANK BANK ON BANK.BANK_CD = ET.BANK_CD ");
            sb.Append(" LEFT JOIN M_BANK_SHITEN BANK_SHITEN ON BANK_SHITEN.BANK_CD = ET.BANK_CD ");
            sb.Append("  AND BANK_SHITEN.BANK_SHITEN_CD = ET.BANK_SHITEN_CD ");
            sb.Append("  AND BANK_SHITEN.KOUZA_SHURUI = ET.KOUZA_SHURUI ");
            sb.Append("  AND BANK_SHITEN.KOUZA_NO = ET.KOUZA_NO ");
            sb.Append(" WHERE ");
            sb.Append("  ET.DELETE_FLG = 0 ");
            sb.Append(CreateWhere(dto));
            sb.Append(" ) BASE ");
            sb.Append(" GROUP BY ");
            sb.Append(select.ToString().TrimEnd(','));
            sb.Append(") ");
            sb.Append("SELECT ");
            sb.Append(caseJudge);
            sb.Append("  KINGAKU ");
            foreach (string rankName in array)
            {
                sb.Append(" ," + rankName + "_RNK ");
            }
            sb.Append(" FROM ");
            sb.Append(" ( ");
            sb.Append("  SELECT ");
            sb.Append(select.ToString().TrimEnd(','));
            sb.Append(analysis);
            sb.Append("   ,KINGAKU ");
            sb.Append("   ,ROWNUM ");
            sb.Append(rank);
            sb.Append("  FROM TEMP A ");
            sb.Append(" ) AA ");
            sb.Append(" ORDER BY ROWNUM ");

            return sb.ToString();
        }

        /// <summary>
        /// SQL FOR CSV作成
        /// </summary>
        /// <returns></returns>
        private string CreateNyuukinShuukeiDataForCsv(NyuukinShuukeihyouDtoClass dto)
        {
            StringBuilder select = new StringBuilder();
            StringBuilder selectIn = new StringBuilder();
            StringBuilder getname = new StringBuilder();

            PatternDto patternDto = dto.Pattern;
            ArrayList cond = new ArrayList();
            for (int i = 0; i < patternDto.PatternColumnList.Count; i++)
            {
                string butsuriName = patternDto.GetColumnSelectDetail(i + 1).BUTSURI_NAME;
                switch (butsuriName)
                {
                    case "TORIHIKISAKI_CD":
                        selectIn.Append("TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU" + " as " + butsuriName + "_NAME" + ",");
                        selectIn.Append("ET." + butsuriName + " as ");
                        getname.Append(butsuriName + "_NAME" + ",");
                        break;
                    case "NYUUKINSAKI_CD":
                        selectIn.Append("NYUUKINSAKI.NYUUKINSAKI_NAME_RYAKU" + " as " + butsuriName + "_NAME" + ",");
                        selectIn.Append("ET." + butsuriName + " as ");
                        getname.Append(butsuriName + "_NAME" + ",");
                        break;
                    case "BANK_CD":
                        selectIn.Append("BANK.BANK_NAME_RYAKU" + " as " + butsuriName + "_NAME" + ",");
                        selectIn.Append("ET." + butsuriName + " as ");
                        getname.Append(butsuriName + "_NAME" + ",");
                        break;
                    case "BANK_SHITEN_CD":
                        selectIn.Append("BANK_SHITEN.BANK_SHIETN_NAME_RYAKU" + " as " + butsuriName + "_NAME" + ",");
                        selectIn.Append("ET." + butsuriName + " as ");
                        getname.Append(butsuriName + "_NAME" + ",");
                        break;
                    case "NYUUSHUKKIN_KBN_CD":
                        selectIn.Append("DT.NYUUSHUKKIN_KBN_NAME" + " as " + butsuriName + "_NAME" + ",");
                        selectIn.Append("DT." + butsuriName + " as ");
                        getname.Append(butsuriName + "_NAME" + ",");
                        break;
                }
                selectIn.Append(butsuriName + ",");
                select.Append(butsuriName + ",");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" WITH TEMP ");
            sb.Append("( ");
            sb.Append(select);
            sb.Append(getname);
            sb.Append("  KINGAKU ");
            sb.Append(" ,ROWNUM ");
            sb.Append(") AS ");
            sb.Append("( ");
            sb.Append("SELECT ");
            sb.Append(select);
            sb.Append(getname);
            sb.Append("  SUM(KINGAKU) KINGAKU ");
            sb.Append(" ,ROW_NUMBER() OVER (ORDER BY " + select.ToString().TrimEnd(',') + ") ROWNUM ");
            sb.Append("FROM ");
            sb.Append("( ");
            sb.Append(" SELECT ");
            sb.Append(selectIn);
            sb.Append("  ISNULL(DT.KINGAKU, 0) KINGAKU ");
            sb.Append(" FROM T_NYUUKIN_ENTRY ET ");
            sb.Append(" LEFT JOIN ( ");
            sb.Append("  SELECT SYSTEM_ID ");
            sb.Append("   ,SEQ ");
            sb.Append("   ,D.NYUUSHUKKIN_KBN_CD ");
            sb.Append("   ,K.NYUUSHUKKIN_KBN_NAME ");
            sb.Append("   ,D.KINGAKU ");
            sb.Append("  FROM T_NYUUKIN_DETAIL D ");
            sb.Append("   ,M_NYUUSHUKKIN_KBN K ");
            sb.Append("  WHERE D.NYUUSHUKKIN_KBN_CD = K.NYUUSHUKKIN_KBN_CD ");
            sb.Append("  ) AS DT ON ET.SYSTEM_ID = DT.SYSTEM_ID ");
            sb.Append("  AND ET.SEQ = DT.SEQ ");
            sb.Append(" LEFT JOIN M_KYOTEN KYOTEN ON KYOTEN.KYOTEN_CD = ET.KYOTEN_CD ");
            sb.Append(" LEFT JOIN M_TORIHIKISAKI TORIHIKISAKI ON TORIHIKISAKI.TORIHIKISAKI_CD = ET.TORIHIKISAKI_CD ");
            sb.Append(" LEFT JOIN M_NYUUKINSAKI NYUUKINSAKI ON NYUUKINSAKI.NYUUKINSAKI_CD = ET.NYUUKINSAKI_CD ");
            sb.Append(" LEFT JOIN M_BANK BANK ON BANK.BANK_CD = ET.BANK_CD ");
            sb.Append(" LEFT JOIN M_BANK_SHITEN BANK_SHITEN ON BANK_SHITEN.BANK_CD = ET.BANK_CD ");
            sb.Append("  AND BANK_SHITEN.BANK_SHITEN_CD = ET.BANK_SHITEN_CD ");
            sb.Append("  AND BANK_SHITEN.KOUZA_SHURUI = ET.KOUZA_SHURUI ");
            sb.Append("  AND BANK_SHITEN.KOUZA_NO = ET.KOUZA_NO ");
            sb.Append(" WHERE ");
            sb.Append("  ET.DELETE_FLG = 0 ");
            sb.Append(CreateWhere(dto));
            sb.Append(" ) BASE ");
            sb.Append(" GROUP BY ");
            sb.Append(select.ToString());
            sb.Append(getname.ToString().TrimEnd(','));
            sb.Append(") ");
            sb.Append("SELECT ");
            sb.Append(select);
            sb.Append(getname);
            sb.Append("  KINGAKU ");
            sb.Append("  ,ROWNUM ");
            sb.Append("  FROM TEMP A ");
            sb.Append(" ORDER BY ROWNUM ");

            return sb.ToString();
        }

        /// <summary>
        /// 条件作成
        /// </summary>
        /// <returns></returns>
        private string CreateWhere(NyuukinShuukeihyouDtoClass dto)
        {
            StringBuilder sb = new StringBuilder();

            if (dto.KyotenCd != 99)
            {
                sb.AppendFormat(" AND ET.KYOTEN_CD = {0} ", dto.KyotenCd);
            }
            if (dto.DateShuruiCd == 1)
            {
                sb.AppendFormat(" AND ET.DENPYOU_DATE >= '{0}' ", dto.DateFrom);
                sb.AppendFormat(" AND ET.DENPYOU_DATE <= '{0}' ", dto.DateTo);
            }
            if (dto.DateShuruiCd == 2)
            {
                sb.AppendFormat(" AND CONVERT(date, ET.UPDATE_DATE, 111) >= '{0}' ", dto.DateFrom);
                sb.AppendFormat(" AND CONVERT(date, ET.UPDATE_DATE, 111) <= '{0}' ", dto.DateTo);
            }
            if (!string.IsNullOrEmpty(dto.NyuukinsakiCdFrom))
            {
                sb.AppendFormat(" AND ET.NYUUKINSAKI_CD >= '{0}' ", dto.NyuukinsakiCdFrom);
            }
            if (!string.IsNullOrEmpty(dto.NyuukinsakiCdTo))
            {
                sb.AppendFormat(" AND ET.NYUUKINSAKI_CD <= '{0}'", dto.NyuukinsakiCdTo);
            }
            if (!string.IsNullOrEmpty(dto.TorihikisakiCdFrom))
            {
                sb.AppendFormat(" AND ET.TORIHIKISAKI_CD >= '{0}'", dto.TorihikisakiCdFrom);
            }
            if (!string.IsNullOrEmpty(dto.TorihikisakiCdTo))
            {
                sb.AppendFormat(" AND ET.TORIHIKISAKI_CD <= '{0}'", dto.TorihikisakiCdTo);
            }
            if (!string.IsNullOrEmpty(dto.BankCdFrom))
            {
                sb.AppendFormat(" AND ET.BANK_CD >= '{0}'", dto.BankCdFrom);
            }
            if (!string.IsNullOrEmpty(dto.BankCdTo))
            {
                sb.AppendFormat(" AND ET.BANK_CD <= '{0}'", dto.BankCdTo);
            }
            if (!string.IsNullOrEmpty(dto.BankShitenCdFrom))
            {
                sb.AppendFormat(" AND ET.BANK_SHITEN_CD >= '{0}'", dto.BankShitenCdFrom);
            }
            if (!string.IsNullOrEmpty(dto.BankShitenCdTo))
            {
                sb.AppendFormat(" AND ET.BANK_SHITEN_CD <= '{0}'", dto.BankShitenCdTo);
            }
            if (!string.IsNullOrEmpty(dto.NyuukinKbnCdFrom))
            {
                sb.AppendFormat(" AND DT.NYUUSHUKKIN_KBN_CD >= {0}", dto.NyuukinKbnCdFrom);
            }
            if (!string.IsNullOrEmpty(dto.NyuukinKbnCdTo))
            {
                sb.AppendFormat(" AND DT.NYUUSHUKKIN_KBN_CD <= {0}", dto.NyuukinKbnCdTo);
            }

            return sb.ToString();
        }
    }
}
