using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MITSUMORI_DETAIL_2 : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public string BIKO_CD { get; set; }
        public string BIKO_NOTE { get; set; }
    }
}