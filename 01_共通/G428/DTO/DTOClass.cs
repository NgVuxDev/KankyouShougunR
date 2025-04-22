using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.DTO
{
    public class DTOClass
    {
        /// <summary>
        /// 伝票番号
        /// </summary>
        public long DenpyouNumber { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public long SystemID { get; set; }

        /// <summary>
        /// 明細システムID
        /// </summary>
        public long DetailSystemID { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        public int SEQ { get; set; }
    }
}
