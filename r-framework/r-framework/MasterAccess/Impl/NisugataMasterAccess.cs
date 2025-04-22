using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Impl
{
    class NisugataMasterAccess : Base.AbstractMasterAcess
        <Dao.IM_NISUGATADao, Entity.M_NISUGATA>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NisugataMasterAccess(CustomControl.ICustomControl control, object[] obj, object[] sendParam)
            : base(control, obj, sendParam,
                   "NISUGATA_CD", "荷姿CD", "NISUGATA_NAME")
        {
        }



    }

}
