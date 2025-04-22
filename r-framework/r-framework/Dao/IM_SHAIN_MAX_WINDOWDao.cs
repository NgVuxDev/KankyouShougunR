using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 社員最大表示画面数マスタDao
    /// </summary>
    [Bean(typeof(M_SHAIN_MAX_WINDOW))]
    public interface IM_SHAIN_MAX_WINDOWDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHAIN_MAX_WINDOW data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHAIN_MAX_WINDOW data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SHAIN_MAX_WINDOW data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_SHAIN_MAX_WINDOW")]
        M_SHAIN_MAX_WINDOW[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ShainMaxWindow.IM_SHAIN_MAX_WINDOWDao_GetAllValidData.sql")]
        M_SHAIN_MAX_WINDOW[] GetAllValidData(M_SHAIN_MAX_WINDOW data);

        /// <summary>
        /// 論理削除フラグ更新処理（"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"のみを更新する）
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(M_SHAIN_MAX_WINDOW data);

        /// <summary>
        /// 社員コードを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="cd">社員コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_SHAIN_MAX_WINDOW GetDataByCd(string cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">検索用データ</param>
        /// <returns></returns>
        DataTable GetShainDataSqlFile(string path, M_SHAIN_MAX_WINDOW data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHAIN_MAX_WINDOW data, bool deletechuFlg);

    }
}