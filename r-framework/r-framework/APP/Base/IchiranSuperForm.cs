using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Logic;

namespace r_framework.APP.Base
{
    [Obsolete("IchiranCommonプロジェクトの同名クラスを使用します。")]
    public partial class IchiranSuperForm : SuperForm
    {
        /// <summary>
        /// 社員コード
        /// </summary>
        public string ShainCd { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN DenshuKbn { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 実施パターン番号
        /// </summary>
        public int PatternNo { get; set; }

        /// <summary>
        /// 検索条件設定文字列（ＳＱＬ文を想定）
        /// </summary>
        public string SerachSetting { get; set; }

        /// <summary>
        /// 単純系の検索条件文字列
        /// </summary>
        public string SimpleSearchSettings { get; set; }

        /// <summary>
        /// 一覧のロジック処理
        /// </summary>
        public IchiranBaseLogic logic { get; protected set; }

        /// <summary>
        /// 表示用データ格納変数
        /// </summary>
        public DataTable Table { get; set; }

        /// <summary>
        /// SELECT句を保持するプロパティ
        /// </summary>
        public string SelectQuery
        {
            get { return this.logic.SelectQeury; }
        }

        /// <summary>
        /// ORDER BY句を保持するプロパティ
        /// </summary>
        public string OrderByQuery
        {
            get { return this.logic.OrderByQuery; }
        }

        /// <summary>
        /// 最初のLoadイベントかどうか
        /// </summary>
        private bool isFirstLoad = true;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IchiranSuperForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        public IchiranSuperForm(DENSHU_KBN denshuKbn)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.customDataGridView1.IsBrowsePurpose = true;
                this.DenshuKbn = denshuKbn;
                this.PatternNo = 0;
                this.logic = new IchiranBaseLogic(this);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IchiranSuperForm"/> class.
        /// </summary>
        /// <param name="denshuKbn">denshuKbn</param>
        /// <param name="isDataload">データの読み込みをSuperForm側で行うかどうかを表す値</param>
        public IchiranSuperForm(DENSHU_KBN denshuKbn, bool isDataload)
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                this.customDataGridView1.IsBrowsePurpose = true;
                this.DenshuKbn = denshuKbn;
                this.PatternNo = 0;
                this.logic = new IchiranBaseLogic(this, isDataload);
            }
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(System.EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            if (isFirstLoad)
            {
                base.OnLoad(e);
                isFirstLoad = false;
            }

            var parentForm = base.Parent;
            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
            var alertNo = (TextBox)controlUtil.FindControl(parentForm, "alertNumber");
            titleControl.Text = this.DenshuKbn.ToTitleString();
            // タイトルはフォームヘッダーの画面タイトル文字列と同じであること
            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.DenshuKbn.ToTitleString());

            if (alertNo != null)
            {
                this.logic.AlertCount = string.IsNullOrEmpty(alertNo.Text) ? 0 : int.Parse(alertNo.Text, NumberStyles.AllowThousands);
            }
            this.logic.PatternInit();
            this.logic.SetPatternButton();
            this.logic.SortHeaderInit();
            this.PatternButtonInit();

            var readDataNumber = (TextBox)controlUtil.FindControl(parentForm, "ReadDataNumber");

            if (Table != null && readDataNumber != null)
            {
                readDataNumber.Text = Table.Rows.Count.ToString();
            }
        }

        /// <summary>
        /// パターンボタン押下時処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        private void PatternButtonClick(object sender, System.EventArgs e)
        {
            var button = (Button)sender;

            if (!string.IsNullOrEmpty(button.Text))
            {
                this.PatternNo = int.Parse(button.Name.Replace("bt_ptn", ""));
            }
            else
            {
                this.PatternNo = 0;
            }
            this.OnLoad(e);
        }

        /// <summary>
        /// パターンボタンイベント追加処理
        /// </summary>
        public void PatternButtonInit()
        {
            var btnList = new List<CustomControl.CustomButton>() { this.bt_ptn1, this.bt_ptn2, this.bt_ptn3, this.bt_ptn4, this.bt_ptn5 };

            btnList.ForEach(btn =>
                {
                    btn.Click -= new EventHandler(PatternButtonClick);
                    btn.Click += new EventHandler(PatternButtonClick);
                }
            );
        }

        private void IchiranSuperForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
