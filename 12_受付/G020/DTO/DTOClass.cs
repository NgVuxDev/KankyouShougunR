// $Id: DTOClass.cs 7927 2013-11-22 06:57:47Z sys_dev_22 $

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
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
