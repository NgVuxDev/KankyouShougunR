// $Id: DataRowUnitCompare.cs 357 2013-08-01 11:49:18Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace GurupuNyuryoku.Validator
{
    /// <summary>
    /// M_UNITが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowUnitCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_UNITのPKキーであるUNIT_CDで判定
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

            var xValue = x[Const.GurupuNyuryokuConstans.GURUPU_CD];
            var yValue = y[Const.GurupuNyuryokuConstans.GURUPU_CD];

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

            var value = dataRow[Const.GurupuNyuryokuConstans.GURUPU_CD];

            return value.GetHashCode();
        }
    }
}
