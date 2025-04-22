using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    /// <summary>
    /// エラーメッセージマスタのDaoクラス
    /// </summary>
    [Bean(typeof(M_ERROR_MESSAGE))]
    public interface IM_ERROR_MESSAGEDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_ERROR_MESSAGE")]
        M_ERROR_MESSAGE[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ERROR_MESSAGE data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ERROR_MESSAGE data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_ERROR_MESSAGE data);

        /// <summary>
        /// メッセージIDを元にDBからメッセージを取得するメソッド
        /// </summary>
        /// <parameparam name="messageId">メッセージID</parameparam>
        [Query("MESSAGE_ID = /*messageId*/")]
        M_ERROR_MESSAGE GetMessage(string messageId);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ERROR_MESSAGE data);
    }
}
