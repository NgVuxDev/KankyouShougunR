using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill;
using DataGridViewCheckBoxColumnHeeader;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;

namespace Shougun.Core.ElectronicManifest.SousinHoryuSaisyuSyobunhoukoku
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private SousinHoryuSaisyuSyobunhoukoku.LogicClass MILogic;

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
            : base(DENSHU_KBN.SOUSHIN_HORYUU_SAISYUU_HOUKOKU, false)
        {
            this.InitializeComponent();

            // 社員CD
            this.ShainCd = SystemProperty.Shain.CD;
            // 全ユーザー固定の場合、コメントアウトを解除する
            //this.ShainCd = "000001";

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
                this.ParentBaseForm = (BusinessBaseForm)this.Parent;

                this.customDataGridView1.Location = new System.Drawing.Point(3,29);

                this.customDataGridView1.Size = new System.Drawing.Size(997, 402);

                // 非表示列の登録
                this.SetHiddenColumns(this.MILogic.DisableColumnNames);
            }

            this.PatternReload(!this.isLoaded);

            //表示の初期化
            if (!this.MILogic.ClearScreen("Initial"))
            {
                return;
            }

            //検索
            this.MILogic.selectQuery = this.SelectQuery;
            this.MILogic.orderByQuery = this.OrderByQuery;
            this.MILogic.joinQuery = this.JoinQuery;

            //if (this.MILogic.selectQuery != null)
            //{
            //    this.MILogic.Search();
            //}

            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {

                    //this.Table.Rows.Add("", typeof(CheckedListBox));

                    this.logic.CreateDataGridView(this.Table);

                    if (isLoaded == false)
                    {
                        ////選択チェックボックス作成
                        DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                        column.Width = 50;
                        column.DefaultCellStyle.Tag = "処理対象とする場合はチェックしてください";
                        DataGridviewCheckboxHeaderCell newheader = new DataGridviewCheckboxHeaderCell();
                        newheader.ToolTipText = "処理対象とする場合はチェックしてください";
                        newheader.OnCheckBoxClicked += new DataGridViewCheckBoxColumnHeeader.DataGridviewCheckboxHeaderCell.
                                datagridviewcheckboxHeaderEventHander(this.MILogic.ch_OnCheckBoxClicked);
                        column.HeaderCell = newheader;
                        this.customDataGridView1.Columns.Insert(0, column);
                    }

                }
            }

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
        /// パターン1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn1_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// パターン2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn2_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// パターン3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn3_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// パターン4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn4_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// パターン5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn5_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ClearCustomSortSetting();
        }

        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func4_Click(object sender, EventArgs e)
        {
            this.MILogic.delete();
        }

        /// <summary>
        /// CSV出力(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func6_Click(object sender, EventArgs e)
        {
            // 検索結果をCSV出力
            CSVExport export = new CSVExport();
            export.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, "送信保留最終処分終了報告一覧", this);
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            // パターンチェック
            if (this.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            if (this.MILogic.selectQuery != null)
            {
                this.MILogic.Search();
            }
        }

        /// <summary>
        /// JWNET送信(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            this.MILogic.update();
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            ////仕様不明なため、未実装。確認用
            //MessageBox.Show("並列移動", "フォーカス移動");
//            if (this.MILogic.SearchResult.Rows.Count > 0)
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

            if (parentForm != null)
            {
                parentForm.Close();
            }
        }

        /// <summary>
        /// パターン一覧(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            var sysID = this.OpenPatternIchiran();

            this.MILogic.selectQuery = this.SelectQuery;
            this.MILogic.orderByQuery = this.OrderByQuery;
            this.MILogic.joinQuery = this.JoinQuery;

            if (!string.IsNullOrEmpty(sysID))
            {
                if (this.Table != null)
                {
                    this.logic.CreateDataGridView(this.Table);
                }
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

    }
}
