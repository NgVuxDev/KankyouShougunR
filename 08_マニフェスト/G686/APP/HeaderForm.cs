using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Shougun.Core.PaperManifest.ManifestIkkatsuKousin;
using System.Windows.Forms;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.ManifestIkkatsuKousin
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        public HeaderForm()
        {
            InitializeComponent();

            // Load前に非表示にすれば、タイトルは左に詰まる
                 base.windowTypeLabel.Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            base.lb_title.Text = "　"+WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_HENKYAKUBI)+"　";
            //base.lb_title.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HOGE");
        }

        private void HeaderSample_Load(object sender, EventArgs e)
        {

        }
    }
}
