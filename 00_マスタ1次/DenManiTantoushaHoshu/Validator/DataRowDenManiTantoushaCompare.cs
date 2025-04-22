// $Id: DataRowDenManiTantoushaCompare.cs 715 2013-08-25 02:38:58Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace DenManiTantoushaHoshu.Validator
{
    /// <summary>
    /// M_DENSHI_TANTOUSHAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowDenManiTantoushaCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_DENSHI_TANTOUSHAのPKキーであるTANTOUSHA_CDで判定
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

            var xValue = x[Const.DenManiTantoushaHoshuConstans.TANTOUSHA_CD];
            var yValue = y[Const.DenManiTantoushaHoshuConstans.TANTOUSHA_CD];

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

            var value = dataRow[Const.DenManiTantoushaHoshuConstans.TANTOUSHA_CD];

            return value.GetHashCode();
        }
    }
}
