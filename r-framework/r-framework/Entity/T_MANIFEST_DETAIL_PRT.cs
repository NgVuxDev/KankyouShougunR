using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_DETAIL_PRT : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 REC_NO { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_SHURUI_NAME { get; set; }
        public SqlDecimal HAIKI_SUURYOU { get; set; }
        public SqlBoolean PRT_FLG { get; set; }
    }
}