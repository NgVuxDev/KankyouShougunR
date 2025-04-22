using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_SAISEIHINM : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string SAISEI_HINMOKU_NAME { get; set; }
        public string BAIKYAKUSAKI_NAME { get; set; }
    }
}