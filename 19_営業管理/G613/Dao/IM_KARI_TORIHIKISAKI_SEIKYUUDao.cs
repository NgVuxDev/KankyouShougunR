
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GyoushaKakunin.Dao
{
    [Bean(typeof(M_KARI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_KARI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        /// <summary>
        /// 取引先CDコードをもとに取引先_請求情報マスタのデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);
    }
}
