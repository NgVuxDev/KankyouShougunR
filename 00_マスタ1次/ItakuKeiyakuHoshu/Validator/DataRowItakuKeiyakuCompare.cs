// $Id: DataRowItakuKeiyakuCompare.cs 582 2013-08-19 05:32:49Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ItakuKeiyakuHoshu.Validator
{
    /// <summary>
    /// M_SHAINが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowItakuKeiyakuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_SHAINのPKキーであるSHAIN_CDで判定
        /// </remarks>
        public bool Equals(DataRow x, DataRow y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            var xValue = x[Const.ItakuKeiyakuHoshuConstans.SYSTEM_ID];
            var yValue = y[Const.ItakuKeiyakuHoshuConstans.SYSTEM_ID];

            xValue = xValue.ToString();
            yValue = yValue.ToString();

            if (xValue.Equals(yValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public int GetHashCode(DataRow dataRow)
        {
            if (Object.ReferenceEquals(dataRow, null))
            {
                return 0;
            }

            var value = dataRow[Const.ItakuKeiyakuHoshuConstans.SYSTEM_ID];

            return value.GetHashCode();
        }
    }
}
