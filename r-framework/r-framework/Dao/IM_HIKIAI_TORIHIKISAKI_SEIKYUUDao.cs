using System.Collections.Generic;
using Seasar.Dao.Attrs;
using r_framework.Entity;

namespace r_framework.Dao
{
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        List<M_HIKIAI_TORIHIKISAKI_SEIKYUU> GetHikiaiTorihikisakiSeikyuuList(M_HIKIAI_TORIHIKISAKI_SEIKYUU entity);
    }
}
