// $Id: DataRowChiikibetsuShisetsuCompare.cs 710 2013-08-24 06:19:11Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ChiikibetsuShisetsuHoshu.Validator
{
    /// <summary>
    /// M_CHIIKIBETSU_SHISETSUが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowChiikibetsuShisetsuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_CHIIKIBETSU_SHISETSUのPKキーであるSHOBUN_HOUHOU_CDで判定
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

            var xValue = x[Const.ChiikibetsuShisetsuHoshuConstans.SHOBUN_HOUHOU_CD];
            var yValue = y[Const.ChiikibetsuShisetsuHoshuConstans.SHOBUN_HOUHOU_CD];

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

            var value = dataRow[Const.ChiikibetsuShisetsuHoshuConstans.SHOBUN_HOUHOU_CD];

            return value.GetHashCode();
        }
    }
}
