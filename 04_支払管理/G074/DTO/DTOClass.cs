using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo
{
    internal class DTOClass
    {
        /// <summary>
        /// 検索条件  :DenpyoHizukeFrom
        /// </summary>
        public String DenpyoHizukeFrom { get; set; }

        /// <summary>
        /// 検索条件  :DenpyoHizukeTo
        /// </summary>
        public String DenpyoHizukeTo { get; set; }
    }
}
