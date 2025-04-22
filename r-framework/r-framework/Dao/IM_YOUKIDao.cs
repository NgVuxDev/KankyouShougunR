using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_YOUKI))]
    public interface IM_YOUKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_YOUKI")]
        M_YOUKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Youki.IM_YOUKIDao_GetAllValidData.sql")]
        M_YOUKI[] GetAllValidData(M_YOUKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_YOUKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_YOUKI data);

        int Delete(M_YOUKI data);

        [Sql("select M_YOUKI.YOUKI_CD AS CD,M_YOUKI.YOUKI_NAME_RYAKU AS NAME,M_YOUKI.YOUKI_JYURYO FROM M_YOUKI /*$whereSql*/ group by M_YOUKI.YOUKI_CD,M_YOUKI.YOUKI_NAME_RYAKU,M_YOUKI.YOUKI_JYURYO")]
        DataTable GetAllMasterDataForPopup(string whereSql);


        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] YOUKI_CD);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_YOUKI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("YOUKI_CD = /*cd*/")]
        M_YOUKI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_YOUKI data, bool deletechuFlg);
    }
}
