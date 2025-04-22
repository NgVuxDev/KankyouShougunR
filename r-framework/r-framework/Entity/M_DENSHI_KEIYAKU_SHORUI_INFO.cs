using System.Data.SqlTypes;
namespace r_framework.Entity
{
    public class M_DENSHI_KEIYAKU_SHORUI_INFO : SuperEntity
    {
        public string SHORUI_INFO_ID { get; set; }
        public string SHORUI_INFO_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}