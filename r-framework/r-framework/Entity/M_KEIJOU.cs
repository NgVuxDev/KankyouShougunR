using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KEIJOU : SuperEntity
    {
        public string KEIJOU_CD { get; set; }
        public string KEIJOU_NAME { get; set; }
        public string KEIJOU_NAME_RYAKU { get; set; }
        public string KEIJOU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}