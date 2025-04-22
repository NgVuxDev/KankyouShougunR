using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MOBISYO_RT_KADOUJYOUKYOU : SuperEntity
    {
        public string ID { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public SqlDateTime ICHI_UPDATE_DATE { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
    }
}