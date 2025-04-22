using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_SHAIN_MAX_WINDOW : SuperEntity
    {
        public string SHAIN_CD { get; set; }
        public SqlInt16 MAX_WINDOW_COUNT { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
