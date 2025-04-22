// $Id: DataRowBankShitenCompare.cs 21390 2014-05-26 05:18:09Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace BankShitenHoshu.Validator
{
    /// <summary>
    /// M_BANK_SHITENが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowBankShitenCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_BANK_SHITENのPKキーであるBANK_SHITEN_CDで判定
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

            var xValue1 = x[Const.BankShitenHoshuConstans.BANK_SHITEN_CD];
            var yValue1 = y[Const.BankShitenHoshuConstans.BANK_SHITEN_CD];
            var xValue2 = x[Const.BankShitenHoshuConstans.KOUZA_NO];
            var yValue2 = y[Const.BankShitenHoshuConstans.KOUZA_NO];
            var xValue3 = x[Const.BankShitenHoshuConstans.KOUZA_SHURUI_CD];
            var yValue3 = y[Const.BankShitenHoshuConstans.KOUZA_SHURUI_CD];

            xValue1 = xValue1.ToString();
            yValue1 = yValue1.ToString();
            xValue2 = xValue2.ToString();
            yValue2 = yValue2.ToString();
            xValue3 = xValue3.ToString();
            yValue3 = yValue3.ToString();

            if (xValue1.Equals(yValue1) && xValue2.Equals(yValue2) && xValue3.Equals(yValue3))
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

            var value = dataRow[Const.BankShitenHoshuConstans.BANK_SHITEN_CD].ToString()
                      + dataRow[Const.BankShitenHoshuConstans.KOUZA_SHURUI_CD].ToString()
                      + dataRow[Const.BankShitenHoshuConstans.KOUZA_NO].ToString();

            return value.GetHashCode();
        }
    }
}
