using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件 : 作業日
        /// </summary>
        public String SagyouDate { get; set; }
        /// <summary>
        /// 検索条件 : 作業日From
        /// </summary>
        public String SagyouDateFrom { get; set; }
        /// <summary>
        /// 検索条件 : 作業日To
        /// </summary>
        public String SagyouDateTo { get; set; }
        /// <summary>
        /// 検索条件 : 抽出設定
        /// </summary>
        public int ChusyutsuSettei { get; set; }
        /// <summary>
        /// 検索条件 : 拠点
        /// </summary>
        public String KyotenCd { get; set; }

        
        /// <summary>
        /// 検索条件  :SYSTEM_ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 検索条件 : SEQ
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 検索条件 : 明細システムID
        /// </summary>
        public long DetailSystemId { get; set; }
        /// <summary>
        /// 検索条件 : 定期配車番号
        /// </summary>
        public String TeikiHaishaNumber { get; set; }
        /// <summary>
        /// 検索条件 : 曜日CD
        /// </summary>
        public int DayCd { get; set; }
        /// <summary>
        /// 検索条件 : コース名称CD
        /// </summary>
        public String CourseNameCd { get; set; }
        /// <summary>
        /// 検索条件 : レコードID
        /// </summary>
        public int RecId { get; set; }
        /// <summary>
        /// 検索条件 : 受付番号
        /// </summary>
        public long UketsukeNumber { get; set; }
    }
}
