using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using System.Windows.Forms;


namespace Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            //InitializeComponent();
            base.windowTypeLabel.Visible = false;
         
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.lb_title.Width = base.lb_title.Width + 120;
        }
    }
}
