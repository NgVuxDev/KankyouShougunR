using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HAIKI_SHURUI : SuperEntity
    {
        public SqlInt16 HAIKI_KBN_CD { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public string HAIKI_SHURUI_NAME { get; set; }
        public string HAIKI_SHURUI_NAME_RYAKU { get; set; }
        public string HAIKI_SHURUI_FURIGANA { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HAIKI_SHURUI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}