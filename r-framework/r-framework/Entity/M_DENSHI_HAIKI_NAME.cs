using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_HAIKI_NAME : SuperEntity
    {
        public string EDI_MEMBER_ID { get; set; }
        public string HAIKI_NAME_CD { get; set; }
        public string HAIKI_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}