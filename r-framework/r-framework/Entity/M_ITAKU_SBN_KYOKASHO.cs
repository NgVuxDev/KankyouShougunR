using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_SBN_KYOKASHO : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string CHIIKI_CD { get; set; }
        public SqlInt16 KYOKA_KBN { get; set; }
        public string KYOKA_NO { get; set; }
    }
}