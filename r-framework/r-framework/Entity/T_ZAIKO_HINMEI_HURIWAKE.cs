using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_HINMEI_HURIWAKE : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public string ZAIKO_HINMEI_NAME { get; set; }
        public SqlInt16 ZAIKO_HIRITSU { get; set; }
        public SqlDecimal ZAIKO_RYOU { get; set; }
        public SqlDecimal ZAIKO_TANKA { get; set; }
        public SqlDecimal ZAIKO_KINGAKU { get; set; }
    }
}
