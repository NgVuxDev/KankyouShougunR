using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_KARIUKE_CONTROL : SuperEntity
    {
        public string NYUUKINSAKI_CD { get; set; }
        public SqlDecimal KARIUKE_TOTAL_KINGAKU { get; set; }
    }
}