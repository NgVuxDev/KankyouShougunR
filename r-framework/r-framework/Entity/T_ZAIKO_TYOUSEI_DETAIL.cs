using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_TYOUSEI_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 TYOUSEI_NUMBER { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public string ZAIKO_HINMEI_NAME { get; set; }
        public SqlDecimal TYOUSEI_BEFORE_ZAIKO_RYOU { get; set; }
        public SqlDecimal TYOUSEI_RYOU { get; set; }
        public SqlDecimal TYOUSEI_AFTER_ZAIKO_RYOU { get; set; }
    }
}