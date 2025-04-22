using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CORP_CLOSED))]
    public interface IM_CORP_CLOSEDDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CORP_CLOSED")]
        M_CORP_CLOSED[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.CorpClosed.IM_CORP_CLOSEDDao_GetAllValidData.sql")]
        M_CORP_CLOSED[] GetAllValidData(M_CORP_CLOSED data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CORP_CLOSED data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CORP_CLOSED data);

        int Delete(M_CORP_CLOSED data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CORP_CLOSED data);
    }
}
