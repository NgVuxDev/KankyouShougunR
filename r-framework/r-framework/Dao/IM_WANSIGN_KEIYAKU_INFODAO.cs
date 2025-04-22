using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// WAN-Sign文書詳細情報
    /// </summary>
    [Bean(typeof(M_WANSIGN_KEIYAKU_INFO))]
    public interface IM_WANSIGN_KEIYAKU_INFODAO : IS2Dao
    {
        /// <summary>
        /// 全データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO")]
        M_WANSIGN_KEIYAKU_INFO[] GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WANSIGN_KEIYAKU_INFO data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WANSIGN_KEIYAKU_INFO data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("WANSIGN_SYSTEM_ID = /*wanSignSystemId*/ AND DELETE_FLG = 0")]
        M_WANSIGN_KEIYAKU_INFO GetDataBySystemId(string wanSignSystemId);

        /// <summary>
        /// トランザクションIDをもとにデータを取得する
        /// </summary>
        /// <param name="controlNumber">関連コード</param>
        /// <returns>取得したデータ</returns>
        [Query("CONTROL_NUMBER = /*controlNumber*/ AND DELETE_FLG = 0")]
        M_WANSIGN_KEIYAKU_INFO GetDataByControlNumber(string controlNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable getDateForStringSql(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(WANSIGN_SYSTEM_ID), 0) + 1 FROM M_WANSIGN_KEIYAKU_INFO")]
        long GetMaxPlusKey();

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO WHERE ORIGINAL_CONTROL_NUMBER = /*originalControlNumber*/")]
        M_WANSIGN_KEIYAKU_INFO[] GetDataByKanriBango(string originalControlNumber);

        /// <summary>
        /// データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO WHERE DOCUMENT_ID = /*documentId*/ ORDER BY CONTROL_NUMBER ASC")]
        M_WANSIGN_KEIYAKU_INFO[] GetDataByDocumentId(string documentId);

        //PhuocLoc 2022/03/08 #161248 -Start
        /// <summary>
        /// データを取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO WHERE DOCUMENT_ID <> /*documentId*/ AND ORIGINAL_CONTROL_NUMBER = /*originalControlNumber*/")]
        M_WANSIGN_KEIYAKU_INFO[] GetDataDuplicate(string documentId, string originalControlNumber);
        //PhuocLoc 2022/03/08 #161248 -End
    }
}