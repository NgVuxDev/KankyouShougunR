using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface IM_DENSHI_TANTOUSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_TANTOUSHA")]
        M_DENSHI_TANTOUSHA[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_TANTOUSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_TANTOUSHA data);

        int Delete(M_DENSHI_TANTOUSHA data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_TANTOUSHA data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_TANTOUSHA data,bool deletechuFlg);

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
        [SqlFile("r_framework.Dao.SqlFile.DenshiTantousha.IM_DENSHI_TANTOUSHADao_GetAllValidData.sql")]
        M_DENSHI_TANTOUSHA[] GetAllValidData(M_DENSHI_TANTOUSHA data);
    }
}
