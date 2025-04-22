using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_CONTENA_RESULT : SuperEntity
    {
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 CONTENA_SET_KBN { get; set; }
        public string CONTENA_SHURUI_CD { get; set; }
        public string CONTENA_CD { get; set; }
        public SqlInt16 DAISUU_CNT { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}