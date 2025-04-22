using System;
using r_framework.Utility;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// 売上支払固定帳票で共通で使用するメソッドを集めたクラス
    /// </summary>
    internal class ReportInfo
    {
        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、Decimal.Zeroを返します</returns>
        internal static decimal ConvertNullOrEmptyToZero(object value)
        {
            //LogUtility.DebugMethodStart(value);

            decimal ret = Decimal.Zero;

            if (!String.IsNullOrEmpty(Convert.ToString(value)))
            {
                Decimal.TryParse(Convert.ToString(value), out ret);
            }

            //LogUtility.DebugMethodEnd(ret);

            return ret;
        }
    }
}
