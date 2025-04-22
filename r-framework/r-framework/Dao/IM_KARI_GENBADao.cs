using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// 仮現場マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA))]
    public interface IM_KARI_GENBADao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_GENBA data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_GENBA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_GENBA data);

        /// <summary>
        /// 業者、現場コードを元に削除されていない情報を取得
        /// </summary>
        /// <parameparam name="genbaCd">現場コード</parameparam>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/ AND DELETE_FLG = 0")]
        M_KARI_GENBA GetKariGenbaData(string gyoushaCd, string genbaCd);

        List<M_KARI_GENBA> GetKariGenbaList(M_KARI_GENBA entity);
    }
}