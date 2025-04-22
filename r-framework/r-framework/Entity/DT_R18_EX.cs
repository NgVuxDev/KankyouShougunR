using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_R18_EX : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string KANRI_ID { get; set; }
        public string MANIFEST_ID { get; set; }
        public string HST_GYOUSHA_CD { get; set; }
        public string HST_GENBA_CD { get; set; }
        public string SBN_GYOUSHA_CD { get; set; }
        public string SBN_GENBA_CD { get; set; }
        public string NO_REP_SBN_EDI_MEMBER_ID { get; set; }
        public string HAIKI_NAME_CD { get; set; }
        public string SBN_HOUHOU_CD { get; set; }
        public string HOUKOKU_TANTOUSHA_CD { get; set; }
        public string SBN_TANTOUSHA_CD { get; set; }
        public string UPN_TANTOUSHA_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public SqlDecimal KANSAN_SUU { get; set; }
        public SqlDecimal GENNYOU_SUU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}