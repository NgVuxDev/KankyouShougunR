using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 現場出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_NAVI_OUTPUT_GENBA))]
    public interface IM_NAVI_OUTPUT_GENBADao : IS2Dao
    {
        /// <summary>
        /// SQL構文からデータの更新を行う
        /// </summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(GENBA_CD) FROM M_NAVI_OUTPUT_GENBA WHERE GYOUSHA_CD = /*cd1*/ AND GENBA_CD = /*cd2*/")]
        int GetDataByCd(string cd1, string cd2);

        /// <summary>
        /// デジタコ連携車輛の全データを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_NAVI_OUTPUT_GENBA")]
        M_NAVI_OUTPUT_GENBA[] GetAllData();
    }
}