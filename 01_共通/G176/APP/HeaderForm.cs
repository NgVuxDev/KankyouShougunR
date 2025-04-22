using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Logic;

namespace Shougun.Core.Common.KensakuKekkaIchiran
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

            base.lb_title.Text = "検索結果一覧";
        }
    }
}
