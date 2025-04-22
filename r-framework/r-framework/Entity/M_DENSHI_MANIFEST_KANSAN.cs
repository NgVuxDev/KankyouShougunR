using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_MANIFEST_KANSAN : SuperEntity
    {
        public string EDI_MEMBER_ID { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_SHURUI_SAIBUNRUI_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlInt16 KANSANSHIKI { get; set; }
        public SqlDecimal KANSANCHI { get; set; }
        public string MANIFEST_KANSAN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}