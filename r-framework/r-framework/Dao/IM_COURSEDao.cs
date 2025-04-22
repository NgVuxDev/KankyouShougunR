using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_COURSE))]
    public interface IM_COURSEDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_COURSE")]
        M_COURSE[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Course.IM_COURSEDao_GetAllValidData.sql")]
        M_COURSE[] GetAllValidData(M_COURSE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE data);

        int Delete(M_COURSE data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_COURSE data);
    }
}
