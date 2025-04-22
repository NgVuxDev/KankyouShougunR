using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHARYOU : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_NAME { get; set; }
        public string SHARYOU_NAME_RYAKU { get; set; }
        public string SHASYU_CD { get; set; }
        public string SHAIN_CD { get; set; }
        public SqlDecimal SAIDAI_SEKISAI { get; set; }
        public SqlDecimal KUUSHA_JYURYO { get; set; }
        public string SHARYOU_BIKOU { get; set; }
        public SqlDecimal SAIDAI_WAKU_SUU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlBoolean HAISHA_WARIATE { get; set; }
        public SqlBoolean KEIRYOU_GYOUSHA_SET_KBN { get; set; }
    }
}