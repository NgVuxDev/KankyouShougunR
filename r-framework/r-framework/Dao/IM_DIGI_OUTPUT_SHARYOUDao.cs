using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 車輌出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_SHARYOU))]
    public interface IM_DIGI_OUTPUT_SHARYOUDao : IS2Dao
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
        [Query("SELECT COUNT(DIGI_SHARYOU_CD) FROM M_DIGI_OUTPUT_SHARYOU WHERE (GYOUSHA_CD <> /*cdpk1*/ OR SHARYOU_CD <> /*cdpk2*/) AND DIGI_SHARYOU_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk1, string cdpk2, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(SHARYOU_CD) FROM M_DIGI_OUTPUT_SHARYOU WHERE GYOUSHA_CD = /*cd1*/ AND SHARYOU_CD = /*cd2*/")]
        int GetDataByCd(string cd1, string cd2);

        /// <summary>
        /// デジタコ連携車輛の全データを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_DIGI_OUTPUT_SHARYOU")]
        M_DIGI_OUTPUT_SHARYOU[] GetAllData();
    }
}