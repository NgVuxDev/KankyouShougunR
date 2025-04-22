using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(T_KEIRYOU_DETAIL))]
    public interface IT_KEIRYOU_DETAILDao : IS2Dao
    {

        [Sql("SELECT * FROM T_KEIRYOU_DETAIL")]
        T_KEIRYOU_DETAIL[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KEIRYOU_DETAIL data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_KEIRYOU_DETAIL data);

        int Delete(T_KEIRYOU_DETAIL data);

        DataTable GetDataBySqlFile(string path, T_KEIRYOU_DETAIL data);
    }
}
