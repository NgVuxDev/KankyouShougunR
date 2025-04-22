using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class DT_R08_EX : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public string KANRI_ID { get; set; }
        public SqlDecimal REC_SEQ { get; set; }
        public string MANIFEST_ID { get; set; }
        public string HST_GYOUSHA_CD { get; set; }
        public string HST_GENBA_CD { get; set; }
        public string HAIKI_SHURUI_CD { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}