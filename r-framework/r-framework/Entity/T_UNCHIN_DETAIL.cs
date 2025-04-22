using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UNCHIN_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string UNCHIN_HINMEI_CD { get; set; }
        public string UNCHIN_HINMEI_NAME { get; set; }
        public SqlDecimal NET_JYUURYOU { get; set; }
        public SqlDecimal SUURYOU { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal TANKA { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public string MEISAI_BIKOU { get; set; }
    }
}