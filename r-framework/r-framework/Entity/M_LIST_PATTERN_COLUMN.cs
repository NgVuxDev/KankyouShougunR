using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_LIST_PATTERN_COLUMN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlBoolean DETAIL_KBN { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public SqlInt32 WINDOW_ID { get; set; }
        public SqlInt32 KOUMOKU_ID { get; set; }
    }
}