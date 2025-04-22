using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_NAME_CD { get; set; }
        public string NISUGATA_CD { get; set; }
        public SqlDecimal WARIAI { get; set; }
        public SqlDecimal HAIKI_SUU { get; set; }
        public SqlInt16 HAIKI_UNIT_CD { get; set; }
        public SqlDecimal KANSAN_SUU { get; set; }
        public SqlDecimal GENNYOU_SUU { get; set; }
        public string SBN_HOUHOU_CD { get; set; }
        public SqlDateTime SBN_END_DATE { get; set; }
        public string SEARCH_SBN_END_DATE { get; set; }
        public SqlDateTime LAST_SBN_END_DATE { get; set; }
        public string SEARCH_LAST_SBN_END_DATE { get; set; }
        public string LAST_SBN_GYOUSHA_CD { get; set; }
        public string LAST_SBN_GENBA_CD { get; set; }
    }
}