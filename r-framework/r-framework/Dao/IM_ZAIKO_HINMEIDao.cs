using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface IM_ZAIKO_HINMEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ZAIKO_HINMEI")]
        M_ZAIKO_HINMEI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ZaikoHinmei.IM_ZAIKO_HINMEIDao_GetAllValidData.sql")]
        M_ZAIKO_HINMEI[] GetAllValidData(M_ZAIKO_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ZAIKO_HINMEI data);

        int Delete(M_ZAIKO_HINMEI data);

        [Sql("select M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD, M_ZAIKO_HINMEI.ZAIKO_HINMEI_NAME_RYAKU, M_ZAIKO_HINMEI.ZAIKO_TANKA FROM M_ZAIKO_HINMEI /*$whereSql*/ group by M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD, M_ZAIKO_HINMEI.ZAIKO_HINMEI_NAME_RYAKU, M_ZAIKO_HINMEI.ZAIKO_TANKA")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ZAIKO_HINMEI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/")]
        M_ZAIKO_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_ZAIKO_HINMEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
