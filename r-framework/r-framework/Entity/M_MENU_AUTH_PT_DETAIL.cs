using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_MENU_AUTH_PT_DETAIL : SuperEntity
    {
        public SqlInt64 PATTERN_ID { get; set; }
        public string FORM_ID { get; set; }
        public SqlInt32 WINDOW_ID { get; set; }
        public SqlBoolean AUTH_READ { get; set; }
        public SqlBoolean AUTH_ADD { get; set; }
        public SqlBoolean AUTH_EDIT { get; set; }
        public SqlBoolean AUTH_DELETE { get; set; }
        public string BIKOU { get; set; }
    }
}
