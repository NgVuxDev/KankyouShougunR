using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_LIST_COLUMN_SELECT_DETAIL : SuperEntity
    {
        public SqlInt32 WINDOW_ID { get; set; }
        public SqlInt32 KOUMOKU_ID { get; set; }
        public SqlInt32 ROW_ID { get; set; }
        public string TABLE_NAME { get; set; }
        public string BUTSURI_NAME { get; set; }
        public string DATA_TYPE { get; set; }
    }
}