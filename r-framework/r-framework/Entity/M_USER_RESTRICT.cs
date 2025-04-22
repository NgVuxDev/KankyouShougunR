using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_USER_RESTRICT : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string URINFO { get; set; }
    }
}
