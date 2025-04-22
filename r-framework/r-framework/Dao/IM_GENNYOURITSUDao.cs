using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_GENNYOURITSU))]
    public interface IM_GENNYOURITSUDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_GENNYOURITSU")]
        M_GENNYOURITSU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gennyouritsu.IM_GENNYOURITSUDao_GetAllValidData.sql")]
        M_GENNYOURITSU[] GetAllValidData(M_GENNYOURITSU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENNYOURITSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENNYOURITSU data);

        int Delete(M_GENNYOURITSU data);

        [Sql("select M_GENNYOURITSU.HOUKOKUSHO_BUNRUI_CD,M_GENNYOURITSU.HAIKI_NAME_CD,M_GENNYOURITSU.SHOBUN_HOUHOU_CD,M_GENNYOURITSU.GENNYOURITSU FROM M_GENNYOURITSU /*$whereSql*/")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENNYOURITSU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GENNYOURITSU data, bool deletechuFlg);

        /// <summary>
        /// Entityを元にデータの取得を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*/ and HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/ and SHOBUN_HOUHOU_CD = /*data.SHOBUN_HOUHOU_CD*/")]
        M_GENNYOURITSU GetDataByCd(M_GENNYOURITSU data);

        /// <summary>
        /// ユーザ指定の更新条件によるデータ更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_GENNYOURITSU data, M_GENNYOURITSU updateKey);
    }
}
