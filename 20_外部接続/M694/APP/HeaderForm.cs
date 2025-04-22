using System;
using r_framework.APP.Base;
using r_framework.Const;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran
{
    public partial class HeaderForm : HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnDenpyouHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = "登録日付※";
            }
        }

        private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnNyuuryokuHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = "更新日付※";
            }
            else
            {
                // デフォルトは伝票日付
                this.lab_HidukeNyuuryoku.Text = "登録日付※";
            }
        }

        /// <summary>
        /// 検収伝票日付選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radbtnKenshuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnKenshuHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = "連携日付※";
            }
            else
            {
                // デフォルトは伝票日付
                this.lab_HidukeNyuuryoku.Text = "更新日付※";
            }
        }

        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_DoubleClick(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.Text = this.HIDUKE_TO.Text;
        }

        private void HIDUKE_FROM_DoubleClick(object sender, EventArgs e)
        {
            this.HIDUKE_TO.Text = this.HIDUKE_FROM.Text;
        }
    }
}
