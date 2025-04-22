using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 品名出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_HINMEI))]
    public interface IM_DIGI_OUTPUT_HINMEIDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_HINMEI_CD) FROM M_DIGI_OUTPUT_HINMEI WHERE HINMEI_CD <> /*cdpk*/ AND DIGI_HINMEI_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(HINMEI_CD) FROM M_DIGI_OUTPUT_HINMEI WHERE HINMEI_CD = /*cd*/")]
        int GetDataByCd(string cd);

        /// <summary>
        /// デジタコ連携品名の全データを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DIGI_OUTPUT_HINMEI")]
        M_DIGI_OUTPUT_HINMEI[] GetAllData();
    }
}