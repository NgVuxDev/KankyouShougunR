// $Id: DataRowKeitaiKbnCompare.cs 312 2013-07-26 06:08:43Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace KeitaiKbnHoshu.Validator
{
    /// <summary>
    /// M_KEITAI_KBNが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowKeitaiKbnCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_KEITAI_KBNのPKキーであるKEITAI_KBN_CDで判定
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

            var xValue = x[Const.KeitaiKbnHoshuConstans.KEITAI_KBN_CD];
            var yValue = y[Const.KeitaiKbnHoshuConstans.KEITAI_KBN_CD];

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

            var value = dataRow[Const.KeitaiKbnHoshuConstans.KEITAI_KBN_CD];

            return value.GetHashCode();
        }
    }
}
