using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DIGI_OUTPUT_SHARYOU : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string DIGI_SHARYOU_CD { get; set; }
    }
}