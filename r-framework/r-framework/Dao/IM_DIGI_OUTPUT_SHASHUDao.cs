using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 車種出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHASHU))]
    public interface IM_DIGI_OUTPUT_SHASHUDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_SHASHU_CD) FROM M_DIGI_OUTPUT_SHASHU WHERE SHASHU_CD <> /*cdpk*/ AND DIGI_SHASHU_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHASHU_CD) FROM M_DIGI_OUTPUT_SHASHU WHERE SHASHU_CD = /*cd*/")]
        int GetDataByCd(string cd);
        
    }
}