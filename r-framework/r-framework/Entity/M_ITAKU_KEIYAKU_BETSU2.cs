using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_BETSU2 : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public string UNPAN_GYOUSHA_ADDRESS { get; set; }
        public string UNPAN_GYOUSHA_ADDRESS1 { get; set; }
        public string UNPAN_GYOUSHA_ADDRESS2 { get; set; }
        public SqlInt32 KYOKA_SHARYOU_SUU { get; set; }
    }
}