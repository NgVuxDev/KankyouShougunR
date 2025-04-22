using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_LAST_SBN_SUSPEND : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}