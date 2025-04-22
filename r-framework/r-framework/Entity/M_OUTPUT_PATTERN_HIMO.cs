using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_PATTERN_HIMO : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 OUTPUT_KBN { get; set; }
        public string PATTERN_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}