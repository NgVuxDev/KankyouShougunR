using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_MANIFEST_SHURUI))]
    public interface IM_MANIFEST_SHURUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_MANIFEST_SHURUI")]
        M_MANIFEST_SHURUI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestShurui.IM_MANIFEST_SHURUIDao_GetAllValidData.sql")]
        M_MANIFEST_SHURUI[] GetAllValidData(M_MANIFEST_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MANIFEST_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MANIFEST_SHURUI data);

        int Delete(M_MANIFEST_SHURUI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_SHURUI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// 取得方法未定
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("MANIFEST_SHURUI_CD = /*cd*/")]
        M_MANIFEST_SHURUI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_MANIFEST_SHURUI data, bool deletechuFlg);

        [Sql("select right('00' + convert(varchar, M_MANIFEST_SHURUI.MANIFEST_SHURUI_CD), 2) AS CD,M_MANIFEST_SHURUI.MANIFEST_SHURUI_NAME_RYAKU AS NAME FROM M_MANIFEST_SHURUI /*$whereSql*/ group by  M_MANIFEST_SHURUI.MANIFEST_SHURUI_CD,M_MANIFEST_SHURUI.MANIFEST_SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">ﾏﾆﾌｪｽﾄ種類</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] MANIFEST_SHURUI_CD);
    }
}
