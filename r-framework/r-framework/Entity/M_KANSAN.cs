using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KANSAN : SuperEntity
    {
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlInt16 KANSANSHIKI { get; set; }
        public SqlDecimal KANSANCHI { get; set; }
        public string KANSAN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}