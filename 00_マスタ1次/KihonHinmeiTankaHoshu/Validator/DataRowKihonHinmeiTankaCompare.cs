// $Id: DataRowKihonHinmeiTankaCompare.cs 9954 2013-12-06 07:36:09Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace KihonHinmeiTankaHoshu.Validator
{
    /// <summary>
    /// M_KIHON_HINMEI_TANKAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowKihonHinmeiTankaCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_KIHON_HINMEI_TANKAのPKキーであるSYS_IDで判定
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

            //システムID
            var xSysId = x[Const.KihonHinmeiTankaHoshuConstans.SYS_ID];
            var ySysId = y[Const.KihonHinmeiTankaHoshuConstans.SYS_ID];

            if (xSysId == null)
            {
                xSysId = string.Empty;
            }
            if (ySysId == null)
            {
                ySysId = string.Empty;
            }
            xSysId = xSysId.ToString();
            ySysId = ySysId.ToString();

            if (xSysId.Equals(ySysId))
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

            var value = dataRow[Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD];

            return value.GetHashCode();
        }
    }
}
