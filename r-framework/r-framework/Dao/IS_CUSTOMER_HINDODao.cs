using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(S_CUSTOMER_HINDO))]
    public interface IS_CUSTOMER_HINDODao : IS2Dao
    {

        [Sql("SELECT * FROM S_CUSTOMER_HINDO")]
        S_CUSTOMER_HINDO[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_CUSTOMER_HINDO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_CUSTOMER_HINDO data);

        int Delete(S_CUSTOMER_HINDO data);

        DataTable GetDataBySqlFile(string path, S_CUSTOMER_HINDO data);
    }
}
