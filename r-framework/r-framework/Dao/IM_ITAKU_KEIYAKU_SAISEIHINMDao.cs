using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ITAKU_KEIYAKU_SAISEIHINM))]
    public interface IM_ITAKU_KEIYAKU_SAISEIHINMDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ITAKU_KEIYAKU_SAISEIHINM")]
        M_ITAKU_KEIYAKU_SAISEIHINM[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_SAISEIHINM data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_KEIYAKU_SAISEIHINM data);

        int Delete(M_ITAKU_KEIYAKU_SAISEIHINM data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ITAKU_KEIYAKU_SAISEIHINM data);

        /// <summary>
        /// SystemIdを元に
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        M_ITAKU_KEIYAKU_SAISEIHINM[] GetDataBySystemId(string systemId);

        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ and SEQ = /*data.SEQ*/")]
        M_ITAKU_KEIYAKU_SAISEIHINM GetDataByPrimaryKey(M_ITAKU_KEIYAKU_SAISEIHINM data);
    }
}
