using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KONGOU_SHURUI))]
    public interface IM_KONGOU_SHURUIDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_KONGOU_SHURUI")]
        M_KONGOU_SHURUI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouShurui.IM_KONGOU_SHURUIDao_GetAllValidData.sql")]
        M_KONGOU_SHURUI[] GetAllValidData(M_KONGOU_SHURUI data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouShurui.IM_KONGOU_SHURUIDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KONGOU_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KONGOU_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KONGOU_SHURUI data);

        int Delete(M_KONGOU_SHURUI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KONGOU_SHURUI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">混合種類データ</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] KONGOU_SHURUI_CD, int HAIKI_KBN_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/ and KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*/")]
        M_KONGOU_SHURUI GetDataByCd(M_KONGOU_SHURUI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KONGOU_SHURUI data, bool deletechuFlg);

        [Sql("select KONGOU_SHURUI_CD AS CD, KONGOU_SHURUI_NAME_RYAKU AS NAME from M_KONGOU_SHURUI /*$whereSql*/ group by HAIKI_KBN_CD, KONGOU_SHURUI_CD, KONGOU_SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
