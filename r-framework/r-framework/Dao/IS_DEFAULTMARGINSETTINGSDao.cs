using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 帳票余白設定Dao
    /// </summary>
    [Bean(typeof(S_DEFAULTMARGINSETTINGS))]
    public interface IS_DEFAULTMARGINSETTINGSDao : IS2Dao
    {
        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM S_DEFAULTMARGINSETTINGS")]
        S_DEFAULTMARGINSETTINGS[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        int Delete(S_DEFAULTMARGINSETTINGS data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, S_DEFAULTMARGINSETTINGS data);
    }
}
