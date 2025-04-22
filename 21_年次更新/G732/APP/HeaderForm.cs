using System;
using r_framework.APP.Base;

namespace Shougun.Core.AnnualUpdates.AnnualUpdatesDEL
{
    public partial class HeaderForm : HeaderBaseForm
    {

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;

        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
