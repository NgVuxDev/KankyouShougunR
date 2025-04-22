using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HAIKI_KBN : SuperEntity
    {
        public SqlInt16 HAIKI_KBN_CD { get; set; }
        public string HAIKI_KBN_NAME { get; set; }
        public string HAIKI_KBN_NAME_RYAKU { get; set; }
        public string HAIKI_KBN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}