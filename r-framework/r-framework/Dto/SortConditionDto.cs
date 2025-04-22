using System.Windows.Forms;

namespace r_framework.Dto
{
    /// <summary>
    /// ソート条件の格納クラス
    /// </summary>
    public class SortConditionDto
    {
        /// <summary>
        /// ソートを行う順番
        /// </summary>
        public int sortNo { get; set; }

        /// <summary>
        /// ソートを行うセルの番号
        /// </summary>
        public int sortRowNo { get; set; }

        /// <summary>
        /// ソート条件
        /// </summary>
        public SortOrder sortOrder { get; set; }

        /// <summary>
        /// ソートコントロール名
        /// </summary>
        public string sortColumnName { get; set; }

        /// <summary>
        /// ソートコントロールのインデックス
        /// </summary>
        public int sortColumnIndex { get; set; }
    }
}
