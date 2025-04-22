using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ITAKU_KEIYAKU_BETSU4 : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string LAST_SHOBUN_GYOUSHA_CD { get; set; }
        public string LAST_SHOBUN_GYOUSHA_NAME { get; set; }
        public string LAST_SHOBUN_GYOUSHA_ADDRESS { get; set; }
        public string LAST_SHOBUN_GYOUSHA_ADDRESS1 { get; set; }
        public string LAST_SHOBUN_GYOUSHA_ADDRESS2 { get; set; }
        public string LAST_SHOBUN_JIGYOUJOU_CD { get; set; }
        public string LAST_SHOBUN_JIGYOUJOU_NAME { get; set; }
        public string LAST_SHOBUN_JIGYOUJOU_ADDRESS { get; set; }
        public string LAST_SHOBUN_JIGYOUJOU_ADDRESS1 { get; set; }
        public string LAST_SHOBUN_JIGYOUJOU_ADDRESS2 { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHORI_SPEC { get; set; }
        public string OTHER { get; set; }
        public SqlInt16 BUNRUI { get; set; }
        public SqlInt16 END_KUBUN { get; set; }
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        public string HOUKOKUSHO_BUNRUI_NAME { get; set; }
        public string SHOBUNSAKI_NO { get; set; }
    }
}