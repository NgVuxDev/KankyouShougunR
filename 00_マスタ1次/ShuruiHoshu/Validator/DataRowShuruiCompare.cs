// $Id: DataRowShuruiCompare.cs 295 2013-07-23 13:18:57Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace ShuruiHoshu.Validator
{
    /// <summary>
    /// M_SHURUIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowShuruiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_SHURUIのPKキーであるSHURUI_CDで判定
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

            var xValue = x[Const.ShuruiHoshuConstans.SHURUI_CD];
            var yValue = y[Const.ShuruiHoshuConstans.SHURUI_CD];

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

            var value = dataRow[Const.ShuruiHoshuConstans.SHURUI_CD];

            return value.GetHashCode();
        }
    }
}
