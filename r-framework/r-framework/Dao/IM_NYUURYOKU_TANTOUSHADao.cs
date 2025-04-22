using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NYUURYOKU_TANTOUSHA))]
    public interface IM_NYUURYOKU_TANTOUSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_NYUURYOKU_TANTOUSHA")]
        M_NYUURYOKU_TANTOUSHA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.NyuuryokuTantousha.IM_NYUURYOKU_TANTOUSHADao_GetAllValidData.sql")]
        M_NYUURYOKU_TANTOUSHA[] GetAllValidData(M_NYUURYOKU_TANTOUSHA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUURYOKU_TANTOUSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUURYOKU_TANTOUSHA data);

        int Delete(M_NYUURYOKU_TANTOUSHA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUURYOKU_TANTOUSHA data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_NYUURYOKU_TANTOUSHA GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NYUURYOKU_TANTOUSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
