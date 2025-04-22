using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.PaperManifest.ManifestCheckHyo
{
    public partial class UIHeader : HeaderBaseForm
    {
        public UIHeader()
        {
            this.InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
            //base.windowTypeLabel.Visible = false;
            this.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //base.lb_title.Text = "マニフェストチェック表";
            //this.lb_title.Text = "マニフェストチェック表";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HOGE");
        }
    }
}
