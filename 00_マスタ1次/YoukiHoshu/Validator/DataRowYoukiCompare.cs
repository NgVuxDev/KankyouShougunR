// $Id: DataRowYoukiCompare.cs 313 2013-07-26 06:12:07Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace YoukiHoshu.Validator
{
    /// <summary>
    /// M_YOUKIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowYoukiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_YOUKIのPKキーであるYOUKI_CDで判定
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

            var xValue = x[Const.YoukiHoshuConstans.YOUKI_CD];
            var yValue = y[Const.YoukiHoshuConstans.YOUKI_CD];          

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

            var value = dataRow[Const.YoukiHoshuConstans.YOUKI_CD];

            return value.GetHashCode();
        }
    }
}
