using System.Collections.Generic;
using r_framework.Entity;

namespace r_framework.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class OutputPatternDto
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OutputPatternDto()
        {
            OutputPattern = new M_OUTPUT_PATTERN();
            OutputPatternKobetsu = new M_OUTPUT_PATTERN_KOBETSU();
            OutputPatternColumn = new List<M_OUTPUT_PATTERN_COLUMN>();
        }

        public M_OUTPUT_PATTERN OutputPattern { get; set; }
        public M_OUTPUT_PATTERN_KOBETSU OutputPatternKobetsu { get; set; }
        public List<M_OUTPUT_PATTERN_COLUMN> OutputPatternColumn { get; set; }

    }
}
