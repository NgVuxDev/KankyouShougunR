using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(SuperEntity))]
    public interface GET_SYSDATEDao : IS2Dao
    {

        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
