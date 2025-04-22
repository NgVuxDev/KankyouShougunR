using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_CSV_PATTERN_NYUUSHUKKIN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }

        /// <summary>
        /// 伝票種類_入金
        /// </summary>
        public SqlBoolean DENPYOU_SHURUI_NYUUKIN { get; set; }

        /// <summary>
        /// 伝票種類_出金
        /// </summary>
        public SqlBoolean DENPYOU_SHURUI_SHUKKIN { get; set; }

        /// <summary>
        /// 締処理状況
        /// </summary>
        public SqlInt16 SHIME_SHORI_JOUKYOU { get; set; }
    }
}