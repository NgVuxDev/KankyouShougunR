using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_PT_UPN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 UPN_ROUTE_NO { get; set; }
        public string UPN_GYOUSHA_CD { get; set; }
        public string UPN_GYOUSHA_NAME { get; set; }
        public string UPN_GYOUSHA_POST { get; set; }
        public string UPN_GYOUSHA_TEL { get; set; }
        public string UPN_GYOUSHA_ADDRESS { get; set; }
        public string UPN_HOUHOU_CD { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public SqlInt16 TMH_KBN { get; set; }
        public SqlInt16 UPN_SAKI_KBN { get; set; }
        public string UPN_SAKI_GYOUSHA_CD { get; set; }
        public string UPN_SAKI_GENBA_CD { get; set; }
        public string UPN_SAKI_GENBA_NAME { get; set; }
        public string UPN_SAKI_GENBA_POST { get; set; }
        public string UPN_SAKI_GENBA_TEL { get; set; }
        public string UPN_SAKI_GENBA_ADDRESS { get; set; }
        public string UPN_JYUTAKUSHA_CD { get; set; }
        public string UPN_JYUTAKUSHA_NAME { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNTENSHA_NAME { get; set; }
        public SqlDateTime UPN_END_DATE { get; set; }
        public string SEARCH_UPN_END_DATE { get; set; }
        public SqlDecimal YUUKA_SUU { get; set; }
        public SqlInt16 YUUKA_UNIT_CD { get; set; }
    }
}