using System;

namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    public partial class UIHeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public UIHeaderForm()
        {
            InitializeComponent();
            base.windowTypeLabel.Visible = false;

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //base.lb_title.Text = ConstCls.HeaderTitle;
            base.lb_title.Text = "運賃入力";

        }

       
    }
}
