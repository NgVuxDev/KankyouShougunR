using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_JUCHU_M_KENSU : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt32 NUMBERED_YEAR { get; set; }
        public string BUSHO_CD { get; set; }
        public string SHAIN_CD { get; set; }
        public string SHAIN_NAME { get; set; }
        public SqlInt32 MONTH_KENSU_01 { get; set; }
        public SqlInt32 MONTH_KENSU_02 { get; set; }
        public SqlInt32 MONTH_KENSU_03 { get; set; }
        public SqlInt32 MONTH_KENSU_04 { get; set; }
        public SqlInt32 MONTH_KENSU_05 { get; set; }
        public SqlInt32 MONTH_KENSU_06 { get; set; }
        public SqlInt32 MONTH_KENSU_07 { get; set; }
        public SqlInt32 MONTH_KENSU_08 { get; set; }
        public SqlInt32 MONTH_KENSU_09 { get; set; }
        public SqlInt32 MONTH_KENSU_10 { get; set; }
        public SqlInt32 MONTH_KENSU_11 { get; set; }
        public SqlInt32 MONTH_KENSU_12 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}