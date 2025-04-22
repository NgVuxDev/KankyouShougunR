using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_JUUSHO : SuperEntity
    {
        public string CHIIKI_CD { get; set; }
        public string CHANGE_CHIIKI_CD { get; set; }
        public string HOUKOKU_JUUSHO_CD { get; set; }
        public string HOUKOKU_JUUSHO_NAME { get; set; }
        public string CHIIKIBETSU_JUUSHO_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}