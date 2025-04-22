using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(T_UKEIRE_DETAIL))]
    public interface IT_UKEIRE_DETAILDao : IS2Dao
    {

        [Sql("SELECT * FROM T_UKEIRE_DETAIL")]
        T_UKEIRE_DETAIL[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_DETAIL data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKEIRE_DETAIL data);

        int Delete(T_UKEIRE_DETAIL data);

        DataTable GetDataBySqlFile(string path, T_UKEIRE_DETAIL data);
    }
}
