using System.Collections.Generic;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(M_OUTPUT_CSV_PATTERN_COLUMN))]
    public interface IM_OUTPUT_CSV_PATTERN_COLUMNDao : IS2Dao
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_CSV_PATTERN_COLUMN data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<M_OUTPUT_CSV_PATTERN_COLUMN> GetDataForEntities(M_OUTPUT_CSV_PATTERN_COLUMN condition);
    }
}