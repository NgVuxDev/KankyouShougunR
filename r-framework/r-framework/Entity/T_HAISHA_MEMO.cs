using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_HAISHA_MEMO : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDateTime SAGYOU_DATE { get; set; }
        public string SEARCH_SAGYOU_DATE { get; set; }
        public string HAISHA_MEMO { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}