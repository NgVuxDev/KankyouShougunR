using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace ContenaPopup
{
    public class DTOCls
    {
        /// <summary>
        /// 検索条件  :GYOUSYA_CD
        /// </summary>
        public String Gyousya_Cd { get; set; }
        /// <summary>
        /// 検索条件  :GENNBA_CD
        /// </summary>
        public String Gennba_Cd { get; set; }
        /// <summary>
        /// 検索条件  :PARENT_CONDITION_ITEM
        /// </summary>
        public String Parent_Condition_Item { get; set; }
        /// <summary>
        /// 検索条件  :PARENT_CONDITION_VALUE
        /// </summary>
        public String Parent_Condition_Value { get; set; }
        /// <summary>
        /// 検索条件  :CHILD_CONDITION_ITEM
        /// </summary>
        public String Child_Condition_Item { get; set; }
        /// <summary>
        /// 検索条件  :CHILD_CONDITION_VALUE
        /// </summary>
        public String Child_Condition_Value { get; set; }
    }
}
