using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KEITAI_KBN : SuperEntity
    {
        public SqlInt16 KEITAI_KBN_CD { get; set; }
        public string KEITAI_KBN_NAME { get; set; }
        public string KEITAI_KBN_NAME_RYAKU { get; set; }
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlBoolean KENSHU_FLG { get; set; }
        public string KEITAI_KBN_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}