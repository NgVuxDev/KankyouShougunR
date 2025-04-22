using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface IM_DENSHI_JIGYOUSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_JIGYOUSHA")]
        M_DENSHI_JIGYOUSHA[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_JIGYOUSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUSHA data);

        int Delete(M_DENSHI_JIGYOUSHA data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">加入者番号</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string EDI_MEMBER_ID);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiJigyousha.IM_DENSHI_JIGYOUSHADao_GetAllValidData.sql")]
        M_DENSHI_JIGYOUSHA[] GetAllValidData(M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// 業者コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_GENBA WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        [Sql("select M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID AS CD,M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME AS NAME FROM M_DENSHI_JIGYOUSHA /*$whereSql*/ group by M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID,M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

    }
}
