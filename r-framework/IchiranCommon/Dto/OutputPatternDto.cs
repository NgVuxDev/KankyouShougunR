using System.Collections.Generic;
using r_framework.Entity;

namespace Shougun.Core.Common.IchiranCommon.Dto
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
            //Communicate InxsSubApplication Start
            OutputPatternInxs = new M_OUTPUT_PATTERN_INXS();
            OutputPatternColumnShougun = new List<M_OUTPUT_PATTERN_COLUMN>();
            OutputPatternColumnInxs = new List<M_OUTPUT_PATTERN_COLUMN_INXS>();
            //Communicate InxsSubApplication End
        }

        public M_OUTPUT_PATTERN OutputPattern { get; set; }
        public M_OUTPUT_PATTERN_KOBETSU OutputPatternKobetsu { get; set; }
        public List<M_OUTPUT_PATTERN_COLUMN> OutputPatternColumn { get; set; }
        //Communicate InxsSubApplication Start
        public M_OUTPUT_PATTERN_INXS OutputPatternInxs { get; set; }
        public List<M_OUTPUT_PATTERN_COLUMN> OutputPatternColumnShougun { get; set; }
        public List<M_OUTPUT_PATTERN_COLUMN_INXS> OutputPatternColumnInxs { get; set; }
        //Communicate InxsSubApplication End
    }
}
