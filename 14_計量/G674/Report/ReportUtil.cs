using System;
using System.Data;
using r_framework.Dao;
using r_framework.Utility;

namespace Shougun.Core.Scale.KeiryouHoukoku.Report
{
    /// <summary>
    /// 共通で使用するメソッドを集めたクラス
    /// </summary>
    internal class ReportUtil
    {
        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、Decimal.Zeroを返します</returns>
        internal static decimal ConvertNullOrEmptyToZero(object value)
        {
            LogUtility.DebugMethodStart(value);

            decimal ret = decimal.Zero;
            if (!string.IsNullOrWhiteSpace(Convert.ToString(value)))
                decimal.TryParse(Convert.ToString(value), out ret);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal static DateTime GetSysDateTime()
        {
            DateTime now = DateTime.Now;

            DataTable dt =
                DaoInitUtility.GetComponent<GET_SYSDATEDao>().GetDateForStringSql("SELECT GETDATE() AS DATE_TIME"); // DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);

            return now;
        }
    }
}