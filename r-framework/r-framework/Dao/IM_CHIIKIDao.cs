using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKI))]
    public interface IM_CHIIKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CHIIKI")]
        M_CHIIKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Chiiki.IM_CHIIKIDao_GetAllValidData.sql")]
        M_CHIIKI[] GetAllValidData(M_CHIIKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKI data);

        int Delete(M_CHIIKI data);

        [Sql("select M_CHIIKI.CHIIKI_CD AS CD,M_CHIIKI.CHIIKI_NAME_RYAKU AS NAME FROM M_CHIIKI /*$whereSql*/ group by M_CHIIKI.CHIIKI_CD,M_CHIIKI.CHIIKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] CHIIKI_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("CHIIKI_CD = /*cd*/")]
        M_CHIIKI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKI data, bool deletechuFlg);
    }
}
