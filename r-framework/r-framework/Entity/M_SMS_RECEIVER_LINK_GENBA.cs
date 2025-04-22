using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SMS_RECEIVER_LINK_GENBA : SuperEntity
    {
        public SqlInt32 SYSTEM_ID { get; set; }
        public string MOBILE_PHONE_NUMBER { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
    }
}
