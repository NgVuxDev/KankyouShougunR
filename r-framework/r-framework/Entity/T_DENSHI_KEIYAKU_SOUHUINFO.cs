using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_DENSHI_KEIYAKU_SOUHUINFO : SuperEntity
    {
        public string SYSTEM_ID { get; set; }
        public string DENSHI_KEIYAKU_SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlBoolean SEND_FLG { get; set; }
        public string FILE_SHURUI_CD { get; set; }
        public string FILE_SHURUI_NAME { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_NAME { get; set; }
        public string CHIIKI_NAME { get; set; }
        public string KYOKA_NO { get; set; }
        public string BIKO { get; set; }
        public string FILE_PATH { get; set; }
        public string FILE_ID { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
