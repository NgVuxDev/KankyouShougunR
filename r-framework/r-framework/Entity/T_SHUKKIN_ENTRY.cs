using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SHUKKIN_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt64 SHUKKIN_NUMBER { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public string SEARCH_DENPYOU_DATE { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string SHUKKINSAKI_CD { get; set; }
        public string EIGYOU_TANTOUSHA_CD { get; set; }
        public string DENPYOU_BIKOU { get; set; }
        public SqlDecimal SHUKKIN_AMOUNT_TOTAL { get; set; }
        public SqlDecimal CHOUSEI_AMOUNT_TOTAL { get; set; }
        public SqlBoolean SEISAN_SOUSAI_CREATE_KBN { get; set; }

        public string BANK_CD { get; set; }
        public string BANK_SHITEN_CD { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public string KOUZA_NAME { get; set; }

        public SqlInt16 FURIKOMI_SHUTSURYOKU_KBN { get; set; }
        public string FURIKOMI_BANK_CD { get; set; }
        public string FURIKOMI_BANK_NAME { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD { get; set; }
        public string FURIKOMI_BANK_SHITEN_NAME { get; set; }
        public SqlInt16 FURIKOMI_KOUZA_SHURUI_CD { get; set; }
        public string FURIKOMI_KOUZA_SHURUI_NAME { get; set; }
        public string FURIKOMI_KOUZA_NO { get; set; }
        public string FURIKOMI_KOUZA_NAME { get; set; }
        public SqlBoolean BANK_EXPORTED_FLG { get; set; }

        public SqlBoolean DELETE_FLG { get; set; }
    }
}