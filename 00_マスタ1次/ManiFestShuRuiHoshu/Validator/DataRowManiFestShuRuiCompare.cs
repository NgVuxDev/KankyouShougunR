// $Id: DataRowManiFestShuRuiCompare.cs 290 2013-07-23 10:34:40Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace ManiFestShuRuiHoshu.Validator
{
    /// <summary>
    /// M_MANIFEST_SHURUIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowManiFestShuRuiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_MANIFEST_SHURUIのPKキーであるMANIFEST_SHURUI_CDで判定
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

            var xValue = x[Const.ManiFestShuRuiHoshuConstans.MANIFEST_SHURUI_CD];
            var yValue = y[Const.ManiFestShuRuiHoshuConstans.MANIFEST_SHURUI_CD];

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

            var value = dataRow[Const.ManiFestShuRuiHoshuConstans.MANIFEST_SHURUI_CD];

            return value.GetHashCode();
        }
    }
}
