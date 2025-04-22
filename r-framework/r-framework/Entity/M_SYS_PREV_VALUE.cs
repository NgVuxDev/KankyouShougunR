using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SYS_PREV_VALUE : SuperEntity
    {
        public string GAMEN_ID { get; set; }
        public string FIELD_NAME { get; set; }
        public string FIELD_VALUE { get; set; }
    }
}