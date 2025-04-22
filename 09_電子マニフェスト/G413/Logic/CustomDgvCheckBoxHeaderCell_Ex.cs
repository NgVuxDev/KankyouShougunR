using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Shougun.Core.ElectronicManifest.RealInfoSearch
{
    public delegate void CheckBoxClickedHandler(bool state);
    public class CustomDataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public CustomDataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }
   public class CustomDgvCheckBoxHeaderCell_Ex : DataGridViewColumnHeaderCell
   {
        UIForm _form;
        Point checkBoxLocation;
        Size checkBoxSize;
        int _idxCol = -1;
        string _title = string.Empty;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        
        public CustomDgvCheckBoxHeaderCell_Ex(int idxCol)
        {
            _idxCol = idxCol;
        }
        public CustomDgvCheckBoxHeaderCell_Ex(int idxCol, string title)
        {
            _idxCol = idxCol;
            _title = title;
        }
        public CustomDgvCheckBoxHeaderCell_Ex(int idxCol, UIForm form)
        {
            _idxCol = idxCol;
            _form = form;
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
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

            _cellLocation = cellBounds.Location;
            if(string.IsNullOrEmpty(_title)){
                p.X = cellBounds.Location.X +
               (cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height / 2) - (s.Height / 2);
                
                checkBoxLocation = p;
                
                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
            }
            else{
                //have title
                Size stitle = new Size();
                stitle.Width = cellBounds.Width;
                stitle.Height = cellBounds.Height / 2;
                Point ptitle = new Point();
                ptitle.X = cellBounds.Location.X + (cellBounds.Width / 2) - (stitle.Width / 2);
                ptitle.Y = cellBounds.Location.Y + (cellBounds.Height / 2 / 2) - (stitle.Height / 2);
                Rectangle titleRec = new Rectangle(ptitle, stitle);

                p.X = cellBounds.Location.X +(cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height *3 /4) - (s.Height / 2);
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
                _checked = !_checked;
                if (this.DataGridView.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                    {
                        dgvRow.Cells[0].Value = false;
                        if (dgvRow.Cells[7].Value.ToString() == "1")
                        {
                            dgvRow.Cells[0].ReadOnly = false;
                        }
                    }
                    int checkCnt = this._form.MILogic.MAX_CHECK - this._form.MILogic.GetRequestDataInDay();
                    if (checkCnt > 0)
                    {
                        if (_checked == true)
                        {
                            int cnt = 0;
                            string preManifestNumber = "";
                            foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                            {
                                dgvRow.Cells[0].ReadOnly = true;
                                if (cnt < checkCnt)
                                {
                                    if (dgvRow.Cells[7].Value.ToString() == "1")
                                    {
                                        dgvRow.Cells[0].ReadOnly = false;
                                        dgvRow.Cells[0].Value = true;
                                        if (!dgvRow.Cells[this._form.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString().Equals(preManifestNumber))
                                        {
                                            cnt++;
                                        }
                                    }
                                }

                                preManifestNumber = dgvRow.Cells[this._form.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString();
                            }

                            if (cnt == checkCnt)
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendFormat("{0}の最新情報照会件数が{1}件を超えたため、これ以上のチェックは出来ません",
                                        String.Format("過去{0}時間以内", this._form.MILogic.EXECUTING_DECISION_HOUR),
                                        this._form.MILogic.MAX_CHECK);
                                MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        _checked = false;
                        this._form.isShowMessage = true;
                    }
                }

            }

            if (_checked == true)
            {
                this._form.isShowMessage = true;
            }
            else
            {
                if (this._form.MILogic.GetRequestDataInDay() != 100)
                {
                    this._form.isShowMessage = false;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}の最新情報照会件数が{1}件を超えたため、これ以上のチェックは出来ません",
                            String.Format("過去{0}時間以内", this._form.MILogic.EXECUTING_DECISION_HOUR),
                            this._form.MILogic.MAX_CHECK);
                    MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            base.OnMouseClick(e);
            this.DataGridView.InvalidateCell(this);
            this.DataGridView.RefreshEdit();
            
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
            if (this.DataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                {
                    dgvRow.Cells[0].Value = false;
                    if (dgvRow.Cells[7].Value.ToString() == "1")
                    {
                        dgvRow.Cells[0].ReadOnly = false;
                    }
                }
                int checkCnt = this._form.MILogic.MAX_CHECK - this._form.MILogic.GetRequestDataInDay();
                if (checkCnt > 0)
                {
                    if (_checked == true)
                    {
                        int cnt = 0;
                        string preManifestNumber = "";
                        foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                        {
                            dgvRow.Cells[0].ReadOnly = true;
                            if (cnt < checkCnt)
                            {
                                if (dgvRow.Cells[7].Value.ToString() == "1")
                                {
                                    dgvRow.Cells[0].ReadOnly = false;
                                    dgvRow.Cells[0].Value = true;
                                    if (!dgvRow.Cells[this._form.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString().Equals(preManifestNumber))
                                    {
                                        cnt++;
                                    }
                                }
                            }

                            preManifestNumber = dgvRow.Cells[this._form.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString();
                        }

                        if (cnt == checkCnt)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("{0}の最新情報照会件数が{1}件を超えたため、これ以上のチェックは出来ません",
                                    String.Format("過去{0}時間以内", this._form.MILogic.EXECUTING_DECISION_HOUR),
                            this._form.MILogic.MAX_CHECK);
                            MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    _checked = false;
                    this._form.isShowMessage = true;
                }
            }

            if (_checked == true)
            {
                this._form.isShowMessage = true;
            }
            else
            {
                if (this._form.MILogic.GetRequestDataInDay() != 100)
                {
                    this._form.isShowMessage = false;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}の最新情報照会件数が{1}件を超えたため、これ以上のチェックは出来ません",
                            String.Format("過去{0}時間以内", this._form.MILogic.EXECUTING_DECISION_HOUR),
                            this._form.MILogic.MAX_CHECK);
                    MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            this.DataGridView.InvalidateCell(this);
            this.DataGridView.RefreshEdit();
        }
    }
}
