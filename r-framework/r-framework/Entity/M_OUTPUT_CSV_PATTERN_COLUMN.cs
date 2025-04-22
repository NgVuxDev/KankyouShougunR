using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_CSV_PATTERN_COLUMN : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }

        /// <summary>
        /// 出力区分
        /// </summary>
        public SqlInt16 OUTPUT_KBN { get; set; }

        /// <summary>
        /// 項目ID
        /// </summary>
        public SqlInt32 KOUMOKU_ID { get; set; }

        /// <summary>
        /// 並び順
        /// </summary>
        public SqlInt32 SORT_NO { get; set; }
    }
}