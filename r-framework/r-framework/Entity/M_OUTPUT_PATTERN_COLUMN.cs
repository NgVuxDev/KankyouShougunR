using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_PATTERN_COLUMN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public string TABLE_NAME { get; set; }
        public string KOUMOKU_RONRI_NAME { get; set; }
        public string KOUMOKU_BUTSURI_NAME { get; set; }
        public SqlInt16 OUTPUT_KBN { get; set; }
        public SqlInt32 KOUMOKU_ID { get; set; }
        public SqlInt16 SORT_NO { get; set; }
        public SqlInt16 PRIORITY_NO { get; set; }
    }
}