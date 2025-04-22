using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_GYOUSHU : SuperEntity
    {
        public string CHIIKI_CD { get; set; }
        public string GYOUSHU_CD { get; set; }
        public string HOUKOKU_GYOUSHU_CD { get; set; }
        public string HOUKOKU_GYOUSHU_NAME { get; set; }
        public string CHIIKIBETSU_GYOUSHU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}