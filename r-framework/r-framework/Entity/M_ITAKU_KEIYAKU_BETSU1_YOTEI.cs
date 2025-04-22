using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_BETSU1_YOTEI : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string HAISHUTSU_JIGYOUSHA_CD { get; set; }
        public string HAISHUTSU_JIGYOUJOU_CD { get; set; }
        public string HAISHUTSU_JIGYOUJOU_NAME { get; set; }
        public string HAISHUTSU_JIGYOUJOU_ADDRESS { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public SqlDecimal YOTEI_SUU { get; set; }
        public SqlInt16 YOTEI_SUU_UNIT_CD { get; set; }
        public SqlInt16 YOTEI_KIKAN { get; set; }
        public SqlDecimal ITAKU_RYOUKIN { get; set; }
        public SqlDecimal ITAKU_TANKA { get; set; }
        public SqlDecimal UPN_TANKA { get; set; }
        public SqlDecimal SBN_TANKA { get; set; }
        public SqlInt16 ITAKU_RYOUKIN_UNIT_CD { get; set; }
    }
}