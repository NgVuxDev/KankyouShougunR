using System.Data;
using System.Collections.Generic;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(T_OPERATE_LOG))]
    public interface IT_OPERATE_LOGDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_OPERATE_LOG data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_OPERATE_LOG data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_OPERATE_LOG data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_OPERATE_LOG")]
        T_OPERATE_LOG[] GetAllData();


        /// <summary>
        /// 論理削除フラグ更新処理（"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"のみを更新する）
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(T_OPERATE_LOG data);

        /// <summary>
        /// 社員コードを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="cd">社員コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*cd*/")]
        T_OPERATE_LOG GetDataByCd(long cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">社員データ</param>
        /// <returns></returns>
        DataTable GetShainDataSqlFile(string path, T_OPERATE_LOG data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        [Sql("SELECT (ISNULL(MAX(SYSTEM_ID),0) + 1) AS SYSTEM_ID from T_OPERATE_LOG")]
        string GetMaxSystem_ID();

        [Sql("SELECT SYSTEM_ID from T_OPERATE_LOG where SYSTEM_ID = 1")]
        int CheckTableConnect();
    }
}
