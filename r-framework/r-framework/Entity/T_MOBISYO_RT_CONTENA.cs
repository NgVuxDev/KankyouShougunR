using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MOBISYO_RT_CONTENA : SuperEntity
    {
        public SqlInt64 SEQ_NO { get; set; }
        public SqlInt64 CONTENA_SEQ_NO { get; set; }
        public SqlInt32 JISSEKI_FLG { get; set; }
        public SqlInt32 CONTENA_SET_KBN { get; set; }
        public string CONTENA_SHURUI_CD { get; set; }
        public string CONTENA_CD { get; set; }
        public SqlInt16 DAISUU_CNT { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}