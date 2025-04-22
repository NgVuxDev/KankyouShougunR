using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NISUGATA : SuperEntity
    {
        public string NISUGATA_CD { get; set; }
        public string NISUGATA_NAME { get; set; }
        public string NISUGATA_NAME_RYAKU { get; set; }
        public string NISUGATA_BIKOU { get; set; }
        public SqlBoolean DENSHI_USE_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlBoolean KAMI_USE_KBN { get; set; }
    }
}