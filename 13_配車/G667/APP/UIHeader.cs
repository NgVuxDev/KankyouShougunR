using System;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
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
        }
    }
}
