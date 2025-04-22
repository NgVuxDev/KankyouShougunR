using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_CUSTOMER_HINDO : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt32 TOUROKU_KAISUU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}