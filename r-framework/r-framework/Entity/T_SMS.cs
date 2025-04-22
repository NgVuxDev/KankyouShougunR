using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class T_SMS : SuperEntity
    {
        public SqlInt32 SYSTEM_ID { get; set; }
        public string SAGYOU_DATE { get; set; }
        public string GENCHAKU_TIME { get; set; }
        public string HAISHA_JOKYO_NAME { get; set; }
        public SqlInt16 DENPYOU_SHURUI { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 ROW_NUMBER { get; set; }
        public SqlInt16 SMS_STATUS { get; set; }
        public SqlInt16 RECEIVER_STATUS { get; set; }
        public SqlInt16 CARRIER { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string RECEIVER_NAME { get; set; }
        public string MOBILE_PHONE_NUMBER { get; set; }
        public string MESSAGE_ID { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_DETAIL { get; set; }
        public SqlDateTime SEND_DATE_R { get; set; }
        public SqlDateTime SEND_DATE_KARADEN { get; set; }
        public string SEND_USER { get; set; }
    }
}
