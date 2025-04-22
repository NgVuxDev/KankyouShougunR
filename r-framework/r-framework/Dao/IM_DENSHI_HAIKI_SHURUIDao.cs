using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface IM_DENSHI_HAIKI_SHURUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_HAIKI_SHURUI")]
        M_DENSHI_HAIKI_SHURUI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_HAIKI_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_HAIKI_SHURUI data);

        int Delete(M_DENSHI_HAIKI_SHURUI data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_HAIKI_SHURUI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_HAIKI_SHURUI data, bool deletechuFlg);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">廃棄物種類CDの配列</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] HAIKI_SHURUI_CD);

        /// <summary>
        /// 最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(HAIKI_SHURUI_CD),0)+1 FROM M_DENSHI_HAIKI_SHURUI WHERE ISNUMERIC(HAIKI_SHURUI_CD) = 1")]
        int GetMaxPlusKey();

        /// <summary>
        /// 情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiHaikiShurui.M_DENSHI_HAIKI_SHURUI_GetAllValidData.sql")]
        M_DENSHI_HAIKI_SHURUI[] GetAllValidData(M_DENSHI_HAIKI_SHURUI data);

        /// <summary>
        /// マスタ共通ポップアップ用情報取得
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("SELECT M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD AS CD,M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS NAME FROM M_DENSHI_HAIKI_SHURUI /*$whereSql*/ GROUP BY M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD,M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
