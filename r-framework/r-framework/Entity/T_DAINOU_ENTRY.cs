using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DAINOU_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string BUMON_CD { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
        public SqlInt64 DAINOU_NUMBER { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}