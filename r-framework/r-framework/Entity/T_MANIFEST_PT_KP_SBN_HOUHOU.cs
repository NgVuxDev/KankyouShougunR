using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_PT_KP_SBN_HOUHOU : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 REC_NO { get; set; }
        public string SHOBUN_HOUHOU_CD { get; set; }
        public string SHOBUN_HOUHOU_NAME { get; set; }
        public SqlBoolean PRT_FLG { get; set; }
    }
}