using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Collections.Generic;
using Shougun.Core.SalesPayment.UriageZennenTaihihyou;

namespace Shougun.Core.SalesPayment.UriageZennenTaihihyou
{
    /// <summary>
    /// 売上集計表Daoインタフェース
    /// </summary>
    [Bean(typeof(UriageData))]
    internal interface UriageZennenTaihihyouDao : IS2Dao
    {
        /// <summary>
        /// 受入伝票の売上集計表データを取得します
        /// </summary>
        /// <param name="dto">売上集計表DTOクラス</param>
        /// <returns>受入伝票の売上集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageZennenTaihihyou.Sql.GetUriageShukeiHyoDataUkeire.sql")]
        List<UriageData> GetShukeiHyoDataUkeire(UriageZennenTaihihyouDto dto);

        /// <summary>
        /// 出荷伝票の売上集計表データを取得します
        /// </summary>
        /// <param name="dto">売上集計表DTOクラス</param>
        /// <returns>出荷伝票の売上集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageZennenTaihihyou.Sql.GetUriageShukeiHyoDataShukka.sql")]
        List<UriageData> GetShukeiHyoDataShukka(UriageZennenTaihihyouDto dto);

        /// <summary>
        /// 売上／支払伝票の売上集計表データを取得します
        /// </summary>
        /// <param name="dto">売上集計表DTOクラス</param>
        /// <returns>売上／支払伝票の売上集計表データ</returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageZennenTaihihyou.Sql.GetUriageShukeiHyoDataUriageShiharai.sql")]
        List<UriageData> GetShukeiHyoDataUriageShiharai(UriageZennenTaihihyouDto dto);
    }
}
