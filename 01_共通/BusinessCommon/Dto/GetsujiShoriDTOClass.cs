using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    public class GetsujiShoriDTOClass
    {
        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /*
        /// <summary>
        /// 年
        /// </summary>
        public short YEAR { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public short MONTH { get; set; }
        */

        /// <summary>
        /// 月次処理対象開始年月日
        /// </summary>
        public string FROM_DATE { get; set; }

        /// <summary>
        /// 月次処理対象終了年月日
        /// </summary>
        public string TO_DATE { get; set; }

        /// <summary>
        /// 繰越残高
        /// </summary>
        public decimal PREVIOUS_MONTH_BALANCE { get; set; }
    }
}
