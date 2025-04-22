// $Id: DataRowKeiryouChouseiCompare.cs 392 2013-08-04 19:23:24Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace KeiryouChouseiHoshu.Validator
{
    /// <summary>
    /// M_KEIRYOU_CHOUSEIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowKeiryouChouseiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_KEIRYOU_CHOUSEIのPKキーであるKEIRYOU_CHOUSEI_CDで判定
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

            var xHinmei = x[Const.KeiryouChouseiHoshuConstans.HINMEI_CD];
            var yHinmei = y[Const.KeiryouChouseiHoshuConstans.HINMEI_CD];

            xHinmei = xHinmei.ToString();
            yHinmei = yHinmei.ToString();

            if (xHinmei.Equals(yHinmei))
            {
                return true;
            }

            var xUnit = x[Const.KeiryouChouseiHoshuConstans.UNIT_CD];
            var yUnit = y[Const.KeiryouChouseiHoshuConstans.UNIT_CD];

            xUnit = xUnit.ToString();
            yUnit = yUnit.ToString();

            if (xUnit.Equals(yUnit))
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

            var value = dataRow[Const.KeiryouChouseiHoshuConstans.TORIHIKISAKI_CD];

            return value.GetHashCode();
        }
    }
}
