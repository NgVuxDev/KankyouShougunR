using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_FILE_LINK_UKEIRE_JISSEKI_ENTRY : SuperEntity
    {
        public SqlInt16 DENPYOU_SHURUI { get; set; }
        public SqlInt64 DENPYOU_SYSTEM_ID { get; set; }
        public SqlInt64 FILE_ID { get; set; }
    }
}
