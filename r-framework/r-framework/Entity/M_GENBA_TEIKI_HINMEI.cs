using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENBA_TEIKI_HINMEI : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal KANSANCHI { get; set; }
        public SqlInt16 KANSAN_UNIT_CD { get; set; }
        public SqlBoolean KANSAN_UNIT_MOBILE_OUTPUT_FLG { get; set; }
        public SqlBoolean MONDAY { get; set; }
        public SqlBoolean TUESDAY { get; set; }
        public SqlBoolean WEDNESDAY { get; set; }
        public SqlBoolean THURSDAY { get; set; }
        public SqlBoolean FRIDAY { get; set; }
        public SqlBoolean SATURDAY { get; set; }
        public SqlBoolean SUNDAY { get; set; }
        public SqlInt16 KEIYAKU_KBN { get; set; }
        public string TSUKI_HINMEI_CD { get; set; }
        public SqlInt16 KEIJYOU_KBN { get; set; }
        public SqlBoolean ANBUN_FLG { get; set; }
    }
}