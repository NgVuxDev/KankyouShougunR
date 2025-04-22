using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHOBUN_MOKUTEKI : SuperEntity
    {
        public string SHOBUN_MOKUTEKI_CD { get; set; }
        public string SHOBUN_MOKUTEKI_NAME { get; set; }
        public string SHOBUN_MOKUTEKI_NAME_RYAKU { get; set; }
        public string SHOBUN_MOKUTEKI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}