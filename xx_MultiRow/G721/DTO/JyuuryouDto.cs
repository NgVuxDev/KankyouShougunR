using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku.DTO
{
    /// <summary>
    /// 重量値用Dto
    /// Detailの行と重量値の行と関連性が無いため
    /// このDtoを利用し重量値計算を実施する
    /// </summary>
    internal class JyuuryouDto
    {
        /// <summary>
        /// ローカルで使用する行番号
        /// ≠ROW_NO
        /// </summary>
        internal short no { get; set; }

        /// <summary>
        /// 総重量
        /// </summary>
        internal float stackJyuuryou { get; set; }

        /// <summary>
        /// 空車重量
        /// </summary>
        internal float emptyJyuuryou { get; set; }

        /// <summary>
        /// 割振重量
        /// </summary>
        internal float warifuriJyuuryou { get; set; }

        /// <summary>
        /// 調整重量
        /// </summary>
        internal float chouseiJyuuryou { get; set; }
    }
}
