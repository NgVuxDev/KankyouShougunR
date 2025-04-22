using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_USER_LOGIN : SuperEntity
    {
        public string LOGIN_ID { get; set; }
        public SqlInt32 LOGIN_COUNTER { get; set; }
        public string CLIENT_COMPUTER_NAME { get; set; }
        public string CLIENT_USER_NAME { get; set; }
        public SqlDateTime LOGIN_TIME { get; set; }
        public SqlDateTime LOGOUT_TIME { get; set; }
        public SqlInt32 RETURN_CODE { get; set; }
    }
}
