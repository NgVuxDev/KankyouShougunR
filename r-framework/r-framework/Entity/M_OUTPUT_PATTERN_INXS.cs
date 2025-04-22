using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_PATTERN_INXS : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string HYOUJUN_TEMPLATE_CD { get; set; }
    }
}
