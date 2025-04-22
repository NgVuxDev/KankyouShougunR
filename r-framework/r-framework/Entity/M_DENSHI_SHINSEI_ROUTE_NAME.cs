using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class M_DENSHI_SHINSEI_ROUTE_NAME : SuperEntity
    {
        public string DENSHI_SHINSEI_ROUTE_CD { get; set; }
        public string DENSHI_SHINSEI_ROUTE_NAME { get; set; }
        public string DENSHI_SHINSEI_ROUTE_FURIGANA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}