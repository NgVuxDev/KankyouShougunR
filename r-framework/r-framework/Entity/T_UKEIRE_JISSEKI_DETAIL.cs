using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UKEIRE_JISSEKI_DETAIL : SuperEntity
    {
        public SqlInt16 DENPYOU_SHURUI { get; set; }
        public SqlInt64 DENPYOU_SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt32 DETAIL_SEQ { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 SUURYOU_WARIAI { get; set; }
    }
}