using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_MENU_AUTH_PT_ENTRY : SuperEntity
    {
        public SqlInt64 PATTERN_ID { get; set; }
        public string PATTERN_NAME { get; set; }
        public string PATTERN_FURIGANA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
