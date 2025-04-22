using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R24 : SuperEntity
    {
        public SqlDecimal TUUCHI_ID { get; set; }
        public string MEMBER_ID { get; set; }
        public SqlDecimal TUUCHI_CODE { get; set; }
        public string TUUCHI_STATUS { get; set; }
        public string TUUCHI_DATE { get; set; }
        public string TUUCHI_TIME { get; set; }
        public string MANIFEST_ID { get; set; }
        public string RENRAKU_ID1 { get; set; }
        public string RENRAKU_ID2 { get; set; }
        public string RENRAKU_ID3 { get; set; }
        public string HIKIWATASHI_DATE { get; set; }
        public string HST_JOU_NAME { get; set; }
        public string END_DATE { get; set; }
        public SqlDecimal UPN_ROUTE_NO { get; set; }
        public string BIKOU { get; set; }
        public string ACTION_FLAG { get; set; }
        public SqlDecimal READ_FLAG { get; set; }
        public SqlDecimal IMPORT_FLAG { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}