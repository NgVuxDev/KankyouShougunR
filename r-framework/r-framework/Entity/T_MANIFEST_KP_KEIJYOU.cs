using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_MANIFEST_KP_KEIJYOU : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt16 REC_NO { get; set; }
        public string KEIJOU_CD { get; set; }
        public string KEIJOU_NAME { get; set; }
        public SqlBoolean PRT_FLG { get; set; }
    }
}