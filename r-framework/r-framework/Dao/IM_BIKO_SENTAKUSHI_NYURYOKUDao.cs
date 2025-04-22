using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_BIKO_SENTAKUSHI_NYURYOKU))]
    public interface IM_BIKO_SENTAKUSHI_NYURYOKUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_BIKO_SENTAKUSHI_NYURYOKU")]
        M_BIKO_SENTAKUSHI_NYURYOKU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.BikoSentakushiNyuryoku.IM_BIKO_SENTAKUSHI_NYURYOKUDao_GetAllValidData.sql")]
        M_BIKO_SENTAKUSHI_NYURYOKU[] GetAllValidData(M_BIKO_SENTAKUSHI_NYURYOKU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BIKO_SENTAKUSHI_NYURYOKU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BIKO_SENTAKUSHI_NYURYOKU data);

        int Delete(M_BIKO_SENTAKUSHI_NYURYOKU data);

        [Sql("select M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_CD AS CD,M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_NOTE AS NOTE FROM M_BIKO_SENTAKUSHI_NYURYOKU /*$whereSql*/ group by M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_CD,M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_NOTE")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BIKO_SENTAKUSHI_NYURYOKU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">品名データ</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'備考内訳入力' AS NAME FROM M_BIKO_UCHIWAKE_NYURYOKU WHERE BIKO_CD IN /*BIKO_CD*/('') " +
             "UNION " +
             "SELECT DISTINCT N'見積入力' AS NAME FROM T_MITSUMORI_DETAIL_2 WHERE BIKO_CD IN /*BIKO_CD*/('') ")]
        DataTable GetDataBySqlFileCheck(string[] BIKO_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("BIKO_CD = /*cd*/")]
        M_BIKO_SENTAKUSHI_NYURYOKU GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BIKO_SENTAKUSHI_NYURYOKU data, bool deletechuFlg);
    }
}
