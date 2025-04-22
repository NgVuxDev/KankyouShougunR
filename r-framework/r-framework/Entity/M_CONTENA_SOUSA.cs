using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CONTENA_SOUSA : SuperEntity
    {
        public SqlInt16 CONTENA_SOUSA_CD { get; set; }
        public string CONTENA_SOUSA_NAME { get; set; }
        public string CONTENA_SOUSA_NAME_RYAKU { get; set; }
        public string CONTENA_SOUSA_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}