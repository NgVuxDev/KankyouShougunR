using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_TYOUSEI_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 TYOUSEI_NUMBER { get; set; }
        public SqlDateTime TYOUSEI_DATE { get; set; }
        public string SEARCH_TYOUSEI_DATE { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string TYOUSEI_BIKOU1 { get; set; }
        public string TYOUSEI_BIKOU2 { get; set; }
        public string TYOUSEI_BIKOU3 { get; set; }
        public SqlDecimal TYOUSEI_BEFORE_GOUKEI { get; set; }
        public SqlDecimal TYOUSEI_AFTER_GOUKEI { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}