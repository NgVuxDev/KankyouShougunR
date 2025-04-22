using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_NYUUKIN_KESHIKOMI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt64 SEIKYUU_NUMBER { get; set; }
        public SqlInt32 KAGAMI_NUMBER { get; set; }
        public SqlInt32 KESHIKOMI_SEQ { get; set; }
        public SqlInt64 NYUUKIN_NUMBER { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlDecimal KESHIKOMI_GAKU { get; set; }
        public string KESHIKOMI_BIKOU { get; set; }
        public SqlInt32 NYUUKIN_SEQ { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}