using r_framework.APP.Base;

namespace Shougun.Core.Allocation.CarTransferTeiki
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();
            base.lb_title.Text = "他車振替登録（定期配車）";
            base.windowTypeLabel.Visible = false;
        }
    }
}
