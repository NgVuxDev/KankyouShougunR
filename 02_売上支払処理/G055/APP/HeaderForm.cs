using System;
using System.ComponentModel;
using r_framework.Logic;
using r_framework.Const;

namespace Shougun.Core.SalesPayment.Denpyouichiran
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
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

            base.lb_title.Text = "伝票一覧";
            //読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
        }

        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnDenpyouHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = "伝票日付※";
            }
        }

        private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnNyuuryokuHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = "入力日付※";
            }
            else
            {
                // デフォルトは伝票日付
                this.lab_HidukeNyuuryoku.Text = "伝票日付※";
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
                this.lab_HidukeNyuuryoku.Text = "検収伝票日付※";
            }
            else
            {
                // デフォルトは伝票日付
                this.lab_HidukeNyuuryoku.Text = "伝票日付※";
            }
        }

        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
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
        // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

        private void txtNum_HidukeSentaku_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_HidukeSentaku.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "3");
                e.Cancel = true;
            }
        }

    }
}
