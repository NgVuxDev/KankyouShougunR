using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OLD_DATA_DEL : SuperEntity
    {
        public SqlDateTime EIGYOU_DAY { get; set; }
        public SqlDateTime UKETSUKE_DAY { get; set; }
        public SqlDateTime HAISHA_DAY { get; set; }
        public SqlDateTime KEIRYOU_DAY { get; set; }
        public SqlDateTime HANKAN_DAY { get; set; }
        public SqlDateTime MANIFEST_DAY { get; set; }
        public SqlDateTime UNCHIN_DAY { get; set; }
        public SqlDateTime RENKEI_DAY { get; set; }
        public string ERR_STEP { get; set; }
    }
}