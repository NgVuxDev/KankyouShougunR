using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHOBUN_HOUHOU : SuperEntity
    {
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHOBUN_HOUHOU_NAME { get; set; }
        public string SHOBUN_HOUHOU_NAME_RYAKU { get; set; }
        public string SHOBUN_HOUHOU_BIKOU { get; set; }
        public SqlBoolean DENSHI_USE_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlBoolean KAMI_USE_KBN { get; set; }
    }
}