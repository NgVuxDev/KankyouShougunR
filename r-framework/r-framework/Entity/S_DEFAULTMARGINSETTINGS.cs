using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_DEFAULTMARGINSETTINGS : SuperEntity
    {
        public SqlInt32 ID { get; set; }
        public string PRINTERMAKERNAME { get; set; }
        public string PRINTERNAME { get; set; }
        public string REPORTDISPNAME { get; set; }
        public SqlDecimal MARGIN_TOP { get; set; }
        public SqlDecimal MARGIN_LEFT { get; set; }
        public SqlDecimal MARGIN_BOTTOM { get; set; }
        public SqlDecimal MARGIN_RIGHT { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}