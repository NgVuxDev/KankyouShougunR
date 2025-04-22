using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.Billing.InxsSeikyuushoHakkou.APP
{
    public partial class UIHeader : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;

        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "請求書発行";
        }

        /// 20141023 Houkakou 「請求書発行」の日付チェックを追加する　start
        private void DenpyouHidukeFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DenpyouHidukeTo.Text))
            {
                this.DenpyouHidukeTo.IsInputErrorOccured = false;
                this.DenpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void DenpyouHidukeTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DenpyouHidukeFrom.Text))
            {
                this.DenpyouHidukeFrom.IsInputErrorOccured = false;
                this.DenpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141023 Houkakou 「請求書発行」の日付チェックを追加する　end
    }
}
