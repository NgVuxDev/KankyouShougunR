using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_LIST_PATTERN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt32 WINDOW_ID { get; set; }
        public string PATTERN_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        //VAN 20200326 #134974, #134977 S
        public SqlBoolean HINMEI_DISP_KBN { get; set; }
        public SqlBoolean NET_JYUURYOU_DISP_KBN { get; set; }
        public SqlBoolean SUURYOU_UNIT_DISP_KBN { get; set; }
        //VAN 20200326 #134974, #134977 E
    }
}