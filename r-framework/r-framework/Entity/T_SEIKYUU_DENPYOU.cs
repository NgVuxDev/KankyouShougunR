using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEIKYUU_DENPYOU : SuperEntity
    {
        public SqlInt64 SEIKYUU_NUMBER { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt16 SHIMEBI { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 SHOSHIKI_KBN { get; set; }
        public SqlInt16 SHOSHIKI_MEISAI_KBN { get; set; }
        public SqlInt16 SHOSHIKI_GENBA_KBN { get; set; }
        public SqlInt16 SEIKYUU_KEITAI_KBN { get; set; }
        public SqlInt16 NYUUKIN_MEISAI_KBN { get; set; }
        public SqlInt16 YOUSHI_KBN { get; set; }
        public SqlDateTime SEIKYUU_DATE { get; set; }
        public string SEARCH_SEIKYUU_DATE { get; set; }
        public SqlDateTime NYUUKIN_YOTEI_BI { get; set; }
        public string SEARCH_NYUUKIN_YOTEI_BI { get; set; }
        public SqlDecimal ZENKAI_KURIKOSI_GAKU { get; set; }
        public SqlDecimal KONKAI_NYUUKIN_GAKU { get; set; }
        public SqlDecimal KONKAI_CHOUSEI_GAKU { get; set; }
        public SqlDecimal KONKAI_URIAGE_GAKU { get; set; }
        public SqlDecimal KONKAI_SEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEI_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_DEN_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_DEN_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_MEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_MEI_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEIKYU_GAKU { get; set; }
        public string FURIKOMI_BANK_CD { get; set; }
        public string FURIKOMI_BANK_NAME { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD { get; set; }
        public string FURIKOMI_BANK_SHITEN_NAME { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public string KOUZA_NAME { get; set; }
        public SqlBoolean HAKKOU_KBN { get; set; }
        public SqlInt64 SHIME_JIKKOU_NO { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }       
        //ã‚çsèÓïÒÇQ
        public string FURIKOMI_BANK_CD_2 { get; set; }
        public string FURIKOMI_BANK_NAME_2 { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD_2 { get; set; }
        public string FURIKOMI_BANK_SHITEN_NAME_2 { get; set; }
        public string KOUZA_SHURUI_2 { get; set; }
        public string KOUZA_NO_2 { get; set; }
        public string KOUZA_NAME_2 { get; set; }
        //ã‚çsèÓïÒÇR
        public string FURIKOMI_BANK_CD_3 { get; set; }
        public string FURIKOMI_BANK_NAME_3 { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD_3 { get; set; }
        public string FURIKOMI_BANK_SHITEN_NAME_3 { get; set; }
        public string KOUZA_SHURUI_3 { get; set; }
        public string KOUZA_NO_3 { get; set; }
        public string KOUZA_NAME_3 { get; set; }
        public SqlBoolean HIKAE_INSATSU_KBN { get; set; }
        //ìKäiêøãÅèë
        public string TOUROKU_NO { get; set; }
        public SqlInt16 INVOICE_KBN { get; set; }
    }
}