using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_OUTPUT_COLUMN_SELECT : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt16 OUTPUT_KBN { get; set; }
        public SqlInt32 KOUMOKU_ID { get; set; }
        public string DISP_KOUMOKU_NAME { get; set; }
        public SqlBoolean HISSU_KBN { get; set; }
        public string TABLE_NAME { get; set; }
        public string KOUMOKU_BUTSURI_NAME { get; set; }
    }
}