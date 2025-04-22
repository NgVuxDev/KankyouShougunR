// $Id: DataRowDenManiHaikiShuruiCompare.cs 2306 2013-09-23 14:03:17Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace DenManiHaikiShuruiHoshu.Validator
{
    /// <summary>
    /// M_SHAINが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DenManiHaikiShuruiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_SHAINのPKキーであるSHAIN_CDで判定
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

            var xValue1 = x[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];
            var yValue1 = y[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];
            var xValue2 = x[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD];
            var yValue2 = y[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_SAIBUNRUI_CD];

            xValue1 = xValue1.ToString();
            yValue1 = yValue1.ToString();
            xValue2 = xValue2.ToString();
            yValue2 = yValue2.ToString();

            if (xValue1.Equals(yValue1) && xValue2.Equals(yValue2))
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

            var value = dataRow[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];

            return value.GetHashCode();
        }
    }
}
