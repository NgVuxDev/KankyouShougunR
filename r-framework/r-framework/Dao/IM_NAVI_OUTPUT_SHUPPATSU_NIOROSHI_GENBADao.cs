using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 車輌出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA))]
    public interface IM_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBADao : IS2Dao
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
        [Query("SELECT COUNT(NAVI_EIGYOUSHO_CD) FROM M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA WHERE (GYOUSHA_CD <> /*cdpk1*/ OR GENBA_CD <> /*cdpk2*/) AND NAVI_EIGYOUSHO_CD = /*cd*/ AND OUTPUT_DATE IS NOT NULL AND JYOGAI_FLG = 0")]
        int GetDuplicationCountByCd(string cdpk1, string cdpk2, string cd);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT COUNT(GENBA_CD) FROM M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA WHERE GYOUSHA_CD = /*cd1*/ AND GENBA_CD = /*cd2*/")]
        int GetDataByCd(string cd1, string cd2);

        /// <summary>
        /// コードからデータを取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT NAVI_EIGYOUSHO_CD FROM M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA WHERE GYOUSHA_CD = /*cd1*/ AND GENBA_CD = /*cd2*/")]
        string GetStringDataByCd(string cd1, string cd2);

        /// <summary>
        /// コードから業者名を取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT DISTINCT G.GYOUSHA_NAME_RYAKU FROM M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS N INNER JOIN M_GYOUSHA G ON N.GYOUSHA_CD = G.GYOUSHA_CD WHERE N.OUTPUT_DATE IS NOT NULL AND N.JYOGAI_FLG = 0 AND N.GYOUSHA_CD = /*cd1*/")]
        string GetStringDataByGyoushaName(string cd1);

        /// <summary>
        /// コードから現場名を取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [Query("SELECT DISTINCT G.GENBA_NAME_RYAKU FROM M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA AS N INNER JOIN M_GENBA G ON N.GYOUSHA_CD = G.GYOUSHA_CD AND N.GENBA_CD = G.GENBA_CD WHERE N.OUTPUT_DATE IS NOT NULL AND N.JYOGAI_FLG = 0 AND N.GYOUSHA_CD = /*cd1*/ AND N.GENBA_CD = /*cd2*/")]
        string GetStringDataByGenbaName(string cd1, string cd2);

        /// <summary>
        /// デジタコ連携車輛の全データを取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA")]
        M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA[] GetAllData();
    }
}