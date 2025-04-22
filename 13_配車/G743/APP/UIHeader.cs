using r_framework.APP.Base;

namespace Shougun.Core.Allocation.CarTransferSpot
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();
            base.lb_title.Text = "他車振替登録（スポット）";
            base.windowTypeLabel.Visible = false;
        }
    }
}
