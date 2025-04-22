using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CHIIKIBETSU_KYOKA_MEIGARA : SuperEntity
    {
        public SqlInt16 KYOKA_KBN { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string CHIIKI_CD { get; set; }
        public SqlBoolean TOKUBETSU_KANRI_KBN { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public SqlBoolean TSUMIKAE { get; set; }
    }
}