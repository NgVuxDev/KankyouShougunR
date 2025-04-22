using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NYUUKINSAKI : SuperEntity
    {
        public string NYUUKINSAKI_CD { get; set; }
        public string NYUUKINSAKI_NAME1 { get; set; }
        public string NYUUKINSAKI_NAME2 { get; set; }
        public string NYUUKINSAKI_NAME_RYAKU { get; set; }
        public string NYUUKINSAKI_FURIGANA { get; set; }
        public string NYUUKINSAKI_TEL { get; set; }
        public string NYUUKINSAKI_FAX { get; set; }
        public string NYUUKINSAKI_POST { get; set; }
        public SqlInt16 NYUUKINSAKI_TODOUFUKEN_CD { get; set; }
        public string NYUUKINSAKI_ADDRESS1 { get; set; }
        public string NYUUKINSAKI_ADDRESS2 { get; set; }
        public SqlInt16 TORIKOMI_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}