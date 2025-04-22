using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_ZAIKO_TANK : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlDateTime ZAIKO_SHIME_DATE { get; set; }
        public string SEARCH_ZAIKO_SHIME_DATE { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlInt16 ZAIKO_ASSESSMENT_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}