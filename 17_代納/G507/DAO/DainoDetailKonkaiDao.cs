using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DAO
{
    /// <summary>
    /// 代納番号から今回明細情報(代納番号に紐づいているデータ)を取得
    /// </summary>
    [Bean(typeof(ResultDainoDetailKonkaiDto))]
    internal interface DainoDetailKonkaiDao : IS2Dao
    {
        /// <summary>
        /// [受入請求] 今回値を取得
        /// </summary>
        /// <param name="dainouNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetKonkaiUkeireSeikyuu.sql")]
        List<ResultDainoDetailKonkaiDto> GetKonkaiUkeireSeikyuList(long dainouNumber, bool meisaiChecked);

        /// <summary>
        /// [受入支払] 今回値を取得
        /// </summary>
        /// <param name="dainouNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetKonkaiUkeireShiharai.sql")]
        List<ResultDainoDetailKonkaiDto> GetKonkaiUkeireShiharaiList(long dainouNumber, bool meisaiChecked);

        /// <summary>
        /// [出荷請求] 今回値を取得
        /// </summary>
        /// <param name="dainouNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetKonkaiShukkaSeikyuu.sql")]
        List<ResultDainoDetailKonkaiDto> GetKonkaiShukkaSeikyuList(long dainouNumber, bool meisaiChecked);

        /// <summary>
        /// [出荷支払] 今回値を取得
        /// </summary>
        /// <param name="dainouNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetKonkaiShukkaShiharai.sql")]
        List<ResultDainoDetailKonkaiDto> GetKonkaiShukkaShiharaiList(long dainouNumber, bool meisaiChecked);

    }

}
