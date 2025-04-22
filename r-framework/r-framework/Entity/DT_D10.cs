using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_D10 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public string SBN_END_DATE { get; set; }
        public string HAIKI_IN_DATE { get; set; }
        public SqlDecimal RECEPT_SUU { get; set; }
        public string RECEPT_UNIT_CODE { get; set; }
        public string UPN_TAN_NAME { get; set; }
        public string CAR_NO { get; set; }
        public string REP_TAN_NAME { get; set; }
        public string SBN_TAN_NAME { get; set; }
        public string BIKOU { get; set; }
        public string REP_KBN { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}