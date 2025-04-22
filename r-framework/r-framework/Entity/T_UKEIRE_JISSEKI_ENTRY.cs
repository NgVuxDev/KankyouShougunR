using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_UKEIRE_JISSEKI_ENTRY : SuperEntity
    {
        public SqlInt16 DENPYOU_SHURUI { get; set; }
        public SqlInt64 DENPYOU_SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlDateTime SAGYOU_DATE { get; set; }
        public string SAGYOU_TIME { get; set; }
        public string SAGYOUSHA_CD { get; set; }
        public string SAGYOUSHA_NAME { get; set; }
        public string SAGYOU_BIKOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}