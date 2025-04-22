using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DataGridViewCheckBoxColumnHeaderEx
{
    public delegate void CheckBoxClickedHandler(bool state);

    /// <summary>
    /// イベント
    /// </summary>
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

    /// <summary>
    /// DataGridView コントロールの列ヘッダーを表します
    /// </summary>
    class DataGridviewCheckboxHeaderCell : DataGridViewColumnHeaderCell
    {
        
        Point checkBoxLocation;
        Size checkBoxSize;
        int _idxCol = -1;
        string _title = string.Empty;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        
        public DataGridviewCheckboxHeaderCell(int idxCol)
        {
            _idxCol = idxCol;
        }
        public DataGridviewCheckboxHeaderCell(int idxCol, string title)
        {
            _idxCol = idxCol;
            _title = title;
        }

        /// <summary>
        /// チェックボックスの状態が設定または取得する
        /// </summary>
        public System.Windows.Forms.VisualStyles.CheckBoxState CheckBoxStateReadOnly { get; private set; }

        /// <summary>
        /// 非活性/活性が設定または取得する
        /// </summary>
        public bool CheckBoxEnable { get; set; }

        //
        // 概要:
        //     DataGridViewColumnHeaderCell を描画します。
        //
        // パラメーター:
        //   graphics:
        //     セルの描画に使用する System.Drawing.Graphics。
        //
        //   clipBounds:
        //     再描画が必要な System.Windows.Forms.DataGridView の領域を表す System.Drawing.Rectangle。
        //
        //   cellBounds:
        //     描画されるセルの境界が格納された System.Drawing.Rectangle。
        //
        //   rowIndex:
        //     描画されるセルの行インデックス。
        //
        //   value:
        //     描画されるセルのデータ。
        //
        //   formattedValue:
        //     描画されるセルの書式指定済みデータ。
        //
        //   errorText:
        //     セルに関連付けられたエラー メッセージ。
        //
        //   cellStyle:
        //     セルに関する書式とスタイルの情報が格納された System.Windows.Forms.DataGridViewCellStyle。
        //
        //   advancedBorderStyle:
        //     描画されるセルの境界線スタイルが格納された System.Windows.Forms.DataGridViewAdvancedBorderStyle。
        //
        //   paintParts:
        //     セルのどの部分を描画する必要があるのかを指定する、System.Windows.Forms.DataGridViewPaintParts 値のビットごとの組み合わせ。
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

            if (!CheckBoxEnable)
            {
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled;
                this.CheckBoxStateReadOnly = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled;
            }
            else if (_checked && CheckBoxEnable)
            {
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                this.CheckBoxStateReadOnly = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
            }
            else
            {
                _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                this.CheckBoxStateReadOnly = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            }

            _cellLocation = cellBounds.Location;

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
                                            _cbState);
                
            
        }

        ///   <summary> 
        ///   ヘダーチェックボックスのクリックイヴェント
        ///   </summary> 
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //チェック ボックスがオフで、無効になります
            if (!this.CheckBoxEnable)
            {
                return;
            }

            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;
                foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
                {
                    dgvRow.Cells[_idxCol].Value = _checked;
                }
            }
            base.OnMouseClick(e);
                       
            this.DataGridView.InvalidateCell(this);
            this.DataGridView.RefreshEdit();
        }

        /// <summary>
        /// チェックボックスの状態を設定する
        /// </summary>
        /// <param name="checkBoxChecked"></param>
        public void SetCheckBoxState(bool checkBoxChecked)
        {
            datagridviewCheckboxHeaderEventArgs ex = new datagridviewCheckboxHeaderEventArgs();
            ex.CheckedState = checkBoxChecked;

            _checked = checkBoxChecked;

            this.DataGridView.InvalidateCell(this);

        }

        public void MouseClick(DataGridViewCellMouseEventArgs e)
        {
            //チェック ボックスがオフで、無効になります
            if (!this.CheckBoxEnable)
            {
                return;
            }

            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                return;
            }

            _checked = !_checked;
            foreach (DataGridViewRow dgvRow in this.DataGridView.Rows)
            {
                dgvRow.Cells[_idxCol].Value = _checked;
            }

            this.DataGridView.InvalidateCell(this);
            this.DataGridView.RefreshEdit();
        }
    }

    /// <summary>
    /// イベント
    /// </summary>
    class datagridviewCheckboxHeaderEventArgs : EventArgs
    {
        private bool checkedState = false;
        public bool CheckedState
        {
            get { return checkedState; }
            set { checkedState = value; }
        }
    }

}