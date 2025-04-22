using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_BANK : SuperEntity
    {
        public string BANK_CD { get; set; }
        public string BANK_NAME { get; set; }
        public string BANK_NAME_RYAKU { get; set; }
        public string BANK_FURIGANA { get; set; }
        public string RENKEI_CD { get; set; }
        public string BANK_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}