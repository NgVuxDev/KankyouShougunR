using System;
using System.Windows.Forms;
using r_framework.Utility;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    /// <summary>
    /// 一覧用ヘッダーForm
    /// </summary>
    [Obsolete("ヘッダーフォームは各画面プロジェクトにて用意します。")]
    public partial class IchiranHeaderForm2 : Form
    {
        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 967;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        [Obsolete("ヘッダーフォームは各画面プロジェクトにて用意します。")]
        public IchiranHeaderForm2()
        {
            InitializeComponent();
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
