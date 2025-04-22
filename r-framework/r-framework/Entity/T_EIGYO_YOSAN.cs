using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class T_EIGYO_YOSAN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt32 NUMBERED_YEAR { get; set; }
        public string DENPYOU_KBN_CD { get; set; }
        public string BUSHO_CD { get; set; }
        public string SHAIN_CD { get; set; }
        public string SHAIN_NAME { get; set; }
        public SqlInt32 MONTH_YOSAN_01 { get; set; }
        public SqlInt32 MONTH_YOSAN_02 { get; set; }
        public SqlInt32 MONTH_YOSAN_03 { get; set; }
        public SqlInt32 MONTH_YOSAN_04 { get; set; }
        public SqlInt32 MONTH_YOSAN_05 { get; set; }
        public SqlInt32 MONTH_YOSAN_06 { get; set; }
        public SqlInt32 MONTH_YOSAN_07 { get; set; }
        public SqlInt32 MONTH_YOSAN_08 { get; set; }
        public SqlInt32 MONTH_YOSAN_09 { get; set; }
        public SqlInt32 MONTH_YOSAN_10 { get; set; }
        public SqlInt32 MONTH_YOSAN_11 { get; set; }
        public SqlInt32 MONTH_YOSAN_12 { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}