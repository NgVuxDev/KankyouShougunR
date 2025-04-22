using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GYOUSHU : SuperEntity
    {
        public string GYOUSHU_CD { get; set; }
        public string GYOUSHU_NAME { get; set; }
        public string GYOUSHU_NAME_RYAKU { get; set; }
        public string GYOUSHU_FURIGANA { get; set; }
        public string GYOUSHU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}