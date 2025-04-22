    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Allocation.Teikihaisyajissekihyou
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

//            base.lb_title.Text = ConstCls.HeaderTitle;
            base.lb_title.Text = "定期配車実績表　条件指定";
            
        }
    }
}
