using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_LOGI_DELIVERY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt16 HAISHA_KBN { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UPN_GYOUSHA_CD { get; set; }
        public SqlDateTime DELIVERY_DATE { get; set; }
        public SqlInt32 DELIVERY_NO { get; set; }
        public string DELIVERY_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
