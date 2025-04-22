using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_IDOU_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 IDOU_NUMBER { get; set; }
        public SqlDateTime IDOU_DATE { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public string ZAIKO_HINMEI_NAME { get; set; }
        public SqlDecimal IDOU_BEFORE_ZAIKO_RYOU { get; set; }
        public SqlDecimal IDOU_RYOU_GOUKEI { get; set; }
        public SqlDecimal IDOU_AFTER_ZAIKO_RYOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}