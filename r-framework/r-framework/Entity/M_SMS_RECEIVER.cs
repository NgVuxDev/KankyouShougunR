using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_SMS_RECEIVER : SuperEntity
    {
        public SqlInt32 SYSTEM_ID { get; set; }
        public SqlBoolean RENKEI_FLG { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public string MOBILE_PHONE_NUMBER { get; set; }
        public string RECEIVER_NAME { get; set; }
        public string BIKOU { get; set; }
    }
}
