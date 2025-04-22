using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface IT_KEIRYOU_ENTRYDao : IS2Dao
    {

        [Sql("SELECT * FROM T_KEIRYOU_ENTRY")]
        T_KEIRYOU_ENTRY[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KEIRYOU_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_KEIRYOU_ENTRY data);

        int Delete(T_KEIRYOU_ENTRY data);

        DataTable GetDataBySqlFile(string path, T_KEIRYOU_ENTRY data);

        [SqlFile("r_framework.Dao.SqlFile.Busho.IT_KEIRYOU_ENTRYDao_DataByStringSql.sql")]
        DataTable DataByStringSql(string whereSql);
    }
}
