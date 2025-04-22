using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R05 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal RENRAKU_ID_NO { get; set; }
        public string RENRAKU_ID { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}