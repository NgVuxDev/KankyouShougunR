using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHURUI))]
    public interface IM_SHURUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHURUI")]
        M_SHURUI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shurui.IM_SHURUIDao_GetAllValidData.sql")]
        M_SHURUI[] GetAllValidData(M_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHURUI data);

        int Delete(M_SHURUI data);

        [Sql("select M_SHURUI.SHURUI_CD AS CD,M_SHURUI.SHURUI_NAME_RYAKU AS NAME FROM M_SHURUI /*$whereSql*/ group by M_SHURUI.SHURUI_CD,M_SHURUI.SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHURUI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SHURUI_CD = /*cd*/")]
        M_SHURUI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHURUI data, bool deletechuFlg);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">種類データ</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHURUI_CD);

        //20250325
        [Sql("SELECT SHURUI_CD, SHURUI_NAME_RYAKU FROM M_SHURUI WHERE DELETE_FLG = 0")]
        DataTable GetAllDataForCbb();
    }
}
