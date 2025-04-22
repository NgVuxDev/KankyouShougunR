using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_JISSEKI_HOUKOKU_MANIFEST_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt16 REPORT_ID { get; set; }
        public SqlInt32 DETAIL_ROW_NO { get; set; }
        public SqlInt16 HAIKI_KBN_CD { get; set; }
        public SqlInt64 MANI_SYSTEM_ID { get; set; }
        public SqlInt32 MANI_SEQ { get; set; }
        public string DEN_MANI_KANRI_ID { get; set; }
        public SqlInt32 DEN_MANI_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
    }
}