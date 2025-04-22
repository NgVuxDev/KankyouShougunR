using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_KARIUKE_CHOUSEI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 NYUUKIN_NUMBER { get; set; }
        public string NYUUKINSAKI_CD { get; set; }
        public SqlDecimal KINGAKU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}