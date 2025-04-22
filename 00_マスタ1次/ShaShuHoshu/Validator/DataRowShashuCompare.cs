// $Id: DataRowShashuCompare.cs 281 2013-07-23 06:08:14Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace ShashuHoshu.Validator
{
    /// <summary>
    /// M_SHASHUが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowShashuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_SHASHUのPKキーであるSHASHU_CDで判定
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

            var xValue = x[Const.ShashuHoshuConstans.SHASHU_CD];
            var yValue = y[Const.ShashuHoshuConstans.SHASHU_CD];

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

            var value = dataRow[Const.ShashuHoshuConstans.SHASHU_CD];

            return value.GetHashCode();
        }
    }
}
