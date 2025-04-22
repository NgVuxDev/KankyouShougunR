// $Id: DataRowChiikibetsuJuushoCompare.cs 708 2013-08-24 04:43:28Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ChiikibetsuJuushoHoshu.Validator
{
    /// <summary>
    /// M_CHIIKIBETSU_JUUSHOが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowChiikibetsuJuushoCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_CHIIKIBETSU_JUUSHOのPKキーであるCHANGE_CHIIKI_CDで判定
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

            var xValue = x[Const.ChiikibetsuJuushoHoshuConstans.CHANGE_CHIIKI_CD];
            var yValue = y[Const.ChiikibetsuJuushoHoshuConstans.CHANGE_CHIIKI_CD];

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

            var value = dataRow[Const.ChiikibetsuJuushoHoshuConstans.CHANGE_CHIIKI_CD];

            return value.GetHashCode();
        }
    }
}
