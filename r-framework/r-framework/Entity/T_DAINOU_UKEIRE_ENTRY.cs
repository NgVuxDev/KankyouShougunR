using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DAINOU_UKEIRE_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DAINOU_NUMBER { get; set; }
        public SqlInt16 KAKUTEI_KBN { get; set; }
        public SqlDateTime URIAGE_DATE { get; set; }
        public string SEARCH_URIAGE_DATE { get; set; }
        public SqlDateTime SHIHARAI_DATE { get; set; }
        public string SEARCH_SHIHARAI_DATE { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public SqlDecimal NET_TOTAL { get; set; }
        public SqlDecimal URIAGE_SHOUHIZEI_RATE { get; set; }
        public SqlDecimal URIAGE_KINGAKU_TOTAL { get; set; }
        public SqlDecimal URIAGE_TAX_SOTO { get; set; }
        public SqlDecimal URIAGE_TAX_UCHI { get; set; }
        public SqlDecimal URIAGE_TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal URIAGE_TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal HINMEI_URIAGE_KINGAKU_TOTAL { get; set; }
        public SqlDecimal HINMEI_URIAGE_TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal HINMEI_URIAGE_TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal SHIHARAI_SHOUHIZEI_RATE { get; set; }
        public SqlDecimal SHIHARAI_KINGAKU_TOTAL { get; set; }
        public SqlDecimal SHIHARAI_TAX_SOTO { get; set; }
        public SqlDecimal SHIHARAI_TAX_UCHI { get; set; }
        public SqlDecimal SHIHARAI_TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal SHIHARAI_TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal HINMEI_SHIHARAI_KINGAKU_TOTAL { get; set; }
        public SqlDecimal HINMEI_SHIHARAI_TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal HINMEI_SHIHARAI_TAX_UCHI_TOTAL { get; set; }
        public SqlInt16 URIAGE_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 URIAGE_ZEI_KBN_CD { get; set; }
        public SqlInt16 URIAGE_TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_ZEI_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_TORIHIKI_KBN_CD { get; set; }
    }
}