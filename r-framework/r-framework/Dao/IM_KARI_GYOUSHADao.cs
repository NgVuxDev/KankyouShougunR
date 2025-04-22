using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// 仮業者マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_GYOUSHA))]
    public interface IM_KARI_GYOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_GYOUSHA data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_GYOUSHA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_GYOUSHA data);

        /// <summary>
        /// コードを元に業者データを取得する
        /// </summary>
        /// <parameparam name="cd">業者コード</parameparam>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*cd*/ AND DELETE_FLG = 0")]
        M_KARI_GYOUSHA GetDataByCd(string cd);

        List<M_KARI_GYOUSHA> GetKariGyoushaList(M_KARI_GYOUSHA entity);
    }
}