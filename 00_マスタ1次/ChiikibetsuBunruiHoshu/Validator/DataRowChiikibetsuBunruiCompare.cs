// $Id: DataRowChiikibetsuBunruiCompare.cs 706 2013-08-24 02:58:47Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ChiikibetsuBunruiHoshu.Validator
{
    /// <summary>
    /// M_CHIIKIBETSU_BUNRUIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowChiikibetsuBunruiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_CHIIKIBETSU_BUNRUIのPKキーであるHOUKOKUSHO_BUNRUI_CDで判定
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

            var xValue = x[Const.ChiikibetsuBunruiHoshuConstans.HOUKOKUSHO_BUNRUI_CD];
            var yValue = y[Const.ChiikibetsuBunruiHoshuConstans.HOUKOKUSHO_BUNRUI_CD];

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

            var value = dataRow[Const.ChiikibetsuBunruiHoshuConstans.HOUKOKUSHO_BUNRUI_CD];

            return value.GetHashCode();
        }
    }
}
