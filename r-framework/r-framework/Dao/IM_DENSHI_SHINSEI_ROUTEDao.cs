// $Id: IM_DENSHI_SHINSEI_ROUTEDao.cs 27927 2014-08-19 06:27:28Z j-kikuchi $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 電子申請経路Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_SHINSEI_ROUTE))]
    public interface IM_DENSHI_SHINSEI_ROUTEDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_SHINSEI_ROUTE data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_SHINSEI_ROUTE data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_DENSHI_SHINSEI_ROUTE data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_SHINSEI_ROUTE")]
        M_DENSHI_SHINSEI_ROUTE[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiShinseiRoute.IM_DENSHI_SHINSEI_ROUTEDao_GetAllValidData.sql")]
        M_DENSHI_SHINSEI_ROUTE[] GetAllValidData(M_DENSHI_SHINSEI_ROUTE data);

        /// <summary>
        /// 電子申請経路コードを元に経路情報を取得
        /// </summary>
        /// <param name="data"></param>
        [Query("DENSHI_SHINSEI_ROUTE_CD = /*data.DENSHI_SHINSEI_ROUTE_CD.Value*/ and DENSHI_SHINSEI_ROW_NO = /*data.DENSHI_SHINSEI_ROW_NO.Value*/")]
        M_DENSHI_SHINSEI_ROUTE GetDataByCd(M_DENSHI_SHINSEI_ROUTE data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
