using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENBAMEMO_BUNRUI : SuperEntity
    {
        public string GENBAMEMO_BUNRUI_CD { get; set; }
        public string GENBAMEMO_BUNRUI_NAME { get; set; }
        public string GENBAMEMO_BUNRUI_NAME_RYAKU { get; set; }
        public string GENBAMEMO_BUNRUI_FURIGANA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}