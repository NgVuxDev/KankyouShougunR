using System;

namespace Shougun.Core.BusinessManagement.ShouninzumiDenshiShinseiIchiran
{
    /// <summary>
    /// G561 承認済申請一覧ヘッダ画面クラス
    /// </summary>
    public partial class ShouninzumiDenshiShinseiIchiranHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShouninzumiDenshiShinseiIchiranHeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.readDataNumber.Text = "0";
        }
    }
}
