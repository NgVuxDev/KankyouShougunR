using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataGridViewCheckBoxColumn
{
    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        private bool enabledValue;

        /// <summary>
        /// This property decides whether the checkbox should be shown 
        /// checked or unchecked.
        /// </summary>

        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        /// Override the Clone method so that the Enabled property is copied.

        public override object Clone()
        {
            DataGridViewDisableCheckBoxCell cell =
                (DataGridViewDisableCheckBoxCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }
        protected override void Paint
        (Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
                int rowIndex, DataGridViewElementStates elementState, object value,
                object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                    DataGridViewPaintParts paintParts)
        {

            SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
            graphics.FillRectangle(cellBackground, cellBounds);
            cellBackground.Dispose();
            PaintBorder(graphics, clipBounds, cellBounds,
            cellStyle, advancedBorderStyle);
            Rectangle checkBoxArea = cellBounds;
            Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
            checkBoxArea.X += buttonAdjustment.X;
            checkBoxArea.Y += buttonAdjustment.Y;

            checkBoxArea.Height -= buttonAdjustment.Height;
            checkBoxArea.Width -= buttonAdjustment.Width;
            Point drawInPoint = new Point(cellBounds.X + cellBounds.Width / 2 - 7,
                cellBounds.Y + cellBounds.Height / 2 - 7);

            if (this.enabledValue)
                CheckBoxRenderer.DrawCheckBox(graphics, drawInPoint,
        System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled);
            else
                CheckBoxRenderer.DrawCheckBox(graphics, drawInPoint,
       System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled);
        }
    } 

}