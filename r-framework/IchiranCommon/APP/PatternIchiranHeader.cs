using System;
using r_framework.APP.Base;
using Shougun.Core.Common.IchiranCommon.Const;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    public partial class PatternIchiranHeader : HeaderBaseForm
    {
        public PatternIchiranHeader()
        {
            InitializeComponent();

            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = PatternIchiranConst.HeaderTitle;
        }
    }
}
