using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_GENBAMEMO_HYOUDAI : SuperEntity
    {
        public string GENBAMEMO_HYOUDAI_CD { get; set; }
        public string GENBAMEMO_HYOUDAI_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}