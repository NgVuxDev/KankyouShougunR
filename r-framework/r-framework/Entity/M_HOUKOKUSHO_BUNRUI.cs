using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_HOUKOKUSHO_BUNRUI : SuperEntity
    {
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_NAME { get; set; }
        public string HOUKOKUSHO_BUNRUI_NAME_RYAKU { get; set; }
        public string HOUKOKUSHO_BUNRUI_FURIGANA { get; set; }
        public string HOUKOKUSHO_BUNRUI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}