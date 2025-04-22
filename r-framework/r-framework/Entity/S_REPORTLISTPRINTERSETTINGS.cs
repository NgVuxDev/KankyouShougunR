using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_REPORTLISTPRINTERSETTINGS : SuperEntity
    {
        public SqlInt32 ID { get; set; }
        public SqlInt32 SORT_NO { get; set; }
        public string REPORT_DISP_NAME { get; set; }
        public string REPORT_BUTSURI_NAME { get; set; }
        public string REPORT_LAYOUT_NAME { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}