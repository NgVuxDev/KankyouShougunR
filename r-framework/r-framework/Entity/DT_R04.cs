using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R04 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public SqlDecimal REC_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public string LAST_SBN_JOU_NAME { get; set; }
        public string LAST_SBN_JOU_POST { get; set; }
        public string LAST_SBN_JOU_ADDRESS1 { get; set; }
        public string LAST_SBN_JOU_ADDRESS2 { get; set; }
        public string LAST_SBN_JOU_ADDRESS3 { get; set; }
        public string LAST_SBN_JOU_ADDRESS4 { get; set; }
        public string LAST_SBN_JOU_TEL { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}