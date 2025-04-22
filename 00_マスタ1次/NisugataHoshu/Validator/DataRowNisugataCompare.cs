// $Id: DataRowNisugataCompare.cs 302 2013-07-25 06:25:08Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace NisugataHoshu.Validator
{
    /// <summary>
    /// M_NISUGATAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowNisugataCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_NISUGATAのPKキーであるNISUGATA_CDで判定
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

            var xValue = x[Const.NisugataHoshuConstans.NISUGATA_CD];
            var yValue = y[Const.NisugataHoshuConstans.NISUGATA_CD];


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

            var value = dataRow[Const.NisugataHoshuConstans.NISUGATA_CD];

            return value.GetHashCode();
        }
    }
}
