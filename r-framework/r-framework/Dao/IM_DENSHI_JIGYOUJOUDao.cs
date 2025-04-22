using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface IM_DENSHI_JIGYOUJOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_JIGYOUJOU")]
        M_DENSHI_JIGYOUJOU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_JIGYOUJOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUJOU data);

        int Delete(M_DENSHI_JIGYOUJOU data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_JIGYOUJOU data);

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_DENSHI_JIGYOUJOU WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiJigyoujo.IM_DENSHI_JIGYOUJOUDao_GetAllValidData.sql")]
        M_DENSHI_JIGYOUJOU[] GetAllValidData(M_DENSHI_JIGYOUJOU data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
