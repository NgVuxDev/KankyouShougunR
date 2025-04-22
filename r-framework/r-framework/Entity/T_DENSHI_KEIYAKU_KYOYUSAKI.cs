using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_KYOYUSAKI : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string DENSHI_KEIYAKU_SYSTEM_ID { get; set; }
        public SqlInt16 PRIORITY_NO { get; set; }
        public string KYOYUSAKI_CORP_NAME { get; set; }
        public string KYOYUSAKI_NAME { get; set; }
        public string KYOYUSAKI_MAIL_ADDRESS { get; set; }
    }
}
