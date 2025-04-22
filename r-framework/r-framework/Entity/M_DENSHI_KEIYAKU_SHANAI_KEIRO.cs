using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_KEIYAKU_SHANAI_KEIRO : SuperEntity
    {
        public SqlInt16 DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD { get; set; }
        public SqlInt32 DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO { get; set; }
        public string SHAIN_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}