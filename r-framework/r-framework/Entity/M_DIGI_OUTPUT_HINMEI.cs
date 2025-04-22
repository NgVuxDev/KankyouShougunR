using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DIGI_OUTPUT_HINMEI : SuperEntity
    {
        public string HINMEI_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string DIGI_HINMEI_CD { get; set; }
    }
}