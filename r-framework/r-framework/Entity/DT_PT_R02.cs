using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_PT_R02 : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDecimal REC_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public string YUUGAI_CODE { get; set; }
        public string YUUGAI_NAME { get; set; }
    }
}