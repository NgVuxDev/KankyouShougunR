using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface IT_UKEIRE_ENTRYDao : IS2Dao
    {

        [Sql("SELECT * FROM T_UKEIRE_ENTRY")]
        T_UKEIRE_ENTRY[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKEIRE_ENTRY data);

        int Delete(T_UKEIRE_ENTRY data);

        DataTable GetDataBySqlFile(string path, T_UKEIRE_ENTRY data);

        [SqlFile("r_framework.Dao.SqlFile.Busho.IT_UKEIRE_ENTRYDao_DataByStringSql.sql")]
        DataTable DataByStringSql(string whereSql);
    }
}
