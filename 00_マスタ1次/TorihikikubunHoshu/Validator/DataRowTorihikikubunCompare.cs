// $Id: DataRowTorihikikubunCompare.cs 327 2013-07-29 04:37:43Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace TorihikikubunHoshu.Validator
{
    /// <summary>
    /// M_TORIHIKI_KBNが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowTorihikikubunCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_TORIHIKI_KBNのPKキーであるTORIHIKI_KBN_CDで判定
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

            var xValue = x[Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD];
            var yValue = y[Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD];

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

            var value = dataRow[Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
