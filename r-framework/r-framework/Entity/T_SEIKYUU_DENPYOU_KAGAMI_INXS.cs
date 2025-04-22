using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEIKYUU_DENPYOU_KAGAMI_INXS : SuperEntity
    {
        public SqlInt64 SEIKYUU_NUMBER { get; set; }
        public SqlInt32 KAGAMI_NUMBER { get; set; }
        public string POSTED_FILE_PATH { get; set; }
    }
}
