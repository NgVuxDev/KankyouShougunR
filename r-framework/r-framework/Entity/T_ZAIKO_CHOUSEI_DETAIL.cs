using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_CHOUSEI_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 CHOUSEI_NUMBER { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public SqlDecimal JYUURYOU { get; set; }
        public SqlDecimal TANKA { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}