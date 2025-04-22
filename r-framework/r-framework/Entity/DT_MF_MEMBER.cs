using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_MF_MEMBER : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public string HST_MEMBER_ID { get; set; }
        public string UPN1_MEMBER_ID { get; set; }
        public string UPN2_MEMBER_ID { get; set; }
        public string UPN3_MEMBER_ID { get; set; }
        public string UPN4_MEMBER_ID { get; set; }
        public string UPN5_MEMBER_ID { get; set; }
        public string SBN_MEMBER_ID { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}