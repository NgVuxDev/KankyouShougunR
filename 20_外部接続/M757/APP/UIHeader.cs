    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.Logic;

namespace Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.APP
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "楽楽明細マスタ一覧";
            this.RAKURAKU_MEISAI_RENKEI.Text = "1";
        }
    }
}
