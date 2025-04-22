using System.Drawing;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Utility;
using DgvCell = System.Windows.Forms.DataGridViewCell;
using MrCell = GrapeCity.Win.MultiRow.Cell;

namespace r_framework.Logic
{
    /// <summary>
    /// カスタムコントロール
    /// </summary>
    public static class CustomControlExtLogic
    {
        /// <summary>
        /// ポップアップを呼び出す
        /// </summary>
        /// <param name="ctl">ポップアップを出したいコントロール(PopupWindowName等設定済み)</param>
        /// <returns>Popup設定がない場合はNone、あとはポップアップの仕様による</returns>
        public static DialogResult PopUp(this ICustomControl ctl)
        {
            if (!string.IsNullOrEmpty(ctl.PopupWindowName))
            {
                // テキストボックスにてスペースキー押下時の処理
                // ポップアップ画面が設定されている場合は、表示を行う

                APP.PopUp.Base.SuperPopupForm displayPopUp = null;

                var p = ctl.GetType().GetProperty("DisplayPopUp");
                if (p != null) displayPopUp = (APP.PopUp.Base.SuperPopupForm)p.GetValue(ctl, null);

                var c = ctl as DataGridViewCell;
                if (ctl is DataGridViewCell) //グリッドの場合
                {
                    //日付セルの場合
                    var dateCell = ctl as DgvCustomDataTimeCell;
                    if (dateCell != null)
                    {
                        var ctrlUtil = new ControlUtility();
                        object[] sendParamArray = null;
                        Control control = new Control();
                        control.Text = "DATE_TIME_VALUE=" + dateCell.EditedFormattedValue;

                        if (dateCell.PopupSendParams != null)
                        {
                            int index = dateCell.PopupSendParams.Length;
                            index += 1;
                            //sendParamArray = new Control[dateCell.PopupSendParams.Length];
                            sendParamArray = new Control[index];
                            for (int i = 0; i < dateCell.PopupSendParams.Length; i++)
                            {
                                var sendParam = dateCell.PopupSendParams[i];
                                sendParamArray[i] = ctrlUtil.FindControl(dateCell.DataGridView.FindForm(), sendParam);
                            }
                            sendParamArray[index - 1] = control;
                        }
                        else
                        {
                            sendParamArray = new Control[1];
                            sendParamArray[0] = control;
                        }

                        object[] fields = new object[dateCell.OwningRow.Cells.Count];
                        dateCell.OwningRow.Cells.CopyTo(fields, 0);

                        // ポップアップウィンドウ表示処理
                        var logic = new CustomControlLogic(dateCell, dateCell.DisplayPopUp, dateCell.ReturnControls);

                        object editingControl = null;//エディティングコントロールor対象セル
                        if (dateCell.IsInEditMode)
                        {
                            editingControl = dateCell.DataGridView.EditingControl;
                        }
                        else
                        {
                            editingControl = dateCell;
                        }

                        logic.ShowPopupWindow(dateCell, fields, editingControl, sendParamArray);
                    }
                    else
                    {
                        //テキストセルの場合
                        var textCell = ctl as DgvCustomTextBoxCell;

                        if (textCell == null) return DialogResult.None;//対象外セル
                        var ctrlUtil = new ControlUtility();
                        object[] sendParamArray = null;

                        if (textCell.PopupSendParams != null)
                        {
                            sendParamArray = new Control[textCell.PopupSendParams.Length];
                            for (int i = 0; i < textCell.PopupSendParams.Length; i++)
                            {
                                var sendParam = textCell.PopupSendParams[i];
                                sendParamArray[i] = ctrlUtil.FindControl(textCell.DataGridView.FindForm(), sendParam);
                            }
                        }

                        object[] fields = new object[textCell.OwningRow.Cells.Count];

                        textCell.OwningRow.Cells.CopyTo(fields, 0);

                        // ポップアップウィンドウ表示処理
                        var logic = new CustomControlLogic(textCell, textCell.DisplayPopUp, textCell.ReturnControls);

                        object editingControl = null;//エディティングコントロールor対象セル
                        if (textCell.IsInEditMode)
                        {
                            editingControl = textCell.DataGridView.EditingControl;
                        }
                        else
                        {
                            editingControl = textCell;
                        }

                        logic.ShowPopupWindow(textCell, fields, editingControl, sendParamArray);
                    }
                }
                else if (ctl is MrCell) //Multirowの場合
                {
                    var textCell = ctl as GcCustomTextBoxCell;
                    if (textCell == null) return DialogResult.None;//対象外セル

                    GrapeCity.Win.MultiRow.Row row = textCell.GcMultiRow.Rows[textCell.RowIndex];
                    var ctrlUtil = new ControlUtility();

                    object[] sendParamArray = null;

                    var dateMultiRowCell = ctl as GcCustomDataTime;
                    bool datetimeFlag = false;
                    Control control = new Control();
                    if (dateMultiRowCell != null)
                    {
                        datetimeFlag = true;
                        control.Text = "DATE_TIME_VALUE=" + textCell.EditedFormattedValue;
                    }

                    if (textCell.PopupSendParams != null)
                    {
                        int index = textCell.PopupSendParams.Length;
                        if (datetimeFlag)
                        {
                            index += 1;
                        }
                        sendParamArray = new Control[index];
                        //sendParamArray = new Control[textCell.PopupSendParams.Length];
                        for (int i = 0; i < textCell.PopupSendParams.Length; i++)
                        {
                            var sendParam = textCell.PopupSendParams[i];
                            sendParamArray[i] = ctrlUtil.FindControl(textCell.GcMultiRow.FindForm(), sendParam);
                        }
                        if (datetimeFlag)
                        {
                            sendParamArray[index - 1] = control;
                        }
                    }
                    else
                    {
                        if (datetimeFlag)
                        {
                            sendParamArray = new Control[1];
                            sendParamArray[0] = control;
                        }
                    }

                    object editingControl = null;//エディティングコントロールor対象セル
                    if (textCell.IsInEditMode)
                    {
                        editingControl = textCell.GcMultiRow.EditingControl;
                    }
                    else
                    {
                        editingControl = textCell;
                    }

                    // ポップアップウィンドウ表示処理
                    var logic = new CustomControlLogic(textCell);
                    logic.ShowPopupWindow(textCell, row.Cells.ToArray(), editingControl, sendParamArray);
                }
                else // 通常コントロールの場合
                {
                    var cstmLogic = new CustomControlLogic(ctl, displayPopUp, null);
                    var ctrlUtil = new ControlUtility();
                    ctrlUtil.ControlCollection = ((Control)ctl).Parent.Controls;
                    object[] sendParamArray = null;
                    CustomDateTimePicker dateTimePicker = null;
                    bool check = false;
                    if (ctl is CustomDateTimePicker)
                    {
                        dateTimePicker = (CustomDateTimePicker)ctl;
                        check = true;
                    }

                    string dateTimeControl = "";
                    string dateTimeYearControl = "";

                    if (check)
                    {
                        dateTimeControl = "DATE_TIME_VALUE=" + ctl.GetResultText();
                    }
                    if (null != dateTimePicker && dateTimePicker.DisplayOnlyYear)
                    {
                        dateTimeYearControl = "DISPLAY_ONLY_YEAR";
                    }

                    if (ctl.PopupSendParams != null)
                    {
                        int index = ctl.PopupSendParams.Length;
                        if (check == true && dateTimePicker.DisplayOnlyYear == true)
                        {
                            index += 1;
                        }

                        if (check) { index += 1; }

                        sendParamArray = new object[index];
                        for (int i = 0; i < ctl.PopupSendParams.Length; i++)
                        {
                            var sendParam = ctl.PopupSendParams[i];
                            sendParamArray[i] = ctrlUtil.FindControl(((Control)ctl).FindForm(), sendParam);
                        }

                        if (check == true && dateTimePicker.DisplayOnlyYear == true)
                        {
                            sendParamArray[index - 2] = dateTimeControl;
                            sendParamArray[index - 1] = dateTimeYearControl;
                        }
                        else
                        {
                            if (check)
                            {
                                sendParamArray[index - 1] = dateTimeControl;
                            }
                        }
                    }
                    else
                    {
                        if (check == true && dateTimePicker.DisplayOnlyYear == true)
                        {
                            sendParamArray = new object[2];
                            sendParamArray[0] = dateTimeControl;
                            sendParamArray[1] = dateTimeYearControl;
                        }
                        else
                        {
                            if (check)
                            {
                                sendParamArray = new object[1];
                                sendParamArray[0] = dateTimeControl;
                            }
                        }
                    }
                    var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl((Control)ctl));
                    return cstmLogic.ShowPopupWindow(ctl, fields, ctl, sendParamArray);
                }
            }

            return DialogResult.None;
        }

        /// <summary>
        /// EnterおよびLeave以外はこちらを利用
        /// </summary>
        /// <param name="ctl"></param>
        public static void UpdateBackColor(this ICustomAutoChangeBackColor ctl)
        {
            CustomControlExtLogic.UpdateBackColor(ctl, ctl.Focused);
        }

        /// <summary>
        /// EnterおよびLeaveで利用してください
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="focusEntered">Enter時(true)、Leave時(false)</param>
        public static void UpdateBackColor(this ICustomAutoChangeBackColor ctl, bool focusEntered)
        {
            if (ctl.AutoChangeBackColorEnabled)
            {
                Color colorBack = Color.Empty;
                Color colorFore = Color.Empty;

                if (!ctl.Enabled) // グレーは最優先
                {
                    colorBack = Constans.DISABLE_COLOR;
                    colorFore = Constans.DISABLE_COLOR_FORE;
                }
                else if (ctl.ReadOnly && focusEntered) // フォーカス入れない系が優先
                {
                    colorBack = Constans.READONLY_FOCUSED_COLOR;
                    colorFore = Constans.READONLY_FOCUSED_COLOR_FORE;
                }
                else if (ctl.ReadOnly && !focusEntered) // フォーカス入れない系が優先
                {
                    colorBack = Constans.READONLY_COLOR;
                    colorFore = Constans.READONLY_COLOR_FORE;
                }
                else if (!ctl.ReadOnly && focusEntered) // エラーよりフォーカス優先(エラーだと現在セルがわからなくなるので)
                {
                    colorBack = Constans.FOCUSED_COLOR;
                    colorFore = Constans.FOCUSED_COLOR_FORE;
                }
                else if (ctl.IsInputErrorOccured)
                {
                    colorBack = Constans.ERROR_COLOR;
                    colorFore = Constans.ERROR_COLOR_FORE;
                }
                else
                {
                    colorBack = Constans.NOMAL_COLOR;
                    colorFore = Constans.NOMAL_COLOR_FORE;
                }

                var c = ctl as Control;
                if (c != null)
                {
                    c.BackColor = colorBack;
                    c.ForeColor = colorFore;
                    c.Invalidate();
                    return;
                }
                var dgvcell = ctl as DgvCell;
                if (dgvcell != null)
                {
                    // 20150902 ouken add 一覧で選択行の背景色を黄色に塗りつぶし start
                    CustomControlExtLogic.UpdateDgvCellBackColor(ctl as DgvCell, colorBack, colorFore);
                    // 20150902 ouken add 一覧で選択行の背景色を黄色に塗りつぶし end
                    return;
                }
                var mrcell = ctl as MrCell;
                if (mrcell != null)
                {
                    // 20150902 ouken add 一覧で選択行の背景色を黄色に塗りつぶし start
                    CustomControlExtLogic.UpdateMrCellBackColor(ctl as MrCell, colorBack, colorFore);
                    // 20150902 ouken add 一覧で選択行の背景色を黄色に塗りつぶし end
                    //mrcell.Invalidate();
                    return;
                }
            }
        }

        // 20150902 ouken add 一覧で選択行の背景色を黄色に塗りつぶし start
        /// <summary>
        /// グリッドのセル背景色を設定する
        /// </summary>
        /// <param name="c"></param>
        /// <param name="backColor">背景色</param>
        /// <param name="foreColor">前色</param>
        private static void UpdateDgvCellBackColor(this DgvCell dgvcell, Color backColor, Color foreColor)
        {
            // エディットコントロールも変える
            if (dgvcell.IsInEditMode && dgvcell.DataGridView != null && dgvcell.DataGridView.EditingControl != null)
            {
                dgvcell.DataGridView.EditingControl.BackColor = dgvcell.Style.BackColor;
                dgvcell.DataGridView.EditingControl.ForeColor = dgvcell.Style.ForeColor;
            }
            // パネルも
            if (dgvcell.IsInEditMode && dgvcell.DataGridView != null && dgvcell.DataGridView.EditingPanel != null)
            {
                dgvcell.DataGridView.EditingPanel.BackColor = dgvcell.Style.BackColor;
                dgvcell.DataGridView.EditingPanel.ForeColor = dgvcell.Style.ForeColor;
            }

            dgvcell.Style.BackColor = backColor;
            dgvcell.Style.ForeColor = foreColor;
            dgvcell.Style.SelectionBackColor = backColor;
            dgvcell.Style.SelectionForeColor = foreColor;

            return;
        }

        /// <summary>
        /// グリッドのセル背景色を設定する
        /// </summary>
        /// <param name="c"></param>
        /// <param name="backColor">背景色</param>
        /// <param name="foreColor">前色</param>
        private static void UpdateMrCellBackColor(this MrCell mrcell, Color backColor, Color foreColor)
        {
            // エディットコントロールも変える
            if (mrcell.IsInEditMode && mrcell.GcMultiRow != null && mrcell.GcMultiRow.EditingControl != null)
            {
                mrcell.GcMultiRow.EditingControl.BackColor = mrcell.Style.BackColor;
                mrcell.GcMultiRow.EditingControl.ForeColor = mrcell.Style.ForeColor;
            }
            // パネルも
            if (mrcell.IsInEditMode && mrcell.GcMultiRow != null && mrcell.GcMultiRow.EditingPanel != null)
            {
                mrcell.GcMultiRow.EditingPanel.BackColor = mrcell.Style.BackColor;
                mrcell.GcMultiRow.EditingPanel.ForeColor = mrcell.Style.ForeColor;
            }

            mrcell.Style.BackColor = backColor;
            mrcell.Style.ForeColor = foreColor;
            mrcell.Style.SelectionBackColor = backColor;
            mrcell.Style.SelectionForeColor = foreColor;

            return;
        }

        /// <summary>
        /// 指定列以外のセルに対して、該当行のセルの背景色を更新する。
        /// </summary>
        /// <param name="r">グリッドの行(DGV)</param>
        /// <param name="focused">フォーカスを示すのフラグ</param>
        /// <param name="skipColumnIndex">指定スキップの列(デフォルト値：-1、全行更新とする)</param>
        public static void UpdateRowBackColor(this DataGridViewRow r, bool focused, int skipColumnIndex = -1)
        {
            foreach (DgvCell cell in r.Cells)
            {
                if (cell.ColumnIndex != skipColumnIndex)
                {
                    UpdateBackColor(cell, focused);
                }
            }
        }

        /// <summary>
        /// 指定列以外のセルに対して、該当行のセルの背景色を更新する。
        /// </summary>
        /// <param name="r">グリッドの行(MultiRow)</param>
        /// <param name="focused">フォーカスを示すのフラグ</param>
        /// <param name="skipCellIndex">指定スキップの列(デフォルト値：-1、全行更新とする)</param>
        public static void UpdateRowBackColor(this GrapeCity.Win.MultiRow.Row r, bool focused, int skipCellIndex = -1)
        {
            foreach (MrCell cell in r.Cells)
            {
                if (cell.CellIndex != skipCellIndex)
                {
                    UpdateBackColor(cell, focused);
                }
            }
        }

        // 20150902 ouken add 一覧で選択行の背景色を黄色に塗りつぶし end

        /// <summary>
        /// 非カスタムコントロールのCellEnterおよびCellLeaveで利用してください
        /// ※RowsAddでカスタム問わず利用も可能
        /// </summary>
        /// <param name="dgvcell"></param>
        /// <param name="focusEntered">Enter時(true)、Leave時(false)</param>
        public static void UpdateBackColor(this DgvCell dgvcell, bool focusEntered)
        {
            if (dgvcell is ICustomAutoChangeBackColor)
            {
                UpdateBackColor(dgvcell as ICustomAutoChangeBackColor, focusEntered);
                return;
            }

            Color colorBack = Color.Empty;
            Color colorFore = Color.Empty;

            // Enableやエラーは無し
            if (dgvcell.ReadOnly && focusEntered) // フォーカス入れない系が優先
            {
                colorBack = Constans.READONLY_FOCUSED_COLOR;
                colorFore = Constans.READONLY_FOCUSED_COLOR_FORE;
            }
            else if (dgvcell.ReadOnly && !focusEntered) // フォーカス入れない系が優先
            {
                colorBack = Constans.READONLY_COLOR;
                colorFore = Constans.READONLY_COLOR_FORE;
            }
            else if (!dgvcell.ReadOnly && focusEntered) // エラーよりフォーカス優先(エラーだと現在セルがわからなくなるので)
            {
                colorBack = Constans.FOCUSED_COLOR;
                colorFore = Constans.FOCUSED_COLOR_FORE;
            }
            else
            {
                colorBack = Constans.NOMAL_COLOR;
                colorFore = Constans.NOMAL_COLOR_FORE;
            }

            // 20150902 ouken update 一覧で選択行の背景色を黄色に塗りつぶし start
            CustomControlExtLogic.UpdateDgvCellBackColor(dgvcell as DgvCell, colorBack, colorFore);
            // 20150902 ouken update 一覧で選択行の背景色を黄色に塗りつぶし end
        }

        /// <summary>
        /// 非カスタムコントロールのCellEnterおよびCellLeaveで利用してください
        /// ※RowsAddでカスタム問わず利用も可能
        /// </summary>
        /// <param name="dgvcell"></param>
        /// <param name="focusEntered">Enter時(true)、Leave時(false)</param>
        public static void UpdateBackColor(this MrCell mrcell, bool focusEntered)
        {
            if (mrcell is ICustomAutoChangeBackColor)
            {
                UpdateBackColor(mrcell as ICustomAutoChangeBackColor, focusEntered);
                return;
            }

            Color colorBack = Color.Empty;
            Color colorFore = Color.Empty;

            if (!mrcell.Enabled) // Enabled最優先
            {
                colorBack = Constans.READONLY_COLOR;
                colorFore = Constans.READONLY_COLOR_FORE;
            }
            else if (mrcell.ReadOnly && focusEntered) // フォーカス入れない系が優先
            {
                colorBack = Constans.READONLY_FOCUSED_COLOR;
                colorFore = Constans.READONLY_FOCUSED_COLOR_FORE;
            }
            else if (mrcell.ReadOnly && !focusEntered) // フォーカス入れない系が優先
            {
                colorBack = Constans.READONLY_COLOR;
                colorFore = Constans.READONLY_COLOR_FORE;
            }
            else if (!mrcell.ReadOnly && focusEntered) // エラーよりフォーカス優先(エラーだと現在セルがわからなくなるので)
            {
                colorBack = Constans.FOCUSED_COLOR;
                colorFore = Constans.FOCUSED_COLOR_FORE;
            }
            else
            {
                colorBack = Constans.NOMAL_COLOR;
                colorFore = Constans.NOMAL_COLOR_FORE;
            }

            CustomControlExtLogic.UpdateMrCellBackColor(mrcell as MrCell, colorBack, colorFore);
        }
    }
}