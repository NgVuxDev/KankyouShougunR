using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEIKYUU_DENPYOU_KAGAMI_USER_INXS : SuperEntity
    {
        public SqlInt64 SEIKYUU_NUMBER { get; set; }
        public SqlInt32 KAGAMI_NUMBER { get; set; }
        public SqlInt64 USER_SYS_ID { get; set; }
    }
}
