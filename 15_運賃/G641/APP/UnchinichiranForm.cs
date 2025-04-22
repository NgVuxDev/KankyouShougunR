using System;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.IchiranCommon.Logic;
using System.Windows.Forms;

namespace Shougun.Core.Carriage.Unchinichiran
{
    [Implementation]
    public partial class UnchinichiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        private Unchinichiran.LogicClass DenpyouichiranLogic;

        HeaderForm header_new;

        private Boolean isLoaded;

        #endregion

        /// <summary>
        /// 画面ロジック
        /// </summary>
        //private r_framework.Logic.IBuisinessLogic logic;

        public UnchinichiranForm(DENSHU_KBN denshuKbn, string searchString, HeaderForm headerFor, string txt_SyainCode)
            : base(DENSHU_KBN.DENPYOU_ICHIRAN, false)
        {
            this.InitializeComponent();

            this.header_new = headerFor;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.DenpyouichiranLogic = new LogicClass(this);
            if (!string.IsNullOrEmpty(searchString))
            {
                string getSearchString = searchString.Replace("\r", "").Replace("\n", "");
                //検索対象文字列取得
                this.DenpyouichiranLogic.searchString = getSearchString;
            }

            DenpyouichiranLogic.SetHeader(header_new);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //社員コードを取得すること
            this.ShainCd = txt_SyainCode;
            //Main画面で社員コード値を取得すること
            DenpyouichiranLogic.syainCode = txt_SyainCode;
            //伝種区分を取得すること
            DenpyouichiranLogic.denShu_Kbn = denshuKbn;

            //タブオーダー設定
            this.customDataGridView1.TabIndex = 23;

            this.customDataGridView1.Location = new System.Drawing.Point(this.customDataGridView1.Location.X, this.customDataGridView1.Location.Y - 55);
            this.customDataGridView1.Height += 55;
            isLoaded = false;
        }

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 画面情報の初期化
            if (!isLoaded)
            {
                this.DenpyouichiranLogic.WindowInit();

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 80);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 102);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 129);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 301);

                // ヘッダー名チェック
                if (String.IsNullOrEmpty(this.txtNum_DenpyoKind.Text))
                {
                    this.DenpyouichiranLogic.disp_Flg = 0;
                }
                else
                {
                    this.DenpyouichiranLogic.disp_Flg = int.Parse(this.txtNum_DenpyoKind.Text);
                    this.header_new.lb_title.Text = this.DenshuKbn.ToTitleString();
                    this.Parent.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.header_new.lb_title.Text);
                }
                this.DenshuKbn = DENSHU_KBN.UNCHIN_ICHIRAN;
                this.PatternUpdate();
            }

            isLoaded = true;

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }

            //thongh 2015/10/16 #13526 start
            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.header_new.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header_new.ReadDataNumber.Text = "0";
            }
            //thongh 2015/10/16 #13526 end

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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

            base.OnShown(e);
        }
        #endregion

        /// <summary>
        /// パターン再表示（グリッドビューは更新しない）
        /// </summary>
        public void PatternUpdate()
        {
            // ロジッククラスを初期化（伝種区分を更新するため）
            this.logic = new IchiranBaseLogic(this);
            this.PatternReload(true);
            this.DenpyouichiranLogic.HideColumnHeader();
        }

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

        public virtual void ShowData()
        {
            this.Table = this.DenpyouichiranLogic.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
                this.DenpyouichiranLogic.HideColumnHeader();
            }
        }

        #endregion

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
    }
}
