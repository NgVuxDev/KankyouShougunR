using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_D13 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal D12_SEQ { get; set; }
        public SqlDecimal D13_SEQ { get; set; }
        public string LAST_SBN_JOU_NAME { get; set; }
        public string LAST_SBN_JOU_POST { get; set; }
        public string LAST_SBN_JOU_ADDRESS1 { get; set; }
        public string LAST_SBN_JOU_ADDRESS2 { get; set; }
        public string LAST_SBN_JOU_ADDRESS3 { get; set; }
        public string LAST_SBN_JOU_ADDRESS4 { get; set; }
        public string LAST_SBN_JOU_TEL { get; set; }
        public string LAST_SBN_END_DATE { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}