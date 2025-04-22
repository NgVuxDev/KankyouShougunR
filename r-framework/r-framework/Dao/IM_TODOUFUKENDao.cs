using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 都道府県マスタDao
    /// </summary>
    [Bean(typeof(M_TODOUFUKEN))]
    public interface IM_TODOUFUKENDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_TODOUFUKEN")]
        M_TODOUFUKEN[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Todoufuken.IM_TODOUFUKENDao_GetAllValidData.sql")]
        M_TODOUFUKEN[] GetAllValidData(M_TODOUFUKEN data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TODOUFUKEN data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TODOUFUKEN data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_TODOUFUKEN data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] TODOUFUKEN_CD);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TODOUFUKEN data);

        [Sql("select right('00' + convert(varchar, M_TODOUFUKEN.TODOUFUKEN_CD), 2) AS CD,M_TODOUFUKEN.TODOUFUKEN_NAME AS NAME FROM M_TODOUFUKEN /*$whereSql*/ group by M_TODOUFUKEN.TODOUFUKEN_CD,M_TODOUFUKEN.TODOUFUKEN_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TODOUFUKEN_CD = /*cd*/")]
        M_TODOUFUKEN GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_TODOUFUKEN data, bool deletechuFlg);
    }
}