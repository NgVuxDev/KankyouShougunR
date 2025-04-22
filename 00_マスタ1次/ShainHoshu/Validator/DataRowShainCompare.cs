// $Id: DataRowShainCompare.cs 254 2013-07-17 14:30:30Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data;

namespace ShainHoshu.Validator
{
    /// <summary>
    /// M_SHAINが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowShainCompare : IEqualityComparer<DataRow>
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

            var xValue = x[Const.ShainHoshuConstans.SHAIN_CD];
            var yValue = y[Const.ShainHoshuConstans.SHAIN_CD];

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

            var value = dataRow[Const.ShainHoshuConstans.SHAIN_CD];

            return value.GetHashCode();
        }
    }
}
