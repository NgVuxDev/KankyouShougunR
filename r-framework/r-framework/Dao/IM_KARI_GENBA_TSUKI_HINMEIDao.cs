using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 仮現場_月極品名マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA_TSUKI_HINMEI))]
    public interface IM_KARI_GENBA_TSUKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// 業者、現場コードを元に情報を取得
        /// </summary>
        /// <parameparam name="genbaCd">現場コード</parameparam>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/")]
        M_KARI_GENBA_TSUKI_HINMEI[] GetKariGenbaTsukiHinmeiData(string gyoushaCd, string genbaCd);

        /// <summary>
        /// 指定されたSQLファイルを使用して一覧を取得します
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="entity">条件エンティティ</param>
        /// <returns>仮現場月極品名の一覧</returns>
        DataTable GetDataBySqlFile(string path, M_KARI_GENBA_TSUKI_HINMEI data);
    }
}