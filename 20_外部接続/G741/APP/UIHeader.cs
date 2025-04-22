using r_framework.APP.Base;

namespace Shougun.Core.ExternalConnection.GenbamemoNyuryoku
{
    public partial class UIHeader : HeaderBaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }
    }
}
