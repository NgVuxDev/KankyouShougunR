using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_FILE_LINK_CHIIKIBETSU_KYOKA : SuperEntity
    {
        public SqlInt16 KYOKA_KBN { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string CHIIKI_CD { get; set; }
        public SqlInt64 FILE_ID { get; set; }
    }
}
