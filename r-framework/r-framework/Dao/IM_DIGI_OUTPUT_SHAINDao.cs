using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 社員出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHAIN))]
    public interface IM_DIGI_OUTPUT_SHAINDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_SHAIN_CD) FROM M_DIGI_OUTPUT_SHAIN WHERE SHAIN_CD <> /*cdpk*/ AND DIGI_SHAIN_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHAIN_CD) FROM M_DIGI_OUTPUT_SHAIN WHERE SHAIN_CD = /*cd*/")]
        int GetDataByCd(string cd);
    }
}