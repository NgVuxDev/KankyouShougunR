using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_YUUGAI_BUSSHITSU : SuperEntity
    {
        public string YUUGAI_BUSSHITSU_CD { get; set; }
        public string YUUGAI_BUSSHITSU_NAME { get; set; }
        public string YUUGAI_BUSSHITSU_NAME_RYAKU { get; set; }
        public string YUUGAI_BUSSHITSU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}