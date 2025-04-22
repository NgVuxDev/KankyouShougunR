using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SYS_INFO_MAIL : SuperEntity
    {
        public SqlInt16 SYS_ID { get; set; }
        public string SYS_MAIL_SERVER_NAME { get; set; }
        public SqlInt32 SYS_MAIL_ENCODE { get; set; }
        public string SYS_MAIL_FROM_ADDRESS { get; set; }
        public string SYS_MAIL_CC_ADDRESS { get; set; }
        public SqlInt16 SYS_MAIL_AUTH { get; set; }
        public string SYS_MAIL_SMTP_USER { get; set; }
        public string SYS_MAIL_SMTP_PWD { get; set; }
        public SqlInt16 SYS_MAIL_SMTP_PORT { get; set; }
    }
}