using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NYUUKIN_SUM_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt16 ROW_NUMBER { get; set; }
        public SqlInt16 NYUUSHUKKIN_KBN_CD { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public string MEISAI_BIKOU { get; set; }
    }
}