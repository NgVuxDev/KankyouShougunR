using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_TEIKI_JISSEKI_DETAIL : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 TEIKI_JISSEKI_NUMBER { get; set; }
        public SqlInt16 ROW_NUMBER { get; set; }
        public SqlInt32 ROUND_NO { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlDecimal SUURYOU { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal KANSAN_SUURYOU { get; set; }
        public SqlInt16 KANSAN_UNIT_CD { get; set; }
        public SqlDecimal ANBUN_SUURYOU { get; set; }
        public SqlInt32 NIOROSHI_NUMBER { get; set; }
        public SqlDateTime SHUUSHUU_TIME { get; set; }
        public string SEARCH_SHUUSHUU_TIME { get; set; }
        public string KAISHUU_BIKOU { get; set; }
        public string HINMEI_BIKOU { get; set; }
        public SqlInt16 TSUKIGIME_KBN { get; set; }
        public SqlInt64 UR_SH_NUMBER { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlInt16 KEIYAKU_KBN { get; set; }
        public SqlBoolean KANSAN_UNIT_MOBILE_OUTPUT_FLG { get; set; }
        public SqlBoolean ANBUN_FLG { get; set; }
        public SqlInt16 INPUT_KBN { get; set; }
        public SqlBoolean KAKUTEI_FLG { get; set; }
    }
}