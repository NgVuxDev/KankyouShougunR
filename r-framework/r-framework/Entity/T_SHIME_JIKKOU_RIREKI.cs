using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_SHIME_JIKKOU_RIREKI : SuperEntity
    {
        public SqlInt64 SHIME_JIKKOU_NO { get; set; }
        public SqlInt16 SHORI_KBN { get; set; }
        public SqlInt16 DENPYOU_SHURUI { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt16 SHIMEBI { get; set; }
        public SqlDateTime HIDUKE_HANI_BEGIN { get; set; }
        public string SEARCH_HIDUKE_HANI_BEGIN { get; set; }
        public SqlDateTime HIDUKE_HANI_END { get; set; }
        public string SEARCH_HIDUKE_HANI_END { get; set; }
    }
}