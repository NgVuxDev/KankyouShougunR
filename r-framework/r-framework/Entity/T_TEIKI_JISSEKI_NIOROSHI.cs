using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_TEIKI_JISSEKI_NIOROSHI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt32 NIOROSHI_NUMBER { get; set; }
        public SqlInt64 TEIKI_JISSEKI_NUMBER { get; set; }
        public SqlInt16 ROW_NUMBER { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public SqlDecimal NIOROSHI_RYOU { get; set; }
        public SqlDateTime HANNYUU_DATE { get; set; }
        public string SEARCH_HANNYUU_DATE { get; set; }
    }
}