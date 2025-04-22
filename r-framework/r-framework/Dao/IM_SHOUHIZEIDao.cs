using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;

namespace r_framework.Dao
{
    /// <summary>
    /// 消費税マスタDao
    /// </summary>
    [Bean(typeof(M_SHOUHIZEI))]
    public interface IM_SHOUHIZEIDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_SHOUHIZEI")]
        M_SHOUHIZEI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shouhizei.IM_SHOUHIZEIDao_GetAllValidData.sql")]
        M_SHOUHIZEI[] GetAllValidData(M_SHOUHIZEI data);

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHOUHIZEI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHOUHIZEI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SHOUHIZEI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHOUHIZEI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SYS_ID = /*cd*/")]
        M_SHOUHIZEI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHOUHIZEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// システムIDの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),0)+1 FROM M_SHOUHIZEI where ISNUMERIC(SYS_ID) = 1")]
        short GetMaxPlusKey();

        /// <summary>
        /// システムIDの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),1) FROM M_SHOUHIZEI where ISNUMERIC(SYS_ID) = 1")]
        short GetMaxKey();


        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="date">DateTime</param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Shouhizei.IT_IM_SHOUHIZEIDao_GetDataBySqlFile.sql")]
        M_SHOUHIZEI GetDataByDate(DateTime date);

    }
}