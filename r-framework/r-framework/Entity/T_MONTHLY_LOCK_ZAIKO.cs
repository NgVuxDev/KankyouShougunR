using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MONTHLY_LOCK_ZAIKO : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public SqlInt16 YEAR { get; set; }
        public SqlInt16 MONTH { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDecimal PREVIOUS_MONTH_ZAIKO_RYOU { get; set; }
        public SqlDecimal PREVIOUS_MONTH_KINGAKU { get; set; }
        public SqlDecimal UKEIRE_RYOU { get; set; }
        public SqlDecimal SHUKKA_RYOU { get; set; }
        public SqlDecimal TYOUSEI_RYOU { get; set; }
        public SqlDecimal IDOU_RYOU { get; set; }
        public SqlDecimal MONTH_ZAIKO_RYOU { get; set; }
        public SqlDecimal MONTH_KINGAKU { get; set; }
        public SqlDecimal GOUKEI_ZAIKO_RYOU { get; set; }
        public SqlDecimal GOUKEI_KINGAKU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}