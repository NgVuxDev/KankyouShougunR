using Seasar.Dao.Attrs;
using r_framework.Entity;
using System.Collections.Generic;
namespace r_framework.Dao
{
    [Bean(typeof(M_KARI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_KARI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_TORIHIKISAKI_SHIHARAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_TORIHIKISAKI_SHIHARAI data);

        int Delete(M_KARI_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        ///取引先コードをもとに仮取引先_支払情報マスタのデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);

        List<M_KARI_TORIHIKISAKI_SHIHARAI> GetKariTorihikisakiShiharaiList(M_KARI_TORIHIKISAKI_SHIHARAI entity);
    }
}
