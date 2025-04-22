using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_FILE_LINK_SYS_INFO : SuperEntity
    {
        public string SYS_ID { get; set; }
        public SqlInt64 FILE_ID { get; set; }
    }
}
