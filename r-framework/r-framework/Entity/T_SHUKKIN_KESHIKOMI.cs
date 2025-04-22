using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SHUKKIN_KESHIKOMI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt64 SEISAN_NUMBER { get; set; }
        public SqlInt64 KAGAMI_NUMBER { get; set; }
        public SqlInt32 KESHIKOMI_SEQ { get; set; }
        public SqlInt64 SHUKKIN_NUMBER { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlDecimal KESHIKOMI_GAKU { get; set; }
        public string KESHIKOMI_BIKOU { get; set; }
        public SqlInt32 SHUKKIN_SEQ { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}