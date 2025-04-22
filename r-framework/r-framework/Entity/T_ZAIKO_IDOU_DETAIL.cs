using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_IDOU_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 IDOU_NUMBER { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public SqlDecimal IDOU_BEFORE_ZAIKO_RYOU { get; set; }
        public SqlDecimal IDOU_RYOU { get; set; }
        public SqlDecimal IDOU_AFTER_ZAIKO_RYOU { get; set; }
    }
}