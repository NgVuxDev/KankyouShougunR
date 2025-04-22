using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_R19_EX : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string KANRI_ID { get; set; }
        public SqlDecimal UPN_ROUTE_NO { get; set; }
        public string MANIFEST_ID { get; set; }
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
        public SqlBoolean DELETE_FLG { get; set; }
    }
}