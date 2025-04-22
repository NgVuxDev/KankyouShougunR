using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DIGI_OUTPUT_GENBA : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string OUTPUT_USER { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public string OUTPUT_PC { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
    }
}