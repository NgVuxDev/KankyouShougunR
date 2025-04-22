using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku
{
    /// <summary>
    /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力画面ロジック
        /// </summary>
        private LogicCls logic;

        bool IsCdFlg = false;

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #endregion

        #region プロパティ
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_SMS_RECEIVER, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 読み込み
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);

                // 初期化
                if (!this.logic.WindowInit())
                {
                    return;
                }
                // 検索
                this.Search(null, e);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.smsReceiverIchiran != null)
                {
                    this.smsReceiverIchiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!this.InitialFlg)
            {
                this.Height -= 7;
                this.InitialFlg = true;
            }

            this.logic.SMSRenkeiStatus();

            base.OnShown(e);
        }
        #endregion

        #region ファンクションイベント

        #region ﾘｽﾄ登録(F1)
        /// <summary>
        /// ﾘｽﾄ登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ListRegist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                bool catchErr = true;

                // DGV件数チェック
                if (this.logic.ActionBeforeCheck())
                {
                    this.logic.msgLogic.MessageBoxShow("E061");
                    return;
                }

                // 連携チェック
                Boolean isSmsRenkeiCheckOK = this.logic.SmsRenkeiCheck();
                if (!isSmsRenkeiCheckOK)
                {
                    return;
                }

                // 入力チェック
                Boolean isCheckOK = this.logic.CheckBeforeUpdate();
                if (!isCheckOK)
                {
                    return;
                }

                // 重複チェック
                Boolean isDuplicationCheckOK = this.logic.SmsDuplicationCheck();
                if (!isDuplicationCheckOK)
                {
                    return;
                }

                // 携帯番号桁数チェック
                Boolean isPhoneNumberCheckOK = this.logic.SmsPhoneNumberCheck();
                if (!isPhoneNumberCheckOK)
                {
                    return;
                }

                // Entity作成
                Boolean isOK = this.logic.CreateEntity(false, true, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                if (!isOK)
                {
                    this.logic.msgLogic.MessageBoxShow("E061");
                    return;
                }

                // 登録
                this.logic.ListRegist();

                // 検索
                this.Search(sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListRegist", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ﾘｽﾄ削除(F2)
        /// <summary>
        /// ﾘｽﾄ削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ListDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                bool catchErr = true;
                DialogResult result = DialogResult.Yes;

                //確認メッセージ表示
                result = this.logic.msgLogic.MessageBoxShowConfirm("削除を行う場合、\n登録済みのデータを含めて空電プッシュ（システム）に連携されます。\nよろしいでしょうか？");

                if (result != DialogResult.Yes)
                {
                    return;
                }

                // DGV件数チェック
                if (this.logic.ActionBeforeCheck())
                {
                    this.logic.msgLogic.MessageBoxShow("E061");
                    return;
                }

                // 連携チェック
                Boolean isSmsDeleteCheckOK = this.logic.SmsDeleteCheck();
                if (!isSmsDeleteCheckOK)
                {
                    return;
                }

                // 入力チェック
                Boolean isCheckOK = this.logic.CheckBeforeUpdate();
                if (!isCheckOK)
                {
                    return;
                }

                // Entity作成
                this.logic.CreateEntity(true, false, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                // 削除
                this.logic.ListDelete();

                // 検索
                this.Search(sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ListDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 削除(F4)
        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void LogicalDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!base.RegistErrorFlag)
                {
                    bool catchErr = true;

                    // DGV件数チェック
                    if (this.logic.ActionBeforeCheck())
                    {
                        this.logic.msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    // 入力チェック
                    Boolean isCheckOK = this.logic.CheckBeforeUpdate();
                    if (!isCheckOK)
                    {
                        return;
                    }

                    // Entity作成
                    this.logic.CreateEntity(true, false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    // 削除
                    this.logic.LogicalDelete();

                    // 検索
                    this.Search(sender, e);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region クリア(F7)
        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ClearCondition(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 初期化
                if (!this.logic.Cancel())
                {
                    return;
                }
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 検索(F8)
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 検索
                int count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }
                else if (count == 0)
                {
                    this.smsReceiverIchiran.CellValidating -= Ichiran_CellValidating;
                    this.smsReceiverIchiran.DataSource = this.logic.SearchResult;
                    this.smsReceiverIchiran.CellValidating += Ichiran_CellValidating;

                    this.logic.ColumnAllowDBNull();
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.smsReceiverIchiran.CellValidating -= Ichiran_CellValidating;
                this.smsReceiverIchiran.DataSource = table;
                this.smsReceiverIchiran.CellValidating += Ichiran_CellValidating;

                this.logic.SMSRenkeiStatus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 登録(F9)
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!base.RegistErrorFlag)
                {
                    bool catchErr = true;

                    // DGV件数チェック
                    if (this.logic.ActionBeforeCheck())
                    {
                        this.logic.msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    // 入力チェック
                    Boolean isCheckOK = this.logic.CheckBeforeUpdate();
                    if (!isCheckOK)
                    {
                        return;
                    }

                    // Entity作成
                    Boolean isOK = this.logic.CreateEntity(false, false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOK)
                    {
                        this.logic.msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    // 登録
                    this.logic.Regist(base.RegistErrorFlag);

                    // 検索
                    this.Search(sender, e);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 取り消し(F11)
        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 初期化
                if (!this.logic.Cancel())
                {
                    return;
                }

                // 検索
                this.Search(null, e);
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 閉じる(F12)
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                this.smsReceiverIchiran.CellValidating -= Ichiran_CellValidating;

                var parentForm = (MasterBaseForm)this.Parent;

                Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                this.logic.msgLogic.MessageBoxShow("E245");
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region 新規行セル規定値
        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (smsReceiverIchiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["SYSTEM_ID"].Value = DBNull.Value;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["chb_renkei"].Value = false;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["chb_delete"].Value = false;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["RENKEI_FLG"].Value = false;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["MOBILE_PHONE_NUMBER"].Value = DBNull.Value;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["UPDATE_DATE"].Value = DBNull.Value;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["CREATE_DATE"].Value = DBNull.Value;
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    smsReceiverIchiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DefaultValuesNeeded", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 重複チェック
        /// <summary>
        /// 一覧のValidating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var cell = smsReceiverIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if(cell == null || e.FormattedValue == null)
                {
                    e.Cancel = true;
                }

                if (cell.OwningColumn.Name == "MOBILE_PHONE_NUMBER")
                {
                    // 携帯番号重複チェック処理
                    if (!DBNull.Value.Equals(cell.Value) && !"".Equals(cell.Value) &&
                        smsReceiverIchiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.MOBILE_PHONE_NUMBER) &&
                        !cell.ReadOnly)
                    {
                        string phone_Number = (string)smsReceiverIchiran.Rows[e.RowIndex].Cells["MOBILE_PHONE_NUMBER"].Value;
                        int system_Id = 0;
                        if (!DBNull.Value.Equals(smsReceiverIchiran.Rows[e.RowIndex].Cells["SYSTEM_ID"].Value))
                        {
                            system_Id = (int)smsReceiverIchiran.Rows[e.RowIndex].Cells["SYSTEM_ID"].Value;
                        }
                        bool catchErr = true;
                        bool isError = this.logic.DuplicationCheck(phone_Number, system_Id, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (isError)
                        {
                            this.logic.msgLogic.MessageBoxShow("E022", "入力された携帯番号");
                            e.Cancel = true;
                            this.smsReceiverIchiran.BeginEdit(false);
                            return;
                        }
                        // ハイフンを除く
                        if (phone_Number.Contains("-"))
                        {
                            phone_Number = phone_Number.ToString().Replace("-", "");
                        }
                        smsReceiverIchiran.Rows[e.RowIndex].Cells["MOBILE_PHONE_NUMBER"].Value = phone_Number;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region IME制御
        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var cell = this.smsReceiverIchiran.Rows[e.RowIndex].Cells[e.ColumnIndex];

                string columName = smsReceiverIchiran.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "SYSTEM_ID": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "RENKEI_FLG": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "DELETE_FLG": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "RENKEI_STATUS": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "MOBILE_PHONE_NUMBER": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "RECEIVER_NAME": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "CREATE_USER": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "CREATE_DATE": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "CREATE_PC": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_USER": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_DATE": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_PC": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "TIME_STAMP": smsReceiverIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellEnter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 入力チェック
        /// <summary>
        /// MOBILE_PHONE_NUMBER入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (smsReceiverIchiran.CurrentCell.ColumnIndex == smsReceiverIchiran.Columns["MOBILE_PHONE_NUMBER"].Index)
            {
                IsCdFlg = true;
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(itemID_KeyPress);
                }
            }
            else
            {
                IsCdFlg = false;
            }
        }

        /// <summary>
        /// itemID_KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (IsCdFlg && !char.IsControl(e.KeyChar)
                     && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '-')
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("itemID_KeyPress", ex);
                throw;
            }
        }
        #endregion

        #region その他
        /// <summary>
        /// CONDITION_VALUE_Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            this.CONDITION_VALUE.ImeMode = this.CONDITION_VALUE.ImeMode;
        }

        /// <summary>
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if ("".Equals(e.Value)) //空文字を入力された場合
                {
                    e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                    e.ParsingApplied = true; //後続の解析不要
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellParsing", ex);
                throw;
            }
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            //// 主キーを非活性にする
            //this.logic.EditableToPrimaryKey();
        }
        #endregion

        private void Ichiran_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 例外を無視する
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DataError", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
    }
}