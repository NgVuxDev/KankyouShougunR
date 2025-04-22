using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NYUUKINSAKI_FURIKOMI : SuperEntity
    {
        public string NYUUKINSAKI_CD { get; set; }
        public SqlInt16 FURIKOMI_SEQ { get; set; }
        public string FURIKOMI_NAME { get; set; }
    }
}