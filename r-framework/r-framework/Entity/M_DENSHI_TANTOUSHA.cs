using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHI_TANTOUSHA : SuperEntity
    {
        public string EDI_MEMBER_ID { get; set; }
        public SqlInt16 TANTOUSHA_KBN { get; set; }
        public string TANTOUSHA_CD { get; set; }
        public string TANTOUSHA_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}