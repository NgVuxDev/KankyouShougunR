using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEISAN_DENPYOU : SuperEntity
    {
        public SqlInt64 SEISAN_NUMBER { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 SHIMEBI { get; set; }
        public SqlInt16 SHOSHIKI_KBN { get; set; }
        public SqlInt16 SHOSHIKI_MEISAI_KBN { get; set; }
        public SqlInt16 SHOSHIKI_GENBA_KBN { get; set; }
        public SqlInt16 SHIHARAI_KEITAI_KBN { get; set; }
        public SqlInt16 SHUKKIN_MEISAI_KBN { get; set; }
        public SqlInt16 YOUSHI_KBN { get; set; }
        public SqlDateTime SEISAN_DATE { get; set; }
        public string SEARCH_SEISAN_DATE { get; set; }
        public SqlDateTime SHUKKIN_YOTEI_BI { get; set; }
        public string SEARCH_SHUKKIN_YOTEI_BI { get; set; }
        public SqlDecimal ZENKAI_KURIKOSI_GAKU { get; set; }
        public SqlDecimal KONKAI_SHUKKIN_GAKU { get; set; }
        public SqlDecimal KONKAI_CHOUSEI_GAKU { get; set; }
        public SqlDecimal KONKAI_SHIHARAI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEI_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_DEN_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_DEN_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_MEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_MEI_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEISAN_GAKU { get; set; }
        public SqlBoolean HAKKOU_KBN { get; set; }
        public SqlInt64 SHIME_JIKKOU_NO { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlBoolean HIKAE_INSATSU_KBN { get; set; }
        //ìKäiêøãÅèë
        public string TOUROKU_NO { get; set; }
        public SqlInt16 INVOICE_KBN { get; set; }
    }
}