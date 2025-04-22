using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_CONTENA_SHURUI : SuperEntity
    {
        public string CONTENA_SHURUI_CD { get; set; }
        public string CONTENA_SHURUI_NAME { get; set; }
        public string CONTENA_SHURUI_NAME_RYAKU { get; set; }
        public string CONTENA_SHURUI_FURIGANA { get; set; }
        public string CONTENA_SHURUI_BIKOU { get; set; }
        public SqlInt32 SHOYUU_DAISUU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}