using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI : SuperEntity
    {
        public SqlInt64 SHIME_JIKKOU_NO { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
    }
}