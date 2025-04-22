// $Id: DataRowBankShitenCompare.cs 21390 2014-05-26 05:18:09Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace BikoUchiwakeNyuryoku.Validator
{
    /// <summary>
    /// M_BANK_SHITENが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowBikoUchiwakeNyuryokuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_BANK_SHITENのPKキーであるBIKO_CDで判定
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

            var xValue1 = x[Const.BikoUchiwakeNyuryokuConstans.BIKO_CD];
            var yValue1 = y[Const.BikoUchiwakeNyuryokuConstans.BIKO_CD];


            xValue1 = xValue1.ToString();
            yValue1 = yValue1.ToString();


            if (xValue1.Equals(yValue1))
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

            var value = dataRow[Const.BikoUchiwakeNyuryokuConstans.BIKO_CD].ToString();

            return value.GetHashCode();
        }
    }
}
