using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MONTHLY_ADJUST_SH : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 YEAR { get; set; }
        public SqlInt16 MONTH { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt32 UPDATE_SEQ { get; set; }
        public SqlDecimal ADJUST_TAX { get; set; }
        public SqlDecimal ZANDAKA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
