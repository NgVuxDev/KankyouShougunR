using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class IMPORT_MEMBER_FILTER : SuperEntity
    {
        public SqlDecimal FILTER_NUMBER { get; set; }
        public string MEMBER_ID1 { get; set; }
        public string MEMBER_ID2 { get; set; }
        public string MEMBER_ID3 { get; set; }
        public string MEMBER_ID4 { get; set; }
        public string MEMBER_ID5 { get; set; }
    }
}