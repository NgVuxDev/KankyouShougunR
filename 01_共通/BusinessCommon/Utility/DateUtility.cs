using System;
using System.Globalization;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// 日付関連ユーティリティを集めたクラス
    /// </summary>
    public class DateUtility
    {
        ///<summary>
        /// YYYY/MM/DDフォーマット
        /// </summary>
        public const string yyyyMMdd = "yyyy/MM/dd";

        ///<summary>
        /// YYYY年MM月DD日フォーマット
        /// </summary>
        public const string yyyy年MM月dd日 = "yyyy年MM月dd日";

        ///<summary>
        /// YYYY/MMフォーマット
        /// </summary>
        public const string yyyyMM = "yyyy/MM";

        ///<summary>
        ///MM/DDフォーマット
        /// </summary>
        public const string MMdd = "MM/dd";

        ///<summary>
        ///hh:mm:ssフォーマット
        /// </summary>
        public const string hhmmss = "hh:mm:ss";

        ///<summary>
        /// H:mm:ssフォーマット
        /// </summary>
        public const string HHmmss = "HH:mm:ss";

        ///<summary>
        /// hh:mmフォーマット
        /// </summary>
        public const string hhmm = "hh:mm";

        ///<summary>
        /// HH:mmフォーマット
        /// </summary>
        public const string HHmm = "HH:mm";

        ///<summary>
        /// (ddd) フォーマット
        /// </summary>
        public const string ddd = "(ddd)";

        #region "20150714 CongBinh 削除"
        /////<summary>
        /////平成フォーマット
        ///// </summary>          
        //private const string 平成 = "平成";
        #endregion

        ///<summary>
        ///スペース
        /// </summary>
        public const string Space = " ";

        #region "20150714 CongBinh 追加"
        /// <summary>
        /// 年
        /// </summary>
        public const string Year = "年";

        /// <summary>
        /// 月
        /// </summary>
        public const string Month = "月";

        /// <summary>
        /// 日
        /// </summary>
        public const string Day = "日";

        #endregion

        /// <summary>
        /// 曜日を将軍のCD体系で取得します
        /// （1:月、2:火、3:水、4:木、5:金、6:土、7:日）
        /// </summary>
        /// <param name="dateTime">対象日付</param>
        /// <returns>曜日CD</returns>
        public static int GetShougunDayOfWeek(DateTime dateTime)
        {
            int ret = 1;

            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    ret = 7;
                    break;
                case DayOfWeek.Monday:
                    ret = 1;
                    break;
                case DayOfWeek.Tuesday:
                    ret = 2;
                    break;
                case DayOfWeek.Wednesday:
                    ret = 3;
                    break;
                case DayOfWeek.Thursday:
                    ret = 4;
                    break;
                case DayOfWeek.Friday:
                    ret = 5;
                    break;
                case DayOfWeek.Saturday:
                    ret = 6;
                    break;
            }

            return ret;
        }

        /// <summary>
        ///日付を渡してYYYYMMDDフォーマットで文字列を戻します
        ///</summary>
        public static string FormatDate(DateTime? prmValue)
        {
            //チェックを行います。ＮＵＬＬだったら空白で戻します
            if (!prmValue.HasValue)
            {
                return string.Empty;
            }
            return prmValue.Value.ToString(yyyyMMdd);
        }

        /// <summary>
        ///日付フォーマットで日付型値を渡して渡したフォーマットで文字列を戻します
        ///</summary>
        public static string FormatDate(DateTime? prmValue, string prmFormatString)
        {
            try
            {
                //チェックを行います。ＮＵＬＬだったら空白で戻します
                if (!prmValue.HasValue)
                {
                    return string.Empty;
                }
                //チェックを行います。ブランクだったら空白で戻します
                if (string.IsNullOrEmpty(prmFormatString))
                {
                    return string.Empty;
                }

                return prmValue.Value.ToString(prmFormatString);
            }
            catch (Exception ex)
            {
                return string.Empty;//フォーマットが間違っていたら空白で戻します
            }
        }

        /// <summary>
        ///Ｏｂｊｅｃｔ型値を渡してdatetime型値を戻します。渡した値が日付ではなかったらＮＵＬＬで戻します
        ///</summary>
        public static DateTime? ConvertToDate(object prmValue)
        {
            DateTime retur = new DateTime();
            //入力した値がNULL又は空文字の場合、ＮＵＬＬで戻します
            if (prmValue == null || string.IsNullOrEmpty(prmValue.ToString()))
            {
                return null;
            }

            //値をyyyy/MM/ddフォーマットでdatetimeにコンバートします
            string[] str = prmValue.ToString().Split(new Char[] { '/' });
            if (str.Length == 3)
            {
                if (str[0].Length == 4 && str[2].Length == 2)
                {
                    prmValue = str[0] + "/" + str[1] + "/" + str[2];
                }
                if (str[0].Length == 2 && str[2].Length == 4)
                {
                    prmValue = str[2] + "/" + str[1] + "/" + str[0];
                }

            }
            //渡した値が日付ではなかったらＮＵＬＬで戻します
            if (!DateTime.TryParse(prmValue.ToString(), out retur))
            {
                return null;
            }

            return retur;
        }

        /// <summary>
        ///一日目と二日目を渡します。一日目が二日目より後ろになる場合は-1で戻します。一日目と二日目が同じの場合は0で戻します
        ///一日目が二日目より前になる場合は1で戻します
        ///</summary>
        public static int CompareDate(DateTime? prmValue1, DateTime? prmValue2)
        {
            //prmValue1,prmValue2がＮｕｌｌになるかチェックします
            if (!prmValue1.HasValue && !(prmValue2.HasValue))
            {
                return 0;
            }
            //prmValue1がＮｕｌｌにならないか、prmValue2がＮｕｌｌになるかチェックします
            if (!prmValue1.HasValue)
            {
                if (prmValue2.HasValue)
                {
                    return 1;
                }
            }
            //prmValue2がＮｕｌｌにならないか、prmValue1がＮｕｌｌになるかチェックします
            if (!prmValue2.HasValue)
            {
                if (prmValue1.HasValue)
                {
                    return -1;
                }
            }
            //prmValue1とprmValue2を比べます
            if (prmValue1.Value.Date > prmValue2.Value.Date)
            {
                return -1;
            }
            else if (prmValue1.Value.Date < prmValue2.Value.Date)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        ///一日目と二日目を渡して二日目から一日目を引いた日値を戻します。一日目が二日目より後ろになる場合は-1で戻します
        ///</summary>
        public static int CountNumberDay(DateTime dateFrom, DateTime dateTo)
        {
            // dateFromとdateToをチェックします
            if (dateFrom > dateTo)
            {
                return -1;
            }
            return (dateTo.Date - dateFrom.Date).Days;
        }

        /// <summary>
        /// 一日目と二日目を渡して二日目から一日目を引いた月値を戻します。一日目が二日目より後ろになる場合は-1で戻します
        ///</summary>
        public static int CountNumberMonth(DateTime dateFrom, DateTime dateTo)
        {
            //dateFromとdateToをチェックします
            if (dateFrom > dateTo)
            {
                return -1;
            }

            var monthDiff = Math.Abs((dateTo.Year * 12 + (dateTo.Month - 1)) - (dateFrom.Year * 12 + (dateFrom.Month - 1)));

            //開始日が終了日より後ろになる場合は1月分を引きます
            if (dateFrom.AddMonths(monthDiff) > dateTo || dateTo.Day < dateFrom.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }

        /// <summary>
        /// 一日目と二日目を渡して二日目から一日目を引いた年値を戻します。一日目が二日目より後ろになる場合は-1で戻します
        ///</summary>
        public static int CountNumberYear(DateTime dateFrom, DateTime dateTo)
        {
            //dateFrom とdateToをチェックします
            if (dateFrom > dateTo)
            {
                return -1;
            }

            //月数合計を取得します
            int months = ((dateTo.Year - dateFrom.Year) * 12) + (dateTo.Month - dateFrom.Month);

            // 開始日が終了日より後ろになる場合は1月分を引きます
            if (dateTo.Day < dateFrom.Day)
            {
                months--;
            }

            #region "20150714 CongBinh 修正"
            //return months / 12;
            return (int)Math.Floor(months / 12.0);
            #endregion

        }

        /// <summary>
        /// Get Current DateTime In Server
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentDateTime()
        {
            r_framework.Dao.GET_SYSDATEDao dateDao = r_framework.Utility.DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
    }
}
