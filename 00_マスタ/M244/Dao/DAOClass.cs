using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Master.ZaikoHinmeiHoshu.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.ZaikoHinmeiHoshu.DAO
{
    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface IMZAIKOHINMEIDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HINMEI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ZAIKO_HINMEI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_ZAIKO_HINMEI data);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_ZAIKO_HINMEI")]
        M_ZAIKO_HINMEI[] GetAllData();

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/")]
        M_ZAIKO_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaikoHinmeiHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranData(DTOClass data, bool deletechuFlg);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="data">在庫品名CDのリスト</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaikoHinmeiHoshu.Sql.CheckDeleteZaikoHinmeiSql.sql")]
        DataTable GetDataBySqlFileCheck(string[] ZAIKO_HINMEI_CD);
    }

    /// <summary>
    ///
    /// </summary>
    [Bean(typeof(M_ZAIKO_HIRITSU))]
    public interface IMZAIKOHIRITSUDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HIRITSU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ZAIKO_HIRITSU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_ZAIKO_HIRITSU data);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_ZAIKO_HIRITSU")]
        M_ZAIKO_HIRITSU[] GetAllData();

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/ AND DELETE_FLG = 0")]
        M_ZAIKO_HIRITSU[] GetDataByZaikoHimeiCd(string cd);
    }
}