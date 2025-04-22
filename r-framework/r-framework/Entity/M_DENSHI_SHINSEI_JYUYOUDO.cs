using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_SHINSEI_JYUYOUDO : SuperEntity
    {
        public string JYUYOUDO_CD { get; set; }
        public string JYUYOUDO_NAME { get; set; }
        public SqlBoolean JYUYOUDO_DEFAULT { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
