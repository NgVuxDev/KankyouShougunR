using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Utility;
using r_framework.Entity;
using r_framework.Dao;
using System.Data.SqlTypes;

namespace Shougun.Core.Billing.SeikyuuMeisaihyouShutsuryoku
{
    #region - Classes -

    //VINHNV R636 請求明細表（取引先別）
    public class ReportInfoR636 : ReportInfoBase
    {
        private DataTable chouhyouDataTable = new DataTable();
        public string OutputFormFullPathName { get; set; }
        public string OutputFormLayout { get; set; }
        private M_KYOTEN mKyoten { get; set; }
        private DTOClass searchConditionDto { get; set; }
        private Encoding encoding = Encoding.GetEncoding("Shift_JIS");
        private byte[] byteArray;
        #region - Constructors -
       /// <summary>
       /// 
       /// </summary>
       /// <param name="dataTableInput"></param>
       /// <param name="dto"></param>
        public ReportInfoR636(DataTable dataTableInput, DTOClass dto)
            : base(dataTableInput)
        {
           
            this.Title = "請求明細表（取引先別）";          
            this.OutputFormFullPathName = "./Template/R636-Form.xml";
            this.OutputFormLayout = "LAYOUT1";
            this.searchConditionDto = dto;

            var mKyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            if (!dto.KYOTEN_CD.IsNull)
            {
                this.mKyoten = mKyotenDao.GetDataByCd(dto.KYOTEN_CD.Value.ToString());
            }

            this.InputDataToMem(dataTableInput);
            this.SetRecord(chouhyouDataTable);
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, chouhyouDataTable);
        }

        #endregion - Constructors -

        #region - Methods -
        private string GetStringByLength(object param, int length)
        {
            string inputString = Convert.ToString(param);
            byteArray = encoding.GetBytes(inputString);
            if (byteArray.Length > length)
            {
                return encoding.GetString(byteArray, 0, length);
            }
            else
            {
                return inputString;
            }
        }
        private string GetStringDate(object param)
        {
            string dateTimeFormat = "yyyy/MM/dd";
            try
            {
                if (param != null && !String.IsNullOrEmpty(Convert.ToString(param)))
                {
                    if (param.GetType() == typeof(SqlDateTime))
                    {
                        if (!((SqlDateTime)param).IsNull)
                        {
                            return ((SqlDateTime)param).Value.ToString(dateTimeFormat);
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else if (param.GetType() == typeof(DateTime))
                    {
                        return ((DateTime)param).ToString(dateTimeFormat);
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(param.ToString());
                        return dt.ToString(dateTimeFormat);
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
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
        /// <summary>消費税端数CD</summary>
        private enum TAX_HASUU_CD : short
        {
            CEILING = 1,    // 切り上げ
            FLOOR,          // 切り捨て
            ROUND,          // 四捨五入
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kingaku"></param>
        /// <param name="calcCD"></param>
        /// <returns></returns>
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
        private string FormatDecimal(object strdata, int mode)
        {
            string str = Convert.ToString(strdata);

            if (!string.IsNullOrEmpty(str))
            {
                decimal dtmp = decimal.Parse(str);
                switch (mode)
                {
                    case 0: // 金額
                        str = dtmp.ToString("#,##0");
                        break;
                    case 1: // 重量 単価
                        str = dtmp.ToString("#,##0.00");
                        break;
                }
            }
            return str;
        }
        private void CreateChouhyouDataTable()
        {
            string[] fields = new string[] {
                                            "PHY_TORIHIKISAKI_CD_VLB",
                                            "PHY_TORIHIKISAKI_NAME_RYAKU_VLB",
                                            "PHY_ZENKAI_KURIKOSI_GAKU_VLB",
                                            "PHY_KONKAI_NYUUKIN_GAKU_VLB",
                                            "PHY_KONKAI_CHOUSEI_GAKU_VLB",
                                            "PHY_KURIKOSI_GAKU_VLB",
                                            "PHY_SHIMEBI_VLB",
                                            "PHY_KONKAI_URIAGE_GAKU_VLB",
                                            "PHY_SHOUHIZEI_VLB",
                                            "PHY_KONKAI_TORIHIKI_GAKU_VLB",
                                            "PHY_KONKAI_KURIKOSI_GAKU_VLB",
                                            "PHY_NYUUKIN_YOTEI_BI_VLB",
                                            "PHY_SEIKYUU_DATE_VLB",
                                            "PHN_ZENKAI_KURIKOSI_GAKU_TOTAL_VLB",
                                            "PHN_KONKAI_NYUUKIN_GAKU_TOTAL_VLB",
                                            "PHN_KONKAI_CHOUSEI_GAKU_TOTAL_VLB",
                                            "PHN_KURIKOSI_GAKU_TOTAL_VLB",
                                            "PHN_KONKAI_URIAGE_GAKU_TOTAL_VLB",
                                            "PHN_SHOUHIZEI_TOTAL_VLB",
                                            "PHN_KONKAI_TORIHIKI_GAKU_TOTAL_VLB",
                                            "PHN_KONKAI_KURIKOSI_GAKU_TOTAL_VLB"};
            this.chouhyouDataTable = new DataTable();
            foreach (string field in fields)
            {
                this.chouhyouDataTable.Columns.Add(field, typeof(string));
            }
        }
        private void InputDataToMem(DataTable dataTable)
        {
            //Create dataTable Struct
            this.CreateChouhyouDataTable();
            //Prosess data 
            //// 前回請求額 合計
            //decimal ZENKAI_KURIKOSI_GAKU_TOTAL = 0;
            // 入金額 合計
            decimal KONKAI_NYUUKIN_GAKU_TOTAL = 0;
            // 調整額 合計
            decimal KONKAI_CHOUSEI_GAKU_TOTAL = 0;
            //// 繰越額 合計
            //decimal KURIKOSHI_GAKU_TOTAL = 0;
            // 今回取引額(税抜) 合計
            decimal KONKAI_URIAGE_GAKU_TOTAL = 0;
            // 消費税 合計
            decimal SHOUHIZEI_TOTAL = 0;
            //今回取引額 合計＝ 今回取引額(税抜) 合計 ＋ 消費税 合計
            decimal KONKAI_TORIHIKI_GAKU_TOTAL = 0;
            //// 今回御請求額 合計
            //decimal KONKAI_SEIKYU_GAKU_TOTAL = 0;
            foreach (DataRow dtRow in dataTable.Rows)
            {
                string seikyuuKakuteiKbn = dtRow["SEIKYUU_KEITAI_KBN"] != null ? dtRow["SEIKYUU_KEITAI_KBN"].ToString() : string.Empty;

                DataRow newRow = this.chouhyouDataTable.NewRow();
                newRow["PHY_TORIHIKISAKI_CD_VLB"] = dtRow["TORIHIKISAKI_CD"];
                newRow["PHY_TORIHIKISAKI_NAME_RYAKU_VLB"] = this.GetStringByLength(dtRow["TORIHIKISAKI_NAME_RYAKU"],40);
                newRow["PHY_SHIMEBI_VLB"] = dtRow["SHIMEBI"];
                //前回請求額
                decimal temp = this.GetDecimal(dtRow["ZENKAI_KURIKOSI_GAKU"]);
                temp = this.FractionCalc(temp, 3);
                if ("1".Equals(seikyuuKakuteiKbn))
                {
                    newRow["PHY_ZENKAI_KURIKOSI_GAKU_VLB"] = string.Empty;
                }
                else
                {
                    newRow["PHY_ZENKAI_KURIKOSI_GAKU_VLB"] = this.FormatDecimal(temp, 0);
                    //ZENKAI_KURIKOSI_GAKU_TOTAL += temp;
                }

                //入金額
                temp = this.GetDecimal(dtRow["KONKAI_NYUUKIN_GAKU"]);
                temp = this.FractionCalc(temp, 3);
                if ("1".Equals(seikyuuKakuteiKbn))
                {
                    newRow["PHY_KONKAI_NYUUKIN_GAKU_VLB"] = string.Empty;
                }
                else
                {
                    newRow["PHY_KONKAI_NYUUKIN_GAKU_VLB"] = this.FormatDecimal(temp, 0);
                    KONKAI_NYUUKIN_GAKU_TOTAL += temp;
                }

                //調整額
                temp = this.GetDecimal(dtRow["KONKAI_CHOUSEI_GAKU"]);
                temp = this.FractionCalc(temp, 3);
                if ("1".Equals(seikyuuKakuteiKbn))
                {
                    newRow["PHY_KONKAI_CHOUSEI_GAKU_VLB"] = string.Empty;
                }
                else
                {
                    newRow["PHY_KONKAI_CHOUSEI_GAKU_VLB"] = this.FormatDecimal(temp, 0);
                    KONKAI_CHOUSEI_GAKU_TOTAL += temp;
                }

                //繰越額
                temp = this.GetDecimal(dtRow["KURIKOSHI_GAKU"]);
                temp = this.FractionCalc(temp, 3);
                if ("1".Equals(seikyuuKakuteiKbn))
                {
                    newRow["PHY_KURIKOSI_GAKU_VLB"] = string.Empty;
                }
                else
                {
                    newRow["PHY_KURIKOSI_GAKU_VLB"] = this.FormatDecimal(temp, 0);
                    //KURIKOSHI_GAKU_TOTAL += temp;
                }

                //今回取引額(税抜）
                decimal konkaiUriageGaku = this.GetDecimal(dtRow["KONKAI_URIAGE_GAKU"]);
                konkaiUriageGaku = this.FractionCalc(konkaiUriageGaku, 3);
                newRow["PHY_KONKAI_URIAGE_GAKU_VLB"] = this.FormatDecimal(konkaiUriageGaku, 1);
                KONKAI_URIAGE_GAKU_TOTAL += konkaiUriageGaku;

                //消費税
                decimal shouhizei = this.GetDecimal(dtRow["SHOUHIZEI"]);
                newRow["PHY_SHOUHIZEI_VLB"] = this.FormatDecimal(shouhizei, 1);
                SHOUHIZEI_TOTAL += shouhizei;

                //今回取引額 = 今回取引額(税抜) ＋ 消費税 
                decimal konkaiTorihikisakiGaku = konkaiUriageGaku + shouhizei;
                konkaiTorihikisakiGaku = this.FractionCalc(konkaiTorihikisakiGaku, 3);
                newRow["PHY_KONKAI_TORIHIKI_GAKU_VLB"] = this.FormatDecimal(konkaiTorihikisakiGaku, 1);
                KONKAI_TORIHIKI_GAKU_TOTAL += konkaiTorihikisakiGaku;

                //今回御請求額
                temp = this.GetDecimal(dtRow["KONKAI_SEIKYU_GAKU"]);
                temp = this.FractionCalc(temp, 3);
                newRow["PHY_KONKAI_KURIKOSI_GAKU_VLB"] = this.FormatDecimal(temp, 1);
                //KONKAI_SEIKYU_GAKU_TOTAL += temp;

                //入金予定日
                newRow["PHY_NYUUKIN_YOTEI_BI_VLB"] = this.GetStringDate(dtRow["NYUUKIN_YOTEI_BI"]);
                //請求年月日
                newRow["PHY_SEIKYUU_DATE_VLB"] = this.GetStringDate(dtRow["SEIKYUU_DATE"]);

                this.chouhyouDataTable.Rows.Add(newRow);
            }
            if (this.chouhyouDataTable.Rows.Count > 0)
            {
                DataRow lastRow = this.chouhyouDataTable.Rows[this.chouhyouDataTable.Rows.Count - 1];
                ////前回請求額 合計                
                //lastRow["PHN_ZENKAI_KURIKOSI_GAKU_TOTAL_VLB"] = this.FormatDecimal(ZENKAI_KURIKOSI_GAKU_TOTAL, 1);
                //入金額 合計
                lastRow["PHN_KONKAI_NYUUKIN_GAKU_TOTAL_VLB"] = this.FormatDecimal(KONKAI_NYUUKIN_GAKU_TOTAL, 1);
                //調整額 合計
                lastRow["PHN_KONKAI_CHOUSEI_GAKU_TOTAL_VLB"] = this.FormatDecimal(KONKAI_CHOUSEI_GAKU_TOTAL, 1);
                ////繰越額 合計
                //lastRow["PHN_KURIKOSI_GAKU_TOTAL_VLB"] = this.FormatDecimal(KURIKOSHI_GAKU_TOTAL, 1);
                //今回取引額(税抜）合計
                lastRow["PHN_KONKAI_URIAGE_GAKU_TOTAL_VLB"] = this.FormatDecimal(KONKAI_URIAGE_GAKU_TOTAL, 1);
                //消費税 合計
                lastRow["PHN_SHOUHIZEI_TOTAL_VLB"] = this.FormatDecimal(SHOUHIZEI_TOTAL, 1);
                //今回取引額 合計
                lastRow["PHN_KONKAI_TORIHIKI_GAKU_TOTAL_VLB"] = this.FormatDecimal(KONKAI_TORIHIKI_GAKU_TOTAL, 1);
                ////今回御請求額 合計
                //lastRow["PHN_KONKAI_KURIKOSI_GAKU_TOTAL_VLB"] = this.FormatDecimal(KONKAI_SEIKYU_GAKU_TOTAL, 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void UpdateFieldsStatus()
        {
            //抽出条件
            this.SetFieldName("FH_DENPYOU_DATE_CTL", string.Format("{0}　～　{1}", this.GetStringDate(this.searchConditionDto.HIDUKE_FROM),  this.GetStringDate(this.searchConditionDto.HIDUKE_TO)));
            //20151027 hoanghm #13688 start
            //this.SetFieldName("FH_TORIHIKISAKI_CD_CTL", string.Format("{0}～{1}", this.searchConditionDto.TORIHIKISAKI_CD_FROM, this.searchConditionDto.TORIHIKISAKI_CD_TO));
            this.SetFieldName("FH_TORIHIKISAKI_CD_CTL", string.Format("{0}　～　{1}", this.searchConditionDto.TORIHIKISAKI_CD_FROM + "　" + this.searchConditionDto.TORIHIKISAKI_NAME_RYAKU_FROM, this.searchConditionDto.TORIHIKISAKI_CD_TO + "　" + this.searchConditionDto.TORIHIKISAKI_NAME_RYAKU_TO));          
            //20151027 hoanghm #13688 end
            //拠点
            if (this.mKyoten == null)
                this.SetFieldName("FH_KYOTEN_NAME_VLB", string.Empty);
            else
                this.SetFieldName("FH_KYOTEN_NAME_VLB", this.GetStringByLength(this.mKyoten.KYOTEN_NAME_RYAKU, 40));
            // 発行日付
            this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行");
        }

        #endregion - Methods -
    }

    #endregion - Classes -
}
