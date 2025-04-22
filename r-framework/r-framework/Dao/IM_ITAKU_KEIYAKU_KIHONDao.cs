using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ITAKU_KEIYAKU_KIHON))]
    public interface IM_ITAKU_KEIYAKU_KIHONDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ITAKU_KEIYAKU_KIHON")]
        M_ITAKU_KEIYAKU_KIHON[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ItakuKeiyakuKihon.IM_ITAKU_KEIYAKU_KIHONDao_GetAllValidData.sql")]
        M_ITAKU_KEIYAKU_KIHON[] GetAllValidData(M_ITAKU_KEIYAKU_KIHON data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_KIHON data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_KEIYAKU_KIHON data);

        int Delete(M_ITAKU_KEIYAKU_KIHON data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ITAKU_KEIYAKU_KIHON data);

        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM M_ITAKU_KEIYAKU_KIHON where ISNUMERIC(SYSTEM_ID) = 1 ")]
        int GetMaxPlusKey();

        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/")]
        M_ITAKU_KEIYAKU_KIHON GetDataBySystemId(M_ITAKU_KEIYAKU_KIHON data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        //#160047 20220328 CongBinh S
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keiyakuNo"></param>
        /// <returns></returns>
        [Query("ITAKU_KEIYAKU_NO = /*keiyakuNo*/")]
        M_ITAKU_KEIYAKU_KIHON[] GetDataByKeiyakuNo(string keiyakuNo);
        //#160047 20220328 CongBinh E
    }
}
