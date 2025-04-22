using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KARI_GENBA_TSUKI_HINMEI : SuperEntity
    {
        public SqlBoolean HIKIAI_GYOUSHA_USE_FLG { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal TANKA { get; set; }
        public SqlBoolean TEIKI_JISSEKI_NO_SEIKYUU_KBN { get; set; }
    }
}