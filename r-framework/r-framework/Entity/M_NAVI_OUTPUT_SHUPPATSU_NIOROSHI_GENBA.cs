using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_NAVI_OUTPUT_SHUPPATSU_NIOROSHI_GENBA : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlDateTime OUTPUT_DATE { get; set; }
        public SqlBoolean JYOGAI_FLG { get; set; }
        public string NAVI_EIGYOUSHO_CD { get; set; }
    }
}