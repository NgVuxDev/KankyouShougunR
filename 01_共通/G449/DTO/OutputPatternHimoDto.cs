using System.Collections.Generic;
using r_framework.Entity;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputPatternHimoDto
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutputPatternHimoDto()
        {
            OutputPattern = new M_OUTPUT_PATTERN_HIMO();
            OutputPatternKobetsu = new M_OUTPUT_PATTERN_KOBETSU_HIMO();
            OutputPatternColumn = new List<M_OUTPUT_PATTERN_COLUMN_HIMO>();
        }

        public M_OUTPUT_PATTERN_HIMO OutputPattern { get; set; }
        public M_OUTPUT_PATTERN_KOBETSU_HIMO OutputPatternKobetsu { get; set; }
        public List<M_OUTPUT_PATTERN_COLUMN_HIMO> OutputPatternColumn { get; set; }

    }
}
