using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MOBISYO_RT_DTL : SuperEntity
    {
        public SqlInt64 SEQ_NO { get; set; }
        public SqlInt64 HANYU_SEQ_NO { get; set; }
        public SqlInt64 HANYU_JISSEKI_SEQ_NO { get; set; }
        public SqlInt64 EDABAN { get; set; }
        public string GENBA_DETAIL_HINMEICD { get; set; }
        public SqlDouble GENBA_DETAIL_SUURYO1 { get; set; }
        public SqlInt16 GENBA_DETAIL_UNIT_CD1 { get; set; }
        public SqlDouble GENBA_DETAIL_SUURYO2 { get; set; }
        public SqlInt16 GENBA_DETAIL_UNIT_CD2 { get; set; }
        public SqlBoolean GENBA_DETAIL_ADDHINMEIFLG { get; set; }
        public SqlBoolean JISSEKI_REGIST_FLG { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}