using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_MANIFEST_KANSAN))]
    public interface IM_DENSHI_MANIFEST_KANSANDao : IS2Dao
    {
        /// <summary>
        /// Insert処理
        /// </summary>
        /// <param name="data">Insert対象Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="data">更新対象Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="data">削除対象Entity</param>
        /// <returns></returns>
        int Delete(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// 全データを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DENSHI_MANIFEST_KANSAN")]
        M_DENSHI_MANIFEST_KANSAN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">検索条件</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestKansan.IM_DENSHI_MANIFEST_KANSANDao_GetAllValidData.sql")]
        M_DENSHI_MANIFEST_KANSAN[] GetAllValidData(M_DENSHI_MANIFEST_KANSAN data);

        /// <summary>
        /// コードを基にデータを取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>取得したデータ</returns>
        [Query("EDI_MEMBER_ID = /*data.EDI_MEMBER_ID*/ and HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*/ and HAIKI_SHURUI_SAIBUNRUI_CD = /*data.HAIKI_SHURUI_SAIBUNRUI_CD*/ and UNIT_CD = /*data.UNIT_CD*/")]
        M_DENSHI_MANIFEST_KANSAN GetDataByCd(M_DENSHI_MANIFEST_KANSAN data);
    }
}
