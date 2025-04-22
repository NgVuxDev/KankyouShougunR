using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHUUKEI_KOUMOKU))]
    public interface IM_SHUUKEI_KOUMOKUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHUUKEI_KOUMOKU")]
        M_SHUUKEI_KOUMOKU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ShuukeiKoumoku.IM_SHUUKEI_KOUMOKUDao_GetAllValidData.sql")]
        M_SHUUKEI_KOUMOKU[] GetAllValidData(M_SHUUKEI_KOUMOKU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHUUKEI_KOUMOKU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHUUKEI_KOUMOKU data);

        int Delete(M_SHUUKEI_KOUMOKU data);

        [Sql("select M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_CD as CD,M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU as NAME FROM M_SHUUKEI_KOUMOKU /*$whereSql*/ group by M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_CD,M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHUUKEI_KOUMOKU data);

        /// <summary>
        /// PKキー配列の検索条件による他データ使用有無判定用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="SHUUKEI_KOUMOKU_CD">集計項目CDリスト</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHUUKEI_KOUMOKU_CD);

        /// <summary>
        /// コードを元に集計項目データを取得する
        /// </summary>
        /// <parameparam name="cd">集計項目コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("SHUUKEI_KOUMOKU_CD = /*cd*/")]
        M_SHUUKEI_KOUMOKU GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHUUKEI_KOUMOKU data, bool deletechuFlg);
    }
}
