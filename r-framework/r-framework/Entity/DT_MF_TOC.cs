using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_MF_TOC : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal LATEST_SEQ { get; set; }
        public SqlDecimal APPROVAL_SEQ { get; set; }
        public SqlDecimal STATUS_FLAG { get; set; }
        public SqlDecimal STATUS_DETAIL { get; set; }
        public SqlDecimal READ_FLAG { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
        public SqlDecimal KIND { get; set; }
    }
}