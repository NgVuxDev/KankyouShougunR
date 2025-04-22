using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_LIST_PATTERN_FILL_COND : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 FILL_COND_DATE_KBN { get; set; }
        public SqlDateTime FILL_COND_DATE_BEGIN { get; set; }
        public string SEARCH_FILL_COND_DATE_BEGIN { get; set; }
        public SqlDateTime FILL_COND_DATE_END { get; set; }
        public string SEARCH_FILL_COND_DATE_END { get; set; }
        public SqlInt16 FILL_COND_KYOTEN_CD { get; set; }
        public SqlInt16 FILL_COND_DENPYOU_SBT { get; set; }
        public SqlInt16 FILL_COND_DENPYOU_KBN { get; set; }
        public SqlInt32 FILL_COND_ID_1 { get; set; }
        public string FILL_COND_CD_BEGIN_1 { get; set; }
        public string FILL_COND_CD_END_1 { get; set; }
        public SqlInt32 FILL_COND_ID_2 { get; set; }
        public string FILL_COND_CD_BEGIN_2 { get; set; }
        public string FILL_COND_CD_END_2 { get; set; }
        public SqlInt32 FILL_COND_ID_3 { get; set; }
        public string FILL_COND_CD_BEGIN_3 { get; set; }
        public string FILL_COND_CD_END_3 { get; set; }
        public SqlInt32 FILL_COND_ID_4 { get; set; }
        public string FILL_COND_CD_BEGIN_4 { get; set; }
        public string FILL_COND_CD_END_4 { get; set; }
    }
}