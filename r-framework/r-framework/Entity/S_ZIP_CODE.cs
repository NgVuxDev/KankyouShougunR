using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_ZIP_CODE : SuperEntity
    {
        public SqlInt32 SYS_ID { get; set; }
        public string POST3 { get; set; }
        public string POST5 { get; set; }
        public string POST7 { get; set; }
        public string TODOUFUKEN { get; set; }
        public string SIKUCHOUSON { get; set; }
        public string OTHER1 { get; set; }
        public string OTHER2 { get; set; }
    }
}