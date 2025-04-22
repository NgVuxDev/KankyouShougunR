using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SHIME_SHORI_ERROR : SuperEntity
    {
        public SqlInt16 SHORI_KBN { get; set; }
        public SqlInt16 CHECK_KBN { get; set; }
        public SqlInt16 DENPYOU_SHURUI_CD { get; set; }
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }
        public SqlInt32 GYO_NUMBER { get; set; }
        public string ERROR_NAIYOU { get; set; }
        public string RIYUU { get; set; }
    }
}