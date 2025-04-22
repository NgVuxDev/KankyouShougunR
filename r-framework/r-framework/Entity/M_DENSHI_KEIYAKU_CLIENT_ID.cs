using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_KEIYAKU_CLIENT_ID : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public string DENSHI_KEIYAKU_CLIENT_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}