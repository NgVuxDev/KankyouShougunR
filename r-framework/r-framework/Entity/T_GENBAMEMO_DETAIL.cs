using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_GENBAMEMO_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public string COMMENT { get; set; }
        public string TOUROKUSHA_NAME { get; set; }
        public SqlDateTime TOUROKU_DATE { get; set; }
    }
}
