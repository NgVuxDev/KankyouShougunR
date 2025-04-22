using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_COURSE : SuperEntity
    {
        public SqlInt16 DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public string COURSE_BIKOU { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlInt16 SAGYOU_BEGIN_HOUR { get; set; }
        public SqlInt16 SAGYOU_BEGIN_MINUTE { get; set; }
        public SqlInt16 SAGYOU_END_HOUR { get; set; }
        public SqlInt16 SAGYOU_END_MINUTE { get; set; }
        public string SHUPPATSU_GYOUSHA_CD { get; set; }
        public string SHUPPATSU_GENBA_CD { get; set; }
    }
}