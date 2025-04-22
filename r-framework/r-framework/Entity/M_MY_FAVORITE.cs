using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_MY_FAVORITE : SuperEntity
    {
        public string BUSHO_CD { get; set; }
        public string SHAIN_CD { get; set; }
        public string INDEX_NO { get; set; }
        public string FORM_ID { get; set; }
        public SqlInt32 MY_FAVORITE { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
