using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_COURSE_NAME : SuperEntity
    {
        public string COURSE_NAME_CD { get; set; }
        public string COURSE_NAME { get; set; }
        public string COURSE_NAME_RYAKU { get; set; }
        public string COURSE_NAME_FURIGANA { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlBoolean MONDAY { get; set; }
        public SqlBoolean TUESDAY { get; set; }
        public SqlBoolean WEDNESDAY { get; set; }
        public SqlBoolean THURSDAY { get; set; }
        public SqlBoolean FRIDAY { get; set; }
        public SqlBoolean SATURDAY { get; set; }
        public SqlBoolean SUNDAY { get; set; }
        public string COURSE_NAME_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}