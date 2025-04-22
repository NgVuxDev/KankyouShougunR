using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NAVI_LINK_STATUS : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt16 LINK_STATUS { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
