using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity
{
    /// <summary>
    /// 挿入データ
    /// </summary>
    [Serializable()]
    class DB_INSERT_DATA
    {
        public int ORDER_NO { get; set; }
        public T_UR_SH_ENTRY ENTRY { get; set; }
        public List<T_UR_SH_DETAIL> DetailList { get; set; }

    }
}
