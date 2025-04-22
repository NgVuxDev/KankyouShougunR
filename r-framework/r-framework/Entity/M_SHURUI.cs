using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHURUI : SuperEntity
    {
        public string SHURUI_CD { get; set; }
        public string SHURUI_NAME { get; set; }
        public string SHURUI_NAME_RYAKU { get; set; }
        public string SHURUI_FURIGANA { get; set; }
        public string SHURUI_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}