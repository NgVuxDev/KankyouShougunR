using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_DENSHI_SOUHUSAKI : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ACCESS_CD { get; set; }
        public SqlInt16 DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD { get; set; }
        public SqlInt16 PRIORITY_NO { get; set; }
        public string SHAIN_CD { get; set; }
        public string SOUHU_TANTOUSHA_NAME { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string TEL_NO { get; set; }
        public string ATESAKI_NAME { get; set; }
        public string BUSHO_NAME { get; set; }
        public string SOUHUSAKI_BIKO { get; set; }
        public string KEIYAKUSAKI_CORP_NAME { get; set; }
        public SqlInt16 DENSHI_KEIYAKU_SHANAI_KEIRO { get; set; }
    }
}
