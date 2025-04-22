using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UKETSUKE_BP_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt64 UKETSUKE_NUMBER { get; set; }
        public SqlDateTime UKETSUKE_DATE { get; set; }
        public string SEARCH_UKETSUKE_DATE { get; set; }
        public SqlInt16 HAISHA_JOKYO_CD { get; set; }
        public string HAISHA_JOKYO_NAME { get; set; }
        public SqlInt16 HAISHA_SHURUI_CD { get; set; }
        public string HAISHA_SHURUI_NAME { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GYOSHA_TEL { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string GENBA_TEL { get; set; }
        public string TANTOSHA_NAME { get; set; }
        public string TANTOSHA_TEL { get; set; }
        public string EIGYOU_TANTOUSHA_CD { get; set; }
        public string EIGYOU_TANTOUSHA_NAME { get; set; }
        public string NOHIN_YOTEI_DATE { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNTENSHA_NAME { get; set; }
        public string HOJOIN_CD { get; set; }
        public string HOJOIN_NAME { get; set; }
        public string BP_ZAIKO_M_CD { get; set; }
        public string BP_ZAIKO_M_NAME { get; set; }
        public SqlBoolean HAISHA_SIJISHO_FLG { get; set; }
        public SqlBoolean MAIL_SEND_FLG { get; set; }
        public string UKETSUKE_BIKOU1 { get; set; }
        public string UKETSUKE_BIKOU2 { get; set; }
        public string UKETSUKE_BIKOU3 { get; set; }
        public string BIKOU1 { get; set; }
        public string BIKOU2 { get; set; }
        public string BIKOU3 { get; set; }
        public string UNTENSHA_SIJIJIKOU1 { get; set; }
        public string UNTENSHA_SIJIJIKOU2 { get; set; }
        public string UNTENSHA_SIJIJIKOU3 { get; set; }
        public SqlDecimal KINGAKU_TOTAL { get; set; }
        public SqlDecimal SHOUHIZEI_RATE { get; set; }
        public SqlDecimal TAX_SOTO { get; set; }
        public SqlDecimal TAX_UCHI { get; set; }
        public SqlDecimal TAX_SOTO_TOTAL { get; set; }
        public SqlDecimal TAX_UCHI_TOTAL { get; set; }
        public SqlDecimal SHOUHIZEI_TOTAL { get; set; }
        public SqlDecimal GOUKEI_KINGAKU_TOTAL { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}