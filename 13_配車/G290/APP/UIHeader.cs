using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;
using r_framework.Utility;
using r_framework.Const;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

         
            //読込データ件数の初期値設定
            this.readDataNumber.Text = "0";
        }

        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radbtnDenpyouHiduke.Checked)
            {
                this.lab_HidukeNyuuryoku.Text = "作業日※";
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
                this.lab_HidukeNyuuryoku.Text = "作業日※";
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
            r_framework.CustomControl.CustomNumericTextBox2 obj = (r_framework.CustomControl.CustomNumericTextBox2)sender;
            if (string.IsNullOrEmpty(obj.Text.ToString()))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "2");
                e.Cancel = true;
            }
        }

    }
}
