using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_TODOUFUKEN : SuperEntity
    {
        public SqlInt16 TODOUFUKEN_CD { get; set; }
        public string TODOUFUKEN_NAME { get; set; }
        public string TODOUFUKEN_NAME_RYAKU { get; set; }
        public string TODOUFUKEN_NAME_FURIGANA { get; set; }
        public string TODOUFUKEN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}