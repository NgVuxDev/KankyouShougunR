// $Id: HeaderForm.cs 32948 2014-10-21 09:06:54Z huangxy@oec-h.com $
using System;
using r_framework.Const;

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku
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
            //読込データ件数の初期値設定
            this.ReadDataNumber.Text = "0";
        }

        private void txtNum_HidukeSentaku_TextChanged(object sender, EventArgs e)
        {
            if ("1".Equals(this.txtNum_HidukeSentaku.Text))
            {
                this.lab_HidukeNyuuryoku.Text = "伝票日付";
            }
            if ("2".Equals(this.txtNum_HidukeSentaku.Text))
            {
                this.lab_HidukeNyuuryoku.Text = "入力日付";
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
    }
}
