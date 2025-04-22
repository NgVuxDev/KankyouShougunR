using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_LOGI_TO_URSH : SuperEntity
    {
        public SqlInt64 URSH_SYSTEM_ID { get; set; }
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
