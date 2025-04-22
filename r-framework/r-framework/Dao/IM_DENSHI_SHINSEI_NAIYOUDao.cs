using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 電子申請内容名Dao
    /// </summary>
    [Bean(typeof(M_DENSHI_SHINSEI_NAIYOU))]
    public interface IM_DENSHI_SHINSEI_NAIYOUDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_SHINSEI_NAIYOU data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_SHINSEI_NAIYOU data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_DENSHI_SHINSEI_NAIYOU data);

        [Sql("select M_DENSHI_SHINSEI_NAIYOU.NAIYOU_CD AS CD,M_DENSHI_SHINSEI_NAIYOU.NAIYOU_NAME AS NAME FROM M_DENSHI_SHINSEI_NAIYOU /*$whereSql*/ group by M_DENSHI_SHINSEI_NAIYOU.NAIYOU_CD, M_DENSHI_SHINSEI_NAIYOU.NAIYOU_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_DENSHI_SHINSEI_NAIYOU")]
        M_DENSHI_SHINSEI_NAIYOU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiShinseiNaiyou.IM_DENSHI_SHINSEI_NAIYOUDao_GetAllValidData.sql")]
        M_DENSHI_SHINSEI_NAIYOU[] GetAllValidData(M_DENSHI_SHINSEI_NAIYOU data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("NAIYOU_CD = /*cd*/")]
        M_DENSHI_SHINSEI_NAIYOU GetDataByCd(string cd);
    }
}
