using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CORP_CLOSED : SuperEntity
    {
        public SqlDateTime CORP_CLOSED_DATE { get; set; }
        public string SEARCH_CORP_CLOSED_DATE { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}