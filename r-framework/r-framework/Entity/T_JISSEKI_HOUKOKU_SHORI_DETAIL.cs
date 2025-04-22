using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_JISSEKI_HOUKOKU_SHORI_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt16 REPORT_ID { get; set; }
        public SqlInt16 HOUKOKU_SHOSHIKI_KBN { get; set; }
        public string HOZON_NAME { get; set; }
        public string HOUKOKU_YEAR { get; set; }
        public string TEISHUTSUSAKI_CHIIKI_CD { get; set; }
        public string SHORI_SHISETSU_CD { get; set; }
        public string SHORI_SHISETSU_NAME { get; set; }
        public SqlInt16 JIGYOUJOU_KBN { get; set; }
        public SqlInt32 KEN_KBN { get; set; }
        public string SBN_AFTER_HAIKI_NAME { get; set; }
        public SqlInt32 PAGE_NO { get; set; }
        public string HAIKI_SHURUI_CD1 { get; set; }
        public string HAIKI_SHURUI_NAME1 { get; set; }
        public SqlDecimal SBN_RYOU1 { get; set; }
        public string HAIKI_SHURUI_CD2 { get; set; }
        public string HAIKI_SHURUI_NAME2 { get; set; }
        public SqlDecimal SBN_RYOU2 { get; set; }
        public string HAIKI_SHURUI_CD3 { get; set; }
        public string HAIKI_SHURUI_NAME3 { get; set; }
        public SqlDecimal SBN_RYOU3 { get; set; }
        public string HAIKI_SHURUI_CD4 { get; set; }
        public string HAIKI_SHURUI_NAME4 { get; set; }
        public SqlDecimal SBN_RYOU4 { get; set; }
        public string UNIT_NAME { get; set; }
        public SqlDecimal HST_RYOU { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHOBUN_HOUHOU_NAME { get; set; }
        public SqlDecimal SBN_RYOU { get; set; }
        public SqlInt16 HST_KEN_KBN { get; set; }
        public SqlInt16 HST_JOU_TODOUFUKEN_CD { get; set; }
        public string HST_JOU_CHIIKI_CD { get; set; }
        public SqlInt16 SYUKEI_KBN { get; set; }
    }
}