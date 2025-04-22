using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.CourseNyuryoku
{
    public class M_COURSE_DETAIL_ITEMS_add_DELETE_FLG : M_COURSE_DETAIL_ITEMS
    {
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int DELETE_FLG { get; set; }
    }
    /// <summary>
    /// 現場取得区分検索
    /// </summary>
    public class GetGenbaDtoCls
    {
        /// <summary>検索条件  :業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>検索条件  :現場CD</summary>
        public string GENBA_CD { get; set; }
    }
}
