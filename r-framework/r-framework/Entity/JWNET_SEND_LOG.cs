using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class JWNET_SEND_LOG : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal QUE_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public string FUNCTION_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public string ERROR_CODE { get; set; }
        public SqlDecimal READ_FLAG { get; set; }
        public string HR1_CREATE_DAY { get; set; }
        public string HR1_CREATE_TIME { get; set; }
    }
}