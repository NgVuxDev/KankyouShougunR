using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// オールドデータデルDao
    /// </summary>
    [Bean(typeof(M_OLD_DATA_DEL))]
    public interface IM_OLD_DATA_DELDao : IS2Dao
    {
        [Sql("SELECT * FROM M_OLD_DATA_DEL")]
        M_OLD_DATA_DEL[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OLD_DATA_DEL data);

        [NoPersistentProps("TIME_STAMP")]
        int Update(M_OLD_DATA_DEL data);

    }
}