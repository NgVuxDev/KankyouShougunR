// $Id: DataRowShuukeikoumokuHoshuCompare.cs 320 2013-07-27 02:45:18Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace ShuukeikoumokuHoshu.Validator
{
    /// <summary>
    /// M_SHUUKEI_KOUMOKUが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowShuukeikoumokuHoshuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_SHUUKEI_KOUMOKUのPKキーであるSHUUKEI_KOUMOKU_CDで判定
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

            var xValue = x[Const.ShuukeikoumokuHoshuConstans.SHUUKEI_KOUMOKU_CD];
            var yValue = y[Const.ShuukeikoumokuHoshuConstans.SHUUKEI_KOUMOKU_CD];

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

            var value = dataRow[Const.ShuukeikoumokuHoshuConstans.SHUUKEI_KOUMOKU_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
