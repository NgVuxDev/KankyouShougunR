using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DAO
{
    /// <summary>
    /// 代納番号から基本情報を取得
    /// </summary>
    [Bean(typeof(ResultDainoEntryDto))]
    internal interface DainoEntryDto : IS2Dao
    {
        /// <summary>
        /// 代納番号から基本情報を取得
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="seikyuuDate"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PayByProxy.DainoDenpyoHakkou.Sql.GetDainoInfo.sql")]
        List<ResultDainoEntryDto> GetDainoInfo(long dainouNumber);
    }
    
}
