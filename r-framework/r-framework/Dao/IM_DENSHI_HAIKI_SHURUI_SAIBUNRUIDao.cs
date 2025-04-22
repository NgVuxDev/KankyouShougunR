using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI))]
    public interface IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI")]
        M_DENSHI_HAIKI_SHURUI_SAIBUNRUI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        int Delete(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data, bool deletechuFlg);

        /// <summary>
        /// 最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        /// 情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiHaikiShuruiSaibunrui.M_DENSHI_HAIKI_SHURUI_SAIBUNRUI_GetAllValidData.sql")]
        M_DENSHI_HAIKI_SHURUI_SAIBUNRUI[] GetAllValidData(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        /// <summary>
        /// マスタ共通ポップアップ用情報取得
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("SELECT M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_SAIBUNRUI_CD AS CD, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_NAME AS NAME FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI /*$whereSql*/ GROUP BY M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.EDI_MEMBER_ID, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_CD, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_SAIBUNRUI_CD, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
