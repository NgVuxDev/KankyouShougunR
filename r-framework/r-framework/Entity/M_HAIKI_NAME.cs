using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HAIKI_NAME : SuperEntity
    {
        public string HAIKI_NAME_CD { get; set; }
        public string HAIKI_NAME { get; set; }
        public string HAIKI_NAME_RYAKU { get; set; }
        public string HAIKI_NAME_FURIGANA { get; set; }
        public string HAIKI_NAME_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}