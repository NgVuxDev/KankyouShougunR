using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.Const;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi
{
    public partial class UIHeader : HeaderBaseForm
    {
        //アラート件数（初期値）
        public Int32 InitialNumberAlert = 0;

        //アラート件数
        public Int32 NumberAlert = 0;

        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        //2013.12.23 naitou upd start
        private void alertNumber_Validating(object sender, CancelEventArgs e)
        {
            if (this.alertNumber.Text == "") this.alertNumber.Text = "0";
            this.NumberAlert = Int32.Parse(this.alertNumber.Text);
        }
        //2013.12.23 naitou upd end

        #endregion
    }
}
