using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ZAIKO_HINMEI : SuperEntity
    {
        public string ZAIKO_HINMEI_CD { get; set; }
        public string ZAIKO_HINMEI_NAME { get; set; }
        public string ZAIKO_HINMEI_NAME_RYAKU { get; set; }
        public string ZAIKO_HINMEI_FURIGANA { get; set; }
        public SqlDecimal ZAIKO_TANKA { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public string UNIT_NAME { get; set; }
        public string BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}