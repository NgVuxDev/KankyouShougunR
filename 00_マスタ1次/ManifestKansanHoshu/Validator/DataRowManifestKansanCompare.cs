// $Id: DataRowManifestKansanCompare.cs 9954 2013-12-06 07:36:09Z gai $
using System;
using System.Collections.Generic;
using System.Data;

namespace ManifestKansanHoshu.Validator
{
    /// <summary>
    /// M_MANIFEST_KANSANが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowManifestKansanCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_MANIFEST_KANSANのPKキーで判定
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

            var xHaikiName = x[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD];
            var yHaikiName = y[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD];
            var xUnit = x[Const.ManifestKansanHoshuConstans.UK_UNIT_CD];
            var yUnit = y[Const.ManifestKansanHoshuConstans.UK_UNIT_CD];
            var xNisugata = x[Const.ManifestKansanHoshuConstans.UK_NISUGATA_CD];
            var yNisugata = y[Const.ManifestKansanHoshuConstans.UK_NISUGATA_CD];

            if (xHaikiName == null)
            {
                xHaikiName = string.Empty;
            }
            if (yHaikiName == null)
            {
                yHaikiName = string.Empty;
            }
            if (xUnit == null)
            {
                xUnit = string.Empty;
            }
            if (yUnit == null)
            {
                yUnit = string.Empty;
            }
            if (xNisugata == null)
            {
                xNisugata = string.Empty;
            }
            if (yNisugata == null)
            {
                yNisugata = string.Empty;
            }
            xHaikiName = xHaikiName.ToString();
            yHaikiName = yHaikiName.ToString();
            xUnit = xUnit.ToString();
            yUnit = yUnit.ToString();
            xNisugata = xNisugata.ToString();
            yNisugata = yNisugata.ToString();

            if (xHaikiName.Equals(yHaikiName) && xUnit.Equals(yUnit) && xNisugata.Equals(yNisugata))
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

            var value = dataRow[Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD];
            if (value == null)
            {
                return 0;
            }

            return value.GetHashCode();
        }
    }
}
