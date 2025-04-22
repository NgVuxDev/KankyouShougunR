using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_SOUHUSAKI : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string DENSHI_KEIYAKU_SYSTEM_ID { get; set; }
        public SqlInt16 PRIORITY_NO { get; set; }
        public string SHAIN_CD { get; set; }
        public string SHAIN_NAME { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string TEL_NO { get; set; }
        public string ATESAKI_NAME { get; set; }
        public string BUSHO_NAME { get; set; }
        public string SOUHUSAKI_BIKO { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
