using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MOBISYO_RT_HANNYUU : SuperEntity
    {
        public SqlInt64 HANYU_SEQ_NO { get; set; }
        public SqlInt64 EDABAN { get; set; }
        public SqlDateTime HANNYUU_CREATEDATE { get; set; }
        public SqlDateTime HANNYUU_UPDATEDATE { get; set; }
        public SqlInt16 HANNYUU_UPDATECNT { get; set; }
        public SqlDateTime HANNYUU_HANNYUUDATE { get; set; }
        public string HANNYUU_GYOUSHACD { get; set; }
        public string HANNYUU_GENBACD { get; set; }
        public SqlDouble HANNYUU_RYO { get; set; }
        public string HANNYUU_JISSEKI_GYOUSHACD { get; set; }
        public string HANNYUU_JISSEKI_GENBACD { get; set; }
        public SqlDouble HANNYUU_JISSEKI_RYO { get; set; }
        public SqlBoolean JISSEKI_REGIST_FLG { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}