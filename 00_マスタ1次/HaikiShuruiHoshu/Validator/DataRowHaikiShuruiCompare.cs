// $Id: DataRowHaikiShuruiCompare.cs 319 2013-07-27 02:31:20Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace HaikiShuruiHoshu.Validator
{
    /// <summary>
    /// M_HAIKI_SHURUIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowHaikiShuruiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_HAIKI_SHURUIのPKキーであるHAIKI_SHURUI_CDで判定
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

            var xValue = x[Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];
            var yValue = y[Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];

            if (xValue == null && yValue == null)
            {
                return true;
            }

            if (xValue == null || yValue == null)
            {
                return false;
            }

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

            var value = dataRow[Const.HaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
