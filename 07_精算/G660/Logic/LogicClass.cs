using System;
using System.Data;
using System.Linq;
using System.Reflection;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using CommonChouhyouPopup.App;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou
{
    /// <summary>
    /// 未入金一覧出力画面ロジッククラス
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        /// <summary>
        /// ボタン設定XMLファイルパス
        /// </summary>
        private readonly string buttonInfoXmlPath = "Shougun.Core.Adjustment.ShiharaiMeisaiMeisaihyou.Setting.ButtonSetting.xml";

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        private HeaderBaseForm header;

        /// <summary>
        /// メインフォーム
        /// </summary>
        private UIForm form;

        private DAOClass dao;

        internal MessageBoxShowLogic errmessage;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">画面クラス</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dao = DaoInitUtility.GetComponent<DAOClass>();

            this.errmessage = new MessageBoxShowLogic();
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
        public bool CSV(DTOClass dto)
        {
            LogUtility.DebugMethodStart(dto);
            bool ret = true;
            try
            {
                var dao = DaoInitUtility.GetComponent<DAOClass>();
                DataTable dtResult;
                DataTable csvDT = new DataTable();
                DataRow rowTmp;
                DateTime dt;
                if (dto.SYOSIKI == ConstClass.SHOSHIKI_T)
                {
                    string[] csvHead = { "支払先CD", "支払先", "前回精算額", "出金額","調整額", "繰越額", "締",
                                       "今回取引額（税抜）", "消費税","今回取引額", "今回御精算額",
                                       "出金予定日", "支払年月日"};
                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }
                    dtResult = dao.GetPrintDataReportR661_Torihikisaki(dto);
                    if (0 < dtResult.Rows.Count)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            rowTmp = csvDT.NewRow();

                            if (dtResult.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                            {
                                rowTmp["支払先CD"] = dtResult.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if (dtResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["支払先"] = dtResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                            }

                            string shiharaiKeitaiKbn = dtResult.Rows[i]["SHIHARAI_KEITAI_KBN"] != null ? dtResult.Rows[i]["SHIHARAI_KEITAI_KBN"].ToString() : string.Empty;

                            decimal temp = this.GetDecimal(dtResult.Rows[i]["ZENKAI_KURIKOSI_GAKU"]);
                            temp = this.FractionCalc(temp, 3);
                            if ("1".Equals(shiharaiKeitaiKbn))
                            {
                                rowTmp["前回精算額"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["前回精算額"] = this.FormatDecimal(temp);
                            }

                            temp = this.GetDecimal(dtResult.Rows[i]["KONKAI_SHUKKIN_GAKU"]);
                            temp = this.FractionCalc(temp, 3);
                            if ("1".Equals(shiharaiKeitaiKbn))
                            {
                                rowTmp["出金額"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["出金額"] = this.FormatDecimal(temp);
                            }

                            //調整額
                            temp = this.GetDecimal(dtResult.Rows[i]["KONKAI_CHOUSEI_GAKU"]);
                            temp = this.FractionCalc(temp, 3);
                            if ("1".Equals(shiharaiKeitaiKbn))
                            {
                                rowTmp["調整額"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["調整額"] = this.FormatDecimal(temp);
                            }

                            //繰越額
                            temp = this.GetDecimal(dtResult.Rows[i]["KURIKOSHI_GAKU"]);
                            temp = this.FractionCalc(temp, 3);
                            if ("1".Equals(shiharaiKeitaiKbn))
                            {
                                rowTmp["繰越額"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["繰越額"] = this.FormatDecimal(temp);
                            }

                            if (dtResult.Rows[i]["SHIMEBI"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["SHIMEBI"].ToString()))
                            {
                                rowTmp["締"] = dtResult.Rows[i]["SHIMEBI"].ToString();
                            }

                            //今回取引額(税抜）
                            decimal konkaiShiharaiGaku = this.GetDecimal(dtResult.Rows[i]["KONKAI_SHIHARAI_GAKU"]);
                            konkaiShiharaiGaku = this.FractionCalc(konkaiShiharaiGaku, 3);
                            rowTmp["今回取引額（税抜）"] = this.FormatDecimal(konkaiShiharaiGaku);

                            //消費税
                            decimal shouhizei = this.GetDecimal(dtResult.Rows[i]["SHOUHIZEI"]);
                            rowTmp["消費税"] = this.FormatDecimal(shouhizei);

                            //今回取引額 = 今回取引額(税抜) ＋ 消費税 
                            decimal konkaiTorihikisakiGaku = konkaiShiharaiGaku + shouhizei;
                            konkaiTorihikisakiGaku = this.FractionCalc(konkaiTorihikisakiGaku, 3);
                            rowTmp["今回取引額"] = this.FormatDecimal(konkaiTorihikisakiGaku);

                            //今回御請求額
                            temp = this.GetDecimal(dtResult.Rows[i]["KONKAI_SEISAN_GAKU"]);
                            temp = this.FractionCalc(temp, 3);
                            rowTmp["今回御精算額"] = this.FormatDecimal(temp);

                            if (dtResult.Rows[i]["SHUKKIN_YOTEI_BI"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["SHUKKIN_YOTEI_BI"].ToString()))
                            {
                                dt = Convert.ToDateTime(dtResult.Rows[i]["SHUKKIN_YOTEI_BI"].ToString());
                                rowTmp["出金予定日"] = dt.ToString("yyyy/MM/dd");
                            }

                            if (dtResult.Rows[i]["SEISAN_DATE"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["SEISAN_DATE"].ToString()))
                            {
                                dt = Convert.ToDateTime(dtResult.Rows[i]["SEISAN_DATE"].ToString());
                                rowTmp["支払年月日"] = dt.ToString("yyyy/MM/dd");
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
                else
                {
                    string[] csvHead = { "支払先CD", "支払先", "業者CD", "業者", "現場CD","現場",
                                           "締","今回取引額（税抜）", "消費税","今回取引額",
                                         "出金予定日", "支払年月日"};
                    for (int i = 0; i < csvHead.Length; i++)
                    {
                        csvDT.Columns.Add(csvHead[i]);
                    }
                    dtResult = dao.GetPrintDataReportR661_GyoushaGenba(dto);
                    if (0 < dtResult.Rows.Count)
                    {
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            rowTmp = csvDT.NewRow();

                            if (dtResult.Rows[i]["TORIHIKISAKI_CD"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["TORIHIKISAKI_CD"].ToString()))
                            {
                                rowTmp["支払先CD"] = dtResult.Rows[i]["TORIHIKISAKI_CD"].ToString();
                            }

                            if (dtResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["支払先"] = dtResult.Rows[i]["TORIHIKISAKI_NAME_RYAKU"].ToString();
                            }

                            if (dtResult.Rows[i]["GYOUSHA_CD"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["GYOUSHA_CD"].ToString()))
                            {
                                rowTmp["業者CD"] = dtResult.Rows[i]["GYOUSHA_CD"].ToString();
                            }

                            if (dtResult.Rows[i]["GYOUSHA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["業者"] = dtResult.Rows[i]["GYOUSHA_NAME_RYAKU"].ToString();
                            }

                            if (dtResult.Rows[i]["GENBA_CD"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["GENBA_CD"].ToString()))
                            {
                                rowTmp["現場CD"] = dtResult.Rows[i]["GENBA_CD"].ToString();
                            }

                            if (dtResult.Rows[i]["GENBA_NAME_RYAKU"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["GENBA_NAME_RYAKU"].ToString()))
                            {
                                rowTmp["現場"] = dtResult.Rows[i]["GENBA_NAME_RYAKU"].ToString();
                            }

                            if (dtResult.Rows[i]["SHIMEBI"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["SHIMEBI"].ToString()))
                            {
                                rowTmp["締"] = dtResult.Rows[i]["SHIMEBI"].ToString();
                            }

                            //今回取引額(税抜）
                            decimal konkaiShiharaiGaku = this.GetDecimal(dtResult.Rows[i]["KONKAI_SHIHARAI_GAKU"]);
                            konkaiShiharaiGaku = this.FractionCalc(konkaiShiharaiGaku, 3);
                            rowTmp["今回取引額（税抜）"] = this.FormatDecimal(konkaiShiharaiGaku);

                            //消費税
                            decimal shouhizei = this.GetDecimal(dtResult.Rows[i]["SHOUHIZEI"]);
                            rowTmp["消費税"] = this.FormatDecimal(shouhizei);

                            decimal konkaiSeikyuGaku = this.GetDecimal(dtResult.Rows[i]["KONKAI_SEISAN_GAKU"]);
                            konkaiSeikyuGaku = this.FractionCalc(konkaiSeikyuGaku, 3);
                            rowTmp["今回取引額"] = this.FormatDecimal(konkaiSeikyuGaku);

                            if (dtResult.Rows[i]["SHUKKIN_YOTEI_BI"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["SHUKKIN_YOTEI_BI"].ToString()))
                            {
                                dt = Convert.ToDateTime(dtResult.Rows[i]["SHUKKIN_YOTEI_BI"].ToString());
                                rowTmp["出金予定日"] = dt.ToString("yyyy/MM/dd");
                            }

                            if (dtResult.Rows[i]["SEISAN_DATE"] != null && !string.IsNullOrEmpty(dtResult.Rows[i]["SEISAN_DATE"].ToString()))
                            {
                                dt = Convert.ToDateTime(dtResult.Rows[i]["SEISAN_DATE"].ToString());
                                rowTmp["支払年月日"] = dt.ToString("yyyy/MM/dd");
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
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CsvPrint", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CsvPrint", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 帳票出力データを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        public bool Preview(DTOClass dto)
        {
            LogUtility.DebugMethodStart(dto);
            bool ret = true;
            try
            {
                var dao = DaoInitUtility.GetComponent<DAOClass>();
                DataTable dtResult;
                if (dto.SYOSIKI == ConstClass.SHOSHIKI_T)
                {
                    dtResult = dao.GetPrintDataReportR661_Torihikisaki(dto);
                    if (0 < dtResult.Rows.Count)
                    {
                        ReportInfoR661_Torihikisaki reportInfo = new ReportInfoR661_Torihikisaki(dtResult, dto);
                        using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                        {
                            popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                            popup.ShowDialog();
                            popup.Dispose();
                        }
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
                else
                {
                    dtResult = dao.GetPrintDataReportR661_GyoushaGenba(dto);
                    if (0 < dtResult.Rows.Count)
                    {
                        ReportInfoR661_GyoushaGenba reportInfo = new ReportInfoR661_GyoushaGenba(dtResult, dto);
                        using (FormReportPrintPopup popup = new FormReportPrintPopup(reportInfo, this.form.WindowId))
                        {
                            popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.form.WindowId);
                            popup.ShowDialog();
                            popup.Dispose();
                        }
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001");
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Preview", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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

            // CSVボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.ButtonFunc5_Clicked);

            // 表示ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ButtonFunc7_Clicked);

            // 閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.ButtonFunc12_Clicked);

            this.form.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(this.HIDUKE_TO_MouseDoubleClick);

            LogUtility.DebugMethodEnd();
        }

        internal M_TORIHIKISAKI GetMaxTorihikisaki()
        {
            return dao.GetMaxTorihikisaki();
        }
        internal M_TORIHIKISAKI GetMinTorihikisaki()
        {
            return dao.GetMinTorihikisaki();
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

        #region 20151027 hoanghm #13692
        internal void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.HIDUKE_TO.Text = this.form.HIDUKE_FROM.Text;

            LogUtility.DebugMethodEnd();
        }
        #endregion
        
        private string FormatDecimal(object strdata)
        {
            string str = Convert.ToString(strdata);

            if (!string.IsNullOrEmpty(str))
            {
                decimal dtmp = decimal.Parse(str);
                str = dtmp.ToString("#,##0");
            }
            return str;
        }

        private decimal GetDecimal(object param)
        {
            if (param != null && param != DBNull.Value)
            {
                return Convert.ToDecimal(param);
            }
            else
            {
                return 0;
            }

        }

        private decimal FractionCalc(decimal kingaku, int calcCD)
        {
            decimal returnVal = 0;		// 戻り値
            decimal sign = 1;           // 1(正) or -1(負)

            if (kingaku < 0)
                sign = -1;

            switch ((TAX_HASUU_CD)calcCD)
            {
                case TAX_HASUU_CD.CEILING:
                    returnVal = Math.Ceiling(Math.Abs(kingaku)) * sign;
                    break;
                case TAX_HASUU_CD.FLOOR:
                    returnVal = Math.Floor(Math.Abs(kingaku)) * sign;
                    break;
                case TAX_HASUU_CD.ROUND:
                    returnVal = Math.Round(Math.Abs(kingaku), 0, MidpointRounding.AwayFromZero) * sign;
                    break;
                default:
                    // 何もしない
                    returnVal = kingaku;
                    break;
            }

            return returnVal;
        }
    }

    public enum TAX_HASUU_CD : short
    {
        CEILING = 1,    // 切り上げ
        FLOOR,          // 切り捨て
        ROUND,          // 四捨五入
    }
}
