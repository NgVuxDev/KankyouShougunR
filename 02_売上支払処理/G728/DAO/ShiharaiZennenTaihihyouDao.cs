using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Collections.Generic;
using Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou;

namespace Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou
{
    /// <summary>
    /// 支払集計表Daoインタフェース
    /// </summary>
    [Bean(typeof(ShiharaiData))]
    internal interface ShiharaiZennenTaihihyouDao : IS2Dao
    {
        /// <summary>
        /// 受入伝票の支払集計表データを取得します
        /// </summary>
        /// <param name="dto">支払集計表DTOクラス</param>
        /// <returns>受入伝票の支払集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou.Sql.GetShiharaiShukeiHyoDataUkeire.sql")]
        List<ShiharaiData> GetShukeiHyoDataUkeire(ShiharaiZennenTaihihyouDto dto);

        /// <summary>
        /// 出荷伝票の支払集計表データを取得します
        /// </summary>
        /// <param name="dto">支払集計表DTOクラス</param>
        /// <returns>出荷伝票の支払集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou.Sql.GetShiharaiShukeiHyoDataShukka.sql")]
        List<ShiharaiData> GetShukeiHyoDataShukka(ShiharaiZennenTaihihyouDto dto);

        /// <summary>
        /// 支払／支払伝票の支払集計表データを取得します
        /// </summary>
        /// <param name="dto">支払集計表DTOクラス</param>
        /// <returns>支払／支払伝票の支払集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiZennenTaihihyou.Sql.GetShiharaiShukeiHyoDataUriageShiharai.sql")]
        List<ShiharaiData> GetShukeiHyoDataUriageShiharai(ShiharaiZennenTaihihyouDto dto);
    }
}
