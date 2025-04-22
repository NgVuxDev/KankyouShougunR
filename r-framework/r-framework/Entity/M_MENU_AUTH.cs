using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_MENU_AUTH : SuperEntity
    {
        public string BUSHO_CD { get; set; }
        public string SHAIN_CD { get; set; }
        public string FORM_ID { get; set; }
        public SqlInt32 WINDOW_ID { get; set; }
        public SqlBoolean AUTH_READ { get; set; }
        public SqlBoolean AUTH_ADD { get; set; }
        public SqlBoolean AUTH_EDIT { get; set; }
        public SqlBoolean AUTH_DELETE { get; set; }
        public string BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}