using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_PT_R04 : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDecimal REC_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public string LAST_SBN_JOU_NAME { get; set; }
        public string LAST_SBN_JOU_POST { get; set; }
        public string LAST_SBN_JOU_ADDRESS1 { get; set; }
        public string LAST_SBN_JOU_ADDRESS2 { get; set; }
        public string LAST_SBN_JOU_ADDRESS3 { get; set; }
        public string LAST_SBN_JOU_ADDRESS4 { get; set; }
        public string LAST_SBN_JOU_TEL { get; set; }
        public string LAST_SBN_GYOUSHA_CD { get; set; }
        public string LAST_SBN_GENBA_CD { get; set; }
    }
}