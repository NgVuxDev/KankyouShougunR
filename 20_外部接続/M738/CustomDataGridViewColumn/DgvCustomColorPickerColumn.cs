using r_framework.CustomControl;

namespace Shougun.Core.ExternalConnection.ContenaKeikaDate.CustomDataGridViewColumn
{
    public class DataGridViewColourPickerColumn : DgvCustomTextBoxColumn
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataGridViewColourPickerColumn()
        {
            this.CellTemplate = new DataGridViewColourPickerCell();
        }
    }
}