using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_YOUKI : SuperEntity
    {
        public string YOUKI_CD { get; set; }
        public string YOUKI_NAME { get; set; }
        public string YOUKI_NAME_RYAKU { get; set; }
        public string YOUKI_FURIGANA { get; set; }
        public SqlDecimal YOUKI_JYURYO { get; set; }
        public string YOUKI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}