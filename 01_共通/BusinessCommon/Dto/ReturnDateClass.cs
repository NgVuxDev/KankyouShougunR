using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// Dtoクラス・コントロール
    /// </summary>
    public class ReturnDate
    {
        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime dtDATE { get; set; }

        /// <summary>
        /// 取引先コード
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }
    }
}
