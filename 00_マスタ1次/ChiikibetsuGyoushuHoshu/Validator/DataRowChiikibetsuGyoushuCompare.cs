// $Id: DataRowChiikibetsuGyoushuCompare.cs 700 2013-08-23 12:23:17Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ChiikibetsuGyoushuHoshu.Validator
{
    /// <summary>
    /// M_CHIIKIBETSU_GYOUSHUが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowChiikibetsuGyoushuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_CHIIKIBETSU_GYOUSHUのPKキーであるGYOUSHU_CDで判定
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

            var xValue = x[Const.ChiikibetsuGyoushuHoshuConstans.GYOUSHU_CD];
            var yValue = y[Const.ChiikibetsuGyoushuHoshuConstans.GYOUSHU_CD];

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

            var value = dataRow[Const.ChiikibetsuGyoushuHoshuConstans.GYOUSHU_CD];

            return value.GetHashCode();
        }
    }
}
