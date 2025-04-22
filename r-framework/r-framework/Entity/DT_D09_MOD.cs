using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_D09_MOD : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal UPN_ROUTE_NO { get; set; }
        public string UPN_END_DATE { get; set; }
        public string CAR_NO { get; set; }
        public SqlDecimal UPN_SUU { get; set; }
        public string UPN_SUU_CODE { get; set; }
        public SqlDecimal YUUKA_SUU { get; set; }
        public string YUUKA_UNIT_CODE { get; set; }
        public string REP_TAN_NAME { get; set; }
        public string UPN_TAN_NAME { get; set; }
        public string BIKOU { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}