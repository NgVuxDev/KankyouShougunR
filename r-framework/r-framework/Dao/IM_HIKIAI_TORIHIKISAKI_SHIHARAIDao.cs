using System.Collections.Generic;
using Seasar.Dao.Attrs;
using r_framework.Entity;

namespace r_framework.Dao
{
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_HIKIAI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {
        List<M_HIKIAI_TORIHIKISAKI_SHIHARAI> GetHikiaiTorihikisakiShiharaiList(M_HIKIAI_TORIHIKISAKI_SHIHARAI entity);
    }
}
