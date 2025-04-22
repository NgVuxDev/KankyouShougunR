using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Allocation.Sharyoukyuudounyuryoku
{
    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    public interface T_UKETSUKE_MK_ENTRYDao : IS2Dao
    {

        [Sql("SELECT * FROM T_UKETSUKE_MK_ENTRY")]
        T_UKETSUKE_MK_ENTRY[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_MK_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_MK_ENTRY data);

        int Delete(T_UKETSUKE_MK_ENTRY data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        [SqlFile("Shougun.Core.Allocation.Sharyoukyuudounyuryoku.Sql.GetUketsukeMKData.sql")]
        DataTable GetUketsukeMKData(T_UKETSUKE_MK_ENTRY data);
    }
}
