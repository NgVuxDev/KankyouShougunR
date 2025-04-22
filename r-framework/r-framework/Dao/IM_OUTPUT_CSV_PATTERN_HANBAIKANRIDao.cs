using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(M_OUTPUT_CSV_PATTERN_HANBAIKANRI))]
    public interface IM_OUTPUT_CSV_PATTERN_HANBAIKANRIDao : IS2Dao
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_CSV_PATTERN_HANBAIKANRI data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        M_OUTPUT_CSV_PATTERN_HANBAIKANRI GetDataForEntity(M_OUTPUT_CSV_PATTERN_HANBAIKANRI condition);
    }
}