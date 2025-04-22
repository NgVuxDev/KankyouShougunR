using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_D12 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal D12_SEQ { get; set; }
        public string SCND_MANIFEST_ID { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}