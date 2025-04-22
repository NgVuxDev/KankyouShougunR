using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_TEIKI_HAISHA_SHOUSAI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt64 ROW_NUMBER { get; set; }
        public SqlInt64 TEIKI_HAISHA_NUMBER { get; set; }
        public string HINMEI_CD { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlDecimal KANSANCHI { get; set; }
        public SqlInt16 KANSAN_UNIT_CD { get; set; }
        public SqlInt16 KEIYAKU_KBN { get; set; }
        public SqlInt16 KEIJYOU_KBN { get; set; }
        public SqlBoolean ADD_FLG { get; set; }
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        public SqlBoolean KANSAN_UNIT_MOBILE_OUTPUT_FLG { get; set; }
        public SqlInt16 INPUT_KBN { get; set; }
        public SqlInt32 NIOROSHI_NUMBER { get; set; }
        public SqlBoolean ANBUN_FLG { get; set; }
    }
}