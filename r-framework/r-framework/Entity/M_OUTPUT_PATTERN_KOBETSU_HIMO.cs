using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_PATTERN_KOBETSU_HIMO : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlBoolean DEFAULT_KBN { get; set; }
        public SqlInt16 DISP_NUMBER { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}