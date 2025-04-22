using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 電子契約接続管理Dao
    /// </summary>
    [Bean(typeof(S_DENSHI_CONNECT))]
    public interface IS_DENSHI_CONNECTDao : IS2Dao
    {
        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM S_DENSHI_CONNECT")]
        S_DENSHI_CONNECT[] GetAllData();

        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_DENSHI_CONNECT data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_DENSHI_CONNECT data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_DENSHI_CONNECT data);

        /// <summary>
        /// 目次をもとにデータを取得する
        /// </summary>
        /// <param name="contentName">目次</param>
        /// <returns>取得したデータ</returns>
        [Query("CONTENT_NAME = /*contentName*/")]
        S_DENSHI_CONNECT GetDataByContentName(string contentName);
    }
}
