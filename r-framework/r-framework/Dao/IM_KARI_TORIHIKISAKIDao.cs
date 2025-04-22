using Seasar.Dao.Attrs;
using r_framework.Entity;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// 仮取引先マスタDao
    /// </summary>
    [Bean(typeof(M_KARI_TORIHIKISAKI))]
    public interface IM_KARI_TORIHIKISAKIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードをもとに削除されていない仮取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/ AND DELETE_FLG = 0")]
        M_KARI_TORIHIKISAKI GetDataByCd(string cd);

        List<M_KARI_TORIHIKISAKI> GetKariTorihikisakiList(M_KARI_TORIHIKISAKI entity);
    }
}