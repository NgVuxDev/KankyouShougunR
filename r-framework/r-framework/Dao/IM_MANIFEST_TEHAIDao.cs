using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_MANIFEST_TEHAI))]
    public interface IM_MANIFEST_TEHAIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_MANIFEST_TEHAI")]
        M_MANIFEST_TEHAI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestTehai.IM_MANIFEST_TEHAIDao_GetAllValidData.sql")]
        M_MANIFEST_TEHAI[] GetAllValidData(M_MANIFEST_TEHAI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MANIFEST_TEHAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MANIFEST_TEHAI data);

        int Delete(M_MANIFEST_TEHAI data);

        [Sql("select right('00' + convert(varchar, M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD), 2) AS CD,M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU AS NAME FROM M_MANIFEST_TEHAI /*$whereSql*/ group by M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD,M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_TEHAI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("MANIFEST_TEHAI_CD = /*cd*/")]
        M_MANIFEST_TEHAI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_MANIFEST_TEHAI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
