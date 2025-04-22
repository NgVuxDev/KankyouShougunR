using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.SalesPayment.UriageShukeiHyo
{
    /// <summary>
    /// 売上集計表Daoインタフェース
    /// </summary>
    [Bean(typeof(UriageData))]
    internal interface UriageShukeiHyoDao : IS2Dao
    {
        /// <summary>
        /// 受入伝票の売上集計表データを取得します
        /// </summary>
        /// <param name="dto">売上集計表DTOクラス</param>
        /// <returns>受入伝票の売上集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShukeiHyo.Sql.GetUriageShukeiHyoDataUkeire.sql")]
        List<UriageData> GetShukeiHyoDataUkeire(UriageShukeiHyoDto dto);

        /// <summary>
        /// 出荷伝票の売上集計表データを取得します
        /// </summary>
        /// <param name="dto">売上集計表DTOクラス</param>
        /// <returns>出荷伝票の売上集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShukeiHyo.Sql.GetUriageShukeiHyoDataShukka.sql")]
        List<UriageData> GetShukeiHyoDataShukka(UriageShukeiHyoDto dto);

        /// <summary>
        /// 売上／支払伝票の売上集計表データを取得します
        /// </summary>
        /// <param name="dto">売上集計表DTOクラス</param>
        /// <returns>売上／支払伝票の売上集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShukeiHyo.Sql.GetUriageShukeiHyoDataUriageShiharai.sql")]
        List<UriageData> GetShukeiHyoDataUriageShiharai(UriageShukeiHyoDto dto);
    }
}
