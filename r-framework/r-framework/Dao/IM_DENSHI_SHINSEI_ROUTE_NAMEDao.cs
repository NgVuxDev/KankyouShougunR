using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 電子申請経路名Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_SHINSEI_ROUTE_NAME))]
    public interface IM_DENSHI_SHINSEI_ROUTE_NAMEDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_DENSHI_SHINSEI_ROUTE_NAME data);

        [Sql("select M_DENSHI_SHINSEI_ROUTE_NAME.DENSHI_SHINSEI_ROUTE_CD AS CD,M_DENSHI_SHINSEI_ROUTE_NAME.DENSHI_SHINSEI_ROUTE_NAME AS NAME FROM M_DENSHI_SHINSEI_ROUTE_NAME /*$whereSql*/ group by M_DENSHI_SHINSEI_ROUTE_NAME.DENSHI_SHINSEI_ROUTE_CD, M_DENSHI_SHINSEI_ROUTE_NAME.DENSHI_SHINSEI_ROUTE_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_SHINSEI_ROUTE_NAME")]
        M_DENSHI_SHINSEI_ROUTE_NAME[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiShinseiRouteName.IM_DENSHI_SHINSEI_ROUTE_NAMEDao_GetAllValidData.sql")]
        M_DENSHI_SHINSEI_ROUTE_NAME[] GetAllValidData(M_DENSHI_SHINSEI_ROUTE_NAME data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
