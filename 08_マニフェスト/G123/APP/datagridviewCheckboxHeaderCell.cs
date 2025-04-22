using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.ManifestHimoduke
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
    public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        int _idxCol = -1;
        string _title = string.Empty;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

        public DatagridViewCheckBoxHeaderCell(int idxCol)
        {
            _idxCol = idxCol;
        }
        public DatagridViewCheckBoxHeaderCell(int idxCol, string title)
        {
            _idxCol = idxCol;
            _title = title;
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
                p.X = cellBounds.Location.X +
               (cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height / 2) - (s.Height / 2);

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
            bool catchErr = false;
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;
                bool displayed = false;
                foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                {
                    //[F1]選択外クリアで、非表示になったデータはチェック対象外とする
                    if (dgvRow.Visible)
                    {
                        var parentForm = this.DataGridView.Parent as UIForm;
                        if (_checked
                            && !parentForm.IsHimodukeOk(dgvRow.Index, out catchErr))
                        {
                            if (catchErr) { return; }
                            // チェック不可能な場合、一度だけエラーを表示する
                            if (!displayed)
                            {
                                new MessageBoxShowLogic().MessageBoxShowWarn("最終処分の場所の情報が一致しないマニフェストは選択されません。");
                                displayed = true;
                            }
                        }
                        else
                        {
                            dgvRow.Cells[_idxCol].Value = _checked;

                        }
                        if (!parentForm.IsLastSbnEndFlgTrue(dgvRow.Index))
                        {
                            // 最終処分終了報告ありの場合、チェック状態を保持
                            dgvRow.Cells[_idxCol].Value = true;
                        }
                    }
                }

                if (this.DataGridView.Rows.Count > 0)
                {
                    int idxDisplayed = 0;
                    for (int i = 0; i < this.DataGridView.ColumnCount; i++)
                    {
                        if (i != _idxCol)
                        {
                            if (this.DataGridView.Columns[i].Displayed)
                            {
                                idxDisplayed = i;
                                break;
                            }
                        }
                    }
                    if (this.DataGridView.CurrentRow != null)
                    {
                        this.DataGridView.CurrentCell = this.DataGridView.CurrentRow.Cells[idxDisplayed];
                        this.DataGridView.CurrentCell = this.DataGridView.CurrentRow.Cells[_idxCol];
                    }
                }
            }
            base.OnMouseClick(e);
        }

        public void MouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                return;
            }

            _checked = !_checked;
            bool displayed = false;
            bool catchErr = false;
            foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
            {
                var parentForm = this.DataGridView.Parent as UIForm;
                if (_checked
                    && !parentForm.IsHimodukeOk(dgvRow.Index, out catchErr))
                {
                    if (catchErr) { return; }
                    // チェック不可能な場合、一度だけエラーを表示する
                    if (!displayed)
                    {
                        new MessageBoxShowLogic().MessageBoxShowWarn("最終処分の場所の情報が一致しないマニフェストは選択されません。");
                        displayed = true;
                    }
                }
                else
                {
                    dgvRow.Cells[_idxCol].Value = _checked;

                }
                if (!parentForm.IsLastSbnEndFlgTrue(dgvRow.Index))
                {
                    // 最終処分終了報告ありの場合、チェック状態を保持
                    dgvRow.Cells[_idxCol].Value = true;
                }
            }

            if (this.DataGridView.Rows.Count > 0)
            {
                int idxDisplayed = 0;
                for (int i = 0; i < this.DataGridView.ColumnCount; i++)
                {
                    if (i != _idxCol)
                    {
                        if (this.DataGridView.Columns[i].Displayed)
                        {
                            idxDisplayed = i;
                            break;
                        }
                    }
                }
                if (this.DataGridView.CurrentRow != null)
                {
                    this.DataGridView.CurrentCell = this.DataGridView.CurrentRow.Cells[idxDisplayed];
                    this.DataGridView.CurrentCell = this.DataGridView.CurrentRow.Cells[_idxCol];
                }
            }
        }
    }
}