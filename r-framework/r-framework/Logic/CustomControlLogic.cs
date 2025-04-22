using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Setting;
using r_framework.Utility;
using System.Data;
using r_framework.Dao;

namespace r_framework.Logic
{
    /// <summary>
    /// カスタムコントロールにて共通的に行われる処理を纏めたクラス
    /// </summary>
    internal class CustomControlLogic
    {
        /// <summary>
        /// カスタムコントロール
        /// </summary>
        private ICustomControl _customCtrl;

        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        private APP.PopUp.Base.SuperPopupForm displayPopUp = null;

        /// <summary>
        /// <para>値設定先コントロール。未指定(null)の場合、ICustomControl.PopupSetFormFieldが使用される。</para>
        /// <para>PopupGetMasterFieldと同じ順序で格納。</para>
        /// </summary>
        private CustomControl.ICustomDataGridControl[] returnControls = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="customCtrl">カスタムコントロール</param>
        internal CustomControlLogic(ICustomControl customCtrl)
        {
            this._customCtrl = customCtrl;
        }

        /// <summary>
        /// コンストラクタ。実質CustomDataGridView用。
        /// </summary>
        /// <param name="customCtrl"></param>
        /// <param name="displaypopup"></param>
        /// <param name="returncontrols"></param>
        internal CustomControlLogic(ICustomControl customCtrl, APP.PopUp.Base.SuperPopupForm displaypopup, ICustomDataGridControl[] returncontrols)
        {
            this._customCtrl = customCtrl;
            this.displayPopUp = displaypopup;
            this.returnControls = returncontrols;
        }

        /// <summary>
        /// カスタムコントロールインタフェース取得
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="customCtrl"></param>
        /// <returns></returns>
        internal static bool TryGetCustomCtrl(object obj, out ICustomControl customCtrl)
        {
            customCtrl = obj as ICustomControl;
            return (customCtrl != null);
        }

        /// <summary>
        /// コントロールのフォーカスアウト時に自動起動チェックを起動するメソッド
        /// </summary>
        /// <param name="source">送信元コントロール</param>
        /// <param name="fields">全コントロール</param>
        /// <param name="superForm">継承フォーム</param>
        /// <returns>エラーフラグ（true：エラー false：エラーではない）</returns>
        internal bool StartingFocusOutCheck(object source, object[] fields, SuperForm superForm)
        {
            //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
            bool preFocusOutErrorFlag = false; //エラー値→元と同じ値　と　値変更無し　を判断するため

            // フォーカスアウトエラーフラグ解除
            if (superForm != null)
            {
                preFocusOutErrorFlag = superForm.FocusOutErrorFlag;

                superForm.FocusOutErrorFlag = false;
                superForm.PreviousSaveFlag = true;
            }

            // フォーカスアウトチェックメソッド取得
            var mthodList = this._customCtrl.FocusOutCheckMethod;
            if (mthodList == null || mthodList.Count == 0)
            {
                return false;
            }

            // 前回値と同じ場合は未チェック

            // 前回値と前回コントロール
            ICustomControl prevControl = superForm.GetPreviousControl();
            string prevValue = superForm.GetPreviousValue();
            // 今回値と今回コントロール
            ICustomControl control = this._customCtrl;
            string value = this._customCtrl.GetResultText();

            // 前回コントロールがnullの場合はコントロール比較を行わない
            bool isControlCheck = false;
            if (null != prevControl)
            {
                isControlCheck = true;
            }

            // 前回値と今回値をチェック
            bool isChangeValue = false;
            if (value == prevValue)
            {
                isChangeValue = true;
            }

            // 前回コントロールと今回コントロールをチェック
            bool isChangeControl = false;
            if (control == prevControl)
            {
                isChangeControl = true;
            }
            // コントロール比較を行わない場合は、trueにしておく
            if (false == isControlCheck)
            {
                isChangeControl = true;
            }

            if (!preFocusOutErrorFlag && !string.IsNullOrWhiteSpace(value) && isChangeValue && isChangeControl)
            {
                // 関連箇所の前回値復元
                if (superForm.PreviousControlValue != null)
                {
                    foreach (KeyValuePair<string, string> pair in superForm.PreviousControlValue)
                    {
                        ControlUtility ctrlUtil = new ControlUtility();
                        string[] controlName = new string[] { pair.Key };
                        object[] findControls = ctrlUtil.FindControl(fields, controlName);

                        foreach (object findControl in findControls)
                        {
                            var ctrl = findControl as ICustomControl;
                            if (ctrl == null)
                            {
                                continue;
                            }

                            ctrl.SetResultText(pair.Value);
                        }
                    }
                }

                return false;
            }
            //Start Sontt Process Clear Field
            if (!(!preFocusOutErrorFlag && isChangeValue && isChangeControl))
            {
                if (!String.IsNullOrEmpty(this._customCtrl.ClearFormField))
                {
                    var clearFields = ControlUtility.CreateFields(fields, this._customCtrl.ClearFormField);
                    foreach (var clearField in clearFields)
                    {
                        var controlClearField = clearField as ICustomControl;
                        if (controlClearField != null)
                        {
                            controlClearField.SetResultText(string.Empty);
                        }
                    }
                }
            }
            //End Sontt Process Clear Field
            var autoCheckLogic = new AutoFocusOutCheckLogic(this._customCtrl, fields);
            var errorFlag = autoCheckLogic.AutoFocusOutCheck();
            if (errorFlag)
            {

                // メッセージの表示
                MessageBox.Show(autoCheckLogic.ErrorMessage, Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);


                // フォーカスアウトエラーフラグ設定
                if (superForm != null)
                {
                    superForm.FocusOutErrorFlag = true;
                    superForm.PreviousSaveFlag = false;

                    //FromTOチェックで相互移動を可能にしたことによりエラーになった場合は前回値をクリアして、必ずチェックするようにする。
                    superForm.SetPreviousValue(String.Empty, this._customCtrl);
                }

                //フォーカスイベントを起こすと複数回警告が動くため、validatingのe.Cancel=trueでフォーカス制御する。
                //// エラーとなったセルを選択する
                //var ctrl = source as Control;
                //if (ctrl != null)
                //{
                //    ctrl.Focus(); //ここで前回値セットを行うので、PreviousSaveFlagのセット後に呼ぶ必要あり
                //}
            }

            return errorFlag;
        }

        /// <summary>
        /// カスタムコントロールにてスペースキーを押下されたときに
        /// 設定されているプロパティから自動的にポップアップを特定し
        /// 起動する処理
        /// </summary>
        /// <param name="source">送信元コントロール</param>
        /// <param name="fields">全コントロール</param>
        /// <param name="sender">送信元(編集中コントロールへ設定されない問題解決の為)</param>
        /// <param name="param">送信対象パラメータ</param>
        /// <param name="IsMasterAccessStartUp">MasterAccessから起動されたからどうかのフラグ</param>
        /// <param name="entity">MasterAccess起動時、検索条件用に使用するEntity</param>
        /// <returns></returns>
        internal DialogResult ShowPopupWindow(object source, object[] fields, object sender, object[] param, bool IsMasterAccessStartUp = false, SuperEntity entity = null)
        {
            DialogResult result = DialogResult.None;

            object value;
            if (PropertyUtility.GetValue(source, "Enabled", out value))
            {
                if (!(bool)value)
                {
                    return result;
                }
            }

            //ReadOnlyの場合の対応を追加
            var icontrol = source as ICustomControl;
            if (icontrol != null && icontrol.ReadOnlyPopUp)
            {
                //ReadOnlyだけどポップアップを出す
            }
            else
            {
                //ReadOnlyではポップアップを出さない

                value = null;
                if (PropertyUtility.GetValue(source, "ReadOnly", out value))
                {
                    if ((bool)value)
                    {
                        //マスタ検索項目ポップアップ特別対応（ここはReadOnlyでも起動する例外のため）
                        if (!"マスタ検索項目ポップアップ".Equals(this._customCtrl.PopupWindowName))
                        {
                            return result;
                        }
                    }
                }
            }

            object str;
            if (!PropertyUtility.GetValue(sender, "Text", out str) || str == null)
            {
                str = string.Empty;
            }

            // スペース削除
            str = str.ToString().Replace(" ", "");
            str = str.ToString().Replace("　", "");
            if (string.IsNullOrEmpty(str.ToString()))
            {
                str = string.Empty;
            }




            // 親フォームの情報を取得
            ControlUtility ctrlUtil = new ControlUtility();
            Control form = null;
            List<object> parentControls = new List<object>();
            if (this._customCtrl is ICustomDataGridControl)
            {
                DataGridViewCell dataGridCell = this._customCtrl as DataGridViewCell;
                // ポップアップColse後のメソッド呼び出し用
                form = dataGridCell.DataGridView.FindForm();

                // 親の親フォームのフィールドも検索範囲に含める
                parentControls.AddRange(ctrlUtil.GetAllControls(form));
                parentControls.AddRange(fields);
                fields = parentControls.ToArray();
            }
            else if (this._customCtrl is Cell)
            {
                Cell cell = this._customCtrl as Cell;
                form = cell.GcMultiRow.FindForm();

                parentControls.AddRange(ctrlUtil.GetAllControls(form));
                parentControls.AddRange(fields);
                fields = parentControls.ToArray();
            }
            else
            {
                Control control = this._customCtrl as Control;
                form = control.FindForm();
                // MultiRowとDataGridVewは親の親コントロールを見に行く必要なし
            }


            // ポップアップ表示前、Formのメソッドを呼び出す
            if (this._customCtrl.PopupBeforeExecuteMethod != null)
            {
                if (form != null)
                {
                    var t = form.GetType();
                    MethodInfo mi = t.GetMethod(this._customCtrl.PopupBeforeExecuteMethod.ToString());
                    if (mi != null)
                    {
                        var methodPrams = mi.GetParameters();


                        if (methodPrams == null || methodPrams.Length == 0)
                        {
                            //パラメータなし
                            mi.Invoke(form, null);
                        }
                        else
                        {
                            //パラメータ有の場合 コントロールを送る
                            mi.Invoke(form, new object[] { this._customCtrl });
                        }

                    }
                }
            }

            //ポップアップBeforeデリゲート実行
            if (this._customCtrl.PopupBeforeExecute != null)
            {
                this._customCtrl.PopupBeforeExecute(this._customCtrl);
            }



            // ポップアップ設定されていない場合、処理を中止
            if (string.IsNullOrEmpty(this._customCtrl.PopupWindowName))
            {
                return result;
            }

            SuperPopupForm classinfo;
            if (this.displayPopUp == null)
            {
                // 呼出しDLLを作成
                var popup = new PopupSetting();
                var methodSetting = popup.GetSetting(this._customCtrl.PopupWindowName);
                var assemblyName = methodSetting.AssemblyName;
                var calassNameSpace = methodSetting.ClassNameSpace;
                var assembltyName = assemblyName + ".dll";
                var m = Assembly.LoadFrom(assembltyName);
                var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, assemblyName + "." + calassNameSpace);
                //var classinfo = objectHandler.Unwrap() as SuperPopupForm;
                classinfo = objectHandler.Unwrap() as SuperPopupForm;
            }
            else
            {
                classinfo = this.displayPopUp;
            }
            //classinfo.ReturnControls = this.returnControls;

            if (classinfo != null)
            {
                classinfo.WindowId = this._customCtrl.PopupWindowId;
                classinfo.ParentControls = fields;
                classinfo.IsMasterAccessStartUp = IsMasterAccessStartUp;
                classinfo.Entity = entity;
                if (param != null)
                {
                    //送信対象パラメータ
                    classinfo.Params = param;
                }

                // ポップアップの表示条件を設定
                if (this._customCtrl.popupWindowSetting != null)
                {
                    classinfo.popupWindowSetting = this._customCtrl.popupWindowSetting;
                }

                if (this._customCtrl.PopupSearchSendParams != null)
                {
                    classinfo.PopupSearchSendParams = _customCtrl.PopupSearchSendParams;
                }

                if (this._customCtrl.PopupGetMasterField != null)
                {
                    classinfo.PopupGetMasterField = _customCtrl.PopupGetMasterField;
                }

                classinfo.PopupMultiSelect = _customCtrl.PopupMultiSelect;

                // コントロールにデータソースが設定されている場合、コピーを送る
                object dataSource;
                if (PropertyUtility.GetValue(source, "PopupDataSource", out dataSource))
                {
                    if (dataSource != null && dataSource.GetType() == typeof(DataTable))
                    {
                        DataTable tab = (DataTable)dataSource;
                        classinfo.table = tab.Copy();
                    }

                    object columnsTitle;
                    if (PropertyUtility.GetValue(source, "PopupDataHeaderTitle", out columnsTitle))
                    {
                        if (columnsTitle != null && columnsTitle.GetType() == typeof(string[]))
                        {
                            var columns = (string[])columnsTitle;
                            classinfo.PopupDataHeaderTitle = new string[columns.Length];
                            columns.CopyTo(classinfo.PopupDataHeaderTitle, 0);
                        }
                    }
                }

                //ポップアップのタイトル強制変更対応
                if (icontrol != null)
                {
                    classinfo.PopupTitleLabel = icontrol.PopupTitleLabel;
                }

                GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();
                System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");
                if (dt.Rows.Count > 0)
                {
                    DateTime sysDate = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
                    FieldInfo fieldInfo = classinfo.GetType().GetField("sysDate");
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(classinfo, sysDate);
                    }
                }
                classinfo.StartPosition = FormStartPosition.CenterParent;
                // ポップアップ表示
                result = classinfo.ShowDialog();

                // 送信元のスペースを削除
                if (string.IsNullOrEmpty(str.ToString()))
                {
                    PropertyUtility.SetValue(sender, "Text", string.Empty);
                }

                // ポップアップで選択したデータを格納するためのFiledを作成
                // 既にマスタなどでSetFormFieldを指定して動いている箇所もあるため
                // PopupSetFormFieldが存在した場合にのみPopupSetFormFieldを
                // 使用する
                string targetFileds = string.Empty;

                if (!string.IsNullOrEmpty(this._customCtrl.PopupSetFormField) && !IsMasterAccessStartUp)
                {
                    targetFileds = this._customCtrl.PopupSetFormField;
                }
                else
                {
                    targetFileds = this._customCtrl.SetFormField;
                }

                // ポップアップ結果設定処理
                if (this.returnControls == null)
                {
                    var setFields = ControlUtility.CreateFields(fields, targetFileds);
                    this.SetPopupReturn(classinfo, setFields, source, sender);
                }
                else
                {
                    this.SetPopupReturn(classinfo, this.returnControls, source, sender);
                }
                //Start Sontt Process Clear Field
                if (classinfo.ReturnParams != null)
                {
                    if (!String.IsNullOrEmpty(this._customCtrl.PopupClearFormField))
                    {
                        var clearFields = ControlUtility.CreateFields(fields, this._customCtrl.PopupClearFormField);
                        foreach (var clearField in clearFields)
                        {
                            var controlClearField = clearField as ICustomControl;
                            if (controlClearField != null)
                            {
                                controlClearField.SetResultText(string.Empty);
                            }
                        }
                    }
                }
                //End Sontt Process Clear Field
                // ポップアップClose後、Formのメソッドを呼び出す
                if (this._customCtrl.PopupAfterExecuteMethod != null)
                {
                    if (form != null)
                    {
                        var t = form.GetType();
                        MethodInfo mi = t.GetMethod(this._customCtrl.PopupAfterExecuteMethod.ToString());
                        if (mi != null)
                        {
                            var methodPrams = mi.GetParameters();

                            if (methodPrams == null || methodPrams.Length == 0)
                            {
                                //パラメータなし
                                mi.Invoke(form, null);
                            }
                            else
                            {
                                //パラメータ有の場合 コントロールを送る
                                mi.Invoke(form, new object[] { this._customCtrl });
                            }
                        }
                    }
                }

                //ポップアップAfterデリゲート実行
                if (this._customCtrl.PopupAfterExecute != null)
                {
                    this._customCtrl.PopupAfterExecute(this._customCtrl,result);
                }

                if (classinfo != this.displayPopUp)
                {
                    classinfo.Dispose();
                }
            }

            return result;
        }

        /// <summary>
        /// ポプアップにて選択された情報をコントロールのプロパティに指定されている
        /// コントロールへ設定を行う処理
        /// </summary>
        /// <param name="popupForm"></param>
        /// <param name="setFields">設定を行うコントロール名</param>
        /// <param name="source">送信元コントロール</param>
        /// <param name="sender">送信元(編集中コントロールへ設定されない問題解決の為)</param>
        private void SetPopupReturn(SuperPopupForm popupForm, object[] setFields, object source, object sender)
        {
            int i = 0;

            // 設定を行うコントロール名、または返却値がない場合は処理を中止
            if (setFields == null || popupForm.ReturnParams == null)
            {
                return;
            }

            foreach (var returnParam in popupForm.ReturnParams.Values)
            {
                if (setFields.Length <= i)
                {
                    // 設定フィールド以上になった場合、終了
                    break;
                }
                var setField = setFields[i];
                var setFieldName = PropertyUtility.GetString(setField, "Name");
                var srcFieldName = PropertyUtility.GetString(source, "Name");
                foreach (var popupParam in returnParam)
                {
                    // ポップアップ返却値の編集処理
                    var editParam = this.EditPopupReturnParam(setField, popupParam); //ポップアップごとに列順も固定（車輌の場合は、業者CD,業者名、車輌CD、車輌名）

                    // 複数コントロールにどんどん入れていきたい場合
                    // プロパティへ設定
                    // Text/Valueへの設定を考慮(Control/MultiRow/DataGridView)
                    PropertyUtility.SetTextOrValue(setField, ControlUtility.GetSafetyValue(setField, editParam.Value));

                    // MultiRow/DataGridView対策
                    // 編集中コントロールへ値が設定されない問題を解決
                    // senderにはEdittingControlが入ってくる為、ここに設定を行う。
                    if (!popupForm.IsMasterAccessStartUp && setFieldName == srcFieldName)
                    {
                        // ポップアップ返却値の編集処理
                        editParam = this.EditPopupReturnParam(sender, popupParam);

                        // プロパティへ設定
                        PropertyUtility.SetTextOrValue(sender, ControlUtility.GetSafetyValue(sender, editParam.Value));

                        // 別プロパティへ設定
                        PropertyUtility.SetValue(setField, popupParam.Key, ControlUtility.GetSafetyValue(setField, editParam.Value));
                    }

                    if (!popupForm.IsMasterAccessStartUp)
                    {
                        // 別プロパティへ設定
                        PropertyUtility.SetValue(setField, popupParam.Key, ControlUtility.GetSafetyValue(setField, popupParam.Value));
                    }
                }

                i++;
            }
        }

        /// <summary>
        /// ポップアップから取得したデータをプロパティ設定にしたがって加工する処理
        /// </summary>
        /// <param name="setField">設定先コントロール</param>
        /// <param name="param">ポップアップ返却値</param>
        /// <returns>編集後のポップアップ返却値</returns>
        private Dto.PopupReturnParam EditPopupReturnParam(object setField, Dto.PopupReturnParam param)
        {
            var editParam = param.Clone();

            // ゼロ埋め処理
            ICustomTextBox textBox = setField as ICustomTextBox;
            if (textBox != null && textBox.ZeroPaddengFlag)
            {
                var textLogic = new CustomTextBoxLogic(textBox);
                textLogic.ZeroPadding(editParam);
            }

            return editParam;
        }

        /// <summary>
        /// コントロール枠線を描画
        /// </summary>
        internal static void DrawControlBorder(Control ctrl)
        {
            if (!ctrl.Enabled)
            {
                return;
            }

            var borderColor = Constans.NORMAL_BORDER_COLOR;

            object value;
            if (PropertyUtility.GetValue(ctrl, "ReadOnly", out value))
            {
                if (value is bool && (bool)value)
                {
                    borderColor = Constans.READONLY_BORDER_COLOR;
                }
            }

            using (Graphics g = ctrl.CreateGraphics())
            {
                using (var p = new Pen(borderColor, 2))
                {
                    p.DashStyle = DashStyle.Solid;
                    g.DrawRectangle(p, ctrl.DisplayRectangle);
                }
            }
        }

        /// <summary>
        /// コントロールのSetFormField、PopupSetFormFieldに指定されているコントロールを取得
        /// </summary>
        /// <returns></returns>
        internal object[] GetPreviousControl()
        {
            List<object> list = new List<object>();
            ControlUtility ctrlUtil = new ControlUtility();
            List<object> parentControls = new List<object>();

            Control ctrl = this._customCtrl as Control;
            if (ctrl == null)
            {
                return list.ToArray();
            }

            parentControls.AddRange(ctrlUtil.GetAllControls(ControlUtility.GetTopControl(ctrl)));

            var fields = ControlUtility.CreateFields(parentControls.ToArray(), this._customCtrl.SetFormField);
            var popupFields = ControlUtility.CreateFields(parentControls.ToArray(), this._customCtrl.PopupSetFormField);

            if (fields != null)
            {
                list.AddRange(fields);
            }
            if (popupFields != null)
            {
                list.AddRange(popupFields);
            }

            return list.ToArray();
        }

        /// <summary>
        /// 対象コントロールのSetFormField、PopupSetFormFieldに指定されている対象コントロールを取得
        /// </summary>
        /// <returns></returns>
        internal object[] GetPreviousControlForMultiRow()
        {
            List<object> list = new List<object>();
            ControlUtility ctrlUtil = new ControlUtility();
            List<object> parentControls = new List<object>();

            Cell cell = this._customCtrl as Cell;
            if (cell == null)
            {
                return list.ToArray();
            }

            GcMultiRow multiRow = cell.GcMultiRow;
            Control form = multiRow.FindForm();
            Row row = multiRow.CurrentRow;

            parentControls.AddRange(ctrlUtil.GetAllControls(form));
            parentControls.AddRange(multiRow.CurrentRow.Cells.ToArray());

            var fields = ControlUtility.CreateFields(parentControls.ToArray(), this._customCtrl.SetFormField);
            var popupFields = ControlUtility.CreateFields(parentControls.ToArray(), this._customCtrl.PopupSetFormField);


            if (fields != null)
            {
                list.AddRange(fields);
            }
            if (popupFields != null)
            {
                list.AddRange(popupFields);
            }

            return list.ToArray();
        }
    }
}
