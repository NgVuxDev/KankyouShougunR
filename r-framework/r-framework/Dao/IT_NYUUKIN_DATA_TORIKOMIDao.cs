using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(T_NYUUKIN_DATA_TORIKOMI))]
    public interface IT_NYUUKIN_DATA_TORIKOMIDao : IS2Dao
    {

        [Sql("SELECT * FROM T_NYUUKIN_DATA_TORIKOMI")]
        T_UKEIRE_ENTRY[] GetAllData();

         [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_DATA_TORIKOMI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_DATA_TORIKOMI data);

        int Delete(T_NYUUKIN_DATA_TORIKOMI data);

        DataTable GetDataBySqlFile(string path, T_NYUUKIN_DATA_TORIKOMI data);

    }
}
