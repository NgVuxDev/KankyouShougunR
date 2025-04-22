using System;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.IchiranCommon.APP;
using System.Drawing;

namespace Shougun.Core.Common.KensakuKekkaIchiran
{
    [Implementation]
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        private KensakuKekkaIchiran.LogicClass kensakuLogic;

        private string selectQuery = string.Empty;

        private string orderByQuery = string.Empty;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UIForm"/> class.
        /// </summary>
        public UIForm(DENSHU_KBN denshuKbn, string searchString)
            : base(denshuKbn, false)
        {
            this.InitializeComponent();

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
            this.kensakuLogic = new LogicClass(this);
            if (!string.IsNullOrEmpty(searchString))
            {
                string getSearchString = searchString.Replace("\r", "").Replace("\n", "");
                kensakuLogic.SearchString = getSearchString;  //検索対象文字列取得
                this.SearchString = new string[] { searchString, getSearchString };
                this.SearchValue.Text = getSearchString;
            }
        }

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!isLoaded)
            {
                // 画面情報の初期化
                this.kensakuLogic.WindowInit();

                this.customSearchHeader1.Visible = true;

                this.customSearchHeader1.Location = new Point(4, 31);
                this.customSortHeader1.Location = new Point(4, 59);
                this.customDataGridView1.Location = new Point(3, 87);
                this.customDataGridView1.Size = new Size(997, 333);
            }

            // パターンリロード(初回のみデフォルトパターン読み込み)
            this.PatternReload(!this.isLoaded);

            //検索処理
            //kensakuLogic.Search();

            if (this.isLoaded)
            {
                if (!this.DesignMode)
                {
                    this.customDataGridView1.DataSource = null;
                    if (this.Table != null)
                    {
                        this.logic.CreateDataGridView(this.Table);

                    }
                }
            }

            //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 start
            //並び順ソートヘッダー
            this.customSortHeader1.ClearCustomSortSetting();
            //2013-12-25 Add touti PT 電マニ No.620 横展開 追加 end

            isLoaded = true;
            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();
        }

        /// <summary>
        /// 初回表出イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            if (this.PatternNo > 0)
            {
                // 20140627 syunrei EV004040_検索結果一覧で検索結果が無い時にアラートが表示されない　start
                int cnt = this.kensakuLogic.Search();
                if (cnt <= 0)
                {
                    var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                    return;
                }
                // 20140627 syunrei EV004040_検索結果一覧で検索結果が無い時にアラートが表示されない　end
            }
            else
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }
        }

        #endregion

        #region 並替移動
        /// <summary>
        /// 並替移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MoveToSort(object sender, EventArgs e)
        {
            this.customSortHeader1.Focus();
        }
        #endregion

        #region 検索結果表示
        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ShowData()
        {
            //this.Table = this.kensakuLogic.SearchResult;
            //this.logic.AlertCount = this.kensakuLogic.alertCount;
            //if (!this.DesignMode)
            //{
            //    this.customDataGridView1.DataSource = null;
            //    this.logic.CreateDataGridView(this.Table);
            //}

            this.Table = this.kensakuLogic.SearchResult;
            this.logic.CreateDataGridView(this.Table);
            this.HideSystemColumn();
        }

        /// <summary>
        /// システム上の必須列を非表示にする
        /// </summary>
        private void HideSystemColumn()
        {
            if (this.customDataGridView1.Columns.Contains(this.kensakuLogic.HIDDEN_TORIHIKISAKI_CD))
            {
                this.customDataGridView1.Columns[this.kensakuLogic.HIDDEN_TORIHIKISAKI_CD].Visible = false;
            }
            if (this.customDataGridView1.Columns.Contains(this.kensakuLogic.HIDDEN_GYOUSHA_CD))
            {
                this.customDataGridView1.Columns[this.kensakuLogic.HIDDEN_GYOUSHA_CD].Visible = false;
            }
            if (this.customDataGridView1.Columns.Contains(this.kensakuLogic.HIDDEN_GENBA_CD))
            {
                this.customDataGridView1.Columns[this.kensakuLogic.HIDDEN_GENBA_CD].Visible = false;
            }
        }

        #endregion

        /// <summary>
        /// 検索文字列
        /// </summary>
        public string[] SearchString { get; private set; }

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
    }
}
