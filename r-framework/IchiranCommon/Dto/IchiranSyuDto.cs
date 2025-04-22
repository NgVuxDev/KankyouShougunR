using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.IchiranCommon.Dto
{
    //一覧出力項目
    public class MOPDtoCls
    {
        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件  :枝番
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 検索条件  :伝種区分CD
        /// </summary>
        public String DENSHU_KBN_CD { get; set; }

        /// <summary>
        /// 検索条件  :出力区分
        /// </summary>
        public String OUTPUT_KBN { get; set; }

        /// <summary>
        /// 検索条件  :削除フラグ
        /// </summary>
        public String DELETE_FLG { get; set; }

        /// <summary>
        /// Is user Inxs SubApplication
        /// </summary>
        public bool IS_USE_INXS { get; set; }
    }

    //一覧出力項目詳細
    public class MOPCDtoCls
    {
        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件  :枝番
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 検索条件  :明細システムID
        /// </summary>
        public String DETAIL_SYSTEM_ID { get; set; }
    }
}
