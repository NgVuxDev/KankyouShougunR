using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_TEIKI_HAISHA_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 TEIKI_HAISHA_NUMBER { get; set; }
        public SqlInt16 ROW_NUMBER { get; set; }
        public SqlInt32 ROUND_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlDateTime KIBOU_TIME { get; set; }
        public SqlInt16 SAGYOU_TIME_MINUTE { get; set; }
        public string MEISAI_BIKOU { get; set; }
        public SqlInt64 UKETSUKE_NUMBER { get; set; }
    }
}