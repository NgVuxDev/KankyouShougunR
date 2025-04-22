using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Entity;
using r_framework.Logic;

namespace r_framework.Utility
{
    /// <summary>
    /// コントロール操作クラス
    /// </summary>
    //[Implementation]
    public class ControlUtility
    {
        /// <summary>
        /// コントロール
        /// </summary>
        public Control Control { get; set; }

        /// <summary>
        /// コントロールコレクション
        /// </summary>
        public Control.ControlCollection ControlCollection { get; set; }

        /// <summary>
        /// チェック対象コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        /// <summary>
        /// 送信対象パラメータ
        /// </summary>
        public object[] Params { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlUtility()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Control"></param>
        /// <param name="obj"></param>
        public ControlUtility(ICustomControl Control, params object[] obj)
        {
            this.CheckControl = Control;
            this.Params = obj;
        }

        /// <summary>
        /// 検索したデータを設定するコントロールの特定を行うメソッド
        /// </summary>
        /// <param name="controlName">特定するコントロールの名前</param>
        /// <returns>エラーメッセージ</returns>
        public Control GetSettingField(string controlName)
        {
            if (ControlCollection.ContainsKey(controlName))
            {
                var index = ControlCollection.IndexOfKey(controlName);
                return ControlCollection[index];
            }
            for (var i = 0; i < ControlCollection.Count; i++)
            {
                var searchControl = ControlCollection[i];
                var cont = FindControl(searchControl, controlName);
                if (cont == null)
                {
                    continue;
                }
                return cont;
            }
            return null;
        }

        /// <summary>
        /// 名称からコントロールを取得する
        /// </summary>
        /// <param name="controls">コントロールの配列</param>
        /// <param name="controlName">とってくるコントロールの名前</param>
        /// <returns>対象のコントロール</returns>
        public object[] FindControl(object[] controls, string[] controlName)
        {
            object[] returnControls = new object[controlName.Length];

            for (int i = 0; i < controlName.Length; i++)
            {
                foreach (var control in controls)
                {
                    var fieldName = string.Empty;
                    object obj;
                    if (PropertyUtility.GetValue(control, "Name", out obj))
                    {
                        fieldName = obj as string;
                    }
                    if (fieldName == controlName[i])
                    {
                        returnControls[i] = control;
                    }
                }
            }
            return returnControls;
        }

        /// <summary>
        /// 検索したデータを設定するコントロールのリストを作成するメソッド
        /// </summary>
        /// <param name="controlNames">「,」区切りにて複数登録されているコントロール名</param>
        /// <returns>エラーメッセージ</returns>
        public Control[] CreateGetDataList(string controlNames)
        {
            if (string.IsNullOrEmpty(controlNames))
            {
                return null;
            }
            var controlNameList = controlNames.Split(',');
            return controlNameList.Select(GetSettingField).Where(thisControl => thisControl != null).ToArray();
        }

        /// <summary>
        /// コントロールの内容を初期化するメソッド
        /// </summary>
        /// <param name="controlNames">「,」区切りにて複数登録されているコントロール名</param>
        /// <returns>エラーメッセージ</returns>
        public void ResetControlDate(string controlNames)
        {
            foreach (var controlName in controlNames.Split(','))
            {
                if (ControlCollection.ContainsKey(controlName))
                {
                    var index = ControlCollection.IndexOfKey(controlName);
                    ControlCollection[index].Text = string.Empty;
                }
                else
                {
                    for (var i = 0; i < ControlCollection.Count; i++)
                    {
                        var cont = FindControl(ControlCollection[i], controlName);
                        if (cont == null)
                        {
                            continue;
                        }
                        cont.Text = string.Empty;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// entityから指定したプロパティの値をコントロールに設定する
        /// </summary>
        /// <param name="controlNames">「,」区切りにて複数登録されているコントロール名</param>
        /// <param name="getFiled">Entityから取得するプロパティ名</param>
        /// <param name="entity">データが格納されているentity</param>
        /// <returns>エラーメッセージ</returns>
        public bool SetControlForMaster(string controlNames, string[] getFiled, SuperEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            else
            {
                Control[] controls = CreateGetDataList(controlNames);
                for (var i = 0; i < controls.Length; i++)
                {
                    var setDate = Convert.ToString(entity.GetType().InvokeMember(getFiled[i], BindingFlags.GetProperty, null, entity, new object[] { }));
                    controls[i].Text = setDate;
                }
            }
            return true;
        }

        /// <summary>
        /// コントロールの名前を指定して対象のコントロールを検索する
        /// コントロールの中に別途コントロールが入っている可能性を考慮し処理を行う
        /// </summary>
        /// <param name="control"></param>
        /// <param name="stName">検索を行うコントロールの名前</param>
        /// <returns>検索にヒットしたコントロール</returns>
        public Control FindControl(Control control, string stName)
        {
            if (control == null)
            {
                return null;
            }

            foreach (Control cControl in control.Controls)
            {
                if (cControl.HasChildren)
                {
                    var cFindControl = FindControl(cControl, stName);
                    if (cFindControl != null)
                    {
                        return cFindControl;
                    }
                }
                if (cControl.Name == stName)
                {
                    return cControl;
                }
            }

            return null;
        }

        /// <summary>
        /// リストコントロールのindexが変更された場合の処理を行う
        /// 紐付くコントロールのテキストへindexの値を入れる
        /// </summary>
        /// <param name="lstc">リストコントロール</param>
        /// <param name="cont">紐付くコントロール</param>
        public void ChangeIndex(ListControl lstc, Control cont)
        {
            if (cont == null)
            {
                return;
            }

            var text = string.Empty;
            if (0 <= lstc.SelectedIndex)
            {
                text = lstc.ValueMember.Split(',')[lstc.SelectedIndex];
            }
            cont.Text = text;
        }

        /// <summary>
        /// 配置されている全カスタムコントロールを取得
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public Control[] GetAllCustomControls(Control top)
        {
            var buf = new List<Control>();
            foreach (Control c in top.Controls)
            {
                var cont = c as ICustomControl;

                if (cont != null)
                {
                    buf.Add(c);
                    buf.AddRange(GetAllCustomControls(c));

                    continue;
                }

                var customMultiRow = c as GcCustomMultiRow;

                if (customMultiRow != null)
                {
                    buf.Add(customMultiRow);
                    buf.AddRange(GetAllCustomControls(customMultiRow));
                }
            }
            return (Control[])buf.ToArray();
        }

        /// <summary>
        /// 配置されている全コントロールを取得
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public Control[] GetAllControls(Control top)
        {
            var buf = new List<Control>();
            if (top != null)
            {
                foreach (Control c in top.Controls)
                {
                    if (c != null)
                    {
                        buf.Add(c);
                        buf.AddRange(GetAllControls(c));
                    }
                }
            }
            return (Control[])buf.ToArray();
        }

        /// <summary>
        /// [静的]配置されている全コントロールを取得
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        internal static Control[] GetAllControlsEx(Control top)
        {
            var buf = new List<Control>();
            foreach (Control c in top.Controls)
            {
                if (c != null)
                {
                    buf.Add(c);
                    buf.AddRange(ControlUtility.GetAllControlsEx(c));
                }
            }
            return (Control[])buf.ToArray();
        }

        /// <summary>
        /// 配置されている全コントロールを取得
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public Control[] GetSelectedControl(Control top, object controlType)
        {
            var buf = new List<Control>();
            foreach (object c in top.Controls)
            {
                if (c != null)
                {
                    if (c is GcCustomMultiRow)
                    {
                        buf.Add((Control)c);
                        buf.AddRange(GetAllControls((Control)c));
                    }
                }
            }
            return (Control[])buf.ToArray();
        }

        /// <summary>
        /// 設定ファイルを読み込みリボンメニューを作成する
        /// </summary>
        public virtual void CreateRibbon()
        {
        }

        /// <summary>
        /// チェックを行うデータを設定する
        /// </summary>
        /// <param name="entity">Entity</param>
        public void setCheckDate(SuperEntity entity)
        {
            // 取得フィールドがない場合、処理を中止する
            if (string.IsNullOrEmpty(CheckControl.GetCodeMasterField))
            {
                return;
            }
            var getFieldNames = this.CheckControl.GetCodeMasterField.Split(',').Select(s => s.Trim().ToUpper()).ToArray();

            for (int i = 0; i < getFieldNames.Length; i++)
            {
                var control = Params[i] as ICustomControl;
                object obj;
                if (PropertyUtility.GetValue(entity, getFieldNames[i], out obj))
                {
                    if (obj != null && control != null)
                    {
                        control.SetResultText(obj.ToString());

                        // ゼロ埋め処理
                        ICustomTextBox textBox = this.CheckControl as ICustomTextBox;
                        if (textBox.ZeroPaddengFlag)
                        {
                            var textLogic = new CustomTextBoxLogic(textBox);
                            textLogic.ZeroPadding(this.CheckControl);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 設定コントロールが文字列型以外の場合
        /// 空文字ではなくDBNullにして返却
        /// </summary>
        /// <param name="target">設定コントロール</param>
        /// <param name="value">設定値</param>
        /// <returns>セーフティ設定値</returns>
        public static object GetSafetyValue(object target, object value)
        {
            if (!string.Empty.Equals(value))
            {
                return value;
            }

            object objValue;
            if (!PropertyUtility.GetValue(target, "Value", out objValue))
            {
                return value;
            }

            if (objValue is string)
            {
                return value;
            }

            return DBNull.Value;
        }

        /// <summary>
        /// チェック対象のフィールドを初期化する
        /// </summary>
        public void InitCheckDateField()
        {
            if (this.Params == null) return; //未設定対策

            foreach (var param in Params)
            {
                var control = param as ICustomControl;
                if (control != null && CheckControl.GetName() != control.GetName())
                {
                    control.SetResultText(string.Empty);
                }
            }
        }

        /// <summary>
        /// チェック対象のフィールドを初期化する
        /// </summary>
        /// <param name="exclude">除外するDBFieldsNameの値（PKを渡してください）</param>
        public void InitCheckDateField(string[] exclude)
        {
            foreach (var param in Params)
            {
                var control = param as ICustomControl;

                //キー項目を含む場合はクリアしないようにする
                Func<string, bool> isEqual = x => x.Equals(control.DBFieldsName);

                //除外対象または、チェックコントロール自身は空にしない。（チェックするまえにCDを消さない）
                if (control != null && !exclude.Any(isEqual) && control != this.CheckControl)
                {
                    control.SetResultText(string.Empty);
                }
            }
        }

        /// <summary>
        /// TOPコントロールを取得する
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static Control GetTopControl(Control ctrl)
        {
            if (ctrl.Parent == null)
            {
                return ctrl;
            }

            return ControlUtility.GetTopControl(ctrl.Parent);
        }

        /// <summary>
        /// コントロールを検索し、存在する場合はフォーカスを当てる
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="stName"></param>
        public static void FocusParentFormControl(Control parentForm, string stName)
        {
            if (parentForm == null)
            {
                return;
            }
            ControlUtility ctrlUtil = new ControlUtility();
            var control = ctrlUtil.FindControl(parentForm, stName);
            if (control != null && control.Visible)
            {
                control.Focus();
            }
        }

        /// <summary>
        /// コントロールを検索し、存在する場合は押下処理を実行する
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="stName"></param>
        /// <returns>クリックできた場合true、クリックできなかった場合（コントロールなし、validationNG）false</returns>
        public static bool ClickButton(Control ctrl, string stName)
        {
            if (ctrl == null)
            {
                return false;
            }

            bool buttonFlag = false;
            ControlUtility ctrlUtil = new ControlUtility();
            ctrl = GetTopControl(ctrl);
            var button = ctrlUtil.FindControl(ctrl, stName) as Button;
            if (button != null && button.Enabled)
            {
                //現在フォーカス保存
                var act = r_framework.Utility.ControlUtility.GetActiveControl(ctrl.FindForm());

                // #12198 
                // 閉じるボタン押下してカーソルを動かした場合、一部画面（紙マニ、運賃）でフォーカスが抜けられる。
                // 下記ソースをコメントアウトし、最低１回はチェック処理を起動させる。
                //if (button.Name.ToString().Equals("bt_func12"))
                //{
                //    var mr = FindParent<GcCustomMultiRow>(act);
                //    if (mr != null)
                //    {
                //        // MultiRowのF12ボタン対策
                //        mr.CausesValidation = false;
                //    }
                //}

                //課題 #1574 Validationエラー時対応
                button.Focus();//フォーカスセットしてvalidatingを実行
                if (button.Focused) //validatingエラーの場合フォーカスが設定されていない
                {
                    if (act is IDataGridViewEditingControl)
                    {
                        //グリッドの場合は、セルにフォーカス ※エディてぃんぐコントロールにフォーカスすると、フォーカスがどこかへ飛ぶ
                        ((IDataGridViewEditingControl)act).EditingControlDataGridView.Focus();
                    }
                    else
                    {
                        if (button.Name.ToString().Equals("bt_func12"))
                        {
                            buttonFlag = true;
                        }
                        else if (act != null)
                        {
                            act.Focus();//先にフォーカス戻してから実行（Fキー処理のフォーカスセットを妨げないように）
                        }
                    }

                    //validationがOKか、チェック対象外(F12)等であれば実行
                    if (buttonFlag)
                    {
                        if (act != null)
                        {
                            act.CausesValidation = false;
                        }
                    }
                    button.PerformClick(); //PerformClickではフォーカスは移動しない。
                    if (act != null)
                    {
                        act.CausesValidation = true;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// コントロールを検索し、存在する場合は押下処理を実行する
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="stName"></param>
        public static void ShowPopup(Control ctrl, string stName)
        {
            if (ctrl == null)
            {
                return;
            }

            ControlUtility ctrlUtil = new ControlUtility();
            var topForm = ctrl.FindForm();
            ctrlUtil.ControlCollection = topForm.Controls;
            var control = ctrlUtil.GetSettingField(stName) as Control;
            if (control != null && control.Enabled && !control.Visible)
            {
            }
        }

        /// <summary>
        /// 検索名称より対象フィールド配列を作成
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="searchNames"></param>
        /// <returns></returns>
        public static object[] CreateFields(object[] fields, string searchNames)
        {
            if (fields == null || string.IsNullOrEmpty(searchNames))
            {
                return null;
            }

            string[] names = searchNames.Split(',');
            object[] result = new object[names.Length];

            var i = 0;
            foreach (var name in names)
            {
                foreach (var field in fields)
                {
                    var fieldName = string.Empty;
                    object obj;
                    if (PropertyUtility.GetValue(field, "Name", out obj))
                    {
                        fieldName = obj as string;
                        if (fieldName == null)
                        {
                            var c = field as DataGridViewCell;
                            if (c != null)
                            {
                                fieldName = c.OwningColumn.Name; //セルの名前は列から取る
                            }
                        }
                    }

                    if (fieldName == name.Trim())
                    {
                        result[i] = field;
                        break;
                    }
                }
                i++;
            }

            return result;
        }

        /// <summary>
        /// タブインデックスが最初のコントロールを取得
        /// </summary>
        /// <param name="allControls"></param>
        /// <param name="firstTabIndexControl"></param>
        /// <returns></returns>
        public static bool TryGetFirstTabIndexControl(Control[] allControls, out Control firstTabIndexControl)
        {
            firstTabIndexControl = null;
            foreach (var control in allControls)
            {
                if (control.TabStop && control.Enabled)
                {
                    if (firstTabIndexControl == null)
                    {
                        firstTabIndexControl = control;
                    }
                    else if (control.TabIndex < firstTabIndexControl.TabIndex)
                    {
                        firstTabIndexControl = control;
                    }
                }
            }

            return firstTabIndexControl != null;
        }

        /// <summary>
        /// 最終のコントロールのTabIndex取得
        /// </summary>
        /// <param name="allControls"></param>
        /// <param name="lastTabIndexControl"></param>
        /// <returns></returns>
        public static bool TryBaseFormGetLastTabIndexControl(Control[] allControls, int currentTabIndex, out Control lastTabIndexControl)
        {
            lastTabIndexControl = null;
            int lastTabIndex = 0;
            foreach (var control in allControls)
            {
                if (control.Name.Equals("ProcessButtonPanel")
                    || control.Name.Equals("pn_foot"))
                {
                    foreach (Control ctl in control.Controls)
                    {
                        if (ctl.TabStop && ctl.Enabled)
                        {
                            if (lastTabIndexControl == null)
                            {
                                lastTabIndexControl = ctl;
                                lastTabIndex = ctl.TabIndex;
                            }
                            else
                            {
                                if (lastTabIndex <= ctl.TabIndex)
                                {
                                    lastTabIndex = ctl.TabIndex;
                                }
                            }
                        }
                    }
                }
            }

            if (currentTabIndex >= lastTabIndex) { return true; }

            return false;
        }

        /// <summary>
        /// 最終のコントロールのTabIndex取得
        /// </summary>
        /// <param name="allControls"></param>
        /// <param name="lastTabIndexControl"></param>
        /// <returns></returns>
        public static bool TryHeaderFormGetLastTabIndexControl(Control[] allControls, int currentTabIndex, out Control lastTabIndexControl)
        {
            lastTabIndexControl = null;
            int lastTabIndex = 0;
            foreach (var control in allControls)
            {
                if (control.TabStop && control.Enabled)
                {
                    if (lastTabIndexControl == null)
                    {
                        lastTabIndexControl = control;
                        lastTabIndex = control.TabIndex;
                    }
                    else
                    {
                        if (lastTabIndex <= control.TabIndex)
                        {
                            lastTabIndex = control.TabIndex;
                        }
                    }
                }
            }

            if (currentTabIndex >= lastTabIndex) { return true; }

            return false;
        }

        /// <summary>
        /// コントロール取得
        /// </summary>
        /// <param name="target"></param>
        /// <param name="control"></param>
        /// <returns></returns>
        internal static bool TryGetControl(object target, out Control control)
        {
            control = target as Control;
            if (control == null)
            {
                var multiRowCell = target as GrapeCity.Win.MultiRow.Cell;
                var dataGridCell = target as DataGridViewCell;
                if (multiRowCell != null)
                {
                    control = multiRowCell.GcMultiRow;
                }
                else if (dataGridCell != null)
                {
                    control = dataGridCell.DataGridView;
                }
            }

            return control != null;
        }

        /// <summary>
        /// SuperForm取得
        /// </summary>
        /// <param name="target"></param>
        /// <param name="superForm"></param>
        /// <returns></returns>
        internal static bool TryGetSuperForm(Control target, out SuperForm superForm)
        {
            superForm = target.FindForm() as SuperForm;
            if (superForm == null)
            {
                foreach (var control in ControlUtility.GetAllControlsEx(ControlUtility.GetTopControl(target)))
                {
                    if (control is SuperForm)
                    {
                        superForm = control as SuperForm;
                        break;
                    }
                }
            }

            return superForm != null;
        }

        ///// <summary>
        ///// ヒントテキスト作成
        ///// </summary>
        ///// <param name="sender">元コントロール</param>
        ///// <returns>ヒントテキスト</returns>
        //internal static string CreateHintText(object sender)
        //{
        //    var hintText = new StringBuilder(256);

        //    var iCustom = sender as ICustomControl;
        //    if (iCustom != null)
        //    {
        //        // CharactersNumberの初期値がMaxLengthのデフォルト値が設定される点を考慮して
        //        // １万以上は設定対象外とする
        //        bool isValid = (0 < iCustom.CharactersNumber && iCustom.CharactersNumber < 10000);

        //        if (!isValid)
        //        {
        //            // CharactersNumberが有効範囲以外であれば、ヒントテキストは作成しない
        //            return string.Empty;
        //        }

        //        if (!string.IsNullOrEmpty(iCustom.DisplayItemName))
        //        {
        //            hintText.Append(iCustom.DisplayItemName)
        //                    .Append(" は ");
        //        }

        //        hintText.Append(iCustom.CharactersNumber)
        //                .Append(" 文字以内で入力してください。");
        //    }

        //    return hintText.ToString();
        //}

        /// <summary>
        /// タイトルの横幅調整
        /// </summary>
        /// <param name="lblTitle">タイトル</param>
        /// <param name="maxWidth">ラベルの横幅最大値</param>
        public static void AdjustTitleSize(Label lblTitle, int maxWidth)
        {
            if (string.IsNullOrEmpty(lblTitle.Text))
            {
                return;
            }

            using (Bitmap bitmap = new Bitmap(lblTitle.Size.Width, lblTitle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    string title = "　" + lblTitle.Text + "　　";
                    StringFormat sf = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);

                    g.DrawString(title, lblTitle.Font, Brushes.Black, 0, 0, sf);
                    SizeF stringSize = g.MeasureString(title, lblTitle.Font, maxWidth, sf);
                    lblTitle.Size = new System.Drawing.Size((int)stringSize.Width, lblTitle.Size.Height);

                    g.Dispose();
                }
                bitmap.Dispose();
            }
        }

        /// <summary>
        /// 指定されたセルの入力エラーを設定します
        /// </summary>
        /// <param name="cell">呼出元セルコントロール</param>
        /// <param name="isInputErrorOccured">入力エラー有無</param>
        public static void SetInputErrorOccuredForDgvCell(DataGridViewCell cell, bool isInputErrorOccured)
        {
            if ((cell == null))
            {
                return;
            }

            var iText = cell as ICustomTextBox;
            if (iText != null)
            {
                iText.IsInputErrorOccured = isInputErrorOccured;
                return;
            }

            var cellDataTime = cell as DgvCustomDataTimeCell;
            if (cellDataTime != null)
            {
                cellDataTime.IsInputErrorOccured = isInputErrorOccured;
                return;
            }
        }

        /// <summary>
        /// 指定されたセルの入力エラーを設定します
        /// </summary>
        /// <param name="cell">呼出元セルコントロール</param>
        /// <param name="isInputErrorOccured">入力エラー有無</param>
        public static void SetInputErrorOccuredForMultiRow(GrapeCity.Win.MultiRow.Cell cell, bool isInputErrorOccured)
        {
            if ((cell == null))
            {
                return;
            }

            var iText = cell as ICustomTextBox;
            if (iText != null)
            {
                iText.IsInputErrorOccured = isInputErrorOccured;
                return;
            }

            var cellDataTime = cell as GcCustomDataTime;
            if (cellDataTime != null)
            {
                cellDataTime.IsInputErrorOccured = isInputErrorOccured;
                return;
            }
        }

        /// <summary>
        /// DataGridViewのセル背景色を設定
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="color"></param>
        [Obsolete("ロジック変更のため廃止", false)]
        internal static void SetBackColorForDgvCell(DataGridViewCell cell, Color color)
        {
            if (cell == null)
            {
                return;
            }

            cell.Style.BackColor = color;
            cell.Style.SelectionBackColor = color;
            cell.Style.SelectionForeColor = Color.Black;

            if (cell.DataGridView == null)
            {
                return;
            }

            if (cell.DataGridView.EditingControl != null)
            {
                cell.DataGridView.EditingControl.BackColor = color;
            }

            if (cell.DataGridView.EditingPanel != null)
            {
                cell.DataGridView.EditingPanel.BackColor = color;
            }
        }

        /// <summary>
        /// 一番下層にいるアクティブコントロールを取得
        /// </summary>
        /// <param name="control">フォームなどのコンテナコントロール</param>
        /// <returns>一番下層にいるアクティブコントロール</returns>
        public static Control GetActiveControl(IContainerControl control)
        {
            if (control == null) return null;

            if (control.ActiveControl == null) return null;

            if (control.ActiveControl is IContainerControl)
            {
                return GetActiveControl((IContainerControl)control.ActiveControl);
            }
            else
            {
                return control.ActiveControl;
            }
        }

        /// <summary>
        /// 指定した型の親を探す
        /// </summary>
        public static T FindParent<T>(Control c)
            where T : class
        {
            if (c == null || c.Parent == null)
            {
                return null;
            }
            else if (c is T)
            {
                return c as T;
            }
            else
            {
                return FindParent<T>(c.Parent);
            }
        }

        public static string GetUnselectedText(TextBox textBox)
        {
            string text = textBox.Text;
            if (textBox.SelectionLength > 0)
            {
                if (textBox.SelectionLength == textBox.TextLength)
                {
                    text = "";
                }
                else
                {
                    string a = textBox.Text.Substring(0, textBox.SelectionStart);
                    int s = textBox.SelectionStart + textBox.SelectionLength;
                    string b = textBox.Text.Substring(s, textBox.TextLength - s);
                    text = a + b;
                }
            }
            return text;
        }
    }
}