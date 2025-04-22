using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_RELATION : SuperEntity
    {
        public SqlInt64 NEXT_SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt32 REC_SEQ { get; set; }
        public SqlInt16 NEXT_HAIKI_KBN_CD { get; set; }
        public SqlInt64 FIRST_SYSTEM_ID { get; set; }
        public SqlInt16 FIRST_HAIKI_KBN_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}