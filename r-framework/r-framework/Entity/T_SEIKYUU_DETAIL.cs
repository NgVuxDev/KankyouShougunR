using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEIKYUU_DETAIL : SuperEntity
    {
        public SqlInt64 SEIKYUU_NUMBER { get; set; }
        public SqlInt32 KAGAMI_NUMBER { get; set; }
        public SqlInt32 ROW_NUMBER { get; set; }
        public SqlInt16 DENPYOU_SHURUI_CD { get; set; }
        public SqlInt64 DENPYOU_SYSTEM_ID { get; set; }
        public SqlInt32 DENPYOU_SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME1 { get; set; }
        public string GYOUSHA_NAME2 { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME1 { get; set; }
        public string GENBA_NAME2 { get; set; }
        public string HINMEI_CD { get; set; }
        public string HINMEI_NAME { get; set; }
        public SqlDecimal SUURYOU { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public string UNIT_NAME { get; set; }
        public SqlDecimal TANKA { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public SqlDecimal UCHIZEI_GAKU { get; set; }
        public SqlDecimal SOTOZEI_GAKU { get; set; }
        public SqlDecimal DENPYOU_UCHIZEI_GAKU { get; set; }
        public SqlDecimal DENPYOU_SOTOZEI_GAKU { get; set; }
        public SqlInt16 DENPYOU_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 DENPYOU_ZEI_KBN_CD { get; set; }
        public SqlInt16 MEISAI_ZEI_KBN_CD { get; set; }
        public string MEISAI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlDecimal SHOUHIZEI_RATE { get; set; }
        public string SHARYOU_NAME { get; set; }
    }
}