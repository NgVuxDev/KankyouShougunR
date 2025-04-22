using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_WORK_CLOSED_UNTENSHA : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public SqlDateTime CLOSED_DATE { get; set; }
        public string SEARCH_CLOSED_DATE { get; set; }
        public string BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}