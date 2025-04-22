using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_OPERATE_LOG : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public string OPERATE_DATE { get; set; }
        public string GAMEN_NAME { get; set; }
        public string OPERATE_USER { get; set; }
        public string OPERATE_PC { get; set; }
    }
}
