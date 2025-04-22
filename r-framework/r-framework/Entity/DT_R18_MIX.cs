using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_R18_MIX : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string KANRI_ID { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string MANIFEST_ID { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_DAI_CODE { get; set; }
        public string HAIKI_CHU_CODE { get; set; }
        public string HAIKI_SHO_CODE { get; set; }
        public string HAIKI_SAI_CODE { get; set; }
        public string HAIKI_BUNRUI_NAME { get; set; }
        public string SBN_HOUHOU_CD { get; set; }
        public SqlDecimal HAIKI_SUU { get; set; }
        public SqlInt16 HAIKI_UNIT_CD { get; set; }
        public SqlDecimal KANSAN_SUU { get; set; }
        public SqlDecimal GENNYOU_SUU { get; set; }
        public string HAIKI_NAME_CD { get; set; }
        public SqlInt16 SBN_ENDREP_KBN { get; set; }
        public SqlDateTime LAST_SBN_END_DATE { get; set; }
        public SqlDecimal WARIAI { get; set; }
        public string KONGOU_SHURUI_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}