using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_TORIHIKI_KBN))]
    public interface IM_TORIHIKI_KBNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_TORIHIKI_KBN")]
        M_TORIHIKI_KBN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.TorihikiKbn.IM_TORIHIKI_KBNDao_GetAllValidData.sql")]
        M_TORIHIKI_KBN[] GetAllValidData(M_TORIHIKI_KBN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKI_KBN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKI_KBN data);

        int Delete(M_TORIHIKI_KBN data);

        [Sql("select M_TORIHIKI_KBN.TORIHIKI_KBN_CD AS CD,M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU AS NAME FROM M_TORIHIKI_KBN /*$whereSql*/ group by M_TORIHIKI_KBN.TORIHIKI_KBN_CD,M_TORIHIKI_KBN.TORIHIKI_KBN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TORIHIKI_KBN data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKI_KBN_CD = /*cd*/")]
        M_TORIHIKI_KBN GetDataByCd(short cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_TORIHIKI_KBN data, bool deletechuFlg);
    }
}
