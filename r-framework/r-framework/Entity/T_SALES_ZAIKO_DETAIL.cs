using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SALES_ZAIKO_DETAIL : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt16 ROW_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string SALES_ZAIKO_HINMEI_CD { get; set; }
        public SqlDecimal SUURYOU { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}