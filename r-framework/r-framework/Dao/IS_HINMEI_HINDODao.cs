using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(S_HINMEI_HINDO))]
    public interface IS_HINMEI_HINDODao : IS2Dao
    {
        [Sql("SELECT * FROM S_HINMEI_HINDO")]
        S_HINMEI_HINDO[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_HINMEI_HINDO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_HINMEI_HINDO data);

        int Delete(S_HINMEI_HINDO data);

        DataTable GetDataBySqlFile(string path, S_HINMEI_HINDO data);
    }
}
