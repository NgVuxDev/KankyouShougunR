using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_NUMBER_YEAR : SuperEntity
    {
        public SqlInt32 NUMBERED_YEAR { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt32 CURRENT_NUMBER { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}