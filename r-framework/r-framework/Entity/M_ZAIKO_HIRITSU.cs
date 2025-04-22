using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_ZAIKO_HIRITSU : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public string HINMEI_CD { get; set; }
        public string HINMEI_NAME { get; set; }
        public string ZAIKO_HINMEI_CD { get; set; }
        public string ZAIKO_HINMEI_NAME { get; set; }
        public SqlInt16 UNIT_CD { get; set; }
        public SqlInt16 ZAIKO_HIRITSU { get; set; }
        public string BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}