using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Adjustment.Shiharaishimesyori.DTO
{
    public class CheckErrorMessageDTOClass
    {
        /// <summary>
        /// 処理区分
        /// </summary>
        public Int16 SHORI_KBN { get; set; }

        /// <summary>
        /// 伝票種類CD
        /// </summary>
        public Int16 DENPYOU_SHURUI_CD { get; set; }

        /// <summary>
        /// チェック区分
        /// </summary>
        public Int16 CHECK_KBN { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public Int64 SYSTEM_ID { get; set; }

        /// <summary>
        /// 枝番
        /// </summary>
        public Int32 SEQ { get; set; }

        /// <summary>
        /// 明細システムID
        /// </summary>
        public Int64 DETAIL_SYSTEM_ID { get; set; }
    }
}
