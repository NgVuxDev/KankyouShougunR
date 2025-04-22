using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(M_OUTPUT_CSV_PATTERN))]
    public interface IM_OUTPUT_CSV_PATTERNDao : IS2Dao
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_CSV_PATTERN data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_CSV_PATTERN data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        M_OUTPUT_CSV_PATTERN GetDataForEntity(M_OUTPUT_CSV_PATTERN condition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        List<M_OUTPUT_CSV_PATTERN> GetDataForEntities(M_OUTPUT_CSV_PATTERN condition);
    }
}