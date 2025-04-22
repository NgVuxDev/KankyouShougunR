using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UR_SH_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt64 UR_SH_NUMBER { get; set; }
        public SqlInt32 DATE_NUMBER { get; set; }
        public SqlInt32 YEAR_NUMBER { get; set; }
        public SqlInt16 KAKUTEI_KBN { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
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
        public string NIZUMI_GYOUSHA_CD { get; set; }
        public string NIZUMI_GYOUSHA_NAME { get; set; }
        public string NIZUMI_GENBA_CD { get; set; }
        public string NIZUMI_GENBA_NAME { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_NAME { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_GENBA_NAME { get; set; }
        public string EIGYOU_TANTOUSHA_CD { get; set; }
        public string EIGYOU_TANTOUSHA_NAME { get; set; }
        public string NYUURYOKU_TANTOUSHA_CD { get; set; }
        public string NYUURYOKU_TANTOUSHA_NAME { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNTENSHA_NAME { get; set; }
        public SqlInt16 NINZUU_CNT { get; set; }
        public SqlInt16 KEITAI_KBN_CD { get; set; }
        public SqlInt16 CONTENA_SOUSA_CD { get; set; }
        public SqlInt16 MANIFEST_SHURUI_CD { get; set; }
        public SqlInt16 MANIFEST_TEHAI_CD { get; set; }
        public string DENPYOU_BIKOU { get; set; }
        public SqlInt64 UKETSUKE_NUMBER { get; set; }
        public SqlInt32 RECEIPT_NUMBER { get; set; }
        public SqlInt32 RECEIPT_NUMBER_YEAR { get; set; }
        public SqlDecimal URIAGE_SHOUHIZEI_RATE { get; set; }
        public SqlDecimal URIAGE_AMOUNT_TOTAL { get; set; }
        public SqlDecimal URIAGE_TAX_SOTO { get; set; }
        public SqlDecimal URIAGE_TAX_UCHI { get; set; }
        public SqlDecimal URIAGE_TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal URIAGE_TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal HINMEI_URIAGE_KINGAKU_TOTAL { get; set; }
        public SqlDecimal HINMEI_URIAGE_TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal HINMEI_URIAGE_TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal SHIHARAI_SHOUHIZEI_RATE { get; set; }
        public SqlDecimal SHIHARAI_AMOUNT_TOTAL { get; set; }
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
        public SqlBoolean TSUKI_CREATE_KBN { get; set; }
        public SqlBoolean DAINOU_FLG { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //PhuocLoc 2020/12/01 #136221 -Start
        public string MOD_SHUUKEI_KOUMOKU_CD { get; set; }
        public string MOD_SHUUKEI_KOUMOKU_NAME { get; set; }
        //PhuocLoc 2020/12/01 #136221 -End
    }
}