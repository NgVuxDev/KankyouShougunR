using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_TANK_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 ROW_NO { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public SqlDecimal REMAIN_SUU { get; set; }
        public SqlDecimal ENTER_SUU { get; set; }
        public SqlDecimal OUT_SUU { get; set; }
        public SqlDecimal ADJUST_SUU { get; set; }
        public SqlDecimal TOTAL_SUU { get; set; }
        public SqlDecimal TANKA { get; set; }
    }
}