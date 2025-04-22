using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CONTENA_JOUKYOU : SuperEntity
    {
        public SqlInt16 CONTENA_JOUKYOU_CD { get; set; }
        public string CONTENA_JOUKYOU_NAME { get; set; }
        public string CONTENA_JOUKYOU_NAME_RYAKU { get; set; }
        public string CONTENA_JOUKYOU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}