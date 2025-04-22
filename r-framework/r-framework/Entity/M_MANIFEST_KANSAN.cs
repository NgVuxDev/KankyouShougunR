using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_MANIFEST_KANSAN : SuperEntity
    {
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HAIKI_NAME_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public string NISUGATA_CD { get; set; }
        public SqlInt16 KANSANSHIKI { get; set; }
        public SqlDecimal KANSANCHI { get; set; }
        public string MANIFEST_KANSAN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}