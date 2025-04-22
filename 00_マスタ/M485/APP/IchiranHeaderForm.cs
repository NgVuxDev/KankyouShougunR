using System;
using r_framework.Utility;

namespace Shougun.Core.Master.HikiaiGyoushaIchiran.APP
{
    /// <summary>
    /// 一覧用ヘッダーForm
    /// </summary>
    public partial class IchiranHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 658;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IchiranHeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        /// <summary>
        /// ラベルタイトルテキストチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }
    }
}
