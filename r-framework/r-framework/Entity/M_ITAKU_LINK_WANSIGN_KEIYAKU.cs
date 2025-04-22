using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_LINK_WANSIGN_KEIYAKU : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt64 WANSIGN_SYSTEM_ID { get; set; }
        public string DOCUMENT_ID { get; set; }
    }
}