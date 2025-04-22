// $Id: ChiikibetsuKyokaBangoHoshuCompare.cs 1019 2013-09-02 07:06:49Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ChiikibetsuKyokaBangoHoshu.Validator
{
    /// <summary>
    /// M_ChiikibetsuKyokaBangouNyuuryokuが格納されたDataRow専用の比較クラス
    /// </summary>
    public class ChiikibetsuKyokaBangouNyuuryokuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_ChiikibetsuKyokaBangouNyuuryokuのPKキーであるChiikibetsuKyokaBangouNyuuryoku_CDで判定
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

            var xValue = x[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD];
            var yValue = y[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD];

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

            var value = dataRow[Const.ChiikibetsuKyokaBangouNyuuryokuConstans.CHIIKI_CD];

            return value.GetHashCode();
        }
    }
}
