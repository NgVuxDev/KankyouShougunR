using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_OBOE : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string MEMO { get; set; }
    }
}