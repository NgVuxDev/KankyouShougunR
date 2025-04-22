using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KONGOU_HAIKIBUTSU))]
    public interface IM_KONGOU_HAIKIBUTSUDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_KONGOU_HAIKIBUTSU")]
        M_KONGOU_HAIKIBUTSU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouHaikibutsu.IM_KONGOU_HAIKIBUTSUDao_GetAllValidData.sql")]
        M_KONGOU_HAIKIBUTSU[] GetAllValidData(M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouHaikibutsu.IM_KONGOU_HAIKIBUTSUDao_GetAllValidDataForPupUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KONGOU_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KONGOU_HAIKIBUTSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KONGOU_HAIKIBUTSU data);

        int Delete(M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/ and KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*/ and HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*/")]
        M_KONGOU_HAIKIBUTSU GetDataByCd(M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// ユーザ指定の更新条件によるデータ更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_KONGOU_HAIKIBUTSU data, M_KONGOU_HAIKIBUTSU updateKey);
    }
}
