using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ITAKU_MEMO_IKKATSU_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public SqlInt32 ROW_NO { get; set; }
        public SqlBoolean SHORI_KBN { get; set; }
        public string ITAKU_KEIYAKU_SYSTEM_ID { get; set; }
        public string ITAKU_KEIYAKU_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GYOUSHA_ADDRESS { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string GENBA_ADDRESS { get; set; }
        public SqlInt16 KOUSHIN_SHUBETSU { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_SHURUI { get; set; }
        public SqlDateTime ITAKU_KEIYAKU_DATE_BEGIN { get; set; }
        public string SEARCH_ITAKU_KEIYAKU_DATE_BEGIN { get; set; }
        public SqlDateTime ITAKU_KEIYAKU_DATE_END { get; set; }
        public string SEARCH_ITAKU_KEIYAKU_DATE_END { get; set; }
        public SqlInt64 SHOBUN_PATTERN_SYSYTEM_ID { get; set; }
        public SqlInt32 SHOBUN_PATTERN_SEQ { get; set; }
        public string SHOBUN_PATTERN_NAME { get; set; }
        public SqlInt64 LAST_SHOBUN_PATTERN_SYSYTEM_ID { get; set; }
        public SqlInt32 LAST_SHOBUN_PATTERN_SEQ { get; set; }
        public string LAST_SHOBUN_PATTERN_NAME { get; set; }
    }
}