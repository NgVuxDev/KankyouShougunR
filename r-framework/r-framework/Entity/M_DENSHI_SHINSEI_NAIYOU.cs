using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_SHINSEI_NAIYOU : SuperEntity
    {
        public SqlInt16 NAIYOU_CD { get; set; }
        public string NAIYOU_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
