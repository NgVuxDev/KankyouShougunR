using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class CHANGE_LOG_M_COURSE_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt16 DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public SqlInt32 REC_ID { get; set; }
        public SqlInt32 ROW_NO { get; set; }
        public SqlInt32 ROUND_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string KIBOU_TIME { get; set; }
        public SqlInt16 SAGYOU_TIME_MINUTE { get; set; }
        public string BIKOU { get; set; }
    }
}