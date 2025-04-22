using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.Untenshakyudounyuuryoku
{
    public class DTOClass : SuperEntity
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
}
