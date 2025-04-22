using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KOBETSU_HINMEI : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public string SEIKYUU_HINMEI_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}