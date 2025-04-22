using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_BETSU1_HST : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_NAME { get; set; }
        public SqlInt16 HAIKI_KBN_CD { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public SqlBoolean YUNYU { get; set; }
        public string BUNSEKISHOUMEISHO_TEIJIJIKI { get; set; }
    }
}