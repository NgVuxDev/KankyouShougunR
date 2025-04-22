using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 社員マスタDao
    /// </summary>
    [Bean(typeof(M_SHAIN))]
    public interface IM_SHAINDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHAIN data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHAIN data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SHAIN data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_SHAIN")]
        M_SHAIN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shain.IM_SHAINDao_GetAllValidData.sql")]
        M_SHAIN[] GetAllValidData(M_SHAIN data);

        /// <summary>
        /// 論理削除フラグ更新処理（"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"のみを更新する）
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(M_SHAIN data);

        /// <summary>
        /// 社員コードを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="cd">社員コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_SHAIN GetDataByCd(string cd);

        /// <summary>
        /// 社員コードを元に担当者情報を取得する
        /// </summary>
        /// <parameparam name="shainCd">社員コード</parameparam>
        /// <returns>取得したデータ</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shain.IM_SHAINDao_GetTantoushaDate.sql")]
        M_SHAIN GetTantoushaDate(string shainCd);

        [Sql("select M_SHAIN.SHAIN_CD AS CD,M_SHAIN.SHAIN_NAME_RYAKU AS NAME FROM M_SHAIN /*$whereSql*/ group by M_SHAIN.SHAIN_CD,M_SHAIN.SHAIN_NAME_RYAKU order by M_SHAIN.SHAIN_CD")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">社員データ</param>
        /// <returns></returns>
        DataTable GetShainDataSqlFile(string path, M_SHAIN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">社員データ</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHAIN_CD);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHAIN data, bool deletechuFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}