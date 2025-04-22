using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ITAKU_SBN_KYOKASHO))]
    public interface IM_ITAKU_SBN_KYOKASHODao : IS2Dao
    {

        [Sql("SELECT * FROM M_ITAKU_SBN_KYOKASHO")]
        M_ITAKU_SBN_KYOKASHO[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_SBN_KYOKASHO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_SBN_KYOKASHO data);

        int Delete(M_ITAKU_SBN_KYOKASHO data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ITAKU_SBN_KYOKASHO data);

        [Query("SYSTEM_ID = /*systemId*/")]
        M_ITAKU_SBN_KYOKASHO[] GetDataBySystemId(string systemId);

        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/ and SEQ = /*data.SEQ*/")]
        M_ITAKU_SBN_KYOKASHO GetDataByPrimaryKey(M_ITAKU_SBN_KYOKASHO data);
    }
}
