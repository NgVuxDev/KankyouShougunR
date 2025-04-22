using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_SHISETSU : SuperEntity
    {
        public string CHIIKI_CD { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string HOUKOKU_SHISETSU_CD { get; set; }
        public string HOUKOKU_SHISETSU_NAME { get; set; }
        public string CHIIKIBETSU_SHISETSU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}