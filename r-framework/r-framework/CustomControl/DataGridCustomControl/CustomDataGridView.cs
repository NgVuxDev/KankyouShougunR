using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Event;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Dto;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムデータグリッドビュー
    /// </summary>
    public partial class CustomDataGridView : DataGridView
    {
        /// <summary>
        /// リロードフラグ(未使用)
        /// </summary>
        [DefaultValue(true)]
        [Browsable(false)]
        public bool IsReload { get; set; }

        /// <summary>
        /// 紐付ソート用のパネル名を設定するプロパティ（未使用）
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するソート用パネル名を入力してください")]
        public string LinkedDataPanelName { get; set; }

        /// <summary>
        /// IMEフリガナ取得用オブジェクト
        /// </summary>
        public NativeWindowContorol imeFuri = null;

        /// <summary>
        /// LinkedDataPanelNameの初期化
        /// </summary>
        /// <returns>初期化可否フラグ</returns>
        private bool ShouldSerializeLinkedDataGridViewName()
        {
            return this.LinkedDataPanelName != null;
        }

        /// <summary>
        /// Dispose処理中かどうか
        /// </summary>
        private bool isDisposing = false;

        /// <summary>
        /// 閲覧目的かどうか。trueならメモリ抑制処理。
        /// 一覧画面などで利用する場合にtrueにする。
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public bool IsBrowsePurpose { get; set; }

        /// <summary>
        ///
        /// </summary>
        private int _prevRowIndex = -1;

        /// <summary>
        /// 直前のセル ReadOnlyだとCellLeaveが発生しない？のでその対応用
        /// </summary>
        private DataGridViewCell _prevCell = null;

        /// <summary>
        /// グリッドビューにヘッダチェックボックスを自動作成用
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("ヘッダチェックボックスリストを設定してください")]
        public HeaderCheckboxDto[] ListHeaderCheckbox { get; set; }

        /// <summary>
        /// ListHeaderCheckboxの初期化
        /// </summary>
        /// <returns>初期化可否フラグ</returns>
        private bool ShouldSerializeListHeaderCheckbox()
        {
            return this.ListHeaderCheckbox != null && this.ListHeaderCheckbox.Length > 0;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomDataGridView()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        public CustomDataGridView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            this.IsReload = true;
            this.DoubleBuffered = true;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // ヒントテキストをToolTipTextに設定して画面下部に表示しているので
            // ToolTips自体のバルーン表示はいらない。
            this.ShowCellToolTips = false;
            this.MultiSelect = false; //複数セル選択禁止

            if (this.IsBrowsePurpose)
            {
                this.ImeMode = ImeMode.Disable;
                this.DefaultCellStyle.SelectionForeColor = r_framework.Const.Constans.READONLY_COLOR_FORE;
            }
            else
            {
                this.EditMode = DataGridViewEditMode.EditOnEnter;

                foreach (DataGridViewRow row in this.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.UpdateBackColor(false); // 読み取り専用だと最初に色を付ける
                    }
                }
            }
        }

        /// <summary>
        /// セルクリック時のチェックボックスON/OFF
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseClick(e);

            // 個別対応 最新情報照会
            if (this.Parent.GetType().FullName == "Shougun.Core.ElectronicManifest.RealInfoSearch.UIForm")
            {
                return;
            }

            // 個別対応 マニフェスト紐付
            if (this.Parent.GetType().FullName == "Shougun.Core.PaperManifest.ManifestHimoduke.UIForm")
            {
                return;
            }

            if (e.RowIndex >= 0)
            {
                DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
                // 個別対応 モバイル将軍取込み(G283), 電マニCSV取込(G152)
                if ((string)cell.Tag == "G283" || (string)cell.Tag == "G152")
                {
                    if ((string)cell.Tag == "G152") { cell.Tag = string.Empty; }
                    return;
                }

                if (cell is DgvCustomCheckBoxCell)
                {
                    DgvCustomCheckBoxCell checkCell = cell as DgvCustomCheckBoxCell;
                    if (checkCell.ReadOnly)
                    {
                        return;
                    }

                    checkCell.Value = !Convert.ToBoolean(checkCell.Value is DBNull ? 0 : checkCell.Value);

                    this.RefreshEdit();
                    this.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                else if (cell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;
                    if (checkCell.ReadOnly)
                    {
                        return;
                    }

                    checkCell.Value = !Convert.ToBoolean(checkCell.Value == null ? 0 : checkCell.Value);

                    this.RefreshEdit();
                    this.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        /// <summary>
        /// セルの値が決まる前の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            base.OnCurrentCellDirtyStateChanged(e);

            DataGridViewCell cell = this.CurrentCell;
            // 個別対応その1 最新情報照会
            if (this.Parent.GetType().FullName == "Shougun.Core.ElectronicManifest.RealInfoSearch.UIForm")
            {
                cell.Tag = string.Empty;
                return;
            }

            // 個別対応その2 モバイル将軍取込み
            if (this.Parent.GetType().FullName == "Shougun.Core.Allocation.MobileShougunTorikomi.APP.UIForm")
            {
                this.RefreshEdit();
                this.CommitEdit(DataGridViewDataErrorContexts.Commit);
                return;
            }

            // 個別対応その3 電マニCSV取込み
            if ((string)cell.Tag == "G152")
            {
                cell.Tag = string.Empty;
                return;
            }

            if (cell is DgvCustomCheckBoxCell)
            {
                DgvCustomCheckBoxCell checkCell = cell as DgvCustomCheckBoxCell;
                if (checkCell.ReadOnly)
                {
                    return;
                }

                checkCell.Value = !Convert.ToBoolean(checkCell.Value is DBNull ? 0 : checkCell.Value);

                this.RefreshEdit();
                this.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            else if (cell is DataGridViewCheckBoxCell)
            {
                DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;
                if (checkCell.ReadOnly)
                {
                    return;
                }

                checkCell.Value = !Convert.ToBoolean(checkCell.Value == null ? 0 : checkCell.Value);

                this.RefreshEdit();
                this.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.OnCurrentCellChanged(e);

            if (this.CurrentRow == null)
                return;

            // 前回のカレント行を再描画
            if (this._prevRowIndex != this.CurrentRow.Index)
            {
                if (this._prevRowIndex >= 0 && this._prevRowIndex < this.RowCount)
                {
                    this.InvalidateRow(this._prevRowIndex);
                }

                this._prevRowIndex = this.CurrentRow.Index;
            }

            // 現在のカレント行を再描画
            if (this.CurrentRow.Index >= 0 && this.CurrentRow.Index < this.RowCount)
            {
                this.InvalidateRow(this.CurrentRow.Index);
            }
        }

        /// <summary>
        /// セルの背景描画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {

            if (this.DesignMode) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (!this.IsBrowsePurpose) return;
            if (!e.PaintParts.HasFlag(DataGridViewPaintParts.Background)) return;

            Color color = e.CellStyle.BackColor;

            bool hasFocus = (this.Focused &&
                e.PaintParts.HasFlag(DataGridViewPaintParts.SelectionBackground) &&
                e.State.HasFlag(DataGridViewElementStates.Selected));

            bool readOnly = (this.ReadOnly || e.State.HasFlag(DataGridViewElementStates.ReadOnly));

            if (hasFocus)
            {
                color = r_framework.Const.Constans.FOCUSED_COLOR;
            }

            if (readOnly)
            {
                color = r_framework.Const.Constans.READONLY_COLOR;
            }

            if (this.IsBrowsePurpose && this.CurrentRow != null && e.RowIndex == this.CurrentRow.Index)
            {
                if (!hasFocus || readOnly)
                {
                    color = r_framework.Const.Constans.SELECT_COLOR;
                }
            }

            if (hasFocus)
            {
                e.CellStyle.SelectionForeColor = r_framework.Const.Constans.SELECT_COLOR_FORE;
            }

            using (var brush = new SolidBrush(color))
            {
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }

            e.Paint(e.ClipBounds, e.PaintParts & ~DataGridViewPaintParts.Background);
            e.Handled = true;

            }
            finally
            {
                base.OnCellPainting(e);
            }
        }

        /// <summary>
        /// RowPostPaintイベントハンドラ
        /// </summary>
        /// <param name="e">DataGridViewRowPostPaintEventArgs</param>
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);

            if (this.DesignMode)
                return;

            //カレント行でなければ抜ける
            if (this.CurrentRow == null || e.RowIndex != this.CurrentRow.Index)
                return;

            if (this.Parent is SuperForm)
            {
                if (((SuperForm)this.Parent).WindowId == Const.WINDOW_ID.T_TORIHIKI_RIREKI_ICHIRAN)
                {
                    // G334 取引履歴一覧の場合、
                    // セルだけ赤枠にする処理がある為、ここでの処理は抜ける
                    return;
                }
            }

            //カレント行に赤い枠線を引く
            using (var pen = new Pen(Color.Red, 2))
            {
                Rectangle rc = e.RowBounds;
                int x = e.RowBounds.X;
                int y = e.RowBounds.Y;
                int cy = e.RowBounds.Bottom;

                var cx = this.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                if (this.RowHeadersVisible)
                {
                    cx += this.RowHeadersWidth; // 行ヘッダの幅
                }
                cx -= this.HorizontalScrollingOffset; // 全カラム幅からスクロールで見えない分を引く

                //上下の線
                e.Graphics.DrawLine(pen, x - 1, y + 1, cx + 1, y + 1);
                e.Graphics.DrawLine(pen, x - 1, cy - 2, cx + 1, cy - 2);

                // 左端が見える状態なら左縦線を引く
                if (this.HorizontalScrollingOffset == 0)
                {
                    e.Graphics.DrawLine(pen, x + 1, y, x + 1, cy - 2);
                }

                // 右端が見える状態なら右縦線を引く
                if (cx <= this.Width) // 見えてる分の幅がコントロール自体のサイズ以下なら右端が見えるはず
                {
                    e.Graphics.DrawLine(pen, cx, y, cx, cy - 2);
                }
            }
        }

        /// <summary>
        /// セルにフォーカスが移ったときの処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            base.OnCellEnter(e);

            if (IsHeaderChecboxCell(e.ColumnIndex))
            {
                // Set hint for checkbox cell
                this.DisplayHintText(e);
            }

            if (this.IsBrowsePurpose) return;
            if (!this._FocusEnter) return;

            if (this._prevCell != null)
            {
                this._prevCell.UpdateBackColor(false);
            }

            var cell = this[e.ColumnIndex, e.RowIndex];
            if (cell is DgvCustomCheckBoxCell || cell is DataGridViewCheckBoxCell)
            {
                this.ImeMode = ImeMode.Disable;
            }

            if (this.Columns[e.ColumnIndex] is DgvCustomComboBoxColumn)
            {
                this.BeginEdit(false);
                var edtCtl = this.EditingControl as DataGridViewComboBoxEditingControl;
                if (edtCtl != null)
                {
                    edtCtl.DroppedDown = true;
                }
                else
                {
                    this.EndEdit();
                }
            }

            this.DisplayHintText(e);

            this._prevCell = cell;

            if (cell is ICustomDataGridControl)
            {
                (cell as ICustomDataGridControl).Enter(e);
            }
            else
            {
                cell.UpdateBackColor(false);
            }
        }

        /// <summary>
        /// セルからフォーカスアウトされたときのメソッド
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnCellLeave(DataGridViewCellEventArgs e)
        {
            base.OnCellLeave(e);

            if (this.IsBrowsePurpose) return;

            var cell = this[e.ColumnIndex, e.RowIndex];
            if (cell is ICustomDataGridControl)
            {
                (cell as ICustomDataGridControl).Leave(e);
            }
            else
            {
                // 非カスタムコントロールの場合
                // 表示計で、このパターンが多い為、セルの色変えはここでやってやる
                this[e.ColumnIndex, e.RowIndex].UpdateBackColor(false);
            }
        }

        /// <summary>
        ///Enter～Leaveの間 true それ以外はfalse
        ///※これがtrueの間だけフォーカス色に変える必要あり
        ///※データをバインドしたとき等、OnEnterなしでCellEnterのみ単独で動くため。
        /// </summary>
        private bool _FocusEnter = false;

        protected override void OnEnter(EventArgs e)
        {
            this._FocusEnter = true;
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this._FocusEnter = false;

            if (this._prevCell != null)
                this._prevCell.UpdateBackColor(false);

            base.OnLeave(e);
        }

        /// <summary>
        /// セル検証時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            Debug.WriteLine(LogUtility.GetTraceInfo(MethodBase.GetCurrentMethod(), e.RowIndex, e.ColumnIndex, e.FormattedValue));

            // 20151120 MRと同じ処理を追加 Start
            if (e.Cancel) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            // 20151120 MRと同じ処理を追加 End

            if (this.IsBrowsePurpose || this[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                // CustomDataGridviewが閲覧モードで、かつ、セルが入力不可の場合
                base.OnCellValidating(e);
                return;
            }

            // CurrentCellは使わないこと！ カレント以外でもValidatingは発生する！
            int newIndex = this.NewRowIndex;
            var eCell = this[e.ColumnIndex, e.RowIndex];
            var errorFlag = false;
            var editFlag = false;

            // 新コントロールのValidatingは各セルクラスに任せる
            var cCell = eCell as ICustomCell;
            if (cCell != null)
            {
                if (!cCell.CellValidating(e.FormattedValue))
                {
                    e.Cancel = true;
                    // 失敗したら、事後処理を実行。
                    cCell.PostCellValidating(true);

                    var tCtrl = this.EditingControl as TextBoxBase;
                    if (tCtrl != null)
                    {
                        tCtrl.SelectAll();
                    }
                    return;
                }
            }

            ICustomControl iCtrl;
            if (!CustomControlLogic.TryGetCustomCtrl(eCell, out iCtrl))
            {
                base.OnCellValidating(e);
                return;
            }

            // 日付セル専用処理
            var dCell = eCell as DgvCustomDataTimeCell;
            if (dCell != null)
            {
                dCell.IsInputErrorOccured = false;

                if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    string resultValue;
                    DateTime resultValueDt;
                    errorFlag = CheckInputDateTime(e.FormattedValue.ToString(), dCell.OwningColumn as DgvCustomDataTimeColumn, dCell, out resultValue, out resultValueDt);
                    if (errorFlag)
                    {
                        dCell.IsInputErrorOccured = true;
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (dCell.Value is DateTime)
                        {
                            editFlag = true;
                            // 日付型で比較する
                            if (!resultValueDt.Equals(((DateTime)dCell.Value).Date)) // 変更がない場合は代入しない
                            {
                                dCell.Value = resultValueDt;
                                if (dCell.IsInEditMode && this.EditingControl != null)
                                {
                                    this.EditingControl.Text = resultValue;
                                }
                            }
                            else
                            {
                                // 前回値と同じ値で
                                // EditingControlがnullではない かつ 日付に変換できない場合
                                // CheckInputDateTimeで日付Formatされた値を代入
                                if (this.EditingControl != null)
                                {
                                    DateTime resultDt;
                                    if (!DateTime.TryParse(this.EditingControl.Text, out resultDt))
                                    {
                                        this.EditingControl.Text = resultValue;
                                    }
                                }
                            }
                        }
                        else if (dCell.Value is string) // 文字の場合
                        {
                            DateTime dtResult;
                            if (DateTime.TryParse((string)dCell.Value, out dtResult))
                            {
                                // 日付変換して比較する
                                if (!string.IsNullOrEmpty((string)dCell.Value) && !resultValueDt.Equals((DateTime.Parse((string)dCell.Value).Date))) // 変更がない場合は代入しない
                                {
                                    dCell.Value = resultValueDt.ToString("yyyy/mm/dd");
                                    if (dCell.IsInEditMode && this.EditingControl != null)
                                    {
                                        this.EditingControl.Text = resultValue;
                                    }
                                }
                                else
                                {
                                    // 前回値と同じ値で
                                    // EditingControlがnullではない かつ 日付に変換できない場合
                                    // CheckInputDateTimeで日付Formatされた値を代入
                                    if (this.EditingControl != null)
                                    {
                                        DateTime resultDt;
                                        if (!DateTime.TryParse(this.EditingControl.Text, out resultDt))
                                        {
                                            this.EditingControl.Text = resultValue;
                                        }
                                    }
                                }
                            }
                        }
                        else if (dCell.Value is DBNull || dCell.Value == null) // NULLの場合
                        {
                            dCell.Value = resultValueDt;
                            if (dCell.IsInEditMode && this.EditingControl != null)
                            {
                                this.EditingControl.Text = resultValue;
                            }
                        }
                    }
                }
                else
                {
                    if (dCell.OwningColumn.IsDataBound) // 使い分けしてみる
                    {
                        if (!(dCell.Value is DBNull)) // 同値の場合はセットしない
                        {
                            dCell.Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        if (!(dCell.Value == null)) // 同値の場合はセットしない
                        {
                            dCell.Value = null;
                        }
                    }
                }

                if (!editFlag && dCell.IsInEditMode)
                {
                    dCell.DataGridView.EndEdit(); //コミット
                }
            }

            ICustomTextBox iText;
            if (CustomTextBoxLogic.TryGetCustomTextCtrl(eCell, out iCtrl, out iText))
            {
                // ゼロ埋め処理
                var textLogic = new CustomTextBoxLogic(iText);
                textLogic.ZeroPadding(eCell);

                // 自動フォーマット処理
                textLogic.Format(eCell);

                // MaxByte数まで切る
                textLogic.MaxByteCheckAndCut(eCell);
            }

            bool isNewRow = false;
            if (this.CurrentRow != null && this.CurrentRow.IsNewRow)
            {
                isNewRow = true;
            }

            if (!editFlag)
            {
                this.EndEdit(); // ここ以降は編集中考慮できていない
            }

            if (!isNewRow && iCtrl != null)
            {
                // 自動チェック処理
                var cstmLogic = new CustomControlLogic(iCtrl);
                var ctrlUtil = new ControlUtility();
                object[] fields = new object[this.CurrentRow.Cells.Count];
                this.CurrentRow.Cells.CopyTo(fields, 0);
                SuperForm superForm;
                ControlUtility.TryGetSuperForm(this, out superForm);
                errorFlag = cstmLogic.StartingFocusOutCheck(eCell, fields, superForm);
                ControlUtility.SetInputErrorOccuredForDgvCell(eCell, errorFlag);
            }

            // 新コントロール事後処理
            if (cCell != null)
            {
                // 成功失敗問わず、事後処理を実行。(内部は失敗の場合のみ処理する)
                cCell.PostCellValidating(errorFlag);
            }

            // エラーの場合、イベントをキャンセルする(フォーカスを移動させない)
            if (errorFlag)
            {
                e.Cancel = true;
                return;
            }

            // 正常時は色をフォーカスアウト色にする
            eCell.UpdateBackColor(false);

            // チェックしてから、業務や継承コントロール側のチェックへ移る
            // ※イベントを使わずに、オーバーライドする継承先は先にbaseを動かしてから自前チェックすること！
            base.OnCellValidating(e);

            // Validating中のバグが原因で余計に増えた新規行を非同期で削除する。refs #4396
            if (newIndex < this.NewRowIndex)
            {
                Action<int> action = (int delRowsCount) =>
                {
                    // NewRowIndexが指している最下行はコミットしていないため消せないので、
                    // 最下行以外の増えた行を削除する。
                    while (delRowsCount > 0)
                    {
                        this.Rows.RemoveAt(this.Rows.Count - (delRowsCount + 1));
                        delRowsCount--;
                    }
                };
                this.BeginInvoke(action, this.NewRowIndex - newIndex);
            }
        }

        /// <summary>
        /// CellFormattingイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            Debug.WriteLine(LogUtility.GetTraceInfo(MethodBase.GetCurrentMethod(), e.RowIndex, e.ColumnIndex, e.Value));

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            var eCell = this[e.ColumnIndex, e.RowIndex];

            // 新しいセルは自分各セル実装に任せ
            if (eCell is ICustomCell)
            {
                var cCell = eCell as ICustomCell;

                if (!e.FormattingApplied)
                {
                    var cValue = cCell.CellFormatting(e.Value);
                    // 両方の値は等しくない場合のみ再設定(Null又はDBNullのロープを解消する為)
                    if (!object.Equals(e.Value, cValue))
                    {
                        e.Value = cValue;
                    }

                    e.FormattingApplied = true;
                }
            }
            else
            {
                // 日付セル
                if (eCell is DgvCustomDataTimeCell)
                {
                    var dCell = eCell as DgvCustomDataTimeCell;
                    var dCol = dCell.OwningColumn as DgvCustomDataTimeColumn;

                    if (eCell.Value != null)
                    {
                        try
                        {
                            DateTime dt;
                            string value = eCell.Value.ToString();
                            bool check = false;
                            if (eCell.Value is DateTime)
                            {
                                check = true;
                            }

                            if (value.Length > 10) { value = value.Substring(0, 10); }
                            string[] formats = { "yyyyMMdd", "yyyy/MM/dd", "y/M/d", "yyyy/M/d", "M/d", "MM/dd", "MMdd", "yyMMdd" };
                            if (check)
                            {
                                if (dCol.ShowYoubi)
                                {
                                    eCell.Style.Format = "yyyy/MM/dd(ddd)";
                                }
                                else
                                {
                                    eCell.Style.Format = "yyyy/MM/dd";
                                }
                            }
                            else
                            {
                                if (DateTime.TryParseExact(value, formats, null, DateTimeStyles.None, out dt))
                                {
                                    if (dCol.ShowYoubi && !eCell.IsInEditMode) // 編集中は曜日は出さない
                                    {
                                        string s = ((DateTime)dt).ToString("yyyy/MM/dd(ddd)");
                                        if (!s.Equals(eCell.Value))
                                        {
                                            eCell.Value = s;
                                            if (eCell.IsInEditMode && this.EditingControl != null)
                                            {
                                                this.EditingControl.Text = s;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string s = ((DateTime)dt).ToString("yyyy/MM/dd");
                                        if (!s.Equals(eCell.Value))
                                        {
                                            eCell.Value = s;

                                            if (eCell.IsInEditMode && this.EditingControl != null)
                                            {
                                                this.EditingControl.Text = s;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            // エラー無視;
                        }
                    }
                }
            }

            base.OnCellFormatting(e);
        }

        /// <summary>
        /// セルの値が変更された場合、セルが編集モードを終了する
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellParsing(DataGridViewCellParsingEventArgs e)
        {

            Debug.WriteLine(LogUtility.GetTraceInfo(MethodBase.GetCurrentMethod(), e.RowIndex, e.ColumnIndex, e.Value));

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            if (this.IsBrowsePurpose || this[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                // CustomDataGridviewが閲覧モードで、かつ、セルが入力不可の場合
                base.OnCellParsing(e);
                return;
            }

            var eCell = this[e.ColumnIndex, e.RowIndex];

            // 新しいセル
            if (eCell is ICustomCell)
            {
                var cCell = eCell as ICustomCell;

                if (!e.ParsingApplied)
                {
                    var cValue = cCell.CellParsing(e.Value);
                    // 両方の値は等しくない場合のみ再設定(Null又はDBNullのロープを解消する為)
                    if (!object.Equals(e.Value, cValue))
                    {
                        e.Value = cValue;
                    }

                    e.ParsingApplied = true;
                }
            }
            // 日付セル
            else if (eCell is DgvCustomDataTimeCell)
            {
                var dCell = eCell as DgvCustomDataTimeCell;
                var dCol = dCell.OwningColumn as DgvCustomDataTimeColumn;

                if (e.Value == null || e.Value is DBNull || string.IsNullOrEmpty(e.Value.ToString()))
                {
                    if (dCol.IsDataBound)
                    {
                        e.Value = DBNull.Value;
                    }
                    else
                    {
                        e.Value = null;
                    }

                    e.ParsingApplied = true;
                }
                else
                {
                    DateTime dt;
                    string[] formats = { "yyyy/MM/dd", "yyyy/MM/dd(ddd)" };
                    if (e.Value != null && DateTime.TryParseExact(e.Value.ToString(), formats, null, DateTimeStyles.None, out dt))
                    {
                        e.Value = dt;
                        e.ParsingApplied = true; // パースOK
                    }
                }
            }

            base.OnCellParsing(e);
        }

        /// <summary>
        /// 日付のチェック行う
        /// </summary>
        /// <returns>trueだとエラーあり</returns>
        private bool CheckInputDateTime(string value, DgvCustomDataTimeColumn column, DgvCustomDataTimeCell cell, out string resultValue, out DateTime resultValueDt)
        {
            cell.IsInputErrorOccured = false;

            // 日付チェックを共通化
            string errmsg = Validator.CheckDateTimeString(value, column.MaxValue, column.MinValue, out resultValueDt);
            if (!string.IsNullOrEmpty(errmsg))
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(errmsg);
                resultValue = value;
                return true;
            }

            string dateValue = String.Empty;
            if (column.ShowYoubi && !cell.IsInEditMode)
            {
                dateValue = resultValueDt.ToString("yyyy/MM/dd(ddd)");
            }
            else
            {
                dateValue = resultValueDt.ToString("yyyy/MM/dd");
            }

            resultValue = dateValue;
            return false;
        }

        /// <summary>
        /// キー押下時のイベント処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (this.IsBrowsePurpose) return;

            var cell = this.CurrentCell as ICustomDataGridControl;
            if (cell != null)
            {
                cell.PreviewKeyDown(e);
            }
        }

        /// <summary>
        /// KeyDownイベントハンドラ
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Enterキー以外にもこのイベントが発生するため、修正
            //if (e.KeyCode == Keys.Enter && this.CurrentCell == null)
            if (this.CurrentCell == null) return;

            if ((!this.IsBrowsePurpose || !this.CurrentCell.ReadOnly) && e.KeyCode == Keys.Space)
            {
                // CustomDataGridviewが閲覧モードでないか、または、セルが入力可の場合に、SPACEキーが押下された場合
                var target = this.CurrentCell as ICustomControl;
                if (target != null)
                {
                    if (target is DgvCustomDataTimeCell && string.IsNullOrEmpty(target.PopupWindowName))
                    {
                        var dateTimeCell = (DgvCustomDataTimeCell)target;
                        var bk = dateTimeCell.PopupSetFormField;
                        try
                        {
                            dateTimeCell.PopupWindowName = "カレンダーポップアップ";
                            dateTimeCell.PopupSetFormField = dateTimeCell.Name;
                            dateTimeCell.PopUp();
                            e.Handled = true;
                        }
                        finally
                        {
                            dateTimeCell.PopupWindowName = "";
                            dateTimeCell.PopupSetFormField = bk;
                        }
                    }
                    else
                    {
                        target.PopUp();
                        e.Handled = true;
                    }
                }
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// KeyPressイベントハンドラ
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // GRIDでは不要っぽいが、意外なタイミングで発生するときがあるので念のため残しておく。
            // エンター音防止
            if (e.KeyChar == (char)Keys.Enter)
            {
                //e.Handled = true;
            }
            // タブの音防止
            if (e.KeyChar == '\t')
            {
                //e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// キーが押下された時の前処理
        /// ProcessDataGridViewKeyの前に実行
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool PreProcessDataGridViewKey(KeyEventArgs e)
        {
            if ((!this.IsBrowsePurpose || !this.CurrentCell.ReadOnly) && e.KeyData == Keys.Space)
            {
                // CustomDataGridviewが閲覧モードでないか、または、セルが入力可の場合に、SPACEキーが押下された場合
                var textCell = this.CurrentCell as DgvCustomTextBoxCell;
                var dateTimeCell = this.CurrentCell as DgvCustomDataTimeCell;

                if (textCell == null)
                //|| string.IsNullOrEmpty(textCell.PopupWindowName)) // 未設定でもポップアップBeforeメソッドを実行させる
                {
                    //return base.ProcessDataGridViewKey(e);
                    if (dateTimeCell != null && string.IsNullOrEmpty(dateTimeCell.PopupWindowName))
                    {
                        //日付セル専用
                        string bk = dateTimeCell.PopupSetFormField;
                        try
                        {
                            dateTimeCell.PopupWindowName = "カレンダーポップアップ";
                            dateTimeCell.PopupSetFormField = dateTimeCell.Name;
                            dateTimeCell.PopUp();
                        }
                        finally
                        {
                            dateTimeCell.PopupWindowName = "";
                            dateTimeCell.PopupSetFormField = bk;
                        }

                        e.Handled = true;
                        return false;
                    }
                    else if (dateTimeCell != null) //日付でポップアップが明示的に設定されている場合
                    {
                        dateTimeCell.PopUp();
                        e.Handled = true;
                        return false;
                    }
                    else
                    {
                        return base.ProcessDataGridViewKey(e);
                    }
                }

                var ctrlUtil = new ControlUtility();
                object[] sendParamArray = null;

                if (textCell != null)
                {
                    if (textCell.PopupSendParams != null)
                    {
                        sendParamArray = new Control[textCell.PopupSendParams.Length];
                        for (int i = 0; i < textCell.PopupSendParams.Length; i++)
                        {
                            var sendParam = textCell.PopupSendParams[i];
                            sendParamArray[i] = ctrlUtil.FindControl(this.FindForm(), sendParam);
                        }
                    }

                    var customDataGridView = this as CustomDataGridView;
                    object[] fields = new object[customDataGridView.CurrentRow.Cells.Count];
                    customDataGridView.CurrentRow.Cells.CopyTo(fields, 0);

                    // ポップアップウィンドウ表示処理
                    var logic = new CustomControlLogic(textCell, textCell.DisplayPopUp, textCell.ReturnControls);
                    logic.ShowPopupWindow(textCell, fields, customDataGridView.EditingControl, sendParamArray);
                }
                else if (dateTimeCell != null)
                {
                    if (dateTimeCell.PopupSendParams != null)
                    {
                        sendParamArray = new Control[dateTimeCell.PopupSendParams.Length];
                        for (int i = 0; i < dateTimeCell.PopupSendParams.Length; i++)
                        {
                            var sendParam = dateTimeCell.PopupSendParams[i];
                            sendParamArray[i] = ctrlUtil.FindControl(this.FindForm(), sendParam);
                        }
                    }

                    var customDataGridView = this as CustomDataGridView;
                    object[] fields = new object[customDataGridView.CurrentRow.Cells.Count];
                    customDataGridView.CurrentRow.Cells.CopyTo(fields, 0);

                    // ポップアップウィンドウ表示処理
                    var logic = new CustomControlLogic(dateTimeCell, dateTimeCell.DisplayPopUp, dateTimeCell.ReturnControls);
                    logic.ShowPopupWindow(dateTimeCell, fields, customDataGridView.EditingControl, sendParamArray);
                }

                e.Handled = true;
                return false;
            }

            //Enterキーが押された時は、Tabキーが押されたようにする
            if (e.KeyCode == Keys.Enter)
            {
                int currentCol = this.CurrentCellAddress.X;
                int currentRow = this.CurrentCellAddress.Y;
                int colNum = 0;
                int minColNum = 99;

                foreach (DataGridViewColumn column in this.Columns)
                {
                    //    if (!column.Visible)
                    //    {
                    //        continue;
                    //    }
                    //    colNum++;
                    if (column.Visible)
                    {
                        colNum = column.DisplayIndex > colNum ? column.DisplayIndex : colNum;
                        minColNum = column.DisplayIndex < minColNum ? column.DisplayIndex : minColNum;
                    }
                }

                //空のグリッドで例外が発生する対応
                if (this.RowCount == 0)
                {
                    //空なので必ず次コントロールへ行く
                    var form = GetParentSuperForm(this);
                    if (form != null)
                    {
                        var ctrl = form.GetNextControl(this, !e.Shift);
                        if (ctrl != null)
                        {
                            if (ctrl is HScrollBar || !ctrl.TabStop || !ctrl.Enabled)
                            {
                                return form.SelectNextControl(ctrl, !e.Shift, true, true, true);
                            }
                        }
                        return ctrl.Focus();
                    }
                }

                // 始点、終点、未選択
                // 未選択状態の場合カレントセルはNullなので、必ず最初に選択行と列を調べる
                if ((currentRow == -1 && currentCol == -1)
                    || (currentRow == 0 && minColNum == this.CurrentCell.OwningColumn.DisplayIndex && e.Shift)
                    || (this.Rows.Count == currentRow + 1 && colNum == this.CurrentCell.OwningColumn.DisplayIndex && !e.Shift))
                {
                    var form = GetParentSuperForm(this);
                    if (form != null)
                    {
                        var ctrl = form.GetNextControl(this, !e.Shift);
                        if (ctrl != null)
                        {
                            if (ctrl is HScrollBar || !ctrl.TabStop || !ctrl.Enabled)
                            {
                                return form.SelectNextControl(ctrl, !e.Shift, true, true, true);
                            }

                            return ctrl.Focus();
                        }
                        else
                        {
                            return this.ProcessTabKey(e.KeyData);
                        }
                    }
                }
                else
                {
                    return this.ProcessTabKey(e.KeyData);
                }
            }
            return base.ProcessDataGridViewKey(e);
        }

        /// <summary>
        /// キーが押下されたときのイベント処理
        /// </summary>
        /// <param name="sender">押されたキーの情報</param>
        /// <param name="e">イベントハンドラ</param>
        protected virtual void CustomDateGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.IsBrowsePurpose) return;

            var cell = this.CurrentCell as ICustomDataGridControl;
            if (cell != null)
            {
                cell.KeyUp(sender, e);
            }
        }

        /// <summary>
        /// IME変換イベント処理
        /// 1次マスタのフレームワークより
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImeConvertedEvent(object sender, ConvertedEventArgs e)
        {
            var ctrlUtil = new ControlUtility();

            DataGridViewCell cell = this.CurrentCell;
            DataGridViewRow row = this.Rows[cell.RowIndex];
            object[] fields = new object[row.Cells.Count];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                fields[i] = row.Cells[i];
            }

            ICustomControl customCtrl;
            ICustomTextBox customTextCtrl;
            if (CustomTextBoxLogic.TryGetCustomTextCtrl(cell, out customCtrl, out customTextCtrl))
            {
                CustomTextBoxLogic textLogic = new CustomTextBoxLogic(customTextCtrl);

                // フリガナ設定処理
                textLogic.SettingFuriganaPhase1(this, customTextCtrl.FuriganaAutoSetControl, fields, e.YomiString);
            }
        }

        /// <summary>
        /// EditingControlShowingイベントハンドラ
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            base.OnEditingControlShowing(e);

            // フリガナ設定のあるコントロールの場合
            // 1次マスタより
            var textCell = this.CurrentCell as DgvCustomTextBoxCell;
            if (this.EditingControl != null && this.imeFuri == null && textCell != null && !string.IsNullOrWhiteSpace(textCell.FuriganaAutoSetControl))
            {
                this.imeFuri = new NativeWindowContorol(this.EditingControl);
                this.imeFuri.OnConverted += new NativeWindowContorol.Converted(OnImeConvertedEvent);
                this.imeFuri.MsgEnabled = true;
            }

            //表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                //編集のために表示されているコントロールを取得
                var tb = (DataGridViewTextBoxEditingControl)e.Control;

                //イベントハンドラを削除
                tb.KeyUp -= new KeyEventHandler(CustomDateGridView_KeyUp);
                tb.KeyUp += new KeyEventHandler(CustomDateGridView_KeyUp);

                //これだとセル変更をうまく検知できない ⇒ OnCellValueChangedに修正
                //tb.TextChanged -= new EventHandler(CustomDateGridView_TextChanged);
                //tb.TextChanged += new EventHandler(CustomDateGridView_TextChanged);
            }

            //エディティングコントロールの色設定は、ここがよさそう
            //色の反映
            e.Control.BackColor = e.CellStyle.BackColor;
            e.Control.ForeColor = e.CellStyle.ForeColor;

            if (this.EditingPanel != null)
            {
                this.EditingPanel.BackColor = e.CellStyle.BackColor;
                this.EditingPanel.ForeColor = e.CellStyle.ForeColor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            if (this.IsBrowsePurpose) return;

            //値コピー系処理

            if (e.RowIndex >= 0)
            {
                DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];

                ICustomDataGridControl c = cell as ICustomDataGridControl;

                if (c != null) c.TextChanged(e);
            }

            base.OnCellValueChanged(e);
        }

        /// <summary>
        /// ダイアログキー前処理
        /// ProcessDialogKeyの前に実行される
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        private bool PreProcessDialogKey(Keys keyData)
        {
            //Enterキーが押された時は、Tabキーが押されたようにする
            if ((keyData & Keys.KeyCode) == Keys.Enter)
            {
                //int currentCol = this.CurrentCellAddress.X;
                //グリッドの列は DisplayIndex で制御される！ indexとか使わない！
                int currentDisplayIndex = this.CurrentCell.OwningColumn.DisplayIndex;
                int currentRow = this.CurrentCellAddress.Y;
                int maxDisplayIndex = 0;
                int minDisplayIndex = int.MaxValue;

                //最大と最小のdisplayindexを取得
                foreach (DataGridViewColumn column in this.Columns)
                {
                    if (!column.Visible)
                    {
                        continue;
                    }
                    if (maxDisplayIndex < column.DisplayIndex) maxDisplayIndex = column.DisplayIndex;
                    if (minDisplayIndex > column.DisplayIndex) minDisplayIndex = column.DisplayIndex;
                }

                bool forward = true;
                if ((keyData & Keys.Shift) == Keys.Shift)
                {
                    forward = false;
                }

                // 始点、終点、未選択
                if ((currentRow == 0 && currentDisplayIndex <= minDisplayIndex && !forward)
                    || (this.Rows.Count == currentRow + 1 && currentDisplayIndex >= maxDisplayIndex && forward)
                    || (currentRow == -1))
                {
                    var form = GetParentSuperForm(this);
                    if (form != null)
                    {
                        #region 最後のセルが編集中の場合 おかしくなっていた。 フォーカス制御をここでいれることでGetNextControl等無理な処理は不要になった。

                        //if (this.EditingControl != null && this.EditingControl.Focused)
                        //{
                        //    this.Select();
                        //}
                        //var ret = form.SelectNextControl(this, forward, true, true, true);

                        //return ret;
                        // GetNextControlだと グリッドのスクロールバー（HとV)やパネル等 いろいろなものが引っ掛かる

                        //var ctrl = form.GetNextControl(this, forward);
                        //if (ctrl is HScrollBar) ctrl = form.GetNextControl(ctrl, forward);
                        //return form.SelectNextControl(ctrl, forward, true, true, true);

                        #endregion 最後のセルが編集中の場合 おかしくなっていた。 フォーカス制御をここでいれることでGetNextControl等無理な処理は不要になった。

                        // ↑でいろいろ直しましたが、グリッド自身にタブと認識させるのが良いようです。
                        if (forward)
                        {
                            return base.ProcessDialogKey(Keys.Tab);
                        }
                        else
                        {
                            return base.ProcessDialogKey(Keys.Tab | Keys.Shift);
                        }
                    }
                }
                else
                {
                    return this.ProcessTabKey(keyData);
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        #region private

        /// <summary>
        /// ヒントテキスト表示
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        private void DisplayHintText(DataGridViewCellEventArgs e)
        {
            ControlUtility controlUtil = new ControlUtility();
            var hintLabel = controlUtil.FindControl(ControlUtility.GetTopControl(this), "lb_hint") as Label;
            if (hintLabel == null)
            {
                return;
            }

            // 下記の優先順位でヒントテキストを作成します
            // 1. DataGridViewCellのTagプロパティ
            // 2. DataGridViewColumnのToolTipTextプロパティ
            // 3. DataGridViewCellのCharactersNumber、DisplayItemNameプロパティから自動生成
            DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
            if (cell == null)
            {
                return;
            }

            if (cell.Tag != null && !string.IsNullOrEmpty(cell.Tag.ToString()))
            {
                hintLabel.Text = cell.Tag.ToString();
                return;
            }

            DataGridViewColumn colmun = this.Columns[e.ColumnIndex];
            if (!string.IsNullOrEmpty(colmun.ToolTipText))
            {
                hintLabel.Text = colmun.ToolTipText;
                return;
            }

            //ICustomControl customCtrl;
            //if (CustomControlLogic.TryGetCustomCtrl(cell, out customCtrl))
            //{
            //    customCtrl.CreateHintText();
            //}

            hintLabel.Text = (cell.Tag == null ? string.Empty : cell.Tag.ToString());
        }

        /// <summary>
        /// 親のSuperFormクラス取得
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private SuperForm GetParentSuperForm(Control ctrl)
        {
            if (ctrl == null)
            {
                return null;
            }

            var form = ctrl as SuperForm;
            if (form != null)
            {
                return form;
            }

            return GetParentSuperForm(ctrl.Parent);
        }

        #endregion private

        //バグトラブル ST102 ラベルを中央寄せにする ※デザイナで、ColumnHeadersDefaultCellStyleは必ず設定されるため、ここで上書くようにした。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            //センタリング強制
            e.Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //ソートグリフの非表示
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;

            e.Column.DefaultCellStyle.SelectionForeColor = r_framework.Const.Constans.READONLY_COLOR_FORE;

            base.OnColumnAdded(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            if (this.IsBrowsePurpose)
            {
                base.OnRowsAdded(e);
                return;
            }

            for (int rowindex = e.RowIndex; rowindex < e.RowIndex + e.RowCount; rowindex++)
            {
                foreach (DataGridViewCell cell in this.Rows[rowindex].Cells)
                {
                    // CloneChiledを一斉実行
                    var gridControl = cell as ICustomDataGridControl;
                    if (gridControl != null)
                    {
                        gridControl.CloneChiled(); // カラムから情報をコピー(OnPaintはフォーム表示前はまだ動いていないため)
                    }

                    // 読み取り専用の色を付ける
                    cell.UpdateBackColor(false); // 読み取り専用だと最初に色を付ける

                    // ゼロ埋め処理する
                    var iText = cell as ICustomTextBox;
                    if (iText != null)
                    {
                        var textLogic = new CustomTextBoxLogic(iText);
                        // ゼロ埋め処理
                        textLogic.ZeroPadding(cell);
                        // 自動フォーマット処理
                        textLogic.Format(cell);
                        // MaxByte数まで切る
                        textLogic.MaxByteCheckAndCut(cell);
                    }
                }
            }

            base.OnRowsAdded(e);
        }

        // 編集中にホイールされると.netの内部で例外が出るのでその対策
        // 参考 http://ameblo.jp/sachi-u/entry-10246552655.html
        private bool _IsEditing = false;

        /// <summary>
        /// 編集開始
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            this._IsEditing = true;
            base.OnCellBeginEdit(e);
        }

        /// <summary>
        /// 編集終了
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            try
            {
                // フリガナオブジェクトを初期化する
                if (this.imeFuri != null)
                {
                    this.imeFuri.MsgEnabled = false;
                    this.imeFuri.OnConverted -= new NativeWindowContorol.Converted(OnImeConvertedEvent);
                    this.imeFuri = null;
                }

                base.OnCellEndEdit(e);
            }
            finally
            {
                this._IsEditing = false;
            }
        }

        /// <summary>
        /// マウスホイール
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //if (!this._IsEditing) //編集中はスクロールさせない  →これをやると一切マウススクロールできなくなるので不便
            //{
            //    base.OnMouseWheel(e);
            //}
            try
            {
                base.OnMouseWheel(e);
            }
            catch (InvalidCastException ex)
            {
                // .netのバグ？ 例外握り潰しで対応
                LogUtility.Error("編集中にマウスホイールを動かすと例外発生", ex);
            }
        }

        // 2014.08.27 課題 #552 MIYA ADD START
        /// <summary>
        /// セル境界線ダブルクリック
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnDividerDoubleClick(DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            base.OnColumnDividerDoubleClick(e);
            this.AutoResizeColumnHeadersHeight();
        }
        // 2014.08.27 課題 #552 MIYA ADD END

        // 20170608 katen #100422 Ver1.20対応リスト№１２　一覧系の画面にて罫線部分をダブルクリックするとフォーカスが当たっている（赤枠で囲まれている） start
        /// <summary>
        /// セルWクリックイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (Cursor.Current != Cursors.Default)
            {
                return;
            }
            base.OnCellDoubleClick(e);
        }

        /// <summary>
        /// CellMouseDoubleClickイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            if (Cursor.Current != Cursors.Default)
                return;

            base.OnCellMouseDoubleClick(e);
        }
        // 20170608 katen #100422 Ver1.20対応リスト№１２　一覧系の画面にて罫線部分をダブルクリックするとフォーカスが当たっている（赤枠で囲まれている） end

        /// <summary>
        ///
        /// </summary>
        /// <param name="displayErrorDialogIfNoHandler"></param>
        /// <param name="e"></param>
        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            var eCell = this[e.ColumnIndex, e.RowIndex];

            // 数値2の場合、ここに通せ
            if (eCell is DgvCustomNumericTextBox2Cell)
            {
                var eValue = (eCell as DgvCustomNumericTextBox2Cell).CellParsing(eCell.EditedFormattedValue);
                // 両方の値は等しくない場合のみ再設定(Null又はDBNullのロープを解消する為)
                if (!object.Equals(eCell.Value, eValue))
                {
                    eCell.Value = eValue;
                }
                e.Cancel = false;
                return;
            }

            base.OnDataError(displayErrorDialogIfNoHandler, e);
        }

        /// <summary>
        /// セルからフォーカスアウトされたときのメソッド
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.Columns[e.ColumnIndex];
            DgvCustomCheckBoxHeaderCell header = col.HeaderCell as DgvCustomCheckBoxHeaderCell;
            if (header != null)
            {
                header.ColumnHeaderMouseClick(this, e);
            }

            base.OnColumnHeaderMouseClick(e);
        }

        /// <summary>
        /// キーが押下された時の処理
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        /// <returns>キーが処理された場合は true。それ以外の場合は false。</returns>
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                var parentForm = (BaseBaseForm)this.FindForm().ParentForm;
                if (parentForm != null)
                {
                    parentForm.SetEscapedControl(this);
                    parentForm.txb_process.Focus();
                }
                return true;
            }

            var ret = this.PreProcessDataGridViewKey(e);

            if (IsReadOnly()) { return ret; }

            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && this.CurrentCell != null)
            {
                //可視の最後のセル
                var lastVisibleCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);
                //可視の最初のセル
                var firstVisibleCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible).OrderBy(x => x.ColumnIndex).FirstOrDefault(x => true);
                //フォーカスのセットすべき最後のセル
                var lastFocusCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible && !x.ReadOnly).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);
                //フォーカスのセットすべき最初のセル
                var firstFocusCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible && !x.ReadOnly).OrderBy(x => x.ColumnIndex).FirstOrDefault(x => true);

                //移動先が読み取り専用セルだった場合、もう一回動かす ※全セルReadOnlyのグリッドにはこのロジック適用しないように
                if (this.Focused && this.CurrentCell != null && this.CurrentCell.ReadOnly)
                {
                    if ((((firstVisibleCell.ColumnIndex == this.CurrentCell.ColumnIndex) && this.CurrentCell.RowIndex == 0) ||
                        //((lastVisibleCell.ColumnIndex == this.CurrentCell.ColumnIndex) && this.CurrentCell.RowIndex == this.Rows.Count - 1)) && !GetNextForcusControl(!e.Shift))
                        ((lastVisibleCell.ColumnIndex == this.CurrentCell.ColumnIndex) && this.CurrentCell.RowIndex == this.Rows.Count - 1)))
                    {
                        if (e.Shift) { return this.PreProcessDataGridViewKey(new KeyEventArgs(Keys.Tab | Keys.Shift)); }
                        else { return this.PreProcessDataGridViewKey(new KeyEventArgs(Keys.Tab)); }
                    }

                    //タブで移動した場合、focused==trueのまま次コントロールに移動するため無限ループしてしまう。そのためEnterに差し替え
                    if (e.Shift)
                    {
                        ret = this.ProcessDataGridViewKey(new KeyEventArgs(Keys.Enter | Keys.Shift));
                    }
                    else
                    {
                        ret = this.ProcessDataGridViewKey(new KeyEventArgs(Keys.Enter));
                    }

                    //前フォーカスで、グリッドの外に出た時は、最後の入力可能セルに戻る(フォーカスはエディットコントロールも見る必要あり)
                    if (!e.Shift && !(this.Focused || (this.EditingControl != null && this.EditingControl.Focused)))
                    {
                        this.CurrentCell = lastFocusCell;
                    }
                }
                //最終セルで前移動したときの対応
                else if (!e.Shift && this.CurrentCell != null && this.CurrentCell.ColumnIndex == lastVisibleCell.ColumnIndex &&
                    (this.CurrentRow.IsNewRow || this.CurrentRow.Index == this.Rows.Count - 1))
                {
                    this.CurrentCell = lastFocusCell;
                }
            }
            //終了
            return ret;
        }

        /// <summary>
        /// ダイアログキーを処理
        /// </summary>
        /// <param name="keyData">キーコード</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & Keys.KeyCode) == Keys.Escape)
            {
                var parentForm = (BaseBaseForm)this.FindForm().ParentForm;
                if (parentForm != null)
                {
                    parentForm.SetEscapedControl(this);
                    parentForm.txb_process.Focus();
                }
                return true;
            }

            if ((keyData & Keys.KeyCode) == Keys.Space)
            {
                DataGridViewCell cell = this.CurrentCell;
                if (cell is DgvCustomCheckBoxCell)
                {
                    DgvCustomCheckBoxCell checkCell = cell as DgvCustomCheckBoxCell;
                    if (checkCell.ReadOnly)
                    {
                        return true;
                    }

                    checkCell.Value = !Convert.ToBoolean(checkCell.Value is DBNull ? 0 : checkCell.Value);

                    this.RefreshEdit();
                    this.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                else if (cell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;
                    if (checkCell.ReadOnly)
                    {
                        return true;
                    }

                    checkCell.Value = !Convert.ToBoolean(checkCell.Value == null ? 0 : checkCell.Value);

                    this.RefreshEdit();
                    this.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }

            var ret = this.PreProcessDialogKey(keyData);

            if (IsReadOnly()) { return ret; }

            //エンターとタブの場合
            if ((((keyData & Keys.KeyCode) == Keys.Enter) || (keyData & Keys.KeyCode) == Keys.Tab) && this.CurrentCell != null)
            {
                // 可視の行数
                var lastVisibleRows = this.Rows.Cast<DataGridViewRow>().Where(x => x.Visible).OrderByDescending(x => x.Index).FirstOrDefault(x => true);
                //可視の最後のセル
                var lastVisibleCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);
                //可視の最初のセル
                var firstVisibleCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible).OrderBy(x => x.ColumnIndex).FirstOrDefault(x => true);
                //フォーカスのセットすべき最後のセル
                var lastFocusCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible && !x.ReadOnly).OrderByDescending(x => x.ColumnIndex).FirstOrDefault(x => true);
                //フォーカスのセットすべき最初のセル
                var firstFocusCell = this.CurrentRow.Cells.Cast<DataGridViewCell>().Where(x => x.Visible && !x.ReadOnly).OrderBy(x => x.ColumnIndex).FirstOrDefault(x => true);

                //移動先が読み取り専用セルだった場合、もう一回動かす ※Focusedのチェックを漏らすと、グリッドの外に出ると無限ループするので注意
                if (this.Focused && this.CurrentCell != null && this.CurrentCell.ReadOnly)
                {
                    if ((((firstVisibleCell.ColumnIndex == this.CurrentCell.ColumnIndex) && this.CurrentCell.RowIndex == 0) ||
                        //((lastVisibleCell.ColumnIndex == this.CurrentCell.ColumnIndex) && this.CurrentCell.RowIndex == this.Rows.Count - 1)) && !GetNextForcusControl((keyData & Keys.Shift) != Keys.Shift))
                        ((lastVisibleCell.ColumnIndex == this.CurrentCell.ColumnIndex) && this.CurrentCell.RowIndex == lastVisibleRows.Index)))
                    {
                        if ((keyData & Keys.Shift) == Keys.Shift) { return this.PreProcessDialogKey(Keys.Tab | Keys.Shift); }
                        else { return this.PreProcessDialogKey(Keys.Tab); }
                    }

                    if ((keyData & Keys.Shift) == Keys.Shift)
                    {
                        ret = this.ProcessDialogKey(Keys.Enter | Keys.Shift);
                    }
                    else
                    {
                        ret = this.ProcessDialogKey(Keys.Enter);
                    }

                    //前フォーカスで、グリッドの外に出た時は、最後の入力可能セルに戻る(フォーカスはエディットコントロールも見る必要あり)
                    if (((keyData & Keys.Shift) != Keys.Shift) && !(this.Focused || (this.EditingControl != null && this.EditingControl.Focused)))
                    {
                        //可視の最後のセルにフォーカスセット
                        this.CurrentCell = lastFocusCell;
                    }
                }
                //最終セルで前移動したときの対応
                else if ((keyData & Keys.Shift) != Keys.Shift && this.CurrentCell != null && this.CurrentCell.ColumnIndex == lastVisibleCell.ColumnIndex &&
                    (this.CurrentRow.IsNewRow || this.CurrentRow.Index == lastVisibleRows.Index))
                {
                    //可視の最後のセルにフォーカスセット
                    this.CurrentCell = lastFocusCell;
                }
            }
            //終了
            return ret;
        }

        /// <summary>
        /// 次のフォーカス遷移先があるか
        /// </summary>
        /// <param name="foward">
        ///  true  : 前移動
        ///  false : 後移動(Shift押下)
        /// </param>
        /// <returns>
        ///  true  : 移動するコントロールがある
        ///  false : 移動するコントロールがない
        /// </returns>
        private bool GetNextForcusControl(bool foward)
        {
            bool result = false;

            var form = GetParentSuperForm(this);
            if (form == null)
            {
                return result;
            }

            List<Control> controlsList = new List<Control>();
            Control[] controls = ControlUtility.GetAllControlsEx(form);
            foreach (Control ctrl in controls)
            {
                if (ctrl.TabStop && ctrl.Enabled)
                {
                    controlsList.Add(ctrl);
                }
            }
            // TabIndex順にソートする
            controlsList.Sort(delegate(Control x, Control y) { return x.TabIndex - y.TabIndex; });

            for (int i = 0; i < controlsList.Count; i++)
            {
                //if (controlsList[i] is r_framework.CustomControl.CustomDataGridView && i != 0)
                if (controlsList[i] is r_framework.CustomControl.CustomDataGridView)
                {
                    if (foward)
                    {
                        if (i + 1 < controlsList.Count)
                        {
                            if (controlsList[i + 1] != null) { result = true; }
                        }
                    }
                    else
                    {
                        if (i != 0)
                        {
                            if (controlsList[i - 1] != null) { result = true; }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// DataGridView自体と現在行のセルが読み取り専用か判断する
        /// </summary>
        /// <returns>
        /// true: 読み取り専用、false: 読み取り専用ではない
        /// </returns>
        private bool IsReadOnly()
        {
            bool result = true;

            if (this.ReadOnly)
            {
                return result;
            }

            if (this.CurrentRow != null)
            {
                foreach (DataGridViewCell cell in this.CurrentRow.Cells)
                {
                    if (!cell.ReadOnly && cell.Visible)
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// ヘッダチェックボックスをチェック
        /// </summary>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private bool IsHeaderChecboxCell(int colIndex)
        {
            bool ret = false;
            DataGridViewColumn col = this.Columns[colIndex];
            if (col.HeaderCell is DgvCustomCheckBoxHeaderCell)
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// ヘッダチェックボックスをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void headerCell_OnCheckBoxClicked(object sender, DgvCustomCheckboxHeaderEventArgs e)
        {
            CustomDataGridView customDgv = sender as CustomDataGridView;
            if (customDgv == null)
            {
                return;
            }

            int currentRow = -1;
            int currentCell = -1;
            if (customDgv.CurrentCell != null)
            {
                currentRow = customDgv.CurrentCell.RowIndex;
                currentCell = customDgv.CurrentCell.ColumnIndex;
                customDgv.CurrentCell = null;
            }

            foreach (DataGridViewRow dgvRow in customDgv.Rows)
            {
                if (dgvRow.Cells[e.ColumnIndex].ReadOnly == false)
                {
                    dgvRow.Cells[e.ColumnIndex].Value = e.CheckedState;
                }
            }

            if (currentRow >= 0 && currentCell >= 0)
            {
                customDgv.CurrentCell = customDgv[currentCell, currentRow];
            }
        }
    }
}