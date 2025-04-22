// $Id: DataRowNyuushukkinKbnCompare.cs 292 2013-07-23 10:37:56Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace NyuushukkinKbnHoshu.Validator
{
    /// <summary>
    /// M_NYUUSHUKKIN_KBNが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowNyuushukkinKbnCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_NYUUSHUKKIN_KBNのPKキーであるNYUUSHUKKIN_KBN_CDで判定
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

            var xValue = x[Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD];
            var yValue = y[Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD];

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

            var value = dataRow[Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD];

            return value.GetHashCode();
        }
    }
}
