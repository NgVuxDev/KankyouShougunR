// $Id: DataRowKongouCompare.cs 394 2013-08-04 19:41:24Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace KongouShuruiHoshu.Validator
{
    /// <summary>
    /// M_KONGOU_SHURUIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowKongouCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_KONGOU_SHURUIのPKキーであるKONGOU_SHURUI_CDで判定
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

            var xValue = x[Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD];
            var yValue = y[Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD];

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

            var value = dataRow[Const.KongouShuruiHoshuConstans.KONGOU_SHURUI_CD];

            return value.GetHashCode();
        }
    }
}
