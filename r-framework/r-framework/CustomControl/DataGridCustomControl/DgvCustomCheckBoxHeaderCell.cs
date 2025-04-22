using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace r_framework.CustomControl.DataGridCustomControl
{
    public class DgvCustomCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        Point checkBoxLocation;
        Size checkBoxSize;
        Point cellLocation = new Point();
        CheckBoxState cbState = CheckBoxState.UncheckedNormal;

        bool _checked = false;
        /// <summary>チェックボックスの値</summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        CheckAlign _checkboxPosition = CheckAlign.MIDDLE;
        /// <summary>チェックボックス位置</summary>
        public CheckAlign CheckboxPosition
        {
            get { return _checkboxPosition; }
            set { _checkboxPosition = value; }
        }

        bool _isCanCheck = false;
        /// <summary>編集できるフラグ</summary>
        public bool IsCanChecked
        {
            get { return _isCanCheck; }
            set { _isCanCheck = value; }
        }

        string _headerText = string.Empty;
        /// <summary>ヘッダテキスト</summary>
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

        /// <summary></summary>
        public event CheckboxHeaderClickEventHander OnCheckBoxClicked;
        /// <summary>クリックイベントの委託の声明</summary>
        public delegate void CheckboxHeaderClickEventHander(object sender, DgvCustomCheckboxHeaderEventArgs e);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="idxCol">列チェックボックスのインデックス</param>
        /// <param name="headerText">ヘッダテキスト</param>
        /// <param name="position">チェックボックス位置</param>
        /// <param name="isCanCheck">編集できるフラグ</param>
        /// <param name="check">チェックボックスの値</param>
        public DgvCustomCheckBoxHeaderCell(string headerText, CheckAlign position, bool isCanCheck = true, bool check = false)
        {
            _headerText = headerText;
            _checkboxPosition = position;
            _isCanCheck = isCanCheck;
            _checked = check;
        }

        /// <summary>
        /// ヘッダチェックボックスの描画
        /// </summary>
        protected override void Paint(System.Drawing.Graphics graphics,
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
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
            dataGridViewElementState, value,
            formattedValue, errorText, cellStyle,
            advancedBorderStyle, paintParts);

            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
            checkBoxSize = s;

            if (_checked)
            {
                if (_isCanCheck)
                {
                    cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                }
                else
                {
                    cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled;
                }
            }
            else
            {
                if (_isCanCheck)
                {
                    cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                }
                else
                {
                    cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedDisabled;
                }
            }

            cellLocation = cellBounds.Location;

            //ヘッダテキストがBLANKの場合
            if (string.IsNullOrWhiteSpace(_headerText))
            {
                p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) - 1; // ヘッダチェックボックスのX座標
                p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2); 　// ヘッダチェックボックスのY座標

                checkBoxLocation = p;
                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, cbState);
            }
            else
            {
                //ヘッダテキストがある場合
                Font font = new Font("ＭＳ ゴシック", 9.75F);
                Size textSize = TextRenderer.MeasureText(_headerText, font);

                if (_checkboxPosition == CheckAlign.RIGHT)
                {
                    p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (textSize.Width / 2) + textSize.Width - 1;
                    p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2); 　// ヘッダチェックボックスのY座標
                }
                else if (_checkboxPosition == CheckAlign.LEFT)
                {
                    p.X = cellBounds.Location.X + 3;
                    p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2); 　// ヘッダチェックボックスのY座標
                }
                else if (_checkboxPosition == CheckAlign.MIDDLE)
                {
                    p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2);

                    //ヘッダテキストの下の中央に表示したい場合、ヘッダテキストに「\n」を追加
                    //例: HeaderText = "削除\n"
                    //削除
                    // ☑
                    if (cellBounds.Height > 18)
                    {
                        p.Y = cellBounds.Location.Y + cellBounds.Height - s.Height - 2;
                    }
                    else
                    {
                        p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
                    }
                }

                checkBoxLocation = p;
                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, cbState);
            }
        }

        /// <summary>
        /// ヘッダチェックボックスのクリックイベント
        /// </summary>
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width &&
                p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                if (_isCanCheck)
                {
                    _checked = !_checked;
                    // ヘッダチェックボックスのチェック状態
                    DgvCustomCheckboxHeaderEventArgs ex = new DgvCustomCheckboxHeaderEventArgs();
                    ex.CheckedState = _checked;
                    ex.ColumnIndex = e.ColumnIndex;

                    if (OnCheckBoxClicked != null)
                    {
                        OnCheckBoxClicked(this.DataGridView, ex); // クリック事件をトリガー
                        this.DataGridView.InvalidateCell(this);
                    }
                }
            }
            base.OnMouseClick(e);
        }

        /// <summary>
        /// ヘッダのクリックイベント
        /// </summary>
        public void ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width &&
                p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                return;
            }

            if (_isCanCheck)
            {
                _checked = !_checked;
                // ヘッダチェックボックスのチェック状態
                DgvCustomCheckboxHeaderEventArgs ex = new DgvCustomCheckboxHeaderEventArgs();
                ex.CheckedState = _checked;
                ex.ColumnIndex = e.ColumnIndex;

                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(this.DataGridView, ex); // クリック事件をトリガー
                    this.DataGridView.InvalidateCell(this);
                }
            }
        }
    }

    /// <summary>
    /// イベント
    /// </summary>
    public class DgvCustomCheckboxHeaderEventArgs : EventArgs
    {
        private bool checkedState = false;
        private int columnIndex = -1;

        public bool CheckedState
        {
            get { return checkedState; }
            set { checkedState = value; }
        }
        public int ColumnIndex
        {
            get { return columnIndex; }
            set { columnIndex = value; }
        }
    }

    /// <summary>
    /// チェックボックス整列
    /// </summary>
    public enum CheckAlign : short
    {
        LEFT = 1,
        RIGHT,
        MIDDLE
    }
}