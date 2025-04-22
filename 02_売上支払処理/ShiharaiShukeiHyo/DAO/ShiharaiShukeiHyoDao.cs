using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.SalesPayment.ShiharaiShukeiHyo
{
    /// <summary>
    /// 支払集計表Daoインタフェース
    /// </summary>
    [Bean(typeof(ShiharaiData))]
    internal interface ShiharaiShukeiHyoDao : IS2Dao
    {
        /// <summary>
        /// 受入伝票の支払集計表データを取得します
        /// </summary>
        /// <param name="dto">支払集計表DTOクラス</param>
        /// <returns>受入伝票の支払集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiShukeiHyo.Sql.GetShiharaiShukeiHyoDataUkeire.sql")]
        List<ShiharaiData> GetShukeiHyoDataUkeire(ShiharaiShukeiHyoDto dto);

        /// <summary>
        /// 出荷伝票の支払集計表データを取得します
        /// </summary>
        /// <param name="dto">支払集計表DTOクラス</param>
        /// <returns>出荷伝票の支払集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiShukeiHyo.Sql.GetShiharaiShukeiHyoDataShukka.sql")]
        List<ShiharaiData> GetShukeiHyoDataShukka(ShiharaiShukeiHyoDto dto);

        /// <summary>
        /// 売上／支払伝票の支払集計表データを取得します
        /// </summary>
        /// <param name="dto">支払集計表DTOクラス</param>
        /// <returns>売上／支払伝票の支払集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.ShiharaiShukeiHyo.Sql.GetShiharaiShukeiHyoDataUriageShiharai.sql")]
        List<ShiharaiData> GetShukeiHyoDataUriageShiharai(ShiharaiShukeiHyoDto dto);
    }
}
