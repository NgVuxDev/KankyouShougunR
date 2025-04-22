using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_BUNRUI : SuperEntity
    {
        public string CHIIKI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HOUKOKU_BUNRUI_CD { get; set; }
        public string HOUKOKU_BUNRUI_NAME { get; set; }
        public SqlBoolean SEKIMEN_KBN { get; set; }
        public SqlBoolean TOKUTEI_YUUGAI_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}