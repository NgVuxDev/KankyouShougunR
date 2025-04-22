using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_PT_R19 : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
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
        public string UPN_GYOUSHA_CD { get; set; }
        public string NO_REP_UPN_EDI_MEMBER_ID { get; set; }
        public string UPNSAKI_GYOUSHA_CD { get; set; }
        public string NO_REP_UPNSAKI_EDI_MEMBER_ID { get; set; }
        public string UPNSAKI_GENBA_CD { get; set; }
        public string UPN_TANTOUSHA_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string UPNREP_UPN_TANTOUSHA_CD { get; set; }
        public string UPNREP_SHARYOU_CD { get; set; }
        public string HOUKOKU_TANTOUSHA_CD { get; set; }
    }
}