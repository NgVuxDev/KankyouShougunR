using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME : SuperEntity
    {
        public SqlInt16 DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD { get; set; }
        public string DENSHI_KEIYAKU_SHANAI_KEIRO_NAME { get; set; }
        public string DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}