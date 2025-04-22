using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_PT_R05 : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal RENRAKU_ID_NO { get; set; }
        public string RENRAKU_ID { get; set; }
    }
}