using System;
using r_framework.Utility;

namespace Shougun.Core.Carriage.UnchinMeisaihyou
{
    /// <summary>
    /// 共通で使用するメソッドを集めたクラス
    /// </summary>
    internal class ReportInfo
    {
        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>Nullかstring.Emptyの場合、Decimal.Zeroを返します</returns>
        internal static decimal ConvertNullOrEmptyToZero(object value)
        {
            LogUtility.DebugMethodStart(value);

            decimal ret = Decimal.Zero;

            if (!string.IsNullOrEmpty(Convert.ToString(value)))
            {
                Decimal.TryParse(Convert.ToString(value), out ret);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }
    }
}
