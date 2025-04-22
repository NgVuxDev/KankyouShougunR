using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_LOGI_TO_TEIKI : SuperEntity
    {
        public SqlInt64 TEIKI_SYSTEM_ID { get; set; }
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
