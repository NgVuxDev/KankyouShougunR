using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_UNTENSHA : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public string UNTENSHA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}