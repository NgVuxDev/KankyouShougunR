using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UKETSUKE_BP_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 UKETSUKE_NUMBER { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string HINMEI_CD { get; set; }
        public string HINMEI_NAME { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlDecimal SUURYOU { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal TANKA { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public SqlDecimal TAX_SOTO { get; set; }
        public SqlDecimal TAX_UCHI { get; set; }
        public SqlInt16 HINMEI_ZEI_KBN_CD { get; set; }
        public SqlDecimal HINMEI_KINGAKU { get; set; }
        public SqlDecimal HINMEI_TAX_SOTO { get; set; }
        public SqlDecimal HINMEI_TAX_UCHI { get; set; }
        public string MEISAI_BIKOU { get; set; }
    }
}