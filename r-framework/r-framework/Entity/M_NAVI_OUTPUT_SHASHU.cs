using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NAVI_OUTPUT_SHASHU : SuperEntity
    {
        public string SHASHU_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string NAVI_SHASHU_CD { get; set; }
    }
}