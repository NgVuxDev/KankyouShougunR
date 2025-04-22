using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_SYS_INFO_CHANGE_LOG : SuperEntity
    {
        public SqlInt64 ID { get; set; }
        public SqlInt32 ROW_NO { get; set; }
        public string CHANGE_COLUMN_NAME { get; set; }
        public string OLD_VALUE { get; set; }
        public string NEW_VALUE { get; set; }
    }
}