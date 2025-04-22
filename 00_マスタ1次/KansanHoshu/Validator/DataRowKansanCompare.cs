// $Id: DataRowKansanCompare.cs 9954 2013-12-06 07:36:09Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace KansanHoshu.Validator
{
    /// <summary>
    /// M_KANSANが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowKansanCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_KANSANのPKキーであるKANSAN_CDで判定
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

            var xHinmei = x[Const.KansanHoshuConstans.UK_HINMEI_CD];
            var yHinmei = y[Const.KansanHoshuConstans.UK_HINMEI_CD];
            var xUnit = x[Const.KansanHoshuConstans.UK_UNIT_CD];
            var yUnit = y[Const.KansanHoshuConstans.UK_UNIT_CD];

            if (xHinmei == null)
            {
                xHinmei = string.Empty;
            }
            if (yHinmei == null)
            {
                yHinmei = string.Empty;
            }
            if (xUnit == null)
            {
                xUnit = string.Empty;
            }
            if (yUnit == null)
            {
                yUnit = string.Empty;
            }
            xHinmei = xHinmei.ToString();
            yHinmei = yHinmei.ToString();
            xUnit = xUnit.ToString();
            yUnit = yUnit.ToString();
            if (xHinmei.Equals(yHinmei) && xUnit.Equals(yUnit))
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

            var value = dataRow[Const.KansanHoshuConstans.HINMEI_CD];

            return value.GetHashCode();
        }
    }
}
