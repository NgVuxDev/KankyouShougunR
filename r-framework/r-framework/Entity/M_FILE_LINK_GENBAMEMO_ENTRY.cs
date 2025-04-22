using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_FILE_LINK_GENBAMEMO_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt64 FILE_ID { get; set; }
    }
}
