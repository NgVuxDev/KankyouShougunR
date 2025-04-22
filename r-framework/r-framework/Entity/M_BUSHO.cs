using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_BUSHO : SuperEntity
    {
        public string BUSHO_CD { get; set; }
        public string BUSHO_NAME { get; set; }
        public string BUSHO_NAME_RYAKU { get; set; }
        public string BUSHO_FURIGANA { get; set; }
        public string BUSHO_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}