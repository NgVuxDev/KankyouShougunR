using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_SHINSEI_ROUTE : SuperEntity
    {
        public string DENSHI_SHINSEI_ROUTE_CD { get; set; }
        public SqlInt32 DENSHI_SHINSEI_ROW_NO { get; set; }
        public string BUSHO_CD { get; set; }
        public string SHAIN_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}