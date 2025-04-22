using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_HAIKI_SHURUI : SuperEntity
    {
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_SHURUI_NAME { get; set; }
        public SqlInt16 HAIKI_BUNRUI { get; set; }
        public SqlInt16 HAIKI_KBN { get; set; }
        public SqlBoolean KONGOU_KBN { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}