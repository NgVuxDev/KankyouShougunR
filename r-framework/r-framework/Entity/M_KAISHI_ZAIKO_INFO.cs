using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KAISHI_ZAIKO_INFO : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public string ZAIKO_HINMEI_NAME_RYAKU { get; set; }
        public SqlDecimal KAISHI_ZAIKO_RYOU { get; set; }
        public SqlDecimal KAISHI_ZAIKO_KINGAKU { get; set; }
        public SqlDecimal KAISHI_ZAIKO_TANKA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}