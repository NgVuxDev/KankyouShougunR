using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Impl
{
    class SharyouMasterAccess : Base.AbstractMasterAcess
        <Dao.IM_GYOUSHADao, Entity.M_GYOUSHA,
         Dao.IM_SHARYOUDao, Entity.M_SHARYOU>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SharyouMasterAccess(CustomControl.ICustomControl control, object[] obj, object[] sendParam)
            : base(control, obj, sendParam,
                   "GYOUSHA_CD", "業者CD", "SHARYOU_CD", "車輌CD", "GYOUSHA_NAME")
        {
        }

    }

}
