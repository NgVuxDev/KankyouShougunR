using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NAVI_COLLABORATION_EVENTS : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 PROCESSING_ID { get; set; }
        public string SHAIN_CD { get; set; }
    }
}
