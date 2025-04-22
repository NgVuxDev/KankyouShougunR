using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KONGOU_HAIKIBUTSU : SuperEntity
    {
        public SqlInt16 HAIKI_KBN_CD { get; set; }
        public string KONGOU_SHURUI_CD { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public SqlDecimal HAIKI_HIRITSU { get; set; }
        public string KONGOU_HAIKIBUTSU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}