using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_UNCHIN_TANKA : SuperEntity
    {
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public string UNCHIN_HINMEI_CD { get; set; }
        public string UNCHIN_HINMEI_NAME { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public string UNIT_NAME { get; set; }
        public SqlDecimal TANKA { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
