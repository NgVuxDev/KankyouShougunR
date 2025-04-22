using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DIGI_OUTPUT_UNIT : SuperEntity
    {
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string DIGI_UNIT_CD { get; set; }
    }
}