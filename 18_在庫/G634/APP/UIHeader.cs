using System;
using System.Windows.Forms;
using r_framework.APP.Base;

namespace Shougun.Core.Stock.ZaikoKanriHyo
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

            base.lb_title.Text = "在庫管理表";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HOGE");
        }
    }
}
