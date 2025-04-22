// $Id: DTOClass.cs 6905 2013-11-14 02:43:35Z sys_dev_19 $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Reception.UketsukeMochikomiNyuuryoku
{
    public class DTOClass
    {
        /// <summary>
        /// 受付番号
        /// </summary>
        public long UketsukeNumber { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public long SystemID { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        public int SEQ { get; set; }
    }
}
