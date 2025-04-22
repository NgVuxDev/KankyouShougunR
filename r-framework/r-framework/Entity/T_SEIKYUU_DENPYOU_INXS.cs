using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SEIKYUU_DENPYOU_INXS : SuperEntity
    {
        public SqlInt64 SEIKYUU_NUMBER { get; set; }
        public SqlInt64 INXS_AUTO_KEY { get; set; }
        public SqlInt32 UPLOAD_STATUS { get; set; }
        public SqlInt32 DOWNLOAD_STATUS { get; set; }
    }
}
