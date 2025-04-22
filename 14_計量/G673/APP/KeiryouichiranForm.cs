using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.IchiranCommon.Logic;
using System.Windows.Forms;

namespace Shougun.Core.Scale.KeiryouIchiran
{
    [Implementation]
    internal partial class KeiryouIchiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド

        private KeiryouIchiran.LogicClass KeiryouIchiranLogic;

        private HeaderForm header_new;

        private Boolean isLoaded;

        /// <summary>
        /// ベースロジックで作成したSELECTクエリ
        /// </summary>
        internal string baseSelectQuery = string.Empty;

        /// <summary>
        /// ベースロジックで作成したORDER BYクエリ
        /// </summary>
        internal string baseOrderByQuery = string.Empty;

        /// <summary>
        /// ベースロジックで作成したJOINクエリ
        /// </summary>
        internal string baseJoinQuery = string.Empty;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerFor"></param>
        internal KeiryouIchiranForm(HeaderForm headerFor)
            : base(DENSHU_KBN.KEIRYOU, false)
        {
            this.InitializeComponent();

            this.header_new = headerFor;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.KeiryouIchiranLogic = new LogicClass(this);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            KeiryouIchiranLogic.SetHeader(header_new);

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);

            //タブオーダー設定
            this.customDataGridView1.TabIndex = 830;

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
                if (!this.KeiryouIchiranLogic.WindowInit())
                {
                    return;
                }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 140);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 167);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 196);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 234);

                // ヘッダー名チェック
                this.DenshuKbn = DENSHU_KBN.KEIRYOU;

                this.header_new.lb_title.Text = this.DenshuKbn.ToTitleString();
                this.Parent.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.header_new.lb_title.Text);

                this.PatternUpdate();

            }

            isLoaded = true;

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            // ベースロジックで作成したクエリを一旦保存
            this.baseSelectQuery = this.logic.SelectQeury;
            this.baseOrderByQuery = this.logic.OrderByQuery;
            this.baseJoinQuery = this.logic.JoinQuery;

            //検索処理
            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }

            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.header_new.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header_new.ReadDataNumber.Text = "0";
            }

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

        /// <summary>
        /// 日付FROM Leave処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        /// <summary>
        /// 日付TO Leave処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        #endregion

        /// <summary>
        /// パターン再表示（グリッドビューは更新しない）
        /// </summary>
        private void PatternUpdate()
        {
            // ロジッククラスを初期化（伝種区分を更新するため）
            this.logic = new IchiranBaseLogic(this);
            this.PatternReload(true);
            this.KeiryouIchiranLogic.HideColumnHeader();

            // ベースロジックで作成したクエリを一旦保存
            this.baseSelectQuery = this.logic.SelectQeury;
            this.baseOrderByQuery = this.logic.OrderByQuery;
            this.baseJoinQuery = this.logic.JoinQuery;
        }

        #region 検索結果表示

        /// <summary>
        /// データ表示
        /// </summary>
        internal virtual void ShowData()
        {
            this.Table = this.KeiryouIchiranLogic.searchResult;

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
                this.KeiryouIchiranLogic.HideColumnHeader();
            }
        }

        #endregion

    }
}
