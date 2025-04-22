using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ITAKU_MEMO_IKKATSU_ENTRY : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public SqlDateTime MEMO_UPDATE_DATE { get; set; }
        public string SEARCH_MEMO_UPDATE_DATE { get; set; }
        public string MEMO { get; set; }
        public string HST_GYOUSHA_CD { get; set; }
        public string HST_GYOUSHA_NAME { get; set; }
        public string HST_GENBA_CD { get; set; }
        public string HST_GENBA_NAME { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_NAME { get; set; }
        public SqlInt64 SHOBUN_PATTERN_SYSTEM_ID { get; set; }
        public SqlInt32 SHOBUN_PATTERN_SEQ { get; set; }
        public string SHOBUN_PATTERN_NAME { get; set; }
        public SqlInt64 LAST_SHOBUN_PATTERN_SYSTEM_ID { get; set; }
        public SqlInt32 LAST_SHOBUN_PATTERN_SEQ { get; set; }
        public string LAST_SHOBUN_PATTERN_NAME { get; set; }
        public string KEIYAKU_BEGIN { get; set; }
        public string KEIYAKU_BEGIN_TO { get; set; }
        public string KEIYAKU_END { get; set; }
        public string KEIYAKU_END_TO { get; set; }
        public SqlInt16 UPDATE_SHUBETSU { get; set; }
        public SqlInt16 KEIYAKUSHO_SHURUI { get; set; }
        public SqlInt16 SHOBUN_UPDATE_KBN { get; set; }
        public SqlInt64 UPD_SHOBUN_PATTERN_SYSTEM_ID { get; set; }
        public SqlInt32 UPD_SHOBUN_PATTERN_SEQ { get; set; }
        public string UPD_SHOBUN_PATTERN_NAME { get; set; }
        public SqlInt16 LAST_SHOBUN_UPDATE_KBN { get; set; }
        public SqlInt64 UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID { get; set; }
        public SqlInt32 UPD_LAST_SHOBUN_PATTERN_SEQ { get; set; }
        public string UPD_LAST_SHOBUN_PATTERN_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}