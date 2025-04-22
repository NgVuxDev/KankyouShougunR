using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SALES_ZAIKO_HOKAN_JOU))]
    public interface IM_SALES_ZAIKO_HOKAN_JOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SALES_ZAIKO_HOKAN_JOU")]
        M_SALES_ZAIKO_HOKAN_JOU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaShurui.IM_SALES_ZAIKO_HOKAN_JOUDao_GetAllValidData.sql")]
        M_SALES_ZAIKO_HOKAN_JOU[] GetAllValidData(M_SALES_ZAIKO_HOKAN_JOU data);

        int Insert(M_SALES_ZAIKO_HOKAN_JOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC")]
        int Update(M_SALES_ZAIKO_HOKAN_JOU data);

        int Delete(M_SALES_ZAIKO_HOKAN_JOU data);

        [Sql("select M_SALES_ZAIKO_HOKAN_JOU.SALES_ZAIKO_HOKAN_JOU_CD AS CD,M_SALES_ZAIKO_HOKAN_JOU.SALES_ZAIKO_HOKAN_JOU_NAME_RYAKU AS NAME FROM M_SALES_ZAIKO_HOKAN_JOU /*$whereSql*/ group by  M_SALES_ZAIKO_HOKAN_JOU.SALES_ZAIKO_HOKAN_JOU_CD,M_SALES_ZAIKO_HOKAN_JOU.SALES_ZAIKO_HOKAN_JOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SALES_ZAIKO_HOKAN_JOU data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SALES_ZAIKO_HOKAN_JOU_CD = /*cd*/")]
        M_SALES_ZAIKO_HOKAN_JOU GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SALES_ZAIKO_HOKAN_JOU data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
