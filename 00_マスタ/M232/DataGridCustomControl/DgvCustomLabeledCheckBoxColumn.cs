using r_framework.CustomControl.DataGridCustomControl;

namespace Shougun.Core.Master.CourseNyuryoku.DataGridCustomControl
{
    public class DgvCustomLabeledCheckBoxColumn : DgvCustomCheckBoxColumn
	{
        /// <summary>
        /// DBのフィールド名を指定するプロパティ
        /// </summary>
        public string LabelText { get; set; }
        private bool ShouldSerializeLabelText()
        {
            return this.LabelText != null;
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DgvCustomLabeledCheckBoxColumn myTextBoxCell = base.Clone() as DgvCustomLabeledCheckBoxColumn;
            myTextBoxCell.LabelText = this.LabelText;
            return myTextBoxCell;
        }

        public DgvCustomLabeledCheckBoxColumn()
		{
            this.CellTemplate = new DgvCustomLabeledCheckBoxCell();
		}
	}
}
