using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 単位出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_UNIT))]
    public interface IM_DIGI_OUTPUT_UNITDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_UNIT_CD) FROM M_DIGI_OUTPUT_UNIT WHERE UNIT_CD <> /*cdpk*/ AND DIGI_UNIT_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(UNIT_CD) FROM M_DIGI_OUTPUT_UNIT WHERE UNIT_CD = /*cd*/")]
        int GetDataByCd(string cd);

        /// <summary>
        /// デジタコ連携単位の全データを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DIGI_OUTPUT_UNIT")]
        M_DIGI_OUTPUT_UNIT[] GetAllData();
    }
}