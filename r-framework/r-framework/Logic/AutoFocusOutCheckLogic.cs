using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Setting;
using r_framework.Utility;

namespace r_framework.Logic
{
    /// <summary>
    /// プロパティに設定されているチェック設定を
    /// フォーカスアウト時に自動的に起動するメソッド
    /// </summary>
    public class AutoFocusOutCheckLogic
    {
        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allCustomControl { get; private set; }

        /// <summary>
        /// MultiRowコントロール
        /// </summary>
        internal GcMultiRow MultiRow { get; private set; }

        /// <summary>
        /// 送信されてきたパラメータ配列
        /// </summary>
        internal object[] Params { get; private set; }

        /// <summary>
        /// チェック対象コントロール
        /// </summary>
        internal ICustomControl CheckControl { get; set; }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        internal string ErrorMessage { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <parameparam name="control">対象のコントロール</parameparam>
        public AutoFocusOutCheckLogic(ICustomControl checkControl)
        {
            this.CheckControl = checkControl;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <parameparam name="control">対象のコントロール</parameparam>
        /// <parameparam name="control">Formに紐付くコントロールのコレクション</parameparam>
        public AutoFocusOutCheckLogic(ICustomControl checkControl, object[] param)
        {
            this.CheckControl = checkControl;
            this.Params = param;
        }

        /// <summary>
        /// フォーカスアウト時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="allCustomControl">form上のすべてのコントロール配列</param>
        public virtual bool AutoFocusOutCheck()
        {
            var mthodList = this.CheckControl.FocusOutCheckMethod;
            if (mthodList == null || mthodList.Count == 0)
            {
                return false;
            }

            SuperForm superForm = null;
            bool errorFlag = false;
            var messageList = new StringBuilder();

            if (this.CheckControl is Control)
            {
                var control = this.CheckControl as Control;
                object value;
                if (PropertyUtility.GetValue(control, "Enabled", out value))
                {
                    if ((bool)value)
                    {
                        ControlUtility.TryGetSuperForm(control, out superForm);
                    }
                }
                if (superForm != null)
                {
                    errorFlag = this.AutoCheckMethod(mthodList, superForm);
                    if (errorFlag)
                    {
                        messageList.Append(ErrorMessage);
                        control.BackColor = Constans.ERROR_COLOR;
                    }
                    else
                    {
                        control.BackColor = Constans.NOMAL_COLOR;
                    }
                }
            }
            else if (this.CheckControl is DataGridViewCell)
            {
                var cell = this.CheckControl as DataGridViewCell;
                ControlUtility.TryGetSuperForm(cell.DataGridView.Parent, out superForm);
                if (superForm != null)
                {
                    errorFlag = this.AutoCheckMethod(mthodList, superForm);
                    if (errorFlag)
                    {
                        messageList.Append(ErrorMessage);
                    }
                    ControlUtility.SetInputErrorOccuredForDgvCell(cell, errorFlag);
                }
            }
            else if (this.CheckControl is GrapeCity.Win.MultiRow.Cell) //これは絶対ダメだろう
            {
                var multiRowCell = this.CheckControl as GrapeCity.Win.MultiRow.Cell;
                var multiRow = multiRowCell.GcMultiRow as GcCustomMultiRow;

                if (multiRow != null)
                {
                    ControlUtility.TryGetSuperForm(multiRowCell.GcMultiRow, out superForm);
                    //multiRow.EndEdit(); validatingが動かないのでよんではだめ
                    errorFlag = this.AutoCheckMethod(mthodList, superForm);
                    if (errorFlag)
                    {
                        messageList.Append(ErrorMessage);
                        multiRowCell.Style.BackColor = Constans.ERROR_COLOR;
                    }
                    else
                    {
                        multiRowCell.Style.BackColor = Constans.NOMAL_COLOR;
                    }
                    if (messageList.Length != 0)
                    {
                        ErrorMessage = messageList.ToString();
                        return true;
                    }
                }
            }
            return errorFlag;
        }

        /// <summary>
        /// プロパティに設定されている自動チェックメソッドの起動を行う処理
        /// </summary>
        /// <param name="checkMethodList">自動実行用のメソッド一覧</param>
        /// <returns>エラーメッセージ</returns>
        public virtual bool AutoCheckMethod(Collection<SelectCheckDto> checkMethodList, SuperForm form)
        {
            if (form == null)
            {
                return false;
            }

            //チェック結果格納用変数
            var checkResult = new StringBuilder();
            var returnFlag = false;
            if (checkMethodList.Count != 0)
            {
                var check = new CheckMethodSetting();
                foreach (var checkMethodName in checkMethodList)
                {
                    var ctrlUtil = new ControlUtility();

                    bool checkFlag = true;
                    if (checkMethodName.RunCheckMethod != null && checkMethodName.RunCheckMethod.Count != 0)
                    {
                        AutoCheckLogic autoCheckLogic = new AutoCheckLogic(checkMethodName.RunCheckMethod);
                        autoCheckLogic.WindowType = form.WindowType;
                        autoCheckLogic.ProcessKbn = form.ProcessKbn;
                        autoCheckLogic.ParamControl = this.Params;
                        autoCheckLogic.CheckControl = this.CheckControl;

                        checkFlag = autoCheckLogic.CheckWhetherStartup();
                    }
                    if (checkFlag)
                    {
                        var methodSetting = check[checkMethodName.CheckMethodName];

                        var assemblyName = methodSetting.AssemblyName;
                        var calassNameSpace = methodSetting.ClassNameSpace;

                        ControlUtility util = new ControlUtility();

                        object[] sendParam = null;
                        if (checkMethodName.SendParams != null)
                        {
                            sendParam = util.FindControl(this.Params, checkMethodName.SendParams);
                        }

                        if (sendParam != null)
                        {
                            for (int i = 0; i < sendParam.Length; i++)
                            {
                                if (sendParam[i] == null)
                                {
                                    sendParam[i] = checkMethodName.SendParams[i];
                                }
                            }
                        }

                        var t = Type.GetType(assemblyName + "." + calassNameSpace);
                        object classInstance = System.Activator.CreateInstance(t, new object[] { this.CheckControl, this.Params, sendParam });

                        string result = (string)t.InvokeMember(methodSetting.MethodName, BindingFlags.InvokeMethod,
                            null, classInstance, new object[] { });

                        // DisplayMessage用エラーメッセージ
                        string[] splitMessage = null;
                        if (result.Length == 0)
                        {
                            if (this.CheckControl is ICustomTextBox)
                            {
                                // ゼロ埋め処理
                                ICustomTextBox textBox = this.CheckControl as ICustomTextBox;

                                var textLogic = new CustomTextBoxLogic(textBox);
                                textLogic.ZeroPadding(this.CheckControl);
                            }

                            continue;
                        }
                        else if (!string.IsNullOrEmpty(checkMethodName.DisplayMessage))
                        {
                            // 改行毎に分割
                            string[] delimiter = { "\\n" };
                            splitMessage = checkMethodName.DisplayMessage.Split(delimiter, StringSplitOptions.None);
                        }

                        if (splitMessage == null)
                        {
                            checkResult.AppendLine(result);
                        }
                        else
                        {
                            // splitMessageにメッセージがある場合、既存メッセージは破棄
                            foreach (string str in splitMessage)
                            {
                                checkResult.AppendLine(str);
                            }
                        }
                        returnFlag = true;
                    }
                }
            }
            ErrorMessage = checkResult.ToString();

            return returnFlag;
        }
    }
}