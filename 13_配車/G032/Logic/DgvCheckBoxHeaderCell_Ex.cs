using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    public delegate void CheckBoxClickedHandler(bool state);
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }
    public class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        bool _isCanCheck;
        Point checkBoxLocation;
        Size checkBoxSize;
        int _idxCol = -1;
        int _positionCheck = 1;
        string _title = string.Empty;
        public bool _checked { get; set; }
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

        public DataGridViewCheckBoxHeaderCell()
        {
            _idxCol = 0;
            _checked = false;
            _isCanCheck = true;
        }
        public DataGridViewCheckBoxHeaderCell(int idxCol, bool isCanCheck)
        {
            _idxCol = idxCol;
            _checked = false;
            _isCanCheck = isCanCheck;
        }
        public DataGridViewCheckBoxHeaderCell(int idxCol, int positionCheck)
        {
            _idxCol = idxCol;
            _positionCheck = positionCheck;
            _checked = false;
        }
        public DataGridViewCheckBoxHeaderCell(int idxCol, string title)
        {
            _idxCol = idxCol;
            _title = title;
            _checked = false;
        }

        protected override void Paint(
            System.Drawing.Graphics graphics,
            System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState, value,
                formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

            _cellLocation = cellBounds.Location;
            if (string.IsNullOrEmpty(_title))
            {
                // 決め打ちしてしまう
                if (cellBounds.Height < 20)
                {
                    p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
                    p.Y = 4;
                    if (cellBounds.Width < 65)
                    {
                        p.X = 120;
                    }
                    else
                    {
                        p.X = 160;
                    }
                }
                else
                {
                    p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
                    p.Y = p.Y + 8;
                    p.X = 46;
                }
                checkBoxLocation = p;

                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
            }
            else
            {
                //have title
                Size stitle = new Size();
                stitle.Width = cellBounds.Width;
                stitle.Height = cellBounds.Height / 2;
                Point ptitle = new Point();
                ptitle.X = cellBounds.Location.X + (cellBounds.Width / 2) - (stitle.Width / 2);
                ptitle.Y = cellBounds.Location.Y + (cellBounds.Height / 2 / 2) - (stitle.Height / 2);
                Rectangle titleRec = new Rectangle(ptitle, stitle);

                p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height * 3 / 4) - (s.Height / 2);
                checkBoxLocation = p;
                CheckBoxRenderer.DrawCheckBox(graphics,
                                                checkBoxLocation,
                                                titleRec,
                                                _title,
                                                this.DataGridView.Font,
                                                false,
                                                _cbState);

            }

        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                if (_isCanCheck)
                {
                    _checked = !_checked;

                    foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                    {
                        if (_checked)
                        {
                            // 緯度があるデータにチェックを付ける
                            if (dgvRow.Cells[ConstCls.HIDDEN_LOCATION].Value.ToString() != string.Empty)
                            {
                                dgvRow.Cells[ConstCls.DATA_TAISHO].Value = _checked;
                            }
                        }
                        else
                        {
                            dgvRow.Cells[ConstCls.DATA_TAISHO].Value = _checked;
                        }
                    }
                }
            }
            base.OnMouseClick(e);
            this.DataGridView.InvalidateCell(this);
            this.DataGridView.RefreshEdit();
            this.DataGridView.Refresh();
        }

        public void MouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                return;
            }

            if (_isCanCheck)
            {
                _checked = !_checked;

                foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                {
                    if (_checked)
                    {
                        // 緯度があるデータにチェックを付ける
                        if (dgvRow.Cells[ConstCls.HIDDEN_LOCATION].Value.ToString() != string.Empty)
                        {
                            dgvRow.Cells[ConstCls.DATA_TAISHO].Value = _checked;
                        }
                    }
                    else
                    {
                        dgvRow.Cells[ConstCls.DATA_TAISHO].Value = _checked;
                    }
                }
            }
            this.DataGridView.InvalidateCell(this);
            this.DataGridView.RefreshEdit();
            this.DataGridView.Refresh();
        }
    }
}
