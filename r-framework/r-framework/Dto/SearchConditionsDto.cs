
using System;
using System.ComponentModel;
using r_framework.Const;
namespace r_framework.Dto
{
    [Serializable]
    public class SearchConditionsDto
    {
        [Description("and orを選択してください。")]
        public CONDITION_OPERATOR And_Or { get; set; }

        public string LeftColumn { get; set; }

        public string RightColumn { get; set; }

        public string Value { get; set; }

        public JUGGMENT_CONDITION Condition { get; set; }

        public DB_TYPE ValueColumnType { get; set; }
    }
}
