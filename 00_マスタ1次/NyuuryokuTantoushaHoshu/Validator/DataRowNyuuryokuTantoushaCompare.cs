// $Id: DataRowNyuuryokuTantoushaCompare.cs 261 2013-07-18 10:02:56Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace NyuuryokuTantoushaHoshu.Validator
{
    /// <summary>
    /// M_NYUURYOKU_TANTOUSHAが格納されたDataRow専用の比較クラス
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

            var xValue = x[Const.NyuuryokuTantoushaHoshuConstans.SHAIN_CD];
            var yValue = y[Const.NyuuryokuTantoushaHoshuConstans.SHAIN_CD];

            if (xValue == null && yValue == null)
            {
                return true;
            }

            if (xValue == null || yValue == null)
            {
                return false;
            }

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

            var value = dataRow[Const.NyuuryokuTantoushaHoshuConstans.SHAIN_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
