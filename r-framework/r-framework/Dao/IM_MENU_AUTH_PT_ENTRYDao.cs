// $Id: IM_MENU_AUTH_PT_ENTRYDao.cs 36241 2014-12-01 08:03:28Z sanbongi $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;

namespace r_framework.Dao
{
    /// <summary>
    /// メニュー権限パターンDao
    /// </summary>
    [Bean(typeof(M_MENU_AUTH_PT_ENTRY))]
    public interface IM_MENU_AUTH_PT_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MENU_AUTH_PT_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MENU_AUTH_PT_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_MENU_AUTH_PT_ENTRY data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_MENU_AUTH_PT_ENTRY")]
        M_MENU_AUTH_PT_ENTRY[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.MenuAuthPtEntry.IM_MENU_AUTH_PT_ENTRYDao_GetAllValidData.sql")]
        M_MENU_AUTH_PT_ENTRY[] GetAllValidData(M_MENU_AUTH_PT_ENTRY data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// パターンIDの最大値を取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(PATTERN_ID), 0) FROM M_MENU_AUTH_PT_DETAIL")]
        Int64 GetMaxPatternID();
    }
}
