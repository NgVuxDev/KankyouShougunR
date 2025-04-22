using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 種類出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHURUI))]
    public interface IM_DIGI_OUTPUT_SHURUIDao : IS2Dao
    {
        /// <summary>
        /// SQL構文からデータの更新を行う
        /// </summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);

        /// <summary>
        /// 重複チェック用
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(DIGI_SHURUI_CD) FROM M_DIGI_OUTPUT_SHURUI WHERE SHURUI_CD <> /*cdpk*/ AND DIGI_SHURUI_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHURUI_CD) FROM M_DIGI_OUTPUT_SHURUI WHERE SHURUI_CD = /*cd*/")]
        int GetDataByCd(string cd);
    }
}