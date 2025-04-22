using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_CSV_PATTERN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }

        /// <summary>
        /// 出力範囲区分
        /// </summary>
        public string KBN { get; set; }

        /// <summary>
        /// 出力区分
        /// </summary>
        public SqlInt16 OUTPUT_KBN { get; set; }

        /// <summary>
        /// パターン名
        /// </summary>
        public string PATTERN_NAME { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string BIKOU { get; set; }

        public SqlBoolean DELETE_FLG { get; set; }
    }
}