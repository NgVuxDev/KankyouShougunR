// $Id: DataRowYuugaiBusshitsuCompare.cs 703 2013-08-23 15:35:27Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace DenManiYuugaiBusshitsuHoshu.Validator
{
    /// <summary>
    /// M_DENSHI_YUUGAI_BUSSHITSUが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowDenManiYuugaiBusshitsuCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_DENSHI_YUUGAI_BUSSHITSUのPKキーであるYUUGAI_BUSSITSU_CDで判定
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

            var xValue = x[Const.DenManiYuugaiBusshitsuHoshuConstans.YUUGAI_BUSSHITSU_CD];
            var yValue = y[Const.DenManiYuugaiBusshitsuHoshuConstans.YUUGAI_BUSSHITSU_CD];

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

            var value = dataRow[Const.DenManiYuugaiBusshitsuHoshuConstans.YUUGAI_BUSSHITSU_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
