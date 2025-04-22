using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN))]
    public interface IM_OUTPUT_CSV_PATTERN_NYUUSHUKKINDao : IS2Dao
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN GetDataForEntity(M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN condition);
    }
}