// $Id: DataRowEigyouTantoushaCompare.cs 357 2013-08-01 11:49:18Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace GamenSeigyoHoshu
{
    /// <summary>
    /// M_EIGYOU_TANTOUSHAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowGamenSeigyoCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_EIGYOU_TANTOUSHAのPKキーであるSHAIN_CDで判定
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

            var xValue = x[GamenSeigyoHoshuConstans.SHAIN_CD];
            var yValue = y[GamenSeigyoHoshuConstans.SHAIN_CD];

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

            var value = dataRow[GamenSeigyoHoshuConstans.SHAIN_CD];

            return value.GetHashCode();
        }
    }
}
