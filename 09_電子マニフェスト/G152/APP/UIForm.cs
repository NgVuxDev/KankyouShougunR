using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using r_framework.Dto;
using Shougun.Core.ElectronicManifest.CustomControls_Ex;

namespace Shougun.Core.ElectronicManifest.DenshiCSVTorikomu
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        //private r_framework.Logic.IBuisinessLogic logic;
        private LogicClass MILogic;

        /// <summary>
        /// 一覧のロジック処理
        /// </summary>
        protected IchiranBaseLogic logic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        public UIForm()
            : base(WINDOW_ID.T_DENSHI_MANIFEST_CSV_INPUT, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();

            //社員コード
            //this.ShainCd = SystemProperty.Shain.CD;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (isLoaded == false)
            {

                //初期化、初期表示
                if (!this.MILogic.WindowInit())
                {
                    return;
                }

                //ヘッダーのチェックボックスカラムを追加処理
                if (!this.MILogic.HeaderCheckBoxSupport())
                {
                    return;
                }

                //キー入力設定
                var parentForm = (BusinessBaseForm)this.Parent;

                //画面全体
                parentForm.KeyDown += new KeyEventHandler(UIForm_KeyDown);

                //処理No（ESC）
                //parentForm.txb_process.KeyDown += new KeyEventHandler(TXB_PROCESS_KeyDown);

            }

            // 前回取込みCSVファイルパス設定
            this.ctxt_FilePath.Text = r_framework.Configuration.AppConfig.GetLocalSettings(WINDOW_ID.T_DENSHI_MANIFEST_CSV_INPUT.ToString());

            //表示の初期化
            if (!this.MILogic.ClearScreen("Initial"))
            {
                return;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            }

            isLoaded = true;
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

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }

        #region 画面コントロールイベント

        /// <summary>
        /// フォーム
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape://ESCキー
                    //this.MILogic.SetFocusTxbProcess();
                    break;
            }
        }

        /// <summary>
        /// データ取込(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {
            this.MILogic.CSVImport();
        }

        /// <summary>
        /// 新規(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 修正(F3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// プレビュ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 実行(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            this.MILogic.jikkou();
        }

        /// <summary>
        /// JWNET送信(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 取消(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            //this.MILogic.ClearScreen("ClsSearchCondition");
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = "";

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// パターン一覧(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 検索条件設定(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ESCキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void TXB_PROCESS_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        this.MILogic.SelectButton();
        //    }
        //}

        /// <summary>
        /// Headerセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DatagridViewCheckBoxHeaderCell header = col.HeaderCell as DatagridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                    this.customDataGridView1.Refresh();
                }
            }
        }

        /// <summary>
        /// CellClick Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                var checkBoxHeaderCell = this.customDataGridView1.Columns[e.ColumnIndex].HeaderCell as DatagridViewCheckBoxHeaderCell;
                if (checkBoxHeaderCell == null || checkBoxHeaderCell._checked) { return; }

                string message = string.Empty;

                foreach(DataGridViewRow row in this.customDataGridView1.Rows)
                {
                    bool catchErr = false;
                    bool retChecksaki = this.MILogic.CheckUpnSakiJigyousha(row.Index, out message, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }

                    if (retChecksaki)
                    {
                        // 電子事業者に登録されていない、運搬先事業者が存在する
                        // 補助データ作成画面等で不都合が出てくるためチェック
                        new MessageBoxShowLogic().MessageBoxShow("E232", message);

                        // CellClickイベント後にOnMouseClickが実行されるので、
                        // 最終的にfalseになるよう調整
                        checkBoxHeaderCell._checked = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// CellMouseClickイベント
        /// (CheckBoxクリック、セルクリック時に発生)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string message = string.Empty;

                // チェック付けた時
                if (!(bool)this.customDataGridView1[e.ColumnIndex, e.RowIndex].Value)
                {
                    bool catchErr = false;
                    bool retChecksaki = this.MILogic.CheckUpnSakiJigyousha(e.RowIndex, out message, out catchErr);
                    if (catchErr)
                    {
                        return;
                    }
                    if (retChecksaki)
                    {
                        // 電子事業者に登録されていない、運搬先事業者が存在する
                        // 補助データ作成画面等で不都合が出てくるためチェック
                        new MessageBoxShowLogic().MessageBoxShow("E232", message);
                        this.customDataGridView1[e.ColumnIndex, e.RowIndex].Tag = "G152";
                        this.customDataGridView1.CancelEdit();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// CurrentCellDirtyStateChangedイベント
        /// (CheckBoxクリック、スペースキー押下時に発生)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CustomDataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.customDataGridView1.CurrentCell;
            if (cell.ColumnIndex == 0 && cell.RowIndex >= 0)
            {
                string message = string.Empty;

                // チェック付けた時
                if ((bool)cell.Value)
                {
                    if (this.customDataGridView1.IsCurrentCellDirty)
                    {
                        bool catchErr = false;
                        bool retChecksaki = this.MILogic.CheckUpnSakiJigyousha(cell.RowIndex, out message, out catchErr);
                        if (catchErr)
                        {
                            return;
                        }
                        if (retChecksaki)
                        {
                            // 電子事業者に登録されていない、運搬先事業者が存在する
                            // 補助データ作成画面等で不都合が出てくるためチェック
                            new MessageBoxShowLogic().MessageBoxShow("E232", message);
                            this.customDataGridView1[cell.ColumnIndex, cell.RowIndex].Tag = "G152";
                            this.customDataGridView1.CancelEdit();
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 参照ボタンがクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void cbtn_Sansyou_Click(object sender, EventArgs e)
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "取り込むCSVファイルを選択してください";
            //var initialPath = @"C:\Temp";
            var initialPath = this.ctxt_FilePath.Text; 
            var windowHandle = this.Handle;
            var isFileSelect = true;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            if (!String.IsNullOrEmpty(filePath))
            {
                this.ctxt_FilePath.Text = filePath;
            }
        }
    }
}
