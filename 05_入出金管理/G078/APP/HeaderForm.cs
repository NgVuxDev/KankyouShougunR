using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        //アラート件数（初期値）
        public Int32 InitialNumberAlert = 0;

        //アラート件数
        public Int32 NumberAlert = 0;

        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //base.lb_title.Text = "入出金一覧";

        }

        //2013.12.23 naitou upd start
        private void alertNumber_Validating(object sender, CancelEventArgs e)
        {
            if (this.alertNumber.Text == "") this.alertNumber.Text = "0";
            this.NumberAlert = Int32.Parse(this.alertNumber.Text);
        }
        
        //private void txtNum_HidukeSentaku_TextChanged(object sender, EventArgs e)
        //{
        //    if ("1".Equals(this.txtNum_HidukeSentaku.Text))
        //    {
        //        //this.radbtn_Nyuukin.Focus();
        //        this.radbtnDenpyouHiduke.Checked = true;
        //    }
        //    if ("2".Equals(this.txtNum_HidukeSentaku.Text))
        //    {
        //        //this.radbtn_Syuutukin.Focus();
        //        this.radbtnNyuuryokuHiduke.Checked = true;
        //    }
        //}

        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnDenpyouHiduke.Checked)
            {
                //this.txtNum_HidukeSentaku.Text = "1";
                this.lab_HidukeNyuuryoku.Text = "伝票日付";
            }
        }

        private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnNyuuryokuHiduke.Checked)
            {
                //this.txtNum_HidukeSentaku.Text = "2";
                this.lab_HidukeNyuuryoku.Text = "入力日付";
            }
        }

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
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

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        //private void KYOTEN_CD_Leave(object sender, EventArgs e)
        //{
        //    int i;
        //    if (!int.TryParse(this.KYOTEN_CD.Text, out i))
        //    {
        //        this.KYOTEN_CD.Text = string.Empty;
        //    }
        //}

        //private void txtNum_HidukeSentaku_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '1')
        //    {
        //        radbtnDenpyouHiduke.Checked = true;
        //        txtNum_HidukeSentaku.SelectAll();
        //        e.Handled = true;
        //    }
        //    if (e.KeyChar == '2')
        //    {
        //        radbtnNyuuryokuHiduke.Checked = true;
        //        txtNum_HidukeSentaku.SelectAll();
        //        e.Handled = true;
        //    }
        //    //txtNum_HidukeSentaku.SelectAll();
        //    //e.Handled = true;
        //}

        //private void alertNumber_TextChanged(object sender, EventArgs e)
        //{
        //    this.NumberAlert = Int32.Parse(this.alertNumber.Text);
        //}
        //2013.12.23 naitou upd end

        #endregion

        private void txtNum_HidukeSentaku_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_HidukeSentaku.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "2");
                e.Cancel = true;
            }
        }

    }
}
