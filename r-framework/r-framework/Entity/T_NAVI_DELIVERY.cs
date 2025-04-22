using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NAVI_DELIVERY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt16 SAITEKIKA_TAISHO { get; set; }
        public SqlInt16 DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public SqlInt64 TEIKI_SYSTEM_ID { get; set; }
        public SqlInt32 TEIKI_SEQ { get; set; }
        public SqlDateTime DELIVERY_DATE { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string SAGYOUSHA_CD { get; set; }
        public string SHUPPATSU_GYOUSHA_CD { get; set; }
        public string SHUPPATSU_GENBA_CD { get; set; }
        public string SHUPPATSU_EIGYOUSHO_CD { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_EIGYOUSHO_CD { get; set; }
        public string SHITEI_SHARYOU_TYPE { get; set; }
        public SqlInt16 NAVI_DELIVERY_ORDER { get; set; }
        public SqlInt16 TRAFFIC_CONSIDERATION { get; set; }
        public SqlInt16 SMART_IC_CONSIDERATION { get; set; }
        public SqlInt16 PRIORITY { get; set; }
        public SqlInt32 BIN_NO { get; set; }
        public SqlInt64 MATTER_CODE_BEGIN { get; set; }
        public SqlInt64 MATTER_CODE_END { get; set; }
        public SqlDateTime DEPARTURE_TIME { get; set; }
        public SqlDateTime ARRIVAL_TIME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
