using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Allocation.Untenshakyudounyuuryoku
{
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface T_UKEIRE_ENTRYDao : IS2Dao
    {

        [Sql("SELECT * FROM T_UKEIRE_ENTRY")]
        T_UKEIRE_ENTRY[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKEIRE_ENTRY data);

        int Delete(T_UKEIRE_ENTRY data);

        [SqlFile("Shougun.Core.Allocation.Untenshakyudounyuuryoku.Sql.GetUkeireData.sql")]
        DataTable GetUkeireData(T_UKEIRE_ENTRY data);
    }
}
