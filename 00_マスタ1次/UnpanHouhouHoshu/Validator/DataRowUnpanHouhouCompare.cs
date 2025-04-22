// $Id: DataRowUnpanHouhouCompare.cs 288 2013-07-23 09:53:00Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UnpanHouhouHoshu.Validator
{
    /// <summary>
    /// M_UNPAN_HOUHOUが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowUnpanHouhouCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_UNPAN_HOUHOUのPKキーであるUNPAN_HOUHOU_CDで判定
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

            var xValue = x[Const.UnpanHouhouHoshuConstans.UNPAN_HOUHOU_CD];
            var yValue = y[Const.UnpanHouhouHoshuConstans.UNPAN_HOUHOU_CD];

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

            var value = dataRow[Const.UnpanHouhouHoshuConstans.UNPAN_HOUHOU_CD];

            return value.GetHashCode();
        }
    }
}
