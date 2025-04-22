using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_HAIKI_KBN))]
    public interface IM_HAIKI_KBNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_HAIKI_KBN")]
        M_HAIKI_KBN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.HaikiKbn.IM_HAIKI_KBNDao_GetAllValidData.sql")]
        M_HAIKI_KBN[] GetAllValidData(M_HAIKI_KBN data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.HaikiKbn.IM_HAIKI_KBNDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_HAIKI_KBN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HAIKI_KBN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HAIKI_KBN data);

        int Delete(M_HAIKI_KBN data);

        [Sql("select right('00' + convert(varchar, M_HAIKI_KBN.HAIKI_KBN_CD), 2) AS CD,M_HAIKI_KBN.HAIKI_KBN_NAME_RYAKU AS NAME FROM M_HAIKI_KBN /*$whereSql*/ group by M_HAIKI_KBN.HAIKI_KBN_CD,M_HAIKI_KBN.HAIKI_KBN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] HAIKI_KBN_CD);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HAIKI_KBN data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("HAIKI_KBN_CD = /*cd*/")]
        M_HAIKI_KBN GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_HAIKI_KBN data, bool deletechuFlg);
    }
}
