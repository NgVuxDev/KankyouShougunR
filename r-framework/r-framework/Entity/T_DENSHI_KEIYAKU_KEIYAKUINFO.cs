using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_KEIYAKUINFO : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string DENSHI_KEIYAKU_SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string HAISHUTSU_JIGYOUJOU_CD { get; set; }
        public string HAISHUTSU_JIGYOUJOU_NAME { get; set; }
        public string TODOUFUKEN_NAME { get; set; }
        public string HAISHUTSU_JIGYOUJOU_ADDRESS1 { get; set; }
        public string HAISHUTSU_JIGYOUJOU_ADDRESS2 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
