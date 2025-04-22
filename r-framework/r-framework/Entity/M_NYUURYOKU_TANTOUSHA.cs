using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NYUURYOKU_TANTOUSHA : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public string NYUURYOKU_TANTOUSHA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}