using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NAVI_OUTPUT_GENBA : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public SqlInt32 PROCESSING_ID { get; set; }
        public SqlInt32 LINE_NO { get; set; }
        public SqlInt16 STATUS { get; set; }
        public string ERROR_INFO { get; set; }
    }
}