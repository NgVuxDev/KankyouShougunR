using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;

namespace Shougun.Core.SalesPayment.Uriagekakutenyuryoku
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;

            //this.txtNum_HidukeSentaku.Text = "1";
            //this.radbtnDenpyouHiduke.Checked = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "売上確定入力";
            //日付選択の初期値設定
            //this.txtNum_HidukeSentaku.Text = "1";
            //this.radbtnDenpyouHiduke.Checked = true;
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
    }
}
