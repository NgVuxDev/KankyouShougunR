// $Id: DataRowDenManiHaikiNameCompare.cs 760 2013-08-26 07:40:49Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace DenManiHaikiNameHoshu.Validator
{
    /// <summary>
    /// M_DENSHI_HAIKI_NAMEが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DenManiHaikiNameCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_DENSHI_HAIKI_NAMEのPKキーであるSHAIN_CDで判定
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

            var xValue = x[Const.DenManiHaikiNameHoshuConstans.HAIKI_NAME_CD];
            var yValue = y[Const.DenManiHaikiNameHoshuConstans.HAIKI_NAME_CD];

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

            var value = dataRow[Const.DenManiHaikiNameHoshuConstans.HAIKI_NAME_CD];

            return value.GetHashCode();
        }
    }
}
