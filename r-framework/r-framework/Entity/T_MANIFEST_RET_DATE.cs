using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_RET_DATE : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDateTime SEND_A { get; set; }
        public string SEARCH_SEND_A { get; set; }
        public SqlDateTime SEND_B1 { get; set; }
        public string SEARCH_SEND_B1 { get; set; }
        public SqlDateTime SEND_B2 { get; set; }
        public string SEARCH_SEND_B2 { get; set; }
        public SqlDateTime SEND_B4 { get; set; }
        public string SEARCH_SEND_B4 { get; set; }
        public SqlDateTime SEND_B6 { get; set; }
        public string SEARCH_SEND_B6 { get; set; }
        public SqlDateTime SEND_C1 { get; set; }
        public string SEARCH_SEND_C1 { get; set; }
        public SqlDateTime SEND_C2 { get; set; }
        public string SEARCH_SEND_C2 { get; set; }
        public SqlDateTime SEND_D { get; set; }
        public string SEARCH_SEND_D { get; set; }
        public SqlDateTime SEND_E { get; set; }
        public string SEARCH_SEND_E { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}