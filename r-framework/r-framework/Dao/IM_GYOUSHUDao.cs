using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 業種マスタDao
    /// </summary>
    [Bean(typeof(M_GYOUSHU))]
    public interface IM_GYOUSHUDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GYOUSHU data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GYOUSHU data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GYOUSHU data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_GYOUSHU")]
        M_GYOUSHU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyoushu.IM_GYOUSHUDao_GetAllValidData.sql")]
        M_GYOUSHU[] GetAllValidData(M_GYOUSHU data);
        /// <summary>
        /// コードを元に業種データを取得する
        /// </summary>
        /// <parameparam name="cd">業種コード</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [Query("GYOUSHU_CD = /*cd*/")]
        M_GYOUSHU GetDataByCd(string cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GYOUSHU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">業種データ</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] GYOUSHU_CD);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GYOUSHU data, bool deletechuFlg);

        [Sql("select M_GYOUSHU.GYOUSHU_CD as CD,M_GYOUSHU.GYOUSHU_NAME_RYAKU as NAME FROM M_GYOUSHU /*$whereSql*/ group by M_GYOUSHU.GYOUSHU_CD,M_GYOUSHU.GYOUSHU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}