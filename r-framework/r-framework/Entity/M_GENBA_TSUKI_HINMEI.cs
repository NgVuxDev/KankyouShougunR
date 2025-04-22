using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENBA_TSUKI_HINMEI : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal TANKA { get; set; }
        public SqlBoolean TEIKI_JISSEKI_NO_SEIKYUU_KBN { get; set; }
        public SqlBoolean CHOUKA_SETTING { get; set; }
        public SqlDecimal CHOUKA_LIMIT_AMOUNT { get; set; }
        public string CHOUKA_HINMEI_NAME { get; set; }
    }
}