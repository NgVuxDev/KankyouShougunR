using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_BETSU3 : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string SHOBUN_GYOUSHA_CD { get; set; }
        public string SHOBUN_GYOUSHA_NAME { get; set; }
        public string SHOBUN_GYOUSHA_ADDRESS { get; set; }
        public string SHOBUN_GYOUSHA_ADDRESS1 { get; set; }
        public string SHOBUN_GYOUSHA_ADDRESS2 { get; set; }
        public string SHOBUN_JIGYOUJOU_CD { get; set; }
        public string SHOBUN_JIGYOUJOU_NAME { get; set; }
        public string SHOBUN_JIGYOUJOU_ADDRESS { get; set; }
        public string SHOBUN_JIGYOUJOU_ADDRESS1 { get; set; }
        public string SHOBUN_JIGYOUJOU_ADDRESS2 { get; set; }        
        public string SHOBUN_HOUHOU_CD { get; set; }
        public SqlDecimal HOKAN_JOGEN { get; set; }
        public SqlInt16 HOKAN_JOGEN_UNIT_CD { get; set; }
        public string SHISETSU_CAPACITY { get; set; }
        public SqlInt16 UNPAN_FROM { get; set; }
        public SqlInt16 UNPAN_END { get; set; }
        public SqlInt16 KONGOU { get; set; }
        public SqlInt16 SHUSENBETU { get; set; }
    }
}