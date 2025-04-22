using System;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.ExternalConnection.ContenaKeikaDate.Const;

namespace Shougun.Core.ExternalConnection.ContenaKeikaDate
{
    /// <summary>
    /// コンテナ設置期間表示設定画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {

        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        bool IsCdFlg = false;

        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //初期サイズ表示フラグ
        private bool InitialFlg = false;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_CONTENA_KEIKA_DATE, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

           
        }

        #endregion

        #region 画面Load処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            LogUtility.DebugMethodStart(e);

            try
            {
                base.OnLoad(e);
                if (!this.logic.WindowInit())
                {
                    return;
                }
                this.Search(null, e);

                // 作成PC/更新PCの列を非表示e;
                this.Ichiran.Columns["UPDATE_PC"].Visible = false;
                this.Ichiran.Columns["TIME_STAMP"].Visible = false;
                this.Ichiran.Columns["CREATE_PC"].Visible = false;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }
            catch
            {
                throw;
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

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            try
            {
                int result = this.logic.Search();

                if (result == -1)
                {
                    return;
                }
                else if (result == 0)
                {
                    //var messageShowLogic = new MessageBoxShowLogic();
                    //messageShowLogic.MessageBoxShow("C001");
                    this.Ichiran.CellValidating -= Ichiran_CellValidating;
                    this.Ichiran.DataSource = this.logic.SearchResult;
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
            catch(Exception ex)
            {
                ex.ToString();
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            try
            {
                if (!base.RegistErrorFlag)
                {
                    bool catchErr = true;
                    // 件数チェック
                    if (this.logic.ActionBeforeCheck())
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    Boolean isOK = this.logic.CreateEntity(false, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (!isOK)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E061");
                        return;
                    }

                    this.logic.Regist(base.RegistErrorFlag);
                    if (this.logic.isRegist)
                    {
                        this.Search(sender, e);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        #endregion

        #region 論理削除処理

        /// <summary>
        /// 論理削除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // 削除データ件数チェック
                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E075");
                    return;
                }



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
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        #endregion

        #region 取消処理

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender,e);

            try
            {
                if (!this.logic.Cancel())
                {
                    return;
                }

                // 2014/01/23 oonaka add 検索条件保存内容不正対応 start
                // 選択された条件カラムにより、サブ情報、IMEを設定
                if (!this.SetConditionControl(this.CONDITION_ITEM.Text))
                {
                    return;
                }
                // 2014/01/23 oonaka add 検索条件保存内容不正対応 end

                Search(null, e);
                this.CONDITION_ITEM.Focus();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        #endregion

        #region CSV出力処理

        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void CSVOutput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (this.logic.ActionBeforeCheck())
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E044");
                    return;
                }
                this.logic.CSVOutput();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        #endregion

        #region 条件クリア処理

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ClearCondition(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (!this.logic.Cancel())
                {
                    return;
                }
                //this.logic.ClearCondition();

                // 2014/01/23 oonaka add 検索条件保存内容不正対応 start
                // 選択された条件カラムにより、サブ情報、IMEを設定
                if (!this.SetConditionControl(this.CONDITION_ITEM.Text))
                {
                    return;
                }
                // 2014/01/23 oonaka add 検索条件保存内容不正対応 end

                this.CONDITION_ITEM.Focus();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        #endregion

        #region Formクローズ処理

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var parentForm = (MasterBaseForm)this.Parent;

                Properties.Settings.Default.ConditionValue_Text = this.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.CONDITION_DBFIELD.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.CONDITION_DBFIELD.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.Save();
                //チェックを外す
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

                this.Close();
                parentForm.Close();

            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        #endregion

        #region 新追加行のセル規定値処理


        #endregion

        #region IME制御処理

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);
            
            try
            {
                string columName = Ichiran.Columns[e.ColumnIndex].Name;

                switch (columName)
                {
                    case "CONTENA_KEIKA_DATE":
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case "CONTENA_KEIKA_BIKOU":
                        Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                }

                // 新規行の場合には削除チェックさせない
                this.Ichiran.Rows[e.RowIndex].Cells["chb_delete"].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;
            }
            catch
            {
                throw;
            }

            //LogUtility.DebugMethodEnd(sender, e);

        }

        #endregion

        #region キー重複チェック処理

        /// <summary>
        /// キー重複チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            try
            {
                
                // キー重複チェック処理
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTENA_KEIKA_DATE].Value) &&
                     !"".Equals(Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTENA_KEIKA_DATE].Value) &&
                            Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(Const.ConstClass.CONTENA_KEIKA_DATE))
                {
                    string Contena_Keika_Date = Ichiran.Rows[e.RowIndex].Cells[ConstClass.CONTENA_KEIKA_DATE].Value.ToString();
                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(Contena_Keika_Date, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }

                    if (isError)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E022", "入力された設置経過日数");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }

                }


            }
            catch
            {
                throw;
            }
            
        }

        #endregion

        #region 設置経過日数入力チェック

        /// <summary>
        /// 設置経過日数入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                
                if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns[ConstClass.CONTENA_KEIKA_DATE].Index)
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
            catch
            {
                throw;
            }
        }

        #endregion

        #region キープレスイベント

        /// <summary>
        /// キープレスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                if (IsCdFlg && !char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
        }

        #endregion

        #region CONDITION_VALUE_KeyPress

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            try
            {
                if ((this.CONDITION_DBFIELD.Text.Equals(ConstClass.CONTENA_KEIKA_DATE))||
                    (this.CONDITION_DBFIELD.Text.Equals(ConstClass.CONTENA_KEIKA_BACK_COLOR)) ||
                    (this.CONDITION_DBFIELD.Text.Equals(ConstClass.CONTENA_KEIKA_BIKOU)))
                {

                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
                        
        }

        #endregion

        #region グリッド⇒DataTableへの変換イベント

        /// <summary>
        /// グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if ("".Equals(e.Value)) //空文字を入力された場合
            {
                e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true; //後続の解析不要
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    DataGridViewCellCollection cells = Ichiran.Rows[e.Row.Index].Cells;

                    string colName;
                    object defaultValue;

                    // セルの既定値処理
                    colName = ConstClass.CONTENA_KEIKA_DATE;
                    defaultValue = DBNull.Value;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }

                    colName = ConstClass.CONTENA_KEIKA_BACK_COLOR;
                    defaultValue = DBNull.Value;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }

                    colName = "UPDATE_DATE";
                    DateTime now = this.logic.getDBDateTime();
                    defaultValue = DBNull.Value;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }

                    colName = "CREATE_DATE";
                    defaultValue = DBNull.Value;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }

                    colName = "chb_delete";
                    defaultValue = false;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }

                    colName = "CREATE_PC";
                    defaultValue = string.Empty;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }

                    colName = "UPDATE_PC";
                    defaultValue = string.Empty;
                    if (Ichiran.Columns[colName] != null)
                    {
                        cells[colName].Value = defaultValue;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 条件ポップアップ後の実行処理
        /// </summary>
        public void PupupAfterExecuteFuncByConditonChange()
        {
            LogUtility.DebugMethodStart();

            // 条件のサブ情報を初期化
            this.CONDITION_DBFIELD.Text = string.Empty;
            this.CONDITION_DBFIELD.DBFieldsName = string.Empty;
            this.CONDITION_TYPE.Text = string.Empty;
            this.CONDITION_VALUE.Text = string.Empty;
            this.CONDITION_VALUE.ImeMode = ImeMode.Disable;

            // 選択された条件カラムにより、サブ情報、IMEを設定
            this.SetConditionControl(CONDITION_ITEM.Text.Trim());
            
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件名から検索条件保持コントロールへ値をセット
        /// </summary>
        /// <param name="conditionItemName"></param>
        internal bool SetConditionControl(string conditionItemName)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(conditionItemName);

                // グリッドのカラムを取得
                var conditionCol = this.Ichiran.Columns.Cast<DataGridViewColumn>()
                        .FirstOrDefault(t => t.HeaderText.Trim().Equals(conditionItemName));

                if (conditionCol != null)
                {
                    // カラムから各情報を取得
                    string dbFieldsName = PropertyUtility.GetString(conditionCol, "DBFieldsName");
                    string itemDefinedTypes = PropertyUtility.GetString(conditionCol, "ItemDefinedTypes");
                    MultiRowCreateLogic logic = new MultiRowCreateLogic();
                    ImeMode ime = logic.GetIME(dbFieldsName, itemDefinedTypes);

                    // 検索条件保持コントロールへ値を保存
                    this.CONDITION_DBFIELD.Text = dbFieldsName;
                    this.CONDITION_DBFIELD.DBFieldsName = dbFieldsName;
                    this.CONDITION_TYPE.Text = itemDefinedTypes;
                    this.CONDITION_VALUE.ImeMode = ime;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetConditionControl", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenchakuJikanHoshuForm_Shown(object sender, EventArgs e)
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
