using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_TIMESTAMP : SuperEntity
    {
        public SqlInt64 ID { get; set; }
        public SqlDateTime csvTime { get; set; }
    }
}