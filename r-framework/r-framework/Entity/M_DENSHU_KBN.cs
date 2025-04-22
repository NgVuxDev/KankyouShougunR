using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_DENSHU_KBN : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public string DENSHU_KBN_NAME { get; set; }
        public string DENSHU_KBN_NAME_RYAKU { get; set; }
        public string DENSHU_KBN_BIKOU { get; set; }
        public SqlBoolean HINMEI_USE_KBN { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}