using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_JIGYOUSHA : SuperEntity
    {
        public string EDI_MEMBER_ID { get; set; }
        public string EDI_PASSWORD { get; set; }
        public string JIGYOUSHA_NAME { get; set; }
        public string JIGYOUSHA_POST { get; set; }
        public string JIGYOUSHA_ADDRESS1 { get; set; }
        public string JIGYOUSHA_ADDRESS2 { get; set; }
        public string JIGYOUSHA_ADDRESS3 { get; set; }
        public string JIGYOUSHA_ADDRESS4 { get; set; }
        public string JIGYOUSHA_TEL { get; set; }
        public string JIGYOUSHA_FAX { get; set; }
        public SqlBoolean HST_KBN { get; set; }
        public SqlBoolean UPN_KBN { get; set; }
        public SqlBoolean SBN_KBN { get; set; }
        public SqlBoolean HOUKOKU_HUYOU_KBN { get; set; }
        public string GYOUSHA_CD { get; set; }
    }
}