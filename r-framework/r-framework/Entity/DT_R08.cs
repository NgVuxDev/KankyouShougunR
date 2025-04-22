using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R08 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public SqlDecimal REC_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal MEDIA_TYPE { get; set; }
        public string FIRST_MANIFEST_ID { get; set; }
        public string RENRAKU_ID { get; set; }
        public string KOUHU_DATE { get; set; }
        public string SBN_END_DATE { get; set; }
        public string HST_SHA_NAME { get; set; }
        public string HST_JOU_NAME { get; set; }
        public string HAIKI_SHURUI { get; set; }
        public SqlDecimal HAIKI_SUU { get; set; }
        public string HAIKI_SUU_UNIT { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}