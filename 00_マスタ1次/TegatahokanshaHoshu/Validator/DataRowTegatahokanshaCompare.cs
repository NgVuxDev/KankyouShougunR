// $Id: DataRowTegatahokanshaCompare.cs 322 2013-07-27 03:02:31Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace TegatahokanshaHoshu.Validator
{
    /// <summary>
    /// M_TEGATA_HOKANSHAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowTegatahokanshaCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_TEGATA_HOKANSHAのPKキーであるSHAIN_CDで判定
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

            var xValue = x[Const.TegatahokanshaHoshuConstans.SHAIN_CD];
            var yValue = y[Const.TegatahokanshaHoshuConstans.SHAIN_CD];

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

            var value = dataRow[Const.TegatahokanshaHoshuConstans.SHAIN_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
