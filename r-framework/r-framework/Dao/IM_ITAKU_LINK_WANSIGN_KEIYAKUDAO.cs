using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 委託契約WANSIGN連携
    /// </summary>
    [Bean(typeof(M_ITAKU_LINK_WANSIGN_KEIYAKU))]
    public interface IM_ITAKU_LINK_WANSIGN_KEIYAKUDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_LINK_WANSIGN_KEIYAKU data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_LINK_WANSIGN_KEIYAKU data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns>取得したデータ</returns>
        [Query("WANSIGN_SYSTEM_ID = /*wanSignSystemId*/ AND SYSTEM_ID = /*systemId*/")]
        M_ITAKU_LINK_WANSIGN_KEIYAKU GetDataBySystemId(long wanSignSystemId, long systemId);

        /// <summary>
        /// 委託契約WANSIGN連携（新テーブル）からWANSIGN_システムID　に紐づく情報を削除　
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Sql("DELETE FROM M_ITAKU_LINK_WANSIGN_KEIYAKU WHERE WANSIGN_SYSTEM_ID = /*wanSignSystemId*/ AND SYSTEM_ID = /*systemId*/")]
        int DeleteBySystemId(long wanSignSystemId, long systemId);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns>取得したデータ</returns>
        [Query("WANSIGN_SYSTEM_ID = /*wanSignSystemId*/")]
        M_ITAKU_LINK_WANSIGN_KEIYAKU[] GetDataByWanSignSystemId(long wanSignSystemId);

        /// <summary>
        /// 委託契約WANSIGN連携（新テーブル）からWANSIGN_システムID　に紐づく情報を削除　
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Sql("DELETE FROM M_ITAKU_LINK_WANSIGN_KEIYAKU WHERE WANSIGN_SYSTEM_ID = /*wanSignSystemId*/")]
        int DeleteByWanSignSystemId(long wanSignSystemId);
    }
}