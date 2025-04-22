using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.Sharyoukyuudounyuryoku
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件 : DBフィールド名
        /// </summary>
        public String FieldName { get; set; }
        /// <summary>
        /// 検索条件 : 条件値
        /// </summary>
        public String ConditionValue { get; set; }
    }

    public class SearchDTOClass : SuperEntity
    {
        /// <summary>
        /// 検索条件 : 車輌CD
        /// </summary>
        public String SHARYOU_CD { get; set; }
        /// <summary>
        /// 検索条件 : 車輌名
        /// </summary>
        public String SHARYOU_NAME_RYAKU { get; set; }
        /// <summary>
        /// 検索条件 : 車種CD
        /// </summary>
        public String SHASYU_CD { get; set; }
        /// <summary>
        /// 検索条件 : 車種名
        /// </summary>
        public String SHASHU_NAME_RYAKU { get; set; }
        /// <summary>
        /// 検索条件 : 業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 業者名
        /// </summary>
        public String GYOUSHA_NAME_RYAKU { get; set; }
    }



}
