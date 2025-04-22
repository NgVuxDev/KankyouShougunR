using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NAVI_OUTPUT_SHAIN : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string NAVI_SHAIN_CD { get; set; }
        public string NAVI_ROLE { get; set; }
        public string NAVI_PASSWORD { get; set; }
    }
}