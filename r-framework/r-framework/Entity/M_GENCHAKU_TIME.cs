using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENCHAKU_TIME : SuperEntity
    {
        public SqlInt16 GENCHAKU_TIME_CD { get; set; }
        public string GENCHAKU_TIME_NAME { get; set; }
        public string GENCHAKU_TIME_NAME_RYAKU { get; set; }
        public SqlInt16 GENCHAKU_PRIORITY { get; set; }
        public SqlInt32 GENCHAKU_BACK_COLOR { get; set; }
        public string GENCHAKU_TIME_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}