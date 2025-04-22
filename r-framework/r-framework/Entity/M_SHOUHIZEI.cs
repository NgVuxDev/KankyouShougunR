using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHOUHIZEI : SuperEntity
    {
        public SqlInt16 SYS_ID { get; set; }
        public SqlDecimal SHOUHIZEI_RATE { get; set; }
        public string SHOUHIZEI_BIKOU { get; set; }
        public SqlDateTime TEKIYOU_BEGIN { get; set; }
        public string SEARCH_TEKIYOU_BEGIN { get; set; }
        public SqlDateTime TEKIYOU_END { get; set; }
        public string SEARCH_TEKIYOU_END { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}