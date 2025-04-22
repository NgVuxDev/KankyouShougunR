// $Id: DataRowBankShitenCompare.cs 21390 2014-05-26 05:18:09Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ContenaHoshu.Validator
{
    /// <summary>
    /// M_CONTENAが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowContenaHoshuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_CONTENAのPKキーであるCONTENA_CD/CONTENA_SHURUI_CDで判定
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

            var xValue1 = x[Shougun.Core.Master.ContenaHoshu.Const.ContenaHoshuConstans.CONTENA_CD];
            var yValue1 = y[Shougun.Core.Master.ContenaHoshu.Const.ContenaHoshuConstans.CONTENA_CD];
            var xValue2 = x[Shougun.Core.Master.ContenaHoshu.Const.ContenaHoshuConstans.CONTENA_SHURUI_CD];
            var yValue2 = y[Shougun.Core.Master.ContenaHoshu.Const.ContenaHoshuConstans.CONTENA_SHURUI_CD];

            xValue1 = xValue1.ToString();
            yValue1 = yValue1.ToString();
            xValue2 = xValue2.ToString();
            yValue2 = yValue2.ToString();

            if (xValue1.Equals(yValue1) && xValue2.Equals(yValue2))
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

            var value = dataRow[Shougun.Core.Master.ContenaHoshu.Const.ContenaHoshuConstans.CONTENA_CD].ToString()
                      + dataRow[Shougun.Core.Master.ContenaHoshu.Const.ContenaHoshuConstans.CONTENA_SHURUI_CD].ToString();

            return value.GetHashCode();
        }
    }
}
