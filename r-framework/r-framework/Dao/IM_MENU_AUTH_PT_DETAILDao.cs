// $Id: IM_MENU_AUTH_PT_DETAILDao.cs 36844 2014-12-09 06:46:55Z sanbongi $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// メニュー権限パターン詳細Dao
    /// </summary>
    [Bean(typeof(M_MENU_AUTH_PT_DETAIL))]
    public interface IM_MENU_AUTH_PT_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MENU_AUTH_PT_DETAIL data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_MENU_AUTH_PT_DETAIL data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_MENU_AUTH_PT_DETAIL data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_MENU_AUTH_PT_DETAIL")]
        M_MENU_AUTH_PT_DETAIL[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.MenuAuthPtDetail.IM_MENU_AUTH_PT_DETAILDao_GetAllValidData.sql")]
        M_MENU_AUTH_PT_DETAIL[] GetAllValidData(M_MENU_AUTH_PT_DETAIL data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
