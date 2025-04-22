using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class QUE_INFO : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal QUE_SEQ { get; set; }
        public SqlDecimal SEQ { get; set; }
        public string REQUEST_CODE { get; set; }
        public SqlDecimal EDI_RECORD_ID { get; set; }
        public string FUNCTION_ID { get; set; }
        public SqlDecimal UPN_ROUTE_NO { get; set; }
        public SqlDecimal TUUCHI_ID { get; set; }
        public SqlDecimal STATUS_FLAG { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public SqlDecimal TRF_STATUS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}