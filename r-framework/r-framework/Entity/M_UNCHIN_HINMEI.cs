using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_UNCHIN_HINMEI : SuperEntity
    {
        public string UNCHIN_HINMEI_CD { get; set; }
        public string UNCHIN_HINMEI_NAME { get; set; }
        public string UNCHIN_HINMEI_FURIGANA { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public string UNIT_NAME { get; set; }
        public string BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}