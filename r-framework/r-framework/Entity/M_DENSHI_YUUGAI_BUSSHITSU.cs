using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_YUUGAI_BUSSHITSU : SuperEntity
    {
        public string YUUGAI_BUSSHITSU_CD { get; set; }
        public string YUUGAI_BUSSHITSU_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}