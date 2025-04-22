using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
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

            base.lb_title.Text = "支払明細書発行";
        }

        /// 20141023 Houkakou 「支払明細書発行」の日付チェックを追加する start
        private void tdpDenpyouHidukeFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tdpDenpyouHidukeTo.Text))
            {
                this.tdpDenpyouHidukeTo.IsInputErrorOccured = false;
                this.tdpDenpyouHidukeTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void tdpDenpyouHidukeTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tdpDenpyouHidukeFrom.Text))
            {
                this.tdpDenpyouHidukeFrom.IsInputErrorOccured = false;
                this.tdpDenpyouHidukeFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141023 Houkakou 「支払明細書発行」の日付チェックを追加する end
    }
}
