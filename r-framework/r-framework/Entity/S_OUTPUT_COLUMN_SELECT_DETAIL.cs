using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_OUTPUT_COLUMN_SELECT_DETAIL : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt16 OUTPUT_KBN { get; set; }
        public SqlInt32 KOUMOKU_ID { get; set; }
        public SqlInt32 ROW_ID { get; set; }
        public string TABLE_NAME { get; set; }
        public string BUTSURI_NAME { get; set; }
        public string DATA_TYPE { get; set; }
    }
}