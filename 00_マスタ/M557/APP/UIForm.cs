using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.DenshiShinSeiKeiroName.Logic;

namespace Shougun.Core.Master.DenshiShinSeiKeiroName.APP
{
    /// <summary>
    /// 申請経路名入力画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 申請経路名入力画面ロジック
        /// </summary>
        private LogicCls logic;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_DENSHI_SHINSEI_ROUTE_NAME, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicCls(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
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
                else if (count == 0)
                {
                    var table0 = this.logic.SearchResult;

                    table0.BeginLoadData();
                    for (int i = 0; i < table0.Columns.Count; i++)
                    {
                        table0.Columns[i].ReadOnly = false;
                    }
                    this.Ichiran.CellValidating -= Ichiran_CellValidating;
                    this.Ichiran.DataSource = table0;
                    this.Ichiran.CellValidating += Ichiran_CellValidating;
                    this.logic.ColumnAllowDBNull();

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

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
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
        [Transaction]
        public virtual void Regist(object sender, EventArgs e)
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
                        if (this.logic.Search() == -1)
                        {
                            return;
                        }
                        this.logic.SetIchiran();
                    }
                }

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
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
        [Transaction]
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            if (!base.RegistErrorFlag)
            {
                bool catchErr = true;                
                this.logic.CreateEntity(true, out catchErr);
                if (!catchErr)
                {
                    return;
                }
                this.logic.LogicalDelete();
                if (this.logic.isRegist)
                {
                    this.Search(sender, e);
                }
            }
        }

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            if (!this.logic.Cancel())
            {
                return;
            }
            Search(sender, e);
        }

        /// <summary>
        /// CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSV(object sender, EventArgs e)
        {
            this.logic.CSV();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CancelCondition(object sender, EventArgs e)
        {
            this.logic.CancelCondition();
            Search(sender, e);
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
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
        /// フォーカスアウト時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // PK(申請経路CD)の重複チェック
                if (e.ColumnIndex == this.Ichiran.Columns["DENSHI_SHINSEI_ROUTE_CD"].Index)
                {
                    if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["DENSHI_SHINSEI_ROUTE_CD"].Value) &&
                        !"".Equals(Ichiran.Rows[e.RowIndex].Cells["DENSHI_SHINSEI_ROUTE_CD"].Value))
                    {
                        string strDenshiShiseiRouteCd = (string)Ichiran.Rows[e.RowIndex].Cells["DENSHI_SHINSEI_ROUTE_CD"].Value.ToString();
                        strDenshiShiseiRouteCd = strDenshiShiseiRouteCd.PadLeft(2, '0');
                        bool catchErr = true;
                        bool isError = this.logic.DuplicationCheck(strDenshiShiseiRouteCd, out catchErr);
                        if (!catchErr)
                        {
                            return;
                        }
                        if (isError)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E003", "申請経路名CD", strDenshiShiseiRouteCd);
                            e.Cancel = true;
                            this.Ichiran.BeginEdit(false);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex], true);
                            return;
                        }
                        Ichiran.Rows[e.RowIndex].Cells["DENSHI_SHINSEI_ROUTE_CD"].Value = strDenshiShiseiRouteCd.ToUpper();
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
        /// 一覧セル選択時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var name = this.Ichiran.Columns[e.ColumnIndex].Name;
                if ("chb_delete".Equals(name)
                    || "DENSHI_SHINSEI_ROUTE_CD".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
                else if ("DENSHI_SHINSEI_ROUTE_NAME".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                }
                else if ("DENSHI_SHINSEI_ROUTE_FURIGANA".Equals(name))
                {
                    Ichiran.ImeMode = System.Windows.Forms.ImeMode.Katakana;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellEnter", ex);
                throw;
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            try
            {
                this.logic.InitCondition();
                this.CONDITION_ITEM.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
        }

        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if ("DELETE_FLG".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "DENSHI_SHINSEI_ROUTE_CD".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "CREATE_DATE".Equals(this.CONDITION_VALUE.DBFieldsName) ||
                "UPDATE_DATE".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Disable;
            }
            else if (this.CONDITION_VALUE.DBFieldsName.Equals("DENSHI_SHINSEI_ROUTE_FURIGANA"))
            {
                this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            }
            else
            {
                this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            }
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

        private void Ichiran_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 例外を無視する
                //e.Cancel = true;
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

        /// <summary> cellのデータ取得 </summary>
        public string getCellValue(DataGridViewRow grdRow, string columnName)
        {
            if (grdRow.Cells[columnName] != null && grdRow.Cells[columnName].Value != null)
            {
                return grdRow.Cells[columnName].Value.ToString().Trim();
            }
            else
            {
                return string.Empty;
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

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // 主キーを非活性にする
            this.logic.EditableToPrimaryKey();
        }

        public void BeforeRegist()
        {
            this.logic.EditableToPrimaryKey();
        }
    }
}
