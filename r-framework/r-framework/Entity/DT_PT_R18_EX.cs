using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_PT_R18_EX : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDecimal REC_SEQ { get; set; }
        public string HAIKI_SHURUI_CODE { get; set; }
        public string HAIKI_SHURUI_NAME { get; set; }
        public string HAIKI_NAME_CODE { get; set; }
        public string HAIKI_NAME { get; set; }
        public SqlDecimal HAIKI_SUU { get; set; }
        public string UNIT_CODE { get; set; }
        public string UNIT_NAME { get; set; }
        public SqlDecimal KANSAN_SUU { get; set; }
        public SqlDecimal GENNYOU_SUU { get; set; }
        public string NISUGATA_CODE { get; set; }
        public string NISUGATA_NAME { get; set; }
        public string NISUGATA_SUU { get; set; }
        public string SUU_KAKUTEI_CODE { get; set; }
        public string SUU_KAKUTEI_NAME { get; set; }
        public string YUUGAI_CODE1 { get; set; }
        public string YUUGAI_NAME1 { get; set; }
        public string YUUGAI_CODE2 { get; set; }
        public string YUUGAI_NAME2 { get; set; }
        public string YUUGAI_CODE3 { get; set; }
        public string YUUGAI_NAME3 { get; set; }
        public string YUUGAI_CODE4 { get; set; }
        public string YUUGAI_NAME4 { get; set; }
        public string YUUGAI_CODE5 { get; set; }
        public string YUUGAI_NAME5 { get; set; }
        public string YUUGAI_CODE6 { get; set; }
        public string YUUGAI_NAME6 { get; set; }
        public SqlDecimal INPUT_KBN { get; set; }
    }
}