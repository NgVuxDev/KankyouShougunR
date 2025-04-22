using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_NUMBER_FILE : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt64 CURRENT_NUMBER { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
