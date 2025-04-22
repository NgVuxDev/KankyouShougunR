using System;
using r_framework.APP.Base;

namespace Shougun.Core.Stock.ZaikoIdouIchiran
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
            this.lb_title.Text = "在庫移動一覧";
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
        #endregion
    }
}
