// $Id: DataRowCompare.cs 50440 2015-05-22 10:02:49Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace JissekiBunruiHoshu.Validator
{
    /// <summary>
    /// M_JISSEKI_BUNRUI_MODが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowShainCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_JISSEKI_BUNRUI_MODのPKキーであるJISSEKI_BUNRUI_CDで判定
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

            var xValue = x[Const.ConstCls.JISSEKI_BUNRUI_CD];
            var yValue = y[Const.ConstCls.JISSEKI_BUNRUI_CD];

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

            var value = dataRow[Const.ConstCls.JISSEKI_BUNRUI_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
