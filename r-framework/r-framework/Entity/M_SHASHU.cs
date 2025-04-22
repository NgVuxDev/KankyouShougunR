using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHASHU : SuperEntity
    {
        public string SHASHU_CD { get; set; }
        public string SHASHU_NAME { get; set; }
        public string SHASHU_NAME_RYAKU { get; set; }
        public string SHASHU_FURIGANA { get; set; }
        public string SHASHU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}