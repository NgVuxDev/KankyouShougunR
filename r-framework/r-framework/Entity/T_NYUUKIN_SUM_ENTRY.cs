using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NYUUKIN_SUM_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt64 NYUUKIN_NUMBER { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
        public string NYUUKINSAKI_CD { get; set; }
        public string BANK_CD { get; set; }
        public string BANK_SHITEN_CD { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public string KOUZA_NAME { get; set; }
        public string EIGYOU_TANTOUSHA_CD { get; set; }
        public string DENPYOU_BIKOU { get; set; }
        public SqlDecimal NYUUKIN_AMOUNT_TOTAL { get; set; }
        public SqlDecimal CHOUSEI_AMOUNT_TOTAL { get; set; }
        public SqlDecimal KARIUKEKIN_WARIATE_TOTAL { get; set; }
        public SqlBoolean SEISAN_SOUSAI_CREATE_KBN { get; set; }
        public SqlBoolean TORI_KOMI_KBN { get; set; }
        public SqlBoolean JIDO_KESHIKOMI_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}