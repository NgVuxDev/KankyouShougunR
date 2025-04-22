using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    /// <summary>
    /// 表示レコード
    /// </summary>
    [Serializable()]
    internal class DisplayDataRow
    {
        /// <summary>
        /// 前回残高
        /// </summary>
        public decimal ZenkaiZandaka { get; set; }
        /// <summary>
        /// 今回金額
        /// </summary>
        public decimal KonkaiKingaku { get; set; }
        /// <summary>
        /// 今回税額
        /// </summary>
        public decimal KonkaiZeigaku { get; set; }
        /// <summary>
        /// 今回取引
        /// </summary>
        public decimal KonkaiTorihiki { get; set; }
        /// <summary>
        /// 差引残高
        /// </summary>
        public decimal SasihikiZandaka { get; set; }
    }
}
