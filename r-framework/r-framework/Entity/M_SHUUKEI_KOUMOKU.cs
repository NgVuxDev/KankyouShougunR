using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHUUKEI_KOUMOKU : SuperEntity
    {
        public string SHUUKEI_KOUMOKU_CD { get; set; }
        public string SHUUKEI_KOUMOKU_NAME { get; set; }
        public string SHUUKEI_KOUMOKU_NAME_RYAKU { get; set; }
        public string SHUUKEI_KOUMOKU_FURIGANA { get; set; }
        public string SHUUKEI_KOUMOKU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}