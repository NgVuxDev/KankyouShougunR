using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku3
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
