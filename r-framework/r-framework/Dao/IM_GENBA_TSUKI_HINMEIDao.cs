using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 現場_月極品名マスタDao
    /// </summary>
    [Bean(typeof(M_GENBA_TSUKI_HINMEI))]
    public interface IM_GENBA_TSUKI_HINMEIDao : IchiranBaseDao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得(業者エンティティ付)
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="gyousha">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileWithGyousha(string path, M_GENBA_TSUKI_HINMEI data, M_GYOUSHA gyousha);
    }
}