using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_PT_R06 : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal BIKOU_NO { get; set; }
        public string BIKOU { get; set; }
    }
}