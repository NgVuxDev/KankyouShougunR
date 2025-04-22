using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_SHOBUN : SuperEntity
    {
        public string CHIIKI_CD { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string HOUKOKU_SHOBUN_HOUHOU_CD { get; set; }
        public string HOUKOKU_SHOBUN_HOUHOU_NAME { get; set; }
        public string SHOBUN_MOKUTEKI_CD { get; set; }
        public string CHIIKIBETSU_SHOBUN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}