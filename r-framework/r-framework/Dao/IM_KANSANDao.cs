using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KANSAN))]
    public interface IM_KANSANDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KANSAN")]
        M_KANSAN[] GetAllData();
        
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Kansan.IM_KANSANDao_GetAllValidData.sql")]
        M_KANSAN[] GetAllValidData(M_KANSAN data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Kansan.IM_KANSANDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KANSAN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KANSAN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KANSAN data);

        int Delete(M_KANSAN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KANSAN data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// 利用方法未定
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("DENPYOU_KBN_CD = /*data.DENPYOU_KBN_CD*/ and HINMEI_CD = /*data.HINMEI_CD*/ and UNIT_CD = /*data.UNIT_CD*/")]
        M_KANSAN GetDataByCd(M_KANSAN data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KANSAN data, bool deletechuFlg);

        [Sql("select M_KANSAN.DENPYOU_KBN_CD, M_KANSAN.HINMEI_CD FROM M_KANSAN /*$whereSql*/")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の更新条件によるデータ更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_KANSAN data, M_KANSAN updateKey);
    }
}
