using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.Scale.KeiryouShukeiHyo
{
    /// <summary>
    /// 計量集計表Daoインタフェース
    /// </summary>
    [Bean(typeof(KeiryouData))]
    internal interface KeiryouShukeiHyoDao : IS2Dao
    {
        /// <summary>
        /// 受入伝票の計量集計表データを取得します
        /// </summary>
        /// <param name="dto">計量集計表DTOクラス</param>
        /// <returns>受入伝票の計量集計表データ</returns>
        [SqlFile("Shougun.Core.Scale.KeiryouShukeiHyo.Sql.GetUriageShukeiHyoDataKeiryou.sql")]
        List<KeiryouData> GetUriageShukeiHyoDataKeiryou(KeiryouShukeiHyoDto dto);

        ///// <summary>
        ///// 出荷伝票の計量集計表データを取得します
        ///// </summary>
        ///// <param name="dto">計量集計表DTOクラス</param>
        ///// <returns>出荷伝票の計量集計表データ</returns>
        //[SqlFile("Shougun.Core.Scale.KeiryouShukeiHyo.Sql.GetKeiryouShukeiHyoDataShukka.sql")]
        //List<UriageData> GetShukeiHyoDataShukka(KeiryouShukeiHyoDto dto);

        ///// <summary>
        ///// 集計／支払伝票の計量集計表データを取得します
        ///// </summary>
        ///// <param name="dto">計量集計表DTOクラス</param>
        ///// <returns>集計／支払伝票の計量集計表データ</returns>
        //[SqlFile("Shougun.Core.Scale.KeiryouShukeiHyo.Sql.GetKeiryouShukeiHyoDataUriageShiharai.sql")]
        //List<UriageData> GetShukeiHyoDataUriageShiharai(KeiryouShukeiHyoDto dto);
    }
}
