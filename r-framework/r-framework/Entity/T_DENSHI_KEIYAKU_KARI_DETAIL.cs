using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_KARI_DETAIL : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string DOCUMENT_ID { get; set; }
        public string FILE_ID { get; set; }
        public string FILE_NAME { get; set; }
    }
}