using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENBA_DIGI : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string POINT_ID { get; set; }
        public string POINT_NAME { get; set; }
        public string POINT_KANA_NAME { get; set; }
        public string MAP_NAME { get; set; }
        public string POST_CODE { get; set; }
        public SqlInt16 GENBA_TODOUFUKEN_CD { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string TEL_NO { get; set; }
        public string FAX_NO { get; set; }
        public string CONTACT_NAME { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public SqlInt32 RANGE_RADIUS { get; set; }
        public string REMARKS { get; set; }
        public string UNTENSHA_SHIJI_JIKOU1 { get; set; }
        public string UNTENSHA_SHIJI_JIKOU2 { get; set; }
        public string UNTENSHA_SHIJI_JIKOU3 { get; set; }
        public string KEITAI_TEL { get; set; }
        public string GENBA_NAME1 { get; set; }
        public string GENBA_NAME2 { get; set; }
    }
}
