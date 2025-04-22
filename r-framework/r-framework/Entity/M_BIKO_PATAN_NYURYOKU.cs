using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_BIKO_PATAN_NYURYOKU : SuperEntity
    {
        public string BIKO_CD { get; set; }
        public string BIKO_NAME { get; set; }
        public string BIKO_NAME_RYAKU { get; set; }
        public string BIKO_FURIGANA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}