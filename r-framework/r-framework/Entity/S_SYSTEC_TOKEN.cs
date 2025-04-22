using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_SYSTEC_TOKEN : SuperEntity
    {
        public string USER_ID { get; set; }
        public string ACCESS_TOKEN { get; set; }
        public string TOKEN_TYPE { get; set; }
        public int EXPIRES_IN { get; set; }
        public SqlDateTime EXPIRED_DATE { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
