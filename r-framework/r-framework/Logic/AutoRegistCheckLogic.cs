using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Event;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using System.Data;

namespace r_framework.Logic
{
    /// <summary>
    /// プロパティに設定されているチェック設定を
    /// 保存時に自動的に起動するメソッド
    /// </summary>
    public class AutoRegistCheckLogic
    {
        /// <summary>
        /// エラーメッセージクラス
        /// </summary>
        private class ErrorMessageDto
        {
            /// <summary>
            /// インデックス（コントロールのインデックス）
            /// </summary>
            public int[] Index { get; set; }
            /// <summary>
            /// セルのインデックス（グリッド内）（行は無し、重複メッセージは出さないため）
            /// </summary>
            public int CellIndex { get; set; }

            /// <summary>
            /// コントロール名
            /// </summary>
            public string ControlName { get; set; }

            /// <summary>
            /// メッセージ
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="Index">インデックス</param>
            /// <param name="ControlName">コントロール名</param>
            /// <param name="Message">メッセージ</param>
            public ErrorMessageDto(string ControlName, string Message)
            {
                this.Index = null;
                this.ControlName = ControlName;
                this.Message = Message;
            }
        }

        /// <summary>
        /// コントロール
        /// </summary>
        internal Control[] allCustomControl { get; private set; }

        /// <summary>
        /// MultiRowインスタンス
        /// </summary>
        internal GcMultiRow MultiRow { get; private set; }

        /// <summary>
        /// 送信パラメータ
        /// </summary>
        internal object[] Params { get; private set; }

        /// <summary>
        /// チェック対象コントロール
        /// </summary>
        internal ICustomControl CheckControl { get; set; }

        /// <summary>
        /// エラーメッセージリスト
        /// </summary>
        private List<ErrorMessageDto> _messageList = new List<ErrorMessageDto>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <parameparam name="control">対象のコントロール</parameparam>
        public AutoRegistCheckLogic(Control[] allControl)
        {
            this.allCustomControl = allControl;
            this.Params = allControl;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <parameparam name="control">対象のコントロール</parameparam>
        /// <parameparam name="control">Formに紐付くコントロールのコレクション</parameparam>
        public AutoRegistCheckLogic(Control[] allControl, object[] param)
        {
            this.allCustomControl = allControl;
            this.Params = param;
        }

        /// <summary>
        /// 登録時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="allCustomControl">form上のすべてのコントロール配列</param>
        public virtual bool AutoRegistCheck()
        {
            // エラーメッセージリストクリア
            this._messageList.Clear();

            var returnFlag = false;
            foreach (var c in allCustomControl)
            {
                var errorFlag = false;
                var masterType = c.GetType();
                this.CheckControl = c as ICustomControl;

                // Enabled=falseのコントロールはチェック対象外とする
                object value;
                if (PropertyUtility.GetValue(this.CheckControl, "Enabled", out value))
                {
                    if (!(bool)value)
                    {
                        continue;
                    }
                }
                value = null;

                //タブコントロールに配置されている場合非選択タブだとvisible=falseになってしまう模様

                //所属のタブページをShowすることでvisibleを本来の値にできるが、タブ部分が変更されず中身だけ切り替わるのでNG
                //タブページ上のコントロールの場合 Visibleは見ないようにする
                //タブコントロール上で隠しコントロール作るときはReadOnlyやEnableを併用してください。

                var tabpage = ControlUtility.FindParent<TabPage>(this.CheckControl as Control);
                if (tabpage == null)
                {
                    // Visible=falseのコントロールはチェック対象外とする
                    if (PropertyUtility.GetValue(this.CheckControl, "Visible", out value))
                    {
                        if (!(bool)value)
                        {
                            continue;
                        }
                    }
                }

                value = null;
                // ReadOnlyのコントロールはチェック対象外とする
                if (PropertyUtility.GetValue(this.CheckControl, "ReadOnly", out value))
                {
                    if ((bool)value)
                    {
                        continue;
                    }
                }

                // 対象コントロールがMultiRowの場合にはMultiRow用のチェックを実施する
                SuperForm superForm;
                ControlUtility.TryGetSuperForm(c, out superForm);
                if (this.CheckControl == null)
                {
                    var multiRow = c as GcCustomMultiRow;
                    if (multiRow != null)
                    {
                        var errorFlagForMultiRow = this.AutoCheckMethodForMultiRow(multiRow, superForm);
                    }

                    var dataGridView = c as CustomDataGridView;
                    if (dataGridView != null)
                    {
                        var errorFlagForDataGridView = this.AutoCheckMethodForDataGridView(dataGridView, superForm);
                    }

                    continue;
                }

                // TODO: 例外対応の暫定措置
                if (superForm == null)
                {
                    continue;
                }

                // ユーザーチェック処理をコールする
                RegistCheckEventArgs evt = new RegistCheckEventArgs();
                superForm.OnUserRegistCheck(c, evt);
                if (evt.errorMessages.Count > 0)
                {
                    this._messageList.Add(this.GetErrorMessageDto(string.Join(Environment.NewLine, evt.errorMessages.ToArray())));
                    errorFlag = true;
                }

                // 登録時チェックが設定されていないものはチェック対象外とする
                var mthodList = this.CheckControl.RegistCheckMethod;
                if (mthodList != null && mthodList.Count > 0)
                {
                    // 自動チェック処理を実施する
                    if (this.AutoCheckMethod(mthodList, superForm))
                    {
                        errorFlag = true;
                    }
                }
                if (errorFlag)
                {
                    var textBox = c as ICustomTextBox;
                    if (textBox != null)
                    {
                        textBox.IsInputErrorOccured = true;
                    }
                    else
                    {
                        //リフレクションで試す
                        if (PropertyUtility.SetValue(c, "IsInputErrorOccured", true))
                        {
                            //成功
                        }
                        else
                        {
                            //失敗
                        }
                    }
                }
            }
            if (this._messageList.Count != 0)
            {
                returnFlag = true;
                MessageBox.Show(this.GetErrorMessage(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnFlag;
        }

        /// <summary>
        /// プロパティに設定されている自動チェックメソッドの起動を行う処理
        /// </summary>
        /// <param name="checkMethodList">自動実行用のメソッド一覧</param>
        /// <returns>エラーメッセージ</returns>
        internal virtual bool AutoCheckMethod(Collection<SelectCheckDto> checkMethodList, SuperForm form)
        {
            if (form == null)
            {
                return false;
            }

            //チェック結果格納用変数
            var returnFlag = false;
            if (checkMethodList.Count != 0)
            {
                var check = new CheckMethodSetting();
                foreach (var checkMethodName in checkMethodList)
                {
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

                        if (result.Length == 0)
                        {
                            continue;
                        }
                        else if (!string.IsNullOrEmpty(checkMethodName.DisplayMessage))
                        {
                            result = checkMethodName.DisplayMessage.Replace("\\n", System.Environment.NewLine);
                        }

                        // 重複を除き、インデックス順にメッセージを保存
                        if (!this.ContainsErrorMessage(result))
                        {
                            this._messageList.Add(this.GetErrorMessageDto(result));
                        }
                        returnFlag = true;
                    }
                }
            }

            return returnFlag;
        }

        /// <summary>
        /// MultiRow明細部分の場合に
        /// 保存時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="multiRow">チェックを行うMultiRow</param>
        /// <returns>エラー可否</returns>
        internal virtual bool AutoCheckMethodForMultiRow(GcCustomMultiRow multiRow, SuperForm form)
        {
            multiRow.EndEdit();
            SuperForm superForm;
            ControlUtility.TryGetSuperForm(multiRow, out superForm);
            // this.Paramsを上書きしてしまうと通常のコントロールのチェックに影響がでるので退避しておく。
            var tempParams = this.Params;

            for (int i = 0; i < multiRow.RowCount; i++)
            {
                if (multiRow.Rows[i].IsNewRow)
                {
                    continue;
                }

                this.Params = multiRow.Rows[i].Cells.ToArray();

                for (int j = 0; j < multiRow.Rows[i].Cells.Count; j++)
                {
                    var cell = multiRow.Rows[i].Cells[j];
                    this.CheckControl = cell as ICustomControl;

                    if (this.CheckControl == null)
                    {
                        continue;
                    }

                    // ユーザーチェック処理をコールする
                    RegistCheckEventArgs evt = new RegistCheckEventArgs();
                    evt.multiRow = multiRow;
                    superForm.OnUserRegistCheck(cell, evt);
                    if (evt.errorMessages.Count > 0)
                    {
                        this._messageList.Add(this.GetErrorMessageDto(string.Join(Environment.NewLine, evt.errorMessages.ToArray())));
                    }

                    var mthodList = this.CheckControl.RegistCheckMethod;
                    if (mthodList == null || mthodList.Count == 0)
                    {
                        continue;
                    }

                    var errorFlag = this.AutoCheckMethod(mthodList, form);
                    //if (errorFlag)
                    //{
                    //    multiRow.Rows[i].Cells[j].Style.BackColor = Constans.ERROR_COLOR;
                    //}
                    //else
                    //{
                    //    multiRow.Rows[i].Cells[j].Style.BackColor = Constans.NOMAL_COLOR;
                    //}

                    //正常でもセットする（白く変更も必要）

                    var c = multiRow.Rows[i].Cells[j];
                    var textBox = c as ICustomTextBox;
                    if (textBox != null)
                    {
                        textBox.IsInputErrorOccured = errorFlag;
                    }
                    else
                    {
                        //リフレクションで試す
                        if (PropertyUtility.SetValue(c, "IsInputErrorOccured", errorFlag))
                        {
                            //成功
                        }
                        else
                        {
                            if (errorFlag)
                            {
                                c.Style.BackColor = Constans.ERROR_COLOR;
                            }
                            else
                            {
                                c.Style.BackColor = Constans.NOMAL_COLOR;
                            }

                        }
                    }

                }
            }

            this.Params = tempParams;

            if (this._messageList.Count != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// DataGridView明細部分の場合に
        /// 保存時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="dataGridView">チェックを行うDataGridView</param>
        /// <returns>エラー可否</returns>
        internal virtual bool AutoCheckMethodForDataGridView(CustomDataGridView dataGridView, SuperForm form)
        {
            dataGridView.EndEdit();
            SuperForm superForm;
            ControlUtility.TryGetSuperForm(dataGridView, out superForm);
            // this.Paramsを上書きしてしまうと通常のコントロールのチェックに影響がでるので退避しておく。
            var tempParams = this.Params;

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (dataGridView.Rows[i].IsNewRow)
                {
                    continue;
                }

                object[] objList = new object[dataGridView.Rows[i].Cells.Count];
                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                {
                    objList[j] = dataGridView.Rows[i].Cells[j];
                }
                this.Params = objList;

                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                {
                    var cell = dataGridView.Rows[i].Cells[j];
                    this.CheckControl = cell as ICustomControl;

                    if (this.CheckControl == null)
                    {
                        continue;
                    }

                    // ユーザーチェック処理をコールする
                    RegistCheckEventArgs evt = new RegistCheckEventArgs();
                    evt.dataGridView = dataGridView;
                    superForm.OnUserRegistCheck(cell, evt);
                    if (evt.errorMessages.Count > 0)
                    {
                        this._messageList.Add(this.GetErrorMessageDto(string.Join(Environment.NewLine, evt.errorMessages.ToArray())));
                    }

                    var mthodList = this.CheckControl.RegistCheckMethod;
                    if (mthodList == null || mthodList.Count == 0)
                    {
                        continue;
                    }

                    var errorFlag = this.AutoCheckMethod(mthodList, form);
                    ControlUtility.SetInputErrorOccuredForDgvCell(dataGridView.Rows[i].Cells[j], errorFlag);
                }
            }

            this.Params = tempParams;

            if (this._messageList.Count != 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// エラーメッセージクラス取得処理
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns>エラーメッセージクラス</returns>
        private ErrorMessageDto GetErrorMessageDto(string message)
        {
            ErrorMessageDto result = new ErrorMessageDto(this.CheckControl.GetName(), message);

            if (this.CheckControl != null)
            {

                //hack:インデックスだけではうまくコントロールできない。セルもしかり。行列等要調整 参考： http://d.hatena.ne.jp/zecl/20090226/p1

                List<int> l = new List<int>();

                //グリッドのセルの場合
                var dgvCell = this.CheckControl as DataGridViewCell;
                if (dgvCell != null)
                {
                    GetIndexs(dgvCell.DataGridView, ref l);
                    result.CellIndex = dgvCell.OwningColumn.DisplayIndex;
                }

                //MultiRowの場合
                var mrCell = this.CheckControl as Cell;
                if (mrCell != null)
                {
                    GetIndexs(mrCell.GcMultiRow, ref l);
                    result.CellIndex = mrCell.CellIndex; //タブ順なので MultiRowIndexCreateLogicは使わない（これを使うと表示順を計算できる）
                }

                //通常
                var c = this.CheckControl as Control;
                if (c != null)
                {
                    GetIndexs(c, ref l);
                    result.CellIndex = 0;
                }

                l.Reverse(); // 上位が先になるよう並べ替える
                result.Index = l.ToArray();
            }

            return result;
        }

        /// <summary>
        /// タブインデックスの配列を取得（数値が大きいほうが上位なので、結果をReverseすること）
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        private static void GetIndexs(Control c ,ref List<int> l)
        {
            if (c == null) return;
            l.Add(c.TabIndex);
            GetIndexs(c.Parent,ref l);//親を渡す
        }

        /// <summary>
        /// 重複メッセージ確認
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ContainsErrorMessage(string message)
        {
            foreach (var value in this._messageList)
            {
                if (value.Message == message)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// エラーメッセージ取得処理
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        private string GetErrorMessage()
        {
            var result = new StringBuilder(256);

            if (this._messageList == null)
            {
                return result.ToString();
            }

            //this._messageList.Sort((a, b) => a.Index - b.Index);
            this._messageList.Sort( new Comparison<ErrorMessageDto>(Compare));
            for (int i = 0; i < this._messageList.Count; i++)
            {
                result.AppendLine(this._messageList[i].Message);
            }

            return result.ToString();
        }

        /// <summary>
        /// エラーメッセージ用比較ロジック
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>マイナス:aが小さい、0:等しい、プラス:aが大きい</returns>
        static private int Compare(ErrorMessageDto a, ErrorMessageDto b)
        {
            if (a.Index == null && b.Index == null)
            {
                return 0;
            }
            if (a.Index == null)
            {
                return -1;//あるほうが大きい
            }
            if (b.Index == null)
            {
                return 1;//あるほうが大きい
            }

            //比較開始
            int i = 0;
            while (true)
            {
                if (a.Index.Length <= i && b.Index.Length <= i)
                {
                    return a.CellIndex - b.CellIndex; //親が同じなので CellIndexで比較して終わり（厳密にはzオーダーもあるがそこまでは比較しない）
                }
                if (a.Index.Length <= i)
                {
                    return -1; //aが小さい（bはさらに子なので後）
                }
                if (b.Index.Length <= i)
                {
                    return 1; //aが大きい
                }

                int sa = a.Index[i] - b.Index[i];
                if (sa != 0)
                {
                    return sa;
                }

                //下層を比較
                i++;
            }

        }

        /// <summary>
        /// 登録時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="allCustomControl">form上のすべてのコントロール配列</param>
        public virtual bool BeforeRegistCheck()
        {
            var returnFlag = false;
            foreach (var c in allCustomControl)
            {
                if (c is GcCustomMultiRow)
                {
                    var multiRow = c as GcCustomMultiRow;
                    bool deleteFlg = false;
                    bool createDate = false;

                    if (multiRow.Rows.Count < 0)
                    {
                        return returnFlag;
                    }

                    foreach (Cell col in multiRow.Rows[0].Cells)
                    {
                        if (col.Name == "DELETE_FLG"
                            || col.DataField == "DELETE_FLG")
                        {
                            deleteFlg = true;
                        }
                        else if (col.Name == "CREATE_USER"
                                 || col.DataField == "CREATE_USER")
                        {
                            createDate = true;
                        }
                    }

                    if (!deleteFlg || !createDate || multiRow.ReadOnly)
                    {
                        return returnFlag;
                    }

                    DataTable dt = multiRow.DataSource as DataTable;

                    DataRow row;
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        row = dt.Rows[i];

                        if (Convert.ToString(row["DELETE_FLG"]) == "True"
                            && string.IsNullOrEmpty(Convert.ToString(row["CREATE_USER"])))
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    if (dt != null)
                    {
                        multiRow.DataSource = null;
                        multiRow.DataSource = dt;
                    }

                    var form = multiRow.FindForm();
                    if (form != null)
                    {
                        var t = form.GetType();
                        MethodInfo mi = t.GetMethod("BeforeRegist");
                        if (mi != null)
                        {
                            mi.Invoke(form, null);
                        }
                    }
                }
                else if (c is CustomDataGridView)
                {
                    var dataGridView = c as CustomDataGridView;

                    int deleteIndex = -1;
                    int createIndex = -1;
                    foreach (DataGridViewColumn col in dataGridView.Columns)
                    {
                        if (col.Name == "DELETE_FLG"
                            || col.DataPropertyName == "DELETE_FLG"
                            || (col.CellType.Name.Equals("DgvCustomCheckBoxCell")
                            && ((r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn)col).DBFieldsName == "DELETE_FLG"))
                        {
                            deleteIndex = col.Index;
                        }
                        else if (col.Name == "CREATE_USER"
                                 || col.DataPropertyName == "CREATE_USER")
                        {
                            createIndex = col.Index;
                        }
                    }

                    if (deleteIndex == -1 || createIndex == -1 || dataGridView.ReadOnly)
                    {
                        return returnFlag;
                    }

                    DataTable dt = dataGridView.DataSource as DataTable;

                    DataGridViewRow row;
                    bool notEmpty = false;
                    int cnt = 0;
                    int rowCnt = dataGridView.Rows.Count;
                    for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
                    {
                        row = dataGridView.Rows[i];
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        if (Convert.ToString(row.Cells[deleteIndex].Value) == "True"
                            && string.IsNullOrEmpty(Convert.ToString(row.Cells[createIndex].Value)))
                        {
                            dataGridView.Rows.Remove(row);
                            cnt++;
                            continue;
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            notEmpty = false;
                            DataRow dr = dt.Rows[i];

                            if (c.Parent.Text == "CourseNameHoshuForm" && i == dt.Rows.Count - 1 && string.IsNullOrEmpty(Convert.ToString(dr[createIndex])))
                            {
                                dt.Rows.Remove(dr);
                                continue;
                            }

                            foreach (object obj in dr.ItemArray)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(obj)))
                                {
                                    notEmpty = true;
                                    break;
                                }
                            }
                            if (!notEmpty)
                            {
                                dt.Rows.Remove(dr);
                            }
                        }
                    }

                    if (dt != null)
                    {
                        if (rowCnt - cnt == dataGridView.Rows.Count)
                        {
                            dataGridView.DataSource = dt;
                        }
                        else
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                col.AllowDBNull = true;
                                col.Unique = false;
                            }
                            dataGridView.DataSource = dt.Copy();
                        }
                    }

                    var form = dataGridView.FindForm();
                    if (form != null)
                    {
                        var t = form.GetType();
                        MethodInfo mi = t.GetMethod("BeforeRegist");
                        if (mi != null)
                        {
                            mi.Invoke(form, null);
                        }
                    }
                }
            }
            return returnFlag;
        }

        /// <summary>
        /// 登録時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="allCustomControl">form上のすべてのコントロール配列</param>
        public virtual bool AutoMasterRegistCheck()
        {
            // エラーメッセージリストクリア
            this._messageList.Clear();

            var returnFlag = false;
            foreach (var c in allCustomControl)
            {
                var errorFlag = false;
                var masterType = c.GetType();
                this.CheckControl = c as ICustomControl;

                // Enabled=falseのコントロールはチェック対象外とする
                object value;
                if (PropertyUtility.GetValue(this.CheckControl, "Enabled", out value))
                {
                    if (!(bool)value)
                    {
                        continue;
                    }
                }
                value = null;

                //タブコントロールに配置されている場合非選択タブだとvisible=falseになってしまう模様

                //所属のタブページをShowすることでvisibleを本来の値にできるが、タブ部分が変更されず中身だけ切り替わるのでNG
                //タブページ上のコントロールの場合 Visibleは見ないようにする
                //タブコントロール上で隠しコントロール作るときはReadOnlyやEnableを併用してください。

                var tabpage = ControlUtility.FindParent<TabPage>(this.CheckControl as Control);
                if (tabpage == null)
                {
                    // Visible=falseのコントロールはチェック対象外とする
                    if (PropertyUtility.GetValue(this.CheckControl, "Visible", out value))
                    {
                        if (!(bool)value)
                        {
                            continue;
                        }
                    }
                }

                value = null;
                // ReadOnlyのコントロールはチェック対象外とする
                if (PropertyUtility.GetValue(this.CheckControl, "ReadOnly", out value))
                {
                    if ((bool)value)
                    {
                        continue;
                    }
                }

                // 対象コントロールがMultiRowの場合にはMultiRow用のチェックを実施する
                SuperForm superForm;
                ControlUtility.TryGetSuperForm(c, out superForm);
                if (this.CheckControl == null)
                {
                    var multiRow = c as GcCustomMultiRow;
                    if (multiRow != null)
                    {
                        var errorFlagForMultiRow = this.AutoCheckMasterMethodForMultiRow(multiRow, superForm);
                    }

                    var dataGridView = c as CustomDataGridView;
                    if (dataGridView != null)
                    {
                        var errorFlagForDataGridView = this.AutoCheckMasterMethodForDataGridView(dataGridView, superForm);
                    }

                    continue;
                }

                // TODO: 例外対応の暫定措置
                if (superForm == null)
                {
                    continue;
                }

                // ユーザーチェック処理をコールする
                RegistCheckEventArgs evt = new RegistCheckEventArgs();
                superForm.OnUserRegistCheck(c, evt);
                if (evt.errorMessages.Count > 0)
                {
                    this._messageList.Add(this.GetErrorMessageDto(string.Join(Environment.NewLine, evt.errorMessages.ToArray())));
                    errorFlag = true;
                }

                // 登録時チェックが設定されていないものはチェック対象外とする
                var mthodList = this.CheckControl.RegistCheckMethod;
                if (mthodList != null && mthodList.Count > 0)
                {
                    // 自動チェック処理を実施する
                    if (this.AutoCheckMethod(mthodList, superForm))
                    {
                        errorFlag = true;
                    }
                }
                if (errorFlag)
                {
                    var textBox = c as ICustomTextBox;
                    if (textBox != null)
                    {
                        textBox.IsInputErrorOccured = true;
                    }
                    else
                    {
                        //リフレクションで試す
                        if (PropertyUtility.SetValue(c, "IsInputErrorOccured", true))
                        {
                            //成功
                        }
                        else
                        {
                            //失敗
                        }
                    }
                }
            }
            if (this._messageList.Count != 0)
            {
                returnFlag = true;
                MessageBox.Show(this.GetErrorMessage(), Constans.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnFlag;
        }

        /// <summary>
        /// MultiRow明細部分の場合に
        /// 保存時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="multiRow">チェックを行うMultiRow</param>
        /// <returns>エラー可否</returns>
        internal virtual bool AutoCheckMasterMethodForMultiRow(GcCustomMultiRow multiRow, SuperForm form)
        {
            multiRow.EndEdit();
            SuperForm superForm;
            ControlUtility.TryGetSuperForm(multiRow, out superForm);
            // this.Paramsを上書きしてしまうと通常のコントロールのチェックに影響がでるので退避しておく。
            var tempParams = this.Params;

            //20250409
            var dataTable = (DataTable)multiRow.DataSource;

            for (int i = 0; i < multiRow.RowCount; i++)
            {
                if (multiRow.Rows[i].IsNewRow)
                {
                    continue;
                }

                if (dataTable.Columns.Contains("DELETE_FLG") 
                    && Convert.ToString(((DataTable)multiRow.DataSource).Rows[i]["DELETE_FLG"]) == "True"
                    && string.IsNullOrEmpty(Convert.ToString(((DataTable)multiRow.DataSource).Rows[i]["CREATE_USER"])))
                {
                    continue;
                }

                this.Params = multiRow.Rows[i].Cells.ToArray();

                for (int j = 0; j < multiRow.Rows[i].Cells.Count; j++)
                {
                    var cell = multiRow.Rows[i].Cells[j];
                    this.CheckControl = cell as ICustomControl;

                    if (this.CheckControl == null)
                    {
                        continue;
                    }

                    // ユーザーチェック処理をコールする
                    RegistCheckEventArgs evt = new RegistCheckEventArgs();
                    evt.multiRow = multiRow;
                    superForm.OnUserRegistCheck(cell, evt);
                    if (evt.errorMessages.Count > 0)
                    {
                        this._messageList.Add(this.GetErrorMessageDto(string.Join(Environment.NewLine, evt.errorMessages.ToArray())));
                    }

                    var mthodList = this.CheckControl.RegistCheckMethod;
                    if (mthodList == null || mthodList.Count == 0)
                    {
                        continue;
                    }

                    var errorFlag = this.AutoCheckMethod(mthodList, form);

                    //正常でもセットする（白く変更も必要）

                    var c = multiRow.Rows[i].Cells[j];
                    var textBox = c as ICustomTextBox;
                    if (textBox != null)
                    {
                        textBox.IsInputErrorOccured = errorFlag;
                    }
                    else
                    {
                        //リフレクションで試す
                        if (PropertyUtility.SetValue(c, "IsInputErrorOccured", errorFlag))
                        {
                            //成功
                        }
                        else
                        {
                            if (errorFlag)
                            {
                                c.Style.BackColor = Constans.ERROR_COLOR;
                            }
                            else
                            {
                                c.Style.BackColor = Constans.NOMAL_COLOR;
                            }

                        }
                    }

                }
            }

            this.Params = tempParams;

            if (this._messageList.Count != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// DataGridView明細部分の場合に
        /// 保存時にプロパティに設定されているチェック処理が
        /// 存在するかを判定するメソッド
        /// </summary>
        /// <param name="dataGridView">チェックを行うDataGridView</param>
        /// <returns>エラー可否</returns>
        internal virtual bool AutoCheckMasterMethodForDataGridView(CustomDataGridView dataGridView, SuperForm form)
        {
            dataGridView.EndEdit();
            SuperForm superForm;
            ControlUtility.TryGetSuperForm(dataGridView, out superForm);
            // this.Paramsを上書きしてしまうと通常のコントロールのチェックに影響がでるので退避しておく。
            var tempParams = this.Params;

            int deleteIndex = -1;
            int createIndex = -1;
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                if ((col.Name == "DELETE_FLG"
                    || col.DataPropertyName == "DELETE_FLG"
                    || (col.CellType.Name.Equals("DgvCustomCheckBoxCell")
                    && ((r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn)col).DBFieldsName == "DELETE_FLG"))
                    && col.Visible)
                {
                    deleteIndex = col.Index;
                }
                else if (col.Name == "CREATE_USER"
                         || col.DataPropertyName == "CREATE_USER")
                {
                    createIndex = col.Index;
                }
            }

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (dataGridView.Rows[i].IsNewRow)
                {
                    continue;
                }

                if (deleteIndex != -1 && createIndex != -1 && !dataGridView.ReadOnly)
                {
                    if (Convert.ToString(dataGridView.Rows[i].Cells[deleteIndex].Value) == "True"
                        && string.IsNullOrEmpty(Convert.ToString(dataGridView.Rows[i].Cells[createIndex].Value)))
                    {
                        continue;
                    }
                }

                object[] objList = new object[dataGridView.Rows[i].Cells.Count];
                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                {
                    objList[j] = dataGridView.Rows[i].Cells[j];
                }
                this.Params = objList;

                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                {
                    var cell = dataGridView.Rows[i].Cells[j];
                    this.CheckControl = cell as ICustomControl;

                    if (this.CheckControl == null)
                    {
                        continue;
                    }

                    // ユーザーチェック処理をコールする
                    RegistCheckEventArgs evt = new RegistCheckEventArgs();
                    evt.dataGridView = dataGridView;
                    superForm.OnUserRegistCheck(cell, evt);
                    if (evt.errorMessages.Count > 0)
                    {
                        this._messageList.Add(this.GetErrorMessageDto(string.Join(Environment.NewLine, evt.errorMessages.ToArray())));
                    }

                    var mthodList = this.CheckControl.RegistCheckMethod;
                    if (mthodList == null || mthodList.Count == 0)
                    {
                        continue;
                    }

                    var errorFlag = this.AutoCheckMethod(mthodList, form);
                    ControlUtility.SetInputErrorOccuredForDgvCell(dataGridView.Rows[i].Cells[j], errorFlag);
                }
            }

            this.Params = tempParams;

            if (this._messageList.Count != 0)
            {
                return false;
            }
            return true;
        }
    }
}
