using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_HINMEI : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string HINMEI_CD { get; set; }
        public string HINMEI_NAME { get; set; }
        public SqlBoolean TSUMIKAE { get; set; }
        public SqlBoolean YUNYU { get; set; }
        public string BUNSEKISHOUMEISHO_TEIJIJIKI { get; set; }
        public SqlDecimal UNPAN_YOTEI_SUU { get; set; }
        public SqlInt16 UNPAN_YOTEI_SUU_UNIT_CD { get; set; }
        public SqlDecimal UNPAN_ITAKU_TANKA { get; set; }
        public SqlDecimal SHOBUN_YOTEI_SUU { get; set; }
        public SqlInt16 SHOBUN_YOTEI_SUU_UNIT_CD { get; set; }
        public SqlDecimal SHOBUN_ITAKU_TANKA { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHISETSU_CAPACITY { get; set; }
        public string SHOBUN_JUTAKUSHA_CD { get; set; }
        public string SHOBUN_JUTAKUSHA_NAME { get; set; }
        public string SHOBUN_JIGYOUJOU_CD { get; set; }
        public string SHOBUN_JIGYOUJOU_NAME { get; set; }
        public string SHOBUN_JIGYOUJOU_ADDRESS1 { get; set; }
        public string SHOBUN_JIGYOUJOU_ADDRESS2 { get; set; }
    }
}