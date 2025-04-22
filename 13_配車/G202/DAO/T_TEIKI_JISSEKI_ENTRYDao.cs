using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Allocation.Untenshakyudounyuuryoku
{
    [Bean(typeof(T_TEIKI_JISSEKI_ENTRY))]
    public interface T_TEIKI_JISSEKI_ENTRYDao : IS2Dao
    {

        [Sql("SELECT * FROM T_TEIKI_JISSEKI_ENTRY")]
        T_TEIKI_JISSEKI_ENTRY[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_JISSEKI_ENTRY data);

        int Delete(T_TEIKI_JISSEKI_ENTRY data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        [SqlFile("Shougun.Core.Allocation.Untenshakyudounyuuryoku.Sql.GetTeikiJissekiData.sql")]
        DataTable GetTeikiJissekiData(T_TEIKI_JISSEKI_ENTRY data);
    }
}
