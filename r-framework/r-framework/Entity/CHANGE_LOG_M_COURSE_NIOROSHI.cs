using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class CHANGE_LOG_M_COURSE_NIOROSHI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt16 DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public SqlInt32 NIOROSHI_NO { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
    }
}