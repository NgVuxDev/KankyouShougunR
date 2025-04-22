using System.Windows.Forms;
using r_framework.CustomControl.DataGridCustomControl;
using System.ComponentModel;

namespace r_framework.Dto
{
    /// <summary>
    /// チェックボックスクラス
    /// </summary>
    public class HeaderCheckboxDto
    {
        int _checkboxIndex = -1;
        /// <summary>
        /// チェックボックスのインデックス
        /// </summary>
        public int CHECKBOX_INDEX
        {
            get { return _checkboxIndex; }
            set { _checkboxIndex = value; }
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string COLUMN_NAME { get; set; }

        /// <summary>
        /// ヘダーテキスト
        /// </summary>
        public string HEADER_TEXT { get; set; }

        CheckAlign _checkboxPosition = CheckAlign.MIDDLE;
        /// <summary>
        /// チェックボックス位置
        /// </summary>
        public CheckAlign CHECKBOX_POSITON
        {
            get { return _checkboxPosition; }
            set { _checkboxPosition = value; }
        }

        /// <summary>
        /// ヒント
        /// </summary>
        public string HINT_TEXT { get; set; }

        /// <summary>
        /// [F10]並び替え, [F11]フィルタの時、表示フラグ
        /// True: 表示
        /// False: 非表示
        /// </summary>
        public bool SORT_FILTER_FLG { get; set; }
    }
}
