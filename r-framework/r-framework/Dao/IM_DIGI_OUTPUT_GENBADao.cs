using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 現場出力済みマスタDao
    /// </summary>
    [Bean(typeof(M_DIGI_OUTPUT_GENBA))]
    public interface IM_DIGI_OUTPUT_GENBADao : IS2Dao
    {
        /// <summary>
        /// 業者CD、現場CDをもとにデータを取得する
        /// </summary>
        /// <param name="gyoushaCD">業者CD</param>
        /// <param name="genbaCD">現場CD</param>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*gyoushaCD*/ and GENBA_CD = /*genbaCD*/")]
        M_DIGI_OUTPUT_GENBA GetDataByCd(string gyoushaCD, string genbaCD);

        /// <summary>
        /// SQL構文からデータの更新を行う
        /// </summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        int ExecuteForStringSql(string sql);
    }
}
