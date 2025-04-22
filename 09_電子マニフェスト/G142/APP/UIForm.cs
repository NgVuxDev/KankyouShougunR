using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;


namespace Shougun.Core.ElectronicManifest.SousinhoryuuTouroku
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass MILogic;


        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #region 出力パラメータ

        /// <summary>
        /// システムID
        /// </summary>
        public String ParamOut_SysID { get; set; }

        /// <summary>
        /// モード
        /// </summary>
        public Int32 ParamOut_WinType { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.SOUSHIN_HORYUU_TOUROKU, false)
        {
            this.InitializeComponent();

            //社員コード
            this.ShainCd = SystemProperty.Shain.CD;

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

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
                this.bt_ptn1.Top += 7;
                this.bt_ptn2.Top += 7;
                this.bt_ptn3.Top += 7;
                this.bt_ptn4.Top += 7;
                this.bt_ptn5.Top += 7;
            }

            if (isLoaded == false)
            {
                //初期化、初期表示
                if (!this.MILogic.WindowInit())
                {
                    return;
                }

                //キー入力設定
                var parentForm = (BusinessBaseForm)this.Parent;

                //画面全体
                parentForm.KeyDown += new KeyEventHandler(UIForm_KeyDown);

                //処理No（ESC）
                parentForm.txb_process.KeyDown += new KeyEventHandler(TXB_PROCESS_KeyDown);

                //グッリドビュータブ順設定
                this.customDataGridView1.TabIndex = 32;

                // グリッドビューLocation設定
                this.customDataGridView1.Location = new Point(3, 99);

                // グリッドビューサイズ設定
                this.customDataGridView1.Size = new Size(997, 357);

                //明細行ダブルクリック処理イベント追加
                this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellDoubleClick);

                // 非表示列登録
                this.SetHiddenColumns(this.MILogic.HIDDEN_KANRI_ID, this.MILogic.HIDDEN_SEQ, this.MILogic.HIDDEN_QUE_SEQ,
                    this.MILogic.HIDDEN_QI_UPDATE_TS, this.MILogic.HIDDEN_DMT_UPDATE_TS,this.MILogic.HST_GYOUSHA_CD_ERROR,
                    this.MILogic.HST_GENBA_CD_ERROR,this.MILogic.HAIKI_SHURUI_CD_ERROR);

                //表示の初期化
                if (!this.MILogic.ClearScreen("Initial"))
                {
                    return;
                }
            }

            this.PatternReload(!this.isLoaded);

            //検索
            //this.MILogic.meisaihyoujiFlg = false;//2013.12.25 touti upd画面起動時に検索しない 処理方法修正
            this.MILogic.selectQuery = this.logic.SelectQeury;
            this.MILogic.orderByQuery = this.logic.OrderByQuery;
            this.MILogic.joinQuery = this.logic.JoinQuery;

            //2013.12.25 touti 追加 パターン更新 start
            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
            //this.MILogic.Search();
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                this.customDataGridView1.Columns.Clear();
                if (this.Table != null)
                {
                    if (!this.MILogic.HeaderCheckBoxSupport())
                    {
                        return;
                    }
                    this.customDataGridView1.AllowUserToAddRows = false;
                    this.customDataGridView1.MultiSelect = false;
                    this.logic.CreateDataGridView(this.Table);
                    DataGridViewColumn newColumn = new DataGridViewColumn();
                    newColumn.Name = "区分";
                    this.customDataGridView1.Columns.Insert(1, newColumn);
                }
            }
            //2013.12.25 touti 追加 パターン更新 end

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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

            base.OnShown(e);
        }

        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData()
        {
            this.logic.CreateDataGridView(this.MILogic.SearchResult);
        }

        #region 画面コントロールイベント

        /// <summary>
        /// フォーム
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Escape://ESCキー
            //        this.MILogic.SetFocusTxbProcess();
            //        break;
            //}
        }

        /// <summary>
        /// パターン1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 受渡確認(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func1_Click(object sender, EventArgs e)
        {

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
            this.MILogic.func3_GamenSennyi();
        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　start
            bool catchErr = false;
            var retDate = this.MILogic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retDate)
            {
                return;
            }
            /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　end
            this.MILogic.Delete();
        }

        /// <summary>
        /// CSV出力(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            // 検索結果をCSV出力
            CSVExport export = new CSVExport();
            export.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, "送信保留登録情報", this);
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　start
            bool catchErr = false;
            var retDate = this.MILogic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retDate)
            {
                return;
            }
            /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　end
            
            // パターンチェック
            if (this.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            if (this.MILogic.Search() == -1) { return; }

            if (this.customDataGridView1.RowCount == 0)
            {
                MessageBoxUtility.MessageBoxShow("C001");
            }
        }

        /// <summary>
        /// JWNET送信(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　start
            bool catchErr = false;
            var retDate = this.MILogic.DateCheck(out catchErr);
            if (catchErr)
            {
                return;
            }
            if (retDate)
            {
                return;
            }
            /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　end
            this.MILogic.Update();
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
//            //2013.12.26 touti 起動時並び替えボタンを押すとシステムエラーになって　バグ対応 start 
//            //if (this.MILogic.SearchResult.Rows.Count > 0)
//            if (this.customDataGridView1.Rows.Count > 0)
//            //2013.12.26 touti 起動時並び替えボタンを押すとシステムエラーになって　バグ対応 end 
//            {
                this.customSortHeader1.ShowCustomSortSettingDialog();
//            }
        }

        /// <summary>
        /// 取消(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.MILogic.ClearScreen("ClsSearchCondition");
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
            var sysID = this.OpenPatternIchiran();

            this.MILogic.selectQuery = this.logic.SelectQeury;
            this.MILogic.orderByQuery = this.logic.OrderByQuery;
            this.MILogic.joinQuery = this.logic.JoinQuery;

            if (!string.IsNullOrEmpty(sysID))
            {
                this.ShowData();
            }
        }

        /// <summary>
        /// 検索条件設定(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            //仕様不明なため、未実装。確認用
            MessageBox.Show("検索条件設定画面", "画面遷移");
        }

        /// <summary>
        /// ESCキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXB_PROCESS_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.MILogic.SelectButton();
            //}
        }

        /// <summary>
        /// 一覧明細のダブルクリック制御
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.MILogic.Ichiran_CellDoubleClick(sender, e);
        }

        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            this.OnLoad(e);
        }

        #endregion

        private void cantxt_HaisyutuGyoushaCd_Validating(object sender, CancelEventArgs e)
        {
            this.MILogic.HaisyutuGyoushaCdCheckANDSet(sender, e);
        }

        /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　start
        private void cDtPicker_StartDay_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cDtPicker_EndDay.Text))
            {
                this.cDtPicker_EndDay.IsInputErrorOccured = false;
                this.cDtPicker_EndDay.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void cDtPicker_EndDay_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cDtPicker_StartDay.Text))
            {
                this.cDtPicker_StartDay.IsInputErrorOccured = false;
                this.cDtPicker_StartDay.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141021 Houkakou 「電マニ仮登録」の日付チェックを追加する　end
    }
}
