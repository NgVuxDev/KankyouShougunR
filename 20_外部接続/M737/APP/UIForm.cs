using System;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo
{
    /// <summary>
    /// コンテナ種類画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// コンテナ種類画面ロジック
        /// </summary>
        private LogicClass logic;

        public DataTable dtDetailList = new DataTable();

        // 初期サイズ表示フラグ
        private bool InitialFlg = false;

        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        #endregion

        #region コンストラクタ

        public UIForm()
            : base(WINDOW_ID.M_DENSHI_KEIYAKU_SHORUI_INFO, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();
            this.Search(null, e);

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran != null)
            {
                this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
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

        #region ファンクションのイベント

        #region F4 削除

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void LogicalDelete(object sender, EventArgs e)
        {
            // 削除機能は無し
        }

        #endregion

        #region F6 CSV

        /// <summary>
        /// CSV(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.Ichiran.RowCount <= 1)
            {
                // 出力対象データなし
                this.msgLogic.MessageBoxShow("E044");
            }
            else
            {
                // 画面表示内容をCSV出力しますか？
                if (this.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    // 「はい」を選択した場合はCSV出力を行う
                    var csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_KEIYAKU_SHORUI_INFO), this);
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region F8 検索
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                int count = this.logic.Search();
                if (count < 0)
                {
                    return;
                }
                else if (count == 0)
                {
                    this.logic.SearchResult.Rows.Clear();
                    this.Ichiran.DataSource = this.logic.SearchResult;

                    dtDetailList = this.logic.SearchResult.Copy();
                    this.logic.ColumnAllowDBNull();
                    return;
                }

                var table = this.logic.SearchResult;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                dtDetailList = this.logic.SearchResult.Copy();

                this.Ichiran.DataSource = table;

                // 主キーを非活性にする
                this.logic.EditableToPrimaryKey();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region F9 登録
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Regist(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (!base.RegistErrorFlag)
                {
                    if (this.logic.ActionBeforeCheck())
                    {
                        msgLogic.MessageBoxShow("E061");
                        return;
                    }

                    this.logic.CreateEntity(false);

                    bool ret = this.logic.RegistData(base.RegistErrorFlag);

                    if (ret)
                    {
                        msgLogic.MessageBoxShow("I001", "登録");
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
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region F11 取り消し

        /// <summary>
        /// 取り消し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cancel(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.Cancel();
                this.Search(null, e);
                this.CONDITION_ITEM.Focus();
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region F12 閉じる

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (MasterBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        #endregion

        #endregion

        #region フォームのイベント

        #region UIForm

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

        #endregion

        #region CNDITION_VALUE

        /// <summary>
        /// 検索条件IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if ("DELETE_FLG".Equals(this.CONDITION_VALUE.DBFieldsName))
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Disable;
            }
        }

        #endregion

        #region Ichiran

        /// <summary>
        /// IME制御処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // IME制御
                switch (e.ColumnIndex)
                {
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
                        break;
                    case 2:
                        this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
                        break;
                }

                // 新規行の場合には削除チェックさせない
                this.Ichiran.Rows[e.RowIndex].Cells["chb_delete"].ReadOnly = this.Ichiran.Rows[e.RowIndex].IsNewRow;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// 新追加行のセル既定値処理
        /// </summary>
        private void Ichiran_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.Ichiran.Rows[e.Row.Index].IsNewRow)
                {
                    // セルの既定値処理
                    this.Ichiran.Rows[e.Row.Index].Cells["DELETE_FLG"].Value = false;
                    this.Ichiran.Rows[e.Row.Index].Cells["CREATE_PC"].Value = "";
                    this.Ichiran.Rows[e.Row.Index].Cells["UPDATE_PC"].Value = "";
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
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

        #endregion

        #endregion

        #region その他イベント

        /// <summary>
        /// 検索値クリア
        /// </summary>
        private void clearConditionValue()
        {
            this.CONDITION_VALUE.Text = string.Empty;
        }

        //public void BeforeRegist()
        //{
        //    this.logic.EditableToPrimaryKey();
        //}

        #endregion
    }
}
