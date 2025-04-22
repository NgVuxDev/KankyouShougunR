using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DIGI_OUTPUT_SHURUI : SuperEntity
    {
        public string SHURUI_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string DIGI_SHURUI_CD { get; set; }
    }
}