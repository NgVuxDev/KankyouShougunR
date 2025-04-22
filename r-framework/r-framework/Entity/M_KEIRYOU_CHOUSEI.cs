using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KEIRYOU_CHOUSEI : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal CHOUSEICHI { get; set; }
        public SqlDecimal CHOUSEIWARIAI { get; set; }
        public string KEIRYOU_CHOUSEI_BIKOU { get; set; }

        public SqlBoolean DELETE_FLG { get; set; }
    }
}