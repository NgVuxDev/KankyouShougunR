using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_UNIT : SuperEntity
    {
        public SqlInt16 UNIT_CD { get; set; }
        public string UNIT_NAME { get; set; }
        public string UNIT_NAME_RYAKU { get; set; }
        public SqlBoolean DENSHI_USE_KBN { get; set; }
        public string UNIT_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlBoolean KAMI_USE_KBN { get; set; }
    }
}