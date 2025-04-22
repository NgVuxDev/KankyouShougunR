using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Event;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    public partial class GcCustomMultiRow : GcMultiRow
    {
        /// <summary>
        /// 以前の行インデックス
        /// </summary>
        private int _prevRowIndex = -1;

        /// <summary>
        /// 以前のセルインデックス
        /// </summary>
        private int _prevCellIndex = -1;

        /// <summary>
        /// 自動チェックフラグ
        /// </summary>
        private bool _FocusOutCheckedFlag = false;

        /// <summary>
        /// エラーフラグ
        /// </summary>
        private bool _ErrorFlag = false;

        /// <summary>
        /// IMEフリガナ取得用オブジェクト
        /// </summary>
        public NativeWindowContorol imeFuri = null;

        /// <summary>
        /// 閲覧目的かどうか。trueならメモリ抑制処理。
        /// 一覧画面などで利用する場合にtrueにする。
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        public bool IsBrowsePurpose { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomMultiRow()
        {
            //バグトラブル ST102 ラベルを中央寄せにする
            this.ColumnHeadersDefaultHeaderCellStyle.TextAlign = MultiRowContentAlignment.MiddleCenter;

            //エンターでタブ移動
            this.ShortcutKeyManager.Unregister(Keys.Enter);
            this.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);

            // Escキーに割り当てられている既定のキャンセルアクションを解除します http://www.grapecity.com/tools/support/technical/knowledge_detail.asp?id=31372
            this.ShortcutKeyManager.Unregister(Keys.Escape);
            //新アクションは無し

            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public GcCustomMultiRow(IContainer container)
        {
            container.Add(this);

            this.ShortcutKeyManager.Unregister(Keys.Enter);
            this.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);
            //バグトラブル ST102 ラベルを中央寄せにする
            this.ColumnHeadersDefaultHeaderCellStyle.TextAlign = MultiRowContentAlignment.MiddleCenter;

            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // コンストラクタでは効かなかった
            // バグトラブル ST102 ラベルを中央寄せにする
            this.ColumnHeadersDefaultHeaderCellStyle.TextAlign = MultiRowContentAlignment.MiddleCenter;

            // エンターでタブ移動
            this.ShortcutKeyManager.Unregister(Keys.Enter);
            this.ShortcutKeyManager.Unregister(Keys.Tab);
            this.ShortcutKeyManager.Register(new GcCustomMoveToNextContorol(), Keys.Enter);
            this.ShortcutKeyManager.Register(new GcCustomMoveToNextContorol(), Keys.Tab);

            // #1781 Shift+Enterも対応  http://www.grapecity.com/tools/support/technical/knowledge_detail.asp?id=25835
            this.ShortcutKeyManager.Unregister(Keys.Enter | Keys.Shift);
            this.ShortcutKeyManager.Unregister(Keys.Tab | Keys.Shift);
            this.ShortcutKeyManager.Register(new GcCustomMoveToPreviousContorol(), Keys.Enter | Keys.Shift);
            this.ShortcutKeyManager.Register(new GcCustomMoveToPreviousContorol(), Keys.Tab | Keys.Shift);

            // Escキーに割り当てられている既定のキャンセルアクションを解除します http://www.grapecity.com/tools/support/technical/knowledge_detail.asp?id=31372
            this.ShortcutKeyManager.Unregister(Keys.Escape);

            this.ShowCellToolTips = false;
            this.MultiSelect = false; //複数セル選択禁止

            // 自動リサイズ 下部にコントロール配置している画面がおかしくなるので一旦封印
            //if (this.Anchor == (AnchorStyles.Top | AnchorStyles.Left)) //デフォルトから変えてない場合のみ。
            //    this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            if (this.IsBrowsePurpose)
            {
                this.ImeMode = ImeMode.Disable;
                this.DefaultCellStyle.SelectionForeColor = r_framework.Const.Constans.READONLY_COLOR_FORE;
            }
            else
            {
                // 新アクションは無し
                this.EditMode = EditMode.EditOnEnter; // 編集モード統一

                // タブコントロール等で後ろに隠れている場合など対策
                foreach (Row row in this.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cell.UpdateBackColor(false); // 読み取り専用だと最初に色を付ける
                    }
                }
            }

            // タブコントロール等で後ろに隠れている場合など対策
            foreach (Row row in this.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
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
        protected override void OnCellPainting(CellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (this.DesignMode) return;
            if (e.CellIndex < 0 || e.RowIndex < 0) return;
            if (!this.IsBrowsePurpose) return;

            Color color = e.CellStyle.BackColor;

            bool hasFocus = this.Focused && e.Selected;
            bool readOnly = this.ReadOnly || this[e.RowIndex, e.CellIndex].ReadOnly;

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

            //e.PaintBackground(e.ClipBounds); // DGVの「e.PaintParts & ~DataGridViewPaintParts.Background」と同じ
            e.PaintForeground(e.ClipBounds);
            e.PaintBorder(e.ClipBounds);
            e.Handled = true;
        }

        /// <summary>
        /// セルフォーカスイン処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellEnter(CellEventArgs e)
        {
            if (this.IsBrowsePurpose) return;
            if (!this._FocusEnter) return;

            if (e.Scope == CellScope.Row)
            {
                Cell pCell;
                bool ret = this.TryGetCell(this._prevRowIndex, this._prevCellIndex, out pCell);
                if (ret)
                {
                    pCell.UpdateBackColor(false);
                }

                // 前回値保持
                Cell cCell = this.CurrentCell;
                if (cCell == this[e.RowIndex, e.CellIndex])
                {
                    cCell.UpdateBackColor(true); // 拡張メソッド

                    // インデックス保存
                    this._prevRowIndex = e.RowIndex;
                    this._prevCellIndex = e.CellIndex;
                    this._FocusOutCheckedFlag = false;

                    // ヒントテキスト
                    this.DisplayHintText(cCell);

                    SuperForm form;
                    if (ControlUtility.TryGetSuperForm(this, out form))
                    {
                        ICustomControl ctrl;
                        if (CustomControlLogic.TryGetCustomCtrl(cCell, out ctrl) && form.PreviousSaveFlag)
                        {
                            // 前回値を保持
                            form.SetPreviousValue(ctrl.GetResultText(), ctrl);

                            var cstmLogic = new CustomControlLogic(ctrl);
                            object[] obj = cstmLogic.GetPreviousControlForMultiRow();
                            form.SetPreviousControlValue(obj);
                        }
                    }
                }
            }

            base.OnCellEnter(e);
        }

        /// <summary>
        /// セルフォーカスアウト処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellLeave(CellEventArgs e)
        {
            if (this.IsBrowsePurpose) return;

            if (this._ErrorFlag)
            {
                this.Focus();
                return;
            }

            if (e.Scope == CellScope.Row)
            {
                Row pRow;
                Cell pCell;
                bool ret = this.TryGetCellAndRow(this._prevRowIndex, this._prevCellIndex, out pCell, out pRow);
                if (!ret)
                {
                    base.OnCellLeave(e);
                    return;
                }

                if (pRow.IsNewRow)
                {
                    this._FocusOutCheckedFlag = true;
                }

                ICustomControl customCtrl;
                if (!CustomControlLogic.TryGetCustomCtrl(pCell, out customCtrl))
                {
                    base.OnCellLeave(e);
                    return;
                }

                // Add
                if (e.RowIndex < 0 || e.CellIndex < 0)
                {
                    base.OnCellLeave(e);
                    return;
                }
                else
                {
                    this[e.RowIndex, e.CellIndex].UpdateBackColor(false);
                }
            }

            base.OnCellLeave(e);
        }

        // Enter～Leaveの間 true それ以外はfalse
        // ※これがtrueの間だけフォーカス色に変える必要あり
        // ※データをバインドしたとき等、OnEnterなしでCellEnterのみ単独で動くため。
        private bool _FocusEnter = false;
        private bool _FocusLeave = false; // OnLeaveでエラーを出すと何度も出るため

        protected override void OnEnter(EventArgs e)
        {
            this._FocusEnter = true;
            base.OnEnter(e);
        }

        /// <summary>
        /// フォーカスアウト処理
        /// Esc押下時 OnCellLeave が起動しない為、こちらでも処理を実行する。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            this._FocusEnter = false;

            Row pRow;
            Cell pCell;
            bool ret = this.TryGetCellAndRow(this._prevRowIndex, this._prevCellIndex, out pCell, out pRow);

            //F12対策 だまって閉じる
            var cForm = this.FindForm();
            if (cForm != null)
            {
                var pForm = cForm.Parent as Form;
                if (pForm != null && pForm.ActiveControl != null)
                {
                    if (!pForm.ActiveControl.CausesValidation || pForm.AutoValidate == AutoValidate.Disable)
                    {
                        if (pForm.AutoValidate == AutoValidate.Disable)
                        {
                            // ActiveControl.CausesValidationがFalseのケースはここでは考慮しない
                            // ControlUtility.ClickButtonメソッド内で判定 #12198
                            this.CausesValidation = false;
                            //base.OnLeave(e);
                        }

                        return;
                    }
                }
            }

            //外部へフォーカスを出させないのでValidatingだけでなく、ここでもチェックする
            if (this._ErrorFlag && !this._FocusOutCheckedFlag && !this._FocusLeave)
            {
                this._FocusLeave = true;
                try
                {
                    ICustomControl customCtrl;
                    if (!CustomControlLogic.TryGetCustomCtrl(pCell, out customCtrl))
                    {
                        base.OnLeave(e);
                        return;
                    }

                    //ポップアップを出すことでフォーカスを維持
                    // 自動チェック処理
                    SuperForm sForm;
                    if (!this._FocusOutCheckedFlag && ControlUtility.TryGetSuperForm(this, out sForm))
                    {
                        int controlCount = pRow.Cells.Count + sForm.allControl.Length;

                        List<object> controls = new List<object>();
                        for (int i = 0; i < pRow.Cells.Count; i++)
                        {
                            controls.Add(pRow.Cells[i]);
                        }
                        for (int i = 0; i < sForm.allControl.Length; i++)
                        {
                            controls.Add(sForm.allControl[0]);
                        }

                        this.EndEdit();

                        var logic = new CustomControlLogic(customCtrl);
                        this._ErrorFlag = logic.StartingFocusOutCheck(pCell, controls.ToArray(), sForm);
                        this._FocusOutCheckedFlag = true;
                        ControlUtility.SetInputErrorOccuredForMultiRow(pCell, this._ErrorFlag);
                    }
                }
                finally
                {
                    this._FocusLeave = false;
                }
            }

            if (this._ErrorFlag)
            {
                this.Focus();
                return;
            }

            if (ret)
            {
                // 背景色変更解除
                pCell.UpdateBackColor(false);
            }

            base.OnLeave(e);
        }

        /// <summary>
        /// 検証中処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (this._ErrorFlag)
            {
                e.Cancel = true;
                //this.PrevBack(); // これがあるとLeaveとCellValidatingで二度メッセージが出る。(フォーカスセットはCancel=TrueだけでOK)
            }

            base.OnValidating(e);
        }

        /// <summary>
        /// 選択変更処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(EventArgs e)
        {
            if (this._ErrorFlag)
                this.PrevBack();

            base.OnSelectionChanged(e);
        }

        /// <summary>
        /// セルフォーカスアウト処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValidating(CellValidatingEventArgs e)
        {
            Debug.WriteLine(LogUtility.GetTraceInfo(MethodBase.GetCurrentMethod(), e.RowIndex, e.CellIndex, e.FormattedValue));

            if (e.Cancel) return;
            if (e.RowIndex < 0 || e.CellIndex < 0) return;

            if (this.IsBrowsePurpose || this[e.RowIndex, e.CellIndex].ReadOnly)
            {
                // MRが閲覧モードで、かつ、セルが入力不可の場合
                base.OnCellValidating(e);
                return;
            }

            Cell eCell = this[e.RowIndex, e.CellIndex];
            Row eRow = this.Rows[e.RowIndex];

            if (eRow.IsNewRow)
            {
                this._FocusOutCheckedFlag = true;
            }

            // 新コントロールのValidatingは各セルクラスに任せる
            var cCell = eCell as ICustomCell;
            if (cCell != null)
            {
                if (!cCell.CellValidating(e.FormattedValue))
                {
                    this._ErrorFlag = true;
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

            //日付セルのvalidate処理が必要
            if (eCell is GcCustomDataTime)
            {
                var dCell = eCell as GcCustomDataTime;
                dCell.IsInputErrorOccured = false;

                if (eCell.IsInEditMode)
                {
                    //編集中の場合
                    if (eCell.EditedFormattedValue == null)
                    {
                        eCell.Value = DBNull.Value; //NULLではなくDBNULL
                        this.EndEdit(); //チェック済みなのでEndEdit OK
                    }
                    else
                    {
                        var value = eCell.EditedFormattedValue.ToString();

                        var _maxdate = dCell.MaxValue;
                        var _mindate = dCell.MinValue;

                        DateTime dt;

                        //複数書式でパース率を上げる
                        string[] formats = { "yyyyMMdd", "yyyy/MM/dd", "y/M/d", "yyyy/M/d", "M/d", "MM/dd", "MMdd", "yyMMdd" };

                        if (DateTime.TryParseExact(value, formats, null, System.Globalization.DateTimeStyles.None, out dt))
                        {
                            if (dt > _maxdate)
                            {
                                dCell.IsInputErrorOccured = true;
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E042", "[ " + _maxdate.ToString("yyyy/MM/dd") + " ]以下");
                                e.Cancel = true;
                                (this.EditingControl as GcCustomeDateTimeTextBoxEditingControl).SelectAll();
                                return;
                            }
                            else if (dt < _mindate)
                            {
                                dCell.IsInputErrorOccured = true;
                                var messageShowLogic = new MessageBoxShowLogic();
                                messageShowLogic.MessageBoxShow("E042", "[ " + _mindate.ToString("yyyy/MM/dd") + " ]以上");
                                e.Cancel = true;
                                (this.EditingControl as GcCustomeDateTimeTextBoxEditingControl).SelectAll();
                                return;
                            }
                            else
                            {
                                eCell.Value = dt;
                                this.EditingControl.Text = dt.ToString("yyyy/MM/dd");
                                this.EndEdit(); //チェック済みなのでEndEdit OK
                            }
                        }
                        else
                        {
                            dCell.IsInputErrorOccured = true;
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E084", value);
                            e.Cancel = true;
                            (this.EditingControl as GcCustomeDateTimeTextBoxEditingControl).SelectAll();
                            return;
                        }
                    }
                }
            }

            // 自動チェック処理
            SuperForm superForm;
            if (!this._FocusOutCheckedFlag && ControlUtility.TryGetSuperForm(this, out superForm))
            {
                int controlCount = eRow.Cells.Count + superForm.allControl.Length;

                List<object> controls = new List<object>();

                for (int i = 0; i < eRow.Cells.Count; i++)
                {
                    controls.Add(eRow.Cells[i]);
                }

                for (int i = 0; i < superForm.allControl.Length; i++)
                {
                    controls.Add(superForm.allControl[0]);
                }
                this.EndEdit(); // 日付チェック後であればEndEdit OK

                var logic = new CustomControlLogic(iCtrl);
                this._ErrorFlag = logic.StartingFocusOutCheck(eCell, controls.ToArray(), superForm);
                this._FocusOutCheckedFlag = true;
                e.Cancel = this._ErrorFlag;

                ControlUtility.SetInputErrorOccuredForMultiRow(eCell, this._ErrorFlag);
            }

            // 新コントロール事後処理
            if (cCell != null)
            {
                // 成功失敗問わず、事後処理を実行。(内部は失敗の場合のみ処理する)
                cCell.PostCellValidating(this._ErrorFlag);
            }

            if (this._ErrorFlag)
            {
                return;
            }

            //base.OnCellLeave(e);
            base.OnCellValidating(e);
        }

        /// <summary>
        /// セル値変更処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellValueChanged(CellEventArgs e)
        {
            if (this.IsBrowsePurpose)
            {
                base.OnCellValueChanged(e);
                return;
            }

            if (e.Scope != CellScope.Row)
            {
                base.OnCellValueChanged(e);
                return;
            }

            Cell eCell = this[e.RowIndex, e.CellIndex];
            Row eRow = this.Rows[e.RowIndex];

            ICustomControl cCtrl;
            ICustomTextBox tCtrl;
            if (CustomTextBoxLogic.TryGetCustomTextCtrl(eCell, out cCtrl, out tCtrl))
            {
                CustomTextBoxLogic textLogic = new CustomTextBoxLogic(tCtrl);

                // ゼロ埋め処理
                textLogic.ZeroPadding(eCell);

                // 自動フォーマット処理
                textLogic.Format(eCell);

                // 大文字変換処理
                textLogic.ChangeUpperCase(eCell);

                // 全角変換処理
                textLogic.ChangeWideCase(eCell);

                // byte数チェック
                textLogic.MaxByteCheckAndCut(eCell);

                // フリガナ自動入力処理
                // フリガナ変換処理を1次マスタに合わせる為にコメントアウト
                //textLogic.SettingFurigana(eCell, eRow.Cells.ToArray());

                // 1次マスタのフレームワークより
                if (eCell.Value == null || string.IsNullOrWhiteSpace(eCell.Value.ToString()))
                {
                    textLogic.SettingFuriganaPhase1(this, tCtrl.FuriganaAutoSetControl, eRow.Cells.ToArray(), string.Empty);
                }

                // 自動複写処理
                textLogic.SettingCopyValue(eCell, eRow.Cells.ToArray());
            }

            base.OnCellValueChanged(e);
        }

        /// <summary>
        /// IME変換イベント処理
        /// 1次マスタのフレームワークより
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnImeConvertedEvent(object sender, ConvertedEventArgs e)
        {
            var ctrlUtil = new ControlUtility();

            Cell cell = this.CurrentCell;
            Row row = this.Rows[cell.RowIndex];
            ICustomControl customCtrl;
            ICustomTextBox customTextCtrl;
            if (CustomTextBoxLogic.TryGetCustomTextCtrl(cell, out customCtrl, out customTextCtrl))
            {
                CustomTextBoxLogic textLogic = new CustomTextBoxLogic(customTextCtrl);

                // フリガナ設定処理
                textLogic.SettingFuriganaPhase1(this, customTextCtrl.FuriganaAutoSetControl, row.Cells.ToArray(), e.YomiString);
            }
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (this.IsBrowsePurpose)
            {
                base.OnPreviewKeyDown(e);
                return;
            }

            // スペースの場合、ポップアップ表示
            if (this.RowCount > 0 && e.KeyCode == Keys.Space)
            {
                // CheckBoxCell は OnEditingControlShowing で対応していない為
                // こちらで対応する。
                var cell = this.CurrentCell as GcCustomCheckBoxCell;
                if (cell == null)
                {
                    return;
                }

                //ReadOnlyもEditingControl使わないのでここでポップアップする
                if (this.CurrentCell.ReadOnly)
                {
                    var c = this.CurrentCell as ICustomControl;
                    if (c != null) c.PopUp(); // ReadOnlyでポップアップ出すかどうかはロジック内でチェックします
                    return;
                }

                Row row = this.Rows[cell.RowIndex];
                var ctrlUtil = new ControlUtility();

                object[] sendParamArray = null;
                if (cell.PopupSendParams != null)
                {
                    sendParamArray = new Control[cell.PopupSendParams.Length];
                    for (int i = 0; i < cell.PopupSendParams.Length; i++)
                    {
                        var sendParam = cell.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(this.FindForm(), sendParam);
                    }
                }
                // ポップアップウィンドウ表示処理
                var logic = new CustomControlLogic(cell);
                logic.ShowPopupWindow(cell, row.Cells.ToArray(), cell, sendParamArray);

                return;
            }

            base.OnPreviewKeyDown(e);
        }

        // 20170608 katen #100422 Ver1.20対応リスト№１２　一覧系の画面にて罫線部分をダブルクリックするとフォーカスが当たっている（赤枠で囲まれている） start
        /// <summary>
        /// セルWクリックイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellDoubleClick(CellEventArgs e)
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
        protected override void OnCellMouseDoubleClick(CellMouseEventArgs e)
        {
            if (Cursor.Current != Cursors.Default)
                return;

            base.OnCellMouseDoubleClick(e);
        }
        // 20170608 katen #100422 Ver1.20対応リスト№１２　一覧系の画面にて罫線部分をダブルクリックするとフォーカスが当たっている（赤枠で囲まれている） end

        /// <summary>
        /// セル編集開始処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellBeginEdit(CellBeginEditEventArgs e)
        {
            if (e.Scope != CellScope.Row)
            {
                base.OnCellBeginEdit(e);
                return;
            }

            base.OnCellBeginEdit(e);
        }

        /// <summary>
        /// セル編集コントロール表示処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEditingControlShowing(EditingControlShowingEventArgs e)
        {
            // フリガナ設定のあるコントロールの場合
            // 1次マスタより
            var textCell = this.CurrentCell as GcCustomTextBoxCell;
            if (this.EditingControl != null && this.imeFuri == null && textCell != null && !string.IsNullOrWhiteSpace(textCell.FuriganaAutoSetControl))
            {
                this.imeFuri = new NativeWindowContorol(this.EditingControl);
                this.imeFuri.OnConverted += new NativeWindowContorol.Converted(OnImeConvertedEvent);
                this.imeFuri.MsgEnabled = true;
            }

            // ポップアップ表示
            if (e.Control is TextBoxEditingControl ||
                e.Control is ComboBoxEditingControl ||
                e.Control is DateTimePickerEditingControl)
            {
                e.Control.KeyDown -= new KeyEventHandler(editCtrl_Popup_KeyDown);
                e.Control.KeyDown += new KeyEventHandler(editCtrl_Popup_KeyDown);

                e.Control.KeyPress -= new KeyPressEventHandler(editCtrl_Popup_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(editCtrl_Popup_KeyPress);
            }

            // 日時セル初期化
            if (e.Control is DateTimePickerEditingControl)
            {
                e.Control.KeyUp -= new KeyEventHandler(editCtrl_DateTimeClear_KeyUp);
                e.Control.KeyUp += new KeyEventHandler(editCtrl_DateTimeClear_KeyUp);
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

            base.OnEditingControlShowing(e);
        }

        /// <summary>
        /// セル編集終了処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellEndEdit(CellEndEditEventArgs e)
        {
            if (this.imeFuri != null)
            {
                this.imeFuri.MsgEnabled = false;
                this.imeFuri.OnConverted -= new NativeWindowContorol.Converted(OnImeConvertedEvent);
                this.imeFuri = null;
            }

            base.OnCellEndEdit(e);
        }

        /// <summary>
        /// データエラー発生時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDataError(DataErrorEventArgs e)
        {
            var eCell = this[e.RowIndex, e.CellIndex];
            // 数値2の場合、ここに通せ
            if (eCell is GcCustomNumericTextBox2Cell)
            {
                var eValue = (eCell as GcCustomNumericTextBox2Cell).CellParsing(eCell.EditedFormattedValue);
                // 両方の値は等しくない場合のみ再設定(Null又はDBNullのロープを解消する為)
                if (!object.Equals(eCell.Value, eValue))
                {
                    eCell.Value = eValue;
                }
                e.Cancel = false;
                return;
            }

            base.OnDataError(e);
        }

        /// <summary>
        /// 編集コントロール押下時処理
        /// ポップアップ表示時のスペースキー入力の取消対応
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void editCtrl_Popup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                Cell cell = this.CurrentCell;
                if (cell == null)
                {
                    return;
                }

                ICustomControl customCtrl;
                if (!CustomControlLogic.TryGetCustomCtrl(cell, out customCtrl))
                {
                    return;
                }

                // PopupWindowName設定済みの場合入力取消
                if (!string.IsNullOrEmpty(customCtrl.PopupWindowName))
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true; //beep音対策
            }
        }

        /// <summary>
        /// 編集コントロール押下時処理
        /// ポップアップ表示用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void editCtrl_Popup_KeyDown(object sender, KeyEventArgs e)
        {
            // スペースの場合、ポップアップ表示
            if (e.KeyCode == Keys.Space)
            {
                Cell cell = this.CurrentCell;
                if (cell == null)
                {
                    return;
                }
                Row row = this.Rows[cell.RowIndex];

                // ポップアップウィンドウ表示処理
                ICustomControl customCtrl;
                if (!CustomControlLogic.TryGetCustomCtrl(cell, out customCtrl))
                {
                    return;
                }

                var logic = new CustomControlLogic(customCtrl);

                var ctrlUtil = new ControlUtility();
                object[] sendParamArray = null;

                if (customCtrl.PopupSendParams != null)
                {
                    sendParamArray = new Control[customCtrl.PopupSendParams.Length];
                    for (int i = 0; i < customCtrl.PopupSendParams.Length; i++)
                    {
                        var sendParam = customCtrl.PopupSendParams[i];
                        sendParamArray[i] = ctrlUtil.FindControl(this.FindForm(), sendParam);
                    }
                }

                logic.ShowPopupWindow(cell, row.Cells.ToArray(), sender, sendParamArray);
            }
        }

        /// <summary>
        /// 編集コントロール押下時処理
        /// 日時セル初期化用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void editCtrl_DateTimeClear_KeyUp(object sender, KeyEventArgs e)
        {
            //★★★
            //var eCell = this.CurrentCell as GcCustomDataTime;
            //if (eCell == null)
            //{
            //    return;
            //}

            switch (e.KeyCode)
            {
                case Keys.Delete:
                case Keys.Back:
                    //★★★
                    //eCell.ClearLock();
                    //this.EndEdit();
                    //eCell.Value = DBNull.Value;
                    break;
            }
        }

        /// <summary>
        /// ヒントテキスト表示
        /// </summary>
        /// <param name="eCell"></param>
        private void DisplayHintText(Cell cell)
        {
            ControlUtility controlUtil = new ControlUtility();
            var hintLabel = (Label)controlUtil.FindControl(ControlUtility.GetTopControl(this), "lb_hint");
            if (hintLabel == null)
            {
                return;
            }

            //ICustomControl customCtrl;
            //if (CustomControlLogic.TryGetCustomCtrl(cell, out customCtrl))
            //{
            //    customCtrl.CreateHintText();
            //}

            hintLabel.Text = (string)(cell.Tag == null ? string.Empty : cell.Tag);
        }

        /// <summary>
        /// エラー時にフォーカスを元に戻す処理
        /// </summary>
        private void PrevBack()
        {
            Cell cell;
            if (this.TryGetCell(this._prevRowIndex, this._prevCellIndex, out cell))
            {
                this.Focus();
                this.CurrentCell = cell;
                this.BeginEdit(true);
            }
            this._ErrorFlag = false;
        }

        /// <summary>
        /// 表示用のテキスト等に変換
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellFormatting(CellFormattingEventArgs e)
        {
            Debug.WriteLine(LogUtility.GetTraceInfo(MethodBase.GetCurrentMethod(), e.RowIndex, e.CellIndex, e.Value));

            if (e.RowIndex < 0 || e.CellIndex < 0)
                return;

            var eCell = this[e.RowIndex, e.CellIndex];
            // 新しいセル
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

            base.OnCellFormatting(e);
        }

        /// <summary>
        /// 表示用のテキストから、内部用の値に変換
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellParsing(CellParsingEventArgs e)
        {
            Debug.WriteLine(LogUtility.GetTraceInfo(MethodBase.GetCurrentMethod(), e.RowIndex, e.CellIndex, e.Value));

            if (e.CellIndex < 0 || e.RowIndex < 0) return;

            if (this.IsBrowsePurpose && this[e.RowIndex, e.CellIndex].ReadOnly)
            {
                // CustomDataGridviewが閲覧モードで、かつ、セルが入力不可の場合
                base.OnCellParsing(e);
                return;
            }

            var eCell = this[e.RowIndex, e.CellIndex];
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

            base.OnCellParsing(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowsAdded(RowsAddedEventArgs e)
        {
            if (this.IsBrowsePurpose)
            {
                base.OnRowsAdded(e);
                return;
            }

            // 読み取り専用の色を付ける
            for (int rowindex = e.RowIndex; rowindex < e.RowIndex + e.RowCount; rowindex++)
            {
                foreach (Cell cell in this.Rows[rowindex].Cells)
                {
                    cell.UpdateBackColor(false); // 読み取り専用だと最初に色を付ける

                    //ゼロ埋め処理する
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

        // 最後や最初のセルでは 次コントロールへ移動させる
        // http://www.grapecity.com/tools/support/technical/knowledge_detail.asp?id=34497
        // TODO:タブインデックスを見て判断する必要あり
        /// <summary>
        /// 前用
        /// </summary>
        public class GcCustomMoveToNextContorol : IAction
        {
            public bool CanExecute(GcMultiRow target)
            {
                return true;
            }

            public string DisplayName
            {
                get { return this.ToString(); }
            }

            public void Execute(GcMultiRow target)
            {
                // 空のグリッドで例外が発生する対応
                if (target.RowCount == 0)
                {
                    // データ無は次のコントロールへ
                    ComponentActions.SelectNextControl.Execute(target);
                    return;
                }

                Boolean isLastRow = (target.CurrentCellPosition.RowIndex == target.RowCount - 1);

                //int tabMaxIndex = 0;
                //int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

                if (isLastRow)
                {
                    int tabMaxIndex = 0;
                    int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

                    for (int i = 0; i < target.Template.Row.Cells.Count; i++)
                    {
                        if (target.Template.Row.Cells[i].TabStop)
                        {
                            if (tabMaxIndex < target.Template.Row.Cells[i].TabIndex)
                            {
                                tabMaxIndex = target.Template.Row.Cells[i].TabIndex;
                            }
                        }
                    }

                    if (tabMaxIndex <= currentTabIndex)
                    {
                        // 最後のセルでは次のコントロールへ移動します。
                        ComponentActions.SelectNextControl.Execute(target);
                        //SelectNextControl(target., true, true, true, true);
                    }
                    else
                    {
                        // 最後のセル以外のセルでは次のセルへ移動します。
                        SelectionActions.MoveToNextCell.Execute(target);
                    }
                }
                else
                {
                    // 最後のセル以外のセルでは次のセルへ移動します。
                    SelectionActions.MoveToNextCell.Execute(target);
                }
            }
        }

        /// <summary>
        /// 後用
        /// </summary>
        public class GcCustomMoveToPreviousContorol : IAction
        {
            public bool CanExecute(GcMultiRow target)
            {
                return true;
            }

            public string DisplayName
            {
                get { return this.ToString(); }
            }

            public void Execute(GcMultiRow target)
            {
                //空のグリッドで例外が発生する対応
                if (target.RowCount == 0)
                {
                    //データ無は次のコントロールへ
                    ComponentActions.SelectPreviousControl.Execute(target);
                    return;
                }

                Boolean isFirstRow = (target.CurrentCellPosition.RowIndex == 0);

                //int tabMinIndex = -1;
                //int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

                if (isFirstRow)
                {
                    int tabMinIndex = -1;
                    int currentTabIndex = target.Template.Row.Cells[target.CurrentCellPosition.CellIndex].TabIndex;

                    for (int i = 0; i < target.Template.Row.Cells.Count; i++)
                    {
                        if (target.Template.Row.Cells[i].TabStop)
                        {
                            if (tabMinIndex > target.Template.Row.Cells[i].TabIndex)
                            {
                                tabMinIndex = target.Template.Row.Cells[i].TabIndex;
                            }
                            else if (tabMinIndex == -1)
                            {
                                tabMinIndex = target.Template.Row.Cells[i].TabIndex;
                            }
                        }
                    }

                    if (tabMinIndex == currentTabIndex)
                    {
                        //最初のセルでは前のコントロールへ移動します。
                        ComponentActions.SelectPreviousControl.Execute(target);
                    }
                    else
                    {
                        // 最初のセル以外のセルでは前のセルへ移動します。
                        SelectionActions.MoveToPreviousCell.Execute(target);
                    }
                }
                else
                {
                    // 最初のセル以外のセルでは前のセルへ移動します。
                    SelectionActions.MoveToPreviousCell.Execute(target);
                }
            }
        }
    }
}