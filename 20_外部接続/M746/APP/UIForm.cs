using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.ExternalConnection.KyoyusakiNyuuryoku.logic;

namespace Shougun.Core.ExternalConnection.KyoyusakiNyuuryoku.APP
{
    /// <summary>
    /// M746 共有先入力（電子）
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        /// <summary>
        /// 共有先CDの最大値
        /// </summary>
        internal int maxKyoyusakiCd = 0;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_KYOYUSAKI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);

            try
            {
                if (!this.logic.WindowInit())
                {
                    return;
                }

                this.Search(null, e);

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
            base.OnShown(e);
        }

        #region function
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Search(object sender, EventArgs e)
        {
            try
            {
                // 値が不正かのチェック
                if (!this.logic.CheckSearchString())
                {
                    // 不正時は中断
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E084", CONDITION_ITEM.Text);
                    CONDITION_VALUE.Focus();
                    return;
                }

                int count = this.logic.Search();
                if (count == -1)
                {
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.Ichiran.CellValidating -= Ichiran_CellValidating;
                this.Ichiran.DataSource = table;
                this.Ichiran.CellValidating += Ichiran_CellValidating;

                if (count == 0)
                {
                    this.logic.ColumnAllowDBNull();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    bool catchErr = true;
                    if (this.Ichiran.Rows.Count == 0)
                    {
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        msgLogic.MessageBoxShow("I001", "登録");

                        this.Search(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void LogicalDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!base.RegistErrorFlag)
            {
                bool catchErr = true;
                bool isOK = this.logic.CreateEntity(true, out catchErr);
                if (!catchErr)
                {
                    // 例外
                    return;
                }

                // 削除するデータが存在するか
                if (isOK)
                {
                    this.logic.LogicalDelete();
                    if (this.logic.isRegist)
                    {
                        this.Search(sender, e);
                    }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E075");
                    return;
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CSV(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.CSV();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ClearCondition(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.logic.InitCondition();
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (!this.logic.Cancel())
            {
                LogUtility.DebugMethodEnd();
                return;
            }
            Search(sender, e);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void FormClose(object sender, EventArgs e)
        {
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
        #endregion

        #region DataGridView_Event

        /// <summary>
        /// ColumnWidthChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                this.Ichiran.Refresh();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// DefaultValuesNeededイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_DefaultValuesNeeded", ex);
                throw;
            }
        }

        /// <summary>
        /// CellEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var name = this.Ichiran.Columns[e.ColumnIndex].Name;
                if ("chb_delete".Equals(name)
                    || "KYOYUSAKI_CD".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
                else if ("KYOYUSAKI_CORP_NAME".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                }
                else if ("KYOYUSAKI_NAME".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                }
                else if ("KYOYUSAKI_MAIL_ADDRESS".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellEnter", ex);
                throw;
            }
        }

        /// <summary>
        /// CellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // メールアドレスの重複チェック
                if (e.ColumnIndex == this.Ichiran.Columns["KYOYUSAKI_MAIL_ADDRESS"].Index)
                {
                    if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value) &&
                        !"".Equals(Ichiran.Rows[e.RowIndex].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value))
                    {
                        string cd = (string)Ichiran.Rows[e.RowIndex].Cells["KYOYUSAKI_CD"].Value.ToString();
                        string address = (string)Ichiran.Rows[e.RowIndex].Cells["KYOYUSAKI_MAIL_ADDRESS"].Value.ToString();
                        bool catchErr = true;
                        bool isError = this.logic.DuplicationCheck(address, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (isError)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E003", "メールアドレス", address);
                            e.Cancel = true;
                            this.Ichiran.BeginEdit(false);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex], true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                throw;
            }
        }

        /// <summary>
        /// CellParsingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// RowsAddedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex <= 0)
                {
                    return;
                }

                if (this.Ichiran.Rows[e.RowIndex].IsNewRow)
                {
                    //CD列を採番
                    this.maxKyoyusakiCd++;
                    this.Ichiran.Rows[e.RowIndex - 1].Cells["KYOYUSAKI_CD"].Value = this.maxKyoyusakiCd;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_RowsAdded", ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if ("DELETE_FLG".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "KYOYUSAKI_CD".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "KYOYUSAKI_MAIL_ADDRESS".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "CREATE_DATE".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "UPDATE_DATE".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Disable;
            }
            else
            {
                this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            }
        }
    }
}
