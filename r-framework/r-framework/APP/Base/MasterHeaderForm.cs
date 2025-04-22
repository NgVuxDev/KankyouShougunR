using System;
using System.Windows.Forms;
using r_framework.Utility;

namespace r_framework.APP.Base
{
    /// <summary>
    /// マスタ画面のヘッダーForm
    /// </summary>
    public partial class MasterHeaderForm : Form
    {
        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 1086;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasterHeaderForm()
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