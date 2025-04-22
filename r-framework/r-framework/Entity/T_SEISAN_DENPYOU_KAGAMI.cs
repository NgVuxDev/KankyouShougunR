using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEISAN_DENPYOU_KAGAMI : SuperEntity
    {
        public SqlInt64 SEISAN_NUMBER { get; set; }
        public SqlInt32 KAGAMI_NUMBER { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 DAIHYOU_PRINT_KBN { get; set; }
        public string CORP_NAME { get; set; }
        public string CORP_DAIHYOU { get; set; }
        public SqlInt16 KYOTEN_NAME_PRINT_KBN { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string KYOTEN_NAME { get; set; }
        public string KYOTEN_DAIHYOU { get; set; }
        public string KYOTEN_POST { get; set; }
        public string KYOTEN_ADDRESS1 { get; set; }
        public string KYOTEN_ADDRESS2 { get; set; }
        public string KYOTEN_TEL { get; set; }
        public string KYOTEN_FAX { get; set; }
        public string SHIHARAI_SOUFU_NAME1 { get; set; }
        public string SHIHARAI_SOUFU_NAME2 { get; set; }
        public string SHIHARAI_SOUFU_KEISHOU1 { get; set; }
        public string SHIHARAI_SOUFU_KEISHOU2 { get; set; }
        public string SHIHARAI_SOUFU_POST { get; set; }
        public string SHIHARAI_SOUFU_ADDRESS1 { get; set; }
        public string SHIHARAI_SOUFU_ADDRESS2 { get; set; }
        public string SHIHARAI_SOUFU_BUSHO { get; set; }
        public string SHIHARAI_SOUFU_TANTOU { get; set; }
        public string SHIHARAI_SOUFU_TEL { get; set; }
        public string SHIHARAI_SOUFU_FAX { get; set; }
        public SqlDecimal KONKAI_SHIHARAI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_SEI_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_DEN_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_DEN_SOTOZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_MEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal KONKAI_MEI_SOTOZEI_GAKU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //éxï•ñæç◊èëîı
        public string BIKOU_1 { get; set; }
        public string BIKOU_2 { get; set; }
        //ìKäiêøãÅèë
        public SqlInt16 KONKAI_KAZEI_KBN_1 { get; set; }
        public SqlDecimal KONKAI_KAZEI_RATE_1 { get; set; }
        public SqlDecimal KONKAI_KAZEI_GAKU_1 { get; set; }
        public SqlDecimal KONKAI_KAZEI_ZEIGAKU_1 { get; set; }
        public SqlInt16 KONKAI_KAZEI_KBN_2 { get; set; }
        public SqlDecimal KONKAI_KAZEI_RATE_2 { get; set; }
        public SqlDecimal KONKAI_KAZEI_GAKU_2 { get; set; }
        public SqlDecimal KONKAI_KAZEI_ZEIGAKU_2 { get; set; }
        public SqlInt16 KONKAI_KAZEI_KBN_3 { get; set; }
        public SqlDecimal KONKAI_KAZEI_RATE_3 { get; set; }
        public SqlDecimal KONKAI_KAZEI_GAKU_3 { get; set; }
        public SqlDecimal KONKAI_KAZEI_ZEIGAKU_3 { get; set; }
        public SqlInt16 KONKAI_KAZEI_KBN_4 { get; set; }
        public SqlDecimal KONKAI_KAZEI_RATE_4 { get; set; }
        public SqlDecimal KONKAI_KAZEI_GAKU_4 { get; set; }
        public SqlDecimal KONKAI_KAZEI_ZEIGAKU_4 { get; set; }
        public SqlInt16 KONKAI_HIKAZEI_KBN { get; set; }
        public SqlDecimal KONKAI_HIKAZEI_GAKU { get; set; }
    }
}