using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_JIGYOUJOU : SuperEntity
    {
        public string EDI_MEMBER_ID { get; set; }
        public string JIGYOUJOU_CD { get; set; }
        public string JWNET_JIGYOUJOU_CD { get; set; }
        public SqlInt16 JIGYOUSHA_KBN { get; set; }
        public SqlInt16 JIGYOUJOU_KBN { get; set; }
        public string JIGYOUJOU_NAME { get; set; }
        public string JIGYOUJOU_POST { get; set; }
        public string JIGYOUJOU_ADDRESS1 { get; set; }
        public string JIGYOUJOU_ADDRESS2 { get; set; }
        public string JIGYOUJOU_ADDRESS3 { get; set; }
        public string JIGYOUJOU_ADDRESS4 { get; set; }
        public string JIGYOUJOU_TEL { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
    }
}