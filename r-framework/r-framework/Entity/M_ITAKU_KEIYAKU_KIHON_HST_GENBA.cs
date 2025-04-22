using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_KIHON_HST_GENBA : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string HAISHUTSU_JIGYOUSHA_CD { get; set; }
        public string HAISHUTSU_JIGYOUJOU_CD { get; set; }
        public string HAISHUTSU_JIGYOUJOU_NAME { get; set; }
        public string HAISHUTSU_JIGYOUJOU_ADDRESS { get; set; }
        public string HAISHUTSU_JIGYOUJOU_ADDRESS1 { get; set; }
        public string HAISHUTSU_JIGYOUJOU_ADDRESS2 { get; set; }
    }
}