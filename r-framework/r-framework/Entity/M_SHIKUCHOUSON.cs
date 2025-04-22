using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SHIKUCHOUSON : SuperEntity
    {
        public string SHIKUCHOUSON_CD { get; set; }
        public string SHIKUCHOUSON_NAME { get; set; }
        public string SHIKUCHOUSON_NAME_RYAKU { get; set; }
        public string SHIKUCHOUSON_FURIGANA { get; set; }
        public string SHIKUCHOUSON_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}