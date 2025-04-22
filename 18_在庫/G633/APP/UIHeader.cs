using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Stock.ZaikoHinmeiHuriwake
{
    public partial class UIHeader : HeaderBaseForm
    {
        public string HinmeiCd { get; private set; }
        public string HinmeiName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <param name="hinmeiName"></param>
        public UIHeader(string hinmeiCd, string hinmeiName) :
            base()
        {
            InitializeComponent();

            this.HinmeiCd = hinmeiCd;
            this.HinmeiName = hinmeiName;

            // Load前に非表示にすれば、タイトルは左に詰まる
            base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
