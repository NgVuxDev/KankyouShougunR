using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 業者マスタDao
    /// </summary>
    [Bean(typeof(M_RAKU_RAKU))]
    public interface IM_RAKU_RAKUDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("RAKU_ID", "TIME_STAMP")]
        int Insert(M_RAKU_RAKU data);
        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("RAKU_ID", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_RAKU_RAKU data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_RAKU_RAKU data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_RAKU_RAKU")]
        M_RAKU_RAKU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.RakuRaku.IM_RAKU_RAKUDao_GetAllValidData.sql")]
        M_RAKU_RAKU[] GetAllValidData(M_RAKU_RAKU data);

        /// <summary>
        /// コードを元に業者データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>

        [Query("RAKU_ID = /*cd*/ AND DELETE_FLG = 0")]
        M_RAKU_RAKU GetDataByCd(string cd);

        [Query("SHOSHIKI_KBN = /*shoshiki*/ AND TORIHIKISAKI_CD = /*toriCd*/ AND DELETE_FLG = 0")]
        M_RAKU_RAKU GetDataByToriCd(string shoshiki, string toriCd);

        [Query("SHOSHIKI_KBN = /*shoshiki*/ AND TORIHIKISAKI_CD = /*toriCd*/ AND GYOUSHA_CD = /*gyoushaCd*/ AND DELETE_FLG = 0")]
        M_RAKU_RAKU GetDataByGyoushaCd(string shoshiki, string toriCd, string gyoushaCd);

        [Query("SHOSHIKI_KBN = /*shoshiki*/ AND TORIHIKISAKI_CD = /*toriCd*/ AND GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/ AND DELETE_FLG = 0")]
        M_RAKU_RAKU GetDataByGenbaCd(string shoshiki, string toriCd, string gyoushaCd, string genbaCd);
    }
}
