using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_BIKO_SENTAKUSHI_NYURYOKU : SuperEntity
    {
        public SqlBoolean BIKO_DEFAULT_KBN { get; set; }
        public string BIKO_CD { get; set; }
        public string BIKO_NOTE { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}