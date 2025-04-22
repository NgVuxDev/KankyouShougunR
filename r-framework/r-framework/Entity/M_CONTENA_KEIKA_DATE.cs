using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CONTENA_KEIKA_DATE : SuperEntity
    {
        public SqlInt16 CONTENA_KEIKA_DATE { get; set; }
        public SqlInt32 CONTENA_KEIKA_BACK_COLOR { get; set; }
        public string CONTENA_KEIKA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}