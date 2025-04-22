using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_BUNRUI))]
    public interface IM_BUNRUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_BUNRUI")]
        M_BUNRUI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Bunrui.IM_BUNRUIDao_GetAllValidData.sql")]
        M_BUNRUI[] GetAllValidData(M_BUNRUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BUNRUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BUNRUI data);

        int Delete(M_BUNRUI data);

        [Sql("select M_BUNRUI.BUNRUI_CD AS CD,M_BUNRUI.BUNRUI_NAME_RYAKU AS NAME FROM M_BUNRUI /*$whereSql*/ group by M_BUNRUI.BUNRUI_CD,M_BUNRUI.BUNRUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BUNRUI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">品名データ</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'品名マスタ' AS NAME FROM M_HINMEI WHERE BUNRUI_CD IN /*BUNRUI_CD*/('') AND DELETE_FLG = 'False'")]
        DataTable GetDataBySqlFileCheck(string[] BUNRUI_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("BUNRUI_CD = /*cd*/")]
        M_BUNRUI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BUNRUI data, bool deletechuFlg);
    }
}
