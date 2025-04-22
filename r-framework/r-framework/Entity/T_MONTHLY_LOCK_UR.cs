using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MONTHLY_LOCK_UR : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 YEAR { get; set; }
        public SqlInt16 MONTH { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDecimal PREVIOUS_MONTH_BALANCE { get; set; }
        public SqlDecimal NYUUKIN_KINGAKU { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public SqlDecimal TAX { get; set; }
        public SqlDecimal SHIME_UTIZEI_GAKU { get; set; }
        public SqlDecimal SHIME_SOTOZEI_GAKU { get; set; }
        public SqlDecimal DEN_UTIZEI_GAKU { get; set; }
        public SqlDecimal DEN_SOTOZEI_GAKU { get; set; }
        public SqlDecimal MEI_UTIZEI_GAKU { get; set; }
        public SqlDecimal MEI_SOTOZEI_GAKU { get; set; }
        public SqlDecimal TOTAL_KINGAKU { get; set; }
        public SqlDecimal ZANDAKA { get; set; }
        public SqlInt32 INVOICE_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
