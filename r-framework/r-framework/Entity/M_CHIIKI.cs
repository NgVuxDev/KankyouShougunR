using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKI : SuperEntity
    {
        public string CHIIKI_CD { get; set; }
        public string CHIIKI_NAME { get; set; }
        public string CHIIKI_NAME_RYAKU { get; set; }
        public string CHIIKI_FURIGANA { get; set; }
        public string CHIIKI_BIKOU { get; set; }
        public string GOV_OR_MAY_NAME { get; set; }
        public SqlInt16 TODOUFUKEN_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}