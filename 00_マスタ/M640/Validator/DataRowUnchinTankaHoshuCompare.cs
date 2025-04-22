// $Id: DataRowSharyouCompare.cs 391 2013-08-04 18:40:43Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace Shougun.Core.Master.UnchinTankaHoshu.Validator
{
    /// <summary>
    /// M_UNCHIN_TANKAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowUnchinTankaHoshuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_UNCHIN_TANKAのPKキーであるUNCHIN_HINMEI_CDで判定
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

            var xValue1 = x[Const.UnchinTankaHoshuConstans.UNPAN_GYOUSHA_CD];
            var xValue2 = x[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD];
            var xValue3 = x[Const.UnchinTankaHoshuConstans.UNIT_CD];
            var xValue4 = x[Const.UnchinTankaHoshuConstans.SHASHU_CD];
            var yValue1 = y[Const.UnchinTankaHoshuConstans.UNPAN_GYOUSHA_CD];
            var yValue2 = y[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD];
            var yValue3 = y[Const.UnchinTankaHoshuConstans.UNIT_CD];
            var yValue4 = y[Const.UnchinTankaHoshuConstans.SHASHU_CD];

            xValue1 = xValue1.ToString();
            xValue2 = xValue2.ToString();
            xValue3 = xValue3.ToString();
            xValue4 = xValue4.ToString();
            yValue1 = yValue1.ToString();
            yValue2 = yValue2.ToString();
            yValue3 = yValue3.ToString();
            yValue4 = yValue4.ToString();

            if (xValue1.Equals(yValue1) && xValue2.Equals(yValue2) && xValue3.Equals(yValue3) && xValue4.Equals(yValue4))
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
            if (object.ReferenceEquals(dataRow, null))
            {
                return 0;
            }

            var value =
                dataRow[Const.UnchinTankaHoshuConstans.UNPAN_GYOUSHA_CD].ToString() +
                dataRow[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].ToString() +
                dataRow[Const.UnchinTankaHoshuConstans.UNIT_CD].ToString() +
                dataRow[Const.UnchinTankaHoshuConstans.SHASHU_CD].ToString();

            return value.GetHashCode();
        }
    }
}
