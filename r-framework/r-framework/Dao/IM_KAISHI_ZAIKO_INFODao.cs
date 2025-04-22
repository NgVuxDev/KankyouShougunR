using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KAISHI_ZAIKO_INFO))]
    public interface IM_KAISHI_ZAIKO_INFODao : IS2Dao
    {

        [Sql("SELECT * FROM M_KAISHI_ZAIKO_INFO")]
        M_KAISHI_ZAIKO_INFO[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ZaikoHinmei.IM_KAISHI_ZAIKO_INFODao_GetAllValidData.sql")]
        M_KAISHI_ZAIKO_INFO[] GetAllValidData(M_KAISHI_ZAIKO_INFO data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KAISHI_ZAIKO_INFO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KAISHI_ZAIKO_INFO data);

        int Delete(M_KAISHI_ZAIKO_INFO data);

       
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KAISHI_ZAIKO_INFO data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/ AND ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/")]
        M_KAISHI_ZAIKO_INFO GetDataByCd(string gyoushaCd, string genbaCd, string zaikoHinmeiCd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KAISHI_ZAIKO_INFO data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
