using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_TEGATA_HOKANSHA : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public string TEGATA_HOKANSHA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}