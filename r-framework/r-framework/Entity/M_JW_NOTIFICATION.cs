using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_JW_NOTIFICATION : SuperEntity
    {
        public string NOTIFICATION_CODE { get; set; }
        public SqlInt16 ATESAKI_KBN { get; set; }
        public string TYPE { get; set; }
        public SqlInt16 LAW_KBN { get; set; }
        public SqlInt16 NOTIFICATION_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}