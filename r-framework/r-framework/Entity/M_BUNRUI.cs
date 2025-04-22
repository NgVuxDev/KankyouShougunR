using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_BUNRUI : SuperEntity
    {
        public string BUNRUI_CD { get; set; }
        public string BUNRUI_NAME { get; set; }
        public string BUNRUI_NAME_RYAKU { get; set; }
        public string BUNRUI_FURIGANA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}