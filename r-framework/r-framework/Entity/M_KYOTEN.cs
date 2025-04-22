using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KYOTEN : SuperEntity
    {
        public SqlInt16 KYOTEN_CD { get; set; }
        public string KYOTEN_NAME { get; set; }
        public string KYOTEN_NAME_RYAKU { get; set; }
        public string KYOTEN_FURIGANA { get; set; }
        public string KYOTEN_POST { get; set; }
        public SqlInt16 KYOTEN_TODOUFUKEN_CD { get; set; }
        public string KYOTEN_ADDRESS1 { get; set; }
        public string KYOTEN_ADDRESS2 { get; set; }
        public string KYOTEN_TEL { get; set; }
        public string KYOTEN_FAX { get; set; }
        public string KYOTEN_DAIHYOU { get; set; }
        public string KYOTEN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public string KEIRYOU_SHOUMEI_1 { get; set; }
        public string KEIRYOU_SHOUMEI_2 { get; set; }
        public string KEIRYOU_SHOUMEI_3 { get; set; }
    }
}