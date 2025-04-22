using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 車種出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_NAVI_OUTPUT_SHASHU))]
    public interface IM_NAVI_OUTPUT_SHASHUDao : IS2Dao
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
        [Query("SELECT COUNT(SHASHU_CD) FROM M_NAVI_OUTPUT_SHASHU WHERE SHASHU_CD = /*cd*/")]
        int GetDataByCd(string cd);
    }
}