using System.Data.SqlTypes;

namespace r_framework.Entity
{

    public class T_LOGI_DELIVERY_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt32 DELIVERY_NO { get; set; }
        public SqlInt32 DELIVERY_ORDER { get; set; }
        public SqlBoolean NOT_URIAGE_KBN { get; set; }
        public SqlInt16 DENPYOU_ATTR { get; set; }
        public SqlInt64 REF_SYSTEM_ID { get; set; }
        public SqlInt64 REF_DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 REF_DENPYOU_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public SqlBoolean NIZUMI_NIOROSHI_FLG { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
