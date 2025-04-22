using System;
using KyoutsuuIchiran.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill.Attrs;

namespace KyoutsuuIchiran.APP
{
    /// <summary>
    /// 検索一覧画面(共通)
    /// </summary>
    [Implementation]
    public partial class KyoutsuuIchiranForm : SuperForm
    {
        /// <summary>
        /// 検索文字列
        /// </summary>
        private string searchString = string.Empty;

        /// <summary>
        /// 共通一覧画面のロジッククラス
        /// </summary>
        private KyoutsuuIchiranLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public bool SearchFlag { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">画面ID</param>
        /// <param name="searchString">検索文字列</param>
        /// <param name="windowType">画面のタイプ</param>
        public KyoutsuuIchiranForm(WINDOW_ID windowID, WINDOW_TYPE windowType, string searchString)
            : base(windowID, windowType)
        {
            InitializeComponent();

            this.logic = new KyoutsuuIchiranLogic(this);

            base.WindowId = windowID;
            base.WindowType = windowType;
            this.logic.SearchString = searchString;

            SearchFlag = this.Search();    // 一覧取得

            if (!SearchFlag)
            {
                this.Close();
            }
        }
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            bool catchErr = this.logic.WindowInit();

            this.Ichiran.DataSource = this.logic.SearchResult;
        }

        /// <summary>
        /// 閉じるボタン押下時処理
        /// </summary>
        public virtual void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// 検索文字列でデータ取得
        /// </summary>
        public virtual bool Search()
        {
            int count = this.logic.Search();
            if (count <= 0)
            {
                if (count == 0)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("C001");
                }
                return false;
            }

            return true;
        }

    }
}