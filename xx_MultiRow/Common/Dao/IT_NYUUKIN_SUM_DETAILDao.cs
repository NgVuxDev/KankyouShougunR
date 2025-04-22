using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using r_framework.Entity;

namespace Shougun.Function.ShougunCSCommon.Dao
{
    [Bean(typeof(T_NYUUKIN_SUM_DETAIL))]
    public interface IT_NYUUKIN_SUM_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_SUM_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_SUM_DETAIL data);
    }
}
