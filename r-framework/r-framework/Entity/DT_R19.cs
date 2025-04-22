using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Seasar.Dao.Attrs.TimestampProperty("UPDATE_TS")]
    public class DT_R19 : SuperEntity
    {
        public string KANRI_ID { get; set; }
        public SqlDecimal SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public SqlDecimal UPN_ROUTE_NO { get; set; }
        public string UPN_SHA_EDI_MEMBER_ID { get; set; }
        public string UPN_SHA_NAME { get; set; }
        public string UPN_SHA_POST { get; set; }
        public string UPN_SHA_ADDRESS1 { get; set; }
        public string UPN_SHA_ADDRESS2 { get; set; }
        public string UPN_SHA_ADDRESS3 { get; set; }
        public string UPN_SHA_ADDRESS4 { get; set; }
        public string UPN_SHA_TEL { get; set; }
        public string UPN_SHA_FAX { get; set; }
        public string UPN_SHA_KYOKA_ID { get; set; }
        public string SAI_UPN_SHA_EDI_MEMBER_ID { get; set; }
        public string SAI_UPN_SHA_NAME { get; set; }
        public string SAI_UPN_SHA_POST { get; set; }
        public string SAI_UPN_SHA_ADDRESS1 { get; set; }
        public string SAI_UPN_SHA_ADDRESS2 { get; set; }
        public string SAI_UPN_SHA_ADDRESS3 { get; set; }
        public string SAI_UPN_SHA_ADDRESS4 { get; set; }
        public string SAI_UPN_SHA_TEL { get; set; }
        public string SAI_UPN_SHA_FAX { get; set; }
        public string SAI_UPN_SHA_KYOKA_ID { get; set; }
        public string UPN_WAY_CODE { get; set; }
        public string UPN_TAN_NAME { get; set; }
        public string CAR_NO { get; set; }
        public string UPNSAKI_EDI_MEMBER_ID { get; set; }
        public string UPNSAKI_NAME { get; set; }
        public SqlDecimal UPNSAKI_JOU_ID { get; set; }
        public SqlDecimal UPNSAKI_JOU_KBN { get; set; }
        public string UPNSAKI_JOU_NAME { get; set; }
        public string UPNSAKI_JOU_POST { get; set; }
        public string UPNSAKI_JOU_ADDRESS1 { get; set; }
        public string UPNSAKI_JOU_ADDRESS2 { get; set; }
        public string UPNSAKI_JOU_ADDRESS3 { get; set; }
        public string UPNSAKI_JOU_ADDRESS4 { get; set; }
        public string UPNSAKI_JOU_TEL { get; set; }
        public SqlDecimal UPN_SHOUNIN_FLAG { get; set; }
        public string UPN_END_DATE { get; set; }
        public string UPNREP_UPN_TAN_NAME { get; set; }
        public string UPNREP_CAR_NO { get; set; }
        public SqlDecimal UPN_SUU { get; set; }
        public string UPN_UNIT_CODE { get; set; }
        public SqlDecimal YUUKA_SUU { get; set; }
        public string YUUKA_UNIT_CODE { get; set; }
        public string REP_TAN_NAME { get; set; }
        public string BIKOU { get; set; }
        public SqlDateTime UPDATE_TS { get; set; }
        public string SEARCH_UPDATE_TS { get; set; }
    }
}