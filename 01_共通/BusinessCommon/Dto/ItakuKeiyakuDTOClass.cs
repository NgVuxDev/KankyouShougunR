using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// Dtoクラス・コントロール
    /// </summary>
    public class ItakuKeiyakuDTO
    {
        /// <summary>
        /// 作業日
        /// </summary>
        public SqlDateTime SAGYOU_DATE { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }
        
        /// <summary>
        /// 品名CD
        /// </summary>
        public string HINMEI_CD { get; set; }

        /// <summary>
        /// 廃棄種類CD
        /// </summary>
        public string HAIKI_SHURUI_CD { get; set; }

        /// <summary>
        /// 廃棄区分CD
        /// </summary>
        public int HAIKI_KBN_CD { get; set; }
    }
}
