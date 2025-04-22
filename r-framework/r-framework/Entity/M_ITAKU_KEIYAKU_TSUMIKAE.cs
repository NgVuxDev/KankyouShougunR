using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_TSUMIKAE : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public string TSUMIKAE_HOKANBA_CD { get; set; }
        public string TSUMIKAE_HOKANBA_NAME { get; set; }
        public string TSUMIKAE_HOKANBA_ADDRESS1 { get; set; }
        public string TSUMIKAE_HOKANBA_ADDRESS2 { get; set; }
        public SqlDecimal HOKAN_JOGEN { get; set; }
        public SqlInt16 HOKAN_JOGEN_UNIT_CD { get; set; }
        public SqlInt16 KONGOU { get; set; }
        public SqlInt16 SHUSENBETU { get; set; }
        public SqlInt16 UNPAN_FROM { get; set; }
        public SqlInt16 UNPAN_TO { get; set; }
    }
}