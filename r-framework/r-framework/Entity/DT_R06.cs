using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R06 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal BIKOU_NO { get; set; }
        public string BIKOU { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}