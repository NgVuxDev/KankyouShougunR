using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 営業担当者マスタのDaoクラス
    /// </summary>
    [Bean(typeof(M_EIGYOU_TANTOUSHA))]
    public interface IM_EIGYOU_TANTOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_EIGYOU_TANTOUSHA")]
        M_EIGYOU_TANTOUSHA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.EigyouTantousha.IM_EIGYOU_TANTOUSHADao_GetAllValidData.sql")]
        M_EIGYOU_TANTOUSHA[] GetAllValidData(M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_EIGYOU_TANTOUSHA data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_EIGYOU_TANTOUSHA GetDataByCd(string cd);

        [SqlFile("r-framework.Dao.SqlFile.EigyouTantousha.IM_EIGYOU_TANTOUSHADao_GetAllMasterDataForPopup.sql")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_EIGYOU_TANTOUSHA data, bool deletechuFlg);
    }
}