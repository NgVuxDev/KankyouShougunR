using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 入出金区分マスタdao
    /// </summary>
    [Bean(typeof(M_NYUUSHUKKIN_KBN))]
    public interface IM_NYUUSHUKKIN_KBNDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_NYUUSHUKKIN_KBN")]
        M_NYUUSHUKKIN_KBN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.NyuushukkinKbn.IM_NYUUSHUKKIN_KBNDao_GetAllValidData.sql")]
        M_NYUUSHUKKIN_KBN[] GetAllValidData(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// コードを元に入出金区分情報を取得する
        /// </summary>
        /// <parameparam name="cd">入出金区分コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("NYUUSHUKKIN_KBN_CD = /*cd*/")]
        M_NYUUSHUKKIN_KBN GetDataByCd(int cd);

        [Sql("select right('00' + convert(varchar, M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD), 2) AS CD,M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME AS NAME FROM M_NYUUSHUKKIN_KBN /*$whereSql*/ group by M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD,M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// PKキー配列の検索条件による他データ使用有無判定用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="NYUUSHUKKIN_KBN_CD">入出金区分CDリスト</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] NYUUSHUKKIN_KBN_CD);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NYUUSHUKKIN_KBN data, bool deletechuFlg);
    }
}