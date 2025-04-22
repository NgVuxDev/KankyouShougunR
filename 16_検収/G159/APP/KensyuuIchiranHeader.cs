using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Core.Inspection.KensyuuIchiran
{
    public partial class KensyuuIchiranHeader : r_framework.APP.Base.HeaderBaseForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal KensyuuIchiranLogicCls logic { get; set; }

        public KensyuuIchiranHeader()
        {
            InitializeComponent();
            base.windowTypeLabel.Visible = false;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.lb_title.Text = "検収一覧";
        }
    }
}
