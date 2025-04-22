using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.ExternalConnection.ClientIdNyuuryoku
{
    /// <summary>
    /// クライアントID入力
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// クライアントID入力画面ロジック
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
            : base(WINDOW_ID.M_DENSHI_KEIYAKU_CLIENT_ID, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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
            base.OnShown(e);
        }
        #endregion

        #region ファンクションイベント

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
                    this.logic.CreateEntity(true, out catchErr);
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

        #region CSV出力(F6)
        /// <summary>
        /// CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CSVOutput(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // DGV件数チェック
                if (this.logic.ActionBeforeCheck())
                {
                    this.logic.msgLogic.MessageBoxShow("E044");
                    return;
                }

                // CSV出力
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
                    Boolean isOK = this.logic.CreateEntity(false, out catchErr);
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
                this.Ichiran.CellValidating -= Ichiran_CellValidating;

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

        #endregion

        #region 新規行セル規定値
        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_DATE"].Value = DBNull.Value;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_DATE"].Value = DBNull.Value;
                    Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 重複チェック
        /// <summary>
        /// 社員CDの重複チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // キー重複チェック処理
                if (!DBNull.Value.Equals(Ichiran.Rows[e.RowIndex].Cells["SHAIN_CD"].Value) &&
                    !"".Equals(Ichiran.Rows[e.RowIndex].Cells["SHAIN_CD"].Value) &&
                    Ichiran.Columns[e.ColumnIndex].DataPropertyName.Equals(ConstCls.SHAIN_CD))
                {
                    string shain_Cd = (string)Ichiran.Rows[e.RowIndex].Cells["SHAIN_CD"].Value;
                    shain_Cd = shain_Cd.PadLeft(6, '0');
                    bool catchErr = true;
                    bool isError = this.logic.DuplicationCheck(shain_Cd, out catchErr);
                    if (!catchErr)
                    {
                        return;
                    }
                    if (isError)
                    {
                        this.logic.msgLogic.MessageBoxShow("E022", "入力された社員CD");
                        e.Cancel = true;
                        this.Ichiran.BeginEdit(false);
                        return;
                    }
                    Ichiran.Rows[e.RowIndex].Cells["SHAIN_CD"].Value = shain_Cd.ToUpper();
                }
            }
            catch
            {
                throw;
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
            try
            {
                var cell = this.Ichiran.Rows[e.RowIndex].Cells[e.ColumnIndex];

                string columName = Ichiran.Columns[e.ColumnIndex].Name;
                switch (columName)
                {
                    case "SHAIN_CD": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "SHAIN_NAME": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana; break;
                    case "DENSHI_KEIYAKU_CLIENT_ID": Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable; break;
                    case "CREATE_USER": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "CREATE_DATE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "CREATE_PC": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_USER": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_DATE": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "UPDATE_PC": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "DELETE_FLG": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                    case "TIME_STAMP": Ichiran.ImeMode = System.Windows.Forms.ImeMode.NoControl; break;
                }

                // 新規行の場合には削除チェックさせない
                this.Ichiran.Rows[e.RowIndex].Cells["chb_delete"].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 入力チェック
        /// <summary>
        /// SHAIN_CD入力チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Ichiran.CurrentCell.ColumnIndex == Ichiran.Columns["SHAIN_CD"].Index)
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
                     && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch
            {
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
            if ("".Equals(e.Value)) //空文字を入力された場合
            {
                e.Value = System.DBNull.Value;  //AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true; //後続の解析不要
            }
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
        #endregion
    }
}