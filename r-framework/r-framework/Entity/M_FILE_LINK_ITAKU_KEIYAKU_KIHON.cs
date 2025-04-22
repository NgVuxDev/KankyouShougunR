using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_FILE_LINK_ITAKU_KEIYAKU_KIHON : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt64 FILE_ID { get; set; }
    }
}
