using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_COURSE_DETAIL_ITEMS : SuperEntity
    {
        public SqlInt16 DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public SqlInt32 REC_ID { get; set; }
        public SqlInt32 REC_SEQ { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal KANSANCHI { get; set; }
        public SqlInt16 KANSAN_UNIT_CD { get; set; }
        public SqlInt16 KEIYAKU_KBN { get; set; }
        public SqlInt16 KEIJYOU_KBN { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlBoolean KANSAN_UNIT_MOBILE_OUTPUT_FLG { get; set; }
        public SqlInt16 INPUT_KBN { get; set; }
        public SqlInt32 NIOROSHI_NO { get; set; }
        public SqlBoolean ANBUN_FLG { get; set; }
        public SqlDateTime TEKIYOU_BEGIN { get; set; }
        public string SEARCH_TEKIYOU_BEGIN { get; set; }
        public SqlDateTime TEKIYOU_END { get; set; }
        public string SEARCH_TEKIYOU_END { get; set; }
    }
}