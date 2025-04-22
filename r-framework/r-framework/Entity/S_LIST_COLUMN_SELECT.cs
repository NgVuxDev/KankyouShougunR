using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_LIST_COLUMN_SELECT : SuperEntity
    {
        public SqlInt32 WINDOW_ID { get; set; }
        public SqlInt32 KOUMOKU_ID { get; set; }
        public SqlBoolean DETAIL_KBN { get; set; }
        public string KOUMOKU_RONRI_NAME { get; set; }
        public SqlBoolean DISP_KBN { get; set; }
        public SqlBoolean TOTAL_KBN { get; set; }
        public string OUTPUT_FORMAT { get; set; }
        public SqlInt16 OUTPUT_ALIGN { get; set; }
    }
}