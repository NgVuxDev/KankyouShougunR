using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_CHOUSEI_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 CHOUSEI_NUMBER { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}