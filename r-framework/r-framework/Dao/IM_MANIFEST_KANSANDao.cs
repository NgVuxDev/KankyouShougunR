using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_MANIFEST_KANSAN))]
    public interface IM_MANIFEST_KANSANDao : IS2Dao
    {

        [Sql("SELECT * FROM M_MANIFEST_KANSAN")]
        M_MANIFEST_KANSAN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestKansan.IM_MANIFEST_KANSANDao_GetAllValidData.sql")]
        M_MANIFEST_KANSAN[] GetAllValidData(M_MANIFEST_KANSAN data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestKansan.IM_MANIFEST_KANSANDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_BANK_SHITEN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MANIFEST_KANSAN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MANIFEST_KANSAN data);

        int Delete(M_MANIFEST_KANSAN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_KANSAN data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// 使用方法未定
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*/ and HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/ and UNIT_CD = /*data.UNIT_CD*/ and NISUGATA_CD = /*data.NISUGATA_CD*/")]
        M_MANIFEST_KANSAN GetDataByCd(M_MANIFEST_KANSAN data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_MANIFEST_KANSAN data, bool deletechuFlg);

        [Sql("select M_MANIFEST_KANSAN.HOUKOKUSHO_BUNRUI_CD,M_MANIFEST_KANSAN.HAIKI_NAME_CD FROM M_MANIFEST_KANSAN /*$whereSql*/ group by  M_MANIFEST_KANSAN.HOUKOKUSHO_BUNRUI_CD,M_MANIFEST_KANSAN.HAIKI_NAME_CD")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の更新条件によるデータ更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_MANIFEST_KANSAN data, M_MANIFEST_KANSAN updateKey);
    }
}
