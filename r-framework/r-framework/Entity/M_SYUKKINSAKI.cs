using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_SYUKKINSAKI : SuperEntity
    {
        public string SYUKKINSAKI_CD { get; set; }
        public string SYUKKINSAKI_NAME1 { get; set; }
        public string SYUKKINSAKI_NAME2 { get; set; }
        public string SYUKKINSAKI_NAME_RYAKU { get; set; }
        public string SYUKKINSAKI_FURIGANA { get; set; }
        public string SYUKKINSAKI_TEL { get; set; }
        public string SYUKKINSAKI_FAX { get; set; }
        public string SYUKKINSAKI_POST { get; set; }
        public SqlInt16 SYUKKINSAKI_TODOUFUKEN_CD { get; set; }
        public string SYUKKINSAKI_ADDRESS1 { get; set; }
        public string SYUKKINSAKI_ADDRESS2 { get; set; }
        public SqlInt16 TORIKOMI_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}