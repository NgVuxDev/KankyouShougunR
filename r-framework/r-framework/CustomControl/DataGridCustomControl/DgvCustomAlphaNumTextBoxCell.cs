using System;
using System.Windows.Forms;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// データグリッドビュー用の英数字専用カスタムテキストボックス
    /// </summary>
    public partial class DgvCustomAlphaNumTextBoxCell : DgvCustomTextBoxCell
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomAlphaNumTextBoxCell()
        {
        }

        /// <summary>
        /// クローン処理
        /// </summary>
        public override void CloneChiled()
        {
            base.CloneChiled();
        }

        /// <summary>
        /// ペイント処理
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            this.CloneChiled();
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        /// <summary>
        /// 編集コントロールの型を指定する
        /// </summary>
        public override Type EditType
        {
            get { return typeof(DgvCustomAlphaNumTextBoxEditingControl); }
        }
    }
}