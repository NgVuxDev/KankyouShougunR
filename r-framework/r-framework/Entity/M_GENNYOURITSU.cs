using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENNYOURITSU : SuperEntity
    {
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HAIKI_NAME_CD { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public SqlDecimal GENNYOURITSU { get; set; }
        public string GENNYOURITSU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}