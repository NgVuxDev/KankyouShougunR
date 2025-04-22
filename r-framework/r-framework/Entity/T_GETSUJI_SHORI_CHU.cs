using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_GETSUJI_SHORI_CHU : SuperEntity
    {
        public SqlInt16 YEAR { get; set; }
        public SqlInt16 MONTH { get; set; }
        public string CLIENT_COMPUTER_NAME { get; set; }
        public string CLIENT_USER_NAME { get; set; }
    }
}
